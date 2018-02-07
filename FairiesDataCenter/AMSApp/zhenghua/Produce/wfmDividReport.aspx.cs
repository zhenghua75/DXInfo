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
	/// wfmDividReport 的摘要说明。
	/// </summary>
	public class wfmDividReport : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblShipDate;
		protected System.Web.UI.WebControls.Label lblOrderDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblOper;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnPrint;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnDPrint;
		protected System.Web.UI.WebControls.TextBox txtAssignSerialNo;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblAssignSerialNo;
		protected System.Web.UI.WebControls.Button btnReturn;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,1);");
			this.btnDPrint.Attributes.Add("onclick","document.all.WebBrowser.ExecWB(6,6);");
			if(!this.IsPostBack)
			{
				if(Request["OrderSerialNo"]== null ||Request["ProduceSerialNo"]== null||Request["AssignSerialNo"]==null)
				{
					Popup("无效链接");
				}
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				string strAssignSerialNo = Request["AssignSerialNo"].ToString();
				txtProduceSerialNo.Text = Request["ProduceSerialNo"].ToString();
				txtOrderSerialNo.Text = strOrderSerialNo;
				txtAssignSerialNo.Text = strAssignSerialNo;
				this.lblAssignSerialNo.Text = strAssignSerialNo.PadLeft(10,'0');
				BindOrder(strOrderSerialNo,strAssignSerialNo);
			}
		}
		private void BindOrder(string strOrderSerialNo,string strAssignSerialNo)
		{
			string strOrder = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strOrder);
			if(dtOrder.Rows.Count == 0)
			{
				Popup("数据异常，无此订单");
				return;
			}
			this.DataTableConvert(dtOrder, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			OrderBook order = new OrderBook(dtOrder);
			string strAssign = "select * from tbAssignDetail where cnnOrderSerialNo="+strOrderSerialNo+" and cnnAssignSerialNo=" + strAssignSerialNo +" order by cnvcInvCode";
			DataTable dtAssign = Helper.Query(strAssign);
			if(dtAssign.Rows.Count == 0)
			{
				Popup("数据异常，此订单无分货数据");
				return;
			}
			this.DataTableConvert(dtAssign,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			//
			this.DataTableConvert(dtAssign,"cnvcInvCode","cnfRetailPrice","tbInventory","cnvcInvCode","cnfRetailPrice","");
			dtAssign.Columns.Add("cnnSum");
			foreach(DataRow dr in dtAssign.Rows)
			{
				dr["cnnSum"] = Convert.ToDecimal(dr["cnnAssignCount"].ToString())*Convert.ToDecimal(dr["cnfretailprice"].ToString());
			}
			this.DataGrid1.DataSource = dtAssign;
			this.DataGrid1.DataBind();

			this.lblOper.Text = oper.strOperName;
			this.lblOrderDept.Text = dtOrder.Rows[0]["cnvcOrderDeptIDComments"].ToString();
			this.lblShipDate.Text = order.cndShipDate.ToString("yyyy年MM月dd日");
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
			this.btnPrint.ServerClick += new System.EventHandler(this.btnPrint_ServerClick);
			this.btnDPrint.ServerClick += new System.EventHandler(this.btnDPrint_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmDividAdjust.aspx?ProduceSerialNo="+txtProduceSerialNo.Text);
		}

		private void btnPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "发货单";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnDPrint_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				ProduceFacade pf = new ProduceFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "发货单";
				pf.AssignPrint(txtAssignSerialNo.Text,operLog);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
