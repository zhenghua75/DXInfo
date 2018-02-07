using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.SessionState;
using Ninject;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// tbLogin 的摘要说明
    /// </summary>
    public class tbLogin : MyHttpHandlerBase
    {
        public tbLogin(IAMSCMUow uow)
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
                    context.Response.Write(GettbLogin(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbLogin(context));
                    break;
                case "update":
                    //context.Response.Write(updatetbComputationUnit(context));
                    break;
                case "new":
                    //context.Response.Write(newtbComputationUnit(context));
                    break;
                case "remove":
                    //context.Response.Write(removetbComputationUnit(context));
                    break;
                case "excel":
                    //tbComputationUnitExportToExcel(context);
                    break;
            }
        }
        private IQueryable<tbLoginSub> QueryCondition(HttpContext context)
        {
            string strvcDeptID = context.Request["vcDeptID"] == null ? string.Empty : context.Request["vcDeptID"];
            string strvcLoginID = context.Request["vcLoginID"] == null ? string.Empty : context.Request["vcLoginID"];
            string strvcOperName = context.Request["vcOperName"] == null ? string.Empty : context.Request["vcOperName"];
            string strq = context.Request["q"] == null ? string.Empty : context.Request["q"];

            var q = from p in Uow.tbLogin.GetAll() select new tbLoginSub { vcDeptID = p.vcDeptID, vcLoginID = p.vcLoginID, vcOperName = p.vcOperName };
            if (strvcDeptID != string.Empty) q = q.Where(w => w.vcDeptID == strvcDeptID);
            if (strvcLoginID != string.Empty) q = q.Where(w => w.vcLoginID == strvcLoginID);
            if (strvcOperName != string.Empty) q = q.Where(w => w.vcOperName == strvcOperName);

            if (strq != string.Empty) q = q.Where(w => w.vcLoginID.Contains(strq) || w.vcOperName.Contains(strq));
            return q;
        }
        private string GettbLogin(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<tbLoginSub> ltbLogin = new List<tbLoginSub>();

            int skitCount = (page - 1) * rows;
            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbLogin = q.OrderBy(o => o.vcLoginID)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbLogin);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private DataTable List2DataTable(HttpContext context, List<tbLoginSub> ltbLogin)
        {
            DataTable dt = ltbLogin.ToDataTable<tbLoginSub>();
            ServiceHelper.DataTableConvert(context, dt, "vcDeptID", ServiceHelper.Table_tbDept, "cnvcDeptID", "cnvcDeptName", "");
            return dt;
        }

        private string GetAlltbLogin(HttpContext context)
        {
            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{
            var ltbLogin = (from l in Uow.tbLogin.GetAll() select new { l.vcDeptID, l.vcLoginID, l.vcOperName }).ToList();
                DataTable dt = ltbLogin.ToDataTable();
                ServiceHelper.DataTableConvert(context, dt, "vcDeptID", ServiceHelper.Table_tbDept, "cnvcDeptID", "cnvcDeptName", "");
                return ServiceHelper.DataTableToEasyUIJson(dt);
            //}
            
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class tbLoginSub
    {
        public string vcDeptID { get; set; }
        public string vcLoginID { get; set; }
        public string vcOperName { get; set; }
    }
}