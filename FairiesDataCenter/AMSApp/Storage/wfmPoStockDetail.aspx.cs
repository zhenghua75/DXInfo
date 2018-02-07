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
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// wfmPoStockDetail 的摘要说明。
	/// </summary>
	public class wfmPoStockDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtPoID;
		protected System.Web.UI.WebControls.DropDownList ddlDeptID;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtStockPrice;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtStockCount;
		protected System.Web.UI.WebControls.Label Label2;
	
		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtGoodsName;
		protected System.Web.UI.WebControls.TextBox txtGoodsCode;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsCode;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsName;
		protected ucPageView UcPageView1;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList ddlStockUnit;
		protected System.Web.UI.WebControls.DropDownList ddlUnitGroup;
		protected System.Web.UI.WebControls.TextBox txtState;
		protected string strArriveDate;
		protected System.Web.UI.WebControls.TextBox txtgopage;
		protected int gopage;

		private void Page_Load(object sender, System.EventArgs e)
		{
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]!=null)
			{
				string strPoid=Request.QueryString["POID"];
				string strProvider=Request.QueryString["Prvd"];
				string strPoState=Request.QueryString["pos"];
				if(strPoid==""||strPoid==null||strProvider==""||strProvider==null||strPoState==""||strPoState==null)
				{
					this.SetErrorMsgPageBydir("采购订单号或供应商编码为空，请重试！");
				}
				else
				{
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
					if (!IsPostBack )
					{
						gopage=-1;
						this.txtgopage.Text="-1";
						Session.Remove("QUERY");
						Session.Remove("page_view");
						Session.Remove("podetailtomod");
						this.txtPoID.Text=strPoid;
						this.txtState.Text=strPoState;
						this.txtPoID.Enabled=false;
						this.txtGoodsName.Enabled=false;
						this.ddlUnitGroup.Enabled=false;
						this.ddlStockUnit.Enabled=false;
						if(ls1.strDeptID=="CEN00")
							this.FillDropDownList("NewDept",this.ddlDeptID);
						else
							this.FillDropDownList("NewDept",this.ddlDeptID,"vcCommCode='"+ls1.strNewDeptID+"'");
						this.FillDropDownList("Provider",this.ddlProvider);
						this.FillDropDownList("ComputationGroup",this.ddlUnitGroup);
						this.FillDropDownList("ComputationUnit",this.ddlStockUnit,"cnvcGroupCode='"+this.ddlUnitGroup.SelectedValue+"'");
						this.ddlProvider.SelectedIndex=this.ddlProvider.Items.IndexOf(this.ddlProvider.Items.FindByValue(strProvider));
						this.ddlProvider.Enabled=false;
						if(strPoState=="2")
						{
							this.Button1.Enabled=false;
							this.txtStockCount.Enabled=false;
							this.ddlDeptID.Enabled=false;
							this.btnAdd.Enabled=false;
							this.Datagrid2.Columns[19].Visible=false;
							this.Datagrid2.Columns[20].Visible=false;
						}
						this.DBBind();
					}
					else
					{
						gopage=int.Parse(this.txtgopage.Text.Trim())-1;
						this.txtgopage.Text=gopage.ToString();
						strArriveDate = Request.Form["txtArrive"].ToString();
					}
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Datagrid2.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_DeleteCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void DBBind()
		{
			string strPoID=this.txtPoID.Text.Trim();
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataSet dsout=StoBusi.GetPoStockDetailSum(strPoID);
				if(dsout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dsout.Tables["detail"],"cnvcDeptName","NewDept");
					this.TableConvert(dsout.Tables["detail"],"cnvcRowState","tbNameCodeToStorage","vcCommSign='PoState'");
					this.TableConvert(dsout.Tables["detail"],"cnvcGroupName","ComputationGroup");
					this.TableConvert(dsout.Tables["detail"],"cnvcStockUnit","ComputationUnit");
					Session["podetailtomod"]=dsout.Tables["detail"];
					DataView dvOut1 =new DataView(dsout.Tables["detail"]);
					this.Datagrid2.PageSize=10;
					this.Datagrid2.EditItemIndex=-1;
					this.Datagrid2.DataSource = dvOut1;
					this.Datagrid2.DataBind();
					foreach(DataGridItem dgi in this.Datagrid2.Items)
					{
						if(dgi.Cells[13].Text.Trim()=="完成")
						{
							((Button)dgi.Cells[19].Controls[0]).Visible=false;
							((Button)dgi.Cells[20].Controls[0]).Visible=false;
						}
					}

					this.TableConvert(dsout.Tables["sum"],"计量单位组","ComputationGroup");
					this.TableConvert(dsout.Tables["sum"],"单位","ComputationUnit");
					UcPageView1.MyDataGrid.Caption="采购订单汇总表";
					UcPageView1.MyDataGrid.PageSize = 20;
					DataView dvOut2 =new DataView(dsout.Tables["sum"]);
					this.UcPageView1.MyDataSource = dvOut2;
					this.UcPageView1.BindGrid();
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			strArriveDate = Request.Form["txtArrive"].ToString();
			if(strArriveDate==""||strArriveDate==null)
			{
				this.Popup("到货时间不能为空，请重新选择时间！");
				return;
			}
			if(this.txtStockPrice.Text.Trim()==""||!this.JudgeIsNum(this.txtStockPrice.Text.Trim()))
			{
				this.Popup("采购货品单价不能为空且必须是数字！");
				return;
			}
			if(this.txtStockCount.Text.Trim()==""||!this.JudgeIsNum(this.txtStockCount.Text.Trim()))
			{
				this.Popup("采购货品数量不能为空且必须是数字！");
				return;
			}
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			CMSMStruct.PoStockDetailStruct psds1=new CMSMStruct.PoStockDetailStruct();
			psds1.strPoID=this.txtPoID.Text.Trim();
			psds1.strDeptID=this.ddlDeptID.SelectedValue;
			psds1.strGoodsCode=this.txtGoodsCode.Text.Trim();
			psds1.strStockUnit=this.ddlStockUnit.SelectedValue;
			psds1.strGroupCode=this.ddlUnitGroup.SelectedValue;
			psds1.dStockPrice=Math.Round(double.Parse(this.txtStockPrice.Text.Trim()),2);
			psds1.dStockCount=Math.Round(double.Parse(this.txtStockCount.Text.Trim()),2);
			psds1.dStockFee=Math.Round(psds1.dStockPrice*psds1.dStockCount,2);
			psds1.dtArriveDate=DateTime.Parse(strArriveDate);
			psds1.strRowState="0";
			psds1.strCreater=ls1.strOperName;

			if(psds1.strPoID==""||psds1.strPoID.Length!=13)
			{
				this.Popup("采购订单号不正确！");
				return;
			}
			if(psds1.strDeptID=="")
			{
				this.Popup("下订部门不能为空！");
				return;
			}
			if(psds1.strGoodsCode=="")
			{
				this.Popup("采购货品不能为空！");
				return;
			}
			if(psds1.strStockUnit=="")
			{
				this.Popup("采购货品单位不能为空！");
				return;
			}
			if(psds1.dStockCount==0)
			{
				this.Popup("采购货品数量不能为0！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.IsExistPoStockDetail(psds1.strPoID,psds1.strDeptID,psds1.strGoodsCode))
				{
					this.Popup("该订单下已经有此部门和货品的子订单，你可直接在已有子订单中修改订购数量！");
                    this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
					return;
				}

				if(StoBusi.NewPoSotckDetailAdd(psds1,ls1.strLoginID))
				{
					this.SetSuccMsgPageBydir("新采购子订单添加成功！","Storage/wfmPoStockDetail.aspx?POID="+psds1.strPoID+"&Prvd="+this.ddlProvider.SelectedValue+"&pos="+this.txtState.Text.Trim());
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("新采购子订单添加时发生错误，请重试！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);

			DataTable dt = StoBusi.GetPoStockGoodsBySelect(this.txtPoID.Text.Trim(),this.ddlProvider.SelectedValue,this.txtQueryGoodsCode.Text.Trim(),this.txtQueryGoodsName.Text.Trim());
			this.TableConvert(dt,"cnvcGroupCode","ComputationGroup");
			this.TableConvert(dt,"cnvcStockUnit","ComputationUnit");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			this.txtGoodsCode.Text=item.Cells[0].Text.Trim();
			this.txtGoodsName.Text=item.Cells[1].Text.Trim();
			this.ddlUnitGroup.SelectedIndex=this.ddlUnitGroup.Items.IndexOf(this.ddlUnitGroup.Items.FindByText(item.Cells[2].Text.Trim()));
			this.FillDropDownList("ComputationUnit",this.ddlStockUnit,"cnvcGroupCode='"+this.ddlUnitGroup.SelectedValue+"'");
			this.ddlStockUnit.SelectedIndex=this.ddlStockUnit.Items.IndexOf(this.ddlStockUnit.Items.FindByText(item.Cells[3].Text.Trim()));
			this.txtStockPrice.Text=item.Cells[4].Text.Trim();
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DBBind();
		}

//		private void btnreturn_Click(object sender, System.EventArgs e)
//		{
////			Response.Redirect("wfmPoStock.aspx");
//			Response.Write("<script language=javascript>history.go(-1);</script>");
//		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string newEnterCount=((TextBox)e.Item.Cells[9].Controls[0]).Text.Trim();
				string price=e.Item.Cells[8].Text.Trim();
				if(newEnterCount=="")
				{
					this.Popup("请输入订单数量");
					return;
				}
				if(!this.JudgeIsNum(newEnterCount,"订单数量"))
				{
					return;
				}
				decimal newfee=Math.Round(decimal.Parse(price)*decimal.Parse(newEnterCount),2);

				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				Hashtable htpara=new Hashtable();
				htpara.Add("strNewEnterCount",newEnterCount);
				htpara.Add("strNewfee",newfee.ToString());
				htpara.Add("strPoId",e.Item.Cells[0].Text.Trim());
				htpara.Add("strDeptID",e.Item.Cells[1].Text.Trim());
				htpara.Add("strGoodsCode",e.Item.Cells[3].Text.Trim());
				htpara.Add("strOperName",ls1.strOperName);
				htpara.Add("strOperID",ls1.strLoginID);

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				if(StoBusi.PoSotckDetailMod(htpara))
				{
					this.Popup("采购订单明细修正成功！");
					this.DBBind();
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("采购订单明细修正失败！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

//		private void Datagrid2_ItemCommand(object source, DataGridCommandEventArgs e)
//		{
//			if(e.CommandName=="Select")
//			{
//				try
//				{
//					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//					Hashtable htpara=new Hashtable();
//					htpara.Add("strPoId",e.Item.Cells[0].Text.Trim());
//					htpara.Add("strDeptID",e.Item.Cells[1].Text.Trim());
//					htpara.Add("strGoodsCode",e.Item.Cells[3].Text.Trim());
//					htpara.Add("strOperName",ls1.strOperName);
//
//					Hashtable htapp=(Hashtable)Application["appconf"];
//					string strcons=(string)htapp["cons"];
//					StoBusi=new BusiComm.StorageBusi(strcons);
//					if(StoBusi.PoSotckDetailChecked(htpara))
//					{
//						this.Popup("采购订单明细审核成功！");
//						this.DBBind();
//						this.RegisterStartupScript("hide","<script lanaguage=javascript>ShowHide('1','none');</script>");
//						return;
//					}
//					else
//					{
//						this.SetErrorMsgPageBydir("采购订单明细审核失败！");
//						return;
//					}
//				}
//				catch(Exception er)
//				{
//					this.clog.WriteLine(er);
//					this.SetErrorMsgPageBydir("查询错误，请重试！");
//					return;
//				}
//			}
		//		}

		private void Datagrid2_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string strPoID=e.Item.Cells[0].Text.Trim();
				string strDeptID=e.Item.Cells[1].Text.Trim();
				string strGoodsID=e.Item.Cells[3].Text.Trim();

				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				Hashtable htpara=new Hashtable();
				htpara.Add("strPoID",strPoID);
				htpara.Add("strDeptID",strDeptID);
				htpara.Add("strGoodsID",strGoodsID);
				htpara.Add("strOperID",ls1.strLoginID);

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				if(StoBusi.PoSotckDetailDelete(htpara))
				{
					this.Popup("采购订单明细删除成功！");
					this.DBBind();
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("采购订单明细删除失败！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
		}
	}
}
