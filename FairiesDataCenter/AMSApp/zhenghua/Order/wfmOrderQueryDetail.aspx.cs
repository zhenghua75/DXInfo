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
	/// wfmOrderQueryDetail 的摘要说明。
	/// </summary>
	public class wfmOrderQueryDetail : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList ddlSalesRoom;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList ddlOrderType;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.DropDownList ddlOrderOper;
		protected System.Web.UI.WebControls.TextBox txtOrderDate;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtShipDate;
		protected System.Web.UI.WebControls.DropDownList ddlOrderState;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtShipAddress;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtLinkPhone;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.TextBox txtArrivedDate;
		protected System.Web.UI.HtmlControls.HtmlTable tblCustom;
		protected System.Web.UI.WebControls.TextBox txtCustomName;
		protected System.Web.UI.WebControls.Label lblOrderState;
		protected System.Web.UI.WebControls.Label lblOrderDate;
		protected System.Web.UI.WebControls.Label lblOrderOper;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnModify;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtOrderComments;
		#endregion
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				BindDept(ddlSalesRoom, "cnvcDeptType<>'Corp'");//"cnvcDeptType='SalesRoom'");
				BindDept(ddlProduceDept, "cnvcDeptType<>'Corp'");
				BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");
				BindNameCode(ddlOrderState, "cnvcType='ORDERSTATE'");
				BindOper(ddlOrderOper, "");
				this.tblCustom.Visible = false;
				if(Request["OperFlag"] == null || Request["OrderSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				string strOperFlag = Request["OperFlag"].ToString();
				string strOrderSerialNo = Request["OrderSerialNo"].ToString();
				Disp(strOperFlag);
				//ViewState["OperFlag"] = strOperFlag;
				this.BindOrder(strOrderSerialNo);
				this.BindDetail(strOrderSerialNo);
//				if(Session["ProductList"] != null)
//				{
//					DataTable dtOrderDetail = (DataTable) Session["ProductList"];
//					this.DataGrid1.DataSource = dtOrderDetail;
//					this.DataGrid1.DataBind();
//				}
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
			this.ddlOrderType.SelectedIndexChanged += new System.EventHandler(this.ddlOrderType_SelectedIndexChanged);
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
			this.DataGrid2.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_DeleteCommand);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void Disp(string strOperFlag)
		{
			
			switch(strOperFlag)
			{
				case "Edit":
					lblOrderState.Visible = false;
					ddlOrderState.Visible = false;
					lblOrderDate.Visible = false;
					txtOrderDate.Visible = false;
					lblOrderOper.Visible = false;
					ddlOrderOper.Visible = false;
					txtOrderSerialNo.Enabled = false;
					this.lblTitle.Text = "订单编辑";
					break;
				case "Detail":
					this.ddlSalesRoom.Enabled = false;
					this.ddlProduceDept.Enabled = false;
					this.ddlOrderType.Enabled = false;
					this.txtOrderSerialNo.Enabled = false;
					this.txtShipDate.Enabled = false;
					this.ddlOrderState.Enabled = false;
					this.txtOrderDate.Enabled = false;
					this.ddlOrderOper.Enabled = false;
					this.btnModify.Visible = false;
					this.btnCancel.Visible = false;
					this.txtCustomName.Enabled = false;
					this.txtShipAddress.Enabled = false;
					this.txtLinkPhone.Enabled = false;
					this.txtArrivedDate.Enabled = false;
					this.lblTitle.Text = "订单查看";
					this.DataGrid2.Columns[6].Visible = false;
					this.DataGrid2.Columns[7].Visible = false;
					this.DataGrid1.Visible = false;
					this.txtOrderComments.Enabled = false;
					break;
			}
		}
		private void BindOrder(string strOrderSerialNo)
		{
			string strSql = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strSql);
			OrderBook order = new OrderBook(dtOrder);
			this.ddlOrderOper.Items.FindByValue(order.cnvcOperID).Selected = true;
			this.ddlOrderState.Items.FindByValue(order.cnvcOrderState).Selected = true;
			this.ddlOrderType.Items.FindByValue(order.cnvcOrderType).Selected = true;
			this.ddlProduceDept.Items.FindByValue(order.cnvcProduceDeptID).Selected = true;
			this.ddlSalesRoom.Items.FindByValue(order.cnvcOrderDeptID).Selected = true;

			if(order.cnvcOrderType == "WDO")
			{
				this.tblCustom.Visible = true;
				this.txtArrivedDate.Text = order.cndArrivedDate.ToString("yyyy-MM-dd hh:mm");
				this.txtLinkPhone.Text = order.cnvcLinkPhone;
				this.txtShipAddress.Text = order.cnvcShipAddress;
				this.txtCustomName.Text = order.cnvcCustomName;
				
			}
			this.txtOrderDate.Text = order.cndOrderDate.ToString("yyyy-MM-dd");
			this.txtOrderSerialNo.Text = order.cnnOrderSerialNo.ToString();
			
			this.txtShipDate.Text = order.cndShipDate.ToString("yyyy-MM-dd");
			
		}
		private void BindDetail(string strOrderSerialNo)
		{
			string strSql = "select * from tbOrderBookDetail where cnnOrderSerialNo="+strOrderSerialNo;
			DataTable dtOrderBookDetail = Helper.Query(strSql);

			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcinvname","tbInventory","cnvcinvcode","cnvcinvname","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnfretailprice","tbInventory","cnvcinvcode","cnfretailprice","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcproduceunitcode","tbInventory","cnvcinvcode","cnvcproduceunitcode","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcproduceunitcode","cnvccomunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

			dtOrderBookDetail.Columns.Add("cnnsum");
			foreach(DataRow dr in dtOrderBookDetail.Rows)
			{
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dr);
				double sum = Convert.ToDouble(dr["cnnordercount"].ToString())*Convert.ToDouble(dr["cnfretailprice"].ToString());
				dr["cnnsum"] = sum;
			}
			this.DataGrid2.DataSource = dtOrderBookDetail;
			this.DataGrid2.DataBind();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid2_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				OrderBookDetail detail = new OrderBookDetail();
				detail.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				detail.cnvcInvCode = e.Item.Cells[3].Text;
			
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "订单删除产品";

				OrderFacade order = new OrderFacade();
				order.DeleteDetail(detail,operLog);
				Popup("删除成功");
				BindDetail(txtOrderSerialNo.Text);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				string strCount = ((TextBox) e.Item.Cells[4].Controls[0]).Text;
				if(this.JudgeIsNull(strCount,"数量"))
				{
					return;
				}
				if(!this.JudgeIsNum(strCount,"数量"))
				{
					return;
				}
				if(decimal.Parse(strCount) <= 0)
				{
					Popup("数量必需大于零");
					return;
				}

				OrderBookDetail detail = new OrderBookDetail();
				detail.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				detail.cnvcInvCode = e.Item.Cells[0].Text;
				detail.cnnOrderCount = Convert.ToDecimal(strCount);

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "订单修改产品";

				OrderFacade order = new OrderFacade();
				order.UpdateDetail(detail,operLog);
				Popup("修改成功");
				this.DataGrid2.EditItemIndex = -1;
				BindDetail(txtOrderSerialNo.Text);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[7].Controls[0]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			} 
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindDetail(txtOrderSerialNo.Text);
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Add")
			{
				try
				{
					if(Session["ProductList"] == null)
					{
						Popup("请选择产品");
						return;
					}
					DataTable dtDetail = (DataTable) Session["ProductList"];
					if(dtDetail.Rows.Count == 0)
					{
						Popup("请选择订单里面没有的产品");
						return;
					}
					OrderFacade order = new OrderFacade();				
					string strOrderSerialNo = txtOrderSerialNo.Text;
					OperLog operLog = new OperLog();
					operLog.cnvcOperID = oper.strLoginID;
					operLog.cnvcDeptID = oper.strDeptID;
					operLog.cnvcOperType = "订单加产品";
					order.AddDetail(strOrderSerialNo, operLog, dtDetail);
					Session["ProductList"] = null;
					this.DataGrid1.DataSource = null;
					this.DataGrid1.DataBind();
					BindDetail(strOrderSerialNo);
					Popup("订单中加入产品成功");
				}
				catch(Exception ex)
				{
					Popup(ex.Message);
				}
			}
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			//返回
			this.Response.Redirect("wfmOrderQuery.aspx");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//
			this.BindOrder(txtOrderSerialNo.Text);
			this.BindDetail(txtOrderSerialNo.Text);
		}

		private void btnModify_Click(object sender, System.EventArgs e)
		{
			//修改
			try
			{
				OrderBook order = new OrderBook();
				order.cnvcOperID = oper.strLoginID;
				order.cnvcOrderType = ddlOrderType.SelectedValue;
				order.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				order.cnvcOrderDeptID = ddlSalesRoom.SelectedValue;

				if(order.cnvcOrderType == "WDO")
				{
					order.cndArrivedDate = DateTime.Parse(txtArrivedDate.Text);
					order.cnvcLinkPhone = txtLinkPhone.Text;
					order.cnvcShipAddress = txtShipAddress.Text;
					order.cnvcCustomName = txtCustomName.Text;
				
				}
				order.cnnOrderSerialNo = decimal.Parse(txtOrderSerialNo.Text);
				order.cndShipDate = DateTime.Parse(txtShipDate.Text);

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "修改订单";

				OrderFacade orderFacade = new OrderFacade();
				orderFacade.UpdateOrder(order,operLog);
				Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
