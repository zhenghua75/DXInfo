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
	/// Summary description for wfmStorageReport.
	/// </summary>
	public class wfmStorageReport : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblStorageReportMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trEnterStorageReport;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trReceiveMaterialReport;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trStorageCheckLog;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWHStorageRepot;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPoStockReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trPoStockReturnReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trInventoryMoveReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDeptStorageEnterReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDeptInventoryLostReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSaleDailyCheckReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trStorageBalanceReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trStorageHisReport;

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
//			trEnterStorageReport.Visible=false;
//			trReceiveMaterialReport.Visible=false;
//			trStorageCheckLog.Visible=false;
			trWHStorageRepot.Visible=false;
			trPoStockReport.Visible=false;
			trPoStockReturnReport.Visible=false;
			trInventoryMoveReport.Visible=false;
			trDeptStorageEnterReport.Visible=false;
			trDeptInventoryLostReport.Visible=false;
			trSaleDailyCheckReport.Visible=false;
			trStorageBalanceReport.Visible=false;
			trStorageHisReport.Visible=false;
			#endregion

			#region 控制当前显示菜单
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblStorageReportMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm",String.Empty)) as HtmlTableRow;
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
