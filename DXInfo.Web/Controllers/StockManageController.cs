using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;
using System.Linq.Dynamic;
using System.Transactions;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using AutoMapper;
using DXInfo.Data.Contracts;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using DXInfo.Web.Models;
using System.Data.Entity.SqlServer;
using System.Reflection;

namespace DXInfo.Web.Controllers
{
    public class CodeVouchDate
    {
        public string Code { get; set; }
        public string VouchDateId { get; set; }
        public string CurDate { get; set; }
        public Guid Salesman { get; set; }
    }
    [Authorize]
    public class StockManageController : BaseController
    {
        #region 构造方法
        public StockManageController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");
        }
        
        #endregion

        #region 私有方法
        private object GetVouchAuthorityData(IQueryable source, string VouchType, DateTime? MakeTime, bool Descending)
        {
            IQueryable q = businessCommon.SetVouchAuthority(source);
            if (MakeTime.HasValue)
            {
                if (Descending)
                {
                    q = q.Where("MakeTime < @0", MakeTime.Value);
                }
                else
                {
                    q = q.Where("MakeTime > @0", MakeTime.Value.AddSeconds(1));
                }
            }
            if (!string.IsNullOrEmpty(VouchType))
            {
                q = q.Where("VouchType == @0", VouchType);
            }
            
            if (Descending)
            {
                q = q.OrderBy("MakeTime desc");
            }
            else
            {
                q = q.OrderBy("MakeTime asc");
            }
            object obj = q.FirstOrDefault();
            return obj;
        }
        private JsonResult Navigator<T>(string VouchType, DateTime? MakeTime, Func<string, DateTime?, T> getData)
        {
            try
            {
                var rdRecord = getData(VouchType, MakeTime);
                if (rdRecord == null) throw new DXInfo.Models.BusinessException("无记录");
                T retRdRecord = Mapper.Map<T>(rdRecord);
                PropertyInfo propertyInfo = retRdRecord.GetType().GetProperty("IsModify");
                propertyInfo.SetValue(retRdRecord, Convert.ChangeType(true, propertyInfo.PropertyType), null);                
                return Json(retRdRecord, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private void SetShelfLifeColumn(JQGrid grid, bool ShelfLifeEditable)
        {
            this.SetGridColumn(grid, "ShelfLife", isShelfLife);
            this.SetGridColumn(grid, "ShelfLifeType", isShelfLife);
            this.SetGridColumn(grid, "ShelfLifeTypeName", isShelfLife);
            this.SetGridColumn(grid, "InvalidDate", isShelfLife);
            this.SetGridColumn(grid, "MadeDate", isShelfLife);
            if (isShelfLife)
            {
                SetRequiredColumn(grid, "ShelfLife");
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());
                SetRequiredColumn(grid, "ShelfLifeType");
                SetDateColumn(grid, "MadeDate");
                SetRequiredColumn(grid, "MadeDate");
                SetDateColumn(grid, "InvalidDate");

                JQGridColumn madeDateColumn = grid.Columns.Find(c => c.DataField == "MadeDate");
                JQGridColumn shelfLifeColumn = grid.Columns.Find(c => c.DataField == "ShelfLife");
                JQGridColumn shelfLifeTypeColumn = grid.Columns.Find(c => c.DataField == "ShelfLifeType");
                if (madeDateColumn != null)
                {
                    madeDateColumn.Editable = ShelfLifeEditable;
                }
                if (shelfLifeColumn != null)
                {
                    shelfLifeColumn.Editable = ShelfLifeEditable;
                }
                if (shelfLifeTypeColumn != null)
                {
                    shelfLifeTypeColumn.Editable = ShelfLifeEditable;
                }
            }

        }
        private void SetBatchColumn(JQGrid grid, bool BatchIsDropDown)
        {
            this.SetGridColumn(grid, "Batch", isBatch);
            if (isBatch)
            {
                if (isNecessaryBatch)
                {
                    SetRequiredColumn(grid, "Batch");
                }
                if (BatchIsDropDown)
                {
                    JQGridColumn batchColumn = grid.Columns.Find(c => c.DataField == "Batch");
                    if (batchColumn != null)
                    {
                        batchColumn.EditType = EditType.DropDown;
                        batchColumn.EditList = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" } };
                        batchColumn.DataEvents = new List<DataEvent>() { new DataEvent() { Type = DataEventType.Change, Function = "BatchChange" } };
                    }
                }
            }
        }
        private void SetLocatorColumn(JQGrid grid)
        {
            this.SetGridColumn(grid, "Locator", isLocator);
            this.SetGridColumn(grid, "LocatorName", isLocator);
            if (isLocator)
            {
                SetRequiredColumn(grid, "Locator");
            }
        }
        private void SetupVouchsGrid(JQGrid grid, string VouchType)
        {
            grid.DataUrl = Url.Action("Vouchs_RequestData");
            grid.EditUrl = Url.Action("Vouchs_EditData");

            #region 设置列
            SetLocatorColumn(grid);            
            SetRequiredColumn(grid, "Num");                        
            #endregion

            #region 显示数量金额的合计数
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

            #region 其它
            bool PriceVisible = false;
            bool PriceEditable = true;
            string PriceSuffix = "(*)";
            bool AmountVisible = false;
            bool AmountEditable = true;
            bool DueNumVisible = false;
            bool AvaNumEditable = false;
            bool ShelfLifeEditable = false;
            bool BatchIsDropDown = true;
            string InvIdSuffix = "(*)<div id='InvIdDiv'></div><a href='javascript:OpenInvDialog();'><span class='ui-icon ui-icon-plus' style='position:absolute; top:2px; right:25px; '></span></a>";
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.PurchaseInStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    ShelfLifeEditable = true;
                    BatchIsDropDown = false;
                    break;
                case DXInfo.Models.VouchTypeCode.SaleOutStock:  
                    PriceVisible = true;
                    AmountVisible = true;
                    AvaNumEditable = true;
                    if (!isSyncSaleStock)
                    {
                        grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
                        {
                            Position = ToolBarButtonPosition.Last,
                            ToolTip = "关联零售数据",
                            Text = "关联零售数据",
                            OnClick = "associateRetail",
                            ButtonIcon = "ui-icon-extlink",
                        });
                    }
                    //销售出库单单价折扣            
                    if (this.isSaleDiscount)
                    {
                        //JQGridColumn priceColumn = grid.Columns.Find(c => c.DataField == "Price");
                        string discount = "<div>";
                        discount += "<input type='hidden' id='OriginPrice'/>";
                        discount += "<input type='radio' name='discount' value='100' onclick='Discount(this)'>无</input>";
                        discount += "<input type='radio' name='discount' value='90' onclick='Discount(this)'>9折</input>";
                        discount += "<input type='radio' name='discount' value='80' onclick='Discount(this)'>8折</input>";
                        discount += "<input type='radio' name='discount' value='70' onclick='Discount(this)'>7折</input>";
                        discount += "<input type='radio' name='discount' value='60' onclick='Discount(this)'>6折</input>";
                        discount += "<input type='text' style='width:40px' id='CustomDiscount' onkeypress='if (event.keyCode == 13 && $(\"#CustomDiscount\").val() && !isNaN($(\"#CustomDiscount\").val())) { Discount(this);}'/>折";
                        discount += "</div>";
                        PriceSuffix = PriceSuffix + discount;
                        //priceColumn.EditDialogFieldSuffix = "(*)" + discount;
                    }
                    break;
                case DXInfo.Models.VouchTypeCode.OtherInStock:                    
                    DueNumVisible = true;
                    BatchIsDropDown = false;
                    grid.Width = 1000;
                    grid.EditDialogSettings.CloseAfterEditing = false;
                    break;
                case DXInfo.Models.VouchTypeCode.OtherOutStock:
                    PriceVisible = otherOutStockPriceColumnVisible;
                    AmountVisible =  otherOutStockAmountColumnVisible;
                    PriceEditable = false;
                    AmountEditable = false;
                    AvaNumEditable = true;
                    grid.Width = 1000;
                    break;   
                case DXInfo.Models.VouchTypeCode.MaterialOutStock:
                    AvaNumEditable = true;
                    grid.Width = 1000;
                    break;
                case DXInfo.Models.VouchTypeCode.ProductInStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    ShelfLifeEditable = true;
                    BatchIsDropDown = false;
                    break;
                case DXInfo.Models.VouchTypeCode.InitStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    ShelfLifeEditable = true;
                    BatchIsDropDown = false;
                    break;
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    PriceVisible = scrapVouchPriceColumnVisible;
                    AmountVisible = scrapVouchAmountColumnVisible;
                    AvaNumEditable = true;
                    PriceEditable = false;
                    AmountEditable = false;
                    grid.Width = 1000;
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    PriceVisible = transVouchPriceColumnVisible;
                    AmountVisible = transVouchAmountColumnVisible;
                    AvaNumEditable = true;
                    PriceEditable = false;
                    AmountEditable = false;
                    grid.Width = 1000;
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    grid.ToolBarSettings.ShowAddButton = false;
                    BatchIsDropDown = false;
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    AvaNumEditable = true;
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    grid.Width = 1000;
                    grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
                    {
                        Position = ToolBarButtonPosition.Last,
                        ToolTip = "批量添加",
                        Text = "<span class='ui-pg-button-text'>批量添加</span>",
                        OnClick = "customButtonClicked",
                        ButtonIcon = "ui-icon-extlink",
                    });
                    grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
                    {
                        Position = ToolBarButtonPosition.Last,
                        ToolTip = "日常补货",
                        Text = "<span class='ui-pg-button-text'>日常补货</span>",
                        OnClick = "customButtonClicked1",
                        ButtonIcon = "ui-icon-extlink",
                    });
                    break;
                default:
                    
