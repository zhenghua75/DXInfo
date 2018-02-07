using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AMSApp.zhenghua.Business;
using System.Web.SessionState;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// ProductType 的摘要说明
    /// </summary>
    public class tbProductType : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetProductType(context));
        }
        private string GetProductType(HttpContext context)
        {
            DataTable dt = Helper.Query("select cnvcCode as id,cnvcName as text from tbNameCode where cnvcType='PRODUCTTYPE'");
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