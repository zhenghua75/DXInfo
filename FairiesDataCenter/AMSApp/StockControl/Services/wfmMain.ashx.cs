using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using CommCenter;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// LoginStruct 的摘要说明
    /// </summary>
    public class wfmMain : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(GetOperName(context));
        }
        private string GetOperName(HttpContext context)
        {
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)context.Session["Login"];
            return ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(true, ls1.strOperName));
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