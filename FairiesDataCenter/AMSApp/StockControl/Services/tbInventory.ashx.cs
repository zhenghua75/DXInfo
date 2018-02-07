using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Entity.Infrastructure;
using System.Web.SessionState;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// Inventory 的摘要说明
    /// </summary>
    public class tbInventory : MyHttpHandlerBase
    {
        public tbInventory(IAMSCMUow uow)
        {
            this.Uow = uow;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int page = context.Request["page"] == null ? 1 : Convert.ToInt32(context.Request["page"]);
            int rows = context.Request["rows"] == null ? 1 : Convert.ToInt32(context.Request["rows"]);
            string method = context.Request["method"];
            switch (method)
            {
                case "query":
                    context.Response.Write(GetInventory(context, page, rows));
                    break;
                case "update":
                    context.Response.Write(updateInventory(context));
                    break;
                case "new":
                    context.Response.Write(newInventory(context));
                    break;
                case "remove":
                    context.Response.Write(removeInventory(context));
                    break;
                case "excel":
                    InventoryExportToExcel(context);
                    break;
                case "all":
                    context.Response.Write(GetAllInventory(context));
                    break;
                case "invcode":
                    context.Response.Write(GetInvCode(context));
                    break;
            }
        }
        private string GetInvCode(HttpContext context)
        {
            string invCCode = context.Request["InvCCode"];
            string[] invCCodes = invCCode.Split('~');
            string begin = invCCodes[0];
            string end = invCCodes[1];
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var inv = Uow.tbInventory.GetAll().Where(w => w.cnvcInvCCode == invCCode).OrderByDescending(o => o.cnvcInvCode).FirstOrDefault();
                if (inv == null)
                {
                    return begin;
                }
                int icount = Convert.ToInt32(inv.cnvcInvCode) + 1;
                string ret = icount.ToString().PadLeft(begin.Length, '0');
                return ret;
            //}
        }
        private IQueryable<DXInfo.Models.tbInventory> QueryCondition(HttpContext context)
        {
            string strcnbProductBill = context.Request["cnbProductBill"] == null ? string.Empty : context.Request["cnbProductBill"];
            string strcnvcInvCode = context.Request["cnvcInvCode"] == null ? string.Empty : context.Request["cnvcInvCode"];
            string strcnvcInvName = context.Request["cnvcInvName"] == null ? string.Empty : context.Request["cnvcInvName"];
            string strcnvcInvStd = context.Request["cnvcInvStd"] == null ? string.Empty : context.Request["cnvcInvStd"];
            string strcnvcInvCCode = context.Request["cnvcInvCCode"] == null ? string.Empty : context.Request["cnvcInvCCode"];
            string strcnbSale = context.Request["cnbSale"] == null ? string.Empty : context.Request["cnbSale"];
            string strcnbPurchase = context.Request["cnbPurchase"] == null ? string.Empty : context.Request["cnbPurchase"];
            string strcnbSelf = context.Request["cnbSelf"] == null ? string.Empty : context.Request["cnbSelf"];
            string strcnbComsume = context.Request["cnbComsume"] == null ? string.Empty : context.Request["cnbComsume"];
            string strcniInvCCost = context.Request["cniInvCCost"] == null ? string.Empty : context.Request["cniInvCCost"];
            string strcniInvNCost = context.Request["cniInvNCost"] == null ? string.Empty : context.Request["cniInvNCost"];
            string strcniSafeNum = context.Request["cniSafeNum"] == null ? string.Empty : context.Request["cniSafeNum"];
            string strcniLowSum = context.Request["cniLowSum"] == null ? string.Empty : context.Request["cniLowSum"];
            string strcndSDate = context.Request["cndSDate"] == null ? string.Empty : context.Request["cndSDate"];
            string strcndSDate1 = context.Request["cndSDate1"] == null ? string.Empty : context.Request["cndSDate1"];
            string strcndEDate = context.Request["cndEDate"] == null ? string.Empty : context.Request["cndEDate"];
            string strcndEDate1 = context.Request["cndEDate1"] == null ? string.Empty : context.Request["cndEDate1"];
            string strcnvcCreatePerson = context.Request["cnvcCreatePerson"] == null ? string.Empty : context.Request["cnvcCreatePerson"];
            string strcnvcModifyPerson = context.Request["cnvcModifyPerson"] == null ? string.Empty : context.Request["cnvcModifyPerson"];
            string strcndModifyDate = context.Request["cndModifyDate"] == null ? string.Empty : context.Request["cndModifyDate"];
            string strcndModifyDate1 = context.Request["cndModifyDate1"] == null ? string.Empty : context.Request["cndModifyDate1"];
            string strcnvcValueType = context.Request["cnvcValueType"] == null ? string.Empty : context.Request["cnvcValueType"];
            string strcnvcGroupCode = context.Request["cnvcGroupCode"] == null ? string.Empty : context.Request["cnvcGroupCode"];
            string strcnvcComUnitCode = context.Request["cnvcComUnitCode"] == null ? string.Empty : context.Request["cnvcComUnitCode"];
            string strcnvcSAComUnitCode = context.Request["cnvcSAComUnitCode"] == null ? string.Empty : context.Request["cnvcSAComUnitCode"];
            string strcnvcPUComUnitCode = context.Request["cnvcPUComUnitCode"] == null ? string.Empty : context.Request["cnvcPUComUnitCode"];
            string strcnvcSTComUnitCode = context.Request["cnvcSTComUnitCode"] == null ? string.Empty : context.Request["cnvcSTComUnitCode"];
            string strcnvcProduceUnitCode = context.Request["cnvcProduceUnitCode"] == null ? string.Empty : context.Request["cnvcProduceUnitCode"];
            string strcnfRetailPrice = context.Request["cnfRetailPrice"] == null ? string.Empty : context.Request["cnfRetailPrice"];
            string strcnvcShopUnitCode = context.Request["cnvcShopUnitCode"] == null ? string.Empty : context.Request["cnvcShopUnitCode"];
            string strcnvcFeel = context.Request["cnvcFeel"] == null ? string.Empty : context.Request["cnvcFeel"];
            string strcnvcOrganise = context.Request["cnvcOrganise"] == null ? string.Empty : context.Request["cnvcOrganise"];
            string strcnvcColor = context.Request["cnvcColor"] == null ? string.Empty : context.Request["cnvcColor"];
            string strcnvcTaste = context.Request["cnvcTaste"] == null ? string.Empty : context.Request["cnvcTaste"];
            string strcnnExpire = context.Request["cnnExpire"] == null ? string.Empty : context.Request["cnnExpire"];
            string strcnnDue = context.Request["cnnDue"] == null ? string.Empty : context.Request["cnnDue"];

            string strq = context.Request["q"] == null ? string.Empty : context.Request["q"];
            var q = from p in Uow.tbInventory.GetAll() select p;

            if (strcnbProductBill != string.Empty) q = q.Where(w => w.cnbProductBill == true);
            if (strcnvcInvCode != string.Empty) q = q.Where(w => w.cnvcInvCode.Contains(strcnvcInvCode));
            if (strcnvcInvName != string.Empty) q = q.Where(w => w.cnvcInvName.Contains(strcnvcInvName));
            if (strcnvcInvStd != string.Empty) q = q.Where(w => w.cnvcInvStd.Contains(strcnvcInvStd));
            if (strcnvcInvCCode != string.Empty) q = q.Where(w => w.cnvcInvCCode == strcnvcInvCCode);
            if (strcnbSale != string.Empty) q = q.Where(w => w.cnbSale == true);
            if (strcnbPurchase != string.Empty) q = q.Where(w => w.cnbPurchase == true);
            if (strcnbSelf != string.Empty) q = q.Where(w => w.cnbSelf == true);
            if (strcnbComsume != string.Empty) q = q.Where(w => w.cnbComsume == true);
            if (strcniInvCCost != string.Empty)
            {
                decimal dcniInvCCost = Convert.ToDecimal(strcniInvCCost);
                q = q.Where(w => w.cniInvCCost == dcniInvCCost);
            }
            if (strcniInvNCost != string.Empty)
            {
                decimal dcniInvNCost = Convert.ToDecimal(strcniInvNCost);
                q = q.Where(w => w.cniInvNCost == dcniInvNCost);
            }
            if (strcniSafeNum != string.Empty)
            {
                decimal dcniSafeNum = Convert.ToDecimal(strcniSafeNum);
                q = q.Where(w => w.cniSafeNum == dcniSafeNum);
            }
            if (strcniLowSum != string.Empty)
            {
                decimal dcniLowSum = Convert.ToDecimal(strcniLowSum);
                q = q.Where(w => w.cniLowSum == dcniLowSum);
            }
            if (strcndSDate != string.Empty)
            {
                DateTime dtcndSDate = Convert.ToDateTime(strcndSDate);
                q = q.Where(w => w.cndSDate >= dtcndSDate);
            }
            if (strcndSDate1 != string.Empty)
            {
                DateTime dtcndSDate1 = Convert.ToDateTime(strcndSDate1);
                q = q.Where(w => w.cndSDate <= dtcndSDate1);
            }
            if (strcndEDate != string.Empty)
            {
                DateTime dtcndEDate = Convert.ToDateTime(strcndEDate);
                q = q.Where(w => w.cndEDate >= dtcndEDate);
            }
            if (strcndEDate1 != string.Empty)
            {
                DateTime dtcndEDate1 = Convert.ToDateTime(strcndEDate1);
                q = q.Where(w => w.cndEDate <= dtcndEDate1);
            }
            if (strcnvcCreatePerson != string.Empty) q = q.Where(w => w.cnvcCreatePerson == strcnvcCreatePerson);
            if (strcnvcModifyPerson != string.Empty) q = q.Where(w => w.cnvcModifyPerson == strcnvcModifyPerson);
            if (strcndModifyDate != string.Empty)
            {
                DateTime dtcndModifyDate = Convert.ToDateTime(strcndModifyDate);
                q = q.Where(w => w.cndModifyDate >= dtcndModifyDate);
            }
            if (strcndModifyDate1 != string.Empty)
            {
                DateTime dtcndModifyDate1 = Convert.ToDateTime(strcndModifyDate1);
                q = q.Where(w => w.cndModifyDate <= dtcndModifyDate1);
            }
            if (strcnvcValueType != string.Empty) q = q.Where(w => w.cnvcValueType == strcnvcValueType);
            if (strcnvcGroupCode != string.Empty) q = q.Where(w => w.cnvcGroupCode == strcnvcGroupCode);
            if (strcnvcComUnitCode != string.Empty) q = q.Where(w => w.cnvcComUnitCode == strcnvcComUnitCode);
            if (strcnvcSAComUnitCode != string.Empty) q = q.Where(w => w.cnvcSAComUnitCode == strcnvcSAComUnitCode);
            if (strcnvcPUComUnitCode != string.Empty) q = q.Where(w => w.cnvcPUComUnitCode == strcnvcPUComUnitCode);
            if (strcnvcSTComUnitCode != string.Empty) q = q.Where(w => w.cnvcSTComUnitCode == strcnvcSTComUnitCode);
            if (strcnvcProduceUnitCode != string.Empty) q = q.Where(w => w.cnvcProduceUnitCode == strcnvcProduceUnitCode);
            if (strcnfRetailPrice != string.Empty)
            {
                decimal dcnfRetailPrice = Convert.ToDecimal(strcnfRetailPrice);
                q = q.Where(w => w.cnfRetailPrice == dcnfRetailPrice);
            }
            if (strcnvcShopUnitCode != string.Empty) q = q.Where(w => w.cnvcShopUnitCode == strcnvcShopUnitCode);
            if (strcnvcFeel != string.Empty) q = q.Where(w => w.cnvcFeel.Contains(strcnvcFeel));
            if (strcnvcOrganise != string.Empty) q = q.Where(w => w.cnvcOrganise.Contains(strcnvcOrganise));
            if (strcnvcColor != string.Empty) q = q.Where(w => w.cnvcColor.Contains(strcnvcColor));
            if (strcnvcTaste != string.Empty) q = q.Where(w => w.cnvcTaste.Contains(strcnvcTaste));
            if (strcnnExpire != string.Empty)
            {
                int icnnExpire = Convert.ToInt32(strcnnExpire);
                q = q.Where(w => w.cnnExpire == icnnExpire);
            }
            if (strcnnDue != string.Empty)
            {
                int icnnDue = Convert.ToInt32(strcnnDue);
                q = q.Where(w => w.cnnDue == icnnDue);
            }

            if (strq != string.Empty) q = q.Where(w => w.cnvcInvCode.Contains(strq) || w.cnvcInvName.Contains(strq));
            return q;
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbInventory> lInventory)
        {
            DataTable dt = lInventory.ToDataTable<DXInfo.Models.tbInventory>();
            ServiceHelper.DataTableConvert(context, dt, "cnvcInvCCode", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductClassName", "");

            ServiceHelper.DataTableConvert(context, dt,  "cnvcInvCCode","cnvcProductType", ServiceHelper.Table_tbProductClass, "cnvcProductClassCode", "cnvcProductType", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcProductType", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");

            ServiceHelper.DataTableConvert(context, dt, "cnvcCreatePerson", ServiceHelper.Table_tbLogin, "vcLoginID", "vcOperName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcModifyPerson", ServiceHelper.Table_tbLogin, "vcLoginID", "vcOperName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcValueType", ServiceHelper.Table_tbNameCode, "cnvcCode", "cnvcName", "cnvcType='ValueType'");
            ServiceHelper.DataTableConvert(context, dt, "cnvcGroupCode", ServiceHelper.Table_tbComputationGroup, "cnvcGroupCode", "cnvcGroupName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcComUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcSAComUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcPUComUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcSTComUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcProduceUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");
            ServiceHelper.DataTableConvert(context, dt, "cnvcShopUnitCode", ServiceHelper.Table_tbComputationUnit, "cnvcComunitCode", "cnvcComUnitName", "");


            ServiceHelper.DataTableConvert(context, dt, "cnbProductBill");
            ServiceHelper.DataTableConvert(context, dt, "cnbSale");
            ServiceHelper.DataTableConvert(context, dt, "cnbPurchase");
            ServiceHelper.DataTableConvert(context, dt, "cnbSelf");
            ServiceHelper.DataTableConvert(context, dt, "cnbComsume");
            return dt;
        }
        private string GetInventory(HttpContext context, int page, int rows)
        {
            string totalcount = "";
           List<DXInfo.Models.tbInventory> lInventory = new List<DXInfo.Models.tbInventory>();

            int skitCount = (page - 1) * rows;
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                var q = QueryCondition(context);
                totalcount = q.Count().ToString();
                lInventory = q.OrderBy(o => o.cnvcInvCode)
                    .Skip(skitCount)
                    .Take(rows).ToList();
            //}
            DataTable dt = List2DataTable(context, lInventory);
            return ServiceHelper.DataTableToEasyUIDataGridJson(dt, totalcount);

        }
        private string newInventory(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbInventory inventory = new DXInfo.Models.tbInventory();
                    inventory.cnbProductBill = context.Request.Form["cnbProductBill"] == "on" ? true : false;// Convert.ToBoolean(context.Request.Form["cnbProductBill"]);
                    inventory.cnvcInvCode = context.Request.Form["cnvcInvCode"];
                    inventory.cnvcInvName = context.Request.Form["cnvcInvName"];
                    inventory.cnvcInvStd = context.Request.Form["cnvcInvStd"];
                    inventory.cnvcInvCCode = context.Request.Form["cnvcInvCCode"];
                    inventory.cnbSale = context.Request.Form["cnbSale"] == "on" ? true : false;// Convert.ToBoolean(context.Request.Form["cnbSale"]);
                    inventory.cnbPurchase = context.Request.Form["cnbPurchase"] == "on" ? true : false;// Convert.ToBoolean(context.Request.Form["cnbPurchase"]);
                    inventory.cnbSelf = context.Request.Form["cnbSelf"] == "on" ? true : false;// Convert.ToBoolean(context.Request.Form["cnbSelf"]);
                    inventory.cnbComsume = context.Request.Form["cnbComsume"] == "on" ? true : false;// Convert.ToBoolean(context.Request.Form["cnbComsume"]);
                    if(context.Request.Form["cniInvCCost"]!="")
                        inventory.cniInvCCost = Convert.ToDecimal(context.Request.Form["cniInvCCost"]);
                    if(context.Request.Form["cniInvNCost"]!="")
                    inventory.cniInvNCost = Convert.ToDecimal(context.Request.Form["cniInvNCost"]);
                    if(context.Request.Form["cniSafeNum"]!="")
                    inventory.cniSafeNum = Convert.ToDecimal(context.Request.Form["cniSafeNum"]);
                    if(context.Request.Form["cniLowSum"]!="")
                    inventory.cniLowSum = Convert.ToDecimal(context.Request.Form["cniLowSum"]);
                    if(context.Request.Form["cndSDate"]!="")
                    inventory.cndSDate = Convert.ToDateTime(context.Request.Form["cndSDate"]);
                    if(context.Request.Form["cndEDate"]!="")
                    inventory.cndEDate = Convert.ToDateTime(context.Request.Form["cndEDate"]);
                    inventory.cnvcCreatePerson = context.Request.Form["cnvcCreatePerson"];
                    
                    inventory.cnvcModifyPerson = context.Request.Form["cnvcModifyPerson"];

                    inventory.cndModifyDate = DateTime.Now;
                    inventory.cnvcValueType = context.Request.Form["cnvcValueType"];
                    inventory.cnvcGroupCode = context.Request.Form["cnvcGroupCode"];
                    inventory.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                    inventory.cnvcSAComUnitCode = context.Request.Form["cnvcSAComUnitCode"];
                    inventory.cnvcPUComUnitCode = context.Request.Form["cnvcPUComUnitCode"];
                    inventory.cnvcSTComUnitCode = context.Request.Form["cnvcSTComUnitCode"];
                    inventory.cnvcProduceUnitCode = context.Request.Form["cnvcProduceUnitCode"];
                    if(context.Request.Form["cnfRetailPrice"]!="")
                    inventory.cnfRetailPrice = Convert.ToDecimal(context.Request.Form["cnfRetailPrice"]);
                    inventory.cnvcShopUnitCode = context.Request.Form["cnvcShopUnitCode"];
                    inventory.cnvcFeel = context.Request.Form["cnvcFeel"];
                    inventory.cnvcOrganise = context.Request.Form["cnvcOrganise"];
                    inventory.cnvcColor = context.Request.Form["cnvcColor"];
                    inventory.cnvcTaste = context.Request.Form["cnvcTaste"];
                    if(context.Request.Form["cnnExpire"]!="")
                    inventory.cnnExpire = Convert.ToInt32(context.Request.Form["cnnExpire"]);
                    if(context.Request.Form["cnnDue"]!="")
                    inventory.cnnDue = Convert.ToInt32(context.Request.Form["cnnDue"]);
                    Uow.tbInventory.Add(inventory);

                    ServiceHelper.SyncGoods(inventory, Uow);
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
        private string updateInventory(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbInventory inventory = Uow.tbInventory.GetById(g=>g.cnvcInvCode==context.Request.Form["cnvcInvCode"]);
                    inventory.cnbProductBill = context.Request.Form["cnbProductBill"] == "on" ? true : false;

                    inventory.cnvcInvName = context.Request.Form["cnvcInvName"];
                    inventory.cnvcInvStd = context.Request.Form["cnvcInvStd"];
                    inventory.cnbSale = context.Request.Form["cnbSale"]=="on"?true:false;
                    inventory.cnbPurchase = context.Request.Form["cnbPurchase"]=="on"?true:false;
                    inventory.cnbSelf = context.Request.Form["cnbSelf"]=="on"?true:false;
                    inventory.cnbComsume = context.Request.Form["cnbComsume"] == "on" ? true : false;
                    if (context.Request.Form["cniInvCCost"] != "")
                        inventory.cniInvCCost = Convert.ToDecimal(context.Request.Form["cniInvCCost"]);
                    if (context.Request.Form["cniInvNCost"] != "")
                        inventory.cniInvNCost = Convert.ToDecimal(context.Request.Form["cniInvNCost"]);
                    if (context.Request.Form["cniSafeNum"] != "")
                        inventory.cniSafeNum = Convert.ToDecimal(context.Request.Form["cniSafeNum"]);
                    if (context.Request.Form["cniLowSum"] != "")
                        inventory.cniLowSum = Convert.ToDecimal(context.Request.Form["cniLowSum"]);
                    if (context.Request.Form["cndSDate"] != "")
                        inventory.cndSDate = Convert.ToDateTime(context.Request.Form["cndSDate"]);
                    if (context.Request.Form["cndEDate"] != "")
                        inventory.cndEDate = Convert.ToDateTime(context.Request.Form["cndEDate"]);
                    inventory.cnvcCreatePerson = context.Request.Form["cnvcCreatePerson"];
                    inventory.cnvcModifyPerson = context.Request.Form["cnvcModifyPerson"];

                    inventory.cndModifyDate = DateTime.Now;
                    inventory.cnvcValueType = context.Request.Form["cnvcValueType"];
                    inventory.cnvcGroupCode = context.Request.Form["cnvcGroupCode"];
                    inventory.cnvcComUnitCode = context.Request.Form["cnvcComUnitCode"];
                    inventory.cnvcSAComUnitCode = context.Request.Form["cnvcSAComUnitCode"];
                    inventory.cnvcPUComUnitCode = context.Request.Form["cnvcPUComUnitCode"];
                    inventory.cnvcSTComUnitCode = context.Request.Form["cnvcSTComUnitCode"];
                    inventory.cnvcProduceUnitCode = context.Request.Form["cnvcProduceUnitCode"];
                    if (context.Request.Form["cnfRetailPrice"] != "")
                        inventory.cnfRetailPrice = Convert.ToDecimal(context.Request.Form["cnfRetailPrice"]);
                    inventory.cnvcShopUnitCode = context.Request.Form["cnvcShopUnitCode"];
                    inventory.cnvcFeel = context.Request.Form["cnvcFeel"];
                    inventory.cnvcOrganise = context.Request.Form["cnvcOrganise"];
                    inventory.cnvcColor = context.Request.Form["cnvcColor"];
                    inventory.cnvcTaste = context.Request.Form["cnvcTaste"];
                    if (context.Request.Form["cnnExpire"] != "")
                        inventory.cnnExpire = Convert.ToInt32(context.Request.Form["cnnExpire"]);
                    if (context.Request.Form["cnnDue"] != "")
                        inventory.cnnDue = Convert.ToInt32(context.Request.Form["cnnDue"]);

                    ServiceHelper.SyncGoods(inventory, Uow);
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
        private string removeInventory(HttpContext context)
        {
            try
            {
                //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
                //{
                    DXInfo.Models.tbInventory inventory = Uow.tbInventory.GetById(g=>g.cnvcInvCode==context.Request.Form["cnvcInvCode"]);
                    Uow.tbInventory.Delete(inventory);
                    DXInfo.Models.tbGoods tbGoods = Uow.tbGoods.GetById(g=>g.vcGoodsID==inventory.cnvcInvCode);
                    if (tbGoods != null)
                    {
                        Uow.tbGoods.Delete(tbGoods);
                    }
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
        private void InventoryExportToExcel(HttpContext context)
        {
            string fileName = "存货档案.xls";

            List<DXInfo.Models.tbInventory> lInventory = new List<DXInfo.Models.tbInventory>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                var q = QueryCondition(context);
                lInventory = q.OrderBy(o => o.cnvcInvCode).ToList();
            //}
            DataTable dt = List2DataTable(context, lInventory);
            GridView view = new GridView();
            view.AutoGenerateColumns = false;

            BoundField field = new BoundField();
            field.DataField = "cnbProductBill";
            field.HeaderText = "允许生产订单";
            view.Columns.Add(field);

            field = new BoundField(); field.DataField = "cnvcInvCode"; field.HeaderText = "存货编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcInvName"; field.HeaderText = "存货名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcInvStd"; field.HeaderText = "规格型号"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcInvCCode"; field.HeaderText = "存货类别编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcInvCCodeComments"; field.HeaderText = "存货类别名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnbSale"; field.HeaderText = "是否销售"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnbPurchase"; field.HeaderText = "是否外购"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnbSelf"; field.HeaderText = "是否自制"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnbComsume"; field.HeaderText = "是否生产耗用"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cniInvCCost"; field.HeaderText = "参考成本（元）"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cniInvNCost"; field.HeaderText = "最新成本（元）"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cniSafeNum"; field.HeaderText = "安全库存量"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cniLowSum"; field.HeaderText = "最低库存量"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cndSDate"; field.HeaderText = "启用日期"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cndEDate"; field.HeaderText = "停用日期"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcCreatePerson"; field.HeaderText = "建档人编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcCreatePersonComments"; field.HeaderText = "建档人姓名"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcModifyPerson"; field.HeaderText = "变更人编码"; view.Columns.Add(field);            
            field = new BoundField(); field.DataField = "cnvcModifyPersonComments"; field.HeaderText = "变更人姓名"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cndModifyDate"; field.HeaderText = "变更日期"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcValueType"; field.HeaderText = "计价方式编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcValueTypeComments"; field.HeaderText = "计价方式名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcGroupCode"; field.HeaderText = "计量单位组编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcGroupCodeComments"; field.HeaderText = "计量单位组名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcComUnitCode"; field.HeaderText = "主计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcComUnitCodeComments"; field.HeaderText = "主计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcSAComUnitCode"; field.HeaderText = "销售计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcSAComUnitCodeComments"; field.HeaderText = "销售计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcPUComUnitCode"; field.HeaderText = "采购计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcPUComUnitCodeComments"; field.HeaderText = "采购计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcSTComUnitCode"; field.HeaderText = "库存计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcSTComUnitCodeComments"; field.HeaderText = "库存计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcProduceUnitCode"; field.HeaderText = "生产计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcProduceUnitCodeComments"; field.HeaderText = "生产计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnfRetailPrice"; field.HeaderText = "零售价格"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcShopUnitCode"; field.HeaderText = "零售计量单位编码"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcShopUnitCodeComments"; field.HeaderText = "零售计量单位名称"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcFeel"; field.HeaderText = "口感"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcOrganise"; field.HeaderText = "组织"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcColor"; field.HeaderText = "内馅"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnvcTaste"; field.HeaderText = "表面装饰"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnnExpire"; field.HeaderText = "过期限制（天）"; view.Columns.Add(field);
            field = new BoundField(); field.DataField = "cnnDue"; field.HeaderText = "到期提示（天）"; view.Columns.Add(field);

            view.DataSource = dt;
            view.DataBind();

            ServiceHelper.DoExportToExcel(context, fileName, view);
        }
        private string GetAllInventory(HttpContext context)
        {
            List<DXInfo.Models.tbInventory> lInventory = new List<DXInfo.Models.tbInventory>();

            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{

                lInventory = Uow.tbInventory.GetAll().ToList();
            //}
            DataTable dt = List2DataTable(context, lInventory);
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