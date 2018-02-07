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
	/// Summary description for wfmSuccess.
	/// </summary>
	public class wfmSuccess : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lbMessage;
		//protected System.Web.UI.HtmlControls.HtmlInputButton btreturn;

		protected string strpage="";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.IsPostBack)
			{
				lbMessage.Text = Session["CommMsg"].ToString();
				strpage=(string)Session["CommMsgReturn"];
                //if (strpage != "")
                //{
                //    string strpageurl = "javascript:parent.right.location='" + strpage + "';";
                //    this.btreturn.Attributes.Add("onclick", strpageurl);
                //}
                //else
                //{
                //    this.btreturn.Attributes.Add("onclick", "javascript:window.history.go(-1);");
                //}
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
