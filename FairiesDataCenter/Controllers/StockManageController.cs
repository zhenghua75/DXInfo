using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;
using ynhnTransportManage.Models.StockManage;
using System.Linq.Dynamic;
using System.Data.Objects.SqlClient;
using System.Data.Objects;
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
using ynhnTransportManage.Models;

namespace ynhnTransportManage.Controllers
{
    public class CodeVouchDate
    {
        public string Code { get; set; }
        public string VouchDateId { get; set; }
        public string CurDate { get; set; }
        public Guid Salesman { get; set; }
    }
    
    public class StockManageController : BaseController
    {
        #region 私有方法
        private void SetStockColumn(JQGrid grid)
        {
            //保质期
            SetGridColumn(grid, "ShelfLife", isShelfLife);
            SetGridColumn(grid, "ShelfLifeType", isShelfLife);
            //SetGridColumn(grid, "ShelfLifeTypeName", isShelfLife);
            SetGridColumn(grid, "InvalidDate", isShelfLife);
            
            //SetGridColumn(grid, "LocatorName", isLocator);
            SetGridColumn(grid, "Locator", isLocator);
            //货位
            SetGridColumn(grid, "Batch", isBatch);
        }
        #endregion
        #region 构造方法
        public StockManageController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");
        }
        
        #endregion

        #region 常量
        public const string RdRecordsSession = "RdRecordsSession";
        public const string ScrapVouchsSession = "ScrapVouchsSession";
        public const string TransVouchsSession = "TransVouchsSession";
        public const string CheckVouchsSession = "CheckVouchsSession";
        public const string AdjustLocatorVouchsSession = "AdjustLocatorVouchsSession";
        public const string MixVouchsSession = "MixVouchsSession";
        public const string BatchInventorySession = "BatchInventorySession";
        public const string BatchWarehouseInventorySession = "BatchWarehouseInventorySession";
        #endregion

        #region 菜单目录
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult InitSetting()
        {
            return View();
        }
        [Authorize]
        public ActionResult DayToDayBusiness()
        {
            return View();
        }
        [Authorize]
        public ActionResult TransactionProcess()
        {
            return View();
        }
        [Authorize]
        public ActionResult Report()
        {
            return View();
        }
        #endregion

        

        #region 库存仓库档案
        private void SetupWarehouseGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Warehouse_RequestData");
            grid.EditUrl = Url.Action("Warehouse_EditData");

