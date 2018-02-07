using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXInfo.Data.Contracts;
using DXInfo.Models;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Routing;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;

namespace ynhnTransportManage
{
    public class EntityJQGrid:JQGrid
    {
        public EntityJQGrid()
        {
            this.ToolBarSettings.ToolBarPosition = ToolBarPosition.TopAndBottom;
            this.AppearanceSettings.AlternateRowBackground = true;
            this.AppearanceSettings.HighlightRowsOnHover = true;
            this.AppearanceSettings.ShowRowNumbers = true;            
            this.ToolBarSettings.ShowAddButton = true;
            this.ToolBarSettings.ShowEditButton = true;
            this.ToolBarSettings.ShowViewRowDetailsButton = true;
            this.ToolBarSettings.ShowDeleteButton = true;
            this.ToolBarSettings.ShowSearchButton = true;
            this.ToolBarSettings.ShowRefreshButton = true;
            this.ToolBarSettings.ShowExcelButton = true;
            this.ToolBarSettings.ShowColumnChooser = true;
            this.SearchDialogSettings.MultipleSearch = true;
            this.SearchDialogSettings.Resizable = true;
            this.AutoWidth = true;
            this.Height = Unit.Percentage(100);
            this.ClientSideEvents.RowDoubleClick = "RowDoubleClick";
            //this.ClientSideEvents.GridInitialized = "GridInitialized";            
        }
    }
    public class Common
    {
        private IFairiesMemberManageUow uow;
        private DXInfo.Business.Common common;
        public Common(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            this.common = new DXInfo.Business.Common(uow);
        }

        public List<SelectListItem> GetEnumTypeDescription(string enumType)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" } };
            List<DXInfo.Models.EnumTypeDescription> lEnumTypeDescription = common.GetlEnumTypeDescription(enumType);
            lEnumTypeDescription.ForEach(delegate(DXInfo.Models.EnumTypeDescription enumtd){ listItems.Add(new SelectListItem(){ Text=enumtd.Description,Value=enumtd.Value.ToString()});});
            return listItems;
        }

