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

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmOrderQuery 的摘要说明。
	/// </summary>
	public class wfmOrderQuery : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox TextBox3;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList ddlSalesRoom;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList ddlOrderType;
		protected System.Web.UI.WebControls.TextBox txtOrderSerialNo;
		protected System.Web.UI.WebControls.DropDownList ddlOrderOper;
		protected System.Web.UI.WebControls.TextBox txtOrderDate;
		protected System.Web.UI.WebControls.TextBox txtShipDate;
		protected System.Web.UI.WebControls.DropDownList ddlOrderState;
		protected System.Web.UI.WebControls.TextBox txtShipAddress;
		protected System.Web.UI.WebControls.TextBox txtLinkPhone;
		protected System.Web.UI.WebControls.TextBox txtArrivedDate;
		protected System.Web.UI.HtmlControls.HtmlTable tblCustom;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.TextBox txtCustomName;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.Button btnSumPrint;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtOrderDateEnd;
		protected System.Web.UI.WebControls.TextBox txtShipDateEnd;
		protected System.Web.UI.WebControls.Button btnQuery;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			
			if(!this.IsPostBack)
			{
				this.tblCustom.Visible = false;
				this.txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtShipDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtOrderDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtShipDateEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
				
				BindDDL();
				BindGrid1();
				//Session["Login"]=null;
//				string str1 = Helper.GetChineseSpell("草莓月饼50g");
//				string str2 = Helper.GetPinyinCode("草莓月饼50g");
//				string str3 = Helper.GetFirstPYLetter("草莓月饼50g");
//				string str4 = Helper.GetAllPYLetters("草莓月饼50g");
//				string str5 = Helper.GetChineseSpell("草莓月饼50g");
//				string str6 = "";
				
			}
		}

		private void BindDDL()
		{
			BindNameCode(this.ddlOrderState,"cnvcType='ORDERSTATE'");
			BindDept(ddlProduceDept, "cnvcDeptType<>'Corp'");
			BindOper(ddlOrderOper, "");
			BindDept(ddlSalesRoom, "cnvcDeptType<>'Corp'");//"cnvcDeptType='SalesRoom'");
			BindNameCode(ddlOrderType, "cnvcType='ORDERTYPE'");

			ListItem li = new ListItem("所有", "%");
			this.ddlOrderState.Items.Add(li);
			this.ddlProduceDept.Items.Add(li);
			this.ddlOrderType.Items.Add(li);
			this.ddlSalesRoom.Items.Add(li);
			this.ddlOrderOper.Items.Add(li);

//			this.ddlSalesRoom.SelectedItem.Selected = false;
//			this.ddlOrderOper.SelectedItem.Selected = false;
//			this.ddlOrderState.SelectedItem.Selected = false;		
//			this.ddlProduceDept.SelectedItem.Selected = false;
//			this.ddlOrderType.SelectedItem.Selected = false;
			this.SetDDL(this.ddlSalesRoom,this.oper.strDeptID);
			this.SetDDL(this.ddlOrderOper,this.oper.strLoginID);
			this.SetDDL(this.ddlOrderState,"0");
//			this.SetDDL(this.ddlProduceDept,"%");
//			this.SetDDL(this.ddlOrderType,"%");
			if(this.oper.strDeptID !="CEN00")
			{
				
				this.ddlSalesRoom.Enabled = false;
				this.ddlOrderOper.Enabled = false;				
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
			this.ddlOrderType.SelectedIndexChanged += new System.EventHandler(this.ddlOrderType_SelectedIndexChanged_1);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnSumPrint.Click += new System.EventHandler(this.btnSumPrint_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		private void BindGrid1()
		{
			string strSql =
				" select cnnOrderSerialNo,cnvcOrderState,cndShipDate,cndOrderDate,cnvcOperID,cnvcOrderDeptID,cnvcProduceDeptID,cnvcOrderType,cnvcOrderState "
				+ " from tbOrderBook "
				+ " where 1=1 ";
			strSql += " and cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			//strSql += " and cnvcOperID like '"+ddlOrderOper.SelectedValue+"'";
			
			strSql += " and cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strSql += " and cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strSql += " and cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strSql += " and cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndOrderDate,120)>='" + txtOrderDate.Text + "'";
			}
			if(txtOrderDateEnd.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndOrderDate,120)<='" + txtOrderDateEnd.Text + "'";
			}
			if(txtShipDate.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndShipDate,120)>='" + txtShipDate.Text + "'";
			}
			if(txtShipDateEnd.Text.Length > 0)
			{
				strSql += " and convert(char(10),cndShipDate,120)<='" + txtShipDateEnd.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO" ||ddlOrderType.SelectedValue == "WDOSELF")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strSql += " and cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strSql += " and cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strSql += " and cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strSql += " and cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			DataTable dtOrderBook = Helper.Query(strSql);
			this.DataTableConvert(dtOrderBook, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataTableConvert(dtOrderBook, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			this.DataTableConvert(dtOrderBook, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
			this.DataTableConvert(dtOrderBook, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtOrderBook, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
			this.DataGrid1.DataSource = dtOrderBook;
			this.DataGrid1.DataBind();

			string strReport = "select a.*,b.cnvcInvCode,c.cnvcInvName,b.cnnOrderCount,c.cnvcComUnitCode,"
							+"c.cnfRetailPrice from tbOrderBook a "
							+"left outer join tbOrderBookDetail b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
				+" left outer join tbInventory c on b.cnvcInvCode=c.cnvcInvCode "
							+ " where 1=1 ";
			//strReport += " and a.cnvcOperID like '"+ddlOrderOper.SelectedValue+"'";
			strReport += " and a.cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			strReport += " and a.cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strReport += " and a.cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strReport += " and a.cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strReport += " and a.cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strReport += " and convert(char(10),a.cndOrderDate,120)>='" + txtOrderDate.Text + "'";
			}
//			if(this.txtOrderDateEnd.Text.Length>0)
//			{
//				strReport += " and convert(char(10),a.cndOrderDate,120)<='" + txtOrderDateEnd.Text + "'";
//			}
			if(txtShipDate.Text.Length > 0)
			{
				strReport += " and convert(char(10),a.cndShipDate,120)>='" + txtShipDate.Text + "'";
			}
			if(txtOrderDateEnd.Text.Length > 0)
			{
				strReport += " and convert(char(10),cndOrderDate,120)<='" + txtOrderDateEnd.Text + "'";
			}
			
			if(txtShipDateEnd.Text.Length > 0)
			{
				strReport += " and convert(char(10),cndShipDate,120)<='" + txtShipDateEnd.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strReport += " and a.cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strReport += " and a.cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strReport += " and a.cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strReport += " and a.cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			Session["OrderReport"] = strReport;

			string strSumReport = "select a.cnvcOrderDeptID,b.cnvcinvcode,c.cnvcinvname,c.cnvccomunitcode,c.cnfretailprice,"
				+ " sum(b.cnnOrderCount) as cnnCount  from tbOrderBook a "
				+"left outer join tbOrderBookDetail b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
				+" left outer join tbinventory c on b.cnvcinvcode=c.cnvcinvcode "
				+ " where 1=1 ";
			//strSumReport += " and a.cnvcOperID like '"+ddlOrderOper.SelectedValue+"'";
			strSumReport += " and a.cnvcOrderState like '" + ddlOrderState.SelectedValue + "'";
			strSumReport += " and a.cnvcOrderType like '" + ddlOrderType.SelectedValue + "'";
			strSumReport += " and a.cnvcProduceDeptID like '" + ddlProduceDept.SelectedValue + "'";
			strSumReport += " and a.cnvcOrderDeptID like '" + ddlSalesRoom.SelectedValue + "'";
			if(txtOrderSerialNo.Text.Length > 0)
			{
				strSumReport += " and a.cnnOrderSerialNo = " + txtOrderSerialNo.Text;
			}
			if(txtOrderDate.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),a.cndOrderDate,120)>='" + txtOrderDate.Text + "'";
			}
			if(txtShipDate.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),a.cndShipDate,120)>='" + txtShipDate.Text + "'";
			}
			if(txtOrderDateEnd.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),cndOrderDate,120)<='" + txtOrderDateEnd.Text + "'";
			}
			
			if(txtShipDateEnd.Text.Length > 0)
			{
				strSumReport += " and convert(char(10),cndShipDate,120)<='" + txtShipDateEnd.Text + "'";
			}
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				if(txtCustomName.Text.Length > 0)
				{
					strSumReport += " and a.cnvcCustomName like '%" + txtCustomName.Text + "%'";
				}
				if(txtShipAddress.Text.Length > 0)
				{
					strSumReport += " and a.cnvcShipAddress like '%" + txtShipAddress.Text + "%'";
				}
				if(txtLinkPhone.Text.Length > 0)
				{
					strSumReport += " and a.cnvcLinkPhone like '%" + txtLinkPhone.Text + "%'";
				}
				if(txtArrivedDate.Text.Length > 0)
				{
					strSumReport += " and a.cndArrivedDate <= '" + txtArrivedDate.Text + "'";
				}
			}
			strSumReport += " group by a.cnvcOrderDeptID,b.cnvcinvcode,c.cnvcinvname,c.cnvccomunitcode,c.cnfretailprice";
			Session["OrderSumReport"] = strSumReport;
		}
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid1();
		}



		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			BindGrid1();
		}

		private void ddlOrderType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				if(e.Item.Cells[10].Text != "0")
				{
					HyperLink btnEdit = (HyperLink)(e.Item.Cells[9].Controls[0]);
					btnEdit.Attributes.Add("onClick","JavaScript:alert('不可编辑');return false;");
				}									
			} 
		}

		private void ddlOrderType_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			if(ddlOrderType.SelectedValue == "WDO" || ddlOrderType.SelectedValue == "WDOSELF")
			{
				this.tblCustom.Visible = true;
			}
			else
			{
				this.tblCustom.Visible = false;
			}
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			txtOrderSerialNo.Text = "";
			this.txtArrivedDate.Text = "";
			this.txtCustomName.Text = "";
			this.txtLinkPhone.Text = "";
			this.txtOrderDate.Text = "";
			this.txtOrderSerialNo.Text = "";
			this.txtShipAddress.Text = "";
			this.txtShipDate.Text = "";
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			if(Session["OrderReport"] == null)
			{
				Popup("请先查询结果");
				return;
			}
			this.Response.Redirect("wfmOrderReport.aspx?OrderType="+this.ddlOrderType.SelectedItem.Text);
		}

		private void btnSumPrint_Click(object sender, System.EventArgs e)
		{
			if(Session["OrderSumReport"] == null)
			{
				Popup("请先查询结果");
				return;
			}
			this.Response.Redirect("wfmOrderSumReport.aspx");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//添加生产订单
			this.Response.Redirect("wfmProOrder.aspx?OperFlag=Add");
		}
//
//		private void ddlOrderState_DataBinding(object sender, EventArgs e)
//		{
//			//
//			//this.SetDDL(this.ddlOrderState,"0");
//		}
//
//		private void ddlOrderState_Load(object sender, EventArgs e)
//		{
//
//			//this.SetDDL(sender as DropDownList,"0");
//		}
//
//		private void ddlOrderState_PreRender(object sender, EventArgs e)
//		{
//			//this.SetDDL(sender as DropDownList,"0");
//		}
	}
}
