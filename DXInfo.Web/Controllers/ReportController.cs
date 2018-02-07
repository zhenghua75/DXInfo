using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXInfo.Web.Models;
using Trirand.Web.Mvc;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Web.Security;
using DXInfo.Data.Contracts;
using System.Web.Routing;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq.Dynamic;
using System.Data.Entity;
using AutoMapper;
namespace DXInfo.Web.Controllers
{
    #region 其它类
    public class Temp1Class
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int Day { get; set; }
        public decimal Amount { get; set; }
    }
    public class Temp2Class
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
    public class Temp3Class
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public int Hour { get; set; }
        public decimal Amount { get; set; }
    }
    #endregion

    [Authorize]
    public class ReportController : BaseController
    {
        #region 构造
        public ReportController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");    
        }
        #endregion
        private int PageCount = 100;
        #region 基础报表
        #region WRReport1 会员资料查询
        public ActionResult WRReport1()
        {
            var gridModel = new WRReport1GridModel();
            SetupWRReport1GridModel(gridModel.WRReportGrid);
            return PartialView(gridModel);
        }
        private void SetupWRReport1GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WRReport1_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(WRReport1_DataResolved);
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetDateTimeColumn(grid, "CreateDate");
            SetDateTimeColumn(grid, "LossDate");
            SetDateTimeColumn(grid, "FoundDate");
            SetDateTimeColumn(grid, "AddDate");

            grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "停用",
                Text = "<span class='ui-pg-button-text'>停用</span>",
                OnClick = "customButtonClicked",
                ButtonIcon = "ui-icon-extlink",
            });
            grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "启用",
                Text = "<span class='ui-pg-button-text'>启用</span>",
                OnClick = "customButton2Clicked",
                ButtonIcon = "ui-icon-extlink",
            });
        }
        private IQueryable WRReport1_GetDate()
        {
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

                    join d4 in Uow.NameCode.GetAll().Where(w=>w.Type=="CardStatus") on SqlFunctions.StringConvert((double?)c.Status).Trim() equals d4.Code into dd4
                    from dd4s in dd4.DefaultIfEmpty()

                    select new
                    {
                        c.Id,
                        CardType = c.CardType,
                        CardTypeName = cts.Name,
                        CardLevel = c.CardLevel,
                        CardLevelName = cls.Name,
                        DeptId = c.DeptId,
                        DeptName = cds.DeptName,
                        CardNo = c.CardNo,
                        MemberName = cms.MemberName,

                        Balance = c.Balance,
                        Recharge = crs.Recharg == null ? 0 : crs.Recharg,
                        Points = cps.Points == null ? 0 : cps.Points,

                        CreateDate = c.CreateDate,
                        c.UserId,
                        FullName = cus.FullName,
                        Status = c.Status,
                        StatusName = dd4s.Name,
                        Email = cms.Email,
                        IdCard = cms.IdCard,
                        LinkAddress = cms.LinkAddress,
                        LinkPhone = cms.LinkPhone,
                        Sex = cms.Sex,
                        Birthday = cms.Birthday,
                        LossDate = c.LossDate,
                        LossFullName = cus1.FullName,
                        LossDeptName = cds1.DeptName,
                        FoundDate = c.FoundDate,
                        FoundFullName = cus2.FullName,
                        FoundDeptName = cds2.DeptName,

                        SecondCardNo = c.SecondCardNo,

                        AddDate = c.AddDate,
                        AddReason = c.AddReason,
                        AddFullName = cus3.FullName,
                        AddDeptName = cds3.DeptName,
                        cds3.OrganizationId,
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "UserId", true);
            return q2;
        }
        void WRReport1_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {

            decimal? balance = e.FilterData.Select("Balance").Cast<Decimal?>().Sum();
            decimal? points = e.FilterData.Select("Points").Cast<Decimal?>().Sum();            
            JQGridColumn balanceColumn = e.GridModel.Columns.Find(c => c.DataField == "Balance");
            balanceColumn.FooterValue = balance.ToString();
            JQGridColumn pointsColumn = e.GridModel.Columns.Find(c => c.DataField == "Points");
            pointsColumn.FooterValue = points.ToString();
        }
        public ActionResult WRReport1_RequestData()
        {
            var gridModel = new WRReport1GridModel();
            SetupWRReport1GridModel(gridModel.WRReportGrid);
            IQueryable q = WRReport1_GetDate();
            return QueryAndExcel(gridModel.WRReportGrid, q, "会员资料查询.xls");
        }

        public ActionResult StopCard(Guid CardId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                DXInfo.Models.Cards oldCard = Uow.Cards.GetById(g => g.Id == CardId);
                if (oldCard.Status != (int)DXInfo.Models.CardStatus.InUser)
                    throw new Exception("正常在用会员才可以停用");
                if (oldCard != null)
                {
                    oldCard.Status = (int)DXInfo.Models.CardStatus.Stoped;
                    Uow.Cards.Update(oldCard);

                    DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(oldCard);
                    cardsLog.CardId = oldCard.Id;
                    cardsLog.UserId = this.operId;
                    cardsLog.DeptId = this.deptId;
                    cardsLog.CreateDate = DateTime.Now;
                    Uow.CardsLog.Add(cardsLog);
                    Uow.Commit();
                    json.Data = new { Error = "" };
                }
                else
                    json.Data = new { Error = "未找到会员" };
            }
            catch (Exception ex)
            {
                json.Data = new { Error = ex.Message };
            }
            
            return json;
        }
        public ActionResult StartCard(Guid CardId)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                DXInfo.Models.Cards oldCard = Uow.Cards.GetById(g => g.Id == CardId);
                if (oldCard.Status != (int)DXInfo.Models.CardStatus.Stoped)
                    throw new Exception("停用会员才可以启用");
                if (oldCard != null)
                {
                    oldCard.Status = (int)DXInfo.Models.CardStatus.InUser;
                    Uow.Cards.Update(oldCard);

                    DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(oldCard);
                    cardsLog.CardId = oldCard.Id;
                    cardsLog.UserId = this.operId;
                    cardsLog.DeptId = this.deptId;
                    cardsLog.CreateDate = DateTime.Now;
                    Uow.CardsLog.Add(cardsLog);
                    Uow.Commit();
                    json.Data = new { Error = "" };
                }
                else
                    json.Data = new { Error = "未找到会员" };
            }
            catch (Exception ex)
            {
                json.Data = new { Error = ex.Message };
            }

            return json;
        }
        #endregion

        #region WRReport3 充值明细查询
        public ActionResult WRReport3()
        {
            var gridModel = new WRReport3GridModel();
            SetupWRReport3GridModel(gridModel.WRReportGrid);
            return PartialView(gridModel);
        }
        public ActionResult WRReport3_RequestData()
        {

            var gridModel = new WRReport3GridModel();
            SetupWRReport3GridModel(gridModel.WRReportGrid);
            IQueryable q = WRReport3_GetData();
            return QueryAndExcel(gridModel.WRReportGrid, q, "充值明细查询.xls");
        }
        private void SetupWRReport3GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WRReport3_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(WRReport3_DataResolved);
            JQGridColumn operatorsOnDutyColumn = grid.Columns.Find(f => f.DataField == "OperatorsOnDuty");
            if (operatorsOnDutyColumn != null)
                operatorsOnDutyColumn.Visible = isOperatorsOnDuty;
            SetDateTimeColumn(grid, "CreateDate");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public IQueryable WRReport3_GetData()
        {
            Guid deptIEmpty = Guid.Empty;
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

                    join d1 in Uow.Depts.GetAll() on rcs.DeptId equals d1.DeptId into rd1
                    from rd1s in rd1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w=>w.Type=="RechargeType") on SqlFunctions.StringConvert((double?)r.RechargeType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    select new
                    {
                        LocalDeptId = rcs == null ? deptIEmpty : rcs.DeptId,
                        LocalDeptName = rd1s.DeptName,
                        r.DeptId,
                        r.UserId,
                        rds.OrganizationId,
                        RechargeType = r.RechargeType,
                        PayType = r.PayType,
                        Amount = r.Amount,
                        Balance = r.Balance,
                        CreateDate = r.CreateDate,
                        Donate = r.Donate,
                        LastBalance = r.LastBalance,
                        RechargeTypeName = dd2s.Name,
                        CardNo = rcs.CardNo,
                        MemberName = rcsms.MemberName,
                        FullName = rus.FullName,
                        DeptName = rds.DeptName,
                        PayTypeName = rps.Name,
                        r.OperatorsOnDuty,
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "UserId", true);
            return q2;
        }
        void WRReport3_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {

            decimal? Amount = e.FilterData.Select("Amount").Cast<Decimal?>().Sum();
            decimal? Donate = e.FilterData.Select("Donate").Cast<Decimal?>().Sum();
            decimal? LastBalance = e.FilterData.Select("LastBalance").Cast<Decimal?>().Sum();
            decimal? Balance = e.FilterData.Select("Balance").Cast<Decimal?>().Sum();

            JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            amountColumn.FooterValue = Amount.ToString();
            JQGridColumn pointsColumn = e.GridModel.Columns.Find(c => c.DataField == "Donate");
            pointsColumn.FooterValue = Donate.ToString();
            JQGridColumn lastBalanceColumn = e.GridModel.Columns.Find(c => c.DataField == "LastBalance");
            lastBalanceColumn.FooterValue = LastBalance.ToString();
            JQGridColumn balanceColumn = e.GridModel.Columns.Find(c => c.DataField == "Balance");
            balanceColumn.FooterValue = Balance.ToString();
        }
        #endregion

        #region WRReport6 业务量统计
        public ActionResult WRReport6()
        {
            if (Session["WRReport6Model"] != null)
            {
                WRReport6Model rm = Session["WRReport6Model"] as WRReport6Model;
                return PartialView(rm);
            }
            else
            {
                WRReport6Model rm = new WRReport6Model();
                WRReport6Result r6 = new WRReport6Result();
                r6.Card = new List<WRReport6Card>();
                r6.MemberConsume = new List<WRReport6Consume>();
                r6.NoMemberConsume = new List<WRReport6Consume>();
                r6.Recharge = new List<WRReport6Recharge>();
                rm.result = r6;
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return PartialView(rm);
            }
        }

        public void WRReport6ExportToExcel()
        {
            if (Session["WRReport6Model"] == null) return;
            var m = Session["WRReport6Model"] as WRReport6Model;
            WRReport6Result dataSource = m.result;
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

            GridView view5 = new GridView();
            view5.AutoGenerateColumns = false;
            
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
            this.Response.Write("<html><head><meta http-equiv=Content-Type content='text/html; charset=utf-8'></head><body>" + writer.ToString() + "</body></html>");
            this.Response.End();
        }
        [HttpPost]
        public ActionResult WRReport6(WRReport6Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            if (rm.result == null)
            {
                rm.result = new WRReport6Result();
            }
            #region 充值
            var q1 = from r in Uow.Recharges.GetAll()
                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()
                     join d1 in Uow.Depts.GetAll() on r.DeptId equals d1.DeptId into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate &&
                     r.CreateDate <= dtEndDate &&
                     r.RechargeType == 0
                     select new
                     {
                         Id = Guid.NewGuid(),
                         r.DeptId,
                         r.UserId,
                         dd1s.OrganizationId,
                         PayType = rps.Name,
                         r.Amount,
                         r.Donate
                     };
            if (rm.DeptId.HasValue)
            {
                q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q12 = businessCommon.SetVouchAuthority(q1, "UserId", true);            
            var q13 = q12
                .GroupBy("new(it.PayType)", "it")
                .Select("new (it.key.PayType,it.Sum(Amount) as Amount,it.Sum(Donate) as Donate,it.Count() as Count)")
                .ToList<WRReport6Recharge>();
            rm.result.Recharge = q13;
            #endregion
            #region 会员消费
            var q2 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                     from clcsps in clcsp.DefaultIfEmpty()
                     join d1 in Uow.Depts.GetAll() on cl.DeptId equals d1.DeptId into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate &&
                     (clcs.ConsumeType == 0 || clcs.ConsumeType == 2 || clcs.ConsumeType == 3)

                     select new
                     {
                         ConsumeListId = cl.Id,
                         ConsumeId = clcs.Id,
                         cl.DeptId,
                         dd1s.OrganizationId,
                         cl.UserId,
                         Amount = cl.Amount,
                         cl.Quantity,
                         PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                     };

            if (rm.DeptId.HasValue)
            {
                q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q21 = businessCommon.SetVouchAuthority(q2, "UserId", true);
            var q22 = q21
                .GroupBy("new(it.PayType)", "it")
                .Select("new (it.key.PayType,it.Sum(Amount) as Amount,it.Sum(Quantity) as Quantity,it.Count() as Count)")
                .ToList<WRReport6Consume>();
            var q23 = from c in Uow.Consume.GetAll()
                      join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                      && (c.ConsumeType == 0 || c.ConsumeType == 2 || c.ConsumeType == 3)
                      select new
                      {
                          ConsumeId = c.Id,
                          c.DeptId,
                          c.UserId,
                          dd1s.OrganizationId,
                          Amount = c.PayVoucher,
                          Quantity = 0,
                          PayType = "代金券"
                      };

            if (rm.DeptId.HasValue)
            {
                q23 = q23.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q24 = businessCommon.SetVouchAuthority(q23, "UserId", true);

            var q25 = q24
                .GroupBy("new(it.PayType)", "it")
                .Select("new (it.key.PayType,it.Sum(Amount) as Amount,it.Sum(Quantity) as Quantity,it.Count() as Count)")
                .ToList<WRReport6Consume>();
            var q26 = q22.Union(q25);
            rm.result.MemberConsume = q26.ToList();
            #endregion
            #region 非会员消费
            var q3 = from cl in Uow.ConsumeList.GetAll()
                     join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into clcsp
                     from clcsps in clcsp.DefaultIfEmpty()
                     join d1 in Uow.Depts.GetAll() on cl.DeptId equals d1.DeptId into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && clcs.ConsumeType == 1

                     select new
                     {
                         ConsumeListId = cl.Id,
                         ConsumeId = clcs.Id,
                         cl.DeptId,
                         cl.UserId,
                         dd1s.OrganizationId,
                         Amount = cl.Amount,
                         cl.Quantity,
                         PayType = clcsps.Name,
                     };

            if (rm.DeptId.HasValue)
            {
                q3 = q3.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q31 = businessCommon.SetVouchAuthority(q3, "UserId", true);
            var q32 = q31
                .GroupBy("new(it.PayType)", "it")
                .Select("new (it.key.PayType,it.Sum(Amount) as Amount,it.Sum(Quantity) as Quantity,it.Count() as Count)")
                .ToList<WRReport6Consume>();

            var q33 = from c in Uow.Consume.GetAll()
                      join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      where c.PayVoucher > 0 && c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate && c.ConsumeType == 1
                      select new
                      {
                          ConsumeId = c.Id,
                          c.DeptId,
                          c.UserId,
                          dd1s.OrganizationId,
                          Amount = -((c.PayVoucher * c.Discount) / 100),
                          Quantity = 0,
                          PayType = "代金券"
                      };

            if (rm.DeptId.HasValue)
            {
                q33 = q33.Where(w => w.DeptId == rm.DeptId.Value);
            }
            var q34 = businessCommon.SetVouchAuthority(q33, "UserId", true);
            var q35 = q34
                .GroupBy("new(it.PayType)", "it")
                .Select("new (it.key.PayType,it.Sum(Amount) as Amount,it.Sum(Quantity) as Quantity,it.Count() as Count)")
                .ToList<WRReport6Consume>();
            var q36 = q32.Union(q35);
            rm.result.NoMemberConsume = q36.ToList();
            #endregion

            #region 办卡 q4
            var q4 = from c in Uow.Cards.GetAll()
                     join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     where c.CreateDate >= dtBeginDate && c.CreateDate <= dtEndDate
                     select new { c.DeptId,c.UserId,dd1s.OrganizationId, c.Id, Status = "办卡", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q4 = q4.Where(w => w.DeptId == rm.DeptId.Value);
            }
            //var q41 = businessCommon.SetVouchAuthority(q4, "UserId", true);
            #endregion
            #region 挂失 q41
            var q41 = from c in Uow.Cards.GetAll()
                      join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      where c.LossDate >= dtBeginDate && c.LossDate <= dtEndDate
                      select new { c.DeptId,c.UserId,dd1s.OrganizationId, c.Id, Status = "挂失", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q41 = q41.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 补卡 q42
            var q42 = from c in Uow.Cards.GetAll()
                      join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      where c.AddDate >= dtBeginDate && c.AddDate <= dtEndDate
                      select new { c.DeptId, c.UserId, dd1s.OrganizationId, c.Id, Status = "补卡", c.Balance };
            if (rm.DeptId.HasValue)
            {
                q42 = q42.Where(w => w.DeptId == rm.DeptId.Value);
            }
            #endregion
            #region 正常在用 q43
            var q43 = from c in Uow.Cards.GetAll()
                      join d1 in Uow.Depts.GetAll() on c.DeptId equals d1.DeptId into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      where c.Status == 0
                      select new { c.DeptId, c.UserId, dd1s.OrganizationId, c.Id, Status = "正常在用", c.Balance };
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
                         Count = 1,
                         q.DeptId,
                         q.UserId,
                         q.OrganizationId,
                     };
            var q71 = businessCommon.SetVouchAuthority(q7, "UserId", true);
            var q72 = q71
                .GroupBy("new(it.Status)", "it")
                .Select("new (it.key.Status,it.Sum(Balance) as Balance,it.Sum(Point) as Point,it.Sum(Fee) as Fee,it.Count() as Count)")
                .ToList<WRReport6Card>();
            //var q71 = from q in q7
            //          group q by q.Status into g
            //          select new WRReport6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            //var q72 = q71.ToList();
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
                         Count = 1,
                         q.DeptId,
                         q.UserId,
                         q.OrganizationId,
                     };
            var q81 = businessCommon.SetVouchAuthority(q8, "UserId", true);
            var q82 = q81
                .GroupBy("new(it.Status)", "it")
                .Select("new (it.key.Status,it.Sum(Balance) as Balance,it.Sum(Point) as Point,it.Sum(Fee) as Fee,it.Count() as Count)")
                .ToList<WRReport6Card>();
            //var q81 = from q in q8
            //          group q by q.Status into g
            //          select new WRReport6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            //var q82 = q81.ToList();
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
                         Count = 1,
                         q.DeptId,
                         q.UserId,
                         q.OrganizationId,
                     };
            var q91 = businessCommon.SetVouchAuthority(q9, "UserId", true);
            var q92 = q91
                .GroupBy("new(it.Status)", "it")
                .Select("new (it.key.Status,it.Sum(Balance) as Balance,it.Sum(Point) as Point,it.Sum(Fee) as Fee,it.Count() as Count)")
                .ToList<WRReport6Card>();
            //var q91 = from q in q9
            //          group q by q.Status into g
            //          select new WRReport6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            //var q92 = q91.ToList();
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
                          Count = 1,
                          q.DeptId,
                          q.UserId,
                          q.OrganizationId,
                      };
            //var q101 = from q in q10
            //           group q by q.Status into g
            //           select new WRReport6Card() { Status = g.Key, Balance = g.Sum(s => s.Balance), Point = g.Sum(s => s.Point), Fee = g.Sum(s => s.Fee), Count = g.Sum(s => s.Count) };
            //var q102 = q101.ToList();
            var q101 = businessCommon.SetVouchAuthority(q10, "UserId", true);
            var q102 = q101
                .GroupBy("new(it.Status)", "it")
                .Select("new (it.key.Status,it.Sum(Balance) as Balance,it.Sum(Point) as Point,it.Sum(Fee) as Fee,it.Count() as Count)")
                .ToList<WRReport6Card>();
            #endregion
            var q110 = q72.Union(q82).Union(q92).Union(q102);
            rm.result.Card = q110.ToList();

            Session["WRReport6Model"] = rm;
            return RedirectToAction("WRReport6");
        }
        #endregion

        #region WRReport7 收银查询
        [HttpGet]
        public ActionResult WRReport7(int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "CreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            if (Session["WRReport7Model"] != null)
            {
                WRReport7Model rm = Session["WRReport7Model"] as WRReport7Model;
                if (page.HasValue || !string.IsNullOrEmpty(sort))
                {
                    rm.result2 = rm.result.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();
                }
                return PartialView(rm);
            }
            else
            {
                WRReport7Model rm = new WRReport7Model();
                rm.result = new List<WRReport7Result>();
                rm.result2 = new List<WRReport7Result>();
                rm.Sum = 0;
                rm.Cash = 0;
                rm.Bank = 0;
                rm.Other = 0;
                rm.SumConsume = 0;
                rm.CardConsume = 0;
                rm.CashConsume = 0;
                rm.BankConsume = 0;
                rm.VoucherConsume = 0;
                rm.OtherConsume = 0;
                rm.DiscountCardConsume = 0;
                rm.CardQuantity = 0;
                rm.CashCardQuantity = 0;
                rm.BankCardQuantity = 0;
                rm.OtherCardQuantity = 0;
                rm.CardAmount = 0;
                rm.CashCardAmount = 0;
                rm.BankCardAmount = 0;
                rm.OtherCardAmount = 0;
                rm.RechargeQuantity = 0;
                rm.CashRechargeQuantity = 0;
                rm.BankRechargeQuantity = 0;
                rm.OtherRechargeQuantity = 0;
                rm.RechargeAmount = 0;
                rm.CashRechargeAmount = 0;
                rm.BankRechargeAmount = 0;
                rm.OtherRechargeAmount = 0;
                rm.AvgPrice = 0;
                rm.SumQuantity = 0;
                rm.AddCardQuantity = 0;
                rm.AddCardAmount = 0;
                rm.Sum1 = 0;
                rm.Sum2 = 0;
                rm.Sum3 = 0;
                rm.ComeQuantity = 0;
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return PartialView(rm);
            }
        }
        private List<WRReport7Result> WRReport7_GetData(WRReport7Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            #region 会员卡消费q11
            var q1 = from cl in Uow.Consume.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()
                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()
                     where cl.ConsumeType == 0 &&
                     cl.CreateDate >= dtBeginDate && cl.CreateDate < dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         clds.OrganizationId,
                         cl.UserId,
                         cl.OperatorsOnDuty,
                         clds.DeptName,
                         cus.FullName,
                         Voucher = cl.PayVoucher,
                         Amount = cl.Amount,
                         CreateDate = DbFunctions.TruncateTime(cl.CreateDate).Value
                     };
            if (rm.DeptId.HasValue)
            {
                q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            }
            var q11 = from q in q1
                      group q by new { q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId=g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = 0,
                          Cash = 0,
                          Bank = 0,
                          Other = 0,
                          SumConsume = g.Sum(s => s.Amount + s.Voucher),
                          CardConsume = g.Sum(s => s.Amount),
                          CashConsume = 0,
                          BankConsume = 0,
                          VoucherConsume = g.Sum(s => s.Voucher),
                          OtherConsume = 0,
                          DiscountCardConsume = 0,
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };            
            #endregion

            #region 打折卡消费q11
            var q9 = from cl in Uow.Consume.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()
                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()
                     join p in Uow.PayTypes.GetAll() on cl.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()
                     where cl.ConsumeType == 3 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         cl.UserId,
                         clds.OrganizationId,
                         cl.OperatorsOnDuty,
                         clds.DeptName,
                         cus.FullName,
                         Amount = cl.Amount,
                         Voucher = cl.PayVoucher,
                         CreateDate = DbFunctions.TruncateTime(cl.CreateDate).Value,
                         cps.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q9 = q9.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q9 = q9.Where(w => w.UserId == rm.UserId.Value);
            }

            var q91 = from q in q9
                      group q by new { q.PayType, q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Amount),
                          Cash = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          Bank = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          Other = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          SumConsume = g.Sum(s => s.Amount + s.Voucher),
                          CardConsume = 0,
                          CashConsume = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          BankConsume = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          VoucherConsume = (g.Key.PayType == 2 ? g.Sum(s => s.Amount) : 0) + g.Sum(s => s.Voucher),
                          OtherConsume = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          DiscountCardConsume = g.Sum(s => s.Amount),
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };
            #endregion

            #region 非会员消费
            var q2 = from cl in Uow.Consume.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()
                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()
                     join p in Uow.PayTypes.GetAll() on cl.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()
                     where cl.ConsumeType == 1 && cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         cl.UserId,
                         clds.OrganizationId,
                         cl.OperatorsOnDuty,
                         clds.DeptName,
                         cus.FullName,
                         cl.Amount,
                         Voucher = cl.PayVoucher,
                         CreateDate = DbFunctions.TruncateTime(cl.CreateDate).Value,
                         cps.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q2 = q2.Where(w => w.UserId == rm.UserId.Value);
            }

            var q21 = from q in q2
                      group q by new { q.PayType, q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Amount),
                          Cash = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          Bank = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          Other = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          SumConsume = g.Sum(s => s.Amount + s.Voucher),
                          CardConsume = 0,
                          CashConsume = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          BankConsume = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          VoucherConsume = (g.Key.PayType == 2 ? g.Sum(s => s.Amount) : 0) + g.Sum(s => s.Voucher),
                          OtherConsume = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          DiscountCardConsume = 0,
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };
            #endregion

            #region 办卡(张，元)
            var q6 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()
                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()
                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()
                     where r.CreateDate >= dtBeginDate && r.CreateDate < dtEndDate &&
                     r.RechargeType == 2
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.OrganizationId,
                         r.OperatorsOnDuty,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         rps.PayType,
                         Quantity = 1,
                     };
            if (rm.DeptId.HasValue)
            {
                q6 = q6.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q6 = q6.Where(w => w.UserId == rm.UserId.Value);
            }

            var q61 = from q in q6
                      group q by new { q.PayType, q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Amount),
                          Cash = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          Bank = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          Other = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CashConsume = 0,
                          BankConsume = 0,
                          VoucherConsume = 0,
                          OtherConsume = 0,
                          DiscountCardConsume = 0,
                          CardQuantity = g.Sum(s => s.Quantity),
                          CashCardQuantity = g.Key.PayType == 0 ? g.Sum(s => s.Quantity) : 0,
                          BankCardQuantity = g.Key.PayType == 1 ? g.Sum(s => s.Quantity) : 0,
                          OtherCardQuantity = g.Key.PayType == 99 ? g.Sum(s => s.Quantity) : 0,
                          CardAmount = g.Sum(s => s.Amount),
                          CashCardAmount = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          BankCardAmount = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          OtherCardAmount = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };

            #endregion

            #region 补卡(张，元)
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
                         rds.OrganizationId,
                         r.OperatorsOnDuty,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         Quantity = 1,
                     };
            if (rm.DeptId.HasValue)
            {
                q8 = q8.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q8 = q8.Where(w => w.UserId == rm.UserId.Value);
            }

            var q81 = from q in q8
                      group q by new { q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Amount),
                          Cash = g.Sum(s => s.Amount),
                          Bank = 0,
                          Other = 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CashConsume = 0,
                          BankConsume = 0,
                          VoucherConsume = 0,
                          OtherConsume = 0,
                          DiscountCardConsume = 0,
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = g.Sum(s => s.Quantity),
                          AddCardAmount = g.Sum(s => s.Amount),
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };
            #endregion

            #region 会员卡充值
            var q3 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 0
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.OrganizationId,
                         r.OperatorsOnDuty,
                         rds.DeptName,
                         rus.FullName,
                         r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         rps.PayType,
                         Quantity = 1,
                     };
            if (rm.DeptId.HasValue)
            {
                q3 = q3.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q3 = q3.Where(w => w.UserId == rm.UserId.Value);
            }

            var q31 = from q in q3
                      group q by new { q.PayType, q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Amount),
                          Cash = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          Bank = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          Other = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CashConsume = 0,
                          BankConsume = 0,
                          VoucherConsume = 0,
                          OtherConsume = 0,
                          DiscountCardConsume = 0,
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = g.Sum(s => s.Quantity),
                          CashRechargeQuantity = g.Key.PayType == 0 ? g.Sum(s => s.Quantity) : 0,
                          BankRechargeQuantity = g.Key.PayType == 1 ? g.Sum(s => s.Quantity) : 0,
                          OtherRechargeQuantity = g.Key.PayType == 99 ? g.Sum(s => s.Quantity) : 0,
                          RechargeAmount = g.Sum(s => s.Amount),
                          CashRechargeAmount = g.Key.PayType == 0 ? g.Sum(s => s.Amount) : 0,
                          BankRechargeAmount = g.Key.PayType == 1 ? g.Sum(s => s.Amount) : 0,
                          OtherRechargeAmount = g.Key.PayType == 99 ? g.Sum(s => s.Amount) : 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = 0,
                          AvgPrice = 0,
                          ComeQuantity = 0,
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
                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     select new
                     {
                         cl.Id,
                         cl.DeptId,
                         cl.UserId,
                         clds.OrganizationId,
                         clds.DeptName,
                         cus.FullName,
                         clcs.OperatorsOnDuty,
                         cl.Quantity,
                         CreateDate = DbFunctions.TruncateTime(cl.CreateDate).Value,
                     };
            if (rm.DeptId.HasValue)
            {
                q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q5 = q5.Where(w => w.UserId == rm.UserId.Value);
            }
            var q51 = from q in q5
                      group q by new { q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = 0,
                          Cash = 0,
                          Bank = 0,
                          Other = 0,
                          SumConsume = 0,
                          CardConsume = 0,
                          CashConsume = 0,
                          BankConsume = 0,
                          VoucherConsume = 0,
                          OtherConsume = 0,
                          DiscountCardConsume = 0,
                          CardQuantity = 0,
                          CashCardQuantity = 0,
                          BankCardQuantity = 0,
                          OtherCardQuantity = 0,
                          CardAmount = 0,
                          CashCardAmount = 0,
                          BankCardAmount = 0,
                          OtherCardAmount = 0,
                          RechargeQuantity = 0,
                          CashRechargeQuantity = 0,
                          BankRechargeQuantity = 0,
                          OtherRechargeQuantity = 0,
                          RechargeAmount = 0,
                          CashRechargeAmount = 0,
                          BankRechargeAmount = 0,
                          OtherRechargeAmount = 0,
                          AddCardQuantity = 0,
                          AddCardAmount = 0,
                          SumQuantity = g.Sum(s => s.Quantity),
                          AvgPrice = 0,
                          ComeQuantity = 0,
                      };
            #endregion

            #region 到店人数
            var q10 = from d in Uow.OrderDishesHis.GetAll()
                      join u in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals u.UserId into cu
                      from cus in cu.DefaultIfEmpty()
                      join d in Uow.Depts.GetAll() on d.DeptId equals d.DeptId into cld
                      from clds in cld.DefaultIfEmpty()
                      where d.Status == (int)DXInfo.Models.OrderDishStatus.Checkouted &&
                      d.CreateDate >= dtBeginDate && d.CreateDate <= dtEndDate
                      select new
                      {
                          d.Id,
                          clds.DeptId,
                          cus.UserId,
                          clds.OrganizationId,
                          clds.DeptName,
                          cus.FullName,
                          Quantity = d.Quantity,
                          CreateDate = DbFunctions.TruncateTime(d.CreateDate).Value,
                      };
            if (rm.DeptId.HasValue)
            {
                q10 = q10.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q10 = q10.Where(w => w.UserId == rm.UserId.Value);
            }
            var q101 = from q in q10
                       group q by new { q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId } into g
                       select new WRReport7Result()
                       {
                           Id = Guid.NewGuid(),
                           CreateDate = g.Key.CreateDate,
                           DeptName = g.Key.DeptName,
                           FullName = g.Key.FullName,
                           DeptId = g.Key.DeptId,
                           UserId = g.Key.UserId,
                           OrganizationId = g.Key.OrganizationId,
                           OperatorsOnDuty = "",
                           Sum = 0,
                           Cash = 0,
                           Bank = 0,
                           Other = 0,
                           SumConsume = 0,
                           CardConsume = 0,
                           CashConsume = 0,
                           BankConsume = 0,
                           VoucherConsume = 0,
                           OtherConsume = 0,
                           DiscountCardConsume = 0,
                           CardQuantity = 0,
                           CashCardQuantity = 0,
                           BankCardQuantity = 0,
                           OtherCardQuantity = 0,
                           CardAmount = 0,
                           CashCardAmount = 0,
                           BankCardAmount = 0,
                           OtherCardAmount = 0,
                           RechargeQuantity = 0,
                           CashRechargeQuantity = 0,
                           BankRechargeQuantity = 0,
                           OtherRechargeQuantity = 0,
                           RechargeAmount = 0,
                           CashRechargeAmount = 0,
                           BankRechargeAmount = 0,
                           OtherRechargeAmount = 0,
                           AddCardQuantity = 0,
                           AddCardAmount = 0,
                           SumQuantity = 0,
                           AvgPrice = 0,
                           ComeQuantity = g.Sum(s => s.Quantity),
                       };
            #endregion
            var qa = q11.Union(q21).Union(q31).Union(q51).Union(q61).Union(q81).Union(q91).Union(q101);

            var qa1 = from q in qa
                      group q by new { q.CreateDate, q.DeptName, q.FullName, q.DeptId, q.UserId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport7Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          DeptId = g.Key.DeptId,
                          UserId = g.Key.UserId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          Sum = g.Sum(s => s.Sum),
                          Cash = g.Sum(s => s.Cash),
                          Bank = g.Sum(s => s.Bank),
                          Other = g.Sum(s => s.Other),
                          SumConsume = g.Sum(s => s.SumConsume),
                          CardConsume = g.Sum(s => s.CardConsume),
                          CashConsume = g.Sum(s => s.CashConsume),
                          BankConsume = g.Sum(s => s.BankConsume),
                          VoucherConsume = g.Sum(s => s.VoucherConsume),
                          OtherConsume = g.Sum(s => s.OtherConsume),
                          DiscountCardConsume = g.Sum(s => s.DiscountCardConsume),
                          CardQuantity = g.Sum(s => s.CardQuantity),
                          CashCardQuantity = g.Sum(s => s.CashCardQuantity),
                          BankCardQuantity = g.Sum(s => s.BankCardQuantity),
                          OtherCardQuantity = g.Sum(s => s.OtherCardQuantity),
                          CardAmount = g.Sum(s => s.CardAmount),
                          CashCardAmount = g.Sum(s => s.CashCardAmount),
                          BankCardAmount = g.Sum(s => s.BankCardAmount),
                          OtherCardAmount = g.Sum(s => s.OtherCardAmount),
                          RechargeQuantity = g.Sum(s => s.RechargeQuantity),
                          CashRechargeQuantity = g.Sum(s => s.CashRechargeQuantity),
                          BankRechargeQuantity = g.Sum(s => s.BankRechargeQuantity),
                          OtherRechargeQuantity = g.Sum(s => s.OtherRechargeQuantity),
                          RechargeAmount = g.Sum(s => s.RechargeAmount),
                          CashRechargeAmount = g.Sum(s => s.CashRechargeAmount),
                          BankRechargeAmount = g.Sum(s => s.BankRechargeAmount),
                          OtherRechargeAmount = g.Sum(s => s.OtherRechargeAmount),
                          AddCardQuantity = g.Sum(s => s.AddCardQuantity),
                          AddCardAmount = g.Sum(s => s.AddCardAmount),
                          SumQuantity = g.Sum(s => s.SumQuantity),
                          AvgPrice = g.Sum(s => s.SumQuantity) > 0 ? Math.Round(g.Sum(s => s.SumConsume) / g.Sum(s => s.SumQuantity), 2) : 0,
                          ComeQuantity = g.Sum(s => s.ComeQuantity),
                      };
            var qa2 = businessCommon.SetVouchAuthority(qa1, "UserId", true);
            var qa3 = qa2.ToList<WRReport7Result>();
            return qa3;
        }
        [HttpPost]
        public ActionResult WRReport7(WRReport7Model rm, int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "CreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);
            //IQueryable<WRReport7Result> qa2 = WRReport7_GetData(rm);
            List<WRReport7Result> qa1 = WRReport7_GetData(rm);//qa2.ToList();
            rm.Sum = qa1.Sum(s => (decimal?)s.Sum);
            rm.Cash = qa1.Sum(s => (decimal?)s.Cash);
            rm.Bank = qa1.Sum(s => (decimal?)s.Bank);
            rm.Other = qa1.Sum(s => (decimal?)s.Other);
            rm.SumConsume = qa1.Sum(s => (decimal?)s.SumConsume ?? 0);
            rm.CardConsume = qa1.Sum(s => (decimal?)s.CardConsume);
            rm.CashConsume = qa1.Sum(s => (decimal?)s.CashConsume);
            rm.BankConsume = qa1.Sum(s => (decimal?)s.BankConsume);
            rm.VoucherConsume = qa1.Sum(s => (decimal?)s.VoucherConsume);
            rm.OtherConsume = qa1.Sum(s => (decimal?)s.OtherConsume);
            rm.DiscountCardConsume = qa1.Sum(s => (decimal?)s.DiscountCardConsume);
            rm.CardQuantity = qa1.Sum(s => (int?)s.CardQuantity);
            rm.CashCardQuantity = qa1.Sum(s => (int?)s.CashCardQuantity);
            rm.BankCardQuantity = qa1.Sum(s => (int?)s.BankCardQuantity);
            rm.OtherCardQuantity = qa1.Sum(s => (int?)s.OtherCardQuantity);
            rm.CardAmount = qa1.Sum(s => (int?)s.CardAmount);
            rm.CashCardAmount = qa1.Sum(s => (int?)s.CashCardAmount);
            rm.BankCardAmount = qa1.Sum(s => (int?)s.BankCardAmount);
            rm.OtherCardAmount = qa1.Sum(s => (int?)s.OtherCardAmount);
            rm.RechargeQuantity = qa1.Sum(s => (int?)s.RechargeQuantity);
            rm.CashRechargeQuantity = qa1.Sum(s => (int?)s.CashRechargeQuantity);
            rm.BankRechargeQuantity = qa1.Sum(s => (int?)s.BankRechargeQuantity);
            rm.OtherRechargeQuantity = qa1.Sum(s => (int?)s.OtherRechargeQuantity);
            rm.RechargeAmount = qa1.Sum(s => (int?)s.RechargeAmount);
            rm.CashRechargeAmount = qa1.Sum(s => (int?)s.CashRechargeAmount);
            rm.BankRechargeAmount = qa1.Sum(s => (int?)s.BankRechargeAmount);
            rm.OtherRechargeAmount = qa1.Sum(s => (int?)s.OtherRechargeAmount);
            rm.AddCardQuantity = qa1.Sum(s => (int?)s.AddCardQuantity);
            rm.AddCardAmount = qa1.Sum(s => (decimal?)s.AddCardAmount);
            rm.AvgPrice = qa1.Average(a => (decimal?)a.AvgPrice);
            rm.SumQuantity = qa1.Sum(s => (decimal?)s.SumQuantity);
            rm.ComeQuantity = qa1.Sum(s => (decimal?)s.ComeQuantity);
            rm.Sum1 = qa1.Sum(s => (decimal?)(s.CardAmount + s.RechargeAmount));
            rm.Sum2 = qa1.Sum(s => (decimal?)s.SumConsume);
            rm.Sum3 = qa1.Sum(s => (decimal?)(s.CardAmount + s.RechargeAmount - s.CardConsume));
            rm.rowCount = qa1.Count;
            rm.result = qa1;
            rm.result2 = qa1.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();

            Session["WRReport7Model"] = rm;
            return RedirectToAction("WRReport7");
        }
        public void WRReport7ExportToExcel()
        {
            if (Session["WRReport7Model"] == null) return;
            var m = Session["WRReport7Model"] as WRReport7Model;
            object dataSource = m.result;
            string fileName = "收银查询.xls";

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

            if (isOperatorsOnDuty)
            {
                field = new BoundField();
                field.DataField = "OperatorsOnDuty";
                field.HeaderText = "当班操作员";
                view.Columns.Add(field);
            }

            field = new BoundField();
            field.DataField = "Sum";
            field.HeaderText = "资金总收入";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Cash";
            field.HeaderText = "实收现金";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Bank";
            field.HeaderText = "实收银行卡";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Other";
            field.HeaderText = "实收其它";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumConsume";
            field.HeaderText = "销售收入";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardConsume";
            field.HeaderText = "会员卡消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CashConsume";
            field.HeaderText = "现金消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "BankConsume";
            field.HeaderText = "银行卡消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "VoucherConsume";
            field.HeaderText = "代金券消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OtherConsume";
            field.HeaderText = "其它消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "DiscountCardConsume";
            field.HeaderText = "打折卡消费";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardQuantity";
            field.HeaderText = "办卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CashCardQuantity";
            field.HeaderText = "现金办卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "BankCardQuantity";
            field.HeaderText = "银行卡办卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OtherCardQuantity";
            field.HeaderText = "其它办卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CardAmount";
            field.HeaderText = "办卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CashCardAmount";
            field.HeaderText = "现金办卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "BankCardAmount";
            field.HeaderText = "银行卡办卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OtherCardAmount";
            field.HeaderText = "其它办卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RechargeQuantity";
            field.HeaderText = "充值（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CashRechargeQuantity";
            field.HeaderText = "现金充值（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "BankRechargeQuantity";
            field.HeaderText = "银行卡充值（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OtherRechargeQuantity";
            field.HeaderText = "其它充值（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RechargeAmount";
            field.HeaderText = "充值（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "CashRechargeAmount";
            field.HeaderText = "现金充值（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "BankRechargeAmount";
            field.HeaderText = "银行卡充值（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OtherRechargeAmount";
            field.HeaderText = "其它充值（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddCardQuantity";
            field.HeaderText = "补卡（张）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AddCardAmount";
            field.HeaderText = "补卡（元）";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumQuantity";
            field.HeaderText = "数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "AvgPrice";
            field.HeaderText = "平均单价";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "ComeQuantity";
            field.HeaderText = "到店人数";
            view.Columns.Add(field);

            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }

        #endregion

        #region WRReport8 收款差异查询
        public ActionResult WRReport8()
        {
            if (Session["WRReport8Model"] != null)
            {
                WRReport8Model rm = Session["WRReport8Model"] as WRReport8Model;
                return PartialView(rm);
            }
            else
            {
                WRReport8Model rm = new WRReport8Model();
                rm.result = new List<WRReport8Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 00:00";
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport8(WRReport8Model rm)
        {

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);

            var q1 = from q in Uow.CheckDifferences.GetAll()
                     join d in Uow.Depts.GetAll() on q.DeptId equals d.DeptId into qd
                     from qds in qd.DefaultIfEmpty()
                     join u in Uow.aspnet_CustomProfile.GetAll() on q.UserId equals u.UserId into qu
                     from qus in qu.DefaultIfEmpty()
                     where q.DifDate >= dtBeginDate && q.DifDate <= dtEndDate
                     select new WRReport8Result()
                     {
                         UserId=q.UserId,
                         DeptId=q.DeptId,
                         OrganizationId = qds.OrganizationId,
                         DeptName = qds.DeptName,
                         FullName =qus.FullName,
                         DifDate = q.DifDate,
                         Amount = q.Amount,
                         More = q.More,
                         Less = q.Less,
                         MoreDay = q.More > 0 ? 1 : 0,
                         LessDay = q.Less > 0 ? 1 : 0,
                         NormalDay = q.More == 0 && q.Less == 0 ? 1 : 0,
                         Comment=q.Comment,
                     };
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            var q11 = businessCommon.SetVouchAuthority(q1, "UserId", true);
            var q3 = q11.ToList<WRReport8Result>();
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
                         select new WRReport8Result()
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
            Session["WRReport8Model"] = rm;
            return RedirectToAction("WRReport8");
        }
        public void WRReport8ExportToExcel()
        {
            if (Session["WRReport8Model"] == null) return;
            var m = Session["WRReport8Model"] as WRReport8Model;
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

        #region WRReport9 销售曲线图
        [Authorize]
        public ActionResult WRReport9(string year, string month, bool? IsCup)
        {
            WRReport9Model r9 = new WRReport9Model();
            r9.year = year;
            r9.month = month;
            return PartialView(r9);
        }
        public void MyChart(string year, string month)
        {
            if (string.IsNullOrEmpty(year))
            {
                var chart2 = new Chart(900, 400, ChartTheme.Blue);
                chart2.AddTitle(year + "年" + month + "月" + "各分店销售额日走势（元）");
                chart2.Write("png");
                return;
            }
            if (!string.IsNullOrEmpty(month))
            {
                int iy = Convert.ToInt32(year);
                int im = Convert.ToInt32(month);
                var q = from c in Uow.Consume.GetAll().Where(w => w.CreateDate.Year == iy).Where(w => w.CreateDate.Month == im)
                        join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                        from cds in cd.DefaultIfEmpty()
                        group new { c, cds } by new { c.UserId, c.DeptId, cds.OrganizationId, cds.DeptCode, cds.DeptName, c.CreateDate.Day } into g
                        select new
                        {
                            g.Key.UserId,
                            g.Key.DeptId,
                            g.Key.OrganizationId,
                            g.Key.DeptCode,
                            g.Key.DeptName,
                            g.Key.Day,
                            Amount = g.Sum(s => s.c.Amount)
                        };

                var q1 = businessCommon.SetVouchAuthority(q, "UserId", true);
                var q2 = q1.GroupBy("new(it.DeptName,it.DeptCode,it.Day)", "it").Select("new(it.key.DeptName,it.key.DeptCode,it.key.Day,it.Sum(Amount) as Amount)").ToList<Temp1Class>();
                var q3 = q2.OrderBy(o => o.DeptCode).OrderBy(o => o.Day).ToList();
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
                        group new { c, cds } 
                        by new { c.UserId, c.DeptId, cds.OrganizationId, cds.DeptCode, cds.DeptName, c.CreateDate.Month } into g
                        select new
                        {
                            g.Key.UserId,
                            g.Key.DeptId,
                            g.Key.OrganizationId,
                            g.Key.DeptCode,
                            g.Key.DeptName,
                            g.Key.Month,
                            Amount = g.Sum(s => s.c.Amount)
                        };
                var q1 = businessCommon.SetVouchAuthority(q, "UserId", true);
                var q2 = q1.GroupBy("new(it.DeptName,it.DeptCode,it.Month)", "it").Select("new(it.key.DeptName,it.key.DeptCode,it.key.Month,it.Sum(Amount) as Amount)").ToList<Temp2Class>();
                var q3 = q2.OrderBy(o => o.DeptCode).OrderBy(o => o.Month).ToList();

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
        #endregion

        #region WRReport12 长短款设置
        public ActionResult WRReport12()
        {
            if (Session["WRReport12Model"] != null)
            {
                WRReport12Model rm = Session["WRReport12Model"] as WRReport12Model;
                return PartialView(rm);
            }
            else
            {
                WRReport12Model rm = new WRReport12Model();
                rm.result = new List<WRReport12Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport12(string button, WRReport12Model rm)
        {
            if (button == "删除")
            {
                if (rm.curResult != null)
                {
                    if (rm.curResult.IsIn != 1)
                    {
                        ModelState.AddModelError("", "不能删除长短款设置");
                        return PartialView(rm);
                    }
                    DXInfo.Models.CheckDifferences cd = Uow.CheckDifferences.GetById(g => g.Id == rm.curResult.Id);
                    if (cd != null)
                    {
                        Uow.CheckDifferences.Delete(cd);
                        Uow.Commit();
                    }
                }
            }
            if (button == "修改")
            {
                if (rm.curResult != null)
                {
                    if (rm.curResult.IsIn == 1)
                    {
                        ModelState.AddModelError("", "不能修改长短款设置");
                        return PartialView(rm);
                    }
                    MembershipUser user = Membership.GetUser();
                    Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
                    var dept = Uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == userId).FirstOrDefault();
                    if (dept == null || !dept.DeptId.HasValue || dept.DeptId == Guid.Empty)
                    {
                        ModelState.AddModelError("", "请设置操作员部门信息");
                        return PartialView(rm);
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
                    Uow.Commit();
                }
            }
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);

            var q1 = from cl in Uow.Consume.GetAll() 
                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate
                     && cl.ConsumeType != 0
                     select new WRReport121Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         CreateDate = DbFunctions.TruncateTime(cl.CreateDate).Value,
                         Amount = cl.Amount,
                         OrganizationId=clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);

            Guid gCategory = Guid.Parse("A33C9E98-9781-4C5A-983A-2A76FEBD2660");
            Guid gInventory = Guid.Parse("17516BE0-CCF2-4A58-BF2D-8C762BD8A8C4");

            var q5 = from r in Uow.Recharges.GetAll()
                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()
                     where r.RechargeType != 1 && r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate
                     select new WRReport121Result()
                     {
                         Id = r.Id,
                         DeptId = r.DeptId,
                         DeptName = cds.DeptName,
                         UserId = r.UserId,
                         FullName = clus.FullName,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         Amount = r.Amount,
                         OrganizationId = cds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q5 = q5.Where(w => w.UserId == rm.UserId.Value);

            var q3 = businessCommon.SetVouchAuthority(q1.Union(q5), "UserId", true).ToList<WRReport121Result>();

            var q4 = from q in q3
                     group q by new { q.CreateDate, q.DeptId, q.DeptName, q.UserId, q.FullName,q.OrganizationId } into g
                     select new WRReport12Result()
                     {
                         DifDate = g.Key.CreateDate,
                         DeptId = g.Key.DeptId,
                         DeptName = g.Key.DeptName,
                         UserId = g.Key.UserId,
                         FullName = g.Key.FullName,
                         OrganizationId = g.Key.OrganizationId,
                         Amount = g.Sum(s => s.Amount),
                         IsIn = 0,
                     };

            var q6 = from q in Uow.CheckDifferences.GetAll()
                     join d in Uow.Depts.GetAll() on q.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on q.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     where q.DifDate >= dtBeginDate.Date && q.DifDate <= dtEndDate.Date
                     select new WRReport12Result()
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
                         OrganizationId = cds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q6 = q6.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q6 = q6.Where(w => w.UserId == rm.UserId.Value);

            var q7 = q4.ToList();
            var q8 = businessCommon.SetVouchAuthority(q6, "UserId", true).ToList<WRReport12Result>();
            foreach (WRReport12Result r12 in q8)
            {
                q7.RemoveAll(delegate(WRReport12Result r121) { return r121.DifDate == r12.DifDate && r121.DeptId == r12.DeptId && r121.UserId == r12.UserId; });
            }
            var q9 = q7.Union(q8).ToList();
            rm.result = q9;
            Session["WRReport12Model"] = rm;
            return RedirectToAction("WRReport12");
        }
        #endregion

        #region WRReport13 时段销量统计
        public ActionResult WRReport13(Guid? deptId, Guid? userId, Guid? category, Guid? inventory, DateTime? beginDate, DateTime? endDate)
        {
            WRReport13Model r13 = new WRReport13Model();
            r13.deptId = deptId;
            r13.userId = userId;
            r13.category = category;
            r13.inventory = inventory;
            r13.beginDate = beginDate.HasValue ? beginDate.Value.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
            r13.endDate = endDate.HasValue ? endDate.Value.ToString("yyyy-MM-dd HH:mm") : DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";

            return PartialView(r13);
        }
        public void MyChart13(Guid? deptId, Guid? userId, Guid? category, Guid? inventory, DateTime beginDate, DateTime endDate)
        {

            var q = from c in Uow.ConsumeList.GetAll()
                    join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()
                    join y in Uow.Inventory.GetAll() on c.Inventory equals y.Id into cy
                    from cys in cy.DefaultIfEmpty()

                    where c.CreateDate >= beginDate && c.CreateDate <= endDate
                    select new
                    {
                        Amount = c.Quantity,
                        cds.DeptCode,
                        cds.DeptName,
                        c.CreateDate.Hour,
                        c.DeptId,
                        c.UserId,
                        Category = cys == null ? Guid.Empty : cys.Category,
                        c.Inventory,
                        cds.OrganizationId
                    };
            if (deptId.HasValue) q = q.Where(w => w.DeptId == deptId.Value);
            if (userId.HasValue) q = q.Where(w => w.UserId == userId.Value);
            if (category.HasValue) q = q.Where(w => w.Category == category.Value);
            if (inventory.HasValue) q = q.Where(w => w.Inventory == inventory.Value);
            var q1 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<Temp3Class>();

            //var q1 = from r in q.ToList()
            //         select new { r.Amount, r.DeptCode, r.DeptName, Day = r.Hour };

            var q2 = from c in q1
                     select new { c.Amount, c.DeptCode, c.DeptName, Day = c.Hour };
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

        #region WRReport17 收银查询分支付方式
        [HttpGet]
        public ActionResult WRReport17(int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "CreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            if (Session["WRReport17Model"] != null)
            {
                WRReport17Model rm = Session["WRReport17Model"] as WRReport17Model;
                if (page.HasValue || !string.IsNullOrEmpty(sort))
                {
                    rm.result2 = rm.result.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();
                }
                return PartialView(rm);
            }
            else
            {
                WRReport17Model rm = new WRReport17Model();
                rm.result = new List<WRReport17Result>();
                rm.result2 = new List<WRReport17Result>();

                rm.AddCardSum = 0;
                rm.AvgPrice = 0;
                rm.CardRecharge = 0;
                rm.RechargeSum = 0;
                rm.SumAmount = 0;
                rm.SumConsume = 0;
                rm.SumCup = 0;
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return PartialView(rm);
            }
        }
        private List<WRReport17Result> WRReport17_GetData(WRReport17Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);//.AddDays(1);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);
            Guid payType_Card = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid payType_TakeOut = Guid.Parse(DXInfo.Business.Helper.PayType_TakeOut);

            #region 会员卡消费q11
            var q1 = from c in Uow.Consume.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     where c.ConsumeType == 0 &&
                     c.CreateDate >= dtBeginDate &&
                     c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.DeptId,
                         c.UserId,
                         clds.DeptName,
                         cus.FullName,
                         c.OperatorsOnDuty,
                         Amount = c.Amount,
                         PayTypeId=c.PayType,
                         PayTypeName = c.PayType.HasValue ?
                         c.PayType.Value == payType_Card ? "会员卡" :
                         c.PayType.Value == payType_TakeOut ? "会员卡外卖" : "其它" :
                         "会员卡",
                         CreateDate = DbFunctions.TruncateTime(c.CreateDate).Value,
                         clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue)
            {
                q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q1 = q1.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q1 = q1.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q11 = from q in q1
                      group q by new { q.CreateDate, q.DeptName, q.FullName,q.UserId,q.DeptId,q.OrganizationId, q.OperatorsOnDuty, q.PayTypeName } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = 0,
                          SumConsume = g.Sum(s => s.Amount),
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

            #region 打折卡消费q11
            var q9 = from c in Uow.Consume.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on c.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()

                     where c.ConsumeType == 3 &&
                     c.CreateDate >= dtBeginDate &&
                     c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.DeptId,
                         c.UserId,
                         clds.DeptName,
                         cus.FullName,
                         c.OperatorsOnDuty,
                         Amount = c.Amount,
                         CreateDate = DbFunctions.TruncateTime(c.CreateDate).Value,
                         PayTypeName = cps.Name,
                         cps.PayType,
                         clds.OrganizationId,
                         PayTypeId=c.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q9 = q9.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q9 = q9.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q9 = q9.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q91 = from q in q9
                      group q by new { q.PayTypeName, q.PayType, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = "打折卡-" + g.Key.PayTypeName,
                          SumAmount = g.Key.PayType == 2 ? 0 : g.Sum(s => s.Amount),
                          SumConsume = g.Sum(s => s.Amount),
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
            var q2 = from c in Uow.Consume.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on c.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()

                     where c.ConsumeType == 1 &&
                     c.CreateDate >= dtBeginDate &&
                     c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.DeptId,
                         c.UserId,
                         clds.DeptName,
                         cus.FullName,
                         c.OperatorsOnDuty,
                         c.Amount,
                         CreateDate = DbFunctions.TruncateTime(c.CreateDate).Value,
                         PayTypeName = cps.Name,
                         cps.PayType,
                         clds.OrganizationId,
                         PayTypeId = c.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q2 = q2.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q2 = q2.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q2 = q2.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q21 = from q in q2
                      group q by new { q.PayTypeName, q.PayType, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = g.Key.PayType == 2 ? 0 : g.Sum(s => s.Amount),
                          SumConsume = g.Sum(s => s.Amount),
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

            #region 办卡(张，元)
            var q6 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 2
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.OperatorsOnDuty,
                         CardRecharge = r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         PayTypeName = rps.Name,
                         CardCount = 1,
                         rds.OrganizationId,
                         PayTypeId=r.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q6 = q6.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q6 = q6.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q6 = q6.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q61 = from q in q6
                      group q by new { q.PayTypeName, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = g.Sum(s => s.CardRecharge),
                          SumConsume = 0,
                          CardCount = g.Sum(s => s.CardCount),
                          CardRecharge = g.Sum(s => s.CardRecharge),
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0
                      };

            #endregion

            #region 代金券
            var q7 = from c in Uow.Consume.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on c.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()

                     where c.PayVoucher > 0 &&
                     c.CreateDate >= dtBeginDate &&
                     c.CreateDate <= dtEndDate
                     select new
                     {
                         c.Id,
                         c.DeptId,
                         c.UserId,
                         clds.DeptName,
                         cus.FullName,
                         c.OperatorsOnDuty,
                         Amount = c.PayVoucher,
                         CreateDate = DbFunctions.TruncateTime(c.CreateDate).Value,
                         PayTypeName = c.ConsumeType == 0 ?
                         "会员卡-代金券" : c.ConsumeType == 1 ? "非会员-代金券" : c.ConsumeType == 3 ? "打折卡-代金券" : "积分兑换-代金券",
                         cps.PayType,
                         clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue)
            {
                q7 = q7.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q7 = q7.Where(w => w.UserId == rm.UserId.Value);
            }

            var q71 = from q in q7
                      group q by new { q.PayTypeName, q.PayType, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = 0,
                          SumConsume = g.Sum(s => s.Amount),
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

            #region 补卡(张、元)
            var q8 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 3
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.OperatorsOnDuty,
                         r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         //AddCardCount = 1,
                         PayTypeName = rps.Name,
                         rds.OrganizationId,
                         PayTypeId = r.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q8 = q8.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q8 = q8.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q8 = q8.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q81 = from q in q8
                      group q by new { q.PayTypeName, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = g.Sum(s => s.Amount),
                          SumConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,//g.Sum(s => s.AddCardCount),
                          AddCardSum = g.Sum(s => s.Amount),
                      };
            #endregion

            #region 会员卡充值
            var q3 = from r in Uow.Recharges.GetAll()
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                     from rds in rd.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                     from rps in rp.DefaultIfEmpty()

                     where r.CreateDate >= dtBeginDate && r.CreateDate <= dtEndDate && r.RechargeType == 0
                     select new
                     {
                         r.Id,
                         r.UserId,
                         r.DeptId,
                         rds.DeptName,
                         rus.FullName,
                         r.OperatorsOnDuty,
                         r.Amount,
                         CreateDate = DbFunctions.TruncateTime(r.CreateDate).Value,
                         PayTypeName = rps.Name,
                         RechargeCount = 1,
                         rds.OrganizationId,
                         PayTypeId = r.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q3 = q3.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q3 = q3.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q3 = q3.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q31 = from q in q3
                      group q by new { q.PayTypeName, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = g.Sum(s => s.Amount),
                          SumConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = g.Sum(s => s.RechargeCount),
                          RechargeSum = g.Sum(s => s.Amount),
                          SumCup = 0,
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0,
                      };
            #endregion

            #region 总杯数
            var q5 = from c in Uow.Consume.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join p in Uow.PayTypes.GetAll() on c.PayType equals p.Id into clcsp
                     from clcsps in clcsp.DefaultIfEmpty()

                     where c.CreateDate >= dtBeginDate &&
                     c.CreateDate <= dtEndDate

                     select new
                     {
                         c.Id,
                         c.DeptId,
                         c.UserId,
                         clds.DeptName,
                         cus.FullName,
                         c.OperatorsOnDuty,
                         Amount = c.Quantity,
                         CreateDate = DbFunctions.TruncateTime(c.CreateDate).Value,
                         PayTypeName = c.PayType.HasValue ?
                         c.PayType.Value == payType_Card ? "会员卡" :
                         c.PayType.Value == payType_TakeOut ? "会员卡外卖" :
                         clcsps.Name : "会员卡",
                         clds.OrganizationId,
                         PayTypeId = c.PayType,
                     };
            if (rm.DeptId.HasValue)
            {
                q5 = q5.Where(w => w.DeptId == rm.DeptId.Value);
            }
            if (rm.UserId.HasValue)
            {
                q5 = q5.Where(w => w.UserId == rm.UserId.Value);
            }
            if (rm.PayType.HasValue)
            {
                q5 = q5.Where(w => w.PayTypeId == rm.PayType.Value);
            }
            var q51 = from q in q5
                      group q by new { q.PayTypeName, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = 0,
                          SumConsume = 0,
                          CardCount = 0,
                          CardRecharge = 0,
                          RechargeCount = 0,
                          RechargeSum = 0,
                          SumCup = g.Sum(s => s.Amount),
                          AvgPrice = 0,
                          AddCardCount = 0,
                          AddCardSum = 0,
                      };
            #endregion
            var qa = q11.Union(q21).Union(q31).Union(q51).Union(q61).Union(q71).Union(q81).Union(q91);

            var qa1 = from q in qa
                      group q by new { q.PayTypeName, q.CreateDate, q.DeptName, q.FullName, q.UserId, q.DeptId, q.OrganizationId, q.OperatorsOnDuty } into g
                      select new WRReport17Result()
                      {
                          Id = Guid.NewGuid(),
                          CreateDate = g.Key.CreateDate,
                          DeptName = g.Key.DeptName,
                          FullName = g.Key.FullName,
                          UserId = g.Key.UserId,
                          DeptId = g.Key.DeptId,
                          OrganizationId = g.Key.OrganizationId,
                          OperatorsOnDuty = g.Key.OperatorsOnDuty,
                          PayTypeName = g.Key.PayTypeName,
                          SumAmount = g.Sum(s => s.SumAmount),
                          SumConsume = g.Sum(s => s.SumConsume),
                          CardCount = g.Sum(s => s.CardCount),
                          CardRecharge = g.Sum(s => s.CardRecharge),
                          RechargeCount = g.Sum(s => s.RechargeCount),
                          RechargeSum = g.Sum(s => s.RechargeSum),
                          SumCup = g.Sum(s => s.SumCup),
                          AvgPrice = g.Sum(s => s.SumCup) > 0 ? Math.Round(g.Sum(s => s.SumConsume) / g.Sum(s => s.SumCup), 2) : 0,
                          AddCardCount = g.Sum(s => s.AddCardCount),
                          AddCardSum = g.Sum(s => s.AddCardSum),
                      };
            var qa2 = businessCommon.SetVouchAuthority(qa1, "UserId", true).ToList<WRReport17Result>();
            return qa2;
        }
        [HttpPost]
        public ActionResult WRReport17(WRReport17Model rm, int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "CreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);            
            List < WRReport17Result > qa1 = WRReport17_GetData(rm);
            rm.AvgPrice = qa1.Average(a => (decimal?)a.AvgPrice);
            rm.CardCount = qa1.Sum(s => (int?)s.CardCount);
            rm.CardRecharge = qa1.Sum(s => (decimal?)(s.CardRecharge));
            rm.RechargeCount = qa1.Sum(s => (int?)(s.RechargeCount));
            rm.RechargeSum = qa1.Sum(s => (decimal?)(s.RechargeSum));

            rm.SumAmount = qa1.Sum(s => (decimal?)s.SumAmount);
            rm.SumConsume = qa1.Sum(s => (decimal?)s.SumConsume ?? 0);
            rm.SumCup = qa1.Sum(s => (decimal?)s.SumCup);
            rm.AddCardCount = qa1.Sum(s => (int?)s.AddCardCount);
            rm.AddCardSum = qa1.Sum(s => (decimal?)s.AddCardSum);

            rm.rowCount = qa1.Count;
            rm.result = qa1;
            rm.result2 = qa1.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();

            Session["WRReport17Model"] = rm;
            return RedirectToAction("WRReport17");
        }
        public void WRReport17ExportToExcel()
        {
            if (Session["WRReport17Model"] == null) return;
            var m = Session["WRReport17Model"] as WRReport17Model;
            object dataSource = m.result;
            string fileName = "收银查询按支付方式.xls";

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

            if (isOperatorsOnDuty)
            {
                field = new BoundField();
                field.DataField = "OperatorsOnDuty";
                field.HeaderText = "当班操作员";
                view.Columns.Add(field);
            }

            field = new BoundField();
            field.DataField = "PayTypeName";
            field.HeaderText = "支付方式";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SumAmount";
            field.HeaderText = "资金总收入";
            view.Columns.Add(field);


            field = new BoundField();
            field.DataField = "SumConsume";
            field.HeaderText = "销售收入";
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

        #region WRReport18 结算报表
        [HttpGet]
        public ActionResult WRReport18(int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "LocalDeptName";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";
            if (Session["WRReport18Model"] != null)
            {
                WRReport18Model rm = Session["WRReport18Model"] as WRReport18Model;
                return PartialView(rm);
            }
            else
            {
                WRReport18Model rm = new WRReport18Model();
                rm.result = new List<WRReport18Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                return PartialView(rm);
            }

        }
        private IEnumerable<WRReport18Result> WRReport18_GetData(WRReport18Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            Guid deptid = Guid.Empty;
            if (rm.DeptId.HasValue)
            {
                deptid = rm.DeptId.Value;
            }
            var q = Uow.Db.SqlQuery<WRReport18Result>("proc_Balance_Report @BeginDate,@EndDate,@DeptId",
                new SqlParameter("BeginDate", dtBeginDate),
                new SqlParameter("EndDate", dtEndDate),
                new SqlParameter("DeptId", deptid));
            return q;
        }
        [HttpPost]
        public ActionResult WRReport18(WRReport18Model rm, int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "LocalDeptName";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            var qa2 = WRReport18_GetData(rm);
            List<WRReport18Result> qa1 = qa2.ToList();

            rm.rowCount = qa1.Count;
            rm.result = qa1;

            Session["WRReport18Model"] = rm;
            return RedirectToAction("WRReport18");
        }
        public void WRReport18ExportToExcel()
        {
            if (Session["WRReport18Model"] == null) return;
            var m = Session["WRReport18Model"] as WRReport18Model;
            object dataSource = m.result;
            string fileName = "结算报表.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "LocalDeptName";
            field.HeaderText = "发卡门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "RemoteDeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FillFee_Pay";
            field.HeaderText = "充值金额(支出)";
            view.Columns.Add(field);

            //field = new BoundField();
            //field.DataField = "FillProm_Pay";
            //field.HeaderText = "充值赠送金额(支出)";
            //view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Fee_Pay";
            field.HeaderText = "消费金额(支出)";
            view.Columns.Add(field);


            field = new BoundField();
            field.DataField = "sumFee_Pay";
            field.HeaderText = "小计(支出)";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FillFee_Income";
            field.HeaderText = "充值金额(收入)";
            view.Columns.Add(field);

            //field = new BoundField();
            //field.DataField = "FillProm_Income";
            //field.HeaderText = "充值赠送金额(收入)";
            //view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Fee_Income";
            field.HeaderText = "消费金额(收入)";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "sumFee_Income";
            field.HeaderText = "小计(收入)";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "FillFee_Dif";
            field.HeaderText = "充值金额(差额)";
            view.Columns.Add(field);

            //field = new BoundField();
            //field.DataField = "FillProm_Dif";
            //field.HeaderText = "充值赠送金额(差额)";
            //view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Fee_Dif";
            field.HeaderText = "消费金额(差额)";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "sumFee_Dif";
            field.HeaderText = "小计(差额)";
            view.Columns.Add(field);

            view.DataSource = dataSource;
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        [Authorize]
        public ActionResult WRReport18ComputeBalance(WRReport18Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            int count = Uow.Db.ExecuteSqlCommand("proc_balance_bydate @BeginDate,@EndDate", new SqlParameter("BeginDate", dtBeginDate),
                new SqlParameter("EndDate", dtEndDate));

            var qa2 = WRReport18_GetData(rm);
            List<WRReport18Result> qa1 = qa2.ToList();

            rm.rowCount = qa1.Count;
            rm.result = qa1;

            Session["WRReport18Model"] = rm;
            return RedirectToAction("WRReport18");
        }
        #endregion

        #region WRReport19 会员卡充值消费明细
        private void SetupWRReport19GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WRReport19_RequestData");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.DataType = "local";
        }
        public ActionResult WRReport19_RequestData()
        {
            var gridModel = new WRReport19GridModel();
            SetupWRReport19GridModel(gridModel.WRReport19Grid);
            string type = DXInfo.Models.NameCodeType.BillType.ToString();
            var q = from d in Uow.Bills.GetAll()
                    join d1 in Uow.NameCode.GetAll().Where(w => w.Type == type)
                    on d.BillType equals d1.Code into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        d.Id,
                        d.CardNo,
                        d.MemberName,
                        BillTypeName = dd1s.Name,
                        d.LastBalance,
                        d.Sum,
                        d.Discount,
                        d.Amount,
                        d.Balance,
                        d.DeptName,
                        d.FullName,
                        d.CreateDate,
                        d.PayTypeName,
                    };
            return QueryAndExcel(gridModel.WRReport19Grid, q, "会员卡充值消费明细.xls");
        }
        public ActionResult WRReport19()
        {
            var gridModel = new WRReport19GridModel();
            SetupWRReport19GridModel(gridModel.WRReport19Grid);
            return PartialView(gridModel);
        }
        #endregion
        #endregion

        #region 冷饮店报表
        #region Report2 消费明细查询
        private void SetupReport2GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Report2_RequestData");
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(Report2Grid_DataResolved);

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
            SetDateTimeColumn(grid, "CreateDate");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            
        }
        void Report2Grid_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? quantity = e.FilterData.Select("Quantity").Cast<decimal?>().Sum();
            decimal? sum = e.FilterData.Select("Sum").Cast<decimal?>().Sum();
            decimal? amount = e.FilterData.Select("Amount").Cast<decimal?>().Sum();
            JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
            quantityColumn.FooterValue = quantity.ToString();
            JQGridColumn sumColumn = e.GridModel.Columns.Find(c => c.DataField == "Sum");
            sumColumn.FooterValue = sum.ToString();
            JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            amountColumn.FooterValue = amount.ToString();
        }
        public ActionResult Report2(int InvType,int CategoryType,int DeptType)
        {
            var gridModel = new Report2GridModel();
            gridModel.InvType = InvType;
            SetupReport2GridModel(gridModel.ReportGrid);
            gridModel.ReportGrid.DataType = "local";
            return PartialView(gridModel);
        }
        public ActionResult Report2_RequestData()
        {
            var gridModel = new Report2GridModel();
            SetupReport2GridModel(gridModel.ReportGrid);
            Guid gEmpty = Guid.Empty;
            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);

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
                        ConsumeType = clcs.ConsumeType == null ? -1 : clcs.ConsumeType,
                        ConsumeTypeName = dd3s.Name,
                        PayType = clcs.ConsumeType == null ? gEmpty : clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : cps.Id,
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
                        UnitOfMeasureName = dd4s.Name,
                        cis.InvType,
                        cds.DeptType,
                        iics.CategoryType,
                        cd1s.OrganizationId,
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "UserId", true);
            return QueryAndExcel(gridModel.ReportGrid, q, "消费明细查询.xls");
        }

        #endregion
        
        #region Report4 消费分类统计
        public ActionResult Report4(int InvType,int CategoryType,int DeptType)
        {
            if (Session["Report4Model"] != null)
            {
                Report4Model rm = Session["Report4Model"] as Report4Model;
                return PartialView(rm);
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

                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult Report4(Report4Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invtype = rm.InvType;
            int deptType = rm.DeptType;
            int categoryType = rm.CategoryType;

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
                        cl.Id,
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
                        clds.OrganizationId,
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
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<Report4Result>();
            //var q11 = from c in q
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
            //              UnitOfMeasureName = c.UnitOfMeasureName,
            //          };
            
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
        public ActionResult Report5(int InvType,int CategoryType,int DeptType)
        {
            if (Session["Report5Model"] != null)
            {
                Report5Model rm = Session["Report5Model"] as Report5Model;
                return PartialView(rm);
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
                
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult Report5(Report5Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);

            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;

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
                        cl.Id,
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
                        clds.OrganizationId,
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
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<Report5Result>();
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
        public ActionResult Report10(int InvType,int CategoryType,int DeptType)
        {
            if (Session["Report10Model"] != null)
            {
                Report10Model rm = Session["Report10Model"] as Report10Model;
                return PartialView(rm);
            }
            else
            {
                Report10Model rm = new Report10Model();
                rm.result = new List<Report10Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult Report10(Report10Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;
            var q1 = from cl in Uow.ConsumeList.GetAll()

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
                         Amount = cl.Amount,
                         Quantity=cl.Quantity,
                         OrganizationId=clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);

            var q3 = businessCommon.SetVouchAuthority(q1, "UserId", true).ToList<Report101Result>();
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
        public ActionResult Report11(int InvType,int CategoryType,int DeptType)
        {
            if (Session["Report11Model"] != null)
            {
                Report11Model rm = Session["Report11Model"] as Report11Model;
                return PartialView(rm);
            }
            else
            {
                Report11Model rm = new Report11Model();
                rm.result = new List<Report11Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                string strInvType = this.Request["InvType"];                
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
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
        public ActionResult Report11(Report11Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType; 
            int deptType = rm.DeptType;
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
                         Quantity = cl.Quantity,
                         OrganizationId=clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.UserId.HasValue) q1 = q1.Where(w => w.UserId == rm.UserId.Value);

            var q3 = businessCommon.SetVouchAuthority(q1, "UserId", true).ToList<Report111Result>();
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
        public ActionResult Report14(int InvType,int CategoryType,int DeptType)
        {
            if (Session["Report14Model"] != null)
            {
                Report14Model rm = Session["Report14Model"] as Report14Model;
                return PartialView(rm);
            }
            else
            {
                Report14Model rm = new Report14Model();
                rm.result = new List<Report14Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult Report14(Report14Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate).AddDays(1);
            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;

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
                        ConsumeType = dd1s.Name,
                        iConsumeType = clcs.ConsumeType,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : cps.Id,
                        cds.OrganizationId,
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
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<Report14Result>();
            
            int i2 = 0;
            var q31 = from c in q11
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

        #endregion

        #region 西餐厅报表

        #region WRReport2 消费明细查询
        private void SetupWRReport2GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WRReport2_RequestData");
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(WRReport2Grid_DataResolved);
            grid.ExcelExportSettings.Url = Url.Action("WRReport2_RequestData");
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "CreateDate");
        }
        void WRReport2Grid_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? quantity = e.FilterData.Select("Quantity").Cast<Decimal?>().Sum();
            decimal? sum = e.FilterData.Select("Sum").Cast<Decimal?>().Sum();
            decimal? amount = e.FilterData.Select("Amount").Cast<Decimal?>().Sum();

            JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
            quantityColumn.FooterValue = quantity.ToString();
            JQGridColumn sumColumn = e.GridModel.Columns.Find(c => c.DataField == "Sum");
            sumColumn.FooterValue = sum.ToString();
            JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            amountColumn.FooterValue = amount.ToString();
        }
        public ActionResult WRReport2(int InvType, int CategoryType,int DeptType)
        {
            var gridModel = new WRReport2GridModel();
            gridModel.InvType = InvType;
            gridModel.CategoryType = CategoryType;
            gridModel.DeptType = DeptType;//(int)DXInfo.Models.DeptType.Shop;
            SetupWRReport2GridModel(gridModel.WRReport2Grid);
            return PartialView(gridModel);
        }
        public ActionResult WRReport2_RequestData()
        {
            var gridModel = new WRReport2GridModel();
            SetupWRReport2GridModel(gridModel.WRReport2Grid);
            Guid gEmpty = Guid.Empty;
            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);

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

                    join p in Uow.Packages.GetAll() on cl.PackageId equals p.PackageId into clp
                    from clps in clp.DefaultIfEmpty()

                    join p1 in Uow.Inventory.GetAll() on clps.PackageId equals p1.Id into pp1
                    from pp1s in pp1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)iics.SectionType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d3.Code into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    select new
                    {
                        cl.Id,
                        cl.DeptId,
                        OrgId=cds.OrganizationId,
                        OrgName = ods.Name,
                        DeptName = cds.DeptName,
                        SectionId = iics == null ? -1 : iics.SectionType,
                        SectionName = dd2s.Name,
                        cl.UserId,
                        FullName = cus.FullName,
                        ConsumeType = clcs.ConsumeType == null ? -1 : clcs.ConsumeType,
                        ConsumeTypeName = dd3s.Name,
                        PayType = clcs.ConsumeType == null ? gEmpty :
                        clcs.ConsumeType == 0 ? gcardPayType :
                        clcs.ConsumeType == 2 ? gpointPayType : cps == null ? gEmpty : cps.Id,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        CardNo = ccards.CardNo,
                        MemberName = cms.MemberName,
                        Category = cis == null ? gEmpty : cis.Category,
                        CategoryName = iics.Name,
                        cl.IsPackage,
                        PackageId = cl.PackageId == null ? gEmpty : cl.PackageId,
                        PackageName = pp1s.Name,
                        cl.Inventory,
                        InventoryName = cis.Name,
                        Price = cl.Price,
                        Quantity = cl.Quantity,
                        Sum = cl.Sum,
                        Discount = cl.Discount,
                        Amount = cl.Amount,
                        CreateDate = cl.CreateDate,
                        cis.InvType,
                        iics.CategoryType,
                        cds.OrganizationId,
                        cds.DeptType,
                    };
            var q1 = businessCommon.SetVouchAuthority(q, "UserId", true);
            return QueryAndExcel(gridModel.WRReport2Grid, q1, "消费明细查询.xls");
        }
        #endregion

        #region WRReport4 消费分类统计
        public ActionResult WRReport4(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport4Model"] != null)
            {
                WRReport4Model rm = Session["WRReport4Model"] as WRReport4Model;
                return PartialView(rm);
            }
            else
            {
                WRReport4Model rm = new WRReport4Model();

                rm.result = new List<WRReport4Result>().AsQueryable();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.IsDept = true;
                rm.IsConsumeType = true;
                rm.IsPayType = true;
                rm.IsCategory = true;
                rm.IsInventory = true;
                rm.IsSection = true;

                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport4(WRReport4Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);

            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;

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

                    join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d1.Code into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)iics.SectionType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && //ods.Code == "001"	
                    cis.InvType == invType &&
                         iics.CategoryType == categoryType &&
                         clds.DeptType == deptType
                    select new
                    {
                        cl.DeptId,
                        cl.UserId,
                        Inventory = cis.Id,
                        Category = iics.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Amount = cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        ConsumeType = dd1s.Name,
                        clds.DeptName,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : clcsps.Id,
                        iics.SectionType,
                        SectionName = dd2s.Name,
                        clds.OrganizationId,
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
                DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetAll().Where(w => w.Id == rm.Category.Value).FirstOrDefault();
                if (ic != null)
                {
                    var ics = (from i in Uow.InventoryCategory.GetAll()
                               where i.Code.Contains(ic.Code)
                               select i.Id).ToList();
                    q = q.Where(w => ics.Contains(w.Category));
                }
                else
                    q = q.Where(w => w.Category == rm.Category.Value);
            }
            if (rm.Inventory.HasValue)
            {
                q = q.Where(w => w.Inventory == rm.Inventory.Value);
            }
            if (rm.Section.HasValue)
            {
                q = q.Where(w => w.SectionType == rm.Section.Value);
            }
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<WRReport4Result>();
            
            var q41 = from c in q11
                      group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType }
                          into g
                          select new WRReport4Result()
                          {
                              Id = Guid.NewGuid(),
                              DeptName = g.Key.DeptName,
                              SectionName = g.Key.SectionName,
                              CategoryName = g.Key.CategoryName,
                              InventoryName = g.Key.InventoryName,
                              ConsumeType = g.Key.ConsumeType,
                              PayType = g.Key.PayType,
                              Amount = g.Sum(s => s.Amount),
                              Quantity = g.Sum(s => s.Quantity)
                          };
            var q4 = q41.ToList().AsQueryable();
            if (!rm.IsDept)
            {
                q4 = from c in q4
                     group c by new { c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = "",
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
                         };
            }
            if (!rm.IsConsumeType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.PayType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = "",
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
                         };
            }
            if (!rm.IsPayType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = "",
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
                         };
            }
            if (!rm.IsCategory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.InventoryName, c.ConsumeType, c.PayType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = "",
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
                         };
            }
            if (!rm.IsInventory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.ConsumeType, c.PayType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = "",
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
                         };
            }
            if (!rm.IsSection)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType }
                         into g
                         select new WRReport4Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = "",
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity)
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
            Session["WRReport4Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport4", routeValues);
        }
        public void WRReport4ExportToExcel()
        {
            if (Session["WRReport4Model"] == null) return;
            var m = Session["WRReport4Model"] as WRReport4Model;
            object dataSource = m.result;
            string fileName = "消费分类统计.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SectionName";
            field.HeaderText = "部门";
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

        #region WRReport5 销售排名统计
        public ActionResult WRReport5(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport5Model"] != null)
            {
                WRReport5Model rm = Session["WRReport5Model"] as WRReport5Model;
                return PartialView(rm);
            }
            else
            {
                WRReport5Model rm = new WRReport5Model();
                rm.result = new List<WRReport5Result>().AsQueryable();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.IsDept = true;
                rm.IsConsumeType = true;
                rm.IsPayType = true;
                rm.IsCategory = true;
                rm.IsInventory = true;
                rm.IsCard = true;
                rm.IsSection = true;
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport5(WRReport5Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);

            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;

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

                    join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals d1.Code into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)iics.SectionType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && 
                    cis.InvType == invType &&
                         iics.CategoryType == categoryType &&
                         clds.DeptType == deptType
                    select new
                    {
                        cl.DeptId,
                        cl.UserId,
                        Inventory = cis.Id,
                        Category = iics.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Amount = cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,                        
                        ConsumeType = dd1s.Name,
                        clds.DeptName,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : clcsps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : clcsps.Id,
                        clcscds.CardNo,
                        cms.MemberName,
                        iics.SectionType,
                        SectionName=dd2s.Name,
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
                DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetAll().Where(w => w.Id == rm.Category.Value).FirstOrDefault();
                if (ic != null)
                {
                    var ics = (from i in Uow.InventoryCategory.GetAll()
                               where i.Code.Contains(ic.Code)
                               select i.Id).ToList();
                    q = q.Where(w => ics.Contains(w.Category));
                }
                else
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
            if (rm.Section.HasValue)
            {
                q = q.Where(w => w.SectionType == rm.Section.Value);
            }
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<WRReport5Result>();
            
            var q41 = from c in q11
                      group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CardNo, c.MemberName }
                          into g
                          select new WRReport5Result()
                          {
                              Id = Guid.NewGuid(),
                              DeptName = g.Key.DeptName,
                              SectionName = g.Key.SectionName,
                              CategoryName = g.Key.CategoryName,
                              InventoryName = g.Key.InventoryName,
                              ConsumeType = g.Key.ConsumeType,
                              PayType = g.Key.PayType,
                              Amount = g.Sum(s => s.Amount),
                              Quantity = g.Sum(s => s.Quantity),
                              CardNo = g.Key.CardNo,
                              MemberName = g.Key.MemberName,
                          };
            var q4 = q41.ToList().AsQueryable();
            if (!rm.IsDept)
            {
                q4 = from c in q4
                     group c by new { c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = "",
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsConsumeType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.PayType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = "",
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsPayType)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.InventoryName, c.ConsumeType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = "",
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsCategory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.InventoryName, c.ConsumeType, c.PayType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = "",
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsInventory)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.SectionName, c.CategoryName, c.ConsumeType, c.PayType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = "",
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsSection)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType, c.CardNo, c.MemberName }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = "",
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = g.Key.CardNo,
                             MemberName = g.Key.MemberName,
                         };
            }
            if (!rm.IsCard)
            {
                q4 = from c in q4
                     group c by new { c.DeptName, c.CategoryName, c.InventoryName, c.ConsumeType, c.PayType }
                         into g
                         select new WRReport5Result()
                         {
                             DeptName = g.Key.DeptName,
                             CategoryName = g.Key.CategoryName,
                             InventoryName = g.Key.InventoryName,
                             ConsumeType = g.Key.ConsumeType,
                             PayType = g.Key.PayType,
                             Amount = g.Sum(s => s.Amount),
                             Quantity = g.Sum(s => s.Quantity),
                             CardNo = "",
                             MemberName = "",
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


            rm.result = q4.OrderByDescending(o => o.Amount);
            Session["WRReport5Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport5", routeValues);
        }
        public void WRReport5ExportToExcel()
        {
            if (Session["WRReport5Model"] == null) return;
            var m = Session["WRReport5Model"] as WRReport5Model;
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

        #region WRReport10 系列类别比例
        public ActionResult WRReport10(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport10Model"] != null)
            {
                WRReport10Model rm = Session["WRReport10Model"] as WRReport10Model;
                return PartialView(rm);
            }
            else
            {
                WRReport10Model rm = new WRReport10Model();
                rm.result = new List<WRReport10Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport10(WRReport10Model rm)
        {

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;
            var q1 = from cl in Uow.ConsumeList.GetAll()

                     join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into clu
                     from clus in clu.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join c in Uow.Inventory.GetAll() on cl.Inventory equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join t in Uow.InventoryCategory.GetAll() on clcs.Category equals t.Id into clcst
                     from clcsts in clcst.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && //ods.Code == "001"	
                     clcs.InvType == invType &&
                         clcsts.CategoryType == categoryType &&
                         clds.DeptType == deptType
                     select new WRReport101Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         Category = clcsts.Id,
                         CategoryName = clcsts.Name,
                         CreateDate = cl.CreateDate,
                         Amount = cl.Amount,
                         Quantity = cl.Quantity,
                         Section = clcsts.SectionType,
                         SectionName = clcsts.SectionType == 0 ? "后厨" : clcsts.SectionType == 1 ? "吧台" : "前厅",
                         OrganizationId=clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.Section.HasValue) q1 = q1.Where(w => w.Section == rm.Section.Value);

            var q3 = businessCommon.SetVouchAuthority(q1, "UserId", true).ToList<WRReport101Result>();
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
                         group q by new { q.DeptName, q.SectionName, q.Category, q.CategoryName } into g
                         select new WRReport10Result()
                         {
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             Category = g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Quantity = g.Sum(s => s.Quantity),
                             Amount = g.Sum(s => s.Amount),
                             AmountOfDayAvg = g.Sum(a => a.Amount) / iday,
                             QuantityRatio = g.Sum(s => s.Quantity) / dQuantity,
                             AmountRatio = g.Sum(s => s.Amount) / dAmount,
                         };
                var q7 = q4.ToList();
                if (q7 != null)
                {
                    if (rm.Category.HasValue)
                    {
                        DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetAll().Where(w => w.Id == rm.Category.Value).FirstOrDefault();
                        if (ic != null)
                        {
                            var ics = (from i in Uow.InventoryCategory.GetAll()
                                       where i.Code.Contains(ic.Code)
                                       select i.Id).ToList();
                            q7 = q7.Where(w => ics.Contains(w.Category)).ToList();
                        }
                        else
                            q7 = q7.Where(w => w.Category == rm.Category.Value).ToList();
                    }
                    rm.result = q7;
                }
                rm.Amount = dAmount;
                rm.AmountOfDayAvg = dAvg;
            }
            else
            {
                rm.result = new List<WRReport10Result>();
            }
            Session["WRReport10Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport10", routeValues);
        }
        public void WRReport10ExportToExcel()
        {
            if (Session["WRReport10Model"] == null) return;
            var m = Session["WRReport10Model"] as WRReport10Model;
            object dataSource = m.result;
            string fileName = "系列类别比例.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "店名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SectionName";
            field.HeaderText = "部门";
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
            field.DataField = "Quantity";
            field.HeaderText = "销售数数";
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
            field.DataField = "QuantityRatio";
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

        #region WRReport11 单品类别比例        
        public ActionResult WRReport11(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport11Model"] != null)
            {
                WRReport11Model rm = Session["WRReport11Model"] as WRReport11Model;
                return PartialView(rm);
            }
            else
            {
                WRReport11Model rm = new WRReport11Model();
                rm.result = new List<WRReport11Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport11(WRReport11Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;
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

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && //ods.Code == "001"
                     clcs.InvType == invType &&
                         clcsts.CategoryType == categoryType &&
                         clds.DeptType == deptType
                     select new WRReport111Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         Section = clcsts.SectionType,
                         SectionName = clcsts.SectionType == 0 ? "后厨" : clcsts.SectionType == 1 ? "吧台" : "前厅",
                         UserId = cl.UserId,
                         FullName = clus.FullName,
                         Category = clcsts.Id,
                         CategoryName = clcsts.Name,
                         Inventory = clcs.Id,
                         InventoryName = clcs.Name,
                         CreateDate = cl.CreateDate,
                         Amount = cl.Amount,
                         Quantity = cl.Quantity,
                         OrganizationId = clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);
            if (rm.Section.HasValue) q1 = q1.Where(w => w.Section == rm.Section.Value);

            var q3 = businessCommon.SetVouchAuthority(q1, "UserId", true).ToList<WRReport111Result>();
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
                         group q by new { CreateDate = "", q.DeptId, q.DeptName, q.Section, q.SectionName, q.Category, q.CategoryName, q.Inventory, q.InventoryName } into g
                         select new WRReport11Result()
                         {
                             CreateDate = g.Key.CreateDate,
                             DeptName = g.Key.DeptName,
                             SectionName = g.Key.SectionName,
                             Category = g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Inventory = g.Key.Inventory,
                             InventoryName = g.Key.InventoryName,
                             Amount = g.Sum(s => s.Amount),
                             AmountOfDayAvg = g.Sum(a => a.Amount) / iday,
                             AmountRatioOfAll = g.Sum(s => s.Amount) / dAmount,

                         };
                var q7 = from q in q3
                         group q by new { q.Category, q.CategoryName } into g
                         select new WRReport11Result()
                         {
                             Category = g.Key.Category,
                             CategoryName = g.Key.CategoryName,
                             Amount = g.Sum(s => s.Amount),
                         };
                var q8 = from q in q4
                         join q9 in q7 on q.Category equals q9.Category into qq9
                         from qq9s in qq9.DefaultIfEmpty()
                         select new WRReport11Result()
                         {
                             CreateDate = q.CreateDate,
                             DeptName = q.DeptName,
                             SectionName = q.SectionName,
                             Category = q.Category,
                             CategoryName = q.CategoryName,
                             Inventory = q.Inventory,
                             InventoryName = q.InventoryName,
                             Amount = q.Amount,
                             AmountOfDayAvg = q.AmountOfDayAvg,
                             AmountRatioOfAll = q.AmountRatioOfAll,
                             AmountRatioOfCategory = q.Amount / (qq9s.Amount == 0 ? 1 : qq9s.Amount),
                         };

                if (rm.Category.HasValue)
                {
                    DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetAll().Where(w => w.Id == rm.Category.Value).FirstOrDefault();
                    if (ic != null)
                    {
                        var ics = (from i in Uow.InventoryCategory.GetAll()
                                   where i.Code.Contains(ic.Code)
                                   select i.Id).ToList();
                        q8 = q8.Where(w => ics.Contains(w.Category));
                    }
                    else
                        q8 = q8.Where(w => w.Category == rm.Category.Value);
                }
                if (rm.Inventory.HasValue) q8 = q8.Where(w => w.Inventory == rm.Inventory.Value);

                rm.result = q8.ToList();
                rm.Amount = dAmount;
                rm.AmountOfDayAvg = dAvg;
            }
            else
            {
                rm.result = new List<WRReport11Result>();
            }
            Session["WRReport11Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport11", routeValues);
        }
        public void WRReport11ExportToExcel()
        {
            if (Session["WRReport11Model"] == null) return;
            var m = Session["WRReport11Model"] as WRReport11Model;
            object dataSource = m.result;
            string fileName = "单品类别比例.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "店名";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "SectionName";
            field.HeaderText = "位置";
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

        #region WRReport14 单数明细和统计        
        public ActionResult WRReport14(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport14Model"] != null)
            {
                WRReport14Model rm = Session["WRReport14Model"] as WRReport14Model;
                return PartialView(rm);
            }
            else
            {
                WRReport14Model rm = new WRReport14Model();
                rm.result = new List<WRReport14Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport14(WRReport14Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            Guid gcardPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
            Guid gpointPayType = Guid.Parse(DXInfo.Business.Helper.PayType_Point);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;
            var q = from cl in Uow.ConsumeList.GetAll()
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join o in Uow.OrderDishes.GetAll() on clcs.OrderId equals o.Id into clcso
                    from clcsos in clcso.DefaultIfEmpty()

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

                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)iics.SectionType).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && 
                    cis.InvType == invType &&
                         iics.CategoryType == categoryType &&
                         cds.DeptType == deptType
                    select new
                    {
                        Consume = clcsos.OrderNo == null ? 0 : clcsos.OrderNo,
                        cl.DeptId,
                        cl.UserId,
                        CardNo = ccards.CardNo,
                        MemberName = cms.MemberName,
                        FullName = cus.FullName,
                        DeptName = cds.DeptName,
                        Discount = cl.Discount,
                        Amount = cl.Amount,
                        Sum = cl.Sum,
                        CreateDate = cl.CreateDate,
                        Quantity = cl.Quantity,                        
                        ConsumeType = dd1s.Name,
                        iConsumeType = clcs.ConsumeType,
                        PayType = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        gPayType = clcs.ConsumeType == 0 ? gcardPayType : clcs.ConsumeType == 2 ? gpointPayType : cps.Id,
                        Section = iics.SectionType == null ? 0 : iics.SectionType,
                        cds.OrganizationId,
                        SectionName=dd2s.Name,
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
            if (rm.Section.HasValue)
            {
                q = q.Where(w => w.Section == rm.Section.Value);
            }
            var q11 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList<WRReport14Result>();
            
            int i2 = 0;
            var q31 = from c in q11
                      group c by new { c.Consume, c.CardNo, c.MemberName, c.FullName, c.DeptName, c.SectionName, c.Discount, c.CreateDate, c.ConsumeType, c.PayType }
                          into g
                          select new WRReport14Result()
                          {
                              Consume = g.Key.Consume,
                              Id = i2++,
                              CardNo = g.Key.CardNo,
                              MemberName = g.Key.MemberName,
                              FullName = g.Key.FullName,
                              DeptName = g.Key.DeptName,
                              SectionName = g.Key.SectionName,
                              Discount = g.Key.Discount,
                              CreateDate = g.Key.CreateDate,
                              ConsumeType = g.Key.ConsumeType,
                              PayType = g.Key.PayType,

                              Amount = g.Sum(s => s.Amount),
                              Quantity = g.Sum(s => s.Quantity),
                              Sum = g.Sum(s => s.Sum)
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
            Session["WRReport14Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport14", routeValues);
        }
        public void WRReport14ExportToExcel()
        {
            if (Session["WRReport14Model"] == null) return;
            var m = Session["WRReport14Model"] as WRReport14Model;
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
            field.DataField = "SectionName";
            field.HeaderText = "部门";
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


        #region WRReport15 部门销售比例
        public ActionResult WRReport15(int InvType,int CategoryType,int DeptType)
        {
            if (Session["WRReport15Model"] != null)
            {
                WRReport15Model rm = Session["WRReport15Model"] as WRReport15Model;
                return PartialView(rm);
            }
            else
            {
                WRReport15Model rm = new WRReport15Model();
                rm.result = new List<WRReport15Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                rm.InvType = InvType;
                rm.CategoryType = CategoryType;
                rm.DeptType = DeptType;
                return PartialView(rm);
            }
        }
        [HttpPost]
        public ActionResult WRReport15(WRReport15Model rm)
        {

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            int invType = rm.InvType;
            int categoryType = rm.CategoryType;
            int deptType = rm.DeptType;
            var q1 = from cl in Uow.ConsumeList.GetAll()
                     join cm in Uow.Consume.GetAll() on cl.Consume equals cm.Id into clcm
                     from clcms in clcm.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cld
                     from clds in cld.DefaultIfEmpty()

                     join c in Uow.Inventory.GetAll() on cl.Inventory equals c.Id into clc
                     from clcs in clc.DefaultIfEmpty()

                     join t in Uow.InventoryCategory.GetAll() on clcs.Category equals t.Id into clcst
                     from clcsts in clcst.DefaultIfEmpty()

                     where cl.CreateDate >= dtBeginDate && cl.CreateDate <= dtEndDate && //ods.Code == "001"
                     clcs.InvType == invType &&
                         clcsts.CategoryType == categoryType &&
                         clds.DeptType == deptType
                     select new WRReport151Result()
                     {
                         Id = cl.Id,
                         DeptId = cl.DeptId,
                         DeptName = clds.DeptName,
                         Section = clcsts.SectionType,
                         SectionName = clcsts.SectionType == 0 ? "后厨" : clcsts.SectionType == 1 ? "吧台" : clcsts.SectionType == 2 ? "前厅" : "未知",
                         CreateDate = cl.CreateDate,
                         Amount = cl.Amount,
                         Quantity = cl.Quantity,
                         OrganizationId = clds.OrganizationId,
                     };
            if (rm.DeptId.HasValue) q1 = q1.Where(w => w.DeptId == rm.DeptId.Value);

            var q3 = businessCommon.SetVouchAuthority(q1, "", false).ToList<WRReport151Result>();
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
                         group q by new { q.DeptName, q.Section, q.SectionName } into g
                         select new WRReport15Result()
                         {
                             DeptName = g.Key.DeptName,
                             Section = g.Key.Section,
                             SectionName = g.Key.SectionName,
                             Quantity = g.Sum(s => s.Quantity),
                             Amount = g.Sum(s => s.Amount),
                             AmountOfDayAvg = g.Sum(a => a.Amount) / iday,
                             QuantityRatio = g.Sum(s => s.Quantity) / dQuantity,
                             AmountRatio = g.Sum(s => s.Amount) / dAmount,
                         };
                var q7 = q4.ToList();
                if (q7 != null)
                {
                    rm.result = q7;
                }
                rm.Quantity = dQuantity;
                rm.Amount = dAmount;
                rm.AmountOfDayAvg = dAvg;
            }
            else
            {
                rm.result = new List<WRReport15Result>();
            }
            Session["WRReport15Model"] = rm;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", invType);
            routeValues.Add("CategoryType", categoryType);
            routeValues.Add("DeptType", deptType);
            return RedirectToAction("WRReport15", routeValues);
        }
        public void WRReport15ExportToExcel()
        {
            if (Session["WRReport15Model"] == null) return;
            var m = Session["WRReport15Model"] as WRReport15Model;
            object dataSource = m.result;
            string fileName = "部门销售比例.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
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
            field.DataField = "QuantityRatio";
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

        #region WRReport16 西餐厅菜品清单
        [HttpGet]
        public ActionResult WRReport16(int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;

            if (string.IsNullOrEmpty(sort)) sort = "OrderDishCreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            if (Session["WRReport16Model"] != null)
            {
                WRReport16Model rm = Session["WRReport16Model"] as WRReport16Model;
                if (page.HasValue || !string.IsNullOrEmpty(sort))
                {
                    rm.result2 = rm.result.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();
                }
                return PartialView(rm);
            }
            else
            {
                WRReport16Model rm = new WRReport16Model();
                rm.result = new List<WRReport16Result>();
                rm.result2 = new List<WRReport16Result>();
                rm.BeginDate = DateTime.Now.ToString("yyyy-MM-dd") + " 03:00";
                rm.EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 03:00";
                return PartialView(rm);
            }
        }
        private List<WRReport16Result> WRReport16_GetData(WRReport16Model rm)
        {
            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);
            Guid empty = Guid.Empty;
            DateTime dm = DateTime.MinValue;
            var q1 = from d1 in Uow.OrderDeskesHis.GetAll()
                     join d2 in Uow.Desks.GetAll() on d1.DeskId equals d2.Id into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.OrderDishesHis.GetAll() on d1.OrderId equals d3.LinkId into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     join d4 in Uow.Depts.GetAll() on dd3s.DeptId equals d4.DeptId into dd4
                     from dd4s in dd4.DefaultIfEmpty()

                     join d5 in Uow.aspnet_CustomProfile.GetAll() on dd3s.UserId equals d5.UserId into dd5
                     from dd5s in dd5.DefaultIfEmpty()

                     join d6 in Uow.aspnet_CustomProfile.GetAll() on d1.UserId equals d6.UserId into dd6
                     from dd6s in dd6.DefaultIfEmpty()

                     join d7 in Uow.NameCode.GetAll().Where(w => w.Type == "OrderDishStatus") on SqlFunctions.StringConvert((double?)dd3s.Status).Trim() equals d7.Code into dd7
                     from dd7s in dd7.DefaultIfEmpty()

                     select new
                     {
                         OrderDeskCreateDate = d1.CreateDate,
                         OrderDeskFullName = dd6s.FullName,
                         OrderDeskStatus = d1.Status == null ? 0 : d1.Status,
                         OrderId = d1.OrderId == null ? empty : d1.OrderId,
                         DeskNo = dd2s.Code,
                         OrderDishUserId = dd3s.UserId,
                         OrderDishFullName = dd5s.FullName,
                         OrderDishCreateDate = dd3s.CreateDate,
                         iOrderDishStatus = dd3s.Status == null ? 0 : dd3s.Status,
                         OrderDishStatus = dd7s.Name,
                         DeptName = dd4s.DeptName,
                         dd3s.Quantity,
                         //d1.UserId,
                         dd3s.DeptId,
                         dd4s.OrganizationId,
                     };

            var q = from d1 in Uow.OrderMenusHis.GetAll()
                    join d2 in q1 on d1.OrderId equals d2.OrderId into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.Inventory.GetAll() on d1.InventoryId equals d3.Id into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    join d4 in Uow.aspnet_CustomProfile.GetAll() on d1.UserId equals d4.UserId into dd4
                    from dd4s in dd4.DefaultIfEmpty()

                    join d5 in Uow.NameCode.GetAll().Where(w => w.Type == "OrderDishStatus") on SqlFunctions.StringConvert((double?)d1.Status).Trim() equals d5.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()
                    select new
                    {
                        dd2s.DeskNo,
                        iOrderDishStatus = dd2s.OrderDishStatus == null ? 0 : dd2s.iOrderDishStatus,
                        OrderDishStatus = dd2s.OrderDishStatus,
                        dd2s.DeptName,
                        OrderId = dd2s.OrderId == null ? empty : dd2s.OrderId,
                        Quantity = dd2s.Quantity == null ? 0 : dd2s.Quantity,
                        dd2s.OrderDishFullName,
                        OrderDishCreateDate = dd2s.OrderDishCreateDate == null ? dm : dd2s.OrderDishCreateDate,
                        dd2s.OrderDeskFullName,
                        OrderDeskCreateDate = dd2s.OrderDeskCreateDate == null ? dm : dd2s.OrderDeskCreateDate,
                        OrderDeskStatus = dd2s.OrderDeskStatus == null ? 0 : dd2s.OrderDeskStatus,
                        OrderMenuInvName = dd3s.Name,
                        iOrderMenuStatus = d1.Status == null ? 0 : d1.Status,
                        OrderMenuStatus = dd5s.Name,
                        OrderMenuInvPrice = d1.Price,
                        OrderMenuInvQuantity = d1.Quantity,
                        OrderMenuInvAmount = d1.Amount,
                        OrderMenuUserId = d1.UserId == null ? empty : d1.UserId,
                        OrderMenuFullName = dd4s.FullName,
                        OrderMenuCreateDate = d1.CreateDate,
                        OrderDishUserId = dd2s.OrderDishUserId == null ? empty : dd2s.OrderDishUserId,
                        d1.UserId,
                        dd2s.DeptId,
                        dd2s.OrganizationId,
                    };


            q = q.Where(w => w.OrderMenuCreateDate >= dtBeginDate);
            q = q.Where(w => w.OrderMenuCreateDate <= dtEndDate);
            if (rm.OrderDishStatus.HasValue)
            {
                q = q.Where(w => w.iOrderDishStatus == rm.OrderDishStatus.Value);
            }
            if (rm.UserId.HasValue)
            {
                q = q.Where(w => w.OrderMenuUserId == rm.UserId.Value);
            }
            if (rm.OrderMenuStatus.HasValue)
            {
                q = q.Where(w => w.iOrderMenuStatus == rm.OrderMenuStatus);
            }
            if (!string.IsNullOrWhiteSpace(rm.DeskNo))
            {
                q = q.Where(w => w.DeskNo.Contains(rm.DeskNo));
            }
            var q3 = businessCommon.SetVouchAuthority(q, "UserId", true).ToList <WRReport16Result>();            
            return q3;
        }
        [HttpPost]
        public ActionResult WRReport16(WRReport16Model rm, int? page, string sort, string sortdir)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            if (string.IsNullOrEmpty(sort)) sort = "OrderDishCreateDate";
            if (string.IsNullOrEmpty(sortdir)) sortdir = "ASC";

            DateTime dtBeginDate = Convert.ToDateTime(rm.BeginDate);
            DateTime dtEndDate = Convert.ToDateTime(rm.EndDate);
            if (Convert.ToDateTime(rm.EndDate).TimeOfDay > TimeSpan.Zero)
                dtEndDate = Convert.ToDateTime(rm.EndDate);            
            List < WRReport16Result > qa1 = WRReport16_GetData(rm);

            rm.rowCount = qa1.Count;
            rm.result = qa1;
            rm.result2 = qa1.OrderBy(sort + " " + sortdir).Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();

            Session["WRReport16Model"] = rm;
            return RedirectToAction("WRReport16");
        }
        public void WRReport16ExportToExcel()
        {
            if (Session["WRReport16Model"] == null) return;
            var m = Session["WRReport16Model"] as WRReport16Model;
            object dataSource = m.result;
            string fileName = "西餐厅菜品清单.xls";

            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "DeptName";
            field.HeaderText = "门店";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "Quantity";
            field.HeaderText = "人数";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderDishStatus";
            field.HeaderText = "订单状态";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderDishFullName";
            field.HeaderText = "订单操作";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderDishCreateDate";
            field.HeaderText = "订单时间";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "DeskNo";
            field.HeaderText = "桌台";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderDeskFullName";
            field.HeaderText = "桌台操作";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderDeskCreateDate";
            field.HeaderText = "桌台时间";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuInvName";
            field.HeaderText = "菜品";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuStatus";
            field.HeaderText = "菜品状态";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuInvPrice";
            field.HeaderText = "菜品单价";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuInvQuantity";
            field.HeaderText = "菜品数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuInvAmount";
            field.HeaderText = "菜品金额";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuFullName";
            field.HeaderText = "菜品操作";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "OrderMenuCreateDate";
            field.HeaderText = "菜品时间";
            view.Columns.Add(field);

            view.DataSource = dataSource;//d.ToList();
            view.DataBind();
            DoExportToExcel(fileName, view);
        }
        #endregion

        #region WRReport20 西餐厅等待时间查询报表
        public ActionResult WRReport20()
        {
            var gridModel = new WRReport20GridModel();
            SetupWRReport20GridModel(gridModel.WRReportGrid);
            return PartialView(gridModel);
        }
        private void SetupWRReport20GridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("WRReport20_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(WRReport20_DataResolved);
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            grid.IgnoreFilterField = "WaitMinute";
        }
        private IQueryable WRReport20_GetDate()
        {
            var q = from d in Uow.OrderMenusHis.GetAll().Where(w=>w.Status==6 || w.Status==8)

                    join d1 in Uow.Inventory.GetAll() on d.InventoryId equals d1.Id into dd1 
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.InventoryCategory.GetAll() on dd1s.Category equals d2.Id into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d3.UserId into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    join d4 in Uow.Depts.GetAll() on dd3s.DeptId equals d4.DeptId into dd4
                    from dd4s in dd4.DefaultIfEmpty()

                    join d5 in Uow.NameCode.GetAll().Where(w => w.Type == "SectionType") on SqlFunctions.StringConvert((double?)dd2s.SectionType).Trim() equals d5.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()

                    select new {
                        CreateDate=d.OrderCreateDate,
                        dd3s.DeptId,
                        dd4s.DeptName,
                        dd2s.SectionType,
                        SectionName=dd5s.Name,
                        InvId=d.InventoryId,
                        InvName=dd1s.Name,
                        dd1s.Category,
                        d.UserId,
                        WaitMinute = SqlFunctions.DateDiff("minute", d.OrderCreateDate, d.MenuCreateDate),
                        d.Quantity,
                        Amount = d.Quantity*d.Price,
                        //Timeout = SqlFunctions.DateDiff("minute", d.OrderCreateDate, d.MenuCreateDate)>30,
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "UserId", true);
            return q2;
        }
        void WRReport20_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            //string strWaitMinutes = Request["WaitMinutes"];
            //int WaitMinutes = 0;
            //if (!string.IsNullOrEmpty(strWaitMinutes))
            //{
            //    WaitMinutes = Convert.ToInt32(strWaitMinutes);
            //}            
            decimal? quantity = e.IgnoreFilterFieldData.Select("Quantity").Cast<Decimal?>().Sum();
            decimal? amount = e.IgnoreFilterFieldData.Select("Amount").Cast<Decimal?>().Sum();
            decimal? timeOutQuantity = e.FilterData.Select("Quantity").Cast<Decimal?>().Sum();
            decimal? timeOutAmount = e.FilterData.Select("Amount").Cast<Decimal?>().Sum();
            
            JQGridColumn deptNameColumn = e.GridModel.Columns.Find(c => c.DataField == "DeptName");
            deptNameColumn.FooterValue = "总单数:" + Convert.ToDecimal(quantity).ToString("f0") + ",总金额:" + Convert.ToDecimal(amount).ToString("f2");
            JQGridColumn sectionNameColumn = e.GridModel.Columns.Find(c => c.DataField == "SectionName");
            sectionNameColumn.FooterValue = "超时单数:" + Convert.ToDecimal(timeOutQuantity).ToString("f0") + ",超时金额:" + Convert.ToDecimal(timeOutAmount).ToString("f2");
            JQGridColumn invNameColumn = e.GridModel.Columns.Find(c => c.DataField == "InvName");
            invNameColumn.FooterValue = "占单数比例:" + Convert.ToDecimal(timeOutQuantity / quantity).ToString("P")+",占金额比例:" + Convert.ToDecimal(timeOutAmount / amount).ToString("P");

            //JQGridColumn waitMinutesColumn = e.GridModel.Columns.Find(c => c.DataField == "WaitMinutes");
            //waitMinutesColumn.FooterValue = ;            
            //JQGridColumn amountColumn = e.GridModel.Columns.Find(c => c.DataField == "Amount");
            //amountColumn.FooterValue = ;
            //JQGridColumn quantityColumn = e.GridModel.Columns.Find(c => c.DataField == "Quantity");
            //quantityColumn.FooterValue = ;
        }
        public ActionResult WRReport20_RequestData()
        {
            var gridModel = new WRReport20GridModel();
            SetupWRReport20GridModel(gridModel.WRReportGrid);
            IQueryable q = WRReport20_GetDate();
            return QueryAndExcel(gridModel.WRReportGrid, q, "西餐厅等待时间查询报表.xls");
        }
        #endregion
        #endregion

        #region AMSApp 销售报表

        #region 会员资料查询
        private void SetupAssInfoGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("AssInfo_RequestData");
            grid.EditUrl = Url.Action("AssInfo_EditData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(AssInfo_DataResolved);            

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "dtCreateDate");
            SetDateTimeColumn(grid, "dtOperDate");

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcAssState", centerCommon.GetAssState());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());
        }
        void AssInfo_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {

            decimal? nCharge = e.FilterData.Select("nCharge").Cast<Decimal?>().Sum();
            decimal? iIgValue = e.FilterData.Select("iIgValue").Cast<Decimal?>().Sum();

            JQGridColumn nChargeColumn = e.GridModel.Columns.Find(c => c.DataField == "nCharge");
            nChargeColumn.FooterValue = nCharge.ToString();
            JQGridColumn iIgValueColumn = e.GridModel.Columns.Find(c => c.DataField == "iIgValue");
            iIgValueColumn.FooterValue = iIgValue.ToString();
        }
        public ActionResult AssInfo()
        {
            var gridModel = new AssInfoGridModel();
            SetupAssInfoGridModel(gridModel.AssInfoGrid);
            return PartialView(gridModel);
        }
        public ActionResult AssInfo_RequestData()
        {
            var gridModel = new AssInfoGridModel();
            SetupAssInfoGridModel(gridModel.AssInfoGrid);
            var q = from d in Uow.tbAssociator.GetAll() select d;
            var q2 = businessCommon.SetVouchAuthority(q, "", false, "", "vcDeptId", "", deptCode,"");
            return QueryAndExcel(gridModel.AssInfoGrid, q2, "会员资料查询.xls");
        }
        private void editAss(DXInfo.Models.tbAssociator ass)
        {
            var oldAss = Uow.tbAssociator.GetAll().Where(w => w.iAssID == ass.iAssID && w.vcCardID == ass.vcCardID).FirstOrDefault();
            if (oldAss == null)
            {
                throw new DXInfo.Models.BusinessException("无此会员");
            }
            oldAss.vcAssName = ass.vcAssName;
            oldAss.vcLinkPhone = ass.vcLinkPhone;
            oldAss.vcLinkAddress = ass.vcLinkAddress;
            oldAss.vcSpell = ass.vcSpell;
            oldAss.vcAssNbr = ass.vcAssNbr;
            oldAss.vcEmail = ass.vcEmail;
            oldAss.vcComments = ass.vcComments;
            oldAss.dtOperDate = DateTime.Now;
            Uow.tbAssociator.Update(oldAss);

            string loginId = this.User.Identity.Name;
            var tbLogin = Uow.tbLogin.GetById(g => g.vcLoginID == loginId);

            DXInfo.Models.tbAssociatorLog log = Mapper.Map<DXInfo.Models.tbAssociatorLog>(oldAss);
            log.vcOperName = loginId;
            log.vcOperDeptID = tbLogin.vcDeptID;
            Uow.tbAssociatorLog.Add(log);

            DXInfo.Models.tbAssociatorSync sync = Mapper.Map<DXInfo.Models.tbAssociatorSync>(oldAss);
            sync.iUpdateCount = 0;
            Uow.tbAssociatorSync.Add(sync);

            Uow.Commit();
        } 
        public ActionResult AssInfo_EditData(DXInfo.Models.tbAssociator ass)
        {
            var gridModel = new AssInfoGridModel();
            SetupAssInfoGridModel(gridModel.AssInfoGrid);
            return ajaxCallBack<DXInfo.Models.tbAssociator>(gridModel.AssInfoGrid, ass, null, editAss, null);
        }
        #endregion

        #region 消费统计查询
        private void SetupConsItemGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ConsItem_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(ConsItem_DataResolved);

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "dtConsDate");

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcConsType", centerCommon.GetPT());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());

            SetDropDownColumn(grid, "cFlag", centerCommon.GetFlag());
        }
        void ConsItem_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {

            decimal? nFee = e.FilterData.Select("nFee").Cast<Decimal?>().Sum();
            JQGridColumn nFeeColumn = e.GridModel.Columns.Find(c => c.DataField == "nFee");
            nFeeColumn.FooterValue = nFee.ToString();
        }
        public ActionResult ConsItem()
        {
            var gridModel = new ConsItemGridModel();
            SetupConsItemGridModel(gridModel.ConsItemGrid);
            return PartialView(gridModel);
        }
        public ActionResult ConsItem_RequestData()
        {
            var gridModel = new ConsItemGridModel();
            SetupConsItemGridModel(gridModel.ConsItemGrid);
            var q = from d in Uow.vwConsItem.GetAll()
                    join d1 in Uow.vwBill.GetAll() on new { key1 = d.iSerial, key2 = d.vcCardID, key3 = d.vcDeptID } equals new { key1 = d1.iSerial, key2 = d1.vcCardID, key3 = d1.vcDeptID }
                    join d2 in Uow.tbAssociator.GetAll() on d.vcCardID equals d2.vcCardID into dd2 from dd2s in dd2.DefaultIfEmpty()
                    join d3 in Uow.tbGoods.GetAll() on d.vcGoodsID equals d3.vcGoodsID into dd3 from dd3s in dd3.DefaultIfEmpty()
                    select new
                    {
                        d.iSerial,
                        dd2s.vcAssName,
                        dd2s.vcAssType,
                        d.vcCardID,
                        dd3s.vcGoodsName,
                        d.nPrice,
                        d.iCount,
                        d.nFee,
                        d1.vcConsType,
                        d.vcComments,
                        d.cFlag,
                        d.dtConsDate,
                        d.vcOperName,
                        d.vcDeptID
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "vcOperName", true, "", "vcDeptID", "", deptCode, operName);
            return QueryAndExcel(gridModel.ConsItemGrid, q2, "消费统计查询.xls");
        }
        #endregion

        #region 会员充值查询
        private void SetupFillQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("FillQuery_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(FillQuery_DataResolved);

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "dtFillDate");

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());
        }
        void FillQuery_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? nFillFee = e.FilterData.Select("nFillFee").Cast<Decimal?>().Sum();
            JQGridColumn nFillFeeColumn = e.GridModel.Columns.Find(c => c.DataField == "nFillFee");
            nFillFeeColumn.FooterValue = nFillFee.ToString();
        }
        public ActionResult FillQuery()
        {
            var gridModel = new FillQueryGridModel();
            SetupFillQueryGridModel(gridModel.FillQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult FillQuery_RequestData()
        {
            var gridModel = new FillQueryGridModel();
            SetupFillQueryGridModel(gridModel.FillQueryGrid);
            var q = from d in Uow.vwFillFee.GetAll().Where(w=>w.nFillFee>0)
                    join d1 in Uow.tbAssociator.GetAll() on d.vcCardID equals d1.vcCardID into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        d.iSerial,
                        dd1s.vcAssName,
                        dd1s.vcAssType,
                        d.vcCardID,
                        d.nFillFee,
                        d.nFillProm,
                        d.nFeeLast,
                        d.nFeeCur,
                        d.vcComments,
                        d.dtFillDate,
                        d.vcOperName,
                        d.vcDeptID
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "vcOperName", true, "", "vcDeptID", "", deptCode, operName);
            return QueryAndExcel(gridModel.FillQueryGrid, q2, "消费统计查询.xls");
        }
        #endregion

        #region 消费分类统计
        private void SetupConsKindQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ConsKindQuery_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(ConsKindQuery_DataResolved);

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "dtConsDate");

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());
            SetDropDownColumn(grid, "vcGoodsType", centerCommon.GetGoodsType());

        }
        void ConsKindQuery_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            int? tolcount = e.FilterData.Select("tolcount").Cast<int?>().Sum();
            JQGridColumn tolcountColumn = e.GridModel.Columns.Find(c => c.DataField == "tolcount");
            tolcountColumn.FooterValue = tolcount.ToString();

            decimal? tolfee = e.FilterData.Select("tolfee").Cast<Decimal?>().Sum();
            JQGridColumn tolfeeColumn = e.GridModel.Columns.Find(c => c.DataField == "tolfee");
            tolfeeColumn.FooterValue = tolfee.ToString();
        }
        public ActionResult ConsKindQuery()
        {
            var gridModel = new ConsKindQueryGridModel();
            SetupConsKindQueryGridModel(gridModel.ConsKindQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult ConsKindQuery_RequestData(DateTime BeginDate,DateTime EndDate,bool IsAssType,bool IsDeptId,bool IsGoodsType,bool IsGoodsName)
        {
            var gridModel = new ConsKindQueryGridModel();
            SetupConsKindQueryGridModel(gridModel.ConsKindQueryGrid);

            var q = from d in Uow.vwConsItem.GetAll().Where(w=>w.dtConsDate>=BeginDate && w.dtConsDate<=EndDate&&w.cFlag=="0")
                    join d1 in Uow.tbAssociator.GetAll() on d.vcCardID equals d1.vcCardID into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    join d2 in Uow.tbGoods.GetAll() on d.vcGoodsID equals d2.vcGoodsID into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.vcDeptID,
                        dd1s.vcAssType,
                        dd2s.vcGoodsType,
                        dd2s.vcGoodsName,
                        d.iCount,
                        d.nFee,
                    };
            var q1 = from d in q
                     group d by new { d.vcDeptID, d.vcAssType, d.vcGoodsType, d.vcGoodsName } into g
                     select new
                     {
                         g.Key.vcDeptID,
                         g.Key.vcAssType,
                         g.Key.vcGoodsType,
                         g.Key.vcGoodsName,
                         tolcount = g.Sum(s => s.iCount),
                         tolfee = g.Sum(s => s.nFee),
                     };
            if (!IsAssType)
            {
                q1 = from d in q1
                     group d by new { d.vcDeptID, d.vcGoodsType, d.vcGoodsName } into g
                     select new
                     {
                         g.Key.vcDeptID,
                         vcAssType="",
                         g.Key.vcGoodsType,
                         g.Key.vcGoodsName,
                         tolcount = g.Sum(s => s.tolcount),
                         tolfee = g.Sum(s => s.tolfee),
                     };
            }
            if (!IsDeptId)
            {
                q1 = from d in q1
                     group d by new { d.vcAssType, d.vcGoodsType, d.vcGoodsName } into g
                     select new
                     {
                         vcDeptID = "",
                         g.Key.vcAssType,
                         g.Key.vcGoodsType,
                         g.Key.vcGoodsName,
                         tolcount = g.Sum(s => s.tolcount),
                         tolfee = g.Sum(s => s.tolfee),
                     };
            }
            if (!IsGoodsType)
            {
                q1 = from d in q1
                     group d by new { d.vcDeptID,d.vcAssType,d.vcGoodsName } into g
                     select new
                     {
                         g.Key.vcDeptID,
                         g.Key.vcAssType,
                         vcGoodsType="",
                         g.Key.vcGoodsName,
                         tolcount = g.Sum(s => s.tolcount),
                         tolfee = g.Sum(s => s.tolfee),
                     };
            }
            if (!IsGoodsName)
            {
                q1 = from d in q1
                     group d by new { d.vcDeptID, d.vcAssType, d.vcGoodsType } into g
                     select new
                     {
                         g.Key.vcDeptID,
                         g.Key.vcAssType,
                         g.Key.vcGoodsType,
                         vcGoodsName = "",
                         tolcount = g.Sum(s => s.tolcount),
                         tolfee = g.Sum(s => s.tolfee),
                     };
            }
            var q2 = businessCommon.SetVouchAuthority(q1, "", false, "", "vcDeptID", "", deptCode, "");
            return QueryAndExcel(gridModel.ConsKindQueryGrid, q2, "消费分类统计.xls");
        }
        #endregion

        #region 操作员日志
        private void SetupBusiLogQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BusiLogQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateTimeColumn(grid, "dtOperDate");

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcOperType", centerCommon.GetOP());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());
        }
        public ActionResult BusiLogQuery()
        {
            var gridModel = new BusiLogQueryGridModel();
            SetupBusiLogQueryGridModel(gridModel.BusiLogQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult BusiLogQuery_RequestData()
        {
            var gridModel = new BusiLogQueryGridModel();
            SetupBusiLogQueryGridModel(gridModel.BusiLogQueryGrid);
            var q = from d in Uow.vwBusiLog.GetAll()
                    join d1 in Uow.tbAssociator.GetAll() on d.vcCardID equals d1.vcCardID into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        d.iSerial,
                        dd1s.vcAssName,
                        dd1s.vcAssType,
                        d.vcCardID,                        
                        d.vcOperType,
                        d.vcOperName,
                        d.dtOperDate,                        
                        d.vcDeptID,
                        d.vcComments,
                    };
            var q2 = businessCommon.SetVouchAuthority(q, "vcOperName", true, "", "vcDeptID", "", deptCode, operName);
            return QueryAndExcel(gridModel.BusiLogQueryGrid, q2, "操作员日志.xls");
        }
        #endregion

        #region 商品销售排名榜
        private void SetupGoodsTopQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("GoodsTopQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDropDownColumn(grid, "vcGoodsType", centerCommon.GetGoodsType());
        }
        public ActionResult GoodsTopQuery()
        {
            var gridModel = new GoodsTopQueryGridModel();
            SetupGoodsTopQueryGridModel(gridModel.GoodsTopQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult GoodsTopQuery_RequestData(DateTime BeginDate,DateTime EndDate,string vcDeptID)
        {
            var gridModel = new GoodsTopQueryGridModel();
            SetupGoodsTopQueryGridModel(gridModel.GoodsTopQueryGrid);
            var q01 = from d in Uow.vwConsItem.GetAll().Where(w => w.cFlag == "0" && w.dtConsDate >= BeginDate && w.dtConsDate <= EndDate)
                      join d1 in Uow.tbGoods.GetAll() on d.vcGoodsID equals d1.vcGoodsID into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      select new { d.vcGoodsID, dd1s.vcGoodsType, dd1s.vcGoodsName, d.vcDeptID, d.iCount, d.nFee };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q01 = q01.Where(w => w.vcDeptID == vcDeptID);
            }
            //var q02 = businessCommon.SetVouchAuthority(q01, "", false, "", "vcDeptID", "", deptCode, "");
            var q0 = from d in q01
                     group d by new { d.vcGoodsID, d.vcGoodsType, d.vcGoodsName } into g
                     select new
                     {
                         g.Key.vcGoodsID,
                         g.Key.vcGoodsType,
                         g.Key.vcGoodsName,
                         SaleCount = g.Sum(s => s.iCount),
                         nFee = g.Sum(s => s.nFee)
                     };

            return QueryAndExcel(gridModel.GoodsTopQueryGrid, q0, "商品销售排名榜.xls");
        }
        #endregion

        #region 会员消费排行榜
        private void SetupCardTopQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("CardTopQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
        }
        public ActionResult CardTopQuery()
        {
            var gridModel = new CardTopQueryGridModel();
            SetupCardTopQueryGridModel(gridModel.CardTopQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult CardTopQuery_RequestData(DateTime BeginDate, DateTime EndDate, string vcDeptID)
        {
            var gridModel = new CardTopQueryGridModel();
            SetupGoodsTopQueryGridModel(gridModel.CardTopQueryGrid);

            var q11 = from d in Uow.vwConsItem.GetAll().Where(w => w.cFlag == "0" && w.dtConsDate >= BeginDate && w.dtConsDate <= EndDate)
                      join d1 in Uow.tbAssociator.GetAll() on d.vcCardID equals d1.vcCardID into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      select new { d.vcCardID, dd1s.vcAssName, d.vcDeptID, d.nFee };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q11 = q11.Where(w => w.vcDeptID == vcDeptID);
            }
            var q1 = from d in q11
                     group d by new { d.vcCardID, d.vcAssName } into g
                     select new
                     {
                         g.Key.vcCardID,
                         g.Key.vcAssName,
                         SaleFee = g.Sum(s => s.nFee)
                     };

            return QueryAndExcel(gridModel.CardTopQueryGrid, q1, "会员消费排行榜.xls");
        }
        #endregion

        #region 业务量统计
        void GroupingGrandTotal_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            int REP1 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP1")
                .Cast<int>().FirstOrDefault();

            int REP2 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP2")
                .Cast<int>().FirstOrDefault();

            decimal REP3 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP3")
                .Cast<decimal>().FirstOrDefault();

            decimal REP4 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP4")
                .Cast<decimal>().FirstOrDefault();
            decimal REP5 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP5")
                .Cast<decimal>().FirstOrDefault();
            decimal REP6 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP6")
                .Cast<decimal>().FirstOrDefault();
            int REP7 = e.FilterData.Where("Type=\"Total\"")
                .Select("REP7")
                .Cast<int>().FirstOrDefault();

            var REP1Column = e.GridModel.Columns.Find(c => c.DataField == "REP1");            
            REP1Column.FooterValue = "会员增加总数:"+REP1.ToString();

            var REP2Column = e.GridModel.Columns.Find(c => c.DataField == "REP2");            
            REP2Column.FooterValue = "积分增加总额:" + REP2.ToString();

            var REP3Column = e.GridModel.Columns.Find(c => c.DataField == "REP3");            
            REP3Column.FooterValue = "余额增加总额:" + REP3.ToString();

            var REP4Column = e.GridModel.Columns.Find(c => c.DataField == "REP4");            
            REP4Column.FooterValue = "现金增加总额:" + REP4.ToString();

            var REP5Column = e.GridModel.Columns.Find(c => c.DataField == "REP5");            
            REP5Column.FooterValue = "赠款增加总额:" + REP5.ToString();

            var REP6Column = e.GridModel.Columns.Find(c => c.DataField == "REP6");            
            REP6Column.FooterValue = "销售总额:" + REP6.ToString();

            var REP7Column = e.GridModel.Columns.Find(c => c.DataField == "REP7");            
            REP7Column.FooterValue = "商品销售总数:" + REP7.ToString();
        }
        private void SetupBusiIncomeGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BusiIncome_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetDropDownColumn(grid, "Type", centerCommon.GetBusiIncomeType());

            grid.DataResolved += new JQGridDataResolvedEventHandler(GroupingGrandTotal_DataResolved);

            GroupField groupField = new GroupField();
            groupField.DataField = "GroupType";
            groupField.GroupSortDirection = Trirand.Web.Mvc.SortDirection.Asc;
            groupField.HeaderText = "{0}";
            groupField.ShowGroupColumn = false;
            groupField.ShowGroupSummary = false;
            
            grid.GroupSettings.GroupFields.Add(groupField);
        }
        public ActionResult BusiIncome()
        {
            var gridModel = new BusiIncomeGridModel();
            SetupBusiIncomeGridModel(gridModel.BusiIncomeGrid);
            return PartialView(gridModel);
        }
        public ActionResult BusiIncome_RequestData(string vcDeptID,DateTime BeginDate,DateTime EndDate)
        {
            var gridModel = new BusiIncomeGridModel();
            SetupBusiIncomeGridModel(gridModel.BusiIncomeGrid);
            DateTime Yestody = BeginDate.AddDays(-1);
            string strBeginDate = BeginDate.ToString("yyyyMMdd");
            string strEndDate = EndDate.ToString("yyyyMMdd");
            string strYestody = Yestody.ToString("yyyyMMdd");

            string strGroupType = "0-所有门店";
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                var dept = Uow.Depts.GetAll().Where(w => w.DeptCode == vcDeptID).FirstOrDefault();
                if (dept != null)
                {
                    strGroupType = "0-" + dept.DeptName;
                }
            }
            
            string strDeptId = vcDeptID;
            if (string.IsNullOrEmpty(vcDeptID))
            {
                strDeptId = "%%";
            }
            int count = Uow.Db.ExecuteSqlCommand("sp_BusiIncomeReport @DeptID,@QueryBeginDate,@QueryEndDate,@YestodayDate",
                new SqlParameter("DeptID", strDeptId),
                new SqlParameter("QueryBeginDate", strBeginDate),
                new SqlParameter("QueryEndDate", strEndDate),
            new SqlParameter("YestodayDate", strYestody));

            string strDateZoom = strBeginDate + strEndDate;

            
            var q0 = from d in Uow.tbBusiIncomeReport.GetAll()
                     where d.vcDateZoom == strDateZoom &&
                     !d.Type.EndsWith("-L") &&
                     !d.Type.EndsWith("-O") &&
                     !d.Type.EndsWith("-X")
                     orderby d.ReNo
                     select new { d.vcDeptID,GroupType = strGroupType, d.Type, d.REP1, d.REP2, d.REP3, d.REP4, d.REP5, d.REP6, d.REP7 };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q0 = q0.Where(w => w.vcDeptID == vcDeptID);
            }
            var q1 = from d in Uow.tbBusiIncomeReport.GetAll()
                     where d.vcDateZoom == strDateZoom  &&
                     d.Type.EndsWith("-L")
                     orderby d.ReNo
                     select new { d.vcDeptID, GroupType = "1-本店会员在本店", d.Type, d.REP1, d.REP2, d.REP3, d.REP4, d.REP5, d.REP6, d.REP7 };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q1 = q1.Where(w => w.vcDeptID == vcDeptID);
            }
            var q2 = from d in Uow.tbBusiIncomeReport.GetAll()
                     where d.vcDateZoom == strDateZoom &&
                     d.Type.EndsWith("-O") 
                     orderby d.ReNo
                     select new { d.vcDeptID, GroupType = "2-他店会员在本店", d.Type, d.REP1, d.REP2, d.REP3, d.REP4, d.REP5, d.REP6, d.REP7 };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q2 = q2.Where(w => w.vcDeptID == vcDeptID);
            }
            var q3 = from d in Uow.tbBusiIncomeReport.GetAll()
                     where d.vcDateZoom == strDateZoom &&
                     d.Type.EndsWith("-X")
                     orderby d.ReNo
                     select new { d.vcDeptID, GroupType = "3-本店会员在他店", d.Type, d.REP1, d.REP2, d.REP3, d.REP4, d.REP5, d.REP6, d.REP7 };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q3 = q3.Where(w => w.vcDeptID == vcDeptID);
            }
            //if (!string.IsNullOrEmpty(vcDeptID))
            //{
                var q = q0.ToList().Union(q1.ToList()).Union(q2.ToList()).Union(q3.ToList()).AsQueryable();
                return QueryAndExcel(gridModel.BusiIncomeGrid, q, "业务量统计.xls");
            //}
            //else
            //{
            //    var q = q0.ToList().AsQueryable();
            //    return QueryAndExcel(gridModel.BusiIncomeGrid, q, "会员消费排行榜.xls");
            //}
        }
        #endregion

        #region 销售曲线图
        [Authorize]
        public ActionResult SalesChart(string dept, string month)
        {
            SalesChartModel r9 = new SalesChartModel();
            r9.dept = dept;
            r9.month = month;
            return PartialView(r9);
        }
        public void MyChart2(string dept, string month)
        {

            if (string.IsNullOrEmpty(month))
            {
                var chart2 = new Chart(900, 400, ChartTheme.Blue);
                chart2.AddTitle("-年-月各分店销售额日走势（元）");
                chart2.Write("png");
                return;
            }
            if (!string.IsNullOrEmpty(month))
            {
                //int iy = Convert.ToInt32(year);
                //int im = Convert.ToInt32(month);
                DateTime dt = DateTime.Parse(month+"-01");
                int iy = dt.Year;
                int im = dt.Month;
                var q = from d in Uow.tbConsItemOther.GetAll().Where(w=>w.dtConsDate.Value.Year==iy&&w.dtConsDate.Value.Month==im && w.cFlag=="0")
                        join d1 in Uow.tbCommCode.GetAll().Where(w=>w.vcCommSign=="MD") on d.vcDeptID equals d1.vcCommCode into dd1
                        from dd1s in dd1.DefaultIfEmpty()
                        select new { d.vcDeptID,vcDeptName=dd1s.vcCommName, SaleDay=d.dtConsDate.Value.Day,d.nFee };
                if (!string.IsNullOrEmpty(dept))
                {
                    q = q.Where(w => w.vcDeptID == dept);
                }
                var q1 = from d in q
                         group d by new { d.vcDeptID,d.vcDeptName, d.SaleDay } into g
                        select new 
                        {
                            g.Key.vcDeptID,
                            g.Key.vcDeptName,
                            g.Key.SaleDay,
                            SaleFee=g.Sum(s=>s.nFee/10000)
                        };


                var q2 = q1.OrderBy(o => o.vcDeptID).OrderBy(o => o.SaleDay).ToList();
                var q3 = from d in q2
                         group d by new { d.vcDeptID, d.vcDeptName } into g
                         select new { g.Key.vcDeptID, g.Key.vcDeptName };

                var chart = new Chart(900, 400, ChartTheme.Blue);
                chart.AddTitle(iy.ToString() + "年" + im.ToString().PadLeft(2,'0') + "月" + "各分店销售额日走势（元）");

                chart.AddLegend("门店", "dept");
                foreach (var ie in q3)
                {
                    int[] strd = q2.Where(w => w.vcDeptID == ie.vcDeptID).Select(s => s.SaleDay).ToArray();
                    decimal?[] stra = q2.Where(w => w.vcDeptID == ie.vcDeptID).Select(s => s.SaleFee).ToArray();

                    chart.AddSeries(
                    name: ie.vcDeptName,
                    xValue: strd,
                    //markerStep: 31,
                    yValues: stra,
                    chartType: "Line",
                    legend: "dept"
                    );

                    //chart.AddSeries(
                    //name: ie.vcDeptName,
                    //xValue: strd,
                    //    //markerStep: 31,
                    //yValues: stra,
                    //chartType: "Column",
                    //legend: "dept"
                    //);
                }
                chart.Write("png");
            }
            //else
            //{
            //    int iy = Convert.ToInt32(year);
            //    var q = from c in Uow.Consume.GetAll().Where(w => w.CreateDate.Year == iy)
            //            join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
            //            from cds in cd.DefaultIfEmpty()
            //            group new { c, cds }
            //            by new { c.UserId, c.DeptId, cds.OrganizationId, cds.DeptCode, cds.DeptName, c.CreateDate.Month } into g
            //            select new
            //            {
            //                g.Key.UserId,
            //                g.Key.DeptId,
            //                g.Key.OrganizationId,
            //                g.Key.DeptCode,
            //                g.Key.DeptName,
            //                g.Key.Month,
            //                Amount = g.Sum(s => s.c.Amount)
            //            };
            //    var q1 = businessCommon.SetVouchAuthority(q, "UserId", true);
            //    var q2 = q1.GroupBy("new(it.DeptName,it.DeptCode,it.Month)", "it").Select("new(it.key.DeptName,it.key.DeptCode,it.key.Month,it.Sum(Amount) as Amount)").ToList<Temp2Class>();
            //    var q3 = q2.OrderBy(o => o.DeptCode).OrderBy(o => o.Month).ToList();

            //    var q4 = from qs in q3
            //             group qs by new { qs.DeptCode, qs.DeptName } into g
            //             select new { g.Key.DeptCode, g.Key.DeptName };

            //    var chart = new Chart(900, 400, ChartTheme.Blue);
            //    chart.AddTitle(year + "年" + "各分店销售额月走势（元）");

            //    chart.AddLegend("门店", "dept");
            //    foreach (var ie in q4)
            //    {
            //        int[] strd = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Month).ToArray();
            //        decimal[] stra = q3.Where(w => w.DeptCode == ie.DeptCode).Select(s => s.Amount).ToArray();
            //        chart.AddSeries(
            //        name: ie.DeptName,
            //        xValue: strd,
            //        markerStep: 1,
            //        yValues: stra,
            //        chartType: "Line",
            //        legend: "dept"
            //        );
            //    }
            //    chart.Write("png");
            //}
        }
        #endregion

        #region 当日收银
        [HttpGet]
        public ActionResult DailyCashQuery()
        {
            
            var model = new DailyCashQueryModel();
            model.BeginDate = DateTime.Now.Date;
            model.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 0);
            model.CountResult = new List<DailyCashQueryCountResult>();
            model.FeeResult = new List<DailyCashQueryFeeResult>();
            model.lOperName = new List<string>();
            model.lConsType = new List<string>();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult DailyCashQuery(DailyCashQueryModel model)
        {
            var q11 = from d in Uow.vwBill.GetAll()
                    where d.dtConsDate >= model.BeginDate && d.dtConsDate <= model.EndDate
                    select new { d.iSerial,d.vcDeptID, d.vcOperName, d.vcConsType, d.nPay, d.nBalance };
            
            var q12 = from d in Uow.vwConsItem.GetAll()
                     where d.cFlag!="9" && d.dtConsDate>=model.BeginDate && d.dtConsDate<=model.EndDate
                     select new {d.iSerial,d.vcDeptID,d.vcOperName};
            if (!string.IsNullOrEmpty(model.vcDeptId))
            {
                q11 = q11.Where(w => w.vcDeptID == model.vcDeptId);
                q12 = q12.Where(w => w.vcDeptID == model.vcDeptId);
            }
            if (!string.IsNullOrEmpty(model.vcOperName))
            {
                q11 = q11.Where(w => w.vcOperName == model.vcOperName);
                q12 = q12.Where(w => w.vcOperName == model.vcOperName);
            }
            var q13 = from d in q11
                     where ((from d1 in q12 select d1.iSerial).Distinct().Contains(d.iSerial))
                     group d by new { d.vcOperName, d.vcConsType } into g
                      select new DailyCashQueryResult 
                      {
                          vcOperName = g.Key.vcOperName,
                          vcConsType=g.Key.vcConsType,
                          ConsCount = g.Count(),
                          ConsFee = g.Sum(s => s.nPay - s.nBalance)
                      };

            var q21 = from d in Uow.vwFillFee.GetAll()
                      where d.nFillFee > 0 && d.dtFillDate >= model.BeginDate && d.dtFillDate <= model.EndDate &&
                      d.vcComments != "银行卡" && 
                      !d.vcComments.Contains("补卡") && 
                      !d.vcComments.Contains("补充值") &&
                      //!d.vcComments.Contains("消费撤消") && 
                      !d.vcComments.StartsWith("回收卡") && 
                      !d.vcComments.StartsWith("合并") &&
                      !d.vcComments.Contains("撤消")
                      select new { d.vcOperName, d.nFillFee, d.vcDeptID };
            if (!string.IsNullOrEmpty(model.vcDeptId))
            {
                q21 = q21.Where(w => w.vcDeptID == model.vcDeptId);
            }
            if (!string.IsNullOrEmpty(model.vcOperName))
            {
                q21 = q21.Where(w => w.vcOperName == model.vcOperName);
            }
            var q23 = from d in q21
                      group d by d.vcOperName into g
                      select new DailyCashQueryResult
                      {
                          vcOperName = g.Key,
                          vcConsType = "Fill",
                          ConsCount = g.Count(),
                          ConsFee = g.Sum(s => s.nFillFee)
                      };

            var q31 = from d in Uow.vwFillFee.GetAll()
                      where d.nFillFee>0 && 
                      d.dtFillDate >= model.BeginDate && 
                      d.dtFillDate <= model.EndDate &&
                      d.vcComments == "银行卡"
                      select new { d.vcOperName, d.nFillFee, d.vcDeptID };
            if (!string.IsNullOrEmpty(model.vcDeptId))
            {
                q31 = q31.Where(w => w.vcDeptID == model.vcDeptId);
            }
            if (!string.IsNullOrEmpty(model.vcOperName))
            {
                q31 = q31.Where(w => w.vcOperName == model.vcOperName);
            }
            var q33 = from d in q31
                      group d by d.vcOperName into g
                      select new DailyCashQueryResult
                      {
                          vcOperName = g.Key,
                          vcConsType = "FillBank",
                          ConsCount = g.Count(),
                          ConsFee = g.Sum(s => s.nFillFee)
                      };

            var q41 = from d in Uow.vwFillFee.GetAll()
                      where //d.nFillFee > 0 &&
                      d.dtFillDate >= model.BeginDate &&
                      d.dtFillDate <= model.EndDate &&
                      d.vcComments.StartsWith("回收卡")
                      select new { d.vcOperName, d.nFillFee, d.vcDeptID };
            if (!string.IsNullOrEmpty(model.vcDeptId))
            {
                q41 = q41.Where(w => w.vcDeptID == model.vcDeptId);
            }
            if (!string.IsNullOrEmpty(model.vcOperName))
            {
                q41 = q41.Where(w => w.vcOperName == model.vcOperName);
            }
            var q43 = from d in q41
                      group d by d.vcOperName into g
                      select new DailyCashQueryResult
                      {
                          vcOperName = g.Key,
                          vcConsType = "CradRoll",
                          ConsCount = g.Count(),
                          ConsFee = g.Sum(s => s.nFillFee)
                      };

            var q5 = q13.ToList().Union(q23.ToList()).Union(q33.ToList()).Union(q43.ToList()).OrderBy(o=>o.vcConsType).OrderBy(o=>o.vcConsType).ToList();
            model.lOperName = q5.Select(s => s.vcOperName).Distinct().ToList();
            model.lConsType = q5.Select(s => s.vcConsType).Distinct().ToList();
            model.lConsType.Add("Cash");
            List<DailyCashQueryCountResult> lCountResult = new List<DailyCashQueryCountResult>();
            foreach (var q in q5)
            {
                DailyCashQueryCountResult countResult = new DailyCashQueryCountResult();
                if (q.vcConsType == "PT001" || q.vcConsType == "PT002" || q.vcConsType == "PT008")
                {
                    countResult.vcConsType = "PT001";
                }
                else
                {
                    countResult.vcConsType = q.vcConsType;
                }
                countResult.vcOperName = q.vcOperName;
                countResult.ConsCount = q.ConsCount;
                lCountResult.Add(countResult);
            }
            var q6 = from d in lCountResult
                     group d by new { d.vcOperName, d.vcConsType } into g
                     select new DailyCashQueryCountResult { vcOperName = g.Key.vcOperName, vcConsType = g.Key.vcConsType, ConsCount = g.Sum(s => s.ConsCount) };

            model.CountResult = q6.OrderBy(o => o.vcConsType).OrderBy(o => o.vcConsType).ToList();

            List<DailyCashQueryFeeResult> lFeeResult = new List<DailyCashQueryFeeResult>();
            foreach (var q in q5)
            {
                DailyCashQueryFeeResult feeResult = new DailyCashQueryFeeResult();
                feeResult.vcConsType = q.vcConsType;
                feeResult.vcOperName = q.vcOperName;
                feeResult.ConsFee = q.ConsFee;
                lFeeResult.Add(feeResult);
            }
            var q8 = from d in q5
                     where d.vcConsType == "PT002" || d.vcConsType == "Fill" || d.vcConsType == "CradRoll"
                     select new { d.ConsFee, d.vcConsType, d.vcOperName };
            var q7 = (from d in q8
                     group d by d.vcOperName into g
                     select new { vcOperName = g.Key, ConsFee = g.Sum(s => s.ConsFee) });
            foreach (var q in q7)
            {
                DailyCashQueryFeeResult feeResult = new DailyCashQueryFeeResult();
                feeResult.vcConsType = "Cash";
                feeResult.vcOperName = q.vcOperName;
                feeResult.ConsFee = q.ConsFee;
                lFeeResult.Add(feeResult);
            }
            model.FeeResult = lFeeResult;
            return PartialView(model);
        }
        #endregion

        #region 特殊消费统计
        private void SetupSpecConsQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("SpecConsQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";
            SetDropDownColumn(grid, "vcConsType", centerCommon.GetPT());
        }
        public ActionResult SpecConsQuery()
        {
            var gridModel = new SpecConsQueryGridModel();
            SetupSpecConsQueryGridModel(gridModel.SpecConsQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult SpecConsQuery_RequestData(DateTime BeginDate, DateTime EndDate, string vcDeptID,string vcOperName,string vcConsType)
        {
            var gridModel = new SpecConsQueryGridModel();
            SetupGoodsTopQueryGridModel(gridModel.SpecConsQueryGrid);

            var q = from d in Uow.tbBillOther.GetAll().Where(w => w.vcConsType == "PT003" || w.vcConsType == "PT004" || w.vcConsType == "PT005" || w.vcConsType == "PT006" || w.vcConsType == "PT007")
                    join d1 in Uow.tbConsItemOther.GetAll().Where(w => w.dtConsDate >= BeginDate && w.dtConsDate <= EndDate && w.cFlag == "0") on new { key1 = d.vcDeptID, key2 = d.iSerial } equals new { key1 = d1.vcDeptID, key2 = d1.iSerial }
                    join d2 in Uow.tbGoods.GetAll() on d1.vcGoodsID equals d2.vcGoodsID into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.vcConsType,
                        dd2s.vcGoodsName,
                        d1.iCount,
                        dd2s.nPrice,
                        d1.nFee,
                        d.vcOperName,
                        d.vcDeptID
                    };
            if (!string.IsNullOrEmpty(vcDeptID))
            {
                q = q.Where(w => w.vcDeptID == vcDeptID);
            }
            if (!string.IsNullOrEmpty(vcOperName))
            {
                q = q.Where(w => w.vcOperName == vcOperName);
            }
            if (!string.IsNullOrEmpty(vcConsType))
            {
                q = q.Where(w => w.vcConsType == vcConsType);
            }
            var q1 = from d in q
                     group d by new { d.vcConsType, d.vcGoodsName } into g
                     select new
                     {
                         g.Key.vcConsType,
                         g.Key.vcGoodsName,
                         tolCount = g.Sum(s => s.iCount),
                         tolfee = g.Sum(s => s.iCount * s.nPrice),
                         tolcash = g.Sum(s => s.nFee)
                     };
            return QueryAndExcel(gridModel.SpecConsQueryGrid, q1, "特殊消费统计.xls");
        }
        #endregion

        #region 销售平衡表
        private void SetupSaleBalanceGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("SaleBalance_RequestData");
            grid.DataType = "local";
            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(SaleBalance_DataResolved);

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDateColumn(grid, "BalanceDate");
            SetBoolColumn(grid, "IsBalance");
        }
        void SaleBalance_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            int? InQuantity = e.FilterData.Select("InQuantity").Cast<int?>().Sum();
            JQGridColumn InQuantityColumn = e.GridModel.Columns.Find(c => c.DataField == "InQuantity");
            InQuantityColumn.FooterValue = InQuantity.ToString();

            int? SaleQuantity = e.FilterData.Select("SaleQuantity").Cast<int?>().Sum();
            JQGridColumn SaleQuantityColumn = e.GridModel.Columns.Find(c => c.DataField == "SaleQuantity");
            SaleQuantityColumn.FooterValue = SaleQuantity.ToString();

            int? CheckQuantity = e.FilterData.Select("CheckQuantity").Cast<int?>().Sum();
            JQGridColumn CheckQuantityColumn = e.GridModel.Columns.Find(c => c.DataField == "CheckQuantity");
            CheckQuantityColumn.FooterValue = CheckQuantity.ToString();
        }
        public ActionResult SaleBalance()
        {
            var gridModel = new SaleBalanceGridModel();
            SetupSaleBalanceGridModel(gridModel.SaleBalanceGrid);
            return PartialView(gridModel);
        }
        public ActionResult SaleBalance_RequestData(DateTime BalanceDate, string vcGoodsType, string vcDeptId, bool? IsBalance)
        {
            var gridModel = new SaleBalanceGridModel();
            SetupSaleBalanceGridModel(gridModel.SaleBalanceGrid);
            DateTime dtNext = BalanceDate.AddDays(1);
            DateTime dtLast = BalanceDate.AddDays(-1);

            var ci = from d in Uow.vwConsItem.GetAll().Where(w => w.cFlag == "0" && w.dtConsDate>=BalanceDate && w.dtConsDate<=dtNext)
                     group d by new { vcDeptId = d.vcDeptID, vcGoodsId = d.vcGoodsID, SaleDate = DbFunctions.TruncateTime(d.dtConsDate).Value } into g
                     select new
                     {
                         g.Key.vcDeptId,
                         g.Key.vcGoodsId,
                         g.Key.SaleDate,
                         Quantity=g.Sum(s=>s.iCount),
                     };
            var q1 = from d in Uow.ProductionInStorage.GetAll().Where(w => w.InDate == BalanceDate)
                     join d1 in Uow.SaleCheck.GetAll().Where(w => w.CheckDate == dtLast) 
                     on new { key1 = d.vcDeptId, key2 = d.vcGoodsId } equals new { key1 = d1.vcDeptId, key2 = d1.vcGoodsId } into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     select new
                     {
                         vcGoodsId = d.vcGoodsId == null ? dd1s.vcGoodsId : d.vcGoodsId,
                         vcDeptId = d.vcDeptId == null ? dd1s.vcDeptId : d.vcDeptId,
                         InDate = BalanceDate,
                         LastCheckQuantity = dd1s.Quantity == null ? 0 : dd1s.Quantity,
                         InQuantity = d.Quantity == null ? 0 : d.Quantity,
                     };

            var q2 = from d in Uow.SaleCheck.GetAll().Where(w => w.CheckDate == dtLast)
                     join d1 in Uow.ProductionInStorage.GetAll().Where(w => w.InDate == BalanceDate) 
                     on new { key1 = d.vcDeptId, key2 = d.vcGoodsId} equals new { key1 = d1.vcDeptId, key2 = d1.vcGoodsId} into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     //where d.CheckDate == dtLast && dd1s.InDate==BalanceDate
                     select new
                     {
                         vcGoodsId = d.vcGoodsId == null ? dd1s.vcGoodsId : d.vcGoodsId,
                         vcDeptId = d.vcDeptId == null ? dd1s.vcDeptId : d.vcDeptId,
                         InDate = BalanceDate,
                         LastCheckQuantity = d.Quantity == null ? 0 : d.Quantity,
                         InQuantity = dd1s.Quantity == null ? 0 : dd1s.Quantity,
                     };
            var q3 = q1.Union(q2);

            var q = from d in q3//Uow.ProductionInStorage.GetAll().Where(w => w.InDate == BalanceDate)
                    join d1 in ci on new { key1 = d.vcDeptId, key2 = d.vcGoodsId, key3 = d.InDate } equals new { key1 = d1.vcDeptId, key2 = d1.vcGoodsId, key3 = d1.SaleDate } into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    join d2 in Uow.SaleCheck.GetAll().Where(w => w.CheckDate == BalanceDate) on new { key1 = d.vcDeptId, key2 = d.vcGoodsId, key3 = d.InDate } equals new { key1 = d2.vcDeptId, key2 = d2.vcGoodsId, key3 = d2.CheckDate } into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    join d3 in Uow.tbCommCode.GetAll().Where(w=>w.vcCommSign=="MD") on d.vcDeptId equals d3.vcCommCode into dd3
                    from dd3s in dd3.DefaultIfEmpty()
                    join d4 in Uow.tbGoods.GetAll() on d.vcGoodsId equals d4.vcGoodsID into dd4
                    from dd4s in dd4.DefaultIfEmpty()
                    join d5 in Uow.tbCommCode.GetAll().Where(w => w.vcCommSign == "GT") on dd4s.vcGoodsType equals d5.vcCommCode into dd5
                    from dd5s in dd5.DefaultIfEmpty()
                    select new
                    {
                        d.vcDeptId,
                        BalanceDate = d.InDate,
                        vcDeptName=dd3s.vcCommName,
                        dd5s.vcCommCode,
                        vcGoodsTypeName=dd5s.vcCommName,
                        dd4s.vcGoodsName,
                        dd4s.nPrice,
                        LastCheckQuantity = d.LastCheckQuantity,
                        InQuantity = d.InQuantity,
                        SaleQuantity = dd1s.Quantity==null?0:dd1s.Quantity,
                        CheckQuantity = dd2s.Quantity==null?0:dd2s.Quantity,
                        Differences = (d.LastCheckQuantity + d.InQuantity - (dd1s.Quantity == null ? 0 : dd1s.Quantity) - (dd2s.Quantity == null ? 0 : dd2s.Quantity)) * dd4s.nPrice * -1,
                        IsBalance = d.LastCheckQuantity+d.InQuantity - (dd1s.Quantity == null ? 0 : dd1s.Quantity) - (dd2s.Quantity == null ? 0 : dd2s.Quantity) == 0,
                    };
            if (!string.IsNullOrEmpty(vcDeptId))
            {
                q = q.Where(w => w.vcDeptId == vcDeptId);
            }
            if (IsBalance.HasValue)
            {
                q = q.Where(w => w.IsBalance == IsBalance);
            }
            if (!string.IsNullOrEmpty(vcGoodsType))
            {
                q = q.Where(w => w.vcCommCode == vcGoodsType);
            }
            return QueryAndExcel(gridModel.SaleBalanceGrid, q, "销售平衡表.xls");
        }
        #endregion

        #endregion

        #region AMSApp 结算报表

        #region 结算报表
        private void SetupBalanceQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("BalanceQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            grid.HeaderGroups.Add(new JQGridHeaderGroup
                {
                    StartColumnName = "vcLocalDeptName",
                    NumberOfColumns = 2,
                    TitleText = "门店"
                });
            grid.HeaderGroups.Add(new JQGridHeaderGroup
            {
                StartColumnName = "nFillFee_Pay",
                NumberOfColumns = 4,
                TitleText = "支出"
            });
            grid.HeaderGroups.Add(new JQGridHeaderGroup
            {
                StartColumnName = "nFillFee_Income",
                NumberOfColumns = 4,
                TitleText = "收入"
            });
            grid.HeaderGroups.Add(new JQGridHeaderGroup
            {
                StartColumnName = "nFillFee_Dif",
                NumberOfColumns = 4,
                TitleText = "差额(收入-支出)"
            });

        }
        public ActionResult BalanceQuery()
        {
            var gridModel = new BalanceQueryGridModel();
            SetupBalanceQueryGridModel(gridModel.BalanceQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult BalanceQuery_RequestData(DateTime BeginDate, DateTime EndDate, string vcDeptID)
        {
            var gridModel = new BalanceQueryGridModel();
            SetupBalanceQueryGridModel(gridModel.BalanceQueryGrid);
            IEnumerable<BalanceQueryModel> ibqm = Uow.Db.SqlQuery<BalanceQueryModel>("proc_Balance_Report @BeginDate,@EndDate,@DeptId",
                new SqlParameter("BeginDate", BeginDate),
                new SqlParameter("EndDate", EndDate),
                new SqlParameter("DeptId", vcDeptID));
            int i=0;
            List<BalanceQueryModel> lbqm = ibqm.ToList();
            foreach (BalanceQueryModel bqm in lbqm)
            {
                i = i + 1;
                bqm.Id = i;
            }
            return QueryAndExcel(gridModel.BalanceQueryGrid, lbqm.AsQueryable(), "结算报表.xls");
        }
        public ActionResult ComputeBalance(int Days)
        {
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var q = Uow.Db.SqlQuery<int>("Proc_Balance_ByDate @BeforeDate", new SqlParameter("BeforeDate", Days));
            }
            catch (Exception ex)
            {
                json.Data = new { Error = ex.Message };
                return json;
            }
            json.Data = new { Error = "" };
            return json;
        }
        #endregion

        #region 消费门店查询
        private void SetupConsItemMdGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("ConsItemMd_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcLocalDeptId", centerCommon.GetMD());
            SetDropDownColumn(grid, "vcConsType", centerCommon.GetPT());
            SetDropDownColumn(grid, "cFlag", centerCommon.GetFlag());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());

            SetDateTimeColumn(grid, "dtConsDate");

            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(ConsItemMd_DataResolved);
        }
        void ConsItemMd_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? nFee = e.FilterData.Select("nFee").Cast<Decimal?>().Sum();
            JQGridColumn nFeeColumn = e.GridModel.Columns.Find(c => c.DataField == "nFee");
            nFeeColumn.FooterValue = nFee.ToString();
        }
        public ActionResult ConsItemMd()
        {
            var gridModel = new ConsItemMdGridModel();
            SetupConsItemMdGridModel(gridModel.ConsItemMdGrid);
            return PartialView(gridModel);
        }
        public ActionResult ConsItemMd_RequestData()
        {
            var gridModel = new ConsItemMdGridModel();
            SetupConsItemMdGridModel(gridModel.ConsItemMdGrid);
            var q = from d in Uow.vwConsItem.GetAll()
                    join d1 in Uow.vwBill.GetAll() on new { key1 = d.iSerial, key2 = d.vcCardID, key3 = d.vcDeptID } equals new { key1 = d1.iSerial, key2 = d1.vcCardID, key3 = d1.vcDeptID }
                    join d2 in Uow.tbAssociator.GetAll() on d.vcCardID equals d2.vcCardID into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.iSerial,
                        dd2s.vcAssName,
                        dd2s.vcAssType,
                        d.vcCardID,
                        vcLocalDeptId=dd2s.vcDeptID,
                        d.vcGoodsID,
                        d.nPrice,
                        d.iCount,
                        d.nFee,
                        d1.vcConsType,
                        d.vcComments,
                        d.cFlag,
                        d.dtConsDate,
                        d.vcOperName,
                        d.vcDeptID,
                    };
            return QueryAndExcel(gridModel.ConsItemMdGrid, q, "消费门店查询.xls");
        }
        #endregion

        #region 充值门店查询
        private void SetupFillMdQueryGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("FillMdQuery_RequestData");
            grid.DataType = "local";

            grid.ClientSideEvents.SerializeGridData = "serializeGridData";

            SetDropDownColumn(grid, "vcAssType", centerCommon.GetAssType());
            SetDropDownColumn(grid, "vcLocalDeptId", centerCommon.GetMD());
            SetDropDownColumn(grid, "vcDeptID", centerCommon.GetMD());

            SetDateTimeColumn(grid, "dtFillDate");

            grid.AppearanceSettings.ShowFooter = true;
            grid.DataResolved += new JQGridDataResolvedEventHandler(FillMdQuery_DataResolved);
        }
        void FillMdQuery_DataResolved(object sender, JQGridDataResolvedEventArgs e)
        {
            decimal? nFillFee = e.FilterData.Select("nFillFee").Cast<Decimal?>().Sum();
            JQGridColumn nFillFeeColumn = e.GridModel.Columns.Find(c => c.DataField == "nFillFee");
            nFillFeeColumn.FooterValue = nFillFee.ToString();
        }
        public ActionResult FillMdQuery()
        {
            var gridModel = new FillMdQueryGridModel();
            SetupFillMdQueryGridModel(gridModel.FillMdQueryGrid);
            return PartialView(gridModel);
        }
        public ActionResult FillMdQuery_RequestData(string FillType)
        {
            var gridModel = new FillMdQueryGridModel();
            SetupFillMdQueryGridModel(gridModel.FillMdQueryGrid);
            var q = from d in Uow.vwFillFee.GetAll()
                    join d1 in Uow.tbAssociator.GetAll() on d.vcCardID equals d1.vcCardID into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        d.iSerial,
                        dd1s.vcAssName,
                        dd1s.vcAssType,
                        d.vcCardID,
                        vcLocalDeptId=dd1s.vcDeptID,
                        d.nFillFee,
                        d.nFillProm,
                        d.nFeeLast,
                        d.nFeeCur,
                        d.vcComments,
                        d.dtFillDate,
                        d.vcOperName,
                        d.vcDeptID,
                    };
            if (FillType == "Norm")
            {
                q = q.Where(w => (w.vcComments == "" || w.vcComments == "银行卡") && w.nFillFee > 0);
            }
            else
            {
                q = q.Where(w => w.vcComments.Contains(FillType));
            }
            return QueryAndExcel(gridModel.FillMdQueryGrid, q, "消费门店查询.xls");
        }
        #endregion

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
}
