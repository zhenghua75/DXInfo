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
	/// Summary description for wfmQueryMenu.
	/// </summary>
	public class wfmQueryMenu : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblQueryMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAssInfo;
		protected System.Web.UI.HtmlControls.HtmlTableRow trConsItem;
		protected System.Web.UI.HtmlControls.HtmlTableRow trFillQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trConsKindQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBusiLogQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trTopQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBusiIncome;
		protected System.Web.UI.HtmlControls.HtmlTableRow trDailyCashQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSalesChart;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSpecConsQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProductClassQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSellReport;

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

			#region �������в˵�Ϊ����
			trAssInfo.Visible = false;
			trConsItem.Visible = false;
			trFillQuery.Visible = false;
			trConsKindQuery.Visible  = false;
			trBusiLogQuery.Visible  = false;
			trTopQuery.Visible = false;
			trBusiIncome.Visible = false;
			trDailyCashQuery.Visible=false;
			trSalesChart.Visible = false;
			trSpecConsQuery.Visible = false;
			this.trProductClassQuery.Visible = false;
			this.trSellReport.Visible = false;
			#endregion

			#region ���Ƶ�ǰ��ʾ�˵�
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblQueryMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm",String.Empty)) as HtmlTableRow;
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
