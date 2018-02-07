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
	/// wfmPoStockReturnReport 的摘要说明。
	/// </summary>
	public class wfmPoStockReturnReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
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
				this.FillDropDownList("Provider",this.ddlProvider,"","全部");
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
			string strsql="select a.cnvcCode as 退货单号,a.cnvcDepID as 部门,a.cnvcWhCode as 仓库,b.cnvcPOID as 对应采购单号,b.cnvcProviderID as 供应商,b.cnvcInvCode as 存货编码,";
			strsql+="c.cnvcInvName as 存货名称,b.cnvcComunitCode as 单位,b.cnnQuantity as 退货数量,b.cnnPrice as 单价,b.cnnCost as 金额";
			strsql+=" from tbRdRecord a,tbRdRecordDetail b,tbInventory c where a.cnvcRdCode='RD002' and a.cnnRdID=b.cnnRdID and a.cnvcState='1' and b.cnvcInvCode=c.cnvcInvCode";
			strsql+=" and a.cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlProvider.SelectedValue!="全部")
				strsql+=" and b.cnvcProviderID='"+this.ddlProvider.SelectedValue+"'";
			strsql+=" order by a.cnvcCode,a.cnvcDepID,a.cnvcWhCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"供应商","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtout,"单位","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="采购退货报表";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
