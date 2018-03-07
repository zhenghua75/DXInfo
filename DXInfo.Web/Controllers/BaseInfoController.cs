using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Data;
using System.Collections;
using Ninject;
using DXInfo.Data.Contracts;
using AutoMapper;
using DXInfo.Models;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using DXInfo.Web.Models;
using System.Data.Entity.SqlServer;
using System.Data.OleDb;
using System.Transactions;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace DXInfo.Web.Controllers
{
    
    [Authorize]
    public class BaseInfoController : BaseController
    {
        #region 构造
        public BaseInfoController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");    
        }
        #endregion
        
        #region 参数设置
        public ActionResult Para()
        {
            var gridModel = new ParaGridModel();
            SetupParaGridModel(gridModel.ParasGrid);
            return PartialView(gridModel);
        }
        private void SetupParaGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Para_RequestData");
            grid.EditUrl = Url.Action("Para_EditData");
            SetUpGrid(grid);
            this.SetDropDownColumn(grid, "Type", centerCommon.GetNameCodeType());
        }
        public ActionResult Para_RequestData()
        {
            var ncs = Uow.NameCode.GetAll();
            var gridModel = new ParaGridModel();
            SetupParaGridModel(gridModel.ParasGrid);
            return QueryAndExcel(gridModel.ParasGrid, ncs, "参数.xls");
        }
        private string GetNameCodeTypeName(string type)
        {
            List<SelectListItem> lsi = centerCommon.GetNameCodeType();
            SelectListItem sli = lsi.Where(w => w.Value == type).FirstOrDefault();
            if (sli != null)
            {
                return sli.Text;
            }
            return "";
        }
        private bool CheckNameCodeTypeDup(string type,out string msg)
        {
            bool isDup = false;
            msg = "";
            var n = Uow.NameCode.GetAll().Where(w => w.Type == type).Count();
            if (n>0)
            {
                msg = GetNameCodeTypeName(type) + "只能有一条";
                isDup = true;
            }
            return isDup;
        }
        private bool CheckNameCodeValueNull(string type,string value,out string msg)
        {
            bool isNull = false;
            msg = "";
            if (string.IsNullOrEmpty(value))
            {
                msg = "请输入"+GetNameCodeTypeName(type)+"(值)";
                isNull = true;
            }
            return isNull;
        }
        private bool CheckNameCodeValuePositiveInt(string type, string value, out string msg)
        {
            bool isPositiveInt = true;
            msg = "";
            if (!DXInfo.Models.RegexCheck.IsPositiveInt(value))
            {
                isPositiveInt = false;
                msg = GetNameCodeTypeName(type)+"请输入正整数";
            }
            return isPositiveInt;
        }
        private bool CheckNameCodeValuePositiveNum(string type, string value, out string msg)
        {
            bool isPositiveNum = true;
            msg = "";
            if (!DXInfo.Models.RegexCheck.IsPositiveNumeric(value))
            {
                isPositiveNum = false;
                msg = GetNameCodeTypeName(type) + "请输入正数";
            }
            return isPositiveNum;
        }
        private void addNameCode(DXInfo.Models.NameCode nc)
        {
            if (nc.Type != DXInfo.Models.NameCodeType.NoActiveXCheck.ToString())
            {
                string msg;
                if (CheckNameCodeTypeDup(nc.Type, out msg))
                {
                    throw new DXInfo.Models.BusinessException(msg);
                }
                if (CheckNameCodeValueNull(nc.Type, nc.Value, out msg))
                {
                    throw new DXInfo.Models.BusinessException(msg);
                }
            }
            Uow.NameCode.Add(nc);
            Uow.Commit();
        }
        private void editNameCode(DXInfo.Models.NameCode nc)
        {
            var oldnc = Uow.NameCode.GetById(g => g.ID == nc.ID);
            oldnc.Code = nc.Code;
            oldnc.Name = nc.Name;
            oldnc.Value = nc.Value;
            oldnc.Comment = nc.Comment;
            Uow.NameCode.Update(oldnc);
            Uow.Commit();
        }
        private void delNameCode(DXInfo.Models.NameCode nc)
        {
            var oldnc = Uow.NameCode.GetById(g => g.ID == nc.ID);
            Uow.NameCode.Delete(oldnc);
            Uow.Commit();
        }
        public ActionResult Para_EditData(DXInfo.Models.NameCode nc)
        {
            var gridModel = new ParaGridModel();
            SetupParaGridModel(gridModel.ParasGrid);
            string msg;
            switch (nc.Type)
            {
                case "CostFee":
                    if (!CheckNameCodeValuePositiveNum(nc.Type, nc.Value, out msg))
                    {
                        return new HttpErrorResult(msg);
                        //return gridModel.ParasGrid.ShowEditValidationMessage(msg);
                    }
                    break;                
            }

            return ajaxCallBack<DXInfo.Models.NameCode>(gridModel.ParasGrid, nc, addNameCode, editNameCode, delNameCode);
        }
        #endregion

        #region 部门
        private void SetupOrganizationsGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Organizations_RequestData");
            grid.EditUrl = Url.Action("Organizations_EditData");
            SetUpGrid(grid);
        }
        public ActionResult Organizations()
        {
            var gridModel = new OrganizationsGridModel();
            SetupOrganizationsGridModel(gridModel.OrganizationsGrid);
            return PartialView(gridModel);
        }
        public ActionResult Organizations_RequestData()
        {
            var gridModel = new OrganizationsGridModel();
            SetupOrganizationsGridModel(gridModel.OrganizationsGrid);
            var q = Uow.Organizations.GetAll();
            return QueryAndExcel(gridModel.OrganizationsGrid, q, "部门.xls");
        }
        private void addOrg(DXInfo.Models.Organizations org)
        {
            Uow.Organizations.Add(org);
            Uow.Commit();
        }
        private void editOrg(DXInfo.Models.Organizations org)
        {
            var oldinv = Uow.Organizations.GetById(g => g.Id == org.Id);
            oldinv.Code = org.Code;
            oldinv.Name = org.Name;
            oldinv.Comment = org.Comment;
            Uow.Organizations.Update(oldinv);
            Uow.Commit();
        }
        private void delOrg(DXInfo.Models.Organizations org)
        {
            var oldOrg = Uow.Organizations.GetById(g => g.Id == org.Id);
            if (oldOrg != null)
            {
                Uow.Organizations.Delete(oldOrg);
                Uow.Commit();
            }
        }
        public ActionResult Organizations_EditData(DXInfo.Models.Organizations org)
        {
            var gridModel = new OrganizationsGridModel();
            SetupOrganizationsGridModel(gridModel.OrganizationsGrid);
            return ajaxCallBack<DXInfo.Models.Organizations>(gridModel.OrganizationsGrid, org, addOrg, editOrg, delOrg);
        }
        #endregion

        #region 门店
        public ActionResult Dept()
        {
            var deptModel = new DeptGridModel();
            SetupDeptJqGridModel(deptModel.DeptsGrid);
            return PartialView(deptModel);
        }
        private void SetupDeptJqGridModel(JQGrid grid)
        {
            grid.SortSettings.InitialSortColumn = "DeptCode";
            grid.SortSettings.InitialSortDirection = SortDirection.Asc;
            grid.DataUrl = Url.Action("Dept_RequestData");
            grid.EditUrl = Url.Action("Dept_EditData");

            SetBoolColumn(grid, "IsDeptPrice");
            SetDropDownColumn(grid, "DeptType", centerCommon.GetDeptType());
            SetDropDownColumn(grid, "OrganizationId", centerCommon.GetOrganization());
            SetUpGrid(grid);
        }        
        public ActionResult Dept_RequestData()
        {
            string deptType = DXInfo.Models.NameCodeType.DeptType.ToString();
            var q = from d in Uow.Depts.GetAll()
                         join o in Uow.Organizations.GetAll() on d.OrganizationId equals o.Id into od
                         from ods in od.DefaultIfEmpty()

                         join d1 in Uow.NameCode.GetAll().Where(w => w.Type == deptType) on SqlFunctions.StringConvert((double?)d.DeptType).Trim() equals d1.Code into dd1
                         from dd1s in dd1.DefaultIfEmpty()

                         select new
                         {
                             d.DeptId,
                             d.DeptCode,
                             d.DeptName,
                             d.ParentDeptId,
                             d.Address,
                             d.Comment,
                             d.IsDeptPrice,
                             OrganizationId = (ods == null) ? Guid.Empty : ods.Id,
                             OrganizationName = (ods == null) ? "" : ods.Name,
                             d.DeptType,
                             DeptTypeName = (ods == null) ? "" : dd1s.Name,
                         };
            var gridModel = new DeptGridModel();
            SetupDeptJqGridModel(gridModel.DeptsGrid);
            return QueryAndExcel(gridModel.DeptsGrid, q,"门店.xls");

        }
        private void addDept(DXInfo.Models.Depts dept)
        {
            DXInfo.Models.Depts newdept = new DXInfo.Models.Depts();
            newdept.DeptCode = dept.DeptCode;
            newdept.DeptName = dept.DeptName;
            newdept.Address = dept.Address;
            newdept.Comment = dept.Comment;
            newdept.OrganizationId = dept.OrganizationId;
            newdept.IsDeptPrice = dept.IsDeptPrice;
            newdept.DeptType = dept.DeptType;
            Uow.Depts.Add(newdept);
            Uow.Commit();
        }
        private void editDept(DXInfo.Models.Depts dept)
        {
            var olddept = Uow.Depts.GetById(g => g.DeptId == dept.DeptId);
            olddept.DeptName = dept.DeptName;
            olddept.Comment = dept.Comment;
            olddept.Address = dept.Address;
            olddept.IsDeptPrice = dept.IsDeptPrice;
            olddept.OrganizationId = dept.OrganizationId;
            olddept.DeptType = dept.DeptType;
            Uow.Depts.Update(olddept);
            Uow.Commit();
        }
        private void delDept(DXInfo.Models.Depts dept)
        {
            var oldDept = Uow.Depts.GetById(g => g.DeptId == dept.DeptId);
            if (oldDept != null)
            {
                Uow.Depts.Delete(oldDept);
                Uow.Commit();
            }
        }
        public ActionResult Dept_EditData(DXInfo.Models.Depts dept)
        {
            var deptModel = new DeptGridModel();
            SetupDeptJqGridModel(deptModel.DeptsGrid);
            return ajaxCallBack<DXInfo.Models.Depts>(deptModel.DeptsGrid, dept, addDept, editDept, delDept);
        }
        #endregion

        #region 其它
        #region 车辆信息
        private void SetupVehicleGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Vehicle_RequestData");
            grid.EditUrl = Url.Action("Vehicle_EditData");
        }
        public ActionResult Vehicle()
        {
            var gridModel = new VehicleGridModel();
            SetupVehicleGridModel(gridModel.VehicleGrid);
            return View(gridModel);
        }
        public ActionResult Vehicle_RequestData()
        {
            var gridModel = new VehicleGridModel();
            SetupVehicleGridModel(gridModel.VehicleGrid);
            var vehicles =
                (from v in Uow.Vehicles.GetAll()
                 select new
                 {
                     v.Id,
                     v.PlateNo,
                     v.MotorNo,
                     v.BrandModel,
                     v.Comment,
                     v.OwnerName
                 });
            return gridModel.VehicleGrid.DataBind(vehicles);
        }
        public ActionResult Vehicle_EditData(DXInfo.Models.Vehicles vehicle)
        {
            var gridModel = new VehicleGridModel();
            if (gridModel.VehicleGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                var v = Uow.Vehicles.GetAll().Where(w => w.PlateNo == vehicle.PlateNo).FirstOrDefault();
                if (v != null)
                    return new HttpErrorResult("车牌号已存在");
                    //return gridModel.VehicleGrid.ShowEditValidationMessage("车牌号已存在");
                Uow.Vehicles.Add(vehicle);
                Uow.Commit();
            }
            if (gridModel.VehicleGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldvehicle = Uow.Vehicles.GetById(g => g.Id == vehicle.Id);//.Where(w => w.Id == vehicle.Id).FirstOrDefault();
                if (oldvehicle.PlateNo != vehicle.PlateNo)
                {
                    var v = Uow.Vehicles.GetAll().Where(w => w.PlateNo == vehicle.PlateNo).FirstOrDefault();
                    if (v != null)
                        return new HttpErrorResult("车牌号已存在");
                }
                oldvehicle.PlateNo = vehicle.PlateNo;
                oldvehicle.BrandModel = vehicle.BrandModel;
                oldvehicle.MotorNo = vehicle.MotorNo;
                oldvehicle.Comment = vehicle.Comment;
                oldvehicle.OwnerName = vehicle.OwnerName;
                Uow.Vehicles.Update(oldvehicle);
                Uow.Commit();
            }
            return RedirectToAction("Vehicle");
        }
        #endregion

        #region 车主信息
        private void SetupDriversGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Driver_RequestData");
            grid.EditUrl = Url.Action("Driver_EditData");
        }
        public ActionResult Driver()
        {
            var gridModel = new DriversGridModel();
            SetupDriversGridModel(gridModel.DriversGrid);
            return View(gridModel);
        }
        public ActionResult Driver_RequestData()
        {
            var gridModel = new DriversGridModel();
            SetupDriversGridModel(gridModel.DriversGrid);

            var drivers = Uow.Drivers.GetAll().ToList();
            return gridModel.DriversGrid.DataBind(drivers);
        }
        public ActionResult Driver_EditData(DXInfo.Models.Drivers driver)
        {
            var gridModel = new DriversGridModel();
            SetupDriversGridModel(gridModel.DriversGrid);

            if (gridModel.DriversGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                Uow.Drivers.Add(driver);
                Uow.Commit();
            }
            if (gridModel.DriversGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var olddriver = Uow.Drivers.GetById(g => g.Id == driver.Id);
                olddriver.Code = driver.Code;
                olddriver.Name = driver.Name;
                olddriver.DriverNo = driver.DriverNo;
                olddriver.Telephone = driver.Telephone;
                olddriver.Address = driver.Address;
                olddriver.IdCardNo = driver.IdCardNo;
                olddriver.Comment = driver.Comment;
                Uow.Drivers.Update(olddriver);
                Uow.Commit();
            }
            return RedirectToAction("Driver");
        }
        #endregion

        #region 运输路线
        private void SetupLinesGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Lines_RequestData");
            grid.EditUrl = Url.Action("Lines_EditData");
        }
        public ActionResult Lines()
        {
            var gridModel = new LinesGridModel();
            SetupLinesGridModel(gridModel.LinesGrid);
            return View(gridModel);
        }
        public ActionResult Lines_RequestData()
        {
            var gridModel = new LinesGridModel();
            SetupLinesGridModel(gridModel.LinesGrid);

            var lines = Uow.Lines.GetAll();
            return gridModel.LinesGrid.DataBind(lines);
        }
        public ActionResult Lines_EditData(DXInfo.Models.Lines line)
        {
            var gridModel = new LinesGridModel();
            SetupLinesGridModel(gridModel.LinesGrid);
            if (gridModel.LinesGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                Uow.Lines.Add(line);
                Uow.Commit();
            }
            if (gridModel.LinesGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldline = Uow.Lines.GetById(g => g.Id == line.Id);
                oldline.Code = line.Code;
                oldline.Name = line.Name;
                oldline.Comment = line.Comment;
                oldline.Mileage = line.Mileage;
                Uow.Lines.Update(oldline);
                Uow.Commit();
            }
            return RedirectToAction("Lines");
        }
        #endregion
        #endregion

        #region 存货分类
        private void SetUpInventoryCategoryGrid(JQGrid grid,int DeptType,int CategoryType)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("InventoryCategory_RequestData");
            grid.EditUrl = Url.Action("InventoryCategory_EditData");

            //if (isProductTypeVisible)
            //{
            //    SetDropDownColumn(grid, "ProductType", centerCommon.GetProductType());
            //}
            //SetGridColumn(grid, "ProductType", isProductTypeVisible);
            //SetGridColumn(grid, "ProductTypeName", isProductTypeVisible);

            bool isShop = DeptType == (int)DXInfo.Models.DeptType.Shop;
            if (isShop)
            {
                SetDropDownColumn(grid, "SectionType", centerCommon.GetSectionType());                
            }
            SetGridColumn(grid, "SectionType", isShop);
            SetGridColumn(grid, "SectionTypeName", isShop);
            //if (CategoryType == 2)
            //{
            //    SetGridColumn(grid, "IsDiscount", false);
            //}
            SetBoolColumn(grid, "IsDiscount");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.SerializeRowData = "serializeGridData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.SerializeDelData = "serializeGridData";
            
        }
        private InventoryCategoryGridModel SetUpInventoryCateogryGridModel(int CategoryType, int DeptType)
        {
            var gridModel = new InventoryCategoryGridModel();
            gridModel.CategoryType = CategoryType;
            gridModel.DeptType = DeptType;
            SetUpInventoryCategoryGrid(gridModel.InventoryCategoryGrid, DeptType,CategoryType);
            return gridModel;
        }
        public ActionResult InventoryCategory(int CategoryType,int DeptType)
        {
            var gridModel = SetUpInventoryCateogryGridModel(CategoryType, DeptType);
            return PartialView(gridModel);
        }
        public ActionResult InventoryCategory_RequestData(int CategoryType,int DeptType)
        {
            var gridModel = SetUpInventoryCateogryGridModel(CategoryType, DeptType);
            var q = from d in Uow.InventoryCategory.GetAll()
                    join d1 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == "ProductType") on d.ProductType equals d1.Value into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)d.SectionType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.Id,
                        d.Code,
                        d.Name,
                        d.Comment,
                        d.IsDiscount,
                        d.CategoryType,
                        d.SectionType,
                        SectionTypeName = dd2s.Name,
                        d.ProductType,
                        ProductTypeName = dd1s.Description,
                    };
            return QueryAndExcel(gridModel.InventoryCategoryGrid, q, "存货分类.xls");
        }
        private void addCategory(DXInfo.Models.InventoryCategory categroy)
        {
            Uow.InventoryCategory.Add(categroy);
            Uow.Commit();
        }
        private void editCategory(DXInfo.Models.InventoryCategory categroy)
        {
            var oldCategroy = Uow.InventoryCategory.GetById(g => g.Id == categroy.Id);
            oldCategroy.Code = categroy.Code;
            oldCategroy.Name = categroy.Name;
            oldCategroy.Comment = categroy.Comment;
            oldCategroy.ProductType = categroy.ProductType;
            oldCategroy.IsDiscount = categroy.IsDiscount;
            oldCategroy.SectionType = categroy.SectionType;

            Uow.InventoryCategory.Update(oldCategroy);
            Uow.Commit();
        }
        private void delCategory(DXInfo.Models.InventoryCategory categroy)
        {
            var count = Uow.Inventory.GetAll().Where(w => w.Category == categroy.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("存货分类已使用不能删除");
            var oldInv = Uow.InventoryCategory.GetById(g => g.Id == categroy.Id);
            Uow.InventoryCategory.Delete(oldInv);
            Uow.Commit();
        }
        public ActionResult InventoryCategory_EditData(DXInfo.Models.InventoryCategory category,int DeptType)
        {
            var gridModel = SetUpInventoryCateogryGridModel(category.CategoryType,DeptType);
            return ajaxCallBack<DXInfo.Models.InventoryCategory>(gridModel.InventoryCategoryGrid, category, addCategory, editCategory, delCategory);
        }
        #endregion

        #region 存货档案
        public bool DeleteImg()
        {
            //失效图片删除
            var invs = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.WesternRestaurant && w.IsInvalid).ToList();
            string imgPath = this.Server.MapPath("~/ckfinder/userfiles/images");
            foreach (DXInfo.Models.Inventory inv in invs)
            {
                if (!string.IsNullOrEmpty(inv.ImageFileName))
                {

                    string imgFile = System.IO.Path.Combine(imgPath, inv.ImageFileName);
                    if (System.IO.File.Exists(imgFile))
                    {
                        System.IO.File.Delete(imgFile);
                    }
                }
            }
            //无用图片删除
            var invs2 = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.WesternRestaurant && !w.IsInvalid).ToList();
            List<string> limgs = (from d in invs2 select d.ImageFileName).Distinct().ToList();

            if (System.IO.Directory.Exists(imgPath))
            {
                List<string> limgFiles = System.IO.Directory.GetFiles(imgPath).ToList();
                limgFiles.RemoveAll(delegate(string img) { return limgs.Contains(System.IO.Path.GetFileName(img)); });
                foreach (string img in limgFiles)
                {
                    string imgFile = System.IO.Path.Combine(imgPath, img);
                    if (System.IO.File.Exists(imgFile))
                    {
                        System.IO.File.Delete(imgFile);
                    }
                }
            }
            return true;
        }
        private void SetUpInventoryGrid(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Inventory_RequestData");
            grid.EditUrl = Url.Action("Inventory_EditData");
            SetUpGrid(grid);
            SetBoolColumn(grid, "IsDonate");
            SetBoolColumn(grid, "IsInvalid");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            SetDropDownColumn(grid, "Category", this.GetCategory());
            if (isDisplayImage)
            {
                SetImgColumn(grid, "ImageFileName");
            }
            SetDropDownColumn(grid, "MeasurementUnitGroup", centerCommon.GetMeasurementUnitGroup());

            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetRequiredColumn(grid, "Category");
            SetRequiredColumn(grid, "SalePrice");
            SetRequiredColumn(grid, "SalePoint");
        }
        private void SetUpCDInventoryGrid(JQGrid grid)
        {
            SetGridColumn(grid, "SalePrice0", salePrice0ColumnVisible);
            SetGridColumn(grid, "SalePoint0", salePrice0ColumnVisible);
            SetGridColumn(grid, "SalePrice1", salePrice1ColumnVisible);
            SetGridColumn(grid, "SalePoint1", salePrice1ColumnVisible);
            SetGridColumn(grid, "SalePrice2", salePrice2ColumnVisible);
            SetGridColumn(grid, "SalePoint2", salePrice2ColumnVisible);
            SetDropDownColumn(grid, "UnitOfMeasure", centerCommon.GetUnitOfMeasure((int)DXInfo.Models.UOMType.Retail));
        }
        private void SetUpWRInventoryGrid(JQGrid grid)
        {
            SetBoolColumn(grid, "IsRecommend");
            SetBoolColumn(grid, "IsPackage");
            SetStarColumn(grid, "Stars");
            SetDropDownColumn(grid, "UnitOfMeasure", centerCommon.GetUnitOfMeasure((int)DXInfo.Models.UOMType.Retail));
        }
        private void SetUpSTInventoryGrid(JQGrid grid)
        {
            grid.ClientSideEvents.AfterEditDialogShown = "populateEditUnit";
            grid.ClientSideEvents.AfterAddDialogShown = "populateUnit";
            grid.ExportSettings.ExportUrl = "Inventory_RequestData";


            JQGridColumn column = grid.Columns.Find(c => c.DataField == "MainUnit");
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            SetDropDownColumn(grid, "MainUnit", centerCommon.GetUnitOfMeasure((int)DXInfo.Models.UOMType.StockManage));
            SetDropDownColumn(grid, "StockUnit", centerCommon.GetUnitOfMeasure((int)DXInfo.Models.UOMType.StockManage));
            SetDropDownColumn(grid, "UnitOfMeasure", centerCommon.GetUnitOfMeasure((int)DXInfo.Models.UOMType.StockManage));
            //销售公共列
            column = grid.Columns.Find(c => c.DataField == "ImageFileName");

            if (column != null)
            {
                column.Visible = saleColumnVisibility || ipadColumnVisibility || jewelryColumnVisibility;
                column.Editable = saleColumnVisibility || ipadColumnVisibility || jewelryColumnVisibility;
            }
            column = grid.Columns.Find(c => c.DataField == "IsSale");
            if (column != null)
            {
                column.Visible = saleColumnVisibility || ipadColumnVisibility || jewelryColumnVisibility;
                column.Editable = saleColumnVisibility || ipadColumnVisibility || jewelryColumnVisibility;
            }
            //零售
            SetGridColumn(grid, "UnitOfMeasure", saleColumnVisibility);
            //SetGridColumn(grid, "UnitOfMeasureName", saleColumnVisibility);
            SetGridColumn(grid, "SalePrice", saleColumnVisibility);
            SetGridColumn(grid, "SalePoint", saleColumnVisibility);
            SetGridColumn(grid, "Printer", saleColumnVisibility);
            SetGridColumn(grid, "IsDonate", saleColumnVisibility);
            SetGridColumn(grid, "IsPackage", saleColumnVisibility);
            //ipad列
            SetGridColumn(grid, "IsRecommend", ipadColumnVisibility);
            SetGridColumn(grid, "Sort", ipadColumnVisibility);
            SetGridColumn(grid, "Stars", ipadColumnVisibility);
            if (ipadColumnVisibility)
            {
                SetStarColumn(grid, "Stars");
            }
            SetGridColumn(grid, "Feature", ipadColumnVisibility);
            SetGridColumn(grid, "Dosage", ipadColumnVisibility);
            SetGridColumn(grid, "Palette", ipadColumnVisibility);
            SetGridColumn(grid, "EnglishName", ipadColumnVisibility);
            SetGridColumn(grid, "EnglishIntroduce", ipadColumnVisibility);
            SetGridColumn(grid, "EnglishDosage", ipadColumnVisibility);
            //珠宝
            SetGridColumn(grid, "Karat", jewelryColumnVisibility);
            SetGridColumn(grid, "MetalWeight", jewelryColumnVisibility);
            SetGridColumn(grid, "Jewelweight", jewelryColumnVisibility);
            SetGridColumn(grid, "TotalWeight", jewelryColumnVisibility);
            SetGridColumn(grid, "QTY", jewelryColumnVisibility);
            SetGridColumn(grid, "OrderNo", jewelryColumnVisibility);
            SetGridColumn(grid, "VendorSpec", jewelryColumnVisibility);
            SetGridColumn(grid, "Length", jewelryColumnVisibility);

            SetDateColumn(grid, "LastCheckDate");
            SetBoolColumn(grid, "IsPackage");
            SetBoolColumn(grid, "IsRecommend");
            SetBoolColumn(grid, "IsInvalid");
            SetBoolColumn(grid, "IsSale");

            SetGridColumn(grid, "ShelfLife", isShelfLife);
            SetGridColumn(grid, "ShelfLifeType", isShelfLife);
            SetGridColumn(grid, "ShelfLifeTypeName", isShelfLife);
            if (isShelfLife)
            {
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetEnumTypeDescription("ShelfLifeType"));
            }
            SetDropDownColumn(grid, "CheckCycle", centerCommon.GetCheckCycle());
            //SetStarColumn(grid, "Stars");

            SetRequiredColumn(grid, "MeasurementUnitGroup");
            SetRequiredColumn(grid, "MainUnit");

            int width = 500;
            int step = 250;
            if (saleColumnVisibility) width += step;
            if (ipadColumnVisibility) width += step;
            if (jewelryColumnVisibility) width += step;
            grid.EditDialogSettings.Width = width;
            grid.AddDialogSettings.Width = width;
        }
        private InventoryModels SetUpInventoryGridModel(int InvType)
        {
            InventoryModels im = new InventoryModels();
            switch (InvType)
            {
                case (int)DXInfo.Models.InvType.ColdDrinkShop:
                    im = new CDInventoryGridModel();
                    SetUpInventoryGrid(im.InventoryGrid);
                    SetUpCDInventoryGrid(im.InventoryGrid);
                    break;
                case (int)DXInfo.Models.InvType.WesternRestaurant:
                    im = new WRInventoryGridModel();
                    SetUpInventoryGrid(im.InventoryGrid);
                    SetUpWRInventoryGrid(im.InventoryGrid);
                    break;
                case (int)DXInfo.Models.InvType.StockManage:
                    im = new STInventoryGridModel();
                    SetUpInventoryGrid(im.InventoryGrid);
                    SetUpSTInventoryGrid(im.InventoryGrid);
                    break;
            }
            return im;
        }
        public ActionResult Inventory(int InvType)
        {
            var gridModel = SetUpInventoryGridModel(InvType);
            return PartialView(gridModel);
        }
        private IQueryable GetCDInvData()
        {
            var q = (from inv in Uow.Inventory.GetAll().Where(w => w.InvType == 0)
                     join unit in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                     from iu in invunit.DefaultIfEmpty()
                     join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                     from invcs in invc.DefaultIfEmpty()

                     select new
                     {
                         inv.Id,
                         inv.Code,
                         inv.Name,
                         inv.Category,
                         CategoryName = invcs.Name,
                         inv.Comment,
                         inv.IsDonate,
                         inv.UnitOfMeasure,
                         inv.SalePrice,
                         inv.SalePrice0,
                         inv.SalePrice1,
                         inv.SalePrice2,
                         inv.SalePoint,
                         inv.SalePoint0,
                         inv.SalePoint1,
                         inv.SalePoint2,
                         inv.ImageFileName,
                         inv.Specs,
                         inv.Printer,
                         UnitOfMeasureName = iu.Name,
                         inv.Stars,
                         inv.Feature,
                         inv.Dosage,
                         inv.Palette,
                         inv.EnglishName,
                         inv.IsInvalid,
                         inv.InvType,
                     });
            return q;
        }
        private IQueryable GetWRInvData()
        {
            var q = (from inv in Uow.Inventory.GetAll().Where(w => w.InvType == 1)
                     join unit in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                     from iu in invunit.DefaultIfEmpty()
                     join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                     from invcs in invc.DefaultIfEmpty()

                     select new
                     {
                         inv.Id,
                         inv.Code,
                         inv.Name,
                         inv.Category,
                         CategoryName = invcs.Name,
                         inv.Comment,
                         inv.IsDonate,
                         inv.UnitOfMeasure,
                         inv.SalePrice,
                         inv.SalePrice0,
                         inv.SalePrice1,
                         inv.SalePrice2,
                         inv.SalePoint,
                         inv.SalePoint0,
                         inv.SalePoint1,
                         inv.SalePoint2,
                         inv.ImageFileName,
                         inv.Specs,
                         UnitOfMeasureName = iu.Name,
                         inv.Stars,
                         inv.Feature,
                         inv.Dosage,
                         inv.Palette,
                         inv.EnglishName,
                         inv.IsRecommend,
                         inv.Printer,
                         inv.EnglishIntroduce,
                         inv.EnglishDosage,
                         inv.IsPackage,
                         inv.IsInvalid,
                         inv.Sort,
                         inv.InvType,
                     });
            return q;
        }
        private IQueryable GetSTInvData()
        {
            var q = (from inv in Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage)

                     join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                     from invcs in invc.DefaultIfEmpty()

                     join d1 in Uow.MeasurementUnitGroup.GetAll() on inv.MeasurementUnitGroup equals d1.Id into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     join d2 in Uow.UnitOfMeasures.GetAll() on inv.MainUnit equals d2.Id into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.MeasurementUnitGroupCategory) on inv.UnitCategory equals d3.Value into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     //join d4 in Uow.UnitOfMeasures.GetAll() on inv.PurchaseUnit equals d4.Id into dd4
                     //from dd4s in dd4.DefaultIfEmpty()

                     join d5 in Uow.UnitOfMeasures.GetAll() on inv.StockUnit equals d5.Id into dd5
                     from dd5s in dd5.DefaultIfEmpty()

                     join d6 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.CheckCycle) on inv.CheckCycle equals d6.Value into dd6
                     from dd6s in dd6.DefaultIfEmpty()

                     join d7 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on inv.ShelfLifeType equals d7.Value into dd7
                     from dd7s in dd7.DefaultIfEmpty()

                     join d8 in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals d8.Id into dd8
                     from dd8s in dd8.DefaultIfEmpty()

                     join d9 in Uow.Warehouse.GetAll() on inv.WhId equals d9.Id into dd9
                     from dd9s in dd9.DefaultIfEmpty()

                     join d10 in Uow.Locator.GetAll() on inv.Locator equals d10.Id into dd10
                     from dd10s in dd10.DefaultIfEmpty()

                     select new
                     {
                         inv.Id,
                         inv.Code,
                         inv.Name,
                         inv.EnglishName,
                         inv.Category,
                         CategoryName = invcs.Name,
                         inv.IsRecommend,
                         inv.ImageFileName,
                         inv.Specs,
                         inv.IsDonate,
                         inv.MeasurementUnitGroup,
                         MeasurementUnitGroupName = dd1s.Name,
                         inv.MainUnit,
                         MainUnitName = dd2s.Name,
                         inv.SalePrice,
                         inv.SalePoint,
                         inv.SalePrice0,
                         inv.SalePoint0,
                         inv.SalePrice1,
                         inv.SalePoint1,
                         inv.SalePrice2,
                         inv.SalePoint2,
                         inv.UnitOfMeasure,
                         UnitOfMeasureName = dd8s.Name,
                         inv.StockUnit,
                         StockUnitName = dd5s.Name,
                         inv.HighStock,
                         inv.LowStock,
                         inv.SecurityStock,
                         inv.LastCheckDate,
                         inv.CheckCycle,
                         CheckCycleName = dd6s.Description,
                         inv.SomeDay,
                         inv.ShelfLife,
                         inv.ShelfLifeType,
                         ShelfLifeTypeName = dd7s.Description,
                         inv.EarlyWarningDay,
                         inv.Comment,
                         inv.Stars,
                         inv.Feature,
                         inv.Dosage,
                         inv.Palette,
                         inv.Printer,
                         inv.EnglishIntroduce,
                         inv.EnglishDosage,
                         inv.IsPackage,
                         inv.IsInvalid,
                         inv.IsSale,
                         inv.Sort,
                         inv.WhId,
                         WhName = dd9s == null ? "" : dd9s.Name,
                         inv.Locator,
                         LocatorName = dd10s == null ? "" : dd10s.Name,
                         inv.Karat,
                         inv.MetalWeight,
                         inv.JewelWeight,
                         inv.TotalWeight,
                         inv.QTY,
                         inv.OrderNo,
                         inv.VendorSpec,
                         inv.Length,
                         inv.InvType,
                     });
            return q;
        }
        public ActionResult Inventory_RequestData(int InvType)
        {
            var gridModel = SetUpInventoryGridModel(InvType);
            IQueryable q = null;
            switch (InvType)
            {
                case (int)DXInfo.Models.InvType.ColdDrinkShop:
                    q = GetCDInvData();
                    break;
                case (int)DXInfo.Models.InvType.WesternRestaurant:
                    q = GetWRInvData();
                    break;
                case (int)DXInfo.Models.InvType.StockManage:
                    q = GetSTInvData();
                    break;
            }
            return QueryAndExcel(gridModel.InventoryGrid, q, "存货档案.xls");
        }
        private bool CheckInventoryCodeDup(string code,int invType)
        {
            return Uow.Inventory.GetAll().Where(w => w.Code == code && w.InvType == invType).Count() > 0;
        }
        private bool CheckInventoryNameDup(string name, int invType)
        {
            return Uow.Inventory.GetAll().Where(w => w.Name == name && w.InvType == invType).Count() > 0;
        }
        private void addInv(DXInfo.Models.Inventory inv)
        {
            if (CheckInventoryCodeDup(inv.Code,inv.InvType))
            {
                throw new BusinessException("编码重复");
            }
            if (CheckInventoryNameDup(inv.Name, inv.InvType))
            {
                throw new BusinessException("名称重复");
            }
            inv.IsBatch = isBatch;
            inv.IsShelfLife = isShelfLife;
            inv.IsSale = true;
            Uow.Inventory.Add(inv);
            Uow.Commit();
        }
        private void addSTInv(DXInfo.Models.Inventory inv)
        {
            if (CheckInventoryCodeDup(inv.Code, inv.InvType))
            {
                throw new BusinessException("编码重复");
            }
            if (CheckInventoryNameDup(inv.Name, inv.InvType))
            {
                throw new BusinessException("名称重复");
            }

            inv.IsBatch = isBatch;
            inv.IsShelfLife = isShelfLife;
            //获取自动编码,同步入tbGoods
            //if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
            //{
            //    DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetById(g => g.Id == inv.Category);
            //    if (ic.ProductType == (int)DXInfo.Models.ProductType.Product)
            //    {
            //        CommCenter.CMSMStruct.GoodsStruct gs = new CommCenter.CMSMStruct.GoodsStruct();
            //        gs.strGoodsType = ic.Name;
            //        string strGoodsTypeEN = ic.Code;
            //        BusiComm.Manager m1;
            //        Hashtable htapp = (Hashtable)this.HttpContext.Application["appconf"];
            //        string strcons = (string)htapp["cons"];
            //        m1 = new BusiComm.Manager(strcons);
            //        if (!m1.ChkGoodsNameDup(inv.Name))
            //        {
            //            throw new DXInfo.Models.BusinessException("名称重复");
            //        }
            //        gs.strGoodsName = inv.Name;
            //        gs.dPrice = Convert.ToDouble(inv.SalePrice);
            //        gs.iIgValue = -1;
            //        gs.strComments = "否";
            //        gs.strSpell = AMSApp.zhenghua.Business.Helper.GetChineseSpell(inv.Name);
            //        string strGoodsId = "";
            //        m1.InsertGoods(gs, strGoodsTypeEN, out strGoodsId);
            //        inv.Code = strGoodsId;
            //    }
            //}
            var g2 = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (g2.Category == (int)DXInfo.Models.UnitGroupCategory.No)
            {
                inv.StockUnit = inv.MainUnit;
                inv.UnitOfMeasure = inv.MainUnit;
                inv.PurchaseUnit = inv.MainUnit;
            }
            Uow.Inventory.Add(inv);
            Uow.Commit();
        }
        private void editCDInv(DXInfo.Models.Inventory inv)
        {
            var oldinv = Uow.Inventory.GetById(g => g.Id == inv.Id);
            if (oldinv.Code != inv.Code)
            {
                if (CheckInventoryCodeDup(inv.Code, inv.InvType))
                {
                    throw new BusinessException("编码重复");
                }
            }
            if (oldinv.Name != inv.Name)
            {
                if (CheckInventoryNameDup(inv.Name, inv.InvType))
                {
                    throw new BusinessException("名称重复");
                }
            }
            oldinv.Code = inv.Code;
            oldinv.Name = inv.Name;
            oldinv.Category = inv.Category;
            oldinv.Comment = inv.Comment;
            oldinv.UnitOfMeasure = inv.UnitOfMeasure;
            oldinv.Specs = inv.Specs;
            oldinv.SalePoint = inv.SalePoint;
            oldinv.SalePoint0 = inv.SalePoint0;
            oldinv.SalePoint1 = inv.SalePoint1;
            oldinv.SalePoint2 = inv.SalePoint2;

            oldinv.SalePrice = inv.SalePrice;
            oldinv.SalePrice0 = inv.SalePrice0;
            oldinv.SalePrice1 = inv.SalePrice1;
            oldinv.SalePrice2 = inv.SalePrice2;
            oldinv.IsDonate = inv.IsDonate;
            oldinv.IsInvalid = inv.IsInvalid;
            oldinv.Printer = inv.Printer;
            Uow.Inventory.Update(oldinv);
            Uow.Commit();
        }
        private void editWRInv(DXInfo.Models.Inventory inv)
        {
            var oldinv = Uow.Inventory.GetById(g => g.Id == inv.Id);
            if (oldinv.Code != inv.Code)
            {
                if (CheckInventoryCodeDup(inv.Code, inv.InvType))
                {
                    throw new BusinessException("编码重复");
                }
            }
            if (oldinv.Name != inv.Name)
            {
                if (CheckInventoryNameDup(inv.Name, inv.InvType))
                {
                    throw new BusinessException("名称重复");
                }
            }
            oldinv.Code = inv.Code;
            oldinv.Name = inv.Name;
            oldinv.Category = inv.Category;
            oldinv.Comment = inv.Comment;
            oldinv.UnitOfMeasure = inv.UnitOfMeasure;
            oldinv.Specs = inv.Specs;
            oldinv.ImageFileName = inv.ImageFileName;
            oldinv.SalePoint = inv.SalePoint;
            oldinv.SalePoint0 = inv.SalePoint0;
            oldinv.SalePoint1 = inv.SalePoint1;
            oldinv.SalePoint2 = inv.SalePoint2;

            oldinv.SalePrice = inv.SalePrice;
            oldinv.SalePrice0 = inv.SalePrice0;
            oldinv.SalePrice1 = inv.SalePrice1;
            oldinv.SalePrice2 = inv.SalePrice2;
            oldinv.IsDonate = inv.IsDonate;

            oldinv.Stars = inv.Stars;
            oldinv.Feature = inv.Feature;
            oldinv.Dosage = inv.Dosage;
            oldinv.Palette = inv.Palette;

            oldinv.EnglishName = inv.EnglishName;
            oldinv.IsRecommend = inv.IsRecommend;

            oldinv.EnglishDosage = inv.EnglishDosage;
            oldinv.EnglishIntroduce = inv.EnglishIntroduce;
            oldinv.Printer = inv.Printer;

            oldinv.IsPackage = inv.IsPackage;
            oldinv.IsInvalid = inv.IsInvalid;
            oldinv.Sort = inv.Sort;

            Uow.Inventory.Update(oldinv);
            Uow.Commit();
        }
        private void editSTInv(DXInfo.Models.Inventory inv)
        {
            var oldinv = Uow.Inventory.GetById(g => g.Id == inv.Id);
            if (oldinv.Code != inv.Code)
            {
                if (CheckInventoryCodeDup(inv.Code, inv.InvType))
                {
                    throw new BusinessException("编码重复");
                }
            }
            if (oldinv.Name != inv.Name)
            {
                if (CheckInventoryNameDup(inv.Name, inv.InvType))
                {
                    throw new BusinessException("名称重复");
                }
            }
            //获取自动编码,同步入tbGoods
            //if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
            //{
            //    DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetById(g => g.Id == oldinv.Category);
            //    if (ic.ProductType == (int)DXInfo.Models.ProductType.Product)
            //    {
            //        if (oldinv.Category != inv.Category)
            //        {
            //            throw new DXInfo.Models.BusinessException("成品分类不能修改");
            //        }
            //        if (oldinv.Code != inv.Code)
            //        {
            //            throw new DXInfo.Models.BusinessException("成品编码不能修改");
            //        }
            //        CommCenter.CMSMStruct.GoodsStruct gs = new CommCenter.CMSMStruct.GoodsStruct();
            //        gs.strGoodsID = inv.Code;
            //        gs.strGoodsType = ic.Name;
            //        string strGoodsTypeEN = ic.Code;
            //        BusiComm.Manager m1;
            //        Hashtable htapp = (Hashtable)this.HttpContext.Application["appconf"];
            //        string strcons = (string)htapp["cons"];
            //        m1 = new BusiComm.Manager(strcons);
            //        if (!m1.ChkNewGoodsNameDup(inv.Name, inv.Code))
            //        {
            //            throw new DXInfo.Models.BusinessException("名称重复");
            //        }
            //        gs.strGoodsName = inv.Name;
            //        gs.dPrice = Convert.ToDouble(inv.SalePrice);
            //        gs.iIgValue = -1;
            //        gs.strComments = "否";
            //        gs.strSpell = AMSApp.zhenghua.Business.Helper.GetChineseSpell(inv.Name);
            //        CommCenter.CMSMStruct.GoodsStruct gsold = m1.GetGoodsInfo(inv.Code);
            //        m1.UpdateGoods(gs, gsold);
            //    }
            //}

            oldinv.Code = inv.Code;
            oldinv.Name = inv.Name;
            oldinv.Category = inv.Category;
            oldinv.Comment = inv.Comment;

            oldinv.Specs = inv.Specs;

            oldinv.IsInvalid = inv.IsInvalid;

            var g1 = Uow.MeasurementUnitGroup.GetById(g => g.Id == oldinv.MeasurementUnitGroup);
            if (g1 != null)
            {
                if (g1.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                {
                    oldinv.StockUnit = oldinv.MainUnit;
                    oldinv.UnitOfMeasure = oldinv.MainUnit;
                    oldinv.PurchaseUnit = oldinv.MainUnit;
                }
            }

            var g2 = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
            if (g2 != null)
            {
                if (g2.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                {
                    inv.StockUnit = inv.MainUnit;
                    inv.UnitOfMeasure = inv.MainUnit;
                    inv.PurchaseUnit = inv.MainUnit;
                }
            }

            if (oldinv.MeasurementUnitGroup != inv.MeasurementUnitGroup || oldinv.StockUnit != inv.StockUnit ||
                oldinv.MainUnit != inv.MainUnit)
            {
                var count = Uow.RdRecords.GetAll().Where(w => w.InvId == inv.Id).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("库存单位已使用不能修改");
            }

            oldinv.MeasurementUnitGroup = inv.MeasurementUnitGroup;
            oldinv.MainUnit = inv.MainUnit;
            oldinv.UnitCategory = g2.Category;

            oldinv.StockUnit = inv.StockUnit;
            oldinv.HighStock = inv.HighStock;
            oldinv.LowStock = inv.LowStock;
            oldinv.SecurityStock = inv.SecurityStock;
            oldinv.LastCheckDate = inv.LastCheckDate;
            oldinv.CheckCycle = inv.CheckCycle;
            oldinv.SomeDay = inv.SomeDay;
            oldinv.EarlyWarningDay = inv.EarlyWarningDay;

            oldinv.ShelfLife = inv.ShelfLife;
            oldinv.ShelfLifeType = inv.ShelfLifeType;
            oldinv.IsBatch = isBatch;
            oldinv.IsShelfLife = isShelfLife;
            oldinv.InvType = (int)DXInfo.Models.InvType.StockManage;

            oldinv.UnitOfMeasure = inv.UnitOfMeasure;
            oldinv.SalePrice = inv.SalePrice;
            oldinv.SalePoint = inv.SalePoint;
            oldinv.IsSale = inv.IsSale;

            oldinv.Stars = inv.Stars;
            oldinv.Feature = inv.Feature;
            oldinv.Dosage = inv.Dosage;
            oldinv.Palette = inv.Palette;
            oldinv.Printer = inv.Printer;
            oldinv.EnglishIntroduce = inv.EnglishIntroduce;
            oldinv.EnglishDosage = inv.EnglishDosage;
            oldinv.IsPackage = inv.IsPackage;
            oldinv.IsRecommend = inv.IsRecommend;
            oldinv.ImageFileName = inv.ImageFileName;
            oldinv.IsDonate = inv.IsDonate;
            oldinv.Sort = inv.Sort;
            oldinv.EnglishName = inv.EnglishName;
            oldinv.WhId = inv.WhId;
            oldinv.Locator = inv.Locator;
            oldinv.Karat = inv.Karat;
            oldinv.MetalWeight = inv.MetalWeight;
            oldinv.JewelWeight = inv.JewelWeight;
            oldinv.TotalWeight = inv.TotalWeight;
            oldinv.QTY = inv.QTY;
            oldinv.OrderNo = inv.OrderNo;
            oldinv.VendorSpec = inv.VendorSpec;
            oldinv.Length = inv.Length;
            Uow.Inventory.Update(oldinv);
            Uow.Commit();
        }
        private void delInv(DXInfo.Models.Inventory inv)
        {
            var count = Uow.RdRecords.GetAll().Where(w => w.InvId == inv.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("存货已使用不能删除");
            var oldInv = Uow.Inventory.GetById(g => g.Id == inv.Id);
            Uow.Inventory.Delete(oldInv);
            Uow.Commit();
        }
        public ActionResult Inventory_EditData(DXInfo.Models.Inventory inv)
        {
            var gridModel = SetUpInventoryGridModel(inv.InvType);
            Action<DXInfo.Models.Inventory> etInv = null;
            Action<DXInfo.Models.Inventory> adInv = null;
            switch (inv.InvType)
            {
                case (int)DXInfo.Models.InvType.ColdDrinkShop:
                    adInv = addInv;
                    etInv = editCDInv;
                    break;
                case (int)DXInfo.Models.InvType.WesternRestaurant:
                    adInv = addInv;
                    etInv = editWRInv;
                    break;
                case (int)DXInfo.Models.InvType.StockManage:
                    adInv = addSTInv;
                    etInv = editSTInv;
                    break;
            }
            return ajaxCallBack<DXInfo.Models.Inventory>(gridModel.InventoryGrid, inv, addInv, etInv, delInv);
        }
        public int GetUnitGroupCagegory(Guid group)
        {
            var g1 = Uow.MeasurementUnitGroup.GetById(g => g.Id == group);
            return g1.Category;
        }
        public JsonResult GetUnitJson(Guid? group)
        {
            if (!group.HasValue || group == Guid.Empty)
                return Json("", JsonRequestBehavior.AllowGet);
            var units = Uow.UnitOfMeasures.GetAll().Where(w => w.Group == group).ToList();
            if (units == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(units, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitGroupJson(Guid? group)
        {
            if (!group.HasValue)
                return Json("", JsonRequestBehavior.AllowGet);
            var g1 = Uow.MeasurementUnitGroup.GetById(g => g.Id == group);
            if (g1 == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            return Json(g1.Category, JsonRequestBehavior.AllowGet);
        }
        public ContentResult LoadExcel1(HttpPostedFileBase FileData,
            string tableName,
            string defaultValue)
        {
            string uploadPath = Server.MapPath("/ckfinder/userfiles/excel/");
            string FileName;
            string savePath;
            if (FileData == null)
            {
                return Content("请选择文件");
            }

            if (null != FileData)
            {
                string filename = Path.GetFileName(FileData.FileName);
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    return Content("文件类型不对，只能导入xls和xlsx格式的文件");
                }
                try
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    savePath = Path.Combine(uploadPath, FileName);
                    FileData.SaveAs(savePath);//Path.Combine(uploadPath,FileData.FileName));
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
                DXInfo.Models.ExcelLoad excelLoad = new ExcelLoad();
                string configurationPath = Server.MapPath("/LoadExcel.xml");
                excelLoad.LoadExcel(Uow.Db.Connection.ConnectionString,
                    savePath, defaultValue, configurationPath, tableName);
            }
            return Content("1");
        }
        public ContentResult LoadExcel(HttpPostedFileBase FileData)
        {
            string uploadPath = Server.MapPath("/ckfinder/userfiles/excel/");
            string FileName;
            string savePath;
            if (null != FileData)
            {
                string filename = Path.GetFileName(FileData.FileName);
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串
                FileName = NoFileName + DateTime.Now.ToString("yyyyMMddhhmmss") + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    return Content("文件类型不对，只能导入xls和xlsx格式的文件" );
                }
                try
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    savePath = Path.Combine(uploadPath, FileName);
                    FileData.SaveAs(savePath);//Path.Combine(uploadPath,FileData.FileName));
                }
                catch (Exception ex)
                {
                    return Content(ex.Message );
                }

                string strConn;
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + 
                //    savePath + ";" + "Extended Properties=Excel 8.0";
                strConn = "Provider=Microsoft.Ace.OleDb.12.0;" +
                    "data source=" + savePath +
                    ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [那友存货档案导入模板$]", strConn);
                DataSet myDataSet = new DataSet();
                try
                {
                    myCommand.Fill(myDataSet, "ExcelInfo");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message );
                }
                DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                //判断条码重复
                var groupedData = (from b in table.AsEnumerable()
                                   group b by b.Field<string>("编码") into g
                                   select new
                                   {
                                       Code = g.Key,
                                       Count = g.Count(),
                                   }).ToList().Where(w => w.Count > 1).Select(s => s.Code).ToList();
                if (groupedData.Count > 0)
                {
                    string retstr = groupedData.Aggregate(delegate(string str1, string str2) { return str1 + str2; });
                    return Content("以下编码重复：" + retstr);
                }
                try
                {
                    using (TransactionScope transaction = new TransactionScope())
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            string code = dr["编码"].ToString();
                            if (string.IsNullOrEmpty(code)) continue;
                            //获取地区名称
                            string categoryCode = dr["存货分类"].ToString();
                            string unitName = dr["计量单位"].ToString();
                            DXInfo.Models.InventoryCategory category = Uow.InventoryCategory.GetAll().Where(w => w.Code == categoryCode).FirstOrDefault();

                            //判断地区是否存在
                            if (category == null)
                            {
                                return Content("导入的文件中：" + categoryCode + "存货分类不存在，请先添加该存货分类");
                            }

                            DXInfo.Models.UnitOfMeasures unit = Uow.UnitOfMeasures.GetAll().Where(w => w.Name == unitName).FirstOrDefault();
                            if (unit == null)
                            {
                                return Content("导入的文件中：" + unitName + "计量单位不存在，请先添加该计量单位");
                            }
                            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == unit.Group);
                            
                            string name = dr["名称"].ToString();
                            string specs = dr["规格型号"].ToString();
                            string imgFileName = dr["图片文件名"].ToString();
                            if (string.IsNullOrEmpty(imgFileName))
                            {
                                imgFileName = code + ".jpg";
                            }
                            if (!Regex.IsMatch(imgFileName.ToLower(), DXInfo.Models.MyReg.ImageFileName))
                            {
                                return Content("编号为：" + code + "的【图片文件名】的格式不正确，扩展名必须是jpg|gif|bmp|png。");
                            }
                            string strprice = dr["零售单价"].ToString();
                            if (string.IsNullOrEmpty(strprice)) strprice = "0";
                            if (!Regex.IsMatch(strprice, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为："+code+"的【零售单价】请输入正浮点数");
                            }
                            decimal price = Convert.ToDecimal(strprice);
                            string strKarat = dr["含量"].ToString();
                            if (string.IsNullOrEmpty(strKarat)) strKarat = "0";
                            if (!Regex.IsMatch(strKarat, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为：" + code + "的【含量】请输入正浮点数");
                            }
                            decimal Karat = Convert.ToDecimal(strKarat);
                            string strMetalWeight = dr["金属重量"].ToString();
                            if (string.IsNullOrEmpty(strMetalWeight)) strMetalWeight = "0";
                            if (!Regex.IsMatch(strMetalWeight, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为：" + code + "的【金属重量】请输入正浮点数");
                            }
                            decimal MetalWeight = Convert.ToDecimal(strMetalWeight);
                            string strJewelweight = dr["宝石重量"].ToString();
                            if (string.IsNullOrEmpty(strJewelweight)) strJewelweight = "0";
                            if (!Regex.IsMatch(strJewelweight, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为：" + code + "的【宝石重量】请输入正浮点数");
                            }
                            decimal Jewelweight = Convert.ToDecimal(strJewelweight);
                            string strTotalWeight = dr["总重量"].ToString();
                            if (string.IsNullOrEmpty(strTotalWeight)) strTotalWeight = "0";
                            if (!Regex.IsMatch(strTotalWeight, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为：" + code + "的【总重量】请输入正浮点数");
                            }
                            decimal TotalWeight = Convert.ToDecimal(strTotalWeight);
                            string strQTY = dr["数量"].ToString();
                            if (string.IsNullOrEmpty(strQTY)) strQTY = "0";
                            if (!Regex.IsMatch(strQTY, DXInfo.Models.MyReg.PlusNumber))
                            {
                                return Content("编号为：" + code + "的【数量】请输入正浮点数");
                            }
                            decimal QTY = Convert.ToDecimal(strQTY);
                            string OrderNo = dr["订货号"].ToString();
                            string VendorSpec = dr["供应商型号"].ToString();
                            string Length = dr["长度"].ToString();
                            string strSale = dr["是否零售"].ToString();
                            bool isSale = string.IsNullOrEmpty(strSale) ? false :
                                strSale == "是" ? true : false;
                            List<DXInfo.Models.Inventory> lInv = Uow.Inventory.GetAll().Where(w => w.Code == code).ToList();
                            if (lInv.Count > 1)
                            {
                                //throw new HttpException((int)HttpStatusCode.InternalServerError, ex.Message);

                                return Content("编码为：" + code + "存货档案重复");
                            }
                            DXInfo.Models.Inventory inv = null;
                            if (lInv.Count == 1)
                            {
                                inv = lInv[0];
                            }
                            else
                            {
                                inv = new Inventory();
                                inv.Code = code;
                            }
                            inv.InvType = 2;
                            inv.Name = name;
                            inv.Specs = specs;
                            inv.ImageFileName = imgFileName;
                            inv.SalePrice = price;
                            inv.SalePoint = price;
                            inv.Karat = Karat;
                            inv.MetalWeight = MetalWeight;
                            inv.JewelWeight = Jewelweight;
                            inv.TotalWeight = TotalWeight;
                            inv.QTY = QTY;
                            inv.OrderNo = OrderNo;
                            inv.VendorSpec = VendorSpec;
                            inv.Length = Length;
                            inv.Category = category.Id;
                            inv.MeasurementUnitGroup = group.Id;
                            inv.UnitCategory = group.Category;
                            inv.UnitOfMeasure = unit.Id;
                            inv.PurchaseUnit = unit.Id;
                            inv.StockUnit = unit.Id;
                            inv.MainUnit = unit.Id;
                            inv.IsSale = isSale;
                            if (lInv.Count == 1)
                            {
                                Uow.Inventory.Update(inv);
                            }
                            else
                            {
                                Uow.Inventory.Add(inv);
                            }
                            Uow.Commit();

                        }
                        transaction.Complete();
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.InnerException != null)
                        {
                            return Content(ex.InnerException.InnerException.Message);
                        }
                        return Content(ex.InnerException.Message);
                    }
                    return Content(ex.Message);
                }
            }
            return Content("1");
        }
        #endregion

        #region 计量单位组
        private void SetupMeasurementUnitGroupGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("MeasurementUnitGroup_RequestData");
            grid.EditUrl = Url.Action("MeasurementUnitGroup_EditData");

            this.SetDropDownColumn(grid, "Category", centerCommon.GetUnitGroupCategories());
            this.SetRequiredColumn(grid, "Category");
            this.SetRequiredColumn(grid, "Code");
            this.SetRequiredColumn(grid, "Name");
        }
        public ActionResult MeasurementUnitGroup()
        {
            var gridModel = new MeasurementUnitGroupGridModel();
            SetupMeasurementUnitGroupGridModel(gridModel.MeasurementUnitGroupGrid);
            return PartialView(gridModel);
        }
        public ActionResult MeasurementUnitGroup_RequestData()
        {
            var gridModel = new MeasurementUnitGroupGridModel();
            SetupMeasurementUnitGroupGridModel(gridModel.MeasurementUnitGroupGrid);

            var q = from d in Uow.MeasurementUnitGroup.GetAll()
                        select new { d.Id, d.Code, d.Name, d.Comment, d.Category, CategoryName = d.Category == 0 ? "无换算" : d.Category == 1 ? "固定换算" : d.Category == 2 ? "浮动换算" : "未知换算" };
            return QueryAndExcel(gridModel.MeasurementUnitGroupGrid, q, "计量单位组.xls");
        }
        private bool CheckUnitGroupCodeDup(string code)
        {
            return Uow.MeasurementUnitGroup.GetAll().Where(w => w.Code == code).Count() > 0;
        }
        private bool CheckUnitGroupNameDup(string name)
        {
            return Uow.MeasurementUnitGroup.GetAll().Where(w => w.Name == name).Count() > 0;
        }
        private void addMUG(DXInfo.Models.MeasurementUnitGroup mug)
        {
            if (CheckUnitGroupCodeDup(mug.Code))
            {
                throw new DXInfo.Models.BusinessException("编码重复");
            }
            if (CheckUnitGroupNameDup(mug.Name))
            {
                throw new DXInfo.Models.BusinessException("名称重复");
            }
            Uow.MeasurementUnitGroup.Add(mug);
            Uow.Commit();
        }
        private void editMUG(DXInfo.Models.MeasurementUnitGroup mug)
        {
            var oldunit = Uow.MeasurementUnitGroup.GetById(g => g.Id == mug.Id);
            if (oldunit.Code != mug.Code && CheckUnitGroupCodeDup(mug.Code))
            {
                throw new DXInfo.Models.BusinessException("编码重复");
            }
            if (oldunit.Name != mug.Name && CheckUnitGroupNameDup(mug.Name))
            {
                throw new DXInfo.Models.BusinessException("名称重复");
            }
            oldunit.Code = mug.Code;
            oldunit.Name = mug.Name;
            oldunit.Comment = mug.Comment;
            if (oldunit.Category != mug.Category)
            {
                var count = (from d in Uow.RdRecords.GetAll()
                             join d1 in Uow.Inventory.GetAll() on d.InvId equals d1.Id into dd1
                             from dd1s in dd1.DefaultIfEmpty()
                             where dd1s.MeasurementUnitGroup == oldunit.Id
                             select d).Count();

                if (count > 0)
                    throw new DXInfo.Models.BusinessException("计量单位组已使用不能修改类别");
            }
            oldunit.Category = mug.Category;
            Uow.MeasurementUnitGroup.Update(oldunit);
            Uow.Commit();
        }
        private void delMUG(DXInfo.Models.MeasurementUnitGroup mug)
        {
            var count = Uow.UnitOfMeasures.GetAll().Where(w => w.Group == mug.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("计量单位组已使用不能删除");
            var oldUnit = Uow.MeasurementUnitGroup.GetById(g => g.Id == mug.Id);
            Uow.MeasurementUnitGroup.Delete(oldUnit);
            Uow.Commit();
        }
        public ActionResult MeasurementUnitGroup_EditData(DXInfo.Models.MeasurementUnitGroup mug)
        {
            var gridModel = new MeasurementUnitGroupGridModel();
            SetupMeasurementUnitGroupGridModel(gridModel.MeasurementUnitGroupGrid);
            return ajaxCallBack<DXInfo.Models.MeasurementUnitGroup>(gridModel.MeasurementUnitGroupGrid, mug, addMUG, editMUG, delMUG);
        }
        #endregion

        #region 计量单位
        private void SetupUnitsGridModel(JQGrid grid,int UOMType)
        {
            grid.DataUrl = Url.Action("Unit_RequestData");
            grid.EditUrl = Url.Action("Unit_EditData");
            this.SetDropDownColumn(grid, "Group", centerCommon.GetMeasurementUnitGroup());
            this.SetBoolColumn(grid, "IsMain");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.SerializeRowData = "serializeGridData";
            grid.ClientSideEvents.SerializeDelData = "serializeGridData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            SetUpGrid(grid);
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            if (UOMType == (int)DXInfo.Models.UOMType.StockManage)
            {
                SetRequiredColumn(grid, "Group");
            }

        }
        public ActionResult UnitOfMeasure(int UOMType)
        {
            var gridModel = new UnitOfMeasureGridModel();
            gridModel.UOMType = UOMType;
            SetupUnitsGridModel(gridModel.UnitOfMeasuresGrid,UOMType);
            return PartialView(gridModel);
        }
        public ActionResult Unit_RequestData(int UOMType)
        {
            var gridModel = new UnitOfMeasureGridModel();
            SetupUnitsGridModel(gridModel.UnitOfMeasuresGrid, UOMType);

            var q = from d in Uow.UnitOfMeasures.GetAll()
                    join d1 in Uow.MeasurementUnitGroup.GetAll() on d.Group equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new { d.Id, d.Code, d.Name, d.Group, d.Comment, d.Rate, d.IsMain, GroupName = dd1s.Name, d.UOMType };
            return QueryAndExcel(gridModel.UnitOfMeasuresGrid, q, "计量单位.xls");
        }
        private void ProcessUnit(DXInfo.Models.UnitOfMeasures unit)
        {
            DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == unit.Group);
            if (group != null)
            {
                if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                {
                    unit.IsMain = true;
                    unit.Rate = 0;
                }
                else
                {
                    //保证第一个添加的是主计量单位
                    var groups = Uow.UnitOfMeasures.GetAll().Where(w => w.Group == unit.Group && w.IsMain).Count();
                    if (groups == 0 && !unit.IsMain)
                    {
                        throw new DXInfo.Models.BusinessException("同一个计量单位组只能有一个且必须有一个主计量单位");
                    }
                }
            }
        }
        private bool CheckUnitCodeDup(string code)
        {
            var units = (from d in Uow.UnitOfMeasures.GetAll()
                         where d.Code == code
                         select d).Count();
            return units > 0 ? true : false;
        }
        private bool CheckUnitNameDup(string name)
        {
            var units = (from d in Uow.UnitOfMeasures.GetAll()
                         where d.Name == name
                         select d).Count();
            return units > 0 ? true : false;
        }
        private void addUnit(DXInfo.Models.UnitOfMeasures unit)
        {
            ProcessUnit(unit);
            Uow.UnitOfMeasures.Add(unit);
            Uow.Commit();
        }
        private void editUnit(DXInfo.Models.UnitOfMeasures unit)
        {
            var oldunit = Uow.UnitOfMeasures.GetById(g => g.Id == unit.Id);
            oldunit.Code = unit.Code;
            oldunit.Name = unit.Name;
            if (oldunit.Group != unit.Group || oldunit.IsMain != unit.IsMain)
            {
                var count = Uow.Inventory.GetAll().Where(w => w.StockUnit == oldunit.Id || w.MainUnit == oldunit.Id).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("已使用计量单位不能修改所属计量单位组及其主计量单位标识");
            }
            oldunit.Group = unit.Group;
            oldunit.IsMain = unit.IsMain;

            if (oldunit.Rate != unit.Rate)
            {
                var count = Uow.RdRecords.GetAll().Where(w => w.MainUnit == oldunit.Id || w.STUnit == oldunit.Id).Count();
                if (count > 0)
                    throw new DXInfo.Models.BusinessException("已使用计量单位不能修改换算率");
            }
            oldunit.Rate = unit.Rate;
            oldunit.Comment = unit.Comment;
            oldunit.UOMType = unit.UOMType;
            ProcessUnit(oldunit);
            Uow.UnitOfMeasures.Update(oldunit);
            Uow.Commit();
        }
        private void delUnit(DXInfo.Models.UnitOfMeasures unit)
        {
            var count = Uow.Inventory.GetAll().Where(w => w.MainUnit == unit.Id || w.StockUnit == unit.Id || w.PurchaseUnit == unit.Id ||
                        w.UnitOfMeasure == unit.Id).Count();
            if (count > 0)
                throw new DXInfo.Models.BusinessException("计量单位已使用不能删除");
            var oldUnit = Uow.UnitOfMeasures.GetById(g => g.Id == unit.Id);
            Uow.UnitOfMeasures.Delete(oldUnit);
            Uow.Commit();
        }
        public ActionResult Unit_EditData(DXInfo.Models.UnitOfMeasures unit, int UOMType)
        {
            var gridModel = new UnitOfMeasureGridModel();
            SetupUnitsGridModel(gridModel.UnitOfMeasuresGrid, UOMType);
            return ajaxCallBack<DXInfo.Models.UnitOfMeasures>(gridModel.UnitOfMeasuresGrid, unit, addUnit, editUnit, delUnit);
        }
        #endregion

        #region 存货单价
        private void SetupInvPriceGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("InventoryPrice_RequestData");
            grid.EditUrl = Url.Action("InventoryPrice_EditData");
            SetUpGrid(grid);
            
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetRequiredColumn(grid, "InvId");
            SetRequiredColumn(grid, "SalePrice");
            SetRequiredColumn(grid, "SalePoint");
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsInvalid");
        }
        public ActionResult InvPrice(int InvType)
        {
            var gridModel = new InvPriceGridModel();
            gridModel.InvType = InvType;
            SetupInvPriceGridModel(gridModel.InvPriceGrid);
            return PartialView(gridModel);
        }
        public ActionResult InventoryPrice_RequestData()
        {
            var gridModel = new InvPriceGridModel();
            SetupInvPriceGridModel(gridModel.InvPriceGrid);

            var q = (from inv in Uow.InvPrice.GetAll()
                        join d1 in Uow.Inventory.GetAll() on inv.InvId equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        where !inv.IsInvalid
                        select new
                        {
                            inv.Id,
                            inv.Code,
                            inv.Name,
                            inv.InvId,
                            InvCode = dd1s.Code,
                            InvName = dd1s.Name,
                            inv.SalePrice,
                            inv.SalePoint,
                            inv.IsInvalid,
                            inv.Comment,
                            dd1s.InvType,
                        });
            return QueryAndExcel(gridModel.InvPriceGrid, q,"存货单价.xls");
        }
        private void addInvPrice(DXInfo.Models.InvPrice invPrice)
        {
            Uow.InvPrice.Add(invPrice);
            Uow.Commit();
        }
        private void editInvPrice(DXInfo.Models.InvPrice invPrice)
        {
            var oldinvPrice = Uow.InvPrice.GetById(g => g.Id == invPrice.Id);
            oldinvPrice.Code = invPrice.Code;
            oldinvPrice.Name = invPrice.Name;
            oldinvPrice.InvId = invPrice.InvId;
            oldinvPrice.SalePoint = invPrice.SalePoint;
            oldinvPrice.SalePrice = invPrice.SalePrice;
            oldinvPrice.IsInvalid = invPrice.IsInvalid;
            oldinvPrice.Comment = invPrice.Comment;
            Uow.InvPrice.Update(oldinvPrice);
            Uow.Commit();
        }
        private void delInvPrice(DXInfo.Models.InvPrice invPrice)
        {
            var oldinvPrice = Uow.InvPrice.GetById(g => g.Id == invPrice.Id);
            Uow.InvPrice.Delete(oldinvPrice);
            Uow.Commit();
        }
        public ActionResult InventoryPrice_EditData(DXInfo.Models.InvPrice invPrice)
        {
            var gridModel = new InvPriceGridModel();
            SetupInvPriceGridModel(gridModel.InvPriceGrid);
            return ajaxCallBack<DXInfo.Models.InvPrice>(gridModel.InvPriceGrid,invPrice, addInvPrice, editInvPrice, delInvPrice);
        }
        #endregion

        #region 存货部门关联
        private void SetupInvDeptGridModel(InvDeptGridModel gridModel)
        {
            var InvGrid = gridModel.InvGrid;
            InvGrid.DataUrl = Url.Action("Inv_RequestData");
            InvGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            InvGrid.ClientSideEvents.SubGridRowExpanded = "showDeptSubGrid";
            SetUpGrid(InvGrid);

            SetGridColumn(InvGrid, "SalePrice0", salePrice0ColumnVisible);
            SetGridColumn(InvGrid, "SalePoint0", salePrice0ColumnVisible);
            SetGridColumn(InvGrid, "SalePrice1", salePrice1ColumnVisible);
            SetGridColumn(InvGrid, "SalePoint1", salePrice1ColumnVisible);
            SetGridColumn(InvGrid, "SalePrice2", salePrice2ColumnVisible);
            SetGridColumn(InvGrid, "SalePoint2", salePrice2ColumnVisible);
            SetBoolColumn(InvGrid, "IsDonate");

            var DeptGrid = gridModel.DeptGrid;
            DeptGrid.ClientSideEvents.SerializeGridData = "serializeGridData2";
            DeptGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.SerializeRowData = "serializeRowData";
            DeptGrid.DataUrl = Url.Action("InvDept_RequestData");
            DeptGrid.EditUrl = Url.Action("InvDept_EditData");
            SetBoolColumn(DeptGrid, "IsSelected");

            var Dept2Grid = gridModel.Dept2Grid;
            Dept2Grid.ClientSideEvents.SerializeGridData = "serializeGridData2";
            Dept2Grid.DataUrl = Url.Action("Dept2_RequestData");
        }
        public ActionResult InvDept(int InvType,int DeptType)
        {
            var gridModel = new InvDeptGridModel();
            gridModel.InvType = InvType;
            gridModel.DeptType = DeptType;
            SetupInvDeptGridModel(gridModel);
            return PartialView(gridModel);
        }
        public ActionResult Inv_RequestData()
        {
            var gridModel = new InvDeptGridModel();
            SetupInvDeptGridModel(gridModel);

            var q = (from inv in Uow.Inventory.GetAll()

                        join unit in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                        from iu in invunit.DefaultIfEmpty()

                        join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                        from invcs in invc.DefaultIfEmpty()

                        
                        where !inv.IsInvalid && inv.IsSale
                        
                        select new
                        {
                            inv.Id,
                            inv.Code,
                            inv.Name,
                            CategoryName = invcs.Name,
                            inv.Comment,
                            inv.IsDonate,
                            inv.SalePrice,
                            inv.SalePrice0,
                            inv.SalePrice1,
                            inv.SalePrice2,
                            inv.SalePoint,
                            inv.SalePoint0,
                            inv.SalePoint1,
                            inv.SalePoint2,
                            inv.Specs,
                            UnitName = iu.Name,
                            inv.InvType,
                        });
            return gridModel.InvGrid.DataBind(q);
        }

        public ActionResult InvDept_RequestData(Guid ParentRowId)
        {
            var gridModel = new InvDeptGridModel();
            SetupInvDeptGridModel(gridModel);

            var q = from d in Uow.Depts.GetAll()
                    join i in Uow.InvDepts.GetAll().Where(w => w.Inv == ParentRowId) on d.DeptId equals i.Dept into di
                    from dis in di.DefaultIfEmpty()
                    select new
                    {
                        d.DeptId,
                        d.DeptName,
                        d.DeptCode,
                        d.Address,
                        d.Comment,
                        IsSelected = dis == null ? false : true,
                        d.DeptType,
                    };
            return gridModel.DeptGrid.DataBind(q);
        }
        public ActionResult InvDept_EditData(Guid ParentRowId, 
            Guid DeptId, 
            bool IsSelected,
            int InvType,
            int DeptType)
        {
            var deptModel = new InvDeptGridModel();
            SetupInvDeptGridModel(deptModel);


            if (deptModel.DeptGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var q = from d in Uow.InvDepts.GetAll()
                        where d.Dept == DeptId && d.Inv == ParentRowId
                        select d;
                if (q.Count() == 0)
                {
                    if (IsSelected)
                    {
                        DXInfo.Models.InvDepts invDept = new DXInfo.Models.InvDepts();
                        invDept.Inv = ParentRowId;
                        invDept.Dept = DeptId;
                        Uow.InvDepts.Add(invDept);
                    }
                }
                else
                {
                    var q1 = q.FirstOrDefault();
                    if (q1 != null)
                    {
                        if (!IsSelected)
                        {
                            Uow.InvDepts.Delete(q1);
                        }
                    }
                }
                Uow.Commit();
            }
            //System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
            //routeValues.Add("InvType", InvType);
            //routeValues.Add("DeptType", DeptType);
            //return RedirectToAction("InvDept",routeValues);
            return new EmptyResult();
        }
        public ActionResult Dept2_RequestData()
        {
            var gridModel = new InvDeptGridModel();
            SetupInvDeptGridModel(gridModel);

            var q = from d in Uow.Depts.GetAll()
                    select d;
            return gridModel.Dept2Grid.DataBind(q);
        }
        [HttpPost]
        public ActionResult InvDept2_EditData(string invs, string depts)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            List<Guid> lginvs = new List<Guid>();
            List<Guid> lgdepts = new List<Guid>();

            if (string.IsNullOrEmpty(invs))
            {
                json.Data = "请选择商品";
                return json;
            }
            foreach (string c in invs.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lginvs.Add(Guid.Parse(c));
                }
            }
            if (lginvs.Count == 0)
            {
                json.Data = "请选择商品";
                return json;
            }
            if (string.IsNullOrEmpty(depts))
            {
                json.Data = "请选择门店";
                return json;
            }
            foreach (string c in depts.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lgdepts.Add(Guid.Parse(c));
                }
            }
            if (lgdepts.Count == 0)
            {
                json.Data = "请选择门店";
                return json;
            }
            foreach (Guid id in lginvs)
            {
                var q1 = (from d in Uow.InvDepts.GetAll() where d.Inv == id select d).ToList();

                foreach (Guid id2 in lgdepts)
                {
                    var q2 = (from d in q1 where d.Dept == id2 select d).ToList();
                    if (q2.Count == 0)
                    {
                        DXInfo.Models.InvDepts invDept = new DXInfo.Models.InvDepts();
                        invDept.Dept = id2;
                        invDept.Inv = id;
                        Uow.InvDepts.Add(invDept);
                    }
                    if (q2.Count > 1)
                    {
                        for (int i = 1; i < q2.Count; i++)
                        {
                            DXInfo.Models.InvDepts moreInvDept = q2[i];
                            Uow.InvDepts.Delete(moreInvDept);
                        }
                    }
                }
                var q3 = from d in q1 where !lgdepts.Contains(d.Dept) select d;

                foreach (var q4 in q3)
                {
                    Uow.InvDepts.Delete(q4);
                }
            }
            Uow.Commit();

            json.Data = "设置成功";
            return json;
        }
        #endregion

        #region 存货部门单价
        private void SetupInvDeptPriceGridModel(InvDeptPriceGridModel gridModel)
        {
            var invGrid = gridModel.InvGrid;
            invGrid.DataUrl = Url.Action("InvPrice_RequestData");
            invGrid.EditUrl = Url.Action("InvPrice_EditData");

            JQGridColumn col = invGrid.Columns.Find(f => f.DataField == "Code");
            col.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };

            JQGridColumn col1 = invGrid.Columns.Find(f => f.DataField == "Name");
            col1.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };

            this.SetBoolColumn(invGrid, "IsDonate");
            SetGridColumn(invGrid, "SalePrice0", salePrice0ColumnVisible);
            SetGridColumn(invGrid, "SalePoint0", salePrice0ColumnVisible);

            SetGridColumn(invGrid, "SalePrice1", salePrice1ColumnVisible);
            SetGridColumn(invGrid, "SalePoint1", salePrice1ColumnVisible);

            SetGridColumn(invGrid, "SalePrice2", salePrice2ColumnVisible);
            SetGridColumn(invGrid, "SalePoint2", salePrice2ColumnVisible);
            SetRequiredColumn(invGrid, "SalePrice");
            SetRequiredColumn(invGrid, "SalePoint");

            SetRequiredColumn(invGrid, "SalePrice0");
            SetRequiredColumn(invGrid, "SalePoint0");
            SetRequiredColumn(invGrid, "SalePrice1");
            SetRequiredColumn(invGrid, "SalePoint1");
            SetRequiredColumn(invGrid, "SalePrice2");
            SetRequiredColumn(invGrid, "SalePoint2");
            invGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            invGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            invGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            invGrid.ClientSideEvents.SerializeRowData = "serializeRowData";

            var deptGrid = gridModel.DeptGrid;
            deptGrid.ClientSideEvents.SubGridRowExpanded = "showInvSubGrid";
            deptGrid.ClientSideEvents.SerializeGridData = "serializeGridData2";            
            deptGrid.DataUrl = Url.Action("DeptPrice_RequestData");
            SetUpGrid(deptGrid);
        }
        public ActionResult InvDeptPrice(int InvType,int DeptType)
        {
            var gridModel = new InvDeptPriceGridModel();
            gridModel.InvType = InvType;
            gridModel.DeptType = DeptType;
            SetupInvDeptPriceGridModel(gridModel);
            return PartialView(gridModel);
        }
        public ActionResult DeptPrice_RequestData()
        {
            var gridModel = new InvDeptPriceGridModel();
            SetupInvDeptPriceGridModel(gridModel);
            var q = from d in Uow.Depts.GetAll().Where(w=>w.IsDeptPrice)
                    orderby d.DeptCode
                    select new
                    {
                        d.DeptId,
                        d.DeptName,
                        d.DeptCode,
                        d.Address,
                        d.Comment,
                        d.DeptType,
                    };
            return gridModel.DeptGrid.DataBind(q);
        }
        public ActionResult InvPrice_RequestData(Guid ParentRowId)
        {
            var gridModel = new InvDeptPriceGridModel();
            SetupInvDeptPriceGridModel(gridModel);

            var invs = (
                from d in Uow.InvDepts.GetAll().Where(w => w.Dept == ParentRowId)

                join inv in Uow.Inventory.GetAll() on d.Inv equals inv.Id into dinv
                from dinvs in dinv.DefaultIfEmpty()

                join p in Uow.InventoryDeptPrice.GetAll().Where(w => w.DeptId == ParentRowId) on dinvs.Id equals p.InvId into pinv
                from pinvs in pinv.DefaultIfEmpty()

                join unit in Uow.UnitOfMeasures.GetAll() on dinvs.UnitOfMeasure equals unit.Id into invunit
                from iu in invunit.DefaultIfEmpty()

                join c in Uow.InventoryCategory.GetAll() on dinvs.Category equals c.Id into invc
                from invcs in invc.DefaultIfEmpty()
                where dinvs.IsSale && !dinvs.IsInvalid
                orderby dinvs.Code
                select new
                {
                    InvId = d.Inv,
                    DeptId = ParentRowId,
                    dinvs.Code,
                    dinvs.Name,
                    CategoryName = invcs.Name,
                    dinvs.Comment,
                    IsDonate = dinvs == null ? false : dinvs.IsDonate,
                    SalePrice = pinvs == null ? 0 : pinvs.SalePrice,
                    SalePrice0 = pinvs == null ? 0 : pinvs.SalePrice0,
                    SalePrice1 = pinvs == null ? 0 : pinvs.SalePrice1,
                    SalePrice2 = pinvs == null ? 0 : pinvs.SalePrice2,
                    SalePoint = pinvs == null ? 0 : pinvs.SalePoint,
                    SalePoint0 = pinvs == null ? 0 : pinvs.SalePoint0,
                    SalePoint1 = pinvs == null ? 0 : pinvs.SalePoint1,
                    SalePoint2 = pinvs == null ? 0 : pinvs.SalePoint2,
                    dinvs.Specs,
                    UnitName = iu.Name,
                    dinvs.InvType,
                });
            return gridModel.InvGrid.DataBind(invs);
        }
        public ActionResult InvPrice_EditData(Guid ParentRowId, 
            DXInfo.Models.InventoryDeptPrice deptPrice,
            int InvType,int DeptType)
        {
            var deptModel = new InvDeptPriceGridModel();
            SetupInvDeptPriceGridModel(deptModel);


            if (deptModel.DeptGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldDeptPrice = Uow.InventoryDeptPrice.GetById(g=>g.DeptId==ParentRowId&&g.InvId==deptPrice.InvId);
                if (oldDeptPrice != null)
                {
                    oldDeptPrice.SalePoint = deptPrice.SalePoint;
                    oldDeptPrice.SalePoint0 = deptPrice.SalePoint0;
                    oldDeptPrice.SalePoint1 = deptPrice.SalePoint1;
                    oldDeptPrice.SalePoint2 = deptPrice.SalePoint2;
                    oldDeptPrice.SalePrice = deptPrice.SalePrice;
                    oldDeptPrice.SalePrice0 = deptPrice.SalePrice0;
                    oldDeptPrice.SalePrice1 = deptPrice.SalePrice1;
                    oldDeptPrice.SalePrice2 = deptPrice.SalePrice2;
                    Uow.InventoryDeptPrice.Update(oldDeptPrice);
                }
                else
                {
                    DXInfo.Models.InventoryDeptPrice newDeptPrice = new DXInfo.Models.InventoryDeptPrice();
                    newDeptPrice.DeptId = ParentRowId;
                    newDeptPrice.InvId = deptPrice.InvId;
                    newDeptPrice.SalePoint = deptPrice.SalePoint;
                    newDeptPrice.SalePoint0 = deptPrice.SalePoint0;
                    newDeptPrice.SalePoint1 = deptPrice.SalePoint1;
                    newDeptPrice.SalePoint2 = deptPrice.SalePoint2;
                    newDeptPrice.SalePrice = deptPrice.SalePrice;
                    newDeptPrice.SalePrice0 = deptPrice.SalePrice0;
                    newDeptPrice.SalePrice1 = deptPrice.SalePrice1;
                    newDeptPrice.SalePrice2 = deptPrice.SalePrice2;
                    Uow.InventoryDeptPrice.Add(newDeptPrice);
                }
                Uow.Commit();
            }
            //System.Web.Routing.RouteValueDictionary routeValue = new System.Web.Routing.RouteValueDictionary();
            //routeValue.Add("InvType",InvType);
            //routeValue.Add("DeptType",DeptType);
            //return RedirectToAction("InvDeptPrice",routeValue);
            return new EmptyResult();
        }
        #endregion

        #region 分类部门关联
        private void SetupCategoryDeptGridModel(CategoryDeptGridModel gridModel)
        {
            var CategoryGrid = gridModel.CategoryGrid;
            CategoryGrid.DataUrl = Url.Action("Category_RequestData");
            CategoryGrid.ClientSideEvents.SubGridRowExpanded = "showDeptSubGrid";
            CategoryGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetUpGrid(CategoryGrid);

            var DeptGrid = gridModel.DeptGrid;
            DeptGrid.ClientSideEvents.SerializeGridData = "serializeGridData2";
            DeptGrid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            DeptGrid.ClientSideEvents.SerializeRowData = "serializeRowData";
            SetBoolColumn(DeptGrid, "IsSelected");
            DeptGrid.DataUrl = Url.Action("CategoryDept_RequestData");
            DeptGrid.EditUrl = Url.Action("CategoryDept_EditData");

            var Dept2Grid = gridModel.Dept2Grid;
            Dept2Grid.ClientSideEvents.SerializeGridData = "serializeGridData2";           
            Dept2Grid.DataUrl = Url.Action("CategoryDept2_RequestData");
        }
        public ActionResult CategoryDept(int CategoryType, int DeptType)
        {
            var gridModel = new CategoryDeptGridModel();
            gridModel.CategoryType = CategoryType;
            gridModel.DeptType = DeptType;
            SetupCategoryDeptGridModel(gridModel);
            return PartialView(gridModel);
        }
        public ActionResult Category_RequestData()
        {
            var gridModel = new CategoryDeptGridModel();
            SetupCategoryDeptGridModel(gridModel);
            var categories = Uow.InventoryCategory.GetAll();
            return gridModel.CategoryGrid.DataBind(categories);
        }
        public ActionResult CategoryDept_RequestData(Guid ParentRowId)
        {
            var gridModel = new CategoryDeptGridModel();
            SetupCategoryDeptGridModel(gridModel);

            var q = from d in Uow.Depts.GetAll()
                    join i in Uow.CategoryDepts.GetAll().Where(w => w.Category == ParentRowId) on d.DeptId equals i.Dept into di
                    from dis in di.DefaultIfEmpty()
                    select new
                    {
                        d.DeptId,
                        d.DeptName,
                        d.DeptCode,
                        d.Address,
                        d.Comment,
                        IsSelected = dis == null ? false : true,
                        d.DeptType,
                    };
            return gridModel.DeptGrid.DataBind(q);
        }
        public ActionResult CategoryDept_EditData(Guid ParentRowId, Guid DeptId,
            bool IsSelected,
            int CategoryType,
            int DeptType)
        {
            var deptModel = new CategoryDeptGridModel();
            SetupCategoryDeptGridModel(deptModel);


            if (deptModel.DeptGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var q = from d in Uow.CategoryDepts.GetAll()
                        where d.Dept == DeptId && d.Category == ParentRowId
                        select d;
                if (q.Count() == 0)
                {
                    if (IsSelected)
                    {
                        DXInfo.Models.CategoryDepts categoryDept = new DXInfo.Models.CategoryDepts();
                        categoryDept.Category = ParentRowId;
                        categoryDept.Dept = DeptId;
                        Uow.CategoryDepts.Add(categoryDept);
                    }
                }
                else
                {
                    var q1 = q.FirstOrDefault();
                    if (q1 != null)
                    {
                        if (!IsSelected)
                        {
                            Uow.CategoryDepts.Delete(q1);
                        }
                    }
                }
                Uow.Commit();
            }
            //System.Web.Routing.RouteValueDictionary routeValues = new System.Web.Routing.RouteValueDictionary();
            //routeValues.Add("CategoryType", CategoryType);
            //routeValues.Add("DeptType", DeptType);
            //return RedirectToAction("CategoryDept", routeValues);
            return new EmptyResult();
        }
        public ActionResult CategoryDept2_RequestData()
        {
            var gridModel = new CategoryDeptGridModel();
            SetupCategoryDeptGridModel(gridModel);

            var q = from d in Uow.Depts.GetAll()
                    select d;
            return gridModel.Dept2Grid.DataBind(q);
        }
        [HttpPost]
        public ActionResult CategoryDept2_EditData(string categories, string depts)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            List<Guid> lginvs = new List<Guid>();
            List<Guid> lgdepts = new List<Guid>();

            if (string.IsNullOrEmpty(categories))
            {
                json.Data = "请选择商品";
                return json;
            }
            foreach (string c in categories.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lginvs.Add(Guid.Parse(c));
                }
            }
            if (lginvs.Count == 0)
            {
                json.Data = "请选择商品";
                return json;
            }
            if (string.IsNullOrEmpty(depts))
            {
                json.Data = "请选择门店";
                return json;
            }
            foreach (string c in depts.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lgdepts.Add(Guid.Parse(c));
                }
            }
            if (lgdepts.Count == 0)
            {
                json.Data = "请选择门店";
                return json;
            }
            foreach (Guid id in lginvs)
            {
                var q1 = from d in Uow.CategoryDepts.GetAll() where d.Category == id select d;

                foreach (Guid id2 in lgdepts)
                {
                    var q2 = from d in q1 where d.Dept == id2 select d;

                    if (q2.Count() == 0)
                    {
                        DXInfo.Models.CategoryDepts categoriesDept = new DXInfo.Models.CategoryDepts();
                        categoriesDept.Dept = id2;
                        categoriesDept.Category = id;
                        Uow.CategoryDepts.Add(categoriesDept);
                    }
                }
                var q3 = from d in q1 where !lgdepts.Contains(d.Dept) select d;

                foreach (var q4 in q3)
                {
                    Uow.CategoryDepts.Delete(q4);
                }
            }
            Uow.Commit();

            json.Data = "设置成功";
            return json;
        }
        #endregion

        #region 套餐
        private void SetupPackageGridModel(PackageGridModel gridModel)
        {
            var PackageGrid = gridModel.PackageGrid;
            PackageGrid.ClientSideEvents.SubGridRowExpanded = "showSubGrid";
            PackageGrid.ClientSideEvents.SerializeGridData = "serializeGridData";
            PackageGrid.DataUrl = Url.Action("Package_RequestData");
            SetUpGrid(PackageGrid);

            var InvGrid = gridModel.InvGrid;

            
            InvGrid.DataUrl = Url.Action("PackageInv_RequestData");
            InvGrid.EditUrl = Url.Action("PackageInv_EditData");

            
            SetDropDownColumn(InvGrid, "InventoryId", this.GetInventoryExceptPackage());
            SetRequiredColumn(InvGrid, "InventoryId");
            SetRequiredColumn(InvGrid, "Price");
            SetBoolColumn(InvGrid, "IsOptional");
        }
        public ActionResult Package(int InvType)
        {
            var gridModel = new PackageGridModel();
            gridModel.InvType = InvType;
            SetupPackageGridModel(gridModel);
            return PartialView(gridModel);
        }
        public ActionResult Package_RequestData()
        {
            var gridModel = new PackageGridModel();
            SetupPackageGridModel(gridModel);
            var q = from d in Uow.Inventory.GetAll()
                           join d1 in Uow.InventoryCategory.GetAll() on d.Category equals d1.Id into dd1
                           from dd1s in dd1.DefaultIfEmpty()

                           join d2 in Uow.UnitOfMeasures.GetAll() on d.UnitOfMeasure equals d2.Id into dd2
                           from dd2s in dd2.DefaultIfEmpty()

                           where d.IsPackage
                           select new
                           {
                               d.Id,
                               d.Code,
                               d.Name,
                               CategoryName = dd1s.Name,
                               UnitName = dd2s.Name,
                               d.SalePrice,
                               d.SalePoint,
                               d.Specs,
                               d.InvType
                           };
            return gridModel.PackageGrid.DataBind(q);
        }
        public ActionResult PackageInv_RequestData(Guid ParentRowId)
        {
            var gridModel = new PackageGridModel();
            SetupPackageGridModel(gridModel);

            var q = from d in Uow.Packages.GetAll()
                    join d1 in Uow.Inventory.GetAll() on d.InventoryId equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    where d.PackageId == ParentRowId
                    select new
                    {
                        d.Id,
                        d.InventoryId,
                        dd1s.Code,
                        dd1s.Name,
                        d.Price,
                        d.IsOptional,
                        d.OptionalGroup,
                        d.Comment,
                        d.Quantity
                    };
            return gridModel.InvGrid.DataBind(q);
        }
        private void addPackage(DXInfo.Models.Packages package)
        {
            if (Uow.Packages.GetAll().Where(w => w.PackageId == package.PackageId && w.InventoryId == package.InventoryId).Count() > 0)
            {
                throw new DXInfo.Models.BusinessException("存货不能重复");
            }
            var oldinv = Uow.InvDepts.GetAll().Where(w => w.Inv == package.PackageId).Select(s => s.Dept).Distinct().ToList();
            var oldinv2 = Uow.InvDepts.GetAll().Where(w => w.Inv == package.InventoryId).Select(s => s.Dept).Distinct().ToList();
            foreach (Guid deptid in oldinv)
            {
                if (!oldinv2.Contains(deptid)) throw new DXInfo.Models.BusinessException("商品门店不一致");
            }
            Uow.Packages.Add(package);
            Uow.Commit();
        }
        private void editPackage(DXInfo.Models.Packages package)
        {
            DXInfo.Models.Packages oldPackage = Uow.Packages.GetById(g => g.Id == package.Id);
            oldPackage.InventoryId = package.InventoryId;
            oldPackage.Price = package.Price;
            oldPackage.IsOptional = package.IsOptional;
            oldPackage.OptionalGroup = package.OptionalGroup;
            oldPackage.Comment = package.Comment;
            oldPackage.Quantity = package.Quantity;
            Uow.Packages.Update(oldPackage);
            Uow.Commit();
        }
        private void delPackage(DXInfo.Models.Packages package)
        {
            DXInfo.Models.Packages oldPackage = Uow.Packages.GetById(g => g.Id == package.Id);
            Uow.Packages.Delete(oldPackage);
            Uow.Commit();
        }

        public ActionResult PackageInv_EditData(Guid ParentRowId, DXInfo.Models.Packages package)
        {
            var gridModel = new PackageGridModel();
            SetupPackageGridModel(gridModel);
            if (package.PackageId == Guid.Empty)
            {
                package.PackageId = ParentRowId;
            }
            return ajaxCallBack<DXInfo.Models.Packages>(gridModel.InvGrid,package, addPackage, editPackage, delPackage);
        }

        #endregion

        #region ekey
        private void SetupkeyMangeGrid(JQGrid grid)
        {
            grid.DataUrl = Url.Action("KeyManage_RequestData");
            grid.EditUrl = Url.Action("KeyManage_EditData");

            SetUpGrid(grid);
            this.SetDropDownColumn(grid, "UserId", this.GetOper());
            this.SetDateColumn(grid, "CreateDate");
            this.SetBoolColumn(grid, "IsUse");
            grid.ToolBarSettings.ShowAddButton = false;
            grid.ToolBarSettings.ShowDeleteButton = false;
        }
        public ActionResult KeyManage()
        {
            var gridModel = new KeyManageGridModel();
            SetupkeyMangeGrid(gridModel.KeyManageGrid);
            return PartialView(gridModel);
        }
        public ActionResult KeyManage_RequestData()
        {
            var gridModel = new KeyManageGridModel();
            SetupkeyMangeGrid(gridModel.KeyManageGrid);
            var q = (from e in Uow.ekey.GetAll()
                        join u in Uow.aspnet_CustomProfile.GetAll() on e.UserId equals u.UserId into eu
                        from eus in eu.DefaultIfEmpty()
                        select new { e.HardwareID, e.CardNo, e.CreateDate, e.IsUse,e.UserId,eus.FullName });
            return QueryAndExcel(gridModel.KeyManageGrid, q, "ekey.xls");
        }
        public ActionResult KeyManage_EditData(DXInfo.Models.ekey key)
        {
            var gridModel = new KeyManageGridModel();
            SetupkeyMangeGrid(gridModel.KeyManageGrid);
            if (gridModel.KeyManageGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                var oldkey = Uow.ekey.GetById(g => g.HardwareID == key.HardwareID);
                if (oldkey != null)
                {
                    oldkey.IsUse = key.IsUse;
                    oldkey.UserId = key.UserId;
                    Uow.ekey.Update(oldkey);
                    Uow.Commit();
                }
            }
            return new EmptyResult();
        }
        #endregion

        #region 文件管理
        [Authorize]
        public ActionResult ckfinder()
        {
            return PartialView();
        }
        #endregion

        #region 文件上传
        public ActionResult fileUpload()
        {
            return PartialView();
        }
        public ContentResult UploadHandler(HttpPostedFileBase FileData)
        {
            string uploadPath = Server.MapPath("/ckfinder/userfiles/images/");
            if (null != FileData)
            {
                try
                {
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    FileData.SaveAs(uploadPath + FileData.FileName);
                }
                catch
                {
                    return Content("0");
                }
            }
            return Content("1");
        }
        #endregion

        #region 充值赠送
        private void SetupRechargeDonationGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("RechargeDonation_RequestData");
            grid.EditUrl = Url.Action("RechargeDonation_EditData");
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetUpGrid(grid);
            SetRequiredColumn(grid, "BeginAmount");
            SetRequiredColumn(grid, "DonationTopLimit");
        }
        public ActionResult RechargeDonation()
        {
            var gridModel = new RechargeDonationGridModel();
            SetupRechargeDonationGridModel(gridModel.RechargeDonationGrid);
            return PartialView(gridModel);
        }
        public ActionResult RechargeDonation_RequestData()
        {
            var gridModel = new RechargeDonationGridModel();
            SetupRechargeDonationGridModel(gridModel.RechargeDonationGrid);

            var q = from r in Uow.RechargeDonations.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()
                      select new { r.Id, r.DeptId, r.BeginAmount, r.Comment, r.DonationRatio, r.DonationTopLimit, rds.DeptName };
            return QueryAndExcel(gridModel.RechargeDonationGrid, q, "充值赠送.xls");
        }
        private void addRechargeDonation(DXInfo.Models.RechargeDonations rechargeDonation)
        {
            Uow.RechargeDonations.Add(rechargeDonation);
            Uow.Commit();
        }
        private void editRechargeDonation(DXInfo.Models.RechargeDonations rechargeDonation)
        {
            var oldrdn = Uow.RechargeDonations.GetById(g => g.Id == rechargeDonation.Id);
            oldrdn.BeginAmount = rechargeDonation.BeginAmount;
            oldrdn.Comment = rechargeDonation.Comment;
            oldrdn.DeptId = rechargeDonation.DeptId;
            oldrdn.DonationRatio = rechargeDonation.DonationRatio;
            oldrdn.DonationTopLimit = rechargeDonation.DonationTopLimit;
            Uow.RechargeDonations.Update(oldrdn);
            Uow.Commit();
        }
        private void delRechargeDonation(DXInfo.Models.RechargeDonations rechargeDonation)
        {
            var oldrdn = Uow.RechargeDonations.GetById(g => g.Id == rechargeDonation.Id);
            if (oldrdn != null)
            {
                Uow.RechargeDonations.Delete(oldrdn);
                Uow.Commit();
            }
        }
        public ActionResult RechargeDonation_EditData(DXInfo.Models.RechargeDonations rdn)
        {
            var gridModel = new RechargeDonationGridModel();
            SetupRechargeDonationGridModel(gridModel.RechargeDonationGrid);
            return ajaxCallBack<DXInfo.Models.RechargeDonations>(gridModel.RechargeDonationGrid, rdn, addRechargeDonation, editRechargeDonation, delRechargeDonation);
        }
        #endregion

        #region 卡级别设置
        private void SetupCardLevelGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("CardLevel_RequestData");
            grid.EditUrl = Url.Action("CardLevel_EditData");
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetUpGrid(grid);
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetBoolColumn(grid, "IsDefault");

            SetGridColumn(grid, "Point", isCardLevelAuto);
            SetGridColumn(grid, "IsDefault", isCardLevelAuto);
        }
        public ActionResult CardLevel()
        {
            var gridModel = new CardLevelGridModel();
            SetupCardLevelGridModel(gridModel.CardLevelGrid);
            return PartialView(gridModel);
        }
        public ActionResult CardLevel_RequestData()
        {
            var gridModel = new CardLevelGridModel();
            SetupCardLevelGridModel(gridModel.CardLevelGrid);

            var q = from r in Uow.CardLevels.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()
                      select new { r.Id, r.DeptId, r.Code, r.Name, r.Comment, r.BeginLetter, r.Discount,r.Point, rds.DeptName,r.IsDefault };
            return QueryAndExcel(gridModel.CardLevelGrid, q, "卡级别.xls");
        }
        private void addCardLevel(DXInfo.Models.CardLevels cardLevel)
        {
            Uow.CardLevels.Add(cardLevel);
            Uow.Commit();
        }
        private void editCardLevel(DXInfo.Models.CardLevels cardLevel)
        {
            var oldCardLevel = Uow.CardLevels.GetById(g => g.Id == cardLevel.Id);
            oldCardLevel.Comment = cardLevel.Comment;
            oldCardLevel.DeptId = cardLevel.DeptId;
            oldCardLevel.Code = cardLevel.Code;
            oldCardLevel.Name = cardLevel.Name;
            oldCardLevel.Discount = cardLevel.Discount;
            oldCardLevel.BeginLetter = cardLevel.BeginLetter;
            oldCardLevel.Point = cardLevel.Point;
            oldCardLevel.IsDefault = cardLevel.IsDefault;
            Uow.CardLevels.Update(oldCardLevel);
            Uow.Commit();
        }
        private void delCardLevel(DXInfo.Models.CardLevels cardLevel)
        {
            var oldCardLevel = Uow.CardLevels.GetById(g => g.Id == cardLevel.Id);
            if (oldCardLevel != null)
            {
                Uow.CardLevels.Delete(oldCardLevel);
                Uow.Commit();
            }
        }
        public ActionResult CardLevel_EditData(DXInfo.Models.CardLevels cardLevel)
        {
            var gridModel = new CardLevelGridModel();
            SetupCardLevelGridModel(gridModel.CardLevelGrid);
            return ajaxCallBack<DXInfo.Models.CardLevels>(gridModel.CardLevelGrid, cardLevel, addCardLevel, editCardLevel, delCardLevel);
        }
        #endregion

        #region 消费积分设置
        private void SetupConsumePointGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ConsumePoint_RequestData");
            grid.EditUrl = Url.Action("ConsumePoint_EditData");
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetDropDownColumn(grid, "Category", this.GetCategory());
            SetUpGrid(grid);
            SetRequiredColumn(grid, "Amount");
            SetRequiredColumn(grid, "Point");
        }
        public ActionResult ConsumePoint()
        {
            var gridModel = new ConsumePointGridModel();
            SetupConsumePointGridModel(gridModel.ConsumePointGrid);
            return PartialView(gridModel);
        }
        public ActionResult ConsumePoint_RequestData()
        {
            var gridModel = new ConsumePointGridModel();
            SetupConsumePointGridModel(gridModel.ConsumePointGrid);

            var q = from r in Uow.ConsumePoints.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()
                      join c in Uow.InventoryCategory.GetAll() on r.Category equals c.Id into rc
                      from rcs in rc.DefaultIfEmpty()

                      select new { r.Id, r.DeptId, r.Category, CategoryName = rcs.Name, r.Amount, r.Comment, r.Point, rds.DeptName };
            return QueryAndExcel(gridModel.ConsumePointGrid, q, "消费积分.xls");
        }
        private void addConsumePoint(DXInfo.Models.ConsumePoints consumePoint)
        {
            Uow.ConsumePoints.Add(consumePoint);
            Uow.Commit();
        }
        private void editConsumePoint(DXInfo.Models.ConsumePoints consumePoint)
        {
            var oldconsumePoint = Uow.ConsumePoints.GetById(g => g.Id == consumePoint.Id);
            oldconsumePoint.Comment = consumePoint.Comment;
            oldconsumePoint.DeptId = consumePoint.DeptId;
            oldconsumePoint.Category = consumePoint.Category;
            oldconsumePoint.Amount = consumePoint.Amount;
            oldconsumePoint.Point = consumePoint.Point;
            Uow.ConsumePoints.Update(oldconsumePoint);
            Uow.Commit();
        }
        private void delConsumePoint(DXInfo.Models.ConsumePoints consumePoint)
        {
            var oldconsumePoint = Uow.ConsumePoints.GetById(g => g.Id == consumePoint.Id);
            if (oldconsumePoint != null)
            {
                Uow.ConsumePoints.Delete(oldconsumePoint);
                Uow.Commit();
            }
        }
        public ActionResult ConsumePoint_EditData(DXInfo.Models.ConsumePoints consumePoint)
        {
            var gridModel = new ConsumePointGridModel();
            SetupConsumePointGridModel(gridModel.ConsumePointGrid);
            return ajaxCallBack<DXInfo.Models.ConsumePoints>(gridModel.ConsumePointGrid, consumePoint, addConsumePoint, editConsumePoint, delConsumePoint);
        }
        #endregion

        #region 口味设置
        private void SetupTasteGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Taste_RequestData");
            grid.EditUrl = Url.Action("Taste_EditData");
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetUpGrid(grid);
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
        }
        public ActionResult Taste()
        {
            var gridModel = new TasteGridModel();
            SetupTasteGridModel(gridModel.TasteGrid);
            return PartialView(gridModel);
        }
        public ActionResult Taste_RequestData()
        {
            var gridModel = new TasteGridModel();
            SetupTasteGridModel(gridModel.TasteGrid);

            var q = from r in Uow.Tastes.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()

                      select new { r.Id, r.DeptId, r.Code, r.Name, r.Comment, rds.DeptName };
            return QueryAndExcel(gridModel.TasteGrid, q, "口味.xls");
        }
        private void addTaste(DXInfo.Models.Tastes taste)
        {
            Uow.Tastes.Add(taste);
            Uow.Commit();
        }
        private void editTaste(DXInfo.Models.Tastes taste)
        {
            var oldTaste = Uow.Tastes.GetById(g => g.Id == taste.Id);
            oldTaste.Comment = taste.Comment;
            oldTaste.DeptId = taste.DeptId;
            oldTaste.Code = taste.Code;
            oldTaste.Name = taste.Name;
            Uow.Tastes.Update(oldTaste);
            Uow.Commit();
        }
        private void delTaste(DXInfo.Models.Tastes taste)
        {
            var oldTaste = Uow.Tastes.GetById(g => g.Id == taste.Id);
            if (oldTaste != null)
            {
                Uow.Tastes.Delete(oldTaste);
                Uow.Commit();
            }
        }
        public ActionResult Taste_EditData(DXInfo.Models.Tastes taste)
        {
            var gridModel = new TasteGridModel();
            SetupTasteGridModel(gridModel.TasteGrid);
            return ajaxCallBack<DXInfo.Models.Tastes>(gridModel.TasteGrid, taste, addTaste, editTaste, delTaste);
        }
        #endregion        

        #region 播放列表
        private void SetupPlayListGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("PlayList_RequestData");
            grid.EditUrl = Url.Action("PlayList_EditData");
            SetUpGrid(grid);
            SetBoolColumn(grid, "IsEnabled");
            SetTimeColumn(grid, "BeginTime");
            SetTimeColumn(grid, "EndTime");
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
            SetRequiredColumn(grid, "BeginTime");
            SetRequiredColumn(grid, "EndTime");
        }
        public ActionResult PlayList()
        {
            var gridModel = new PlayListGridModel();
            SetupPlayListGridModel(gridModel.PlayListGrid);
            return PartialView(gridModel);
        }
        public ActionResult PlayList_RequestData()
        {
            var gridModel = new PlayListGridModel();
            SetupPlayListGridModel(gridModel.PlayListGrid);

            var q = from r in Uow.PlayLists.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()

                      select new { r.Id, r.DeptId, r.Code, r.Name, r.BeginTime, r.EndTime, r.IsEnabled, rds.DeptName };
            return QueryAndExcel(gridModel.PlayListGrid, q, "播放列表.xls");
        }
        private void addPlayList(DXInfo.Models.PlayLists playList)
        {
            Uow.PlayLists.Add(playList);
            Uow.Commit();
        }
        private void editPlayList(DXInfo.Models.PlayLists playList)
        {
            var oldPlayList = Uow.PlayLists.GetById(g => g.Id == playList.Id);
            oldPlayList.DeptId = playList.DeptId;
            oldPlayList.Code = playList.Code;
            oldPlayList.Name = playList.Name;
            oldPlayList.BeginTime = playList.BeginTime;
            oldPlayList.EndTime = playList.EndTime;
            oldPlayList.IsEnabled = playList.IsEnabled;
            Uow.PlayLists.Update(oldPlayList);
            Uow.Commit();
        }
        private void delPlayList(DXInfo.Models.PlayLists playList)
        {
            var oldPlayList = Uow.PlayLists.GetById(g => g.Id == playList.Id);
            if (oldPlayList != null)
            {
                Uow.PlayLists.Delete(oldPlayList);
                Uow.Commit();
            }
        }
        public ActionResult PlayList_EditData(DXInfo.Models.PlayLists playList)
        {
            var gridModel = new PlayListGridModel();
            SetupPlayListGridModel(gridModel.PlayListGrid);
            return ajaxCallBack<DXInfo.Models.PlayLists>(gridModel.PlayListGrid,playList, addPlayList, editPlayList, delPlayList);
        }
        #endregion

        #region 卡商品赠送
        private void SetUpCardDonateInventoryGridModel(CardDonateInventoryGrid gridModel)
        {
            var CardGrid = gridModel.CardGrid;
            CardGrid.ClientSideEvents.SubGridRowExpanded = "showInventorySubGrid";
            CardGrid.DataUrl = Url.Action("CardDonateInventoryOfCard_RequestData");
            SetUpGrid(CardGrid);
            SetDateColumn(CardGrid, "CreateDate");

            var InventoryGrid = gridModel.InventoryGrid;
            InventoryGrid.DataUrl = Url.Action("CardDonateInventoryOfInventory_RequestData");
            InventoryGrid.EditUrl = Url.Action("CardDonateInventoryOfInventory_EditData");
            SetBoolColumn(InventoryGrid, "IsValidate");
            SetDateColumn(InventoryGrid, "InvalideDate");

            SetGridColumn(InventoryGrid, "SalePrice0", salePrice0ColumnVisible);
            SetGridColumn(InventoryGrid, "SalePrice1", salePrice1ColumnVisible);
            SetGridColumn(InventoryGrid, "SalePrice2", salePrice2ColumnVisible);

            SetGridColumn(InventoryGrid, "SalePoint0", salePrice0ColumnVisible);
            SetGridColumn(InventoryGrid, "SalePoint1", salePrice1ColumnVisible);
            SetGridColumn(InventoryGrid, "SalePoint2", salePrice2ColumnVisible);

            var InventoryGrid2 = gridModel.InventoryGrid2;
            InventoryGrid2.DataUrl = Url.Action("DonateInventory_RequestData");
            SetGridColumn(InventoryGrid2, "SalePrice0", salePrice0ColumnVisible);
            SetGridColumn(InventoryGrid2, "SalePrice1", salePrice1ColumnVisible);
            SetGridColumn(InventoryGrid2, "SalePrice2", salePrice2ColumnVisible);
            SetGridColumn(InventoryGrid2, "SalePoint0", salePrice0ColumnVisible);
            SetGridColumn(InventoryGrid2, "SalePoint1", salePrice1ColumnVisible);
            SetGridColumn(InventoryGrid2, "SalePoint2", salePrice2ColumnVisible);
        }
        [Authorize]
        public ActionResult CardDonateInventory()
        {
            var gridModel = new CardDonateInventoryGrid();
            SetUpCardDonateInventoryGridModel(gridModel);
            return PartialView(gridModel);
        }
        public JsonResult CardDonateInventoryOfCard_RequestData()
        {
            var gridModel = new CardDonateInventoryGrid();
            SetUpCardDonateInventoryGridModel(gridModel);
            var ps = from p in Uow.CardPoints.GetAll()
                     group p by p.Card into g
                     select new { Card = g.Key, Points = g.Sum(s => s.Point) };
            var rg = from r in Uow.Recharges.GetAll().Where(w => w.RechargeType == 0)
                     group r by r.Card into g
                     select new { Card = g.Key, Recharge = g.Sum(s => s.Amount) };

            var q = from c in Uow.Cards.GetAll()
                    join m in Uow.Members.GetAll() on c.Member equals m.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join l in Uow.CardLevels.GetAll() on c.CardLevel equals l.Id into cl
                    from cls in cl.DefaultIfEmpty()

                    join t in Uow.CardTypes.GetAll() on c.CardType equals t.Id into ct
                    from cts in ct.DefaultIfEmpty()

                    join p in ps on c.Id equals p.Card into cp
                    from cps in cp.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join r in rg on c.Id equals r.Card into rc
                    from rcs in rc.DefaultIfEmpty()


                    select new
                    {
                        c.Id,
                        c.Balance,
                        CardLevel = cls.Name,
                        c.CardNo,
                        CardType = cts.Name,
                        c.CreateDate,
                        Status = c.Status == 0 ? "正常在用" : c.Status == 1 ? "挂失" : "补卡",
                        cms.Email,
                        cms.IdCard,
                        cms.LinkAddress,
                        cms.LinkPhone,
                        cms.MemberName,
                        Points = cps.Points,// == null ? 0 : cps.Points,
                        FullName = cus.FullName,
                        DeptName = cds.DeptName,
                        Recharge = rcs.Recharge,// == null ? 0 : rcs.Recharge,
                    };
            return gridModel.CardGrid.DataBind(q);
        }
        public JsonResult CardDonateInventoryOfInventory_RequestData(Guid ParentRowId)
        {
            var gridModel = new CardDonateInventoryGrid();
            SetUpCardDonateInventoryGridModel(gridModel);
            DateTime? dt = null;
            var invs = (from inv in Uow.Inventory.GetAll().Where(w => w.IsDonate == true)
                        join unit in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                        from iu in invunit.DefaultIfEmpty()
                        join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                        from invcs in invc.DefaultIfEmpty()

                        join d in Uow.CardDonateInventory.GetAll().Where(w => w.CardId == ParentRowId) on inv.Id equals d.Inventory into invd
                        from invds in invd.DefaultIfEmpty()

                        select new
                        {
                            inv.Id,
                            inv.Code,
                            inv.Name,
                            CategoryName = invcs.Name,
                            inv.Comment,
                            IsValidate = (invds == null ? false : invds.IsValidate),
                            InvalideDate = (invds == null ? dt : invds.InvalideDate),
                            inv.SalePrice,
                            inv.SalePrice0,
                            inv.SalePrice1,
                            inv.SalePrice2,
                            inv.SalePoint,
                            inv.SalePoint0,
                            inv.SalePoint1,
                            inv.SalePoint2,
                            inv.ImageFileName,
                            inv.Specs,
                            UnitName = iu.Name,
                        });
            return gridModel.InventoryGrid.DataBind(invs);
        }
        public JsonResult DonateInventory_RequestData()
        {
            var gridModel = new CardDonateInventoryGrid();
            SetUpCardDonateInventoryGridModel(gridModel);
            var invs = (from inv in Uow.Inventory.GetAll().Where(w => w.IsDonate == true)
                        join unit in Uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                        from iu in invunit.DefaultIfEmpty()
                        join c in Uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                        from invcs in invc.DefaultIfEmpty()

                        select new
                        {
                            inv.Id,
                            inv.Code,
                            inv.Name,
                            CategoryName = invcs.Name,
                            inv.Comment,
                            inv.SalePrice,
                            inv.SalePrice0,
                            inv.SalePrice1,
                            inv.SalePrice2,
                            inv.SalePoint,
                            inv.SalePoint0,
                            inv.SalePoint1,
                            inv.SalePoint2,
                            inv.ImageFileName,
                            inv.Specs,
                            UnitName = iu.Name,
                        });
            return gridModel.InventoryGrid2.DataBind(invs);
        }
        private void addOrEditCardDonateInventory(DXInfo.Models.CardDonateInventory cardDonateInventory, 
            Guid cardId, Guid invId, DateTime invalidateDate, bool isValidate)
        {
            if (cardDonateInventory == null)
            {
                DXInfo.Models.CardDonateInventory cdi = new DXInfo.Models.CardDonateInventory();
                cdi.CardId = cardId;
                cdi.Inventory = invId;
                cdi.InvalideDate = invalidateDate;
                cdi.IsValidate = isValidate;
                Uow.CardDonateInventory.Add(cdi);
                Uow.Commit();
            }
            else
            {
                cardDonateInventory.IsValidate = isValidate;
                cardDonateInventory.InvalideDate = invalidateDate;
                Uow.CardDonateInventory.Update(cardDonateInventory);
                Uow.Commit();
            }
        }
        public ActionResult CardDonateInventoryOfInventory_EditData(Guid ParentRowId, Guid Id, bool IsValidate, DateTime InvalideDate)
        {
            var d = Uow.CardDonateInventory.GetAll().Where(w => w.Inventory == Id).Where(w => w.CardId == ParentRowId).FirstOrDefault();
            addOrEditCardDonateInventory(d, ParentRowId, Id, InvalideDate, IsValidate);
            return new EmptyResult();
        }
        [HttpPost]
        public ActionResult DonateInventory_EditData(string cards, string invs, DateTime? InvalideDate)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            List<Guid> lgcards = new List<Guid>();
            List<Guid> lginvs = new List<Guid>();

            if (string.IsNullOrEmpty(cards))
            {
                json.Data = "请选择会员";
                return json;
            }
            foreach (string c in cards.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lgcards.Add(Guid.Parse(c));
                }
            }
            if (lgcards.Count == 0)
            {
                json.Data = "请选择会员";
                return json;
            }
            if (string.IsNullOrEmpty(invs))
            {
                json.Data = "请选择赠送商品";
                return json;
            }
            foreach (string c in invs.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lginvs.Add(Guid.Parse(c));
                }
            }
            if (lginvs.Count == 0)
            {
                json.Data = "请选择赠送商品";
                return json;
            }
            if (!InvalideDate.HasValue)
            {
                json.Data = "请输入有效期";
                return json;
            }
            foreach (Guid id in lgcards)
            {

                foreach (Guid id2 in lginvs)
                {
                    var d = Uow.CardDonateInventory.GetAll().Where(w => w.Inventory == id2 && w.CardId == id).FirstOrDefault();
                    addOrEditCardDonateInventory(d, id, id2, InvalideDate.Value, true);
                }
            }
            json.Data = "设置成功";
            return json;
        }
        #endregion

        #region 卡型设置
        private void SetupCardTypeGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("CardType_RequestData");
            grid.EditUrl = Url.Action("CardType_EditData");
            SetUpGrid(grid);
            this.SetBoolColumn(grid, "IsMoney");
            this.SetBoolColumn(grid, "IsVirtual");
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
        }
        public ActionResult CardType()
        {
            var gridModel = new CardTypeGridModel();
            SetupCardTypeGridModel(gridModel.CardTypeGrid);
            return PartialView(gridModel);
        }
        public ActionResult CardType_RequestData()
        {
            var gridModel = new CardTypeGridModel();
            SetupCardTypeGridModel(gridModel.CardTypeGrid);

            var q = from r in Uow.CardTypes.GetAll()
                      select r;
            return QueryAndExcel(gridModel.CardTypeGrid, q, "卡型.xls");
        }
        private void addCardType(DXInfo.Models.CardTypes cardType)
        {
            Uow.CardTypes.Add(cardType);
            Uow.Commit();
        }
        private void editCardType(DXInfo.Models.CardTypes cardType)
        {
            var oldCardType = Uow.CardTypes.GetById(g => g.Id == cardType.Id);
            oldCardType.Comment = cardType.Comment;
            oldCardType.Code = cardType.Code;
            oldCardType.Name = cardType.Name;
            oldCardType.IsMoney = cardType.IsMoney;
            oldCardType.IsVirtual = cardType.IsVirtual;
            oldCardType.CardNoRule = cardType.CardNoRule;
            Uow.CardTypes.Update(oldCardType);
            Uow.Commit();
        }
        private void delCardType(DXInfo.Models.CardTypes cardType)
        {
            var oldCardType = Uow.CardTypes.GetById(g => g.Id == cardType.Id);
            if (oldCardType != null)
            {
                Uow.CardTypes.Delete(oldCardType);
                Uow.Commit();
            }
        }
        public ActionResult CardType_EditData(DXInfo.Models.CardTypes cardType)
        {
            var gridModel = new CardTypeGridModel();
            SetupCardTypeGridModel(gridModel.CardTypeGrid);
            return ajaxCallBack<DXInfo.Models.CardTypes>(gridModel.CardTypeGrid, cardType, addCardType, editCardType, delCardType);
        }
        #endregion

        #region 支付方式设置
        private void SetupPayTypeGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("PayType_RequestData");
            grid.EditUrl = Url.Action("PayType_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "PayType", centerCommon.GetNameCodeType(DXInfo.Models.NameCodeType.PayType));
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Name");
        }
        public ActionResult PayType()
        {
            var gridModel = new PayTypeGridModel();
            SetupPayTypeGridModel(gridModel.PayTypeGrid);
            return PartialView(gridModel);
        }
        public ActionResult PayType_RequestData()
        {
            var gridModel = new PayTypeGridModel();
            SetupPayTypeGridModel(gridModel.PayTypeGrid);

            var q = from r in Uow.PayTypes.GetAll()
                      join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "PayType") on SqlFunctions.StringConvert((double?)r.PayType).Trim() equals d1.Code into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      select new
                      {
                          r.Id,
                          r.Code,
                          r.Name,
                          r.Comment,
                          r.PayType,
                          PayTypeName = dd1s.Name
                      };
            return QueryAndExcel(gridModel.PayTypeGrid, q, "支付方式.xls");
        }
        private void addPayType(DXInfo.Models.PayTypes payType)
        {
            Uow.PayTypes.Add(payType);
            Uow.Commit();
        }
        private void editPayType(DXInfo.Models.PayTypes payType)
        {
            var oldPayType = Uow.PayTypes.GetById(g => g.Id == payType.Id);
            oldPayType.Comment = payType.Comment;
            oldPayType.Code = payType.Code;
            oldPayType.Name = payType.Name;
            oldPayType.PayType = payType.PayType;
            Uow.PayTypes.Update(oldPayType);
            Uow.Commit();
        }
        private void delPayType(DXInfo.Models.PayTypes payType)
        {
            var oldPayType = Uow.PayTypes.GetById(g => g.Id == payType.Id);
            if (oldPayType != null)
            {
                Uow.PayTypes.Delete(oldPayType);
                Uow.Commit();
            }
        }
        public ActionResult PayType_EditData(DXInfo.Models.PayTypes payTypes)
        {
            var gridModel = new PayTypeGridModel();
            SetupPayTypeGridModel(gridModel.PayTypeGrid);
            return ajaxCallBack<DXInfo.Models.PayTypes>(gridModel.PayTypeGrid, payTypes, addPayType, editPayType, delPayType);
        }
        #endregion

        #region 菜单设置
        private void SetupSitemapGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Sitemap_RequestData");
            grid.EditUrl = Url.Action("Sitemap_EditData");
            SetUpGrid(grid);
            SetBoolColumn(grid, "IsMenu");
            SetBoolColumn(grid, "IsClient");
            SetBoolColumn(grid, "IsAuthorize");
            SetRequiredColumn(grid, "Code");
            SetRequiredColumn(grid, "Title");
            SetRequiredColumn(grid, "Description");
            SetRequiredColumn(grid, "ParentCode");
            SetRequiredColumn(grid, "Sort");
        }
        [Authorize]
        public ActionResult Sitemap()
        {
            var gridModel = new SitemapGridModel();
            SetupSitemapGridModel(gridModel.SitemapGrid);
            return PartialView(gridModel);
        }
        public ActionResult Sitemap_RequestData()
        {
            var gridModel = new SitemapGridModel();
            SetupSitemapGridModel(gridModel.SitemapGrid);

            var q = from r in Uow.aspnet_Sitemaps.GetAll()
                      select r;
            return QueryAndExcel(gridModel.SitemapGrid, q, "菜单.xls");
        }
        private void addSitemap(DXInfo.Models.aspnet_Sitemaps sitemap)
        {
            Uow.aspnet_Sitemaps.Add(sitemap);
            Uow.Commit();
        }
        private void editSitemap(DXInfo.Models.aspnet_Sitemaps sitemap)
        {
            var oldSitemap = Uow.aspnet_Sitemaps.GetById(g => g.Code == sitemap.Code);
            oldSitemap.Code = sitemap.Code;
            oldSitemap.Name = sitemap.Name;
            oldSitemap.Title = sitemap.Title;
            oldSitemap.Description = sitemap.Description;
            oldSitemap.Controller = sitemap.Controller;
            oldSitemap.Action = sitemap.Action;
            oldSitemap.ParaId = sitemap.ParaId;
            oldSitemap.Url = sitemap.Url;
            oldSitemap.ParentCode = sitemap.ParentCode;
            oldSitemap.IsAuthorize = sitemap.IsAuthorize;
            oldSitemap.IsMenu = sitemap.IsMenu;
            oldSitemap.IsClient = sitemap.IsClient;
            oldSitemap.Sort = sitemap.Sort;
            Uow.aspnet_Sitemaps.Update(oldSitemap);
            Uow.Commit();
        }
        private void delSitemap(DXInfo.Models.aspnet_Sitemaps sitemap)
        {
            var oldSitemap = Uow.aspnet_Sitemaps.GetById(g => g.Code == sitemap.Code);
            if (oldSitemap != null)
            {
                Uow.aspnet_Sitemaps.Delete(oldSitemap);
                Uow.Commit();
            }
        }
        public ActionResult Sitemap_EditData(DXInfo.Models.aspnet_Sitemaps sitemap)
        {
            var gridModel = new SitemapGridModel();
            SetupSitemapGridModel(gridModel.SitemapGrid);
            return ajaxCallBack<DXInfo.Models.aspnet_Sitemaps>(gridModel.SitemapGrid, sitemap, addSitemap, editSitemap, delSitemap);
        }
        #endregion

        #region 单据
        private void SetupReceiptGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Receipt_RequestData");
            grid.ExcelExportSettings.Url = Url.Action("Receipt_RequestData");
            //grid.ClientSideEvents.BeforeAjaxRequest = "beforeRequest";   
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.MultiSelect = true;
            grid.MultiSelectKey = MultiSelectKey.None;
            grid.MultiSelectMode = MultiSelectMode.SelectOnCheckBoxClickOnly;

            SetDropDownColumn(grid,"Status",centerCommon.GetNameCodeType(DXInfo.Models.NameCodeType.ReceiptStatus));
            SetDateTimeColumn(grid, "CreateDate");
            SetDateTimeColumn(grid, "ModifyDate");
        }
        public ActionResult Receipt(int ReceiptType)
        {
            var gridModel = new ReceiptGridModel();
            gridModel.ReceiptType = ReceiptType;
            SetupReceiptGridModel(gridModel.ReceiptGrid);
            return PartialView(gridModel);
        }
        public ActionResult Receipt_RequestData()
        {
            var gridModel = new ReceiptGridModel();
            SetupReceiptGridModel(gridModel.ReceiptGrid);            
            var rts =
                     from d in Uow.Receipts.GetAll()

                     join d1 in Uow.Members.GetAll() on d.Member equals d1.Id into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     join d2 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d2.UserId into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.aspnet_CustomProfile.GetAll() on d.ModifyUserId equals d3.UserId into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     join d4 in Uow.Depts.GetAll() on d.DeptId equals d4.DeptId into dd4
                     from dd4s in dd4.DefaultIfEmpty()

                     join d5 in Uow.Depts.GetAll() on d.ModifyDeptId equals d5.DeptId into dd5
                     from dd5s in dd5.DefaultIfEmpty()

                     join d6 in Uow.NameCode.GetAll().Where(w => w.Type == "ReceiptStatus") on SqlFunctions.StringConvert((double?)d.Status).Trim() equals d6.Code into dd6
                     from dd6s in dd6.DefaultIfEmpty()

                     where dd1s.MemberType == DXInfo.Models.MemberType.Receipt
                     orderby d.CreateDate
                     select new
                     {
                         d.Id,
                         d.Status,
                         StatusName=dd6s==null?"":dd6s.Name,
                         d.ReceiptType,
                         d.Content,
                         d.Comment,
                         d.CreateDate,
                         dd2s.FullName,
                         dd4s.DeptName,
                         d.ModifyDate,
                         ModifyDeptName = dd5s.DeptName,
                         ModifyFullName = dd3s.FullName,                         
                         dd1s.Email,
                         dd1s.IdCard,
                         dd1s.LinkAddress,
                         dd1s.LinkPhone,
                         dd1s.MemberName,
                         dd1s.Birthday,
                         dd1s.Sex,
                     };
            return QueryAndExcel(gridModel.ReceiptGrid, rts, "单据.xls");
        }
        [HttpPost]
        public ActionResult ReceiptComplete(string receipts)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            List<Guid> lReceipt = new List<Guid>();

            if (string.IsNullOrEmpty(receipts))
            {
                json.Data = "请选择单据";
                return json;
            }
            foreach (string c in receipts.Split(','))
            {
                if (!string.IsNullOrEmpty(c))
                {
                    lReceipt.Add(Guid.Parse(c));
                }
            }
            if (lReceipt.Count > 0)
            {
                foreach (Guid receiptId in lReceipt)
                {
                    DXInfo.Models.Receipts receipt = Uow.Receipts.GetById(g => g.Id == receiptId);
                    if (receipt != null && receipt.Status == (int)DXInfo.Models.ReceiptStatus.Normal)
                    {
                        receipt.Status = (int)DXInfo.Models.ReceiptStatus.Complete;
                        receipt.ModifyDate = DateTime.Now;
                        Guid userId = operId;
                        //Guid deptId = this.deptId;
                        receipt.ModifyUserId = userId;
                        receipt.ModifyDeptId = deptId;
                        Uow.Receipts.Update(receipt);

                        DXInfo.Models.ReceiptHis receiptHis = Mapper.Map<DXInfo.Models.ReceiptHis>(receipt);
                        receiptHis.LinkId = receipt.Id;
                        Uow.ReceiptHis.Add(receiptHis);

                        Uow.Commit();
                    }
                }
            }
            json.Data = "设置成功";
            return json;
        }
        #endregion

        #region AMSApp

        #region 商品管理
        private void SetupGoodsGridModel(JQGrid grid)
        {            
            grid.DataUrl = Url.Action("Goods_RequestData");
            grid.EditUrl = Url.Action("Goods_EditData");
            SetUpGrid(grid);
            SetRequiredColumn(grid, "vcGoodsName");
            SetRequiredColumn(grid, "vcSpell");
            SetRequiredColumn(grid, "nPrice");
            SetRequiredColumn(grid, "iIgValue");
            SetRequiredColumn(grid, "vcComments");
            SetRequiredColumn(grid, "vcGoodsType");

            SetDropDownColumn(grid, "vcComments", new List<SelectListItem>() { new SelectListItem { Selected=true,Text="否",Value="否" },
                new SelectListItem {Text="是",Value="是" } });

            SetDropDownColumn(grid, "cNewFlag", new List<SelectListItem>() { new SelectListItem { Selected=true,Text="否",Value="0" },
                new SelectListItem {Text="是",Value="1" } });

            SetDropDownColumn(grid, "vcGoodsType", centerCommon.GetGoodsType());

            grid.ClientSideEvents.AfterAddDialogShown = "enableFields";
            grid.ClientSideEvents.AfterEditDialogShown = "disableFields";
            
        }
        public ActionResult Goods_RequestData()
        {
            var gridModel = new GoodsGridModel();
            SetupGoodsGridModel(gridModel.GoodsGrid);

            var q = from d in Uow.tbGoods.GetAll()
                    select d;
            return QueryAndExcel(gridModel.GoodsGrid, q, "商品.xls");
        }
        private void addGoods(DXInfo.Models.tbGoods tbGoods)
        {
            string[] goodsTypes = tbGoods.vcGoodsType.Split('-');
            int minId = Convert.ToInt32(goodsTypes[0].PadRight(9,'0'));// + "00");
            int maxId = Convert.ToInt32(goodsTypes[1].PadRight(9,'9'));// + "99");

            int? d = Uow.tbGoods.GetAll()
                .Where(w => w.vcGoodsType == tbGoods.vcGoodsType)
                .Select(s => s.vcGoodsID)
                .Cast<int?>().Max();
            int goodsId = 0;
            if (d.HasValue)
            {
                goodsId = d.Value + 1;
            }
            if (goodsId < minId) goodsId = minId;
            if (goodsId > maxId) throw new BusinessException("商品编码已超过范围");
            tbGoods.vcGoodsID = goodsId.ToString();
            //名称不重复
            int count = Uow.tbGoods.GetAll().Where(w => w.vcGoodsName == tbGoods.vcGoodsName).Count();
            if (count > 0) throw new BusinessException("商品名称重复");
            tbGoods.nRate = 0;
            tbGoods.vcComments = "否";
            Uow.tbGoods.Add(tbGoods);
            Uow.Commit();
        }
        private void editGoods(DXInfo.Models.tbGoods tbGoods)
        {
            var oldtbGoods = Uow.tbGoods.GetById(g => g.vcGoodsID == tbGoods.vcGoodsID);
            if (oldtbGoods.vcGoodsName != tbGoods.vcGoodsName)
            {
                //名称不重复
                int count = Uow.tbGoods.GetAll().Where(w => w.vcGoodsName == tbGoods.vcGoodsName).Count();
                if (count > 0) throw new BusinessException("商品名称重复");
            }
            oldtbGoods.vcGoodsName = tbGoods.vcGoodsName;
            oldtbGoods.vcSpell = tbGoods.vcSpell;
            oldtbGoods.nPrice = tbGoods.nPrice;
            oldtbGoods.cNewFlag = tbGoods.cNewFlag;
            oldtbGoods.iIgValue = tbGoods.iIgValue;
            Uow.tbGoods.Update(oldtbGoods);
            Uow.Commit();
        }
        private void delGoods(DXInfo.Models.tbGoods tbGoods)
        {
            var oldtbGoods = Uow.tbGoods.GetById(g => g.vcGoodsID == tbGoods.vcGoodsID);
            if (oldtbGoods != null)
            {
                Uow.tbGoods.Delete(oldtbGoods);
                Uow.Commit();
            }
        }
        public ActionResult Goods_EditData(DXInfo.Models.tbGoods tbGoods)
        {
            var gridModel = new GoodsGridModel();
            SetupGoodsGridModel(gridModel.GoodsGrid);
            return ajaxCallBack<DXInfo.Models.tbGoods>(gridModel.GoodsGrid, tbGoods, addGoods, editGoods, delGoods);
        }
        public ActionResult Goods()
        {
            var gridModel = new GoodsGridModel();
            SetupGoodsGridModel(gridModel.GoodsGrid);
            return PartialView(gridModel);
        }
        #endregion

        #region 系统参数设定
        private void SetupSysParaSetGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("SysParaSet_RequestData");
            grid.EditUrl = Url.Action("SysParaSet_EditData");
            SetUpGrid(grid);
        }
        public ActionResult SysParaSet_RequestData()
        {
            var gridModel = new SysParaSetGridModel();
            SetupSysParaSetGridModel(gridModel.SysParaSetGrid);
            var q = from d in Uow.tbCommCode.GetAll()
                    where d.vcCommSign == "MD" || d.vcCommSign == "IG" ||
                    d.vcCommSign == "CP" || d.vcCommSign.StartsWith("FP")
                    select d;
            return QueryAndExcel(gridModel.SysParaSetGrid, q, "系统参数设定.xls");
        }
        private void addSysParaSet(DXInfo.Models.tbCommCode tbCommCode)
        {
            Uow.tbCommCode.Add(tbCommCode);
            Uow.Commit();
        }
        private void editSysParaSet(DXInfo.Models.tbCommCode tbCommCode)
        {
            var oldtbCommCode = Uow.tbCommCode.GetById(g => g.Id==tbCommCode.Id);
            oldtbCommCode.vcCommCode = tbCommCode.vcCommCode;
            oldtbCommCode.vcCommName = tbCommCode.vcCommName;
            oldtbCommCode.vcCommSign = tbCommCode.vcCommSign;
            oldtbCommCode.vcComments = tbCommCode.vcComments;
            Uow.tbCommCode.Update(oldtbCommCode);
            Uow.Commit();
        }
        private void delSysParaSet(DXInfo.Models.tbCommCode tbCommCode)
        {
            var oldtbCommCode = Uow.tbCommCode.GetById(g => g.Id==tbCommCode.Id);
            if (oldtbCommCode != null)
            {
                Uow.tbCommCode.Delete(oldtbCommCode);
                Uow.Commit();
            }
        }
        public ActionResult SysParaSet_EditData(DXInfo.Models.tbCommCode tbCommCode)
        {
            var gridModel = new SysParaSetGridModel();
            SetupSysParaSetGridModel(gridModel.SysParaSetGrid);
            return ajaxCallBack<DXInfo.Models.tbCommCode>(gridModel.SysParaSetGrid, tbCommCode, addSysParaSet, editSysParaSet, delSysParaSet);
        }
        public ActionResult SysParaSet()
        {
            var gridModel = new SysParaSetGridModel();
            SetupSysParaSetGridModel(gridModel.SysParaSetGrid);
            return PartialView(gridModel);
        }
        #endregion

        #endregion
    }
}
