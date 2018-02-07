using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Transactions;
using DXInfo.Models;

namespace DXInfo.Business
{
    public class RetailTemp
    {
        public Guid ConsumeListId { get; set; }
        //public Guid WhId { get; set; }
        public Guid InvId { get; set; }
        public string Batch { get; set; }
        public decimal Price { get; set; }
        public decimal Num { get; set; }
        public Guid UserId { get; set; }
        public Guid DeptId { get; set; }
        public DateTime CreateDate { get; set; }
        public DXInfo.Models.CurrentStock CurStock { get; set; }
        public Guid? RdsId { get; set; }              
    }
    public class StockManageFacade
    {
        #region 字段
        private IFairiesMemberManageUow uow;
        private DXInfo.Business.Common common;
        private bool isShelfLife;
        private bool isLocator;
        private Guid operId;
        private Guid deptId;
        private Guid? orgId;
        private string deptCode;
        private string operCode;
        private int vouchAuthority;
        #endregion

        #region 构造
        public StockManageFacade(IFairiesMemberManageUow uow, Guid operId, Guid deptId, Guid? orgId,string deptCode,string operCode)//, int vouchAuthority)
        {
            this.uow = uow;
            this.operId = operId;
            this.deptId = deptId;
            this.orgId = orgId;
            this.deptCode = deptCode;
            this.operCode = operCode;
            //this.vouchAuthority = vouchAuthority;
            this.common = new Common(uow,operId,deptId,orgId,deptCode,operCode);//,vouchAuthority);
            this.vouchAuthority = this.common.vouchAuthority;
            this.isShelfLife = this.common.IsShelfLife();
            this.isLocator = this.common.IsLocator();
        }
        #endregion

