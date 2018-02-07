
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
    /// 库存主表
    /// </summary>
    public class tbStockMain : MyHttpHandlerBase
    {
        public tbStockMain(IAMSCMUow uow)
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
                    context.Response.Write(GettbStockMain(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbStockMain(context));
                    break;
                case "update":
                    context.Response.Write(updatetbStockMain(context));
                    break;
                case "new":
                    context.Response.Write(newtbStockMain(context));
                    break;
                case "remove":
                    context.Response.Write(removetbStockMain(context));
                    break;
                case "excel":
                    tbStockMainExportToExcel(context);
                    break;
            }
        }
        private IQueryable<DXInfo.Models.tbStockMain> QueryCondition(HttpContext context)
        {
            string strcnnId = context.Request["cnnId"] == null ? string.Empty : context.Request["cnnId"];
            string strcnvcSupplierCode = context.Request["cnvcSupplierCode"] == null ? string.Empty : context.Request["cnvcSupplierCode"];
            string strcnvcWhCode = context.Request["cnvcWhCode"] == null ? string.Empty : context.Request["cnvcWhCode"];
            string strcnvcDeptId = context.Request["cnvcDeptId"] == null ? string.Empty : context.Request["cnvcDeptId"];
            string strcnvcOperType = context.Request["cnvcOperType"] == null ? string.Empty : context.Request["cnvcOperType"];
            string strcndCreateDate = context.Request["cndCreateDate"] == null ? string.Empty : context.Request["cndCreateDate"];
            string strcnvcCreaterId = context.Request["cnvcCreaterId"] == null ? string.Empty : context.Request["cnvcCreaterId"];
            string strcnvcCreaterName = context.Request["cnvcCreaterName"] == null ? string.Empty : context.Request["cnvcCreaterName"];
            string strcndCheckDate = context.Request["cndCheckDate"] == null ? string.Empty : context.Request["cndCheckDate"];
            string strcnvcCheckerId = context.Request["cnvcCheckerId"] == null ? string.Empty : context.Request["cnvcCheckerId"];
            string strcnvcCheckerName = context.Request["cnvcCheckerName"] == null ? string.Empty : context.Request["cnvcCheckerName"];
            string strcndBusinessDate = context.Request["cndBusinessDate"] == null ? string.Empty : context.Request["cndBusinessDate"];
            string strcnnYear = context.Request["cnnYear"] == null ? string.Empty : context.Request["cnnYear"];
            string strcnnMonth = context.Request["cnnMonth"] == null ? string.Empty : context.Request["cnnMonth"];
            string strcnnStatus = context.Request["cnnStatus"] == null ? string.Empty : context.Request["cnnStatus"];
            string strcnvcComments = context.Request["cnvcComments"] == null ? string.Empty : context.Request["cnvcComments"];


            var q = from p in Uow.tbStockMain.GetAll() select p;
            if (strcnnId != string.Empty) q = q.Where(w => w.cnnMainId == Convert.ToInt64(strcnnId));
            if (strcnvcSupplierCode != string.Empty) q = q.Where(w => w.cnvcSupplierCode == strcnvcSupplierCode);
            if (strcnvcWhCode != string.Empty) q = q.Where(w => w.cnvcWhCode == strcnvcWhCode);
            if (strcnvcDeptId != string.Empty) q = q.Where(w => w.cnvcDeptId == strcnvcDeptId);
            //if (strcnvcOperType != string.Empty) q = q.Where(w => w.cnvcOperType == Convert.ToInt32(strcnvcOperType));
            if (strcndCreateDate != string.Empty) q = q.Where(w => w.cndCreateDate == Convert.ToDateTime(strcndCreateDate));
            if (strcnvcCreaterId != string.Empty) q = q.Where(w => w.cnvcCreaterId == strcnvcCreaterId);
            if (strcnvcCreaterName != string.Empty) q = q.Where(w => w.cnvcCreaterName == strcnvcCreaterName);
            if (strcndCheckDate != string.Empty) q = q.Where(w => w.cndCheckDate == Convert.ToDateTime(strcndCheckDate));
            if (strcnvcCheckerId != string.Empty) q = q.Where(w => w.cnvcCheckerId == strcnvcCheckerId);
            if (strcnvcCheckerName != string.Empty) q = q.Where(w => w.cnvcCheckerName == strcnvcCheckerName);
            if (strcndBusinessDate != string.Empty) q = q.Where(w => w.cndBusinessDate == Convert.ToDateTime(strcndBusinessDate));
            if (strcnnYear != string.Empty) q = q.Where(w => w.cnnYear == Convert.ToInt32(strcnnYear));
            if (strcnnMonth != string.Empty) q = q.Where(w => w.cnnMonth == Convert.ToInt32(strcnnMonth));
            if (strcnnStatus != string.Empty) q = q.Where(w => w.cnnStatus == Convert.ToInt32(strcnnStatus));
            if (strcnvcComments != string.Empty) q = q.Where(w => w.cnvcComments == strcnvcComments);

            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbStockMain> ltbStockMain)
        {
            DataTable dt = ltbStockMain.ToDataTable<DXInfo.Models.tbStockMain>();
            return dt;
        }
        private string GettbStockMain(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<DXInfo.Models.tbStockMain> ltbStockMain = new List<DXInfo.Models.tbStockMain>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbStockMain = q.OrderBy(o => o.cnnMainId)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockMain);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newtbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = new DXInfo.Models.tbStockMain();
                    tbStockMain.cnvcSupplierCode = context.Request.Form["cnvcSupplierCode"];
                    tbStockMain.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                    tbStockMain.cnvcDeptId = context.Request.Form["cnvcDeptId"];
                    tbStockMain.cnvcCreaterId = context.Request.Form["cnvcCreaterId"];
                    tbStockMain.cnvcCreaterName = context.Request.Form["cnvcCreaterName"];
                    tbStockMain.cndBusinessDate = Convert.ToDateTime(context.Request.Form["cndBusinessDate"]);
                    tbStockMain.cnnYear = Convert.ToInt32(context.Request.Form["cnnYear"]);
                    tbStockMain.cnnMonth = Convert.ToInt32(context.Request.Form["cnnMonth"]);
                    tbStockMain.cnnStatus = Convert.ToInt32(context.Request.Form["cnnStatus"]);
                    tbStockMain.cnvcComments = context.Request.Form["cnvcComments"];

                    Uow.tbStockMain.Add(tbStockMain);
                    Uow.Commit();
                //}
            }
            catch (DbUpdateException dex)
            {
                ExceptionPolicy.HandleException(dex, ServiceHelper.ExceptionPolicy);
                return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, dex.Message));
            }
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ""));
        }
        private string updatetbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==Convert.ToInt64(context.Request.Form["cnnId"]));

                    tbStockMain.cnvcSupplierCode = context.Request.Form["cnvcSupplierCode"];
                    tbStockMain.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                    tbStockMain.cnvcDeptId = context.Request.Form["cnvcDeptId"];
                    tbStockMain.cndCheckDate = Convert.ToDateTime(context.Request.Form["cndCheckDate"]);
                    tbStockMain.cnvcCheckerId = context.Request.Form["cnvcCheckerId"];
                    tbStockMain.cnvcCheckerName = context.Request.Form["cnvcCheckerName"];
                    tbStockMain.cndBusinessDate = Convert.ToDateTime(context.Request.Form["cndBusinessDate"]);
                    tbStockMain.cnnYear = Convert.ToInt32(context.Request.Form["cnnYear"]);
                    tbStockMain.cnnMonth = Convert.ToInt32(context.Request.Form["cnnMonth"]);
                    tbStockMain.cnnStatus = Convert.ToInt32(context.Request.Form["cnnStatus"]);
                    tbStockMain.cnvcComments = context.Request.Form["cnvcComments"];

                    Uow.Commit();
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
        private string removetbStockMain(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbStockMain tbStockMain = Uow.tbStockMain.GetById(g=>g.cnnMainId==Convert.ToInt64(context.Request.Form["cnnId"]));
                    Uow.tbStockMain.Delete(tbStockMain);
                    Uow.Commit();
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
        private void tbStockMainExportToExcel(HttpContext context)
        {
            string fileName = "库存主表.xls";

            List<DXInfo.Models.tbStockMain> ltbStockMain = new List<DXInfo.Models.tbStockMain>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbStockMain = q.OrderBy(o => o.cnnMainId).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockMain);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

            field = new BoundField();
            field.DataField = "cnnId";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcSupplierCode";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhCode";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDeptId";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcOperType";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndCreateDate";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCreaterId";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCreaterName";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndCheckDate";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCheckerId";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcCheckerName";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cndBusinessDate";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnYear";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnMonth";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnStatus";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcComments";
            field.HeaderText = "";
            view.Columns.Add(field);


            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAlltbStockMain(HttpContext context)
        {
            List<DXInfo.Models.tbStockMain> ltbStockMain = new List<DXInfo.Models.tbStockMain>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                ltbStockMain = Uow.tbStockMain.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockMain);
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
