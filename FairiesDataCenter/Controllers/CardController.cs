using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trirand.Web.Mvc;
using ynhnTransportManage.Models;
using System.Web.Security;

namespace ynhnTransportManage.Controllers
{
    public class CardController : Controller
    {
        //
        // GET: /Card/

        public ActionResult Index()
        {
            return View();
        }
        private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        #region 发卡
        
        private void SetupIssueGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Issue_RequestData");
            grid.EditUrl = Url.Action("Issue_EditData");
            
        }
        public ActionResult Issue()
        {
            var gridModel = new IssueCardsGridModel();
            SetupIssueGridModel(gridModel.CardsGrid);
            return View(gridModel);
        }
        public ActionResult Issue_RequestData()
        {
            var gridModel = new IssueCardsGridModel();
            SetupIssueGridModel(gridModel.CardsGrid);
            var vehicles =
                (from v in db.Vehicles where !(from c in db.Cards select c.Vehicle).Contains(v.Id)
                 //join d in db.Drivers on v.Driver equals d.Id into vd
                 //from veh in vd.DefaultIfEmpty()                 
                 select new
                 {
                     v.Id,
                     v.PlateNo,
                     v.MotorNo,
                     v.BrandModel,
                     v.Comment,
                     v.OwnerName
                 }).ToList();
            var veh2 = vehicles.Select(s => new { CardNo="",s.Id, s.PlateNo, s.BrandModel, s.MotorNo, s.OwnerName, s.Comment });
            return gridModel.CardsGrid.DataBind(veh2.ToList().AsQueryable());
        }
        public ActionResult Issue_EditData(DXInfo.Models.Vehicle vehicle,string CardNo)
        {
            var gridModel = new VehicleGridModel();            
            if (gridModel.VehicleGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                if (CardNo.Length != 5) return gridModel.VehicleGrid.ShowEditValidationMessage("请输入5位卡号");
                var c = db.Cards.Where(w => w.CardNo == CardNo).FirstOrDefault();
                if (c != null)
                {
                    return gridModel.VehicleGrid.ShowEditValidationMessage("卡号已存在");
                }
                using(var Content = db)
                {
                    var dept = db.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                    Guid deptId = dept.DeptId.HasValue ? dept.DeptId.Value : Guid.Empty;
                    DXInfo.Models.Card card = new DXInfo.Models.Card();
                    card.CardNo=CardNo;
                    card.Vehicle = vehicle.Id;
                    card.CreateDate = DateTime.Now;
                    card.UserId = userId;
                    card.DeptId = deptId;
                    db.Cards.Add(card);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Issue");
        }
        #endregion
        #region 挂失
        private void SetupLossGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Loss_RequestData");
            grid.EditUrl = Url.Action("Loss_EditData");
        }
        public ActionResult Loss()
        {
            var gridModel = new LossCardsGridModel();
            SetupLossGridModel(gridModel.CardsGrid);
            return View(gridModel);
        }
        public ActionResult Loss_RequestData()
        {
            var gridModel = new LossCardsGridModel();
            SetupLossGridModel(gridModel.CardsGrid);

            var cards =
                (from c in db.Cards.Where(w => w.Status == 0)
                 join v in db.Vehicles on c.Vehicle equals v.Id

                 join u in db.aspnet_CustomProfile on c.UserId equals u.UserId into cu
                 from cus in cu.DefaultIfEmpty()

                 join d in db.Depts on c.DeptId equals d.DeptId into cd
                 from cds in cd.DefaultIfEmpty()
                 select new
                 {
                     c.Id,
                     IsInUser=true,
                     c.CardNo,
                     c.CreateDate,
                     v.PlateNo,
                     v.MotorNo,
                     v.BrandModel,
                     cus.FullName,
                     cds.DeptName
                 }).ToList();
            return gridModel.CardsGrid.DataBind(cards.AsQueryable());
        }
        public ActionResult Loss_EditData(Guid Id)
        {
            var gridModel = new LossCardsGridModel();
            SetupLossGridModel(gridModel.CardsGrid);
            if (gridModel.CardsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                using (var context = db)
                {
                    var card = context.Cards.Where(w => w.Id == Id).FirstOrDefault();
                    card.Status = 1;
                    card.LossUserId = userId;
                    card.LossDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Loss");
        }
        #endregion
        #region 解挂
        private void SetupFoundGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Found_RequestData");
            grid.EditUrl = Url.Action("Found_EditData");
        }
        public ActionResult Found()
        {
            var gridModel = new FoundCardsGridModel();
            SetupFoundGridModel(gridModel.CardsGrid);
            return View(gridModel);
        }
        public ActionResult Found_RequestData()
        {
            var gridModel = new FoundCardsGridModel();
            SetupFoundGridModel(gridModel.CardsGrid);

            var cards =
                (from c in db.Cards.Where(w => w.Status == 1)
                 join v in db.Vehicles on c.Vehicle equals v.Id
                 join u in db.aspnet_CustomProfile on c.UserId equals u.UserId into cu
                 from cus in cu.DefaultIfEmpty()

                 join d in db.Depts on c.DeptId equals d.DeptId into cd 
                 from cds in cd.DefaultIfEmpty()
                 select new
                 {
                     c.Id,
                     IsInUser = true,
                     c.CardNo,
                     c.LossDate,
                     v.PlateNo,
                     v.MotorNo,
                     v.BrandModel,
                     cus.FullName,
                     cds.DeptName
                 }).ToList();
            return gridModel.CardsGrid.DataBind(cards.AsQueryable());
        }
        public ActionResult Found_EditData(Guid Id)
        {
            var gridModel = new FoundCardsGridModel();
            SetupFoundGridModel(gridModel.CardsGrid);
            if (gridModel.CardsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                using (var context = db)
                {
                    var card = context.Cards.Where(w => w.Id == Id).FirstOrDefault();
                    card.Status = 0;
                    card.FoundUserId = userId;
                    card.FoundDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Found");
        }
        #endregion
        #region 补卡
        private void SetupAddGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Add_RequestData");
            grid.EditUrl = Url.Action("Add_EditData");
        }
        public ActionResult Add()
        {
            var gridModel = new AddCardsGridModel();
            SetupAddGridModel(gridModel.CardsGrid);
            return View(gridModel);
        }
        public ActionResult Add_RequestData()
        {
            var gridModel = new AddCardsGridModel();
            SetupAddGridModel(gridModel.CardsGrid);

            var cards =
                (from c in db.Cards.Where(w => w.Status == 1)
                 join v in db.Vehicles on c.Vehicle equals v.Id
                 join u in db.aspnet_CustomProfile on c.UserId equals u.UserId into cu
                 from cus in cu.DefaultIfEmpty()

                 join d in db.Depts on c.DeptId equals d.DeptId into cd
                 from cds in cd.DefaultIfEmpty()
                 select new
                 {
                     c.Id,
                     IsInUser = true,
                     c.CardNo,
                     c.SecondCardNo,
                     c.AddReason,
                     c.LossDate,
                     v.PlateNo,
                     v.MotorNo,
                     v.BrandModel,
                     cus.FullName,
                     cds.DeptName
                 }).ToList();
            return gridModel.CardsGrid.DataBind(cards.AsQueryable());
        }
        public ActionResult Add_EditData(DXInfo.Models.Card card)
        {
            var gridModel = new AddCardsGridModel();
            SetupAddGridModel(gridModel.CardsGrid);
            if (gridModel.CardsGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {

                if (string.IsNullOrEmpty(card.SecondCardNo) || card.SecondCardNo.Length != 5) return gridModel.CardsGrid.ShowEditValidationMessage("请输入5位卡号");
                var c = db.Cards.Where(w => w.CardNo == card.SecondCardNo).FirstOrDefault();
                if (c != null)
                    return gridModel.CardsGrid.ShowEditValidationMessage("卡号已存在");
                MembershipUser user = Membership.GetUser();
                Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                using (var context = db)
                {
                    var dept = context.aspnet_CustomProfile.Where(w => w.UserId == userId).FirstOrDefault();
                    var oldcard = context.Cards.Where(w => w.Id == card.Id).FirstOrDefault();
                    oldcard.Status = 2;
                    oldcard.SecondCardNo = card.SecondCardNo;
                    oldcard.AddDate = DateTime.Now;
                    oldcard.AddUserId = userId;
                    oldcard.AddReason = card.AddReason;

                    DXInfo.Models.Card newcard = new DXInfo.Models.Card();
                    newcard.CardNo = card.SecondCardNo;
                    newcard.Vehicle = oldcard.Vehicle;
                    newcard.CreateDate = DateTime.Now;
                    newcard.UserId = userId;
                    newcard.DeptId = dept.DeptId.HasValue ? dept.DeptId.Value : Guid.Empty;

                    context.Cards.Add(newcard);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region 管理
        private void SetupManageGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Manage_RequestData");
            grid.EditUrl = Url.Action("Manage_EditData");
        }
        public ActionResult Manage()
        {
            var gridModel = new ManageCardsGridModel();
            SetupManageGridModel(gridModel.CardsGrid);
            return View(gridModel);
        }
        public ActionResult Manage_RequestData()
        {
            var gridModel = new ManageCardsGridModel();
            SetupAddGridModel(gridModel.CardsGrid);

            JQGridState gridState = gridModel.CardsGrid.GetState(false);
            Session["GridState"] = gridState;
            var cards =
                 from c in db.Cards
                 join v in db.Vehicles on c.Vehicle equals v.Id into cv
                 from cvs in cv.DefaultIfEmpty()

                 //join o in db.Drivers on cvs.Driver equals o.Id into cvso
                 //from cvsos in cvso.DefaultIfEmpty()


                 join u in db.aspnet_CustomProfile on c.UserId equals u.UserId into cu1
                 from cu1s in cu1.DefaultIfEmpty()

                 join u1 in db.aspnet_CustomProfile on c.LossUserId equals u1.UserId into cu2
                 from cu2s in cu2.DefaultIfEmpty()

                 join u2 in db.aspnet_CustomProfile on c.FoundUserId equals u2.UserId into cu3
                 from cu3s in cu3.DefaultIfEmpty()

                 join u3 in db.aspnet_CustomProfile on c.AddUserId equals u3.UserId into cu4
                 from cu4s in cu4.DefaultIfEmpty()

                 join d in db.Depts on c.DeptId equals d.DeptId into cd 
                 from cds in cd.DefaultIfEmpty()

                 select new
                 {
                     c.Id,
                     c.CardNo,
                     c.Status,
                     cvs.PlateNo,
                     cvs.MotorNo,
                     cvs.BrandModel,

                     cvs.OwnerName,
                     cds.DeptName,

                     IssueOper = cu1s.FullName,
                     c.CreateDate,

                     LossOper = cu2s.FullName,
                     c.LossDate,

                     FoundOper = cu3s.FullName,
                     c.FoundDate,

                     c.SecondCardNo,
                     AddOper = cu4s.FullName,
                     c.AddDate,
                     c.AddReason
                 };
            var cards2 = cards.Select(s => new
            {
                s.Id,
                s.CardNo,
                Status = s.Status == 0 ? "正常在用" :
                    s.Status == 1 ? "挂失" : "补卡",
                s.PlateNo,
                s.MotorNo,
                s.BrandModel,
                s.OwnerName,
                s.DeptName,
                s.IssueOper,
                s.CreateDate,
                s.LossOper,
                s.LossDate,
                s.FoundOper,
                s.FoundDate,
                s.SecondCardNo,
                s.AddOper,
                s.AddDate,
                s.AddReason
            });
            return gridModel.CardsGrid.DataBind(cards2.AsQueryable());
        }
        public ActionResult Manage_EditData(DXInfo.Models.Card card)
        {
            return RedirectToAction("Manage");
        }
        public ActionResult ExportToExcel()
        {
            var gridModel = new ManageCardsGridModel();
            SetupAddGridModel(gridModel.CardsGrid);

            var cards =
                 from c in db.Cards
                 join v in db.Vehicles on c.Vehicle equals v.Id into cv
                 from cvs in cv.DefaultIfEmpty()

                 //join o in db.Drivers on cvs.Driver equals o.Id into cvso
                 //from cvsos in cvso.DefaultIfEmpty()


                 join u in db.aspnet_CustomProfile on c.UserId equals u.UserId into cu1
                 from cu1s in cu1.DefaultIfEmpty()

                 join u1 in db.aspnet_CustomProfile on c.LossUserId equals u1.UserId into cu2
                 from cu2s in cu2.DefaultIfEmpty()

                 join u2 in db.aspnet_CustomProfile on c.FoundUserId equals u2.UserId into cu3
                 from cu3s in cu3.DefaultIfEmpty()

                 join u3 in db.aspnet_CustomProfile on c.AddUserId equals u3.UserId into cu4
                 from cu4s in cu4.DefaultIfEmpty()

                 join d in db.Depts on c.DeptId equals d.DeptId into cd
                 from cds in cd.DefaultIfEmpty()

                 select new
                 {
                     c.Id,
                     c.CardNo,
                     c.Status,
                     cvs.PlateNo,
                     cvs.MotorNo,
                     cvs.BrandModel,

                     cvs.OwnerName,
                     cds.DeptName,

                     IssueOper = cu1s.FullName,
                     c.CreateDate,

                     LossOper = cu2s.FullName,
                     c.LossDate,

                     FoundOper = cu3s.FullName,
                     c.FoundDate,

                     c.SecondCardNo,
                     AddOper = cu4s.FullName,
                     c.AddDate,
                     c.AddReason
                 };
            var cards2 = cards.ToList().Select(s => new
            {
                s.Id,
                s.CardNo,
                Status = s.Status == 0 ? "正常在用" :
                    s.Status == 1 ? "挂失" : "补卡",
                s.PlateNo,
                s.MotorNo,
                s.BrandModel,
                s.OwnerName,
                s.DeptName,
                s.IssueOper,
                s.CreateDate,
                s.LossOper,
                s.LossDate,
                s.FoundOper,
                s.FoundDate,
                s.SecondCardNo,
                s.AddOper,
                s.AddDate,
                s.AddReason
            });
            //JQGridState jqstate = gridModel.CardsGrid.GetState(true);
            JQGridState gridState = Session["GridState"] as JQGridState;
            gridModel.CardsGrid.ExportToExcel(cards2.AsQueryable(), "IC卡清单.xls", gridState);
            return View();
        }
        #endregion

        #region 卡号是否存在
        public ActionResult IsHaveCardNo(string cardNo)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var card = db.Cards.Where(w => w.CardNo == cardNo).FirstOrDefault();
            if (card == null)
            {
                json.Data = new { IsCard = false };
            }
            else
            {
                json.Data = new { IsCard = true };
            }
            return json;
        }
        #endregion
    }
}
