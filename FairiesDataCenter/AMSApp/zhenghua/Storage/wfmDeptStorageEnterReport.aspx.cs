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
	/// wfmDeptStorageEnterReport 的摘要说明。
	/// </summary>
	public class wfmDeptStorageEnterReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Button btExcel;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				this.FillDropDownList("NewDept",this.ddlDept,"","全部");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string strsql="select a.cnvcCode as 分货单号,a.cnvcDepID as 入库部门,a.cnvcWhCode as 入库仓库,d.cnvcInvName as 存货名称,b.cnvcComunitCode as 计量单位,c.cnnAssignCount as 分货数量,";
			strsql+="b.cnnQuantity as 入库数量,convert(char(10),a.cndARVDate,120) as 入库日期,a.cnvcState as 单据状态 from tbRdRecord a,tbRdRecordDetail b,tbAssignDetail c,tbInventory d";
			strsql+=" where a.cnnRdID=b.cnnRdID and b.cnvcPOID=cast(c.cnnAssignSerialNo as varchar(12)) and b.cnnMPoID=c.cnnOrderSerialNo and b.cnvcInvCode=c.cnvcInvCode and b.cnvcInvCode=d.cnvcInvCode";
			strsql+=" and a.cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlDept.SelectedValue!="全部")
				strsql+=" and a.cnvcDepID='"+this.ddlDept.SelectedValue+"'";
			strsql+=" order by a.cnvcCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"入库部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"入库仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"计量单位","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"单据状态","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			dtout.TableName="分货入库报表";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
