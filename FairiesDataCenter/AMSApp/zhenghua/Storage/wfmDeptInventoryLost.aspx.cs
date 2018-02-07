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
	/// wfmDeptInventoryLost 的摘要说明。
	/// </summary>
	public class wfmDeptInventoryLost : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
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
			string strsql="select a.cnnLostSerialNo as 损耗流水,b.cnvcInvName as 损耗存货,a.cnvcComUnitCode as 单位,a.cnnLostCount as 损耗数量,a.cnnAddCount as 调增量,a.cnnReduceCount as 调减量,convert(char(10),a.cndLostDate,120) as 损耗日期,a.cnvcDeptID as 报损部门,";
			strsql+="a.cnvcWhCode as 报损仓库,a.cndMdate as 生产日期,a.cndExpDate as 过期日期,a.cnvcOperID as 操作员,a.cndOperDate as 操作时间,a.cnvcComments as 备注,(case a.cnvcInvalidFlag when '0' then '未确认' else '已确认' end) as 确认标志";
			strsql+=" from tbLostSerial a,tbInventory b where a.cnvcLostType='1' and a.cnvcInvCode=b.cnvcInvCode and a.cndLostDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlDept.SelectedValue!="")
				strsql+=" and a.cnvcDeptID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhouse.SelectedValue!="")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			strsql+=" order by a.cnnLostSerialNo";

			DataTable dtout=Helper.Query(strsql);
			dtout.Columns.Add("操作");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				if(dtout.Rows[i]["确认标志"].ToString()=="未确认")
					dtout.Rows[i]["操作"]="<a href='wfmDeptInventoryLostDetail.aspx?seno=" + dtout.Rows[i]["损耗流水"].ToString() +"'>修正</a>";
			}
			this.TableConvert(dtout,"单位","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"报损部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"报损仓库","Warehouse","vcCommCode","vcCommName");
			dtout.TableName="销售报损列表";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(this.ddlDept.SelectedValue=="")
			{
				this.Popup("无部门仓库信息，不能操作！");
				return;
			}
			else
			{
				Response.Redirect("wfmDeptInventoryLostDetail.aspx");	
			}
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
