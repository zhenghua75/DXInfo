using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AMSApp.zhenghua.Business;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using DXInfo.Data.Contracts;
//using AMSApp.zhenghua.Services;
namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// Inventory1 的摘要说明
    /// </summary>
    public class tbProductClass : MyHttpHandlerBase
    {
        public tbProductClass(IAMSCMUow uow)
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
                    context.Response.Write(GetProductClass(context));
                break;
                case "all":
                context.Response.Write(GetAllProductClass(context));
                break;
                case "update":
                context.Response.Write(updateProductClass(context));
                break;
                case "new":
                context.Response.Write(newProductClass(context));
                break;
                case "remove":
                context.Response.Write(removeProductClass(context));
                break;
                case "excel":
                ProductCLassExportToExcel(context);
                break;
            }
        }
        private IQueryable<DXInfo.Models.tbProductClass> QueryCondition(HttpContext context)
        {
            string strcnvcProductType = context.Request["cnvcProductType"] == null ? string.Empty : context.Request["cnvcProductType"];
            string strcnvcProductClassCode = context.Request["cnvcProductClassCode"] == null ? string.Empty : context.Request["cnvcProductClassCode"];
            string strcnvcProductClassName = context.Request["cnvcProductClassName"] == null ? string.Empty : context.Request["cnvcProductClassName"];
            string strcnnDays = context.Request["cnnDays"] == null ? string.Empty : context.Request["cnnDays"];
            int icnnDays = strcnnDays == string.Empty ? 0 : Convert.ToInt32(strcnnDays);
            string strcnvcComments = context.Request["cnvcComments"] == null ? string.Empty : context.Request["cnvcComments"];
            string strq = context.Request["q"] == null ? string.Empty : context.Request["q"];
            var q = from p in Uow.tbProductClass.GetAll() select p;
            if (strcnvcProductType != string.Empty) q = q.Where(w => w.cnvcProductType == strcnvcProductType);
            if (strcnvcProductClassCode != string.Empty) q = q.Where(w => w.cnvcProductClassCode.Contains(strcnvcProductClassCode));
            if (strcnvcProductClassName != string.Empty) q = q.Where(w => w.cnvcProductClassName.Contains(strcnvcProductClassName));
            if (strcnnDays != string.Empty) q = q.Where(w => w.cnnDays == icnnDays);
            if (strcnvcComments != string.Empty) q = q.Where(w => w.cnvcComments.Contains(strcnvcComments));
            if (strq != string.Empty) q = q.Where(w => w.cnvcProductClassCode.Contains(strq) || w.cnvcProductClassName.Contains(strq));
            return q;
        }
        private DataTable List2DataTable(HttpContext context,List<DXInfo.Models.tbProductClass> lProductClass)
        {
            DataTable dt = lProductClass.ToDataTable<DXInfo.Models.tbProductClass>();
            ServiceHelper.DataTableConvert(context, dt, "cnvcProductType", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");
            return dt;
        }
        private string GetProductClass(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";            
            List<DXInfo.Models.tbProductClass> lProductClass = new List<DXInfo.Models.tbProductClass>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount= q.Count().ToString();
                lProductClass = q.OrderBy(o => o.cnvcProductClassCode)
                    .Skip(skitCount)
                    .Take(rows).ToList();                
            //}
            DataTable dt = List2DataTable(context, lProductClass);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);
            
        }
        private string newProductClass(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbProductClass productClass = new DXInfo.Models.tbProductClass();
                    productClass.cnvcProductType = context.Request.Form["cnvcProductType"];
                    productClass.cnvcProductClassCode = context.Request.Form["cnvcProductClassCode"];
                    productClass.cnvcProductClassName = context.Request.Form["cnvcProductClassName"];
                    productClass.cnnDays = Convert.ToInt32(context.Request.Form["cnnDays"]);
                    productClass.cnvcComments = context.Request.Form["cnvcComments"];                    
                    Uow.tbProductClass.Add(productClass);
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
        private string updateProductClass(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbProductClass productClass = Uow.tbProductClass.GetById(g=>g.cnvcProductClassCode==context.Request.Form["cnvcProductClassCode"]);
                    productClass.cnvcProductType = context.Request.Form["cnvcProductType"];
                    productClass.cnvcProductClassName = context.Request.Form["cnvcProductClassName"];
                    productClass.cnnDays = Convert.ToInt32(context.Request.Form["cnnDays"]);
                    productClass.cnvcComments = context.Request.Form["cnvcComments"];
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
        private string removeProductClass(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbProductClass productClass = Uow.tbProductClass.GetById(g=>g.cnvcProductClassCode==context.Request.Form["cnvcProductClassCode"]);
                    Uow.tbProductClass.Delete(productClass);
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
        private void ProductCLassExportToExcel(HttpContext context)
        {
            string fileName = "存货分类.xls";

            List<DXInfo.Models.tbProductClass> lProductClass = new List<DXInfo.Models.tbProductClass>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                lProductClass = q.OrderBy(o => o.cnvcProductClassCode).ToList();
            //}
            DataTable dt = List2DataTable(context, lProductClass);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "cnvcProductType";
            field.HeaderText = "产品组编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcProductTypeComments";
            field.HeaderText = "产品组名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcProductClassCode";
            field.HeaderText = "类别编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcProductClassName";
            field.HeaderText = "类别名称";
            view.Columns.Add(field);


            field = new BoundField();
            field.DataField = "cnnDays";
            field.HeaderText = "过期天数";
            view.Columns.Add(field);            

            field = new BoundField();
            field.DataField = "cnvcComments";
            field.HeaderText = "描述";
            view.Columns.Add(field);

            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context,fileName,view);
        }
        private string GetAllProductClass(HttpContext context)
        {
            List<DXInfo.Models.tbProductClass> lProductClass = new List<DXInfo.Models.tbProductClass>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                lProductClass = Uow.tbProductClass.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, lProductClass);
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