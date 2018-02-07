using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ynhnTransportManage.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using Trirand.Web.Mvc;

namespace ynhnTransportManage.Controllers
{
    public class TransportController : Controller
    {
        //
        // GET: /Transport/

        private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private string GetStatus(int istatus)
        {
            string strstatus = "";
            switch (istatus)
            {
                case 0:
                    strstatus = "此卡正常在用";
                    break;
                case 1:
                    strstatus = "此卡已挂失";
                    break;
                case 2:
                    strstatus = "此卡已补卡";
                    break;
                default:
                    strstatus = "此卡状态未知";
                    break;
            }
            return strstatus;
        }
        public ActionResult Index()
        {
            return View();
        }
        #region 进厂
        [Authorize]
        [HttpGet]
        public ActionResult InFactory(string CardNo)
        {
            
            if (!string.IsNullOrEmpty(CardNo))
            {
                JsonResult json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                var card = db.Cards.Where(w => w.CardNo==CardNo).FirstOrDefault();
                if (card == null)
                {
                    json.Data = new { Error = "此卡不存在！" };
                    return json;
                }
                if (card.Status != 0)
                {
                    json.Data = new { Error = GetStatus(card.Status) };
                    return json;
                }
                var vehicle = db.Vehicles.Where(w => w.Id == card.Vehicle).FirstOrDefault();
                if (vehicle == null)
                {
                    json.Data = new { Error = "无此卡对应的车辆信息" };
                    return json;
                }
                var oldtp = db.Transports.Where(w => w.Card == card.Id && w.Status!=3).ToList();
                if (oldtp.Count > 0)
                {
                    json.Data = new { Error = "此车在运输途中！" };
                    return json;
                }
                if (card == null)
                {
                    json.Data = new { Error = "无此卡号！" };
                    return json;
                }
                json.Data = new { Error = "", Card = card.Id, card.CardNo, card.Vehicle, vehicle.PlateNo, vehicle.BrandModel, vehicle.MotorNo, vehicle.OwnerName };
                return json;
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetMileage(Guid? MileageId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (MileageId.HasValue)
            {
                var l = db.Lines.Where(w => w.Id == MileageId).FirstOrDefault();
                if (l != null)
                {
                    
                    json.Data = new { l.Mileage };
                    
                }
            }
            else
            {
                json.Data = new { Mileage = "" };
            }
            return json;
        }
        [Authorize]
        [HttpPost]        
        public ActionResult InFactory(InFactoryModels model)
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            using (var context = db)
            {
                var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                if (dept == null || !dept.DeptId.HasValue || dept.DeptId==Guid.Empty)
                {
                    ModelState.AddModelError("", "请设置操作员部门信息");
                    return View(model);
                }
                var card = context.Cards.Where(w=>w.Id==model.Card).FirstOrDefault();
                if (card == null)
                {
                    ModelState.AddModelError("", "请读取卡号");
                    return View(model);
                }
                if (card.Status != 0)
                {
                    ModelState.AddModelError("", GetStatus(card.Status));
                    return View(model);
                }
                var oldtp = context.Transports.Where(w => w.Card == model.Card && w.Status!=3).ToList();
                if (oldtp.Count > 0)
                {
                    ModelState.AddModelError("", "此车在运输途中！");
                    return View(model);
                }
                DXInfo.Models.Transport tp = new DXInfo.Models.Transport();
                //tp.Id = Guid.NewGuid();
                tp.Card = model.Card;
                tp.InFactory_Date = DateTime.Now;
                tp.InFactory_DeptId = dept.DeptId.Value;
                tp.InFactory_UserId = userId;
                tp.Comment = model.Comment;

                tp.BalanceType = model.BalanceType;
                tp.AgreeFreightRate = model.AgreeFreightPrice;
                tp.FreightRate = model.FreightPrice;
                tp.Shipper = model.Shipper;
                tp.Shipper_Telephone = model.Shipper_Telephone;
                tp.Carrier = model.Carrier;
                tp.Carrier_Telephone = model.Carrier_Telephone;
                tp.Lines = model.Lines;
                tp.Driver = model.Driver;
                tp.Status = 0;

                context.Transports.Add(tp);
                context.SaveChanges();
            }
            return View("InFactorySucess");
        }
        #endregion
        #region 装车
        [Authorize]
        [HttpGet]
        public ActionResult Load(string CardNo)
        {
            if (!string.IsNullOrEmpty(CardNo))
            {
                JsonResult json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                var card = db.Cards.Where(w => w.CardNo == CardNo).FirstOrDefault();
                if (card == null)
                {
                    json.Data = new { Error = "此卡不存在！" };
                    return json;
                }
                if (card.Status != 0)
                {
                    json.Data = new { Error = GetStatus(card.Status) };
                    return json;
                }
                var vehicle = db.Vehicles.Where(w => w.Id == card.Vehicle).FirstOrDefault();
                if (vehicle == null)
                {
                    json.Data = new { Error = "无此卡对应的车辆信息" };
                    return json;
                }
                var tp = db.Transports.Where(w=>w.Card==card.Id && w.Status==0).FirstOrDefault();                
                if (tp == null)
                {
                    json.Data = new { Error = "此车未进厂或在运输途中" };
                    return json;
                }
                json.Data = new { Error="",Card = card.Id, card.CardNo, card.Vehicle,tp.Id,InFactory_Date=tp.InFactory_Date.ToString("yyyy-MM-dd"), 
                    vehicle.PlateNo, vehicle.BrandModel, vehicle.MotorNo,vehicle.OwnerName,tp.Comment};
                return json;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Load(LoadModels model)
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            using (var context = db)
            {
                var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                if (dept == null || !dept.DeptId.HasValue || dept.DeptId == Guid.Empty)
                {
                    ModelState.AddModelError("", "请设置操作员部门信息");
                    return View(model);
                }
                var card = context.Cards.Where(w => w.Id == model.Card).FirstOrDefault();
                if (card == null)
                {
                    ModelState.AddModelError("", "请读取卡号");
                    return View(model);
                }
                if (card.Status != 0)
                {
                    ModelState.AddModelError("", GetStatus(card.Status));
                    return View(model);
                }
                var tps = context.Transports.Where(w => w.Id == model.Id && w.Status == 0).ToList();
                if (tps.Count != 1)
                {
                    ModelState.AddModelError("", "此车在运输途中或无进厂数据");
                    return View(model);
                }
                if (model.Load_Inventory == Guid.Empty)
                {
                    ModelState.AddModelError("", "请选择存货");                    
                    return View(model);
                }
                if (model.Load_Quantity == 0)
                {
                    ModelState.AddModelError("", "请输入装车数量");
                    return View(model);
                }
                DXInfo.Models.Transport tp = tps[0];
                tp.Load_Date = DateTime.Now;
                tp.Load_DeptId = dept.DeptId.Value;
                tp.Load_UserId = userId;
                tp.Comment = model.Comment;
                tp.Load_Inventory = model.Load_Inventory;
                tp.Load_Quantity = model.Load_Quantity;
                tp.Status = 1;


                context.SaveChanges();
            }
            return View("LoadSucess");
        }
        #endregion
        public ActionResult GetInv(Guid? InvId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (InvId.HasValue)
            {
                var inv = db.Inventory.Where(w => w.Id == InvId).FirstOrDefault();
                var unit = db.UnitOfMeasures.Where(w=>w.Id==inv.UnitOfMeasure).FirstOrDefault();
                json.Data = new { UnitName = unit.Name, inv.Specs };
            }
            else
            {
                json.Data = new { UnitName = "", Specs = "" };
            }
            return json;
        }
        #region 卸货
        [Authorize]
        [HttpGet]
        public ActionResult Shipment(string CardNo)
        {
            if (!string.IsNullOrEmpty(CardNo))
            {
                JsonResult json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                var card = db.Cards.Where(w => w.CardNo == CardNo&& w.Status==0).FirstOrDefault();
                if (card == null)
                {
                    json.Data = new { Error = "此卡不存在" };
                    return json;
                }
                if (card.Status != 0)
                {
                    json.Data = new { Error = GetStatus(card.Status) };
                    return json;
                }
                var vehicle = db.Vehicles.Where(w => w.Id == card.Vehicle).FirstOrDefault();
                if (vehicle == null)
                {
                    json.Data = new { Error = "无此卡对应的车辆信息" };
                    return json;
                }	
                var tps = db.Transports.Where(w => w.Card == card.Id && w.Status == 1).ToList();                                
                if (tps.Count!=1)
                {
                    json.Data = new { Error = "此车未装车或在运输途中" };
                    return json;
                }
                var tp = tps[0];
                var inv = db.Inventory.Where(w => w.Id == tp.Load_Inventory).FirstOrDefault();
                var unit = db.UnitOfMeasures.Where(w => w.Id == inv.UnitOfMeasure).FirstOrDefault();
                json.Data = new
                {
                    Error = "",
                    Card = card.Id,
                    card.CardNo,
                    card.Vehicle,
                    tp.Id,
                    Load_Date = tp.Load_Date.Value.ToString("yyyy-MM-dd"),
                    Load_Inventory=inv.Name,
                    UnitName = unit.Name,
                    inv.Specs,
                    tp.Load_Quantity, vehicle.PlateNo, vehicle.BrandModel, vehicle.MotorNo, vehicle.OwnerName 
                ,tp.Comment};
                return json;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Shipment(ShipmentModels model)
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            using (var context = db)
            {
                var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                if (dept == null || !dept.DeptId.HasValue || dept.DeptId == Guid.Empty)
                {
                    ModelState.AddModelError("", "请设置操作员部门信息");
                    return View(model);
                }
                var tps = context.Transports.Where(w => w.Id == model.Id && w.Status == 1).ToList();
                if (tps.Count != 1)
                {
                    ModelState.AddModelError("", "此车未装车或在运输途中");
                    return View(model);
                }

                if (string.IsNullOrEmpty(model.Shipment_CheckUser))
                {
                    ModelState.AddModelError("", "请输入签收人");
                    return View(model);
                }
                if (model.Shipment_Quantity == 0)
                {
                    ModelState.AddModelError("", "请输入卸货数量");
                    return View(model);
                }

                DXInfo.Models.Transport tp = tps[0];
                tp.Shipment_Date = DateTime.Now;
                tp.Shipment_DeptId = dept.DeptId.Value;
                tp.Shipment_UserId = userId;
                tp.Comment = model.Comment;

                tp.Shipment_Quantity = model.Shipment_Quantity;
                tp.Shipment_CheckUser = model.Shipment_CheckUser;

                tp.Status = 2;

                context.SaveChanges();
            }
            return View("ShipmentSucess");
        }
        #endregion

        #region 出厂
        [Authorize]
        [HttpGet]
        public ActionResult OutFactory(string CardNo)
        {
            if (!string.IsNullOrEmpty(CardNo))
            {
                JsonResult json = new JsonResult();
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                var card = db.Cards.Where(w => w.CardNo == CardNo&& w.Status==0).FirstOrDefault();
                if (card == null)
                {
                    json.Data = new { Error = "此卡不存在" };
                    return json;
                }
                if (card.Status != 0)
                {
                    json.Data = new { Error = GetStatus(card.Status) };
                    return json;
                }
                var vehicle = db.Vehicles.Where(w => w.Id == card.Vehicle).FirstOrDefault();
                if (vehicle == null)
                {
                    json.Data = new { Error = "无此卡对应的车辆信息" };
                    return json;
                }
                var tps = db.Transports.Where(w => w.Card == card.Id && w.Status == 2).ToList();

                if (tps.Count!=1)
                {
                    json.Data = new { Error = "此车未卸货或在运输途中" };
                    return json;
                }
                var tp = tps[0];
                var inv = db.Inventory.Where(w => w.Id == tp.Load_Inventory).FirstOrDefault();
                var unit = db.UnitOfMeasures.Where(w => w.Id == inv.UnitOfMeasure).FirstOrDefault();
                //var u = db.aspnet_CustomProfile.Where(w => w.UserId == tp.Shipment_CheckUserId).FirstOrDefault();
                json.Data = new { Error="",Card = card.Id, card.CardNo, card.Vehicle, tp.Id, Load_Date = tp.Load_Date.Value.ToString("yyyy-MM-dd"), 
                    Load_Inventory=inv.Name, 
                    UnitName = unit.Name,
                    inv.Specs,
                    tp.Load_Quantity,
                    tp.Shipment_CheckUser,
                    Shipment_Date=tp.Shipment_Date.Value.ToString("yyyy-MM-dd"),
                    tp.Shipment_Quantity, vehicle.PlateNo, vehicle.BrandModel, vehicle.MotorNo, vehicle.OwnerName
                ,tp.Comment};
                return json;
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult OutFactory(OutFactoryModels model)
        {
            MembershipUser user = Membership.GetUser();
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            using (var context = db)
            {
                var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                if (dept == null || !dept.DeptId.HasValue || dept.DeptId == Guid.Empty)
                {
                    ModelState.AddModelError("", "请设置操作员部门信息");
                    return View(model);
                }
                var tps = context.Transports.Where(w => w.Id == model.Id && w.Status == 2).ToList();
                if (tps.Count != 1)
                {
                    ModelState.AddModelError("", "此车未卸货");
                    return View(model);
                }
                DXInfo.Models.Transport tp = tps[0];
                tp.OutFactory_Date = DateTime.Now;
                tp.OutFactory_DeptId = dept.DeptId.Value;
                tp.OutFactory_UserId = userId;
                tp.Comment = model.Comment;

                tp.Status = 3;

                context.SaveChanges();
            }
            return View("OutFactorySucess");
        }
        #endregion

        private void SetupVehicleListGrid(JQGrid grid)
        {
            grid.DataUrl = Url.Action("VehicleList_RequestData");
            grid.EditUrl = Url.Action("VehicleList_EditData");

            JQGridColumn balanceTypeColumn = grid.Columns.Find(c => c.DataField == "BalanceType");

            balanceTypeColumn.Formatter = new CustomFormatter()
            {
                FormatFunction = "ShowBalanceTypeName",
                UnFormatFunction = "UnShowBalanceTypeName"
            };
            balanceTypeColumn.EditType = EditType.DropDown;
            balanceTypeColumn.SearchType = SearchType.DropDown;
            if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                var editList = (from u in db.NameCode.Where(w=>w.Type=="BalanceType")
                                select new
                                {
                                    u.ID,
                                    u.Name
                                }).ToList();
                var list = editList.Select(s => new SelectListItem { Text = s.Name, Value = s.ID.ToString() });
                balanceTypeColumn.EditList = list.ToList<SelectListItem>();
                balanceTypeColumn.SearchList = list.ToList<SelectListItem>();
                balanceTypeColumn.SearchList.Insert(0, new SelectListItem { Text = "所有", Value = "" });
            }


            //JQGridColumn checkUserColumn = grid.Columns.Find(c => c.DataField == "Shipment_CheckUserId");

            //checkUserColumn.Formatter = new CustomFormatter()
            //{
            //    FormatFunction = "ShowCheckUserName",
            //    UnFormatFunction = "UnShowCheckUserName"
            //};
            //checkUserColumn.EditType = EditType.DropDown;
            //checkUserColumn.SearchType = SearchType.DropDown;
            //if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            //{
            //    var editList = (from u in db.aspnet_CustomProfile
            //                    select new
            //                    {
            //                        u.UserId,
            //                        u.FullName
            //                    }).ToList();
            //    var list = editList.Select(s => new SelectListItem { Text = s.FullName, Value = s.UserId.ToString() });
            //    checkUserColumn.EditList = list.ToList<SelectListItem>();
            //    checkUserColumn.SearchList = list.ToList<SelectListItem>();
            //    checkUserColumn.SearchList.Insert(0, new SelectListItem { Text = "所有", Value = "" });
            //}
            //---------------------------
            JQGridColumn linesColumn = grid.Columns.Find(c => c.DataField == "Lines");

            linesColumn.Formatter = new CustomFormatter()
            {
                FormatFunction = "ShowLinesName",
                UnFormatFunction = "UnShowLinesName"
            };
            linesColumn.EditType = EditType.DropDown;
            linesColumn.SearchType = SearchType.DropDown;
            if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                var editList = (from u in db.Lines
                                select new
                                {
                                    u.Id,
                                    u.Name
                                }).ToList();
                var list = editList.Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() });
                linesColumn.EditList = list.ToList<SelectListItem>();
                linesColumn.EditList.Insert(0, new SelectListItem { Text = "", Value = "" });
                linesColumn.SearchList = list.ToList<SelectListItem>();
                linesColumn.SearchList.Insert(0, new SelectListItem { Text = "所有", Value = "" });
            }
        }
        public ActionResult VehicleList()
        {
            var gridModel = new VehicleListGridModel();
            SetupVehicleListGrid(gridModel.VehicleListGrid);
            return View(gridModel);
        }
        public ActionResult VehicleList_RequestData()
        {
            var gridModel = new VehicleListGridModel();
            SetupVehicleListGrid(gridModel.VehicleListGrid);
            JQGridState gridState = gridModel.VehicleListGrid.GetState(false);
            Session["GridStateV"] = gridState;
            var tps = (from t in db.Transports

                      join c in db.Cards on t.Card equals c.Id into tc
                      from tcs in tc.DefaultIfEmpty()

                      join v in db.Vehicles on tcs.Vehicle equals v.Id into cv
                      from cvs in cv.DefaultIfEmpty()

                      //join dr in db.Drivers on cvs.Driver equals dr.Id into vdr
                      //from vdrs in vdr.DefaultIfEmpty()

                      join dr1 in db.Drivers on t.Driver equals dr1.Id into tdr
                      from tdrs in tdr.DefaultIfEmpty()

                      join n in db.NameCode on t.BalanceType equals n.ID into tn
                      from tns in tn.DefaultIfEmpty()

                      join l in db.Lines on t.Lines equals l.Id into tl
                      from tls in tl.DefaultIfEmpty()

                      join d1 in db.Depts on t.InFactory_DeptId equals d1.DeptId into td1
                      from td1s in td1.DefaultIfEmpty()

                      join u1 in db.aspnet_CustomProfile on t.InFactory_UserId equals u1.UserId into tu1
                      from tu1s in tu1.DefaultIfEmpty()

                      join d2 in db.Depts on t.Load_DeptId equals d2.DeptId into td2
                      from td2s in td2.DefaultIfEmpty()

                      join u2 in db.aspnet_CustomProfile on t.Load_UserId equals u2.UserId into tu2
                      from tu2s in tu2.DefaultIfEmpty()

                      join i in db.Inventory on t.Load_Inventory equals i.Id into ti
                      from tis in ti.DefaultIfEmpty()

                      join um in db.UnitOfMeasures on tis.UnitOfMeasure equals um.Id into ium
                      from iums in ium.DefaultIfEmpty()

                      join d3 in db.Depts on t.Shipment_DeptId equals d3.DeptId into td3
                      from td3s in td3.DefaultIfEmpty()

                      join u3 in db.aspnet_CustomProfile on t.Shipment_UserId equals u3.UserId into tu3
                      from tu3s in tu3.DefaultIfEmpty()

                      join d4 in db.Depts on t.OutFactory_DeptId equals d4.DeptId into td4
                      from td4s in td4.DefaultIfEmpty()

                      join u4 in db.aspnet_CustomProfile on t.OutFactory_UserId equals u4.UserId into tu4
                      from tu4s in tu4.DefaultIfEmpty()

                      //join u5 in db.aspnet_CustomProfile on t.Shipment_CheckUserId equals u5.UserId into tu5
                      //from tu5s in tu5.DefaultIfEmpty()

                      join d6 in db.Depts on t.ModifyDeptId equals d6.DeptId into td6
                      from td6s in td6.DefaultIfEmpty()

                      join u6 in db.aspnet_CustomProfile on t.ModifyUserId equals u6.UserId into tu6
                      from tu6s in tu6.DefaultIfEmpty()
                      select new
                      {
                          t.Id,
                          tcs.CardNo,
                          t.Status,
                          cvs.PlateNo,
                          cvs.BrandModel,
                          cvs.MotorNo,
                          OwnName = cvs.OwnerName,
                          DriverName = tdrs.Name,
                          t.BalanceType,
                          BalanceTypeName = tns.Name,
                          t.AgreeFreightRate,
                          t.FreightRate,
                          t.Shipper,
                          t.Shipper_Telephone,
                          t.Carrier,
                          t.Carrier_Telephone,
                          t.Lines,
                          LinesName = tls.Name,
                          tls.Mileage,
                          InFactory_Dept = td1s.DeptName,
                          t.InFactory_Date,
                          InFactory_Oper = tu1s.FullName,
                          Load_Dept = td2s.DeptName,
                          t.Load_Date,
                          Load_Oper = tu2s.FullName,
                          Load_Inventory = tis.Name,
                          UnitName = iums.Name,
                          tis.Specs,
                          t.Load_Quantity,
                          Shipment_Dept = td3s.DeptName,
                          t.Shipment_Date,
                          Shipment_Oper = tu3s.FullName,
                          t.Shipment_Quantity,
                          //t.Shipment_CheckUserId,
                          t.Shipment_CheckUser,
                          OutFactory_Dept = td4s.DeptName,
                          t.OutFactory_Date,
                          OutFactory_Oper = tu4s.FullName,
                          Modify_Dept=td6s.DeptName,
                          t.ModifyDate,
                          Modify_Oper=tu6s.FullName,
                          t.Comment
                      }).ToList();
            //var tpls = tps.ToList();
            var tpls = tps.Select(s => new {
                s.Id,
                s.CardNo,
                Status=s.Status==0?"进厂":s.Status==1?"装车":s.Status==2?"卸货":"出厂",
                s.PlateNo,
                s.BrandModel,
                s.MotorNo,
                s.OwnName,
                s.DriverName,
                s.BalanceType,
                s.BalanceTypeName,
                s.AgreeFreightRate,
                s.FreightRate,
                s.Shipper,
                s.Shipper_Telephone,
                s.Carrier,
                s.Carrier_Telephone,
                s.Lines,
                s.LinesName,
                s.Mileage,
                s.InFactory_Dept,
                s.InFactory_Date,
                s.InFactory_Oper,
                s.Load_Dept,
                s.Load_Date,
                s.Load_Oper,
                s.Load_Inventory,
                s.UnitName,
                s.Specs,
                s.Load_Quantity,
                s.Shipment_Dept,
                s.Shipment_Date,
                s.Shipment_Oper,
                s.Shipment_Quantity,
                //s.Shipment_CheckUserId,
                s.Shipment_CheckUser,
                s.OutFactory_Dept,
                s.OutFactory_Date,
                s.OutFactory_Oper,
                s.Modify_Dept,
                s.ModifyDate,
                s.Modify_Oper,
                s.Comment
            });
            return gridModel.VehicleListGrid.DataBind(tpls.AsQueryable());
            
        }
        public ActionResult ExportToExcel()
        {
            var gridModel = new VehicleListGridModel();
            SetupVehicleListGrid(gridModel.VehicleListGrid);
            var tps = (from t in db.Transports

                       join c in db.Cards on t.Card equals c.Id into tc
                       from tcs in tc.DefaultIfEmpty()

                       join v in db.Vehicles on tcs.Vehicle equals v.Id into cv
                       from cvs in cv.DefaultIfEmpty()

                       //join dr in db.Drivers on cvs.Driver equals dr.Id into vdr
                       //from vdrs in vdr.DefaultIfEmpty()

                       join dr1 in db.Drivers on t.Driver equals dr1.Id into tdr
                       from tdrs in tdr.DefaultIfEmpty()

                       join n in db.NameCode on t.BalanceType equals n.ID into tn
                       from tns in tn.DefaultIfEmpty()

                       join l in db.Lines on t.Lines equals l.Id into tl
                       from tls in tl.DefaultIfEmpty()

                       join d1 in db.Depts on t.InFactory_DeptId equals d1.DeptId into td1
                       from td1s in td1.DefaultIfEmpty()

                       join u1 in db.aspnet_CustomProfile on t.InFactory_UserId equals u1.UserId into tu1
                       from tu1s in tu1.DefaultIfEmpty()

                       join d2 in db.Depts on t.Load_DeptId equals d2.DeptId into td2
                       from td2s in td2.DefaultIfEmpty()

                       join u2 in db.aspnet_CustomProfile on t.Load_UserId equals u2.UserId into tu2
                       from tu2s in tu2.DefaultIfEmpty()

                       join i in db.Inventory on t.Load_Inventory equals i.Id into ti
                       from tis in ti.DefaultIfEmpty()

                       join um in db.UnitOfMeasures on tis.UnitOfMeasure equals um.Id into ium
                       from iums in ium.DefaultIfEmpty()

                       join d3 in db.Depts on t.Shipment_DeptId equals d3.DeptId into td3
                       from td3s in td3.DefaultIfEmpty()

                       join u3 in db.aspnet_CustomProfile on t.Shipment_UserId equals u3.UserId into tu3
                       from tu3s in tu3.DefaultIfEmpty()

                       join d4 in db.Depts on t.OutFactory_DeptId equals d4.DeptId into td4
                       from td4s in td4.DefaultIfEmpty()

                       join u4 in db.aspnet_CustomProfile on t.OutFactory_UserId equals u4.UserId into tu4
                       from tu4s in tu4.DefaultIfEmpty()

                       //join u5 in db.aspnet_CustomProfile on t.Shipment_CheckUserId equals u5.UserId into tu5
                       //from tu5s in tu5.DefaultIfEmpty()

                       join d6 in db.Depts on t.ModifyDeptId equals d6.DeptId into td6
                       from td6s in td6.DefaultIfEmpty()

                       join u6 in db.aspnet_CustomProfile on t.ModifyUserId equals u6.UserId into tu6
                       from tu6s in tu6.DefaultIfEmpty()
                       select new
                       {
                           t.Id,
                           tcs.CardNo,
                           t.Status,
                           cvs.PlateNo,
                           cvs.BrandModel,
                           cvs.MotorNo,
                           OwnName = cvs.OwnerName,
                           DriverName = tdrs.Name,
                           t.BalanceType,
                           BalanceTypeName = tns.Name,
                           t.AgreeFreightRate,
                           t.FreightRate,
                           t.Shipper,
                           t.Shipper_Telephone,
                           t.Carrier,
                           t.Carrier_Telephone,
                           t.Lines,
                           LinesName = tls.Name,
                           tls.Mileage,
                           InFactory_Dept = td1s.DeptName,
                           t.InFactory_Date,
                           InFactory_Oper = tu1s.FullName,
                           Load_Dept = td2s.DeptName,
                           t.Load_Date,
                           Load_Oper = tu2s.FullName,
                           Load_Inventory = tis.Name,
                           UnitName = iums.Name,
                           tis.Specs,
                           t.Load_Quantity,
                           Shipment_Dept = td3s.DeptName,
                           t.Shipment_Date,
                           Shipment_Oper = tu3s.FullName,
                           t.Shipment_Quantity,
                           //t.Shipment_CheckUserId,
                           t.Shipment_CheckUser,
                           OutFactory_Dept = td4s.DeptName,
                           t.OutFactory_Date,
                           OutFactory_Oper = tu4s.FullName,
                           Modify_Dept = td6s.DeptName,
                           t.ModifyDate,
                           Modify_Oper = tu6s.FullName,
                           t.Comment
                       }).ToList();
            //var tpls = tps.ToList();
            var tpls = tps.Select(s => new
            {
                s.Id,
                s.CardNo,
                Status = s.Status == 0 ? "进厂" : s.Status == 1 ? "装车" : s.Status == 2 ? "卸货" : "出厂",
                s.PlateNo,
                s.BrandModel,
                s.MotorNo,
                s.OwnName,
                s.DriverName,
                s.BalanceType,
                s.BalanceTypeName,
                s.AgreeFreightRate,
                s.FreightRate,
                s.Shipper,
                s.Shipper_Telephone,
                s.Carrier,
                s.Carrier_Telephone,
                s.Lines,
                s.LinesName,
                s.Mileage,
                s.InFactory_Dept,
                s.InFactory_Date,
                s.InFactory_Oper,
                s.Load_Dept,
                s.Load_Date,
                s.Load_Oper,
                s.Load_Inventory,
                s.UnitName,
                s.Specs,
                s.Load_Quantity,
                s.Shipment_Dept,
                s.Shipment_Date,
                s.Shipment_Oper,
                s.Shipment_Quantity,
                //s.Shipment_CheckUserId,
                s.Shipment_CheckUser,
                s.OutFactory_Dept,
                s.OutFactory_Date,
                s.OutFactory_Oper,
                s.Modify_Dept,
                s.ModifyDate,
                s.Modify_Oper,
                s.Comment
            });
            JQGridState gridState = Session["GridStateV"] as JQGridState;
            gridModel.VehicleListGrid.ExportToExcel(tpls.AsQueryable(),"车辆清单.xls", gridState);
            return View();
        }
        public ActionResult VehicleList_EditData(DXInfo.Models.Transport tp)
        {
            var gridModel = new VehicleListGridModel();
            SetupVehicleListGrid(gridModel.VehicleListGrid);
            if (gridModel.VehicleListGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                using (var context = db)
                {
                    var oldtp = context.Transports.Where(w => w.Id == tp.Id).FirstOrDefault();
                    var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                    if (oldtp == null)
                    {
                        return gridModel.VehicleListGrid.ShowEditValidationMessage("未找到车辆记录");
                    }

                    oldtp.BalanceType = tp.BalanceType;
                    oldtp.AgreeFreightRate = tp.AgreeFreightRate;
                    oldtp.FreightRate = tp.FreightRate;
                    oldtp.Shipper = tp.Shipper;
                    oldtp.Shipper_Telephone = tp.Shipper_Telephone;
                    oldtp.Carrier = tp.Carrier;
                    oldtp.Carrier_Telephone = tp.Carrier_Telephone;
                    oldtp.Lines = tp.Lines;
                    oldtp.Load_Quantity = tp.Load_Quantity;
                    oldtp.Shipment_Quantity = tp.Shipment_Quantity;
                    oldtp.Shipment_CheckUser = tp.Shipment_CheckUser;
                    oldtp.Comment = tp.Comment;

                    oldtp.ModifyDate = DateTime.Now;
                    oldtp.ModifyUserId = userId;
                    oldtp.ModifyDeptId = dept.DeptId;


                    DXInfo.Models.TransportsLog tl = new DXInfo.Models.TransportsLog();

                    //tl.Id = Guid.NewGuid();
                    tl.tpId = oldtp.Id;
                    tl.AgreeFreightRate = oldtp.AgreeFreightRate;
                    tl.BalanceType = oldtp.BalanceType;
                    tl.Card = oldtp.Card;
                    tl.Carrier = oldtp.Carrier;
                    tl.Carrier_Telephone = oldtp.Carrier_Telephone;
                    tl.Comment = oldtp.Comment;
                    tl.Driver = oldtp.Driver;
                    tl.FreightRate = oldtp.FreightRate;
                    tl.InFactory_Date = oldtp.InFactory_Date;
                    tl.InFactory_DeptId = oldtp.InFactory_DeptId;
                    tl.InFactory_UserId = oldtp.InFactory_UserId;
                    tl.Lines = oldtp.Lines;
                    tl.Load_Date = oldtp.Load_Date;
                    tl.Load_DeptId = oldtp.Load_DeptId;
                    tl.Load_Inventory = oldtp.Load_Inventory;
                    tl.Load_Quantity = oldtp.Load_Quantity;
                    tl.Load_UserId = oldtp.Load_UserId;
                    tl.ModifyDate = oldtp.ModifyDate;
                    tl.ModifyDeptId = oldtp.ModifyDeptId;
                    tl.ModifyUserId = oldtp.ModifyUserId;
                    tl.OutFactory_Date = oldtp.OutFactory_Date;
                    tl.OutFactory_DeptId = oldtp.OutFactory_DeptId;
                    tl.OutFactory_UserId = oldtp.OutFactory_UserId;
                    tl.Shipment_CheckUser = oldtp.Shipment_CheckUser;
                    tl.Shipment_Date = oldtp.Shipment_Date;
                    tl.Shipment_DeptId = oldtp.Shipment_DeptId;
                    tl.Shipment_Quantity = oldtp.Shipment_Quantity;
                    tl.Shipment_UserId = oldtp.Shipment_UserId;
                    tl.Shipper = oldtp.Shipper;
                    tl.Shipper_Telephone = oldtp.Shipper_Telephone;
                    tl.Status = oldtp.Status;
                    

                    context.TransportsLog.Add(tl);

                    context.SaveChanges();
                }
            }
            return RedirectToAction("VehicleList");
        }
    }
}
