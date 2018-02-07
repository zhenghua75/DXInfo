using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using Ninject;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// StockReport 的摘要说明
    /// </summary>
    public class StockReport : MyHttpHandlerBase
    {
        public StockReport(IAMSCMUow uow)
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
                    context.Response.Write(GetComputationGroup(context));
                    break;               
                case "excel":
                    ComputationGroupExportToExcel(context);
                    break;
            }
        }
        private List<tbStockReportData> QueryCondition(HttpContext context)
        {
            string strcnvcWhCode = context.Request["cnvcWhCode"] == null ? string.Empty : context.Request["cnvcWhCode"];
            string strcnvcDeptId = context.Request["cnvcDeptId"] == null ? string.Empty : context.Request["cnvcDeptId"];
            string strcnvcInvCode = context.Request["cnvcInvCode"] == null ? string.Empty : context.Request["cnvcInvCode"];
            string strcnnYear = context.Request["cnnYear"] == null ? string.Empty : context.Request["cnnYear"];
            string strcnnMonth = context.Request["cnnMonth"] == null ? string.Empty : context.Request["cnnMonth"];

            int icnnStatus = (int)StockStatus.Check;
            var q = from d in Uow.tbStockDetail.GetAll()
                    join main in Uow.tbStockMain.GetAll() on d.cnnMainId equals main.cnnMainId into md
                    from detail in md.DefaultIfEmpty()

                    join d1 in Uow.tbInventory.GetAll() on d.cnvcInvCode equals d1.cnvcInvCode into dd1
                    from inventory in dd1.DefaultIfEmpty()

                    join d2 in Uow.tbComputationUnit.GetAll() on inventory.cnvcSTComUnitCode equals d2.cnvcComunitCode into dd2
                    from uom in dd2.DefaultIfEmpty()

                    where detail.cnnStatus == icnnStatus
                    orderby d.cnnDetailId
                    select new tbStockReportData
                    {
                        cnnMainId = detail.cnnMainId,
                        cnvcWhCode = detail.cnvcWhCode,
                        cnvcDeptId = detail.cnvcDeptId,
                        cnnOperType = detail.cnnOperType,
                        cndBusinessDate = detail.cndBusinessDate,
                        cnnYear = detail.cnnYear,
                        cnnMonth = detail.cnnMonth,
                        cnvcInvCode = d.cnvcInvCode,
                        cnvcInvName = inventory.cnvcInvName,
                        cnvcSTComUnitCode = inventory.cnvcSTComUnitCode,
                        cnvcSTComUnitName = uom.cnvcComUnitName,
                        cnnSTQuantity = d.cnnMainQuantity / (uom.cniChangRate == 0 ? 1 : uom.cniChangRate),
                        cnnAmount = d.cnnAmount
                    };
            if (strcnvcWhCode != string.Empty) q = q.Where(w => w.cnvcWhCode == strcnvcWhCode);
            if (strcnvcDeptId != string.Empty) q = q.Where(w => w.cnvcDeptId == strcnvcDeptId);            
            if (strcnvcInvCode != string.Empty) q = q.Where(w => w.cnvcInvCode == strcnvcInvCode);
            int icnnYear = DateTime.Now.Year;
            if (strcnnYear != string.Empty)
                icnnYear = Convert.ToInt32(strcnnYear);
            q = q.Where(w => w.cnnYear == icnnYear);
            int icnnMonth = DateTime.Now.Month;
            if (strcnnMonth != string.Empty)
                icnnMonth = Convert.ToInt32(strcnnMonth);
            q = q.Where(w => w.cnnMonth == icnnMonth);            
            var q1 = q.ToList();
            q1.ForEach(delegate(tbStockReportData data) 
            {
                if (data.cnnOperType == 0)
                {
                    data.cnnInitQuantity = data.cnnSTQuantity;
                    data.cnnInitAmount = data.cnnAmount;
                }
                else
                {

                    if (data.cnnSTQuantity > 0)
                    {
                        data.cnnInQuantity = data.cnnSTQuantity;
                        data.cnnInAmount = data.cnnAmount;
                    }
                    else
                    {
                        data.cnnOutQuantity = Math.Abs(data.cnnSTQuantity.HasValue?data.cnnSTQuantity.Value:0);
                        data.cnnOutAmount = Math.Abs(data.cnnAmount.HasValue?data.cnnAmount.Value:0);
                    }
                }
                data.cnnEndQuantity = data.cnnSTQuantity;
                data.cnnEndAmount = data.cnnAmount;
            });
            var q2 = (from d in q1
                      where d.cnvcInvCode!=null
                     group d by new { d.cnvcInvCode, d.cnvcInvName,d.cnvcSTComUnitCode,d.cnvcSTComUnitName } into g
                     select new tbStockReportData
                     {
                         cnvcInvCode = g.Key.cnvcInvCode,
                         cnvcInvName = g.Key.cnvcInvName,
                         cnvcSTComUnitCode = g.Key.cnvcSTComUnitCode,
                         cnvcSTComUnitName = g.Key.cnvcSTComUnitName,
                         cnnInitQuantity = g.Sum(s => s.cnnInitQuantity),
                         cnnInitAmount = g.Sum(s => s.cnnInitAmount),
                         cnnInQuantity = g.Sum(s => s.cnnInQuantity),
                         cnnInAmount = g.Sum(s => s.cnnInAmount),
                         cnnOutQuantity = g.Sum(s => s.cnnOutQuantity),
                         cnnOutAmount = g.Sum(s => s.cnnOutAmount),
                         cnnEndQuantity = g.Sum(s => s.cnnEndQuantity),
                         cnnEndAmount = g.Sum(s => s.cnnEndAmount),
                         cnnSTPrice = g.Sum(s => s.cnnEndAmount) / (g.Sum(s => s.cnnEndQuantity) == 0 ? 1 : g.Sum(s => s.cnnEndQuantity))
                     }).ToList();
            return q2;
        }
        private DataTable List2DataTable(HttpContext context, List<tbStockReportData> ltbStockReportData)
        {
            DataTable dt = ltbStockReportData.ToDataTable<tbStockReportData>();
            return dt;
        }
        private string GetComputationGroup(HttpContext context)
        {
            string totalcount = "";
            List<tbStockReportData> ltbStockReportData = new List<tbStockReportData>();

            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count.ToString();
                ltbStockReportData = q.OrderBy(o => o.cnvcInvCode).ToList();

            //}
            DataTable dt = List2DataTable(context, ltbStockReportData);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private void ComputationGroupExportToExcel(HttpContext context)
        {
            string fileName = "库存统计表.xls";

            List<tbStockReportData> ltbStockReportData = new List<tbStockReportData>();

            //using (AMSCM.Models.AMSCM amscm = new AMSCM.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                ltbStockReportData = q.OrderBy(o => o.cnvcInvCode).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbStockReportData);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "cnvcInvCode";
            field.HeaderText = "存货编码";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcInvName";
            field.HeaderText = "存货名称";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnvcSTComUnitName";
            field.HeaderText = "单位";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnInitQuantity";
            field.HeaderText = "期初数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnInQuantity";
            field.HeaderText = "本期入库数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnOutQuantity";
            field.HeaderText = "本期出库数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnEndQuantity";
            field.HeaderText = "结余数量";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnSTPrice";
            field.HeaderText = "单位成本";
            view.Columns.Add(field);

            field = new BoundField();
            field.DataField = "cnnEndAmount";
            field.HeaderText = "库存成本";
            view.Columns.Add(field);

            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class tbStockReportData
    {
        public long cnnMainId { get; set; }
        public string cnvcSupplierCode { get; set; }
        public string cnvcWhCode { get; set; }
        public string cnvcDeptId { get; set; }
        public int cnnOperType { get; set; }
        public DateTime cndCreateDate { get; set; }
        public string cnvcCreaterId { get; set; }
        public string cnvcCreaterName { get; set; }
        public DateTime? cndCheckDate { get; set; }
        public string cnvcCheckerId { get; set; }
        public string cnvcCheckerName { get; set; }
        public DateTime cndBusinessDate { get; set; }
        public int cnnYear { get; set; }
        public int cnnMonth { get; set; }
        public int cnnStatus { get; set; }
        public string cnvcComments { get; set; }
        public long? cnnDetailId { get; set; }
        public string cnvcInvCode { get; set; }
        public string cnvcInvName { get; set; }
        public string cnvcComUnitCode { get; set; }
        public decimal? cnnQuantity { get; set; }
        public string cnvcMainComUnitCode { get; set; }
        public string cnvcSTComUnitCode { get; set; }
        public string cnvcSTComUnitName { get; set; }
        public decimal? cnnMainQuantity { get; set; }
        public decimal? cnnSTQuantity { get; set; }
        public decimal? cnnInitQuantity { get; set; }
        public decimal? cnnInQuantity { get; set; }
        public decimal? cnnOutQuantity { get; set; }
        public decimal? cnnEndQuantity { get; set; }
        public decimal? cnnPrice { get; set; }
        public decimal? cnnSTPrice { get; set; }
        public decimal? cnnMainPrice { get; set; }
        public decimal? cnnAmount { get; set; }
        public decimal? cnnInitAmount { get; set; }
        public decimal? cnnInAmount { get; set; }
        public decimal? cnnOutAmount { get; set; }
        public decimal? cnnEndAmount { get; set; }
    }
}