                    break;
            }
            SetRequiredColumn(grid, "InvId",InvIdSuffix);
            if (PriceVisible)
            {                
                JQGridColumn PriceColumn = grid.Columns.Find(c => c.DataField == "Price");
                if (PriceColumn != null)
                {
                    PriceColumn.Editable = PriceEditable;
                }
                SetRequiredColumn(grid, "Price", PriceSuffix);
            }
            if (AmountVisible)
            {                
                JQGridColumn AmountColumn = grid.Columns.Find(c => c.DataField == "Amount");
                if (AmountColumn != null)
                {
                    AmountColumn.Editable = AmountEditable;
                }
                SetRequiredColumn(grid, "Amount");
            }
            SetGridColumn(grid, "Price", PriceVisible);
            SetGridColumn(grid, "Amount", AmountVisible);
            SetGridColumn(grid, "DueNum", DueNumVisible);
            JQGridColumn avaNumColumn = grid.Columns.Find(c => c.DataField == "AvaNum");
            if (avaNumColumn != null)
            {
                avaNumColumn.Editable = AvaNumEditable;
            }
            SetShelfLifeColumn(grid, ShelfLifeEditable);
            SetBatchColumn(grid, BatchIsDropDown);
            #endregion
        }
        private void SetupVouchGrid(JQGrid grid, string VouchType)
        {
            grid.DataUrl = Url.Action("Vouch_RequestData");
            bool PriceVisible = false;
            bool AmountVisible = false;
            string CodeHeadText = "入库单号";
            string RdDateHeadText = "入库日期";
            bool VenNameVisible = true;
            bool ArvCodeVisible = false;
            bool ArvDateVisible = false;
            bool SalesmanNameVisible = false;
            bool BusTypeNameVisible = false;
            string BusTypeNameHeadText = "入库类别";
            string NumHeadText = "数量";
            string InWhNameHeadText = "转入仓库";
            string OutWhNameHeadText = "转出仓库";
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.PurchaseInStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    ArvCodeVisible = true;
                    ArvDateVisible = true;
                    SalesmanNameVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.SaleOutStock:
                    CodeHeadText = "出库单号";
                    RdDateHeadText = "出库日期";
                    SalesmanNameVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.OtherInStock:
                    VenNameVisible = false;
                    SalesmanNameVisible = true;
                    BusTypeNameVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.OtherOutStock:
                    PriceVisible = otherOutStockPriceColumnVisible;
                    AmountVisible = otherOutStockAmountColumnVisible;
                    CodeHeadText = "出库单号";
                    RdDateHeadText = "出库日期";
                    BusTypeNameHeadText = "出库类别";
                    BusTypeNameVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.MaterialOutStock:
                    CodeHeadText = "出库单号";
                    RdDateHeadText = "出库日期";
                    break;
                case DXInfo.Models.VouchTypeCode.ProductInStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.InitStock:
                    PriceVisible = true;
                    AmountVisible = true;
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    PriceVisible = transVouchPriceColumnVisible;
                    AmountVisible = transVouchAmountColumnVisible;
                    CodeHeadText = "单据号";
                    break;
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    CodeHeadText = "单据号";
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    CodeHeadText = "盘点单号";
                    NumHeadText = "账面数";
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    CodeHeadText = "单据号";
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    InWhNameHeadText = "门店仓库";
                    OutWhNameHeadText = "配料仓库";
                    break;
            }
            SetGridColumn(grid, "Price", PriceVisible);
            SetGridColumn(grid, "Amount", AmountVisible);
            SetGridColumn(grid, "VenName", VenNameVisible);
            SetGridColumn(grid, "ARVCode", ArvCodeVisible);
            SetGridColumn(grid, "ArvDate", ArvDateVisible);
            SetGridColumn(grid, "SalesmanName", SalesmanNameVisible);
            SetGridColumn(grid, "BusTypeName", BusTypeNameVisible);
            SetGridColumnHeadText(grid, "Code", CodeHeadText);
            SetGridColumnHeadText(grid, "RdDate", RdDateHeadText);
            SetGridColumnHeadText(grid, "BusTypeName", BusTypeNameHeadText);
            SetGridColumnHeadText(grid, "Num", NumHeadText);
            SetGridColumnHeadText(grid, "InWhName", InWhNameHeadText);
            SetGridColumnHeadText(grid, "OutWhName", OutWhNameHeadText);
            SetShelfLifeColumn(grid, false);
            SetBatchColumn(grid, false);
            SetLocatorColumn(grid);

            SetDateColumn(grid, "RdDate");
            SetDateColumn(grid, "TVDate");
            SetDateColumn(grid, "SVDate");
            SetDateColumn(grid, "CVDate");
            SetDateColumn(grid, "ALVDate");
            SetDateColumn(grid, "MVDate");
            SetDateColumn(grid, "ArvDate");
            SetDateColumn(grid, "VerifyDate");
            this.SetBoolColumn(grid, "IsVerify");

            #region 显示数量、金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion
        }
        void grid_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            JQGridColumn NumColumn = e.GridModel.Columns.Find(f => f.DataField == "Num");
            if (NumColumn != null)
            {
                decimal? num = e.FilterData.Select("Num").Cast<decimal?>().Sum();
                NumColumn.FooterValue = num.ToString();
            }
            JQGridColumn AmountColumn = e.GridModel.Columns.Find(f => f.DataField == "Amount");
            if (AmountColumn != null)
            {
                decimal? amount = e.FilterData.Select("Amount").Cast<decimal?>().Sum();
                AmountColumn.FooterValue = amount.ToString();
            }
        }
        //     2个参数 返回不同
        private JsonResult VouchOper<T,T1,T2>(T1 t1,T2 t2,Func<T1,T2,T> oper)
        {
            try
            {
                T retRecord = oper(t1, t2);
                return Json(retRecord, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }            
        }
        //删除 1个参数 返回不同
        private JsonResult VouchOper<T, T1>(T1 t1, Func<T1, T> oper)
        {
            try
            {
                T retRecord = oper(t1);
                return Json(retRecord, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //添加 2个参数 返回相同
        private JsonResult VouchOper<T, T1>(T t1,T1 t2, Func<T,T1, T> oper)
        {
            try
            {
                T retRecord = oper(t1,t2);
                return Json(retRecord, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //修改 1个参数 返回相同
        private JsonResult VouchOper<T>(T t, Func<T, T> oper)
        {
            try
            {
                T retRecord = oper(t);
                return Json(retRecord, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private void CheckCodeDup(string VouchType,string code)
        {
            int icount = 0;
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    icount = Uow.ScrapVouch.GetAll().Where(w => w.Code == code).Count();
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    icount = Uow.TransVouch.GetAll().Where(w => w.Code == code).Count();
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    icount = Uow.CheckVouch.GetAll().Where(w => w.Code == code).Count();
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    icount = Uow.AdjustLocatorVouch.GetAll().Where(w => w.Code == code).Count();
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    icount = Uow.MixVouch.GetAll().Where(w => w.Code == code).Count();
                    break;
                default:
                    icount = Uow.RdRecord.GetAll().Where(w => w.Code == code).Count();
                    break;
            }            
            if (icount > 0) throw new DXInfo.Models.BusinessException("编码重复");
        }
        private ActionResult GenerateVouch(string VouchType)
        {
            var gridModel = new VouchsGridModel(VouchType);
            List<DXInfo.Models.BusType> lBusType = Uow.BusType.GetAll().Where(w => w.VouchType == VouchType).ToList();
            if (lBusType.Count == 1)
            {
                DXInfo.Models.BusType BusType = lBusType[0];
                gridModel.BusType = BusType.Code;
                gridModel.RdCode = BusType.Code;
                DXInfo.Models.RdType RdType = Uow.RdType.GetById(g => g.Code == BusType.Code);
                if (RdType != null)
                {
                    gridModel.RdFlag = RdType.Flag;
                }
                DXInfo.Models.PTType PTType = Uow.PTType.GetById(g => g.RdCode == BusType.Code);
                if (PTType != null)
                {
                    gridModel.PTCode = PTType.Code;
                }
                DXInfo.Models.STType STType = Uow.STType.GetById(g => g.RdCode == BusType.Code);
                if (STType != null)
                {
                    gridModel.STCode = STType.Code;
                }
            }
            gridModel.VouchType = VouchType;
            gridModel.IsBatch = isBatch;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.IsLocator = isLocator;

            gridModel.InvInit = VouchType == DXInfo.Models.VouchTypeCode.InitStock;
            gridModel.InvType = (int)DXInfo.Models.InvType.StockManage;

            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);

                switch (VouchType)
                {
                    case DXInfo.Models.VouchTypeCode.ScrapVouch:
                        var scrapVouchs = Uow.ScrapVouchs.GetById(g => g.Id == Id);
                        if (scrapVouchs == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = scrapVouchs.SVId;
                        }
                        break;
                    case DXInfo.Models.VouchTypeCode.TransVouch:
                        var transVouchs = Uow.TransVouchs.GetById(g => g.Id == Id);
                        if (transVouchs == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = transVouchs.TVId;
                        }
                        break;
                    case DXInfo.Models.VouchTypeCode.CheckVouch:
                        var checkVouchs = Uow.CheckVouchs.GetById(g => g.Id == Id);
                        if (checkVouchs == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = checkVouchs.CVId;
                        }
                        break;
                    case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                        var adjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == Id);
                        if (adjustLocatorVouchs == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = adjustLocatorVouchs.ALVId;
                        }
                        break;
                    case DXInfo.Models.VouchTypeCode.MixVouch:
                        var mixVouchs = Uow.MixVouchs.GetById(g => g.Id == Id);
                        if (mixVouchs == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = mixVouchs.MVId;
                        }
                        break;
                    default:
                        var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                        if (rdRecords == null)
                        {
                            gridModel.Id = Id;
                        }
                        else
                        {
                            gridModel.Id = rdRecords.RdId;
                        }
                        break;
                }
                
            }
            SetupVouchsGrid(gridModel.VouchsGrid, gridModel.VouchType);
            return PartialView(gridModel);
        }
        #endregion

        #region 公共方法
        public JsonResult Cur(string VouchType,Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            try
            {
                switch (VouchType)
                {
                    case DXInfo.Models.VouchTypeCode.ScrapVouch:
                        var curScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == Id);
                        if (curScrapVouch == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(curScrapVouch);
                        break;
                    case DXInfo.Models.VouchTypeCode.TransVouch:
                        var curTransVouch = Uow.TransVouch.GetById(g => g.Id == Id);
                        if (curTransVouch == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(curTransVouch);
                        break;
                    case DXInfo.Models.VouchTypeCode.CheckVouch:
                        var curCheckVouch = Uow.CheckVouch.GetById(g => g.Id == Id);
                        if (curCheckVouch == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(curCheckVouch);
                        break;
                    case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                        var curAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == Id);
                        if (curAdjustLocatorVouch == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(curAdjustLocatorVouch);
                        break;
                    case DXInfo.Models.VouchTypeCode.MixVouch:
                        var curMixVouch = Uow.MixVouch.GetById(g => g.Id == Id);
                        if (curMixVouch == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(curMixVouch);
                        break;
                    default:
                        DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == Id);
                        if (rdRecord == null)
                        {
                            throw new DXInfo.Models.BusinessException("空记录");
                        }
                        retVouchModel = Mapper.Map<VouchModel>(rdRecord);
                        break;
                }

                retVouchModel.IsModify = true;
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retVouchModel, JsonRequestBehavior.AllowGet);            
        }
        public JsonResult Start(string VouchType)
        {
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return Navigator<VouchModel>(VouchType, null, GetScrapVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return Navigator<VouchModel>(VouchType, null, GetTransVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return Navigator<VouchModel>(VouchType, null, GetCheckVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return Navigator<VouchModel>(VouchType, null, GetAdjustLocatorVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return Navigator<VouchModel>(VouchType, null, GetMixVouchOrderBy);
                default:
                    return Navigator<VouchModel>(VouchType, null, GetRdRecordOrderBy);
            }
        }
        public JsonResult Prev(string VouchType, DateTime? MakeTime)
        {
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetScrapVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetTransVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetCheckVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetAdjustLocatorVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetMixVouchOrderByDescending);
                default:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetRdRecordOrderByDescending);
            }
        }
        public JsonResult Next(string VouchType, DateTime? MakeTime)
        {
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetScrapVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetTransVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetCheckVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetAdjustLocatorVouchOrderBy);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetMixVouchOrderBy);
                default:
                    return Navigator<VouchModel>(VouchType, MakeTime, GetRdRecordOrderBy);
            }

        }
        public JsonResult End(string VouchType)
        {
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return Navigator<VouchModel>(VouchType, null, GetScrapVouchOrderByDescending);  
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return Navigator<VouchModel>(VouchType, null, GetTransVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return Navigator<VouchModel>(VouchType, null, GetCheckVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return Navigator<VouchModel>(VouchType, null, GetAdjustLocatorVouchOrderByDescending);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return Navigator<VouchModel>(VouchType, null, GetMixVouchOrderByDescending);
                default:
                    return Navigator<VouchModel>(VouchType, null, GetRdRecordOrderByDescending);
            }

        }

        public JsonResult AddVouch([JsonBinder]VouchsGridModel vouchsGridModel)
        {
            switch (vouchsGridModel.VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addScrapVouch);            
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addTransVouch);  
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addCheckVouch);  
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addAdjustLocatorVouch);  
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addMixVouch);  
                default:
                    return VouchOper<VouchModel, VouchsGridModel>(vouchsGridModel, addRdRecord);
            }   
            
        }
        public JsonResult ModifyVouch(VouchModel vouchModel)
        {
            switch (vouchModel.VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return VouchOper<VouchModel>(vouchModel, editScrapVouch);                    
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return VouchOper<VouchModel>(vouchModel, editTransVouch);  
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return VouchOper<VouchModel>(vouchModel, editCheckVouch);  
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return VouchOper<VouchModel>(vouchModel, editAdjustLocatorVouch);  
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return VouchOper<VouchModel>(vouchModel, editMixVouch);  
                default:
                    return VouchOper<VouchModel>(vouchModel, editRdRecord);
            }            
        }
        public ActionResult DeleteVouch(VouchModel vouchModel)
        {
            switch (vouchModel.VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delScrapVouch); 
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delTransVouch); 
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delCheckVouch); 
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delAdjustLocatorVouch); 
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delMixVouch); 
                default:
                    return VouchOper<DXInfo.Models.JsonObject, VouchModel>(vouchModel, delRdRecord);
            }               
        }
        public JsonResult VerifyVouch(VouchModel vouchModel)
        {
            switch (vouchModel.VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, verifyScrapVouch); 
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, verifyTransVouch); 
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, verifyCheckVouch); 
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, verifyAdjustLocatorVouch); 
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, verifyMixVouch); 
                default:
                    return VouchOper<VouchModel, Guid, string>(vouchModel.Id.Value, vouchModel.VouchType, VerifyRdRecord);
            }            
        }
        public JsonResult UnVerifyVouch(VouchModel vouchModel)
        {
            switch (vouchModel.VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, unVerifyScrapVouch);    
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, unVerifyTransVouch);    
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, unVerifyCheckVouch);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, unVerifyAdjustLocatorVouch);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, unVerifyMixVouch);
                default:
                    return VouchOper<VouchModel, Guid>(vouchModel.Id.Value, UnVerifyRdRecord);
            }            
        }

        public ActionResult Vouchs_EditData(VouchsModel vouchsModel, string VouchType)
        {
            var gridModel = new VouchsGridModel(VouchType);
            SetupVouchsGrid(gridModel.VouchsGrid, VouchType);

            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    DXInfo.Models.ScrapVouchs scrapVouchs = Mapper.Map<DXInfo.Models.ScrapVouchs>(vouchsModel);
                    return ajaxCallBack<DXInfo.Models.ScrapVouchs>(gridModel.VouchsGrid, scrapVouchs, addScrapVouchs, editScrapVouchs, delScrapVouchs);
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    DXInfo.Models.TransVouchs transVouchs = Mapper.Map<DXInfo.Models.TransVouchs>(vouchsModel);
                    return ajaxCallBack<DXInfo.Models.TransVouchs>(gridModel.VouchsGrid, transVouchs, addTransVouchs, editTransVouchs, delTransVouchs);
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(vouchsModel);
                    return ajaxCallBack<DXInfo.Models.CheckVouchs>(gridModel.VouchsGrid, checkVouchs, addCheckVouchs, editCheckVouchs, delCheckVouchs);
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs = Mapper.Map<DXInfo.Models.AdjustLocatorVouchs>(vouchsModel);
                    return ajaxCallBack<DXInfo.Models.AdjustLocatorVouchs>(gridModel.VouchsGrid, adjustLocatorVouchs, addAdjustLocatorVouchs, editAdjustLocatorVouchs, delAdjustLocatorVouchs);
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    DXInfo.Models.MixVouchs mixVouchs = Mapper.Map<DXInfo.Models.MixVouchs>(vouchsModel);
                    return ajaxCallBack<DXInfo.Models.MixVouchs>(gridModel.VouchsGrid, mixVouchs, addMixVouchs, editMixVouchs, delMixVouchs);
                default:
                    DXInfo.Models.RdRecords rdRecords = Mapper.Map<DXInfo.Models.RdRecords>(vouchsModel);
                    //if (rdRecords.Batch == "")
                    //{
                    //    rdRecords.Batch = null;
                    //}
                    return ajaxCallBack<DXInfo.Models.RdRecords>(gridModel.VouchsGrid, rdRecords, addRdRecords, editRdRecords, delRdRecords);
            }

        }        
        public ActionResult Vouchs_RequestData(string VouchType)
        {
            var gridModel = new VouchsGridModel(VouchType);
            SetupVouchsGrid(gridModel.VouchsGrid, VouchType);
            IQueryable q = null;
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    q = GetScrapVouchsRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    q = GetTransVouchsRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    q = GetCheckVouchsRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    q = GetAdjustLocatorVouchsRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    q = GetMixVouchsRequestData();
                    break;
                default:
                    q = GetRdRecordsRequestData();
                    break;
            }  
            return gridModel.VouchsGrid.DataBind(q);
        }

        public ActionResult Vouch_RequestData(string VouchType)
        {
            var gridModel = new VouchGridModel(VouchType);
            SetupVouchGrid(gridModel.VouchGrid, VouchType);

            string fileName = "";
            switch (VouchType)
            {
                case "001":
                    fileName = "库存采购入库单.xls";
                    break;
                case "002":
                    fileName = "库存销售出库单.xls";
                    break;
                case "003":
                    fileName = "库存其它入库单.xls";
                    break;
                case "004":
                    fileName = "库存其它出库单.xls";
                    break;
                case "005":
                    fileName = "库存材料出库单.xls";
                    break;
                case "006":
                    fileName = "库存产成品入库单.xls";
                    break;
                case "007":
                    fileName = "期初库存.xls";
                    break;
                case "008":
                    fileName = "库存不合格品记录单.xls";
                    break;
                case "009":
                    fileName = "库存调拨单.xls";
                    break;
                case "010":
                    fileName = "库存盘点单.xls";
                    break;
                case "011":
                    fileName = "库存货位调整单.xls";
                    break;
                case "012":
                    fileName = "库存配料单.xls";
                    break;
            }  
            IQueryable q = null;
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.ScrapVouch:
                    q = GetScrapVouchRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.TransVouch:
                    q = GetTransVouchRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.CheckVouch:
                    q = GetCheckVouchRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.AdjustLocatorVouch:
                    q = GetAdjustLocatorVouchRequestData();
                    break;
                case DXInfo.Models.VouchTypeCode.MixVouch:
                    q = GetMixVouchRequestData();
                    break;
                default:
                    q = GetRdRecordRequestData();
                    break;
            }
            return QueryAndExcel(gridModel.VouchGrid, q, fileName);
        }
        public ActionResult SearchVouch(string VouchType)
        {            
            if (!string.IsNullOrEmpty(VouchType))
            {
                var gridModel = new VouchGridModel(VouchType);
                SetupVouchGrid(gridModel.VouchGrid, VouchType);
                var vt = Uow.VouchType.GetById(g => g.Code == VouchType);
                gridModel.VouchType = VouchType;
                if (vt != null)
                {
                    gridModel.VouchGrid.AppearanceSettings.Caption = "搜索" + vt.Name;
                    ViewBag.Title = "搜索" + vt.Name;
                }
                return PartialView(gridModel);
            }
            return new EmptyResult();
        }
        public JsonResult GetCode(string VouchType)
        {
            //return Json(GetRdRecordCode(vouchType), JsonRequestBehavior.AllowGet);
            //BatchUnVerifyRdRecord();
            //BatchVerifyRdRecord();
            //BatchUnVerifyTransVouch();
            //BatchVerifyTransVouch();
            //BatchVerifyRdRecord();
            //var wh = Uow.WarehouseDept.GetAll().Where(w=>w.Dept==deptId).FirstOrDefault();
            var wh = Uow.Warehouse.GetAll().Where(w => w.Dept == deptId).FirstOrDefault();
            Guid whId = Guid.Empty;
            if (wh != null)
            {
                whId = wh.Id;
            }
            return Json(new
            {
                Code = businessCommon.GetVouchCode(VouchType),
                RdDate = DateTime.Now,
                SVDate = DateTime.Now,
                TVDate = DateTime.Now,
                CVDate = DateTime.Now,
                ALVDate = DateTime.Now,
                MVDate = DateTime.Now,
                Salesman = operId,
                WhId = whId,
                InWhId=whId,
                IsModify = false,
                IsVerify = false,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 常量
        public const string BatchInventorySession = "BatchInventorySession";
        public const string BatchWarehouseInventorySession = "BatchWarehouseInventorySession";
        #endregion

        #region 库存仓库档案
        private void SetupWarehouseGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Warehouse_RequestData");
            grid.EditUrl = Url.Action("Warehouse_EditData");
            SetUpGrid(grid);
            this.SetDropDownColumn(grid, "Dept", this.GetDept());
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetRequiredColumn(grid, "Dept");
        }
        public ActionResult Warehouse()
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);
            return PartialView(gridModel);
        }
        public ActionResult Warehouse_RequestData()
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);

            var q = from d in Uow.Warehouse.GetAll()
                        join d1 in Uow.Depts.GetAll() on d.Dept equals d1.DeptId into dd1
                        from dd1s in dd1.DefaultIfEmpty()

                        select new { d.Id, d.Code, d.Name, d.Comment, d.Dept, DeptName = dd1s.DeptName,d.Principal,d.Tele,d.Address };
            return QueryAndExcel(gridModel.WarehouseGrid, q, "库存仓库档案.xls");            
        }

        private void addWarehouse(DXInfo.Models.Warehouse warehouse)
        {
            Uow.Warehouse.Add(warehouse);
            Uow.Commit();
        }
        private void editWarehouse(DXInfo.Models.Warehouse warehouse)
        {
            var oldWarehouse = Uow.Warehouse.GetById(g => g.Id == warehouse.Id);
            oldWarehouse.Code = warehouse.Code;
            oldWarehouse.Name = warehouse.Name;
            oldWarehouse.Comment = warehouse.Comment;
            oldWarehouse.Dept = warehouse.Dept;
            oldWarehouse.Principal = warehouse.Principal;
            oldWarehouse.Tele = warehouse.Tele;
            oldWarehouse.Address = warehouse.Address;
            Uow.Warehouse.Update(oldWarehouse);
            Uow.Commit();
        }
        private void delWarehouse(DXInfo.Models.Warehouse warehouse)
        {
            var count = Uow.Locator.GetAll().Where(w => w.Warehouse == warehouse.Id).Count() + Uow.RdRecord.GetAll().Where(w => w.WhId == warehouse.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("仓库已使用不能删除");
            var oldWarehouse = Uow.Warehouse.GetById(g => g.Id == warehouse.Id);
            Uow.Warehouse.Delete(oldWarehouse);
            Uow.Commit();
        }
        
        public ActionResult Warehouse_EditData(DXInfo.Models.Warehouse warehouse)
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);
            return ajaxCallBack<DXInfo.Models.Warehouse>(gridModel.WarehouseGrid, warehouse, addWarehouse, editWarehouse, delWarehouse);
        }
        #endregion

        #region 库存货位档案
        private void SetupLocatorGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Locator_RequestData");
            grid.EditUrl = Url.Action("Locator_EditData");
            SetUpGrid(grid);
            this.SetDropDownColumn(grid, "Warehouse", centerCommon.GetWarehouse());
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetRequiredColumn(grid, "Warehouse");
        }
        public ActionResult Locator()
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);
            return PartialView(gridModel);
        }
        public ActionResult Locator_RequestData()
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);

            var q = from d in Uow.Locator.GetAll()
                        join d1 in Uow.Warehouse.GetAll() on d.Warehouse equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        select new { d.Id, d.Code, d.Name, d.Comment, d.Warehouse, WarehouseName = dd1s.Name };
            return QueryAndExcel(gridModel.LocatorGrid, q, "库存货位档案.xls");
        }
        private void addLocator(DXInfo.Models.Locator locator)
        {
            Uow.Locator.Add(locator);
            Uow.Commit();
        }
        private void editLocator(DXInfo.Models.Locator locator)
        {
            var oldlocator = Uow.Locator.GetById(g => g.Id == locator.Id);
            if (oldlocator.Warehouse != locator.Warehouse)
            {
                var count = Uow.RdRecords.GetAll().Where(w => w.Locator == oldlocator.Id).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("已使用货位不能修改所属仓库");
            }

            oldlocator.Code = locator.Code;
            oldlocator.Name = locator.Name;
            oldlocator.Comment = locator.Comment;
            oldlocator.Warehouse = locator.Warehouse;
            Uow.Locator.Update(oldlocator);
            Uow.Commit();
        }
        private void delLocator(DXInfo.Models.Locator locator)
        {
            var count = Uow.RdRecords.GetAll().Where(w => w.Locator == locator.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("货位已使用不能删除");
            var oldLocator = Uow.Locator.GetById(g => g.Id == locator.Id);
            Uow.Locator.Delete(oldLocator);
            Uow.Commit();
        }

        public ActionResult Locator_EditData(DXInfo.Models.Locator locator)
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);
            return ajaxCallBack<DXInfo.Models.Locator>(gridModel.LocatorGrid, locator, addLocator, editLocator, delLocator);
        }
        #endregion

        #region 库存供应商档案
        private void SetupVendorGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Vendor_RequestData");
            grid.EditUrl = Url.Action("Vendor_EditData");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.SerializeRowData = "serializeRowData";
            grid.ClientSideEvents.SerializeDelData = "serializeRowData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            SetUpGrid(grid);
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
        }
        public ActionResult Vendor(int VendorType)
        {
            var gridModel = new VendorGridModel();
            gridModel.VendorType = VendorType;
            SetupVendorGridModel(gridModel.VendorGrid);
            return PartialView(gridModel);
        }
        public ActionResult Vendor_RequestData()
        {
            var gridModel = new VendorGridModel();
            SetupVendorGridModel(gridModel.VendorGrid);

            var q = from d in Uow.Vendor.GetAll() select d;
            return QueryAndExcel(gridModel.VendorGrid, q, "库存供应商档案.xls");
        }
        private void addVendor(DXInfo.Models.Vendor vendor)
        {
            Uow.Vendor.Add(vendor);
            Uow.Commit();
        }
        private void editVendor(DXInfo.Models.Vendor vendor)
        {
            var oldVendor = Uow.Vendor.GetById(g => g.Id == vendor.Id);
            oldVendor.Code = vendor.Code;
            oldVendor.Name = vendor.Name;
            oldVendor.Tel = vendor.Tel;
            oldVendor.Fax = vendor.Fax;
            oldVendor.Phone = vendor.Phone;
            oldVendor.Zip = vendor.Zip;
            oldVendor.Linkman = vendor.Linkman;
            oldVendor.Address = vendor.Address;
            oldVendor.Email = vendor.Email;
            oldVendor.VendorType = vendor.VendorType;
            Uow.Vendor.Update(oldVendor);
            Uow.Commit();
        }
        private void delVendor(DXInfo.Models.Vendor vendor)
        {
            var count = Uow.RdRecord.GetAll().Where(w => w.VenId == vendor.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("供应商已使用不能删除");
            var oldVendor = Uow.Vendor.GetById(g => g.Id == vendor.Id);
            Uow.Vendor.Delete(oldVendor);
            Uow.Commit();
        }
        public ActionResult Vendor_EditData(DXInfo.Models.Vendor vendor,int VendorType)
        {
            var gridModel = new VendorGridModel();
            SetupVendorGridModel(gridModel.VendorGrid);
            vendor.VendorType = VendorType;
            return ajaxCallBack<DXInfo.Models.Vendor>(gridModel.VendorGrid, vendor, addVendor, editVendor, delVendor);
        }
        public JsonResult GetReceivers()
        {
            List<SelectListItem> lsi = centerCommon.GetVendor((int)DXInfo.Models.VendorType.Receiver);
            return Json(lsi, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取操作员、计量单位、存货信息，换算率、货位
        
        public JsonResult GetUnitJsonByInv(Guid? inv)
        {
            if (inv.HasValue)
            {
                var iventory = Uow.Inventory.GetById(g => g.Id == inv.Value);
                var units = Uow.UnitOfMeasures.GetAll().Where(w => w.Group == iventory.MeasurementUnitGroup && !w.IsMain).ToList();

                return Json(units, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetInvInfo(Guid? inv)
        {
            if (inv.HasValue)
            {
                var iventory = Uow.Inventory.GetById(g => g.Id == inv);
                if(iventory==null)
                    return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "未找到存货档案" }, JsonRequestBehavior.AllowGet);
                DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == iventory.MeasurementUnitGroup);
                if (group == null)
                {
                    return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "请设置计量单位组" }, JsonRequestBehavior.AllowGet);                            
                }
                DXInfo.Models.UnitOfMeasures uom = new DXInfo.Models.UnitOfMeasures();
                if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                {
                    uom = Uow.UnitOfMeasures.GetById(g => g.Id == iventory.MainUnit);
                }
                else
                {
                    if (!iventory.StockUnit.HasValue)
                        return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "请设置库存单位" }, JsonRequestBehavior.AllowGet);
                    uom = Uow.UnitOfMeasures.GetById(g => g.Id == iventory.StockUnit.Value);
                }
                return Json(new { StockUnitName=uom.Name,iventory.ShelfLife,iventory.ShelfLifeType,iventory.Specs,iventory.SalePrice}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetExtRate(Guid? uom)
        {
            if (uom.HasValue)
            {
                var unit = Uow.UnitOfMeasures.GetById(g => g.Id == uom);
                return Json(unit.Rate, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetLocatorByWh(Guid WhId)
        {
            var q = (from d in Uow.Locator.GetAll()
                     where d.Warehouse == WhId
                     select new { d.Id, d.Name }).ToList();
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRdType(string ptCode)
        {
            if (string.IsNullOrEmpty(ptCode))
            {
                var rdTypes = Uow.RdType.GetAll().ToList();
                return Json(rdTypes, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ptType = Uow.PTType.GetById(g=>g.Code==ptCode);
                var rdType = Uow.RdType.GetById(g=>g.Code==ptType.RdCode);
                return Json(rdType, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBatch(Guid wh, Guid inv)
        {
            DateTime dtNow = DateTime.Now.Date;
            var batchs = (from d in Uow.CurrentStock.GetAll()
                          where d.WhId == wh && d.InvId == inv
                          && d.Num>0 && !d.StopFlag
                          select new { Id = d.Batch, Name = d.Batch,d.InvalidDate }).ToList();

            if (isShelfLife)
            {
                batchs = batchs.Where(w => w.InvalidDate > dtNow).ToList();
            }
            //var batchs = (from d in batchs1
            //              select new { Id=d.Id==null?"":d.Id,Name=d.Name==null?"":d.Name }).ToList();

            return Json(batchs, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetBatch2(Guid wh, Guid inv, string batch)//不合格品过期也给查询
        {
            DateTime dtNow = DateTime.Now.Date;
            var batchs = (from d in Uow.CurrentStock.GetAll()
                          where d.WhId == wh && d.InvId == inv && d.Num > 0
                          select new { Id = d.Batch == null ? "空" : d.Batch, Name = d.Batch == null ? "空" : d.Batch }).ToList();
            if (!string.IsNullOrEmpty(batch) && batchs.Where(w => w.Id == batch).Count() == 0)
                batchs.Insert(0, new { Id = batch, Name = batch });
            return Json(batchs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLocatorByWhBatch(Guid wh, Guid inv, string batch)
        {
            DateTime dtNow = DateTime.Now.Date;
            var locators = (from d in Uow.CurrentInvLocator.GetAll()
                            join d1 in Uow.Locator.GetAll() on d.Locator equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where d.WhId == wh && d.InvId == inv && d.Batch == batch && d.InvalidDate > dtNow && d.Num > 0
                            select new { Id = d.Locator, Name = dd1s.Name }).Distinct();
            return Json(locators, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getLocatorByWhBatch2(Guid wh, Guid inv, string batch)
        {
            DateTime dtNow = DateTime.Now.Date;
            var locators = (from d in Uow.CurrentInvLocator.GetAll()
                            join d1 in Uow.Locator.GetAll() on d.Locator equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where d.WhId == wh && d.InvId == inv && d.Batch == batch && d.Num > 0
                            select new { Id = d.Locator, Name = dd1s.Name }).Distinct();
            return Json(locators, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAvaNum(Guid wh, Guid? locator, Guid inv, string batch)
        {
            if (batch == "空")
            {
                batch = null;
            }
            if (locator.HasValue)
            {
                var currentInvLocator = Uow.CurrentInvLocator.GetAll()
                    .Where(w => w.WhId == wh && w.Locator == locator && w.InvId == inv && w.Batch == batch)
                    .FirstOrDefault();
                decimal num = 0;
                if (currentInvLocator != null)
                    num = currentInvLocator.Num;
                return Json(num, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DXInfo.Models.CurrentStock currentStock;
                if(string.IsNullOrEmpty(batch))
                {
                    currentStock = Uow.CurrentStock.GetAll()
                    .Where(w => w.WhId == wh && w.InvId == inv && w.Batch == null)
                    .FirstOrDefault();
                }
                else
                {
                    currentStock = Uow.CurrentStock.GetAll()
                    .Where(w => w.WhId == wh && w.InvId == inv && w.Batch == batch)
                    .FirstOrDefault();
                }
                decimal num = 0;
                if (currentStock != null)
                    num = currentStock.Num;
                return Json(num, JsonRequestBehavior.AllowGet);
            }
        }
        public DateTime? getInvalidDate(int ShelfLifeType,int ShelfLife,DateTime MadeDate)
        {
            DateTime? InvalidDate = null;
            switch (ShelfLifeType)
            {
                case (int)DXInfo.Models.ShelfLifeType.Day:
                    InvalidDate = MadeDate.AddDays(ShelfLife);
                    break;
                case (int)DXInfo.Models.ShelfLifeType.Week:
                    InvalidDate = MadeDate.AddDays(ShelfLife * 7);
                    break;
                case (int)DXInfo.Models.ShelfLifeType.Month:
                    InvalidDate = MadeDate.AddMonths(ShelfLife);
                    break;
                case (int)DXInfo.Models.ShelfLifeType.Year:
                    InvalidDate = MadeDate.AddYears(ShelfLife);
                    break;
            }
            return InvalidDate;
        }        
        
        private bool IsStop(Guid whId,Guid invId,string batch)
        {
            var oldCurrentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == whId && w.InvId == invId && w.Batch == batch).FirstOrDefault();
            if (oldCurrentStock == null)
            {
                return false;
            }
            else
            {
                return oldCurrentStock.StopFlag;
            }
        }

        #endregion

        #region 批量审核弃审
        //private void BatchVerifyRdRecord()
        //{
        //    DXInfo.Models.VouchType vouchType1 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.InitStock);
        //    BatchVerifyRdRecord(vouchType1);

        //    DXInfo.Models.VouchType vouchType2 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.PurchaseInStock);
        //    BatchVerifyRdRecord(vouchType2);

        //    DXInfo.Models.VouchType vouchType3 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.ProductInStock);
        //    BatchVerifyRdRecord(vouchType3);

        //    DXInfo.Models.VouchType vouchType5 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.OtherInStock);
        //    BatchVerifyRdRecord(vouchType5);

        //    DXInfo.Models.VouchType vouchType4 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.OtherOutStock);
        //    BatchVerifyRdRecord(vouchType4);
            
        //    DXInfo.Models.VouchType vouchType6 = Uow.VouchType.GetById(DXInfo.Models.VouchTypeCode.SaleOutStock);
        //    BatchVerifyRdRecord(vouchType6);
        //}
        private void BatchVerifyRdRecord()//DXInfo.Models.VouchType vouchType)
        {
            DateTime dt = new DateTime(2013,7,2);
            var lRdRecord = Uow.RdRecord.GetAll().Where(w => w.IsVerify && w.VerifyDate > dt).ToList();// w.VouchType == vouchType.Code && w.Code!="0042012110600027").ToList();                        
            Guid userId = this.operId;
            foreach (DXInfo.Models.RdRecord record in lRdRecord)
            {
                VouchModel rdRecordModel = new VouchModel();
                rdRecordModel.Id = record.Id;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 10, 0)))
                {
                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                    //if (rdRecord.IsVerify)
                    //{
                    //    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                    //}
                    if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    rdRecord.IsVerify = true;
                    rdRecord.Verifier = userId;
                    rdRecord.VerifyDate = DateTime.Now;
                    rdRecord.VerifyTime = DateTime.Now;
                    
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    var l1 = (from d in lRdRecords
                              group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType } into g
                              select new DXInfo.Models.RdRecords()
                              {
                                  InvId = g.Key.InvId,
                                  MainUnit = g.Key.MainUnit,
                                  STUnit = g.Key.STUnit,
                                  ExchRate = g.Key.ExchRate,
                                  Quantity = g.Sum(s => s.Quantity),
                                  Num = g.Sum(s => s.Num),
                                  Batch = g.Key.Batch,
                                  Price = g.Key.Price,
                                  Amount = g.Sum(s => s.Amount),
                                  InvalidDate = g.Key.InvalidDate,
                                  MadeDate = g.Key.MadeDate,
                                  ShelfLife = g.Key.ShelfLife,
                                  ShelfLifeType = g.Key.ShelfLifeType,
                              }).ToList();
                    UpdateCurrentStock(rdRecord, l1);
                    //DXInfo.Models.VouchType vouchType = Uow.VouchType.GetById(record.VouchType);                        
                    //AddInvLocatorByRdRecord(rdRecord, lRdRecords, vouchType);
                    var l2 = (from d in lRdRecords
                              group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType, d.Locator } into g
                              select new DXInfo.Models.RdRecords()
                              {
                                  InvId = g.Key.InvId,
                                  MainUnit = g.Key.MainUnit,
                                  STUnit = g.Key.STUnit,
                                  ExchRate = g.Key.ExchRate,
                                  Quantity = g.Sum(s => s.Quantity),
                                  Num = g.Sum(s => s.Num),
                                  Batch = g.Key.Batch,
                                  Price = g.Key.Price,
                                  Amount = g.Sum(s => s.Amount),
                                  InvalidDate = g.Key.InvalidDate,
                                  MadeDate = g.Key.MadeDate,
                                  ShelfLife = g.Key.ShelfLife,
                                  ShelfLifeType = g.Key.ShelfLifeType,
                                  Locator = g.Key.Locator,
                              }).ToList();
                    UpdateCurrentInvLocator(rdRecord, l2);
                    Uow.Commit();
                    transaction.Complete();
                }
            }
        }
        private void BatchUnVerifyRdRecord()
        {
            DateTime dt = new DateTime(2013,7,2);
            var lRdRecord = Uow.RdRecord.GetAll().Where(w => w.IsVerify&&w.VerifyDate>=dt).OrderBy(o=>o.RdDate).ToList();
            Guid userId = operId;
            foreach (DXInfo.Models.RdRecord record in lRdRecord)
            {
                VouchModel rdRecordModel = new VouchModel();
                rdRecordModel.Id = record.Id;
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew,new TimeSpan(0,10,0)))
                {
                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                    if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    rdRecord.IsVerify = false;
                    rdRecord.Verifier = null;
                    rdRecord.VerifyDate = null;
                    rdRecord.VerifyTime = null;
                    rdRecord.Modifier = userId;
                    rdRecord.ModifyDate = DateTime.Now;
                    rdRecord.ModifyTime = DateTime.Now;

                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    UpdateCurrentStock(rdRecord, lRdRecords);

                    //List<DXInfo.Models.InvLocator> lInvLocator = Uow.InvLocator.GetAll().Where(w => w.SourceId == rdRecord.Id).ToList();
                    //foreach (DXInfo.Models.InvLocator invLocator in lInvLocator)
                    //{
                    //    Uow.InvLocator.Delete(invLocator);
                    //}
                    UpdateCurrentInvLocator(rdRecord, lRdRecords);

                    Uow.Commit();
                    transaction.Complete();
                }
            }
        }

        private void BatchUnVerifyTransVouch()
        {
            var lTransVouch = Uow.TransVouch.GetAll().Where(w => w.IsVerify).ToList();

            Guid userId = operId;
            foreach (DXInfo.Models.TransVouch transVouch in lTransVouch)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.TransVouch OldTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                    if (businessCommon.IsBalance(OldTransVouch.TVDate, OldTransVouch.OutWhId) ||
                        businessCommon.IsBalance(OldTransVouch.TVDate, OldTransVouch.InWhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == transVouch.Id).ToList();
                    foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                    {
                        if (rdRecord.IsVerify)
                        {
                            if (rdRecord.RdFlag == 0)
                                throw new DXInfo.Models.BusinessException("已审核");
                            else
                                throw new DXInfo.Models.BusinessException("已审核");
                        }

                        Uow.RdRecord.Delete(rdRecord);
                        List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                        foreach (DXInfo.Models.RdRecords rdRecordSub in lRdRecords)
                        {
                            Uow.RdRecords.Delete(rdRecordSub);
                        }
                    }

                    OldTransVouch.IsVerify = false;
                    OldTransVouch.Verifier = null;
                    OldTransVouch.VerifyDate = null;
                    OldTransVouch.VerifyTime = null;
                    OldTransVouch.Modifier = userId;
                    OldTransVouch.ModifyDate = DateTime.Now;
                    OldTransVouch.ModifyTime = DateTime.Now;

                    Uow.Commit();
                    transaction.Complete();
                }
            }
        }
        private void BatchVerifyTransVouch()
        {
            var lTransVouch = Uow.TransVouch.GetAll().Where(w => !w.IsVerify).ToList();
            foreach (DXInfo.Models.TransVouch transVouch in lTransVouch)
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {

                    DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                    if (oldTransVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                    }
                    if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId) ||
                        businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.InWhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    oldTransVouch.Salesman = transVouch.Salesman;
                    oldTransVouch.IsVerify = true;
                    oldTransVouch.Verifier = userId;
                    oldTransVouch.VerifyDate = DateTime.Now;
                    oldTransVouch.VerifyTime = DateTime.Now;

                    List<DXInfo.Models.TransVouchs> lTransVouchs = Uow.TransVouchs.GetAll().Where(w => w.TVId == oldTransVouch.Id).ToList();
                    foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                    {
                        if (string.IsNullOrEmpty(transVouchs.Batch))
                            throw new DXInfo.Models.BusinessException("请输入批号");
                        if (!transVouchs.Locator.HasValue)
                            throw new DXInfo.Models.BusinessException("请输入货位");
                    }
                    DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g=>g.Code=="006");
                    DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g=>g.Code=="006");
                    //DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, DXInfo.Models.VouchTypeCode.OtherOutStock, outRdType, outBusType);

                    DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g=>g.Code=="003");
                    DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g=>g.Code=="003");
                    //DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                    AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, DXInfo.Models.VouchTypeCode.OtherInStock, inRdType, inBusType);


                    transaction.Complete();
                }
            }
        }
        #endregion

        #region 收发记录
        public JsonResult GetInvs(int InvType)
        {
            var q = (from d in Uow.Inventory.GetAll()
                     where d.InvType == InvType && !d.IsInvalid
                     select new { d.Id, Name = d.Code + "-" + d.Name });
            //JQAutoComplete model = new JQAutoComplete();
            //model.DataField = "Name";
            //return model.DataBind(q);
            return Json(q, JsonRequestBehavior.AllowGet);
        }        
                
        private VouchModel addRdRecord(VouchsGridModel vouchsGridModel)
        {
            if (businessCommon.IsBalance(vouchsGridModel.RdDate.Value, vouchsGridModel.WhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.RdRecord>(vouchsGridModel);
            List<DXInfo.Models.RdRecords> lRdRecords = Mapper.Map<List<DXInfo.Models.RdRecords>>(vouchsGridModel.lVouchs);
            //foreach (VouchsModel vouchsModel in vouchsGridModel.lVouchs)
            //{
            //    lRdRecords.Add(Mapper.Map<VouchsModel, DXInfo.Models.RdRecords>(vouchsModel));
            //}
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckCodeDup(rdRecord.VouchType,rdRecord.Code);                
                rdRecord.Maker = operId;
                rdRecord.MakeDate = DateTime.Now;
                rdRecord.MakeTime = DateTime.Now;
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == rdRecord.WhId);
                rdRecord.DeptId = warehouse.Dept;
                Uow.RdRecord.Add(rdRecord);
                Uow.Commit();

                foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
                {
                    rdRecords.RdId = rdRecord.Id;
                    addRdRecords(rdRecords, rdRecord.WhId, rdRecord.VouchType);
                }
                Uow.Commit();
                transaction.Complete();
                VouchModel retVouchModel = Mapper.Map<VouchModel>(rdRecord);
                retVouchModel.IsModify = true;
                return retVouchModel;
            }
        }                
        private void AddInvLocatorByRdRecord(DXInfo.Models.RdRecord rdRecord, List<DXInfo.Models.RdRecords> lRdRecords, 
            string VouchType)
        {
            foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
            {
                DXInfo.Models.InvLocator inInvLocator = Mapper.Map<DXInfo.Models.RdRecords, DXInfo.Models.InvLocator>(rdRecords);
                inInvLocator.ILDate = rdRecord.RdDate;
                inInvLocator.RdFlag = rdRecord.RdFlag;
                inInvLocator.WhId = rdRecord.WhId;
                inInvLocator.VenId = rdRecord.VenId;
                inInvLocator.SourceId = rdRecord.Id;
                inInvLocator.SourcesId = rdRecords.Id;
                inInvLocator.SourceVouchType = VouchType;
                inInvLocator.Salesman = rdRecord.Salesman;
                Uow.InvLocator.Add(inInvLocator);                
            }
        }

        private VouchModel editRdRecord(VouchModel vouchModel)
        {
            DXInfo.Models.RdRecord oldRdRecord = Uow.RdRecord.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldRdRecord.RdDate, oldRdRecord.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldRdRecord.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核不能修改");
            }
            oldRdRecord = Mapper.Map<VouchModel, DXInfo.Models.RdRecord>(vouchModel, oldRdRecord);
            oldRdRecord.Modifier = operId;
            oldRdRecord.ModifyDate = DateTime.Now;
            oldRdRecord.ModifyTime = DateTime.Now;
            Uow.RdRecord.Update(oldRdRecord);
            Uow.Commit();
            return vouchModel;
        }
        private DXInfo.Models.JsonObject delRdRecord(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (businessCommon.IsBalance(vouchModel.RdDate.Value, vouchModel.WhId.Value))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == vouchModel.Id);
                if (rdRecord == null) throw new DXInfo.Models.BusinessException("空记录");
                if (rdRecord.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                if (rdRecord.SourceId.HasValue)
                {
                    throw new DXInfo.Models.BusinessException("请从来源单据删除，" + rdRecord.Memo);
                }
                Uow.RdRecord.Delete(rdRecord);

                List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
                {
                    Uow.RdRecords.Delete(rdRecords);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }        
        private VouchModel VerifyRdRecord(Guid rdId, string VouchType)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdId);
                if (rdRecord.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                }
                if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                rdRecord.IsVerify = true;
                rdRecord.Verifier = operId;
                rdRecord.VerifyDate = DateTime.Now;
                rdRecord.VerifyTime = DateTime.Now;
                Uow.RdRecord.Update(rdRecord);

                List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                var l1 = (from d in lRdRecords
                          group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType } into g
                          select new DXInfo.Models.RdRecords()
                          {
                              InvId = g.Key.InvId,
                              MainUnit = g.Key.MainUnit,
                              STUnit = g.Key.STUnit,
                              ExchRate = g.Key.ExchRate,
                              Quantity = g.Sum(s => s.Quantity),
                              Num = g.Sum(s => s.Num),
                              Batch = g.Key.Batch,
                              Price = g.Key.Price,
                              Amount = g.Sum(s => s.Amount),
                              InvalidDate = g.Key.InvalidDate,
                              MadeDate = g.Key.MadeDate,
                              ShelfLife = g.Key.ShelfLife,
                              ShelfLifeType = g.Key.ShelfLifeType,
                          }).ToList();
                UpdateCurrentStock(rdRecord, l1);
                if (isLocator)
                {
                    AddInvLocatorByRdRecord(rdRecord, lRdRecords, VouchType);
                }

                if (isLocator)
                {
                    var l2 = (from d in lRdRecords
                              group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType, d.Locator } into g
                              select new DXInfo.Models.RdRecords()
                              {
                                  InvId = g.Key.InvId,
                                  MainUnit = g.Key.MainUnit,
                                  STUnit = g.Key.STUnit,
                                  ExchRate = g.Key.ExchRate,
                                  Quantity = g.Sum(s => s.Quantity),
                                  Num = g.Sum(s => s.Num),
                                  Batch = g.Key.Batch,
                                  Price = g.Key.Price,
                                  Amount = g.Sum(s => s.Amount),
                                  InvalidDate = g.Key.InvalidDate,
                                  MadeDate = g.Key.MadeDate,
                                  ShelfLife = g.Key.ShelfLife,
                                  ShelfLifeType = g.Key.ShelfLifeType,
                                  Locator = g.Key.Locator,
                              }).ToList();
                    UpdateCurrentInvLocator(rdRecord, l2);
                }
                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<DXInfo.Models.RdRecord, VouchModel>(rdRecord);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }        
        private VouchModel UnVerifyRdRecord(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                rdRecord.IsVerify = false;
                rdRecord.Verifier = null;
                rdRecord.VerifyDate = null;
                rdRecord.VerifyTime = null;
                rdRecord.Modifier = operId;
                rdRecord.ModifyDate = DateTime.Now;
                rdRecord.ModifyTime = DateTime.Now;
                Uow.RdRecord.Update(rdRecord);

                List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                UpdateCurrentStock(rdRecord, lRdRecords);


                if (isLocator)
                {
                    List<DXInfo.Models.InvLocator> lInvLocator = Uow.InvLocator.GetAll().Where(w => w.SourceId == rdRecord.Id).ToList();
                    foreach (DXInfo.Models.InvLocator invLocator in lInvLocator)
                    {
                        Uow.InvLocator.Delete(invLocator);
                    }
                    UpdateCurrentInvLocator(rdRecord, lRdRecords);
                }
                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<DXInfo.Models.RdRecord, VouchModel>(rdRecord);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        
        private VouchModel GetRdRecordVouchAuthorityData(string VouchType, DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.RdRecord.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        ARVCode=d.ARVCode,
                        ARVDate = d.ArvDate,
                        BusType=d.BusType,
                        Code=d.Code,
                        DeptId=d.DeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        Maker=d.Maker,
                        MakeTime=(DateTime?)d.MakeTime,
                        Memo=d.Memo,
                        PTCode=d.PTCode,
                        RdCode=d.RdCode,
                        RdDate=(DateTime?)d.RdDate,
                        RdFlag=d.RdFlag,
                        Salesman=(Guid?)d.Salesman,
                        STCode=d.STCode,
                        VenId=d.VenId,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        VerifyDate=d.VerifyDate,
                        VouchType=d.VouchType,
                        WhId=(Guid?)d.WhId,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, VouchType, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetRdRecordOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetRdRecordVouchAuthorityData(VouchType, makeTime, true);
        }
        private VouchModel GetRdRecordOrderBy(string VouchType, DateTime? makeTime)
        {
            return GetRdRecordVouchAuthorityData(VouchType, makeTime, false);  
        }

        private IQueryable GetRdRecordsRequestData()
        {
            var records = from d in Uow.RdRecords.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d2 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d2.Id into dd2
                          from dd2s in dd2.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          //join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          //from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          select new
                          {
                              d.Id,
                              d.RdId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.DueNum,
                              d.Num,
                              d.Price,
                              d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              //ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              d.Locator,
                              LocatorName = dd5s.Name,
                              AvaNum = "",
                              d.Memo,
                          };
            return records;
        }             
        
        private void setInvUnit(DXInfo.Models.RdRecords rdRecords,DXInfo.Models.Inventory inv)
        {
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
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
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                rdRecords.MainUnit = inv.MainUnit;
                rdRecords.STUnit = inv.StockUnit.Value;
                rdRecords.ExchRate = uom.Rate;
                rdRecords.Quantity = rdRecords.Num * uom.Rate;
            }
        }
        private void addRdRecords(DXInfo.Models.RdRecords rdRecords)
        {
            DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecords.RdId);
            if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            addRdRecords(rdRecords,rdRecord.WhId,rdRecord.VouchType);
        }
        private void SetStateByVouchType(string VouchType,ref bool iscs,ref bool isst,ref bool isDeliver)
        {
            switch (VouchType)
            {
                case DXInfo.Models.VouchTypeCode.PurchaseInStock:
                    break;
                case DXInfo.Models.VouchTypeCode.SaleOutStock:
                    iscs = true;
                    isst = true;
                    isDeliver = true;
                    break;
                case DXInfo.Models.VouchTypeCode.OtherInStock:
                    iscs = true;
                    break;
                case DXInfo.Models.VouchTypeCode.OtherOutStock:
                    iscs = true;
                    isDeliver = true;
                    break;
                case DXInfo.Models.VouchTypeCode.MaterialOutStock:
                    iscs = true;
                    isDeliver = true;
                    break;
                case DXInfo.Models.VouchTypeCode.ProductInStock:
                    break;
                case DXInfo.Models.VouchTypeCode.InitStock:
                    break;
            }
        }
        private void addRdRecords(DXInfo.Models.RdRecords rdRecords,Guid WhId,string VouchType)
        {
            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
            setInvUnit(rdRecords, inv);
            bool iscs = false;//使用当前库存信息
            bool isst = false;//是否销售
            bool isDeliver = false;//是否发
            SetStateByVouchType(VouchType,ref iscs,ref isst,ref isDeliver);
            if(iscs)
            {
                DXInfo.Models.CurrentStock currentStock;
                if (string.IsNullOrEmpty(rdRecords.Batch))
                {
                    currentStock =
                        Uow.CurrentStock.GetAll().Where(w => w.WhId == WhId && w.InvId == rdRecords.InvId && w.Batch == null).FirstOrDefault();
                }
                else
                {
                    currentStock =
                        Uow.CurrentStock.GetAll().Where(w => w.WhId == WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                }                
                if (!isst)//非销售
                {
                    rdRecords.Price = currentStock.Price;
                }
                rdRecords.Amount = rdRecords.Num * rdRecords.Price;
                if (isShelfLife)
                {
                    rdRecords.MadeDate = currentStock.MadeDate;
                    rdRecords.ShelfLife = currentStock.ShelfLife;
                    rdRecords.ShelfLifeType = currentStock.ShelfLifeType;
                    rdRecords.InvalidDate = currentStock.InvalidDate;
                }
            }
            else
            {
                rdRecords.Amount = rdRecords.Num * rdRecords.Price;
                if (isShelfLife)
                {
                    rdRecords.InvalidDate = getInvalidDate(rdRecords.ShelfLifeType.Value, rdRecords.ShelfLife.Value, rdRecords.MadeDate.Value);
                }
                //if (rdRecords.Amount != rdRecords.Num * rdRecords.Price)
                //{
                //    throw new DXInfo.Models.BusinessException("单价乘数量必须等于金额");
                //}
                if (isBatch)
                {
                    DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                    if (currentStock != null)
                    {
                        if (rdRecords.InvalidDate != currentStock.InvalidDate || rdRecords.Price != currentStock.Price)
                        {
                            if (isShelfLife)
                            {
                                throw new DXInfo.Models.BusinessException("同批次货物单价、生产日期、失效日期要一致！");
                            }
                            else
                            {
                                throw new DXInfo.Models.BusinessException("同批次货物单价要一致！");
                            }
                        }
                    }
                }
            }
            if (isLocator)
            {
                if (isDeliver)
                {
                    var currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == WhId && w.Locator == rdRecords.Locator.Value && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                    if (rdRecords.Num > currentInvLocator.Num)
                    {
                        throw new DXInfo.Models.BusinessException("货位现存量不足");
                    }
                }
            }
            Uow.RdRecords.Add(rdRecords);
            Uow.Commit();
        }
        private void editRdRecords(DXInfo.Models.RdRecords rdRecords)
        {
            DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecords.RdId);
            if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            var oldRecords = Uow.RdRecords.GetById(g => g.Id == rdRecords.Id);
            oldRecords.InvId = rdRecords.InvId;
            oldRecords.Memo = rdRecords.Memo;
            oldRecords.Num = rdRecords.Num;
            oldRecords.Batch = rdRecords.Batch;
            oldRecords.Locator = rdRecords.Locator;

            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
            setInvUnit(oldRecords, inv);

            bool iscs = false;//使用当前库存信息
            bool isst = false;//是否销售
            bool isDeliver = false;//是否发
            SetStateByVouchType(rdRecord.VouchType, ref iscs, ref isst, ref isDeliver);
            if (iscs)
            {
                DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();

                if (isst)
                {
                    oldRecords.Price = rdRecords.Price;
                }
                else
                {
                    if (currentStock != null)
                    {
                        oldRecords.Price = currentStock.Price;
                    }
                }
                oldRecords.Amount = rdRecords.Num * oldRecords.Price;
                if (currentStock != null)
                {
                    oldRecords.MadeDate = currentStock.MadeDate;
                    oldRecords.ShelfLife = currentStock.ShelfLife;
                    oldRecords.ShelfLifeType = currentStock.ShelfLifeType;
                }
            }
            else
            {
                oldRecords.Price = rdRecords.Price;
                oldRecords.Amount = rdRecords.Num * oldRecords.Price;
                oldRecords.MadeDate = rdRecords.MadeDate;
                oldRecords.ShelfLife = rdRecords.ShelfLife;
                oldRecords.ShelfLifeType = rdRecords.ShelfLifeType;
            }
            if (oldRecords.Amount != oldRecords.Num * oldRecords.Price)
            {
                throw new DXInfo.Models.BusinessException("单价乘数量必须等于金额");
            }
            if (isShelfLife)
            {
                oldRecords.InvalidDate = getInvalidDate(oldRecords.ShelfLifeType.Value, oldRecords.ShelfLife.Value, oldRecords.MadeDate.Value);
            }
            if (isLocator)
            {
                if (isDeliver)
                {
                    var currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == rdRecord.WhId && w.Locator == rdRecords.Locator.Value && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                    if (rdRecords.Num > currentInvLocator.Num)
                    {
                        throw new DXInfo.Models.BusinessException("货位现存量不足");
                    }
                }
            }
            Uow.RdRecords.Update(oldRecords);
            Uow.Commit();
        }
        private void delRdRecords(DXInfo.Models.RdRecords rdRecords)
        {
            var oldRdReocrds = Uow.RdRecords.GetById(g => g.Id == rdRecords.Id);
            if (oldRdReocrds != null)
            {
                var oldRdRecord = Uow.RdRecord.GetById(g => g.Id == oldRdReocrds.RdId);
                if (oldRdRecord.SourceId.HasValue)
                {
                    throw new DXInfo.Models.BusinessException(oldRdRecord.Memo + "其它单据生成，不能删除，请弃审其它单据来删除");
                }
                if (businessCommon.IsBalance(oldRdRecord.RdDate, oldRdRecord.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                Uow.RdRecords.Delete(oldRdReocrds);
                Uow.Commit();
            }
        }
        
        private IQueryable GetRdRecordRequestData()
        {
            var records = from dd6s in Uow.RdRecord.GetAll()
                          join d6 in Uow.RdRecords.GetAll() on dd6s.Id equals d6.RdId into dd6
                          from d in dd6.DefaultIfEmpty()

                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d2 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d2.Id into dd2
                          from dd2s in dd2.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          join d7 in Uow.Warehouse.GetAll() on dd6s.WhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()
                          join d8 in Uow.Vendor.GetAll() on dd6s.VenId equals d8.Id into dd8
                          from dd8s in dd8.DefaultIfEmpty()

                          join d10 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d10.UserId into dd10
                          from dd10s in dd10.DefaultIfEmpty()
                          join d11 in Uow.BusType.GetAll() on dd6s.BusType equals d11.Code into dd11
                          from dd11s in dd11.DefaultIfEmpty()

                          join d12 in Uow.Depts.GetAll() on dd6s.DeptId equals d12.DeptId into dd12
                          from dd12s in dd12.DefaultIfEmpty()

                          select new
                          {
                              dd6s.Id,
                              dd6s.VouchType,
                              dd6s.Code,
                              dd6s.RdDate,
                              dd6s.DeptId,
                              dd6s.WhId,
                              WhName = dd7s.Name,
                              dd6s.ARVCode,
                              dd6s.ArvDate,
                              dd6s.VenId,
                              VenName = dd8s.Name,
                              dd6s.BusType,
                              BusTypeName = dd11s.Name,
                              dd6s.Salesman,
                              SalesmanName = dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id == null ? dd6s.Id : d.Id,
                              InvId = d.InvId == null ? Guid.Empty : d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnit = d.STUnit == null ? Guid.Empty : d.STUnit,
                              STUnitName = dd3s.Name,
                              DueNum = d.DueNum == null ? 0 : d.DueNum,
                              Num = d.Num == null ? 0 : d.Num,
                              Price = d.Price == null ? 0 : d.Price,
                              Amount = d.Amount == null ? 0 : d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              Locator = d.Locator == null ? Guid.Empty : d.Locator,
                              LocatorName = dd5s.Name,
                              SubMemo = d.Memo,
                              dd12s.OrganizationId,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;
        }                

        #region 采购入库单
        public ActionResult PurchaseInStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.PurchaseInStock);
        }
        #endregion

        #region 销售出库单
        public ActionResult SaleOutStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.SaleOutStock);
        }
        private List<DXInfo.Models.RdRecords> Sell(string strdate, string strdeptid)
        {

            string sql = @"select a.vcGoodsID as cnvcInvCode,a.nPrice as cnnPrice,
sum(iCount) as cnnQuantity,sum(nFee) as cnnAmount
from vwConsItem a
 where CONVERT(varchar(10),dtConsDate,121)='" + strdate + "' and cFlag='0' and vcDeptID='" + strdeptid + "' group by a.vcGoodsID,a.nPrice";
            var amscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();


            SqlDataAdapter da = new SqlDataAdapter(sql, amscmUow.Db.Connection.ConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<DXInfo.Models.RdRecords> lRdRecords = new List<DXInfo.Models.RdRecords>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strcnvcInvCode = dr["cnvcInvCode"].ToString();
                    decimal dcnnPrice = Convert.ToDecimal(dr["cnnPrice"]);
                    decimal dcnnQuantity = Convert.ToDecimal(dr["cnnQuantity"]);
                    decimal dcnnAmount = Convert.ToDecimal(dr["cnnAmount"]);
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetAll().Where(w => w.Code == strcnvcInvCode).FirstOrDefault();
                    if (inv != null)
                    {
                        DXInfo.Models.RdRecords rdRecords = new DXInfo.Models.RdRecords();
                        rdRecords.InvId = inv.Id;
                        rdRecords.Num = dcnnQuantity;
                        rdRecords.Amount = dcnnAmount;
                        rdRecords.Price = dcnnPrice;
                        rdRecords.Id = Guid.NewGuid();
                        lRdRecords.Add(rdRecords);
                    }
                }
            }
            return lRdRecords;
        }
        private List<DXInfo.Models.RdRecords> Sell2(DateTime beginDate, DateTime endDate, Guid deptId)
        {
            List<DXInfo.Models.RdRecords> lRdRecords = new List<DXInfo.Models.RdRecords>();
            int invType = (int)DXInfo.Models.InvType.StockManage;
            var lConsumeList = (from d in Uow.ConsumeList.GetAll()
                                join d1 in Uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                                from dd1s in dd1.DefaultIfEmpty()

                                where dd1s.InvType == invType &&
                                d.CreateDate >= beginDate &&
                                d.CreateDate <= endDate
                                select new
                                {
                                    d.Inventory,
                                    d.Quantity,
                                    d.Amount,
                                    d.Price,
                                    dd1s.MainUnit,
                                    dd1s.StockUnit,
                                    dd1s.UnitOfMeasure,
                                    dd1s.MeasurementUnitGroup,
                                }
                                                        ).ToList();
            if (lConsumeList.Count > 0)
            {
                foreach (var consumeList in lConsumeList)
                {
                    DXInfo.Models.RdRecords rdRecords = new DXInfo.Models.RdRecords();
                    rdRecords.InvId = consumeList.Inventory;
                    rdRecords.Num = consumeList.Quantity;
                    rdRecords.Amount = consumeList.Amount;
                    rdRecords.Price = consumeList.Price;
                    rdRecords.Id = Guid.NewGuid();
                    //rdRecords.MainUnit = consumeList.MainUnit;
                    //rdRecords.
                    //DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == consumeList.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        rdRecords.MainUnit = consumeList.MainUnit;
                        rdRecords.STUnit = consumeList.MainUnit;
                        rdRecords.ExchRate = 1;
                        rdRecords.Quantity = rdRecords.Num;
                    }
                    else
                    {
                        if (!consumeList.StockUnit.HasValue)
                        {
                            //return gridModel.RdRecordsGrid.ShowEditValidationMessage("请设置库存单位");
                            throw new DXInfo.Models.BusinessException("请设置库存单位");
                        }
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == consumeList.StockUnit);
                        DXInfo.Models.UnitOfMeasures uom1 = Uow.UnitOfMeasures.GetById(g => g.Id == consumeList.UnitOfMeasure);

                        rdRecords.MainUnit = consumeList.MainUnit;
                        rdRecords.STUnit = consumeList.StockUnit.Value;
                        rdRecords.ExchRate = uom.Rate;
                        rdRecords.Num = consumeList.Quantity * uom1.Rate / uom.Rate;
                        rdRecords.Quantity = consumeList.Quantity * uom1.Rate;
                    }
                    lRdRecords.Add(rdRecords);
                }
            }
            return lRdRecords;
        }
        public JsonResult CreateSell(DateTime date, Guid whId)
        {
            string strdate = date.ToString("yyyy-MM-dd");
            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == whId);
            if (warehouse == null)
            {
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "仓库未找到" }, JsonRequestBehavior.AllowGet);
            }
            DXInfo.Models.Depts dept = Uow.Depts.GetById(g => g.DeptId == warehouse.Dept);
            if (dept == null)
            {
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "部门未找到" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                {
                    List<DXInfo.Models.RdRecords> lRdRecords = Sell(strdate, dept.DeptCode);
                }
                else
                {
                    DateTime beginDate = Convert.ToDateTime(strdate);
                    DateTime endDate = beginDate.AddDays(1);

                    List<DXInfo.Models.RdRecords> lRdRecords = Sell2(beginDate, endDate, dept.DeptId);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new DXInfo.Models.JsonObject() { Sucess = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 产品入库单
        public ActionResult ProductInStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.ProductInStock);
        }
        #endregion

        #region 材料出库单
        public ActionResult MaterialOutStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.MaterialOutStock);
        }
        #endregion

        #region 其它入库单
        public ActionResult OtherInStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.OtherInStock);
        }
        #endregion

        #region 其它出库单
        public ActionResult OtherOutStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.OtherOutStock);
        }
        #endregion

        #region 期初库存
        public ActionResult InitStock()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.InitStock);
        }
        #endregion

        #endregion

        #region 现存量
        private void UpdateCurrentStock(DXInfo.Models.RdRecord rdRecord, List<DXInfo.Models.RdRecords> lRdRecords)
        {
            foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
            {
                DXInfo.Models.CurrentStock oldCurStock;
                if (string.IsNullOrEmpty(rdRecords.Batch))
                {
                    oldCurStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId &&
                   w.Batch == null).OrderBy(o => o.Num).FirstOrDefault();
                }
                else
                {
                    oldCurStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId &&
                    w.Batch == rdRecords.Batch).OrderBy(o => o.Num).FirstOrDefault();
                }
                if (rdRecord.IsVerify)
                {
                    if (oldCurStock == null)
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            throw new DXInfo.Models.BusinessException("库存现存量不足");
                        }
                        oldCurStock = new DXInfo.Models.CurrentStock();
                        oldCurStock.WhId = rdRecord.WhId;
                        oldCurStock.InvId = rdRecords.InvId;
                        oldCurStock.MainUnit = rdRecords.MainUnit;
                        oldCurStock.STUnit = rdRecords.STUnit;
                        oldCurStock.ExchRate = rdRecords.ExchRate;
                        oldCurStock.Quantity = rdRecord.RdFlag == 0 ? rdRecords.Quantity : -rdRecords.Quantity;
                        oldCurStock.Num = rdRecord.RdFlag == 0 ? rdRecords.Num : -rdRecords.Num;
                        oldCurStock.Batch = rdRecords.Batch;
                        oldCurStock.Price = rdRecords.Price;
                        oldCurStock.Amount = oldCurStock.Num * oldCurStock.Price;

                        oldCurStock.InvalidDate = rdRecords.InvalidDate;

                        oldCurStock.MadeDate = rdRecords.MadeDate;
                        oldCurStock.ShelfLife = rdRecords.ShelfLife;
                        oldCurStock.ShelfLifeType = rdRecords.ShelfLifeType;

                        Uow.CurrentStock.Add(oldCurStock);

                        rdRecords.LastBanaceQuantity = 0;
                        rdRecords.BalanceQuantity = oldCurStock.Quantity;
                        rdRecords.LastBalanceNum = 0;
                        rdRecords.BalanceNum = oldCurStock.Num;
                        rdRecords.LastBalanceAmount = 0;
                        rdRecords.BalanceAmount = oldCurStock.Amount;
                    }
                    else
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            if (rdRecords.Num > oldCurStock.Num)
                            {
                                throw new DXInfo.Models.BusinessException("库存现存量不足");
                            }
                        }
                        if (rdRecord.VouchType == DXInfo.Models.VouchTypeCode.PurchaseInStock
                             || rdRecord.VouchType == DXInfo.Models.VouchTypeCode.InitStock
                              || rdRecord.VouchType == DXInfo.Models.VouchTypeCode.ProductInStock
                           )
                        {
                            oldCurStock.Price = rdRecords.Price;
                        }
                        rdRecords.LastBanaceQuantity = oldCurStock.Quantity;
                        rdRecords.LastBalanceNum = oldCurStock.Num;
                        rdRecords.LastBalanceAmount = oldCurStock.Amount;

                        oldCurStock.Quantity = rdRecord.RdFlag == 0 ? oldCurStock.Quantity + rdRecords.Quantity : oldCurStock.Quantity - rdRecords.Quantity;
                        oldCurStock.Num = rdRecord.RdFlag == 0 ? oldCurStock.Num + rdRecords.Num : oldCurStock.Num - rdRecords.Num;
                        oldCurStock.Amount = oldCurStock.Price * oldCurStock.Num;

                        rdRecords.BalanceQuantity = oldCurStock.Quantity;
                        rdRecords.BalanceNum = oldCurStock.Num;
                        rdRecords.BalanceAmount = oldCurStock.Amount;

                        Uow.CurrentStock.Update(oldCurStock);
                    }
                }
                else
                {
                    if (oldCurStock != null)
                    {
                        rdRecords.LastBanaceQuantity = 0;
                        rdRecords.LastBalanceNum = 0;
                        rdRecords.LastBalanceAmount = 0;
                        rdRecords.BalanceQuantity = 0;
                        rdRecords.BalanceNum = 0;
                        rdRecords.BalanceAmount = 0;

                        oldCurStock.Quantity = rdRecord.RdFlag == 0 ? oldCurStock.Quantity - rdRecords.Quantity : oldCurStock.Quantity + rdRecords.Quantity;
                        oldCurStock.Num = rdRecord.RdFlag == 0 ? oldCurStock.Num - rdRecords.Num : oldCurStock.Num + rdRecords.Num;
                        oldCurStock.Amount = oldCurStock.Num * oldCurStock.Price;

                       Uow.CurrentStock.Update(oldCurStock);
                    }
                }
            }
        }
        #endregion

        #region 货位现存量
        private void UpdateCurrentInvLocator(DXInfo.Models.RdRecord rdRecord, List<DXInfo.Models.RdRecords> lRdRecords)
        {
            foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
            {
                if (!rdRecords.Locator.HasValue) throw new DXInfo.Models.BusinessException("请选择货位");
                DXInfo.Models.CurrentInvLocator oldCurInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch&& w.Locator==rdRecords.Locator.Value).FirstOrDefault();

                if (rdRecord.IsVerify)
                {
                    if (oldCurInvLocator == null)
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            throw new DXInfo.Models.BusinessException("货位现存量不足");
                        }

                        oldCurInvLocator = new DXInfo.Models.CurrentInvLocator();
                        oldCurInvLocator.WhId = rdRecord.WhId;
                        oldCurInvLocator.InvId = rdRecords.InvId;
                        oldCurInvLocator.MainUnit = rdRecords.MainUnit;
                        oldCurInvLocator.STUnit = rdRecords.STUnit;
                        oldCurInvLocator.ExchRate = rdRecords.ExchRate;
                        oldCurInvLocator.Quantity = rdRecord.RdFlag == 0 ? rdRecords.Quantity : -rdRecords.Quantity;
                        oldCurInvLocator.Num = rdRecord.RdFlag == 0 ? rdRecords.Num : -rdRecords.Num;
                        oldCurInvLocator.Batch = rdRecords.Batch;
                        oldCurInvLocator.Price = rdRecords.Price;
                        oldCurInvLocator.Amount = oldCurInvLocator.Num * oldCurInvLocator.Price;

                        oldCurInvLocator.InvalidDate = rdRecords.InvalidDate;

                        oldCurInvLocator.MadeDate = rdRecords.MadeDate;
                        oldCurInvLocator.ShelfLife = rdRecords.ShelfLife;
                        oldCurInvLocator.ShelfLifeType = rdRecords.ShelfLifeType;
                        oldCurInvLocator.Locator = rdRecords.Locator.Value;
                        Uow.CurrentInvLocator.Add(oldCurInvLocator);

                        rdRecords.LastBalanceLocatorQuantity = 0;
                        rdRecords.BalanceLocatorQuantity = oldCurInvLocator.Quantity;
                        rdRecords.LastBalanceLocatorNum = 0;
                        rdRecords.BalanceLocatorNum = oldCurInvLocator.Num;
                        rdRecords.LastBalanceLocatorAmount = 0;
                        rdRecords.BalanceLocatorAmount = oldCurInvLocator.Amount;
                    }
                    else
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            if (rdRecords.Num > oldCurInvLocator.Num)
                            {
                                throw new DXInfo.Models.BusinessException("货位现存量不足");
                            }
                        }
                        rdRecords.LastBalanceLocatorQuantity = oldCurInvLocator.Quantity;
                        rdRecords.LastBalanceLocatorNum = oldCurInvLocator.Num;
                        rdRecords.LastBalanceLocatorAmount = oldCurInvLocator.Amount;
                        oldCurInvLocator.Quantity = rdRecord.RdFlag == 0 ? oldCurInvLocator.Quantity + rdRecords.Quantity : oldCurInvLocator.Quantity - rdRecords.Quantity;
                        oldCurInvLocator.Num = rdRecord.RdFlag == 0 ? oldCurInvLocator.Num + rdRecords.Num : oldCurInvLocator.Num - rdRecords.Num;
                        oldCurInvLocator.Amount = oldCurInvLocator.Price * oldCurInvLocator.Num;

                        rdRecords.BalanceLocatorQuantity = oldCurInvLocator.Quantity;
                        rdRecords.BalanceLocatorNum = oldCurInvLocator.Num;
                        rdRecords.BalanceLocatorAmount = oldCurInvLocator.Amount;

                        Uow.CurrentInvLocator.Update(oldCurInvLocator);
                    }
                }
                else
                {
                    if (oldCurInvLocator != null)
                    {
                        rdRecords.LastBalanceLocatorQuantity = 0;
                        rdRecords.LastBalanceLocatorNum = 0;
                        rdRecords.LastBalanceLocatorAmount = 0;
                        rdRecords.BalanceLocatorQuantity = 0;
                        rdRecords.BalanceLocatorNum = 0;
                        rdRecords.BalanceLocatorAmount = 0;

                        oldCurInvLocator.Quantity = rdRecord.RdFlag == 0 ? oldCurInvLocator.Quantity - rdRecords.Quantity : oldCurInvLocator.Quantity + rdRecords.Quantity;
                        oldCurInvLocator.Num = rdRecord.RdFlag == 0 ? oldCurInvLocator.Num - rdRecords.Num : oldCurInvLocator.Num + rdRecords.Num;
                        oldCurInvLocator.Amount = oldCurInvLocator.Num * oldCurInvLocator.Price;

                        Uow.CurrentInvLocator.Update(oldCurInvLocator);
                    }
                }
            }
        }
        #endregion

        #region 库存调拨单
        private VouchModel addTransVouch(VouchsGridModel vouchsGridModel)
        {
            if (businessCommon.IsBalance(vouchsGridModel.TVDate.Value, vouchsGridModel.OutWhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            DXInfo.Models.TransVouch transVouch = Mapper.Map<VouchsGridModel, DXInfo.Models.TransVouch>(vouchsGridModel);
            List<DXInfo.Models.TransVouchs> lTransVouchs = Mapper.Map<List<DXInfo.Models.TransVouchs>>(vouchsGridModel.lVouchs);

            using (TransactionScope transaction = new TransactionScope())
            {
                CheckCodeDup(vouchsGridModel.VouchType, vouchsGridModel.Code);
                transVouch.Maker = operId;
                transVouch.MakeDate = DateTime.Now;
                transVouch.MakeTime = DateTime.Now;
                transVouch.OutRdCode = "006";
                transVouch.InRdCode = "003";
                DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == transVouch.InWhId);
                transVouch.InDeptId = inWarehouse.Dept;
                DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == transVouch.OutWhId);
                transVouch.OutDeptId = outWarehouse.Dept;

                Uow.TransVouch.Add(transVouch);
                Uow.Commit();
                foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                {
                    transVouchs.TVId = transVouch.Id;
                    addTransVouchs(transVouchs, transVouch.OutWhId);
                }
                Uow.Commit();
                transaction.Complete();

                VouchModel retVouchVouch = Mapper.Map<DXInfo.Models.TransVouch, VouchModel>(transVouch);
                retVouchVouch.IsModify = true;
                return retVouchVouch;
            }
        }
        private VouchModel editTransVouch(VouchModel vouchModel)
        {
            DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldTransVouch.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核");
            }
            oldTransVouch = Mapper.Map<VouchModel, DXInfo.Models.TransVouch>(vouchModel, oldTransVouch);
            oldTransVouch.Modifier = operId;
            oldTransVouch.ModifyDate = DateTime.Now;
            oldTransVouch.ModifyTime = DateTime.Now;
            Uow.TransVouch.Update(oldTransVouch);
            Uow.Commit();
            return vouchModel;
        }
        public DXInfo.Models.JsonObject delTransVouch(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {

                DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == vouchModel.Id);
                if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldTransVouch == null) throw new DXInfo.Models.BusinessException("空记录");
                if (oldTransVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                if (oldTransVouch.SourceId.HasValue)
                {
                    throw new DXInfo.Models.BusinessException("请从来源单据删除，" + oldTransVouch.Memo);
                }
                Uow.TransVouch.Delete(oldTransVouch);

                List<DXInfo.Models.TransVouchs> lTransVouchs = Uow.TransVouchs.GetAll().Where(w => w.TVId == vouchModel.Id).ToList();
                foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                {
                    Uow.TransVouchs.Delete(transVouchs);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }

        private Guid AddRdRecordByTransVouch(DXInfo.Models.TransVouch transVouch, List<DXInfo.Models.TransVouchs> lTransVouchs, 
            string VouchType, DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType)
        {
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.TransVouch, DXInfo.Models.RdRecord>(transVouch);
            rdRecord.SourceCode = transVouch.Code;
            rdRecord.SourceId = transVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = VouchType;
            rdRecord.Maker = rdRecord.Verifier.Value;
            rdRecord.MakeDate = rdRecord.MakeDate;
            rdRecord.MakeTime = rdRecord.MakeTime;

            rdRecord.IsVerify = false;
            rdRecord.Verifier = null;
            rdRecord.VerifyDate = null;
            rdRecord.VerifyTime = null;
            rdRecord.RdDate = transVouch.TVDate;
            rdRecord.Memo = rdRecord.Memo + "调拨单号：" + transVouch.Code;
            rdRecord.RdCode = rdType.Code;
            rdRecord.RdFlag = rdType.Flag;
            rdRecord.VouchType = VouchType;
            rdRecord.Code = businessCommon.GetVouchCode(VouchType);
            rdRecord.Maker = operId;
            rdRecord.MakeDate = DateTime.Now;
            rdRecord.MakeTime = DateTime.Now;
            Uow.RdRecord.Add(rdRecord);

            if (rdType.Flag == 0)
            {
                rdRecord.WhId = transVouch.InWhId;
                rdRecord.DeptId = transVouch.InDeptId;
            }
            else
            {
                rdRecord.WhId = transVouch.OutWhId;
                rdRecord.DeptId = transVouch.OutDeptId;
            }
            Uow.Commit();
            List<DXInfo.Models.RdRecords> lrecords = new List<DXInfo.Models.RdRecords>();
            foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
            {
                DXInfo.Models.RdRecords rdRecordSub = Mapper.Map<DXInfo.Models.TransVouchs, DXInfo.Models.RdRecords>(transVouchs);
                if (rdType.Flag == 0)
                {
                    rdRecordSub.Locator = null;
                }
                rdRecordSub.InvId = transVouchs.InvId;
                lrecords.Add(rdRecordSub);
            }

            

            foreach (DXInfo.Models.RdRecords recordSub in lrecords)
            {
                recordSub.RdId = rdRecord.Id;

                Uow.RdRecords.Add(recordSub);

            }
            Uow.Commit();
            return rdRecord.Id;
        }


        public VouchModel verifyTransVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            Guid rdId = Guid.Empty;
            //DXInfo.Models.VouchType outVouchType;
            using (TransactionScope transaction = new TransactionScope())
            {

                DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == Id);
                if (oldTransVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                }
                if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId) ||
                    businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                //oldTransVouch.Salesman = vouchModel.Salesman;
                oldTransVouch.IsVerify = true;
                oldTransVouch.Verifier = operId;
                oldTransVouch.VerifyDate = DateTime.Now;
                oldTransVouch.VerifyTime = DateTime.Now;
                Uow.TransVouch.Update(oldTransVouch);

                List<DXInfo.Models.TransVouchs> lTransVouchs = Uow.TransVouchs.GetAll().Where(w => w.TVId == oldTransVouch.Id).ToList();
                foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                {
                    if (isBatch)
                    {
                        if (string.IsNullOrEmpty(transVouchs.Batch))
                            throw new DXInfo.Models.BusinessException("请输入批号");
                    }
                    if (isLocator)
                    {
                        if (!transVouchs.Locator.HasValue)
                            throw new DXInfo.Models.BusinessException("请输入货位");
                    }
                }
                DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g => g.Code == "006");
                DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g => g.Code == "006");
                //outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                rdId = AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, DXInfo.Models.VouchTypeCode.OtherOutStock, outRdType, outBusType);


                DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g => g.Code == "003");
                DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g => g.Code == "003");
                //DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, DXInfo.Models.VouchTypeCode.OtherInStock, inRdType, inBusType);


                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldTransVouch);
                retVouchModel.IsModify = true;
            }
            using (TransactionScope transaction = new TransactionScope())
            {
                //出库单审核
                VerifyRdRecord(rdId, DXInfo.Models.VouchTypeCode.OtherOutStock);
                Uow.Commit();
                transaction.Complete();
            }
            return retVouchModel;
        }
        public VouchModel unVerifyTransVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();

            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.TransVouch OldTransVouch = Uow.TransVouch.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(OldTransVouch.TVDate, OldTransVouch.OutWhId) ||
                    businessCommon.IsBalance(OldTransVouch.TVDate, OldTransVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == Id).ToList();
                foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                {
                    if (rdRecord.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核");
                    }

                    Uow.RdRecord.Delete(rdRecord);
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    foreach (DXInfo.Models.RdRecords rdRecordSub in lRdRecords)
                    {
                        Uow.RdRecords.Delete(rdRecordSub);
                    }
                }

                OldTransVouch.IsVerify = false;
                OldTransVouch.Verifier = null;
                OldTransVouch.VerifyDate = null;
                OldTransVouch.VerifyTime = null;
                OldTransVouch.Modifier = operId;
                OldTransVouch.ModifyDate = DateTime.Now;
                OldTransVouch.ModifyTime = DateTime.Now;
                Uow.TransVouch.Update(OldTransVouch);

                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(OldTransVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }

        public IQueryable GetTransVouchsRequestData()
        {
            var records = from d in Uow.TransVouchs.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          select new
                          {
                              d.Id,
                              d.TVId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.Price,
                              d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              d.Locator,
                              LocatorName = dd5s.Name,
                              AvaNum = "",
                              d.Memo,
                          };
            return records;
        }
        private void addTransVouchs(DXInfo.Models.TransVouchs transVouchs, Guid OutWhId)
        {            
            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == transVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                transVouchs.MainUnit = inv.MainUnit;
                transVouchs.STUnit = inv.MainUnit;
                transVouchs.ExchRate = 1;
                transVouchs.Quantity = transVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                transVouchs.MainUnit = inv.MainUnit;
                transVouchs.STUnit = inv.StockUnit.Value;
                transVouchs.ExchRate = uom.Rate;
                transVouchs.Quantity = transVouchs.Num * uom.Rate;
            }

            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == OutWhId && w.InvId == transVouchs.InvId && w.Batch == transVouchs.Batch).FirstOrDefault();
            transVouchs.Price = currentStock.Price;
            transVouchs.Amount = transVouchs.Num * transVouchs.Price;
            transVouchs.MadeDate = currentStock.MadeDate;
            transVouchs.ShelfLife = currentStock.ShelfLife;
            transVouchs.ShelfLifeType = currentStock.ShelfLifeType;
            if (isShelfLife)
            {
                transVouchs.InvalidDate = getInvalidDate(transVouchs.ShelfLifeType.Value, transVouchs.ShelfLife.Value, transVouchs.MadeDate.Value);
            }

            Uow.TransVouchs.Add(transVouchs);
            Uow.Commit();
        }
        private void addTransVouchs(DXInfo.Models.TransVouchs transVouchs)
        {
            DXInfo.Models.TransVouch transVouch = Uow.TransVouch.GetById(g => g.Id == transVouchs.TVId);
            if (businessCommon.IsBalance(transVouch.TVDate, transVouch.OutWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            addTransVouchs(transVouchs, transVouch.OutWhId);
        }
        private void editTransVouchs(DXInfo.Models.TransVouchs transVouchs)
        {
            var oldTransVouchs = Uow.TransVouchs.GetById(g => g.Id == transVouchs.Id);
            oldTransVouchs.InvId = transVouchs.InvId;
            oldTransVouchs.Num = transVouchs.Num;
            oldTransVouchs.Batch = transVouchs.Batch;
            oldTransVouchs.Locator = transVouchs.Locator;

            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == transVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                oldTransVouchs.MainUnit = inv.MainUnit;
                oldTransVouchs.STUnit = inv.MainUnit;
                oldTransVouchs.ExchRate = 1;
                oldTransVouchs.Quantity = oldTransVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                oldTransVouchs.MainUnit = inv.MainUnit;
                oldTransVouchs.STUnit = inv.StockUnit.Value;
                oldTransVouchs.ExchRate = uom.Rate;
                oldTransVouchs.Quantity = oldTransVouchs.Num * uom.Rate;
            }

            var oldTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouchs.TVId);
            if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == oldTransVouch.OutWhId && w.InvId == oldTransVouchs.InvId && w.Batch == oldTransVouchs.Batch).FirstOrDefault();
            oldTransVouchs.Price = currentStock.Price;
            oldTransVouchs.Amount = transVouchs.Num * oldTransVouchs.Price;
            oldTransVouchs.MadeDate = currentStock.MadeDate;
            oldTransVouchs.ShelfLife = currentStock.ShelfLife;
            oldTransVouchs.ShelfLifeType = currentStock.ShelfLifeType;
            oldTransVouchs.InvalidDate = getInvalidDate(oldTransVouchs.ShelfLifeType.Value, oldTransVouchs.ShelfLife.Value, oldTransVouchs.MadeDate.Value);

            oldTransVouchs.Memo = transVouchs.Memo;
            Uow.TransVouchs.Update(oldTransVouchs);

            Uow.Commit();
        }
        private void delTransVouchs(DXInfo.Models.TransVouchs transVouchs)
        {
            var oldTransVouchs = Uow.TransVouchs.GetById(g => g.Id == transVouchs.Id);
            if (oldTransVouchs != null)
            {
                var oldTransVouch = Uow.TransVouch.GetById(g => g.Id == oldTransVouchs.TVId);
                if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                Uow.TransVouchs.Delete(oldTransVouchs);
                Uow.Commit();
            }
        }

        public ActionResult TransVouch()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.TransVouch);
        }

        private VouchModel GetTransVouchAuthorityData(DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.TransVouch.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.OutDeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        Code=d.Code,
                        DeptId = d.OutDeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        VerifyDate=d.VerifyDate,
                        Maker=d.Maker,
                        MakeTime = (DateTime?)d.MakeTime,
                        Memo=d.Memo,
                        TVDate=(DateTime?)d.TVDate,
                        Salesman = (Guid?)d.Salesman,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        OutWhId=(Guid?)d.OutWhId,
                        InWhId=(Guid?)d.InWhId,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, null, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetTransVouchOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetTransVouchAuthorityData(makeTime, true);            
        }
        private VouchModel GetTransVouchOrderBy(string VouchType, DateTime? makeTime)
        {
            return GetTransVouchAuthorityData(makeTime, false);            
        }


        public IQueryable GetTransVouchRequestData()
        {
            var records = from dd6s in Uow.TransVouch.GetAll()
                          join d6 in Uow.TransVouchs.GetAll() on dd6s.Id equals d6.TVId into dd6
                          from d in dd6.DefaultIfEmpty()

                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          join d7 in Uow.Warehouse.GetAll() on dd6s.OutWhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()
                          join d10 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d10.UserId into dd10
                          from dd10s in dd10.DefaultIfEmpty()

                          join d13 in Uow.Warehouse.GetAll() on dd6s.InWhId equals d13.Id into dd13
                          from dd13s in dd13.DefaultIfEmpty()
                          select new
                          {
                              dd6s.Id,
                              dd6s.Code,
                              dd6s.TVDate,
                              DeptId=dd6s.OutDeptId,
                              dd6s.OutWhId,
                              OutWhName = dd7s.Name,
                              dd6s.InWhId,
                              InWhName = dd13s.Name,                             
                              dd6s.Salesman,
                              SalesmanName = dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id==null?dd6s.Id:d.Id,
                              InvId=d.InvId==null?Guid.Empty:d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              Num=d.Num==null?0:d.Num,
                              Price=d.Price==null?0:d.Price,
                              Amount=d.Amount==null?0:d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              LocatorName = dd5s.Name,
                              SubMemo=d.Memo,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;
        }
        #endregion

        #region 不合格品记录单
        private VouchModel addScrapVouch(VouchsGridModel vouchsGridModel)
        {
            if (businessCommon.IsBalance(vouchsGridModel.SVDate.Value, vouchsGridModel.WhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            DXInfo.Models.ScrapVouch scrapVouch = Mapper.Map<VouchsGridModel, DXInfo.Models.ScrapVouch>(vouchsGridModel);
            List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Mapper.Map<List<DXInfo.Models.ScrapVouchs>>(vouchsGridModel.lVouchs);
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckCodeDup(DXInfo.Models.VouchTypeCode.ScrapVouch,scrapVouch.Code);
                DXInfo.Models.ScrapVouch newScrapVouch = Mapper.Map<DXInfo.Models.ScrapVouch>(scrapVouch);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newScrapVouch.WhId);
                newScrapVouch.DeptId = warehouse.Dept;
                newScrapVouch.Maker = operId;
                newScrapVouch.MakeDate = DateTime.Now;
                newScrapVouch.MakeTime = DateTime.Now;

                Uow.ScrapVouch.Add(newScrapVouch);
                Uow.Commit();
                foreach (DXInfo.Models.ScrapVouchs scrapVouchs in lScrapVouchs)
                {
                    scrapVouchs.SVId = newScrapVouch.Id;
                    addScrapVouchs(scrapVouchs, newScrapVouch.WhId);                    
                }
                Uow.Commit();
                transaction.Complete();

                VouchModel retScrapVouch = Mapper.Map<VouchModel>(newScrapVouch);
                retScrapVouch.IsModify = true;
                return retScrapVouch;
            }
        }
        private VouchModel editScrapVouch(VouchModel vouchModel)
        {
            DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldScrapVouch.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核");
            }
            oldScrapVouch = Mapper.Map<VouchModel, DXInfo.Models.ScrapVouch>(vouchModel, oldScrapVouch);
            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == oldScrapVouch.WhId);
            oldScrapVouch.DeptId = warehouse.Dept;
            oldScrapVouch.Modifier = operId;
            oldScrapVouch.ModifyDate = DateTime.Now;
            oldScrapVouch.ModifyTime = DateTime.Now;
            Uow.ScrapVouch.Update(oldScrapVouch);
            Uow.Commit();
            return vouchModel;
        }
        private DXInfo.Models.JsonObject delScrapVouch(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == vouchModel.Id);
                if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldScrapVouch == null) throw new DXInfo.Models.BusinessException("空记录");
                if (oldScrapVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                Uow.ScrapVouch.Delete(oldScrapVouch);

                List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Uow.ScrapVouchs.GetAll().Where(w => w.SVId == vouchModel.Id).ToList();
                foreach (DXInfo.Models.ScrapVouchs scrapVouchs in lScrapVouchs)
                {
                    Uow.ScrapVouchs.Delete(scrapVouchs);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }

        private void AddRdRecordByScrapVouch(DXInfo.Models.ScrapVouch scrapVouch,
            List<DXInfo.Models.ScrapVouchs> lScrapVouchs,
            string VouchType,DXInfo.Models.RdType rdType,DXInfo.Models.BusType busType)
        {
            
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.ScrapVouch, DXInfo.Models.RdRecord>(scrapVouch);
            rdRecord.SourceCode = scrapVouch.Code;
            rdRecord.SourceId = scrapVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = VouchType;
            rdRecord.Maker = rdRecord.Verifier.Value;
            rdRecord.MakeDate = rdRecord.MakeDate;
            rdRecord.MakeTime = rdRecord.MakeTime;

            rdRecord.IsVerify = false;
            rdRecord.Verifier = null;
            rdRecord.VerifyDate = null;
            rdRecord.VerifyTime = null;
            rdRecord.RdDate = scrapVouch.SVDate;
            rdRecord.Memo = rdRecord.Memo+"不合格品记录单号："+scrapVouch.Code;
            rdRecord.RdCode = rdType.Code;
            rdRecord.RdFlag = rdType.Flag;
            rdRecord.VouchType = VouchType;
            rdRecord.Code = businessCommon.GetVouchCode(VouchType);
            rdRecord.Maker = operId;
            rdRecord.MakeDate = DateTime.Now;
            rdRecord.MakeTime = DateTime.Now;
            Uow.RdRecord.Add(rdRecord);
            Uow.Commit();

            List<DXInfo.Models.RdRecords> lrecords = new List<DXInfo.Models.RdRecords>();
            foreach (DXInfo.Models.ScrapVouchs scrapVouchs in lScrapVouchs)
            {
                DXInfo.Models.RdRecords rdRecordSub = Mapper.Map<DXInfo.Models.ScrapVouchs, DXInfo.Models.RdRecords>(scrapVouchs);
                rdRecordSub.InvId = scrapVouchs.InvId;
                lrecords.Add(rdRecordSub);
            }                        

            foreach (DXInfo.Models.RdRecords recordSub in lrecords)
            {
                recordSub.RdId = rdRecord.Id;
                Uow.RdRecords.Add(recordSub);

            }
            Uow.Commit();
        }

        private VouchModel verifyScrapVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == Id);
                if (oldScrapVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                }
                if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                oldScrapVouch.IsVerify = true;
                oldScrapVouch.Verifier = operId;
                oldScrapVouch.VerifyDate = DateTime.Now;
                oldScrapVouch.VerifyTime = DateTime.Now;
                Uow.ScrapVouch.Update(oldScrapVouch);

                List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Uow.ScrapVouchs.GetAll().Where(w => w.SVId == oldScrapVouch.Id).ToList();

                DXInfo.Models.RdType rdType = Uow.RdType.GetById(g => g.Code == "008");
                DXInfo.Models.BusType busType = Uow.BusType.GetById(g => g.Code == "008");
                AddRdRecordByScrapVouch(oldScrapVouch, lScrapVouchs, DXInfo.Models.VouchTypeCode.OtherOutStock, rdType, busType);

                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldScrapVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        private VouchModel unVerifyScrapVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.ScrapVouch OldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(OldScrapVouch.SVDate, OldScrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == Id).ToList();
                foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                {
                    if (rdRecord.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核");
                    }

                    Uow.RdRecord.Delete(rdRecord);
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    foreach (DXInfo.Models.RdRecords rdRecordSub in lRdRecords)
                    {
                        Uow.RdRecords.Delete(rdRecordSub);
                    }
                }

                OldScrapVouch.IsVerify = false;
                OldScrapVouch.Verifier = null;
                OldScrapVouch.VerifyDate = null;
                OldScrapVouch.VerifyTime = null;
                OldScrapVouch.Modifier = operId;
                OldScrapVouch.ModifyDate = DateTime.Now;
                OldScrapVouch.ModifyTime = DateTime.Now;
                Uow.ScrapVouch.Update(OldScrapVouch);
                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(OldScrapVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }

        public IQueryable GetScrapVouchsRequestData()
        {
            var records = from d in Uow.ScrapVouchs.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          select new
                          {
                              d.Id,
                              d.SVId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.Price,
                              d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              d.InvalidDate,
                              d.Locator,
                              LocatorName = dd5s.Name,
                              AvaNum = "",
                              d.Memo,
                          };
            return records;
        }
        private void addScrapVouchs(DXInfo.Models.ScrapVouchs scrapVouchs)
        {
            var oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouchs.SVId);
            if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            addScrapVouchs(scrapVouchs, oldScrapVouch.WhId);
        }
        private void addScrapVouchs(DXInfo.Models.ScrapVouchs scrapVouchs,Guid WhId)
        {            
            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == scrapVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                scrapVouchs.MainUnit = inv.MainUnit;
                scrapVouchs.STUnit = inv.MainUnit;
                scrapVouchs.ExchRate = 1;
                scrapVouchs.Quantity = scrapVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                scrapVouchs.MainUnit = inv.MainUnit;
                scrapVouchs.STUnit = inv.StockUnit.Value;
                scrapVouchs.ExchRate = uom.Rate;
                scrapVouchs.Quantity = scrapVouchs.Num * uom.Rate;
            }
            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == WhId && w.InvId == scrapVouchs.InvId && w.Batch == scrapVouchs.Batch).FirstOrDefault();
            scrapVouchs.Price = currentStock.Price;
            scrapVouchs.Amount = scrapVouchs.Num * scrapVouchs.Price;
            scrapVouchs.MadeDate = currentStock.MadeDate;
            scrapVouchs.ShelfLife = currentStock.ShelfLife;
            scrapVouchs.ShelfLifeType = currentStock.ShelfLifeType;
            scrapVouchs.InvalidDate = getInvalidDate(scrapVouchs.ShelfLifeType.Value, scrapVouchs.ShelfLife.Value, scrapVouchs.MadeDate.Value);

            Uow.ScrapVouchs.Add(scrapVouchs);
            Uow.Commit();
        }
        private void editScrapVouchs(DXInfo.Models.ScrapVouchs scrapVouchs)
        {
            var oldScrapVouchs = Uow.ScrapVouchs.GetById(g => g.Id == scrapVouchs.Id);
            var oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouchs.SVId);
            if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            oldScrapVouchs.InvId = scrapVouchs.InvId;
            oldScrapVouchs.Num = scrapVouchs.Num;
            oldScrapVouchs.Batch = scrapVouchs.Batch;
            oldScrapVouchs.Locator = scrapVouchs.Locator;
            oldScrapVouchs.Memo = scrapVouchs.Memo;

            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == scrapVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                oldScrapVouchs.MainUnit = inv.MainUnit;
                oldScrapVouchs.STUnit = inv.MainUnit;
                oldScrapVouchs.ExchRate = 1;
                oldScrapVouchs.Quantity = scrapVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                oldScrapVouchs.MainUnit = inv.MainUnit;
                oldScrapVouchs.STUnit = inv.StockUnit.Value;
                oldScrapVouchs.ExchRate = uom.Rate;
                oldScrapVouchs.Quantity = oldScrapVouchs.Num * uom.Rate;
            }
            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == oldScrapVouch.WhId && w.InvId == scrapVouchs.InvId && w.Batch == scrapVouchs.Batch).FirstOrDefault();
            oldScrapVouchs.Price = currentStock.Price;
            oldScrapVouchs.Amount = scrapVouchs.Num * oldScrapVouchs.Price;
            oldScrapVouchs.MadeDate = currentStock.MadeDate;
            oldScrapVouchs.ShelfLife = currentStock.ShelfLife;
            oldScrapVouchs.ShelfLifeType = currentStock.ShelfLifeType;
            oldScrapVouchs.InvalidDate = getInvalidDate(oldScrapVouchs.ShelfLifeType.Value, oldScrapVouchs.ShelfLife.Value, oldScrapVouchs.MadeDate.Value);
            Uow.ScrapVouchs.Update(oldScrapVouchs);
            Uow.Commit();
        }
        private void delScrapVouchs(DXInfo.Models.ScrapVouchs scrapVouchs)
        {
            var oldScrapVouchs = Uow.ScrapVouchs.GetById(g => g.Id == scrapVouchs.Id);
            if (oldScrapVouchs != null)
            {
                var oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == oldScrapVouchs.SVId);
                if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                Uow.ScrapVouchs.Delete(oldScrapVouchs);
                Uow.Commit();
            }
        }

        public ActionResult ScrapVouch()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.ScrapVouch);
        }

        private VouchModel GetScrapVouchAuthorityData(DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.ScrapVouch.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        Code=d.Code,
                        DeptId=d.DeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        VerifyDate=d.VerifyDate,
                        Maker=d.Maker,
                        MakeTime = (DateTime?)d.MakeTime,
                        Memo=d.Memo,
                        SVDate=(DateTime?)d.SVDate,
                        Salesman = (Guid?)d.Salesman,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        WhId = (Guid?)d.WhId,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, null, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetScrapVouchOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetScrapVouchAuthorityData(makeTime, true);
        }
        private VouchModel GetScrapVouchOrderBy(string VouchType,DateTime? makeTime)
        {
            return GetScrapVouchAuthorityData(makeTime, false);            
        }

        public IQueryable GetScrapVouchRequestData()
        {
            var records = from dd6s in Uow.ScrapVouch.GetAll()
                          join d6 in Uow.ScrapVouchs.GetAll() on dd6s.Id equals d6.SVId into dd6
                          from d in dd6.DefaultIfEmpty()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          join d7 in Uow.Warehouse.GetAll() on dd6s.WhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()
                          join d10 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d10.UserId into dd10
                          from dd10s in dd10.DefaultIfEmpty()
                          join d12 in Uow.Locator.GetAll() on d.Locator equals d12.Id into dd12
                          from dd12s in dd12.DefaultIfEmpty()
                          join d13 in Uow.Depts.GetAll() on dd6s.DeptId equals d13.DeptId into dd13
                          from dd13s in dd13.DefaultIfEmpty()
                          select new
                          {
                              dd6s.Id,
                              dd6s.Code,
                              dd6s.SVDate,
                              dd6s.DeptId,
                              dd6s.WhId,
                              WhName = dd7s.Name,                           
                              dd6s.Salesman,
                              SalesmanName = dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.ExchRate,
                              d.Quantity,
                              d.Num,
                              d.Price,
                              d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              LocatorName = dd5s.Name,
                              SubMemo=d.Memo,
                              dd13s.OrganizationId,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;                    
        }
        #endregion

        #region 库存月结周期
        private void SetupPeriodGridModel(JQGrid grid)
        {
            this.SetUpGrid(grid);
            grid.DataUrl = Url.Action("Period_RequestData");
            grid.EditUrl = Url.Action("Period_EditData");
            this.SetRequiredColumn(grid, "Code");
            SetDateColumn(grid, "BeginDate");
            SetRequiredColumn(grid, "BeginDate");
            SetDateColumn(grid, "EndDate");
            SetRequiredColumn(grid, "EndDate");
        }
        public ActionResult Period()
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            return PartialView(gridModel);
        }
        public ActionResult Period_RequestData()
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            var period = from d in Uow.Period.GetAll()
                       select d;
            return QueryAndExcel(gridModel.PeriodGrid, period, "库存月结周期.xls");
        }
        private bool CheckPeriodCodeDup(string code)
        {
            return Uow.Period.GetAll().Where(w => w.Code == code).Count() > 0;
        }
        private bool CheckPeriodDateDup(DateTime beginDate, DateTime endDate)
        {
            return Uow.Period.GetAll().Where(w => (w.BeginDate >= beginDate && w.EndDate <= endDate)
                ||(beginDate>=w.BeginDate && endDate<=w.EndDate)
                ||(w.BeginDate>=beginDate && w.BeginDate<=endDate)
                ||(w.EndDate>=beginDate && w.EndDate<=endDate)
                ||(beginDate>=w.BeginDate && beginDate<=w.EndDate)
                ||(endDate>=w.BeginDate&&endDate<=w.EndDate)).Count()>0;
        }
        private void addPeriod(DXInfo.Models.Period period)
        {
            if (CheckPeriodCodeDup(period.Code))
            {
                throw new DXInfo.Models.BusinessException("编码重复");

            }
            if (CheckPeriodDateDup(period.BeginDate, period.EndDate))
            {
                throw new DXInfo.Models.BusinessException("时间段重复");
            }
            if (period.EndDate <= period.BeginDate)
                throw new DXInfo.Models.BusinessException("开始日期必须小于结束日期");
            Uow.Period.Add(period);
            Uow.Commit();
        }
        private void editPeriod(DXInfo.Models.Period period)
        {
            var oldPeriod = Uow.Period.GetById(g => g.Id == period.Id);
            if (oldPeriod.Code != period.Code && CheckPeriodCodeDup(period.Code))
            {
                throw new DXInfo.Models.BusinessException("编码重复");

            }
            if (oldPeriod.BeginDate != period.BeginDate || oldPeriod.EndDate != period.EndDate)
            {
                if (CheckPeriodDateDup(period.BeginDate, period.EndDate))
                {
                    throw new DXInfo.Models.BusinessException("时间段重复");
                }
            }
            if (period.EndDate <= period.BeginDate)
                throw new DXInfo.Models.BusinessException("开始日期必须小于结束日期");

            var count = Uow.MonthBalance.GetAll().Where(w => w.Period == period.Id).Count();
            if (count > 0)
            {
                if (oldPeriod.BeginDate != period.BeginDate || oldPeriod.EndDate != period.EndDate)
                    throw new DXInfo.Models.BusinessException("已使用月结周期不能修改时间段");
            }
            oldPeriod.Code = period.Code;
            oldPeriod.BeginDate = period.BeginDate;
            oldPeriod.EndDate = period.EndDate;
            oldPeriod.Memo = period.Memo;
            Uow.Period.Update(oldPeriod);
            Uow.Commit();
        }
        private void delPeriod(DXInfo.Models.Period period)
        {
            var count = Uow.MonthBalance.GetAll().Where(w => w.Period == period.Id).Count();
            if (count > 0) throw new DXInfo.Models.BusinessException("已使用不能删除");
            var oldPeriod = Uow.Period.GetById(g => g.Id == period.Id);
            Uow.Period.Delete(oldPeriod);
            Uow.Commit();
        }
        public ActionResult Period_EditData(DXInfo.Models.Period period)
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            return ajaxCallBack<DXInfo.Models.Period>(gridModel.PeriodGrid, period, addPeriod, editPeriod, delPeriod);
        }
        #endregion

        #region 库存盘点单
        public VouchModel addCheckVouch(VouchsGridModel vouchGridModel)
        {
            VouchModel retVouchModel = new VouchModel();
            if (businessCommon.IsBalance(vouchGridModel.CVDate.Value, vouchGridModel.WhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            CheckCodeDup(vouchGridModel.VouchType, vouchGridModel.Code);

            using (TransactionScope transaction = new TransactionScope())
            {

                int count = Uow.CheckVouch.GetAll().Where(w => !w.IsVerify && w.WhId == vouchGridModel.WhId).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("有未审核盘点单，不能添加盘点单");
                DXInfo.Models.CheckVouch newCheckVouch = Mapper.Map<DXInfo.Models.CheckVouch>(vouchGridModel);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newCheckVouch.WhId);
                newCheckVouch.DeptId = warehouse.Dept;

                newCheckVouch.Maker = operId;
                newCheckVouch.MakeDate = DateTime.Now;
                newCheckVouch.MakeTime = DateTime.Now;

                newCheckVouch.OutRdCode = "007";
                newCheckVouch.InRdCode = "004";

                Uow.CheckVouch.Add(newCheckVouch);
                Uow.Commit();
                if (isLocator)
                {
                    List<DXInfo.Models.CurrentInvLocator> lCurrentInvLocator =
                        Uow.Db.SqlQuery<DXInfo.Models.CurrentInvLocator>("sp_DXInfo_GetCurrentInvLocatorOfCheck @WhId",
                new SqlParameter("WhId", newCheckVouch.WhId)).ToList();
                        //Uow.CurrentInvLocator.GetAll()
                        //.Where(w => w.WhId == newCheckVouch.WhId)
                        //.ToList();

                    foreach (DXInfo.Models.CurrentInvLocator currentInvLocator in lCurrentInvLocator)
                    {
                        DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentInvLocator);
                        checkVouchs.CVId = newCheckVouch.Id;
                        Uow.CheckVouchs.Add(checkVouchs);
                    }
                }
                else
                {
                    List<DXInfo.Models.CurrentStock> lCurrentStock =
                        Uow.Db.SqlQuery<DXInfo.Models.CurrentStock>("sp_DXInfo_GetCurrentStockOfCheck @WhId",
                new SqlParameter("WhId", newCheckVouch.WhId)).ToList();
                        //Uow.CurrentStock.GetAll().Where(w => w.WhId == newCheckVouch.WhId).ToList();

                    foreach (DXInfo.Models.CurrentStock currentStock in lCurrentStock)
                    {
                        DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentStock);
                        checkVouchs.CVId = newCheckVouch.Id;
                        Uow.CheckVouchs.Add(checkVouchs);
                    }
                }
                Uow.Commit();
                transaction.Complete();

                retVouchModel = Mapper.Map<VouchModel>(newCheckVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        public VouchModel editCheckVouch(VouchModel vouchModel)
        {
            DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldCheckVouch.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核");
            }
            oldCheckVouch = Mapper.Map<VouchModel, DXInfo.Models.CheckVouch>(vouchModel, oldCheckVouch);
            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == oldCheckVouch.WhId);
            oldCheckVouch.DeptId = warehouse.Dept;
            oldCheckVouch.Modifier = operId;
            oldCheckVouch.ModifyDate = DateTime.Now;
            oldCheckVouch.ModifyTime = DateTime.Now;
            Uow.CheckVouch.Update(oldCheckVouch);

            List<DXInfo.Models.CheckVouchs> lCheckVouchs = Uow.CheckVouchs.GetAll().Where(w => w.CVId == oldCheckVouch.Id).ToList();
            if (isLocator)
            {
                List<DXInfo.Models.CurrentInvLocator> lCurrentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == oldCheckVouch.WhId).ToList();

                foreach (DXInfo.Models.CurrentInvLocator currentInvLocator in lCurrentInvLocator)
                {
                    var count = lCheckVouchs.Where(w => w.InvId == currentInvLocator.InvId && w.Batch == currentInvLocator.Batch && w.Locator == currentInvLocator.Locator).Count();
                    if (count == 0)
                    {
                        DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentInvLocator);
                        checkVouchs.CVId = oldCheckVouch.Id;
                        Uow.CheckVouchs.Add(checkVouchs);
                    }
                }
            }
            else
            {
                List<DXInfo.Models.CurrentStock> lCurrentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == oldCheckVouch.WhId).ToList();
                foreach (DXInfo.Models.CurrentStock currentStock in lCurrentStock)
                {
                    var count = lCheckVouchs.Where(w => w.InvId == currentStock.InvId && w.Batch == currentStock.Batch).Count();
                    if (count == 0)
                    {
                        DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentStock);
                        checkVouchs.CVId = oldCheckVouch.Id;
                        Uow.CheckVouchs.Add(checkVouchs);
                    }
                }
            }
            Uow.Commit();
            return vouchModel;
        }
        public DXInfo.Models.JsonObject delCheckVouch(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == vouchModel.Id);
                if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldCheckVouch == null) throw new DXInfo.Models.BusinessException("空记录");
                if (oldCheckVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                Uow.CheckVouch.Delete(oldCheckVouch);

                List<DXInfo.Models.CheckVouchs> lCheckVouchs = Uow.CheckVouchs.GetAll().Where(w => w.CVId == vouchModel.Id).ToList();
                foreach (DXInfo.Models.CheckVouchs checkVouchs in lCheckVouchs)
                {
                    Uow.CheckVouchs.Delete(checkVouchs);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }
        private void AddRdRecordByCheckVouch(DXInfo.Models.CheckVouch checkVouch, 
            List<DXInfo.Models.CheckVouchs> lCheckVouchs, string VouchType, 
            DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType)
        {
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.CheckVouch, DXInfo.Models.RdRecord>(checkVouch);
            rdRecord.SourceCode = checkVouch.Code;
            rdRecord.SourceId = checkVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = VouchType;
            rdRecord.Maker = rdRecord.Verifier.Value;
            rdRecord.MakeDate = rdRecord.MakeDate;
            rdRecord.MakeTime = rdRecord.MakeTime;
            rdRecord.IsVerify = false;
            rdRecord.Verifier = null;
            rdRecord.VerifyDate = null;
            rdRecord.VerifyTime = null;
            rdRecord.RdDate = checkVouch.CVDate;
            rdRecord.Memo = rdRecord.Memo + "盘点单号：" + checkVouch.Code;
            rdRecord.RdFlag = rdType.Flag;
            rdRecord.RdCode = rdType.Code;
            rdRecord.VouchType = VouchType;
            rdRecord.Code = businessCommon.GetVouchCode(VouchType);
            rdRecord.Maker = operId;
            rdRecord.MakeDate = DateTime.Now;
            rdRecord.MakeTime = DateTime.Now;
            Uow.RdRecord.Add(rdRecord);
            

            List<DXInfo.Models.RdRecords> lrecords = new List<DXInfo.Models.RdRecords>();
            foreach (DXInfo.Models.CheckVouchs checkVouchs in lCheckVouchs)
            {
                DXInfo.Models.RdRecords rdRecordSub = Mapper.Map<DXInfo.Models.CheckVouchs, DXInfo.Models.RdRecords>(checkVouchs);
                if (rdType.Flag == 0 && checkVouchs.AddInQuantity>0)
                {
                    rdRecordSub.Quantity = checkVouchs.AddInQuantity;
                    rdRecordSub.Num = checkVouchs.AddInNum;
                    rdRecordSub.Amount = checkVouchs.AddInAmount;
                    rdRecordSub.InvId = checkVouchs.InvId;
                    lrecords.Add(rdRecordSub);
                }
                if (rdType.Flag == 1 && checkVouchs.AddOutQuantity > 0)
                {
                    rdRecordSub.Quantity = checkVouchs.AddOutQuantity;
                    rdRecordSub.Num = checkVouchs.AddOutNum;
                    rdRecordSub.Amount = checkVouchs.AddOutAmount;
                    rdRecordSub.InvId = checkVouchs.InvId;
                    lrecords.Add(rdRecordSub);
                }
                
            }
            if (lrecords.Count > 0)
            {
                Uow.Commit();
                foreach (DXInfo.Models.RdRecords recordSub in lrecords)
                {
                    recordSub.RdId = rdRecord.Id;

                    Uow.RdRecords.Add(recordSub);

                }
                Uow.Commit();
            }
        }

        public VouchModel verifyCheckVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();

            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == Id);
                if (oldCheckVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                }
                if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                oldCheckVouch.IsVerify = true;
                oldCheckVouch.Verifier = operId;
                oldCheckVouch.VerifyDate = DateTime.Now;
                oldCheckVouch.VerifyTime = DateTime.Now;
                Uow.CheckVouch.Update(oldCheckVouch);

                List<DXInfo.Models.CheckVouchs> lCheckVouchs = Uow.CheckVouchs.GetAll().Where(w => w.CVId == oldCheckVouch.Id).ToList();


                DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g => g.Code == "007");
                DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g => g.Code == "007");
                //DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                AddRdRecordByCheckVouch(oldCheckVouch, lCheckVouchs, DXInfo.Models.VouchTypeCode.OtherOutStock, outRdType, outBusType);

                DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g => g.Code == "004");
                DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g => g.Code == "004");
                //DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                AddRdRecordByCheckVouch(oldCheckVouch, lCheckVouchs, DXInfo.Models.VouchTypeCode.OtherInStock, inRdType, inBusType);


                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldCheckVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        public VouchModel unVerifyCheckVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.CheckVouch OldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(OldCheckVouch.CVDate, OldCheckVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == Id).ToList();
                foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                {
                    if (rdRecord.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核");
                    }

                    Uow.RdRecord.Delete(rdRecord);
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    foreach (DXInfo.Models.RdRecords rdRecordSub in lRdRecords)
                    {
                        Uow.RdRecords.Delete(rdRecordSub);
                    }
                }

                OldCheckVouch.IsVerify = false;
                OldCheckVouch.Verifier = null;
                OldCheckVouch.VerifyDate = null;
                OldCheckVouch.VerifyTime = null;
                OldCheckVouch.Modifier = operId;
                OldCheckVouch.ModifyDate = DateTime.Now;
                OldCheckVouch.ModifyTime = DateTime.Now;
                Uow.CheckVouch.Update(OldCheckVouch);

                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(OldCheckVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }

        public IQueryable GetCheckVouchsRequestData()
        {

            var records = from d in Uow.CheckVouchs.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          select new
                          {
                              d.Id,
                              d.CVId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.CNum,
                              d.AddInNum,
                              d.AddOutNum,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              d.Locator,
                              LocatorName = dd5s == null ? "" : dd5s.Name,
                              d.Memo,
                          };
            return records;
        }

        private void addCheckVouchs(DXInfo.Models.CheckVouchs checkVouchs)
        {

        }
        private void editCheckVouchs(DXInfo.Models.CheckVouchs checkVouchs)
        {
            var oldCheckVouchs = Uow.CheckVouchs.GetById(g => g.Id == checkVouchs.Id);
            var oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == oldCheckVouchs.CVId);
            if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            oldCheckVouchs.CNum = checkVouchs.CNum;
            if (checkVouchs.CNum > oldCheckVouchs.Num)
            {
                oldCheckVouchs.AddInNum = checkVouchs.CNum - oldCheckVouchs.Num;
            }
            else
            {
                oldCheckVouchs.AddOutNum = oldCheckVouchs.Num - checkVouchs.CNum;
            }
            oldCheckVouchs.CQuantity = oldCheckVouchs.CNum * oldCheckVouchs.ExchRate;
            oldCheckVouchs.AddInQuantity = oldCheckVouchs.AddInNum * oldCheckVouchs.ExchRate;
            oldCheckVouchs.AddOutQuantity = oldCheckVouchs.AddOutNum * oldCheckVouchs.ExchRate;

            oldCheckVouchs.CAmount = oldCheckVouchs.CNum * oldCheckVouchs.Price;
            oldCheckVouchs.AddInAmount = oldCheckVouchs.CNum * oldCheckVouchs.Price;
            oldCheckVouchs.AddOutAmount = oldCheckVouchs.CNum * oldCheckVouchs.Price;
            oldCheckVouchs.Memo = checkVouchs.Memo;
            Uow.CheckVouchs.Update(oldCheckVouchs);
            Uow.Commit();
        }
        private void delCheckVouchs(DXInfo.Models.CheckVouchs checkVouchs)
        {

        }
        public ActionResult CheckVouch()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.CheckVouch);            
        }

        private VouchModel GetCheckVouchAuthorityData(string VouchType, DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.CheckVouch.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        Code=d.Code,
                        DeptId=d.DeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        VerifyDate=d.VerifyDate,
                        Maker=d.Maker,
                        MakeTime = (DateTime?)d.MakeTime,
                        Memo=d.Memo,
                        Salesman = (Guid?)d.Salesman,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        WhId = (Guid?)d.WhId,
                        CVDate=d.CVDate,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, null, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetCheckVouchOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetCheckVouchAuthorityData(VouchType, makeTime, true);            
        }
        private VouchModel GetCheckVouchOrderBy(string VouchType, DateTime? makeTime)
        {
            return GetCheckVouchAuthorityData(VouchType, makeTime, false);              
        }

        public IQueryable GetCheckVouchRequestData()
        {
            var records = from dd6s in Uow.CheckVouch.GetAll()
                          join d6 in Uow.CheckVouchs.GetAll() on dd6s.Id equals d6.CVId into dd6
                          from d in dd6.DefaultIfEmpty()

                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()

                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.Locator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          join d7 in Uow.Warehouse.GetAll() on dd6s.WhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()
                          join d9 in Uow.Depts.GetAll() on dd6s.DeptId equals d9.DeptId into dd9
                          from dd9s in dd9.DefaultIfEmpty()

                          join d10 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d10.UserId into dd10
                          from dd10s in dd10.DefaultIfEmpty()
                          select new
                          {
                              dd6s.Id,
                              dd6s.Code,
                              dd6s.CVDate,
                              dd6s.DeptId,
                              DeptName = dd9s.DeptName,

                              dd6s.WhId,
                              WhName = dd7s.Name,

                              dd6s.Salesman,
                              SalesmanName = dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d == null ? dd6s.Id : d.Id,
                              LocatorName = dd5s.Name,
                              InvId=d==null?Guid.Empty:d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              Num=d==null?0:d.Num,
                              CNum=d==null?0:d.CNum,
                              AddInNum=d==null?0:d.AddInNum,
                              AddOutNum=d==null?0:d.AddOutNum,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              SubMemo=d.Memo,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;
        }        
        #endregion

        #region 库存货位流水账
        private void SetupInvLocatorGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("InvLocator_RequestData");
            SetShelfLifeColumn(grid,false);
            SetDateColumn(grid, "ILDate");
            SetLocatorColumn(grid);
            SetBatchColumn(grid, false);
        }
        public ActionResult InvLocator()
        {
            var gridModel = new InvLocatorGridModel();
            SetupInvLocatorGridModel(gridModel.InvLocatorGrid);
            return PartialView(gridModel);
        }
        public ActionResult InvLocator_RequestData()
        {
            var gridModel = new InvLocatorGridModel();
            SetupInvLocatorGridModel(gridModel.InvLocatorGrid);
            Guid dept = Guid.Empty;
            var invLocator = from d in Uow.InvLocator.GetAll()

                             join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                             from dd2s in dd2.DefaultIfEmpty()
                             join d3 in Uow.Vendor.GetAll() on d.VenId equals d3.Id into dd3
                             from dd3s in dd3.DefaultIfEmpty()
                             join d4 in Uow.Locator.GetAll() on d.Locator equals d4.Id into dd4
                             from dd4s in dd4.DefaultIfEmpty()
                             join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                             from dd5s in dd5.DefaultIfEmpty()
                             join d6 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d6.Id into dd6
                             from dd6s in dd6.DefaultIfEmpty()
                             join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                             from dd7s in dd7.DefaultIfEmpty()
                             join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                             from dd8s in dd8.DefaultIfEmpty()
                             join d9 in Uow.aspnet_CustomProfile.GetAll() on d.Salesman equals d9.UserId into dd9
                             from dd9s in dd9.DefaultIfEmpty()
                             join d10 in Uow.Depts.GetAll() on dd2s.Dept equals d10.DeptId into dd10
                             from dd10s in dd10.DefaultIfEmpty()

                             select new
                             {
                                 d.Id,
                                 RdFlag=d.RdFlag==0?"入库":"出库",
                                 d.ILDate,
                                 DeptId=dd2s==null?dept:dd2s.Dept,
                                 dd10s.OrganizationId,
                                 WhName = dd2s.Name,   
                                 VenName = dd3s.Name,
                                 LocatorName = dd4s.Name,
                                 InvName = dd5s.Name,
                                 dd5s.Specs,
                                 STUnitName = dd7s.Name,
                                 d.Num,
                                 d.Batch,
                                 d.MadeDate,
                                 d.ShelfLife,
                                 ShelfLifeTypeName = dd8s.Description,
                                 d.InvalidDate,
                                 SalesmanName=dd9s.FullName,
                                 
                             };
            var q = businessCommon.SetVouchAuthority(invLocator, "", false);
            return QueryAndExcel(gridModel.InvLocatorGrid, q, "库存货位流水账.xls");
        }
        #endregion

        #region 库存货位调整单
        private VouchModel addAdjustLocatorVouch(VouchsGridModel vouchsGridModel)
        {
            if (businessCommon.IsBalance(vouchsGridModel.ALVDate.Value, vouchsGridModel.WhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Mapper.Map<List<DXInfo.Models.AdjustLocatorVouchs>>(vouchsGridModel.lVouchs);
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckCodeDup(vouchsGridModel.VouchType, vouchsGridModel.Code);
                DXInfo.Models.AdjustLocatorVouch newAdjustLocatorVouch = Mapper.Map<DXInfo.Models.AdjustLocatorVouch>(vouchsGridModel);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newAdjustLocatorVouch.WhId);
                newAdjustLocatorVouch.DeptId = warehouse.Dept;
                newAdjustLocatorVouch.Maker = operId;
                newAdjustLocatorVouch.MakeDate = DateTime.Now;
                newAdjustLocatorVouch.MakeTime = DateTime.Now;

                

                Uow.AdjustLocatorVouch.Add(newAdjustLocatorVouch);
                Uow.Commit();
                foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
                {
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == adjustLocatorVouchs.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        adjustLocatorVouchs.MainUnit = inv.MainUnit;
                        adjustLocatorVouchs.STUnit = inv.MainUnit;
                        adjustLocatorVouchs.ExchRate = 1;
                        adjustLocatorVouchs.Quantity = adjustLocatorVouchs.Num;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue)
                            throw new DXInfo.Models.BusinessException("请设置库存单位");
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        adjustLocatorVouchs.MainUnit = inv.MainUnit;
                        adjustLocatorVouchs.STUnit = inv.StockUnit.Value;
                        adjustLocatorVouchs.ExchRate = uom.Rate;
                        adjustLocatorVouchs.Quantity = adjustLocatorVouchs.Num * uom.Rate;
                    }

                    DXInfo.Models.CurrentInvLocator currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == newAdjustLocatorVouch.WhId
                    && w.Locator == adjustLocatorVouchs.OutLocator && w.Batch == adjustLocatorVouchs.Batch).FirstOrDefault();

                    adjustLocatorVouchs.Price = currentInvLocator.Price;
                    adjustLocatorVouchs.Amount = adjustLocatorVouchs.Num * adjustLocatorVouchs.Price;
                    adjustLocatorVouchs.MadeDate = currentInvLocator.MadeDate;
                    adjustLocatorVouchs.ShelfLife = currentInvLocator.ShelfLife;
                    adjustLocatorVouchs.ShelfLifeType = currentInvLocator.ShelfLifeType;
                    adjustLocatorVouchs.InvalidDate = currentInvLocator.InvalidDate;

                    adjustLocatorVouchs.ALVId = newAdjustLocatorVouch.Id;
                    Uow.AdjustLocatorVouchs.Add(adjustLocatorVouchs);
                    Uow.Commit();

                }
                Uow.Commit();
                transaction.Complete();

                VouchModel retVouchModel = Mapper.Map<VouchModel>(newAdjustLocatorVouch);
                retVouchModel.IsModify = true;
                return retVouchModel;
            }
        }
        public VouchModel editAdjustLocatorVouch(VouchModel vouchModel)
        {
            DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldAdjustLocatorVouch.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核");
            }
            oldAdjustLocatorVouch = Mapper.Map<VouchModel, DXInfo.Models.AdjustLocatorVouch>(vouchModel, oldAdjustLocatorVouch);
            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == oldAdjustLocatorVouch.WhId);
            oldAdjustLocatorVouch.DeptId = warehouse.Dept;
            oldAdjustLocatorVouch.Modifier = operId;
            oldAdjustLocatorVouch.ModifyDate = DateTime.Now;
            oldAdjustLocatorVouch.ModifyTime = DateTime.Now;
            Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);
            Uow.Commit();
            return vouchModel;
        }
        private DXInfo.Models.JsonObject delAdjustLocatorVouch(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == vouchModel.Id);
                if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldAdjustLocatorVouch == null) throw new DXInfo.Models.BusinessException("空记录");
                if (oldAdjustLocatorVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                Uow.AdjustLocatorVouch.Delete(oldAdjustLocatorVouch);

                List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == vouchModel.Id).ToList();
                foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
                {
                    Uow.AdjustLocatorVouchs.Delete(adjustLocatorVouchs);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }

        private void AddInvLocatorByAdjustLocatorVouch(DXInfo.Models.AdjustLocatorVouch adjustLocatorVouch, List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs, DXInfo.Models.VouchType vouchType)
        {
            foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
            {
                //入库
                DXInfo.Models.InvLocator inInvLocator = Mapper.Map<DXInfo.Models.InvLocator>(adjustLocatorVouchs);
                inInvLocator.ILDate = adjustLocatorVouch.ALVDate;
                inInvLocator.RdFlag = 0;
                inInvLocator.WhId = adjustLocatorVouch.WhId;
                inInvLocator.Locator = adjustLocatorVouchs.InLocator;
                inInvLocator.SourceId = adjustLocatorVouch.Id;
                inInvLocator.SourcesId = adjustLocatorVouchs.Id;
                inInvLocator.SourceVouchType = vouchType.Code;
                inInvLocator.Salesman = adjustLocatorVouch.Verifier.Value;
                Uow.InvLocator.Add(inInvLocator);
                //出库
                DXInfo.Models.InvLocator outInvLocator = Mapper.Map<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.InvLocator>(adjustLocatorVouchs);
                outInvLocator.ILDate = adjustLocatorVouch.ALVDate;
                outInvLocator.RdFlag = 1;
                outInvLocator.WhId = adjustLocatorVouch.WhId;
                outInvLocator.Locator = adjustLocatorVouchs.OutLocator;
                outInvLocator.SourceId = adjustLocatorVouch.Id;
                outInvLocator.SourcesId = adjustLocatorVouchs.Id;
                outInvLocator.SourceVouchType = vouchType.Code;
                outInvLocator.Salesman = adjustLocatorVouch.Verifier.Value;
                Uow.InvLocator.Add(outInvLocator);
            }
        }
        private void AddRdRecordByAdjustLocatorVouch(DXInfo.Models.AdjustLocatorVouch adjustLocatorVouch, 
            List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs, 
            string VouchType, DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType)
        {
            DXInfo.Models.RdRecord newRdRecord = Mapper.Map<DXInfo.Models.RdRecord>(adjustLocatorVouch);
            newRdRecord.SourceCode = adjustLocatorVouch.Code;
            newRdRecord.SourceId = adjustLocatorVouch.Id;
            newRdRecord.BusType = busType.Code;
            newRdRecord.VouchType = VouchType;
            newRdRecord.Maker = newRdRecord.Verifier.Value;
            newRdRecord.MakeDate = newRdRecord.VerifyDate.Value;
            newRdRecord.MakeTime = newRdRecord.VerifyTime.Value;

            newRdRecord.IsVerify = false;
            newRdRecord.Verifier = null;
            newRdRecord.VerifyDate = null;
            newRdRecord.VerifyTime = null;
            newRdRecord.RdDate = adjustLocatorVouch.ALVDate;
            newRdRecord.Memo = newRdRecord.Memo + "货位调整单号：" + adjustLocatorVouch.Code;
            newRdRecord.RdCode = rdType.Code;
            newRdRecord.RdFlag = rdType.Flag;
            newRdRecord.VouchType = VouchType;
            newRdRecord.Code = businessCommon.GetVouchCode(VouchType);
            newRdRecord.Maker = operId;
            newRdRecord.MakeDate = DateTime.Now;
            newRdRecord.MakeTime = DateTime.Now;
            Uow.RdRecord.Add(newRdRecord);
            Uow.Commit();
            List<DXInfo.Models.RdRecords> lrecords = new List<DXInfo.Models.RdRecords>();
            foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
            {
                DXInfo.Models.RdRecords rdRecords = Mapper.Map<DXInfo.Models.RdRecords>(adjustLocatorVouchs);
                if (rdType.Flag == 0)
                {
                    rdRecords.Locator = adjustLocatorVouchs.InLocator;
                }
                else
                {
                    rdRecords.Locator = adjustLocatorVouchs.OutLocator;
                }
                rdRecords.RdId = newRdRecord.Id;
                Uow.RdRecords.Add(rdRecords);
            }

            Uow.Commit();
        }        
        private void updateCurrentInvLocatorByALV(DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch, List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs)
        {
            DXInfo.Models.RdRecord rdRecord1 = Mapper.Map<DXInfo.Models.RdRecord>(oldAdjustLocatorVouch);
            rdRecord1.RdFlag = 0;
            DXInfo.Models.RdRecord rdRecord2 = Mapper.Map<DXInfo.Models.RdRecord>(oldAdjustLocatorVouch);
            rdRecord2.RdFlag = 1;

            List<DXInfo.Models.RdRecords> lRdRecords1 = new List<DXInfo.Models.RdRecords>();
            List<DXInfo.Models.RdRecords> lRdRecords2 = new List<DXInfo.Models.RdRecords>();
            foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
            {
                DXInfo.Models.RdRecords rdRecords1 = Mapper.Map<DXInfo.Models.RdRecords>(adjustLocatorVouchs);
                rdRecords1.Locator = adjustLocatorVouchs.InLocator;
                DXInfo.Models.RdRecords rdRecords2 = Mapper.Map<DXInfo.Models.RdRecords>(adjustLocatorVouchs);
                rdRecords2.Locator = adjustLocatorVouchs.OutLocator;

                lRdRecords1.Add(rdRecords1);
                lRdRecords2.Add(rdRecords2);
            }
            UpdateCurrentInvLocator(rdRecord1, lRdRecords1);
            UpdateCurrentInvLocator(rdRecord2, lRdRecords2);
        }

        private VouchModel verifyAdjustLocatorVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                oldAdjustLocatorVouch.IsVerify = true;
                oldAdjustLocatorVouch.Verifier = operId;
                oldAdjustLocatorVouch.VerifyDate = DateTime.Now;
                oldAdjustLocatorVouch.VerifyTime = DateTime.Now;
                Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);

                List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == oldAdjustLocatorVouch.Id).ToList();
                if (lAdjustLocatorVouchs.Count > 0)
                {
                    //DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                    DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g => g.Code == "005");
                    DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g => g.Code == "005");
                    AddRdRecordByAdjustLocatorVouch(oldAdjustLocatorVouch, lAdjustLocatorVouchs, DXInfo.Models.VouchTypeCode.OtherInStock, inRdType, inBusType);
                    //DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g => g.Code == "009");
                    DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g => g.Code == "009");
                    AddRdRecordByAdjustLocatorVouch(oldAdjustLocatorVouch, lAdjustLocatorVouchs, DXInfo.Models.VouchTypeCode.OtherOutStock, outRdType, outBusType);
                }
                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldAdjustLocatorVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        private VouchModel unVerifyAdjustLocatorVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();

            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == Id);
                if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }

                List<DXInfo.Models.InvLocator> lInvLocator = Uow.InvLocator.GetAll().Where(w => w.SourceId == Id).ToList();
                foreach (DXInfo.Models.InvLocator invLocator in lInvLocator)
                {
                    Uow.InvLocator.Delete(invLocator);
                }

                oldAdjustLocatorVouch.IsVerify = false;
                oldAdjustLocatorVouch.Verifier = null;
                oldAdjustLocatorVouch.VerifyDate = null;
                oldAdjustLocatorVouch.VerifyTime = null;
                oldAdjustLocatorVouch.Modifier = operId;
                oldAdjustLocatorVouch.ModifyDate = DateTime.Now;
                oldAdjustLocatorVouch.ModifyTime = DateTime.Now;
                Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);
                List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == oldAdjustLocatorVouch.Id).ToList();

                updateCurrentInvLocatorByALV(oldAdjustLocatorVouch, lAdjustLocatorVouchs);

                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldAdjustLocatorVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }

        private IQueryable GetAdjustLocatorVouchsRequestData()
        {
            var records = from d in Uow.AdjustLocatorVouchs.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.OutLocator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()

                          join d6 in Uow.Locator.GetAll() on d.InLocator equals d6.Id into dd6
                          from dd6s in dd6.DefaultIfEmpty()

                          select new
                          {
                              d.Id,
                              d.ALVId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              d.OutLocator,
                              OutLocatorName = dd5s.Name,
                              d.InLocator,
                              InLocatorName = dd6s.Name,
                              AvaNum = "",
                              d.Memo,
                          };
            return records;
        }

        private void addAdjustLocatorVouchs(DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs)
        {
            var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouchs.ALVId);
            if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == adjustLocatorVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                adjustLocatorVouchs.MainUnit = inv.MainUnit;
                adjustLocatorVouchs.STUnit = inv.MainUnit;
                adjustLocatorVouchs.ExchRate = 1;
                adjustLocatorVouchs.Quantity = adjustLocatorVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                adjustLocatorVouchs.MainUnit = inv.MainUnit;
                adjustLocatorVouchs.STUnit = inv.StockUnit.Value;
                adjustLocatorVouchs.ExchRate = uom.Rate;
                adjustLocatorVouchs.Quantity = adjustLocatorVouchs.Num * uom.Rate;
            }

            DXInfo.Models.CurrentInvLocator currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == oldAdjustLocatorVouch.WhId
            && w.Locator == adjustLocatorVouchs.OutLocator && w.Batch == adjustLocatorVouchs.Batch).FirstOrDefault();

            adjustLocatorVouchs.MadeDate = currentInvLocator.MadeDate;
            adjustLocatorVouchs.ShelfLife = currentInvLocator.ShelfLife;
            adjustLocatorVouchs.ShelfLifeType = currentInvLocator.ShelfLifeType;
            adjustLocatorVouchs.InvalidDate = currentInvLocator.InvalidDate;

            Uow.AdjustLocatorVouchs.Add(adjustLocatorVouchs);
            Uow.Commit();
        }
        private void editAdjustLocatorVouchs(DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs)
        {
            if (adjustLocatorVouchs.OutLocator == adjustLocatorVouchs.InLocator)
            {
                throw new DXInfo.Models.BusinessException("调整后货位不能相同");
            }
            var oldAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == adjustLocatorVouchs.Id);
            var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouchs.ALVId);
            if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            oldAdjustLocatorVouchs.InvId = adjustLocatorVouchs.InvId;
            oldAdjustLocatorVouchs.Num = adjustLocatorVouchs.Num;
            oldAdjustLocatorVouchs.Batch = adjustLocatorVouchs.Batch;
            oldAdjustLocatorVouchs.InLocator = adjustLocatorVouchs.InLocator;
            oldAdjustLocatorVouchs.OutLocator = adjustLocatorVouchs.OutLocator;

            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == adjustLocatorVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                oldAdjustLocatorVouchs.MainUnit = inv.MainUnit;
                oldAdjustLocatorVouchs.STUnit = inv.MainUnit;
                oldAdjustLocatorVouchs.ExchRate = 1;
                oldAdjustLocatorVouchs.Quantity = adjustLocatorVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                oldAdjustLocatorVouchs.MainUnit = inv.MainUnit;
                oldAdjustLocatorVouchs.STUnit = inv.StockUnit.Value;
                oldAdjustLocatorVouchs.ExchRate = uom.Rate;
                oldAdjustLocatorVouchs.Quantity = oldAdjustLocatorVouchs.Num * uom.Rate;
            }

            DXInfo.Models.CurrentInvLocator currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == oldAdjustLocatorVouch.WhId
            && w.Locator == adjustLocatorVouchs.OutLocator && w.Batch == adjustLocatorVouchs.Batch).FirstOrDefault();

            oldAdjustLocatorVouchs.MadeDate = currentInvLocator.MadeDate;
            oldAdjustLocatorVouchs.ShelfLife = currentInvLocator.ShelfLife;
            oldAdjustLocatorVouchs.ShelfLifeType = currentInvLocator.ShelfLifeType;
            oldAdjustLocatorVouchs.InvalidDate = currentInvLocator.InvalidDate;
            oldAdjustLocatorVouchs.Memo = adjustLocatorVouchs.Memo;
            Uow.AdjustLocatorVouchs.Update(oldAdjustLocatorVouchs);
            Uow.Commit();
        }
        private void delAdjustLocatorVouchs(DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs)
        {
            if (adjustLocatorVouchs.OutLocator == adjustLocatorVouchs.InLocator)
            {
                throw new DXInfo.Models.BusinessException("调整后货位不能相同");
            }
            var oldAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == adjustLocatorVouchs.Id);
            if (oldAdjustLocatorVouchs != null)
            {
                var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == oldAdjustLocatorVouchs.ALVId);
                if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                Uow.AdjustLocatorVouchs.Delete(oldAdjustLocatorVouchs);
                Uow.Commit();
            }
        }

        public ActionResult AdjustLocatorVouch()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.AdjustLocatorVouch);
        }

        private VouchModel GetAdjustLocatorVouchAuthorityData(string VouchType, DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.AdjustLocatorVouch.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        Code=d.Code,
                        DeptId=d.DeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        VerifyDate=d.VerifyDate,
                        Maker=d.Maker,
                        MakeTime = (DateTime?)d.MakeTime,
                        Memo=d.Memo,
                        Salesman = (Guid?)d.Salesman,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        WhId = (Guid?)d.WhId,
                        ALVDate=d.ALVDate,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, null, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetAdjustLocatorVouchOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetAdjustLocatorVouchAuthorityData(VouchType, makeTime, true);
        }
        private VouchModel GetAdjustLocatorVouchOrderBy(string VouchType, DateTime? makeTime)
        {
            return GetAdjustLocatorVouchAuthorityData(VouchType, makeTime, false);
        }

        public IQueryable GetAdjustLocatorVouchRequestData()
        {
            Guid ept = Guid.Empty;
            var records = from dd6s in Uow.AdjustLocatorVouch.GetAll()
                          join d6 in Uow.AdjustLocatorVouchs.GetAll() on dd6s.Id equals d6.ALVId into dd6
                          from d in dd6.DefaultIfEmpty()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d2 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d2.Id into dd2
                          from dd2s in dd2.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()
                          join d4 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d4.Value into dd4
                          from dd4s in dd4.DefaultIfEmpty()
                          join d5 in Uow.Locator.GetAll() on d.OutLocator equals d5.Id into dd5
                          from dd5s in dd5.DefaultIfEmpty()
                          join d7 in Uow.Warehouse.GetAll() on dd6s.WhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()
                          join d9 in Uow.Depts.GetAll() on dd6s.DeptId equals d9.DeptId into dd9
                          from dd9s in dd9.DefaultIfEmpty()
                          join d12 in Uow.Locator.GetAll() on d.InLocator equals d12.Id into dd12
                          from dd12s in dd12.DefaultIfEmpty()
                          join d13 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d13.UserId into dd13
                          from dd13s in dd13.DefaultIfEmpty()

                          select new
                          {
                              dd6s.Id,
                              dd6s.Code,
                              dd6s.ALVDate,
                              dd6s.DeptId,
                              dd6s.WhId,
                              WhName = dd7s.Name,
                              dd6s.Salesman,
                              SalesmanName=dd13s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id==null?dd6s.Id:d.Id,
                              InvId=d==null?ept:d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              OutLocatorName = dd5s.Name,
                              InLocatorName = dd12s.Name,
                              SubMemo=d.Memo,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;
        }

        #endregion

        #region 配料单
        public JsonResult GetInvByWh(Guid wh)
        {
            var q = (from d in Uow.CurrentStock.GetAll()
                     join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     where d.WhId == wh && dd1s.InvType == (int)DXInfo.Models.InvType.StockManage && !dd1s.IsInvalid
                     select new { Id = d.InvId, Name=dd1s.Code+"-"+dd1s.Name }).Distinct().ToList();
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        private VouchModel addMixVouch(VouchsGridModel vouchsGridModel)
        {
            if (businessCommon.IsBalance(vouchsGridModel.MVDate.Value, vouchsGridModel.InWhId.Value))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            List<DXInfo.Models.MixVouchs> lMixVouchs = Mapper.Map<List<DXInfo.Models.MixVouchs>>(vouchsGridModel.lVouchs);
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckCodeDup(vouchsGridModel.VouchType, vouchsGridModel.Code);
                DXInfo.Models.MixVouch newMixVouch = Mapper.Map<DXInfo.Models.MixVouch>(vouchsGridModel);
                DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == newMixVouch.InWhId);
                DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == newMixVouch.OutWhId);
                newMixVouch.InDeptId = inWarehouse.Dept;
                newMixVouch.OutDeptId = outWarehouse.Dept;
                newMixVouch.Maker = operId;
                newMixVouch.MakeDate = DateTime.Now;
                newMixVouch.MakeTime = DateTime.Now;
                Uow.MixVouch.Add(newMixVouch);
                Uow.Commit();
                foreach (DXInfo.Models.MixVouchs mixVouchs in lMixVouchs)
                {
                    mixVouchs.MVId = newMixVouch.Id;

                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == mixVouchs.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        mixVouchs.MainUnit = inv.MainUnit;
                        mixVouchs.STUnit = inv.MainUnit;
                        mixVouchs.ExchRate = 1;
                        mixVouchs.Quantity = mixVouchs.Num;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue)
                            throw new DXInfo.Models.BusinessException("请设置库存单位");
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        mixVouchs.MainUnit = inv.MainUnit;
                        mixVouchs.STUnit = inv.StockUnit.Value;
                        mixVouchs.ExchRate = uom.Rate;
                        mixVouchs.Quantity = mixVouchs.Num * uom.Rate;
                    }

                    Uow.MixVouchs.Add(mixVouchs);
                    Uow.Commit();
                }
                Uow.Commit();
                transaction.Complete();

                VouchModel retVouchModel = Mapper.Map<VouchModel>(newMixVouch);
                retVouchModel.IsModify = true;
                return retVouchModel;
            }
        }
        private VouchModel editMixVouch(VouchModel vouchModel)
        {
            DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g => g.Id == vouchModel.Id);
            if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            if (oldMixVouch.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核");
            }
            oldMixVouch = Mapper.Map<VouchModel, DXInfo.Models.MixVouch>(vouchModel, oldMixVouch);
            DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == oldMixVouch.InWhId);
            DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == oldMixVouch.OutWhId);
            oldMixVouch.InDeptId = inWarehouse.Dept;
            oldMixVouch.OutDeptId = outWarehouse.Dept;
            oldMixVouch.Modifier = operId;
            oldMixVouch.ModifyDate = DateTime.Now;
            oldMixVouch.ModifyTime = DateTime.Now;
            Uow.MixVouch.Update(oldMixVouch);
            Uow.Commit();
            return vouchModel;
        }
        private DXInfo.Models.JsonObject delMixVouch(VouchModel vouchModel)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g => g.Id == vouchModel.Id);
                if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldMixVouch == null) throw new DXInfo.Models.BusinessException("空记录");
                if (oldMixVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核");
                }
                Uow.MixVouch.Delete(oldMixVouch);

                List<DXInfo.Models.MixVouchs> lMixVouchs = Uow.MixVouchs.GetAll().Where(w => w.MVId == vouchModel.Id).ToList();
                foreach (DXInfo.Models.MixVouchs mixVouchs in lMixVouchs)
                {
                    Uow.MixVouchs.Delete(mixVouchs);
                }
                Uow.Commit();
                transaction.Complete();
            }
            return new DXInfo.Models.JsonObject() { Sucess = true, Message = "已删除" };
        }
        private void AddTransVouchByMixVouch(DXInfo.Models.MixVouch mixVouch, 
            List<DXInfo.Models.MixVouchs> lMixVouchs)
        {
            //DXInfo.Models.VouchType vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.TransVouch);
            DXInfo.Models.TransVouch transVouch = Mapper.Map<DXInfo.Models.TransVouch>(mixVouch);

            transVouch.Maker = operId;
            transVouch.MakeDate = DateTime.Now;
            transVouch.MakeTime = DateTime.Now;

            transVouch.OutRdCode = "006";
            transVouch.InRdCode = "003";

            transVouch.IsVerify = false;
            transVouch.Verifier = null;
            transVouch.VerifyDate = null;
            transVouch.VerifyTime = null;
            transVouch.TVDate = mixVouch.MVDate;
            transVouch.Memo = mixVouch.Memo + "配料单号：" + mixVouch.Code;
            transVouch.SourceId = mixVouch.Id;
            transVouch.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.TransVouch);
            transVouch.Salesman = null;
            Uow.TransVouch.Add(transVouch);
            Uow.Commit();

            foreach (DXInfo.Models.MixVouchs mixVouchs in lMixVouchs)
            {
                DXInfo.Models.TransVouchs transVouchs = Mapper.Map<DXInfo.Models.MixVouchs, DXInfo.Models.TransVouchs>(mixVouchs);
                transVouchs.TVId = transVouch.Id;
                transVouchs.SourceId = mixVouchs.Id;
                transVouchs.DueQuantity = mixVouchs.Quantity;
                transVouchs.DueNum = mixVouchs.Num;
                transVouchs.DueAmount = mixVouchs.Amount;
                Uow.TransVouchs.Add(transVouchs);
            }
            Uow.Commit();
        }

        private VouchModel verifyMixVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g => g.Id == Id);
                if (oldMixVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                }
                if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId) ||
                    businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                oldMixVouch.IsVerify = true;
                oldMixVouch.Verifier = operId;
                oldMixVouch.VerifyDate = DateTime.Now;
                oldMixVouch.VerifyTime = DateTime.Now;
                Uow.MixVouch.Update(oldMixVouch);

                List<DXInfo.Models.MixVouchs> lMixVouchs = Uow.MixVouchs.GetAll().Where(w => w.MVId == oldMixVouch.Id).ToList();

                AddTransVouchByMixVouch(oldMixVouch, lMixVouchs);
                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(oldMixVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }
        private VouchModel unVerifyMixVouch(Guid Id)
        {
            VouchModel retVouchModel = new VouchModel();
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.MixVouch OldMixVouch = Uow.MixVouch.GetById(g => g.Id == Id);
                OldMixVouch.IsVerify = false;
                if (businessCommon.IsBalance(OldMixVouch.MVDate, OldMixVouch.InWhId) ||
                    businessCommon.IsBalance(OldMixVouch.MVDate, OldMixVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.TransVouch> lTransVouch = Uow.TransVouch.GetAll().Where(w => w.SourceId == Id).ToList();
                foreach (DXInfo.Models.TransVouch transVouch in lTransVouch)
                {
                    if (transVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核");
                    }

                    Uow.TransVouch.Delete(transVouch);
                    List<DXInfo.Models.TransVouchs> lTransVouchs = Uow.TransVouchs.GetAll().Where(w => w.TVId == transVouch.Id).ToList();
                    foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                    {
                        Uow.TransVouchs.Delete(transVouchs);
                    }
                }

                OldMixVouch.Verifier = null;
                OldMixVouch.VerifyDate = null;
                OldMixVouch.VerifyTime = null;
                OldMixVouch.Modifier = operId;
                OldMixVouch.ModifyDate = DateTime.Now;
                OldMixVouch.ModifyTime = DateTime.Now;
                Uow.MixVouch.Update(OldMixVouch);

                Uow.Commit();
                transaction.Complete();
                retVouchModel = Mapper.Map<VouchModel>(OldMixVouch);
                retVouchModel.IsModify = true;
            }
            return retVouchModel;
        }

        private IQueryable GetMixVouchsRequestData()
        {

            var records = from d in Uow.MixVouchs.GetAll()
                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()

                          select new
                          {
                              d.Id,
                              d.MVId,
                              d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnitName = dd3s.Name,
                              d.Num,
                              d.Memo,
                          };
            return records;
        }

        private void addMixVouchs(DXInfo.Models.MixVouchs mixVouchs)
        {
            var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouchs.MVId);
            if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }

            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == mixVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                mixVouchs.MainUnit = inv.MainUnit;
                mixVouchs.STUnit = inv.MainUnit;
                mixVouchs.ExchRate = 1;
                mixVouchs.Quantity = mixVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                mixVouchs.MainUnit = inv.MainUnit;
                mixVouchs.STUnit = inv.StockUnit.Value;
                mixVouchs.ExchRate = uom.Rate;
                mixVouchs.Quantity = mixVouchs.Num * uom.Rate;
            }

            Uow.MixVouchs.Add(mixVouchs);
            Uow.Commit();
        }
        private void editMixVouchs(DXInfo.Models.MixVouchs mixVouchs)
        {
            var oldMixVouchs = Uow.MixVouchs.GetById(g => g.Id == mixVouchs.Id);
            var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == oldMixVouchs.MVId);
            if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
            {
                throw new DXInfo.Models.BusinessException("已月结不能操作单据");
            }
            oldMixVouchs.InvId = mixVouchs.InvId;
            oldMixVouchs.Num = mixVouchs.Num;
            DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == mixVouchs.InvId);
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                oldMixVouchs.MainUnit = inv.MainUnit;
                oldMixVouchs.STUnit = inv.MainUnit;
                oldMixVouchs.ExchRate = 1;
                oldMixVouchs.Quantity = mixVouchs.Num;
            }
            else
            {
                if (!inv.StockUnit.HasValue)
                    throw new DXInfo.Models.BusinessException("请设置库存单位");
                DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                oldMixVouchs.MainUnit = inv.MainUnit;
                oldMixVouchs.STUnit = inv.StockUnit.Value;
                oldMixVouchs.ExchRate = uom.Rate;
                oldMixVouchs.Quantity = oldMixVouchs.Num * uom.Rate;
            }
            oldMixVouchs.Memo = mixVouchs.Memo;
            Uow.MixVouchs.Update(oldMixVouchs);
            Uow.Commit();
        }
        private void delMixVouchs(DXInfo.Models.MixVouchs mixVouchs)
        {
            var oldMixVouchs = Uow.MixVouchs.GetById(g => g.Id == mixVouchs.Id);
            if (oldMixVouchs != null)
            {
                var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == oldMixVouchs.MVId);
                if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                Uow.MixVouchs.Delete(oldMixVouchs);
                Uow.Commit();
            }
        }

        public ActionResult MixVouch()
        {
            return GenerateVouch(DXInfo.Models.VouchTypeCode.MixVouch);
        }

        private VouchModel GetMixVouchAuthorityData(string VouchType, DateTime? MakeTime, bool Descending)
        {
            var q = from d in Uow.MixVouch.GetAll()
                    join d1 in Uow.Depts.GetAll() on d.InDeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new VouchModel
                    {
                        Code=d.Code,
                        DeptId=d.InDeptId,
                        Id=d.Id,
                        IsVerify=d.IsVerify,
                        VerifyDate=d.VerifyDate,
                        Maker=d.Maker,
                        MakeTime=d.MakeTime,
                        Memo=d.Memo,
                        Salesman = (Guid?)d.Salesman,
                        Verifier=d.Verifier,
                        VerifyTime=d.VerifyTime,
                        InWhId=(Guid?)d.InWhId,
                        OutWhId=(Guid?)d.OutWhId,
                        MVDate=(DateTime?)d.MVDate,
                        OrganizationId=dd1s.OrganizationId
                    };

            object obj = GetVouchAuthorityData(q, null, MakeTime, Descending);
            return Mapper.DynamicMap<VouchModel>(obj);
        }
        private VouchModel GetMixVouchOrderByDescending(string VouchType, DateTime? makeTime)
        {
            return GetMixVouchAuthorityData(VouchType, makeTime, true);
        }
        private VouchModel GetMixVouchOrderBy(string VouchType, DateTime? makeTime)
        {
            return GetMixVouchAuthorityData(VouchType, makeTime, false);
        }

        public IQueryable GetMixVouchRequestData()
        {
            var records = from dd6s in Uow.MixVouch.GetAll()
                          join d6 in Uow.MixVouchs.GetAll() on dd6s.Id equals d6.MVId into dd6
                          from d in dd6.DefaultIfEmpty()

                          join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()

                          join d3 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d3.Id into dd3
                          from dd3s in dd3.DefaultIfEmpty()

                          join d7 in Uow.Warehouse.GetAll() on dd6s.InWhId equals d7.Id into dd7
                          from dd7s in dd7.DefaultIfEmpty()

                          join d10 in Uow.aspnet_CustomProfile.GetAll() on dd6s.Salesman equals d10.UserId into dd10
                          from dd10s in dd10.DefaultIfEmpty()

                          join d11 in Uow.Warehouse.GetAll() on dd6s.OutWhId equals d11.Id into dd11
                          from dd11s in dd11.DefaultIfEmpty()

                          select new
                          {
                              dd6s.Id,
                              dd6s.Code,
                              dd6s.MVDate,
                              DeptId = dd6s.InDeptId,
                              dd6s.InWhId,
                              InWhName = dd7s.Name,
                              dd6s.OutWhId,
                              OutWhName = dd11s.Name,
                              dd6s.Salesman,
                              SalesmanName = dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id == null ? dd6s.Id : d.Id,
                              InvId = d.InvId == null ? Guid.Empty : d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnit = d.STUnit == null ? Guid.Empty : d.STUnit,
                              STUnitName = dd3s.Name,
                              Num = d.Num == null ? 0 : d.Num,
                              SubMemo = d.Memo,
                          };
            var q = businessCommon.SetVouchAuthority(records);
            return q;
        }        

        public ActionResult BatchInventory()
        {
            var gridModel = new BatchInventoryGridModel();
            SetupBatchInventoryGridModel(gridModel.BatchInventoryGrid);
            //gridModel.WhId = Guid.Parse(this.HttpContext.Request["WhId"]);            
            return PartialView(gridModel);
        }
        private void SetupBatchInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchInventory_RequestData");
            grid.EditUrl = Url.Action("BatchInventory_EditData");
        }
        public ActionResult BatchInventory_RequestData(Guid WhId)
        {
            var gridModel = new BatchInventoryGridModel();
            SetupBatchInventoryGridModel(gridModel.BatchInventoryGrid);
            List<BatchInventoryList> lBIL = new List<BatchInventoryList>();
            //Guid WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
            if (Session[BatchInventorySession] != null)
            {
                lBIL = Session[BatchInventorySession] as List<BatchInventoryList>;
            }
            var records1 =
                            (from d in Uow.CurrentStock.GetAll()
                             join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                             from dd1s in dd1.DefaultIfEmpty()
                             join d3 in Uow.UnitOfMeasures.GetAll() on dd1s.MainUnit equals d3.Id into dd3
                             from dd3s in dd3.DefaultIfEmpty()
                             where dd1s.InvType == (int)DXInfo.Models.InvType.StockManage && dd1s.IsInvalid == false
                             && d.WhId == WhId
                             select new BatchInventoryList()
                             {
                                 WhId = d.WhId,
                                 InvId = d.InvId,
                                 InvCode = dd1s.Code,
                                 InvName = dd1s.Name,
                                 Specs = dd1s.Specs,
                                 STUnitName = dd3s.Name
                             })
                             .Distinct().ToList();
            lBIL.RemoveAll(delegate(BatchInventoryList bil) { return bil.WhId != WhId; });
            Session[BatchInventorySession] = lBIL;
            var records2 = from d1 in records1
                           join d2 in lBIL on d1.InvId equals d2.InvId into dd2
                           from dd2s in dd2.DefaultIfEmpty()
                           //orderby d1.InvCode
                           select new BatchInventoryList()
                          {
                              InvId = d1.InvId,
                              WhId = d1.WhId,
                              InvCode = d1.InvCode,
                              InvName = d1.InvName,
                              Specs = d1.Specs,
                              STUnitName = d1.STUnitName,
                              Num = dd2s == null ? 0 : dd2s.Num,
                              Memo = dd2s == null ? "" : dd2s.Memo,
                          };
            return gridModel.BatchInventoryGrid.DataBind(records2.AsQueryable<BatchInventoryList>());
        }
        public ActionResult BatchInventory_EditData(BatchInventoryList list)
        {
            var gridModel = new BatchInventoryGridModel();
            SetupBatchInventoryGridModel(gridModel.BatchInventoryGrid);
            if (gridModel.BatchInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (list.Num > 0)
                {
                    if (Session[BatchInventorySession] != null)
                    {
                        List<BatchInventoryList> lb = Session[BatchInventorySession] as List<BatchInventoryList>;
                        BatchInventoryList oldList = lb.Find(f => f.InvId == list.InvId);
                        if (oldList == null)
                        {
                            lb.Add(list);
                        }
                        else
                        {
                            oldList.Num = list.Num;
                            oldList.Memo = list.Memo;
                        }
                    }
                    else
                    {
                        List<BatchInventoryList> lb = new List<BatchInventoryList>();
                        lb.Add(list);
                        Session[BatchInventorySession] = lb;
                    }
                }
            }
            return new EmptyResult();
        }
        public ActionResult BatchInventory_SyncData()
        {
            List<DXInfo.Models.MixVouchs> lmixVouchs = new List<DXInfo.Models.MixVouchs>();
            List<BatchInventoryList> lb = Session[BatchInventorySession] as List<BatchInventoryList>;
            List<BatchInventoryList> lb1 = new List<BatchInventoryList>();
            if (lb != null)
            {
                lb1 = lb.FindAll(f => f.Num > 0);
                foreach (BatchInventoryList list in lb1)
                {
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == list.InvId);
                    list.InvName = inv.Name;
                    list.Specs = inv.Specs;
                    DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.MainUnit);
                    list.STUnitName = uom.Name;
                }
            }
            Guid? dempty = null;
            var q = from d in lb1
                    select new {
                        Id = Guid.NewGuid(),
                        MVId = dempty,
                        d.InvId,
                        d.InvName,
                        d.Specs,
                        d.STUnitName,
                        d.Num,
                        d.Memo,
                    };
            var gridModel = new VouchsGridModel(DXInfo.Models.VouchTypeCode.MixVouch);
            SetupVouchsGrid(gridModel.VouchsGrid, DXInfo.Models.VouchTypeCode.MixVouch);
            return gridModel.VouchsGrid.DataBind(q.AsQueryable());
        }

        public ActionResult BatchWarehouseInventory()
        {
            var gridModel = new BatchWarehouseInventoryGridModel();
            SetupBatchWarehouseInventoryGridModel(gridModel.BatchWarehouseInventoryGrid);
            //gridModel.WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
            return PartialView(gridModel);
        }
        private void SetupBatchWarehouseInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchWarehouseInventory_RequestData");
            grid.EditUrl = Url.Action("BatchWarehouseInventory_EditData");
        }
        public ActionResult BatchWarehouseInventory_RequestData(Guid WhId)
        {
            var gridModel = new BatchWarehouseInventoryGridModel();
            SetupBatchWarehouseInventoryGridModel(gridModel.BatchWarehouseInventoryGrid);
            List<BatchWarehouseInventoryList> lBIL = new List<BatchWarehouseInventoryList>();
            //Guid WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
            if (Session[BatchWarehouseInventorySession] != null)
            {
                lBIL = Session[BatchWarehouseInventorySession] as List<BatchWarehouseInventoryList>;
            }
            var records1 =
                            (from d in Uow.WarehouseInventory.GetAll()
                             join d1 in Uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                             from dd1s in dd1.DefaultIfEmpty()
                             join d3 in Uow.UnitOfMeasures.GetAll() on dd1s.MainUnit equals d3.Id into dd3
                             from dd3s in dd3.DefaultIfEmpty()
                             where dd1s.InvType == (int)DXInfo.Models.InvType.StockManage
                             && dd1s.IsInvalid == false
                             && d.Warehouse == WhId
                             select new 
                             {
                                 Id=d.Id,
                                 WhId = d.Warehouse,
                                 InvId = d.Inventory,
                                 InvCode = dd1s.Code,
                                 InvName = dd1s.Name,
                                 Specs = dd1s.Specs,
                                 STUnitName = dd3s.Name
                             })
                             .Distinct().ToList();
            lBIL.RemoveAll(delegate(BatchWarehouseInventoryList bil) { return bil.WhId != WhId; });
            Session[BatchWarehouseInventorySession] = lBIL;
            var records2 = from d1 in records1
                           join d2 in lBIL on d1.InvId equals d2.InvId into dd2
                           from dd2s in dd2.DefaultIfEmpty()
                           orderby d1.Id
                           select new BatchWarehouseInventoryList()
                           {
                               Id=d1.Id,
                               InvId = d1.InvId,
                               WhId = d1.WhId,
                               InvCode = d1.InvCode,
                               InvName = d1.InvName,
                               Specs = d1.Specs,
                               STUnitName = d1.STUnitName,
                               Num = dd2s == null ? 0 : dd2s.Num,
                               Memo = dd2s == null ? "" : dd2s.Memo,
                           };
            return gridModel.BatchWarehouseInventoryGrid.DataBind(records2.AsQueryable<BatchWarehouseInventoryList>());
        }
        public ActionResult BatchWarehouseInventory_EditData(BatchWarehouseInventoryList list)
        {
            var gridModel = new BatchWarehouseInventoryGridModel();
            SetupBatchWarehouseInventoryGridModel(gridModel.BatchWarehouseInventoryGrid);
            if (gridModel.BatchWarehouseInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (list.Num > 0)
                {
                    if (Session[BatchWarehouseInventorySession] != null)
                    {
                        List<BatchWarehouseInventoryList> lb = Session[BatchWarehouseInventorySession] as List<BatchWarehouseInventoryList>;
                        BatchWarehouseInventoryList oldList = lb.Find(f => f.InvId == list.InvId);
                        if (oldList == null)
                        {
                            lb.Add(list);
                        }
                        else
                        {
                            oldList.Num = list.Num;
                            oldList.Memo = list.Memo;
                        }
                        Session[BatchWarehouseInventorySession] = lb;
                    }
                    else
                    {
                        List<BatchWarehouseInventoryList> lb = new List<BatchWarehouseInventoryList>();
                        lb.Add(list);
                        Session[BatchWarehouseInventorySession] = lb;
                    }
                }
            }
            return new EmptyResult();
        }
        public ActionResult BatchWarehouseInventory_SyncData()
        {
            List<BatchWarehouseInventoryList> lb1 = new List<BatchWarehouseInventoryList>();
            List<BatchWarehouseInventoryList> lb = Session[BatchWarehouseInventorySession] as List<BatchWarehouseInventoryList>;
            if (lb != null)
            {
                lb1 = lb.FindAll(f => f.Num > 0);
                foreach (BatchWarehouseInventoryList list in lb1)
                {
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == list.InvId);
                    list.InvName = inv.Name;
                    list.Specs = inv.Specs;

                    DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.MainUnit);
                        list.STUnitName = uom.Name;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue)
                            throw new DXInfo.Models.BusinessException("请设置库存单位");
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        list.STUnitName = uom.Name;
                    }
                }
            }

            Guid? dempty = null;
            var q = from d in lb1
                    select new
                    {
                        Id = Guid.NewGuid(),
                        MVId = dempty,
                        d.InvId,
                        d.InvName,
                        d.Specs,
                        d.STUnitName,
                        d.Num,
                        d.Memo,
                    };
            var gridModel = new VouchsGridModel(DXInfo.Models.VouchTypeCode.MixVouch);
            SetupVouchsGrid(gridModel.VouchsGrid, DXInfo.Models.VouchTypeCode.MixVouch);
            return gridModel.VouchsGrid.DataBind(q.AsQueryable());
        }
        #endregion

        #region 月结
        private void SetupMonthBalanceGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("MonthBalance_RequestData");
            grid.EditUrl = Url.Action("MonthBalance_EditData");

            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "Period", centerCommon.GetPeriod());
            SetBoolColumn(grid, "IsVerify");
            SetDateColumn(grid, "MBDate");
            SetRequiredColumn(grid, "MBDate");
            SetRequiredColumn(grid, "Period");
            SetRequiredColumn(grid, "WhId");
            SetRequiredColumn(grid, "Salesman");
        }
        public ActionResult MonthBalance()
        {
            var gridModel = new MonthBalanceGridModel();
            SetupMonthBalanceGridModel(gridModel.MonthBalanceGrid);
            return PartialView(gridModel);
        }
        public ActionResult MonthBalance_RequestData()
        {
            var gridModel = new MonthBalanceGridModel();
            SetupMonthBalanceGridModel(gridModel.MonthBalanceGrid);
            var monthBalance = from d in Uow.MonthBalance.GetAll()
                               join d1 in Uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                             from dd1s in dd1.DefaultIfEmpty()
                               join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                             from dd2s in dd2.DefaultIfEmpty()
                               join d9 in Uow.aspnet_CustomProfile.GetAll() on d.Salesman equals d9.UserId into dd9
                             from dd9s in dd9.DefaultIfEmpty()
                               join d10 in Uow.Period.GetAll() on d.Period equals d10.Id into dd10
                             from dd10s in dd10.DefaultIfEmpty()
                             select new
                             {
                                 d.Id,
                                 d.MBDate,
                                 d.Period,
                                 PeriodCode = dd10s.Code,
                                 d.DeptId,
                                 dd1s.DeptName,
                                 d.WhId,
                                 WhName = dd2s.Name,                                 
                                 d.Salesman,
                                 SalesmanName = dd9s.FullName,
                                 d.IsVerify,
                                 d.Memo,
                                 dd1s.OrganizationId,
                                 d.Verifier,
                                 d.Maker,
                             };
            var q = businessCommon.SetVouchAuthority(monthBalance);            
            return gridModel.MonthBalanceGrid.DataBind(q);
        }
        private void addMonthBalance(DXInfo.Models.MonthBalance monthBalance)
        {
            var count = Uow.MonthBalance.GetAll().Where(w => w.Period == monthBalance.Period && w.WhId == monthBalance.WhId).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("月结记录重复");
            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == monthBalance.WhId);
            monthBalance.DeptId = warehouse.Dept;
            
            monthBalance.Maker = operId;
            monthBalance.MakeDate = DateTime.Now;
            monthBalance.MakeTime = DateTime.Now;
            Uow.MonthBalance.Add(monthBalance);
            Uow.Commit();
        }
        private void editMonthBalance(DXInfo.Models.MonthBalance monthBalance)
        {
            var oldMonthBalance = Uow.MonthBalance.GetById(g => g.Id == monthBalance.Id);

            if (oldMonthBalance.Period != monthBalance.Period || oldMonthBalance.WhId != monthBalance.WhId)
            {
                var count = Uow.MonthBalance.GetAll().Where(w => w.Period == monthBalance.Period && w.WhId == monthBalance.WhId).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("月结记录重复");
            }
            oldMonthBalance.MBDate = monthBalance.MBDate;
            oldMonthBalance.Period = monthBalance.Period;
            oldMonthBalance.WhId = monthBalance.WhId;
            oldMonthBalance.Memo = monthBalance.Memo;

            DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == monthBalance.WhId);
            oldMonthBalance.DeptId = warehouse.Dept;

            oldMonthBalance.Modifier = operId;
            oldMonthBalance.ModifyDate = DateTime.Now;
            oldMonthBalance.ModifyTime = DateTime.Now;
            Uow.MonthBalance.Update(oldMonthBalance);
            Uow.Commit();
        }
        private void delMonthBalance(DXInfo.Models.MonthBalance monthBalance)
        {
            var oldMonthBalance = Uow.MonthBalance.GetById(g => g.Id == monthBalance.Id);
            if (oldMonthBalance.IsVerify)
            {
                throw new DXInfo.Models.BusinessException("已审核不能删除");
            }
            Uow.MonthBalance.Delete(oldMonthBalance);
            Uow.Commit();
        }
        public ActionResult MonthBalance_EditData(DXInfo.Models.MonthBalance monthBalance)
        {
            var gridModel = new MonthBalanceGridModel();
            SetupMonthBalanceGridModel(gridModel.MonthBalanceGrid);
            return ajaxCallBack<DXInfo.Models.MonthBalance>(gridModel.MonthBalanceGrid, monthBalance, addMonthBalance, editMonthBalance, delMonthBalance);
        }
        public JsonResult MonthBalance_Verify(Guid MonthBalanceId)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.MonthBalance oldMonthBalance = Uow.MonthBalance.GetById(g=>g.Id==MonthBalanceId);
                    if (oldMonthBalance.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核不能审核");
                    }
                    DXInfo.Models.Period period = Uow.Period.GetById(g=>g.Id==oldMonthBalance.Period);
                    var countUnVerify = (from d in Uow.MonthBalance.GetAll()
                                         join d1 in Uow.Period.GetAll() on d.Period equals d1.Id into dd1
                                from dd1s in dd1.DefaultIfEmpty()
                                where dd1s.EndDate<period.BeginDate && !d.IsVerify select d).Count();
                    if(countUnVerify>0)
                        throw new DXInfo.Models.BusinessException("有未审核的前期月结记录，不能月结");
                    var count = Uow.RdRecord.GetAll().Where(w => w.WhId == oldMonthBalance.WhId && w.RdDate >= period.BeginDate && w.RdDate <= period.EndDate && !w.IsVerify).Count();
                    if(count>0)
                        throw new DXInfo.Models.BusinessException("有单据未审核，不能月结");
                    
                    if(!MonthBalnceProcess(oldMonthBalance,period))
                        throw new DXInfo.Models.BusinessException("月结失败");

                    oldMonthBalance.IsVerify = true;
                    oldMonthBalance.Verifier = operId;
                    oldMonthBalance.VerifyDate = DateTime.Now;
                    oldMonthBalance.VerifyTime = DateTime.Now;
                    Uow.MonthBalance.Update(oldMonthBalance);
                    Uow.Commit();
                    transaction.Complete();
                }
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new { Error = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new { Error = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Error = "" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MonthBalance_UnVerify(Guid MonthBalanceId)
        {
            try
            {
                DXInfo.Models.MonthBalance oldMonthBalance = Uow.MonthBalance.GetById(g=>g.Id==MonthBalanceId);
                if (!oldMonthBalance.IsVerify)
                {
                    return Json(new { Error = "未审核不能弃审" }, JsonRequestBehavior.AllowGet);
                }
                DXInfo.Models.Period period = Uow.Period.GetById(g => g.Id == oldMonthBalance.Period);
                var countUnVerify = (from d in Uow.MonthBalance.GetAll()
                                     join d1 in Uow.Period.GetAll() on d.Period equals d1.Id into dd1
                                     from dd1s in dd1.DefaultIfEmpty()
                                     where dd1s.BeginDate > period.EndDate && d.IsVerify
                                     select d).Count();
                if (countUnVerify > 0)
                    throw new DXInfo.Models.BusinessException("有已审核的后期月结记录，不能弃审");

                List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == oldMonthBalance.Id).ToList();
                foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                {
                    if (rdRecord.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核");
                    }

                    Uow.RdRecord.Delete(rdRecord);
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                    foreach (DXInfo.Models.RdRecords rdRecordSub in lRdRecords)
                    {
                        Uow.RdRecords.Delete(rdRecordSub);
                    }
                }
                List<DXInfo.Models.Books> lBooks = Uow.Books.GetAll().Where(w => w.SourceId == oldMonthBalance.Id).ToList();
                foreach (DXInfo.Models.Books books in lBooks)
                {
                    Uow.Books.Delete(books);
                }
                oldMonthBalance.IsVerify = false;
                oldMonthBalance.Verifier = null;
                oldMonthBalance.VerifyDate = null;
                oldMonthBalance.VerifyTime = null;

                oldMonthBalance.Modifier = operId;
                oldMonthBalance.ModifyDate = DateTime.Now;
                oldMonthBalance.ModifyTime = DateTime.Now;

                Uow.MonthBalance.Update(oldMonthBalance);
                Uow.Commit();
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new { Error = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new { Error = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Error = "" }, JsonRequestBehavior.AllowGet);
        }

        private bool MonthBalnceProcess(DXInfo.Models.MonthBalance monthBalance, DXInfo.Models.Period period)
        {
            //结存的数量
            var rdRecord = from d in Uow.RdRecords.GetAll()
                           join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where dd1s.WhId == monthBalance.WhId
                            && dd1s.RdDate <= period.EndDate
                            && dd1s.IsVerify
                            select new
                            {
                                d.InvId,
                                d.MainUnit,
                                d.STUnit,
                                d.ExchRate,
                                Quantity = dd1s.RdFlag == 0 ? d.Quantity : -d.Quantity,
                                Num = dd1s.RdFlag == 0 ? d.Num : -d.Num,
                                Amount = dd1s.RdFlag==0?d.Amount:-d.Amount,
                                d.Batch,
                                d.Price,
                                d.MadeDate,
                                d.ShelfLife,
                                d.ShelfLifeType,
                                d.InvalidDate,
                                d.Locator,
                            };
            var rdRecordGroup = (from d in rdRecord
                                 group d by new
                                 {
                                     d.InvId,
                                     d.MainUnit,
                                     d.STUnit,
                                     d.ExchRate,
                                     d.Batch,
                                     d.Price,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeType,
                                     d.InvalidDate,
                                     d.Locator
                                 } into g
                                 select new
                                 {
                                     g.Key.InvId,
                                     g.Key.MainUnit,
                                     g.Key.STUnit,
                                     g.Key.ExchRate,
                                     Quantity = g.Sum(s => s.Quantity),
                                     Num = g.Sum(s => s.Num),
                                     Amount = g.Sum(s=>s.Amount),
                                     g.Key.Batch,
                                     g.Key.Price,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeType,
                                     g.Key.InvalidDate,
                                     g.Key.Locator,
                                 }).ToList();
            var rdRecordGroup1 = rdRecordGroup.Where(w => w.Quantity > 0).ToList();
            var rdRecordGroup2 = rdRecordGroup.Where(w => w.Quantity < 0).ToList();
            if (rdRecordGroup2.Count > 0)
                return false;
            
            //收的数量
            var inRdRecord = (from d in Uow.RdRecords.GetAll()
                              join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where dd1s.WhId == monthBalance.WhId
                            && dd1s.RdDate >= period.BeginDate && dd1s.RdDate <= period.EndDate
                            && dd1s.IsVerify && !dd1s.InvInit
                            select new
                            {
                                d.InvId,
                                d.MainUnit,
                                d.STUnit,
                                d.ExchRate,
                                Quantity = dd1s.RdFlag == 0 ? d.Quantity : 0,
                                Num = dd1s.RdFlag == 0 ? d.Num : 0,
                                Amount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                d.Batch,
                                d.Price,
                                d.MadeDate,
                                d.ShelfLife,
                                d.ShelfLifeType,
                                d.InvalidDate,
                                d.Locator,
                            }).ToList();
            var inRdRecordGroup = (from d in inRdRecord
                                 group d by new
                                 {
                                     d.InvId,
                                     d.MainUnit,
                                     d.STUnit,
                                     d.ExchRate,
                                     d.Batch,
                                     d.Price,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeType,
                                     d.InvalidDate,
                                     d.Locator
                                 } into g
                                 select new
                                 {
                                     g.Key.InvId,
                                     g.Key.MainUnit,
                                     g.Key.STUnit,
                                     g.Key.ExchRate,
                                     Quantity = g.Sum(s => s.Quantity),
                                     Num = g.Sum(s => s.Num),
                                     Amount = g.Sum(s => s.Amount),
                                     g.Key.Batch,
                                     g.Key.Price,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeType,
                                     g.Key.InvalidDate,
                                     g.Key.Locator,
                                 }).ToList();
            //发的数量
            var outRdRecord = (from d in Uow.RdRecords.GetAll()
                               join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where dd1s.WhId == monthBalance.WhId
                            && dd1s.RdDate >= period.BeginDate && dd1s.RdDate <= period.EndDate
                            && dd1s.IsVerify && !dd1s.InvInit
                            select new
                            {
                                d.InvId,
                                d.MainUnit,
                                d.STUnit,
                                d.ExchRate,
                                Quantity = dd1s.RdFlag == 0 ? 0 : d.Quantity,
                                Num = dd1s.RdFlag == 0 ? 0 : d.Num,
                                Amount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                d.Batch,
                                d.Price,
                                d.MadeDate,
                                d.ShelfLife,
                                d.ShelfLifeType,
                                d.InvalidDate,
                                d.Locator,
                            }).ToList();
            var outRdRecordGroup = (from d in outRdRecord
                                 group d by new
                                 {
                                     d.InvId,
                                     d.MainUnit,
                                     d.STUnit,
                                     d.ExchRate,
                                     d.Batch,
                                     d.Price,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeType,
                                     d.InvalidDate,
                                     d.Locator
                                 } into g
                                 select new
                                 {
                                     g.Key.InvId,
                                     g.Key.MainUnit,
                                     g.Key.STUnit,
                                     g.Key.ExchRate,
                                     Quantity = g.Sum(s => s.Quantity),
                                     Num = g.Sum(s => s.Num),
                                     Amount = g.Sum(s => s.Amount),
                                     g.Key.Batch,
                                     g.Key.Price,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeType,
                                     g.Key.InvalidDate,
                                     g.Key.Locator,
                                 }).ToList();

            
            List<DXInfo.Models.Books> lBooks = new List<DXInfo.Models.Books>();

            foreach (dynamic d in inRdRecordGroup)
            {
                DXInfo.Models.Books newBooks = Mapper.DynamicMap<DXInfo.Models.Books>(d);

                newBooks.InQuantity = newBooks.Quantity;
                newBooks.InNum = newBooks.Num;
                newBooks.InAmount = newBooks.Amount;
                newBooks.Quantity = 0;
                newBooks.Num = 0;
                newBooks.Amount = 0;
                lBooks.Add(newBooks);
            }
            foreach (dynamic d in outRdRecordGroup)
            {
                DXInfo.Models.Books newBooks = Mapper.DynamicMap<DXInfo.Models.Books>(d);

                newBooks.OutQuantity = newBooks.Quantity;
                newBooks.OutNum = newBooks.Num;
                newBooks.OutAmount = newBooks.Amount;
                newBooks.Quantity = 0;
                newBooks.Num = 0;
                newBooks.Amount = 0;
                lBooks.Add(newBooks);
            }
            foreach (dynamic d in rdRecordGroup)
            {
                DXInfo.Models.Books newBooks = Mapper.DynamicMap<DXInfo.Models.Books>(d);

                lBooks.Add(newBooks);
            }
            var books = (from d in lBooks
                        group d by new
                                 {
                                     d.InvId,
                                     d.MainUnit,
                                     d.STUnit,
                                     d.ExchRate,
                                     d.Batch,
                                     d.Price,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeType,
                                     d.InvalidDate,
                                     d.Locator
                                 } into g
                                 select new
                                 {
                                     g.Key.InvId,
                                     g.Key.MainUnit,
                                     g.Key.STUnit,
                                     g.Key.ExchRate,
                                     InQuantity = g.Sum(s => s.InQuantity),
                                     OutQuantity = g.Sum(s => s.OutQuantity),
                                     Quantity = g.Sum(s => s.Quantity),
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num),
                                     InAmount = g.Sum(s => s.InAmount),
                                     OutAmount = g.Sum(s => s.OutAmount),
                                     Amount = g.Sum(s => s.Amount),
                                     g.Key.Batch,
                                     g.Key.Price,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeType,
                                     g.Key.InvalidDate,
                                     g.Key.Locator,
                                 }).ToList();
            foreach (dynamic d in books)
            {
                DXInfo.Models.Books newBooks = Mapper.DynamicMap<DXInfo.Models.Books>(d);
                newBooks.Period = period.Id;
                newBooks.SourceId = monthBalance.Id;
                newBooks.WhId = monthBalance.WhId;
                Uow.Books.Add(newBooks);
            }
            Uow.Commit();
            return true;
        }
        #endregion

        #region 批次冻结
        private void SetupBatchStopGridModel(JQGrid grid)
        {
            this.SetUpGrid(grid);
            grid.DataUrl = Url.Action("BatchStop_RequestData");
            grid.EditUrl = Url.Action("BatchStop_EditData");
            this.SetBatchColumn(grid, false);
            this.SetShelfLifeColumn(grid, false);
            SetBoolColumn(grid, "StopFlag");
        }
        [Authorize]
        public ActionResult BatchStop()
        {
            var gridModel = new BatchStopGridModel();
            SetupBatchStopGridModel(gridModel.BatchStopGrid);
            return PartialView(gridModel);
        }
        public ActionResult BatchStop_RequestData()
        {
            var gridModel = new BatchStopGridModel();
            SetupBatchStopGridModel(gridModel.BatchStopGrid);
            var batchStop = from d in Uow.CurrentStock.GetAll()
                            join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                             from dd2s in dd2.DefaultIfEmpty()
                            join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                             from dd5s in dd5.DefaultIfEmpty()
                            join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                             from dd7s in dd7.DefaultIfEmpty()
                            join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                             from dd8s in dd8.DefaultIfEmpty()
                             join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                             from dd9s in dd9.DefaultIfEmpty()

                             select new
                             {
                                 d.Id,
                                 d.StopFlag,
                                 DeptId=dd2s.Dept,
                                 d.WhId,
                                 WhName = dd2s.Name,
                                 d.InvId,
                                 InvName = dd5s.Name,
                                 dd5s.Specs,
                                 STUnitName = dd7s.Name,
                                 d.Num,
                                 d.Batch,
                                 d.MadeDate,
                                 d.ShelfLife,
                                 d.ShelfLifeType,
                                 ShelfLifeTypeName = dd8s.Description,
                                 d.InvalidDate,
                                 dd9s.OrganizationId,
                             };
            var q = businessCommon.SetVouchAuthority(batchStop, null, false);            
            return gridModel.BatchStopGrid.DataBind(q);
        }
        public ActionResult BatchStop_EditData(DXInfo.Models.CurrentStock currentStock)
        {
            var gridModel = new BatchStopGridModel();
            SetupBatchStopGridModel(gridModel.BatchStopGrid);
            if (gridModel.BatchStopGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldCurrentStock = Uow.CurrentStock.GetById(g=>g.Id==currentStock.Id);
                oldCurrentStock.StopFlag = currentStock.StopFlag;
                Uow.CurrentStock.Update(oldCurrentStock);
                Uow.Commit();
            }
            return new EmptyResult();
        }
        #endregion

        #region 失效日期维护
        private void SetupInvalidDateGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("InvalidDate_RequestData");
            grid.EditUrl = Url.Action("InvalidDate_EditData");
            SetShelfLifeColumn(grid, true);
            SetBatchColumn(grid, false);            
            SetBoolColumn(grid, "StopFlag");
        }
        public ActionResult InvalidDate()
        {
            var gridModel = new InvalidDateGridModel();
            SetupInvalidDateGridModel(gridModel.InvalidDateGrid);
            return PartialView(gridModel);
        }
        public ActionResult InvalidDate_RequestData()
        {
            var gridModel = new InvalidDateGridModel();
            SetupInvalidDateGridModel(gridModel.InvalidDateGrid);
            var invalidDate = from d in Uow.CurrentStock.GetAll()
                              join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                              from dd2s in dd2.DefaultIfEmpty()
                              join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()
                              join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                              from dd7s in dd7.DefaultIfEmpty()
                              join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                              from dd8s in dd8.DefaultIfEmpty()
                              join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                              from dd9s in dd9.DefaultIfEmpty()
                              select new
                              {
                                  d.Id,
                                  d.StopFlag,
                                  DeptId = dd2s.Dept,
                                  WhName = dd2s.Name,
                                  InvName = dd5s.Name,
                                  dd5s.Specs,
                                  STUnitName = dd7s.Name,
                                  d.Num,
                                  d.Batch,
                                  d.MadeDate,
                                  d.ShelfLife,
                                  d.ShelfLifeType,
                                  ShelfLifeTypeName = dd8s.Description,
                                  d.InvalidDate,
                                  dd9s.OrganizationId,
                              };
            var q = businessCommon.SetVouchAuthority(invalidDate, "", false);
            return gridModel.InvalidDateGrid.DataBind(q);
        }
        public ActionResult InvalidDate_EditData(DXInfo.Models.CurrentStock currentStock)
        {
            var gridModel = new InvalidDateGridModel();
            SetupInvalidDateGridModel(gridModel.InvalidDateGrid);
            if (gridModel.InvalidDateGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldCurrentStock = Uow.CurrentStock.GetById(g=>g.Id==currentStock.Id);

                oldCurrentStock.MadeDate = currentStock.MadeDate;
                oldCurrentStock.ShelfLife = currentStock.ShelfLife;
                oldCurrentStock.ShelfLifeType = currentStock.ShelfLifeType;
                oldCurrentStock.InvalidDate = getInvalidDate(oldCurrentStock.ShelfLifeType.Value, oldCurrentStock.ShelfLife.Value, oldCurrentStock.MadeDate.Value);
                Uow.CurrentStock.Update(oldCurrentStock);

                List<DXInfo.Models.CurrentInvLocator> lCurrentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == oldCurrentStock.WhId && w.InvId == oldCurrentStock.InvId && w.Batch == oldCurrentStock.Batch).ToList();

                foreach (DXInfo.Models.CurrentInvLocator currentInvLocator in lCurrentInvLocator)
                {
                    currentInvLocator.MadeDate = oldCurrentStock.MadeDate;
                    currentInvLocator.ShelfLife = oldCurrentStock.ShelfLife;
                    currentInvLocator.ShelfLifeType = oldCurrentStock.ShelfLifeType;
                    currentInvLocator.InvalidDate = oldCurrentStock.InvalidDate;
                    Uow.CurrentInvLocator.Update(currentInvLocator);
                }
                List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.WhId == oldCurrentStock.WhId).ToList();
                foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                {
                    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id && w.InvId == oldCurrentStock.InvId && w.Batch == oldCurrentStock.Batch).ToList();
                    foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
                    {
                        rdRecords.MadeDate = oldCurrentStock.MadeDate;
                        rdRecords.ShelfLife = oldCurrentStock.ShelfLife;
                        rdRecords.ShelfLifeType = oldCurrentStock.ShelfLifeType;
                        rdRecords.InvalidDate = oldCurrentStock.InvalidDate;
                        Uow.RdRecords.Update(rdRecords);
                    }
                }
                List<DXInfo.Models.InvLocator> lInvLocator = Uow.InvLocator.GetAll().Where(w => w.WhId == oldCurrentStock.WhId && w.InvId == oldCurrentStock.InvId && w.Batch == oldCurrentStock.Batch).ToList();
                foreach (DXInfo.Models.InvLocator invLocator in lInvLocator)
                {
                    invLocator.MadeDate = oldCurrentStock.MadeDate;
                    invLocator.ShelfLife = oldCurrentStock.ShelfLife;
                    invLocator.ShelfLifeType = oldCurrentStock.ShelfLifeType;
                    invLocator.InvalidDate = oldCurrentStock.InvalidDate;
                    Uow.InvLocator.Update(invLocator);
                }
                Uow.Commit();
            }
            return new EmptyResult();
        }
        #endregion

        #region 库存账与货位账对账
        private void SetupStockLocatorGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("StockLocator_RequestData");
            this.SetBatchColumn(grid, false);
            this.SetShelfLifeColumn(grid, false);
        }
        public ActionResult StockLocator()
        {
            var gridModel = new StockLocatorGridModel();
            SetupStockLocatorGridModel(gridModel.StockLocatorGrid);
            return PartialView(gridModel);
        }
        public ActionResult StockLocator_RequestData()
        {
            var gridModel = new StockLocatorGridModel();
            SetupStockLocatorGridModel(gridModel.StockLocatorGrid);
            var stockLocator = from d in Uow.CurrentStock.GetAll()
                               join d1 in
                                   (
                                     from dd in Uow.CurrentInvLocator.GetAll()
                                     group dd by new { dd.WhId, dd.InvId, dd.Batch } into g
                                     select new { g.Key.WhId, g.Key.InvId, g.Key.Batch, Num = g.Sum(s => s.Num) }
                                   )
                               on new { d.WhId, d.InvId, d.Batch } equals new { d1.WhId, d1.InvId, d1.Batch }

                               join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                               from dd2s in dd2.DefaultIfEmpty()
                               join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                               from dd5s in dd5.DefaultIfEmpty()
                               join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                               from dd7s in dd7.DefaultIfEmpty()
                               join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                               from dd9s in dd9.DefaultIfEmpty()
                               select new
                               {
                                   d.Id,
                                   DeptId = dd2s.Dept,                                   
                                   WhName = dd2s.Name,                                   
                                   InvName = dd5s.Name,
                                   dd5s.Specs,
                                   STUnitName = dd7s.Name,
                                   d.Num,
                                   LocatorNum = d1.Num,
                                   NumDif = d.Num - d1.Num,
                                   d.Batch,
                                   d.MadeDate,
                                   d.ShelfLife,
                                   d.ShelfLifeType,
                                   d.InvalidDate,
                                   dd9s.OrganizationId,
                               };
            var q = businessCommon.SetVouchAuthority(stockLocator, "", false);
            return QueryAndExcel(gridModel.StockLocatorGrid, q, "库存账与货位账对账.xls");            
        }
        #endregion

        #region 库存现存量
        private void SetupCurrentStockGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("CurrentStock_RequestData");
            SetBoolColumn(grid, "StopFlag");
            SetShelfLifeColumn(grid, false);
            SetBatchColumn(grid, false);
        }
        public ActionResult CurrentStock()
        {
            var gridModel = new CurrentStockGridModel();
            SetupCurrentStockGridModel(gridModel.CurrentStockGrid);
            return PartialView(gridModel);
        }
        public ActionResult CurrentStock_RequestData()
        {
            var gridModel = new CurrentStockGridModel();
            SetupCurrentStockGridModel(gridModel.CurrentStockGrid);
            var currentStock = from d in Uow.CurrentStock.GetAll()

                               join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                              from dd2s in dd2.DefaultIfEmpty()

                               join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()

                              join d6 in Uow.InventoryCategory.GetAll() on dd5s.Category equals d6.Id into dd6
                              from dd6s in dd6.DefaultIfEmpty()

                               join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                              from dd7s in dd7.DefaultIfEmpty()
                               join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                              from dd8s in dd8.DefaultIfEmpty()
                              join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                              from dd9s in dd9.DefaultIfEmpty()
                              select new
                              {
                                  d.Id,   
                                  DeptId=dd2s.Dept,
                                  WhName = dd2s.Name,
                                  InvCode=dd5s.Code,
                                  InvName = dd5s.Name,
                                  dd5s.Category,
                                  CategoryCode=dd6s.Code,
                                  CategoryName=dd6s.Name,
                                  dd5s.Specs,
                                  STUnitName = dd7s.Name,
                                  d.Num,
                                  d.Price,
                                  d.Amount,
                                  d.Batch,
                                  d.MadeDate,
                                  d.ShelfLife,
                                  d.ShelfLifeType,
                                  ShelfLifeTypeName = dd8s.Description,
                                  d.InvalidDate,
                                  d.StopFlag,
                                  dd9s.OrganizationId,
                              };
            var q = businessCommon.SetVouchAuthority(currentStock, "", false);
            return QueryAndExcel(gridModel.CurrentStockGrid, q, "库存现存量.xls");
        }
        #endregion

        #region 库存流水账
        private void SetupStockDayBookGridModel(JQGrid grid)
        {            
            grid.DataUrl = Url.Action("StockDayBook_RequestData");
            SetUpGrid(grid);
            SetBoolColumn(grid, "IsVerify");
            SetDateColumn(grid, "RdDate");
            SetDateColumn(grid, "VerifyDate");
            SetShelfLifeColumn(grid, false);
            SetBatchColumn(grid, false);
        }
        public ActionResult StockDayBook()
        {
            var gridModel = new StockDayBookGridModel();
            SetupStockDayBookGridModel(gridModel.StockDayBookGrid);
            return PartialView(gridModel);
        }
        public ActionResult StockDayBook_RequestData()
        {
            var gridModel = new StockDayBookGridModel();
            SetupStockDayBookGridModel(gridModel.StockDayBookGrid);
            var stockDayBook = from d in Uow.RdRecord.GetAll()
                               join d1 in Uow.RdRecords.GetAll() on d.Id equals d1.RdId into dd1
                               from dd1s in dd1.DefaultIfEmpty()

                               join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                               from dd2s in dd2.DefaultIfEmpty()
                               join d5 in Uow.Inventory.GetAll() on dd1s.InvId equals d5.Id into dd5
                               from dd5s in dd5.DefaultIfEmpty()
                               join d7 in Uow.UnitOfMeasures.GetAll() on dd1s.STUnit equals d7.Id into dd7
                               from dd7s in dd7.DefaultIfEmpty()
                               join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on dd1s.ShelfLifeType equals d8.Value into dd8
                               from dd8s in dd8.DefaultIfEmpty()
                               join d9 in Uow.RdType.GetAll() on d.RdCode equals d9.Code into dd9
                               from dd9s in dd9.DefaultIfEmpty()
                               join d10 in Uow.aspnet_CustomProfile.GetAll() on d.Salesman equals d10.UserId into dd10
                               from dd10s in dd10.DefaultIfEmpty()
                               join d11 in Uow.Depts.GetAll() on dd2s.Dept equals d11.DeptId into dd11
                               from dd11s in dd11.DefaultIfEmpty()
                               select new
                               {
                                   Id = dd1s.Id == null ? d.Id : dd1s.Id,
                                   d.Code,
                                   d.RdDate,
                                   RdName = dd9s.Name,
                                   d.DeptId,
                                   WhName = dd2s.Name,
                                   d.Salesman,
                                   SalesmanName = dd10s.FullName,
                                   d.IsVerify,
                                   d.VerifyDate,
                                   InvName = dd5s.Name,
                                   dd5s.Specs,
                                   STUnitName = dd7s.Name,
                                   Num = dd1s.Num == null ? 0 : dd1s.Num,
                                   dd1s.Batch,
                                   dd1s.MadeDate,
                                   dd1s.ShelfLife,
                                   ShelfLifeTypeName = dd8s.Description,
                                   dd1s.InvalidDate,
                                   dd11s.OrganizationId,
                               };
            var q = businessCommon.SetVouchAuthority(stockDayBook, "", false);
            return QueryAndExcel(gridModel.StockDayBookGrid, q, "库存流水账.xls");
        }
        #endregion

        #region 库存台账
        private void SetupStockBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("StockBook_RequestData");
            SetUpGrid(grid);
        }
        public ActionResult StockBook()
        {
            var gridModel = new StockBookGridModel();
            SetupStockBookGridModel(gridModel.StockBookGrid);
            return PartialView(gridModel);
        }
        public ActionResult StockBook_RequestData()
        {
            var gridModel = new StockBookGridModel();
            SetupStockBookGridModel(gridModel.StockBookGrid);
            var stockBook = from d in Uow.Books.GetAll()
                            join d1 in Uow.Period.GetAll() on d.Period equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()

                            join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                               from dd2s in dd2.DefaultIfEmpty()
                            join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                               from dd5s in dd5.DefaultIfEmpty()
                            join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                               from dd7s in dd7.DefaultIfEmpty()
                            join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                               from dd8s in dd8.DefaultIfEmpty()
                               join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                               from dd9s in dd9.DefaultIfEmpty()
                               select new
                               {
                                   d.Id,
                                   dd1s.Code,
                                   DeptId=dd2s.Dept,
                                   //d.WhId,
                                   WhName = dd2s.Name,
                                   //d.InvId,
                                   InvName = dd5s.Name,
                                   dd5s.Specs,
                                   STUnitName = dd7s.Name,
                                   d.InNum,
                                   d.OutNum,
                                   d.Num,
                                   dd9s.OrganizationId,
                               };
            var stockBookGroup = from d in stockBook
                                 group d by new { d.Code,d.DeptId,d.OrganizationId, d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                 select new
                                 {
                                     Id=Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.OrganizationId,
                                     //g.Key.WhId,
                                     g.Key.WhName,
                                     //g.Key.InvId,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num)
                                 };
            var q = businessCommon.SetVouchAuthority(stockBookGroup, "", false);
            return QueryAndExcel(gridModel.StockBookGrid, q, "库存台账.xls");            
        }
        #endregion

        #region 批次台账
        private void SetupBatchBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchBook_RequestData");
            SetUpGrid(grid);
            SetShelfLifeColumn(grid, false);
            SetBatchColumn(grid, false);
        }
        public ActionResult BatchBook()
        {
            var gridModel = new BatchBookGridModel();
            SetupBatchBookGridModel(gridModel.BatchBookGrid);
            return PartialView(gridModel);
        }
        public ActionResult BatchBook_RequestData()
        {
            var gridModel = new BatchBookGridModel();
            SetupBatchBookGridModel(gridModel.BatchBookGrid);

            var batchBook = from d in Uow.Books.GetAll()
                            join d1 in Uow.Period.GetAll() on d.Period equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()

                            join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                            from dd2s in dd2.DefaultIfEmpty()

                            join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                            from dd5s in dd5.DefaultIfEmpty()
                            join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                            from dd7s in dd7.DefaultIfEmpty()
                            join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                            from dd8s in dd8.DefaultIfEmpty()

                            join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                            from dd9s in dd9.DefaultIfEmpty()
                            select new
                            {
                                d.Id,
                                dd1s.Code,
                                DeptId=dd2s.Dept,
                                WhName = dd2s.Name,
                                InvName = dd5s.Name,
                                dd5s.Specs,
                                STUnitName = dd7s.Name,
                                d.InNum,
                                d.OutNum,
                                d.Num,
                                d.Batch,
                                d.MadeDate,
                                d.ShelfLife,
                                ShelfLifeTypeName = dd8s.Description,
                                d.InvalidDate,
                                dd9s.OrganizationId,
                            };
            var batchBookGroup = from d in batchBook
                                 group d by new
                                 {
                                     d.Code,
                                     d.DeptId,
                                     d.OrganizationId,
                                     d.WhName,
                                     d.InvName,
                                     d.Specs,
                                     d.STUnitName,
                                     d.Batch,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeTypeName,
                                     d.InvalidDate,
                                 } into g
                                 select new
                                 {
                                     Id = Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.OrganizationId,
                                     g.Key.WhName,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num),
                                     g.Key.Batch,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeTypeName,
                                     g.Key.InvalidDate,
                                 };
            var q = businessCommon.SetVouchAuthority(batchBookGroup, "", false);
            return QueryAndExcel(gridModel.BatchBookGrid, q, "库存批次台账.xls");
        }
        #endregion

        #region 货位台账
        private void SetupLocatorBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("LocatorBook_RequestData");
            SetUpGrid(grid);
            SetLocatorColumn(grid);
        }
        public ActionResult LocatorBook()
        {
            var gridModel = new LocatorBookGridModel();
            SetupLocatorBookGridModel(gridModel.LocatorBookGrid);
            return PartialView(gridModel);
        }
        public ActionResult LocatorBook_RequestData()
        {
            var gridModel = new LocatorBookGridModel();
            SetupLocatorBookGridModel(gridModel.LocatorBookGrid);
            var locatorBook = from d in Uow.Books.GetAll()
                              join d1 in Uow.Period.GetAll() on d.Period equals d1.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()

                              join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                            from dd2s in dd2.DefaultIfEmpty()
                              join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                            from dd5s in dd5.DefaultIfEmpty()
                              join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                            from dd7s in dd7.DefaultIfEmpty()
                              join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                            from dd8s in dd8.DefaultIfEmpty()
                              join d9 in Uow.Locator.GetAll() on d.Locator equals d9.Id into dd9
                            from dd9s in dd9.DefaultIfEmpty()
                            join d10 in Uow.Depts.GetAll() on dd2s.Dept equals d10.DeptId into dd10
                            from dd10s in dd10.DefaultIfEmpty()
                            select new
                            {
                                d.Id,
                                dd1s.Code,
                                DeptId=dd2s.Dept,
                                dd10s.OrganizationId,
                                WhName = dd2s.Name,
                                LocatorName = dd9s.Name,
                                InvName = dd5s.Name,
                                dd5s.Specs,
                                STUnitName = dd7s.Name,
                                d.InNum,
                                d.OutNum,
                                d.Num,
                            };
            var locatorBookGroup = from d in locatorBook
                                   group d by new { d.Code, d.DeptId,d.OrganizationId, d.WhName, d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                 select new
                                 {
                                     Id = Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.OrganizationId,
                                     g.Key.WhName,
                                     g.Key.LocatorName,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num)
                                 };
            var q = businessCommon.SetVouchAuthority(locatorBookGroup, "", false);
            return QueryAndExcel(gridModel.LocatorBookGrid, q, "库存货位台账.xls");
        }
        #endregion

        #region 库存收发存汇总表
        private void SetupRdSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("RdSummary_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public ActionResult RdSummary()
        {
            var gridModel = new RdSummaryGridModel();
            SetupRdSummaryGridModel(gridModel.RdSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return PartialView(gridModel);
        }
        public ActionResult RdSummary_RequestData(DateTime? BeginDate, DateTime? EndDate, Guid? WhId,
            string InvName)
        {
            var gridModel = new RdSummaryGridModel();
            SetupRdSummaryGridModel(gridModel.RdSummaryGrid);
            //期初
            var initRdSummary = from d in Uow.RdRecords.GetAll()
                                join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                from dd1s in dd1.DefaultIfEmpty()

                                join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                from dd2s in dd2.DefaultIfEmpty()
                                join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                from dd5s in dd5.DefaultIfEmpty()
                                join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                from dd7s in dd7.DefaultIfEmpty()
                                join d8 in Uow.Depts.GetAll() on dd1s.DeptId equals d8.DeptId into dd8
                                from dd8s in dd8.DefaultIfEmpty()
                                where dd1s.RdDate < BeginDate.Value
                                select new SumamaryResult()
                                {
                                    Id = d.Id,
                                    DeptId = dd1s.DeptId,
                                    OrganizationId = dd8s.OrganizationId,
                                    WhId = dd1s.WhId,
                                    WhName = dd2s.Name,
                                    InvName = dd5s.Name,
                                    Specs = dd5s.Specs,
                                    STUnitName = dd7s.Name,
                                    InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                    InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                    InNum = 0,
                                    InAmount = 0,
                                    OutNum = 0,
                                    OutAmount = 0,
                                    Num = 0,
                                    Amount = 0,
                                };
            if (WhId.HasValue) initRdSummary = initRdSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(InvName)) initRdSummary = initRdSummary.Where(w => w.InvName.Contains(InvName));
            var initRdSummaryGroup = (from d in initRdSummary
                                      group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                      select new SumamaryResult()
                                      {
                                          Id = Guid.NewGuid(),
                                          DeptId = g.Key.DeptId,
                                          OrganizationId = g.Key.OrganizationId,
                                          WhId = g.Key.WhId,
                                          WhName = g.Key.WhName,
                                          InvName = g.Key.InvName,
                                          Specs = g.Key.Specs,
                                          STUnitName = g.Key.STUnitName,
                                          InitNum = g.Sum(s => s.InitNum),
                                          InitAmount = g.Sum(s => s.InitAmount),
                                          InNum = 0,
                                          InAmount = 0,
                                          OutNum = 0,
                                          OutAmount = 0,
                                          Num = 0,
                                          Amount = 0,
                                      }).ToList();
            //收发
            var inOutRdSummary = from d in Uow.RdRecords.GetAll()
                                 join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                 from dd1s in dd1.DefaultIfEmpty()

                                 join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                 from dd2s in dd2.DefaultIfEmpty()
                                 join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                 from dd5s in dd5.DefaultIfEmpty()
                                 join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                 from dd7s in dd7.DefaultIfEmpty()
                                 join d8 in Uow.Depts.GetAll() on dd1s.DeptId equals d8.DeptId into dd8
                                 from dd8s in dd8.DefaultIfEmpty()

                                 where dd1s.RdDate >= BeginDate.Value && dd1s.RdDate <= EndDate.Value
                                 select new SumamaryResult()
                                 {
                                     Id = d.Id,
                                     DeptId = dd1s.DeptId,
                                     OrganizationId = dd8s.OrganizationId,
                                     WhId = dd1s.WhId,
                                     WhName = dd2s.Name,
                                     InvName = dd5s.Name,
                                     Specs = dd5s.Specs,
                                     STUnitName = dd7s.Name,
                                     InitNum = 0,
                                     InitAmount = 0,
                                     InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                     InAmount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                     OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                     OutAmount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                     Num = 0,
                                     Amount = 0,
                                 };
            if (WhId.HasValue) inOutRdSummary = inOutRdSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(InvName)) inOutRdSummary = inOutRdSummary.Where(w => w.InvName.Contains(InvName));
            var inOutRdSummaryGroup = (from d in inOutRdSummary
                                       group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                       select new SumamaryResult()
                                       {
                                           Id = Guid.NewGuid(),
                                           DeptId = g.Key.DeptId,
                                           OrganizationId=g.Key.OrganizationId,
                                           WhId = g.Key.WhId,
                                           WhName = g.Key.WhName,
                                           InvName = g.Key.InvName,
                                           Specs = g.Key.Specs,
                                           STUnitName = g.Key.STUnitName,
                                           InitNum = 0,
                                           InitAmount = 0,
                                           InNum = g.Sum(s => s.InNum),
                                           InAmount = g.Sum(s => s.InAmount),
                                           OutNum = g.Sum(s => s.OutNum),
                                           OutAmount = g.Sum(s => s.OutAmount),
                                           Num = 0,
                                           Amount = 0,
                                       }).ToList();
            initRdSummaryGroup.AddRange(inOutRdSummaryGroup);
            var rdSummaryGroup = (from d in initRdSummaryGroup
                                  group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                  select new SumamaryResult()
                                  {
                                      Id = Guid.NewGuid(),
                                      DeptId = g.Key.DeptId,
                                      OrganizationId = g.Key.OrganizationId,
                                      WhId = g.Key.WhId,
                                      WhName = g.Key.WhName,
                                      InvName = g.Key.InvName,
                                      Specs = g.Key.Specs,
                                      STUnitName = g.Key.STUnitName,
                                      InitNum = g.Sum(s => s.InitNum),
                                      InitAmount = g.Sum(s => s.InitAmount),
                                      InNum = g.Sum(s => s.InNum),
                                      InAmount = g.Sum(s => s.InAmount),
                                      OutNum = g.Sum(s => s.OutNum),
                                      OutAmount = g.Sum(s => s.OutAmount),
                                      Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                      Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                  }).ToList();

            var q = businessCommon.SetVouchAuthority(rdSummaryGroup.AsQueryable(), "", false);
            return QueryAndExcel(gridModel.RdSummaryGrid, q, "库存收发存汇总表.xls");
        }
        #endregion

        #region 库存收发存汇总表ByWh
        private void SetupRdSummaryByWhGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("RdSummaryByWh_RequestData");
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved+=new JQGridDataResolvedEventHandler(grid_DataResolved2);
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        void grid_DataResolved2(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? InitNum = e.FilterData.Select("InitNum").Cast<decimal?>().Sum();
            decimal? InitAmount = e.FilterData.Select("InitAmount").Cast<decimal?>().Sum();

            decimal? InNum = e.FilterData.Select("InNum").Cast<decimal?>().Sum();
            decimal? InAmount = e.FilterData.Select("InAmount").Cast<decimal?>().Sum();

            decimal? OutNum = e.FilterData.Select("OutNum").Cast<decimal?>().Sum();
            decimal? OutAmount = e.FilterData.Select("OutAmount").Cast<decimal?>().Sum();

            decimal? Num = e.FilterData.Select("Num").Cast<decimal?>().Sum();
            decimal? Amount = e.FilterData.Select("Amount").Cast<decimal?>().Sum();

            JQGridColumn InitNumColumn = e.GridModel.Columns.Find(c => c.DataField == "InitNum");
            InitNumColumn.FooterValue = InitNum.ToString();
            JQGridColumn InitAmountColumn = e.GridModel.Columns.Find(c => c.DataField == "InitAmount");
            InitAmountColumn.FooterValue = InitAmount.ToString();

            JQGridColumn InNumColumn = e.GridModel.Columns.Find(c => c.DataField == "InNum");
            InNumColumn.FooterValue = InNum.ToString();
            JQGridColumn InAmountColumn = e.GridModel.Columns.Find(c => c.DataField == "InAmount");
            InAmountColumn.FooterValue = InAmount.ToString();

            JQGridColumn OutNumColumn = e.GridModel.Columns.Find(c => c.DataField == "OutNum");
            OutNumColumn.FooterValue = OutNum.ToString();
            JQGridColumn OutAmountColumn = e.GridModel.Columns.Find(c => c.DataField == "OutAmount");
            OutAmountColumn.FooterValue = OutAmount.ToString();

            JQGridColumn NumColumn = e.GridModel.Columns.Find(c => c.DataField == "Num");
            NumColumn.FooterValue = Num.ToString();
            JQGridColumn AmountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            AmountColumn.FooterValue = Amount.ToString();
        }
        public ActionResult RdSummaryByWh()
        {
            var gridModel = new RdSummaryByWhGridModel();
            SetupRdSummaryByWhGridModel(gridModel.RdSummaryByWhGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return PartialView(gridModel);
        }
        public ActionResult RdSummaryByWh_RequestData(DateTime BeginDate, DateTime EndDate, Guid? WhId, string InventoryCategoryCode,
            string InventoryCategoryName)
        {
            var gridModel = new RdSummaryByWhGridModel();
            SetupRdSummaryByWhGridModel(gridModel.RdSummaryByWhGrid);
            //期初
            var initRdSummary = from d in Uow.RdRecords.GetAll()
                                join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                from dd1s in dd1.DefaultIfEmpty()

                                join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                from dd2s in dd2.DefaultIfEmpty()
                                join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                from dd5s in dd5.DefaultIfEmpty()
                                //join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                //from dd7s in dd7.DefaultIfEmpty()

                                join d8 in Uow.InventoryCategory.GetAll() on dd5s.Category equals d8.Id into dd8
                                from dd8s in dd8.DefaultIfEmpty()

                                join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                from dd9s in dd9.DefaultIfEmpty()

                                where dd1s.RdDate < BeginDate
                                select new SumamaryResult()
                                {
                                    Id = d.Id,
                                    DeptId = dd1s.DeptId,
                                    OrganizationId = dd9s.OrganizationId,
                                    WhId = dd1s.WhId,
                                    InventoryCategoryCode = dd8s.Code,
                                    InventoryCategoryName = dd8s.Name,
                                    InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                    InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                    InNum = 0,
                                    InAmount = 0,
                                    OutNum = 0,
                                    OutAmount = 0,
                                    Num = 0,
                                    Amount = 0,
                                };
            if (WhId.HasValue) initRdSummary = initRdSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(InventoryCategoryCode)) initRdSummary = initRdSummary.Where(w => w.InventoryCategoryCode.Contains(InventoryCategoryCode));
            if (!string.IsNullOrEmpty(InventoryCategoryName)) initRdSummary = initRdSummary.Where(w => w.InventoryCategoryName.Contains(InventoryCategoryName));

            var initRdSummaryGroup = (from d in initRdSummary
                                      group d by new { d.DeptId,d.OrganizationId, d.WhId, d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                      select new SumamaryResult()
                                      {
                                          Id = Guid.NewGuid(),
                                          DeptId = g.Key.DeptId,
                                          OrganizationId = g.Key.OrganizationId,
                                          WhId = g.Key.WhId,
                                          InventoryCategoryCode = g.Key.InventoryCategoryCode,
                                          InventoryCategoryName = g.Key.InventoryCategoryName,
                                          InitNum = g.Sum(s => s.InitNum),
                                          InitAmount = g.Sum(s => s.InitAmount),
                                          InNum = 0,
                                          InAmount = 0,
                                          OutNum = 0,
                                          OutAmount = 0,
                                          Num = 0,
                                          Amount = 0,
                                      }).ToList();
            //收发
            var inOutRdSummary = from d in Uow.RdRecords.GetAll()
                                 join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                 from dd1s in dd1.DefaultIfEmpty()

                                 join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                 from dd2s in dd2.DefaultIfEmpty()
                                 join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                 from dd5s in dd5.DefaultIfEmpty()
                                 join d8 in Uow.InventoryCategory.GetAll() on dd5s.Category equals d8.Id into dd8
                                 from dd8s in dd8.DefaultIfEmpty()

                                 join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                 from dd9s in dd9.DefaultIfEmpty()

                                 where dd1s.RdDate >= BeginDate && dd1s.RdDate <= EndDate
                                 select new SumamaryResult()
                                 {
                                     Id = d.Id,
                                     DeptId = dd1s.DeptId,
                                     OrganizationId = dd9s.OrganizationId,
                                     WhId = dd1s.WhId,
                                     InventoryCategoryCode = dd8s.Code,
                                     InventoryCategoryName = dd8s.Name,
                                     InitNum = 0,
                                     InitAmount = 0,
                                     InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                     InAmount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                     OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                     OutAmount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                     Num = 0,
                                     Amount = 0,
                                 };
            if (WhId.HasValue) inOutRdSummary = inOutRdSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(InventoryCategoryCode)) inOutRdSummary = inOutRdSummary.Where(w => w.InventoryCategoryCode.Contains(InventoryCategoryCode));
            if (!string.IsNullOrEmpty(InventoryCategoryName)) inOutRdSummary = inOutRdSummary.Where(w => w.InventoryCategoryName.Contains(InventoryCategoryName));

            var inOutRdSummaryGroup = (from d in inOutRdSummary
                                       group d by new { d.DeptId,d.OrganizationId, d.WhId, d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                       select new SumamaryResult()
                                       {
                                           Id = Guid.NewGuid(),
                                           DeptId = g.Key.DeptId,
                                           OrganizationId = g.Key.OrganizationId,
                                           WhId = g.Key.WhId,
                                           InventoryCategoryCode = g.Key.InventoryCategoryCode,
                                           InventoryCategoryName = g.Key.InventoryCategoryName,
                                           InitNum = 0,
                                           InitAmount = 0,
                                           InNum = g.Sum(s => s.InNum),
                                           InAmount = g.Sum(s => s.InAmount),
                                           OutNum = g.Sum(s => s.OutNum),
                                           OutAmount = g.Sum(s => s.OutAmount),
                                           Num = 0,
                                           Amount = 0,
                                       }).ToList();
            initRdSummaryGroup.AddRange(inOutRdSummaryGroup);
            var rdSummaryGroup = (from d in initRdSummaryGroup
                                  group d by new { d.DeptId,d.OrganizationId, d.WhId, d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                  select new SumamaryResult()
                                  {
                                      Id = Guid.NewGuid(),
                                      DeptId = g.Key.DeptId,
                                      OrganizationId = g.Key.OrganizationId,
                                      InventoryCategoryCode = g.Key.InventoryCategoryCode,
                                      InventoryCategoryName = g.Key.InventoryCategoryName,
                                      InitNum = g.Sum(s => s.InitNum),
                                      InitAmount = g.Sum(s => s.InitAmount),
                                      InNum = g.Sum(s => s.InNum),
                                      InAmount = g.Sum(s => s.InAmount),
                                      OutNum = g.Sum(s => s.OutNum),
                                      OutAmount = g.Sum(s => s.OutAmount),
                                      Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                      Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                  }).ToList();
            var q = businessCommon.SetVouchAuthority(rdSummaryGroup.AsQueryable(), "", false);
            var rdSummaryGroup2 = (from d in q.Cast<SumamaryResult>()
                                   group d by new { d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                   select new SumamaryResult()
                                   {
                                       Id = Guid.NewGuid(),
                                       InventoryCategoryCode = g.Key.InventoryCategoryCode,
                                       InventoryCategoryName = g.Key.InventoryCategoryName,
                                       InitNum = g.Sum(s => s.InitNum),
                                       InitAmount = g.Sum(s => s.InitAmount),
                                       InNum = g.Sum(s => s.InNum),
                                       InAmount = g.Sum(s => s.InAmount),
                                       OutNum = g.Sum(s => s.OutNum),
                                       OutAmount = g.Sum(s => s.OutAmount),
                                       Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                       Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                   }).ToList();
            return QueryAndExcel(gridModel.RdSummaryByWhGrid, rdSummaryGroup2.AsQueryable(), "库存收发存汇总表2.xls");
        }
        #endregion

        #region 库存批次汇总表
        private void SetupBatchSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchSummary_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetShelfLifeColumn(grid, false);
            SetBatchColumn(grid, false);
        }
        [Authorize]
        public ActionResult BatchSummary()
        {
            var gridModel = new BatchSummaryGridModel();
            SetupBatchSummaryGridModel(gridModel.BatchSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return PartialView(gridModel);
        }
        public ActionResult BatchSummary_RequestData(DateTime BeginDate, DateTime EndDate, Guid? WhId, string Batch,
            string InvName)
        {

            var gridModel = new BatchSummaryGridModel();
            SetupBatchSummaryGridModel(gridModel.BatchSummaryGrid);
            //期初
            var initBatchSummary = from d in Uow.RdRecords.GetAll()
                                   join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                   from dd1s in dd1.DefaultIfEmpty()

                                   join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                   from dd2s in dd2.DefaultIfEmpty()
                                   join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                   from dd5s in dd5.DefaultIfEmpty()
                                   join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                   from dd7s in dd7.DefaultIfEmpty()
                                   join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                                   from dd8s in dd8.DefaultIfEmpty()
                                   join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                   from dd9s in dd9.DefaultIfEmpty()

                                   where dd1s.RdDate < BeginDate
                                   select new SumamaryResult()
                                   {
                                       Id = d.Id,
                                       DeptId = dd1s.DeptId,
                                       OrganizationId = dd9s.OrganizationId,
                                       WhId = dd1s.WhId,
                                       WhName = dd2s.Name,
                                       InvName = dd5s.Name,
                                       Specs = dd5s.Specs,
                                       STUnitName = dd7s.Name,
                                       InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                       InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                       InNum = 0,
                                       InAmount = 0,
                                       OutNum = 0,
                                       OutAmount = 0,
                                       Num = 0,
                                       Amount = 0,
                                       Batch = d.Batch,
                                       MadeDate = d.MadeDate,
                                       ShelfLife = d.ShelfLife,
                                       ShelfLifeTypeName = dd8s.Description,
                                       InvalidDate = d.InvalidDate,
                                   };
            if (WhId.HasValue) initBatchSummary = initBatchSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(Batch)) initBatchSummary = initBatchSummary.Where(w => w.Batch == Batch);
            if (!string.IsNullOrEmpty(InvName)) initBatchSummary = initBatchSummary.Where(w => w.InvName.Contains(InvName));
            var initBatchSummaryGroup = (from d in initBatchSummary
                                         group d by new
                                         {
                                             d.DeptId,
                                             d.OrganizationId,
                                             d.WhId,
                                             d.WhName,
                                             d.InvName,
                                             d.Specs,
                                             d.STUnitName,
                                             d.Batch,
                                             d.MadeDate,
                                             d.ShelfLife,
                                             d.ShelfLifeTypeName,
                                             d.InvalidDate
                                         } into g
                                         select new SumamaryResult()
                                         {
                                             Id = Guid.NewGuid(),
                                             DeptId = g.Key.DeptId,
                                             OrganizationId = g.Key.OrganizationId,
                                             WhId = g.Key.WhId,
                                             WhName = g.Key.WhName,
                                             InvName = g.Key.InvName,
                                             Specs = g.Key.Specs,
                                             STUnitName = g.Key.STUnitName,
                                             InitNum = g.Sum(s => s.InitNum),
                                             InitAmount = g.Sum(s => s.InitAmount),
                                             InNum = 0,
                                             InAmount = 0,
                                             OutNum = 0,
                                             OutAmount = 0,
                                             Num = 0,
                                             Amount = 0,
                                             Batch = g.Key.Batch,
                                             MadeDate = g.Key.MadeDate,
                                             ShelfLife = g.Key.ShelfLife,
                                             ShelfLifeTypeName = g.Key.ShelfLifeTypeName,
                                             InvalidDate = g.Key.InvalidDate,
                                         }).ToList();
            //收发
            var inOutBatchSummary = from d in Uow.RdRecords.GetAll()
                                    join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                    from dd1s in dd1.DefaultIfEmpty()

                                    join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                    from dd2s in dd2.DefaultIfEmpty()
                                    join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                    from dd5s in dd5.DefaultIfEmpty()
                                    join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                    from dd7s in dd7.DefaultIfEmpty()
                                    join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                                    from dd8s in dd8.DefaultIfEmpty()
                                    join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                    from dd9s in dd9.DefaultIfEmpty()

                                    where dd1s.RdDate >= BeginDate && dd1s.RdDate <= EndDate
                                    select new SumamaryResult()
                                    {
                                        Id = d.Id,
                                        DeptId = dd1s.DeptId,
                                        OrganizationId = dd9s.OrganizationId,
                                        WhId = dd1s.WhId,
                                        WhName = dd2s.Name,
                                        InvName = dd5s.Name,
                                        Specs = dd5s.Specs,
                                        STUnitName = dd7s.Name,
                                        InitNum = 0,
                                        InitAmount = 0,
                                        InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                        InAmount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                        OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                        OutAmount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                        Num = 0,
                                        Amount = 0,
                                        Batch = d.Batch,
                                        MadeDate = d.MadeDate,
                                        ShelfLife = d.ShelfLife,
                                        ShelfLifeTypeName = dd8s.Description,
                                        InvalidDate = d.InvalidDate,
                                    };
            if (WhId.HasValue) inOutBatchSummary = inOutBatchSummary.Where(w => w.WhId == WhId.Value);
            if (!string.IsNullOrEmpty(Batch)) inOutBatchSummary = inOutBatchSummary.Where(w => w.Batch == Batch);
            if (!string.IsNullOrEmpty(InvName)) inOutBatchSummary = inOutBatchSummary.Where(w => w.InvName.Contains(InvName));
            var inOutBatchSummaryGroup = (from d in inOutBatchSummary
                                          group d by new
                                          {
                                              d.DeptId,
                                              d.OrganizationId,
                                              d.WhId,
                                              d.WhName,
                                              d.InvName,
                                              d.Specs,
                                              d.STUnitName,
                                              d.Batch,
                                              d.MadeDate,
                                              d.ShelfLife,
                                              d.ShelfLifeTypeName,
                                              d.InvalidDate
                                          } into g
                                          select new SumamaryResult()
                                          {
                                              Id = Guid.NewGuid(),
                                              DeptId = g.Key.DeptId,
                                              OrganizationId = g.Key.OrganizationId,
                                              WhId = g.Key.WhId,
                                              WhName = g.Key.WhName,
                                              InvName = g.Key.InvName,
                                              Specs = g.Key.Specs,
                                              STUnitName = g.Key.STUnitName,
                                              InitNum = 0,
                                              InitAmount = 0,
                                              InNum = g.Sum(s => s.InNum),
                                              InAmount = g.Sum(s => s.InAmount),
                                              OutNum = g.Sum(s => s.OutNum),
                                              OutAmount = g.Sum(s => s.OutAmount),
                                              Num = 0,
                                              Amount = 0,
                                              Batch = g.Key.Batch,
                                              MadeDate = g.Key.MadeDate,
                                              ShelfLife = g.Key.ShelfLife,
                                              ShelfLifeTypeName = g.Key.ShelfLifeTypeName,
                                              InvalidDate = g.Key.InvalidDate,
                                          }).ToList();
            initBatchSummaryGroup.AddRange(inOutBatchSummaryGroup);
            var batchSummaryGroup = (from d in initBatchSummaryGroup
                                     group d by new
                                     {
                                         d.DeptId,
                                         d.OrganizationId,
                                         d.WhId,
                                         d.WhName,
                                         d.InvName,
                                         d.Specs,
                                         d.STUnitName,
                                         d.Batch,
                                         d.MadeDate,
                                         d.ShelfLife,
                                         d.ShelfLifeTypeName,
                                         d.InvalidDate
                                     } into g
                                     select new SumamaryResult()
                                     {
                                         Id = Guid.NewGuid(),
                                         DeptId = g.Key.DeptId,
                                         OrganizationId = g.Key.OrganizationId,
                                         WhId = g.Key.WhId,
                                         WhName = g.Key.WhName,
                                         InvName = g.Key.InvName,
                                         Specs = g.Key.Specs,
                                         STUnitName = g.Key.STUnitName,
                                         InitNum = g.Sum(s => s.InitNum),
                                         InitAmount = g.Sum(s => s.InitAmount),
                                         InNum = g.Sum(s => s.InNum),
                                         InAmount = g.Sum(s => s.InAmount),
                                         OutNum = g.Sum(s => s.OutNum),
                                         OutAmount = g.Sum(s => s.OutAmount),
                                         Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                         Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                         Batch = g.Key.Batch,
                                         MadeDate = g.Key.MadeDate,
                                         ShelfLife = g.Key.ShelfLife,
                                         ShelfLifeTypeName = g.Key.ShelfLifeTypeName,
                                         InvalidDate = g.Key.InvalidDate,
                                     }).ToList();
            var q = businessCommon.SetVouchAuthority(batchSummaryGroup.AsQueryable(), "", false);
            return QueryAndExcel(gridModel.BatchSummaryGrid, q, "库存批次汇总表.xls");
        }
        #endregion

        #region 库存货位汇总表
        private void SetupLocatorSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("LocatorSummary_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetLocatorColumn(grid);
        }
        [Authorize]
        public ActionResult LocatorSummary()
        {
            var gridModel = new LocatorSummaryGridModel();
            SetupLocatorSummaryGridModel(gridModel.LocatorSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return PartialView(gridModel);
        }
        public ActionResult LocatorSummary_RequestData(DateTime BeginDate, DateTime EndDate, Guid? WhId, Guid? Locator,
            string InvName)
        {

            var gridModel = new LocatorSummaryGridModel();
            SetupLocatorSummaryGridModel(gridModel.LocatorSummaryGrid);
            //期初
            var initLocatorSummary = from d in Uow.RdRecords.GetAll()
                                     join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                     from dd1s in dd1.DefaultIfEmpty()

                                     join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                     from dd2s in dd2.DefaultIfEmpty()
                                     join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                     from dd5s in dd5.DefaultIfEmpty()
                                     join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                     from dd7s in dd7.DefaultIfEmpty()
                                     join d8 in Uow.Locator.GetAll() on d.Locator equals d8.Id into dd8
                                     from dd8s in dd8.DefaultIfEmpty()
                                     join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                     from dd9s in dd9.DefaultIfEmpty()

                                     where dd1s.RdDate < BeginDate
                                     select new SumamaryResult()
                                     {
                                         Id = d.Id,
                                         DeptId = dd1s.DeptId,
                                         OrganizationId = dd9s.OrganizationId,
                                         WhId = dd1s.WhId,
                                         WhName = dd2s.Name,
                                         Locator = d.Locator,
                                         LocatorName = dd8s.Name,
                                         InvName = dd5s.Name,
                                         Specs = dd5s.Specs,
                                         STUnitName = dd7s.Name,
                                         InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                         InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                         InNum = 0,
                                         InAmount = 0,
                                         OutNum = 0,
                                         OutAmount = 0,
                                         Num = 0,
                                         Amount = 0,
                                     };
            if (WhId.HasValue) initLocatorSummary = initLocatorSummary.Where(w => w.WhId == WhId.Value);
            if (Locator.HasValue) initLocatorSummary = initLocatorSummary.Where(w => w.Locator == Locator.Value);
            if (!string.IsNullOrEmpty(InvName)) initLocatorSummary = initLocatorSummary.Where(w => w.InvName.Contains(InvName));
            var initLocatorSummaryGroup = (from d in initLocatorSummary
                                           group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.Locator, d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                           select new SumamaryResult()
                                           {
                                               Id = Guid.NewGuid(),
                                               DeptId = g.Key.DeptId,
                                               OrganizationId = g.Key.OrganizationId,
                                               WhId = g.Key.WhId,
                                               WhName = g.Key.WhName,
                                               Locator = g.Key.Locator,
                                               LocatorName = g.Key.LocatorName,
                                               InvName = g.Key.InvName,
                                               Specs = g.Key.Specs,
                                               STUnitName = g.Key.STUnitName,
                                               InitNum = g.Sum(s => s.InitNum),
                                               InitAmount = g.Sum(s => s.InitAmount),
                                               InNum = 0,
                                               InAmount = 0,
                                               OutNum = 0,
                                               OutAmount = 0,
                                               Num = 0,
                                               Amount = 0,
                                           }).ToList();
            //收发
            var inOutLocatorSummary = from d in Uow.RdRecords.GetAll()
                                      join d1 in Uow.RdRecord.GetAll() on d.RdId equals d1.Id into dd1
                                      from dd1s in dd1.DefaultIfEmpty()

                                      join d2 in Uow.Warehouse.GetAll() on dd1s.WhId equals d2.Id into dd2
                                      from dd2s in dd2.DefaultIfEmpty()
                                      join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                      from dd5s in dd5.DefaultIfEmpty()
                                      join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                      from dd7s in dd7.DefaultIfEmpty()
                                      join d8 in Uow.Locator.GetAll() on d.Locator equals d8.Id into dd8
                                      from dd8s in dd8.DefaultIfEmpty()
                                      join d9 in Uow.Depts.GetAll() on dd1s.DeptId equals d9.DeptId into dd9
                                      from dd9s in dd9.DefaultIfEmpty()

                                      where dd1s.RdDate >= BeginDate && dd1s.RdDate <= EndDate
                                      select new SumamaryResult()
                                      {
                                          Id = d.Id,
                                          DeptId = dd1s.DeptId,
                                          OrganizationId = dd9s.OrganizationId,
                                          WhId = dd1s.WhId,
                                          WhName = dd2s.Name,
                                          Locator = d.Locator,
                                          LocatorName = dd8s.Name,
                                          InvName = dd5s.Name,
                                          Specs = dd5s.Specs,
                                          STUnitName = dd7s.Name,
                                          InitNum = 0,
                                          InitAmount = 0,
                                          InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                          InAmount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                          OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                          OutAmount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                          Num = 0,
                                          Amount = 0,
                                      };
            if (WhId.HasValue) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.WhId == WhId.Value);
            if (Locator.HasValue) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.Locator == Locator.Value);
            if (!string.IsNullOrEmpty(InvName)) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.InvName.Contains(InvName));
            var inOutLocatorSummaryGroup = (from d in inOutLocatorSummary
                                            group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.Locator, d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                            select new SumamaryResult()
                                            {
                                                Id = Guid.NewGuid(),
                                                DeptId = g.Key.DeptId,
                                                OrganizationId = g.Key.OrganizationId,
                                                WhId = g.Key.WhId,
                                                WhName = g.Key.WhName,
                                                Locator = g.Key.Locator,
                                                LocatorName = g.Key.LocatorName,
                                                InvName = g.Key.InvName,
                                                Specs = g.Key.Specs,
                                                STUnitName = g.Key.STUnitName,
                                                InitNum = 0,
                                                InitAmount = 0,
                                                InNum = g.Sum(s => s.InNum),
                                                InAmount = g.Sum(s => s.InAmount),
                                                OutNum = g.Sum(s => s.OutNum),
                                                OutAmount = g.Sum(s => s.OutAmount),
                                                Num = 0,
                                                Amount = 0,
                                            }).ToList();
            initLocatorSummaryGroup.AddRange(inOutLocatorSummaryGroup);
            var locatorSummaryGroup = (from d in initLocatorSummaryGroup
                                       group d by new { d.DeptId,d.OrganizationId, d.WhId, d.WhName, d.Locator, d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                       select new SumamaryResult()
                                       {
                                           Id = Guid.NewGuid(),
                                           DeptId = g.Key.DeptId,
                                           OrganizationId = g.Key.OrganizationId,
                                           WhId = g.Key.WhId,
                                           WhName = g.Key.WhName,
                                           Locator = g.Key.Locator,
                                           LocatorName = g.Key.LocatorName,
                                           InvName = g.Key.InvName,
                                           Specs = g.Key.Specs,
                                           STUnitName = g.Key.STUnitName,
                                           InitNum = g.Sum(s => s.InitNum),
                                           InitAmount = g.Sum(s => s.InitAmount),
                                           InNum = g.Sum(s => s.InNum),
                                           InAmount = g.Sum(s => s.InAmount),
                                           OutNum = g.Sum(s => s.OutNum),
                                           OutAmount = g.Sum(s => s.OutAmount),
                                           Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                           Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                       }).ToList();
            var q = businessCommon.SetVouchAuthority(locatorSummaryGroup.AsQueryable(), "", false);
            return QueryAndExcel(gridModel.LocatorSummaryGrid, q, "库存货位汇总表.xls");
        }
        #endregion

        #region 库存保质期预警
        private void SetupShelfLifeWarningGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ShelfLifeWarning_RequestData");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.DataType = "local";
            //SetUpGrid(grid);
            SetShelfLifeColumn(grid, false);
            SetLocatorColumn(grid);
            SetBatchColumn(grid, false);
        }
        public ActionResult ShelfLifeWarning()
        {
            var gridModel = new ShelfLifeWarningGridModel();
            SetupShelfLifeWarningGridModel(gridModel.ShelfLifeWarningGrid);
            return PartialView(gridModel);
        }
        public ActionResult ShelfLifeWarning_RequestData(int InvType, DateTime? BeginDate, DateTime? EndDate,
            int? OutOfDays, int? BeginCloseToDays, int? EndCloseToDays)
        {
            var gridModel = new ShelfLifeWarningGridModel();
            SetupShelfLifeWarningGridModel(gridModel.ShelfLifeWarningGrid);

            DateTime dtNow = DateTime.Now.Date;
            var shelfLifeWarning = from d in Uow.CurrentInvLocator.GetAll()
                                   join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                                   from dd2s in dd2.DefaultIfEmpty()
                                   join d4 in Uow.Locator.GetAll() on d.Locator equals d4.Id into dd4
                                   from dd4s in dd4.DefaultIfEmpty()
                                   join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                   from dd5s in dd5.DefaultIfEmpty()
                                   join d6 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d6.Id into dd6
                                   from dd6s in dd6.DefaultIfEmpty()
                                   join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                   from dd7s in dd7.DefaultIfEmpty()
                                   join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                                   from dd8s in dd8.DefaultIfEmpty()
                                   join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                                   from dd9s in dd9.DefaultIfEmpty()
                                   select new
                                   {
                                       d.Id,
                                       DeptId = dd2s.Dept,
                                       dd9s.OrganizationId,
                                       d.WhId,
                                       WhName = dd2s.Name,
                                       InvName = dd5s.Name,
                                       dd5s.Specs,
                                       dd5s.EarlyWarningDay,
                                       STUnitName = dd7s.Name,
                                       d.Num,
                                       d.Batch,
                                       d.MadeDate,
                                       d.ShelfLife,
                                       ShelfLifeTypeName = dd8s.Description,
                                       d.InvalidDate,
                                       LocatorName = dd4s.Name,
                                   };
            if (!isLocator)
            {
                shelfLifeWarning = from d in Uow.CurrentStock.GetAll()
                                   join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                                   from dd2s in dd2.DefaultIfEmpty()
                                   //join d4 in Uow.Locator.GetAll() on d.Locator equals d4.Id into dd4
                                   //from dd4s in dd4.DefaultIfEmpty()
                                   join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                   from dd5s in dd5.DefaultIfEmpty()
                                   join d6 in Uow.UnitOfMeasures.GetAll() on d.MainUnit equals d6.Id into dd6
                                   from dd6s in dd6.DefaultIfEmpty()
                                   join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                   from dd7s in dd7.DefaultIfEmpty()
                                   join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                                   from dd8s in dd8.DefaultIfEmpty()
                                   join d9 in Uow.Depts.GetAll() on dd2s.Dept equals d9.DeptId into dd9
                                   from dd9s in dd9.DefaultIfEmpty()
                                   select new
                                   {
                                       d.Id,
                                       DeptId = dd2s.Dept,
                                       dd9s.OrganizationId,
                                       d.WhId,
                                       WhName = dd2s.Name,
                                       InvName = dd5s.Name,
                                       dd5s.Specs,
                                       dd5s.EarlyWarningDay,
                                       STUnitName = dd7s.Name,
                                       d.Num,
                                       d.Batch,
                                       d.MadeDate,
                                       d.ShelfLife,
                                       ShelfLifeTypeName = dd8s.Description,
                                       d.InvalidDate,
                                       LocatorName = "",//dd4s.Name,
                                   };
            }

            switch (InvType)
            {
                case 1:
                    shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate <= dtNow);
                    break;
                case 2:
                    shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate > dtNow);
                    break;
                case 3:
                    shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate > dtNow && SqlFunctions.DateDiff("dd", dtNow, w.InvalidDate) <= w.EarlyWarningDay);
                    break;
            }
            if (BeginDate.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate >= BeginDate.Value);
            if (EndDate.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate <= EndDate.Value);
            if (OutOfDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd", w.InvalidDate, dtNow) >= OutOfDays);
            if (BeginCloseToDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd", dtNow, w.InvalidDate) >= BeginCloseToDays);
            if (EndCloseToDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd", dtNow, w.InvalidDate) <= EndCloseToDays);

            var q = businessCommon.SetVouchAuthority(shelfLifeWarning, "", false);
            return QueryAndExcel(gridModel.ShelfLifeWarningGrid, q, "库存保质期预警.xls");
        }
        
        #endregion

        #region 库存安全库存预警
        private void SetupSecurityStockGridModel(JQGrid grid)
        {
            //SetUpGrid(grid);
            grid.DataUrl = Url.Action("SecurityStock_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public ActionResult SecurityStock_RequestData(int QueryType)
        {
            var gridModel = new SecurityStockGridModel();
            SetupSecurityStockGridModel(gridModel.SecurityStockGrid);
            DateTime dtNow = DateTime.Now.Date;
            var securityStock = from d in Uow.CurrentStock.GetAll()
                                join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                from dd5s in dd5.DefaultIfEmpty()
                                join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                from dd7s in dd7.DefaultIfEmpty()
                                join d8 in Uow.Warehouse.GetAll() on d.WhId equals d8.Id into dd8
                                from dd8s in dd8.DefaultIfEmpty()
                                join d9 in Uow.Depts.GetAll() on dd8s.Dept equals d9.DeptId into dd9
                                from dd9s in dd9.DefaultIfEmpty()

                                select new
                                {
                                    d.Id,
                                    WhName=dd8s.Name,
                                    DeptId=dd8s.Dept,
                                    dd9s.OrganizationId,
                                    InvName = dd5s.Name,
                                    dd5s.Specs,
                                    STUnitName = dd7s.Name,
                                    SecurityStock = dd5s.SecurityStock,
                                    d.Num,
                                };
            var securityStockGroup = from d in securityStock
                                     group d by new { d.InvName,d.WhName,d.DeptId,d.OrganizationId, d.Specs, d.STUnitName, d.SecurityStock } into g
                                     select new
                                     {
                                         Id = Guid.NewGuid(),
                                         g.Key.InvName,
                                         g.Key.WhName,
                                         g.Key.DeptId,
                                         g.Key.OrganizationId,
                                         g.Key.Specs,
                                         g.Key.STUnitName,
                                         g.Key.SecurityStock,
                                         Num = g.Sum(s => s.Num),
                                         DifNum = g.Sum(s => s.Num) - g.Key.SecurityStock
                                     };

            switch (QueryType)
            {
                case 1:
                    securityStockGroup = securityStockGroup.Where(w => w.DifNum > 0);
                    break;
                case 2:
                    securityStockGroup = securityStockGroup.Where(w => w.DifNum <= 0);
                    break;
            }
            var q = businessCommon.SetVouchAuthority(securityStockGroup, "", false);
            return QueryAndExcel(gridModel.SecurityStockGrid, q, "库存安全库存预警.xls");
        }
        public ActionResult SecurityStock()
        {
            var gridModel = new SecurityStockGridModel();
            SetupSecurityStockGridModel(gridModel.SecurityStockGrid);
            return PartialView(gridModel);
        }
        #endregion

        #region 库存超储存货查询
        private void SetupAboveStockGridModel(JQGrid grid)
        {
            //SetUpGrid(grid);
            grid.DataUrl = Url.Action("AboveStock_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public ActionResult AboveStock_RequestData(int QueryType)
        {
            var gridModel = new AboveStockGridModel();
            SetupAboveStockGridModel(gridModel.AboveStockGrid);
            DateTime dtNow = DateTime.Now.Date;
            var aboveStock = from d in Uow.CurrentStock.GetAll()
                             join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                             from dd5s in dd5.DefaultIfEmpty()
                             join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                             from dd7s in dd7.DefaultIfEmpty()
                             join d8 in Uow.Warehouse.GetAll() on d.WhId equals d8.Id into dd8
                             from dd8s in dd8.DefaultIfEmpty()
                             join d9 in Uow.Depts.GetAll() on dd8s.Dept equals d9.DeptId into dd9
                             from dd9s in dd9.DefaultIfEmpty()
                             select new
                             {
                                 d.Id,
                                 WhName=dd8s.Name,
                                 DeptId=dd8s.Dept,
                                 dd9s.OrganizationId,
                                 InvName = dd5s.Name,                                 
                                 dd5s.Specs,
                                 STUnitName = dd7s.Name,
                                 HighStock = dd5s.HighStock,
                                 d.Num,
                             };
            var aboveStockGroup = from d in aboveStock
                                  group d by new { d.InvName,d.WhName,d.DeptId,d.OrganizationId, d.Specs, d.STUnitName, d.HighStock } into g
                                  select new
                                  {
                                      Id = Guid.NewGuid(),
                                      g.Key.InvName,
                                      g.Key.WhName,
                                      g.Key.DeptId,
                                      g.Key.OrganizationId,
                                      g.Key.Specs,
                                      g.Key.STUnitName,
                                      g.Key.HighStock,
                                      Num = g.Sum(s => s.Num),
                                      DifNum = g.Sum(s => s.Num) - g.Key.HighStock
                                  };
            switch (QueryType)
            {
                case 1:
                    aboveStockGroup = aboveStockGroup.Where(w => w.DifNum > 0);
                    break;
            }
            var q = businessCommon.SetVouchAuthority(aboveStockGroup, "", false);
            return QueryAndExcel(gridModel.AboveStockGrid, q, "库存超储存货查询.xls");
        }
        public ActionResult AboveStock()
        {
            var gridModel = new AboveStockGridModel();
            SetupAboveStockGridModel(gridModel.AboveStockGrid);
            return PartialView(gridModel);
        }
        #endregion

        #region 库存短缺存货查询
        private void SetupLowStockGridModel(JQGrid grid)
        {
            //SetUpGrid(grid);
            grid.DataUrl = Url.Action("LowStock_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public ActionResult LowStock_RequestData(int QueryType)
        {
            var gridModel = new LowStockGridModel();
            SetupLowStockGridModel(gridModel.LowStockGrid);
            DateTime dtNow = DateTime.Now.Date;
            var lowStock = from d in Uow.CurrentStock.GetAll()
                           join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                           from dd5s in dd5.DefaultIfEmpty()
                           join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                           from dd7s in dd7.DefaultIfEmpty()
                           join d8 in Uow.Warehouse.GetAll() on d.WhId equals d8.Id into dd8
                           from dd8s in dd8.DefaultIfEmpty()
                           join d9 in Uow.Depts.GetAll() on dd8s.Dept equals d9.DeptId into dd9
                           from dd9s in dd9.DefaultIfEmpty()
                           select new
                           {
                               d.Id,
                               WhName = dd8s.Name,
                               DeptId = dd8s.Dept,
                               dd9s.OrganizationId,
                               InvName = dd5s.Name,
                               dd5s.Specs,
                               STUnitName = dd7s.Name,
                               LowStock = dd5s.LowStock,
                               d.Num,
                           };
            var lowStockGroup = from d in lowStock
                                group d by new { d.InvName, d.WhName, d.DeptId, d.OrganizationId, d.Specs, d.STUnitName, d.LowStock } into g
                                select new
                                {
                                    Id = Guid.NewGuid(),
                                    g.Key.InvName,
                                    g.Key.WhName,
                                    g.Key.DeptId,
                                    g.Key.OrganizationId,
                                    g.Key.Specs,
                                    g.Key.STUnitName,
                                    g.Key.LowStock,
                                    Num = g.Sum(s => s.Num),
                                    DifNum = g.Key.LowStock - g.Sum(s => s.Num)
                                };
            switch (QueryType)
            {
                case 1:
                    lowStockGroup = lowStockGroup.Where(w => w.DifNum > 0);
                    break;
            }
            var q = businessCommon.SetVouchAuthority(lowStockGroup, "", false);
            return QueryAndExcel(gridModel.LowStockGrid, q, "库存短缺存货查询.xls");
        }
        public ActionResult LowStock()
        {
            var gridModel = new LowStockGridModel();
            SetupLowStockGridModel(gridModel.LowStockGrid);
            return PartialView(gridModel);
        }
        #endregion

        #region 配方
        private void SetupBillOfMaterialsGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BillOfMaterials_RequestData");
            grid.EditUrl = Url.Action("BillOfMaterials_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";
            SetUpGrid(grid);

            SetDropDownColumn(grid, "PartInvId", this.GetInventory());
            SetDropDownColumn(grid, "ComponentInvId", this.GetInventory());
        }
        [Authorize]
        public ActionResult BillOfMaterials()
        {
            var gridModel = new BillOfMaterialsGridModel();
            SetupBillOfMaterialsGridModel(gridModel.BillOfMaterialsGrid);
            return PartialView(gridModel);
        }
        public ActionResult BillOfMaterials_RequestData()
        {
            var gridModel = new BillOfMaterialsGridModel();
            SetupBillOfMaterialsGridModel(gridModel.BillOfMaterialsGrid);

            var invs = from d in Uow.BillOfMaterials.GetAll()
                       join d1 in Uow.Inventory.GetAll() on d.PartInvId equals d1.Id into dd1
                       from dd1s in dd1.DefaultIfEmpty()

                       join d2 in Uow.MeasurementUnitGroup.GetAll() on dd1s.MeasurementUnitGroup equals d2.Id into dd2
                       from dd2s in dd2.DefaultIfEmpty()

                       join d3 in Uow.UnitOfMeasures.GetAll() on dd1s.MainUnit equals d3.Id into dd3
                       from dd3s in dd3.DefaultIfEmpty()

                       join d4 in Uow.Inventory.GetAll() on d.ComponentInvId equals d4.Id into dd4
                       from dd4s in dd4.DefaultIfEmpty()

                       join d5 in Uow.MeasurementUnitGroup.GetAll() on dd4s.MeasurementUnitGroup equals d5.Id into dd5
                       from dd5s in dd5.DefaultIfEmpty()
                       join d6 in Uow.UnitOfMeasures.GetAll() on dd4s.MainUnit equals d6.Id into dd6
                       from dd6s in dd6.DefaultIfEmpty()
                       select new
                       {
                           d.Id,
                           d.PartInvId,
                           PartInvCode = dd1s.Code,
                           PartInvName = dd1s.Name,
                           PartSpecs = dd1s.Specs,
                           PartGroupName = dd2s.Name,
                           PartStockUnitName = dd3s.Name,
                           d.BaseQtyD,
                           d.ComponentInvId,
                           ComponentInvCode = dd4s.Code,
                           ComponentInvName = dd4s.Name,
                           ComponentSpecs = dd4s.Specs,
                           ComponentGroupName = dd5s.Name,
                           ComponentStockUnitName = dd6s.Name,
                           d.BaseQtyN
                       };
            return QueryAndExcel(gridModel.BillOfMaterialsGrid, invs, "配方.xls");
        }
        private bool CheckBillOfMaterialsDup(DXInfo.Models.BillOfMaterials bom)
        {
            var icount = Uow.BillOfMaterials.GetAll().Where(w => w.PartInvId == bom.PartInvId && w.ComponentInvId == bom.ComponentInvId).Count();

            if (icount > 0)
            {
                return true;
            }
            return false;
        }
        private void addBillOfMaterials(DXInfo.Models.BillOfMaterials bom)
        {
            if (CheckBillOfMaterialsDup(bom))
            {
                throw new DXInfo.Models.BusinessException("子件重复");
            }

            Uow.BillOfMaterials.Add(bom);
            Uow.Commit();
        }
        private void editBillOfMaterials(DXInfo.Models.BillOfMaterials bom)
        {
            var oldbom = Uow.BillOfMaterials.GetById(g => g.Id == bom.Id);
            if ((oldbom.ComponentInvId != bom.ComponentInvId || oldbom.PartInvId != bom.PartInvId) && CheckBillOfMaterialsDup(bom))
            {
                throw new DXInfo.Models.BusinessException("子件重复");
            }

            oldbom.PartInvId = bom.PartInvId;
            oldbom.BaseQtyD = bom.BaseQtyD;
            oldbom.ComponentInvId = bom.ComponentInvId;
            oldbom.BaseQtyN = bom.BaseQtyN;

            Uow.BillOfMaterials.Update(oldbom);
            Uow.Commit();
        }
        private void delBillOfMaterials(DXInfo.Models.BillOfMaterials bom)
        {
            var oldbom = Uow.BillOfMaterials.GetById(g => g.Id == bom.Id);
            Uow.BillOfMaterials.Delete(oldbom);
            Uow.Commit();
        }
        public ActionResult BillOfMaterials_EditData(DXInfo.Models.BillOfMaterials bom)
        {
            var gridModel = new BillOfMaterialsGridModel();
            SetupBillOfMaterialsGridModel(gridModel.BillOfMaterialsGrid);
            return ajaxCallBack<DXInfo.Models.BillOfMaterials>(gridModel.BillOfMaterialsGrid, bom, addBillOfMaterials, editBillOfMaterials, delBillOfMaterials);
        }
        
        #endregion

        #region 评估
        private void SetupProduceEvaluationGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ProduceEvaluation_RequestData");
            grid.DataType = "local";
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetUpGrid(grid);
            SetDropDownColumn(grid, "PartInvId", this.GetInventory());
            SetDropDownColumn(grid, "ComponentInvId", this.GetInventory());
        }
        public ActionResult ProduceEvaluation()
        {

            var gridModel = new ProduceEvaluationGridModel();
            gridModel.BeginDate = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
            gridModel.EndDate = DateTime.Now.Date;
            SetupProduceEvaluationGridModel(gridModel.ProduceEvaluationGrid);
            return PartialView(gridModel);
        }
        public ActionResult ProduceEvaluation_RequestData(DateTime BeginDate, DateTime EndDate, Guid DeptId)
        {
            var gridModel = new ProduceEvaluationGridModel();
            SetupProduceEvaluationGridModel(gridModel.ProduceEvaluationGrid);

            DateTime dtBeginDate = BeginDate;
            DateTime dtEndDate = EndDate;
            Guid deptId = DeptId;
            var eva =
                from a in
                    (
                        from d in Uow.BillOfMaterials.GetAll()

                        join d1 in Uow.RdRecords.GetAll() on d.PartInvId equals d1.InvId into dd1
                        from dd1s in dd1.DefaultIfEmpty()

                        join d2 in Uow.RdRecord.GetAll() on dd1s.RdId equals d2.Id into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        join d3 in Uow.RdRecords.GetAll() on d.ComponentInvId equals d3.InvId into dd3
                        from dd3s in dd3.DefaultIfEmpty()

                        join d4 in Uow.RdRecord.GetAll() on dd3s.RdId equals d4.Id into dd4
                        from dd4s in dd4.DefaultIfEmpty()

                        join d5 in Uow.Inventory.GetAll() on dd1s.InvId equals d5.Id into dd5
                        from dd5s in dd5.DefaultIfEmpty()

                        join d6 in Uow.Inventory.GetAll() on dd3s.InvId equals d6.Id into dd6
                        from dd6s in dd6.DefaultIfEmpty()

                        join d7 in Uow.UnitOfMeasures.GetAll() on dd5s.MainUnit equals d7.Id into dd7
                        from dd7s in dd7.DefaultIfEmpty()

                        join d8 in Uow.UnitOfMeasures.GetAll() on dd6s.MainUnit equals d8.Id into dd8
                        from dd8s in dd8.DefaultIfEmpty()

                        where dd2s.RdDate >= dtBeginDate && dd2s.RdDate <= dtEndDate && dd2s.DeptId == deptId
                              && dd4s.RdDate >= dtBeginDate && dd4s.RdDate <= dtEndDate && dd4s.DeptId == deptId
                        select new
                        {
                            d.PartInvId,
                            PartInvCode = dd5s == null ? "" : dd5s.Code,
                            PartInvName = dd5s == null ? "" : dd5s.Name,
                            PartSpecs = dd5s == null ? "" : dd5s.Specs,
                            PartStockUnitName = dd5s == null ? "" : dd7s.Name,
                            d.ComponentInvId,
                            ComponentInvCode = dd6s == null ? "" : dd6s.Code,
                            ComponentInvName = dd6s == null ? "" : dd6s.Name,
                            ComponentSpecs = dd6s == null ? "" : dd6s.Specs,
                            ComponentStockUnitName = dd8s == null ? "" : dd8s.Name,
                            Num1 = d.BaseQtyN / d.BaseQtyD,
                            Num2 = (dd3s == null ? 0 : dd3s.Num),
                            Num3 = (dd1s == null ? 0 : dd1s.Num)
                        }
                        )
                group a by new
                {
                    a.PartInvId,
                    a.PartInvCode,
                    a.PartInvName,
                    a.PartSpecs,
                    a.PartStockUnitName,
                    a.ComponentInvId,
                    a.ComponentInvCode,
                    a.ComponentInvName,
                    a.ComponentSpecs,
                    a.ComponentStockUnitName,
                    a.Num1
                } into g
                select new
                {
                    PartInvId = g.Key.PartInvId,
                    PartInvCode = g.Key.PartInvCode,
                    PartInvName = g.Key.PartInvName,
                    PartSpecs = g.Key.PartSpecs,
                    PartStockUnitName = g.Key.PartStockUnitName,
                    ComponentInvId = g.Key.ComponentInvId,
                    ComponentInvCode = g.Key.ComponentInvCode,
                    ComponentInvName = g.Key.ComponentInvName,
                    ComponentSpecs = g.Key.ComponentSpecs,
                    ComponentStockUnitName = g.Key.ComponentStockUnitName,
                    Num1 = g.Key.Num1,
                    Num2 = g.Sum(s => s.Num2) / g.Sum(s => s.Num3)
                };
            return QueryAndExcel(gridModel.ProduceEvaluationGrid, eva, "评估.xls");
        }
        #endregion

        #region 库存部门配料仓
        private void SetupWarehouseDeptGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WarehouseDept_RequestData");
            grid.EditUrl = Url.Action("WarehouseDept_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "Dept", this.GetDept());
            SetDropDownColumn(grid, "Warehouse", centerCommon.GetWarehouse());            
        }
        public ActionResult WarehouseDept()
        {
            var gridModel = new WarehouseDeptGridModel();
            SetupWarehouseDeptGridModel(gridModel.WarehouseDeptGrid);
            return PartialView(gridModel);
        }
        public ActionResult WarehouseDept_RequestData()
        {
            var gridModel = new WarehouseDeptGridModel();
            SetupWarehouseDeptGridModel(gridModel.WarehouseDeptGrid);

            var units = from d in Uow.WarehouseDept.GetAll()
                        join d1 in Uow.Depts.GetAll() on d.Dept equals d1.DeptId into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        join d2 in Uow.Warehouse.GetAll() on d.Warehouse equals d2.Id into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        select new { d.Id, d.Dept, DeptName = dd1s.DeptName, d.Warehouse, WarehouseName = dd2s.Name };
            return QueryAndExcel(gridModel.WarehouseDeptGrid, units, "库存部门配料仓.xls");
        }
        private bool CheckWarehouseDeptIdDup(Guid dept,Guid warehouse)
        {
            return Uow.WarehouseDept.GetAll().Where(w => w.Dept == dept && w.Warehouse==warehouse).Count() > 0;
        }
        private void addWarehouseDept(DXInfo.Models.WarehouseDept warehouseDept)
        {
            if (CheckWarehouseDeptIdDup(warehouseDept.Dept, warehouseDept.Warehouse))
            {
                throw new DXInfo.Models.BusinessException("部门配料仓重复");
            }
            Uow.WarehouseDept.Add(warehouseDept);
            Uow.Commit();
        }
        private void editWarehouseDept(DXInfo.Models.WarehouseDept warehouseDept)
        {
            var oldWarehouseDept = Uow.WarehouseDept.GetById(g => g.Id == warehouseDept.Id);
            if (oldWarehouseDept.Dept != warehouseDept.Dept && oldWarehouseDept.Warehouse != warehouseDept.Warehouse && CheckWarehouseDeptIdDup(warehouseDept.Dept, warehouseDept.Warehouse))
            {
                throw new DXInfo.Models.BusinessException("部门配料仓重复");
            }
            oldWarehouseDept.Dept = warehouseDept.Dept;
            oldWarehouseDept.Warehouse = warehouseDept.Warehouse;
            Uow.WarehouseDept.Update(oldWarehouseDept);
            Uow.Commit();
        }
        private void delWarehouseDept(DXInfo.Models.WarehouseDept warehouseDept)
        {
            var oldWarehouseDept = Uow.WarehouseDept.GetById(g => g.Id == warehouseDept.Id);
            Uow.WarehouseDept.Delete(oldWarehouseDept);
            Uow.Commit();
        }
        public ActionResult WarehouseDept_EditData(DXInfo.Models.WarehouseDept warehouseDept)
        {
            var gridModel = new WarehouseDeptGridModel();
            SetupWarehouseDeptGridModel(gridModel.WarehouseDeptGrid);
            return ajaxCallBack<DXInfo.Models.WarehouseDept>(gridModel.WarehouseDeptGrid, warehouseDept, addWarehouseDept, editWarehouseDept, delWarehouseDept);
        }
        #endregion

        #region 仓库存货关联
        private void SetupWarehouseInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WarehouseInventory_RequestData");
            grid.EditUrl = Url.Action("WarehouseInventory_EditData");

            SetUpGrid(grid);
            SetDropDownColumn(grid, "Inventory", this.GetInventory());
            SetDropDownColumn(grid, "Warehouse", centerCommon.GetWarehouse());
        }
        public ActionResult WarehouseInventory()
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);
            return PartialView(gridModel);
        }
        public ActionResult WarehouseInventory_RequestData()
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);

            var q = from d in Uow.WarehouseInventory.GetAll()
                        join d1 in Uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        join d2 in Uow.Warehouse.GetAll() on d.Warehouse equals d2.Id into dd2
                        from dd2s in dd2.DefaultIfEmpty()
                        orderby d.Id
                        select new { d.Id, d.Warehouse, WarehouseName = dd2s.Name,Inventory=dd1s.Id,InventoryName=dd1s.Name };
            return QueryAndExcel(gridModel.WarehouseInventoryGrid, q, "仓库存货关联.xls");
        }
        private bool CheckWarehouseInventoryIdDup(Guid warehouse,Guid inventory)
        {
            return Uow.WarehouseInventory.GetAll().Where(w => w.Inventory == inventory && w.Warehouse == warehouse).Count() > 0;
        }
        private void addWarehouseInventory(DXInfo.Models.WarehouseInventory warehouseInventory)
        {
            if (CheckWarehouseInventoryIdDup(warehouseInventory.Warehouse, warehouseInventory.Inventory))
            {
                throw new DXInfo.Models.BusinessException("仓库存货关联重复");
            }
            Uow.WarehouseInventory.Add(warehouseInventory);
            Uow.Commit();
        }
        private void editWarehouseInventory(DXInfo.Models.WarehouseInventory warehouseInventory)
        {
            var oldWarehouseInventory = Uow.WarehouseInventory.GetById(g => g.Id == warehouseInventory.Id);
            if (oldWarehouseInventory.Inventory != warehouseInventory.Inventory
                && oldWarehouseInventory.Warehouse != warehouseInventory.Warehouse
                && CheckWarehouseInventoryIdDup(warehouseInventory.Warehouse, warehouseInventory.Inventory))
            {
                throw new DXInfo.Models.BusinessException("部门配料仓重复");
            }
            oldWarehouseInventory.Inventory = warehouseInventory.Inventory;
            oldWarehouseInventory.Warehouse = warehouseInventory.Warehouse;
            Uow.WarehouseInventory.Update(oldWarehouseInventory);
            Uow.Commit();
        }
        private void delWarehouseInventory(DXInfo.Models.WarehouseInventory warehouseInventory)
        {
            var oldWarehouseInventory = Uow.WarehouseInventory.GetById(g => g.Id == warehouseInventory.Id);
            Uow.WarehouseInventory.Delete(oldWarehouseInventory);
            Uow.Commit();
        }
        public ActionResult WarehouseInventory_EditData(DXInfo.Models.WarehouseInventory warehouseInventory)
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);
            return ajaxCallBack<DXInfo.Models.WarehouseInventory>(gridModel.WarehouseInventoryGrid, warehouseInventory, addWarehouseInventory, editWarehouseInventory, delWarehouseInventory);
        }
        #endregion        

        #region 客户端调用
        public JsonResult GetCurrentStock(string InvName)
        {
            var q = from d in Uow.CurrentStock.GetAll()
                    join d1 in Uow.Warehouse.GetAll() on d.WhId equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    join d2 in Uow.Depts.GetAll() on dd1s.Dept equals d2.DeptId into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    join d3 in Uow.Inventory.GetAll() on d.InvId equals d3.Id into dd3
                    from dd3s in dd3.DefaultIfEmpty()
                    join d4 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d4.Id into dd4
                    from dd4s in dd4.DefaultIfEmpty()
                    select new
                    {
                        DeptId = dd1s.Dept,
                        dd2s.DeptName,
                        d.WhId,
                        WhName = dd1s.Name,
                        d.InvId,
                        InvName=dd3s.Name,
                        d.STUnit,
                        STName=dd4s.Name,
                        d.Num,
                    };
            if (!string.IsNullOrEmpty(InvName))
            {
                q = q.Where(w => w.InvName.Contains(InvName));
            }
            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
