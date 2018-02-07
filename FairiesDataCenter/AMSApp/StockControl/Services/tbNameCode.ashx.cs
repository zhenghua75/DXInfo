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
    /// tbNameCode 的摘要说明
    /// </summary>
    public class tbNameCode : MyHttpHandlerBase
    {
        public tbNameCode(IAMSCMUow uow)
        {
            this.Uow = uow;
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetSomeNameCode(context));
            
        }
        private DataTable List2DataTable(HttpContext context, List<DXInfo.Models.tbNameCode> ltbNameCode)
        {
            DataTable dt = ltbNameCode.ToDataTable<DXInfo.Models.tbNameCode>();
            return dt;
        }
        private string GetSomeNameCode(HttpContext context)
        {
            List<DXInfo.Models.tbNameCode> ltbNameCode = new List<DXInfo.Models.tbNameCode>();
            string method = context.Request["method"];
            //using (DXInfo.Models.AMSCM amscm = new DXInfo.Models.AMSCM())
            //{
                ltbNameCode = Uow.tbNameCode.GetAll().Where(w=>w.cnvcType==method).ToList();
            //}
            DataTable dt = List2DataTable(context, ltbNameCode);
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