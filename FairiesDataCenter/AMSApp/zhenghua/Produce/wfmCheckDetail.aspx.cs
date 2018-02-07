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
namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmCheckDetail 的摘要说明。
	/// </summary>
	public class wfmCheckDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				if(this.Request["produceserialno"] == null)
				{
					this.Popup("无效链接");
					return;
				}
				this.txtProduceSerialNo.Text = this.Request["produceserialno"].ToString();
				bindgrid(this.Request["produceserialno"].ToString());
			}
		}

		private void bindgrid(string strproduceserialno)
		{	
			string strsql = "select * from tbmakelog where cnnproduceserialno="+strproduceserialno;
			DataTable dtMakeLog = Helper.Query(strsql);
			if(dtMakeLog.Rows.Count==0)
			{
				this.Popup("无法找到生产计划");
				return;
			}
			Entity.MakeLog ml = new MakeLog(dtMakeLog);

			string strsql1 = "select * from tbmakedetail where cnnCount>0 and cnnmakeserialno="+ml.cnnMakeSerialNo.ToString();
			DataTable dtMakeDetail = Helper.Query(strsql1);

			string strsql2 = "select * from tbproducechecklog where cnnproduceserialno="+strproduceserialno;
			DataTable dtCheckLog = Helper.Query(strsql2);

			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnCheckCount",dtCheckLog,"cnvcInvCode","cnnCheckCount","");
			Helper.DataTableConvert(dtMakeDetail,"cnnMakeSerialNo","cnnProduceSerialNo",dtMakeLog,"cnnMakeSerialNo","cnnProduceSerialNo","");
			dtMakeDetail.Columns["cnnMakeCount"].ColumnName = "cnnOrderCount";
			dtMakeDetail.Columns["cnnCount"].ColumnName = "cnnProduceCount";
			//Session["tbProduceDetail"] = dtMakeDetail;

			this.DataGrid1.DataSource = dtMakeDetail;
			this.DataGrid1.DataBind();
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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			this.DataGridToExcel(DataGrid1, "盘点细节");
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmDividModify.aspx?ProduceSerialNo="+this.txtProduceSerialNo.Text);
		}
	}
}
