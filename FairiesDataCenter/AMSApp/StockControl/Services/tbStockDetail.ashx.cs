
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// 库存子表
    /// </summary>
    public class tbStockDetail : MyHttpHandlerBase
    {
        public tbStockDetail(IAMSCMUow uow)
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
                    context.Response.Write(GettbStockDetail(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbStockDetail(context));
                    break;
                case "update":
                    context.Response.Write(updatetbStockDetail(context));
                    break;
                case "new":
                    context.Response.Write(newtbStockDetail(context));
                    break;
                case "remove":
                    context.Response.Write(removetbStockDetail(context));
                    break;
                case "excel":
                    tbStockDetailExportToExcel(context);
                    break;
            }
        }
        private IQueryable<DXInfo.Models.tbStockDetail> QueryCondition(HttpContext context)
        {
            string strcnnDetailId = context.Request["cnnDetailId"] == null ? string.Empty : context.Request["cnnDetailId"];
            string strcnnMainId = context.Request["cnnMainId"] == null ? string.Empty : context.Request["cnnMainId"];
            string strcnvcInvCode = context.Request["cnvcInvCode"] == null ? string.Empty : context.Request["cnvcInvCode"];
            string strcnvcComUnitCode = context.Request["cnvcComUnitCode"] == null ? string.Empty : context.Request["cnvcComUnitCode"];
            string strcnnQuantity = context.Request["cnnQuantity"] == null ? string.Empty : context.Request["cnnQuantity"];
            string strcnvcMainComUnitCode = context.Request["cnvcMainComUnitCode"] == null ? string.Empty : context.Request["cnvcMainComUnitCode"];
            string strcnnMainQuantity = context.Request["cnnMainQuantity"] == null ? string.Empty : context.Request["cnnMainQuantity"];
            string strcnnPrice = context.Request["cnnPrice"] == null ? string.Empty : context.Request["cnnPrice"];
            string strcnnAmount = context.Request["cnnAmount"] == null ? string.Empty : context.Request["cnnAmount"];


            var q = from p in Uow.tbStockDetail.GetAll() select p;
            if (strcnnDetailId != string.Empty) q = q.Where(w => w.cnnDetailId == Convert.ToInt64(strcnnDetailId));
            if (strcnnMainId != string.Empty) q = q.Where(w => w.cnnMainId == Convert.ToInt32(strcnnMainId));
            if (strcnvcInvCode != string.Empty) q = q.Where(w => w.cnvcInvCode == strcnvcInvCode);
            if (strcnvcComUnitCode != string.Empty) q = q.Where(w => w.cnvcComUnitCode == strcnvcComUnitCode);
            if (strcnnQuantity != string.Empty) q = q.Where(w => w.cnnQuantity == Convert.ToDecimal(strcnnQuantity));
            if (strcnvcMainComUnitCode != string.Empty) q = q.Where(w => w.cnvcMainComUnitCode == strcnvcMainComUnitCode);
            if (strcnnMainQuantity != string.Empty) q = q.Where(w => w.cnnMainQuantity == Convert.ToDecimal(strcnnMainQuantity));
            if (strcnnPrice != string.Empty) q = q.Where(w => w.cnnPrice == Convert.ToDecimal(strcnnPrice));
            if (strcnnAmount != string.Empty) q = q.Where(w => w.cnnAmount == Convert.ToDecimal(strcnnAmount));

            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbStockDetail> ltbStockDetail)
        {
            DataTable dt = ltbStockDetail.ToDataTable<DXInfo.Models.tbStockDetail>();
            return dt;
        }
        private string GettbStockDetail(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<DXInfo.Models.tbStockDetail> ltbStockDetail = new List<DXInfo.Models.tbStockDetail>();

            int skitCount = (page - 1) * rows;
            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbStockDetail = q.OrderBy(o => o.cnnDetailId)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockDetail);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newtbStockDetail(HttpContext context)
        {
            try
            {
                //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockDetail tbStockDetail = new DXInfo.Models.tbStockDetail();

                    tbStockDetail.cnnMainId = Convert.ToInt64(context.Request.Form["cnnMainId"]);
                    tbStockDetail.cnvcInvCode = context.Request.Form["cnvcInvCode"];
                    tbStockDetail.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                    tbStockDetail.cnnQuantity = Convert.ToDecimal(context.Request.Form["cnnQuantity"]);
                    tbStockDetail.cnvcMainComUnitCode = context.Request.Form["cnvcMainComUnitCode"];
                    tbStockDetail.cnnMainQuantity = Convert.ToDecimal(context.Request.Form["cnnMainQuantity"]);
                    tbStockDetail.cnnPrice = Convert.ToDecimal(context.Request.Form["cnnPrice"]);
                    tbStockDetail.cnnAmount = Convert.ToDecimal(context.Request.Form["cnnAmount"]);

                    Uow.tbStockDetail.Add(tbStockDetail);
                    Uow.Commit();//SaveChanges();
                //}
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
                //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockDetail tbStockDetail = Uow.tbStockDetail.GetById(g=>g.cnnDetailId==Convert.ToInt64(context.Request.Form["cnnId"]));

                    tbStockDetail.cnvcInvCode = context.Request.Form["cnvcInvCode"];
                    tbStockDetail.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                    tbStockDetail.cnnQuantity = Convert.ToDecimal(context.Request.Form["cnnQuantity"]);
                    tbStockDetail.cnvcMainComUnitCode = context.Request.Form["cnvcMainComUnitCode"];
                    tbStockDetail.cnnMainQuantity = Convert.ToDecimal(context.Request.Form["cnnMainQuantity"]);
                    tbStockDetail.cnnPrice = Convert.ToDecimal(context.Request.Form["cnnPrice"]);
                    tbStockDetail.cnnAmount = Convert.ToDecimal(context.Request.Form["cnnAmount"]);

                    Uow.Commit();//SaveChanges();
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
        private string removetbStockDetail(HttpContext context)
        {
            try
            {
                //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockDetail tbStockDetail = Uow.tbStockDetail.GetById(g=>g.cnnDetailId==Convert.ToInt64(context.Request.Form["cnnId"]));
                    Uow.tbStockDetail.Delete(tbStockDetail);
                    Uow.Commit();//SaveChanges();
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
        private void tbStockDetailExportToExcel(HttpContext context)
        {
            string fileName = "库存子表.xls";

            List<DXInfo.Models.tbStockDetail> ltbStockDetail = new List<DXInfo.Models.tbStockDetail>();

            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbStockDetail = q.OrderBy(o => o.cnnMainId).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockDetail);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

                field = new BoundField();
                field.DataField = "cnnId";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnnMainId";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnvcInvCode";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnvcComUnitCode";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnnQuantity";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnvcMainComUnitCode";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnnMainQuantity";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnnPrice";
                field.HeaderText = "";
                view.Columns.Add(field);
                field = new BoundField();
                field.DataField = "cnnAmount";
                field.HeaderText = "";
                view.Columns.Add(field);


            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAlltbStockDetail(HttpContext context)
        {
            List<DXInfo.Models.tbStockDetail> ltbStockDetail = new List<DXInfo.Models.tbStockDetail>();

            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{

                ltbStockDetail = Uow.tbStockDetail.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockDetail);
            return ServiceHelper.DataTableToEasyUIJson(dt);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
