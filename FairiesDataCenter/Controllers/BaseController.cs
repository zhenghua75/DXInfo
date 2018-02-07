using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DXInfo.Data.Contracts;
using System.Web.Mvc;
using DXInfo.Models;
using Trirand.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
//using ynhnTransportManage.Helpers;

namespace ynhnTransportManage.Controllers
{
    /// <summary>
    /// 001西餐002冷饮店
    /// </summary>
    public class BaseController:System.Web.Mvc.Controller
    {
        #region 字段
        /// <summary>
        /// 当班操作员是否显示
        /// </summary>
        protected bool isOperatorsOnDuty;
        /// <summary>
        /// 批号是否显示
        /// </summary>
        protected bool isBatch;
        /// <summary>
        /// 货位是否显示
        /// </summary>
        protected bool isLocator;
        /// <summary>
        /// 保质期是否显示
        /// </summary>
        protected bool isShelfLife;
        /// <summary>
        /// 库存存货档案零售部分是否显示
        /// </summary>
        protected bool saleColumnVisibility;
        /// <summary>
        /// 库存存货档案ipad部分是否显示
        /// </summary>
        protected bool ipadColumnVisibility;
        /// <summary>
        /// 库存存货档案jewelry部分是否显示
        /// </summary>
        protected bool jewelryColumnVisibility;
        /// <summary>
        /// 批号是否必须输入
        /// </summary>
        protected bool isNecessaryBatch;
        /// <summary>
        /// 销售出库单是否显示折扣
        /// </summary>
        protected bool isSaleDiscount;
        /// <summary>
        /// 计量单位列是否显示
        /// </summary>
        protected bool unitOfMeasureColumnVisibility;
        /// <summary>
        /// 杯型列是否显示
        /// </summary>
        protected bool cupTypeColumnVisible;
        protected bool otherOutStockPriceColumnVisible;
        protected bool otherOutStockAmountColumnVisible;
        protected bool salePrice0ColumnVisible;
        protected bool salePrice1ColumnVisible;
        protected bool salePrice2ColumnVisible;
        protected bool transVouchPriceColumnVisible;
        protected bool transVouchAmountColumnVisible;
        protected bool isProductTypeVisible;
        protected bool isDisplayImage;
        //protected bool DeptTypeColumnVisible;DXInfo.Models.SectionType.
        protected DXInfo.Business.Common businessCommon;
        protected Common centerCommon;
        /// <summary>
        /// 当前操作员ID
        /// </summary>
        protected Guid operId;
        /// <summary>
        /// 当前部门ID
        /// </summary>
        protected Guid deptId;
        #endregion

