using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DXInfo.Data.Contracts;

namespace AMSApp.StockControl.Services
{
    public class MyHttpHandlerBase : IHttpHandler, IRequiresSessionState
    {
        protected IAMSCMUow Uow { get; set; }
        public virtual void ProcessRequest(HttpContext context)
        {
        }
        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}