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
	/// wfmAdjustProductLost 的摘要说明。
	/// </summary>
	public class wfmAdjustProductLost : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.TextBox txtBeginDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.Button btnQuery;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				//this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
//				this.BindDept(ddlDept,"cnvcDeptType<>'Corp'");
//				ListItem li = new ListItem("所有","%");
//				this.ddlDept.Items.Insert(0,li);
				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("所有", "%");
				this.ddlDept.Items.Add(li);
				this.SetDDL(this.ddlDept,this.oper.strDeptID);
				if(this.oper.strDeptID !="CEN00")
				{				
					this.ddlDept.Enabled = false;		
				}
				this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				if(Session["tbProductLostSerial_Query"] != null)
				{
					Session.Remove("tbProductLostSerial_Query");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.MyDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_ItemCommand);
			this.MyDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.MyDataGrid_PageIndexChanged);
			this.MyDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.MyDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

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
			if(Session["tbProductLostSerial_Query"] != null)
			{
				dtProduct = (DataTable) Session["tbProductLostSerial_Query"];
			}
			else
			{
				dtProduct = Helper.Query("select cnnLostSerialNo,'' as cnvcInvName,cnvcInvCode,cnnLostCount+cnnAddCount-cnnReduceCount as cnnSum,cnnLostCount,cnnAddCount,cnnReduceCount,convert(char(10),cndLostDate,121) as cndLostDate from tbLostSerial where 1<>1");
				Session["tbProductLostSerial_Query"] = dtProduct;
			}
			return dtProduct;
		}
		private void SetProduct(DataTable dtProduct)
		{
			Session["tbProductLostSerial_Query"] = dtProduct;
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//查询
			string strSql = "select cnnLostSerialNo,'' as cnvcInvName,cnvcInvCode,cnnLostCount+cnnAddCount-cnnReduceCount as cnnSum,cnnLostCount,cnnAddCount,cnnReduceCount,convert(char(10),cndLostDate,121) as cndLostDate from tbLostSerial where cnvcLostType='0' and convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
				+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"'"
				+ " and cnvcDeptID like '"+ddlDept.SelectedValue+"' order by cnnLostSerialNo,cnvcInvCode";
			
			DataTable dtOut = Helper.Query(strSql);
			this.DataTableConvert(dtOut,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
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
			LostSerial ps;
			OperLog ol;
			ProductFacade pf;
			DataRow[] drs;
			DataTable dtProduct = GetProduct();
			
			if (e.CommandName=="Delete")
			{
				strSerialNo = e.Item.Cells[0].Text;
				strGoodsID = e.Item.Cells[1].Text;
				//strCount = e.Item.Cells[4].Text;
				drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"' and cnnLostSerialNo="+strSerialNo);
				if(drs.Length>0)
				{
					dtProduct.Rows.Remove(drs[0]);
					//drs[0]["cnnCount"] = strCount;
				}

				ps = new LostSerial();
				ps.cnnLostSerialNo = Convert.ToInt32(strSerialNo);
				ps.cnvcInvCode = strGoodsID;

				ol = new OperLog();
				ol.cnvcDeptID = oper.strDeptID;
				ol.cnvcOperID = oper.strLoginID;
				ol.cnvcOperType = "生产产品报损删除";

				pf = new ProductFacade();
				pf.AdjustProductLostSerial_Delete(ps,ol);

				SetProduct(dtProduct);
				this.Popup("产品报损删除成功！");

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
			}
			else if(e.CommandName == "Adjust")
			{

				strSerialNo = e.Item.Cells[0].Text;
				strGoodsID = e.Item.Cells[1].Text;
				strCount = e.Item.Cells[4].Text;
				string strAddCount = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
				string strReduceCount = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
					
				if(strAddCount=="")
				{
					this.Popup("请输入调增量！");
					return;
				}
				if(!this.JudgeIsNum(strAddCount))
				{
					this.Popup("调增量请输入数字！");
					return;
				}
				if(Convert.ToDecimal(strAddCount)<=0)
				{
					this.Popup("调增量必须大于等于零！");
					return;
				}
				if(strReduceCount=="")
				{
					this.Popup("请输入调减量！");
					return;
				}
				if(!this.JudgeIsNum(strReduceCount))
				{
					this.Popup("调减量请输入数字！");
					return;
				}
				if(Convert.ToInt32(strReduceCount)<=0)
				{
					this.Popup("调减量必须大于零！");
					return;
				}

				if(Convert.ToDecimal(strCount)+Convert.ToDecimal(strAddCount)-Convert.ToDecimal(strReduceCount)<=0)
				{
					this.Popup("调减量必须小于报损量加调增量！");
					return;
				}

				//				switch(e.CommandName)
				//				{
				//					case "AdjustAdd":					
				drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"' and cnnLostSerialNo="+strSerialNo);
				if(drs.Length>0)
				{
					drs[0]["cnnAddCount"] = strAddCount;
					drs[0]["cnnReduceCount"] = strReduceCount;
					drs[0]["cnnSum"] = Convert.ToDecimal(strCount)+Convert.ToDecimal(strAddCount)-Convert.ToDecimal(strReduceCount);
				}
				ps = new LostSerial();
				ps.cnnLostSerialNo = Convert.ToInt32(strSerialNo);
				ps.cnvcInvCode = strGoodsID;
				ps.cnnAddCount = Convert.ToDecimal(strAddCount);

				ol = new OperLog();
				ol.cnvcDeptID = oper.strDeptID;
				ol.cnvcOperID = oper.strLoginID;
				ol.cnvcOperType = "生产产品报损调增";

				pf = new ProductFacade();
				pf.AdjustProductLostSerial_Add(ps,ol);


				SetProduct(dtProduct);
				this.Popup("调整成功！");
				BindGrid();
				//						break;
				//					case "AdjustReduce":						
				//						drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"' and cnnLostSerialNo="+strSerialNo);
				//						if(drs.Length>0)
				//						{
				//							drs[0]["cnnReduceCount"] = strReduceCount;
				//							drs[0]["cnnSum"] = Convert.ToDecimal(strCount)-Convert.ToDecimal(strReduceCount);
				//						}
				//
				//						ps = new LostSerial();
				//						ps.cnnLostSerialNo = Convert.ToInt32(strSerialNo);
				//						ps.cnvcInvCode = strGoodsID;
				//						ps.cnnReduceCount = Convert.ToDecimal(strReduceCount);
				//
				//
				//						ol = new OperLog();
				//						ol.cnvcDeptID = oper.strDeptID;
				//						ol.cnvcOperID = oper.strLoginID;
				//						ol.cnvcOperType = "生产产品报损调减";
				//
				//						pf = new ProductFacade();
				//						pf.AdjustProductLostSerial_Reduce(ps,ol);
				//
				//						SetProduct(dtProduct);
				//						this.Popup("调减成功！");
				//						BindGrid();
				//						break;
				
			}
			
		}

		private void MyDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//删除确认
				LinkButton delBttn = (LinkButton) e.Item.Cells[9].Controls[1];
				delBttn.Attributes.Add("onclick",@"javascript:return confirm('确定删除报损序号为：【" + e.Item.Cells[0].Text+"】产品名称为：【"+e.Item.Cells[2].Text + "】的产品吗?');"); 
			}
		}
	}
}
