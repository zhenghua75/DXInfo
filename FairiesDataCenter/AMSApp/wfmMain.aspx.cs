using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CommCenter;

namespace AMSApp
{
    public partial class wfmMain : wfmBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 控制当前显示菜单
            CMSMStruct.LoginStruct ls1 = (CMSMStruct.LoginStruct)Session["Login"];
            Hashtable htOperFunc = (Hashtable)Application["OperFunc"];
            ArrayList almenu = (ArrayList)htOperFunc[ls1.strLoginID];
            if (almenu != null)
            {
                for (int i = 0; i < almenu.Count; i++)
                {
                    CMSMStruct.MenuStruct ms1 = (CMSMStruct.MenuStruct)almenu[i];
                    System.Web.UI.HtmlControls.HtmlGenericControl trCurrent = this.FindControl("li" + ms1.strFuncAddress.Replace("wfm", String.Empty)) as System.Web.UI.HtmlControls.HtmlGenericControl;
                    if (trCurrent == null) trCurrent = this.FindControl("li" + ms1.strFuncAddress.Replace("tb", String.Empty)) as System.Web.UI.HtmlControls.HtmlGenericControl;
                    if (trCurrent != null)
                    {
                        trCurrent.Visible = true;
                    }
                }
            }
            #endregion
        }
    }
}