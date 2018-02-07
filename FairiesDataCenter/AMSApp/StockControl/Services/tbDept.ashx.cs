
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
    /// 部门
    /// </summary>
    public class tbDept : MyHttpHandlerBase
    {
        public tbDept(IAMSCMUow uow)
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
                    context.Response.Write(GettbDept(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbDept(context));
                    break;
                case "update":
                    context.Response.Write(updatetbDept(context));
                    break;
                case "new":
                    context.Response.Write(newtbDept(context));
                    break;
                case "remove":
                    context.Response.Write(removetbDept(context));
                    break;
                case "excel":
                    tbDeptExportToExcel(context);
                    break;
            }
        }
        private IQueryable<DXInfo.Models.tbDept> QueryCondition(HttpContext context)
        {
            string strcnvcDeptName = context.Request["cnvcDeptName"] == null ? string.Empty : context.Request["cnvcDeptName"];
            string strcnvcDeptID = context.Request["cnvcDeptID"] == null ? string.Empty : context.Request["cnvcDeptID"];
            string strcnvcDeptType = context.Request["cnvcDeptType"] == null ? string.Empty : context.Request["cnvcDeptType"];
            string strcnvcParentDeptID = context.Request["cnvcParentDeptID"] == null ? string.Empty : context.Request["cnvcParentDeptID"];
            string strcnvcComments = context.Request["cnvcComments"] == null ? string.Empty : context.Request["cnvcComments"];
            string strcnnPriority = context.Request["cnnPriority"] == null ? string.Empty : context.Request["cnnPriority"];


            var q = from p in Uow.tbDept.GetAll() select p;
            if (strcnvcDeptName != string.Empty) q = q.Where(w => w.cnvcDeptName == strcnvcDeptName);
            if (strcnvcDeptID != string.Empty) q = q.Where(w => w.cnvcDeptID == strcnvcDeptID);
            if (strcnvcDeptType != string.Empty) q = q.Where(w => w.cnvcDeptType == strcnvcDeptType);
            if (strcnvcParentDeptID != string.Empty) q = q.Where(w => w.cnvcParentDeptID == strcnvcParentDeptID);
            if (strcnvcComments != string.Empty) q = q.Where(w => w.cnvcComments == strcnvcComments);
            if (strcnnPriority != string.Empty) q = q.Where(w => w.cnnPriority == Convert.ToInt32(strcnnPriority));

            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbDept> ltbDept)
        {
            DataTable dt = ltbDept.ToDataTable<DXInfo.Models.tbDept>();
            ServiceHelper.DataTableConvert(context, dt, "cnvcDeptType", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='DeptType'");
            return dt;
        }
        private string GettbDept(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<DXInfo.Models.tbDept> ltbDept = new List<DXInfo.Models.tbDept>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbDept = q.OrderBy(o => o.cnvcDeptID)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbDept);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newtbDept(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbDept tbDept = new DXInfo.Models.tbDept();
                    tbDept.cnvcDeptName = context.Request.Form["cnvcDeptName"];
                    tbDept.cnvcDeptID = context.Request.Form["cnvcDeptID"];
                    tbDept.cnvcDeptType = context.Request.Form["cnvcDeptType"];
                    tbDept.cnvcParentDeptID = context.Request.Form["cnvcParentDeptID"];
                    tbDept.cnvcComments = context.Request.Form["cnvcComments"];
                    tbDept.cnnPriority = Convert.ToInt32(context.Request.Form["cnnPriority"]);

                    Uow.tbDept.Add(tbDept);
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
        private string updatetbDept(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbDept tbDept = Uow.tbDept.GetById(g=>g.cnvcDeptID==context.Request.Form["cnvcDeptID"]);
                    tbDept.cnvcDeptName = context.Request.Form["cnvcDeptName"];
                    tbDept.cnvcDeptID = context.Request.Form["cnvcDeptID"];
                    tbDept.cnvcDeptType = context.Request.Form["cnvcDeptType"];
                    tbDept.cnvcParentDeptID = context.Request.Form["cnvcParentDeptID"];
                    tbDept.cnvcComments = context.Request.Form["cnvcComments"];
                    tbDept.cnnPriority = Convert.ToInt32(context.Request.Form["cnnPriority"]);

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
        private string removetbDept(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbDept tbDept = Uow.tbDept.GetById(g=>g.cnvcDeptID==context.Request.Form["cnvcDeptID"]);
                    Uow.tbDept.Delete(tbDept);
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
        private void tbDeptExportToExcel(HttpContext context)
        {
            string fileName = "部门.xls";

            List<DXInfo.Models.tbDept> ltbDept = new List<DXInfo.Models.tbDept>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbDept = q.OrderBy(o => o.cnvcDeptID).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbDept);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

            field = new BoundField();
            field.DataField = "cnvcDeptName";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDeptID";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDeptType";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcParentDeptID";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcComments";
            field.HeaderText = "";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnnPriority";
            field.HeaderText = "";
            view.Columns.Add(field);


            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAlltbDept(HttpContext context)
        {
            List<DXInfo.Models.tbDept> ltbDept = new List<DXInfo.Models.tbDept>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                ltbDept = Uow.tbDept.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, ltbDept);
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
