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
	/// wfmProductQuery 的摘要说明。
	/// </summary>
	public class wfmProductQuery : wfmBase
	{
		#region
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.Button btnAddList;
		protected System.Web.UI.WebControls.CheckBox chkSame;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtPercent;
		protected System.Web.UI.WebControls.Button btnPercent;
		protected System.Web.UI.WebControls.Button btnOrderDetail;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button btnBatchAddList;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Response.Expires = -1;
			this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
			this.Response.CacheControl = "no-cache";
			this.Button1.Attributes.Add("onclick","window.returnValue='cccc';window.close()");
			Session["flag"] = "reload";//刷新订单页面
			//Session["Login"]=null;
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
			this.chkSame.CheckedChanged += new System.EventHandler(this.chkSame_CheckedChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnPercent.Click += new System.EventHandler(this.btnPercent_Click);
			this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
			this.btnBatchAddList.Click += new System.EventHandler(this.btnBatchAddList_Click);
			this.btnOrderDetail.Click += new System.EventHandler(this.btnOrderDetail_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_ItemCommand);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.txtProductCode.Text = "";
			this.txtProductName.Text = "";
			this.txtCount.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DataGrid2.DataSource = null;
			this.DataGrid2.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = 0;
			BindProduct();
			//
		}

		private void BindProduct()
		{			
			string strSql = "select cnvcinvcode,cnvcinvname,cnfretailprice,cnvcProduceUnitCode from tbinventory "			
+" where cnbProductBill=1  "
//+" and CONVERT(char(10),cndSDate,121) <= CONVERT(char(10),getdate(),121) "
//+" AND CONVERT(char(10),isnull(cndEDate,getdate()),121)>=CONVERT(char(10),getdate(),121) "
+" and CONVERT(char(10),isnull(cndSDate,CONVERT(datetime,'9999-12-31')),121)<=CONVERT(char(10),getdate(),121) "
+" AND CONVERT(char(10),isnull(cndEDate,getdate()),121)>=CONVERT(char(10),getdate(),121) "

+" and cnvcinvcode like '%"+this.txtProductCode.Text+"%' and cnvcinvname like '%"+this.txtProductName.Text+"%'";
			DataTable dtProduct = Helper.Query(strSql);
			if(Session["ProductList"] != null)
			{
				DataTable dtProductList = (DataTable) Session["ProductList"];
				foreach(DataRow drProductList in dtProductList.Rows)
				{
					OrderBookDetail detail = new OrderBookDetail(drProductList);
					DataRow[] dr = dtProduct.Select("cnvcinvcode='" + detail.cnvcInvCode + "'");
					if(dr.Length > 0)
					{
						dtProduct.Rows.Remove(dr[0]);
					}
				}
			}
			this.DataTableConvert(dtProduct,"cnvcProduceUnitCode","cnvccomunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataGrid1.DataSource = dtProduct;
			this.DataGrid1.DataBind();
			this.DataGrid1.Visible = true;
			this.DataGrid2.Visible = false;
		}
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindProduct();
		}
		private void GetProductListTable(DataTable dtOrderBookDetail)
		{
			DataTable dtProductList = null;
			if(Session["ProductList"] == null)
			{
				OrderBookDetail detail = new OrderBookDetail();
				dtProductList = detail.ToTable().Clone();
				dtProductList.Columns.Add("cnvcinvname");
				dtProductList.Columns.Add("cnfretailprice");
				dtProductList.Columns.Add("cnvcProduceUnitCode");
				dtProductList.Columns.Add("cnvccomunitname");
				dtProductList.Columns.Add("cnnsum");
			}
			else
			{
				dtProductList = (DataTable) Session["ProductList"]; 
			}
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcinvname","tbInventory","cnvcinvcode","cnvcinvname","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnfretailprice","tbInventory","cnvcinvcode","cnfretailprice","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcProduceUnitCode","tbInventory","cnvcinvcode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtOrderBookDetail,"cnvcProduceUnitCode","cnvccomunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			object[] oArray = new object[dtOrderBookDetail.Columns.Count];
			dtOrderBookDetail.Rows[0].ItemArray.CopyTo(oArray, 0);
			dtProductList.Rows.Add(oArray);
			foreach(DataRow dr in dtProductList.Rows)
			{
				//Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dr);
				double sum = Convert.ToDouble(dr["cnnordercount"].ToString())*Convert.ToDouble(dr["cnfretailprice"].ToString());
				dr["cnnsum"] = sum;
			}
			Session["ProductList"] = dtProductList;
		}
		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "putin")
			{
				//放入清单
				string strCount = ((TextBox) e.Item.Cells[4].Controls[1]).Text;

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

				OrderBookDetail productList = new OrderBookDetail();
				productList.cnnOrderCount = decimal.Parse(((TextBox) e.Item.Cells[4].Controls[1]).Text);
				productList.cnvcInvCode = e.Item.Cells[0].Text;
				GetProductListTable(productList.ToTable());
				if((DataGrid1.CurrentPageIndex==DataGrid1.PageCount-1)&&DataGrid1.Items.Count==1)
				{
					if(DataGrid1.CurrentPageIndex-1>1)
					{
						DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex-1;
					}
					else
					{
						DataGrid1.CurrentPageIndex = 0;
					}
            
				} 
				BindProduct();
			}
		}

		private void btnAddList_Click(object sender, System.EventArgs e)
		{
			if(this.JudgeIsNull(txtCount.Text,"数量"))
			{
				return;
			}
			if(!this.JudgeIsNum(txtCount.Text,"数量"))
			{
				return;
			}
			if(decimal.Parse(txtCount.Text) <= 0)
			{
				Popup("数量必需大于零");
				return;
			}
			if(this.DataGrid1.Items.Count > 0)
			{
				//放入清单
				foreach(DataGridItem dgi in this.DataGrid1.Items)
				{
					OrderBookDetail productList = new OrderBookDetail();
					productList.cnnOrderCount = decimal.Parse(txtCount.Text);
					productList.cnvcInvCode = dgi.Cells[0].Text;
					GetProductListTable(productList.ToTable());
				}
				BindProduct();
			}
			BindGrid();
			this.DataGrid1.Visible = false;
			this.DataGrid2.Visible = true;
		}

		private void chkSame_CheckedChanged(object sender, System.EventArgs e)
		{
			//使用同期数据
			if(chkSame.Checked)
			{
				string strOrderBook = "select top 1 cnnOrderSerialNo from tbOrderBook where cnvcOperID='"+this.oper.strLoginID+"' and  convert(char(10),cndOrderDate,120) = '" +
				                      DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + "'";
//				string strOrderBook = "select top 1 cnnOrderSerialNo from tbOrderBook where cnvcOperID='"+this.oper.strLoginID+"' and  convert(char(10),cndOrderDate,120) = '" +
//					DateTime.Now.ToString("yyyy-MM-dd") + "'";
				DataTable dtOrderBook = Helper.Query(strOrderBook);
				if(dtOrderBook.Rows.Count > 0)
				{
					string strOrderSerialNo = dtOrderBook.Rows[0][0].ToString();
					if(strOrderSerialNo != "")
					{
						string strOrderBookDetail = "select * from tbOrderBookDetail where cnnOrderSerialNo=" + strOrderSerialNo;
						DataTable dtOrderBookDetail = Helper.Query(strOrderBookDetail);
						this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcinvname","tbInventory","cnvcinvcode","cnvcinvname","");
						this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnfretailprice","tbInventory","cnvcinvcode","cnfretailprice","");
						this.DataTableConvert(dtOrderBookDetail,"cnvcinvcode","cnvcProduceUnitCode","tbInventory","cnvcinvcode","cnvcProduceUnitCode","");
						this.DataTableConvert(dtOrderBookDetail,"cnvcProduceUnitCode","cnvccomunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
						dtOrderBookDetail.Columns.Add("cnnSum");
						//DataTable dtProduct = Helper.Query(strSql);
						if(Session["ProductList"] != null)
						{
							DataTable dtProductList = (DataTable) Session["ProductList"];
							foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
							{
								OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
								DataRow[] dr = dtProductList.Select("cnvcinvcode='" + detail.cnvcInvCode + "'");
								if(dr.Length == 0)
								{
									double sum = Convert.ToDouble(drOrderBookDetail["cnnordercount"].ToString())*Convert.ToDouble(drOrderBookDetail["cnfretailprice"].ToString());
									drOrderBookDetail["cnnsum"] = sum;

									object[] oArray = new object[dtProductList.Columns.Count];
									drOrderBookDetail.ItemArray.CopyTo(oArray, 0);
									dtProductList.Rows.Add(oArray);
								}
							}		
							Session["ProductList"] = dtProductList;
							//GetProductListTable(dtProductList);
						}
						else
						{
							foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
							{
								OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
								
								double sum = Convert.ToDouble(drOrderBookDetail["cnnordercount"].ToString())*Convert.ToDouble(drOrderBookDetail["cnfretailprice"].ToString());
								drOrderBookDetail["cnnsum"] = sum;					
							}		
							Session["ProductList"] = dtOrderBookDetail;
						}
						btnOrderDetail_Click(null,null);
					}
				}
			}
		}

		private void btnPercent_Click(object sender, System.EventArgs e)
		{
			if(this.JudgeIsNull(txtPercent.Text,"百分比"))
			{
				return;
			}
			if(!this.JudgeIsNum(txtPercent.Text,"百分比"))
			{
				return;
			}
			if(Session["ProductList"] != null)
			{
				DataTable dtProductList = (DataTable) Session["ProductList"];
				foreach(DataRow drProductList in dtProductList.Rows)
				{
					
					OrderBookDetail detail = new OrderBookDetail(drProductList);
					detail.cnnOrderCount = Convert.ToDecimal(Math.Ceiling(Convert.ToDouble(detail.cnnOrderCount)*double.Parse(txtPercent.Text)/100));
					drProductList["cnnOrderCount"] = detail.cnnOrderCount;
				}
				Session["ProductList"] = dtProductList;
			}
			BindGrid();
			this.DataGrid1.Visible = false;
			this.DataGrid2.Visible = true;

		}

		private void BindGrid()
		{			
			if(Session["ProductList"] != null)
			{
				DataTable dtOrderBookDetail = (DataTable) (Session["ProductList"]);
				this.DataGrid2.DataSource = dtOrderBookDetail;
				this.DataGrid2.DataBind();
			}
			else
			{
				Popup("未选择产品，请通过产品查询选择产品");
			}
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			this.BindGrid();
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["ProductList"] == null)
				{
					Popup("请首先选择产品");
					return;
				}
				string strProductCode = e.Item.Cells[0].Text;			
				string strCount = ((TextBox) e.Item.Cells[4].Controls[0]).Text;
				string strprice = e.Item.Cells[3].Text;
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcinvcode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					drOrderBookDetail[0]["cnnOrderCount"] = strCount;
					drOrderBookDetail[0]["cnnsum"] = Convert.ToDouble(strCount)*Convert.ToDouble(strprice);
				}
				this.DataGrid2.EditItemIndex = -1;
				this.BindGrid();
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

		private void DataGrid2_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
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

		private void btnOrderDetail_Click(object sender, System.EventArgs e)
		{
			BindGrid();
			this.DataGrid1.Visible = false;
			this.DataGrid2.Visible = true;
		}

		private void btnBatchAddList_Click(object sender, System.EventArgs e)
		{
			//批量放入清单
			try
			{											
				if(this.DataGrid1.Items.Count > 0)
				{
					//放入清单

					foreach(DataGridItem dgi in this.DataGrid1.Items)
					{
						string strCount = ((TextBox) dgi.Cells[4].Controls[1]).Text;
						if(this.JudgeIsNull(strCount))
						{
							continue;
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
						OrderBookDetail productList = new OrderBookDetail();
						productList.cnnOrderCount = decimal.Parse(strCount);
						productList.cnvcInvCode = dgi.Cells[0].Text;
						this.GetProductListTable(productList.ToTable());
					}
					BindProduct();
				}
				if((DataGrid1.CurrentPageIndex==DataGrid1.PageCount-1)&&DataGrid1.Items.Count==1)
				{
					if(DataGrid1.CurrentPageIndex-1>1)
					{
						DataGrid1.CurrentPageIndex = DataGrid1.CurrentPageIndex-1;
					}
					else
					{
						DataGrid1.CurrentPageIndex = 0;
					}
            
				} 
				BindGrid();
				this.DataGrid1.Visible = false;
				this.DataGrid2.Visible = true;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//Session["flag"] = "reload";
		}
	}
}
