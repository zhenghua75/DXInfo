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
	/// wfmOrderAdd 的摘要说明。
	/// </summary>
	public class wfmOrderAdd : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlAddType;
		protected System.Web.UI.WebControls.TextBox txtAddComments;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["ProductList"] == null)
			{
				btnOK.Visible = false;
			}
			else
			{
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				if(dtOrderBookDetail.Rows.Count > 0)
				{
					btnOK.Visible = true;
				}
				else
				{
					btnOK.Visible = false;
				}
				
			}
			if(!this.IsPostBack)
			{
//				string strAddType = "select * from tbCommCode where vcCommSign='AType'";
//				DataTable dtAddType = Helper.Query(strAddType);
//
//
//				this.ddlAddType.DataSource = dtAddType;
//				this.ddlAddType.DataValueField = "vcCommCode";
//				this.ddlAddType.DataTextField = "vcCommName";
//				this.ddlAddType.DataBind();

				this.BindNameCode(ddlAddType, "cnvcType='ORDEROPERTYPE'");


				if(Session["ProductList"] != null)
				{
					DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
					this.DataGrid2.DataSource = dtOrderBookDetail;
					this.DataGrid2.DataBind();
				}
				if(Request["OrderSerialNo"] == null)
				{
					Popup("无效订单流水");
					return;
				}
				if(Request["OrderState"] == null)
				{
					Popup("无效订单");
					return;
				}
				string strOrderState = Request["OrderState"].ToString();
				if(strOrderState != "1")
				{
					Popup("未到加减单流程，可进行编辑");
					//this.Response.Redirect("wfmOrderQuery.aspx");
					return;
				}
				txtOrderSerialNo.Text = Request["OrderSerialNo"].ToString();	
				txtOrderSerialNo.Enabled = false;
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

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataTable dtOrderAdd = (DataTable) Session["ProductList"];
				OrderFacade order = new OrderFacade();
				string strOrderSerialNo = txtOrderSerialNo.Text;
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "加单";

				order.OrderAdd(strOrderSerialNo, ddlAddType.SelectedValue, txtAddComments.Text, dtOrderAdd, operLog);
				Session["ProductList"] = null;
				btnCancel_Click(null, null);
				btnOK.Visible = false;
				Popup("加单成功");

				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				this.DataGrid2.DataSource = dtOrderBookDetail;
				this.DataGrid2.DataBind();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtAddComments.Text = "";
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