        #region Enum - SelectListItem
        private List<SelectListItem> GetMyEnumSelectListItem(Type type)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.MyEnum> lMyEnum = DXInfo.Business.Helper.GetlMyEnum(type);
            lMyEnum.ForEach(delegate(DXInfo.Models.MyEnum myEnum) { listItems.Add(new SelectListItem() { Text = myEnum.Name, Value = myEnum.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetCardStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.CardStatus));
        }
        public List<SelectListItem> GetRechargeType()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.RechargeType));
        }
        public List<SelectListItem> GetSectionType()
        {

            return GetMyEnumSelectListItem(typeof(DXInfo.Models.SectionType));
        }
        public List<SelectListItem> GetDeptType()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.DeptType));
        }
        public List<SelectListItem> GetUnitGroupCategories()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.UnitGroupCategory));
        }
        public List<SelectListItem> GetConsumeType()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.ConsumeType));
        }
        public List<SelectListItem> GetCupType()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.CupType));
        }
        public List<SelectListItem> GetOrderDishStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.OrderDishStatus));
        }
        public List<SelectListItem> GetOrderMenuStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.OrderMenuStatus));
        }
        public List<SelectListItem> GetRoomStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.RoomStatus));
        }
        public List<SelectListItem> GetDeskStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.DeskStatus));
        }
        public List<SelectListItem> GetIPadStatus()
        {
            return GetMyEnumSelectListItem(typeof(DXInfo.Models.IPadStatus));
        }
        public List<SelectListItem> GetBoolDesc()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem { Value = "", Text = "" });
            listItems.Add(new SelectListItem { Value = "true", Text = "是" });
            listItems.Add(new SelectListItem { Value = "false", Text = "否" });
            return listItems;
        }
        public List<SelectListItem> GetStar()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem() { Text = "", Value = "" });
            listItems.Add(new SelectListItem() { Text = "1", Value = "1" });
            listItems.Add(new SelectListItem() { Text = "2", Value = "2" });
            listItems.Add(new SelectListItem() { Text = "3", Value = "3" });
            listItems.Add(new SelectListItem() { Text = "4", Value = "4" });
            listItems.Add(new SelectListItem() { Text = "5", Value = "5" });
            return listItems;
        }
        public List<SelectListItem> GetNameCodeType(DXInfo.Models.NameCodeType nct)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem { Text = "", Value = "" } };
            List<DXInfo.Models.NameCode> lNameCode = common.GetlNameCode(nct);
            lNameCode.ForEach(delegate(DXInfo.Models.NameCode nc) { listItems.Add(new SelectListItem() { Text = nc.Name, Value = nc.Code }); });
            return listItems;
        }
        public List<SelectListItem> GetNameCodeType()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.MyEnum> lMyEnum = DXInfo.Business.Helper.GetlMyEnum(typeof(DXInfo.Models.NameCodeType));
            lMyEnum.ForEach(delegate(DXInfo.Models.MyEnum myEnum) { listItems.Add(new SelectListItem() { Text = myEnum.Name, Value = myEnum.Code }); });
            return listItems;
        }
        public List<SelectListItem> GetProductType()
        {
            return GetEnumTypeDescription("ProductType");
        }
        public List<SelectListItem> GetShelfLifeType()
        {
            return GetEnumTypeDescription("ShelfLifeType");
        }
        public List<SelectListItem> GetAuthorityType()
        {
            return GetEnumTypeDescription("AuthorityType");
        }
        public List<SelectListItem> GetCheckCycle()
        {
            return GetEnumTypeDescription("CheckCycle");
        }
        #endregion

        #region 日期
        public DateTime GetCurrentMonthFirstDay()
        {
            DateTime dtNow = DateTime.Now.Date;
            return dtNow.AddDays(-dtNow.Day + 1);
        }
        public DateTime GetCurrentMonthLastDay()
        {
            DateTime dtNow = DateTime.Now.Date;
            DateTime dtNext = dtNow.AddMonths(1);
            return dtNext.AddDays(-dtNext.Day);
        }
        //public List<SelectListItem> GetMonths()
        //{
        //    List<SelectListItem> listItems = new List<SelectListItem>();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        SelectListItem listItem = new SelectListItem();
        //        listItem.Text = i.ToString() + "月";
        //        listItem.Value = i.ToString();
        //        listItems.Add(listItem);
        //    }
        //    listItems.Insert(0, new SelectListItem() { Text = "", Value = "" });
        //    return listItems;
        //}
        //public List<SelectListItem> GetYears()
        //{
        //    List<SelectListItem> listItems = new List<SelectListItem>();

        //    for (int i = 2011; i < DateTime.Now.Year + 1; i++)
        //    {
        //        SelectListItem listItem = new SelectListItem();
        //        listItem.Text = i.ToString();
        //        listItem.Value = i.ToString();
        //        listItems.Add(listItem);
        //    }
        //    return listItems;
        //}
        public List<SelectListItem> GetMonths()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Text = i.ToString() + "月";
                listItem.Value = i.ToString();
                listItems.Add(listItem);
            }
            listItems.Insert(0, new SelectListItem() { Text = "", Value = "" });
            return listItems;
        }
        public List<SelectListItem> GetYears()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            for (int i = 2011; i < DateTime.Now.Year + 2; i++)
            {
                SelectListItem listItem = new SelectListItem();
                listItem.Text = i.ToString();
                listItem.Value = i.ToString();
                listItems.Add(listItem);
            }
            return listItems;
        }
        #endregion

        #region other
        public IEnumerable<SelectListItem> GetVehicle()
        {
            var vehicles = uow.Vehicles.GetAll().ToList();
            var listItems =
                from v in vehicles
                select
                    new SelectListItem()
                    {
                        Text = v.PlateNo,
                        Value = v.Id.ToString()
                    };
            return listItems;
        }
        public IEnumerable<SelectListItem> GetLines()
        {
            var users = uow.Lines.GetAll().ToList();
            var listItems =
                (from v in users
                 select
                     new SelectListItem()
                     {
                         Text = v.Name,
                         Value = v.Id.ToString()
                     }).ToList<SelectListItem>();
            listItems.Insert(0, new SelectListItem() { Text = "", Value = "" });
            return listItems;
        }
        public IEnumerable<SelectListItem> GetDrivers()
        {
            var users = uow.Drivers.GetAll().ToList();
            var listItems =
                from v in users
                select
                    new SelectListItem()
                    {
                        Text = v.Name,
                        Value = v.Id.ToString()
                    };
            return listItems;
        }
        /// <summary>
        /// 有问题。。
        /// </summary>
        /// <param name="uow"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetBalanceType()
        {
            //List<DXInfo.Models.NameCode> lNameCode = DXInfo.Business.Common.GetlNameCode(uow,DXInfo.Models.NameCodeType.BalanceType);
            //List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            //lNameCode.ForEach(delegate(DXInfo.Models.NameCode nc) { listItems.Add(new SelectListItem(){ Text=nc.Name,Value=nc.Code })});
            //return listItems;
            return null;
        }
        #endregion

        #region  菜单
        public IEnumerable<string> GetAllSitemapKeys(HttpContextBase contextBase)
        {
            var context = contextBase.ApplicationInstance.Context;
            return GetAllSitemapKeys2(context);
        }
        public IEnumerable<string> GetAllSitemapKeys2(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                var ruleforrole = (from a in uow.aspnet_AuthorizationRules.GetAll()
                                   join b in uow.aspnet_UsersInRoles.GetAll().Where(w => w.UserId == userId) on a.RoleId equals b.RoleId
                                   select a.SiteMapKey).ToList();
                var ruleforuser = (from a in uow.aspnet_AuthorizationRules.GetAll().Where(w => w.UserId == userId)
                                   select a.SiteMapKey).ToList();
                return ruleforrole.Concat(ruleforuser);
            }
            return null;
        }
        #endregion

        public List<SelectListItem> GetDept(int? deptType)
        {
            List<DXInfo.Models.Depts> lDept = common.GetlDept(deptType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lDept.ForEach(delegate(DXInfo.Models.Depts dept) { listItems.Add(new SelectListItem() { Text = dept.DeptName, Value = dept.DeptId.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetDept(HttpRequestBase request)
        {
            string strDeptType = request["DeptType"];
            int? deptType = null;
            if (!string.IsNullOrEmpty(strDeptType))
            {
                deptType = Convert.ToInt32(strDeptType);
            }
            return GetDept(deptType);
        }
        public List<SelectListItem> GetDept(System.Web.Routing.RequestContext rc)
        {
            return GetDept(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetOper(int? deptType)
        {
            List<DXInfo.Models.aspnet_CustomProfile> lOper = common.GetlOper(deptType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lOper.ForEach(delegate(DXInfo.Models.aspnet_CustomProfile oper) { listItems.Add(new SelectListItem() { Text = oper.FullName, Value = oper.UserId.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetOper(HttpRequestBase request)
        {
            string strDeptType = request["DeptType"];
            int? deptType = null;
            if (!string.IsNullOrEmpty(strDeptType))
            {
                deptType = Convert.ToInt32(strDeptType);
            }
            return GetOper(deptType);
        }
        public List<SelectListItem> GetOper(System.Web.Routing.RequestContext rc)
        {            
            return GetOper(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetPayType()
        {            
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.PayTypes> lPayType = common.GetlPayType();
            lPayType.ForEach(delegate(DXInfo.Models.PayTypes payType) { listItems.Add(new SelectListItem() { Text = payType.Name, Value =payType.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetCardType()
        {
            List<DXInfo.Models.CardTypes> lCardType = common.GetlCardType();
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lCardType.ForEach(delegate(DXInfo.Models.CardTypes cardType) { listItems.Add(new SelectListItem(){ Text=cardType.Name,Value=cardType.Id.ToString()}); });
            return listItems;
        }
        public List<SelectListItem> GetCardLevel()
        {
            List<DXInfo.Models.CardLevels> lCardLevel = common.GetlCardLevel();
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lCardLevel.ForEach(delegate(DXInfo.Models.CardLevels cardLevel) { listItems.Add(new SelectListItem() { Text = cardLevel.Name, Value = cardLevel.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetInventory(int? invType)
        {
            List<DXInfo.Models.Inventory> lInventory = common.GetlInventory(invType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lInventory.ForEach(delegate(DXInfo.Models.Inventory inv) { listItems.Add(new SelectListItem() { Text = inv.Name, Value = inv.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetInventoryExceptPackage(int? invType)
        {
            List<DXInfo.Models.Inventory> lInventory = common.GetlInventoryExceptPackage(invType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lInventory.ForEach(delegate(DXInfo.Models.Inventory inv) { listItems.Add(new SelectListItem() { Text = inv.Name, Value = inv.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetInventory(HttpRequestBase request)
        {
            string strInvType = request["InvType"];
            int? invType = null;
            if (!string.IsNullOrEmpty(strInvType))
            {
                invType = Convert.ToInt32(strInvType);
            }
            return GetInventory(invType);
        }
        public List<SelectListItem> GetInventory(System.Web.Routing.RequestContext rc)
        {
            return GetInventory(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetInventoryExceptPackage(HttpRequestBase request)
        {
            string strInvType = request["InvType"];
            int? invType = null;
            if (!string.IsNullOrEmpty(strInvType))
            {
                invType = Convert.ToInt32(strInvType);
            }
            return GetInventoryExceptPackage(invType);
        }
        public List<SelectListItem> GetInventoryExceptPackage(System.Web.Routing.RequestContext rc)
        {
            return GetInventoryExceptPackage(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetCategory(int? categoryType)
        {            
            List<DXInfo.Models.InventoryCategory> lInventoryCategory = common.GetlCategory(categoryType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lInventoryCategory.ForEach(delegate(DXInfo.Models.InventoryCategory category) { listItems.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetCategory(HttpRequestBase request)
        {
            string strCategoryType = request["CategoryType"];
            int? categoryType = null;
            if (!string.IsNullOrEmpty(strCategoryType))
            {
                categoryType = Convert.ToInt32(strCategoryType);
            }
            List<DXInfo.Models.InventoryCategory> lInventoryCategory = common.GetlCategory(categoryType);
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lInventoryCategory.ForEach(delegate(DXInfo.Models.InventoryCategory category) { listItems.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetCategory(System.Web.Routing.RequestContext rc)
        {
            return GetCategory(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetWarehouseDept()
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());            
            List<DXInfo.Models.Warehouse> lWarehouse = new List<Warehouse>();
            List<SelectListItem> lsi = new List<SelectListItem>(){new SelectListItem() { Text = "", Value = "" }};
            DXInfo.Models.aspnet_CustomProfile userOfDb = uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == userId).FirstOrDefault();
            if (userOfDb != null || userOfDb.DeptId.HasValue)
            {
                lWarehouse = common.GetlWarehouseDept(userOfDb.DeptId.Value);
            }
            lWarehouse.ForEach(delegate(DXInfo.Models.Warehouse wh) { lsi.Add(new SelectListItem() { Text = wh.Name, Value = wh.Id.ToString() }); });
            return lsi;
        }
        public List<SelectListItem> GetWarehouse()
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            var vouchAuthority = uow.VouchAuthority.GetById(g => g.UserId == userId);
            int authorityType = 2;
            if (vouchAuthority != null)
            {
                authorityType = vouchAuthority.AuthorityType;
            }
            List<DXInfo.Models.Warehouse> lWarehouse = new List<Warehouse>();
            List<SelectListItem> lsi = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            switch (authorityType)
            {
                case (int)DXInfo.Models.AuthorityType.All:
                    lWarehouse = common.GetlWarehouse(null);
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                case (int)DXInfo.Models.AuthorityType.Self:
                    DXInfo.Models.aspnet_CustomProfile userOfDb = uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == userId).FirstOrDefault();
                    if (userOfDb != null && userOfDb.DeptId.HasValue)
                    {
                        lWarehouse = common.GetlWarehouse(userOfDb.DeptId);
                    }
                    break;
            }
            lWarehouse.ForEach(delegate(DXInfo.Models.Warehouse wh) { lsi.Add(new SelectListItem() { Text = wh.Name, Value = wh.Id.ToString() }); });
            return lsi;
        }
        public List<SelectListItem> GetLocator()
        {
            List<DXInfo.Models.Locator> lLocator = common.GetlLocator();
            List<SelectListItem> lsi = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            lLocator.ForEach(delegate(DXInfo.Models.Locator locator) { lsi.Add(new SelectListItem() { Text = locator.Name, Value = locator.Id.ToString() }); });
            return lsi;
        }
        public List<SelectListItem> GetVendor(int? vendorType)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.Vendor> lVendor = common.GetlVendor(vendorType);
            lVendor.ForEach(delegate(DXInfo.Models.Vendor vendor) { listItems.Add(new SelectListItem() { Text = vendor.Name, Value = vendor.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetVendor(HttpRequestBase request)
        {
            string strVendorType = request["VendorType"];
            int? vendorType = null;
            if (!string.IsNullOrEmpty(strVendorType))
            {
                vendorType = Convert.ToInt32(strVendorType);
            }
            return GetVendor(vendorType);
        }
        public List<SelectListItem> GetVendor(System.Web.Routing.RequestContext rc)
        {
            return GetVendor(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetBusType(string vouchType)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.BusType> lBusType = common.GetlBusType(vouchType);
            lBusType.ForEach(delegate(DXInfo.Models.BusType busType) { listItems.Add(new SelectListItem() { Text = busType.Name, Value = busType.Code }); });
            return listItems;
        }
        public List<SelectListItem> GetPTType()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.PTType> lPTType = common.GetlPTType();
            lPTType.ForEach(delegate(DXInfo.Models.PTType pTType) { listItems.Add(new SelectListItem() { Text = pTType.Name, Value = pTType.Code }); });
            return listItems;
        }
        public List<SelectListItem> GetRdType()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.RdType> lRdType = common.GetlRdType();
            lRdType.ForEach(delegate(DXInfo.Models.RdType rdType) { listItems.Add(new SelectListItem() { Text = rdType.Name, Value = rdType.Code }); });
            return listItems;
        }
        public List<SelectListItem> GetOrganization()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.Organizations> lOrganization = common.GetlOrganization();
            lOrganization.ForEach(delegate(DXInfo.Models.Organizations organization) { listItems.Add(new SelectListItem() { Text = organization.Name, Value = organization.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetMeasurementUnitGroup()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.MeasurementUnitGroup> lMeasurementUnitGroup = common.GetlMeasurementUnitGroup();
            lMeasurementUnitGroup.ForEach(delegate(DXInfo.Models.MeasurementUnitGroup mug) { listItems.Add(new SelectListItem() { Text = mug.Name, Value = mug.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetUnitOfMeasure(int? uomType)
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.UnitOfMeasures> lUnitOfMeasure = common.GetlUnitOfMeasure(uomType);
            lUnitOfMeasure.ForEach(delegate(DXInfo.Models.UnitOfMeasures uom) { listItems.Add(new SelectListItem() { Text = uom.Name, Value = uom.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetUnitOfMeasure(HttpRequestBase request)
        {
            string strUOMType = request["UOMType"];
            int? uomType = null;
            if (!string.IsNullOrEmpty(strUOMType))
            {
                uomType = Convert.ToInt32(strUOMType);
            }
            return GetUnitOfMeasure(uomType);
        }
        public List<SelectListItem> GetUnitOfMeasure(System.Web.Routing.RequestContext rc)
        {
            return GetUnitOfMeasure(rc.HttpContext.Request);
        }
        public List<SelectListItem> GetPeriod()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.Period> lPeriod = common.GetlPeriod();
            lPeriod.ForEach(delegate(DXInfo.Models.Period period) { listItems.Add(new SelectListItem() { Text = period.Code, Value = period.Id.ToString() }); });
            return listItems;
        }
        public List<SelectListItem> GetRoom()
        {
            List<SelectListItem> listItems = new List<SelectListItem>() { new SelectListItem() { Text = "", Value = "" } };
            List<DXInfo.Models.Rooms> lRoom = common.GetlRoom();
            lRoom.ForEach(delegate(DXInfo.Models.Rooms room) { listItems.Add(new SelectListItem() { Text = room.Name, Value = room.Id.ToString() }); });
            return listItems;
        }
        #region 同步AMSApp
        public void SyncGT(IAMSCMUow amscmUow)
        {
            var ltbCommCode = amscmUow.tbCommCode.GetAll().Where(w => w.vcCommSign == "GT").ToList();
            foreach (DXInfo.Models.tbCommCode commCode in ltbCommCode)
            {
                var oldInvCat = uow.InventoryCategory.GetAll().Where(w => w.Code == commCode.vcCommCode).FirstOrDefault();
                if (oldInvCat == null)
                {
                    DXInfo.Models.InventoryCategory newInvCat = new DXInfo.Models.InventoryCategory();
                    newInvCat.Code = commCode.vcCommCode;
                    newInvCat.Name = commCode.vcCommName;
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                    {
                        newInvCat.ProductType = (int)DXInfo.Models.ProductType.Product;
                    }
                    newInvCat.CategoryType = (int)DXInfo.Models.CategoryType.StockManage;
                    uow.InventoryCategory.Add(newInvCat);
                }
            }
            uow.Commit();
        }
        public void SyncOper(IAMSCMUow amscmUow)
        {
            ynhnTransportManage.Models.AccountMembershipService MembershipService = new ynhnTransportManage.Models.AccountMembershipService();

            var ltbLogin = amscmUow.tbLogin.GetAll().ToList();
            foreach (DXInfo.Models.tbLogin tbLogin in ltbLogin)
            {
                DXInfo.Models.aspnet_Users user = uow.aspnet_Users.GetAll().Where(w => w.UserName == tbLogin.vcLoginID).FirstOrDefault();
                DXInfo.Models.Depts dept = uow.Depts.GetAll().Where(w => w.DeptCode == tbLogin.vcDeptID).FirstOrDefault();
                if (user == null)
                {

                    if (dept != null)
                    {
                        MembershipCreateStatus createStatus = MembershipService.CreateUser(tbLogin.vcLoginID, tbLogin.vcPwd, tbLogin.vcOperName, dept.DeptId);
                    }
                }
                else
                {
                    DXInfo.Models.aspnet_Membership memberShip = uow.aspnet_Membership.GetAll().Where(w => w.UserId == user.UserId).FirstOrDefault();
                    if (memberShip == null)
                    {
                        if (dept != null)
                        {
                            MembershipCreateStatus createStatus = MembershipService.CreateUser(tbLogin.vcLoginID, tbLogin.vcPwd, tbLogin.vcOperName, dept.DeptId);
                        }
                    }
                }
            }
        }
        public void UpdateOper(string userName, string fullName, string deptCode)
        {
            ynhnTransportManage.Models.AccountMembershipService MembershipService = new ynhnTransportManage.Models.AccountMembershipService();

            DXInfo.Models.Depts dept = uow.Depts.GetAll().Where(w => w.DeptCode == deptCode).FirstOrDefault();
            if (dept != null)
            {
                MembershipService.UpdateUser(userName, fullName, dept.DeptId);
            }
        }
        public void DeleteOper(string userName)
        {
            ynhnTransportManage.Models.AccountMembershipService MembershipService = new ynhnTransportManage.Models.AccountMembershipService();
            Guid userId = Guid.Empty;

            var user = uow.aspnet_Users.GetAll().Where(w => w.UserName == userName).FirstOrDefault();
            userId = user.UserId;
            var cus = uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == userId);
            if (cus.Count() > 0)
            {
                foreach (DXInfo.Models.aspnet_CustomProfile oldcus in cus)
                {
                    uow.aspnet_CustomProfile.Delete(oldcus);
                }
                uow.Commit();
            }
            MembershipService.DeleteUser(userId);
        }
        public void UpdatePwd(string userName, string newPwd)
        {
            ynhnTransportManage.Models.AccountMembershipService MembershipService = new ynhnTransportManage.Models.AccountMembershipService();
            MembershipService.ChangePassword(userName, newPwd);
        }
        public bool IsAMSApp()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
            {
                string AMSApp = System.Configuration.ConfigurationManager.AppSettings["AMSApp"].ToLower();
                return AMSApp == "true";
            }
            return false;
        }
        public bool IsAMSAppRefresh()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSAppRefresh"))
            {
                string AMSApp = System.Configuration.ConfigurationManager.AppSettings["AMSAppRefresh"].ToLower();
                return AMSApp == "true";
            }
            return false;
        }
        public bool IsProductTypeVisible()
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
            {
                return true;
            }
            return false;
        }
        #endregion

        

        public string GetDataCenterTitle()
        {
            string title = "";
            var nameCode = common.GetNameCode(DXInfo.Models.NameCodeType.DataCenterTitle);
            if (nameCode != null)
            {
                title = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                {

                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Title"))
                    {
                        title = System.Configuration.ConfigurationManager.AppSettings["Title"];
                    }
                    else
                    {
                        title = "面包派对网络中心";
                    }
                }
                else
                {
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Title"))
                    {
                        title = System.Configuration.ConfigurationManager.AppSettings["Title"];
                    }
                    else
                    {
                        title = "寻仙里约数据中心";
                    }
                }
            }
            return title;
        }

        public string LogonCss()
        {
            string logonCss = "logon.css";
            string type = DXInfo.Models.NameCodeType.LogonCss.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                logonCss = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                {
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LogonCss"))
                    {
                        logonCss = System.Configuration.ConfigurationManager.AppSettings["LogonCss"];
                    }
                    else
                    {
                        logonCss = "logon2.css";
                    }
                }
                else
                {
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LogonCss"))
                    {
                        logonCss = System.Configuration.ConfigurationManager.AppSettings["LogonCss"];
                    }
                    else
                    {
                        logonCss = "logon.css";
                    }
                }
            }
            return logonCss;
        }

        

        #region 同步表结构
        public static void SyncStruct(SqlConnection conn)
        {
            //if (JudgeSqlUpdate())
            //{
                DXInfo.Models.SyncTableStruct.Update(conn);
                DXInfo.Models.SyncTableStruct.UpdateServer(conn);
            //    UpdateSqlVersionConfig();
            //}
        }        
        public static bool JudgeSqlUpdate()
        {
            bool update = true;
            if (WebConfigurationManager.AppSettings.AllKeys.Contains("SqlVersion"))
            {
                string sqlversion = WebConfigurationManager.AppSettings["SqlVersion"];
                if (sqlversion == DXInfo.Business.Helper.SqlVersion)
                {
                    update = false;
                }
            }
            return update;
        }
        public static void UpdateSqlVersionConfig()
        {
            System.Configuration.Configuration config =
              WebConfigurationManager.OpenWebConfiguration("~");
            if (config.AppSettings.Settings.AllKeys.Contains("SqlVersion"))
            {
                config.AppSettings.Settings["SqlVersion"].Value = DXInfo.Business.Helper.SqlVersion;
            }
            else
            {
                config.AppSettings.Settings.Add("SqlVersion", DXInfo.Business.Helper.SqlVersion);
            }
            config.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        public static string GetTitle(System.Web.Routing.RequestContext rc)
        {
            string urlStr = rc.HttpContext.Request.Url.PathAndQuery;
            string title = "";
            
            SiteMapNodeCollection smnList = SiteMap.Provider.RootNode.ChildNodes;
            List<SiteMapNode> lSiteMapNode = new List<SiteMapNode>();
            foreach (SiteMapNode item in SiteMap.Provider.RootNode.ChildNodes)
            {
                lSiteMapNode.Add(item);
                if (item.Url == urlStr)
                {
                    title = item.Title;
                    break;
                }
                else
                {
                    foreach (SiteMapNode item2 in item.ChildNodes)
                    {
                        lSiteMapNode.Add(item2);
                        if (item2.Url == urlStr)
                        {
                            title = item2.Title;
                            break;
                        }
                        else
                        {
                            foreach (SiteMapNode item3 in item2.ChildNodes)
                            {
                                lSiteMapNode.Add(item3);
                                if (item3.Url == urlStr)
                                {
                                    title = item3.Title;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(title))
            {
                SiteMapNode smn = lSiteMapNode.Where(w => w.Url == urlStr).FirstOrDefault();
                if (smn == null)
                {
                    string query = rc.HttpContext.Request.Url.Query;
                    string urlStr2 = urlStr;
                    if (!string.IsNullOrEmpty(query))
                    {
                        urlStr2 = urlStr.Replace(query, "");
                    }
                    smn = lSiteMapNode.Where(w => w.Url == urlStr2).FirstOrDefault();
                    if (smn == null)
                    {
                        string urlStr3 = urlStr2.Replace("_RequestData","");
                        smn = lSiteMapNode.Where(w => w.Url == urlStr3).FirstOrDefault();
                    }
                }
                if (smn != null)
                {
                    title = smn.Title;
                }
            }
            return title;
        }

        //public static bool IsReceiver()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    return DXInfo.Business.Common.IsReceiver(uow);
        //}
    }
}