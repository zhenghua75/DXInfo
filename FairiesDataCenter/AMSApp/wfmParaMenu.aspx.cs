using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CommCenter;

namespace AMSApp
{
	/// <summary>
	/// Summary description for wfmParaMenu.
	/// </summary>
	public class wfmParaMenu : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblParaMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;
		protected System.Web.UI.HtmlControls.HtmlTableRow trParaRefresh;
		protected System.Web.UI.HtmlControls.HtmlTableRow trGoods;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLoginOper;
		protected System.Web.UI.HtmlControls.HtmlTableRow trLoginPwd;
		protected System.Web.UI.HtmlControls.HtmlTableRow trNotice;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSysParaSet;
		protected System.Web.UI.HtmlControls.HtmlTableRow trComputationUnit;
        protected System.Web.UI.HtmlControls.HtmlTableRow trComputationGroup;
		protected System.Web.UI.HtmlControls.HtmlTableRow trInventory;
        protected System.Web.UI.HtmlControls.HtmlTableRow trProductClass;
        protected System.Web.UI.HtmlControls.HtmlTableRow trSupplier;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProviderGoods;
        protected System.Web.UI.HtmlControls.HtmlTableRow trBillOfMaterials;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDeptManage;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWarehouse;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDeptOperManage;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			CMSMStruct.LoginStruct ls1=new CommCenter.CMSMStruct.LoginStruct();
			if(Session["Login"]==null)
			{
				Response.Redirect("Exit.aspx");
			}
			else
			{
				ls1=(CMSMStruct.LoginStruct)Session["Login"];
			}

			#region 设置所有菜单为隐藏
			trParaRefresh.Visible = false;
			trGoods.Visible = false;
			trLoginOper.Visible = false;
			trLoginPwd.Visible  = false;
			trNotice.Visible  = false;
			trSysParaSet.Visible = false;
			trComputationUnit.Visible = false;
			trInventory.Visible = false;
            trSupplier.Visible = false;
			trProviderGoods.Visible=false;
            trBillOfMaterials.Visible = false;
			trDeptManage.Visible=false;
			trWarehouse.Visible=false;
			trDeptOperManage.Visible=false;;
            trProductClass.Visible = false;
            trComputationGroup.Visible = false;
			#endregion

			#region 控制当前显示菜单
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblParaMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm",String.Empty)) as HtmlTableRow;
                    if (trCurrent == null) trCurrent = tblParaMenu.FindControl("tr" + ms1.strFuncAddress.Replace("tb", String.Empty)) as HtmlTableRow;
					if(trCurrent!=null)
					{
						trCurrent.Visible = true;
						trnoprom.Visible=false;
					}
				}
			}
			#endregion
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
