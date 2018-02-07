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
	/// wfmStorageBalanceReport 的摘要说明。
	/// </summary>
	public class wfmStorageBalanceReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProClass;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlProType;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnRefreshSTBA;
		protected System.Web.UI.WebControls.Button btExcel;
	
		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept,"","全部");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","全部");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProType,"vcCommSign='PRODUCTTYPE'");
				this.FillDropDownList("PClass",this.ddlProClass,"","全部");
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.ddlProType.SelectedIndexChanged += new System.EventHandler(this.ddlProType_SelectedIndexChanged);
			this.btnRefreshSTBA.Click += new System.EventHandler(this.btnRefreshSTBA_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("toExcel");
			if(this.ddlDept.SelectedValue==""||this.ddlWhouse.SelectedValue=="")
			{
				this.Popup("无部门和仓库信息，无权访问！");
				return;
			}
			string strsql="";
			if(this.ddlProType.SelectedValue=="FINALPRODUCT")
			{
				strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,c.cnvcInvName as 存货名称,b.cnvcComunitName as 单位,a.cnnQuantity as 当前库存量,";
				strsql+="a.cnnCheckEnter as 盘点入库,a.cnnPoEnter as 采购入库,a.cnnPoReturn as 采购退货,a.cnnProductUse as 生产使用,a.cnnProductLost as 生产损耗,";
				strsql+="a.cnnProductEnter as 生产入库,a.cnnAssgOut as 分货出库,a.cnnAssgEnter as 分货入库,a.cnnMoveEnter as 调拨入库,a.cnnMoveOut as 调拨出库,";
				strsql+="a.cnnTranLost as 其中运输损耗,a.cnnConsCount as 销售,a.cnnConsLost as 销售损耗,a.cnnExpLost as 过期损耗,cnnProDiff as 差异";
				strsql+=" from tbStorageBalanceRelation a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode and b.cnvcComunitCode=c.cnvcSTComunitCode";
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='FINALPRODUCT')";
			}
			else
			{
				strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,c.cnvcInvName as 存货名称,b.cnvcComunitName as 单位,a.cnnQuantity as 当前库存量,";
				strsql+="a.cnnCheckEnter as 盘点入库,a.cnnPoEnter as 采购入库,a.cnnPoReturn as 采购退货,a.cnnProductUse as 生产使用,a.cnnProductLost as 生产损耗,";
				strsql+="a.cnnMoveEnter as 调拨入库,a.cnnMoveOut as 调拨出库,a.cnnTranLost as 其中运输损耗,a.cnnConsCount as 销售,a.cnnConsLost as 销售损耗,";
				strsql+="a.cnnExpLost as 过期损耗,cnnMatDiff  as 差异";
				strsql+=" from tbStorageBalanceRelation a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode ";
				strsql+=" and b.cnvcComunitCode=c.cnvcSTComunitCode and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType<>'FINALPRODUCT')";
			}
			if(this.ddlWhouse.SelectedValue!="全部")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			else
			{
				if(this.ddlDept.SelectedValue!="全部")
					strsql+=" and a.cnvcWhCode in(select cnvcWhCode from tbWareHouse where cnvcDepCode='"+this.ddlDept.SelectedValue+"')";
			}
			if(this.ddlProClass.SelectedValue!="全部")
				strsql+=" and c.cnvcInvCCode='"+this.ddlProClass.SelectedValue+"'";
			else
			{
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='"+this.ddlProType.SelectedValue+"')";
			}
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and c.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" order by a.cnvcInvCode,a.cnvcWhCode";

			DataTable dtbalance=Helper.Query(strsql);
			dtbalance.Columns.Add("查看");
			foreach(DataRow dr in dtbalance.Rows)
			{
				dr["查看"]="<a href='wfmStorageBalanceDetailReport.aspx?whcode="+dr["仓库"].ToString()+"&Invcode=" + dr["存货编码"].ToString() + "&Invname="+dr["存货名称"].ToString()+"'>明细</a>";
			}
			this.TableConvert(dtbalance,"仓库","Warehouse","vcCommCode","vcCommName");
			dtbalance.TableName="库存平衡关系报表";
			Session["QUERY"] = dtbalance;
			Session["toExcel"]=dtbalance;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtbalance);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","全部");
			}
		}

		private void ddlProType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.FillDropDownList("PClass",this.ddlProClass,"vcCommSign='"+this.ddlProType.SelectedValue+"'","全部");
		}

		private void btnRefreshSTBA_Click(object sender, System.EventArgs e)
		{
			StorageFacade sto = new StorageFacade();				
			int ret = sto.CreateStorageBalance();
			if(ret > 0 )
			{
				this.Popup("重新刷新库存平衡关系成功！");
			}
			else
			{
				this.Popup("重新刷新库存平衡关系失败！");
			}
		}
	}
}
