
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
    /// 仓库
    /// </summary>
    public class tbWarehouse : MyHttpHandlerBase
    {
        public tbWarehouse(IAMSCMUow uow)
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
                    context.Response.Write(GettbWarehouse(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbWarehouse(context));
                    break;
                case "update":
                    context.Response.Write(updatetbWarehouse(context));
                    break;
                case "new":
                    context.Response.Write(newtbWarehouse(context));
                    break;
                case "remove":
                    context.Response.Write(removetbWarehouse(context));
                    break;
                case "excel":
                    tbWarehouseExportToExcel(context);
                    break;
            }
        }
        private IQueryable<DXInfo.Models.tbWarehouse> QueryCondition(HttpContext context)
        {
            string strcnvcWhCode = context.Request["cnvcWhCode"] == null ? string.Empty : context.Request["cnvcWhCode"];
            string strcnvcWhName = context.Request["cnvcWhName"] == null ? string.Empty : context.Request["cnvcWhName"];
            string strcnvcDepID = context.Request["cnvcDepID"] == null ? string.Empty : context.Request["cnvcDepID"];
            string strcnvcWhAddress = context.Request["cnvcWhAddress"] == null ? string.Empty : context.Request["cnvcWhAddress"];
            string strcnvcWhPhone = context.Request["cnvcWhPhone"] == null ? string.Empty : context.Request["cnvcWhPhone"];
            string strcnvcWhPerson = context.Request["cnvcWhPerson"] == null ? string.Empty : context.Request["cnvcWhPerson"];
            string strcnvcWhComments = context.Request["cnvcWhComments"] == null ? string.Empty : context.Request["cnvcWhComments"];
            string strcnbInvalid = context.Request["cnbInvalid"] == null ? string.Empty : context.Request["cnbInvalid"];
            string strIsInvalid = context.Request["IsInvalid"] == null ? string.Empty : context.Request["IsInvalid"];
            string strq = context.Request["q"] == null ? string.Empty : context.Request["q"];  
            var q = from p in Uow.tbWarehouse.GetAll() select p;
            if (strcnvcWhCode != string.Empty) q = q.Where(w => w.cnvcWhCode.Contains(strcnvcWhCode));
            if (strcnvcWhName != string.Empty) q = q.Where(w => w.cnvcWhName.Contains(strcnvcWhName));
            if (strcnvcDepID != string.Empty) q = q.Where(w => w.cnvcDepID == strcnvcDepID);
            if (strcnvcWhAddress != string.Empty) q = q.Where(w => w.cnvcWhAddress.Contains(strcnvcWhAddress));
            if (strcnvcWhPhone != string.Empty) q = q.Where(w => w.cnvcWhPhone.Contains(strcnvcWhPhone));
            if (strcnvcWhPerson != string.Empty) q = q.Where(w => w.cnvcWhPerson == strcnvcWhPerson);
            if (strcnvcWhComments != string.Empty) q = q.Where(w => w.cnvcWhComments.Contains(strcnvcWhComments));
            if (strcnbInvalid != string.Empty) q = q.Where(w => w.cnbInvalid == true);

            if (strIsInvalid != string.Empty)
            {
                bool bInvalid = Convert.ToBoolean(strIsInvalid);
                q = q.Where(w => w.cnbInvalid == bInvalid);
            }
            if (strq != string.Empty) q = q.Where(w => w.cnvcWhCode.Contains(strq) || w.cnvcWhName.Contains(strq));
            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbWarehouse> ltbWarehouse)
        {
            DataTable dt = ltbWarehouse.ToDataTable<DXInfo.Models.tbWarehouse>();
            ServiceHelper.DataTableConvert(context, dt, "cnvcDepID", ServiceHelper.Table_tbDept, "cnvcDeptID", "cnvcDeptName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcWhPerson", ServiceHelper.Table_tbLogin, "vcLoginID", "vcOperName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnbInvalid");
            return dt;
        }
        private string GettbWarehouse(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<DXInfo.Models.tbWarehouse> ltbWarehouse = new List<DXInfo.Models.tbWarehouse>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbWarehouse = q.OrderBy(o => o.cnvcWhCode)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbWarehouse);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newtbWarehouse(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbWarehouse tbWarehouse = new DXInfo.Models.tbWarehouse();
                    tbWarehouse.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                    tbWarehouse.cnvcWhName = context.Request.Form["cnvcWhName"];
                    tbWarehouse.cnvcDepID = context.Request.Form["cnvcDepID"];
                    tbWarehouse.cnvcWhAddress = context.Request.Form["cnvcWhAddress"];
                    tbWarehouse.cnvcWhPhone = context.Request.Form["cnvcWhPhone"];
                    tbWarehouse.cnvcWhPerson = context.Request.Form["cnvcWhPerson"];
                    tbWarehouse.cnvcWhComments = context.Request.Form["cnvcWhComments"];
                    tbWarehouse.cnbInvalid = context.Request.Form["cnbInvalid"] == "on" ? true : false;

                    Uow.tbWarehouse.Add(tbWarehouse);
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
        private string updatetbWarehouse(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbWarehouse tbWarehouse = Uow.tbWarehouse.GetById(g=>g.cnvcWhCode==context.Request.Form["cnvcWhCode"]);
                    tbWarehouse.cnvcWhCode = context.Request.Form["cnvcWhCode"];
                    tbWarehouse.cnvcWhName = context.Request.Form["cnvcWhName"];
                    tbWarehouse.cnvcDepID = context.Request.Form["cnvcDepID"];
                    tbWarehouse.cnvcWhAddress = context.Request.Form["cnvcWhAddress"];
                    tbWarehouse.cnvcWhPhone = context.Request.Form["cnvcWhPhone"];
                    tbWarehouse.cnvcWhPerson = context.Request.Form["cnvcWhPerson"];
                    tbWarehouse.cnvcWhComments = context.Request.Form["cnvcWhComments"];
                    tbWarehouse.cnbInvalid = context.Request.Form["cnbInvalid"] == "on" ? true : false;

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
        private string removetbWarehouse(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbWarehouse tbWarehouse = Uow.tbWarehouse.GetById(g=>g.cnvcWhCode==context.Request.Form["cnvcWhCode"]);
                    Uow.tbWarehouse.Delete(tbWarehouse);
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
        private void tbWarehouseExportToExcel(HttpContext context)
        {
            string fileName = "仓库.xls";

            List<DXInfo.Models.tbWarehouse> ltbWarehouse = new List<DXInfo.Models.tbWarehouse>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbWarehouse = q.OrderBy(o => o.cnvcWhCode).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbWarehouse);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

            field = new BoundField();
            field.DataField = "cnvcWhCode";
            field.HeaderText = "仓库编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhName";
            field.HeaderText = "仓库名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDepID";
            field.HeaderText = "部门编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcDepIDComments";
            field.HeaderText = "部门名称";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhAddress";
            field.HeaderText = "地址";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhPhone";
            field.HeaderText = "电话";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhPerson";
            field.HeaderText = "负责人编码";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhPersonComments";
            field.HeaderText = "负责人姓名";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnvcWhComments";
            field.HeaderText = "描述";
            view.Columns.Add(field);
            field = new BoundField();
            field.DataField = "cnbInvalidComments";
            field.HeaderText = "是否失效";
            view.Columns.Add(field);


            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAlltbWarehouse(HttpContext context)
        {
            List<DXInfo.Models.tbWarehouse> ltbWarehouse = new List<DXInfo.Models.tbWarehouse>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                ltbWarehouse = Uow.tbWarehouse.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, ltbWarehouse);
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
