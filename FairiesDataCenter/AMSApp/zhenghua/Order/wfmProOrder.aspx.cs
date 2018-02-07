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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;
namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmProOrder 的摘要说明。
	/// </summary>
	public class wfmProOrder : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlSalesRoom;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlOrderType;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtShipDate;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtCustomName;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtShipAddress;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOrderComments;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtLinkPhone;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtArrivedDate;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.HtmlControls.HtmlTable tblOrder;
		protected System.Web.UI.HtmlControls.HtmlTable tblCustom;
		protected System.Web.UI.HtmlControls.HtmlTable tblDetial;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtOrderDate;
		protected System.Web.UI.HtmlControls.HtmlTable tblOper;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Button1.Attributes.Add("onclick","OpenQueryWin()");
			
			
			if(!this.IsPostBack)
			{				

				if(Request["OperFlag"] != null)
				{
					if(Request["OperFlag"].ToString() == "Add")
					{
						Session["ProductList"] = null;
						this.DataGrid1.DataSource = null;
						this.DataGrid1.DataBind();
					}
				}
				BindGrid();				
				//this.BindDept(ddlSalesRoom, "");//"cnvcDeptType = 'SalesRoom'");
				BindDept(ddlSalesRoom, "cnvcDeptType<>'Corp'");
				this.SetDDL(this.ddlSalesRoom,this.oper.strDeptID);
				if(this.oper.strDeptID !="CEN00")
				{
				
					this.ddlSalesRoom.Enabled = false;			
				}
//				CommCenter.CMSMStruct.LoginStruct ls1=(CommCenter.CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1 != null)
//				{
//					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
//					{
//						ListItem li = this.ddlSalesRoom.Items.FindByValue(ls1.strNewDeptID);
//						if(li != null)
//							li.Selected = true;
//						this.ddlSalesRoom.Enabled=false;
//					}
//
//				}
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");
				this.tblCustom.Visible = false;
				this.txtOrderComments.Text = "无特殊要求";



				if(Request["OperFlag"] != null && Request["OrderSerialNo"] != null)
				{
					string strOperFlag = Request["OperFlag"].ToString();
					string strOrderSerialNo = Request["OrderSerialNo"].ToString();
					this.BindOrder(strOrderSerialNo);
					this.BindDetail(strOrderSerialNo);		
					BindGrid();
				}
				this.txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

			}
			else
			{
				if(Session["flag"]!=null && Session["flag"].ToString() == "reload")
				{
					BindGrid();
					Session["flag"] = null;
				}
			}
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue== "WDOSELF")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
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
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
		private void BindOrder(string strOrderSerialNo)
		{
			string strSql = "select * from tbOrderBook where cnnOrderSerialNo=" + strOrderSerialNo;
			DataTable dtOrder = Helper.Query(strSql);
			OrderBook order = new OrderBook(dtOrder);
//			this.ddlOrderOper.Items.FindByValue(order.cnvcOperID).Selected = true;
//			this.ddlOrderState.Items.FindByValue(order.cnvcOrderState).Selected = true;
//			this.ddlOrderType.Items.FindByValue(order.cnvcOrderType).Selected = true;
//			this.ddlProduceDept.Items.FindByValue(order.cnvcProduceDeptID).Selected = true;
//			this.ddlSalesRoom.Items.FindByValue(order.cnvcOrderDeptID).Selected = true;

			this.SetDDL(this.ddlOrderType,order.cnvcOrderType);
			this.SetDDL(this.ddlProduceDept,order.cnvcProduceDeptID);
			this.SetDDL(this.ddlSalesRoom,order.cnvcOrderDeptID);
			if(order.cnvcOrderType == "WDO" || order.cnvcOrderType == "WDOSELF")
			{
				this.tblCustom.Visible = true;
				this.txtArrivedDate.Text = order.cndArrivedDate.ToString("yyyy-MM-dd hh:mm");
				this.txtLinkPhone.Text = order.cnvcLinkPhone;
				this.txtShipAddress.Text = order.cnvcShipAddress;
				this.txtCustomName.Text = order.cnvcCustomName;
				
			}
			//this.txtOrderDate.Text = order.cndOperDate.ToString("yyyy-MM-dd hh:mm");
			this.txtOrderSerialNo.Text = order.cnnOrderSerialNo.ToString();
			
			this.txtShipDate.Text = order.cndShipDate.ToString("yyyy-MM-dd");
			this.txtOrderDate.Text = order.cndOrderDate.ToString("yyyy-MM-dd");
			
		}
		private void BindDetail(string strOrderSerialNo)
		{
			string strSql = "select * from tbOrderBookDetail where cnnOrderSerialNo="+strOrderSerialNo;
			DataTable dtOrderBookDetail = Helper.Query(strSql);

			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcinvname","tbInventory","cnvcinvcode","cnvcinvname","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnfretailprice","tbInventory","cnvcinvcode","cnfretailprice","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcProduceunitcode","tbInventory","cnvcinvcode","cnvcProduceunitcode","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcProduceunitcode","cnvccomunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

			dtOrderBookDetail.Columns.Add("cnnsum");
			foreach(DataRow dr in dtOrderBookDetail.Rows)
			{
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dr);
				double sum = Convert.ToDouble(dr["cnnordercount"].ToString())*Convert.ToDouble(dr["cnfretailprice"].ToString());
				dr["cnnsum"] = sum;
			}
			Session["ProductList"] = dtOrderBookDetail;
		}
		private void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
			
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			//下单
			try
			{
				if(txtShipDate.Text.Length == 0)
				{
					throw new Exception("请输入发货日期");					
				}
				if(this.JudgeIsNull(this.txtOrderDate.Text))
				{
					this.Popup("请输入订单日期");
					return;
				}
				OrderBook orderBook = new OrderBook();
				orderBook.cnvcOperID = oper.strLoginID;
				if(this.txtOrderSerialNo.Text != "")
				orderBook.cnnOrderSerialNo = Convert.ToDecimal(this.txtOrderSerialNo.Text);
				if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
				{
					if(txtArrivedDate.Text.Length == 0)
					{
						throw new Exception("请输入要求到货时间");						
					}
					orderBook.cndArrivedDate = DateTime.Parse(txtArrivedDate.Text);
					orderBook.cnvcCustomName = txtCustomName.Text;
					orderBook.cnvcLinkPhone = txtLinkPhone.Text;
					orderBook.cnvcShipAddress = txtShipAddress.Text;
				}
				if(ddlOrderType.SelectedValue == "SELFPRODUCE" || ddlOrderType.SelectedValue=="WDOSELF")
				{
					if(!ddlSalesRoom.SelectedValue.Equals(ddlProduceDept.SelectedValue))
						throw new Exception(ddlOrderType.SelectedItem.Text+"门市必需和生产单位一致！");
				}
				orderBook.cndOrderDate = Convert.ToDateTime(txtOrderDate.Text);
				orderBook.cndShipDate = DateTime.Parse(txtShipDate.Text);
				orderBook.cnvcOrderDeptID = ddlSalesRoom.SelectedValue;
				orderBook.cnvcOrderState = "0";
				orderBook.cnvcOrderType = ddlOrderType.SelectedValue;
				orderBook.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				orderBook.cnvcOrderComments = txtOrderComments.Text;
				DataTable dtDetail = (DataTable) Session["ProductList"];
				if(dtDetail == null)
				{
					throw new Exception("请查询产品，添加产品清单！");
				}
				Hashtable htDetail = new Hashtable();
				foreach(DataRow dr in dtDetail.Rows)
				{
					string strProductCode = dr["cnvcinvcode"].ToString();
					string strProductName = dr["cnvcinvname"].ToString();
					if(htDetail.ContainsKey(strProductCode))
					{
						throw new Exception(strProductCode+"-"+strProductName+"重复，请首先通过产品查询进行修改");
					}
					else
					{
						htDetail.Add(strProductCode,strProductName);
					}
				}

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "下单";

				OrderFacade order = new OrderFacade();
				order.AddOrder(orderBook, dtDetail, operLog);
				Session["ProductList"] = null;
				btnCancel_Click(null, null);
				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
				Popup("下单成功","wfmOrderQuery.aspx");		
				//this.Button2_Click(null,null);
				//this.Response.Redirect("wfmOrderQuery.aspx",false);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//取消
			this.txtShipDate.Text = "";
			this.txtCustomName.Text = "";
			this.txtShipAddress.Text = "";
			this.txtLinkPhone.Text = "";
			this.txtArrivedDate.Text = "";
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void BindGrid()
		{			
			if(Session["ProductList"] != null)
			{
				DataTable dtOrderBookDetail = (DataTable) (Session["ProductList"]);
				this.DataGrid1.DataSource = dtOrderBookDetail;
				this.DataGrid1.DataBind();
			}
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
			this.BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["ProductList"] == null)
				{
					Popup("请首先选择产品");
					return;
				}
				string strProductCode = e.Item.Cells[0].Text;				
				string strPrice = e.Item.Cells[3].Text;
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
				decimal dSum = decimal.Parse(strPrice)*decimal.Parse(strCount);
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcinvcode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					drOrderBookDetail[0]["cnnOrderCount"] = strCount;
					drOrderBookDetail[0]["cnnSum"] = dSum.ToString();
				}
				 Session["ProductList"] = dtOrderBookDetail;
				this.DataGrid1.EditItemIndex = -1;
				this.BindGrid();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[7].Controls[0]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			} 
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				string strProductCode = e.Item.Cells[0].Text;
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcinvcode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					dtOrderBookDetail.Rows.Remove(drOrderBookDetail[0]);
				}
				this.BindGrid();
			}
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//返回
			this.Response.Redirect("wfmOrderQuery.aspx");
		}

	}
}
