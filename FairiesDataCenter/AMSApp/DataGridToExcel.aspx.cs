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
using System.Globalization;
using System.Threading;

namespace AMSApp
{
	/// <summary>
	/// Summary description for DataGridToExcel.
	/// </summary>
	public class DataGridToExcel : wfmBase
	{
		protected ucPageView UcPageView1 ;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["toExcel"] != null)
			{
				UcPageView1.MyDataGrid.PageSize = 60000;
				DataTable dtOut	= (DataTable)Session["toExcel"];
				string ExcelName =dtOut.TableName + System.DateTime.Now.ToString("_yyMMdd");
				if(dtOut.Rows.Count > 0)
				{
					DataView dvOut =new DataView(dtOut);
					this.UcPageView1.MyDataSource = dvOut;
					this.UcPageView1.BindGrid();
					Response.AddHeader("Content-Disposition","inline; filename="+System.Web.HttpUtility.UrlEncode(ExcelName)+".xls");
					Response.ContentType = "application/vnd.ms-excel";
					Response.Charset = "UTF-8";
					Response.ContentEncoding = System.Text.Encoding.UTF8;
					EnableViewState = false;

//					CultureInfo culture = null;			
//					culture = new CultureInfo("zh-CN",true);
//					Thread.CurrentThread.CurrentCulture = culture;	

					System.IO.StringWriter tw = new System.IO.StringWriter();
					System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
					UcPageView1.MyDataGrid.RenderControl(hw);

					Response.Write(tw.ToString());					
					Response.End();	
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
