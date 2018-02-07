using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ynhnTransportManage.Models
{
    public partial class wfmBase : System.Web.Mvc.ViewPage<dynamic>
    {
        public CommCenter.AMSLog clog = new CommCenter.AMSLog();
        //标准页面重定向方法
        public void RedirectPage(string strPage)
        {
            if (strPage == null && strPage.Trim().Length == 0)
            {
                this.SetErrorMsgPage("页面错误！");
            }
            else
            {
                Response.Redirect(strPage, false);
            }
        }
        //根据错误信息重定向到错误页面，根目录
        public void SetErrorMsgPage(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("wfmFalse.aspx");
        }

        //根据错误信息重定向到错误页面，子目录
        public void SetErrorMsgPageBydir(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("../wfmFalse.aspx");
        }

        //根据错误信息重定向到错误页面，子目录
        public void SetErrorMsgPageBy2dir(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("../../wfmFalse.aspx");
        }

        //根据错误信息重定向到错误页面，子目录，返回上页
        public void SetErrorMsgPageBydirHistory(string strMsg)
        {
            Session["CommMsg"] = strMsg;
            this.RedirectPage("../wfmFalseHistory.aspx");
        }

        //根据错误信息重定向到错误页面，根目录
        public void SetSuccMsgPage(string strMsg)
        {
            Session.Remove("CommMsgNext");
            Session["CommMsg"] = strMsg;
            Session["CommMsgReturn"] = "";
            this.RedirectPage("wfmSuccess.aspx");
        }

        //根据错误信息重定向到错误页面，子目录
        public void SetSuccMsgPageBydir(string strMsg, string returnpage)
        {
            Session.Remove("CommMsgNext");
            Session["CommMsg"] = strMsg;
            Session["CommMsgReturn"] = returnpage;
            this.RedirectPage("../wfmSuccess.aspx");
        }

        public void SetSuccMsgPageBydir(string strMsg, string returnpage, string nextpage, string strnextname)
        {
            Session.Remove("CommMsgNext");
            Session["CommMsg"] = strMsg;
            Session["CommMsgReturn"] = returnpage;
            Session["CommMsgNext"] = nextpage;
            Session["CommMsgNextName"] = strnextname;
            this.RedirectPage("../wfmSuccess2bt.aspx");
        }
    }
}