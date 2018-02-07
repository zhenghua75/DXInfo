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

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmPoStockEnterDetail 的摘要说明。
	/// </summary>
	public class wfmPoStockEnterDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
	
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected ucPageView UcPageView1;
		protected System.Web.UI.WebControls.Button btnreturn;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.WebControls.DropDownList ddlProviderQuery;
		protected System.Web.UI.WebControls.Button btEnterComf;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Button btAddBill;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtQueryPoID;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsID;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtgopage;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected int gopage;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			string strrdid=Request.QueryString["rdid"];
			string strcode=Request.QueryString["code"];
			string strdept=Request.QueryString["dept"];
			string strwhid=Request.QueryString["whid"];
			if(strrdid==""||strrdid==null||strcode==""||strcode==null||strdept==""||strdept==null||strwhid==""||strwhid==null)
			{
				this.SetErrorMsgPageBydir("关键参数传递错误，请重试！");
				return;
			}
			else
			{
				if (!IsPostBack )
				{
					gopage=-1;
					this.txtgopage.Text="-1";
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.txtCode.Text=strcode;
					this.txtRdID.Text=strrdid;
					this.FillDropDownList("Provider",this.ddlProviderQuery);
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+strdept+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"vcCommCode='"+strwhid+"'");
					if(this.ddlProviderQuery.SelectedIndex<0)
						this.Button1.Enabled=false;
					this.txtCode.Enabled=false;
					this.txtRdID.Enabled=false;
					this.txtRdID.Visible=false;
					this.ddlWhouse.Enabled=false;
					this.ddlDept.Enabled=false;
					this.btEnterComf.Enabled=false;
					this.btEnterComf.Visible=false;
					string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strrdid).Rows[0]["cnvcState"].ToString();
					if(strState!="0")
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[20].Visible=false;
						this.Datagrid2.Columns[21].Visible=false;
					}
					this.DBBind();
				}
				else
				{
					gopage=int.Parse(this.txtgopage.Text.Trim())-1;
					this.txtgopage.Text=gopage.ToString();
				}
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
			this.btEnterComf.Click += new System.EventHandler(this.btEnterComf_Click);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			string strRdid=this.txtRdID.Text.Trim();
			string strsql="select a.cnnAutoID,a.cnnRdID,a.cnvcPOID,cnvcProviderID,a.cnvcInvCode,b.cnvcInvName,c.cnnStockCountSum as cnnPoCount,a.cnnQuantity,a.cnnPrice,a.cnnCost,a.cnnExtraCost,a.cnvcGroupCode,a.cnvcComunitCode,";
			strsql+="a.cnvcCommens,a.cnvcFlag,convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate from tbRdRecordDetail a,tbInventory b,tbPoStockSum c where a.cnvcInvCode=b.cnvcInvCode and a.cnvcPOID=c.cnvcPOID";
			strsql+=" and a.cnvcInvCode=c.cnvcGoodsCode and a.cnnRdID="+strRdid;
			DataTable dtdetail=Helper.Query(strsql);

			strsql="select a.cnvcInvCode as 存货编号,b.cnvcInvName as 存货名称,a.cnvcComunitCode as 单位,sum(a.cnnQuantity) as 数量,sum(a.cnnCost+a.cnnExtraCost) as 金额";
			strsql+=" from tbRdRecordDetail a,tbInventory b where a.cnnRdID="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode group by a.cnvcInvCode,b.cnvcInvName,a.cnvcComunitCode";
			DataTable dtsum=Helper.Query(strsql);
			Session["sumenter"]=dtsum;

			this.TableConvert(dtdetail,"cnvcProviderID","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcComunitCode","ComputationUnit","vcCommCode","vcCommName");
			Session["podetailtomod"]=dtdetail;
			this.Datagrid2.Columns[10].Visible=true;
			this.Datagrid2.Columns[11].Visible=false;
			this.Datagrid2.Columns[13].Visible=true;
			this.Datagrid2.Columns[14].Visible=false;
			this.Datagrid2.Columns[15].Visible=true;
			this.Datagrid2.Columns[16].Visible=false;
			this.Datagrid2.Columns[17].Visible=true;
			this.Datagrid2.Columns[18].Visible=false;
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource = dtdetail;
			this.Datagrid2.DataBind();

			this.TableConvert(dtsum,"单位","ComputationUnit","vcCommCode","vcCommName");
			UcPageView1.MyDataGrid.Caption="采购入库子单汇总表";
			UcPageView1.MyDataGrid.PageSize = 20;
			this.UcPageView1.MyDataSource = dtsum.DefaultView;
			this.UcPageView1.BindGrid();
