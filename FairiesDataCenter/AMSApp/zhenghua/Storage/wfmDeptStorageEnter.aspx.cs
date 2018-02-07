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
	/// wfmDeptStorageEnter 的摘要说明。
	/// </summary>
	public class wfmDeptStorageEnter : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtEnterID;
		protected System.Web.UI.WebControls.Label Label2;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
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
				this.ddlState.Items.Add(new ListItem("未入库","0"));
				this.ddlState.Items.Add(new ListItem("已入库","1"));
				this.ddlState.SelectedIndex=0;
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
				strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string strsql="select cnnRdID as 主表标识,cnvcCode as 分货入库单号,cnvcDepID as 部门,cnvcWhCode as 仓库,cnvcMaker as 制单人,cndARVDate as 入库日期,";
			strsql+="cnvcState as 状态 from tbRdRecord where cnvcRdCode='RD007'";
			strsql+=" and cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.txtEnterID.Text.Trim()!="")
				strsql+=" and cnvcCode='"+this.txtEnterID.Text.Trim()+"'";
			if(this.ddlDept.SelectedValue!="全部")
				strsql+=" and cnvcDepID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhouse.SelectedValue!="全部")
				strsql+=" and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			if(this.ddlState.SelectedValue!="")
				strsql+=" and cnvcState='"+this.ddlState.SelectedValue+"'";
			strsql+=" order by cnvcCode";

			DataTable dtout=Helper.Query(strsql);
			dtout.Columns.Add("单据内容");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["单据内容"]="<a href='wfmDeptStorageEnterAdd.aspx?rdid=" + dtout.Rows[i]["主表标识"].ToString() +"'>内容</a>";
			}
			dtout.Columns.Add("单据明细");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["单据明细"]="<a href='wfmDeptStorageEnterDetail.aspx?rdid=" + dtout.Rows[i]["主表标识"].ToString()+"&code="+dtout.Rows[i]["分货入库单号"].ToString()+"&dept="+dtout.Rows[i]["部门"].ToString()+"&whid="+dtout.Rows[i]["仓库"].ToString()+"'>明细</a>";
			}
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"状态","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			dtout.TableName="分货入库单列表";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmDeptStorageEnterAdd.aspx");
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
