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
	/// wfmCostAccount 的摘要说明。
	/// </summary>
	public class wfmCostAccount : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlQueryType;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Button btnInvAccount;
		protected System.Web.UI.WebControls.Button btProductAccount;

		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.ddlQueryType.Items.Add(new ListItem("原材料成本核算","原材料成本核算"));
				this.ddlQueryType.Items.Add(new ListItem("成品成本核算","成品成本核算"));
				this.ddlQueryType.SelectedIndex=0;
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
			this.btnInvAccount.Click += new System.EventHandler(this.btnInvAccount_Click);
			this.btProductAccount.Click += new System.EventHandler(this.btProductAccount_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			string strbegin=DateTime.Now.Year.ToString()+"-"+DateTime.Now.Month.ToString()+"-1";
			string strEnd=DateTime.Now.Year.ToString()+"-"+DateTime.Now.AddMonths(1).Month.ToString()+"-1";
			string strsql="select top 10 cnnOperSerialNo as 操作流水,cnvcOperType as 操作类型,cnvcOperID as 操作员编号,b.vcOperName as 操作员名称,cnvcDeptID as 部门,";
			strsql+="cndOperDate as 操作时间,cnvcComments as 描述 from tbOperLog a,tbLogin b where cnvcOperType='"+this.ddlQueryType.SelectedValue+"' and a.cnvcOperID=b.vcLoginID";
			strsql+=" and a.cndOperDate between '"+strbegin+"' and '"+strEnd+"' order by cnnOperSerialNo desc";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			dtout.TableName="成本核算操作日志";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 10;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnInvAccount_Click(object sender, System.EventArgs e)
		{
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "原材料成本核算";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.InventoryCostAccount(ol,"mate");
			if(ret > 0 )
			{
				this.Popup("原材料成本核算成功！");
			}
			else
			{
				this.Popup("原材料成本核算失败！");
			}
		}

		private void btProductAccount_Click(object sender, System.EventArgs e)
		{
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "成品成本核算";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.InventoryCostAccount(ol,"prod");
			if(ret > 0 )
			{
				this.Popup("成品成本核算成功！");
			}
			else
			{
				this.Popup("成品成本核算失败！");
			}
		}
	}
}
