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
	/// wfmInventotyReStop 的摘要说明。
	/// </summary>
	public class wfmInventotyReStop : wfmBase
	{
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Label Label2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept);
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Datagrid2.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_ItemCommand);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			string strsql="select a.cnnAutoID,a.cnvcWhCode,a.cnvcInvCode,b.cnvcInvName,c.cnvcComunitName,convert(char(8),a.cndMdate,112) as cndMdate,convert(char(8),a.cndExpDate,112) as cndExpDate,";
			strsql+="cast(a.cnnQuantity/cniChangRate as numeric(12,4)) as cnnQuantity,cast(a.cnnStopQuantity/cniChangRate as numeric(12,4)) as cnnStopQuantity,";
			strsql+="cast(a.cnnAvaQuantity/cniChangRate as numeric(12,4)) as cnnAvaQuantity from tbCurrentStock a,tbInventory b,tbComputationUnit c where a.cnvcStopFlag='1'";
			strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode and (a.cnnQuantity>0 or a.cnnStopQuantity>0 or a.cnnAvaQuantity>0)";
			if(this.txtInvCode.Text.Trim()!="")
				strsql+=" and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"'";
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and b.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" order by a.cnvcWhCode,a.cnvcInvCode,a.cndExpDate";
			DataTable dtdetail=Helper.Query(strsql);

			this.TableConvert(dtdetail,"cnvcWhCode","Warehouse","vcCommCode","vcCommName");
			Session["Restop"]=dtdetail;
			this.Datagrid2.DataSource = dtdetail;
			this.Datagrid2.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void Datagrid2_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName=="ReStop")
			{
				string strAutoID=e.Item.Cells[0].Text.Trim();
				if(strAutoID=="")
				{
					this.Popup("库存记录标识错误，请重试！");
					return;
				}
				Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "库存解冻";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;

				StorageFacade sto = new StorageFacade();				
				int ret = sto.UpdateCurrentStockReStop(ol,strAutoID);
				if(ret > 0 )
				{
					this.Popup("库存解冻成功！");
					this.DBBind();
				}
				else
				{
					this.Popup("库存解冻失败！");
				}
			}
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["Restop"];
			this.Datagrid2.DataBind();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}
	}
}
