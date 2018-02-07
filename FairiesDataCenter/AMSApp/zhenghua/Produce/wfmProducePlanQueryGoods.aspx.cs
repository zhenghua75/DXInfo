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
	public class wfmProducePlanQueryGoods : wfmBase
	{
		#region 字段
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
		protected System.Web.UI.WebControls.Button btnDept;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("所有", "%");
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
			this.btnDept.Click += new System.EventHandler(this.btnDept_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
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

			this.DataGrid1.CurrentPageIndex = 0;
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
//			string strSql = "select a.*,b.cnvcDeptName as cnvcProduceDeptIDComments,c.cnvcName as cnvcProduceStateComments,d.vcOperName as cnvcOperIDComments  from tbProduceLog a ";
//			strSql += " left outer join tbDept b on a.cnvcProduceDeptID=b.cnvcDeptID ";
//			strSql += " left outer join (select * from tbNameCode where cnvcType='PRODUCESTATE') c on a.cnvcProduceState=c.cnvcCode ";
//			strSql += " left outer join tbLogin d on a.cnvcOperID=d.vcLoginID";
			strSql += " where cnvcProduceState in('5','6','7','8') and cnvcProduceDeptID like '"+ddlProduceDept.SelectedValue+"'";
			if(txtProduceBeginDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate >='" + txtProduceBeginDate.Text + "'";
			}
			if(txtProduceEndDate.Text.Trim().Length > 0)
			{
				strSql += " and cndProduceDate <='" + txtProduceEndDate.Text + "'";
			}
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

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
//			{								
//				//LinkButton btnDelete = (LinkButton)(e.Item.Cells[8].Controls[0]);
//				//btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
//
//				if(e.Item.Cells[7].Text != "0")
//				{
//					HyperLink btnEdit = (HyperLink)(e.Item.Cells[9].Controls[0]);
//					btnEdit.Attributes.Add("onClick","JavaScript:alert('不可编辑');return false;");
//				}
//				
//				if(e.Item.Cells[7].Text != "1")
//				{
//					HyperLink btnOrderAdd = (HyperLink)(e.Item.Cells[10].Controls[1]);
//					btnOrderAdd.Attributes.Add("onClick","JavaScript:alert('不可加单');return false;");
//
//					HyperLink btnOrderReduce = (HyperLink)(e.Item.Cells[11].Controls[1]);
//					btnOrderReduce.Attributes.Add("onClick","JavaScript:alert('不可减单');return false;");
//				}							
//			} 
		}

		private void btnDept_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmDeptPriority.aspx");
		}
	}
}