//			this.UcPageView1.MyDataGrid.Columns[1].Visible=false;

//			string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strRdid).Rows[0]["cnvcState"].ToString();
//			if(strState=="0"&&dtsum.Rows.Count>0)
//			{
////				this.btEnterComf.Enabled=true;
//			}
//			else if(strState!="0")
//			{
//				this.Button1.Enabled=false;
//				this.btEnterComf.Enabled=false;
//				this.Datagrid2.Columns[15].Visible=false;
//				this.Datagrid2.Columns[16].Visible=false;
//			}
			
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.Cells[22].Text.Trim()=="0")
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=true;
					((Button)dgi.Cells[21].Controls[0]).Visible=true;
				}
				else
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=false;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strsql="";
			if(this.txtQueryPoID.Text.Trim()=="")
			{
				strsql="select cnvcPoID,cnvcPrvdCode,cnvcComments from tbPoStockMain where cnvcPrvdCode='"+this.ddlProviderQuery.SelectedValue+"' and cnvcPoState='1' and cnvcPoID not in(select distinct cnvcPOID from tbRdRecordDetail where cnvcPoID like 'POS%')";
			}
			else
			{
				strsql="select cnvcPoID,cnvcPrvdCode,cnvcComments from tbPoStockMain where cnvcPoID='"+this.txtQueryPoID.Text.Trim()+"' and cnvcPrvdCode='"+this.ddlProviderQuery.SelectedValue+"' and cnvcPoState='1' and cnvcPoID not in(select distinct cnvcPOID from tbRdRecordDetail where cnvcPoID like 'POS%')";
			}
			DataTable dt = Helper.Query(strsql);
			this.TableConvert(dt,"cnvcPrvdCode","Provider","vcCommCode","vcCommName");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			string strPoID=item.Cells[0].Text.Trim();
			OperLog operLog=new OperLog();
			operLog.cnvcOperType = "添加关联采购订单";
			operLog.cnvcOperID = this.oper.strLoginID;
			operLog.cnvcDeptID = this.oper.strDeptID;
			StorageFacade sto=new StorageFacade();
			int ret=sto.AddPoStockEnterDetail(operLog,this.txtRdID.Text.Trim(),strPoID,this.ddlProviderQuery.SelectedValue);
			if(ret > 0 )
			{
				this.Popup("添加关联采购订单成功！");
			}
			else
			{
				this.Popup("添加关联采购订单失败！");
			}

			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DBBind();
		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			DataTable dtjjj=(DataTable)Session["podetailtomod"];
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.DataBind();
			this.Datagrid2.Columns[10].Visible=false;
			this.Datagrid2.Columns[11].Visible=true;
			this.Datagrid2.Columns[13].Visible=false;
			this.Datagrid2.Columns[14].Visible=true;
			this.Datagrid2.Columns[15].Visible=false;
			this.Datagrid2.Columns[16].Visible=true;
			this.Datagrid2.Columns[17].Visible=false;
			this.Datagrid2.Columns[18].Visible=true;
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.Cells[22].Text.Trim()=="0"&&dgi.ItemIndex==e.Item.ItemIndex)
				{
					((TextBox)dgi.Cells[11].Controls[1]).Text=e.Item.Cells[10].Text.Trim();
					((TextBox)dgi.Cells[14].Controls[1]).Text=e.Item.Cells[13].Text.Trim();
					if(e.Item.Cells[15].Text.Trim()=="&nbsp;")
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[16].Controls[1]).Value="";
					else
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[16].Controls[1]).Value=e.Item.Cells[15].Text.Trim();
					if(e.Item.Cells[17].Text.Trim()=="&nbsp;")
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[18].Controls[1]).Value="";
					else
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[18].Controls[1]).Value=e.Item.Cells[17].Text.Trim();
					((Button)dgi.Cells[20].Controls[0]).Visible=true;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
				else
				{
					dgi.Cells[11].Text=dgi.Cells[10].Text.Trim();
					dgi.Cells[14].Text=dgi.Cells[13].Text.Trim();
					dgi.Cells[16].Text=dgi.Cells[15].Text.Trim();
					dgi.Cells[18].Text=dgi.Cells[17].Text.Trim();
					((Button)dgi.Cells[20].Controls[0]).Visible=false;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
			}
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.DataBind();
			this.Datagrid2.Columns[10].Visible=true;
			this.Datagrid2.Columns[11].Visible=false;
			this.Datagrid2.Columns[13].Visible=true;
			this.Datagrid2.Columns[14].Visible=false;
			this.Datagrid2.Columns[15].Visible=true;
			this.Datagrid2.Columns[16].Visible=false;
			this.Datagrid2.Columns[17].Visible=true;
			this.Datagrid2.Columns[18].Visible=false;
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.Cells[22].Text.Trim()=="0")
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=true;
					((Button)dgi.Cells[21].Controls[0]).Visible=true;
				}
				else
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=false;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
			}
		}

		private void hidecontrol(int index)
		{
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.ItemIndex!=index)
				{
					dgi.Cells[11].Text=dgi.Cells[10].Text.Trim();
					dgi.Cells[14].Text=dgi.Cells[13].Text.Trim();
					dgi.Cells[16].Text=dgi.Cells[15].Text.Trim();
					dgi.Cells[18].Text=dgi.Cells[17].Text.Trim();
					((Button)dgi.Cells[20].Controls[0]).Visible=false;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
			}
		}
		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			if(((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim()==""||!this.JudgeIsNum(((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim()))
			{
				this.Popup("单价必须是数字！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			if(((TextBox)e.Item.Cells[11].Controls[1]).Text.Trim()==""||!this.JudgeIsNum(((TextBox)e.Item.Cells[11].Controls[1]).Text.Trim()))
			{
				this.Popup("入库数量必须是数字！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			if(((TextBox)e.Item.Cells[14].Controls[1]).Text.Trim()==""||!this.JudgeIsNum(((TextBox)e.Item.Cells[14].Controls[1]).Text.Trim()))
			{
				this.Popup("其它费用数量必须是数字！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			if(((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[16].Controls[1]).Value=="")
			{
				this.Popup("生产日期不能为空！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			if(((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[18].Controls[1]).Value=="")
			{
				this.Popup("过期日期不能为空！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			string strExpire=Helper.Query("select cnnExpire from tbInventory where cnvcInvCode='"+e.Item.Cells[4].Text.Trim()+"'").Rows[0]["cnnExpire"].ToString();
			if(strExpire!=""&&strExpire!="0")
			{
				DateTime dtExpdate=DateTime.Parse(((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[18].Controls[1]).Value);
                TimeSpan ts=dtExpdate.Subtract(DateTime.Now.Date);
				if(ts.Days<=int.Parse(strExpire))
				{
					this.Popup("到期天数不足"+strExpire+"天，不能入库！");
					this.hidecontrol(e.Item.ItemIndex);
					return;
				}
			}
			Entity.RdRecordDetail rrd=new RdRecordDetail();
			rrd.cnnAutoID=int.Parse(e.Item.Cells[0].Text.Trim());
			rrd.cnnPrice=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim()),2);
			rrd.cnnQuantity=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[11].Controls[1]).Text.Trim()),2);
			rrd.cnnExtraCost=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[14].Controls[1]).Text.Trim()),2);
			rrd.cndMdate=DateTime.Parse(((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[16].Controls[1]).Value.Trim());
			rrd.cndExpDate=DateTime.Parse(((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[18].Controls[1]).Value.Trim());
			rrd.cnvcCommens=((TextBox)e.Item.Cells[19].Controls[0]).Text.Trim();

			if(rrd.cnnAutoID.ToString()==""||rrd.cnnAutoID==0)
			{
				this.Popup("采购入库单子表标识不正确！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
			if(rrd.cndMdate.CompareTo(rrd.cndExpDate)>=0)
			{
				this.Popup("生产日期大于过期日期，不正确！");
				this.hidecontrol(e.Item.ItemIndex);
				return;
			}
//			DateTime dttmp=rrd.cndMdate;
//			switch(rrd.cnvcMassUnit)
//			{
//				case "1":
//					if(dttmp.AddDays(rrd.cnnMassDate).CompareTo(rrd.cndExpDate)!=0)
//					{
//						this.Popup("生产日期+保质期数后不等于过期日期，请检查！");
//						this.hidecontrol(e.Item.ItemIndex);
//						return;
//					}
//					break;
//				case "2":
//					if(dttmp.AddMonths(rrd.cnnMassDate).CompareTo(rrd.cndExpDate)!=0)
//					{
//						this.Popup("生产日期+保质期数后不等于过期日期，请检查！");
//						this.hidecontrol(e.Item.ItemIndex);
//						return;
//					}
//					break;
//				case "3":
//					if(dttmp.AddYears(rrd.cnnMassDate).CompareTo(rrd.cndExpDate)!=0)
//					{
//						this.Popup("生产日期+保质期数后不等于过期日期，请检查！");
//						this.hidecontrol(e.Item.ItemIndex);
//						return;
//					}
//					break;
//			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "修改采购入库子表";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdatePoStockEnterDetail(ol,rrd);
			if(ret > 0 )
			{
				this.Popup("修改采购入库单子表明细成功！");
			}
			else
			{
				this.Popup("修改采购入库单子表明细失败！");
			}
			this.DBBind();
		}

		private void btEnterComf_Click(object sender, System.EventArgs e)
		{

		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["podetailtomod"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
			this.Datagrid2.Columns[10].Visible=true;
			this.Datagrid2.Columns[11].Visible=false;
			this.Datagrid2.Columns[13].Visible=true;
			this.Datagrid2.Columns[14].Visible=false;
			this.Datagrid2.Columns[15].Visible=true;
			this.Datagrid2.Columns[16].Visible=false;
			this.Datagrid2.Columns[17].Visible=true;
			this.Datagrid2.Columns[18].Visible=false;
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.Cells[22].Text.Trim()=="0")
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=true;
					((Button)dgi.Cells[21].Controls[0]).Visible=true;
				}
				else
				{
					((Button)dgi.Cells[20].Controls[0]).Visible=false;
					((Button)dgi.Cells[21].Controls[0]).Visible=false;
				}
			}
		}

		private void Datagrid2_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName=="SelectEnter")
			{
				if(e.Item.Cells[15].Text.Trim()==""||e.Item.Cells[15].Text.Trim()=="&nbsp;")
				{
					this.Popup("请填写生产日期！");
					return;
				}
				if(e.Item.Cells[17].Text.Trim()==""||e.Item.Cells[17].Text.Trim()=="&nbsp;")
				{
					this.Popup("请填写过期日期！");
					return;
				}
				string strWhcode=this.ddlWhouse.SelectedValue;
				Entity.RdRecordDetail rrd=new RdRecordDetail();
				rrd.cnnAutoID=int.Parse(e.Item.Cells[0].Text.Trim());
				rrd.cnvcPOID=e.Item.Cells[2].Text.Trim();
				rrd.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
				rrd.cndMdate=DateTime.Parse(e.Item.Cells[15].Text.Trim());
				rrd.cndExpDate=DateTime.Parse(e.Item.Cells[17].Text.Trim());
				rrd.cnvcInvCode=e.Item.Cells[4].Text.Trim();

				if(strWhcode=="")
				{
					this.Popup("仓库不能为空！");
					return;
				}
//				DataTable dtsum=(DataTable)Session["sumenter"];
//				if(dtsum==null||dtsum.Rows.Count<=0)
//				{
//					this.Popup("该采购入库单无任何货品记录，请先关联采购订单！");
//					return;
//				}
			
				Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "采购入库单入库";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;

				StorageFacade sto = new StorageFacade();				
				int ret = sto.PoStockEnterExecEntering(ol,strWhcode,rrd);
				if(ret > 0 )
				{
					this.Popup("采购入库单入库成功！");
					this.DBBind();
				}
				else
				{
					this.Popup("采购入库单入库失败！");
				}
			}
		}
	}
}
