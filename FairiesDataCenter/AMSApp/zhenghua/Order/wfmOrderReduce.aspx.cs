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
	/// wfmOrderReduce 的摘要说明。
	/// </summary>
	public class wfmOrderReduce : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlReduceType;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtReduceComments;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button btnCancel;
	
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
//				string strReduceType = "select * from tbCommCode where vcCommSign='RTYPE'";
//				DataTable dtReduceType = Helper.Query(strReduceType);
//
//
//				this.ddlReduceType.DataSource = dtReduceType;
//				this.ddlReduceType.DataValueField = "vcCommCode";
//				this.ddlReduceType.DataTextField = "vcCommName";
//				this.ddlReduceType.DataBind();

				BindNameCode(ddlReduceType, "cnvcType='ORDEROPERTYPE'");


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
				DataTable dtOrderReduce = (DataTable) Session["ProductList"];
				OrderFacade order = new OrderFacade();
				string strOrderSerialNo = txtOrderSerialNo.Text;
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "减单";

				order.OrderReduce(strOrderSerialNo, ddlReduceType.SelectedValue, txtReduceComments.Text, dtOrderReduce, operLog);
				Session["ProductList"] = null;
				btnCancel_Click(null, null);
				btnOK.Visible = false;
				Popup("减单成功");

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
			this.txtReduceComments.Text = "";
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
