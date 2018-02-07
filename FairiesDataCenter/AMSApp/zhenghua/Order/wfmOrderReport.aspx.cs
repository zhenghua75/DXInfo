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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmDividReport 的摘要说明。
	/// </summary>
	public class wfmOrderReport : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblOper;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Button btnReturn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				if(Session["OrderReport"] == null)
				{
					Popup("无效链接");
					return;
				}				
				string strReport = Session["OrderReport"].ToString();
				DataTable dtReport = Helper.Query(strReport);

				this.DataTableConvert(dtReport, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
				this.DataTableConvert(dtReport, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
				//this.DataTableConvert(dtReport, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
				this.DataTableConvert(dtReport, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
				this.DataTableConvert(dtReport, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
				this.DataGrid1.DataSource = dtReport;
				this.DataGrid1.DataBind();

				this.lblOper.Text = this.oper.strOperName;
				this.lblDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
				if(Request["OrderType"] != null)
				{
					this.lblDate.Text += Request["OrderType"].ToString();
				}
				this.lblDate.Text += "清单表";
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			Session["OrderReport"] = null;
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
