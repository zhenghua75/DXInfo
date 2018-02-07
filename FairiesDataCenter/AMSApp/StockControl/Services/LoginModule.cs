using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommCenter;
using System.Collections;

namespace AMSApp.StockControl.Services
{
    public class UserAuthorizationModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        { }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
        }
        private bool CanUseModule(HttpApplication application, string strLoginID, string strPage)
        {

            Hashtable htOperFunc = (Hashtable)application.Context.Application["OperFunc"];
            ArrayList almenu = (ArrayList)htOperFunc[strLoginID];
            bool bis = false;
            if (almenu != null)
            {
                for (int i = 0; i < almenu.Count; i++)
                {
                    CMSMStruct.MenuStruct ms1 = (CMSMStruct.MenuStruct)almenu[i];
                    string strfa = ms1.strFuncAddress.Split('_')[0];
                    if (strfa + ".aspx" == strPage)
                    {
                        bis = true;
                        break;
                    }
                }
            }
            return bis;
        }
        void context_AcquireRequestState(object sender, EventArgs e)
        {
            // 获取应用程序
            HttpApplication application = (HttpApplication)sender;

            // 检查用户是否已经登录
            if (application.Context.Session == null || application.Context.Session[ServiceHelper.LoginSessionKey] == null)
            {
                // 获取Url
                string requestUrl = application.Request.CurrentExecutionFilePath;
                string requestPage = requestUrl.Substring(requestUrl.LastIndexOf('/') + 1);

                // 如果请求的页面不是登录页面，刚重定向到登录页面。
                if (application.Request.CurrentExecutionFilePathExtension == ".aspx")
                {
                    if (requestPage != "default.aspx" && requestPage != "wfmFalse.aspx")
                    {
                        application.Response.Write("<script>window.top.location.href='" + application.Request.ApplicationPath + "/default.aspx';</script>");
                        application.Response.End();
                    }
                }
                if (application.Request.CurrentExecutionFilePathExtension == ".ashx")
                {
                    application.Response.Write(ServiceHelper.JsonSerializer<JEasyUIResult>(new JEasyUIResult(false, "loginOvertime")));
                    application.Response.End();
                }

            }
            else
            {
                // 获取用户名和Url
                CommCenter.CMSMStruct.LoginStruct ls = (CommCenter.CMSMStruct.LoginStruct)application.Session[ServiceHelper.LoginSessionKey];

                string requestUrl = application.Request.CurrentExecutionFilePath;
                string requestPage = requestUrl.Substring(requestUrl.LastIndexOf('/') + 1);

                // 如果用户没有被授权，请求被终止，并打印提示信息。
                if (application.Request.CurrentExecutionFilePathExtension == ".aspx"
                    && requestPage != "default.aspx" && requestPage != "wfmMain.aspx" && requestPage != "Exit.aspx"
                    && requestPage != "wfmWelcome.aspx"
                    //&& requestPage !="wfmOperPurview.aspx"
                    && requestPage != "wfmSuccess.aspx"
                    && requestPage != "wfmFalse.aspx"
                    && requestPage != "wfmFalseHistory.aspx")
                {
                    if (!CanUseModule(application, ls.strLoginID, requestPage))
                    {
                        application.CompleteRequest();
                        application.Response.Write(string.Format("对不起！{0}，您无权访问此模块(" + requestPage + ")！", ls.strOperName));
                    }
                }
            }
        }

        #endregion
    }

}