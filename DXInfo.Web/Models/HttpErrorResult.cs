using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXInfo.Web.Models
{
    public class HttpErrorResult : ActionResult
    {
        private string _errorMessage;
        public HttpErrorResult(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Charset = "utf-8";
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
            context.HttpContext.Response.StatusCode = 500;
            context.HttpContext.Response.StatusDescription = _errorMessage;
            context.HttpContext.Response.Write(_errorMessage);
        }
    }
}