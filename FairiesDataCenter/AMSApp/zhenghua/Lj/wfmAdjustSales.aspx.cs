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
namespace AMSApp.zhenghua.Lj
{
	/// <summary>
	/// wfmAdjustSales 的摘要说明。
	/// </summary>
	public class wfmAdjustSales : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtBeginDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
				this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				if(Session["tbSalesSerial_Query"] != null)
				{
					Session.Remove("tbSalesSerial_Query");
				}
				BindGrid();
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.MyDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_ItemCommand);
			this.MyDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.MyDataGrid_PageIndexChanged);
		}
		#endregion

		
		private void BindGrid()
		{
			DataTable dtProduct = GetProduct();
			this.MyDataGrid.DataSource = dtProduct;			
			this.MyDataGrid.DataBind();
		}

		public DataTable GetProduct()
		{
			DataTable dtProduct = null;			
			if(Session["tbSalesSerial_Query"] != null)
			{
				dtProduct = (DataTable) Session["tbSalesSerial_Query"];
			}
			else
			{
				dtProduct = Helper.Query("select cnnSerialNo,cnvcName,cnvcCode,cnnCount+cnnAddCount-cnnReduceCount as cnnSum,cnnCount,cnnAddCount,cnnReduceCount from tbSalesSerial where 1<>1");
				Session["tbSalesSerial_Query"] = dtProduct;
			}
			return dtProduct;
		}
		private void SetProduct(DataTable dtProduct)
		{
			Session["tbSalesSerial_Query"] = dtProduct;
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//查询
			string strSql = "select cnnSerialNo,cnvcName,cnvcCode,cnnCount+cnnAddCount-cnnReduceCount as cnnSum,cnnCount,cnnAddCount,cnnReduceCount from tbSalesSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
				+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
				+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"' order by cnnSerialNo,cnvcCode";

			DataTable dtOut = Helper.Query(strSql);

			SetProduct(dtOut);
			this.MyDataGrid.CurrentPageIndex = 0;
			BindGrid();
		}



		private void MyDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.MyDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void MyDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strSerialNo;// = e.Item.Cells[0].Text;
			string strGoodsID;// = e.Item.Cells[1].Text;
			string strCount;// = e.Item.Cells[4].Text;
			SalesSerial ps;
			OperLog ol;
			ProductFacade pf;
			DataRow[] drs;
			DataTable dtSales = GetProduct();
			
			switch(e.CommandName)
			{
				case "AdjustAdd":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					strCount = e.Item.Cells[4].Text;
					string strAddCount = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
					
					if(strAddCount=="")
					{
						this.Popup("请输入调增量！");
						return;
					}
					if(!this.JudgeIsNum(strAddCount))
					{
						this.Popup("请输入数字！");
						return;
					}
					if(Convert.ToInt32(strAddCount)<=0)
					{
						this.Popup("调增量必须大于零！");
						return;
					}

					drs = dtSales.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						drs[0]["cnnAddCount"] = strAddCount;
						drs[0]["cnnSum"] = Convert.ToInt32(strCount)+Convert.ToInt32(strAddCount);
					}
					ps = new SalesSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;
					ps.cnnAddCount = Convert.ToInt32(strAddCount);

					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "销售产品调增";

					pf = new ProductFacade();
					pf.AdjustSalesSerial_Add(ps,ol);


					SetProduct(dtSales);
					this.Popup("调增成功！");
					BindGrid();
					break;
				case "AdjustReduce":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					strCount = e.Item.Cells[4].Text;
					string strReduceCount = ((TextBox)e.Item.Cells[6].Controls[1]).Text;

					if(strReduceCount=="")
					{
						this.Popup("请输入调减量！");
						return;
					}
					if(!this.JudgeIsNum(strReduceCount))
					{
						this.Popup("请输入数字！");
						return;
					}
					if(Convert.ToInt32(strReduceCount)<=0)
					{
						this.Popup("调减量必须大于零！");
						return;
					}

					if(Convert.ToInt32(strCount)-Convert.ToInt32(strReduceCount)<=0)
					{
						this.Popup("调减量必须小于销售量！");
						return;
					}
					drs = dtSales.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						drs[0]["cnnReduceCount"] = strReduceCount;
						drs[0]["cnnSum"] = Convert.ToInt32(strCount)-Convert.ToInt32(strReduceCount);
					}

					ps = new SalesSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;
					ps.cnnReduceCount = Convert.ToInt32(strReduceCount);


					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "销售产品调减";

					pf = new ProductFacade();
					pf.AdjustSalesSerial_Reduce(ps,ol);

					SetProduct(dtSales);
					this.Popup("调减成功！");
					BindGrid();
					break;
				case "Delete":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					//strCount = e.Item.Cells[4].Text;
					drs = dtSales.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						dtSales.Rows.Remove(drs[0]);
						//drs[0]["cnnCount"] = strCount;
					}

					ps = new SalesSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;

					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "销售产品删除";

					pf = new ProductFacade();
					pf.AdjustSalesSerial_Delete(ps,ol);

					SetProduct(dtSales);
					this.Popup("销售产品删除成功！");

					if((this.MyDataGrid.CurrentPageIndex==MyDataGrid.PageCount-1)&&MyDataGrid.Items.Count==1)
					{
						if(MyDataGrid.CurrentPageIndex-1>1)
						{
							MyDataGrid.CurrentPageIndex = MyDataGrid.CurrentPageIndex-1;
						}
						else
						{
							MyDataGrid.CurrentPageIndex = 0;
						}
            
					} 

					BindGrid();
					break;
			}
			
		}
	}
}