            this.SetDropDownColumn(grid, "Dept", this.GetDept());            
        }
        [Authorize]
        public ActionResult Warehouse()
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);
            return View(gridModel);
        }
        public ActionResult Warehouse_RequestData()
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);

            var units = from d in Uow.Warehouse.GetAll()
                        join d1 in Uow.Depts.GetAll() on d.Dept equals d1.DeptId into dd1
                        from dd1s in dd1.DefaultIfEmpty()

                        select new { d.Id, d.Code, d.Name, d.Comment, d.Dept, DeptName = dd1s.DeptName,d.Principal,d.Tele,d.Address };
            if (gridModel.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.WarehouseGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.WarehouseGrid.ExportToExcel(units, "库存仓库档案.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.WarehouseGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.WarehouseGrid.DataBind(units);
            }
        }
        private bool CheckWarehouseCodeDup(string code)
        {
            return Uow.Warehouse.GetAll().Where(w => w.Code == code).Count() > 0;
        }
        private bool CheckWarehouseNameDup(string name)
        {
            return Uow.Warehouse.GetAll().Where(w => w.Name == name).Count() > 0;
        }
        public ActionResult Warehouse_EditData(DXInfo.Models.Warehouse warehouse)
        {
            var gridModel = new WarehouseGridModel();
            SetupWarehouseGridModel(gridModel.WarehouseGrid);
            try
            {
                if (gridModel.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckWarehouseCodeDup(warehouse.Code))
                    {
                        return gridModel.WarehouseGrid.ShowEditValidationMessage("编码重复");
                    }
                    if (CheckWarehouseNameDup(warehouse.Name))
                    {
                        return gridModel.WarehouseGrid.ShowEditValidationMessage("名称重复");
                    }
                    Uow.Warehouse.Add(warehouse);
                    Uow.Commit();
                }
                if (gridModel.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldWarehouse = Uow.Warehouse.GetById(g=>g.Id==warehouse.Id);
                    if (oldWarehouse.Code != warehouse.Code && CheckWarehouseCodeDup(warehouse.Code))
                    {
                        return gridModel.WarehouseGrid.ShowEditValidationMessage("编码重复");
                    }
                    if (oldWarehouse.Name != warehouse.Name && CheckWarehouseNameDup(warehouse.Name))
                    {
                        return gridModel.WarehouseGrid.ShowEditValidationMessage("名称重复");
                    }
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
                if (gridModel.WarehouseGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var count = Uow.Locator.GetAll().Where(w => w.Warehouse == warehouse.Id).Count() + Uow.RdRecord.GetAll().Where(w => w.WhId == warehouse.Id).Count();
                    if (count > 0)
                        return gridModel.WarehouseGrid.ShowEditValidationMessage("仓库已使用不能删除");
                    var oldWarehouse = Uow.Warehouse.GetById(g => g.Id == warehouse.Id);
                    Uow.Warehouse.Delete(oldWarehouse);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.WarehouseGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("Warehouse");
        }
        #endregion

        #region 库存货位档案
        private void SetupLocatorGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Locator_RequestData");
            grid.EditUrl = Url.Action("Locator_EditData");
            this.SetDropDownColumn(grid, "Warehouse", centerCommon.GetWarehouse());
        }
        [Authorize]
        public ActionResult Locator()
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);
            return View(gridModel);
        }
        public ActionResult Locator_RequestData()
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);

            var units = from d in Uow.Locator.GetAll()
                        join d1 in Uow.Warehouse.GetAll() on d.Warehouse equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        select new { d.Id, d.Code, d.Name, d.Comment, d.Warehouse, WarehouseName = dd1s.Name };

            if (gridModel.LocatorGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.LocatorGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.LocatorGrid.ExportToExcel(units, "库存货位档案.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.LocatorGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.LocatorGrid.DataBind(units);
            }
        }
        private bool CheckLocatorCodeDup(string code,Guid wh)
        {
            return Uow.Locator.GetAll().Where(w => w.Code == code && w.Warehouse==wh).Count() > 0;
        }
        private bool CheckLocatorNameDup(string name,Guid wh)
        {
            return Uow.Locator.GetAll().Where(w => w.Name == name && w.Warehouse ==wh).Count() > 0;
        }
        public ActionResult Locator_EditData(DXInfo.Models.Locator locator)
        {
            var gridModel = new LocatorGridModel();
            SetupLocatorGridModel(gridModel.LocatorGrid);
            try
            {
                if (gridModel.LocatorGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckLocatorCodeDup(locator.Code, locator.Warehouse))
                    {
                        return gridModel.LocatorGrid.ShowEditValidationMessage("编码重复");
                    }
                    if (CheckLocatorNameDup(locator.Name, locator.Warehouse))
                    {
                        return gridModel.LocatorGrid.ShowEditValidationMessage("名称重复");
                    }
                    Uow.Locator.Add(locator);
                    Uow.Commit();
                }
                if (gridModel.LocatorGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldlocator = Uow.Locator.GetById(g=>g.Id==locator.Id);
                    if (oldlocator.Warehouse != locator.Warehouse)
                    {
                        var count = Uow.RdRecords.GetAll().Where(w => w.Locator == oldlocator.Id).Count();
                        if (count > 0)
                            return gridModel.LocatorGrid.ShowEditValidationMessage("已使用货位不能修改所属仓库");
                    }
                    if (oldlocator.Code != locator.Code)
                    {
                        if (oldlocator.Warehouse != locator.Warehouse)
                        {
                            if (CheckLocatorCodeDup(locator.Code, locator.Warehouse))
                            {
                                return gridModel.LocatorGrid.ShowEditValidationMessage("编码重复");
                            }
                        }
                        else
                        {
                            if (CheckLocatorCodeDup(locator.Code, oldlocator.Warehouse))
                            {
                                return gridModel.LocatorGrid.ShowEditValidationMessage("编码重复");
                            }
                        }
                    }
                    if (oldlocator.Name != locator.Name)
                    {
                        if (oldlocator.Warehouse != locator.Warehouse)
                        {
                            if (CheckLocatorNameDup(locator.Name, locator.Warehouse))
                            {
                                return gridModel.LocatorGrid.ShowEditValidationMessage("名称重复");
                            }
                        }
                        else
                        {
                            if (CheckLocatorNameDup(locator.Name, oldlocator.Warehouse))
                                return gridModel.LocatorGrid.ShowEditValidationMessage("名称重复");
                        }
                    }
                    oldlocator.Code = locator.Code;
                    oldlocator.Name = locator.Name;
                    oldlocator.Comment = locator.Comment;
                    oldlocator.Warehouse = locator.Warehouse;
                    Uow.Locator.Update(oldlocator);
                    Uow.Commit();
                }
                if (gridModel.LocatorGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var count = Uow.RdRecords.GetAll().Where(w => w.Locator == locator.Id).Count();
                    if (count > 0)
                        return gridModel.LocatorGrid.ShowEditValidationMessage("货位已使用不能删除");
                    var oldLocator = Uow.Locator.GetById(g => g.Id == locator.Id);
                    Uow.Locator.Delete(oldLocator);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.LocatorGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("Locator");
        }
        #endregion

        #region 库存供应商档案
        private void SetupVendorGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Vendor_RequestData");
            grid.EditUrl = Url.Action("Vendor_EditData");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.ClientSideEvents.BeforeAddDialogSubmit = "beforeSubmit";
            grid.ClientSideEvents.BeforeEditDialogSubmit = "beforeSubmit";
            grid.AppearanceSettings.Caption = this.Title;
            if (this.Request["AddInventory"] != null)
            {
                grid.AppearanceSettings.Caption = "";
            }
        }
        [Authorize]
        public ActionResult Vendor(int? VendorType)
        {
            var gridModel = new VendorGridModel();
            if (VendorType.HasValue)
            {
                gridModel.VendorType = VendorType.Value;
            }
            else
            {
                gridModel.VendorType = (int)DXInfo.Models.VendorType.Supplier;
            }
            SetupVendorGridModel(gridModel.VendorGrid);
            return View(gridModel);
        }
        public ActionResult Vendor_RequestData()
        {
            var gridModel = new VendorGridModel();
            SetupVendorGridModel(gridModel.VendorGrid);

            var units = from d in Uow.Vendor.GetAll() select d;
            if (gridModel.VendorGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.VendorGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.VendorGrid.ExportToExcel(units, "库存供应商档案.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.VendorGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.VendorGrid.DataBind(units);
            }
        }
        private bool CheckVendorCodeDup(string code)
        {
            return Uow.Vendor.GetAll().Where(w => w.Code == code).Count() > 0;
        }
        private bool CheckVendorNameDup(string name)
        {
            return Uow.Vendor.GetAll().Where(w => w.Name == name).Count() > 0;
        }
        public ActionResult Vendor_EditData(DXInfo.Models.Vendor vendor,int VendorType)
        {
            var gridModel = new VendorGridModel();
            SetupVendorGridModel(gridModel.VendorGrid);
            try
            {
                if (gridModel.VendorGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckVendorCodeDup(vendor.Code))
                    {
                        return gridModel.VendorGrid.ShowEditValidationMessage("编码重复");
                    }
                    if (CheckVendorNameDup(vendor.Name))
                    {
                        return gridModel.VendorGrid.ShowEditValidationMessage("名称重复");
                    }
                    vendor.VendorType = VendorType;
                    Uow.Vendor.Add(vendor);
                    Uow.Commit();
                }
                if (gridModel.VendorGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldVendor = Uow.Vendor.GetById(g=>g.Id==vendor.Id);
                    if (oldVendor.Code != vendor.Code && CheckVendorCodeDup(vendor.Code))
                    {
                        return gridModel.VendorGrid.ShowEditValidationMessage("编码重复");
                    }
                    if (oldVendor.Name != vendor.Name && CheckVendorNameDup(vendor.Name))
                    {
                        return gridModel.VendorGrid.ShowEditValidationMessage("名称重复");
                    }
                    oldVendor.Code = vendor.Code;
                    oldVendor.Name = vendor.Name;
                    oldVendor.Tel = vendor.Tel;
                    oldVendor.Fax = vendor.Fax;
                    oldVendor.Phone = vendor.Phone;
                    oldVendor.Zip = vendor.Zip;
                    oldVendor.Linkman = vendor.Linkman;
                    oldVendor.Address = vendor.Address;
                    oldVendor.Email = vendor.Email;
                    oldVendor.VendorType = VendorType;
                    Uow.Vendor.Update(oldVendor);
                    Uow.Commit();
                }
                if (gridModel.VendorGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var count = Uow.RdRecord.GetAll().Where(w => w.VenId == vendor.Id).Count();
                    if (count > 0)
                        return gridModel.VendorGrid.ShowEditValidationMessage("供应商已使用不能删除");
                    var oldVendor = Uow.Vendor.GetById(g => g.Id == vendor.Id);
                    Uow.Vendor.Delete(oldVendor);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.VendorGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("Vendor");
        }
        public JsonResult GetReceivers()
        {
            List<SelectListItem> lsi = centerCommon.GetVendor((int)DXInfo.Models.VendorType.Receiver);
            return Json(lsi, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 库存单据权限
        private void SetupVouchAuthorityGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("VouchAuthority_RequestData");
            grid.EditUrl = Url.Action("VouchAuthority_EditData");

            this.SetDropDownColumn(grid, "AuthorityType", centerCommon.GetAuthorityType());
            this.SetDropDownColumn(grid, "UserId", this.GetOper());            
        }
        [Authorize]
        public ActionResult VouchAuthority()
        {
            var gridModel = new VouchAuthorityGridModel();
            SetupVouchAuthorityGridModel(gridModel.VouchAuthorityGrid);
            return View(gridModel);
        }
        public ActionResult VouchAuthority_RequestData()
        {
            var gridModel = new VouchAuthorityGridModel();
            SetupVouchAuthorityGridModel(gridModel.VouchAuthorityGrid);

            var units = from d in Uow.VouchAuthority.GetAll()
                        join d1 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d1.UserId into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        join d2 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == "AuthorityType") on d.AuthorityType equals d2.Value into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        select new { d.UserId, UserName = dd1s.FullName, d.AuthorityType, AuthorityTypeName = dd2s.Description };
            if (gridModel.VouchAuthorityGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.VouchAuthorityGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.VouchAuthorityGrid.ExportToExcel(units, "库存单据权限.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.VouchAuthorityGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.VouchAuthorityGrid.DataBind(units);
            }
        }
        public ActionResult VouchAuthority_EditData(DXInfo.Models.VouchAuthority vouchAuthority)
        {
            var gridModel = new VouchAuthorityGridModel();
            SetupVouchAuthorityGridModel(gridModel.VouchAuthorityGrid);
            try
            {
                if (gridModel.VouchAuthorityGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    Uow.VouchAuthority.Add(vouchAuthority);
                    Uow.Commit();
                }
                if (gridModel.VouchAuthorityGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldVouchAuthority = Uow.VouchAuthority.GetById(g=>g.UserId==vouchAuthority.UserId);
                    oldVouchAuthority.AuthorityType = vouchAuthority.AuthorityType;
                    Uow.VouchAuthority.Update(oldVouchAuthority);
                    Uow.Commit();
                }
                if (gridModel.VouchAuthorityGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var oldVouchAuthority = Uow.VouchAuthority.GetById(g=>g.UserId==vouchAuthority.UserId);
                    Uow.VouchAuthority.Delete(oldVouchAuthority);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.VouchAuthorityGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("VouchAuthority");
        }
        #endregion

        #region 获取操作员、计量单位、存货信息，换算率、货位
        
        private int GetVouchAuthority()
        {
            Guid userId = operId;
            DXInfo.Models.VouchAuthority vouchAuthority = Uow.VouchAuthority.GetById(g=>g.UserId==userId);
            if (vouchAuthority == null) return 2;
            return vouchAuthority.AuthorityType;
        }
        
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
        public JsonResult GetLocatorByWh(Guid? wh)
        {
            if (wh.HasValue)
            {
                var locators = Uow.Locator.GetAll().Where(w => w.Warehouse == wh).ToList();
                return Json(locators, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
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
        public JsonResult GetBatch(Guid wh, Guid inv,string batch)
        {
            DateTime dtNow = DateTime.Now.Date;

            var batchs1 = (from d in Uow.CurrentStock.GetAll()
                          where d.WhId == wh && d.InvId == inv
                          && d.Num>0 && !d.StopFlag
                          select new { Id = d.Batch, Name = d.Batch,d.InvalidDate }).ToList();
            //isShelfLife?d.InvalidDate > dtNow:
            if (isShelfLife)
            {
                batchs1 = batchs1.Where(w => w.InvalidDate > dtNow).ToList();
            }
            var batchs = (from d in batchs1
                          select new { Id=d.Id==null?"空":d.Id,Name=d.Name==null?"空":d.Name }).ToList();

            if (!string.IsNullOrEmpty(batch) && batchs.Where(w => w.Id == batch).Count() == 0)
                batchs.Insert(0, new { Id = batch, Name = batch });
            return Json(batchs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInvs()
        {
            var editList = (from u in Uow.Inventory.GetAll()
                            where u.InvType == (int)DXInfo.Models.InvType.StockManage && !u.IsInvalid
                            select new
                            {
                                u.Id,
                                u.Name
                            }).ToList();
            List<SelectListItem> lsi = new List<SelectListItem>();
            editList.ForEach(f=>lsi.Add(new SelectListItem(){Text=f.Name,Value=f.Id.ToString()}));

            lsi.Insert(0, new SelectListItem() { Text = "", Value = "" });
            return Json(lsi, JsonRequestBehavior.AllowGet);
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
                RdRecord rdRecordModel = new RdRecord();
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
                RdRecord rdRecordModel = new RdRecord();
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
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它入库单");
                            else
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它出库单");
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
                    DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, outVouchType, outRdType, outBusType, userId);

                    DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g=>g.Code=="003");
                    DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g=>g.Code=="003");
                    DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                    AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, inVouchType, inRdType, inBusType, userId);


                    transaction.Complete();
                }
            }
        }
        #endregion

        #region 收发记录        
        private string GetRdRecordCode(DXInfo.Models.VouchType vouchType)
        {
            Session[RdRecordsSession] = null;
            string vouchCode = businessCommon.GetVouchCode(vouchType.Code);
            return vouchCode;
        }
        private CodeVouchDate GetRdRecordCodeVouchDate(DXInfo.Models.VouchType vouchType)
        {
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = GetRdRecordCode(vouchType);
            cvd.VouchDateId = "RdDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            return cvd;
        }
        public JsonResult GetRdCode([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType)
        {
            //return Json(GetRdRecordCode(vouchType), JsonRequestBehavior.AllowGet);
            //BatchUnVerifyRdRecord();
            //BatchVerifyRdRecord();
            //BatchUnVerifyTransVouch();
            //BatchVerifyTransVouch();
            //BatchVerifyRdRecord();
            return Json(GetRdRecordCodeVouchDate(vouchType), JsonRequestBehavior.AllowGet);
        }
        private void CheckRdCodeDup(string code)
        {
            var icount = Uow.RdRecord.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }
        private RdRecord AddRdRecord(DXInfo.Models.VouchType vouchType, RdRecord rdRecordModel, 
            List<DXInfo.Models.RdRecords> lRdRecords,Guid userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckRdCodeDup(rdRecordModel.Code);
                DXInfo.Models.RdRecord rdRecord = Mapper.Map<RdRecord, DXInfo.Models.RdRecord>(rdRecordModel);
                
                rdRecord.VouchType = vouchType.Code;
                if (vouchType.Code == DXInfo.Models.VouchTypeCode.InitStock)
                {
                    rdRecord.InvInit = true;
                }
                rdRecord.Maker = userId;
                rdRecord.MakeDate = DateTime.Now;
                rdRecord.MakeTime = DateTime.Now;
                DXInfo.Models.RdType rdType = Uow.RdType.GetById(g=>g.Code==rdRecord.BusType);
                rdRecord.RdFlag = rdType.Flag;
                rdRecord.RdCode = rdType.Code;
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == rdRecord.WhId);
                rdRecord.DeptId = warehouse.Dept;

                Uow.RdRecord.Add(rdRecord);
                Uow.Commit();

                foreach (DXInfo.Models.RdRecords rdRecords in lRdRecords)
                {
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
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

                    if (!(vouchType.Code == DXInfo.Models.VouchTypeCode.PurchaseInStock || vouchType.Code == DXInfo.Models.VouchTypeCode.InitStock || vouchType.Code == DXInfo.Models.VouchTypeCode.ProductInStock))
                    {
                        DXInfo.Models.CurrentStock currentStock;
                        if (string.IsNullOrEmpty(rdRecords.Batch))
                        {
                            currentStock =
                                Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == null).FirstOrDefault();
                        }
                        else
                        {
                            currentStock =
                                Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                        }
                        if (vouchType.Code != DXInfo.Models.VouchTypeCode.SaleOutStock)
                        {
                            rdRecords.Price = currentStock.Price;
                        }
                        rdRecords.Amount = rdRecords.Num * rdRecords.Price;
                        rdRecords.MadeDate = currentStock.MadeDate;
                        rdRecords.ShelfLife = currentStock.ShelfLife;
                        rdRecords.ShelfLifeType = currentStock.ShelfLifeType;
                        rdRecords.InvalidDate = currentStock.InvalidDate;
                    }
                    else
                    {
                        
                        if (isShelfLife)
                        {
                            rdRecords.InvalidDate = getInvalidDate(rdRecords.ShelfLifeType.Value, rdRecords.ShelfLife.Value, rdRecords.MadeDate.Value);
                        }
                        if (rdRecords.Amount != rdRecords.Num * rdRecords.Price)
                        {
                            throw new DXInfo.Models.BusinessException("单价乘数量必须等于金额");
                        }
                        if (isBatch)
                        {
                            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                            if (currentStock != null)
                            {
                                if (rdRecords.InvalidDate != currentStock.InvalidDate || rdRecords.Price != currentStock.Price)
                                    throw new DXInfo.Models.BusinessException("同批次货物单价、生产日期、失效日期要一致！");
                            }
                        }
                    }
                    
                    rdRecords.RdId = rdRecord.Id;
                    if (isLocator)
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            var currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == rdRecord.WhId && w.Locator == rdRecords.Locator.Value && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                            if (rdRecords.Num > currentInvLocator.Num)
                            {
                                throw new DXInfo.Models.BusinessException("货位现存量不足");
                            }
                        }
                    }
                    Uow.RdRecords.Add(rdRecords);
                    Uow.Commit();
                }
                Uow.Commit();
                transaction.Complete();
                RdRecord retRecord = Mapper.Map<DXInfo.Models.RdRecord, RdRecord>(rdRecord);
                retRecord.IsModify = true;
                return retRecord;
            }
        }                
        private void AddInvLocatorByRdRecord(DXInfo.Models.RdRecord rdRecord, List<DXInfo.Models.RdRecords> lRdRecords, DXInfo.Models.VouchType vouchType)
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
                inInvLocator.SourceVouchType = vouchType.Code;
                inInvLocator.Salesman = rdRecord.Salesman;
                Uow.InvLocator.Add(inInvLocator);                
            }
        }
        public JsonResult AddRdRecord([Bind(Prefix = "rdRecord")]RdRecord rdRecordModel, [Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                if (businessCommon.IsBalance(rdRecordModel.RdDate.Value, rdRecordModel.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.RdRecords> lRdRecords = new List<DXInfo.Models.RdRecords>();
                if (Session[RdRecordsSession] != null)
                {
                    lRdRecords = Session[RdRecordsSession] as List<DXInfo.Models.RdRecords>;
                }
                Guid userId = operId;
                retRecord = AddRdRecord(vouchType, rdRecordModel, lRdRecords,userId);
                Session[RdRecordsSession] = null;
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
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyRdRecord([Bind(Prefix = "rdRecord")]RdRecord rdRecordModel)
        {
            try
            {
                DXInfo.Models.RdRecord oldRdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                if (businessCommon.IsBalance(oldRdRecord.RdDate, oldRdRecord.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldRdRecord.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldRdRecord = Mapper.Map<RdRecord, DXInfo.Models.RdRecord>(rdRecordModel, oldRdRecord);
                DXInfo.Models.RdType rdType = Uow.RdType.GetById(g=>g.Code==rdRecordModel.BusType);
                oldRdRecord.RdFlag = rdType.Flag;
                oldRdRecord.RdCode = rdType.Code;
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == rdRecordModel.WhId);
                oldRdRecord.DeptId = warehouse.Dept;
                oldRdRecord.Modifier = operId;
                oldRdRecord.ModifyDate = DateTime.Now;
                oldRdRecord.ModifyTime = DateTime.Now;
                Uow.RdRecord.Update(oldRdRecord);
                Uow.Commit();
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
            return Json(rdRecordModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType, [Bind(Prefix = "rdRecord")]RdRecord rdRecordModel)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                    if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (rdRecord == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (rdRecord.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    if (rdRecord.SourceId.HasValue)
                    {
                        throw new DXInfo.Models.BusinessException("请从来源单据删除，"+rdRecord.Memo);
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
            return NextRdRecord(vouchType, rdRecordModel);
        }
        private RdRecord VerifyRdRecord(Guid rdId, Guid userId, DXInfo.Models.VouchType vouchType)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                //Guid userId = operId;
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
                    rdRecord.Verifier = userId;
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
                        AddInvLocatorByRdRecord(rdRecord, lRdRecords, vouchType);
                    }
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
                    if (isLocator)
                    {
                        UpdateCurrentInvLocator(rdRecord, l2);
                    }
                    Uow.Commit();
                    transaction.Complete();
                    retRecord = Mapper.Map<DXInfo.Models.RdRecord, RdRecord>(rdRecord);
                    retRecord.IsModify = true;
                }
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                //return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
                throw bex;
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                //return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
                throw dex;
            }
            return retRecord;
            //return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VerifyRdRecord([Bind(Prefix = "rdRecord")]RdRecord rdRecordModel, [Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                Guid userId = operId;
                //using (TransactionScope transaction = new TransactionScope())
                //{                    
                //    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g=>g.Id==rdRecordModel.Id);
                //    if (rdRecord.IsVerify)
                //    {
                //        throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                //    }
                //    if (IsBalance(rdRecord.RdDate,rdRecord.WhId))
                //    {
                //        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                //    }
                //    rdRecord.IsVerify = true;
                //    rdRecord.Verifier = userId;
                //    rdRecord.VerifyDate = DateTime.Now;
                //    rdRecord.VerifyTime = DateTime.Now;
                //    Uow.RdRecord.Update(rdRecord);

                //    List<DXInfo.Models.RdRecords> lRdRecords = Uow.RdRecords.GetAll().Where(w => w.RdId == rdRecord.Id).ToList();
                //    var l1 = (from d in lRdRecords
                //             group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType } into g
                //             select new DXInfo.Models.RdRecords()
                //             {
                //                 InvId = g.Key.InvId,
                //                 MainUnit = g.Key.MainUnit,
                //                 STUnit = g.Key.STUnit,
                //                 ExchRate = g.Key.ExchRate,
                //                 Quantity = g.Sum(s => s.Quantity),
                //                 Num = g.Sum(s => s.Num),
                //                 Batch = g.Key.Batch,
                //                 Price = g.Key.Price,
                //                 Amount = g.Sum(s => s.Amount),
                //                 InvalidDate = g.Key.InvalidDate,
                //                 MadeDate = g.Key.MadeDate,
                //                 ShelfLife = g.Key.ShelfLife,
                //                 ShelfLifeType = g.Key.ShelfLifeType,
                //             }).ToList();
                //    UpdateCurrentStock(rdRecord, l1);
                //    AddInvLocatorByRdRecord(rdRecord, lRdRecords, vouchType);
                //    var l2 = (from d in lRdRecords
                //              group d by new { d.InvId, d.Batch, d.MainUnit, d.STUnit, d.ExchRate, d.Price, d.InvalidDate, d.MadeDate, d.ShelfLife, d.ShelfLifeType,d.Locator } into g
                //              select new DXInfo.Models.RdRecords()
                //              {
                //                  InvId = g.Key.InvId,
                //                  MainUnit = g.Key.MainUnit,
                //                  STUnit = g.Key.STUnit,
                //                  ExchRate = g.Key.ExchRate,
                //                  Quantity = g.Sum(s => s.Quantity),
                //                  Num = g.Sum(s => s.Num),
                //                  Batch = g.Key.Batch,
                //                  Price = g.Key.Price,
                //                  Amount = g.Sum(s => s.Amount),
                //                  InvalidDate = g.Key.InvalidDate,
                //                  MadeDate = g.Key.MadeDate,
                //                  ShelfLife = g.Key.ShelfLife,
                //                  ShelfLifeType = g.Key.ShelfLifeType,
                //                  Locator=g.Key.Locator,
                //              }).ToList();
                //    UpdateCurrentInvLocator(rdRecord, l2);
                    
                //    Uow.Commit();
                //    transaction.Complete();
                //    retRecord = Mapper.Map<DXInfo.Models.RdRecord, RdRecord>(rdRecord);
                //    retRecord.IsModify = true;
                //}
                retRecord = VerifyRdRecord(rdRecordModel.Id.Value, userId, vouchType);
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
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyRdRecord([Bind(Prefix = "rdRecord")]RdRecord rdRecordModel)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {                    
                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g=>g.Id==rdRecordModel.Id);
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
                    retRecord = Mapper.Map<DXInfo.Models.RdRecord, RdRecord>(rdRecord);
                    retRecord.IsModify = true;
                }
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
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }

        private DXInfo.Models.RdRecord GetRdRecordOrderByDescending(DXInfo.Models.VouchType vouchType,DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.RdRecord rdRecord = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime < makeTime && w.VouchType == vouchType.Code).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime < makeTime && w.VouchType == vouchType.Code && w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code && w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime < makeTime && w.VouchType == vouchType.Code
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return rdRecord;
        }
        private DXInfo.Models.RdRecord GetRdRecordOrderBy(DXInfo.Models.VouchType vouchType,DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.RdRecord rdRecord = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime > makeTime && w.VouchType == vouchType.Code).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime > makeTime && w.VouchType == vouchType.Code && w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code && w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.MakeTime > makeTime && w.VouchType == vouchType.Code
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        rdRecord = Uow.RdRecord.GetAll().Where(w => w.VouchType == vouchType.Code
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return rdRecord;
        }

        public JsonResult CurRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType, [Bind(Prefix = "rdRecord")]RdRecord rdRecordModel)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                if (rdRecordModel.Id == null)
                {
                    return StartRdRecord(vouchType);
                }
                DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                if (rdRecord == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retRecord = Mapper.Map<RdRecord>(rdRecord);
                retRecord.IsModify = true;
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
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType)
        {
            RdRecord retRdRecord = new RdRecord();
            try
            {
                var rdRecord = GetRdRecordOrderBy(vouchType,null);
                if (rdRecord == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retRdRecord = Mapper.Map<RdRecord>(rdRecord);
                retRdRecord.IsModify = true;
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
            return Json(retRdRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType, [Bind(Prefix = "rdRecord")]RdRecord rdRecordModel)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                if (rdRecordModel.Id == null)
                {
                    return StartRdRecord(vouchType);
                }
                var curRecord = Uow.RdRecord.GetById(g => g.Id == rdRecordModel.Id);
                DateTime makeTime = rdRecordModel.MakeTime.Value;
                if (curRecord != null)
                {
                    makeTime = curRecord.MakeTime;
                }
                var prevRecord = GetRdRecordOrderByDescending(vouchType, makeTime);
                    
                if (prevRecord == null)
                {
                    if (curRecord == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retRecord = Mapper.Map<RdRecord>(curRecord);
                }
                else
                {
                    retRecord = Mapper.Map<RdRecord>(prevRecord);
                }
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
            retRecord.IsModify = true;
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType, [Bind(Prefix = "rdRecord")]RdRecord rdRecord)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                if (rdRecord.Id == null)
                {
                    return EndRdRecord(vouchType);
                }
                var curRecord = Uow.RdRecord.GetById(g => g.Id == rdRecord.Id);
                DateTime makeTime = rdRecord.MakeTime.Value;
                if (curRecord != null)
                {
                    makeTime = curRecord.MakeTime;
                }
                var nextRecord = GetRdRecordOrderBy(vouchType,makeTime);
                    
                if (nextRecord == null)
                {
                    if (curRecord == null)
                    {
                        return PrevRdRecord(vouchType,rdRecord);
                    }
                    retRecord = Mapper.Map<RdRecord>(curRecord);
                }
                else
                {
                    retRecord = Mapper.Map<RdRecord>(nextRecord);
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            retRecord.IsModify = true;
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult EndRdRecord([Bind(Prefix = "vouchType")]DXInfo.Models.VouchType vouchType)
        {
            RdRecord retRecord = new RdRecord();
            try
            {
                DXInfo.Models.RdRecord lastRdRecord = GetRdRecordOrderByDescending(vouchType,null);
                if (lastRdRecord == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retRecord = Mapper.Map<RdRecord>(lastRdRecord);
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
            retRecord.IsModify = true;
            return Json(retRecord, JsonRequestBehavior.AllowGet);
        }
        private void SetupRdRecordsGridModel(JQGrid grid, string vouchType)
        {
            grid.DataUrl = Url.Action("RdRecords_RequestData");
            grid.EditUrl = Url.Action("RdRecords_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";

            JQGridColumn priceColumn = grid.Columns.Find(c => c.DataField == "Price");
            JQGridColumn amountColumn = grid.Columns.Find(c => c.DataField == "Amount");

            JQGridColumn madeDateColumn = grid.Columns.Find(c => c.DataField == "MadeDate");
            JQGridColumn shelfLifeColumn = grid.Columns.Find(c => c.DataField == "ShelfLife");
            JQGridColumn shelfLifeTypeColumn = grid.Columns.Find(c => c.DataField == "ShelfLifeType");
            JQGridColumn shelfLifeTypeNameColumn = grid.Columns.Find(c => c.DataField == "ShelfLifeTypeName");
            JQGridColumn invalidDateColumn = grid.Columns.Find(c => c.DataField == "InvalidDate");

            JQGridColumn AvaNumColumn = grid.Columns.Find(c => c.DataField == "AvaNum");
            JQGridColumn dueNumColumn = grid.Columns.Find(c => c.DataField == "DueNum");
            JQGridColumn batchColumn = grid.Columns.Find(c => c.DataField == "Batch");
            JQGridColumn locatorColumn = grid.Columns.Find(c => c.DataField == "Locator");
            JQGridColumn locatorNameColumn = grid.Columns.Find(c => c.DataField == "LocatorName");

            this.SetGridColumn(grid, "ShelfLife", isShelfLife);
            this.SetGridColumn(grid, "ShelfLifeType", isShelfLife);
            this.SetGridColumn(grid, "ShelfLifeTypeName", isShelfLife);
            this.SetGridColumn(grid, "InvalidDate", isShelfLife);

            this.SetGridColumn(grid, "Locator", isLocator);
            this.SetGridColumn(grid, "LocatorName", isLocator);

            if (isNecessaryBatch)
            {
                batchColumn.EditDialogFieldSuffix = "(*)";
                batchColumn.EditClientSideValidators = new List<JQGridEditClientSideValidator>()
                {
                    new RequiredValidator()                            
                };
            }
            dueNumColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };

            if (vouchType != DXInfo.Models.VouchTypeCode.OtherInStock)
            {
                dueNumColumn.Editable = false;
                dueNumColumn.Visible = false;
                dueNumColumn.Searchable = false;
            }

            if (!isShelfLife)
            {
                madeDateColumn.EditDialogFieldSuffix = "";
                madeDateColumn.EditClientSideValidators = new List<JQGridEditClientSideValidator>();
            }

            if (!(vouchType == DXInfo.Models.VouchTypeCode.PurchaseInStock
                || vouchType == DXInfo.Models.VouchTypeCode.InitStock
                || vouchType == DXInfo.Models.VouchTypeCode.ProductInStock
                //|| vouchType == DXInfo.Models.VouchTypeCode.SaleOutStock
                ))
            {
                if (vouchType != DXInfo.Models.VouchTypeCode.SaleOutStock)
                {
                    priceColumn.Editable = false;
                    priceColumn.Visible = false;
                    priceColumn.Searchable = false;
                    amountColumn.Visible = false;
                    amountColumn.Editable = false;
                    amountColumn.Searchable = false;
                }
                if (vouchType == DXInfo.Models.VouchTypeCode.OtherOutStock)
                {
                    if (otherOutStockPriceColumnVisible)
                    {
                        //priceColumn.Editable = true;
                        priceColumn.Visible = true;
                        //priceColumn.Searchable = true;
                    }
                    if (otherOutStockAmountColumnVisible)
                    {
                        amountColumn.Visible = true;
                        //amountColumn.Editable = true;
                        //amountColumn.Searchable = true;
                    }
                }
                madeDateColumn.Editable = false;
                shelfLifeColumn.Editable = false;
                shelfLifeTypeColumn.Editable = false;

                if (vouchType != DXInfo.Models.VouchTypeCode.OtherInStock)
                {
                    AvaNumColumn.Editable = true;
                    if (isBatch)
                    {
                        batchColumn.EditType = EditType.DropDown;
                        batchColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                        batchColumn.EditList.Add(new SelectListItem { Text = "选择批号", Value = "" });
                    }
                }
            }
            JQGridColumn invColumn = grid.Columns.Find(c => c.DataField == "InvId");
            invColumn.EditType = EditType.DropDown;
            invColumn.SearchType = SearchType.DropDown;
            if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                var editList = (from u in Uow.Inventory.GetAll()
                                where u.InvType == (int)DXInfo.Models.InvType.StockManage && !u.IsInvalid
                                select new
                                {
                                    u.Id,
                                    u.Code,
                                    u.Name
                                }).ToList();
                var list = editList.Select(s => new SelectListItem { Text = s.Code + "-" + s.Name, Value = s.Id.ToString() }).ToList();
                invColumn.EditList = list;
                invColumn.SearchList = list;
                invColumn.SearchList.Insert(0, new SelectListItem { Text = "", Value = "" });
            }

            if (isLocator)
            {
                locatorColumn.EditType = EditType.DropDown;
                locatorColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                locatorColumn.EditList.Add(new SelectListItem { Text = "选择货位", Value = "" });
            }

            if (isShelfLife)
            {
                this.SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());
            }

            grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "关联销售数据",
                Text = "关联销售数据",
                OnClick = "customButtonClicked",
                ButtonIcon = "ui-icon-extlink",
            });

            #region 显示数量金额的合计数
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

            //销售出库单单价折扣
            if (vouchType == DXInfo.Models.VouchTypeCode.SaleOutStock && this.isSaleDiscount)
            {
                string discount = "<div>";
                discount += "<input type='hidden' id='OriginPrice'/>";
                discount += "<input type='radio' name='discount' value='100' onclick='Discount(this)'>无</input>";
                discount += "<input type='radio' name='discount' value='90' onclick='Discount(this)'>9折</input>";
                discount += "<input type='radio' name='discount' value='80' onclick='Discount(this)'>8折</input>";
                discount += "<input type='radio' name='discount' value='70' onclick='Discount(this)'>7折</input>";
                discount += "<input type='radio' name='discount' value='60' onclick='Discount(this)'>6折</input>";
                discount += "<input type='text' style='width:40px' id='CustomDiscount' onkeypress='if (event.keyCode == 13 && $(\"#CustomDiscount\").val() && !isNaN($(\"#CustomDiscount\").val())) { Discount(this);}'/>折";
                discount += "</div>";
                priceColumn.EditDialogFieldSuffix = "(*)" + discount;
            }
            else
            {
                priceColumn.EditDialogFieldSuffix = "(*)";
            }

        }

        void grid_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            //throw new NotImplementedException();
            decimal num = 0;
            decimal amount = 0;
            foreach (dynamic q in e.FilterData)
            {
                num += q.Num;
                amount += q.Amount;
            }
            JQGridColumn numColumn = e.GridModel.Columns.Find(c => c.DataField == "Num");
            numColumn.FooterValue = num.ToString();
            JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            amountColumn.FooterValue = amount.ToString();
        }
        //public JsonResult AutoComplete()
        //{
        //    var data = from u in Uow.Inventory.GetAll()
        //               where u.InvType == (int)DXInfo.Models.InvType.StockManage && !u.IsInvalid
        //               select u;


        //    JQAutoComplete model = new JQAutoComplete();
        //    model.AutoCompleteMode = AutoCompleteMode.Contains;
        //    model.MinLength = 0;
        //    model.DataField = "Name";
        //    return model.DataBind(data);
        //}
        public ActionResult RdRecords_RequestData(Guid? searchString,string vouchType)
        {
            var gridModel = new RdRecordsGridModel();
            SetupRdRecordsGridModel(gridModel.RdRecordsGrid,vouchType);
            if (!searchString.HasValue)
            {
                List<DXInfo.Models.RdRecords> lrecords = new List<DXInfo.Models.RdRecords>();
                if(Session[RdRecordsSession] != null)
                    lrecords = Session[RdRecordsSession] as List<DXInfo.Models.RdRecords>;
                List<DXInfo.Models.Inventory> linventories = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage).ToList();
                List<DXInfo.Models.UnitOfMeasures> lunitOfMeasure = Uow.UnitOfMeasures.GetAll().ToList();
                List<DXInfo.Models.EnumTypeDescription> lenumTypeDescription = Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType).ToList();
                List<DXInfo.Models.Locator> llocator = Uow.Locator.GetAll().ToList();
                var records = from d in lrecords
                              join d1 in linventories on d.InvId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d3 in lunitOfMeasure on dd1s.StockUnit equals d3.Id into dd3
                              from dd3s in dd3.DefaultIfEmpty()
                              join d4 in lenumTypeDescription on dd1s.ShelfLifeType equals d4.Value into dd4
                              from dd4s in dd4.DefaultIfEmpty()
                              join d5 in llocator on d.Locator equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()

                              select new
                              {
                                  d.Id,
                                  d.RdId,
                                  d.InvId,
                                  InvName = dd1s.Name,
                                  Specs = dd1s.Specs,
                                  STUnitName = dd3s==null?"":dd3s.Name,
                                  d.DueNum,
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
                                  LocatorName = dd5s==null?"":dd5s.Name,
                                  AvaNum="",
                                  d.Memo,
                              };
                return gridModel.RdRecordsGrid.DataBind(records.AsQueryable());
            }
            else
            {
                var records = from d in Uow.RdRecords.GetAll()
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
                                  ShelfLifeTypeName = dd4s.Description,
                                  d.InvalidDate,
                                  d.Locator,
                                  LocatorName = dd5s.Name,
                                  AvaNum = "",
                                  d.Memo,
                              };
                return gridModel.RdRecordsGrid.DataBind(records);
            }
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
        public ActionResult RdRecords_EditData(DXInfo.Models.RdRecords rdRecords, string vouchType)
        {
            var gridModel = new RdRecordsGridModel();
            SetupRdRecordsGridModel(gridModel.RdRecordsGrid, vouchType);
            if (rdRecords.Batch == "空")
            {
                rdRecords.Batch = null;
            }
            if (gridModel.RdRecordsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                if (rdRecords.RdId != Guid.Empty)
                {
                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
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
                        {
                            return gridModel.RdRecordsGrid.ShowEditValidationMessage("请设置库存单位");
                            //throw new DXInfo.Models.BusinessException("请设置库存单位");
                        }
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        rdRecords.MainUnit = inv.MainUnit;
                        rdRecords.STUnit = inv.StockUnit.Value;
                        rdRecords.ExchRate = uom.Rate;
                        rdRecords.Quantity = rdRecords.Num * uom.Rate;
                    }

                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g=>g.Id==rdRecords.RdId);

                    if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                    {
                        return gridModel.RdRecordsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    if (!(vouchType == DXInfo.Models.VouchTypeCode.PurchaseInStock || vouchType == DXInfo.Models.VouchTypeCode.InitStock || vouchType == DXInfo.Models.VouchTypeCode.ProductInStock))
                    {
                        DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                        //rdRecords.Price = currentStock.Price;
                        if (vouchType != DXInfo.Models.VouchTypeCode.SaleOutStock)
                        {
                            rdRecords.Price = currentStock.Price;
                        }
                        rdRecords.Amount = rdRecords.Num * rdRecords.Price;
                        rdRecords.MadeDate = currentStock.MadeDate;
                        rdRecords.ShelfLife = currentStock.ShelfLife;
                        rdRecords.ShelfLifeType = currentStock.ShelfLifeType;
                        rdRecords.InvalidDate = currentStock.InvalidDate;
                    }
                    else
                    {
                        if (isShelfLife)
                        {
                            rdRecords.InvalidDate = getInvalidDate(rdRecords.ShelfLifeType.Value, rdRecords.ShelfLife.Value, rdRecords.MadeDate.Value);
                        }
                        if (rdRecords.Amount != rdRecords.Num * rdRecords.Price)
                        {
                            //throw new DXInfo.Models.BusinessException("单价乘数量必须等于金额");
                            return gridModel.RdRecordsGrid.ShowEditValidationMessage("单价乘数量必须等于金额");
                        }
                        if (isBatch)
                        {
                            DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                            if (currentStock != null)
                            {
                                if (rdRecords.InvalidDate != currentStock.InvalidDate || rdRecords.Price != currentStock.Price)
                                {
                                    if (isShelfLife)
                                    {
                                        //throw new DXInfo.Models.BusinessException("同批次货物单价、生产日期、失效日期要一致！");
                                        return gridModel.RdRecordsGrid.ShowEditValidationMessage("同批次货物单价、生产日期、失效日期要一致！");
                                    }
                                    else
                                    {
                                        return gridModel.RdRecordsGrid.ShowEditValidationMessage("同批次货物单价要一致！");
                                    }
                                }
                            }
                        }
                    }
                    if (isLocator)
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            var currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == rdRecord.WhId && w.Locator == rdRecords.Locator.Value && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                            if (rdRecords.Num > currentInvLocator.Num)
                            {
                                return gridModel.RdRecordsGrid.ShowEditValidationMessage("货位现存量不足");
                            }
                        }
                    }
                    Uow.RdRecords.Add(rdRecords);
                    Uow.Commit();
                }
                else
                {
                    List<DXInfo.Models.RdRecords> lRdRecords = new List<DXInfo.Models.RdRecords>();
                    if (Session[RdRecordsSession] != null)
                    {
                        lRdRecords = Session[RdRecordsSession] as List<DXInfo.Models.RdRecords>;
                    }
                    rdRecords.Id = Guid.NewGuid();
                    lRdRecords.Add(rdRecords);
                    Session[RdRecordsSession] = lRdRecords;
                }
            }
            if (gridModel.RdRecordsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (rdRecords.RdId != Guid.Empty)
                {
                    var oldRecords = Uow.RdRecords.GetById(g=>g.Id==rdRecords.Id);
                    oldRecords.InvId = rdRecords.InvId;
                    oldRecords.Memo = rdRecords.Memo;
                    oldRecords.Num = rdRecords.Num;
                    oldRecords.Batch = rdRecords.Batch;
                    oldRecords.Locator = rdRecords.Locator;

                    DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == rdRecords.InvId);
                    DXInfo.Models.MeasurementUnitGroup group = Uow.MeasurementUnitGroup.GetById(g => g.Id == inv.MeasurementUnitGroup);
                    if (group.Category == (int)DXInfo.Models.UnitGroupCategory.No)
                    {
                        oldRecords.MainUnit = inv.MainUnit;
                        oldRecords.STUnit = inv.MainUnit;
                        oldRecords.ExchRate = 1;
                        oldRecords.Quantity = rdRecords.Num;
                    }
                    else
                    {
                        if (!inv.StockUnit.HasValue)
                        {
                            return gridModel.RdRecordsGrid.ShowEditValidationMessage("请设置库存单位");
                            //throw new DXInfo.Models.BusinessException("请设置库存单位");
                        }
                        DXInfo.Models.UnitOfMeasures uom = Uow.UnitOfMeasures.GetById(g => g.Id == inv.StockUnit);
                        oldRecords.MainUnit = inv.MainUnit;
                        oldRecords.STUnit = inv.StockUnit.Value;
                        oldRecords.ExchRate = uom.Rate;
                        oldRecords.Quantity = rdRecords.Num * uom.Rate;
                    }


                    DXInfo.Models.RdRecord rdRecord = Uow.RdRecord.GetById(g=>g.Id==rdRecords.RdId);
                    if (businessCommon.IsBalance(rdRecord.RdDate, rdRecord.WhId))
                    {
                        return gridModel.RdRecordsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    if (!(vouchType == DXInfo.Models.VouchTypeCode.PurchaseInStock || vouchType == DXInfo.Models.VouchTypeCode.InitStock
                        || vouchType == DXInfo.Models.VouchTypeCode.ProductInStock
                        || vouchType == DXInfo.Models.VouchTypeCode.OtherInStock))
                    {
                        DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == rdRecord.WhId && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                        //oldRecords.Price = currentStock.Price;
                        if (vouchType == DXInfo.Models.VouchTypeCode.SaleOutStock)
                        {
                            oldRecords.Price = rdRecords.Price;
                        }
                        else
                        {
                            oldRecords.Price = currentStock.Price;
                        }
                        oldRecords.Amount = rdRecords.Num * oldRecords.Price;//rdRecords.Price;
                        oldRecords.MadeDate = currentStock.MadeDate;
                        oldRecords.ShelfLife = currentStock.ShelfLife;
                        oldRecords.ShelfLifeType = currentStock.ShelfLifeType;
                    }
                    else
                    {
                        if (vouchType == DXInfo.Models.VouchTypeCode.OtherInStock)
                        {
                            oldRecords.Amount = oldRecords.Price * rdRecords.Num;
                        }
                        else
                        {
                            oldRecords.Price = rdRecords.Price;
                            oldRecords.Amount = rdRecords.Amount;
                            oldRecords.MadeDate = rdRecords.MadeDate;
                            oldRecords.ShelfLife = rdRecords.ShelfLife;
                            oldRecords.ShelfLifeType = rdRecords.ShelfLifeType;
                            //zhh 20130618
                            if (oldRecords.Amount != oldRecords.Num * oldRecords.Price)
                            {
                                return gridModel.RdRecordsGrid.ShowEditValidationMessage("单价乘数量必须等于金额");
                                //throw new DXInfo.Models.BusinessException("单价乘数量必须等于金额");
                            }
                        }
                    }
                    
                    if (isShelfLife)
                    {
                        oldRecords.InvalidDate = getInvalidDate(oldRecords.ShelfLifeType.Value, oldRecords.ShelfLife.Value, oldRecords.MadeDate.Value);
                    }
                    if (isLocator)
                    {
                        if (rdRecord.RdFlag == 1)
                        {
                            var currentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == rdRecord.WhId && w.Locator == rdRecords.Locator.Value && w.InvId == rdRecords.InvId && w.Batch == rdRecords.Batch).FirstOrDefault();
                            if (rdRecords.Num > currentInvLocator.Num)
                            {
                                return gridModel.RdRecordsGrid.ShowEditValidationMessage("货位现存量不足");
                            }
                        }
                    }
                    Uow.RdRecords.Update(oldRecords);
                    Uow.Commit();
                }
                else
                {
                    if (Session[RdRecordsSession] != null)
                    {

                        List<DXInfo.Models.RdRecords> lRdRecords = Session[RdRecordsSession] as List<DXInfo.Models.RdRecords>;
                        DXInfo.Models.RdRecords oldRecord = lRdRecords.Find(delegate(DXInfo.Models.RdRecords sub) { return sub.Id == rdRecords.Id; });
                        oldRecord.InvId = rdRecords.InvId;
                        oldRecord.Num = rdRecords.Num;
                        oldRecord.Price = rdRecords.Price;
                        oldRecord.Amount = rdRecords.Amount;
                        oldRecord.Batch = rdRecords.Batch;
                        oldRecord.MadeDate = rdRecords.MadeDate;
                        oldRecord.ShelfLife = rdRecords.ShelfLife;
                        oldRecord.ShelfLifeType = rdRecords.ShelfLifeType;
                        oldRecord.Locator = rdRecords.Locator;
                        oldRecord.Memo = rdRecords.Memo;
                        Session[RdRecordsSession] = lRdRecords;
                    }
                }
            }
            if (gridModel.RdRecordsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                var oldRdReocrds = Uow.RdRecords.GetById(g => g.Id == rdRecords.Id);
                if (oldRdReocrds != null)
                {
                    var oldRdRecord = Uow.RdRecord.GetById(g => g.Id == oldRdReocrds.RdId);
                    if (oldRdRecord.SourceId.HasValue)
                    {
                        return gridModel.RdRecordsGrid.ShowEditValidationMessage(oldRdRecord.Memo + "其它单据生成，不能删除，请弃审其它单据来删除");
                    }
                    if (businessCommon.IsBalance(oldRdRecord.RdDate, oldRdRecord.WhId))
                    {
                        return gridModel.RdRecordsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    Uow.RdRecords.Delete(oldRdReocrds);
                    Uow.Commit();
                }
                if (Session[RdRecordsSession] != null)
                {

                    List<DXInfo.Models.RdRecords> lRdRecords = Session[RdRecordsSession] as List<DXInfo.Models.RdRecords>;
                    lRdRecords.RemoveAll(delegate(DXInfo.Models.RdRecords sub) { return sub.Id == rdRecords.Id; });
                    Session[RdRecordsSession] = lRdRecords;
                }
            }
            return RedirectToAction("PurchaseInStock");
        }
        private void SetupRdRecordGridModel(JQGrid grid,string vouchType)
        {
            grid.DataUrl = Url.Action("RdRecord_RequestData");
            grid.EditUrl = Url.Action("RdRecord_EditData");
            JQGridColumn priceColumn = grid.Columns.Find(c => c.DataField == "Price");
            JQGridColumn amountColumn = grid.Columns.Find(c => c.DataField == "Amount");
            if (!(vouchType == DXInfo.Models.VouchTypeCode.PurchaseInStock||vouchType==DXInfo.Models.VouchTypeCode.InitStock
                ||vouchType==DXInfo.Models.VouchTypeCode.ProductInStock))
            {
                priceColumn.Visible = false;
                amountColumn.Visible = false;
                if (vouchType == DXInfo.Models.VouchTypeCode.OtherOutStock)
                {
                    if (otherOutStockPriceColumnVisible)
                    {
                        priceColumn.Visible = true;
                    }
                    if (otherOutStockAmountColumnVisible)
                    {
                        amountColumn.Visible = true;
                    }
                }
            }

            JQGridColumn codeColumn = grid.Columns.Find(c => c.DataField == "Code");
            JQGridColumn rdDateColumn = grid.Columns.Find(c => c.DataField == "RdDate");
            JQGridColumn ARVCodeColumn = grid.Columns.Find(c => c.DataField == "ARVCode");
            JQGridColumn ArvDateColumn = grid.Columns.Find(c => c.DataField == "ArvDate");

            JQGridColumn VenIdColumn = grid.Columns.Find(c => c.DataField == "VenId");
            JQGridColumn VenNameColumn = grid.Columns.Find(c => c.DataField == "VenName");

            if (vouchType == "002" || vouchType == "004"||vouchType=="005")
            {
                codeColumn.HeaderText = "出库单号";
                rdDateColumn.HeaderText = "出库日期";
            }
            if (vouchType == "002" || vouchType == "006" || vouchType == "005" || vouchType == "004" || vouchType == "003"||vouchType=="007")
            {
                if (vouchType == "003")
                {
                    VenIdColumn.Visible = false;
                    VenIdColumn.Searchable = false;

                    VenIdColumn.Visible = false;
                    VenIdColumn.Searchable = false;

                    VenNameColumn.Visible = false;
                    VenNameColumn.Searchable = false;
                }
                else
                {
                    ARVCodeColumn.Visible = false;
                    ARVCodeColumn.Searchable = false;

                    ArvDateColumn.Visible = false;
                    ArvDateColumn.Searchable = false;
                }
                
            }
            if (vouchType == "006" || vouchType == "005" || vouchType == "004" ||vouchType=="007")
            {
                JQGridColumn SalesmanColumn = grid.Columns.Find(c => c.DataField == "Salesman");
                JQGridColumn SalesmanNameColumn = grid.Columns.Find(c => c.DataField == "SalesmanName");
                SalesmanColumn.Visible = false;
                SalesmanColumn.Searchable = false;

                SalesmanNameColumn.Visible = false;
                SalesmanNameColumn.Searchable = false;
            }

            JQGridColumn BusTypeColumn = grid.Columns.Find(c => c.DataField == "BusType");
            JQGridColumn BusTypeNameColumn = grid.Columns.Find(c => c.DataField == "BusTypeName");
            BusTypeColumn.Searchable = false;
            BusTypeNameColumn.Visible = false;
            if (vouchType == "004" || vouchType == "003")
            {
                string headerText = "入库类别";
                if (vouchType == "004") headerText = "出库类别";
                BusTypeColumn.HeaderText = headerText;
                BusTypeNameColumn.HeaderText = headerText;
                BusTypeColumn.Searchable = true;
                BusTypeNameColumn.Visible = true;
            }


            this.SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            this.SetDropDownColumn(grid, "VenId", this.GetVendor());
            this.SetDropDownColumn(grid, "Salesman", this.GetOper());
            this.SetDropDownColumn(grid, "BusType", centerCommon.GetBusType(vouchType));
            this.SetDropDownColumn(grid, "InvId", this.GetInventory());
            
            
            if (isShelfLife)
            {
                this.SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());                
            }
            this.SetBoolColumn(grid, "IsVerify");

            #region 显示数量、金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved+=new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

        }
        public ActionResult RdRecord_RequestData(string vouchType)
        {
            var gridModel = new RdRecordGridModel();
            SetupRdRecordGridModel(gridModel.RdRecordGrid, vouchType);

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

                          select new
                          {
                              dd6s.Id,
                              dd6s.VouchType,
                              dd6s.Code,
                              dd6s.RdDate,
                              dd6s.DeptId,
                              dd6s.WhId,
                              WhName=dd7s.Name,
                              dd6s.ARVCode,
                              dd6s.ArvDate,
                              dd6s.VenId,
                              VenName=dd8s.Name,
                              dd6s.BusType,
                              BusTypeName=dd11s.Name,
                              dd6s.Salesman,
                              SalesmanName=dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId=d.Id==null?dd6s.Id:d.Id,
                              InvId=d.InvId==null?Guid.Empty:d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnit=d.STUnit==null?Guid.Empty:d.STUnit,
                              STUnitName = dd3s.Name,
                              DueNum=d.DueNum==null?0:d.DueNum,
                              Num=d.Num==null?0:d.Num,
                              Price=d.Price==null?0:d.Price,
                              Amount = d.Amount == null ? 0 : d.Amount,
                              d.Batch,
                              d.MadeDate,
                              d.ShelfLife,
                              d.ShelfLifeType,
                              ShelfLifeTypeName = dd4s.Description,
                              d.InvalidDate,
                              Locator=d.Locator==null?Guid.Empty:d.Locator,
                              LocatorName = dd5s.Name,
                              SubMemo=d.Memo,
                          };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.RdRecordGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.RdRecordGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                string fileName = "";
                switch (vouchType)
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
                }
                gridModel.RdRecordGrid.ExportToExcel(records, fileName, gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.RdRecordGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.RdRecordGrid.DataBind(records);
            }
        }
        public ActionResult RdRecord_EditData(DXInfo.Models.RdRecords records,DXInfo.Models.VouchType vouchType)
        {
            var gridModel = new RdRecordsGridModel();
            SetupRdRecordGridModel(gridModel.RdRecordsGrid,vouchType.Code);
            return RedirectToAction("SearchRdRecord");
        }
        public ActionResult SearchRdRecord()
        {
            var gridModel = new RdRecordGridModel();            
            
            if (this.Request["vouchType"] != null)
            {
                string vouchType = this.Request["vouchType"];
                SetupRdRecordGridModel(gridModel.RdRecordGrid, vouchType);
                var vt = Uow.VouchType.GetById(g=>g.Code==vouchType);
                gridModel.vouchType = vt;
                if (vt != null)
                {
                    gridModel.RdRecordGrid.AppearanceSettings.Caption = "搜索"+vt.Name;
                    ViewBag.Title = "搜索"+vt.Name;
                }
            }
            
            return View(gridModel);
        }

        
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

        #region 采购入库单
        public ActionResult PurchaseInStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.PurchaseInStock);
            gridModel.IsBatch = isBatch;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.IsLocator = isLocator;
            gridModel.rdRecord.BusType = DXInfo.Models.VouchTypeCode.PurchaseInStock;
            gridModel.rdRecord.PTCode = "001";
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid,gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 销售出库单
        public ActionResult SaleOutStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.SaleOutStock);
            gridModel.IsBatch = isBatch;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.IsLocator = isLocator;
            gridModel.rdRecord.BusType = DXInfo.Models.VouchTypeCode.SaleOutStock;
            gridModel.rdRecord.STCode = "001";
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid,gridModel.vouchType.Code);
            return View(gridModel);
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
        private List<DXInfo.Models.RdRecords> Sell2(DateTime beginDate,DateTime endDate, Guid deptId)
        {
            List<DXInfo.Models.RdRecords> lRdRecords = new List<DXInfo.Models.RdRecords>();
            int invType = (int)DXInfo.Models.InvType.StockManage;
            var lConsumeList = (from d in Uow.ConsumeList.GetAll()
                                                        join d1 in Uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                                                        from dd1s in dd1.DefaultIfEmpty()

                                                        where dd1s.InvType == invType &&
                                                        d.CreateDate>=beginDate &&
                                                        d.CreateDate<=endDate
                                                        select new{
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
            DXInfo.Models.Depts dept = Uow.Depts.GetById(g=>g.DeptId==warehouse.Dept);
            if (dept == null)
            {
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = "部门未找到" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                {
                    Session[RdRecordsSession] = Sell(strdate, dept.DeptCode);
                }
                else
                {
                    DateTime beginDate = Convert.ToDateTime(strdate);
                    DateTime endDate = beginDate.AddDays(1);

                    Session[RdRecordsSession] = Sell2(beginDate,endDate, dept.DeptId);
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
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.ProductInStock);
            gridModel.rdRecord = new RdRecord();
            gridModel.rdRecord.BusType = "011";
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 材料出库单
        public ActionResult MaterialOutStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.MaterialOutStock);
            gridModel.rdRecord = new RdRecord();
            gridModel.rdRecord.BusType = "010";
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 其它入库单
        public ActionResult OtherInStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);            
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 其它出库单
        public ActionResult OtherOutStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 期初库存
        public ActionResult InitStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.InitStock);
            gridModel.rdRecord = new RdRecord();
            gridModel.rdRecord.BusType = "012";
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
        }
        #endregion

        #region 库存调拨单
        private void CheckTVCodeDup(string code)
        {
            var icount = Uow.TransVouch.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }        
        private TransVouch AddTransVouch(TransVouch transVouch, List<DXInfo.Models.TransVouchs> lTransVouchs,Guid userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckTVCodeDup(transVouch.Code);
                DXInfo.Models.TransVouch newTransVouch = Mapper.Map<TransVouch, DXInfo.Models.TransVouch>(transVouch);
                newTransVouch.Maker = userId;
                newTransVouch.MakeDate = DateTime.Now;
                newTransVouch.MakeTime = DateTime.Now;

                newTransVouch.OutRdCode = "006";
                newTransVouch.InRdCode = "003";
                DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == newTransVouch.InWhId);
                newTransVouch.InDeptId = inWarehouse.Dept;
                DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == newTransVouch.OutWhId);
                newTransVouch.OutDeptId = outWarehouse.Dept;

                Uow.TransVouch.Add(newTransVouch);
                Uow.Commit();
                foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
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


                    transVouchs.TVId = newTransVouch.Id;

                    DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == transVouch.OutWhId && w.InvId == transVouchs.InvId && w.Batch == transVouchs.Batch).FirstOrDefault();
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
                Uow.Commit();
                transaction.Complete();

                TransVouch retTransVouch = Mapper.Map<DXInfo.Models.TransVouch, TransVouch>(newTransVouch);
                retTransVouch.IsModify = true;
                return retTransVouch;
            }
        }
        public JsonResult AddTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                if (businessCommon.IsBalance(transVouch.TVDate.Value, transVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.TransVouchs> lTransVouchs = new List<DXInfo.Models.TransVouchs>();
                if (Session[TransVouchsSession] != null)
                {
                    lTransVouchs = Session[TransVouchsSession] as List<DXInfo.Models.TransVouchs>;
                }
                Guid userId = operId;
                retTransVouch = AddTransVouch(transVouch, lTransVouchs,userId);
                Session[TransVouchsSession] = null;
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            try
            {
                DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldTransVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldTransVouch = Mapper.Map<TransVouch, DXInfo.Models.TransVouch>(transVouch, oldTransVouch);
                oldTransVouch.Modifier = operId;
                oldTransVouch.ModifyDate = DateTime.Now;
                oldTransVouch.ModifyTime = DateTime.Now;
                Uow.TransVouch.Update(oldTransVouch);
                Uow.Commit();
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
            return Json(transVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {

                    DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                    if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (oldTransVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (oldTransVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    if (oldTransVouch.SourceId.HasValue)
                    {
                        throw new DXInfo.Models.BusinessException("请从来源单据删除，" + oldTransVouch.Memo);
                    }
                    Uow.TransVouch.Delete(oldTransVouch);

                    List<DXInfo.Models.TransVouchs> lTransVouchs = Uow.TransVouchs.GetAll().Where(w => w.TVId == transVouch.Id).ToList();
                    foreach (DXInfo.Models.TransVouchs transVouchs in lTransVouchs)
                    {
                        Uow.TransVouchs.Delete(transVouchs);
                    }
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return NextTransVouch(transVouch);
        }
        private Guid AddRdRecordByTransVouch(DXInfo.Models.TransVouch transVouch, List<DXInfo.Models.TransVouchs> lTransVouchs, 
            DXInfo.Models.VouchType vouchType, DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType,Guid userId)
        {
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.TransVouch, DXInfo.Models.RdRecord>(transVouch);
            rdRecord.SourceCode = transVouch.Code;
            rdRecord.SourceId = transVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = vouchType.Code;
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
            rdRecord.VouchType = vouchType.Code;
            rdRecord.Code = GetRdRecordCode(vouchType);
            rdRecord.Maker = userId;
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
        public JsonResult VerifyTransVouch(TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                Guid userId = operId;
                Guid rdId = Guid.Empty;
                DXInfo.Models.VouchType outVouchType;
                using (TransactionScope transaction = new TransactionScope())
                {
                    
                    DXInfo.Models.TransVouch oldTransVouch = Uow.TransVouch.GetById(g=>g.Id==transVouch.Id);
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
                    DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g=>g.Code=="006");
                    DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g=>g.Code=="006");
                    outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    rdId = AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, outVouchType, outRdType, outBusType,userId);
                    

                    DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g=>g.Code=="003");
                    DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g=>g.Code=="003");
                    DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                    AddRdRecordByTransVouch(oldTransVouch, lTransVouchs, inVouchType, inRdType, inBusType,userId);


                    transaction.Complete();
                    retTransVouch = Mapper.Map<TransVouch>(oldTransVouch);
                    retTransVouch.IsModify = true;
                }
                using (TransactionScope transaction = new TransactionScope())
                {
                    //出库单审核
                    VerifyRdRecord(rdId, userId, outVouchType);
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyTransVouch(TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                Guid userId = operId;
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
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它入库单");
                            else
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它出库单");
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
                    Uow.TransVouch.Update(OldTransVouch);

                    Uow.Commit();
                    transaction.Complete();
                    retTransVouch = Mapper.Map<TransVouch>(OldTransVouch);
                    retTransVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTransVouch()
        {
            Session[TransVouchsSession] = null;
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.TransVouch);
            cvd.VouchDateId = "TVDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            return Json(cvd, JsonRequestBehavior.AllowGet);
        }
        private void SetupTransVouchsGridModel(JQGrid grid)
        {
            this.SetUpGrid(grid);
            grid.DataUrl = Url.Action("TransVouchs_RequestData");
            grid.EditUrl = Url.Action("TransVouchs_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";
            this.SetDropDownColumn(grid, "InvId", this.GetInventory());
            
            if (isBatch)
            {
                JQGridColumn batchColumn = grid.Columns.Find(c => c.DataField == "Batch");
                batchColumn.EditType = EditType.DropDown;
                batchColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                batchColumn.EditList.Add(new SelectListItem { Text = "选择批号", Value = "" });
            }
            if (isLocator)
            {
                JQGridColumn column = grid.Columns.Find(c => c.DataField == "Locator");
                column.EditType = EditType.DropDown;
                column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                column.EditList.Add(new SelectListItem { Text = "选择货位", Value = "" });
            }

            #region 是否显示单价、金额
            this.SetGridColumn(grid, "Price", transVouchPriceColumnVisible);
            this.SetGridColumn(grid, "Amount", transVouchAmountColumnVisible);
            #endregion

            #region 显示数量金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved+=new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

        }
        public ActionResult TransVouchs_RequestData(Guid? searchString)
        {
            var gridModel = new TransVouchs();
            SetupTransVouchsGridModel(gridModel.TransVouchsGrid);
            if (!searchString.HasValue)
            {
                List<DXInfo.Models.TransVouchs> lTransVouchs = new List<DXInfo.Models.TransVouchs>();
                if (Session[TransVouchsSession] != null)
                    lTransVouchs = Session[TransVouchsSession] as List<DXInfo.Models.TransVouchs>;
                List<DXInfo.Models.Inventory> linventories = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage).ToList();
                List<DXInfo.Models.UnitOfMeasures> lunitOfMeasure = Uow.UnitOfMeasures.GetAll().ToList();
                List<DXInfo.Models.EnumTypeDescription> lenumTypeDescription = Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType).ToList();
                List<DXInfo.Models.Locator> llocator = Uow.Locator.GetAll().ToList();

                var records = from d in lTransVouchs
                              join d1 in linventories on d.InvId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d3 in lunitOfMeasure on dd1s.StockUnit equals d3.Id into dd3
                              from dd3s in dd3.DefaultIfEmpty()
                              join d4 in lenumTypeDescription on d.ShelfLifeType equals d4.Value into dd4
                              from dd4s in dd4.DefaultIfEmpty()
                              join d5 in llocator on d.Locator equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()

                              select new
                              {
                                  d.Id,
                                  d.TVId,
                                  d.InvId,
                                  InvName = dd1s.Name,
                                  Specs = dd1s.Specs,
                                  STUnitName = dd3s==null?"":dd3s.Name,
                                  d.Num,
                                  d.Price,
                                  d.Amount,
                                  d.Batch,
                                  d.MadeDate,
                                  d.ShelfLife,
                                  d.ShelfLifeType,
                                  ShelfLifeTypeName = dd4s==null?"":dd4s.Description,
                                  d.InvalidDate,
                                  d.Locator,
                                  LocatorName = dd5s==null?"":dd5s.Name,
                                  AvaNum = "",
                                  d.Memo,
                              };
                return gridModel.TransVouchsGrid.DataBind(records.AsQueryable());
            }
            else
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
                return gridModel.TransVouchsGrid.DataBind(records);
            }
        }
        public ActionResult TransVouchs_EditData(DXInfo.Models.TransVouchs transVouchs)
        {
            var gridModel = new TransVouchs();
            SetupTransVouchsGridModel(gridModel.TransVouchsGrid);

            if (gridModel.TransVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                if (transVouchs.TVId != Guid.Empty)
                {
                    DXInfo.Models.TransVouch transVouch = Uow.TransVouch.GetById(g => g.Id == transVouchs.TVId);
                    if (businessCommon.IsBalance(transVouch.TVDate, transVouch.OutWhId))
                    {
                        return gridModel.TransVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
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

                    DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == transVouch.OutWhId && w.InvId == transVouchs.InvId && w.Batch == transVouchs.Batch).FirstOrDefault();
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
                else
                {
                    List<DXInfo.Models.TransVouchs> ltransVouchs = new List<DXInfo.Models.TransVouchs>();
                    if (Session[TransVouchsSession] != null)
                    {
                        ltransVouchs = Session[TransVouchsSession] as List<DXInfo.Models.TransVouchs>;
                    }
                    transVouchs.Id = Guid.NewGuid();
                    ltransVouchs.Add(transVouchs);
                    Session[TransVouchsSession] = ltransVouchs;
                }
            }
            if (gridModel.TransVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (transVouchs.TVId != Guid.Empty)
                {
                    var oldTransVouchs = Uow.TransVouchs.GetById(g=>g.Id==transVouchs.Id);
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
                        return gridModel.TransVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                else
                {
                    if (Session[TransVouchsSession] != null)
                    {

                        List<DXInfo.Models.TransVouchs> lTransVouchs = Session[TransVouchsSession] as List<DXInfo.Models.TransVouchs>;
                        DXInfo.Models.TransVouchs oldTransVouchs = lTransVouchs.Find(delegate(DXInfo.Models.TransVouchs sub) { return sub.Id == transVouchs.Id; });
                        oldTransVouchs.InvId = transVouchs.InvId;
                        oldTransVouchs.Num = transVouchs.Num;
                        oldTransVouchs.Memo = transVouchs.Memo;
                        Session[TransVouchsSession] = lTransVouchs;
                    }
                }
            }
            if (gridModel.TransVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                var oldTransVouchs = Uow.TransVouchs.GetById(g => g.Id == transVouchs.Id);
                if (oldTransVouchs != null)
                {
                    var oldTransVouch = Uow.TransVouch.GetById(g => g.Id == oldTransVouchs.TVId);
                    if (businessCommon.IsBalance(oldTransVouch.TVDate, oldTransVouch.OutWhId))
                    {
                        return gridModel.TransVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    Uow.TransVouchs.Delete(oldTransVouchs);
                    Uow.Commit();
                }
                if (Session[TransVouchsSession] != null)
                {

                    List<DXInfo.Models.TransVouchs> lTransVouchs = Session[TransVouchsSession] as List<DXInfo.Models.TransVouchs>;
                    lTransVouchs.RemoveAll(delegate(DXInfo.Models.TransVouchs sub) { return sub.Id == transVouchs.Id; });
                    Session[TransVouchsSession] = lTransVouchs;
                }
            }
            return RedirectToAction("TransVouch");
        }
        [Authorize]
        public ActionResult TransVouch()
        {
            var gridModel = new TransVouchModel();
            gridModel.IsShelfLife = isShelfLife;
            gridModel.IsBatch = isBatch;
            gridModel.IsLocator = isLocator;
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.TransVouch);       
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var transVouchs = Uow.TransVouchs.GetById(g => g.Id == Id);
                if (transVouchs == null)
                {
                    gridModel.transVouch.Id = Id;
                }
                else
                {
                    gridModel.transVouch.Id = transVouchs.TVId;
                }
            }
            SetupTransVouchsGridModel(gridModel.transVouchs.TransVouchsGrid);
            return View(gridModel);
        }
        private DXInfo.Models.TransVouch GetTransVouchOrderByDescending(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.TransVouch transVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime < makeTime && w.OutDeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.OutDeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime < makeTime
                            && w.OutDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.OutDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return transVouch;
        }
        private DXInfo.Models.TransVouch GetTransVouchOrderBy(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.TransVouch transVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime > makeTime).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime > makeTime && w.OutDeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.OutDeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.MakeTime > makeTime 
                            && w.OutDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        transVouch = Uow.TransVouch.GetAll().Where(w => w.OutDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return transVouch;
        }
        public JsonResult CurTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                if (transVouch.Id == null)
                {
                    return StartTransVouch();
                }
                var curTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                if (curTransVouch == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retTransVouch = Mapper.Map<TransVouch>(curTransVouch);
                retTransVouch.IsModify = true;
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartTransVouch()
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                var firstTransVouch = GetTransVouchOrderBy(null);
                if (firstTransVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retTransVouch = Mapper.Map<TransVouch>(firstTransVouch);
                retTransVouch.IsModify = true;
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                if (transVouch.Id == null)
                {
                    return StartTransVouch();
                }
                var curTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                DateTime makeTime = transVouch.MakeTime.Value;
                if (curTransVouch != null)
                {
                    makeTime = curTransVouch.MakeTime;
                }
                var prevTransVouch = GetTransVouchOrderByDescending(makeTime);//Uow.TransVouch.Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                if (prevTransVouch == null)
                {
                    if (curTransVouch == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retTransVouch = Mapper.Map<TransVouch>(curTransVouch);
                    retTransVouch.IsModify = true;
                }
                else
                {
                    retTransVouch = Mapper.Map<TransVouch>(prevTransVouch);
                    retTransVouch.IsModify = true;
                }
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextTransVouch([Bind(Prefix = "transVouch")]TransVouch transVouch)
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                if (transVouch.Id == null)
                {
                    return EndTransVouch();
                }
                var curTransVouch = Uow.TransVouch.GetById(g => g.Id == transVouch.Id);
                DateTime makeTime = transVouch.MakeTime.Value;
                if (curTransVouch != null)
                {
                    makeTime = curTransVouch.MakeTime;
                }
                var nextTransVouch = GetTransVouchOrderBy(makeTime);
                if (nextTransVouch == null)
                {
                    if (curTransVouch == null)
                    {
                        return PrevTransVouch(transVouch);
                    }
                    retTransVouch = Mapper.Map<TransVouch>(curTransVouch);
                    retTransVouch.IsModify = true;
                }
                else
                {
                    retTransVouch = Mapper.Map<TransVouch>(nextTransVouch);
                    retTransVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EndTransVouch()
        {
            TransVouch retTransVouch = new TransVouch();
            try
            {
                var lastTransVouch = GetTransVouchOrderByDescending(null);
                if (lastTransVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retTransVouch = Mapper.Map<TransVouch>(lastTransVouch);
                retTransVouch.IsModify = true;
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
            return Json(retTransVouch, JsonRequestBehavior.AllowGet);
        }
        private void SetupTransVouchGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("TransVouch_RequestData");
            grid.EditUrl = Url.Action("TransVouch_EditData");

            this.SetDropDownColumn(grid, "OutWhId", centerCommon.GetWarehouse());
            this.SetDropDownColumn(grid, "InWhId", centerCommon.GetWarehouse());
            this.SetDropDownColumn(grid, "Salesman", this.GetOper());
            this.SetDropDownColumn(grid, "InvId", this.GetInventory());

            if (isShelfLife)
            {
                this.SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());                
            }
            this.SetBoolColumn(grid, "IsVerify");
            #region 是否显示单价、金额
            this.SetGridColumn(grid, "Price", transVouchPriceColumnVisible);
            this.SetGridColumn(grid, "Amount", transVouchAmountColumnVisible);
            #endregion

            #region 显示数量金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

            this.SetStockColumn(grid);
        }
        public ActionResult TransVouch_RequestData()
        {
            var gridModel = new TransVouchGridModel();
            SetupTransVouchGridModel(gridModel.TransVouchGrid);

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
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.TransVouchGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.TransVouchGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.TransVouchGrid.ExportToExcel(records, "库存调拨单.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.TransVouchGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.TransVouchGrid.DataBind(records);
            }
        }
        public ActionResult TransVouch_EditData()
        {
            var gridModel = new TransVouchGridModel();
            SetupTransVouchGridModel(gridModel.TransVouchGrid);
            return RedirectToAction("SearchTransVouch");
        }
        public ActionResult SearchTransVouch()
        {
            var gridModel = new TransVouchGridModel();
            SetupTransVouchGridModel(gridModel.TransVouchGrid);
            return View(gridModel);
        }
        #endregion

        #region 不合格品记录单
        private void CheckSVCodeDup(string code)
        {
            var icount = Uow.ScrapVouch.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }        
        private ScrapVouch AddScrapVouch(ScrapVouch scrapVouch, List<DXInfo.Models.ScrapVouchs> lScrapVouchs,Guid userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckSVCodeDup(scrapVouch.Code);
                DXInfo.Models.ScrapVouch newScrapVouch = Mapper.Map<DXInfo.Models.ScrapVouch>(scrapVouch);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newScrapVouch.WhId);
                newScrapVouch.DeptId = warehouse.Dept;
                newScrapVouch.Maker = userId;
                newScrapVouch.MakeDate = DateTime.Now;
                newScrapVouch.MakeTime = DateTime.Now;

                Uow.ScrapVouch.Add(newScrapVouch);
                Uow.Commit();
                foreach (DXInfo.Models.ScrapVouchs scrapVouchs in lScrapVouchs)
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
                    DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == scrapVouch.WhId && w.InvId == scrapVouchs.InvId && w.Batch == scrapVouchs.Batch).FirstOrDefault();
                    scrapVouchs.Price = currentStock.Price;
                    scrapVouchs.Amount = scrapVouchs.Num * scrapVouchs.Price;
                    scrapVouchs.MadeDate = currentStock.MadeDate;
                    scrapVouchs.ShelfLife = currentStock.ShelfLife;
                    scrapVouchs.ShelfLifeType = currentStock.ShelfLifeType;
                    if (isShelfLife)
                    {
                        scrapVouchs.InvalidDate = getInvalidDate(scrapVouchs.ShelfLifeType.Value, scrapVouchs.ShelfLife.Value, scrapVouchs.MadeDate.Value);
                    }
                    scrapVouchs.SVId = newScrapVouch.Id;
                    Uow.ScrapVouchs.Add(scrapVouchs);
                    Uow.Commit();

                }
                Uow.Commit();
                transaction.Complete();

                ScrapVouch retScrapVouch = Mapper.Map<ScrapVouch>(newScrapVouch);
                retScrapVouch.IsModify = true;
                return retScrapVouch;
            }
        }
        public JsonResult AddScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                if (businessCommon.IsBalance(scrapVouch.SVDate.Value, scrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.ScrapVouchs> lScrapVouchs = new List<DXInfo.Models.ScrapVouchs>();
                if (Session[ScrapVouchsSession] != null)
                {
                    lScrapVouchs = Session[ScrapVouchsSession] as List<DXInfo.Models.ScrapVouchs>;
                }
                Guid userId = operId;
                retScrapVouch = AddScrapVouch(scrapVouch, lScrapVouchs,userId);
                Session[ScrapVouchsSession] = null;
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            try
            {
                DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouch.Id);
                if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldScrapVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldScrapVouch = Mapper.Map<ScrapVouch, DXInfo.Models.ScrapVouch>(scrapVouch, oldScrapVouch);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == oldScrapVouch.WhId);
                oldScrapVouch.DeptId = warehouse.Dept;
                oldScrapVouch.Modifier = operId;
                oldScrapVouch.ModifyDate = DateTime.Now;
                oldScrapVouch.ModifyTime = DateTime.Now;
                Uow.ScrapVouch.Update(oldScrapVouch);
                Uow.Commit();
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
            return Json(scrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouch.Id);
                    if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (oldScrapVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (oldScrapVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    Uow.ScrapVouch.Delete(oldScrapVouch);

                    List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Uow.ScrapVouchs.GetAll().Where(w => w.SVId == scrapVouch.Id).ToList();
                    foreach (DXInfo.Models.ScrapVouchs scrapVouchs in lScrapVouchs)
                    {
                        Uow.ScrapVouchs.Delete(scrapVouchs);
                    }
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return NextScrapVouch(scrapVouch);
        }
        private void AddRdRecordByScrapVouch(DXInfo.Models.ScrapVouch scrapVouch,
            List<DXInfo.Models.ScrapVouchs> lScrapVouchs,
            DXInfo.Models.VouchType vouchType,DXInfo.Models.RdType rdType,DXInfo.Models.BusType busType,Guid userId)
        {
            
            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.ScrapVouch, DXInfo.Models.RdRecord>(scrapVouch);
            rdRecord.SourceCode = scrapVouch.Code;
            rdRecord.SourceId = scrapVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = vouchType.Code;
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
            rdRecord.VouchType = vouchType.Code;
            rdRecord.Code = GetRdRecordCode(vouchType);
            rdRecord.Maker = userId;
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
        public JsonResult VerifyScrapVouch(ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {                    
                    DXInfo.Models.ScrapVouch oldScrapVouch = Uow.ScrapVouch.GetById(g=>g.Id==scrapVouch.Id);
                    if (oldScrapVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                    }
                    if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    oldScrapVouch.IsVerify = true;
                    oldScrapVouch.Verifier = userId;
                    oldScrapVouch.VerifyDate = DateTime.Now;
                    oldScrapVouch.VerifyTime = DateTime.Now;
                    Uow.ScrapVouch.Update(oldScrapVouch);

                    List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Uow.ScrapVouchs.GetAll().Where(w => w.SVId == oldScrapVouch.Id).ToList();

                    DXInfo.Models.RdType rdType = Uow.RdType.GetById(g=>g.Code=="008");
                    DXInfo.Models.BusType busType = Uow.BusType.GetById(g=>g.Code=="008");
                    DXInfo.Models.VouchType vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    AddRdRecordByScrapVouch(oldScrapVouch, lScrapVouchs, vouchType, rdType,busType,userId);

                    transaction.Complete();
                    retScrapVouch = Mapper.Map<ScrapVouch>(oldScrapVouch);
                    retScrapVouch.IsModify = true;
                }
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyScrapVouch(ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.ScrapVouch OldScrapVouch = Uow.ScrapVouch.GetById(g=>g.Id==scrapVouch.Id);
                    if (businessCommon.IsBalance(OldScrapVouch.SVDate, OldScrapVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == scrapVouch.Id).ToList();
                    foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                    {
                        if (rdRecord.IsVerify)
                        {
                            throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它出库单");
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
                    OldScrapVouch.Modifier = userId;
                    OldScrapVouch.ModifyDate = DateTime.Now;
                    OldScrapVouch.ModifyTime = DateTime.Now;
                    Uow.ScrapVouch.Update(OldScrapVouch);
                    Uow.Commit();
                    transaction.Complete();
                    retScrapVouch = Mapper.Map<ScrapVouch>(OldScrapVouch);
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetScrapVouch()
        {
            Session[ScrapVouchsSession] = null;
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.ScrapVouch);
            cvd.VouchDateId = "SVDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            return Json(cvd, JsonRequestBehavior.AllowGet);
        }
        private void SetupScrapVouchsGridModel(JQGrid grid)
        {
            this.SetUpGrid(grid);
            grid.DataUrl = Url.Action("ScrapVouchs_RequestData");
            grid.EditUrl = Url.Action("ScrapVouchs_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";

            this.SetDropDownColumn(grid, "InvId", this.GetInventory());

            if (isLocator)
            {
                JQGridColumn column = grid.Columns.Find(c => c.DataField == "Locator");
                column.EditType = EditType.DropDown;
                column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                column.EditList.Add(new SelectListItem { Text = "选择货位", Value = "" });
            }

            if (isBatch)
            {
                JQGridColumn batchColumn = grid.Columns.Find(c => c.DataField == "Batch");
                batchColumn.EditType = EditType.DropDown;
                batchColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                batchColumn.EditList.Add(new SelectListItem { Text = "选择批号", Value = "" });
            }
            #region 是否显示单价、金额
            this.SetGridColumn(grid, "Price", transVouchPriceColumnVisible);
            this.SetGridColumn(grid, "Amount", transVouchAmountColumnVisible);
            #endregion

            #region 显示数量金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

            SetStockColumn(grid);
        }
        public ActionResult ScrapVouchs_RequestData(Guid? searchString)
        {
            var gridModel = new ScrapVouchs();
            SetupScrapVouchsGridModel(gridModel.ScrapVouchsGrid);
            if (!searchString.HasValue)
            {
                List<DXInfo.Models.ScrapVouchs> lScrapVouchs = new List<DXInfo.Models.ScrapVouchs>();
                if (Session[ScrapVouchsSession] != null)
                    lScrapVouchs = Session[ScrapVouchsSession] as List<DXInfo.Models.ScrapVouchs>;
                List<DXInfo.Models.Inventory> linventories = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage).ToList();
                List<DXInfo.Models.UnitOfMeasures> lunitOfMeasure = Uow.UnitOfMeasures.GetAll().ToList();
                List<DXInfo.Models.EnumTypeDescription> lenumTypeDescription = Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType).ToList();
                List<DXInfo.Models.Locator> llocator = Uow.Locator.GetAll().ToList();
                var records = from d in lScrapVouchs
                              join d1 in linventories on d.InvId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d3 in lunitOfMeasure on dd1s.StockUnit equals d3.Id into dd3
                              from dd3s in dd3.DefaultIfEmpty()
                              join d4 in lenumTypeDescription on d.ShelfLifeType equals d4.Value into dd4
                              from dd4s in dd4.DefaultIfEmpty()
                              join d5 in llocator on d.Locator equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()
                              select new
                              {
                                  d.Id,
                                  d.SVId,
                                  d.InvId,
                                  InvName = dd1s.Name,
                                  Specs = dd1s.Specs,
                                  STUnitName = dd3s == null ? "" : dd3s.Name,
                                  d.Num,
                                  d.Price,
                                  d.Amount,
                                  d.Batch,
                                  MadeDate=d.MadeDate,
                                  ShelfLife = d.ShelfLife,
                                  ShelfLifeType = d.ShelfLifeType,
                                  ShelfLifeTypeName= dd4s==null?"":dd4s.Description,
                                  InvalidDate=d.InvalidDate,
                                  d.Locator,
                                  LocatorName = dd5s==null?"":dd5s.Name,
                                  AvaNum="",
                                  d.Memo,
                              };
                return gridModel.ScrapVouchsGrid.DataBind(records.AsQueryable());
            }
            else
            {
                var records = from d in Uow.ScrapVouchs.GetAll()
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
                                  ShelfLifeTypeName = dd4s.Description,
                                  d.InvalidDate,
                                  d.Locator,
                                  LocatorName = dd5s.Name,
                                  AvaNum="",
                                  d.Memo,
                              };
                return gridModel.ScrapVouchsGrid.DataBind(records);
            }
        }
        public ActionResult ScrapVouchs_EditData(DXInfo.Models.ScrapVouchs scrapVouchs)
        {
            var gridModel = new ScrapVouchs();
            SetupScrapVouchsGridModel(gridModel.ScrapVouchsGrid);
            if (gridModel.ScrapVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                if (scrapVouchs.SVId != Guid.Empty)
                {
                    var oldScrapVouch = Uow.ScrapVouch.GetById(g=>g.Id==scrapVouchs.SVId);
                    if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                    {
                        return gridModel.ScrapVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
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
                    DXInfo.Models.CurrentStock currentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == oldScrapVouch.WhId && w.InvId == scrapVouchs.InvId && w.Batch == scrapVouchs.Batch).FirstOrDefault();
                    scrapVouchs.Price = currentStock.Price;
                    scrapVouchs.Amount = scrapVouchs.Num * scrapVouchs.Price;
                    scrapVouchs.MadeDate = currentStock.MadeDate;
                    scrapVouchs.ShelfLife = currentStock.ShelfLife;
                    scrapVouchs.ShelfLifeType = currentStock.ShelfLifeType;
                    scrapVouchs.InvalidDate = getInvalidDate(scrapVouchs.ShelfLifeType.Value, scrapVouchs.ShelfLife.Value, scrapVouchs.MadeDate.Value);

                    Uow.ScrapVouchs.Add(scrapVouchs);
                    Uow.Commit();
                }
                else
                {
                    List<DXInfo.Models.ScrapVouchs> lscrapVouchs = new List<DXInfo.Models.ScrapVouchs>();
                    if (Session[ScrapVouchsSession] != null)
                    {
                        lscrapVouchs = Session[ScrapVouchsSession] as List<DXInfo.Models.ScrapVouchs>;
                    }
                    scrapVouchs.Id = Guid.NewGuid();
                    lscrapVouchs.Add(scrapVouchs);
                    Session[ScrapVouchsSession] = lscrapVouchs;
                }
            }
            if (gridModel.ScrapVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (scrapVouchs.SVId != Guid.Empty)
                {
                    var oldScrapVouchs = Uow.ScrapVouchs.GetById(g=>g.Id==scrapVouchs.Id);
                    var oldScrapVouch = Uow.ScrapVouch.GetById(g=>g.Id==scrapVouchs.SVId);
                    if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                    {
                        return gridModel.ScrapVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                else
                {
                    if (Session[ScrapVouchsSession] != null)
                    {

                        List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Session[ScrapVouchsSession] as List<DXInfo.Models.ScrapVouchs>;
                        DXInfo.Models.ScrapVouchs oldScrapVouchs = lScrapVouchs.Find(delegate(DXInfo.Models.ScrapVouchs sub) { return sub.Id == scrapVouchs.Id; });
                        oldScrapVouchs.InvId = scrapVouchs.InvId;
                        oldScrapVouchs.Num = scrapVouchs.Num;
                        oldScrapVouchs.Batch = scrapVouchs.Batch;
                        oldScrapVouchs.Locator = scrapVouchs.Locator;
                        oldScrapVouchs.Memo = scrapVouchs.Memo;
                        Session[ScrapVouchsSession] = lScrapVouchs;
                    }
                }
            }
            if (gridModel.ScrapVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                var oldScrapVouchs = Uow.ScrapVouchs.GetById(g => g.Id == scrapVouchs.Id);
                if (oldScrapVouchs != null)
                {
                    var oldScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == oldScrapVouchs.SVId);
                    if (businessCommon.IsBalance(oldScrapVouch.SVDate, oldScrapVouch.WhId))
                    {
                        return gridModel.ScrapVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    Uow.ScrapVouchs.Delete(oldScrapVouchs);
                    Uow.Commit();
                }
                if (Session[ScrapVouchsSession] != null)
                {
                    List<DXInfo.Models.ScrapVouchs> lScrapVouchs = Session[ScrapVouchsSession] as List<DXInfo.Models.ScrapVouchs>;
                    lScrapVouchs.RemoveAll(delegate(DXInfo.Models.ScrapVouchs sub) { return sub.Id == scrapVouchs.Id; });
                    Session[ScrapVouchsSession] = lScrapVouchs;
                }
            }
            return RedirectToAction("ScrapVouch");
        }
        public ActionResult ScrapVouch()
        {
            var gridModel = new ScrapVouchModel();
            gridModel.IsBatch = isBatch;
            gridModel.IsLocator = isLocator;
            gridModel.IsShelfLife = isShelfLife;

            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.ScrapVouch);           
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var scrapVouchs = Uow.ScrapVouchs.GetById(g => g.Id == Id);
                if (scrapVouchs == null)
                {
                    gridModel.scrapVouch.Id = Id;
                }
                else
                {
                    gridModel.scrapVouch.Id = scrapVouchs.SVId;
                }
            }
            SetupScrapVouchsGridModel(gridModel.scrapVouchs.ScrapVouchsGrid);
            return View(gridModel);
        }
        private DXInfo.Models.ScrapVouch GetScrapVouchOrderByDescending(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.ScrapVouch scrapVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime < makeTime && w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime < makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return scrapVouch;
        }
        private DXInfo.Models.ScrapVouch GetScrapVouchOrderBy(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.ScrapVouch scrapVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime > makeTime).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime > makeTime && w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.MakeTime > makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        scrapVouch = Uow.ScrapVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return scrapVouch;
        }
        public JsonResult CurScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                if (scrapVouch.Id == null)
                {
                    return StartScrapVouch();
                }
                var curScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouch.Id);
                if (curScrapVouch == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retScrapVouch = Mapper.Map<ScrapVouch>(curScrapVouch);
                retScrapVouch.IsModify = true;
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartScrapVouch()
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                var firstScrapVouch = GetScrapVouchOrderBy(null);
                if (firstScrapVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retScrapVouch = Mapper.Map<ScrapVouch>(firstScrapVouch);
                retScrapVouch.IsModify = true;
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                if (scrapVouch.Id == null)
                {
                    return StartScrapVouch();
                }
                var curScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouch.Id);
                DateTime makeTime = scrapVouch.MakeTime.Value;
                if (curScrapVouch != null)
                {
                    makeTime = curScrapVouch.MakeTime;
                }
                var prevScrapVouch = GetScrapVouchOrderByDescending(makeTime);
                if (prevScrapVouch == null)
                {
                    if (curScrapVouch == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retScrapVouch = Mapper.Map<ScrapVouch>(curScrapVouch);
                    retScrapVouch.IsModify = true;
                }
                else
                {
                    retScrapVouch = Mapper.Map<ScrapVouch>(prevScrapVouch);
                    retScrapVouch.IsModify = true;
                }
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextScrapVouch([Bind(Prefix = "scrapVouch")]ScrapVouch scrapVouch)
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                if (scrapVouch.Id == null)
                {
                    return EndScrapVouch();
                }
                var curScrapVouch = Uow.ScrapVouch.GetById(g => g.Id == scrapVouch.Id);
                DateTime makeTime = scrapVouch.MakeTime.Value;
                if (curScrapVouch != null)
                {
                    makeTime = curScrapVouch.MakeTime;
                }
                var nextScrapVouch = GetScrapVouchOrderBy(null);
                if (nextScrapVouch == null)
                {
                    if (curScrapVouch == null)
                    {
                        return PrevScrapVouch(scrapVouch);
                    }
                    retScrapVouch = Mapper.Map<ScrapVouch>(curScrapVouch);
                    retScrapVouch.IsModify = true;
                }
                else
                {
                    retScrapVouch = Mapper.Map<ScrapVouch>(nextScrapVouch);
                    retScrapVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EndScrapVouch()
        {
            ScrapVouch retScrapVouch = new ScrapVouch();
            try
            {
                var lastScrapVouch = GetScrapVouchOrderByDescending(null);
                if (lastScrapVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retScrapVouch = Mapper.Map<ScrapVouch>(lastScrapVouch);
                retScrapVouch.IsModify = true;
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
            return Json(retScrapVouch, JsonRequestBehavior.AllowGet);
        }
        private void SetupScrapVouchGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ScrapVouch_RequestData");
            grid.EditUrl = Url.Action("ScrapVouch_EditData");

            SetUpGrid(grid);
            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsVerify");
            #region 是否显示单价、金额
            SetGridColumn(grid, "Price", transVouchPriceColumnVisible);
            SetGridColumn(grid, "Amount", transVouchAmountColumnVisible);
            #endregion

            #region 显示数量金额合计
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(grid_DataResolved);
            #endregion

            this.SetStockColumn(grid);
        }
        public ActionResult ScrapVouch_RequestData()
        {
            var gridModel = new ScrapVouchGridModel();
            SetupScrapVouchGridModel(gridModel.ScrapVouchGrid);

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
                          };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.ScrapVouchGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.ScrapVouchGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.ScrapVouchGrid.ExportToExcel(records, "库存不合格品记录单.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.ScrapVouchGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.ScrapVouchGrid.DataBind(records);
            }
        }
        public ActionResult ScrapVouch_EditData()
        {
            var gridModel = new ScrapVouchGridModel();
            SetupScrapVouchGridModel(gridModel.ScrapVouchGrid);
            return RedirectToAction("SearchScrapVouch");
        }
        public ActionResult SearchScrapVouch()
        {
            var gridModel = new ScrapVouchGridModel();
            SetupScrapVouchGridModel(gridModel.ScrapVouchGrid);
            return View(gridModel);
        }
        #endregion

        #region 库存月结周期
        private void SetupPeriodGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Period_RequestData");
            grid.EditUrl = Url.Action("Period_EditData");
        }
        [Authorize]
        public ActionResult Period()
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            return View(gridModel);
        }
        public ActionResult Period_RequestData()
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            var period = from d in Uow.Period.GetAll()
                       select d;
            if (gridModel.PeriodGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.PeriodGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.PeriodGrid.ExportToExcel(period, "库存月结周期.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.PeriodGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.PeriodGrid.DataBind(period);
            }
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
        public ActionResult Period_EditData(DXInfo.Models.Period period)
        {
            var gridModel = new PeriodGridModel();
            SetupPeriodGridModel(gridModel.PeriodGrid);
            try
            {
                if (gridModel.PeriodGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    //using (var context = db)
                    //{
                    if (CheckPeriodCodeDup(period.Code))
                    {
                        return gridModel.PeriodGrid.ShowEditValidationMessage("编码重复");

                    }
                    if (CheckPeriodDateDup(period.BeginDate, period.EndDate))
                    {
                        return gridModel.PeriodGrid.ShowEditValidationMessage("时间段重复");
                    }
                    if (period.EndDate <= period.BeginDate)
                        return gridModel.PeriodGrid.ShowEditValidationMessage("开始日期必须小于结束日期");
                    Uow.Period.Add(period);
                    Uow.Commit();
                    //}
                }
                if (gridModel.PeriodGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    //using (var context = db)
                    //{
                    var oldPeriod = Uow.Period.GetById(g => g.Id == period.Id);
                    if (oldPeriod.Code != period.Code && CheckPeriodCodeDup(period.Code))
                    {
                        return gridModel.PeriodGrid.ShowEditValidationMessage("编码重复");

                    }
                    if (oldPeriod.BeginDate != period.BeginDate || oldPeriod.EndDate != period.EndDate)
                    {
                        if (CheckPeriodDateDup(period.BeginDate, period.EndDate))
                        {
                            return gridModel.PeriodGrid.ShowEditValidationMessage("时间段重复");
                        }
                    }
                    if (period.EndDate <= period.BeginDate)
                        return gridModel.PeriodGrid.ShowEditValidationMessage("开始日期必须小于结束日期");

                    var count = Uow.MonthBalance.GetAll().Where(w => w.Period == period.Id).Count();
                    if (count > 0)
                    {
                        if (oldPeriod.BeginDate != period.BeginDate || oldPeriod.EndDate != period.EndDate)
                            return gridModel.PeriodGrid.ShowEditValidationMessage("已使用月结周期不能修改时间段");
                    }
                    oldPeriod.Code = period.Code;
                    oldPeriod.BeginDate = period.BeginDate;
                    oldPeriod.EndDate = period.EndDate;
                    oldPeriod.Memo = period.Memo;
                    Uow.Period.Update(oldPeriod);
                    Uow.Commit();
                    //}
                }
                if (gridModel.PeriodGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    //using (var context = db)
                    //{
                    var count = Uow.MonthBalance.GetAll().Where(w => w.Period == period.Id).Count();
                    if (count > 0) return gridModel.PeriodGrid.ShowEditValidationMessage("已使用不能删除");
                    var oldPeriod = Uow.Period.GetById(g => g.Id == period.Id);
                    Uow.Period.Delete(oldPeriod);
                    Uow.Commit();
                    //}
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.PeriodGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("Period");
        }
        #endregion

        #region 库存盘点单
        private void CheckCVCodeDup(string code)
        {
            var icount = Uow.CheckVouch.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }
        public JsonResult AddCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {
                    if (businessCommon.IsBalance(checkVouch.CVDate.Value, checkVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    CheckCVCodeDup(checkVouch.Code);
                    int count = Uow.CheckVouch.GetAll().Where(w => !w.IsVerify && w.WhId == checkVouch.WhId).Count();
                    if (count > 0)
                        throw new DXInfo.Models.BusinessException("有未审核盘点单，不能添加盘点单");
                    DXInfo.Models.CheckVouch newCheckVouch = Mapper.Map<DXInfo.Models.CheckVouch>(checkVouch);
                    DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newCheckVouch.WhId);
                    newCheckVouch.DeptId = warehouse.Dept;

                    newCheckVouch.Maker = userId;
                    newCheckVouch.MakeDate = DateTime.Now;
                    newCheckVouch.MakeTime = DateTime.Now;

                    newCheckVouch.OutRdCode = "007";
                    newCheckVouch.InRdCode = "004";

                    Uow.CheckVouch.Add(newCheckVouch);
                    Uow.Commit();
                    if (isLocator)
                    {
                        List<DXInfo.Models.CurrentInvLocator> lCurrentInvLocator = Uow.CurrentInvLocator.GetAll().Where(w => w.WhId == newCheckVouch.WhId).ToList();

                        foreach (DXInfo.Models.CurrentInvLocator currentInvLocator in lCurrentInvLocator)
                        {
                            DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentInvLocator);
                            checkVouchs.CVId = newCheckVouch.Id;
                            Uow.CheckVouchs.Add(checkVouchs);
                        }
                    }
                    else
                    {
                        List<DXInfo.Models.CurrentStock> lCurrentStock = Uow.CurrentStock.GetAll().Where(w => w.WhId == newCheckVouch.WhId).ToList();

                        foreach (DXInfo.Models.CurrentStock currentStock in lCurrentStock)
                        {
                            DXInfo.Models.CheckVouchs checkVouchs = Mapper.Map<DXInfo.Models.CheckVouchs>(currentStock);
                            checkVouchs.CVId = newCheckVouch.Id;
                            Uow.CheckVouchs.Add(checkVouchs);
                        }
                    }
                    Uow.Commit();
                    transaction.Complete();

                    retCheckVouch = Mapper.Map<CheckVouch>(newCheckVouch);
                    retCheckVouch.IsModify = true;
                }
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            try
            {
                //using (var context = db)
                //{                    
                DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == checkVouch.Id);
                if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldCheckVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldCheckVouch = Mapper.Map<CheckVouch, DXInfo.Models.CheckVouch>(checkVouch, oldCheckVouch);
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
                //}
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
            return Json(checkVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == checkVouch.Id);
                    if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (oldCheckVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (oldCheckVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    Uow.CheckVouch.Delete(oldCheckVouch);

                    List<DXInfo.Models.CheckVouchs> lCheckVouchs = Uow.CheckVouchs.GetAll().Where(w => w.CVId == checkVouch.Id).ToList();
                    foreach (DXInfo.Models.CheckVouchs checkVouchs in lCheckVouchs)
                    {
                        Uow.CheckVouchs.Delete(checkVouchs);
                    }
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return NextCheckVouch(checkVouch);
        }
        private void AddRdRecordByCheckVouch(DXInfo.Models.CheckVouch checkVouch, 
            List<DXInfo.Models.CheckVouchs> lCheckVouchs, DXInfo.Models.VouchType vouchType, 
            DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType,Guid userId)
        {

            DXInfo.Models.RdRecord rdRecord = Mapper.Map<DXInfo.Models.CheckVouch, DXInfo.Models.RdRecord>(checkVouch);
            rdRecord.SourceCode = checkVouch.Code;
            rdRecord.SourceId = checkVouch.Id;
            rdRecord.BusType = busType.Code;
            rdRecord.VouchType = vouchType.Code;
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
            rdRecord.VouchType = vouchType.Code;
            rdRecord.Code = GetRdRecordCode(vouchType);
            rdRecord.Maker = userId;
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
        public JsonResult VerifyCheckVouch(CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {                    
                    DXInfo.Models.CheckVouch oldCheckVouch = Uow.CheckVouch.GetById(g=>g.Id==checkVouch.Id);
                    if (oldCheckVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException("已审核不能再次审核");
                    }
                    if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    oldCheckVouch.IsVerify = true;
                    oldCheckVouch.Verifier = userId;
                    oldCheckVouch.VerifyDate = DateTime.Now;
                    oldCheckVouch.VerifyTime = DateTime.Now;
                    Uow.CheckVouch.Update(oldCheckVouch);

                    List<DXInfo.Models.CheckVouchs> lCheckVouchs = Uow.CheckVouchs.GetAll().Where(w => w.CVId == oldCheckVouch.Id).ToList();

                    
                    DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g=>g.Code=="007");
                    DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g=>g.Code=="007");
                    DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                    AddRdRecordByCheckVouch(oldCheckVouch, lCheckVouchs, outVouchType, outRdType, outBusType,userId);

                    DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g=>g.Code=="004");
                    DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g=>g.Code=="004");
                    DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                    AddRdRecordByCheckVouch(oldCheckVouch, lCheckVouchs, inVouchType, inRdType, inBusType,userId);


                    transaction.Complete();
                    retCheckVouch = Mapper.Map<CheckVouch>(oldCheckVouch);
                    retCheckVouch.IsModify = true;
                }
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyCheckVouch(CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.CheckVouch OldCheckVouch = Uow.CheckVouch.GetById(g=>g.Id==checkVouch.Id);
                    if (businessCommon.IsBalance(OldCheckVouch.CVDate, OldCheckVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    List<DXInfo.Models.RdRecord> lRdRecord = Uow.RdRecord.GetAll().Where(w => w.SourceId == checkVouch.Id).ToList();
                    foreach (DXInfo.Models.RdRecord rdRecord in lRdRecord)
                    {
                        if (rdRecord.IsVerify)
                        {
                            if (rdRecord.RdFlag == 0)
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它入库单");
                            else
                                throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "其它出库单");
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
                    OldCheckVouch.Modifier = userId;
                    OldCheckVouch.ModifyDate = DateTime.Now;
                    OldCheckVouch.ModifyTime = DateTime.Now;
                    Uow.CheckVouch.Update(OldCheckVouch);

                    Uow.Commit();
                    transaction.Complete();
                    retCheckVouch = Mapper.Map<CheckVouch>(OldCheckVouch);
                    retCheckVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCheckVouch()
        {
            Session[CheckVouchsSession] = null;
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.CheckVouch);
            cvd.VouchDateId = "CVDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            return Json(cvd, JsonRequestBehavior.AllowGet);
        }
        private void SetupCheckVouchsGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("CheckVouchs_RequestData");
            grid.EditUrl = Url.Action("CheckVouchs_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";

            this.SetDropDownColumn(grid, "InvId", this.GetInventory());
            JQGridColumn invColumn = grid.Columns.Find(c => c.DataField == "InvId");

            JQGridColumn column = grid.Columns.Find(c => c.DataField == "InvName");
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            if (isBatch)
            {
                column = grid.Columns.Find(c => c.DataField == "Batch");
                column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            }
            if (isLocator)
            {
                column = grid.Columns.Find(c => c.DataField == "LocatorName");
                column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            }
            column = grid.Columns.Find(c => c.DataField == "Num");
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            column = grid.Columns.Find(c => c.DataField == "Specs");
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };

            SetStockColumn(grid);
        }
        public ActionResult CheckVouchs_RequestData(Guid? searchString)
        {
            var gridModel = new CheckVouchs();
            SetupCheckVouchsGridModel(gridModel.CheckVouchsGrid);
            if (searchString.HasValue)
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
                                  LocatorName = dd5s==null?"":dd5s.Name,
                                  d.Memo,
                              };
                return gridModel.CheckVouchsGrid.DataBind(records);
            }
            else
            {

                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    CVId = "",
                    InvId = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    Num = "",
                    CNum = "",
                    AddInNum = "",
                    AddOutNum = "",
                    Batch = "",
                    MadeDate = "",
                    ShelfLife = "",
                    ShelfLifeType = "",
                    ShelfLifeTypeName = "",
                    InvalidDate = "",
                    Locator = "",
                    LocatorName = "",
                    Memo="",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.CheckVouchsGrid.DataBind(list.AsQueryable());
            }
        }
        public ActionResult CheckVouchs_EditData(DXInfo.Models.CheckVouchs checkVouchs)
        {
            var gridModel = new CheckVouchs();
            SetupCheckVouchsGridModel(gridModel.CheckVouchsGrid);
            if (gridModel.CheckVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (checkVouchs.CVId != Guid.Empty)
                {
                    //using (var context = db)
                    //{
                    var oldCheckVouchs = Uow.CheckVouchs.GetById(g => g.Id == checkVouchs.Id);
                    var oldCheckVouch = Uow.CheckVouch.GetById(g => g.Id == oldCheckVouchs.CVId);
                    if (businessCommon.IsBalance(oldCheckVouch.CVDate, oldCheckVouch.WhId))
                    {
                        return gridModel.CheckVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                    //}
                }
            }
            return RedirectToAction("CheckVouch");
        }
        public ActionResult CheckVouch()
        {
            var gridModel = new CheckVouchModel();
            gridModel.IsBatch = isBatch;
            gridModel.IsLocator = isLocator;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.CheckVouch);
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var checkVouchs = Uow.CheckVouchs.GetById(g => g.Id == Id);
                if (checkVouchs == null)
                {
                    gridModel.checkVouch.Id = Id;
                }
                else
                {
                    gridModel.checkVouch.Id = checkVouchs.CVId;
                }
            }
            SetupCheckVouchsGridModel(gridModel.checkVouchs.CheckVouchsGrid);
            return View(gridModel);
        }

        private DXInfo.Models.CheckVouch GetCheckVouchOrderByDescending(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.CheckVouch checkVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime < makeTime && w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime < makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return checkVouch;
        }
        private DXInfo.Models.CheckVouch GetCheckVouchOrderBy(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.CheckVouch checkVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime > makeTime).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime > makeTime && w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.MakeTime > makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        checkVouch = Uow.CheckVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return checkVouch;
        }
        public JsonResult CurCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                if (checkVouch.Id == null)
                {
                    return StartCheckVouch();
                }
                var curCheckVouch = Uow.CheckVouch.GetById(g => g.Id == checkVouch.Id);
                if (curCheckVouch == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retCheckVouch = Mapper.Map<CheckVouch>(curCheckVouch);
                retCheckVouch.IsModify = true;
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartCheckVouch()
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                var firstCheckVouch = GetCheckVouchOrderBy(null);
                if (firstCheckVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retCheckVouch = Mapper.Map<CheckVouch>(firstCheckVouch);
                retCheckVouch.IsModify = true;
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                if (checkVouch.Id == null)
                {
                    return StartCheckVouch();
                }
                var curCheckVouch = Uow.CheckVouch.GetById(g => g.Id == checkVouch.Id);
                DateTime makeTime = checkVouch.MakeTime.Value;
                if (curCheckVouch != null)
                {
                    makeTime = curCheckVouch.MakeTime;
                }
                var prevCheckVouch = GetCheckVouchOrderByDescending(makeTime);
                if (prevCheckVouch == null)
                {
                    if (curCheckVouch == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retCheckVouch = Mapper.Map<CheckVouch>(curCheckVouch);
                    retCheckVouch.IsModify = true;
                }
                else
                {
                    retCheckVouch = Mapper.Map<CheckVouch>(prevCheckVouch);
                    retCheckVouch.IsModify = true;
                }
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextCheckVouch([Bind(Prefix = "checkVouch")]CheckVouch checkVouch)
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                if (checkVouch.Id == null)
                {
                    return EndCheckVouch();
                }
                var curCheckVouch = Uow.CheckVouch.GetById(g => g.Id == checkVouch.Id);
                DateTime makeTime = checkVouch.MakeTime.Value;
                if (curCheckVouch != null)
                {
                    makeTime = curCheckVouch.MakeTime;
                }
                var nextCheckVouch = GetCheckVouchOrderBy(makeTime);
                if (nextCheckVouch == null)
                {
                    if (curCheckVouch == null)
                    {
                        return PrevCheckVouch(checkVouch);
                    }
                    retCheckVouch = Mapper.Map<CheckVouch>(curCheckVouch);
                    retCheckVouch.IsModify = true;
                }
                else
                {
                    retCheckVouch = Mapper.Map<CheckVouch>(nextCheckVouch);
                    retCheckVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EndCheckVouch()
        {
            CheckVouch retCheckVouch = new CheckVouch();
            try
            {
                var lastCheckVouch = GetCheckVouchOrderByDescending(null);
                if (lastCheckVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retCheckVouch = Mapper.Map<CheckVouch>(lastCheckVouch);
                retCheckVouch.IsModify = true;
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
            return Json(retCheckVouch, JsonRequestBehavior.AllowGet);
        }

        private void SetupCheckVouchGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("CheckVouch_RequestData");
            grid.EditUrl = Url.Action("CheckVouch_EditData");

            JQGridColumn SalesmanColumn = grid.Columns.Find(c => c.DataField == "Salesman");

            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsVerify");
            SetStockColumn(grid);
        }
        public ActionResult CheckVouch_RequestData()
        {
            var gridModel = new CheckVouchGridModel();
            SetupCheckVouchGridModel(gridModel.CheckVouchGrid);
            
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
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.CheckVouchGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.CheckVouchGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.CheckVouchGrid.ExportToExcel(records, "库存盘点单.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.CheckVouchGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.CheckVouchGrid.DataBind(records);
            }
        }
        public ActionResult CheckVouch_EditData()
        {
            var gridModel = new CheckVouchGridModel();
            SetupCheckVouchGridModel(gridModel.CheckVouchGrid);
            return RedirectToAction("SearchCheckVouch");
        }
        public ActionResult SearchCheckVouch()
        {
            var gridModel = new CheckVouchGridModel();
            SetupCheckVouchGridModel(gridModel.CheckVouchGrid);
            return View(gridModel);
        }
        #endregion

        #region 库存货位流水账
        private void SetupInvLocatorGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("InvLocator_RequestData");
            grid.EditUrl = Url.Action("InvLocator_EditData");

            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "VenId", this.GetVendor());
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            
            if (isShelfLife)
            {
                this.SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());                
            }
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult InvLocator()
        {
            var gridModel = new InvLocatorGridModel();
            SetupInvLocatorGridModel(gridModel.InvLocatorGrid);
            return View(gridModel);
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

                             select new
                             {
                                 d.Id,
                                 RdFlag=d.RdFlag==0?"入库":"出库",
                                 d.ILDate,
                                 DeptId=dd2s==null?dept:dd2s.Dept,
                                 d.WhId,
                                 WhName = dd2s.Name,                                 
                                 d.VenId,
                                 VenName = dd3s.Name,
                                 LocatorName = dd4s.Name,
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
                                 d.Salesman,
                                 SalesmanName=dd9s.FullName
                             };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    invLocator = invLocator.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    invLocator = invLocator.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.InvLocatorGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.InvLocatorGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.InvLocatorGrid.ExportToExcel(invLocator, "库存货位流水账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.InvLocatorGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.InvLocatorGrid.DataBind(invLocator);
            }
        }
        public ActionResult InvLocator_EditData(DXInfo.Models.InvLocator invLocator)
        {
            var gridModel = new InvLocatorGridModel();
            SetupInvLocatorGridModel(gridModel.InvLocatorGrid);
            return RedirectToAction("InvLocator");
        }
        #endregion

        #region 库存货位调整单
        private void CheckALVCodeDup(string code)
        {
            var icount = Uow.AdjustLocatorVouch.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }        
        private AdjustLocatorVouch AddAdjustLocatorVouch(AdjustLocatorVouch adjustLocatorVouch, 
            List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs,Guid userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckALVCodeDup(adjustLocatorVouch.Code);
                DXInfo.Models.AdjustLocatorVouch newAdjustLocatorVouch = Mapper.Map<DXInfo.Models.AdjustLocatorVouch>(adjustLocatorVouch);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == newAdjustLocatorVouch.WhId);
                newAdjustLocatorVouch.DeptId = warehouse.Dept;
                newAdjustLocatorVouch.Maker = userId;
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

                AdjustLocatorVouch retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(newAdjustLocatorVouch);
                retAdjustLocatorVouch.IsModify = true;
                return retAdjustLocatorVouch;
            }
        }
        public JsonResult AddAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                if (businessCommon.IsBalance(adjustLocatorVouch.ALVDate.Value, adjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = new List<DXInfo.Models.AdjustLocatorVouchs>();
                if (Session[AdjustLocatorVouchsSession] != null)
                {
                    lAdjustLocatorVouchs = Session[AdjustLocatorVouchsSession] as List<DXInfo.Models.AdjustLocatorVouchs>;
                }
                Guid userId = operId;
                retAdjustLocatorVouch = AddAdjustLocatorVouch(adjustLocatorVouch, lAdjustLocatorVouchs,userId);
                Session[AdjustLocatorVouchsSession] = null;
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            try
            {
                //using (var context = db)
                //{                    
                DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouch.Id);
                if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldAdjustLocatorVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch, DXInfo.Models.AdjustLocatorVouch>(adjustLocatorVouch, oldAdjustLocatorVouch);
                DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == oldAdjustLocatorVouch.WhId);
                oldAdjustLocatorVouch.DeptId = warehouse.Dept;
                oldAdjustLocatorVouch.Modifier = operId;
                oldAdjustLocatorVouch.ModifyDate = DateTime.Now;
                oldAdjustLocatorVouch.ModifyTime = DateTime.Now;
                Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);
                Uow.Commit();
                //}
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
            return Json(adjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouch.Id);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (oldAdjustLocatorVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (oldAdjustLocatorVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    Uow.AdjustLocatorVouch.Delete(oldAdjustLocatorVouch);

                    List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == adjustLocatorVouch.Id).ToList();
                    foreach (DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs in lAdjustLocatorVouchs)
                    {
                        Uow.AdjustLocatorVouchs.Delete(adjustLocatorVouchs);
                    }
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return NextAdjustLocatorVouch(adjustLocatorVouch);
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
            DXInfo.Models.VouchType vouchType, DXInfo.Models.RdType rdType, DXInfo.Models.BusType busType,Guid userId)
        {
            DXInfo.Models.RdRecord newRdRecord = Mapper.Map<DXInfo.Models.RdRecord>(adjustLocatorVouch);
            newRdRecord.SourceCode = adjustLocatorVouch.Code;
            newRdRecord.SourceId = adjustLocatorVouch.Id;
            newRdRecord.BusType = busType.Code;
            newRdRecord.VouchType = vouchType.Code;
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
            newRdRecord.VouchType = vouchType.Code;
            newRdRecord.Code = GetRdRecordCode(vouchType);
            newRdRecord.Maker = userId;
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
        public JsonResult VerifyAdjustLocatorVouch(AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {                    
                    DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g=>g.Id==adjustLocatorVouch.Id);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    oldAdjustLocatorVouch.IsVerify = true;
                    oldAdjustLocatorVouch.Verifier = userId;
                    oldAdjustLocatorVouch.VerifyDate = DateTime.Now;
                    oldAdjustLocatorVouch.VerifyTime = DateTime.Now;
                    Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);

                    List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == oldAdjustLocatorVouch.Id).ToList();
                    if (lAdjustLocatorVouchs.Count > 0)
                    {
                        DXInfo.Models.VouchType inVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherInStock);
                        DXInfo.Models.RdType inRdType = Uow.RdType.GetById(g=>g.Code=="005");
                        DXInfo.Models.BusType inBusType = Uow.BusType.GetById(g=>g.Code=="005");
                        AddRdRecordByAdjustLocatorVouch(oldAdjustLocatorVouch, lAdjustLocatorVouchs, inVouchType, inRdType, inBusType,userId);
                        DXInfo.Models.VouchType outVouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.OtherOutStock);
                        DXInfo.Models.RdType outRdType = Uow.RdType.GetById(g=>g.Code=="009");
                        DXInfo.Models.BusType outBusType = Uow.BusType.GetById(g=>g.Code=="009");
                        AddRdRecordByAdjustLocatorVouch(oldAdjustLocatorVouch, lAdjustLocatorVouchs, outVouchType, outRdType, outBusType,userId);
                    }
                    Uow.Commit();
                    transaction.Complete();
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(oldAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyAdjustLocatorVouch(AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.AdjustLocatorVouch oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g=>g.Id==adjustLocatorVouch.Id);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }

                    List<DXInfo.Models.InvLocator> lInvLocator = Uow.InvLocator.GetAll().Where(w => w.SourceId == adjustLocatorVouch.Id).ToList();
                    foreach (DXInfo.Models.InvLocator invLocator in lInvLocator)
                    {
                        Uow.InvLocator.Delete(invLocator);
                    }
                    
                    oldAdjustLocatorVouch.IsVerify = false;
                    oldAdjustLocatorVouch.Verifier = null;
                    oldAdjustLocatorVouch.VerifyDate = null;
                    oldAdjustLocatorVouch.VerifyTime = null;
                    oldAdjustLocatorVouch.Modifier = userId;
                    oldAdjustLocatorVouch.ModifyDate = DateTime.Now;
                    oldAdjustLocatorVouch.ModifyTime = DateTime.Now;
                    Uow.AdjustLocatorVouch.Update(oldAdjustLocatorVouch);
                    List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetAll().Where(w => w.ALVId == oldAdjustLocatorVouch.Id).ToList();

                    updateCurrentInvLocatorByALV(oldAdjustLocatorVouch, lAdjustLocatorVouchs);

                    Uow.Commit();
                    transaction.Complete();
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(oldAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult GetAdjustLocatorVouch()
        {
            Session[AdjustLocatorVouchsSession] = null;
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.AdjustLocatorVouch);
            cvd.VouchDateId = "ALVDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            return Json(cvd, JsonRequestBehavior.AllowGet);
        }
        private void SetupAdjustLocatorVouchsGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("AdjustLocatorVouchs_RequestData");
            grid.EditUrl = Url.Action("AdjustLocatorVouchs_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit2";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";
            SetDropDownColumn(grid, "InvId", this.GetInventory());

            SetStockColumn(grid);

            JQGridColumn column = grid.Columns.Find(c => c.DataField == "OutLocator");
            column.EditType = EditType.DropDown;
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            column.EditList.Add(new SelectListItem { Text = "选择货位", Value = "" });

            column = grid.Columns.Find(c => c.DataField == "InLocator");
            column.EditType = EditType.DropDown;
            column.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
            column.EditList.Add(new SelectListItem { Text = "选择货位", Value = "" });

            if (isBatch)
            {
                JQGridColumn batchColumn = grid.Columns.Find(c => c.DataField == "Batch");
                batchColumn.EditType = EditType.DropDown;
                batchColumn.EditFieldAttributes = new List<JQGridEditFieldAttribute>() { new JQGridEditFieldAttribute() { Name = "disabled", Value = "disabled" } };
                batchColumn.EditList.Add(new SelectListItem { Text = "选择批号", Value = "" });
            }

        }
        public ActionResult AdjustLocatorVouchs_RequestData(Guid? searchString)
        {
            var gridModel = new AdjustLocatorVouchs();
            SetupAdjustLocatorVouchsGridModel(gridModel.AdjustLocatorVouchsGrid);
            if (!searchString.HasValue)
            {
                List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = new List<DXInfo.Models.AdjustLocatorVouchs>();
                if (Session[AdjustLocatorVouchsSession] != null)
                    lAdjustLocatorVouchs = Session[AdjustLocatorVouchsSession] as List<DXInfo.Models.AdjustLocatorVouchs>;
                List<DXInfo.Models.Inventory> linventories = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage).ToList();
                List<DXInfo.Models.UnitOfMeasures> lunitOfMeasure = Uow.UnitOfMeasures.GetAll().ToList();
                List<DXInfo.Models.EnumTypeDescription> lenumTypeDescription = Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType).ToList();
                List<DXInfo.Models.Locator> llocator = Uow.Locator.GetAll().ToList();
                var records = from d in lAdjustLocatorVouchs
                              join d1 in linventories on d.InvId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d3 in lunitOfMeasure on dd1s.StockUnit equals d3.Id into dd3
                              from dd3s in dd3.DefaultIfEmpty()

                              join d5 in llocator on d.OutLocator equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()
                              join d6 in llocator on d.InLocator equals d6.Id into dd6
                              from dd6s in dd6.DefaultIfEmpty()
                              
                              select new
                              {
                                  d.Id,
                                  d.ALVId,
                                  d.InvId,
                                  InvName = dd1s.Name,
                                  Specs = dd1s.Specs,
                                  STUnitName = dd3s==null?"":dd3s.Name,
                                  d.Num,
                                  d.Batch,
                                  MadeDate="",
                                  ShelfLife="",
                                  ShelfLifeType="",
                                  ShelfLifeTypeName="",
                                  d.InvalidDate,
                                  d.OutLocator,
                                  OutLocatorName = dd5s.Name,
                                  d.InLocator,
                                  InLocatorName = dd6s.Name,
                                  AvaNum="",
                                  d.Memo,
                              };
                return gridModel.AdjustLocatorVouchsGrid.DataBind(records.AsQueryable());
            }
            else
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
                                  AvaNum="",
                                  d.Memo,
                              };
                return gridModel.AdjustLocatorVouchsGrid.DataBind(records);
            }
        }
        public ActionResult AdjustLocatorVouchs_EditData(DXInfo.Models.AdjustLocatorVouchs adjustLocatorVouchs)
        {
            var gridModel = new AdjustLocatorVouchs();
            SetupAdjustLocatorVouchsGridModel(gridModel.AdjustLocatorVouchsGrid);
            if (gridModel.AdjustLocatorVouchsGrid.AjaxCallBackMode != AjaxCallBackMode.DeleteRow)
            {
                if (adjustLocatorVouchs.OutLocator == adjustLocatorVouchs.InLocator)
                {
                    return gridModel.AdjustLocatorVouchsGrid.ShowEditValidationMessage("调整后货位不能相同");
                }
            }
            if (gridModel.AdjustLocatorVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                if (adjustLocatorVouchs.ALVId != Guid.Empty)
                {
                    //using (var context = db)
                    //{
                    var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouchs.ALVId);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        return gridModel.AdjustLocatorVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                    //}
                }
                else
                {
                    List<DXInfo.Models.AdjustLocatorVouchs> ladjustLocatorVouchs = new List<DXInfo.Models.AdjustLocatorVouchs>();
                    if (Session[AdjustLocatorVouchsSession] != null)
                    {
                        ladjustLocatorVouchs = Session[AdjustLocatorVouchsSession] as List<DXInfo.Models.AdjustLocatorVouchs>;
                    }
                    adjustLocatorVouchs.Id = Guid.NewGuid();
                    ladjustLocatorVouchs.Add(adjustLocatorVouchs);
                    Session[AdjustLocatorVouchsSession] = ladjustLocatorVouchs;
                }
            }
            if (gridModel.AdjustLocatorVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (adjustLocatorVouchs.ALVId != Guid.Empty)
                {
                    //using (var context = db)
                    //{
                    var oldAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == adjustLocatorVouchs.Id);
                    var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouchs.ALVId);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        return gridModel.AdjustLocatorVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                    //}
                }
                else
                {
                    if (Session[AdjustLocatorVouchsSession] != null)
                    {

                        List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Session[AdjustLocatorVouchsSession] as List<DXInfo.Models.AdjustLocatorVouchs>;
                        DXInfo.Models.AdjustLocatorVouchs oldAdjustLocatorVouchs = lAdjustLocatorVouchs.Find(delegate(DXInfo.Models.AdjustLocatorVouchs sub) { return sub.Id == adjustLocatorVouchs.Id; });

                        oldAdjustLocatorVouchs.InvId = adjustLocatorVouchs.InvId;
                        oldAdjustLocatorVouchs.Num = adjustLocatorVouchs.Num;
                        oldAdjustLocatorVouchs.Batch = adjustLocatorVouchs.Batch;
                        oldAdjustLocatorVouchs.InLocator = adjustLocatorVouchs.InLocator;
                        oldAdjustLocatorVouchs.OutLocator = adjustLocatorVouchs.OutLocator;
                        oldAdjustLocatorVouchs.Memo = adjustLocatorVouchs.Memo;
                        Session[AdjustLocatorVouchsSession] = lAdjustLocatorVouchs;
                    }
                }
            }
            if (gridModel.AdjustLocatorVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                //using (var context = db)
                //{
                var oldAdjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == adjustLocatorVouchs.Id);
                if (oldAdjustLocatorVouchs != null)
                {
                    var oldAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == oldAdjustLocatorVouchs.ALVId);
                    if (businessCommon.IsBalance(oldAdjustLocatorVouch.ALVDate, oldAdjustLocatorVouch.WhId))
                    {
                        return gridModel.AdjustLocatorVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    Uow.AdjustLocatorVouchs.Delete(oldAdjustLocatorVouchs);
                    Uow.Commit();
                }
                //}
                if (Session[AdjustLocatorVouchsSession] != null)
                {

                    List<DXInfo.Models.AdjustLocatorVouchs> lAdjustLocatorVouchs = Session[AdjustLocatorVouchsSession] as List<DXInfo.Models.AdjustLocatorVouchs>;
                    lAdjustLocatorVouchs.RemoveAll(delegate(DXInfo.Models.AdjustLocatorVouchs sub) { return sub.Id == adjustLocatorVouchs.Id; });
                    Session[AdjustLocatorVouchsSession] = lAdjustLocatorVouchs;
                }
            }
            return RedirectToAction("AdjustLocatorVouch");
        }
        public ActionResult AdjustLocatorVouch()
        {
            var gridModel = new AdjustLocatorVouchModel();
            gridModel.IsBatch = isBatch;
            gridModel.IsLocator = isLocator;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.AdjustLocatorVouch);
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var adjustLocatorVouchs = Uow.AdjustLocatorVouchs.GetById(g => g.Id == Id);
                if (adjustLocatorVouchs == null)
                {
                    gridModel.adjustLocatorVouch.Id = Id;
                }
                else
                {
                    gridModel.adjustLocatorVouch.Id = adjustLocatorVouchs.ALVId;
                }
            }
            SetupAdjustLocatorVouchsGridModel(gridModel.adjustLocatorVouchs.AdjustLocatorVouchsGrid);
            return View(gridModel);
        }


        private DXInfo.Models.AdjustLocatorVouch GetAdjustLocatorVouchOrderByDescending(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.AdjustLocatorVouch adjustLocatorVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime < makeTime && w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.DeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime < makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return adjustLocatorVouch;
        }
        private DXInfo.Models.AdjustLocatorVouch GetAdjustLocatorVouchOrderBy(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.AdjustLocatorVouch adjustLocatorVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime > makeTime).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime > makeTime && w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.DeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.MakeTime > makeTime
                            && w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        adjustLocatorVouch = Uow.AdjustLocatorVouch.GetAll().Where(w => w.DeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return adjustLocatorVouch;
        }
        public JsonResult CurAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                if (adjustLocatorVouch.Id == null)
                {
                    return StartAdjustLocatorVouch();
                }
                var curAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouch.Id);
                if (curAdjustLocatorVouch == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(curAdjustLocatorVouch);
                retAdjustLocatorVouch.IsModify = true;
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartAdjustLocatorVouch()
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                var firstAdjustLocatorVouch = GetAdjustLocatorVouchOrderBy(null);
                if (firstAdjustLocatorVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(firstAdjustLocatorVouch);
                retAdjustLocatorVouch.IsModify = true;
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                if (adjustLocatorVouch.Id == null)
                {
                    return StartAdjustLocatorVouch();
                }
                var curAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouch.Id);
                DateTime makeTime = adjustLocatorVouch.MakeTime.Value;
                if (curAdjustLocatorVouch != null)
                {
                    makeTime = curAdjustLocatorVouch.MakeTime;
                }
                var prevAdjustLocatorVouch = GetAdjustLocatorVouchOrderByDescending(makeTime);
                if (prevAdjustLocatorVouch == null)
                {
                    if (curAdjustLocatorVouch == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(curAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
                else
                {
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(prevAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextAdjustLocatorVouch([Bind(Prefix = "adjustLocatorVouch")]AdjustLocatorVouch adjustLocatorVouch)
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                if (adjustLocatorVouch.Id == null)
                {
                    return EndAdjustLocatorVouch();
                }
                var curAdjustLocatorVouch = Uow.AdjustLocatorVouch.GetById(g => g.Id == adjustLocatorVouch.Id);
                DateTime makeTime = adjustLocatorVouch.MakeTime.Value;
                if (curAdjustLocatorVouch != null)
                {
                    makeTime = curAdjustLocatorVouch.MakeTime;
                }
                var nextAdjustLocatorVouch = GetAdjustLocatorVouchOrderBy(makeTime);
                if (nextAdjustLocatorVouch == null)
                {
                    if (curAdjustLocatorVouch == null)
                    {
                        return PrevAdjustLocatorVouch(adjustLocatorVouch);
                    }
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(curAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
                else
                {
                    retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(nextAdjustLocatorVouch);
                    retAdjustLocatorVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EndAdjustLocatorVouch()
        {
            AdjustLocatorVouch retAdjustLocatorVouch = new AdjustLocatorVouch();
            try
            {
                var lastAdjustLocatorVouch = GetAdjustLocatorVouchOrderByDescending(null);
                if (lastAdjustLocatorVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retAdjustLocatorVouch = Mapper.Map<AdjustLocatorVouch>(lastAdjustLocatorVouch);
                retAdjustLocatorVouch.IsModify = true;
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
            return Json(retAdjustLocatorVouch, JsonRequestBehavior.AllowGet);
        }
        private void SetupAdjustLocatorVouchGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("AdjustLocatorVouch_RequestData");
            grid.EditUrl = Url.Action("AdjustLocatorVouch_EditData");

            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsVerify");

            this.SetStockColumn(grid);
        }
        public ActionResult AdjustLocatorVouch_RequestData()
        {
            var gridModel = new AdjustLocatorVouchGridModel();
            SetupAdjustLocatorVouchGridModel(gridModel.AdjustLocatorVouchGrid);
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
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.AdjustLocatorVouchGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.AdjustLocatorVouchGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.AdjustLocatorVouchGrid.ExportToExcel(records, "库存货位调整单.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.AdjustLocatorVouchGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.AdjustLocatorVouchGrid.DataBind(records);
            }
        }
        public ActionResult AdjustLocatorVouch_EditData()
        {
            var gridModel = new AdjustLocatorVouchGridModel();
            SetupAdjustLocatorVouchGridModel(gridModel.AdjustLocatorVouchGrid);
            return RedirectToAction("SearchAdjustLocatorVouch");
        }
        public ActionResult SearchAdjustLocatorVouch()
        {
            var gridModel = new AdjustLocatorVouchGridModel();
            SetupAdjustLocatorVouchGridModel(gridModel.AdjustLocatorVouchGrid);
            return View(gridModel);
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
        private void CheckMVCodeDup(string code)
        {
            var icount = Uow.MixVouch.GetAll().Where(w => w.Code == code).Count();
            if (icount > 0) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.CodeDup);
        }        
        private MixVouch AddMixVouch(MixVouch mixVouch, List<DXInfo.Models.MixVouchs> lMixVouchs,Guid userId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                CheckMVCodeDup(mixVouch.Code);
                DXInfo.Models.MixVouch newMixVouch = Mapper.Map<DXInfo.Models.MixVouch>(mixVouch);
                DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == newMixVouch.InWhId);
                DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == newMixVouch.OutWhId);
                newMixVouch.InDeptId = inWarehouse.Dept;
                newMixVouch.OutDeptId = outWarehouse.Dept;
                newMixVouch.Maker = userId;
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

                MixVouch retMixVouch = Mapper.Map<MixVouch>(newMixVouch);
                retMixVouch.IsModify = true;
                return retMixVouch;
            }
        }
        public JsonResult AddMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                if (businessCommon.IsBalance(mixVouch.MVDate.Value, mixVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                List<DXInfo.Models.MixVouchs> lMixVouchs = new List<DXInfo.Models.MixVouchs>();
                if (Session[MixVouchsSession] != null)
                {
                    lMixVouchs = Session[MixVouchsSession] as List<DXInfo.Models.MixVouchs>;
                }
                Guid userId = operId;
                retMixVouch = AddMixVouch(mixVouch, lMixVouchs,userId);
                Session[MixVouchsSession] = null;
                if (Session[BatchInventorySession] != null)
                {
                    List<BatchInventoryList> lb = Session[BatchInventorySession] as List<BatchInventoryList>;
                    foreach (BatchInventoryList list in lb)
                    {
                        list.Num = 0;
                    }
                    Session[BatchInventorySession] = lb;
                }
                if (Session[BatchWarehouseInventorySession] != null)
                {
                    List<BatchWarehouseInventoryList> lb = Session[BatchWarehouseInventorySession] as List<BatchWarehouseInventoryList>;
                    foreach (BatchWarehouseInventoryList list in lb)
                    {
                        list.Num = 0;
                    }
                    Session[BatchWarehouseInventorySession] = lb;
                }
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ModifyMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            try
            {
                //using (var context = db)
                //{                    
                DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouch.Id);
                if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                {
                    throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                }
                if (oldMixVouch.IsVerify)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                }
                oldMixVouch = Mapper.Map<MixVouch, DXInfo.Models.MixVouch>(mixVouch, oldMixVouch);
                DXInfo.Models.Warehouse inWarehouse = Uow.Warehouse.GetById(g => g.Id == oldMixVouch.InWhId);
                DXInfo.Models.Warehouse outWarehouse = Uow.Warehouse.GetById(g => g.Id == oldMixVouch.OutWhId);
                oldMixVouch.InDeptId = inWarehouse.Dept;
                oldMixVouch.OutDeptId = outWarehouse.Dept;
                oldMixVouch.Modifier = operId;
                oldMixVouch.ModifyDate = DateTime.Now;
                oldMixVouch.ModifyTime = DateTime.Now;
                Uow.MixVouch.Update(oldMixVouch);
                Uow.Commit();
                //}
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
            return Json(mixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouch.Id);
                    if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    if (oldMixVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    if (oldMixVouch.IsVerify)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsVerify);
                    }
                    Uow.MixVouch.Delete(oldMixVouch);

                    List<DXInfo.Models.MixVouchs> lMixVouchs = Uow.MixVouchs.GetAll().Where(w => w.MVId == mixVouch.Id).ToList();
                    foreach (DXInfo.Models.MixVouchs mixVouchs in lMixVouchs)
                    {
                        Uow.MixVouchs.Delete(mixVouchs);
                    }
                    Uow.Commit();
                    transaction.Complete();
                }
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
            return NextMixVouch(mixVouch);
        }
        private void AddTransVouchByMixVouch(DXInfo.Models.MixVouch mixVouch, 
            List<DXInfo.Models.MixVouchs> lMixVouchs,Guid userId)
        {
            DXInfo.Models.VouchType vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.TransVouch);
            DXInfo.Models.TransVouch transVouch = Mapper.Map<DXInfo.Models.TransVouch>(mixVouch);

            transVouch.Maker = userId;
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
            transVouch.Code = GetRdRecordCode(vouchType);
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
        public JsonResult VerifyMixVouch(MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {                    
                    DXInfo.Models.MixVouch oldMixVouch = Uow.MixVouch.GetById(g=>g.Id==mixVouch.Id);
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
                    oldMixVouch.Verifier = userId;
                    oldMixVouch.VerifyDate = DateTime.Now;
                    oldMixVouch.VerifyTime = DateTime.Now;
                    Uow.MixVouch.Update(oldMixVouch);

                    List<DXInfo.Models.MixVouchs> lMixVouchs = Uow.MixVouchs.GetAll().Where(w => w.MVId == oldMixVouch.Id).ToList();
                    
                    AddTransVouchByMixVouch(oldMixVouch, lMixVouchs,userId);
                    Uow.Commit();
                    transaction.Complete();
                    retMixVouch = Mapper.Map<MixVouch>(oldMixVouch);
                    retMixVouch.IsModify = true;
                }
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnVerifyMixVouch(MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                Guid userId = operId;
                using (TransactionScope transaction = new TransactionScope())
                {
                    DXInfo.Models.MixVouch OldMixVouch = Uow.MixVouch.GetById(g=>g.Id==mixVouch.Id);
                    OldMixVouch.IsVerify = false;
                    if (businessCommon.IsBalance(OldMixVouch.MVDate, OldMixVouch.InWhId) ||
                        businessCommon.IsBalance(OldMixVouch.MVDate, OldMixVouch.OutWhId))
                    {
                        throw new DXInfo.Models.BusinessException("已月结不能操作单据");
                    }
                    List<DXInfo.Models.TransVouch> lTransVouch = Uow.TransVouch.GetAll().Where(w => w.SourceId == mixVouch.Id).ToList();
                    foreach (DXInfo.Models.TransVouch transVouch in lTransVouch)
                    {
                        if (transVouch.IsVerify)
                        {
                            throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "调拨单单");
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
                    OldMixVouch.Modifier = userId;
                    OldMixVouch.ModifyDate = DateTime.Now;
                    OldMixVouch.ModifyTime = DateTime.Now;
                    Uow.MixVouch.Update(OldMixVouch);

                    Uow.Commit();
                    transaction.Complete();
                    retMixVouch = Mapper.Map<MixVouch>(OldMixVouch);
                    retMixVouch.IsModify = true;
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (DXInfo.Models.BusinessException bex)
            {
                ExceptionPolicy.HandleException(bex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = bex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMixVouch()
        {
            Session[MixVouchsSession] = null;
            Session[BatchInventorySession] = null;
            Session[BatchWarehouseInventorySession] = null;
            CodeVouchDate cvd = new CodeVouchDate();
            cvd.Code = businessCommon.GetVouchCode(DXInfo.Models.VouchTypeCode.MixVouch);
            cvd.VouchDateId = "MVDate";
            cvd.CurDate = DateTime.Now.ToString("yyyy-MM-dd");
            cvd.Salesman = operId;
            return Json(cvd, JsonRequestBehavior.AllowGet);
        }
        private void SetupMixVouchsGridModel(JQGrid grid)
        {
            this.SetUpGrid(grid);
            grid.DataUrl = Url.Action("MixVouchs_RequestData");
            grid.EditUrl = Url.Action("MixVouchs_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populateEdit";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";

            SetDropDownColumn(grid, "InvId", this.GetInventory());
        }
        public ActionResult MixVouchs_RequestData(Guid? searchString)
        {
            var gridModel = new MixVouchs();
            SetupMixVouchsGridModel(gridModel.MixVouchsGrid);
            if (!searchString.HasValue)
            {
                
                if (Session[MixVouchsSession] != null)
                {
                    List<DXInfo.Models.MixVouchs> lMixVouchs = new List<DXInfo.Models.MixVouchs>();
                    lMixVouchs = Session[MixVouchsSession] as List<DXInfo.Models.MixVouchs>;
                    List<DXInfo.Models.Inventory> linventories = Uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage).ToList();
                    List<DXInfo.Models.UnitOfMeasures> lunitOfMeasure = Uow.UnitOfMeasures.GetAll().ToList();
                    List<DXInfo.Models.EnumTypeDescription> lenumTypeDescription = Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType).ToList();
                    List<DXInfo.Models.Locator> llocator = Uow.Locator.GetAll().ToList();
                    var records = from d in lMixVouchs
                                  join d1 in linventories on d.InvId equals d1.Id into dd1
                                  from dd1s in dd1.DefaultIfEmpty()
                                  join d3 in lunitOfMeasure on d.STUnit equals d3.Id into dd3
                                  from dd3s in dd3.DefaultIfEmpty()
                                  select new
                                  {
                                      d.Id,
                                      d.MVId,
                                      d.InvId,
                                      InvName = dd1s.Name,
                                      Specs = dd1s.Specs,
                                      STUnitName = dd3s == null ? "" : dd3s.Name,
                                      d.Num,
                                      d.Memo,
                                  };
                    return gridModel.MixVouchsGrid.DataBind(records.AsQueryable());
                }
                else
                {
                    List<object> lo = new List<object>();
                    var records = new
                    {
                        Id = "",
                        MVId = "",
                        InvId = "",
                        InvName = "",
                        Specs = "",
                        STUnitName = "",
                        LowStock = "",
                        Num = "",
                        Memo = "",
                    };
                    lo.Add(records);
                    var list = Enumerable.Repeat(records, 1).ToList();
                    return gridModel.MixVouchsGrid.DataBind(list.AsQueryable());
                }
            }
            else
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
                return gridModel.MixVouchsGrid.DataBind(records);
            }
        }
        public ActionResult MixVouchs_EditData(DXInfo.Models.MixVouchs mixVouchs)
        {
            var gridModel = new MixVouchs();
            SetupMixVouchsGridModel(gridModel.MixVouchsGrid);
            if (gridModel.MixVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
            {
                if (mixVouchs.MVId != Guid.Empty)
                {
                    //using (var context = db)
                    //{
                    var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouchs.MVId);
                    if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                    {
                        return gridModel.MixVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                    //}
                }
                else
                {
                    List<DXInfo.Models.MixVouchs> lmixVouchs = new List<DXInfo.Models.MixVouchs>();
                    if (Session[MixVouchsSession] != null)
                    {
                        lmixVouchs = Session[MixVouchsSession] as List<DXInfo.Models.MixVouchs>;
                    }
                    mixVouchs.Id = Guid.NewGuid();
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
                    lmixVouchs.Add(mixVouchs);
                    Session[MixVouchsSession] = lmixVouchs;
                }
            }
            if (gridModel.MixVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                if (mixVouchs.MVId != Guid.Empty)
                {
                    //using (var context = db)
                    //{
                    var oldMixVouchs = Uow.MixVouchs.GetById(g => g.Id == mixVouchs.Id);
                    var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == oldMixVouchs.MVId);
                    if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                    {
                        return gridModel.MixVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
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
                    //}
                }
                else
                {
                    if (Session[MixVouchsSession] != null)
                    {

                        List<DXInfo.Models.MixVouchs> lMixVouchs = Session[MixVouchsSession] as List<DXInfo.Models.MixVouchs>;
                        DXInfo.Models.MixVouchs oldMixVouchs = lMixVouchs.Find(delegate(DXInfo.Models.MixVouchs sub) { return sub.Id == mixVouchs.Id; });
                        oldMixVouchs.InvId = mixVouchs.InvId;
                        oldMixVouchs.Num = mixVouchs.Num;
                        oldMixVouchs.Memo = mixVouchs.Memo;
                        Session[MixVouchsSession] = lMixVouchs;
                    }
                }
            }
            if (gridModel.MixVouchsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
            {
                //using (var context = db)
                //{
                var oldMixVouchs = Uow.MixVouchs.GetById(g => g.Id == mixVouchs.Id);
                if (oldMixVouchs != null)
                {
                    var oldMixVouch = Uow.MixVouch.GetById(g => g.Id == oldMixVouchs.MVId);
                    if (businessCommon.IsBalance(oldMixVouch.MVDate, oldMixVouch.InWhId))
                    {
                        return gridModel.MixVouchsGrid.ShowEditValidationMessage("已月结不能操作单据");
                    }
                    Uow.MixVouchs.Delete(oldMixVouchs);
                    Uow.Commit();
                }
                //}
                if (Session[MixVouchsSession] != null)
                {

                    List<DXInfo.Models.MixVouchs> lMixVouchs = Session[MixVouchsSession] as List<DXInfo.Models.MixVouchs>;
                    lMixVouchs.RemoveAll(delegate(DXInfo.Models.MixVouchs sub) { return sub.Id == mixVouchs.Id; });
                    Session[MixVouchsSession] = lMixVouchs;
                }
            }
            return RedirectToAction("MixVouch");
        }
        public ActionResult MixVouch()
        {
            var gridModel = new MixVouchModel();
            gridModel.vouchType = Uow.VouchType.GetById(g=>g.Code==DXInfo.Models.VouchTypeCode.MixVouch);
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var mixVouchs = Uow.MixVouchs.GetById(g => g.Id == Id);
                if (mixVouchs == null)
                {
                    gridModel.mixVouch.Id = Id;
                }
                else
                {
                    gridModel.mixVouch.Id = mixVouchs.MVId;
                }
            }
            
            SetupMixVouchsGridModel(gridModel.mixVouchs.MixVouchsGrid);
            return View(gridModel);
        }


        private DXInfo.Models.MixVouch GetMixVouchOrderByDescending(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.MixVouch mixVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime < makeTime).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime < makeTime && w.InDeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.InDeptId == deptId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime < makeTime
                            && w.InDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.InDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderByDescending(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return mixVouch;
        }
        private DXInfo.Models.MixVouch GetMixVouchOrderBy(DateTime? makeTime)
        {
            int AuthorityType = GetVouchAuthority();
            DXInfo.Models.MixVouch mixVouch = null;
            
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime > makeTime).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime > makeTime && w.InDeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.InDeptId == deptId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    Guid userId = operId;
                    if (makeTime.HasValue)
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.MakeTime > makeTime
                            && w.InDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    else
                    {
                        mixVouch = Uow.MixVouch.GetAll().Where(w => w.InDeptId == deptId
                            && w.IsVerify ? w.Verifier == userId : w.Maker == userId).OrderBy(o => o.MakeTime).FirstOrDefault();
                    }
                    break;
            }
            return mixVouch;
        }
        public JsonResult CurMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                if (mixVouch.Id == null)
                {
                    return StartMixVouch();
                }
                var curMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouch.Id);
                if (curMixVouch == null)
                {
                    throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                }
                retMixVouch = Mapper.Map<MixVouch>(curMixVouch);
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartMixVouch()
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                var firstMixVouch = GetMixVouchOrderBy(null);
                if (firstMixVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retMixVouch = Mapper.Map<MixVouch>(firstMixVouch);
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PrevMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                if (mixVouch.Id == null)
                {
                    return StartMixVouch();
                }
                var curMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouch.Id);
                DateTime makeTime = mixVouch.MakeTime.Value;
                if (curMixVouch != null)
                {
                    makeTime = curMixVouch.MakeTime;
                }
                var prevMixVouch = GetMixVouchOrderByDescending(makeTime);
                if (prevMixVouch == null)
                {
                    if (curMixVouch == null)
                    {
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                    }
                    retMixVouch = Mapper.Map<MixVouch>(curMixVouch);
                }
                else
                {
                    retMixVouch = Mapper.Map<MixVouch>(prevMixVouch);
                }
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NextMixVouch([Bind(Prefix = "mixVouch")]MixVouch mixVouch)
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                if (mixVouch.Id == null)
                {
                    return EndMixVouch();
                }
                var curMixVouch = Uow.MixVouch.GetById(g => g.Id == mixVouch.Id);
                DateTime makeTime = mixVouch.MakeTime.Value;
                if (curMixVouch != null)
                {
                    makeTime = curMixVouch.MakeTime;
                }
                var nextMixVouch = GetMixVouchOrderBy(makeTime);
                if (nextMixVouch == null)
                {
                    if (curMixVouch == null)
                    {
                        return PrevMixVouch(mixVouch);
                    }
                    retMixVouch = Mapper.Map<MixVouch>(curMixVouch);
                }
                else
                {
                    retMixVouch = Mapper.Map<MixVouch>(nextMixVouch);
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return Json(new DXInfo.Models.JsonObject() { Sucess = false, Message = dex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EndMixVouch()
        {
            MixVouch retMixVouch = new MixVouch();
            try
            {
                var lastMixVouch = GetMixVouchOrderByDescending(null);
                if (lastMixVouch == null) throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.IsNull);
                retMixVouch = Mapper.Map<MixVouch>(lastMixVouch);
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
            return Json(retMixVouch, JsonRequestBehavior.AllowGet);
        }
        private void SetupMixVouchGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("MixVouch_RequestData");
            grid.EditUrl = Url.Action("MixVouch_EditData");
            SetDropDownColumn(grid, "Salesman", this.GetOper());
            SetDropDownColumn(grid, "InWhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "OutWhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsVerify");
        }
        public ActionResult MixVouch_RequestData()
        {
            var gridModel = new MixVouchGridModel();
            SetupMixVouchGridModel(gridModel.MixVouchGrid);

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
                              DeptId=dd6s.InDeptId,
                              dd6s.InWhId,
                              InWhName = dd7s.Name,
                              dd6s.OutWhId,
                              OutWhName = dd11s.Name,
                              dd6s.Salesman,
                              SalesmanName=dd10s.FullName,
                              dd6s.Maker,
                              dd6s.IsVerify,
                              dd6s.Verifier,
                              dd6s.VerifyDate,
                              dd6s.Memo,
                              SubId = d.Id==null?dd6s.Id:d.Id,
                              InvId=d.InvId==null?Guid.Empty:d.InvId,
                              InvName = dd1s.Name,
                              Specs = dd1s.Specs,
                              STUnit=d.STUnit==null?Guid.Empty:d.STUnit,
                              STUnitName = dd3s.Name,
                              Num=d.Num==null?0:d.Num,
                              SubMemo=d.Memo,
                          };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    records = records.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    records = records.Where(w => w.IsVerify ? w.Verifier == userId : w.Maker == userId);
                    break;
            }
            if (gridModel.MixVouchGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.MixVouchGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.MixVouchGrid.ExportToExcel(records, "库存配料单.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.MixVouchGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.MixVouchGrid.DataBind(records);
            }
        }
        public ActionResult MixVouch_EditData()
        {
            var gridModel = new MixVouchGridModel();
            SetupMixVouchGridModel(gridModel.MixVouchGrid);
            return RedirectToAction("SearchMixVouch");
        }
        public ActionResult SearchMixVouch()
        {
            var gridModel = new MixVouchGridModel();
            SetupMixVouchGridModel(gridModel.MixVouchGrid);
            return View(gridModel);
        }

        public ActionResult BatchInventory()
        {
            var gridModel = new BatchInventoryGridModel();
            SetupBatchInventoryGridModel(gridModel.BatchInventoryGrid);
            gridModel.WhId = Guid.Parse(this.HttpContext.Request["WhId"]);            
            return View(gridModel);
        }
        private void SetupBatchInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchInventory_RequestData");
            grid.EditUrl = Url.Action("BatchInventory_EditData");
        }
        public ActionResult BatchInventory_RequestData()
        {
            var gridModel = new BatchInventoryGridModel();
            SetupBatchInventoryGridModel(gridModel.BatchInventoryGrid);
            List<BatchInventoryList> lBIL = new List<BatchInventoryList>();
            Guid WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
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
            return RedirectToAction("BatchInventory", new { WhId = list.WhId });
        }
        public ActionResult BatchInventory_SyncData()
        {
            List<DXInfo.Models.MixVouchs> lmixVouchs = new List<DXInfo.Models.MixVouchs>();
            List<BatchInventoryList> lb = Session[BatchInventorySession] as List<BatchInventoryList>;
            if (lb != null)
            {
                List<BatchInventoryList> lb1 = lb.FindAll(f => f.Num > 0);
                foreach (BatchInventoryList list in lb1)
                {
                    DXInfo.Models.MixVouchs mixVouchs = new DXInfo.Models.MixVouchs();
                    mixVouchs.Id = Guid.NewGuid();
                    mixVouchs.InvId = list.InvId;
                    mixVouchs.Num = list.Num;
                    mixVouchs.Memo = list.Memo;
                    lmixVouchs.Add(mixVouchs);
                }
            }
            Session[MixVouchsSession] = lmixVouchs;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BatchWarehouseInventory()
        {
            var gridModel = new BatchWarehouseInventoryGridModel();
            SetupBatchWarehouseInventoryGridModel(gridModel.BatchWarehouseInventoryGrid);
            gridModel.WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
            return View(gridModel);
        }
        private void SetupBatchWarehouseInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchWarehouseInventory_RequestData");
            grid.EditUrl = Url.Action("BatchWarehouseInventory_EditData");
        }
        public ActionResult BatchWarehouseInventory_RequestData()
        {
            var gridModel = new BatchWarehouseInventoryGridModel();
            SetupBatchWarehouseInventoryGridModel(gridModel.BatchWarehouseInventoryGrid);
            List<BatchWarehouseInventoryList> lBIL = new List<BatchWarehouseInventoryList>();
            Guid WhId = Guid.Parse(this.HttpContext.Request["WhId"]);
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
            return RedirectToAction("BatchWarehouseInventory", new { WhId = list.WhId });
        }
        public ActionResult BatchWarehouseInventory_SyncData()
        {
            List<DXInfo.Models.MixVouchs> lmixVouchs = new List<DXInfo.Models.MixVouchs>();
            List<BatchWarehouseInventoryList> lb = Session[BatchWarehouseInventorySession] as List<BatchWarehouseInventoryList>;
            if (lb != null)
            {
                List<BatchWarehouseInventoryList> lb1 = lb.FindAll(f => f.Num > 0);
                foreach (BatchWarehouseInventoryList list in lb1)
                {
                    DXInfo.Models.MixVouchs mixVouchs = new DXInfo.Models.MixVouchs();
                    mixVouchs.Id = Guid.NewGuid();
                    mixVouchs.InvId = list.InvId;
                    mixVouchs.Num = list.Num;

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

                    mixVouchs.Memo = list.Memo;
                    lmixVouchs.Add(mixVouchs);
                }
            }
            Session[MixVouchsSession] = lmixVouchs;
            return Json(true, JsonRequestBehavior.AllowGet);
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
            
        }
        [Authorize]
        public ActionResult MonthBalance()
        {
            var gridModel = new MonthBalanceGridModel();
            SetupMonthBalanceGridModel(gridModel.MonthBalanceGrid);
            return View(gridModel);
        }
        public ActionResult MonthBalance_RequestData(int? page, int rows, string sord, string sidx)
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
                             };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    monthBalance = monthBalance.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    monthBalance = monthBalance.Where(w => w.DeptId == userId);
                    break;
            }
            return gridModel.MonthBalanceGrid.DataBind(monthBalance);
        }
        public ActionResult MonthBalance_EditData(DXInfo.Models.MonthBalance monthBalance)
        {
            var gridModel = new MonthBalanceGridModel();
            SetupMonthBalanceGridModel(gridModel.MonthBalanceGrid);
            try
            {
                if (gridModel.MonthBalanceGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    //using (var context = db)
                    //{
                    var count = Uow.MonthBalance.GetAll().Where(w => w.Period == monthBalance.Period && w.WhId == monthBalance.WhId).Count();
                    if (count > 0)
                        return gridModel.MonthBalanceGrid.ShowEditValidationMessage("月结记录重复");
                    DXInfo.Models.Warehouse warehouse = Uow.Warehouse.GetById(g => g.Id == monthBalance.WhId);
                    monthBalance.DeptId = warehouse.Dept;

                    monthBalance.Maker = operId;
                    monthBalance.MakeDate = DateTime.Now;
                    monthBalance.MakeTime = DateTime.Now;
                    Uow.MonthBalance.Add(monthBalance);
                    Uow.Commit();
                    //}
                }
                if (gridModel.MonthBalanceGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    //using (var context = db)
                    //{
                    var oldMonthBalance = Uow.MonthBalance.GetById(g => g.Id == monthBalance.Id);

                    if (oldMonthBalance.Period != monthBalance.Period || oldMonthBalance.WhId != monthBalance.WhId)
                    {
                        var count = Uow.MonthBalance.GetAll().Where(w => w.Period == monthBalance.Period && w.WhId == monthBalance.WhId).Count();
                        if (count > 0)
                            return gridModel.MonthBalanceGrid.ShowEditValidationMessage("月结记录重复");
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
                    //}
                }
                if (gridModel.MonthBalanceGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    //using (var context = db)
                    //{
                    var oldMonthBalance = Uow.MonthBalance.GetById(g => g.Id == monthBalance.Id);
                    if (oldMonthBalance.IsVerify)
                    {
                        return gridModel.MonthBalanceGrid.ShowEditValidationMessage("已审核不能删除");
                    }
                    Uow.MonthBalance.Delete(oldMonthBalance);
                    Uow.Commit();
                    //}
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.MonthBalanceGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("MonthBalance");
        }
        public JsonResult MonthBalance_Verify(Guid MonthBalanceId)
        {
            try
            {
                Guid userId = operId;
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
                    oldMonthBalance.Verifier = userId;
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
                        throw new DXInfo.Models.BusinessException(DXInfo.Models.BusinessExceptionType.DeleteIsVerify, "期初库存");
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
            grid.DataUrl = Url.Action("BatchStop_RequestData");
            grid.EditUrl = Url.Action("BatchStop_EditData");
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "StopFlag");
        }
        [Authorize]
        public ActionResult BatchStop()
        {
            var gridModel = new BatchStopGridModel();
            SetupBatchStopGridModel(gridModel.BatchStopGrid);
            return View(gridModel);
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
                             };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    batchStop = batchStop.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    batchStop = batchStop.Where(w => w.DeptId == userId);
                    break;
            }
            return gridModel.BatchStopGrid.DataBind(batchStop);
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
            return RedirectToAction("BatchStop");
        }
        #endregion

        #region 失效日期维护
        private void SetupInvalidDateGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("InvalidDate_RequestData");
            grid.EditUrl = Url.Action("InvalidDate_EditData");
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "StopFlag");
            if (isShelfLife)
            {
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());
            }

        }
        [Authorize]
        public ActionResult InvalidDate()
        {
            var gridModel = new InvalidDateGridModel();
            SetupInvalidDateGridModel(gridModel.InvalidDateGrid);
            return View(gridModel);
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
                            };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    invalidDate = invalidDate.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    invalidDate = invalidDate.Where(w => w.DeptId == userId);
                    break;
            }
            return gridModel.InvalidDateGrid.DataBind(invalidDate);
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
            return RedirectToAction("InvalidDate");
        }
        #endregion

        #region 库存账与货位账对账
        private void SetupStockLocatorGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("StockLocator_RequestData");
            grid.EditUrl = Url.Action("StockLocator_EditData");
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
        }
        [Authorize]
        public ActionResult StockLocator()
        {
            var gridModel = new StockLocatorGridModel();
            SetupStockLocatorGridModel(gridModel.StockLocatorGrid);
            return View(gridModel);
        }
        public ActionResult StockLocator_RequestData()
        {
            var gridModel = new StockLocatorGridModel();
            SetupStockLocatorGridModel(gridModel.StockLocatorGrid);
            var stockLocator = from d in Uow.CurrentStock.GetAll()
                               join d1 in 
                               (
                                 from dd in Uow.CurrentInvLocator.GetAll()
                                 group dd by new { dd.WhId, dd.InvId, dd.Batch} into g
                                 select new { g.Key.WhId, g.Key.InvId, g.Key.Batch, Num = g.Sum(s => s.Num) }
                               )
                               on new {d.WhId,d.InvId,d.Batch} equals new { d1.WhId,d1.InvId,d1.Batch}

                               join d2 in Uow.Warehouse.GetAll() on d.WhId equals d2.Id into dd2
                              from dd2s in dd2.DefaultIfEmpty()
                               join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                              from dd5s in dd5.DefaultIfEmpty()
                               join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                              from dd7s in dd7.DefaultIfEmpty()
                               join d8 in Uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on d.ShelfLifeType equals d8.Value into dd8
                              from dd8s in dd8.DefaultIfEmpty()
                              select new
                              {
                                  d.Id,
                                  DeptId=dd2s.Dept,
                                  d.WhId,
                                  WhName = dd2s.Name,
                                  d.InvId,
                                  InvName = dd5s.Name,
                                  dd5s.Specs,
                                  STUnitName = dd7s.Name,
                                  d.Num,
                                  LocatorNum = d1.Num,
                                  NumDif = d.Num-d1.Num,
                                  d.Batch,
                                  d.MadeDate,
                                  d.ShelfLife,
                                  d.ShelfLifeType,
                                  ShelfLifeTypeName = dd8s.Description,
                                  d.InvalidDate,
                              };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    stockLocator = stockLocator.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    stockLocator = stockLocator.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.StockLocatorGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.StockLocatorGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.StockLocatorGrid.ExportToExcel(stockLocator, "库存账与货位账对账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.StockLocatorGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.StockLocatorGrid.DataBind(stockLocator);
            }
        }
        public ActionResult StockLocator_EditData(DXInfo.Models.CurrentStock currentStock)
        {
            var gridModel = new StockLocatorGridModel();
            SetupStockLocatorGridModel(gridModel.StockLocatorGrid);
            return RedirectToAction("StockLocator");
        }
        #endregion

        #region 库存现存量
        private void SetupCurrentStockGridModel(JQGrid grid)
        {
            SetUpGrid(grid);
            grid.DataUrl = Url.Action("CurrentStock_RequestData");
            grid.EditUrl = Url.Action("CurrentStock_EditData");
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetDropDownColumn(grid, "Category", this.GetCategory());
            SetBoolColumn(grid, "StopFlag");

            if (isShelfLife)
            {
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());                
            }
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult CurrentStock()
        {
            var gridModel = new CurrentStockGridModel();
            SetupCurrentStockGridModel(gridModel.CurrentStockGrid);
            return View(gridModel);
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
                              select new
                              {
                                  d.Id,   
                                  DeptId=dd2s.Dept,
                                  d.WhId,
                                  WhName = dd2s.Name,
                                  d.InvId,
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
                              };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    currentStock = currentStock.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    currentStock = currentStock.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.CurrentStockGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.CurrentStockGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.CurrentStockGrid.ExportToExcel(currentStock, "库存现存量.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.CurrentStockGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.CurrentStockGrid.DataBind(currentStock);
            }
        }
        public ActionResult CurrentStock_EditData(DXInfo.Models.CurrentStock currentStock)
        {
            var gridModel = new CurrentStockGridModel();
            SetupCurrentStockGridModel(gridModel.CurrentStockGrid);            
            return RedirectToAction("CurrentStock");
        }
        #endregion

        #region 库存流水账
        private void SetupStockDayBookGridModel(JQGrid grid)
        {            
            grid.DataUrl = Url.Action("StockDayBook_RequestData");
            grid.EditUrl = Url.Action("StockDayBook_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetBoolColumn(grid, "IsVerify");

            if (isShelfLife)
            {
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());
            }
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult StockDayBook()
        {
            var gridModel = new StockDayBookGridModel();
            SetupStockDayBookGridModel(gridModel.StockDayBookGrid);
            return View(gridModel);
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

                               select new
                               {
                                   Id=dd1s.Id==null?d.Id:dd1s.Id,
                                   d.Code,
                                   d.RdDate,
                                   RdName=dd9s.Name,
                                   d.DeptId,
                                   d.WhId,
                                   WhName = dd2s.Name,
                                   d.Salesman,
                                   SalesmanName=dd10s.FullName,
                                   d.IsVerify,
                                   d.VerifyDate,
                                   InvId=dd1s.InvId==null?Guid.Empty:dd1s.InvId,
                                   InvName = dd5s.Name,
                                   dd5s.Specs,
                                   STUnitName = dd7s.Name,
                                   Num=dd1s.Num==null?0:dd1s.Num,
                                   dd1s.Batch,
                                   dd1s.MadeDate,
                                   dd1s.ShelfLife,
                                   dd1s.ShelfLifeType,
                                   ShelfLifeTypeName = dd8s.Description,
                                   dd1s.InvalidDate,
                               };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    stockDayBook = stockDayBook.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    stockDayBook = stockDayBook.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.StockDayBookGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.StockDayBookGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.StockDayBookGrid.ExportToExcel(stockDayBook, "库存流水账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.StockDayBookGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.StockDayBookGrid.DataBind(stockDayBook);
            }
        }
        public ActionResult StockDayBook_EditData()
        {
            var gridModel = new StockDayBookGridModel();
            SetupStockDayBookGridModel(gridModel.StockDayBookGrid);
            return RedirectToAction("StockDayBook");
        }
        #endregion

        #region 库存台账
        private void SetupStockBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("StockBook_RequestData");
            grid.EditUrl = Url.Action("StockBook_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult StockBook()
        {
            var gridModel = new StockBookGridModel();
            SetupStockBookGridModel(gridModel.StockBookGrid);
            return View(gridModel);
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
                               select new
                               {
                                   d.Id,
                                   dd1s.Code,
                                   DeptId=dd2s.Dept,
                                   d.WhId,
                                   WhName = dd2s.Name,
                                   d.InvId,
                                   InvName = dd5s.Name,
                                   dd5s.Specs,
                                   STUnitName = dd7s.Name,
                                   d.InNum,
                                   d.OutNum,
                                   d.Num,
                               };
            var stockBookGroup = from d in stockBook
                                 group d by new { d.Code,d.DeptId, d.WhId, d.WhName, d.InvId, d.InvName, d.Specs, d.STUnitName } into g
                                 select new
                                 {
                                     Id=Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.WhId,
                                     g.Key.WhName,
                                     g.Key.InvId,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num)
                                 };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    stockBookGroup = stockBookGroup.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    stockBookGroup = stockBookGroup.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.StockBookGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.StockBookGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.StockBookGrid.ExportToExcel(stockBookGroup, "库存台账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.StockBookGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.StockBookGrid.DataBind(stockBookGroup);
            }
        }
        public ActionResult StockBook_EditData()
        {
            var gridModel = new StockBookGridModel();
            SetupStockBookGridModel(gridModel.StockBookGrid);
            return RedirectToAction("StockBook");
        }
        #endregion

        #region 批次台账
        private void SetupBatchBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchBook_RequestData");
            grid.EditUrl = Url.Action("BatchBook_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            if (isShelfLife)
            {
                SetDropDownColumn(grid, "ShelfLifeType", centerCommon.GetShelfLifeType());
            }
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult BatchBook()
        {
            var gridModel = new BatchBookGridModel();
            SetupBatchBookGridModel(gridModel.BatchBookGrid);
            return View(gridModel);
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
                            select new
                            {
                                d.Id,
                                dd1s.Code,
                                DeptId=dd2s.Dept,
                                d.WhId,
                                WhName = dd2s.Name,
                                d.InvId,
                                InvName = dd5s.Name,
                                dd5s.Specs,
                                STUnitName = dd7s.Name,
                                d.InNum,
                                d.OutNum,
                                d.Num,
                                d.Batch,
                                d.MadeDate,
                                d.ShelfLife,
                                d.ShelfLifeType,
                                ShelfLifeTypeName = dd8s.Description,
                                d.InvalidDate,
                            };
            var batchBookGroup = from d in batchBook
                                 group d by new
                                 {
                                     d.Code,
                                     d.DeptId,
                                     d.WhId,
                                     d.WhName,
                                     d.InvId,
                                     d.InvName,
                                     d.Specs,
                                     d.STUnitName,
                                     d.Batch,
                                     d.MadeDate,
                                     d.ShelfLife,
                                     d.ShelfLifeType,
                                     d.ShelfLifeTypeName,
                                     d.InvalidDate,
                                 } into g
                                 select new
                                 {
                                     Id = Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.WhId,
                                     g.Key.WhName,
                                     g.Key.InvId,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num),
                                     g.Key.Batch,
                                     g.Key.MadeDate,
                                     g.Key.ShelfLife,
                                     g.Key.ShelfLifeType,
                                     g.Key.ShelfLifeTypeName,
                                     g.Key.InvalidDate,
                                 };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    batchBookGroup = batchBookGroup.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    batchBookGroup = batchBookGroup.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.BatchBookGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.BatchBookGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.BatchBookGrid.ExportToExcel(batchBookGroup, "库存批次台账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.BatchBookGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.BatchBookGrid.DataBind(batchBookGroup);
            }
        }
        public ActionResult BatchBook_EditData()
        {
            var gridModel = new BatchBookGridModel();
            SetupBatchBookGridModel(gridModel.BatchBookGrid);
            return RedirectToAction("BatchBook");
        }
        #endregion

        #region 货位台账
        private void SetupLocatorBookGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("LocatorBook_RequestData");
            grid.EditUrl = Url.Action("LocatorBook_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "WhId", centerCommon.GetWarehouse());
            SetDropDownColumn(grid, "InvId", this.GetInventory());
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult LocatorBook()
        {
            var gridModel = new LocatorBookGridModel();
            SetupLocatorBookGridModel(gridModel.LocatorBookGrid);
            return View(gridModel);
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
                            select new
                            {
                                d.Id,
                                dd1s.Code,
                                DeptId=dd2s.Dept,
                                d.WhId,
                                WhName = dd2s.Name,
                                LocatorName = dd9s.Name,
                                d.InvId,
                                InvName = dd5s.Name,
                                dd5s.Specs,
                                STUnitName = dd7s.Name,
                                d.InNum,
                                d.OutNum,
                                d.Num,
                            };
            var locatorBookGroup = from d in locatorBook
                                   group d by new { d.Code, d.DeptId,d.WhId, d.WhName, d.LocatorName, d.InvId, d.InvName, d.Specs, d.STUnitName } into g
                                 select new
                                 {
                                     Id = Guid.NewGuid(),
                                     g.Key.Code,
                                     g.Key.DeptId,
                                     g.Key.WhId,
                                     g.Key.WhName,
                                     g.Key.LocatorName,
                                     g.Key.InvId,
                                     g.Key.InvName,
                                     g.Key.Specs,
                                     g.Key.STUnitName,
                                     InNum = g.Sum(s => s.InNum),
                                     OutNum = g.Sum(s => s.OutNum),
                                     Num = g.Sum(s => s.Num)
                                 };
            int AuthorityType = GetVouchAuthority();
            
            Guid userId = operId;
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    locatorBookGroup = locatorBookGroup.Where(w => w.DeptId == deptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    locatorBookGroup = locatorBookGroup.Where(w => w.DeptId == userId);
                    break;
            }
            if (gridModel.LocatorBookGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.LocatorBookGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.LocatorBookGrid.ExportToExcel(locatorBookGroup, "库存货位台账.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.LocatorBookGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.LocatorBookGrid.DataBind(locatorBookGroup);
            }
        }
        public ActionResult LocatorBook_EditData()
        {
            var gridModel = new LocatorBookGridModel();
            SetupLocatorBookGridModel(gridModel.LocatorBookGrid);
            return RedirectToAction("LocatorBook");
        }
        #endregion

        #region 库存收发存汇总表
        private void SetupRdSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("RdSummary_RequestData");
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult RdSummary()
        {
            var gridModel = new RdSummaryGridModel();
            SetupRdSummaryGridModel(gridModel.RdSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return View(gridModel);
        }
        public ActionResult RdSummary_RequestData(DateTime? BeginDate, DateTime? EndDate, Guid? WhId, 
            string InvName)
        {
            
            var gridModel = new RdSummaryGridModel();
            SetupRdSummaryGridModel(gridModel.RdSummaryGrid);
            if (BeginDate.HasValue && EndDate.HasValue)
            {
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
                                where dd1s.RdDate<BeginDate.Value
                                select new SumamaryResult()
                                {
                                    Id=d.Id,
                                    DeptId=dd1s.DeptId,
                                    WhId=dd1s.WhId,
                                    WhName = dd2s.Name,
                                    InvName = dd5s.Name,
                                    Specs=dd5s.Specs,
                                    STUnitName = dd7s.Name,
                                    InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                    InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                    InNum=0,
                                    InAmount=0,
                                    OutNum=0,
                                    OutAmount=0,
                                    Num=0,
                                    Amount=0,
                                };
                if (WhId.HasValue) initRdSummary = initRdSummary.Where(w => w.WhId == WhId.Value);
                if (!string.IsNullOrEmpty(InvName)) initRdSummary = initRdSummary.Where(w => w.InvName.Contains(InvName));
                var initRdSummaryGroup = (from d in initRdSummary
                                     group d by new { d.DeptId,d.WhId,d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                     select new SumamaryResult()
                                     {
                                         Id = Guid.NewGuid(),
                                         DeptId=g.Key.DeptId,
                                         WhId = g.Key.WhId,
                                         WhName = g.Key.WhName,
                                         InvName = g.Key.InvName,
                                         Specs = g.Key.Specs,
                                         STUnitName = g.Key.STUnitName,
                                         InitNum = g.Sum(s => s.InitNum),
                                         InitAmount = g.Sum(s => s.InitAmount),
                                         InNum = 0,
                                         InAmount=0,
                                         OutNum = 0,
                                         OutAmount=0,
                                         Num = 0,
                                         Amount=0,
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
                                     where dd1s.RdDate >= BeginDate.Value && dd1s.RdDate<=EndDate.Value
                                     select new SumamaryResult()
                                     {
                                         Id = d.Id,
                                         DeptId=dd1s.DeptId,
                                         WhId = dd1s.WhId,
                                         WhName = dd2s.Name,
                                         InvName = dd5s.Name,
                                         Specs = dd5s.Specs,
                                         STUnitName = dd7s.Name,
                                         InitNum=0,
                                         InitAmount=0,
                                         InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                         InAmount=dd1s.RdFlag==0?d.Amount:0,
                                         OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                         OutAmount=dd1s.RdFlag==0?0:d.Amount,
                                         Num=0,
                                         Amount=0,
                                     };
                if(WhId.HasValue) inOutRdSummary = inOutRdSummary.Where(w=>w.WhId==WhId.Value);
                if (!string.IsNullOrEmpty(InvName)) inOutRdSummary = inOutRdSummary.Where(w => w.InvName.Contains(InvName));
                var inOutRdSummaryGroup = (from d in inOutRdSummary
                                          group d by new { d.DeptId,d.WhId,d.WhName, d.InvName, d.Specs, d.STUnitName } into g
                                          select new SumamaryResult()
                                          {
                                              Id = Guid.NewGuid(),
                                              DeptId=g.Key.DeptId,
                                              WhId=g.Key.WhId,
                                              WhName = g.Key.WhName,
                                              InvName = g.Key.InvName,
                                              Specs = g.Key.Specs,
                                              STUnitName = g.Key.STUnitName,
                                              InitNum=0,
                                              InitAmount=0,
                                              InNum = g.Sum(s => s.InNum),
                                              InAmount = g.Sum(s=>s.InAmount),
                                              OutNum = g.Sum(s => s.OutNum),
                                              OutAmount=g.Sum(s=>s.OutAmount),
                                              Num=0,
                                              Amount=0,
                                          }).ToList();
                initRdSummaryGroup.AddRange(inOutRdSummaryGroup);
                var rdSummaryGroup = (from d in initRdSummaryGroup
                                     group d by new { d.DeptId,d.WhId,d.WhName,d.InvName, d.Specs, d.STUnitName } into g
                                     select new SumamaryResult()
                                     {
                                         Id = Guid.NewGuid(),
                                         DeptId=g.Key.DeptId,
                                         WhId=g.Key.WhId,
                                         WhName=g.Key.WhName,
                                         InvName=g.Key.InvName,
                                         Specs=g.Key.Specs,
                                         STUnitName=g.Key.STUnitName,
                                         InitNum = g.Sum(s=>s.InitNum),
                                         InitAmount = g.Sum(s=>s.InitAmount),
                                         InNum = g.Sum(s => s.InNum),
                                         InAmount = g.Sum(s=>s.InAmount),
                                         OutNum = g.Sum(s => s.OutNum),
                                         OutAmount = g.Sum(s=>s.OutAmount),
                                         Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                         Amount=g.Sum(s=>s.InitAmount)+g.Sum(s=>s.InAmount)-g.Sum(s=>s.OutAmount),
                                     }).ToList();

                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                        rdSummaryGroup = rdSummaryGroup.Where(w => w.DeptId == deptId).ToList<SumamaryResult>();
                        break;
                    case (int)DXInfo.Models.AuthorityType.Self:
                        rdSummaryGroup = rdSummaryGroup.Where(w => w.DeptId == userId).ToList<SumamaryResult>();
                        break;
                }
                if (gridModel.RdSummaryGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.RdSummaryGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.RdSummaryGrid.ExportToExcel(rdSummaryGroup.AsQueryable(), "库存收发存汇总表.xls", gridModel.RdSummaryGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.RdSummaryGrid.DataBind(rdSummaryGroup.AsQueryable());
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    WhName = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    InitNum = "",
                    InitAmount="",
                    InNum = "",
                    InAmount="",
                    OutNum = "",
                    OutAmount="",
                    Num = "",
                    Amount=""
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.RdSummaryGrid.DataBind(list.AsQueryable());
            }
        }
        #endregion

        #region 库存收发存汇总表ByWh
        private void SetupRdSummaryByWhGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("RdSummaryByWh_RequestData");
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved+=new JQGridDataResolvedEventHandler(grid_DataResolved2);
            SetStockColumn(grid);
        }
        void grid_DataResolved2(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal InitNum = 0;
            decimal InitAmount = 0;

            decimal InNum = 0;
            decimal InAmount = 0;

            decimal OutNum = 0;
            decimal OutAmount = 0;

            decimal Num = 0;
            decimal Amount = 0;
            foreach (dynamic q in e.FilterData)
            {
                InitNum += q.InitNum;
                InitAmount += q.InitAmount;

                InNum += q.InNum;
                InAmount += q.InAmount;

                OutNum += q.OutNum;
                OutAmount += q.OutAmount;

                Num += q.Num;
                Amount += q.Amount;
            }
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
        [Authorize]
        public ActionResult RdSummaryByWh()
        {
            var gridModel = new RdSummaryByWhGridModel();
            SetupRdSummaryByWhGridModel(gridModel.RdSummaryByWhGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return View(gridModel);
        }
        public ActionResult RdSummaryByWh_RequestData(DateTime? BeginDate, DateTime? EndDate, Guid? WhId, string InventoryCategoryCode,
            string InventoryCategoryName)
        {
            var gridModel = new RdSummaryByWhGridModel();
            SetupRdSummaryByWhGridModel(gridModel.RdSummaryByWhGrid);
            if (BeginDate.HasValue && EndDate.HasValue)
            {
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

                                    where dd1s.RdDate < BeginDate.Value
                                    select new SumamaryResult()
                                    {
                                        Id = d.Id,
                                        DeptId = dd1s.DeptId,
                                        WhId = dd1s.WhId,
                                        InventoryCategoryCode=dd8s.Code,
                                        InventoryCategoryName = dd8s.Name,
                                        //WhName = dd2s.Name,
                                        //InvName = dd5s.Name,
                                        //Specs = dd5s.Specs,
                                        //STUnitName = dd7s.Name,
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
                                          group d by new { d.DeptId,d.WhId,d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                          select new SumamaryResult()
                                          {
                                              Id = Guid.NewGuid(),
                                              DeptId = g.Key.DeptId,
                                              WhId = g.Key.WhId,
                                              InventoryCategoryCode=g.Key.InventoryCategoryCode,
                                              InventoryCategoryName = g.Key.InventoryCategoryName,
                                              //WhName = g.Key.WhName,
                                              //InvName = g.Key.InvName,
                                              //Specs = g.Key.Specs,
                                              //STUnitName = g.Key.STUnitName,
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
                                     //join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                     //from dd7s in dd7.DefaultIfEmpty()
                                     join d8 in Uow.InventoryCategory.GetAll() on dd5s.Category equals d8.Id into dd8
                                     from dd8s in dd8.DefaultIfEmpty()

                                     where dd1s.RdDate >= BeginDate.Value && dd1s.RdDate <= EndDate.Value
                                     select new SumamaryResult()
                                     {
                                         Id = d.Id,
                                         DeptId = dd1s.DeptId,
                                         WhId = dd1s.WhId,
                                         InventoryCategoryCode = dd8s.Code,
                                         InventoryCategoryName = dd8s.Name,
                                         //WhName = dd2s.Name,
                                         //InvName = dd5s.Name,
                                         //Specs = dd5s.Specs,
                                         //STUnitName = dd7s.Name,
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
                                           group d by new { d.DeptId,d.WhId, d.InventoryCategoryCode,d.InventoryCategoryName } into g
                                           select new SumamaryResult()
                                           {
                                               Id = Guid.NewGuid(),
                                               DeptId = g.Key.DeptId,
                                               WhId = g.Key.WhId,
                                               InventoryCategoryCode=g.Key.InventoryCategoryCode,
                                               InventoryCategoryName=g.Key.InventoryCategoryName,
                                               //WhName = g.Key.WhName,
                                               //InvName = g.Key.InvName,
                                               //Specs = g.Key.Specs,
                                               //STUnitName = g.Key.STUnitName,
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
                                      group d by new { d.DeptId,d.WhId,d.InventoryCategoryCode, d.InventoryCategoryName } into g
                                      select new SumamaryResult()
                                      {
                                          Id = Guid.NewGuid(),
                                          DeptId = g.Key.DeptId,
                                          //WhId = g.Key.WhId,
                                          InventoryCategoryCode=g.Key.InventoryCategoryCode,
                                          InventoryCategoryName=g.Key.InventoryCategoryName,
                                          //WhName = g.Key.WhName,
                                          //InvName = g.Key.InvName,
                                          //Specs = g.Key.Specs,
                                          //STUnitName = g.Key.STUnitName,
                                          InitNum = g.Sum(s => s.InitNum),
                                          InitAmount = g.Sum(s => s.InitAmount),
                                          InNum = g.Sum(s => s.InNum),
                                          InAmount = g.Sum(s => s.InAmount),
                                          OutNum = g.Sum(s => s.OutNum),
                                          OutAmount = g.Sum(s => s.OutAmount),
                                          Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                          Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                      }).ToList();

                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                        rdSummaryGroup = rdSummaryGroup.Where(w => w.DeptId == deptId).ToList<SumamaryResult>();
                        break;
                    case (int)DXInfo.Models.AuthorityType.Self:
                        rdSummaryGroup = rdSummaryGroup.Where(w => w.DeptId == userId).ToList<SumamaryResult>();
                        break;
                }
                var rdSummaryGroup2 = (from d in rdSummaryGroup
                                      group d by new { d.InventoryCategoryCode,d.InventoryCategoryName } into g
                                      select new SumamaryResult()
                                      {
                                          Id = Guid.NewGuid(),
                                          //DeptId = g.Key.DeptId,
                                          //WhId = g.Key.WhId,
                                          InventoryCategoryCode=g.Key.InventoryCategoryCode,
                                          InventoryCategoryName = g.Key.InventoryCategoryName,
                                          //WhName = g.Key.WhName,
                                          //InvName = g.Key.InvName,
                                          //Specs = g.Key.Specs,
                                          //STUnitName = g.Key.STUnitName,
                                          InitNum = g.Sum(s => s.InitNum),
                                          InitAmount = g.Sum(s => s.InitAmount),
                                          InNum = g.Sum(s => s.InNum),
                                          InAmount = g.Sum(s => s.InAmount),
                                          OutNum = g.Sum(s => s.OutNum),
                                          OutAmount = g.Sum(s => s.OutAmount),
                                          Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                          Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                      }).ToList();
                if (gridModel.RdSummaryByWhGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.RdSummaryByWhGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.RdSummaryByWhGrid.ExportToExcel(rdSummaryGroup2.AsQueryable(), "库存收发存汇总表2.xls", gridModel.RdSummaryByWhGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.RdSummaryByWhGrid.DataBind(rdSummaryGroup2.AsQueryable());
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    InventoryCategoryCode="",
                    InventoryCategoryName="",
                    //WhName = "",
                    //InvName = "",
                    //Specs = "",
                    //STUnitName = "",
                    InitNum = 0,
                    InitAmount = 0,
                    InNum = 0,
                    InAmount = 0,
                    OutNum = 0,
                    OutAmount = 0,
                    Num = 0,
                    Amount = 0
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.RdSummaryByWhGrid.DataBind(list.AsQueryable());
            }
        }
        #endregion

        #region 库存批次汇总表
        private void SetupBatchSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BatchSummary_RequestData");
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult BatchSummary()
        {
            var gridModel = new BatchSummaryGridModel();
            SetupBatchSummaryGridModel(gridModel.BatchSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return View(gridModel);
        }
        public ActionResult BatchSummary_RequestData(DateTime? BeginDate, DateTime? EndDate, Guid? WhId,string Batch,
            string InvName)
        {

            var gridModel = new BatchSummaryGridModel();
            SetupBatchSummaryGridModel(gridModel.BatchSummaryGrid);
            if (BeginDate.HasValue && EndDate.HasValue)
            {
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

                                       where dd1s.RdDate < BeginDate.Value
                                       select new SumamaryResult()
                                       {
                                           Id = d.Id,
                                           DeptId=dd1s.DeptId,
                                           WhId = dd1s.WhId,
                                           WhName = dd2s.Name,
                                           InvName = dd5s.Name,
                                           Specs = dd5s.Specs,
                                           STUnitName = dd7s.Name,
                                           InitNum = ((dd1s.RdFlag == 0 ? d.Num : 0) - (dd1s.RdFlag == 0 ? 0 : d.Num)),
                                           InitAmount = ((dd1s.RdFlag == 0 ? d.Amount : 0) - (dd1s.RdFlag == 0 ? 0 : d.Amount)),
                                           InNum = 0,
                                           InAmount=0,
                                           OutNum = 0,
                                           OutAmount=0,
                                           Num = 0,
                                           Amount=0,
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
                                                 DeptId=g.Key.DeptId,
                                                 WhId = g.Key.WhId,
                                                 WhName = g.Key.WhName,
                                                 InvName = g.Key.InvName,
                                                 Specs = g.Key.Specs,
                                                 STUnitName = g.Key.STUnitName,
                                                 InitNum = g.Sum(s => s.InitNum),
                                                 InitAmount=g.Sum(s=>s.InitAmount),
                                                 InNum = 0,
                                                 InAmount=0,
                                                 OutNum = 0,
                                                 OutAmount=0,
                                                 Num = 0,
                                                 Amount=0,
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
                                     where dd1s.RdDate >= BeginDate.Value && dd1s.RdDate <= EndDate.Value
                                     select new SumamaryResult()
                                     {
                                         Id = d.Id,
                                         DeptId=dd1s.DeptId,
                                         WhId = dd1s.WhId,
                                         WhName = dd2s.Name,
                                         InvName = dd5s.Name,
                                         Specs = dd5s.Specs,
                                         STUnitName = dd7s.Name,
                                         InitNum = 0,
                                         InitAmount=0,
                                         InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                         InAmount = dd1s.RdFlag==0?d.Amount:0,
                                         OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                         OutAmount = dd1s.RdFlag==0?0:d.Amount,
                                         Num = 0,
                                         Amount=0,
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
                                                  DeptId=g.Key.DeptId,
                                                  WhId = g.Key.WhId,
                                                  WhName = g.Key.WhName,
                                                  InvName = g.Key.InvName,
                                                  Specs = g.Key.Specs,
                                                  STUnitName = g.Key.STUnitName,
                                                  InitNum = 0,
                                                  InitAmount=0,
                                                  InNum = g.Sum(s => s.InNum),
                                                  InAmount = g.Sum(s=>s.InAmount),
                                                  OutNum = g.Sum(s => s.OutNum),
                                                  OutAmount = g.Sum(s=>s.OutAmount),
                                                  Num = 0,
                                                  Amount=0,
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
                                             DeptId=g.Key.DeptId,
                                             WhId = g.Key.WhId,
                                             WhName = g.Key.WhName,
                                             InvName = g.Key.InvName,
                                             Specs = g.Key.Specs,
                                             STUnitName = g.Key.STUnitName,
                                             InitNum = g.Sum(s => s.InitNum),
                                             InitAmount=g.Sum(s=>s.InitAmount),
                                             InNum = g.Sum(s => s.InNum),
                                             InAmount = g.Sum(s=>s.InAmount),
                                             OutNum = g.Sum(s => s.OutNum),
                                             OutAmount=g.Sum(s=>s.OutAmount),
                                             Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                             Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                             Batch = g.Key.Batch,
                                             MadeDate = g.Key.MadeDate,
                                             ShelfLife = g.Key.ShelfLife,
                                             ShelfLifeTypeName = g.Key.ShelfLifeTypeName,
                                             InvalidDate = g.Key.InvalidDate,
                                         }).ToList();
                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                        batchSummaryGroup = batchSummaryGroup.Where(w => w.DeptId == deptId).ToList<SumamaryResult>();
                        break;
                    case (int)DXInfo.Models.AuthorityType.Self:
                        batchSummaryGroup = batchSummaryGroup.Where(w => w.DeptId == userId).ToList<SumamaryResult>();
                        break;
                }
                if (gridModel.BatchSummaryGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.BatchSummaryGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.BatchSummaryGrid.ExportToExcel(batchSummaryGroup.AsQueryable(), "库存批次汇总表.xls", gridModel.BatchSummaryGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.BatchSummaryGrid.DataBind(batchSummaryGroup.AsQueryable());
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    WhName = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    InitNum = "",
                    InitAmount="",
                    InNum = "",
                    InAmount="",
                    OutNum = "",
                    OutAmount="",
                    Num = "",
                    Amount="",
                    Batch = "",
                    MadeDate = "",
                    ShelfLife = "",
                    ShelfLifeTypeName = "",
                    InvalidDate = "",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.BatchSummaryGrid.DataBind(list.AsQueryable());
            }
        }
        #endregion

        #region 库存货位汇总表
        private void SetupLocatorSummaryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("LocatorSummary_RequestData");
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult LocatorSummary()
        {
            var gridModel = new LocatorSummaryGridModel();
            SetupLocatorSummaryGridModel(gridModel.LocatorSummaryGrid);
            gridModel.BeginDate = DateTime.Now.Date;
            gridModel.EndDate = DateTime.Now.Date;
            return View(gridModel);
        }
        public ActionResult LocatorSummary_RequestData(DateTime? BeginDate, DateTime? EndDate, Guid? WhId,Guid? Locator,
            string InvName)
        {

            var gridModel = new LocatorSummaryGridModel();
            SetupLocatorSummaryGridModel(gridModel.LocatorSummaryGrid);
            if (BeginDate.HasValue && EndDate.HasValue)
            {
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
                                    where dd1s.RdDate < BeginDate.Value
                                    select new SumamaryResult()
                                    {
                                        Id = d.Id,
                                        DeptId=dd1s.DeptId,
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
                                        InAmount=0,
                                        OutNum = 0,
                                        OutAmount=0,
                                        Num = 0,
                                        Amount=0,
                                    };
                if (WhId.HasValue) initLocatorSummary = initLocatorSummary.Where(w => w.WhId == WhId.Value);
                if (Locator.HasValue) initLocatorSummary = initLocatorSummary.Where(w => w.Locator == Locator.Value);
                if (!string.IsNullOrEmpty(InvName)) initLocatorSummary = initLocatorSummary.Where(w => w.InvName.Contains(InvName));
                var initLocatorSummaryGroup = (from d in initLocatorSummary
                                          group d by new { d.DeptId,d.WhId, d.WhName,d.Locator,d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                          select new SumamaryResult()
                                          {
                                              Id = Guid.NewGuid(),
                                              DeptId=g.Key.DeptId,
                                              WhId = g.Key.WhId,
                                              WhName = g.Key.WhName,
                                              Locator = g.Key.Locator,
                                              LocatorName = g.Key.LocatorName,
                                              InvName = g.Key.InvName,
                                              Specs = g.Key.Specs,
                                              STUnitName = g.Key.STUnitName,
                                              InitNum = g.Sum(s => s.InitNum),
                                              InitAmount=g.Sum(s=>s.InitAmount),
                                              InNum = 0,
                                              InAmount = 0,
                                              OutNum = 0,
                                              OutAmount=0,
                                              Num = 0,
                                              Amount=0,
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
                                     where dd1s.RdDate >= BeginDate.Value && dd1s.RdDate <= EndDate.Value
                                     select new SumamaryResult()
                                     {
                                         Id = d.Id,
                                         DeptId=dd1s.DeptId,
                                         WhId = dd1s.WhId,
                                         WhName = dd2s.Name,
                                         Locator = d.Locator,
                                         LocatorName = dd8s.Name,
                                         InvName = dd5s.Name,
                                         Specs = dd5s.Specs,
                                         STUnitName = dd7s.Name,
                                         InitNum = 0,
                                         InitAmount=0,
                                         InNum = dd1s.RdFlag == 0 ? d.Num : 0,
                                         InAmount = dd1s.RdFlag == 0 ? d.Amount : 0,
                                         OutNum = dd1s.RdFlag == 0 ? 0 : d.Num,
                                         OutAmount = dd1s.RdFlag == 0 ? 0 : d.Amount,
                                         Num = 0,
                                         Amount=0,
                                     };
                if (WhId.HasValue) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.WhId == WhId.Value);
                if (Locator.HasValue) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.Locator == Locator.Value);
                if (!string.IsNullOrEmpty(InvName)) inOutLocatorSummary = inOutLocatorSummary.Where(w => w.InvName.Contains(InvName));
                var inOutLocatorSummaryGroup = (from d in inOutLocatorSummary
                                           group d by new { d.DeptId,d.WhId, d.WhName,d.Locator,d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                           select new SumamaryResult()
                                           {
                                               Id = Guid.NewGuid(),
                                               DeptId=g.Key.DeptId,
                                               WhId = g.Key.WhId,
                                               WhName = g.Key.WhName,
                                               Locator = g.Key.Locator,
                                               LocatorName = g.Key.LocatorName,
                                               InvName = g.Key.InvName,
                                               Specs = g.Key.Specs,
                                               STUnitName = g.Key.STUnitName,
                                               InitNum = 0,
                                               InitAmount=0,
                                               InNum = g.Sum(s => s.InNum),
                                               InAmount=g.Sum(s=>s.InAmount),
                                               OutNum = g.Sum(s => s.OutNum),
                                               OutAmount=g.Sum(s=>s.OutAmount),
                                               Num = 0,
                                               Amount=0,
                                           }).ToList();
                initLocatorSummaryGroup.AddRange(inOutLocatorSummaryGroup);
                var locatorSummaryGroup = (from d in initLocatorSummaryGroup
                                      group d by new { d.DeptId,d.WhId, d.WhName,d.Locator,d.LocatorName, d.InvName, d.Specs, d.STUnitName } into g
                                      select new SumamaryResult()
                                      {
                                          Id = Guid.NewGuid(),
                                          DeptId=g.Key.DeptId,
                                          WhId = g.Key.WhId,
                                          WhName = g.Key.WhName,
                                          Locator = g.Key.Locator,
                                          LocatorName = g.Key.LocatorName,
                                          InvName = g.Key.InvName,
                                          Specs = g.Key.Specs,
                                          STUnitName = g.Key.STUnitName,
                                          InitNum = g.Sum(s => s.InitNum),
                                          InitAmount=g.Sum(s=>s.InitAmount),
                                          InNum = g.Sum(s => s.InNum),
                                          InAmount=g.Sum(s=>s.InAmount),
                                          OutNum = g.Sum(s => s.OutNum),
                                          OutAmount=g.Sum(s=>s.OutAmount),
                                          Num = g.Sum(s => s.InitNum) + g.Sum(s => s.InNum) - g.Sum(s => s.OutNum),
                                          Amount = g.Sum(s => s.InitAmount) + g.Sum(s => s.InAmount) - g.Sum(s => s.OutAmount),
                                      }).ToList();
                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                        locatorSummaryGroup = locatorSummaryGroup.Where(w => w.DeptId == deptId).ToList<SumamaryResult>();
                        break;
                    case (int)DXInfo.Models.AuthorityType.Self:
                        locatorSummaryGroup = locatorSummaryGroup.Where(w => w.DeptId == userId).ToList<SumamaryResult>();
                        break;
                }
                if (gridModel.LocatorSummaryGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.LocatorSummaryGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.LocatorSummaryGrid.ExportToExcel(locatorSummaryGroup.AsQueryable(), "库存货位汇总表.xls", gridModel.LocatorSummaryGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.LocatorSummaryGrid.DataBind(locatorSummaryGroup.AsQueryable());
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    WhName = "",
                    LocatorName="",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    InitNum = "",
                    InitAmount = "",
                    InNum = "",
                    InAmount = "",
                    OutNum = "",
                    OutAmount = "",
                    Num = "",
                    Amount = ""
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.LocatorSummaryGrid.DataBind(list.AsQueryable());
            }
        }
        #endregion

        #region 库存保质期预警
        private void SetupShelfLifeWarningGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ShelfLifeWarning_RequestData");
            SetStockColumn(grid);
        }
        [Authorize]
        public ActionResult ShelfLifeWarning()
        {
            var gridModel = new ShelfLifeWarningGridModel();
            SetupShelfLifeWarningGridModel(gridModel.ShelfLifeWarningGrid);
            return View(gridModel);
        }
        public ActionResult ShelfLifeWarning_RequestData(int? InvType,DateTime? BeginDate,DateTime? EndDate,
            int? OutOfDays, int? BeginCloseToDays, int? EndCloseToDays)
        {
            var gridModel = new ShelfLifeWarningGridModel();
            SetupShelfLifeWarningGridModel(gridModel.ShelfLifeWarningGrid);
            if (InvType.HasValue)
            {
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
                                       
                                       select new
                                       {
                                           d.Id,
                                           DeptId=dd2s.Dept,
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
                if (InvType.HasValue)
                {
                    switch (InvType.Value)
                    {
                        case 1:
                            shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate <= dtNow);
                            break;
                        case 2:
                            shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate > dtNow);
                            break;
                        case 3:
                            shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate > dtNow && SqlFunctions.DateDiff("dd",dtNow, w.InvalidDate) <=w.EarlyWarningDay);
                            break;
                    }
                }
                if (BeginDate.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate >= BeginDate.Value);
                if (EndDate.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => w.InvalidDate <= EndDate.Value);
                if (OutOfDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd",w.InvalidDate,dtNow) >= OutOfDays);
                if (BeginCloseToDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd",dtNow, w.InvalidDate) >= BeginCloseToDays);
                if (EndCloseToDays.HasValue) shelfLifeWarning = shelfLifeWarning.Where(w => SqlFunctions.DateDiff("dd", dtNow, w.InvalidDate) <= EndCloseToDays);

                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                        shelfLifeWarning = shelfLifeWarning.Where(w => w.DeptId == deptId);
                        break;
                    case (int)DXInfo.Models.AuthorityType.Self:
                        shelfLifeWarning = shelfLifeWarning.Where(w => w.DeptId == userId);
                        break;
                }
                
                if (gridModel.ShelfLifeWarningGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.ShelfLifeWarningGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.ShelfLifeWarningGrid.ExportToExcel(shelfLifeWarning, "库存保质期预警.xls", gridModel.ShelfLifeWarningGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.ShelfLifeWarningGrid.DataBind(shelfLifeWarning);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    WhName = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    Num = "",
                    Batch = "",
                    MadeDate = "",
                    ShelfLife = "",
                    ShelfLifeTypeName = "",
                    InvalidDate = "",
                    LocatorName = "",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.ShelfLifeWarningGrid.DataBind(list.AsQueryable());
            }
        }
        
        #endregion

        #region 库存安全库存预警
        private void SetupSecurityStockGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("SecurityStock_RequestData");
            SetStockColumn(grid);
        }
        public ActionResult SecurityStock_RequestData(int? QueryType )
        {
            var gridModel = new SecurityStockGridModel();
            SetupSecurityStockGridModel(gridModel.SecurityStockGrid);
            if (QueryType.HasValue)
            {
                DateTime dtNow = DateTime.Now.Date;
                var securityStock = from d in Uow.CurrentStock.GetAll()
                                    join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                       from dd5s in dd5.DefaultIfEmpty()
                                    join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                       from dd7s in dd7.DefaultIfEmpty()
                                       select new
                                       {
                                           d.Id,
                                           InvName = dd5s.Name,
                                           dd5s.Specs,
                                           STUnitName = dd7s.Name,
                                           SecurityStock = dd5s.SecurityStock,
                                           d.Num,
                                       };
                var securityStockGroup = from d in securityStock
                                         group d by new { d.InvName, d.Specs, d.STUnitName, d.SecurityStock } into g
                                         select new
                                         {
                                             Id = Guid.NewGuid(),
                                             g.Key.InvName,
                                             g.Key.Specs,
                                             g.Key.STUnitName,
                                             g.Key.SecurityStock,
                                             Num = g.Sum(s => s.Num),
                                             DifNum = g.Sum(s => s.Num) - g.Key.SecurityStock
                                         };
                if (QueryType.HasValue)
                {
                    switch (QueryType.Value)
                    {
                        case 1:
                            securityStockGroup = securityStockGroup.Where(w => w.DifNum > 0);
                            break;
                        case 2:
                            securityStockGroup = securityStockGroup.Where(w => w.DifNum <= 0);
                            break;
                    }
                }
                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                    case (int)DXInfo.Models.AuthorityType.Self:
                        securityStockGroup = securityStockGroup.Where(w => w.Id == userId);
                        break;
                }
                if (gridModel.SecurityStockGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.SecurityStockGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.SecurityStockGrid.ExportToExcel(securityStockGroup, "库存安全库存预警.xls", gridModel.SecurityStockGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.SecurityStockGrid.DataBind(securityStockGroup);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    SecurityStock="",
                    Num = "",
                    DIfNum="",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.SecurityStockGrid.DataBind(list.AsQueryable());
            }
        }        
        public ActionResult SecurityStock()
        {
            var gridModel = new SecurityStockGridModel();
            SetupSecurityStockGridModel(gridModel.SecurityStockGrid);
            return View(gridModel);
        }
        #endregion

        #region 库存超储存货查询
        private void SetupAboveStockGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("AboveStock_RequestData");
            grid.ExcelExportSettings.Url = Url.Action("AboveStock_RequestData");
            SetStockColumn(grid);
        }
        public ActionResult AboveStock_RequestData(int? QueryType )
        {
            var gridModel = new AboveStockGridModel();
            SetupAboveStockGridModel(gridModel.AboveStockGrid);
            if (QueryType.HasValue)
            {
                DateTime dtNow = DateTime.Now.Date;
                var aboveStock = from d in Uow.CurrentStock.GetAll()
                                 join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                    from dd5s in dd5.DefaultIfEmpty()
                                 join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                    from dd7s in dd7.DefaultIfEmpty()
                                    select new
                                    {
                                        d.Id,
                                        InvName = dd5s.Name,
                                        dd5s.Specs,
                                        STUnitName = dd7s.Name,
                                        HighStock = dd5s.HighStock,
                                        d.Num,
                                    };
                var aboveStockGroup = from d in aboveStock
                                      group d by new { d.InvName, d.Specs, d.STUnitName, d.HighStock } into g
                                      select new
                                      {
                                          Id = Guid.NewGuid(),
                                          g.Key.InvName,
                                          g.Key.Specs,
                                          g.Key.STUnitName,
                                          g.Key.HighStock,
                                          Num = g.Sum(s => s.Num),
                                          DifNum = g.Sum(s => s.Num) - g.Key.HighStock
                                      };
                if (QueryType.HasValue)
                {
                    switch (QueryType.Value)
                    {
                        case 1:
                            aboveStockGroup = aboveStockGroup.Where(w => w.DifNum > 0);
                            break;
                    }
                }
                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                    case (int)DXInfo.Models.AuthorityType.Self:
                        aboveStockGroup = aboveStockGroup.Where(w => w.Id == userId);
                        break;
                }
                if (gridModel.AboveStockGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.AboveStockGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.AboveStockGrid.ExportToExcel(aboveStockGroup, "库存超储存货查询.xls", gridModel.AboveStockGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.AboveStockGrid.DataBind(aboveStockGroup);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    HighStock = "",
                    Num = "",
                    DIfNum = "",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.AboveStockGrid.DataBind(list.AsQueryable());
            }
        }
        public ActionResult AboveStock()
        {
            var gridModel = new AboveStockGridModel();
            SetupAboveStockGridModel(gridModel.AboveStockGrid);
            return View(gridModel);
        }
        #endregion

        #region 库存短缺存货查询
        private void SetupLowStockGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("LowStock_RequestData");
            SetStockColumn(grid);
        }
        public ActionResult LowStock_RequestData(int? QueryType)
        {
            var gridModel = new LowStockGridModel();
            SetupLowStockGridModel(gridModel.LowStockGrid);
            if (QueryType.HasValue)
            {
                DateTime dtNow = DateTime.Now.Date;
                var lowStock = from d in Uow.CurrentStock.GetAll()
                               join d5 in Uow.Inventory.GetAll() on d.InvId equals d5.Id into dd5
                                 from dd5s in dd5.DefaultIfEmpty()
                               join d7 in Uow.UnitOfMeasures.GetAll() on d.STUnit equals d7.Id into dd7
                                 from dd7s in dd7.DefaultIfEmpty()
                                 select new
                                 {
                                     d.Id,
                                     InvName = dd5s.Name,
                                     dd5s.Specs,
                                     STUnitName = dd7s.Name,
                                     LowStock = dd5s.LowStock,
                                     d.Num,
                                 };
                var lowStockGroup = from d in lowStock
                                    group d by new { d.InvName, d.Specs, d.STUnitName, d.LowStock } into g
                                    select new
                                    {
                                        Id = Guid.NewGuid(),
                                        g.Key.InvName,
                                        g.Key.Specs,
                                        g.Key.STUnitName,
                                        g.Key.LowStock,
                                        Num = g.Sum(s => s.Num),
                                        DifNum = g.Key.LowStock-g.Sum(s => s.Num)
                                    };
                if (QueryType.HasValue)
                {
                    switch (QueryType.Value)
                    {
                        case 1:
                            lowStockGroup = lowStockGroup.Where(w => w.DifNum > 0);
                            break;
                    }
                }
                int AuthorityType = GetVouchAuthority();
                
                Guid userId = operId;
                switch (AuthorityType)
                {
                    case (int)DXInfo.Models.AuthorityType.All:
                        break;
                    case (int)DXInfo.Models.AuthorityType.Dept:
                    case (int)DXInfo.Models.AuthorityType.Self:
                        lowStockGroup = lowStockGroup.Where(w => w.Id == userId);
                        break;
                }
                if (gridModel.LowStockGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.LowStockGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.LowStockGrid.ExportToExcel(lowStockGroup, "库存短缺存货查询.xls", gridModel.LowStockGrid.GetState());
                    return View();
                }
                else
                {
                    return gridModel.LowStockGrid.DataBind(lowStockGroup);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    InvName = "",
                    Specs = "",
                    STUnitName = "",
                    LowStock = "",
                    Num = "",
                    DIfNum = "",
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.LowStockGrid.DataBind(list.AsQueryable());
            }
        }
        public ActionResult LowStock()
        {
            var gridModel = new LowStockGridModel();
            SetupLowStockGridModel(gridModel.LowStockGrid);
            return View(gridModel);
        }
        #endregion

        #region 配方
        private void SetupBillOfMaterialsGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BillOfMaterials_RequestData");
            grid.EditUrl = Url.Action("BillOfMaterials_EditData");

            grid.ExportSettings.ExportUrl = "BillOfMaterials_RequestData";
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
            return View(gridModel);
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
                           PartInvCode=dd1s.Code,
                           PartInvName=dd1s.Name,
                           PartSpecs = dd1s.Specs,
                           PartGroupName=dd2s.Name,
                           PartStockUnitName=dd3s.Name,
                           d.BaseQtyD,
                           d.ComponentInvId,
                           ComponentInvCode=dd4s.Code,
                           ComponentInvName=dd4s.Name,
                           ComponentSpecs = dd4s.Specs,
                           ComponentGroupName=dd5s.Name,
                           ComponentStockUnitName=dd6s.Name,
                           d.BaseQtyN
                       };
            if (gridModel.BillOfMaterialsGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.BillOfMaterialsGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.BillOfMaterialsGrid.ExportToExcel(invs, "配方.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.BillOfMaterialsGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.BillOfMaterialsGrid.DataBind(invs);
            }
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
        public ActionResult BillOfMaterials_EditData(DXInfo.Models.BillOfMaterials bom)
        {
            var gridModel = new BillOfMaterialsGridModel();
            SetupBillOfMaterialsGridModel(gridModel.BillOfMaterialsGrid);
            try
            {
                if (gridModel.BillOfMaterialsGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckBillOfMaterialsDup(bom))
                    {
                        return gridModel.BillOfMaterialsGrid.ShowEditValidationMessage("子件重复");
                    }
                    
                    Uow.BillOfMaterials.Add(bom);
                    Uow.Commit();
                }
                if (gridModel.BillOfMaterialsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldbom = Uow.BillOfMaterials.GetById(g => g.Id == bom.Id);
                    if ((oldbom.ComponentInvId != bom.ComponentInvId || oldbom.PartInvId!= bom.PartInvId )&& CheckBillOfMaterialsDup(bom))
                    {
                        return gridModel.BillOfMaterialsGrid.ShowEditValidationMessage("子件重复");
                    }

                    oldbom.PartInvId = bom.PartInvId;
                    oldbom.BaseQtyD = bom.BaseQtyD;
                    oldbom.ComponentInvId = bom.ComponentInvId;
                    oldbom.BaseQtyN = bom.BaseQtyN;

                    Uow.BillOfMaterials.Update(oldbom);
                    Uow.Commit();
                }
                if (gridModel.BillOfMaterialsGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var oldbom = Uow.BillOfMaterials.GetById(g => g.Id == bom.Id);
                    Uow.BillOfMaterials.Delete(oldbom);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.BillOfMaterialsGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("BillOfMaterials");
        }
        
        #endregion

        #region 评估
        private void SetupProduceEvaluationGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ProduceEvaluation_RequestData");
            //grid.EditUrl = Url.Action("BillOfMaterials_EditData");

            
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
            return View(gridModel);
        }
        public ActionResult ProduceEvaluation_RequestData(DateTime? BeginDate, DateTime? EndDate,Guid? DeptId)
        {
            var gridModel = new ProduceEvaluationGridModel();
            SetupProduceEvaluationGridModel(gridModel.ProduceEvaluationGrid);
            if (BeginDate.HasValue && EndDate.HasValue && DeptId.HasValue)
            {
                DateTime dtBeginDate = BeginDate.Value;
                DateTime dtEndDate = EndDate.Value;
                Guid deptId = DeptId.Value;

                //var eva1 = from d in Uow.RdRecord.GetAll()
                //             join d1 in Uow.RdRecords.GetAll() on d.Id equals d1.RdId into dd1
                //             from dd1s in dd1.DefaultIfEmpty()

                //             join d2 in Uow.Inventory.GetAll() on dd1s.InvId equals d2.Id into dd2
                //             from dd2s in dd2.DefaultIfEmpty()

                //             join d3 in Uow.InventoryCategory.GetAll() on dd2s.Category equals d3.Id into dd3
                //             from dd3s in dd3.DefaultIfEmpty()

                //             where d.RdDate >= dtBeginDate && d.RdDate <= dtEndDate && d.DeptId == deptId && dd3s.ProductType == (int)DXInfo.Models.ProductType.Material
                //             group dd1s by dd1s.InvId into g
                //             select new { InvId = g.Key, Num = g.Sum(s => s.Num) };

                //var eva2 = from d in Uow.RdRecord.GetAll()
                //           join d1 in Uow.RdRecords.GetAll() on d.Id equals d1.RdId into dd1
                //           from dd1s in dd1.DefaultIfEmpty()

                //           join d2 in Uow.Inventory.GetAll() on dd1s.InvId equals d2.Id into dd2
                //           from dd2s in dd2.DefaultIfEmpty()

                //           join d3 in Uow.InventoryCategory.GetAll() on dd2s.Category equals d3.Id into dd3
                //           from dd3s in dd3.DefaultIfEmpty()

                //           where d.RdDate >= dtBeginDate && d.RdDate <= dtEndDate && d.DeptId == deptId && dd3s.ProductType == (int)DXInfo.Models.ProductType.Product
                //           group dd1s by dd1s.InvId into g
                //           select new { InvId = g.Key, Num = g.Sum(s => s.Num) };
                var eva =
                    from a in
                        (
                            from d in Uow.BillOfMaterials.GetAll()

                            join d1 in Uow.RdRecords.GetAll() on d.PartInvId equals d1.InvId into dd1
                            from dd1s in dd1.DefaultIfEmpty()//.GroupBy(g => g.InvId).Select(g => new { InvId = g.Key, Num = g.Sum(s => s.Num) }).DefaultIfEmpty()

                            join d2 in Uow.RdRecord.GetAll() on dd1s.RdId equals d2.Id into dd2
                            from dd2s in dd2.DefaultIfEmpty()

                            join d3 in Uow.RdRecords.GetAll() on d.ComponentInvId equals d3.InvId into dd3
                            from dd3s in dd3.DefaultIfEmpty()//.GroupBy(g => g.InvId).Select(g => new { InvId = g.Key, Num = g.Sum(s => s.Num) }).DefaultIfEmpty()

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

                //var eva3 = from d in Uow.BillOfMaterials.GetAll()

                //          join d1 in eva1 on d.PartInvId equals d1.InvId into dd1
                //          from dd1s in dd1.DefaultIfEmpty()

                //          join d2 in eva2 on d.ComponentInvId equals d2.InvId into dd2
                //          from dd2s in dd2.DefaultIfEmpty()

                //          join d3 in Uow.Inventory.GetAll() on d.PartInvId equals d3.Id into dd3
                //          from dd3s in dd3.DefaultIfEmpty()

                //          join d4 in Uow.Inventory.GetAll() on d.ComponentInvId equals d4.Id into dd4
                //          from dd4s in dd4.DefaultIfEmpty()

                //          join d5 in Uow.UnitOfMeasures.GetAll() on dd3s.MainUnit equals d5.Id into dd5
                //          from dd5s in dd5.DefaultIfEmpty()

                //          join d6 in Uow.UnitOfMeasures.GetAll() on dd4s.MainUnit equals d6.Id into dd6
                //          from dd6s in dd6.DefaultIfEmpty()

                //          select new
                //          {
                //              d.PartInvId,
                //              PartInvCode = dd3s == null ? "" : dd3s.Code,
                //              PartInvName = dd3s == null ? "" : dd3s.Name,
                //              PartSpecs = dd3s == null ? "" : dd3s.Specs,
                //              PartStockUnitName = dd5s==null?"":dd5s.Name,
                //              d.ComponentInvId,
                //              ComponentInvCode = dd4s==null?"":dd4s.Code,
                //              ComponentInvName = dd4s == null ? "" : dd4s.Name,
                //              ComponentSpecs = dd4s == null ? "" : dd4s.Specs,
                //              ComponentStockUnitName = dd6s == null ? "" : dd6s.Name,                             
                //              Num1 = d.BaseQtyN/d.BaseQtyD,
                //              Num2 = 0//(dd1s!=null ? dd1s.Num:1)//(dd2s==null?0:dd2s.Num) / (dd1s == null  ? 1 : dd1s.Num)
                //};
                if (gridModel.ProduceEvaluationGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    JQGridState gridState = Session["JQGridState"] as JQGridState;
                    gridModel.ProduceEvaluationGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                    gridModel.ProduceEvaluationGrid.ExportToExcel(eva, "评估.xls", gridState);
                    return View();
                }
                else
                {
                    JQGridState gridState = gridModel.ProduceEvaluationGrid.GetState();
                    Session["JQGridState"] = gridState;
                    return gridModel.ProduceEvaluationGrid.DataBind(eva);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    PartInvId="",
                    PartInvCode = "",
                    PartInvName = "",
                    PartSpecs = "",
                    PartStockUnitName = ""
                              ,
                    ComponentInvId="",
                    ComponentInvCode = "",
                    ComponentInvName = "",
                    ComponentSpecs = "",
                    ComponentStockUnitName = ""
                              ,
                    Num1 = "",
                    Num2 = ""
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.ProduceEvaluationGrid.DataBind(list.AsQueryable());
            }
        }
        #endregion

        #region 库存部门配料仓
        private void SetupWarehouseDeptGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WarehouseDept_RequestData");
            grid.EditUrl = Url.Action("WarehouseDept_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "Dept", this.GetDept());
            SetDropDownColumn(grid, "Dept", centerCommon.GetWarehouse());            
        }
        [Authorize]
        public ActionResult WarehouseDept()
        {
            var gridModel = new WarehouseDeptGridModel();
            SetupWarehouseDeptGridModel(gridModel.WarehouseDeptGrid);
            return View(gridModel);
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

                        select new { d.Id, d.Dept, DeptName = dd1s.DeptName, d.Warehouse, WarehouseName=dd2s.Name };
            if (gridModel.WarehouseDeptGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.WarehouseDeptGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.WarehouseDeptGrid.ExportToExcel(units, "库存部门配料仓.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.WarehouseDeptGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.WarehouseDeptGrid.DataBind(units);
            }
        }
        private bool CheckWarehouseDeptIdDup(Guid dept,Guid warehouse)
        {
            return Uow.WarehouseDept.GetAll().Where(w => w.Dept == dept && w.Warehouse==warehouse).Count() > 0;
        }
        public ActionResult WarehouseDept_EditData(DXInfo.Models.WarehouseDept warehouseDept)
        {
            var gridModel = new WarehouseDeptGridModel();
            SetupWarehouseDeptGridModel(gridModel.WarehouseDeptGrid);
            try
            {
                if (gridModel.WarehouseDeptGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckWarehouseDeptIdDup(warehouseDept.Dept, warehouseDept.Warehouse))
                    {
                        return gridModel.WarehouseDeptGrid.ShowEditValidationMessage("部门配料仓重复");
                    }
                    Uow.WarehouseDept.Add(warehouseDept);
                    Uow.Commit();
                }
                if (gridModel.WarehouseDeptGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldWarehouseDept = Uow.WarehouseDept.GetById(g => g.Id == warehouseDept.Id);
                    if (oldWarehouseDept.Dept != warehouseDept.Dept && oldWarehouseDept.Warehouse != warehouseDept.Warehouse && CheckWarehouseDeptIdDup(warehouseDept.Dept, warehouseDept.Warehouse))
                    {
                        return gridModel.WarehouseDeptGrid.ShowEditValidationMessage("部门配料仓重复");
                    }
                    oldWarehouseDept.Dept = warehouseDept.Dept;
                    oldWarehouseDept.Warehouse = warehouseDept.Warehouse;
                    Uow.WarehouseDept.Update(oldWarehouseDept);
                    Uow.Commit();
                }
                if (gridModel.WarehouseDeptGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var oldWarehouseDept = Uow.WarehouseDept.GetById(g => g.Id == warehouseDept.Id);
                    Uow.WarehouseDept.Delete(oldWarehouseDept);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.WarehouseDeptGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("WarehouseDept");
        }
        #endregion

        #region 仓库存货关联
        private void SetupWarehouseInventoryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WarehouseInventory_RequestData");
            grid.EditUrl = Url.Action("WarehouseInventory_EditData");

            grid.ClientSideEvents.AfterEditDialogShown = "populate";
            grid.ClientSideEvents.AfterAddDialogShown = "populate";

            SetUpGrid(grid);
            SetDropDownColumn(grid, "Inventory", this.GetInventory());
            SetDropDownColumn(grid, "Warehouse", centerCommon.GetWarehouse());
            
        }
        [Authorize]
        public ActionResult WarehouseInventory()
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);
            return View(gridModel);
        }
        public ActionResult WarehouseInventory_RequestData()
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);

            var units = from d in Uow.WarehouseInventory.GetAll()
                        join d1 in Uow.Inventory.GetAll() on d.Inventory equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        join d2 in Uow.Warehouse.GetAll() on d.Warehouse equals d2.Id into dd2
                        from dd2s in dd2.DefaultIfEmpty()
                        orderby d.Id
                        select new { d.Id, d.Warehouse, WarehouseName = dd2s.Name,Inventory=dd1s.Id,InventoryName=dd1s.Name };
            
            if (gridModel.WarehouseInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.Excel)
            {
                JQGridState gridState = Session["JQGridState"] as JQGridState;
                gridModel.WarehouseInventoryGrid.ExportSettings.ExportDataRange = ExportDataRange.Filtered;
                gridModel.WarehouseInventoryGrid.ExportToExcel(units, "仓库存货关联.xls", gridState);
                return View();
            }
            else
            {
                JQGridState gridState = gridModel.WarehouseInventoryGrid.GetState();
                Session["JQGridState"] = gridState;
                return gridModel.WarehouseInventoryGrid.DataBind(units);
            }
        }
        private bool CheckWarehouseInventoryIdDup(Guid warehouse,Guid inventory)
        {
            return Uow.WarehouseInventory.GetAll().Where(w => w.Inventory == inventory && w.Warehouse == warehouse).Count() > 0;
        }
        public ActionResult WarehouseInventory_EditData(DXInfo.Models.WarehouseInventory warehouseInventory)
        {
            var gridModel = new WarehouseInventoryGridModel();
            SetupWarehouseInventoryGridModel(gridModel.WarehouseInventoryGrid);
            try
            {
                if (gridModel.WarehouseInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.AddRow)
                {
                    if (CheckWarehouseInventoryIdDup(warehouseInventory.Warehouse, warehouseInventory.Inventory))
                    {
                        return gridModel.WarehouseInventoryGrid.ShowEditValidationMessage("仓库存货关联重复");
                    }
                    Uow.WarehouseInventory.Add(warehouseInventory);
                    Uow.Commit();
                }
                if (gridModel.WarehouseInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
                {
                    var oldWarehouseInventory = Uow.WarehouseInventory.GetById(g => g.Id == warehouseInventory.Id);
                    if (oldWarehouseInventory.Inventory != warehouseInventory.Inventory
                        && oldWarehouseInventory.Warehouse != warehouseInventory.Warehouse
                        && CheckWarehouseInventoryIdDup(warehouseInventory.Warehouse, warehouseInventory.Inventory))
                    {
                        return gridModel.WarehouseInventoryGrid.ShowEditValidationMessage("部门配料仓重复");
                    }
                    oldWarehouseInventory.Inventory = warehouseInventory.Inventory;
                    oldWarehouseInventory.Warehouse = warehouseInventory.Warehouse;
                    Uow.WarehouseInventory.Update(oldWarehouseInventory);
                    Uow.Commit();
                }
                if (gridModel.WarehouseInventoryGrid.AjaxCallBackMode == AjaxCallBackMode.DeleteRow)
                {
                    var oldWarehouseInventory = Uow.WarehouseInventory.GetById(g => g.Id == warehouseInventory.Id);
                    Uow.WarehouseInventory.Delete(oldWarehouseInventory);
                    Uow.Commit();
                }
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                return gridModel.WarehouseInventoryGrid.ShowEditValidationMessage(dex.Message);
            }
            return RedirectToAction("WarehouseInventory");
        }
        #endregion

        #region 零售出库单
        public ActionResult RetailOutStock()
        {
            var gridModel = new RdRecordModel();
            gridModel.vouchType = Uow.VouchType.GetById(g => g.Code == DXInfo.Models.VouchTypeCode.RetailOutStock);
            gridModel.IsBatch = isBatch;
            gridModel.IsShelfLife = isShelfLife;
            gridModel.IsLocator = isLocator;
            gridModel.rdRecord.BusType = DXInfo.Models.VouchTypeCode.SaleOutStock;
            gridModel.rdRecord.STCode = "002";//002	零售销售	002	0
            gridModel.rdRecord.IsModify = false;
            if (this.Request["Id"] != null)
            {
                Guid Id = Guid.Parse(this.Request["Id"]);
                var rdRecords = Uow.RdRecords.GetById(g => g.Id == Id);
                if (rdRecords == null)
                {
                    gridModel.rdRecord.Id = Id;
                }
                else
                {
                    gridModel.rdRecord.Id = rdRecords.RdId;
                }
            }
            SetupRdRecordsGridModel(gridModel.rdRecordsGridModel.RdRecordsGrid, gridModel.vouchType.Code);
            return View(gridModel);
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
