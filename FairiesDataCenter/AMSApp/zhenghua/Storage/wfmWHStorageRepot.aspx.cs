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
	/// wfmWHStorageRepot 的摘要说明。
	/// </summary>
	public class wfmWHStorageRepot : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProType;
		protected System.Web.UI.WebControls.DropDownList ddlProClass;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.CheckBox chbSum;
		protected System.Web.UI.WebControls.CheckBox chkExp;
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
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProType,"vcCommSign='PRODUCTTYPE'","全部");
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
			this.ddlProType.SelectedIndexChanged += new System.EventHandler(this.ddlProType_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
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
			if(this.chbSum.Checked)
			{
				if(!this.chkExp.Checked)
				{
					strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,b.cnvcInvName as 存货名称,b.cnvcSTComunitCode as 单位,sum(cast(a.cnnQuantity/cniChangRate as numeric(12,4))) as 结存数量,sum(cast(a.cnnAvaQuantity/cniChangRate as numeric(12,4))) as 可用数量";
					strsql+=" from tbCurrentStock a,tbInventory b,tbComputationUnit c where a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode and a.cnnQuantity>0";
				}
				else
				{
					strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,b.cnvcInvName as 存货名称,b.cnvcSTComunitCode as 单位,sum(cast(a.cnnQuantity/cniChangRate as numeric(12,4))) as 结存数量,sum(cast(a.cnnStopQuantity/cniChangRate as numeric(12,4))) as 冻结数量,sum(cast(a.cnnAvaQuantity/cniChangRate as numeric(12,4))) as 可用数量";
					strsql+=" from tbCurrentStock a,tbInventory b,tbComputationUnit c where a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode and a.cnnQuantity>0 and convert(char(8),a.cndExpDate,112)<convert(char(8),getdate(),112)";
				}
			}
			else
			{
				if(!this.chkExp.Checked)
				{
					strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,b.cnvcInvName as 存货名称,b.cnvcSTComunitCode as 单位,cast(a.cnnQuantity/cniChangRate as numeric(12,4)) as 结存数量,cast(a.cnnAvaQuantity/cniChangRate as numeric(12,4)) as 可用数量";
					strsql+=",convert(char(10),a.cndMDate,120) as 生产日期,convert(char(10),a.cndExpDate,120) as 过期日期";
					strsql+=" from tbCurrentStock a,tbInventory b,tbComputationUnit c where a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode and a.cnnQuantity>0";
				}
				else
				{
					strsql="select a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,b.cnvcInvName as 存货名称,b.cnvcSTComunitCode as 单位,cast(a.cnnQuantity/cniChangRate as numeric(12,4)) as 结存数量,cast(a.cnnStopQuantity/cniChangRate as numeric(12,4)) as 冻结数量,cast(a.cnnAvaQuantity/cniChangRate as numeric(12,4)) as 可用数量";
					strsql+=",convert(char(10),a.cndMDate,120) as 生产日期,convert(char(10),a.cndExpDate,120) as 过期日期";
					strsql+=" from tbCurrentStock a,tbInventory b,tbComputationUnit c where a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode and a.cnnQuantity>0 and convert(char(8),a.cndExpDate,112)<convert(char(8),getdate(),112)";
				}
			}
			if(this.ddlWhouse.SelectedValue!="全部")
				strsql+=" and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			else
			{
				if(this.ddlDept.SelectedValue!="全部")
					strsql+=" and cnvcWhCode in(select cnvcWhCode from tbWareHouse where cnvcDepCode='"+this.ddlDept.SelectedValue+"')";
			}
			if(this.ddlProClass.SelectedValue!="全部")
				strsql+=" and b.cnvcInvCCode='"+this.ddlProClass.SelectedValue+"'";
			else
			{
				if(this.ddlProType.SelectedValue!="全部")
					strsql+=" and b.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='"+this.ddlProType.SelectedValue+"')";
			}
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and b.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			if(this.chbSum.Checked)
			{
				strsql+=" group by a.cnvcWhCode,a.cnvcInvCode,b.cnvcInvName,b.cnvcSTComunitCode";
			}
			strsql+=" order by a.cnvcWhCode,a.cnvcInvCode";

			DataTable dtcur=Helper.Query(strsql);
			this.TableConvert(dtcur,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtcur,"单位","ComputationUnit","vcCommCode","vcCommName");
			dtcur.TableName="仓库库存报表";
			Session["QUERY"] = dtcur;
			Session["toExcel"]=dtcur;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtcur);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void ddlProType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.FillDropDownList("PClass",this.ddlProClass,"vcCommSign='"+this.ddlProType.SelectedValue+"'","全部");
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","全部");
			}
		}
	}
}