        #region 零售批量出库
        private bool RetailUpdateCurrentStock(Guid curStockId, int rdFlag, decimal num, decimal quantity)
        {
            DXInfo.Models.CurrentStock oldCurStock = uow.CurrentStock.GetById(g => g.Id == curStockId);
            if (oldCurStock == null)
            {
                return false;
            }
            if (rdFlag == 1)
            {
                if (num > oldCurStock.Num)
                {
                    return false;
                }
            }
            oldCurStock.Quantity = rdFlag == 0 ? oldCurStock.Quantity + quantity : oldCurStock.Quantity - quantity;
            oldCurStock.Num = rdFlag == 0 ? oldCurStock.Num + num : oldCurStock.Num - num;
            oldCurStock.Amount = oldCurStock.Price * oldCurStock.Num;
            uow.CurrentStock.Update(oldCurStock);
            uow.Commit();
            return true;
        }
        private bool RetailUpdateCurrentInvLocator(Guid whId, Guid invId, string batch, int rdFlag, decimal num, decimal quantity)
        {
            DXInfo.Models.CurrentInvLocator oldCurInvLocator;
            if (string.IsNullOrEmpty(batch))
            {
                oldCurInvLocator = uow.CurrentInvLocator.GetAll()
                    .Where(w => w.WhId == whId &&
                        w.InvId == invId &&
                        w.Batch == null &&
                        w.Num > num).OrderBy(o => o.Num).FirstOrDefault();
            }
            else
            {
                oldCurInvLocator = uow.CurrentInvLocator.GetAll()
                    .Where(w => w.WhId == whId &&
                        w.InvId == invId &&
                        w.Batch == batch &&
                        w.Num > num).OrderBy(o => o.Num).FirstOrDefault();
            }

            if (oldCurInvLocator == null)
            {
                return false;
            }
            if (rdFlag == 1)
            {
                if (num > oldCurInvLocator.Num)
                {
                    return false;
                }
            }
            oldCurInvLocator.Quantity = rdFlag == 0 ? oldCurInvLocator.Quantity + quantity : oldCurInvLocator.Quantity - quantity;
            oldCurInvLocator.Num = rdFlag == 0 ? oldCurInvLocator.Num + num : oldCurInvLocator.Num - num;
            oldCurInvLocator.Amount = oldCurInvLocator.Price * oldCurInvLocator.Num;
            uow.CurrentInvLocator.Update(oldCurInvLocator);
            return true;
        }
        private bool RetailUpdateCurrent(DXInfo.Models.CurrentStock oldCurStock,int rdFlag, decimal num, decimal quantity)
        {
            bool ret = true;
            ret = RetailUpdateCurrentStock(oldCurStock.Id, rdFlag, num, quantity);
            if (ret && isLocator)
            {
                ret = RetailUpdateCurrentInvLocator(oldCurStock.WhId, oldCurStock.InvId, oldCurStock.Batch, rdFlag, num, quantity);
            }
            uow.Commit();
            return ret;
        }
        private void AddRetailRdRecords(Guid consumeListId, Guid rdId, int rdFlag, Guid invId,
            Guid mainUnit, Guid sTUnit, decimal exchRate,string batch,decimal num,
            decimal price, DXInfo.Models.CurrentStock oldCurStock)
        {
            DXInfo.Models.RdRecords rdRecords;
            if (string.IsNullOrEmpty(batch))
            {
                rdRecords = uow.RdRecords.GetAll().Where(w => w.RdId == rdId &&
                    w.InvId == invId &&
                    w.Price == price).FirstOrDefault();
            }
            else
            {
                rdRecords = uow.RdRecords.GetAll().Where(w => w.RdId == rdId &&
                    w.InvId == invId &&
                    w.Batch == batch).FirstOrDefault();
            }
            bool isExist = true;
            if (rdRecords != null)
            {                
                rdRecords.Num = rdRecords.Num + num;
            }
            else
            {
                isExist = false;
                rdRecords = new DXInfo.Models.RdRecords();
                rdRecords.RdId = rdId;
                rdRecords.InvId = invId;
                rdRecords.Num = num;
                rdRecords.Price = price;
                rdRecords.Batch = batch;
            }
            rdRecords.MainUnit = mainUnit;
            rdRecords.STUnit = sTUnit;
            rdRecords.ExchRate = exchRate;
            rdRecords.Amount = rdRecords.Num * price;

            if (isShelfLife)
            {
                if (oldCurStock != null)
                {
                    rdRecords.MadeDate = oldCurStock.MadeDate;
                    rdRecords.ShelfLife = oldCurStock.ShelfLife;
                    rdRecords.ShelfLifeType = oldCurStock.ShelfLifeType;
                    rdRecords.InvalidDate = oldCurStock.InvalidDate;
                }
            }

            if (!isExist)
            {
                uow.RdRecords.Add(rdRecords);
            }
            else
            {
                uow.RdRecords.Update(rdRecords);
            }
            uow.Commit();
            AddConsumeListRds(consumeListId, rdRecords.Id);
        }
        private DXInfo.Models.CurrentStock GetCurOutStock(Guid invId, string batch,decimal num)
        {
            DXInfo.Models.CurrentStock oldCurStock=null;
            if (string.IsNullOrEmpty(batch))
            {
                oldCurStock = (from d in uow.CurrentStock.GetAll()
                               join d1 in uow.Warehouse.GetAll() on d.WhId equals d1.Id into dd1
                               from dd1s in dd1.DefaultIfEmpty()
                               where dd1s.Dept == deptId && d.InvId == invId && d.Batch == null && d.Num > num
                               orderby d.Num
                               select d).FirstOrDefault();
            }
            else
            {
                oldCurStock = (from d in uow.CurrentStock.GetAll()
                               join d1 in uow.Warehouse.GetAll() on d.WhId equals d1.Id into dd1
                               from dd1s in dd1.DefaultIfEmpty()
                               where dd1s.Dept == deptId && d.InvId == invId && d.Batch == batch && d.Num > num
                               orderby d.Num
                               select d).FirstOrDefault();
            }
            return oldCurStock;
        }
        private DXInfo.Models.CurrentStock GetCurInStock(Guid rdsId)
        {
            DXInfo.Models.CurrentStock oldCurStock = null;
            DXInfo.Models.RdRecords rds = uow.RdRecords.GetById(g => g.Id == rdsId);
            if (rds != null)
            {
                DXInfo.Models.RdRecord rd = uow.RdRecord.GetById(g => g.Id == rds.RdId);
                if (rd != null)
                {
                    if (string.IsNullOrEmpty(rds.Batch))
                    {
                        oldCurStock = (from d in uow.CurrentStock.GetAll()
                                       where d.WhId == rd.WhId && d.InvId == rds.InvId && d.Batch == null
                                       orderby d.Num
                                       select d).FirstOrDefault();
                    }
                    else
                    {
                        oldCurStock = (from d in uow.CurrentStock.GetAll()
                                       where d.WhId == rd.WhId && d.InvId == rds.InvId && d.Batch == rds.Batch
                                       orderby d.Num
                                       select d).FirstOrDefault();
                    }
                    if (oldCurStock == null)
                    {
                        oldCurStock = new DXInfo.Models.CurrentStock();
                        oldCurStock.WhId = rd.WhId;
                        oldCurStock.InvId = rds.InvId;
                        oldCurStock.MainUnit = rds.MainUnit;
                        oldCurStock.STUnit = rds.STUnit;
                        oldCurStock.ExchRate = rds.ExchRate;
                        oldCurStock.Quantity = rds.Quantity;
                        oldCurStock.Num = rds.Num;
                        oldCurStock.Batch = rds.Batch;
                        oldCurStock.Price = rds.Price;
                        oldCurStock.Amount = oldCurStock.Num * oldCurStock.Price;
                        oldCurStock.InvalidDate = rds.InvalidDate;
                        oldCurStock.MadeDate = rds.MadeDate;
                        oldCurStock.ShelfLife = rds.ShelfLife;
                        oldCurStock.ShelfLifeType = rds.ShelfLifeType;
                        uow.CurrentStock.Add(oldCurStock);
                        uow.Commit();
                    }
                }
            }
            return oldCurStock;
        }
        
