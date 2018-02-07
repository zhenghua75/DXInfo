
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
    /// 配方
    /// </summary>
    public class tbBillOfMaterials :MyHttpHandlerBase
    {
        public tbBillOfMaterials(IAMSCMUow uow)
        {
            this.Uow = uow;
        }
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string method = context.Request["method"];
            switch (method)
            {
                case "query":
                    context.Response.Write(GettbBillOfMaterials(context));
                    break;
                case "all":
                    context.Response.Write(GetAlltbBillOfMaterials(context));
                    break;
                case "update":
                    context.Response.Write(updatetbBillOfMaterials(context));
                    break;
                case "new":
                    context.Response.Write(newtbBillOfMaterials(context));
                    break;
                case "remove":
                    context.Response.Write(removetbBillOfMaterials(context));
                    break;
                case "excel":
                    tbBillOfMaterialsExportToExcel(context);
                    break;
            }
        }
        private IQueryable<DXInfo.Models.tbBillOfMaterials> QueryCondition(HttpContext context)
        {
            string strcnvcPartInvCode = context.Request["cnvcPartInvCode"] == null ? string.Empty : context.Request["cnvcPartInvCode"];
            string strcnvcComponentInvCode = context.Request["cnvcComponentInvCode"] == null ? string.Empty : context.Request["cnvcComponentInvCode"];
            string strcnnBaseQtyN = context.Request["cnnBaseQtyN"] == null ? string.Empty : context.Request["cnnBaseQtyN"];
            string strcnnBaseQtyD = context.Request["cnnBaseQtyD"] == null ? string.Empty : context.Request["cnnBaseQtyD"];


            var q = from p in Uow.tbBillOfMaterials.GetAll() select p;
            if (strcnvcPartInvCode != string.Empty) q = q.Where(w => w.cnvcPartInvCode == strcnvcPartInvCode);
            if (strcnvcComponentInvCode != string.Empty) q = q.Where(w => w.cnvcComponentInvCode == strcnvcComponentInvCode);
            if (strcnnBaseQtyN != string.Empty)
            {
                decimal dcnnBaseQtyN = Convert.ToDecimal(strcnnBaseQtyN);
                q = q.Where(w => w.cnnBaseQtyN == dcnnBaseQtyN);
            }
            if (strcnnBaseQtyD != string.Empty)
            {
                decimal dcnnBaseQtyD = Convert.ToDecimal(strcnnBaseQtyD);
                q = q.Where(w => w.cnnBaseQtyD == dcnnBaseQtyD);
            }

            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials)
        {
            DataTable dt = ltbBillOfMaterials.ToDataTable<DXInfo.Models.tbBillOfMaterials>();

            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCode", "cnvcPartInvName", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcInvName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCode", "cnvcPartGroupCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcGroupCode", "");

            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCode", "cnvcPartInvCCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcInvCCode", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCCode", "cnvcPartInvCName", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductClassName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCCode", "cnvcPartProductType", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductType", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartProductType", "cnvcPartProductTypeName", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartGroupCode", "cnvcPartGroupName", ServiceHelper.Table_tbComputationGroup, "cnvcGroupCode", "cnvcGroupName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartInvCode", "cnvcPartProduceUnitCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcProduceUnitCode", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPartProduceUnitCode", "cnvcPartProduceUnitName", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");

            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCode","cnvcComponentInvName", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcInvName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCode", "cnvcComponentGroupCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcGroupCode", "");

            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCode", "cnvcComponentInvCCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcInvCCode", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCCode", "cnvcComponentInvCName", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductClassName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCCode", "cnvcComponentProductType", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductType", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentProductType", "cnvcComponentProductTypeName", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");

            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentGroupCode", "cnvcComponentGroupName", ServiceHelper.Table_tbComputationGroup, "cnvcGroupCode", "cnvcGroupName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentInvCode", "cnvcComponentProduceUnitCode", ServiceHelper.Table_tbInventory, "cnvcInvCode", "cnvcProduceUnitCode", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComponentProduceUnitCode", "cnvcComponentProduceUnitName", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");

            return dt;
        }
        private string GettbBillOfMaterials(HttpContext context)
        {
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 10 : Convert.ToInt32(context.Request["rows"]);
            string totalcount = "";
            List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials = new List<DXInfo.Models.tbBillOfMaterials>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                ltbBillOfMaterials = q.OrderBy(o => o.cnvcComponentInvCode).OrderBy(o => o.cnvcPartInvCode)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbBillOfMaterials);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newtbBillOfMaterials(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbBillOfMaterials tbBillOfMaterials = new DXInfo.Models.tbBillOfMaterials();
                    tbBillOfMaterials.cnvcPartInvCode = context.Request.Form["cnvcPartInvCode"];
                    tbBillOfMaterials.cnvcComponentInvCode = context.Request.Form["cnvcComponentInvCode"];
                    tbBillOfMaterials.cnnBaseQtyN = Convert.ToDecimal(context.Request.Form["cnnBaseQtyN"]);
                    tbBillOfMaterials.cnnBaseQtyD = Convert.ToDecimal(context.Request.Form["cnnBaseQtyD"]);

                    Uow.tbBillOfMaterials.Add(tbBillOfMaterials);
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
        private string updatetbBillOfMaterials(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbBillOfMaterials tbBillOfMaterials = Uow.tbBillOfMaterials.GetById(g=>g.cnvcPartInvCode==context.Request.Form["cnvcPartInvCode"]&&g.cnvcComponentInvCode==context.Request.Form["cnvcComponentInvCode"]);
                    tbBillOfMaterials.cnvcPartInvCode = context.Request.Form["cnvcPartInvCode"];
                    tbBillOfMaterials.cnvcComponentInvCode = context.Request.Form["cnvcComponentInvCode"];
                    tbBillOfMaterials.cnnBaseQtyN = Convert.ToDecimal(context.Request.Form["cnnBaseQtyN"]);
                    tbBillOfMaterials.cnnBaseQtyD = Convert.ToDecimal(context.Request.Form["cnnBaseQtyD"]);

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
        private string removetbBillOfMaterials(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbBillOfMaterials tbBillOfMaterials = Uow.tbBillOfMaterials.GetById(g=>g.cnvcPartInvCode==context.Request.Form["cnvcPartInvCode"]&&g.cnvcComponentInvCode== context.Request.Form["cnvcComponentInvCode"]);
                    Uow.tbBillOfMaterials.Delete(tbBillOfMaterials);
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
        private void tbBillOfMaterialsExportToExcel(HttpContext context)
        {
            string fileName = "配方.xls";

            List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials = new List<DXInfo.Models.tbBillOfMaterials>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbBillOfMaterials = q.OrderBy(o => o.cnvcPartInvCode).OrderBy(o=>o.cnvcComponentInvCode).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbBillOfMaterials);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field;

            field = new BoundField();
            field.DataField = "cnvcPartProductType";
            field.HeaderText = "母件产品组编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartProductTypeName";
            field.HeaderText = "母件产品组名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartInvCCode";
            field.HeaderText = "母件产品类别编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartInvCName";
            field.HeaderText = "母件产品类别名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartInvCode";
            field.HeaderText = "母件存货编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartInvName";
            field.HeaderText = "母件存货编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartGroupCode";
            field.HeaderText = "母件计量单位组编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartGroupName";
            field.HeaderText = "母件计量单位组名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartProduceUnitCode";
            field.HeaderText = "母件生产计量单位编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcPartProduceUnitName";
            field.HeaderText = "母件生产计量单位名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnBaseQtyD";
            field.HeaderText = "母件基础用量";
            view.Columns.Add(field);

            //子件
            field = new BoundField();
            field.DataField = "cnvcComponentProductType";
            field.HeaderText = "子件产品组编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentProductTypeName";
            field.HeaderText = "子件产品组名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentInvCCode";
            field.HeaderText = "子件产品类别编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentInvCName";
            field.HeaderText = "子件产品类别名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentInvCode";
            field.HeaderText = "子件存货编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentInvName";
            field.HeaderText = "子件存货名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentGroupCode";
            field.HeaderText = "子件计量单位组编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentGroupName";
            field.HeaderText = "子件计量单位组名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentProduceUnitCode";
            field.HeaderText = "子件生产计量单位编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcComponentProduceUnitName";
            field.HeaderText = "子件生产计量单位名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnBaseQtyN";
            field.HeaderText = "子件用量";
            view.Columns.Add(field);
            


            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAlltbBillOfMaterials(HttpContext context)
        {
            List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials = new List<DXInfo.Models.tbBillOfMaterials>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                ltbBillOfMaterials = Uow.tbBillOfMaterials.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, ltbBillOfMaterials);
            return ServiceHelper.DataTableToEasyUIJson(dt);
        }

    }
}
