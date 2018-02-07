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
	/// Summary description for wfmEmpMenu.
	/// </summary>
	public class wfmEmpMenu : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblEmpMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trEmpInfo;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWorkDailyEvery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trShcQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSignCalc;
		protected System.Web.UI.HtmlControls.HtmlTableRow trEmpUnitSign;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;
		protected System.Web.UI.HtmlControls.HtmlTableRow trSignQuery;

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
			trEmpInfo.Visible = false;
			trWorkDailyEvery.Visible = false;
			trShcQuery.Visible = false;
			trSignCalc.Visible  = false;
			trEmpUnitSign.Visible  = false;
			trSignQuery.Visible = false;
			#endregion

			#region ���Ƶ�ǰ��ʾ�˵�
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblEmpMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm",String.Empty)) as HtmlTableRow;
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