        private DXInfo.Models.RdRecord NewSaleOutStock(DateTime dtNow, Guid whId, bool isVerify)
        {
            DXInfo.Models.RdRecord rdRecord = new DXInfo.Models.RdRecord();
            rdRecord.Code = common.GetVouchCode(DXInfo.Models.VouchTypeCode.SaleOutStock);
            rdRecord.VouchType = DXInfo.Models.VouchTypeCode.SaleOutStock;
            rdRecord.Maker = operId;
            rdRecord.MakeDate = dtNow;
            rdRecord.MakeTime = dtNow;
            rdRecord.RdFlag = 1;
            rdRecord.RdCode = DXInfo.Models.VouchTypeCode.SaleOutStock;
            rdRecord.DeptId = deptId;
            rdRecord.STCode = "002";
            rdRecord.RdDate = dtNow;
            rdRecord.Salesman = operId;
            rdRecord.WhId = whId;
            rdRecord.BusType = DXInfo.Models.VouchTypeCode.SaleOutStock;
            rdRecord.IsVerify = isVerify;
            if (isVerify)
            {
                rdRecord.Verifier = operId;
                rdRecord.VerifyDate = dtNow;
                rdRecord.VerifyTime = dtNow;
            }
            return rdRecord;
        }
        private DXInfo.Models.RdRecord NewOtherInStock(DateTime dtNow, Guid whId, bool isVerify)
        {
            DXInfo.Models.RdRecord rdRecord = new DXInfo.Models.RdRecord();
            rdRecord.Code = common.GetVouchCode(DXInfo.Models.VouchTypeCode.OtherInStock);
            rdRecord.VouchType = DXInfo.Models.VouchTypeCode.OtherInStock;
            rdRecord.Maker = operId;
            rdRecord.MakeDate = dtNow;
            rdRecord.MakeTime = dtNow;
            rdRecord.RdFlag = 0;
            rdRecord.RdCode = "013";//零售退货入库
            rdRecord.DeptId = deptId;
            rdRecord.RdDate = dtNow;
            rdRecord.Salesman = operId;
            rdRecord.WhId = whId;
            rdRecord.BusType = "013";
            rdRecord.IsVerify = isVerify;
            if (isVerify)
            {
                rdRecord.Verifier = operId;
                rdRecord.VerifyDate = dtNow;
                rdRecord.VerifyTime = dtNow;
            }
            return rdRecord;
        }
        private void AddConsumeListRds(Guid consumeListId, Guid rdsId)
        {
            DXInfo.Models.ConsumeListRds clrds = uow.ConsumeListRds.GetById(g => g.ConsumeListId == consumeListId);
            if (clrds == null)
            {
                clrds = new ConsumeListRds();
                clrds.ConsumeListId = consumeListId;
                clrds.RdsId = rdsId;
                uow.ConsumeListRds.Add(clrds);
            }
        }
        private void RetailSaleOutStock(List<RetailTemp> q1)
        {
            var g1 = (from d in q1
                      group d by new { d.DeptId,d.CurStock.WhId, d.UserId,d.CreateDate.Date } into g
                      select new { g.Key.DeptId,g.Key.WhId, g.Key.UserId,g.Key.Date }).ToList();
            foreach (var rd in g1)
            {
                if (common.IsBalance(rd.Date, rd.WhId)) continue;
                var lRdRecord = uow.RdRecord.GetAll().Where(w =>
                w.WhId == rd.WhId &&
                w.DeptId == rd.DeptId &&
                w.Maker == rd.UserId &&
                w.RdDate == rd.Date &&
                w.STCode == "002" &&
                w.RdCode == "002" &&
                w.BusType == "002" &&
                w.VouchType == DXInfo.Models.VouchTypeCode.SaleOutStock).ToList();
                var rdRecord = lRdRecord.Where(w => w.IsVerify).FirstOrDefault();
                if (rdRecord == null)
                {
                    rdRecord = NewSaleOutStock(rd.Date, rd.WhId, true);
                    uow.RdRecord.Add(rdRecord);
                    uow.Commit();
                }
                var q3 = (from d in q1
                          where d.DeptId == rd.DeptId && d.CurStock.WhId == rd.WhId && d.UserId == rd.UserId
                          select d).ToList();
                foreach (var rds in q3)
                {
                    decimal exchRate = 1;
                    decimal quantity = 0;
                    Guid sTUnit = Guid.Empty;
                    DXInfo.Models.Inventory inv = uow.Inventory.GetById(g => g.Id == rds.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        quantity = rds.Num;
                        sTUnit = inv.MainUnit;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue) continue;
                        DXInfo.Models.UnitOfMeasures uom = uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        sTUnit = inv.StockUnit.Value;
                        exchRate = uom.Rate;
                        quantity = rds.Num * uom.Rate;
                    }
                    bool ret = RetailUpdateCurrent(rds.CurStock, 1, rds.Num, quantity);
                    if (ret)
                    {
                        AddRetailRdRecords(rds.ConsumeListId, rdRecord.Id, 1,
                            rds.InvId, inv.MainUnit, sTUnit, exchRate, rds.Batch, rds.Num, rds.Price, rds.CurStock);
                    }                   
                }
                uow.Commit();
            }
        }
        private void RetailOtherInStock(List<RetailTemp> q2)
        {
            var g2 = (from d in q2
                      group d by new { d.DeptId, d.CurStock.WhId, d.UserId,d.CreateDate.Date } into g
                      select new { g.Key.DeptId, g.Key.WhId, g.Key.UserId,g.Key.Date }).ToList();
            foreach (var rd in g2)
            {
                if (common.IsBalance(rd.Date, rd.WhId)) continue;
                var lRdRecord = uow.RdRecord.GetAll().Where(w =>
                w.WhId == rd.WhId &&
                w.DeptId == rd.DeptId &&
                w.Maker == rd.UserId &&
                w.RdDate == rd.Date &&
                w.VouchType == DXInfo.Models.VouchTypeCode.OtherInStock &&
                w.RdCode == "013" &&
                w.BusType == "013").ToList();
                var rdRecord = lRdRecord.Where(w => w.IsVerify).FirstOrDefault();
                if (rdRecord == null)
                {
                    rdRecord = NewOtherInStock(rd.Date, rd.WhId, true);
                    uow.RdRecord.Add(rdRecord);
                    uow.Commit();
                }
                var q3 = (from d in q2
                          where d.DeptId == rd.DeptId && d.CurStock.WhId == rd.WhId && d.UserId == rd.UserId
                          select d).ToList();
                foreach (var rds in q3)
                {
                    decimal exchRate = 1;
                    decimal quantity = 0;
                    Guid sTUnit = Guid.Empty;
                    DXInfo.Models.Inventory inv = uow.Inventory.GetById(g => g.Id == rds.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        quantity = rds.Num;
                        sTUnit = inv.MainUnit;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue) continue;
                        DXInfo.Models.UnitOfMeasures uom = uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        sTUnit = inv.StockUnit.Value;
                        exchRate = uom.Rate;
                        quantity = rds.Num * uom.Rate;
                    }
                    bool ret = RetailUpdateCurrent(rds.CurStock, 1, rds.Num, quantity);
                    if (ret)
                    {
                        AddRetailRdRecords(rds.ConsumeListId, rdRecord.Id, 1,
                            rds.InvId, inv.MainUnit, sTUnit, exchRate, rds.Batch, rds.Num, rds.Price, rds.CurStock);
                    }
                }
                uow.Commit();
            }
        }
        /// <summary>
        /// 零售-销售出库单，撤销-其它入库单
        /// 库存自动下无批号，若要下批号需设置各个批号的存货单价
        /// </summary>
        public void BatchRetailStock()
        {
            try
            {
                TransactionScope transaction;
                //销售出库单                
                using (transaction = new TransactionScope())
                {
                    var q1 = (from d in uow.ConsumeList.GetAll()
                              join d1 in uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d2 in uow.ConsumeInvPrice.GetAll() on d.Id equals d2.ConsumeListId into dd2
                              from dd2s in dd2.DefaultIfEmpty()
                              join d3 in uow.ConsumeListRds.GetAll() on d.Id equals d3.ConsumeListId into dd3
                              from dd3s in dd3.DefaultIfEmpty()                              
                              where d.IsValid && dd3s == null && dd1s.InvType==2
                              select new RetailTemp()
                              {
                                  ConsumeListId = d.Id,
                                  InvId = d.Inventory,
                                  Batch = dd2s == null ? "" : dd2s.Code,
                                  Price = d.Price,
                                  Num = d.Quantity,
                                  UserId = d.UserId,
                                  DeptId = d.DeptId,
                                  CreateDate = d.CreateDate,
                                  RdsId = dd3s.RdsId,
                              }).ToList();
                    foreach (RetailTemp tmp in q1)
                    {
                        tmp.CurStock = this.GetCurOutStock(tmp.InvId, tmp.Batch, tmp.Num);
                    }
                    RetailSaleOutStock(q1.Where(w=>w.CurStock!=null).ToList());
                    transaction.Complete();
                }
                //其它入库单                
                using (transaction = new TransactionScope())
                {
                    var q2 = (from d in uow.ConsumeList.GetAll()
                              join d1 in uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d2 in uow.ConsumeInvPrice.GetAll() on d.Id equals d2.ConsumeListId into dd2
                              from dd2s in dd2.DefaultIfEmpty()
                              join d3 in uow.ConsumeListRds.GetAll() on d.Id equals d3.ConsumeListId into dd3
                              from dd3s in dd3.DefaultIfEmpty()

                              where !d.IsValid && dd3s != null && dd1s.InvType == 2//dd1s.WhId != null && 
                              select new RetailTemp()
                              {
                                  ConsumeListId = d.Id,
                                  InvId = d.Inventory,
                                  Batch = dd2s == null ? "" : dd2s.Code,
                                  Price = d.Price,
                                  Num = d.Quantity,
                                  UserId = d.UserId,
                                  DeptId = d.DeptId,
                                  CreateDate = d.CreateDate,
                                  RdsId = dd3s.RdsId,
                              }).ToList();
                    foreach (RetailTemp tmp in q2)
                    {
                        if (tmp.RdsId.HasValue)
                        {
                            tmp.CurStock = this.GetCurInStock(tmp.RdsId.Value);
                        }
                    }
                    RetailOtherInStock(q2.Where(w=>w.CurStock!=null).ToList());
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 库存
        private void SetUnitAndQuantity(DXInfo.Models.RdRecords rdRecords, DXInfo.Models.ClientRetailOutStockDetail detail)
        {
            DXInfo.Models.Inventory inv = uow.Inventory.GetById(g => g.Id == detail.InvId);
            DXInfo.Models.MeasurementUnitGroup group = uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                rdRecords.MainUnit = inv.MainUnit;
                rdRecords.STUnit = inv.MainUnit;
                rdRecords.ExchRate = 1;
                rdRecords.Quantity = rdRecords.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                rdRecords.MainUnit = inv.MainUnit;
                rdRecords.STUnit = inv.StockUnit.Value;
                rdRecords.ExchRate = uom.Rate;
                rdRecords.Quantity = rdRecords.Num * uom.Rate;
            }
        }
        public void AddRetailOutStock(DXInfo.Models.ClientRetailOutStock retail, string stockVouchLocalCode)
        {            
            using (TransactionScope transaction = new TransactionScope())
            {
                DateTime dtNow = DateTime.Now;
                var rdRecord = uow.RdRecord.GetAll().Where(w =>
                    w.WhId == retail.WhId &&
                    w.DeptId == retail.DeptId &&
                    w.Maker == retail.UserId &&
                    w.RdDate == dtNow.Date &&
                    w.STCode == "002" &&
                    w.VouchType == DXInfo.Models.VouchTypeCode.SaleOutStock &&
                    !w.IsVerify).FirstOrDefault();
                if (rdRecord == null)
                {
                    rdRecord = new DXInfo.Models.RdRecord();
                    rdRecord.Code = common.GetVouchCode(DXInfo.Models.VouchTypeCode.SaleOutStock, stockVouchLocalCode);
                    rdRecord.VouchType = DXInfo.Models.VouchTypeCode.SaleOutStock;
                    rdRecord.Maker = retail.UserId;
                    rdRecord.MakeDate = dtNow;
                    rdRecord.MakeTime = dtNow;
                    rdRecord.RdFlag = 1;
                    rdRecord.RdCode = DXInfo.Models.VouchTypeCode.SaleOutStock;
                    rdRecord.DeptId = retail.DeptId;
                    rdRecord.STCode = "002";
                    rdRecord.RdDate = dtNow;
                    rdRecord.Salesman = retail.UserId;
                    rdRecord.WhId = retail.WhId;
                    rdRecord.BusType = DXInfo.Models.VouchTypeCode.SaleOutStock;
                    uow.RdRecord.Add(rdRecord);
                    uow.Commit();
                }
                foreach (DXInfo.Models.ClientRetailOutStockDetail detail in retail.Detail)
                {
                    DXInfo.Models.RdRecords rdRecords;
                    if (string.IsNullOrEmpty(detail.Batch))
                    {
                        rdRecords = uow.RdRecords.GetAll()
                        .Where(w => w.InvId == detail.InvId &&
                            w.RdId == rdRecord.Id &&
                            w.Batch == null && w.Price == detail.Price).FirstOrDefault();
                    }
                    else
                    {
                        rdRecords = uow.RdRecords.GetAll()
                        .Where(w => w.InvId == detail.InvId &&
                            w.RdId == rdRecord.Id &&
                            w.Batch == detail.Batch && w.Price == detail.Price).FirstOrDefault();
                    }
                    if (rdRecords == null)
                    {
                        rdRecords = new DXInfo.Models.RdRecords();
                        rdRecords.RdId = rdRecord.Id;
                        rdRecords.InvId = detail.InvId;
                        rdRecords.Num = detail.Num;
                        rdRecords.Price = detail.Price;
                        rdRecords.Batch = detail.Batch;
                        rdRecords.Amount = detail.Num * detail.Price;
                        this.SetUnitAndQuantity(rdRecords, detail);
                        if (isShelfLife)
                        {
                            DXInfo.Models.CurrentStock currentStock;

                            var q = uow.CurrentStock.GetAll().Where(w =>
                            w.WhId == retail.WhId &&
                            w.InvId == detail.InvId);

                            if (string.IsNullOrEmpty(rdRecords.Batch))
                            {
                                currentStock = q.Where(w => w.Batch == null).FirstOrDefault();
                            }
                            else
                            {
                                currentStock = q.Where(w => w.Batch == detail.Batch).FirstOrDefault();
                            }
                            rdRecords.MadeDate = currentStock.MadeDate;
                            rdRecords.ShelfLife = currentStock.ShelfLife;
                            rdRecords.ShelfLifeType = currentStock.ShelfLifeType;
                            rdRecords.InvalidDate = currentStock.InvalidDate;
                        }
                        uow.RdRecords.Add(rdRecords);
                    }
                    else
                    {
                        rdRecords.Num = rdRecords.Num + detail.Num;
                        rdRecords.Amount = rdRecords.Num * rdRecords.Price;
                        this.SetUnitAndQuantity(rdRecords, detail);
                        uow.RdRecords.Update(rdRecords);
                    }
                    uow.Commit();
                }
                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion
    }
}
