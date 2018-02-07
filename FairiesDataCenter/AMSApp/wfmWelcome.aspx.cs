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

namespace AMSApp
{
	/// <summary>
	/// Summary description for wfmWelcome.
	/// </summary>
	public class wfmWelcome : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblwel;
		public string strComments = "";
		protected System.Web.UI.HtmlControls.HtmlGenericControl divt;
		public string strReleaseDate = "";
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            //if(Session["Login"]==null)
            //{
            //    Response.Redirect("default.aspx");
            //}
			DataTable dtNotice = (DataTable)Session["tbNotice"];
			if(	dtNotice != null && dtNotice.Rows.Count >0)
			{
				divt.Visible = true;
				DataRow drTemp = dtNotice.Rows[0];
				//TableNotice tbNotice = new TableNotice();
				strComments = drTemp["cnvcComments"].ToString();
				strReleaseDate = drTemp["cndReleaseDate"].ToString();
				this.lblwel.Text="";
				this.lblwel.Visible = false;
			}
			else
			{
				divt.Visible = false;
				this.lblwel.Text="欢迎进入网络中心";
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