        #region 属性
        protected string Title { get; set; }
        #endregion

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //if (string.IsNullOrEmpty(ViewBag.Title))
            //{
                this.Title = Common.GetTitle(requestContext);
                ViewBag.Title = this.Title;
            //}
            //else
            //{
            //    this.Title = ViewBag.Title;
            //}
        }
        protected IFairiesMemberManageUow Uow { get; set; }
        public BaseController(IFairiesMemberManageUow uow)
        {
            this.Uow = uow;
            this.businessCommon = new DXInfo.Business.Common(uow);
            this.isOperatorsOnDuty = businessCommon.OperatorsOnDuty();
            this.isBatch = businessCommon.IsBatch();
            this.isLocator = businessCommon.IsLocator();
            this.isShelfLife = businessCommon.IsShelfLife();
            this.saleColumnVisibility = businessCommon.SaleColumnVisibility();
            this.ipadColumnVisibility = businessCommon.IpadColumnVisibility();
            this.jewelryColumnVisibility = businessCommon.JewelryColumnVisibility();
            this.isNecessaryBatch = businessCommon.IsNecessaryBatch();
            this.isSaleDiscount = businessCommon.IsSaleDiscount();
            this.unitOfMeasureColumnVisibility = businessCommon.UnitOfMeasureColumnVisibility();
            this.cupTypeColumnVisible = businessCommon.CupTypeColumnVisible();
            this.otherOutStockPriceColumnVisible = businessCommon.OtherOutStockPriceColumnVisible();
            this.otherOutStockAmountColumnVisible = businessCommon.OtherOutStockAmountColumnVisible();
            this.salePrice0ColumnVisible = businessCommon.SalePrice0ColumnVisible();
            this.salePrice1ColumnVisible = businessCommon.SalePrice1ColumnVisible();
            this.salePrice2ColumnVisible = businessCommon.SalePrice2ColumnVisible();
            this.transVouchPriceColumnVisible = businessCommon.TransVouchPriceColumnVisible();
            this.transVouchAmountColumnVisible = businessCommon.TransVouchAmountColumnVisible();
            this.isDisplayImage = businessCommon.IsDisplayImage();

            MembershipUser muser = Membership.GetUser();
            if (muser != null)
            {
                string struserid = muser.ProviderUserKey.ToString();
                if (!string.IsNullOrEmpty(struserid))
                {
                    this.operId = Guid.Parse(struserid);

                    DXInfo.Models.aspnet_CustomProfile user = uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == this.operId).FirstOrDefault();
                    if (user == null || !user.DeptId.HasValue)
                    {
                        deptId = Guid.Empty;
                    }
                    else
                    {
                        deptId = user.DeptId.Value;
                    }
                }
            }
            this.centerCommon = new Common(uow);
            this.isProductTypeVisible = centerCommon.IsProductTypeVisible();
        }

        protected ActionResult QueryAndExcel(JQGrid grid, IQueryable query, string excelFileName)
        {
            switch (grid.AjaxCallBackMode)
            {
                case AjaxCallBackMode.RequestData:
                case AjaxCallBackMode.Search:
                    return grid.DataBind(query);
                case AjaxCallBackMode.Excel:
                    grid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    grid.ExportToExcel(query, excelFileName, grid.GetState());
                    break;
            }
            return new EmptyResult();
        }
        protected ActionResult ajaxCallBack<T>(JQGrid grid,
            T entity,
            Action<T> addRow,
            Action<T> editRow,
            Action<T> delRow
            )
        {
            try
            {
                switch (grid.AjaxCallBackMode)
                {
                    case AjaxCallBackMode.AddRow:
                        addRow(entity);
                        break;
                    case AjaxCallBackMode.EditRow:
                        editRow(entity);
                        break;
                    case AjaxCallBackMode.DeleteRow:
                        delRow(entity);
                        break;
                }
            }
            catch (BusinessException bex)
            {
                return grid.ShowEditValidationMessage(bex.Message);
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return grid.ShowEditValidationMessage(dex.Message);
            }
            return new EmptyResult();
        }
        protected void SetDropDownColumn(JQGrid grid, string dataField, List<SelectListItem> listItems)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Searchable)
                {
                    column.SearchType = SearchType.DropDown;
                    if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
                    {
                        column.SearchList = listItems;
                    }
                }
                if (column.Editable)
                {
                    column.EditType = EditType.DropDown;
                    if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
                    {
                        column.EditList = listItems;
                    }
                }
                if (column.Visible)
                {
                    column.Formatter = new DropDownFormatter();
                }
            }
        }
        protected void SetBoolColumn(JQGrid grid, string dataField)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Visible)
                {
                    column.Formatter = new CheckBoxFormatter();
                }
                if (column.Searchable)
                {
                    column.SearchType = SearchType.DropDown;
                    if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
                    {
                        column.SearchList = centerCommon.GetBoolDesc();
                    }
                }
                if (column.Editable)
                {
                    column.EditType = EditType.CheckBox;
                }
            }
        }
        protected void SetDateColumn(JQGrid grid, string dataField)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Visible)
                {
                    column.DataFormatString = "{0:yyyy-MM-dd}";
                }
                if (column.Searchable)
                {
                    column.SearchType = SearchType.DatePicker;
                    column.SearchControlID = "DatePicker";
                }
                if (column.Editable)
                {
                    column.EditType = EditType.DatePicker;
                    column.EditorControlID = "DatePicker";
                }
            }
        }
        protected void SetDateTimeColumn(JQGrid grid, string dataField)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Visible)
                {
                    column.DataFormatString = "{0:yyyy-MM-dd HH:mm}";
                }
                if (column.Searchable)
                {
                    column.SearchType = SearchType.DateTimePicker;
                    column.SearchControlID = "DateTimePicker";
                }
                if (column.Editable)
                {
                    column.EditType = EditType.DateTimePicker;
                    column.EditorControlID = "DateTimePicker";
                }
            }
        }
        protected void SetTimeColumn(JQGrid grid, string dataField)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Visible)
                {
                    column.DataFormatString = @"{0:hh\:mm}";
                }
                if (column.Searchable)
                {
                    column.SearchType = SearchType.TimePicker;
                    column.SearchControlID = "TimePicker";
                }
                if (column.Editable)
                {
                    column.EditType = EditType.TimePicker;
                    column.EditorControlID = "TimePicker";
                }
            }
        }
        public void SetUpGrid(JQGrid grid)
        {            
            grid.AppearanceSettings.Caption = this.Title;
        }
        protected void SetGridColumn(JQGrid grid, string dataField, bool enabled)
        {
            JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
            if (column != null)
            {
                if (column.Exportable)
                {
                    column.Exportable = enabled;
                }
                else
                {
                    column.Visible = enabled;
                    column.Editable = enabled;
                    column.Searchable = enabled;
                }
                //column.Exportable = enabled;
            }
        }
        //protected void SetGridColumnVisible(JQGrid grid, string dataField, bool visible)
        //{
        //    JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
        //    if (column != null)
        //    {
        //        column.Visible = visible;
        //        column.Searchable = visible;
        //    }
        //}
        //protected void SetGridColumnEditable(JQGrid grid, string dataField, bool editable)
        //{
        //    JQGridColumn column = grid.Columns.Find(c => c.DataField == dataField);
        //    if (column != null)
        //    {
        //        column.Editable = editable;
        //        column.Searchable = editable;
        //    }
        //}

        protected List<SelectListItem> GetVendor()
        {
            return centerCommon.GetVendor(this.Request);
        }
        protected List<SelectListItem> GetOper()
        {
            return centerCommon.GetOper(this.Request);
        }
        protected List<SelectListItem> GetCategory()
        {
            return centerCommon.GetCategory(this.Request);
        }
        protected List<SelectListItem> GetInventory()
        {
            return centerCommon.GetInventory(this.Request);
        }
        protected List<SelectListItem> GetInventoryExceptPackage()
        {
            return centerCommon.GetInventoryExceptPackage(this.Request);
        }
        protected List<SelectListItem> GetDept()
        {
            return centerCommon.GetDept(this.Request);
        }
    }
}