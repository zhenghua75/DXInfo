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
	/// wfmProducePlanQuery ��ժҪ˵����
	/// </summary>
	public class wfmWarehouseOut : wfmBase
	{
		#region �ֶ�
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtProduceBeginDate;
		protected System.Web.UI.WebControls.TextBox txtProduceEndDate;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("����", "%");
				this.ddlProduceDept.Items.Add(li);
				this.SetDDL(this.ddlProduceDept,this.oper.strDeptID);
				if(this.oper.strDeptID !="CEN00")
				{				
					this.ddlProduceDept.Enabled = false;		
				}
				this.txtProduceBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtProduceEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				btnQuery_Click(null,null);
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
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
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			BindGrid();
		}

		private void BindGrid()
		{
			string strSql = "select * from tbProduceLog ";
			strSql += " where cnvcProduceState in('2','3','8') ";
			if(txtProduceBeginDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate >='" + txtProduceBeginDate.Text + "'";
			}
			if(txtProduceEndDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate <='" + txtProduceEndDate.Text + "'";
			}
			strSql += " and cnvcProduceDeptID like '"+ddlProduceDept.SelectedValue+"'";
			DataTable dtProduceLog = Helper.Query(strSql);
			this.DataTableConvert(dtProduceLog, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtProduceLog, "cnvcProduceState", "tbNameCode", "cnvcCode", "cnvcName",
			                      "cnvcType='PRODUCESTATE'");
			this.DataTableConvert(dtProduceLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataGrid1.DataSource = dtProduceLog;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}
	}
}
