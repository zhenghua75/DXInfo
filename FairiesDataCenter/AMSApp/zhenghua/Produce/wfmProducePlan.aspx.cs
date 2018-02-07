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
	/// wfmProducePlan 的摘要说明。
	/// </summary>
	public class wfmProducePlan : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.TextBox txtShipBeginDate;
		protected System.Web.UI.WebControls.TextBox txtShipEndDate;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.CheckBox chkSelf;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.txtProduceDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtShipBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtShipEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProduceDate.Text = "";
			this.txtShipBeginDate.Text = "";
			this.txtShipEndDate.Text = "";
		}

		
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(JudgeIsNull(txtProduceDate.Text,"生产日期"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipBeginDate.Text,"开始日期"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipEndDate.Text,"结束日期"))
				{
					//Popup();
					return;
				}

				ProduceLog producePlan = new ProduceLog();
				producePlan.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				producePlan.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				producePlan.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				producePlan.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				producePlan.cnvcOperID = oper.strLoginID;
				producePlan.cnvcProduceState = "0";
				producePlan.cnbSelf = this.chkSelf.Checked;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "添加生产计划";

				ProduceFacade produce = new ProduceFacade();
				produce.AddProduceLog(producePlan,operLog);
				
				btnCancel_Click(null, null);
				//this.btnReturn_Click(null,null);
				//this.Response.Redirect("wfmProducePlanQuery.aspx",true);
				Popup("生产计划添加成功","wfmProducePlanQuery.aspx");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQuery.aspx");
		}
	}
}
