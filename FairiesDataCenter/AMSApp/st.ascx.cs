using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AMSApp
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for st.
	/// </summary>
	public class st : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DropDownList ddlDataSource;

		private string strFiled = null;                   //要筛选的字段
		private DataTable dtSearchSource = null;          //提供的数据源

		public string StrFiled
		{
			set { strFiled = value; }
		}
		public DataTable DtSearchSource
		{
			set { dtSearchSource = value; }
		}
		private void BindDdl()
		{
			ddlDataSource.DataSource = dtSearchSource;
			ddlDataSource.DataTextField = strFiled;
			ddlDataSource.DataTextField = strFiled;
			ddlDataSource.DataBind();
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				if (dtSearchSource != null && strFiled != null && strFiled.Trim().Length > 0)
				{
					BindDdl();
				}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
