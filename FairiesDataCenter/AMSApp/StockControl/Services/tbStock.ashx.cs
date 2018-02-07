
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI.WebControls;
using CommCenter;
using System.Web.SessionState;
using System.Transactions;
using System.Reflection;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.Business;
using System.Data.SqlClient;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// 库存主表
    /// </summary>
    public class tbStock : MyHttpHandlerBase
    {
        public tbStock(IAMSCMUow uow)
        {
            this.Uow = uow;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string method = context.Request["method"];
            switch (method)
            {
                case "query":
                    context.Response.Write(GettbStock(context));
                    break;
                //case "all":
                //    context.Response.Write(GetAlltbStockMain(context));
                //    break;
                case "updateMain":
                    context.Response.Write(updatetbStockMain(context));
                    break;
                case "checkMain":
                    context.Response.Write(checktbStockMain(context));
                    break;
                case "uncheckMain":
                    context.Response.Write(unchecktbStockMain(context));
                    break;
                case "updateDetail":
                    context.Response.Write(updatetbStockDetail(context));
                    break;
                case "newMain":
                    context.Response.Write(newtbStockMain(context));
                    break;
                case "newDetail":
                    context.Response.Write(newtbStockDetail(context));
                    break;
                case "removeMain":
                    context.Response.Write(removetbStockMain(context));
                    break;
                case "removeDetail":
                    context.Response.Write(removetbStockDetail(context));
                    break;
                case "excel":
                    tbStockExportToExcel(context);
                    break;
            }
        }
        private IQueryable<tbStockMainAndDetail> QueryCondition(HttpContext context)
        {
            //主表
            string strcnnMainId = context.Request["cnnMainId"] == null ? string.Empty : context.Request["cnnMainId"];
            string strcnvcSupplierCode = context.Request["cnvcSupplierCode"] == null ? string.Empty : context.Request["cnvcSupplierCode"];
            string strcnvcWhCode = context.Request["cnvcWhCode"] == null ? string.Empty : context.Request["cnvcWhCode"];
            string strcnvcDeptId = context.Request["cnvcDeptId"] == null ? string.Empty : context.Request["cnvcDeptId"];
            string strcnnOperType = context.Request["cnnOperType"] == null ? string.Empty : context.Request["cnnOperType"];
            string strcndCreateDate = context.Request["cndCreateDate"] == null ? string.Empty : context.Request["cndCreateDate"];
            string strcndCreateDate2 = context.Request["cndCreateDate2"] == null ? string.Empty : context.Request["cndCreateDate2"];
            string strcnvcCreaterId = context.Request["cnvcCreaterId"] == null ? string.Empty : context.Request["cnvcCreaterId"];
            string strcnvcCreaterName = context.Request["cnvcCreaterName"] == null ? string.Empty : context.Request["cnvcCreaterName"];
            string strcndCheckDate = context.Request["cndCheckDate"] == null ? string.Empty : context.Request["cndCheckDate"];
            string strcndCheckDate2 = context.Request["cndCheckDate2"] == null ? string.Empty : context.Request["cndCheckDate2"];
            string strcnvcCheckerId = context.Request["cnvcCheckerId"] == null ? string.Empty : context.Request["cnvcCheckerId"];
            string strcnvcCheckerName = context.Request["cnvcCheckerName"] == null ? string.Empty : context.Request["cnvcCheckerName"];
            string strcndBusinessDate = context.Request["cndBusinessDate"] == null ? string.Empty : context.Request["cndBusinessDate"];
            string strcndBusinessDate2 = context.Request["cndBusinessDate2"] == null ? string.Empty : context.Request["cndBusinessDate2"];
            string strcnnStatus = context.Request["cnnStatus"] == null ? string.Empty : context.Request["cnnStatus"];
            string strcnvcComments = context.Request["cnvcComments"] == null ? string.Empty : context.Request["cnvcComments"];

            //子表
            string strcnnDetailId = context.Request["cnnDetailId"] == null ? string.Empty : context.Request["cnnDetailId"];
            string strcnvcInvCode = context.Request["cnvcInvCode"] == null ? string.Empty : context.Request["cnvcInvCode"];
            string strcnvcComUnitCode = context.Request["cnvcComUnitCode"] == null ? string.Empty : context.Request["cnvcComUnitCode"];
            string strcnnQuantity = context.Request["cnnQuantity"] == null ? string.Empty : context.Request["cnnQuantity"];
            string strcnnPrice = context.Request["cnnPrice"] == null ? string.Empty : context.Request["cnnPrice"];
            string strcnnAmount = context.Request["cnnAmount"] == null ? string.Empty : context.Request["cnnAmount"];

            var q = from main in Uow.tbStockMain.GetAll()
                    join d in Uow.tbStockDetail.GetAll() on main.cnnMainId equals d.cnnMainId into md
                    from detail in md.DefaultIfEmpty()
                    orderby  main.cnnMainId
                    select new tbStockMainAndDetail
                    {
                        cnnMainId = main.cnnMainId,
                        cnvcSupplierCode = main.cnvcSupplierCode,
                        cnvcWhCode = main.cnvcWhCode,
                        cnvcDeptId = main.cnvcDeptId,
                        cnnOperType = main.cnnOperType,
                        cndCreateDate = main.cndCreateDate,
                        cnvcCreaterId = main.cnvcCreaterId,
                        cnvcCreaterName = main.cnvcCreaterName,
                        cndCheckDate = main.cndCheckDate,
                        cnvcCheckerId = main.cnvcCheckerId,
                        cnvcCheckerName = main.cnvcCheckerName,
                        cndBusinessDate = main.cndBusinessDate,
                        cnnYear = main.cnnYear,
                        cnnMonth = main.cnnMonth,
                        cnnStatus = main.cnnStatus,
                        cnvcComments = main.cnvcComments,
                        cnnDetailId = detail.cnnDetailId,
                        cnvcInvCode = detail.cnvcInvCode,
                        cnvcComUnitCode = detail.cnvcComUnitCode,
                        cnnQuantity = detail.cnnQuantity,
                        cnvcMainComUnitCode = detail.cnvcMainComUnitCode,
                        cnnMainQuantity = detail.cnnMainQuantity,
                        cnnPrice = detail.cnnPrice,
                        cnnAmount = detail.cnnAmount
                    };
            //主表
            if (strcnnMainId != string.Empty) 
            {
                long lcnnMainId = Convert.ToInt64(strcnnMainId);
                q = q.Where(w => w.cnnMainId == lcnnMainId); 
            }
            if (strcnvcSupplierCode != string.Empty) q = q.Where(w => w.cnvcSupplierCode == strcnvcSupplierCode);
            if (strcnvcWhCode != string.Empty) q = q.Where(w => w.cnvcWhCode == strcnvcWhCode);
            if (strcnvcDeptId != string.Empty) q = q.Where(w => w.cnvcDeptId == strcnvcDeptId);
            if (strcnnOperType != string.Empty)
            {
                int icnnOperType = Convert.ToInt32(strcnnOperType);
                q = q.Where(w => w.cnnOperType == icnnOperType);
            }
            if (strcndCreateDate != string.Empty)
            {
                DateTime dcndCreateDate = Convert.ToDateTime(strcndCreateDate);
                q = q.Where(w => w.cndCreateDate >= dcndCreateDate);
            }
            if (strcndCreateDate2 != string.Empty)
            {
                DateTime dcndCreateDate2 = Convert.ToDateTime(strcndCreateDate2);
                q = q.Where(w => w.cndCreateDate <= dcndCreateDate2);
            }
            if (strcnvcCreaterId != string.Empty) q = q.Where(w => w.cnvcCreaterId.Contains(strcnvcCreaterId));
            if (strcnvcCreaterName != string.Empty) q = q.Where(w => w.cnvcCreaterName.Contains(strcnvcCreaterName));
            if (strcndCheckDate != string.Empty)
            {
                DateTime dcndCheckDate = Convert.ToDateTime(strcndCheckDate);
                q = q.Where(w => w.cndCheckDate >= dcndCheckDate);
            }
            if (strcndCheckDate2 != string.Empty)
            {
                DateTime dcndCheckDate2 = Convert.ToDateTime(strcndCheckDate2);
                q = q.Where(w => w.cndCheckDate <= dcndCheckDate2);
            }
            if (strcnvcCheckerId != string.Empty) q = q.Where(w => w.cnvcCheckerId.Contains(strcnvcCheckerId));
            if (strcnvcCheckerName != string.Empty) q = q.Where(w => w.cnvcCheckerName.Contains(strcnvcCheckerName));
            if (strcndBusinessDate != string.Empty)
            {
                DateTime dcndBusinessDate = Convert.ToDateTime(strcndBusinessDate);
                q = q.Where(w => w.cndBusinessDate >= dcndBusinessDate);
            }
            if (strcndBusinessDate2 != string.Empty)
            {
                DateTime dcndBusinessDate2 = Convert.ToDateTime(strcndBusinessDate2);
                q = q.Where(w => w.cndBusinessDate <= dcndBusinessDate2);
            }
            if (strcnnStatus != string.Empty)
            {
                int icnnStatus = Convert.ToInt32(strcnnStatus);
                q = q.Where(w => w.cnnStatus == icnnStatus);
            }
            if (strcnvcComments != string.Empty) q = q.Where(w => w.cnvcComments.Contains(strcnvcComments));
            //子表
            if (strcnnDetailId != string.Empty)
            {
                long lcnnDetailId = Convert.ToInt64(strcnnDetailId);
                q = q.Where(w => w.cnnDetailId == lcnnDetailId);
            }
            if (strcnvcInvCode != string.Empty) q = q.Where(w => w.cnvcInvCode == strcnvcInvCode);
            if (strcnvcComUnitCode != string.Empty) q = q.Where(w => w.cnvcComUnitCode == strcnvcComUnitCode);
            if (strcnnQuantity != string.Empty)
            {
                decimal dcnnQuantity = Convert.ToDecimal(strcnnQuantity);
                q = q.Where(w => w.cnnQuantity == dcnnQuantity);
            }
            if (strcnnPrice != string.Empty)
            {
                decimal dcnnPrice = Convert.ToDecimal(strcnnPrice);
                q = q.Where(w => w.cnnPrice == dcnnPrice);
            }
            if (strcnnAmount != string.Empty)
            {
                decimal dcnnAmount = Convert.ToDecimal(strcnnAmount);
                q = q.Where(w => w.cnnAmount == dcnnAmount);
            }
            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<tbStockMainAndDetail> ltbStock)
        {
            DataTable dt = ltbStock.ToDataTable<tbStockMainAndDetail>();
            ServiceHelper.DataTableConvert(context, dt, "cnvcSupplierCode", ServiceHelper.Table_tbSupplier, "cnvcCode", "cnvcName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcWhCode", ServiceHelper.Table_tbWarehouse, "cnvcWhCode", "cnvcWhName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcDeptId", ServiceHelper.Table_tbDept, "cnvcDeptID", "cnvcDeptName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnnStatus", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='StockStatus'");
            ServiceHelper.DataTableConvert(context, dt, "cnvcInvCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcInvName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComUnitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnnOperType", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='StockType'");
            return dt;
        }
        private string GettbStock(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<tbStockMainAndDetail> ltbStock = new List<tbStockMainAndDetail>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbStock = q.Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStock);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private bool JudgeIsBalance(int cnnYear, int cnnMonth)
        {
            DXInfo.Models.tbMonthlyBalance tbMonthlyBalance = Uow.tbMonthlyBalance.GetById(g=>g.cnnYear==cnnYear&&g.cnnMonth==cnnMonth);
            if (tbMonthlyBalance == null)
            {
                return false;
            }
            else
                return tbMonthlyBalance.cnbIsBalance;
        }
        private string newtbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        DateTime cndBusinessDate = Convert.ToDateTime(context.Request.Form["cndBusinessDate"]);
                        if (JudgeIsBalance(cndBusinessDate.Year, cndBusinessDate.Month))
                        {
                            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                        }
                        DXInfo.Models.tbStockMain tbStockMain = new DXInfo.Models.tbStockMain();
                        if (context.Request.Form["cnvcSupplierCode"] != null)
                            tbStockMain.cnvcSupplierCode = context.Request.Form["cnvcSupplierCode"];
                        tbStockMain.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                        tbStockMain.cnvcDeptId = context.Request.Form["cnvcDeptId"];
                        tbStockMain.cnnOperType = Convert.ToInt32(context.Request.Form["cnnOperType"]);
                        tbStockMain.cnnDirection = 0;
                        if (tbStockMain.cnnOperType == (int)StockType.Transfer)
                        {
                            tbStockMain.cnnDirection = (int)TransferDirection.Out;
                        }
                        tbStockMain.cndCreateDate = DateTime.Now;
                        tbStockMain.cnvcCreaterId = context.User.Identity.Name;
                        CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                        tbStockMain.cnvcCreaterName = ls1.strOperName;

                        tbStockMain.cndBusinessDate = cndBusinessDate;
                        tbStockMain.cnnYear = tbStockMain.cndBusinessDate.Year;
                        tbStockMain.cnnMonth = tbStockMain.cndBusinessDate.Month;
                        tbStockMain.cnnStatus = (int)StockStatus.Create;
                        tbStockMain.cnvcComments = context.Request.Form["cnvcComments"];

                        if (tbStockMain.cnnOperType == (int)StockType.Sell)
                        {
                            int icount = (from d in Uow.tbStockMain.GetAll() where d.cndBusinessDate == tbStockMain.cndBusinessDate && d.cnvcDeptId == tbStockMain.cnvcDeptId && d.cnnOperType == (int)StockType.Sell select d).Count();
                            if (icount > 0)
                            {
                                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, tbStockMain.cnvcDeptId+":"+tbStockMain.cndBusinessDate.ToString("yyyy-MM-dd")+"的销售数据已生成销售出库单,若要更新，可修改或者删除后重新添加！"));
                            }
                        }
                        Uow.tbStockMain.Add(tbStockMain);

                        Uow.Commit();

                        DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
                        ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
                        Uow.tbStockMainLog.Add(tbStockMainLog);
                        List<DXInfo.Models.tbStockDetail> ltbStockDetail = new List<DXInfo.Models.tbStockDetail>();
                        if (tbStockMain.cnnOperType == (int)StockType.Sell)
                        {
                            string strret = Sell(tbStockMain, tbStockMain.cndBusinessDate.ToString("yyyy-MM-dd"), tbStockMain.cnvcDeptId);
                            if (!string.IsNullOrEmpty(strret)) return strret;
                        }                        
                        else if (context.Request["AddDetail"] != null)
                        {
                            bool bAddDetail = Convert.ToBoolean(context.Request["AddDetail"]);
                            if (bAddDetail)
                            {
                                int detailCount = Convert.ToInt32(context.Request["DetailCount"]);
                                for (int i = 0; i < detailCount; i++)
                                {
                                    DXInfo.Models.tbStockDetail tbStockDetail = new DXInfo.Models.tbStockDetail();
                                    tbStockDetail.cnnMainId = tbStockMain.cnnMainId;
                                    tbStockDetail.cnvcInvCode = context.Request["cnvcInvCode[" + i + "]"];


                                    tbStockDetail.cnvcComUnitCode = context.Request["cnvcComUnitCode[" + i + "]"];
                                    tbStockDetail.cnnQuantity = Convert.ToDecimal(context.Request["cnnQuantity[" + i + "]"]);
                                    DXInfo.Models.tbComputationUnit unitCode = Uow.tbComputationUnit.GetById(g=>g.cnvcComunitCode==tbStockDetail.cnvcComUnitCode);
                                    DXInfo.Models.tbComputationUnit mainUnitCode = Uow.tbComputationUnit.GetAll().Where(w => w.cnvcGroupCode == unitCode.cnvcGroupCode && w.cnbMainUnit).FirstOrDefault();
                                    if (mainUnitCode == null)
                                    {
                                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "请设置计量单位组、主计量单位"));
                                    }
                                    tbStockDetail.cnvcMainComUnitCode = mainUnitCode.cnvcComunitCode;//context.Request.Form["cnvcMainComUnitCode"];
                                    tbStockDetail.cnnMainQuantity = tbStockDetail.cnnQuantity * (unitCode.cniChangRate / mainUnitCode.cniChangRate);//Convert.ToDecimal(context.Request.Form["cnnMainQuantity"]);

                                    string strcnnPrice = context.Request["cnnPrice[" + i + "]"];
                                    if (string.IsNullOrEmpty(strcnnPrice)) strcnnPrice = "0";
                                    tbStockDetail.cnnPrice = Convert.ToDecimal(strcnnPrice);
                                    string strcnnAmount = context.Request["cnnAmount[" + i + "]"];
                                    if (string.IsNullOrEmpty(strcnnAmount)) strcnnAmount = "0";
                                    tbStockDetail.cnnAmount = Convert.ToDecimal(strcnnAmount);

                                    tbStockDetail.cndOperDate = tbStockMain.cndCreateDate;
                                    tbStockDetail.cnvcOper = tbStockMain.cnvcCreaterId;
                                    tbStockDetail.cnvcOperName = tbStockMain.cnvcCreaterName;

                                    if (tbStockMain.cnnOperType == 3 || tbStockMain.cnnOperType == 6 || tbStockMain.cnnOperType == 7)
                                    {
                                        if (tbStockDetail.cnnQuantity > 0)
                                            tbStockDetail.cnnQuantity = -tbStockDetail.cnnQuantity;
                                        if (tbStockDetail.cnnAmount > 0)
                                            tbStockDetail.cnnAmount = -tbStockDetail.cnnAmount;
                                        if (tbStockDetail.cnnMainQuantity > 0)
                                            tbStockDetail.cnnMainQuantity = -tbStockDetail.cnnMainQuantity;
                                    }

                                    Uow.tbStockDetail.Add(tbStockDetail);
                                    Uow.Commit();
                                    ltbStockDetail.Add(tbStockDetail);

                                    DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                                    ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                                    Uow.tbStockDetailLog.Add(tbStockDetailLog);
                                }
                            }
                        }
                        if (tbStockMain.cnnOperType == (int)StockType.Complete && ltbStockDetail.Count>0)
                        {
                            Complete( tbStockMain, ltbStockDetail);
                        }
                        if (tbStockMain.cnnOperType == (int)StockType.Transfer && ltbStockDetail.Count > 0)
                        {
                            string incnvcWhCode = context.Request.Form["incnvcWhCode"];
                            string incnvcDeptId = context.Request.Form["incnvcDeptId"];

                            Transfer( tbStockMain, ltbStockDetail, incnvcWhCode, incnvcDeptId);
                        }
                        Uow.Commit();
                        transaction.Complete();
                    }
                //}
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string Sell(DXInfo.Models.tbStockMain tbStockMain,string strdate,string strdeptid)
        {
            try
            {
                string sql = @"select a.vcGoodsID as cnvcInvCode,a.nPrice as cnnPrice,b.cnvcSAComUnitCode as cnvcComUnitCode,
b.cnvcComUnitCode as cnvcMainComUnitCode,isnull(sum(iCount)*c.cniChangRate,0) as cnnMainQuantity,
sum(iCount) as cnnQuantity,sum(nFee) as cnnAmount
from vwConsItem a
left join tbInventory b on a.vcGoodsID=b.cnvcInvCode
left join tbComputationUnit c on b.cnvcSAComUnitCode=c.cnvcComunitCode

 where CONVERT(varchar(10),dtConsDate,121)='"
                    + strdate + "' and cFlag='0' and vcDeptID='"
                    + strdeptid + "'group by a.vcGoodsID,a.nPrice,b.cnvcComUnitCode,b.cnvcSAComUnitCode,c.cniChangRate";
                DataTable dt = Helper.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string strcnvcInvCode = dr["cnvcInvCode"].ToString();
                        decimal dcnnPrice = 0;//Convert.ToDecimal(dr["cnnPrice"]);
                        string strcnvcComUnitCode = dr["cnvcComUnitCode"].ToString();
                        string strcnvcMainComUnitCode = dr["cnvcMainComUnitCode"].ToString();
                        decimal dcnnMainQuantity = -Convert.ToDecimal(dr["cnnMainQuantity"]);
                        decimal dcnnQuantity = -Convert.ToDecimal(dr["cnnQuantity"]);
                        decimal dcnnAmount = 0;// -Convert.ToDecimal(dr["cnnAmount"]);

                        ServiceHelper.AddStockDetal(Uow, tbStockMain, strcnvcInvCode, strcnvcComUnitCode, dcnnQuantity, strcnvcMainComUnitCode, dcnnMainQuantity, dcnnPrice, dcnnAmount);
                    }
                }
            }
            catch (SqlException sex)
            {
                ExceptionPolicy.HandleException(sex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, sex.Message));
            }
            return "";
        }
        private void Complete(DXInfo.Models.tbStockMain tbStockMain, List<DXInfo.Models.tbStockDetail> ltbStockDetail)
        {
            List<DXInfo.Models.tbBillOfMaterials> lbom = ServiceHelper.getBOM(Uow);
            //List<tbStockMainAndDetail> lm = new List<tbStockMainAndDetail>();
            var ltbInventory = (from d in Uow.tbInventory.GetAll()
                                join d1 in Uow.tbComputationUnit.GetAll() on d.cnvcSTComUnitCode equals d1.cnvcComunitCode into dd1
                                from dd1s in dd1.DefaultIfEmpty()

                                join d2 in Uow.tbProductClass.GetAll() on d.cnvcInvCCode equals d2.cnvcProductClassCode into dd2
                                from dd2s in dd2.DefaultIfEmpty()

                                join d3 in Uow.tbComputationUnit.GetAll() on d.cnvcProduceUnitCode equals d3.cnvcComunitCode into dd3
                                from dd3s in dd3.DefaultIfEmpty()

                                select new
                                {
                                    d.cnvcInvCode,
                                    dd2s.cnvcProductType,
                                    d.cnvcComUnitCode,
                                    d.cnvcSTComUnitCode,
                                    cniChangeRate = dd1s == null ? 0 : dd1s.cniChangRate,
                                    cniChangeRate2 = dd3s == null ? 0 : dd3s.cniChangRate
                                }).ToList();
            List<DXInfo.Models.tbBillOfMaterials> lComponentInv = new List<DXInfo.Models.tbBillOfMaterials>();
            List<DXInfo.Models.tbBillOfMaterials> lSemiProduct = new List<DXInfo.Models.tbBillOfMaterials>();
            List<DXInfo.Models.tbBillOfMaterials> lSemi = new List<DXInfo.Models.tbBillOfMaterials>();
            foreach (DXInfo.Models.tbStockDetail sd in ltbStockDetail)
            {
                if (!string.IsNullOrEmpty(sd.cnvcInvCode))
                {
                    //获取所有子件
                    var q1 = (from d in ltbInventory where d.cnvcInvCode == sd.cnvcInvCode select d.cniChangeRate2).FirstOrDefault();
                    List<DXInfo.Models.tbBillOfMaterials> lComponentInv1 = ServiceHelper.ProcBOM(lbom, sd.cnvcInvCode);
                    foreach (DXInfo.Models.tbBillOfMaterials bom in lComponentInv1)
                    {
                        bom.cnnBaseQtyN = bom.cnnBaseQtyN * sd.cnnMainQuantity / q1;
                    }
                    lComponentInv.AddRange(lComponentInv1);


                    List<DXInfo.Models.tbBillOfMaterials> lSemiProduct1 = ServiceHelper.ProcBOM2(lbom, sd.cnvcInvCode);
                    foreach (DXInfo.Models.tbBillOfMaterials bom in lSemiProduct1)
                    {
                        bom.cnnBaseQtyN = bom.cnnBaseQtyN * sd.cnnMainQuantity / q1;
                    }
                    lSemiProduct.AddRange(lSemiProduct1);
                }
            }
            if (lSemiProduct.Count > 0)
            {
                var lcis = from d in lSemiProduct
                           group d by d.cnvcComponentInvCode into g
                           select new { cnvcInvCode = g.Key, cnnQuantity = g.Sum(s => s.cnnBaseQtyN) };
                //获取当月半成品库存
                int icnnStatus = (int)StockStatus.Check;
                var q = (from main in Uow.tbStockMain.GetAll()
                         join d in Uow.tbStockDetail.GetAll() on main.cnnMainId equals d.cnnMainId into md
                        from detail in md.DefaultIfEmpty()

                         join d1 in Uow.tbInventory.GetAll() on detail.cnvcInvCode equals d1.cnvcInvCode into dd1
                        from inventory in dd1.DefaultIfEmpty()

                         join d2 in Uow.tbComputationUnit.GetAll() on inventory.cnvcProduceUnitCode equals d2.cnvcComunitCode into dd2
                        from uom in dd2.DefaultIfEmpty()

                         join d3 in Uow.tbProductClass.GetAll() on inventory.cnvcInvCCode equals d3.cnvcProductClassCode into dd3
                        from productClass in dd3.DefaultIfEmpty()

                        where main.cnnStatus == icnnStatus && productClass.cnvcProductType==ServiceHelper.SEMIPRODUCT
                              && main.cnvcWhCode==tbStockMain.cnvcWhCode && main.cnvcDeptId==tbStockMain.cnvcDeptId
                              && main.cnnYear == tbStockMain.cnnYear && main.cnnMonth==tbStockMain.cnnMonth
                        select new
                        {
                            cnvcInvCode = detail.cnvcInvCode,
                            cnnPQuantity = detail.cnnMainQuantity / uom.cniChangRate == 0 ? 1 : uom.cniChangRate
                        }).ToList();
                var q1 = from d in q
                         group d by new { d.cnvcInvCode } into g
                         select new { g.Key.cnvcInvCode, cnnQuantity = g.Sum(s=>s.cnnPQuantity) };
                lSemi = (from d in lcis
                         join d1 in q1 on d.cnvcInvCode equals d1.cnvcInvCode
                         select new DXInfo.Models.tbBillOfMaterials() { cnvcComponentInvCode = d.cnvcInvCode, cnnBaseQtyN = d.cnnQuantity > d1.cnnQuantity ? d.cnnQuantity : d1.cnnQuantity }
                        ).ToList();
                foreach (var q3 in lSemi)
                {
                    if (!string.IsNullOrEmpty(q3.cnvcComponentInvCode))
                    {
                        //获取所有子件
                        lComponentInv.Add(q3);
                        List<DXInfo.Models.tbBillOfMaterials> lComponentInv1 = ServiceHelper.ProcBOM(lbom, q3.cnvcComponentInvCode);
                        foreach (DXInfo.Models.tbBillOfMaterials bom in lComponentInv1)
                        {
                            bom.cnnBaseQtyN = -bom.cnnBaseQtyN * q3.cnnBaseQtyN;
                        }
                        lComponentInv.AddRange(lComponentInv1);
                    }
                }
            }
            if (lComponentInv.Count > 0)
            {
                //获取子件生产单位数量
                var lcis = from d in lComponentInv
                           group d by d.cnvcComponentInvCode into g
                           select new { cnvcInvCode = g.Key, cnnQuantity = g.Sum(s => s.cnnBaseQtyN) };
                //获取库存计量单位及主计量单位数量
                var q = from d in lcis
                        join d1 in ltbInventory on d.cnvcInvCode equals d1.cnvcInvCode into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        select new
                        {
                            d.cnvcInvCode,
                            cnvcSTComUnitCode = dd1s.cnvcSTComUnitCode,
                            cnvcComUnitCode = dd1s.cnvcComUnitCode,
                            cnnQuantity = d.cnnQuantity * (dd1s == null ? 0 : dd1s.cniChangeRate2 / dd1s.cniChangeRate),
                            cnnMainQuantity = d.cnnQuantity * (dd1s == null ? 0 : dd1s.cniChangeRate2)
                        };
                DXInfo.Models.tbStockMain tbStockMain2 = new DXInfo.Models.tbStockMain();
                ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMain>(tbStockMain, tbStockMain2);
                tbStockMain2.cnnOperType = (int)StockType.Material;
                tbStockMain2.cnnSource = tbStockMain.cnnMainId;
                Uow.tbStockMain.Add(tbStockMain2);
                Uow.Commit();

                DXInfo.Models.tbStockMainLog tbStockMainLog2 = new DXInfo.Models.tbStockMainLog();
                ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain2, tbStockMainLog2);
                Uow.tbStockMainLog.Add(tbStockMainLog2);
                foreach (var q1 in q)
                {
                    ServiceHelper.AddStockDetal(Uow, tbStockMain2, q1.cnvcInvCode, q1.cnvcSTComUnitCode, -q1.cnnQuantity, q1.cnvcComUnitCode, -q1.cnnMainQuantity, 0, 0);
                }
                
            }
        }
        private void Transfer(DXInfo.Models.tbStockMain tbStockMainOut, List<DXInfo.Models.tbStockDetail> ltbStockDetailOut,string incnvcWhCode,string incnvcDeptId)
        {
            DXInfo.Models.tbStockMain tbStockMain = new DXInfo.Models.tbStockMain();
            ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMain>(tbStockMainOut, tbStockMain);
            tbStockMain.cnnDirection = (int)TransferDirection.In;
            tbStockMain.cnnSource = tbStockMainOut.cnnMainId;
            tbStockMain.cnvcWhCode = incnvcWhCode;
            tbStockMain.cnvcDeptId = incnvcDeptId;

            Uow.tbStockMain.Add(tbStockMain);
            Uow.Commit();

            DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
            ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
            Uow.tbStockMainLog.Add(tbStockMainLog);

            foreach (DXInfo.Models.tbStockDetail tbStockDetailOut in ltbStockDetailOut)
            {
                DXInfo.Models.tbStockDetail tbStockDetail = new DXInfo.Models.tbStockDetail();
                ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetail>(tbStockDetailOut, tbStockDetail);
                tbStockDetail.cnnMainId = tbStockMain.cnnMainId;
                tbStockDetail.cnnQuantity = -tbStockDetail.cnnQuantity;
                tbStockDetail.cnnAmount = -tbStockDetail.cnnAmount;
                Uow.tbStockDetail.Add(tbStockDetail);
                Uow.Commit();

                DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                Uow.tbStockDetailLog.Add(tbStockDetailLog);
            }
        }
        private string newtbStockDetail(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        DXInfo.Models.tbStockDetail tbStockDetail = new DXInfo.Models.tbStockDetail();
                        tbStockDetail.cnnMainId = Convert.ToInt64(context.Request.Form["cnnMainId"]);
                        DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==tbStockDetail.cnnMainId);
                        if (tbStockMain.cnnStatus == (int)StockStatus.Check)
                        {
                            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "请弃审后添加商品！"));
                        }
                        if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                        {
                            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                        }
                        tbStockDetail.cnvcInvCode = context.Request.Form["cnvcInvCode"];
                        
                        tbStockDetail.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                        tbStockDetail.cnnQuantity = Convert.ToDecimal(context.Request.Form["cnnQuantity"]);
                        DXInfo.Models.tbComputationUnit unitCode = Uow.tbComputationUnit.GetById(g=>g.cnvcComunitCode==tbStockDetail.cnvcComUnitCode);
                        DXInfo.Models.tbComputationUnit mainUnitCode = Uow.tbComputationUnit.GetAll().Where(w => w.cnvcGroupCode == unitCode.cnvcGroupCode && w.cnbMainUnit).FirstOrDefault();

                        tbStockDetail.cnvcMainComUnitCode = mainUnitCode.cnvcComunitCode;
                        tbStockDetail.cnnMainQuantity = tbStockDetail.cnnQuantity * (unitCode.cniChangRate / mainUnitCode.cniChangRate);
                        

                        string strcnnPrice = context.Request["cnnPrice"];
                        if (string.IsNullOrEmpty(strcnnPrice)) strcnnPrice = "0";
                        tbStockDetail.cnnPrice = Convert.ToDecimal(strcnnPrice);
                        string strcnnAmount = context.Request["cnnAmount"];
                        if (string.IsNullOrEmpty(strcnnAmount)) strcnnAmount = "0";
                        tbStockDetail.cnnAmount = Convert.ToDecimal(strcnnAmount);

                        tbStockDetail.cndOperDate = DateTime.Now;
                        tbStockDetail.cnvcOper = context.User.Identity.Name;
                        CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                        tbStockDetail.cnvcOperName = ls1.strOperName;

                        if (tbStockMain.cnnOperType == 3 || tbStockMain.cnnOperType == 6)
                        {
                            if (tbStockDetail.cnnQuantity > 0)
                                tbStockDetail.cnnQuantity = -tbStockDetail.cnnQuantity;
                            if (tbStockDetail.cnnAmount > 0)
                                tbStockDetail.cnnAmount = -tbStockDetail.cnnAmount;
                            if (tbStockDetail.cnnMainQuantity > 0)
                                tbStockDetail.cnnMainQuantity = -tbStockDetail.cnnMainQuantity;
                        }

                        Uow.tbStockDetail.Add(tbStockDetail);
                        Uow.Commit();

                        DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                        ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                        Uow.tbStockDetailLog.Add(tbStockDetailLog);

                        Uow.Commit();
                        transaction.Complete();
                    }
                //}
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string updatetbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==Convert.ToInt64(context.Request.Form["cnnMainId"]));

                    if (tbStockMain.cnnStatus == (int)StockStatus.Check)
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "请弃审后修改！"));
                    }
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    tbStockMain.cnvcSupplierCode = context.Request.Form["cnvcSupplierCode"];
                    tbStockMain.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                    tbStockMain.cnvcDeptId = context.Request.Form["cnvcDeptId"];

                    tbStockMain.cndBusinessDate = Convert.ToDateTime(context.Request.Form["cndBusinessDate"]);
                    tbStockMain.cnnYear = tbStockMain.cndBusinessDate.Year;
                    tbStockMain.cnnMonth = tbStockMain.cndBusinessDate.Month;

                    tbStockMain.cnvcComments = context.Request.Form["cnvcComments"];

                    tbStockMain.cndModifyDate = DateTime.Now;
                    tbStockMain.cnvcModifier = context.User.Identity.Name;
                    CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                    tbStockMain.cnvcModifierName = ls1.strOperName;

                    DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
                    Uow.tbStockMainLog.Add(tbStockMainLog);

                    Uow.Commit();
                //}
            }
            catch (NullReferenceException nex)
            {
                ExceptionPolicy.HandleException(nex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, nex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string checktbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==Convert.ToInt64(context.Request.Form["cnnMainId"]));
                    if (tbStockMain.cnnStatus == (int)StockStatus.Check)
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已审核！"));
                    }
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    tbStockMain.cndCheckDate = DateTime.Now;
                    tbStockMain.cnvcCheckerId = context.User.Identity.Name;
                    CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                    tbStockMain.cnvcCheckerName = ls1.strOperName;
                    tbStockMain.cnnStatus = (int)StockStatus.Check;

                    tbStockMain.cndModifyDate = tbStockMain.cndCheckDate.Value;
                    tbStockMain.cnvcModifier = tbStockMain.cnvcCheckerId;
                    tbStockMain.cnvcModifierName = tbStockMain.cnvcCheckerName;

                    DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
                    Uow.tbStockMainLog.Add(tbStockMainLog);

                    DXInfo.Models.tbStockMain tbStockMain2 = Uow.tbStockMain.GetAll().Where(w => w.cnnSource == tbStockMain.cnnMainId).FirstOrDefault();
                    if (tbStockMain2 != null)
                    {
                        if (tbStockMain2.cnnStatus != (int)StockStatus.Check)
                        {
                            tbStockMain2.cndCheckDate = tbStockMain.cndCheckDate.Value;
                            tbStockMain2.cnvcCheckerId = tbStockMain.cnvcCheckerId;
                            tbStockMain2.cnvcCheckerName = tbStockMain.cnvcCheckerName;

                            tbStockMain2.cnnStatus = (int)StockStatus.Check;

                            tbStockMain2.cndModifyDate = tbStockMain.cndCheckDate.Value;
                            tbStockMain2.cnvcModifier = tbStockMain.cnvcCheckerId;
                            tbStockMain2.cnvcModifierName = tbStockMain.cnvcCheckerName;

                            DXInfo.Models.tbStockMainLog tbStockMainLog2 = new DXInfo.Models.tbStockMainLog();
                            ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain2, tbStockMainLog2);
                            Uow.tbStockMainLog.Add(tbStockMainLog);
                        }

                    }
                    Uow.Commit();
                //}
            }
            catch (NullReferenceException nex)
            {
                ExceptionPolicy.HandleException(nex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, nex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string unchecktbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==Convert.ToInt64(context.Request.Form["cnnMainId"]));
                    if (tbStockMain.cnnStatus != (int)StockStatus.Check)
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "未审核！"));
                    }
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    tbStockMain.cndCheckDate = null;
                    tbStockMain.cnvcCheckerId = string.Empty;
                    tbStockMain.cnvcCheckerName = string.Empty;
                    tbStockMain.cnnStatus = (int)StockStatus.Create;

                    tbStockMain.cndModifyDate = DateTime.Now;
                    tbStockMain.cnvcModifier = context.User.Identity.Name;
                    CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                    tbStockMain.cnvcModifierName = ls1.strOperName;
                    

                    DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
                    Uow.tbStockMainLog.Add(tbStockMainLog);

                    DXInfo.Models.tbStockMain tbStockMain2 = Uow.tbStockMain.GetAll().Where(w => w.cnnSource == tbStockMain.cnnMainId).FirstOrDefault();
                    if (tbStockMain2 != null)
                    {
                        if (tbStockMain2.cnnStatus == (int)StockStatus.Check)
                        {
                            tbStockMain2.cndCheckDate = null;
                            tbStockMain2.cnvcCheckerId = string.Empty;
                            tbStockMain2.cnvcCheckerName = string.Empty;
                            tbStockMain2.cnnStatus = (int)StockStatus.Create;

                            tbStockMain2.cndModifyDate = tbStockMain.cndModifyDate.Value;
                            tbStockMain2.cnvcModifier = tbStockMain.cnvcModifier;
                            tbStockMain2.cnvcModifierName = tbStockMain.cnvcModifierName;

                            DXInfo.Models.tbStockMainLog tbStockMainLog2 = new DXInfo.Models.tbStockMainLog();
                            ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain2, tbStockMainLog2);
                            Uow.tbStockMainLog.Add(tbStockMainLog);
                        }

                    }
                    Uow.Commit();
                //}
            }
            catch (NullReferenceException nex)
            {
                ExceptionPolicy.HandleException(nex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, nex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string updatetbStockDetail(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    long lcnnDetailId = Convert.ToInt64(context.Request.Form["cnnDetailId"]);
                    DXInfo.Models.tbStockDetail tbStockDetail = Uow.tbStockDetail.GetById(g=>g.cnnDetailId==lcnnDetailId);
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==tbStockDetail.cnnMainId);
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    tbStockDetail.cnvcInvCode = context.Request.Form["cnvcInvCode"];
                    tbStockDetail.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                    tbStockDetail.cnnQuantity = Convert.ToDecimal(context.Request.Form["cnnQuantity"]);
                    DXInfo.Models.tbComputationUnit unitCode = Uow.tbComputationUnit.GetById(g=>g.cnvcComunitCode==tbStockDetail.cnvcComUnitCode);
                    DXInfo.Models.tbComputationUnit mainUnitCode = Uow.tbComputationUnit.GetAll().Where(w => w.cnvcGroupCode == unitCode.cnvcGroupCode && w.cnbMainUnit).FirstOrDefault();

                    tbStockDetail.cnvcMainComUnitCode = mainUnitCode.cnvcComunitCode;
                    tbStockDetail.cnnMainQuantity = tbStockDetail.cnnQuantity * (unitCode.cniChangRate / mainUnitCode.cniChangRate);

                    tbStockDetail.cnnPrice = Convert.ToDecimal(context.Request.Form["cnnPrice"]);
                    tbStockDetail.cnnAmount = Convert.ToDecimal(context.Request.Form["cnnAmount"]);

                    tbStockDetail.cndOperDate = DateTime.Now;
                    tbStockDetail.cnvcOper = context.User.Identity.Name;
                    CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
                    tbStockDetail.cnvcOperName = ls1.strOperName;

                    if (tbStockMain.cnnOperType == 3 || tbStockMain.cnnOperType == 6)
                    {
                        if (tbStockDetail.cnnQuantity > 0)
                            tbStockDetail.cnnQuantity = -tbStockDetail.cnnQuantity;
                        if (tbStockDetail.cnnAmount > 0)
                            tbStockDetail.cnnAmount = -tbStockDetail.cnnAmount;
                        if (tbStockDetail.cnnMainQuantity > 0)
                            tbStockDetail.cnnMainQuantity = -tbStockDetail.cnnMainQuantity;
                    }

                    DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                    Uow.tbStockDetailLog.Add(tbStockDetailLog);

                    Uow.Commit();
                //}
            }
            catch (NullReferenceException nex)
            {
                ExceptionPolicy.HandleException(nex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, nex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string removetbStockMain(HttpContext context)
        {
            try
            {
                long lcnnMainId = Convert.ToInt64(context.Request.Form["cnnMainId"]);
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==lcnnMainId);
                    if (tbStockMain.cnnStatus == (int)StockStatus.Check)
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "弃审后才可以删除"));
                    }
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    tbStockMain.cnnStatus = (int)StockStatus.Delete;

                    DXInfo.Models.tbStockMainLog tbStockMainLog = new DXInfo.Models.tbStockMainLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockMain, DXInfo.Models.tbStockMainLog>(tbStockMain, tbStockMainLog);
                    Uow.tbStockMainLog.Add(tbStockMainLog);

                    Uow.tbStockMain.Delete(tbStockMain);
                    List<DXInfo.Models.tbStockDetail> ltbStockDetail = Uow.tbStockDetail.GetAll().Where(w => w.cnnMainId == lcnnMainId).ToList();
                    foreach (DXInfo.Models.tbStockDetail tbStockDetail in ltbStockDetail)
                    {
                        Uow.tbStockDetail.Delete(tbStockDetail);

                        DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                        ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                        Uow.tbStockDetailLog.Add(tbStockDetailLog);
                    }

                    Uow.Commit();
                //}
            }
            catch (ArgumentNullException aex)
            {
                ExceptionPolicy.HandleException(aex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, aex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string removetbStockDetail(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockDetail tbStockDetail = Uow.tbStockDetail.GetById(g=>g.cnnDetailId==Convert.ToInt64(context.Request.Form["cnnDetailId"]));
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==tbStockDetail.cnnMainId);
                    if (tbStockMain.cnnStatus == (int)StockStatus.Check)
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "弃审后才可以删除"));
                    }
                    if (JudgeIsBalance(tbStockMain.cndBusinessDate.Year, tbStockMain.cndBusinessDate.Month))
                    {
                        return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "已月结不能操作！"));
                    }
                    DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
                    ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
                    Uow.tbStockDetailLog.Add(tbStockDetailLog);

                    Uow.tbStockDetail.Delete(tbStockDetail);

                    Uow.Commit();
                //}
            }
            catch (ArgumentNullException aex)
            {
                ExceptionPolicy.HandleException(aex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, aex.Message));
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private void tbStockExportToExcel(HttpContext context)
        {
            string fileName = "库存.xls";

            List<tbStockMainAndDetail> ltbStock = new List<tbStockMainAndDetail>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbStock = q.ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStock);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

            field = new BoundField();
            field.DataField = "cnnMainId";
            field.HeaderText = "主表流水";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcSupplierCode";
            field.HeaderText = "供应商编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcSupplierCodeComments";
            field.HeaderText = "供应商名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhCode";
            field.HeaderText = "仓库编码";
            view.Columns.Add(field);
            field = new BoundField();
            field = new BoundField();
            field.DataField = "cnvcWhCodeComments";
            field.HeaderText = "仓库名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDeptId";
            field.HeaderText = "部门编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDeptIdComments";
            field.HeaderText = "部门名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnOperType";
            field.HeaderText = "出入库编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnOperTypeComments";
            field.HeaderText = "出入库名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndCreateDate";
            field.HeaderText = "创建时间";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCreaterId";
            field.HeaderText = "创建人编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCreaterName";
            field.HeaderText = "创建人姓名";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndCheckDate";
            field.HeaderText = "审核时间";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCheckerId";
            field.HeaderText = "审核人编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCheckerName";
            field.HeaderText = "审核人姓名";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndBusinessDate";
            field.HeaderText = "业务日期";
            view.Columns.Add(field);
            
            field = new BoundField();
            field.DataField = "cnnStatus";
            field.HeaderText = "状态编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnStatusComments";
            field.HeaderText = "状态名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcComments";
            field.HeaderText = "描述";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnDetailId";
            field.HeaderText = "子表流水";
            view.Columns.Add(field);
            
            field = new BoundField();
            field.DataField = "cnvcInvCode";
            field.HeaderText = "存货编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcInvCodeComments";
            field.HeaderText = "存货名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComUnitCode";
            field.HeaderText = "计量单位编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComUnitCodeComments";
            field.HeaderText = "计量单位名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnQuantity";
            field.HeaderText = "数量";
            view.Columns.Add(field);
            
            field = new BoundField();
            field.DataField = "cnnPrice";
            field.HeaderText = "单价";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnAmount";
            field.HeaderText = "金额";
            view.Columns.Add(field);

            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class tbStockMainAndDetail
    {
        public long cnnMainId { get; set; }
        public string cnvcSupplierCode { get; set; }
        public string cnvcWhCode { get; set; }
        public string cnvcDeptId { get; set; }
        public int cnnOperType { get; set; }
        public DateTime cndCreateDate { get; set; }
        public string cnvcCreaterId { get; set; }
        public string cnvcCreaterName { get; set; }
        public DateTime? cndCheckDate { get; set; }
        public string cnvcCheckerId { get; set; }
        public string cnvcCheckerName { get; set; }
        public DateTime cndBusinessDate { get; set; }
        public int cnnYear { get; set; }
        public int cnnMonth { get; set; }
        public int cnnStatus { get; set; }
        public string cnvcComments { get; set; }
        public long? cnnDetailId { get; set; }
        public string cnvcInvCode { get; set; }
        public string cnvcComUnitCode { get; set; }
        public decimal? cnnQuantity { get; set; }
        public string cnvcMainComUnitCode { get; set; }
        public decimal? cnnMainQuantity { get; set; }
        public decimal? cnnPrice { get; set; }
        public decimal? cnnMainPrice { get; set; }
        public decimal? cnnAmount { get; set; }
        public bool isHave { get; set; }
    }
}
