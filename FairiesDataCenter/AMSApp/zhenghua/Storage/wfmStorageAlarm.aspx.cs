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
	/// wfmStorageAlarm 的摘要说明。
	/// </summary>
	public class wfmStorageAlarm : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
	
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
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE'","全部");
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.DataGrid1.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("STAlarm");
			string strsql="select c.cnvcDepCode as cnvcDept,c.cnvcWhName,d.cnvcName as cnvcInvType,a.cnvcInvCode,a.cnvcInvName,a.cnvcGroupCode,a.cnvcComUnitCode,a.cniSafeNum,a.cniLowSum,sum(b.cnnQuantity) as cnnCurCount";
			strsql+=" from tbInventory a,tbCurrentStock b,tbWarehouse c,tbNameCode d,tbProductClass e";
			strsql+=" where b.cnvcStopFlag='0' and c.cnvcDepCode='"+this.ddlDept.SelectedValue+"' and b.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and d.cnvcType='PRODUCTTYPE'";
			strsql+=" and a.cnvcInvCode=b.cnvcInvCode and b.cnvcWhCode=c.cnvcWhCode and d.cnvcCode=e.cnvcProductType and a.cnvcInvCCode=e.cnvcProductClassCode and b.cnnQuantity<=a.cniSafeNum";
			if(this.ddlProductType.SelectedValue!="全部")
				strsql+=" and d.cnvcCode='"+this.ddlProductType.SelectedValue+"'";
			strsql+=" group by c.cnvcDepCode,c.cnvcWhName,d.cnvcName,a.cnvcInvCode,a.cnvcInvName,a.cnvcGroupCode,a.cnvcComUnitCode,a.cniSafeNum,a.cniLowSum order by d.cnvcName,a.cnvcInvCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"cnvcDept","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcComUnitCode","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="库存报警清单";
			Session["STAlarm"] = dtout;
			this.DataGrid1.CurrentPageIndex = 0;
			this.DataGrid1.PageSize = 20;
			this.DataGrid1.DataSource = dtout;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
			{
				decimal safecount=decimal.Parse(e.Item.Cells[7].Text.Trim());
				decimal lowcount=decimal.Parse(e.Item.Cells[8].Text.Trim());
				decimal curcount=decimal.Parse(e.Item.Cells[9].Text.Trim());
				if(curcount<=safecount&&curcount>lowcount)
				{
					((System.Web.UI.WebControls.Image)e.Item.Cells[10].Controls[1]).ImageUrl="../../image/warning-Y.png";
				}
				if(curcount<=lowcount)
				{
					((System.Web.UI.WebControls.Image)e.Item.Cells[10].Controls[1]).ImageUrl="../../image/warning-R.png";
				}
			}
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;			
			this.DataGrid1.DataSource=(DataTable)Session["STAlarm"];
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataBind();
		}
	}
}
