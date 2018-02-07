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
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmSaleDailyClear 的摘要说明。
	/// </summary>
	public class wfmSaleDailyClear : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Button btnCheckOk;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblCheckDate;
		protected System.Web.UI.WebControls.TextBox txtCheckNo;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",ddlDept);
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
				}
				else
				{
					this.FillDropDownList("NewDept",ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				this.lblCheckDate.Text=DateTime.Now.ToString("yyyy-MM-dd");
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.btnCheckOk.Enabled=false;
				this.txtCheckNo.Text=DateTime.Now.ToString("yyyyMMdd");
				this.txtCheckNo.Visible=false;
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
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.btnCheckOk.Click += new System.EventHandler(this.btnCheckOk_Click);
			this.Datagrid2.PageIndexChanged+=new DataGridPageChangedEventHandler(Datagrid2_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			if(this.txtCheckNo.Text.Trim()==""||this.txtCheckNo.Text.Trim().Length!=8)
			{
				this.Popup("盘点序号不正确，请重试！");
				return;
			}
			string strsql="select '"+this.ddlDept.SelectedValue+"' as cnvcDeptID,a.cnvcWhCode,a.cnvcInvCode,b.cnvcInvName,b.cnvcSTComunitCode,cast(a.cnnQuantity/c.cniChangRate as numeric(18,2)) as cnnSysCount,";
			strsql+="0 as cnnCheckCount,convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate from tbCurrentStock a,tbInventory b,tbComputationUnit c";
			strsql+=" where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112) not in (select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112)";
			strsql+=" from tbStorageCheckLog where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo like '"+this.txtCheckNo.Text.Trim()+"%') and b.cnvcInvCCode in(select cnvcProductClassCode";
			strsql+=" from tbProductClass where cnvcProductType='FINALPRODUCT') and a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode";
			DataTable dtout=Helper.QueryLongTrans(strsql);
			this.TableConvert(dtout,"cnvcDeptID","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcWhCode","WareHouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcSTComunitCode","ComputationUnit","vcCommCode","vcCommName");
			if(dtout.Rows.Count>0)
			{
				this.btnCheckOk.Enabled=true;
				this.ddlDept.Enabled=false;
				this.ddlWhouse.Enabled=false;
			}
			else
			{
				this.btnCheckOk.Enabled=false;
				this.ddlDept.Enabled=true;
				this.ddlWhouse.Enabled=true;
			}
			Session["checkClear"]=dtout;
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.PageSize=20;
			this.Datagrid2.DataSource=dtout;
			this.Datagrid2.DataBind();
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.btnCheckOk.Enabled=false;
			}
		}
		private void btQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void btnCheckOk_Click(object sender, System.EventArgs e)
		{
			if(this.txtCheckNo.Text.Trim()==""||this.txtCheckNo.Text.Trim().Length!=8)
			{
				this.Popup("盘点序号不正确，请重试！");
				return;
			}
			if(this.Datagrid2.Items.Count<=0)
			{
				this.Popup("无任何盘点存货记录！");
				return;
			}
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "仓库库存清零盘点";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.StorageCheckClearConfirm(ol,this.txtCheckNo.Text.Trim(),this.ddlWhouse.SelectedValue,this.ddlDept.SelectedValue,this.oper.strOperName);
			if(ret > 0 )
			{
				this.Popup("仓库库存盘点确认更新库存成功！");
				this.DBBind();
			}
			else
			{
				this.Popup("仓库库存盘点确认更新库存失败！");
			}
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["checkClear"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
		}
	}
}
