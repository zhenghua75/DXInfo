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

namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmProducePlanQuery 的摘要说明。
	/// </summary>
	public class wfmSalesRoomProduce : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtProduceBeginDate;
		protected System.Web.UI.WebControls.TextBox txtProduceEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlan.aspx");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProduceBeginDate.Text = "";
			this.txtProduceEndDate.Text = "";
			this.Datagrid2.DataSource = null;
			this.Datagrid2.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = 0;
			BindGrid();
		}

		private void BindGrid()
		{
			string strSql = "select * from tbOrderBook where cnvcOrderType='SELFPRODUCE' ";
			if(txtProduceBeginDate.Text.Trim().Length > 0)
			{
				strSql += " and cndShipDate >='" + txtProduceBeginDate.Text + "'";
			}
			if(txtProduceEndDate.Text.Trim().Length > 0)
			{
				strSql += " and cndShipDate <='" + txtProduceEndDate.Text + "'";
			}
			strSql += " and cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			DataTable dtProduce = Helper.Query(strSql);
			this.DataTableConvert(dtProduce, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtProduce, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			this.DataTableConvert(dtProduce, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
			this.DataTableConvert(dtProduce, "cnvcOrderOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.Datagrid2.DataSource = dtProduce;
			this.Datagrid2.DataBind();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

			this.Datagrid2.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}
	}
}
