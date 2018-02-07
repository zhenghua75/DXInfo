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
	/// Summary description for wfmMainTop.
	/// </summary>
	public class wfmMainTop : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lbloper;
		protected bool Condition=false;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		public string strPopStr = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			DateTime dtnow=DateTime.Now;
			this.Label1.Text=dtnow.ToLongDateString()+ " "+ dtnow.DayOfWeek.ToString();
			if(Session["Login"]==null)
			{
				Condition=true;
				this.Response.Redirect("Exit.aspx");
			}
			else
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				this.lbloper.Text="²Ù×÷Ô±£º"+ls1.strOperName;
			}

//			if(!this.IsPostBack)
//			{
//				DataTable dtNotice = (DataTable)Session["tbNotice"];				
//				if(	dtNotice != null && dtNotice.Rows.Count >0)
//				{
//					strPopStr ="openwin('./Pop/notice.aspx')";
//				}
//			}
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
