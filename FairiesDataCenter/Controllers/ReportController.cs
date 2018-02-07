using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ynhnTransportManage.Models;
using Trirand.Web.Mvc;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Web.Security;
using DXInfo.Data.Contracts;
using System.Data.Objects.SqlClient;
using System.Web.Routing;


namespace ynhnTransportManage.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");    
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #region 不用
        #region Report1 会员资料查询
        [Authorize]
        public ActionResult Report1()
        {
            if (Session["Report1Model"] != null)
            {
                Report1Model rm = Session["Report1Model"] as Report1Model;
                return View(rm);
            }
            else
            {
                Report1Model rm = new Report1Model();
                rm.result = new List<Report1Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report1(Report1Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            var ps = from p in Uow.CardPoints.GetAll()
                     group p by p.Card into g
                     select new { Card = g.Key, Points = g.Sum(s => s.Point) };
            var re = from r in Uow.Recharges.GetAll()
                     where r.RechargeType == 0 || r.RechargeType == 1
                     group r by r.Card into g
                     select new { Card = g.Key, Recharg = g.Sum(s => s.Amount) };

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

                    join u1 in Uow.aspnet_CustomProfile.GetAll() on c.LossUserId equals u1.UserId into cu1
                    from cus1 in cu1.DefaultIfEmpty()

                    join d1 in Uow.Depts.GetAll() on c.LossDeptId equals d1.DeptId into cd1
                    from cds1 in cd1.DefaultIfEmpty()

                    join u2 in Uow.aspnet_CustomProfile.GetAll() on c.FoundUserId equals u2.UserId into cu2
                    from cus2 in cu2.DefaultIfEmpty()

                    join d2 in Uow.Depts.GetAll() on c.FoundDeptId equals d2.DeptId into cd2
                    from cds2 in cd2.DefaultIfEmpty()

                    join u3 in Uow.aspnet_CustomProfile.GetAll() on c.AddUserId equals u3.UserId into cu3
                    from cus3 in cu3.DefaultIfEmpty()

                    join d3 in Uow.Depts.GetAll() on c.AddDeptId equals d3.DeptId into cd3
                    from cds3 in cd3.DefaultIfEmpty()

                    join r in re on c.Id equals r.Card into cr
                    from crs in cr.DefaultIfEmpty()

                    where c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                    select new
                    {
                        CardTypeId = c.CardType,
                        CardLevelId = c.CardLevel,
                        c.DeptId,
                        AddDate = c.AddDate,
                        AddReason = c.AddReason,
                        Balance = c.Balance,
                        Recharge = crs.Recharg == null ? 0 : crs.Recharg,
                        CardLevel = cls.Name,
                        CardNo = c.CardNo,
                        CardType = cts.Name,
                        CreateDate = c.CreateDate,
                        FoundDate = c.FoundDate,
                        LossDate = c.LossDate,
                        Status = c.Status == 0 ? "正常在用" : c.Status == 1 ? "挂失" : "补卡",
                        iStatus = c.Status,
                        SecondCardNo = c.SecondCardNo,
                        Email = cms.Email,
                        IdCard = cms.IdCard,
                        LinkAddress = cms.LinkAddress,
                        LinkPhone = cms.LinkPhone,
                        MemberName = cms.MemberName,
                        Points = cps.Points == null ? 0 : cps.Points,
                        FullName = cus.FullName,
                        DeptName = cds.DeptName,
                        LossFullName = cus1.FullName,
                        LossDeptName = cds1.DeptName,
                        FoundFullName = cus2.FullName,
                        FoundDeptName = cds2.DeptName,
                        AddFullName = cus3.FullName,
                        AddDeptName = cds3.DeptName,
                        cms.Sex,
                        cms.Birthday,
                    };
            if (rm.CardType.HasValue)
            {
                q = q.Where(w => w.CardTypeId == rm.CardType.Value);
            }
            if (rm.CardLevel.HasValue)
            {
                q = q.Where(w => w.CardLevelId == rm.CardLevel.Value);
            }
            if (rm.DeptId.HasValue)
            {
                q = q.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.Status.HasValue)
            {
                q = q.Where(w => w.iStatus == rm.Status.Value);
            }
            if (!string.IsNullOrEmpty(rm.CardNo))
            {
                q = q.Where(w => w.CardNo.Contains(rm.CardNo));
            }
            if (!string.IsNullOrEmpty(rm.MemberName))
            {
                q = q.Where(w => w.MemberName.Contains(rm.MemberName));
            }
            var q1 = from c in q.ToList()
                     select new Report1Result()
                     {
                         AddDate = c.AddDate,
                         AddReason = c.AddReason,
                         Balance = c.Balance,
                         Recharge = c.Recharge,
                         CardLevel = c.CardLevel,
                         CardNo = c.CardNo,
                         CardType = c.CardType,
                         CreateDate = c.CreateDate,
                         FoundDate = c.FoundDate,
                         LossDate = c.LossDate,
                         Status = c.Status,
                         SecondCardNo = c.SecondCardNo,
                         Email = c.Email,
                         IdCard = c.IdCard,
                         LinkAddress = c.LinkAddress,
                         LinkPhone = c.LinkPhone,
                         MemberName = c.MemberName,
                         Points = c.Points,
                         FullName = c.FullName,
                         DeptName = c.DeptName,
                         LossFullName = c.LossFullName,
                         LossDeptName = c.LossDeptName,
                         FoundFullName = c.FoundFullName,
                         FoundDeptName = c.FoundDeptName,
                         AddFullName = c.AddFullName,
                         AddDeptName = c.AddDeptName,
                         Sex = c.Sex,
                         Birthday = c.Birthday.HasValue ? c.Birthday.Value.ToString("yyyy-MM-dd") : "",
                     };
            rm.result = q1.ToList();

            if (q1.Count() > 0)
            {
                rm.Points = Math.Round(q1.Sum(a => a.Points), 2);
                rm.Balance = Math.Round(q1.Sum(s => s.Balance), 2);
                rm.Recharge = Math.Round(q1.Sum(s => s.Recharge), 2);
            }
            else
            {
                rm.Points = 0;
                rm.Balance = 0;
                rm.Recharge = 0;
            }
            Session["Report1Model"] = rm;
            return RedirectToAction("Report1");
        }

        public void Report1ExportToExcel()
        {
            if (Session["Report1Model"] == null) return;
            var m = Session["Report1Model"] as Report1Model;
            object dataSource = m.result;
            string fileName = "会员资料查询.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "CardType";
            field.HeaderText = "卡类型";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardLevel";
            field.HeaderText = "卡级别";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardNo";
            field.HeaderText = "卡号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MemberName";
            field.HeaderText = "会员名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Balance";
            field.HeaderText = "余额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Recharge";
            field.HeaderText = "充值";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Points";
            field.HeaderText = "积分";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CreateDate";
            field.HeaderText = "发卡日期";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Status";
            field.HeaderText = "卡状态";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Email";
            field.HeaderText = "Email";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "IdCard";
            field.HeaderText = "证件号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LinkAddress";
            field.HeaderText = "联系地址";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LinkPhone";
            field.HeaderText = "联系电话";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Sex";
            field.HeaderText = "性别";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Birthday";
            field.HeaderText = "生日";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LossDate";
            field.HeaderText = "挂失日期";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LossFullName";
            field.HeaderText = "挂失操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LossDeptName";
            field.HeaderText = "挂失门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FoundDate";
            field.HeaderText = "解挂日期";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FoundFullName";
            field.HeaderText = "解挂操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FoundDeptName";
            field.HeaderText = "解挂门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SecondCardNo";
            field.HeaderText = "补卡卡号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddDate";
            field.HeaderText = "补卡日期";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddReason";
            field.HeaderText = "补卡原因";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddFullName";
            field.HeaderText = "补卡操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddDeptName";
            field.HeaderText = "补卡门店";
            view.Columns.Add(field);
            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report3 充值明细查询
        [Authorize]
        public ActionResult Report3()
        {
            if (Session["Report3Model"] != null)
            {
                Report3Model rm = Session["Report3Model"] as Report3Model;
                return View(rm);
            }
            else
            {
                Report3Model rm = new Report3Model();
                rm.result = new List<Report3Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report3(Report3Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            var q = from r in Uow.Recharges.GetAll()
                    join c in Uow.Cards.GetAll() on r.Card equals c.Id into rc
                    from rcs in rc.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on rcs.Member equals m.Id into rcsm
                    from rcsms in rcsm.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                    from rus in ru.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                    from rds in rd.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                    from rps in rp.DefaultIfEmpty()

                    where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate

                    select new
                    {
                        r.DeptId,
                        r.UserId,
                        iRechargeType = r.RechargeType,
                        iPayType = r.PayType,
                        Amount = r.Amount,
                        Balance = r.Balance,
                        CreateDate = r.CreateDate,
                        Donate = r.Donate,
                        LastBalance = r.LastBalance,
                        RechargeType = r.RechargeType == 0 ? "普通充值" : r.RechargeType == 1 ? "补卡充值" : "发卡充值",
                        CardNo = rcs.CardNo,
                        MemberName = rcsms.MemberName,
                        FullName = rus.FullName,
                        DeptName = rds.DeptName,
                        PayType = rps.Name,
                    };

            if (rm.DeptId.HasValue)
            {
                q = q.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q = q.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.RechargeType.HasValue)
            {
                q = q.Where(w => w.iRechargeType == rm.RechargeType.Value);
            }
            if (rm.PayType.HasValue)
            {
                q = q.Where(w => w.iPayType == rm.PayType.Value);
            }
            if (!string.IsNullOrEmpty(rm.CardNo))
            {
                q = q.Where(w => w.CardNo.Contains(rm.CardNo));
            }
            if (!string.IsNullOrEmpty(rm.MemberName))
            {
                q = q.Where(w => w.MemberName.Contains(rm.MemberName));
            }
            var q1 = from r in q.ToList()
                     select new Report3Result()
                     {
                         Amount = r.Amount,
                         Balance = r.Balance,
                         CreateDate = r.CreateDate,
                         Donate = r.Donate,
                         LastBalance = r.LastBalance,
                         RechargeType = r.RechargeType,
                         CardNo = r.CardNo,
                         MemberName = r.MemberName,
                         FullName = r.FullName,
                         DeptName = r.DeptName,
                         PayType = r.PayType,
                     };

            if (q1.Count() > 0)
            {
                rm.Amount = Math.Round(q1.Sum(s => s.Amount), 2);
                rm.Donate = Math.Round(q1.Sum(s => s.Donate), 2);
            }
            else
            {
                rm.Amount = 0;
                rm.Donate = 0;
            }
            rm.result = q1.ToList();
            Session["Report3Model"] = rm;
            return RedirectToAction("Report3");
        }
        public void Report3ExportToExcel()
        {
            if (Session["Report3Model"] == null) return;
            var m = Session["Report3Model"] as Report3Model;
            object dataSource = m.result;
            string fileName = "充值明细查询.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RechargeType";
            field.HeaderText = "充值";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardNo";
            field.HeaderText = "卡号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MemberName";
            field.HeaderText = "会员名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LastBalance";
            field.HeaderText = "上次余额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "充值金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Donate";
            field.HeaderText = "赠送";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Balance";
            field.HeaderText = "余额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CreateDate";
            field.HeaderText = "充值日期";
            view.Columns.Add(field);
            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report6 业务量统计
        [Authorize]
        public ActionResult Report6()
        {
            if (Session["Report6Model"] != null)
            {
                Report6Model rm = Session["Report6Model"] as Report6Model;
                return View(rm);
            }
            else
            {
                Report6Model rm = new Report6Model();
                Report6Result r6 = new Report6Result();
                r6.Card = new List<Report6Card>();
                r6.MemberConsume = new List<Report6Consume>();
                r6.NoMemberConsume = new List<Report6Consume>();
                r6.Recharge = new List<Report6Recharge>();
                rm.result = r6;
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                return View(rm);
            }
        }

        public void Report6ExportToExcel()
        {
            if (Session["Report6Model"] == null) return;
            var m = Session["Report6Model"] as Report6Model;
            Report6Result dataSource = m.result;
            string fileName = "业务量统计.xls";

            GridView view1 = new GridView();
            view1.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view1.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "充值金额";
            view1.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Donate";
            field.HeaderText = "充值赠送";
            view1.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Count";
            field.HeaderText = "充值次数";
            view1.Columns.Add(field);

            GridView view2 = new GridView();
            view2.AutoGenerateColumns = false;

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view2.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "消费金额";
            view2.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "商品数量";
            view2.Columns.Add(field);


            field = new BoundField();
            field.DataField = "Count";
            field.HeaderText = "消费次数";
            view2.Columns.Add(field);

            GridView view3 = new GridView();
            view3.AutoGenerateColumns = false;

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view3.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "消费金额";
            view3.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "商品数量";
            view3.Columns.Add(field);


            field = new BoundField();
            field.DataField = "Count";
            field.HeaderText = "消费次数";
            view3.Columns.Add(field);

            GridView view4 = new GridView();
            view4.AutoGenerateColumns = false;

            field = new BoundField();
            field.DataField = "Status";
            field.HeaderText = "卡状态";
            view4.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Balance";
            field.HeaderText = "余额";
            view4.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Point";
            field.HeaderText = "积分";
            view4.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Fee";
            field.HeaderText = "补卡费";
            view4.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Count";
            field.HeaderText = "数量";
            view4.Columns.Add(field);

            view1.DataSource = dataSource.Recharge;
            view1.DataBind();

            view2.DataSource = dataSource.MemberConsume;
            view2.DataBind();

            view3.DataSource = dataSource.NoMemberConsume;
            view3.DataBind();

            view4.DataSource = dataSource.Card;
            view4.DataBind();

            Table tb = new Table();
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            td.Text = "充值";
            TableCell td2 = new TableCell();
            td2.Controls.Add(view1);

            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tb.Rows.Add(tr);

            tr = new TableRow();
            td = new TableCell();
            td.Text = "会员消费";
            td2 = new TableCell();
            td2.Controls.Add(view2);

            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tb.Rows.Add(tr);

            tr = new TableRow();
            td = new TableCell();
            td.Text = "非会员消费";
            td2 = new TableCell();
            td2.Controls.Add(view3);

            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tb.Rows.Add(tr);

            tr = new TableRow();
            td = new TableCell();
            td.Text = "会员卡";
            td2 = new TableCell();
            td2.Controls.Add(view4);

            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tb.Rows.Add(tr);

            this.Response.ClearContent();

            this.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
            this.Response.ContentType = "application/excel";
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            tb.RenderControl(writer2);
            this.Response.Write(writer.ToString());
            this.Response.End();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report6(Report6Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            if (rm.result == null)
            {
                rm.result = new Report6Result();
            }
            #region 充值
            var q1 = from r in Uow.Recharges.GetAll()
                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()
                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 0
                     select new { Id = Guid.NewGuid(), r.DeptId, PayType = rps.Name, r.Amount, r.Donate };
            if (rm.DeptId.HasValue)
            {
                q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q11 = from q in q1
                      group q by q.PayType into g
                      select new Report6Recharge()
                      {
                          PayType = g.Key,
                          Amount = g.Sum(s => s.Amount),
                          Donate = g.Sum(s => s.Donate),
                          Count = g.Count(),
                      };
            var q12 = q11.ToList();
            rm.result.Recharge = q12;
            #endregion
            #region 会员消费
            var q2 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                     from clcsps in clcsp.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && (clcs.ConsumeType == 0 || clcs.ConsumeType == 2)

                     select new
                     {
                         ConsumeListId = cl.Id,
                         ConsumeId = clcs.Id,
                         cl.DeptId,
                         Amount = cl.Amount * clcs.Discount / 100,
                         cl.Quantity,
                         PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                     };

            if (rm.DeptId.HasValue)
            {
                q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q21 = from q in q2
                      group q by q.PayType into g
                      select new Report6Consume()
                      {
                          PayType = g.Key,
                          Amount = g.Sum(s => s.Amount),
                          Quantity = g.Sum(s => s.Quantity),
                          Count = g.Count()
                      };
            var q22 = q21.ToList();
            var q23 = from c in Uow.Consume.GetAll()

                      where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate && (c.ConsumeType == 0 || c.ConsumeType == 2)
                      select new
                      {
                          ConsumeId = c.Id,
                          c.DeptId,
                          Amount = -((c.PayVoucher * c.Discount) / 100),
                          Quantity = 0,
                          PayType = "代金券"
                      };

            if (rm.DeptId.HasValue)
            {
                q23 = q23.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q24 = from q in q23
                      group q by q.PayType into g
                      select new Report6Consume()
                      {
                          PayType = g.Key,
                          Amount = g.Sum(s => s.Amount),
                          Quantity = g.Sum(s => s.Quantity),
                          Count = g.Count()
                      };
            var q25 = q24.ToList();
            var q26 = q22.Union(q25);
            rm.result.MemberConsume = q26.ToList();
            #endregion
            #region 非会员消费
            var q3 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                     from clcsps in clcsp.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && clcs.ConsumeType == 1

                     select new
                     {
                         ConsumeListId = cl.Id,
                         ConsumeId = clcs.Id,
                         cl.DeptId,
                         Amount = cl.Amount * clcs.Discount / 100,
                         cl.Quantity,
                         PayType = clcsps.Name,
                     };

            if (rm.DeptId.HasValue)
            {
                q3 = q3.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q31 = from q in q3
                      group q by q.PayType into g
                      select new Report6Consume()
                      {
                          PayType = g.Key,
                          Amount = g.Sum(s => s.Amount),
                          Quantity = g.Sum(s => s.Quantity),
                          Count = g.Count()
                      };
            var q32 = q31.ToList();
            var q33 = from c in Uow.Consume.GetAll()

                      where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate && c.ConsumeType == 1
                      select new
                      {
                          ConsumeId = c.Id,
                          c.DeptId,
                          Amount = -((c.PayVoucher * c.Discount) / 100),
                          Quantity = 0,
                          PayType = "代金券"
                      };

            if (rm.DeptId.HasValue)
            {
                q33 = q33.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q34 = from q in q33
                      group q by q.PayType into g
                      select new Report6Consume()
                      {
                          PayType = g.Key,
                          Amount = g.Sum(s => s.Amount),
                          Quantity = g.Sum(s => s.Quantity),
                          Count = g.Count()
                      };
            var q35 = q34.ToList();
            var q36 = q32.Union(q35);
            rm.result.NoMemberConsume = q36.ToList();
            #endregion
            #region 办卡 q4
            var q4 = from c in Uow.Cards.GetAll()
                     where c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                     select new { c.DeptId, c.Id, Status = "办卡", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q4 = q4.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 挂失 q41
            var q41 = from c in Uow.Cards.GetAll()
                      where c.LossDate >= dtBeginDate && c.LossDate <= dtEndDate
                      select new { c.DeptId, c.Id, Status = "挂失", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q41 = q41.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 补卡 q42
            var q42 = from c in Uow.Cards.GetAll()
                      where c.AddDate >= dtBeginDate && c.AddDate <= dtEndDate
                      select new { c.DeptId, c.Id, Status = "补卡", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q42 = q42.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 正常在用 q43
            var q43 = from c in Uow.Cards.GetAll()
                      where c.Status == 0
                      select new { c.DeptId, c.Id, Status = "正常在用", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q43 = q43.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 积分 q51
            var q5 = from p in Uow.CardPoints.GetAll()
                     select new { p.Card, p.DeptId, p.Id, p.Point };
            if (rm.DeptId.HasValue)
            {
                q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q51 = from p in q5
                      group p by p.Card into g
                      select new { Card = g.Key, Point = g.Sum(s => s.Point) };

            #endregion
            #region 补卡费 q61
            var q6 = from p in Uow.Recharges.GetAll()
                     where p.RechargeType == 3 && p.CreateDate >= dtBeginDate && p.CreateDate <= dtEndDate
                     select new { p.Id, p.DeptId, p.Card, p.Amount };
            if (rm.DeptId.HasValue)
            {
                q6 = q6.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q61 = from r in q6
                      group r by r.Card into g
                      select new { Card = g.Key, Amount = g.Sum(s => s.Amount) };

            #endregion
            #region 办卡 q72
            var q7 = from q in q4
                     join p in q51 on q.Id equals p.Card into qp
                     from qps in qp.DefaultIfEmpty()

                     join r in q61 on q.Id equals r.Card into qr
                     from qrs in qr.DefaultIfEmpty()

                     select new
                     {
                         Id = Guid.NewGuid(),
                         q.Status,
                         q.Balance,
                         Point = qps.Point == null ? 0 : qps.Point,
                         Fee = qrs.Amount == null ? 0 : qrs.Amount,
                         Count = 1

                     };
            var q71 = from q in q7
                      group q by q.Status into g
                      select new Report6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            var q72 = q71.ToList();
            #endregion
            #region 挂失 q82
            var q8 = from q in q41
                     join p in q51 on q.Id equals p.Card into qp
                     from qps in qp.DefaultIfEmpty()

                     join r in q61 on q.Id equals r.Card into qr
                     from qrs in qr.DefaultIfEmpty()

                     select new
                     {
                         Id = Guid.NewGuid(),
                         q.Status,
                         q.Balance,
                         Point = qps.Point == null ? 0 : qps.Point,
                         Fee = qrs.Amount == null ? 0 : qrs.Amount,
                         Count = 1

                     };
            var q81 = from q in q8
                      group q by q.Status into g
                      select new Report6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            var q82 = q81.ToList();
            #endregion
            #region 补卡 q92
            var q9 = from q in q42
                     join p in q51 on q.Id equals p.Card into qp
                     from qps in qp.DefaultIfEmpty()

                     join r in q61 on q.Id equals r.Card into qr
                     from qrs in qr.DefaultIfEmpty()

                     select new
                     {
                         Id = Guid.NewGuid(),
                         q.Status,
                         q.Balance,
                         Point = qps.Point == null ? 0 : qps.Point,
                         Fee = qrs.Amount == null ? 0 : qrs.Amount,
                         Count = 1

                     };
            var q91 = from q in q9
                      group q by q.Status into g
                      select new Report6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            var q92 = q91.ToList();
            #endregion
            #region 正常在用 q102
            var q10 = from q in q43
                      join p in q51 on q.Id equals p.Card into qp
                      from qps in qp.DefaultIfEmpty()

                      join r in q61 on q.Id equals r.Card into qr
                      from qrs in qr.DefaultIfEmpty()

                      select new
                      {
                          Id = Guid.NewGuid(),
                          q.Status,
                          q.Balance,
                          Point = qps.Point == null ? 0 : qps.Point,
                          Fee = qrs.Amount == null ? 0 : qrs.Amount,
                          Count = 1

                      };
            var q101 = from q in q10
                       group q by q.Status into g
                       select new Report6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            var q102 = q101.ToList();
            #endregion
            var q110 = q72.Union(q82).Union(q92).Union(q102);
            rm.result.Card = q110.ToList();
            Session["Report6Model"] = rm;
            return RedirectToAction("Report6");
        }
        #endregion

        #region Report7 收银查询
        [Authorize]
        public ActionResult Report7()
        {
            if (Session["Report7Model"] != null)
            {
                Report7Model rm = Session["Report7Model"] as Report7Model;
                return View(rm);
            }
            else
            {
                Report7Model rm = new Report7Model();
                rm.result = new List<Report7Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report7(Report7Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);
            #region 会员消费
            var q1 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     where clcs.ConsumeType == 0 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         cl.UserId,
                         clds.DeptName,
                         cus.FullName,
                         Amount = cl.Amount * clcs.Discount / 100,
                         cl.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            }

            var q12 = from c in Uow.Consume.GetAll()
                      join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                      from cus in cu.DefaultIfEmpty()

                      join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                      from clds in cld.DefaultIfEmpty()
                      where c.PayVoucher > 0 && c.ConsumeType == 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                      select new
                      {
                          c.Id,
                          c.DeptId,
                          c.UserId,
                          clds.DeptName,
                          cus.FullName,
                          Amount = -(c.PayVoucher * c.Discount / 100),
                          c.CreateDate,
                      };
            if (rm.DeptId.HasValue)
            {
                q12 = q12.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q12 = q12.Where(w => w.UserId == rm.UserId.Value);
            }
            var q13 = q1.Union(q12).ToList();
            var q11 = from q in q13
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = 0,
                          SumConsume = q.Amount,
                          CardConsume = q.Amount,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0
                      };
            #endregion
            #region 非会员消费
            var q2 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     where clcs.ConsumeType == 1 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         cl.UserId,
                         clds.DeptName,
                         cus.FullName,
                         cl.Amount,
                         cl.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q2 = q2.Where(w => w.UserId == rm.UserId.Value);
            }
            var q22 = from c in Uow.Consume.GetAll()
                      join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                      from cus in cu.DefaultIfEmpty()

                      join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                      from clds in cld.DefaultIfEmpty()
                      where c.ConsumeType == 1 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                      select new
                      {
                          c.Id,
                          c.DeptId,
                          c.UserId,
                          clds.DeptName,
                          cus.FullName,
                          Amount = -(c.PayVoucher * c.Discount / 100),
                          c.CreateDate,
                      };
            if (rm.DeptId.HasValue)
            {
                q22 = q22.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q22 = q22.Where(w => w.UserId == rm.UserId.Value);
            }
            var q23 = q2.Union(q22).ToList();
            var q21 = from q in q23
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = q.Amount,
                          SumConsume = q.Amount,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0
                      };
            #endregion
            #region 办卡(张)
            var q4 = from c in Uow.Cards.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     where c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.UserId,
                         c.DeptId,
                         cds.DeptName,
                         cus.FullName,
                         Amount = 1,
                         c.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q4 = q4.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q4 = q4.Where(w => w.UserId == rm.UserId.Value);
            }
            var q41 = from q in q4.ToList()
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = q.Amount,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0
                      };
            #endregion
            #region 办卡(元)
            var q6 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 2
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         r.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q6 = q6.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q6 = q6.Where(w => w.UserId == rm.UserId.Value);
            }

            var q61 = from q in q6.ToList()
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = q.Amount,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = q.Amount,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0
                      };

            #endregion
            #region 补卡(张)
            var q7 = from c in Uow.Cards.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     where c.Status == 2 && c.AddDate >= dtBeginDate && c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.UserId,
                         c.DeptId,
                         cds.DeptName,
                         cus.FullName,
                         Amount = 1,
                         c.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q7 = q7.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q7 = q7.Where(w => w.UserId == rm.UserId.Value);
            }
            var q71 = from q in q7.ToList()
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = q.Amount,
                          AddCardSum = 0
                      };
            #endregion

            #region 补卡(元)
            var q8 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 3
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         r.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q8 = q8.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q8 = q8.Where(w => w.UserId == rm.UserId.Value);
            }

            var q81 = from q in q8.ToList()
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = q.Amount,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 1,
                          RechargeSum = q.Amount,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = q.Amount,
                      };
            #endregion
            #region 会员卡充值
            var q3 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 0
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         r.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q3 = q3.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q3 = q3.Where(w => w.UserId == rm.UserId.Value);
            }

            var q31 = from q in q3.ToList()
                      select new Report7Result()
                      {
                          Id = q.Id,
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = q.Amount,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 1,
                          RechargeSum = q.Amount,
                          SumCup = 0,
                          AvgPrice = 0,
                      };
            #endregion
            #region 总杯数
            var q5 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into cli
                     from clis in cli.DefaultIfEmpty()

                     join ic in Uow.InventoryCategory.GetAll() on clis.Category equals ic.Id into iic
                     from iics in iic.DefaultIfEmpty()

                     where iics.Code != "009" && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.DeptId,
                         cl.UserId,
                         clds.DeptName,
                         cus.FullName,
                         Amount = cl.Quantity,
                         cl.CreateDate,
                     };
            if (rm.DeptId.HasValue)
            {
                q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q5 = q5.Where(w => w.UserId == rm.UserId.Value);
            }
            var q51 = from q in q5.ToList()
                      select new Report7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = q.CreateDate.ToString("yyyy-MM-dd"),
                          DeptName = q.DeptName,
                          FullName = q.FullName,
                          SumCash = 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = q.Amount,
                          AvgPrice = 0,
                      };
            #endregion
            var qa = q11.Union(q21).Union(q31).Union(q41).Union(q51).Union(q61);

            var qa1 = from q in qa
                      group q by new { q.CreateDate, q.DeptName, q.FullName } into g
                      select new Report7Result()
                      {
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          SumCash = g.Sum(s => s.SumCash),
                          SumConsume = g.Sum(s => s.SumConsume),
                          CardConsume = g.Sum(s => s.CardConsume),
                          CardCount = g.Sum(s => s.CardCount),
                          CardRecharge = g.Sum(s => s.CardRecharge),
                          RechargeCount = g.Sum(s => s.RechargeCount),
                          RechargeSum = g.Sum(s => s.RechargeSum),
                          SumCup = g.Sum(s => s.SumCup),
                          AvgPrice = g.Sum(s => s.SumCup) > 0 ? Math.Round(g.Sum(s => s.SumConsume) / g.Sum(s => s.SumCup), 2) : 0,
                          AddCardCount = g.Sum(s => s.AddCardCount),
                          AddCardSum = g.Sum(s => s.AddCardSum),
                      };
            if (qa1.Count() > 0)
            {
                rm.AvgPrice = Math.Round(qa1.Average(a => a.AvgPrice), 2);
                rm.CardConsume = Math.Round(qa1.Sum(s => s.CardConsume), 2);
                rm.CardCount = qa1.Sum(s => s.CardCount);
                rm.CardRecharge = Math.Round(qa1.Sum(s => s.CardRecharge), 2);
                rm.RechargeCount = qa1.Sum(s => s.RechargeCount);
                rm.RechargeSum = Math.Round(qa1.Sum(s => s.RechargeSum), 2);
                rm.SumCash = Math.Round(qa1.Sum(s => s.SumCash), 2);
                rm.SumConsume = Math.Round(qa1.Sum(s => s.SumConsume), 2);
                rm.SumCup = qa1.Sum(s => s.SumCup);
                rm.AddCardCount = qa1.Sum(s => s.AddCardCount);
                rm.AddCardSum = Math.Round(qa1.Sum(s => s.AddCardSum), 2);
            }
            else
            {
                rm.AvgPrice = 0;
                rm.CardConsume = 0;
                rm.CardCount = 0;
                rm.CardRecharge = 0;
                rm.RechargeCount = 0;
                rm.RechargeSum = 0;
                rm.SumCash = 0;
                rm.SumConsume = 0;
                rm.SumCup = 0;
                rm.AddCardCount = 0;
                rm.AddCardSum = 0;
            }
            rm.result = qa1.OrderBy(o => o.CreateDate).ToList();
            Session["Report7Model"] = rm;
            return RedirectToAction("Report7");
        }
        public void Report7ExportToExcel()
        {
            if (Session["Report7Model"] == null) return;
            var m = Session["Report7Model"] as Report7Model;
            object dataSource = m.result;
            string fileName = "寻仙记各分店日常结账情况.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "CreateDate";
            field.HeaderText = "日期";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "店名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "收银员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumCash";
            field.HeaderText = "总现金收入";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumConsume";
            field.HeaderText = "实际消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardConsume";
            field.HeaderText = "刷卡消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardCount";
            field.HeaderText = "办卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardRecharge";
            field.HeaderText = "办卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RechargeCount";
            field.HeaderText = "充值（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RechargeSum";
            field.HeaderText = "充值（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumCup";
            field.HeaderText = "总杯数";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AvgPrice";
            field.HeaderText = "平均单价";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddCardCount";
            field.HeaderText = "补卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddCardSum";
            field.HeaderText = "补卡（元）";
            view.Columns.Add(field);

            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }

        #endregion

        #region Report8 收款差异查询
        [Authorize]
        public ActionResult Report8()
        {
            if (Session["Report8Model"] != null)
            {
                Report8Model rm = Session["Report8Model"] as Report8Model;
                return View(rm);
            }
            else
            {
                Report8Model rm = new Report8Model();
                rm.result = new List<Report8Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report8(Report8Model rm)
        {

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            var q1 = from q in Uow.CheckDifferences.GetAll()
                     join d in Uow.Depts.GetAll() on q.DeptId equals d.DeptId into qd
                     from qds in qd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on q.UserId equals u.UserId into qu
                     from qus in qu.DefaultIfEmpty()
                     where q.DifDate >= dtBeginDate && q.DifDate <= dtEndDate
                     select new
                     {
                         q.UserId,
                         q.DeptId,
                         qds.DeptName,
                         qus.FullName,
                         q.DifDate,
                         q.Amount,
                         q.More,
                         q.Less,
                         MoreDay = q.More > 0 ? 1 : 0,
                         LessDay = q.Less > 0 ? 1 : 0,
                         NormalDay = q.More == 0 && q.Less == 0 ? 1 : 0,
                         q.Comment,
                     };
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            var q3 = q1.ToList();
            if (q3.Count > 0)
            {
                var d1 = (from q in q3 select q.DifDate).Max();
                var d2 = (from q in q3 select q.DifDate).Min();

                TimeSpan ts = d1 - d2;
                int iday = 1;
                if (ts.Days > 0)
                {
                    iday = ts.Days + 1;
                }
                var q2 = from q in q3
                         group q by q.FullName into g
                         select new Report8Result()
                         {
                             FullName = g.Key,
                             Amount = g.Sum(s => s.Amount),
                             Less = g.Sum(s => s.Less),
                             LessDays = g.Sum(s => s.LessDay),
                             LessRatio = g.Sum(s => s.Less) / g.Sum(s => s.Amount),
                             More = g.Sum(s => s.More),
                             MoreDays = g.Sum(s => s.MoreDay),
                             MoreRatio = g.Sum(s => s.More) / g.Sum(s => s.Amount),
                             NormalDays = g.Sum(s => s.NormalDay),
                             NormalRatio = Convert.ToDecimal(g.Sum(s => s.NormalDay)) / Convert.ToDecimal(iday),
                             Comment = (string)g.Aggregate("", (a, b) => (a == "" ? "" : a + "，") + b.Comment),

                         };
                var q4 = q2.ToList();
                if (q4.Count > 0)
                {
                    rm.Amount = q4.Sum(s => s.Amount);
                    rm.More = q4.Sum(s => s.More);
                    rm.Less = q4.Sum(s => s.Less);
                    rm.MoreDays = q4.Sum(s => s.MoreDays);
                    rm.LessDays = q4.Sum(s => s.LessDays);
                }
                rm.result = q4;
            }
            Session["Report8Model"] = rm;
            return RedirectToAction("Report8");
        }
        public void Report8ExportToExcel()
        {
            if (Session["Report8Model"] == null) return;
            var m = Session["Report8Model"] as Report8Model;
            object dataSource = m.result;
            string fileName = "收款差异查询.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "收银员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "应收总金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MoreDays";
            field.HeaderText = "长款天数";
            view.Columns.Add(field);



            field = new BoundField();
            field.DataField = "More";
            field.HeaderText = "长款金额";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LessDays";
            field.HeaderText = "短款天数";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Less";
            field.HeaderText = "短款金额";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MoreRatio";
            field.HeaderText = "长款金额比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "LessRatio";
            field.HeaderText = "短款金额比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "NormalRatio";
            field.HeaderText = "正常比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Comment";
            field.HeaderText = "情况说明";
            view.Columns.Add(field);

            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report9 销售曲线图
        [Authorize]
        public ActionResult Report9(string year, string month, bool? IsCup)
        {
            Report9Model r9 = new Report9Model();
            r9.year = year;
            r9.month = month;
            if (IsCup.HasValue) r9.IsCup = IsCup.Value;
            else
                r9.IsCup = false;
            return View(r9);
        }
        public void MyChart(string year, string month, bool IsCup)
        {
            if (string.IsNullOrEmpty(year))
            {
                var chart2 = new Chart(900, 400, ChartTheme.Blue);
                chart2.AddTitle(year + "年" + month + "月" + "各分店销售额日走势（元）");
                chart2.Write("png");
                return;
            }
            if (!IsCup)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    int iy = Convert.ToInt32(year);
                    int im = Convert.ToInt32(month);
                    var q = from c in Uow.Consume.GetAll().Where(w => w.CreateDate.Year == iy).Where(w => w.CreateDate.Month == im)
                            join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                            from cds in cd.DefaultIfEmpty()
                            select new { c.Amount, cds.DeptCode, cds.DeptName, c.CreateDate.Day };
                    var q1 = q.OrderBy(o => o.DeptCode).OrderBy(o => o.Day).ToList();
                    var q2 = from c in q1
                             select new { c.Amount, c.DeptCode, c.DeptName, c.Day };
                    var q3 = from qs in q2
                             group qs by new { qs.DeptName, qs.DeptCode, qs.Day } into g
                             select new { g.Key.DeptName, g.Key.DeptCode, g.Key.Day, Amount = g.Sum(s => s.Amount) };

                    var q4 = from qs in q3
                             group qs by new { qs.DeptCode, qs.DeptName } into g
                             select new { g.Key.DeptCode, g.Key.DeptName };

                    var chart = new Chart(900, 400, ChartTheme.Blue);
                    chart.AddTitle(year + "年" + month + "月" + "各分店销售额日走势（元）");

                    chart.AddLegend("门店", "dept");
                    foreach (var ie in q4)
                    {
                        int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Day).ToArray();
                        decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
                        chart.AddSeries(
                        name: ie.DeptName,
                        xValue: strd,
                        markerStep: 1,
                        yValues: stra,
                        chartType: "Line",
                        legend: "dept"
                        );
                    }
                    chart.Write("png");
                }
                else
                {
                    int iy = Convert.ToInt32(year);
                    var q = from c in Uow.Consume.GetAll().Where(w => w.CreateDate.Year == iy)
                            join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                            from cds in cd.DefaultIfEmpty()
                            select new { c.Amount, cds.DeptCode, cds.DeptName, c.CreateDate.Month };
                    var q1 = q.OrderBy(o => o.DeptCode).OrderBy(o => o.Month).ToList();
                    var q2 = from c in q1
                             select new { c.Amount, c.DeptCode, c.DeptName, c.Month };
                    var q3 = from qs in q2
                             group qs by new { qs.DeptName, qs.DeptCode, qs.Month } into g
                             select new { g.Key.DeptName, g.Key.DeptCode, g.Key.Month, Amount = g.Sum(s => s.Amount) };

                    var q4 = from qs in q3
                             group qs by new { qs.DeptCode, qs.DeptName } into g
                             select new { g.Key.DeptCode, g.Key.DeptName };

                    var chart = new Chart(900, 400, ChartTheme.Blue);
                    chart.AddTitle(year + "年" + "各分店销售额月走势（元）");

                    chart.AddLegend("门店", "dept");
                    foreach (var ie in q4)
                    {
                        int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Month).ToArray();
                        decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
                        chart.AddSeries(
                        name: ie.DeptName,
                        xValue: strd,
                        markerStep: 1,
                        yValues: stra,
                        chartType: "Line",
                        legend: "dept"
                        );
                    }
                    chart.Write("png");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(month))
                {
                    int iy = Convert.ToInt32(year);
                    int im = Convert.ToInt32(month);
                    var q = from c in Uow.ConsumeList.GetAll().Where(w => w.CreateDate.Year == iy).Where(w => w.CreateDate.Month == im)
                            join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                            from cds in cd.DefaultIfEmpty()
                            select new { Amount = c.Quantity, cds.DeptCode, cds.DeptName, c.CreateDate.Day };
                    var q1 = q.OrderBy(o => o.DeptCode).OrderBy(o => o.Day).ToList();
                    var q2 = from c in q1
                             select new { c.Amount, c.DeptCode, c.DeptName, c.Day };
                    var q3 = from qs in q2
                             group qs by new { qs.DeptName, qs.DeptCode, qs.Day } into g
                             select new { g.Key.DeptName, g.Key.DeptCode, g.Key.Day, Amount = g.Sum(s => s.Amount) };

                    var q4 = from qs in q3
                             group qs by new { qs.DeptCode, qs.DeptName } into g
                             select new { g.Key.DeptCode, g.Key.DeptName };

                    var chart = new Chart(900, 400, ChartTheme.Blue);
                    chart.AddTitle(year + "年" + month + "月" + "各分店销售杯数日走势");

                    chart.AddLegend("门店", "dept");
                    foreach (var ie in q4)
                    {
                        int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Day).ToArray();
                        decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
                        chart.AddSeries(
                        name: ie.DeptName,
                        xValue: strd,
                        markerStep: 1,
                        yValues: stra,
                        chartType: "Line",
                        legend: "dept"
                        );
                    }
                    chart.Write("png");
                }
                else
                {
                    int iy = Convert.ToInt32(year);
                    var q = from c in Uow.ConsumeList.GetAll().Where(w => w.CreateDate.Year == iy)
                            join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                            from cds in cd.DefaultIfEmpty()
                            select new { Amount = c.Quantity, cds.DeptCode, cds.DeptName, c.CreateDate.Month };
                    var q1 = q.OrderBy(o => o.DeptCode).OrderBy(o => o.Month).ToList();
                    var q2 = from c in q1
                             select new { c.Amount, c.DeptCode, c.DeptName, c.Month };
                    var q3 = from qs in q2
                             group qs by new { qs.DeptName, qs.DeptCode, qs.Month } into g
                             select new { g.Key.DeptName, g.Key.DeptCode, g.Key.Month, Amount = g.Sum(s => s.Amount) };

                    var q4 = from qs in q3
                             group qs by new { qs.DeptCode, qs.DeptName } into g
                             select new { g.Key.DeptCode, g.Key.DeptName };

                    var chart = new Chart(900, 400, ChartTheme.Blue);
                    chart.AddTitle(year + "年" + "各分店销售杯数月走势");

                    chart.AddLegend("门店", "dept");
                    foreach (var ie in q4)
                    {
                        int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Month).ToArray();
                        decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
                        chart.AddSeries(
                        name: ie.DeptName,
                        xValue: strd,
                        markerStep: 1,
                        yValues: stra,
                        chartType: "Line",
                        legend: "dept"
                        );
                    }
                    chart.Write("png");
                }
            }
        }
        #endregion

        #region Report12 长短款设置
        [Authorize]
        public ActionResult Report12()
        {
            if (Session["Report12Model"] != null)
            {
                Report12Model rm = Session["Report12Model"] as Report12Model;
                return View(rm);
            }
            else
            {
                Report12Model rm = new Report12Model();
                rm.result = new List<Report12Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report12(string button, Report12Model rm)
        {
            if (button == "修改")
            {
                if (rm.curResult != null)
                {
                    if (rm.curResult.IsIn == 1)
                    {
                        ModelState.AddModelError("", "不能修改长短款设置");
                        return View(rm);
                    }
                    MembershipUser user = Membership.GetUser();
                    Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                    var dept = Uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == userId).FirstOrDefault();
                    if (dept == null || !dept.DeptId.HasValue || dept.DeptId == Guid.Empty)
                    {
                        ModelState.AddModelError("", "请设置操作员部门信息");
                        return View(rm);
                    }
                    DXInfo.Models.CheckDifferences cd = new DXInfo.Models.CheckDifferences();
                    cd.Amount = rm.curResult.Amount;
                    cd.Comment = rm.curResult.Comment;
                    cd.CreateDate = DateTime.Now;
                    cd.DeptId = rm.curResult.DeptId;
                    cd.DifDate = rm.curResult.DifDate;
                    cd.Less = rm.curResult.Less;
                    cd.More = rm.curResult.More;
                    cd.OperDeptId = userId;
                    cd.OperUserId = dept.DeptId.Value;
                    cd.UserId = rm.curResult.UserId;

                    Uow.CheckDifferences.Add(cd);
                    Uow.Commit();// SaveChanges();
                }
            }
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);//.AddDays(1);

            var q1 = from cl in Uow.ConsumeList.GetAll()
                     join cm in Uow.Consume.GetAll() on cl.Consume equals cm.Id into clcm
                     from clcms in clcm.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()


                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate

                     select new Report121Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         CreateDate = cl.CreateDate,
                         Amount = cl.Amount * clcms.Discount / 100
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);

            Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            Guid gInventory = Guid.Parse("17516BE0-CCF2-4A58-BF2D-8C762BD8A8C4");
            var q2 = from cl in Uow.Consume.GetAll()
                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     where cl.PayVoucher > 0 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new Report121Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = cds.DeptName,
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         CreateDate = cl.CreateDate,
                         Amount = -((cl.PayVoucher * cl.Discount) / 100),
                     };
            if (rm.DeptId.HasValue) q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q2 = q2.Where(w => w.UserId == rm.UserId.Value);

            var q5 = from r in Uow.Recharges.GetAll()
                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()
                     where r.RechargeType != 1 && r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate
                     select new Report121Result()
                     {
                         Id = r.Id,
                         DeptId = r.DeptId,
                         DeptName = cds.DeptName,
                         UserId = r.UserId,
                         FullName = clus.FullName,
                         CreateDate = r.CreateDate,
                         Amount = r.Amount
                     };
            if (rm.DeptId.HasValue) q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q5 = q5.Where(w => w.UserId == rm.UserId.Value);

            var q3 = q1.Union(q2).Union(q5).ToList();


            var q4 = from q in q3
                     group q by new { CreateDate = q.CreateDate.ToString("yyyy-MM-dd"), q.DeptId, q.DeptName, q.UserId, q.FullName } into g
                     select new Report12Result()
                     {
                         DifDate = Convert.ToDateTime(g.Key.CreateDate),
                         DeptId = g.Key.DeptId,
                         DeptName = g.Key.DeptName,
                         UserId = g.Key.UserId,
                         FullName = g.Key.FullName,
                         Amount = g.Sum(s => s.Amount),
                         IsIn = 0,
                     };
            var q6 = from q in Uow.CheckDifferences.GetAll()
                     join d in Uow.Depts.GetAll() on q.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on q.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     where q.DifDate == dtBeginDate
                     select new Report12Result()
                     {
                         Id = q.Id,
                         DeptId = q.DeptId,
                         DeptName = cds.DeptName,
                         UserId = q.UserId,
                         FullName = clus.FullName,
                         Amount = q.Amount,
                         More = q.More,
                         Less = q.Less,
                         DifDate = q.DifDate,
                         Comment = q.Comment,
                         IsIn = 1,
                     };
            var q7 = q4.ToList();
            var q8 = q6.ToList();
            foreach (Report12Result r12 in q8)
            {
                q7.RemoveAll(delegate(Report12Result r121) { return r121.DifDate == r12.DifDate && r121.DeptId == r12.DeptId && r121.UserId == r12.UserId; });
            }
            var q9 = q7.Union(q8).ToList();
            rm.result = q9;
            Session["Report12Model"] = rm;
            return RedirectToAction("Report12");
        }
        #endregion

        #region Report13 时段销量统计
        [Authorize]
        public ActionResult Report13(Guid? deptId, Guid? userId, Guid? category, Guid? inventory, DateTime? beginDate, DateTime? endDate)
        {
            Report13Model r13 = new Report13Model();
            r13.deptId = deptId;
            r13.userId = userId;
            r13.category = category;
            r13.inventory = inventory;
            r13.beginDate = beginDate.HasValue ? beginDate.Value : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            r13.endDate = endDate.HasValue ? endDate.Value : Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

            return View(r13);
        }
        public void MyChart13(Guid? deptId, Guid? userId, Guid? category, Guid? inventory, DateTime beginDate, DateTime endDate)
        {

            var q = from c in Uow.ConsumeList.GetAll()
                    join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()
                    join y in Uow.Inventory.GetAll() on c.Inventory equals y.Id into cy
                    from cys in cy.DefaultIfEmpty()

                    where c.CreateDate >= beginDate && c.CreateDate <= endDate
                    select new { Amount = c.Quantity, cds.DeptCode, cds.DeptName, c.CreateDate.Hour, c.DeptId, c.UserId, Category = cys.Category, c.Inventory };
            if (deptId.HasValue) q = q.Where(w => w.DeptId == deptId.Value);
            if (userId.HasValue) q = q.Where(w => w.UserId == userId.Value);
            if (category.HasValue) q = q.Where(w => w.Category == category.Value);
            if (inventory.HasValue) q = q.Where(w => w.Inventory == inventory.Value);

            var q1 = from r in q.ToList()
                     select new { r.Amount, r.DeptCode, r.DeptName, Day = r.Hour };

            var q2 = from c in q1
                     select new { c.Amount, c.DeptCode, c.DeptName, c.Day };
            var q3 = from qs in q2
                     group qs by new { qs.DeptName, qs.DeptCode, qs.Day } into g
                     select new { g.Key.DeptName, g.Key.DeptCode, g.Key.Day, Amount = g.Sum(s => s.Amount) };

            var q4 = from qs in q3
                     group qs by new { qs.DeptCode, qs.DeptName } into g
                     select new { g.Key.DeptCode, g.Key.DeptName };

            var chart = new Chart(900, 400, ChartTheme.Blue);
            chart.AddTitle("时段销量统计");

            chart.AddLegend("门店", "dept");
            foreach (var ie in q4)
            {
                int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Day).ToArray();
                decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
                chart.AddSeries(
                name: ie.DeptName,
                xValue: strd,
                markerStep: 1,
                yValues: stra,
                chartType: "column",
                legend: "dept"
                );
            }
            chart.Write("png");
        }
        #endregion

        #endregion
       
        #region Report2 消费明细查询
        private void SetupReport2GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Report2_RequestData");
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(Report2Grid_DataResolved);
            grid.ExcelExportSettings.Url = Url.Action("Report2_RequestData");

            JQGridColumn cupTypeNameCol = grid.Columns.Find(f => f.DataField == "CupName");
            if (cupTypeNameCol != null)
            {
                cupTypeNameCol.Visible = this.cupTypeColumnVisible;
            }

            JQGridColumn unitCol = grid.Columns.Find(f => f.DataField == "UnitOfMeasureName");
            if (unitCol != null)
            {
                unitCol.Visible = this.unitOfMeasureColumnVisibility;
            }

            JQGridColumn operatorsOnDutyCol = grid.Columns.Find(f => f.DataField == "OperatorsOnDuty");
            if (operatorsOnDutyCol != null)
            {
                operatorsOnDutyCol.Visible = this.isOperatorsOnDuty;
            }
        }
        void Report2Grid_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal quantity = 0;
            decimal sum = 0;
            decimal amount = 0;
            foreach (dynamic q in e.FilterData)
            {
                //if(q.Quantity!=null)
                quantity += q.Quantity;
                sum += q.Sum;
                amount += q.Amount;
            }
            JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
            quantityColumn.FooterValue = quantity.ToString();
            JQGridColumn sumColumn = e.GridModel.Columns.Find(c => c.DataField == "Sum");
            sumColumn.FooterValue = sum.ToString();
            JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            amountColumn.FooterValue = amount.ToString();
        }
        [Authorize]
        public ActionResult Report2()
        {
            var gridModel = new Report2GridModel();
            string strInvType = this.Request["InvType"];
            int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
            if (!string.IsNullOrEmpty(strInvType))
            {
                invType = Convert.ToInt32(strInvType);
            }
            gridModel.InvType = invType;
            SetupReport2GridModel(gridModel.Report2Grid);
            return View(gridModel);
        }
        [Authorize]
        public ActionResult Report2_RequestData()
        {
            var gridModel = new Report2GridModel();
            SetupReport2GridModel(gridModel.Report2Grid);
            Guid gEmpty = Guid.Empty;
            Guid gcardPayType = Guid.Parse("589DE614-4779-45E6-ADC6-ABF94E55AF17");
            Guid gpointPayType = Guid.Parse("F4D343D6-236C-4796-9DA4-016C22427F86");
            JQGridState gridState = gridModel.Report2Grid.GetState();
            //int invtype = (int)DXInfo.Models.InvType.ColdDrinkShop;
            int deptType = (int)DXInfo.Models.DeptType.Sale;
            if (gridState.QueryString.AllKeys.Contains("filters"))
            {
                var q = from cl in Uow.ConsumeList.GetAll()
                        join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                        from clcs in clc.DefaultIfEmpty()

                        join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                        from cps in cp.DefaultIfEmpty()

                        join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                        from ccards in ccard.DefaultIfEmpty()

                        join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                        from cms in cm.DefaultIfEmpty()

                        join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                        from cis in ci.DefaultIfEmpty()

                        join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                        from iics in iic.DefaultIfEmpty()

                        join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                        from cus in cu.DefaultIfEmpty()

                        join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                        from cds in cd.DefaultIfEmpty()

                        //join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
                        //from ods in od.DefaultIfEmpty()

                        join d1 in Uow.Depts.GetAll() on ccards.DeptId equals d1.DeptId into cd1
                        from cd1s in cd1.DefaultIfEmpty()

                        join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "CupType") on SqlFunctions.StringConvert((double?)cl.Cup).Trim() equals d2.Code into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        join d3 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d3.Code into dd3
                        from dd3s in dd3.DefaultIfEmpty()

                        join d4 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d4.Id into dd4
                        from dd4s in dd4.DefaultIfEmpty()

                        where cds.DeptType == deptType//ods.Code == "002"cis.InvType == invtype && 

                        select new
                        {
                            cl.Id,
                            LocalDeptId = ccards == null ? gEmpty : ccards.DeptId,
                            LocalDeptName = cd1s.DeptName,
                            cl.DeptId,
                            DeptName = cds.DeptName,
                            cl.UserId,
                            FullName = cus.FullName,
                            clcs.OperatorsOnDuty,
                            ConsumeType = clcs.ConsumeType==null?-1:clcs.ConsumeType,
                            ConsumeTypeName = dd3s.Name,
                            PayType = clcs.ConsumeType==null?gEmpty:clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : cps.Id,
                            PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                            CardNo = ccards.CardNo,
                            MemberName = cms.MemberName,
                            cis.Category,
                            CategoryName = iics.Name,
                            InventoryId = cl.Inventory,
                            InventoryName = cis.Name,
                            Cup = cl.Cup,
                            CupName = dd2s.Name,
                            Price = cl.Price,
                            Quantity = cl.Quantity,
                            Sum = cl.Sum,
                            Discount = cl.Discount,
                            Amount = cl.Amount,
                            CreateDate = cl.CreateDate,
                            UnitOfMeasureName=dd4s.Name,
                            cis.InvType
                        };
                if (gridModel.Report2Grid.AjaxCallBackMode == AjaxCallBackMode.Excel)
                {
                    gridModel.Report2Grid.ExportToExcel(q, "消费明细查询.xls", gridState);
                    return View();
                }
                else
                {
                    return gridModel.Report2Grid.DataBind(q);
                }
            }
            else
            {
                List<object> lo = new List<object>();
                var records = new
                {
                    Id = "",
                    LocalDeptId = "",
                    LocalDeptName = "",
                    DeptId = "",
                    DeptName = "",
                    UserId = "",
                    FullName = "",
                    OperatorsOnDuty="",
                    ConsumeType = "",
                    ConsumeTypeName = "",
                    PayType = "",
                    PayTypeName = "",
                    CardNo = "",
                    MemberName = "",
                    Category = "",
                    CategoryName = "",
                    InventoryId = "",
                    InventoryName = "",
                    Cup = "",
                    CupName = "",
                    Price = "",
                    Quantity = 0,
                    Sum = 0,
                    Discount = "",
                    Amount = 0,
                    CreateDate = "",
                    UnitOfMeasureName="",
                    InvType=0,
                };
                lo.Add(records);
                var list = Enumerable.Repeat(records, 1).ToList();
                return gridModel.Report2Grid.DataBind(list.AsQueryable());
            }
        }

        #endregion
        
        #region Report4 消费分类统计
        [Authorize]
        public ActionResult Report4()
        {
            if (Session["Report4Model"] != null)
            {
                Report4Model rm = Session["Report4Model"] as Report4Model;
                return View(rm);
            }
            else
            {
                Report4Model rm = new Report4Model();

                rm.result = new List<Report4Result>().AsQueryable();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.IsDept = true;
                rm.IsConsumeType = true;
                rm.IsPayType = true;
                rm.IsCategory = true;
                rm.IsInventory = true;
                rm.IsCupType = true;

                string strInvType = this.Request["InvType"];
                int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strInvType))
                {
                    invType = Convert.ToInt32(strInvType);
                }
                rm.InvType = invType;

                string strCategoryType = this.Request["CategoryType"];
                int categoryType = (int)DXInfo.Models.CategoryType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strCategoryType))
                {
                    categoryType = Convert.ToInt32(strCategoryType);
                }
                rm.CategoryType = categoryType;

                string strDeptType = this.Request["DeptType"];
                int deptType = (int)DXInfo.Models.DeptType.Sale;
                if (!string.IsNullOrEmpty(strDeptType))
                {
                    deptType = Convert.ToInt32(strDeptType);
                }
                rm.DeptType = deptType;
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report4(Report4Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            Guid gcardPayType = Guid.Parse("589DE614-4779-45E6-ADC6-ABF94E55AF17");
            Guid gpointPayType = Guid.Parse("F4D343D6-236C-4796-9DA4-016C22427F86");
            int invtype = rm.InvType;//(int)DXInfo.Models.InvType.ColdDrinkShop;
            int deptType = rm.DeptType;//(int)DXInfo.Models.DeptType.Sale;
            int categoryType = rm.CategoryType;//(int)DXInfo.Models.CategoryType.ColdDrinkShop;

            var q = from cl in Uow.ConsumeList.GetAll()
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                    from clds in cld.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                    from clcsps in clcsp.DefaultIfEmpty()

                    join d1 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "CupType") on SqlFunctions.StringConvert((double?)cl.Cup).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d3.Code into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && cis.InvType == invtype &&
                    clds.DeptType == deptType &&
                    iics.CategoryType == categoryType

                    select new
                    {
                        cl.DeptId,
                        cl.UserId,
                        Inventory = cis.Id,
                        Category = iics.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Amount = cl.Amount * clcs.Discount / 100,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        CupType = dd2s.Name,
                        iCupType = cl.Cup,
                        ConsumeType = dd3s.Name,
                        clds.DeptName,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : clcsps.Id,
                        UnitOfMeasureName = dd1s.Name,
                    };

            if (rm.DeptId.HasValue)
            {
                q = q.Where(w => w.DeptId == rm.DeptId.Value);
            }

            if (rm.ConsumeType.HasValue)
            {
                q = q.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            }
            if (rm.PayType.HasValue)
            {
                q = q.Where(w => w.gPayType == rm.PayType.Value);
            }
            if (rm.Category.HasValue)
            {
                q = q.Where(w => w.Category == rm.Category.Value);
            }
            if (rm.Inventory.HasValue)
            {
                q = q.Where(w => w.Inventory == rm.Inventory.Value);
            }
            if (rm.CupType.HasValue)
            {
                q = q.Where(w => w.iCupType == rm.CupType.Value);
            }
            //Guid gvoucherPayType = Guid.Parse("988BF058-9FE5-41C4-8575-2DD8F73DB8F2");
            //Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            //Guid gInventory = Guid.Parse("17516BE0-CCF2-4A58-BF2D-8C762BD8A8C4");

            //var q2 = from c in Uow.Consume.GetAll()

            //         join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
            //         from cds in cd.DefaultIfEmpty()

            //         join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)c.ConsumeType).Trim() equals d1.Code into dd1
            //         from dd1s in dd1.DefaultIfEmpty()

            //         where c.PayVoucher > 0 && 
            //         c.CreateDate >= dtBeginDate && 
            //         c.CreateDate <= dtEndDate && 
            //         cds.DeptType==deptType
            //         select new
            //         {
            //             c.DeptId,
            //             c.UserId,
            //             Category = gCategory,
            //             Inventory = gInventory,
            //             InventoryName = "代金券",
            //             CategoryName = "代金券",
            //             DeptName = cds.DeptName,
            //             Discount = c.Discount,
            //             Amount = -((c.PayVoucher * c.Discount) / 100),
            //             CreateDate = c.CreateDate,
            //             Quantity = 0,
            //             CupType = "标准杯",
            //             iCupType=-1,
            //             iConsumeType = c.ConsumeType,
            //             ConsumeType = dd1s.Name,
            //             PayType = "代金券",
            //             gPayType = gvoucherPayType,
            //             UnitOfMeasureName=""
            //         };

            //if (rm.DeptId.HasValue)
            //{
            //    q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            //}
            //if (rm.ConsumeType.HasValue)
            //{
            //    q2 = q2.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            //}
            //if (rm.PayType.HasValue)
            //{
            //    q2 = q2.Where(w => w.gPayType == rm.PayType.Value);
            //}
            //if (rm.Category.HasValue)
            //{
            //    q2 = q2.Where(w => w.Category == rm.Category.Value);
            //}
            //if (rm.Inventory.HasValue)
            //{
            //    q2 = q2.Where(w => w.Inventory == rm.Inventory.Value);
            //}
            //if (rm.CupType.HasValue)
            //{
            //    q2 = q2.Where(w => w.iCupType == rm.CupType.Value);
            //}
            var q11 = from c in q
                      select new Report4Result()
                      {
                          Id = Guid.NewGuid(),
                          DeptName = c.DeptName,
                          CategoryName = c.CategoryName,
                          InventoryName = c.InventoryName,
                          ConsumeType = c.ConsumeType,
                          PayType = c.PayType,
                          CupType = c.CupType,
                          Amount = c.Amount,
                          Quantity = c.Quantity,
                          UnitOfMeasureName = c.UnitOfMeasureName,
                      };
            //var q21 = from c in q2
            //          select new Report4Result()
            //          {
            //              Id = Guid.NewGuid(),
            //              DeptName = c.DeptName,
            //              CategoryName = c.CategoryName,
            //              InventoryName = c.InventoryName,
            //              ConsumeType = c.ConsumeType,
            //              PayType = c.PayType,
            //              CupType = c.CupType,
            //              Amount = c.Amount,
            //              Quantity = c.Quantity,
            //              UnitOfMeasureName=c.UnitOfMeasureName,
            //          };
            //var q3 = q11.Union(q21);


            var q41 = from c in q11.ToList()
                      group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.UnitOfMeasureName }
                          into g
                          select new Report4Result()
                          {
                              Id = Guid.NewGuid(),
                              DeptName = g.Key.DeptName,
                              CategoryName = g.Key.CategoryName,
                              InventoryName = g.Key.InventoryName,
                              ConsumeType = g.Key.ConsumeType,
                              PayType = g.Key.PayType,
                              CupType = g.Key.CupType,
                              Amount = g.Sum(s => s.Amount),
                              Quantity = g.Sum(s => s.Quantity),
                              UnitOfMeasureName = g.Key.UnitOfMeasureName,
                          };
            var q4 = q41.ToList().AsQueryable();
            if (!rm.IsDept)
            {
                q4 = from c in q4
                     group c by new { c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = "",
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsConsumeType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.PayType, c.CupType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = "",
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsPayType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.CupType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = "",
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsCategory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = "",
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsInventory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.ConsumeType, c.PayType, c.CupType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = "",
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsCupType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.UnitOfMeasureName }
                         into g
                         select new Report4Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = "",
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             UnitOfMeasureName = g.Key.UnitOfMeasureName,
                         };
            }
            if (q4.Count() > 0)
            {
                rm.Amount = Math.Round(q4.Sum(s => s.Amount), 2);
                rm.Quantity = Math.Round(q4.Sum(s => s.Quantity), 2);
            }
            else
            {
                rm.Amount = 0;
                rm.Quantity = 0;
            }
            rm.result = q4.OrderBy(o => o.Quantity);
            Session["Report4Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invtype);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("Report4", routeValues);
        }
        public void Report4ExportToExcel()
        {
            if (Session["Report4Model"] == null) return;
            var m = Session["Report4Model"] as Report4Model;
            object dataSource = m.result;
            string fileName = "消费分类统计.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CategoryName";
            field.HeaderText = "分类";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "InventoryName";
            field.HeaderText = "商品";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "ConsumeType";
            field.HeaderText = "消费类型";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view.Columns.Add(field);

            if (cupTypeColumnVisible)
            {
                field = new BoundField();
                field.DataField = "CupType";
                field.HeaderText = "杯型";
                view.Columns.Add(field);
            }
            if (unitOfMeasureColumnVisibility)
            {
                field = new BoundField();
                field.DataField = "UnitOfMeasureName";
                field.HeaderText = "单位";
                view.Columns.Add(field);
            }
            
            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "数量";
            view.Columns.Add(field);

            
            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }

        
        #endregion

        #region Report5 销售排名统计
        [Authorize]
        public ActionResult Report5()
        {
            if (Session["Report5Model"] != null)
            {
                Report5Model rm = Session["Report5Model"] as Report5Model;
                return View(rm);
            }
            else
            {
                Report5Model rm = new Report5Model();                
                rm.result = new List<Report5Result>().AsQueryable();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.IsDept = true;
                rm.IsConsumeType = true;
                rm.IsPayType = true;
                rm.IsCategory = true;
                rm.IsInventory = true;
                rm.IsCupType = true;
                rm.IsCard = true;
                string strInvType = this.Request["InvType"];
                int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strInvType))
                {
                    invType = Convert.ToInt32(strInvType);
                }
                rm.InvType = invType;

                string strCategoryType = this.Request["CategoryType"];
                int categoryType = (int)DXInfo.Models.CategoryType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strCategoryType))
                {
                    categoryType = Convert.ToInt32(strCategoryType);
                }
                rm.CategoryType = categoryType;
                string strDeptType = this.Request["DeptType"];
                int deptType = (int)DXInfo.Models.DeptType.Sale;
                if (!string.IsNullOrEmpty(strDeptType))
                {
                    deptType = Convert.ToInt32(strDeptType);
                }
                rm.DeptType = deptType;
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report5(Report5Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            Guid gcardPayType = Guid.Parse("589DE614-4779-45E6-ADC6-ABF94E55AF17");
            Guid gpointPayType = Guid.Parse("F4D343D6-236C-4796-9DA4-016C22427F86");
            int invType = rm.InvType;//(int)DXInfo.Models.InvType.ColdDrinkShop;
            int categoryType = rm.CategoryType;//(int)DXInfo.Models.CategoryType.ColdDrinkShop;
            int deptType = rm.DeptType;//(int)DXInfo.Models.DeptType.Sale;

            var q = from cl in Uow.ConsumeList.GetAll()
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                    from clds in cld.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                    from clcsps in clcsp.DefaultIfEmpty()

                    join cd in Uow.Cards.GetAll() on clcs.Card equals cd.Id into clcscd
                    from clcscds in clcscd.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on clcscds.Member equals m.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    //join o in Uow.Organizations.GetAll() on clds.OrganizationId equals o.Id into od
                    //from ods in od.DefaultIfEmpty()
                    join d1 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w=>w.Type=="CupType") on SqlFunctions.StringConvert((double?)cl.Cup).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.NameCode.GetAll().Where(w=>w.Type=="ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d3.Code into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && 
                    cis.InvType == invType &&
                    iics.CategoryType == categoryType &&
                    clds.DeptType == deptType//ods.Code == "002"

                    select new
                    {
                        cl.DeptId,
                        cl.UserId,
                        Inventory = cis.Id,
                        Category = iics.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Amount = cl.Amount * clcs.Discount / 100,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        CupType = dd2s.Name,//cl.Cup == -1 ? "标准杯" : cl.Cup == 0 ? "大杯" : cl.Cup == 1 ? "中杯" : "小杯",
                        iCupType=cl.Cup,
                        ConsumeType = dd3s.Name,//clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : "会员积分兑换",
                        clds.DeptName,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : clcsps.Id,
                        clcscds.CardNo,
                        cms.MemberName,
                        UnitOfMeasureName=dd1s.Name,
                    };

            if (rm.DeptId.HasValue)
            {
                q = q.Where(w => w.DeptId == rm.DeptId.Value);
            }

            if (rm.ConsumeType.HasValue)
            {
                q = q.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            }
            if (rm.PayType.HasValue)
            {
                q = q.Where(w => w.gPayType == rm.PayType.Value);
            }
            if (rm.Category.HasValue)
            {
                q = q.Where(w => w.Category == rm.Category.Value);
            }
            if (rm.Inventory.HasValue)
            {
                q = q.Where(w => w.Inventory == rm.Inventory.Value);
            }
            if (!string.IsNullOrEmpty(rm.CardNo))
            {
                q = q.Where(w => w.CardNo.Contains(rm.CardNo));
            }
            if (!string.IsNullOrEmpty(rm.MemberName))
            {
                q = q.Where(w => w.MemberName.Contains(rm.MemberName));
            }
            if (rm.CupType.HasValue)
            {
                q = q.Where(w => w.iCupType == rm.CupType.Value);
            }
            //Guid gvoucherPayType = Guid.Parse("988BF058-9FE5-41C4-8575-2DD8F73DB8F2");
            //Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            //Guid gInventory = Guid.Parse("17516BE0-CCF2-4A58-BF2D-8C762BD8A8C4");
            //var q2 = from c in Uow.Consume.GetAll()

            //         join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
            //         from cds in cd.DefaultIfEmpty()

            //         join cd in Uow.Cards.GetAll() on c.Card equals cd.Id into clcscd
            //         from clcscds in clcscd.DefaultIfEmpty()

            //         join m in Uow.Members.GetAll() on clcscds.Member equals m.Id into cm
            //         from cms in cm.DefaultIfEmpty()

            //         join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
            //         from ods in od.DefaultIfEmpty()

            //         where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate && ods.Code == "002"
            //         select new
            //         {
            //             c.DeptId,
            //             c.UserId,
            //             Category = gCategory,
            //             Inventory = gInventory,
            //             InventoryName = "代金券",
            //             CategoryName = "代金券",
            //             DeptName = cds.DeptName,
            //             Discount = c.Discount,
            //             Amount = -((c.PayVoucher * c.Discount) / 100),
            //             CreateDate = c.CreateDate,
            //             Quantity = 0,
            //             CupType = "标准杯",
            //             iCupType=-1,
            //             iConsumeType = c.ConsumeType,
            //             ConsumeType = c.ConsumeType == 0 ? "会员卡消费" : c.ConsumeType == 1 ? "非会员消费" : "会员积分兑换",
            //             PayType = "代金券",
            //             gPayType = gvoucherPayType,
            //             clcscds.CardNo,
            //             cms.MemberName,
            //         };

            //if (rm.DeptId.HasValue)
            //{
            //    q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            //}
            //if (rm.ConsumeType.HasValue)
            //{
            //    q2 = q2.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            //}
            //if (rm.PayType.HasValue)
            //{
            //    q2 = q2.Where(w => w.gPayType == rm.PayType.Value);
            //}
            //if (rm.Category.HasValue)
            //{
            //    q2 = q2.Where(w => w.Category == rm.Category.Value);
            //}
            //if (rm.Inventory.HasValue)
            //{
            //    q2 = q2.Where(w => w.Inventory == rm.Inventory.Value);
            //}
            //if (!string.IsNullOrEmpty(rm.CardNo))
            //{
            //    q2 = q2.Where(w => w.CardNo.Contains(rm.CardNo));
            //}
            //if (!string.IsNullOrEmpty(rm.MemberName))
            //{
            //    q2 = q2.Where(w => w.MemberName.Contains(rm.MemberName));
            //}
            //if (rm.CupType.HasValue)
            //{
            //    q2 = q2.Where(w => w.iCupType == rm.CupType.Value);
            //}
            var q11 = from c in q
                      select new Report5Result()
                      {
                          Id=Guid.NewGuid(),
                          DeptName = c.DeptName,
                          CategoryName = c.CategoryName,
                          InventoryName = c.InventoryName,
                          ConsumeType = c.ConsumeType,
                          PayType = c.PayType,
                          CupType = c.CupType,
                          Amount = c.Amount,
                          Quantity = c.Quantity,
                          CardNo=c.CardNo,
                          MemberName=c.MemberName,
                          UnitOfMeasureName=c.UnitOfMeasureName,
                      };
            //var q21 = from c in q2
            //          select new Report5Result()
            //          {
            //              Id=Guid.NewGuid(),
            //              DeptName = c.DeptName,
            //              CategoryName = c.CategoryName,
            //              InventoryName = c.InventoryName,
            //              ConsumeType = c.ConsumeType,
            //              PayType = c.PayType,
            //              CupType = c.CupType,
            //              Amount = c.Amount,
            //              Quantity = c.Quantity,
            //              CardNo = c.CardNo,
            //              MemberName = c.MemberName,
            //          };
            //var q3 = q11.Union(q21);
            var q41 = from c in q11
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             Id=Guid.NewGuid(),
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            var q4 = q41.ToList().AsQueryable();
            if (!rm.IsDept)
            {
               q4= from c in q4
                     group c by new { c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = "",
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsConsumeType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName,  c.PayType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = "",
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsPayType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = "",
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsCategory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = "",
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsInventory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.ConsumeType, c.PayType, c.CupType, c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = "",
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsCupType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType,c.CardNo, c.MemberName,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = "",
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (!rm.IsCard)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CupType,c.UnitOfMeasureName }
                         into g
                         select new Report5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             CupType = g.Key.CupType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = "",
                             MemberName = "",
                             UnitOfMeasureName=g.Key.UnitOfMeasureName,
                         };
            }
            if (q4.Count() > 0)
            {
                rm.Amount = Math.Round(q4.Sum(s => s.Amount), 2);
                rm.Quantity = Math.Round(q4.Sum(s => s.Quantity), 2);
            }
            else
            {
                rm.Amount = 0;
                rm.Quantity = 0;
            }


            rm.result = q4.OrderByDescending(o=>o.Amount);
            Session["Report5Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("Report5",routeValues);
        }
        public void Report5ExportToExcel()
        {
            if (Session["Report5Model"] == null) return;
            var m = Session["Report5Model"] as Report5Model;
            object dataSource = m.result;
            string fileName = "销售排名统计.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CategoryName";
            field.HeaderText = "分类";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "InventoryName";
            field.HeaderText = "商品";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "ConsumeType";
            field.HeaderText = "消费类型";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardNo";
            field.HeaderText = "卡号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MemberName";
            field.HeaderText = "会员名";
            view.Columns.Add(field);

            if (cupTypeColumnVisible)
            {
                field = new BoundField();
                field.DataField = "CupType";
                field.HeaderText = "杯型";
                view.Columns.Add(field);
            }
            if (unitOfMeasureColumnVisibility)
            {
                field = new BoundField();
                field.DataField = "UnitOfMeasureName";
                field.HeaderText = "单位";
                view.Columns.Add(field);
            }
            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "数量";
            view.Columns.Add(field);


            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report10 饮品系列类别比例
        [Authorize]
        public ActionResult Report10()
        {
            if (Session["Report10Model"] != null)
            {
                Report10Model rm = Session["Report10Model"] as Report10Model;
                return View(rm);
            }
            else
            {
                Report10Model rm = new Report10Model();
                rm.result = new List<Report10Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string strInvType = this.Request["InvType"];
                int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strInvType))
                {
                    invType = Convert.ToInt32(strInvType);
                }
                rm.InvType = invType;

                string strCategoryType = this.Request["CategoryType"];
                int categoryType = (int)DXInfo.Models.CategoryType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strCategoryType))
                {
                    categoryType = Convert.ToInt32(strCategoryType);
                }
                rm.CategoryType = categoryType;
                string strDeptType = this.Request["DeptType"];
                int deptType = (int)DXInfo.Models.DeptType.Sale;
                if (!string.IsNullOrEmpty(strDeptType))
                {
                    deptType = Convert.ToInt32(strDeptType);
                }
                rm.DeptType = deptType;
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report10(Report10Model rm)
        {
            
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            int invType = rm.InvType;//(int)DXInfo.Models.InvType.ColdDrinkShop;
            int categoryType = rm.CategoryType;// (int)DXInfo.Models.CategoryType.ColdDrinkShop;
            int deptType = rm.DeptType;//(int)DXInfo.Models.DeptType.Sale;
            var q1 = from cl in Uow.ConsumeList.GetAll()

                     //join cm in Uow.Consume.GetAll() on cl.Consume equals cm.Id into clcm
                     //from clcms in clcm.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join c in Uow.Inventory.GetAll() on cl.Inventory equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join t in Uow.InventoryCategory.GetAll() on clcs.Category equals t.Id into clcst
                     from clcsts in clcst.DefaultIfEmpty()

                     join o in Uow.Organizations.GetAll() on clds.OrganizationId equals o.Id into od
                     from ods in od.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && 
                     clcs.InvType==invType &&
                     clcsts.CategoryType==categoryType &&
                     clds.DeptType==deptType//ods.Code == "002"

                     select new Report101Result()
                     {
                         Id=cl.Id,
                         DeptId=cl.DeptId,
                         DeptName=clds.DeptName,
                         UserId=cl.UserId,
                         FullName=clus.FullName,
                         Category=clcsts.Id,
                         CategoryName = clcsts.Name,
                         CreateDate=cl.CreateDate,
                         Amount = cl.Amount,// * clcms.Discount / 100,
                         Quantity=cl.Quantity
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);

            
            //Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            //var q2 = from cl in Uow.Consume.GetAll()
            //         join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
            //         from cds in cd.DefaultIfEmpty()

            //         join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
            //         from clus in clu.DefaultIfEmpty()

            //         join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
            //         from ods in od.DefaultIfEmpty()

            //         where cl.PayVoucher > 0 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && ods.Code == "002"
            //         select new Report101Result()
            //         {
            //             Id=cl.Id,
            //             DeptId=cl.DeptId,
            //             DeptName = cds.DeptName,
            //             UserId=cl.UserId,
            //             FullName=clus.FullName,
            //             Category = gCategory,
            //             CategoryName = "代金券",
            //             CreateDate=cl.CreateDate,
            //             Amount = -((cl.PayVoucher * cl.Discount) / 100),
            //             Quantity = 0,
            //         };
            //if (rm.DeptId.HasValue) q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);

            var q3 = q1.ToList();//q1.Union(q2).ToList();
            if (q3.Count > 0)
            {
                var q5 = (from q in q3
                          select q.CreateDate).Max();
                var q6 = (from q in q3
                          select q.CreateDate).Min();
                TimeSpan dtSub = q5 - q6;
                int iday = dtSub.Days;
                iday = iday + 1;

                decimal dAmount = 1;
                decimal dQuantity = 1;
                decimal dAvg = 0;
                if (q3.Count > 0)
                {
                    dAmount = Math.Round(q3.Sum(s => s.Amount), 2);
                    dQuantity = Math.Round(q3.Sum(s => s.Quantity), 2);
                    dAvg = Math.Round(dAmount / iday, 2);

                }

                var q4 = from q in q3
                         group q by new { q.DeptName, q.Category, q.CategoryName } into g
                         select new Report10Result()
                         {
                             DeptName = g.Key.DeptName,
                             Category = g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Cups = g.Sum(s => s.Quantity),
                             Amount = g.Sum(s => s.Amount),
                             AmountOfDayAvg = g.Sum(a => a.Amount) / iday,
                             CupsRatio = g.Sum(s => s.Quantity) / dQuantity,
                             AmountRatio = g.Sum(s => s.Amount) / dAmount,
                         };
                var q7 = q4.ToList();
                if (q7 != null)
                {
                    if (rm.Category.HasValue) q7 = q7.Where(w => w.Category == rm.Category.Value).ToList();
                    rm.result = q7;
                }
                rm.Cups = dQuantity;
                rm.Amount = dAmount;
                rm.AmountOfDayAvg = dAvg;
            }
            else
            {
                rm.result = new List<Report10Result>();
            }
            Session["Report10Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("Report10",routeValues);
        }
        [Authorize]
        public void Report10ExportToExcel()
        {
            if (Session["Report10Model"] == null) return;
            var m = Session["Report10Model"] as Report10Model;
            object dataSource = m.result;
            string fileName = "饮品系列类别比例.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;


            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "店名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "收银员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CategoryName";
            field.HeaderText = "系列名称";
            view.Columns.Add(field);

                field = new BoundField();
                field.DataField = "Cups";
                field.HeaderText = "销售数量";
                view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "销售金额";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AmountOfDayAvg";
            field.HeaderText = "日均销量";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

                field = new BoundField();
                field.DataField = "CupsRatio";
                field.HeaderText = "数量比例";
                field.DataFormatString = "{0:P}";
                view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AmountRatio";
            field.HeaderText = "金额比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);
            
            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report11 单品类别比例
        [Authorize]
        public ActionResult Report11()
        {
            if (Session["Report11Model"] != null)
            {
                Report11Model rm = Session["Report11Model"] as Report11Model;
                return View(rm);
            }
            else
            {
                Report11Model rm = new Report11Model();
                rm.result = new List<Report11Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string strInvType = this.Request["InvType"];
                int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strInvType))
                {
                    invType = Convert.ToInt32(strInvType);
                }
                rm.InvType = invType;

                string strCategoryType = this.Request["CategoryType"];
                int categoryType = (int)DXInfo.Models.CategoryType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strCategoryType))
                {
                    categoryType = Convert.ToInt32(strCategoryType);
                }
                rm.CategoryType = categoryType;
                string strDeptType = this.Request["DeptType"];
                int deptType = (int)DXInfo.Models.DeptType.Sale;
                if (!string.IsNullOrEmpty(strDeptType))
                {
                    deptType = Convert.ToInt32(strDeptType);
                }
                rm.DeptType = deptType;
                return View(rm);
            }
        }
        [Authorize]
        public JsonResult GetCatOfInvs(Guid? category)
        {
            List<DXInfo.Models.Inventory> vehicles = new List<DXInfo.Models.Inventory>();
            if (category.HasValue)
            {
                vehicles = Uow.Inventory.GetAll().Where(w => w.Category == category).ToList();
            }
            else
                vehicles = Uow.Inventory.GetAll().ToList();
            var listItems =
                (from v in vehicles
                 select
                     new SelectListItem()
                     {
                         Text = v.Name,
                         Value = v.Id.ToString()
                     }).ToList<SelectListItem>();
            listItems.Insert(0, new SelectListItem() { Text = "", Value = "" });
            JsonResult jr = new JsonResult();
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jr.Data = listItems;
            return jr;
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report11(Report11Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            int invType = rm.InvType;//(int)DXInfo.Models.InvType.ColdDrinkShop;
            int categoryType = rm.CategoryType; //(int)DXInfo.Models.CategoryType.ColdDrinkShop;
            int deptType = rm.DeptType;//(int)DXInfo.Models.DeptType.Sale;
            var q1 = from cl in Uow.ConsumeList.GetAll()

                     join cm in Uow.Consume.GetAll() on cl.Consume equals cm.Id into clcm
                     from clcms in clcm.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join c in Uow.Inventory.GetAll() on cl.Inventory equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join t in Uow.InventoryCategory.GetAll() on clcs.Category equals t.Id into clcst
                     from clcsts in clcst.DefaultIfEmpty()

                     join o in Uow.Organizations.GetAll() on clds.OrganizationId equals o.Id into od
                     from ods in od.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && 
                     clcs.InvType==invType&&
                     clcsts.CategoryType==categoryType&&
                     clds.DeptType==deptType//ods.Code == "002"	

                     select new Report111Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         OperatorsOnDuty = clcms.OperatorsOnDuty,
                         Category = clcsts.Id,
                         CategoryName = clcsts.Name,
                         Inventory = clcs.Id,
                         InventoryName = clcs.Name,                          
                         CreateDate = cl.CreateDate,
                         Amount = cl.Amount,
                         Quantity = cl.Quantity
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            //Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            //Guid gInventory = Guid.Parse("17516BE0-CCF2-4A58-BF2D-8C762BD8A8C4");
            //var q2 = from cl in Uow.Consume.GetAll()
            //         join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
            //         from cds in cd.DefaultIfEmpty()

            //         join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
            //         from clus in clu.DefaultIfEmpty()

            //         join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
            //         from ods in od.DefaultIfEmpty()

            //         where cl.PayVoucher > 0 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && ods.Code == "002"
            //         select new Report111Result()
            //         {
            //             Id = cl.Id,
            //             DeptId = cl.DeptId,
            //             DeptName = cds.DeptName,
            //             UserId = cl.UserId,
            //             FullName = clus.FullName,
            //             Category = gCategory,
            //             CategoryName = "代金券",
            //             Inventory = gInventory,
            //             InventoryName = "代金券",
            //             CreateDate = cl.CreateDate,
            //             Amount = -((cl.PayVoucher * cl.Discount) / 100),
            //             Quantity = 0,
            //         };
            //if (rm.DeptId.HasValue) q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);

            var q3 = q1.ToList();//.Union(q2).ToList();
            if (q3.Count > 0)
            {
                var q5 = (from q in q3
                          select q.CreateDate).Max();
                var q6 = (from q in q3
                          select q.CreateDate).Min();
                TimeSpan dtSub = q5 - q6;
                int iday = dtSub.Days;
                iday = iday + 1;

                decimal dAmount = 1;
                decimal dQuantity = 1;
                decimal dAvg = 0;
                if (q3.Count > 0)
                {
                    dAmount = Math.Round(q3.Sum(s => s.Amount), 2);
                    dQuantity = Math.Round(q3.Sum(s => s.Quantity), 2);
                    dAvg = Math.Round(dAmount / iday, 2);

                }

                var q4 = from q in q3
                         group q by new { CreateDate = "", q.DeptId,q.DeptName,
                             q.Category,q.CategoryName, 
                             q.Inventory,q.InventoryName,
                             q.UserId,q.FullName,q.OperatorsOnDuty
                         } into g
                         select new Report11Result()
                         {
                             CreateDate = g.Key.CreateDate,
                             DeptName = g.Key.DeptName,
                             FullName = g.Key.FullName,
                             OperatorsOnDuty = g.Key.OperatorsOnDuty,
                             Category=g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Inventory = g.Key.Inventory,
                             InventoryName = g.Key.InventoryName,
                             Cups = g.Sum(s => s.Quantity),
                             Amount = g.Sum(s => s.Amount),
                             AmountOfDayAvg = g.Sum(a => a.Amount) / iday,
                             CupsRatioOfAll = g.Sum(s => s.Quantity) / dQuantity,
                             AmountRatioOfAll = g.Sum(s => s.Amount) / dAmount,
                         };
                var q7 = from q in q3
                         group q by new { q.Category,q.CategoryName } into g
                         select new Report11Result()
                         {
                             Category = g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Cups = g.Sum(s => s.Quantity),
                             Amount = g.Sum(s => s.Amount),
                         };
                var q8 = from q in q4
                         join q9 in q7 on q.Category equals q9.Category into qq9
                         from qq9s in qq9.DefaultIfEmpty()
                         select new Report11Result()
                         {
                             CreateDate = q.CreateDate,
                             DeptName = q.DeptName,
                             FullName=q.FullName,
                             OperatorsOnDuty = q.OperatorsOnDuty,
                             Category = q.Category,
                             CategoryName = q.CategoryName,
                             Inventory = q.Inventory,
                             InventoryName = q.InventoryName,
                             Cups = q.Cups,
                             Amount = q.Amount,
                             AmountOfDayAvg = q.AmountOfDayAvg,
                             AmountRatioOfAll = q.AmountRatioOfAll,
                             CupsRatioOfAll = q.CupsRatioOfAll,
                             AmountRatioOfCategory = q.Amount / (qq9s.Amount==0?1:qq9s.Amount),//qq9s.Amount,
                             CupsRatioOfCategory = q.Cups / qq9s.Cups,
                         };
                if (rm.Category.HasValue) q8 = q8.Where(w => w.Category == rm.Category.Value);
                if (rm.Inventory.HasValue) q8 = q8.Where(w => w.Inventory == rm.Inventory.Value);

                rm.result = q8.ToList();
                rm.Cups = dQuantity;
                rm.Amount = dAmount;
                rm.AmountOfDayAvg = dAvg;
            }
            else
            {
                rm.result = new List<Report11Result>();
            }
            Session["Report11Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("Report11", routeValues);
        }

        [Authorize]
        public void Report11ExportToExcel()
        {
            if (Session["Report11Model"] == null) return;
            var m = Session["Report11Model"] as Report11Model;
            object dataSource = m.result;
            string fileName = "单品类别比例.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "店名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CategoryName";
            field.HeaderText = "系列名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "InventoryName";
            field.HeaderText = "单品名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Cups";
            field.HeaderText = "销售数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "销售金额";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AmountOfDayAvg";
            field.HeaderText = "日均销量";
            field.DataFormatString = "{0:f2}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CupsRatioOfCategory";
            field.HeaderText = "本类别数量比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CupsRatioOfAll";
            field.HeaderText = "数量总比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AmountRatioOfCategory";
            field.HeaderText = "本类别金额比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AmountRatioOfAll";
            field.HeaderText = "金额总比例";
            field.DataFormatString = "{0:P}";
            view.Columns.Add(field);

            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region Report14 单数明细和统计
        [Authorize]
        public ActionResult Report14()
        {
            if (Session["Report14Model"] != null)
            {
                Report14Model rm = Session["Report14Model"] as Report14Model;
                return View(rm);
            }
            else
            {
                Report14Model rm = new Report14Model();
                rm.result = new List<Report14Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string strInvType = this.Request["InvType"];
                int invType = (int)DXInfo.Models.InvType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strInvType))
                {
                    invType = Convert.ToInt32(strInvType);
                }
                rm.InvType = invType;

                string strCategoryType = this.Request["CategoryType"];
                int categoryType = (int)DXInfo.Models.CategoryType.ColdDrinkShop;
                if (!string.IsNullOrEmpty(strCategoryType))
                {
                    categoryType = Convert.ToInt32(strCategoryType);
                }
                rm.CategoryType = categoryType;
                string strDeptType = this.Request["DeptType"];
                int deptType = (int)DXInfo.Models.DeptType.Sale;
                if (!string.IsNullOrEmpty(strDeptType))
                {
                    deptType = Convert.ToInt32(strDeptType);
                }
                rm.DeptType = deptType;
                return View(rm);
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Report14(Report14Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            Guid gcardPayType = Guid.Parse("589DE614-4779-45E6-ADC6-ABF94E55AF17");
            Guid gpointPayType = Guid.Parse("F4D343D6-236C-4796-9DA4-016C22427F86");
            int invType = rm.InvType;//(int)DXInfo.Models.InvType.ColdDrinkShop;
            int categoryType = rm.CategoryType;//(int)DXInfo.Models.CategoryType.ColdDrinkShop;
            int deptType = rm.DeptType;//(int)DXInfo.Models.DeptType.Sale;

            var q = from cl in Uow.ConsumeList.GetAll()
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
                    from ods in od.DefaultIfEmpty()

                    join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d1.Code into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && //ods.Code == "002"
                    cis.InvType==invType&&
                    iics.CategoryType==categoryType&&
                    cds.DeptType==deptType
                    select new
                    {
                        Consume=clcs.Id,
                        cl.DeptId,
                        cl.UserId,
                        CardNo = ccards.CardNo,
                        MemberName = cms.MemberName,
                        FullName = cus.FullName,
                        clcs.OperatorsOnDuty,
                        DeptName = cds.DeptName,
                        Discount = clcs.Discount,
                        Amount = cl.Amount * clcs.Discount / 100,
                        Sum = cl.Amount,
                        CreateDate = cl.CreateDate,
                        Quantity = cl.Quantity,                        
                        ConsumeType = dd1s.Name,//clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : "会员积分兑换",
                        iConsumeType = clcs.ConsumeType,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : cps.Id,
                    };
            if (rm.DeptId.HasValue)
            {
                q = q.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q = q.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.ConsumeType.HasValue)
            {
                q = q.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            }
            if (rm.PayType.HasValue)
            {
                q = q.Where(w => w.gPayType == rm.PayType.Value);
            }
            if (!string.IsNullOrEmpty(rm.CardNo))
            {
                q = q.Where(w => w.CardNo.Contains(rm.CardNo));
            }
            if (!string.IsNullOrEmpty(rm.MemberName))
            {
                q = q.Where(w => w.MemberName.Contains(rm.MemberName));
            }
            var q11 = from c in q.ToList()
                      select new Report14Result()
                      {
                          Consume=c.Consume,
                          CardNo = c.CardNo,
                          MemberName = c.MemberName,
                          FullName = c.FullName,
                          OperatorsOnDuty = c.OperatorsOnDuty,
                          DeptName = c.DeptName,
                          Discount = c.Discount,
                          Amount = c.Amount,
                          Sum = c.Sum,
                          CreateDate = Convert.ToDateTime(c.CreateDate.ToString("yyyy-MM-dd HH:mm")),                          
                          Quantity = c.Quantity,                          
                          ConsumeType = c.ConsumeType,
                          PayType = c.PayType
                      };
            //Guid gvoucherPayType = Guid.Parse("988BF058-9FE5-41C4-8575-2DD8F73DB8F2");
            //var q2 = from c in Uow.Consume.GetAll()

            //         join card in Uow.Cards.GetAll() on c.Card.Value equals card.Id into ccard
            //         from ccards in ccard.DefaultIfEmpty()

            //         join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
            //         from cms in cm.DefaultIfEmpty()

            //         join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
            //         from cus in cu.DefaultIfEmpty()

            //         join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
            //         from cds in cd.DefaultIfEmpty()

            //         join o in Uow.Organizations.GetAll() on cds.OrganizationId equals o.Id into od
            //         from ods in od.DefaultIfEmpty()

            //         where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate && ods.Code == "002"
            //         select new
            //         {
            //             Consume=c.Id,
            //             c.DeptId,
            //             c.UserId,
            //             CardNo = ccards.CardNo,
            //             MemberName = cms.MemberName,
            //             InventoryName = "代金券",
            //             CategoryName = "代金券",
            //             FullName = cus.FullName,
            //             DeptName = cds.DeptName,
            //             Discount = c.Discount,
            //             Amount = -((c.PayVoucher * c.Discount) / 100),
            //             Sum = -c.PayVoucher,
            //             CreateDate = c.CreateDate,
            //             Price = -c.PayVoucher,
            //             Quantity = 0,
            //             Cup = "标准杯",
            //             iConsumeType = c.ConsumeType,
            //             ConsumeType = c.ConsumeType == 0 ? "会员卡消费" : c.ConsumeType == 1 ? "非会员消费" : "会员积分兑换",
            //             PayType = "代金券",
            //             gPayType = gvoucherPayType,
            //         };

            //if (rm.DeptId.HasValue)
            //{
            //    q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            //}
            //if (rm.UserId.HasValue)
            //{
            //    q2 = q2.Where(w => w.UserId == rm.UserId.Value);
            //}
            //if (rm.ConsumeType.HasValue)
            //{
            //    q2 = q2.Where(w => w.iConsumeType == rm.ConsumeType.Value);
            //}
            //if (rm.PayType.HasValue)
            //{
            //    q2 = q2.Where(w => w.gPayType == rm.PayType.Value);
            //}
            //if (!string.IsNullOrEmpty(rm.CardNo))
            //{
            //    q2 = q2.Where(w => w.CardNo.Contains(rm.CardNo));
            //}
            //if (!string.IsNullOrEmpty(rm.MemberName))
            //{
            //    q2 = q2.Where(w => w.MemberName.Contains(rm.MemberName));
            //}

            //var q21 = from c in q2.ToList()
            //          select new Report14Result()
            //          {
            //              Consume=c.Consume,
            //              CardNo = c.CardNo,
            //              MemberName = c.MemberName,
            //              FullName = c.FullName,
            //              DeptName = c.DeptName,
            //              Discount = c.Discount,
            //              Amount = c.Amount,
            //              Sum = c.Sum,
            //              CreateDate = Convert.ToDateTime(c.CreateDate.ToString("yyyy-MM-dd HH:mm")),                          
            //              Quantity = c.Quantity,                          
            //              ConsumeType = c.ConsumeType,
            //              PayType = c.PayType
            //          };

            //var q3 = q11.Union(q21);

            int i2 = 0;
            var q31 = from c in q11.ToList()
                      group c by new { c.Consume, c.CardNo, c.MemberName, c.FullName,c.OperatorsOnDuty, c.DeptName, c.Discount, c.CreateDate,c.ConsumeType,c.PayType}
                          into g
                          select new Report14Result()
                          {
                              Consume = g.Key.Consume,
                              Id = i2++,
                              CardNo = g.Key.CardNo,
                              MemberName = g.Key.MemberName,
                              FullName = g.Key.FullName,
                              OperatorsOnDuty = g.Key.OperatorsOnDuty,
                              DeptName = g.Key.DeptName,
                              Discount = g.Key.Discount,
                              CreateDate = g.Key.CreateDate,
                              ConsumeType = g.Key.ConsumeType,
                              PayType = g.Key.PayType,

                              Amount = g.Sum(s => s.Amount),
                              Quantity = g.Sum(s => s.Quantity),
                              Sum=g.Sum(s=>s.Sum)
                          };

            if (q31.Count() > 0)
            {
                rm.Count = q31.Count();
                rm.Quantity = Math.Round(q31.Sum(s => s.Quantity), 2);
                rm.Sum = Math.Round(q31.Sum(s => s.Sum), 2);
                rm.Amount = Math.Round(q31.Sum(s => s.Amount), 2);
            }
            else
            {
                rm.Count = 0;
                rm.Quantity = 0;
                rm.Sum = 0;
                rm.Amount = 0;
            }
            rm.result = q31.ToList();
            Session["Report14Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("Report14", routeValues);
        }

        public void Report14ExportToExcel()
        {
            if (Session["Report14Model"] == null) return;
            var m = Session["Report14Model"] as Report14Model;
            object dataSource = m.result;
            string fileName = "单数明细和统计.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "Consume";
            field.HeaderText = "单号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FullName";
            field.HeaderText = "操作员";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "ConsumeType";
            field.HeaderText = "消费类型";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "PayType";
            field.HeaderText = "支付方式";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardNo";
            field.HeaderText = "卡号";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "MemberName";
            field.HeaderText = "会员名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Sum";
            field.HeaderText = "合计";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Discount";
            field.HeaderText = "折扣";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Amount";
            field.HeaderText = "金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CreateDate";
            field.HeaderText = "消费日期";
            view.Columns.Add(field);
            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region DoExportToExcel
        public void DoExportToExcel(string fileName, GridView view)
        {

            this.Response.ClearContent();
            this.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
            this.Response.ContentType = "application/excel";
            StringWriter writer = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer);
            view.RenderControl(writer2);
            this.Response.Write(writer.ToString());
            this.Response.End();
        }
        #endregion
    }
    public class ChartResult : ActionResult
    {
        private readonly Chart _chart;
        private readonly string _format;

        public ChartResult(Chart chart, string format = "png")
        {
            if (chart == null)
                throw new ArgumentNullException("chart");

            _chart = chart;
            _format = format;

            if (string.IsNullOrEmpty(_format))
                _format = "png";
        }

        public Chart Chart
        {
            get { return _chart; }
        }

        public string Format
        {
            get { return _format; }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            _chart.Write(_format);
        }
    }
    
}
