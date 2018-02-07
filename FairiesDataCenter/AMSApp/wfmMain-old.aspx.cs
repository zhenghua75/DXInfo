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
	/// Summary description for wfmMain.
	/// </summary>
	public class wfmMainOld : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtQueryInvCode;
		protected System.Web.UI.WebControls.TextBox txtQueryInvName;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtMoveCount;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
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

			#region 控制当前显示菜单
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			bool bis = false;
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					if(ms1.strFuncAddress == "wfmPrompt") 
					{
						bis = true;
						break;
					}
					//HtmlTableRow trCurrent = tblParaMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm",String.Empty)) as HtmlTableRow;
//					if(trCurrent!=null)
//					{
						//
						
//					}
				}
			}
			if(bis)
			{
                this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
			}
			else
			{
                this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1',null);</script>");
			}
			#endregion
			

			if(!this.IsPostBack)
			{
				this.DBBind();		

				
			}
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			BusiComm.BusiQuery bq=new BusiComm.BusiQuery(strcons);
			DataTable dt = bq.GetExpInvList();
			Session["ExpInvList"]=dt;
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.PageSize=10;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;			
			this.DataGrid1.DataSource=(DataTable)Session["ExpInvList"];
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataBind();
			//this.RegisterStartupScript("show","<script lanaguage=javascript>ShowHide('1','block');</script>");
		}
	}
}
