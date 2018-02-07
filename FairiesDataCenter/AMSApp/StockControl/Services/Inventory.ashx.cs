using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXInfo.Data.Contracts;
using System.Data;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// Inventory 的摘要说明
    /// </summary>
    public class Inventory : MyHttpHandlerBase
    {

        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 1 : Convert.ToInt32(context.Request["rows"]);
            string method = context.Request["method"];
            switch (method)
            {
                case "query":
                    context.Response.Write(GetInventory(context, page, rows));
                    break;
            }
        }
        private string GetInventory(HttpContext context, int page, int rows)
        {
            string strcnvcInvCode = context.Request["cnvcInvCode"] == null ? string.Empty : context.Request["cnvcInvCode"];
            string strcnvcInvName = context.Request["cnvcInvName"] == null ? string.Empty : context.Request["cnvcInvName"];
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            DateTime dtNow = DateTime.Now;
            var q = (from inv in uow.Inventory.GetAll().Where(w => w.InvType == (int)DXInfo.Models.InvType.StockManage)
                        join unit in uow.UnitOfMeasures.GetAll() on inv.UnitOfMeasure equals unit.Id into invunit
                        from iu in invunit.DefaultIfEmpty()
                        join c in uow.InventoryCategory.GetAll() on inv.Category equals c.Id into invc
                        from invcs in invc.DefaultIfEmpty()

                        join d1 in uow.MeasurementUnitGroup.GetAll() on inv.MeasurementUnitGroup equals d1.Id into dd1
                        from dd1s in dd1.DefaultIfEmpty()

                        join d2 in uow.UnitOfMeasures.GetAll() on inv.MainUnit equals d2.Id into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        join d3 in uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.MeasurementUnitGroupCategory) on inv.UnitCategory equals d3.Value into dd3
                        from dd3s in dd3.DefaultIfEmpty()

                        join d4 in uow.UnitOfMeasures.GetAll() on inv.PurchaseUnit equals d4.Id into dd4
                        from dd4s in dd4.DefaultIfEmpty()

                        join d5 in uow.UnitOfMeasures.GetAll() on inv.StockUnit equals d5.Id into dd5
                        from dd5s in dd5.DefaultIfEmpty()

                        join d6 in uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.CheckCycle) on inv.CheckCycle equals d6.Value into dd6
                        from dd6s in dd6.DefaultIfEmpty()

                        join d7 in uow.EnumTypeDescription.GetAll().Where(w => w.Code == DXInfo.Models.EnumHelper.ShelfLifeType) on inv.ShelfLifeType equals d7.Value into dd7
                        from dd7s in dd7.DefaultIfEmpty()

                        select new
                        {
                            //inv.Id,
                            inv.Code,
                            inv.Name,
                            //inv.Category,
                            CategoryName = invcs.Name,
                            inv.Specs,
                            //inv.MeasurementUnitGroup,
                            MeasurementUnitGroupName = dd1s.Name,
                            //inv.MainUnit,
                            MainUnitName = dd2s.Name,
                            //inv.StockUnit,
                            StockUnitName = dd5s==null?"":dd5s.Name,
                            inv.HighStock,
                            inv.LowStock,
                            inv.SecurityStock,
                            //LastCheckDate = inv.LastCheckDate == null ? dtNow : inv.LastCheckDate,
                            //inv.CheckCycle,
                            CheckCycleName = dd6s.Description,
                            inv.SomeDay,
                            inv.ShelfLife,
                            //inv.ShelfLifeType,
                            ShelfLifeTypeName = dd7s.Description,
                            inv.EarlyWarningDay,
                            inv.Comment,
                            inv.IsInvalid,
                        });
            if (strcnvcInvCode != string.Empty) q = q.Where(w => w.Code.Contains(strcnvcInvCode));
            if (strcnvcInvName != string.Empty) q = q.Where(w => w.Name.Contains(strcnvcInvName));

            string totalcount = "";
            //List<DXInfo.Models.Inventory> lInventory = new List<DXInfo.Models.Inventory>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

            //var q = QueryCondition(context);
            totalcount = q.Count().ToString();
            var slInventory = q.OrderBy(o => o.Code)
                .Skip(skitCount)
                .Take(rows).ToList();
            //}
            DataTable dt = slInventory.ToDataTable();
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        
            
        }
    }
}