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
	/// wfmStorageBalanceDetailReport 的摘要说明。
	/// </summary>
	public class wfmStorageBalanceDetailReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button btExcel;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			string strWhCode=Request.QueryString["whcode"];
			string strInvCode=Request.QueryString["Invcode"];
			string strInvName=Request.QueryString["Invname"];
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				this.FillDropDownList("Warehouse",this.ddlWhouse,"vcCommCode='"+strWhCode+"'");
				this.txtInvCode.Text=strInvCode;
				this.txtInvName.Text=strInvName;
				this.ddlWhouse.Enabled=false;
				this.txtInvCode.Enabled=false;
				this.txtInvName.Enabled=false;
				int line1=strBeginDate.IndexOf("-",0);
				int line2=strBeginDate.IndexOf("-",line1+1);
				string strshortBeginDate=strBeginDate.Substring(0,4);
				if(int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1)))<10)
					strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
				else
					strshortBeginDate+=int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
				if(int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1)))<10)
					strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();
				else
					strshortBeginDate+=int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();

				line1=strEndDate.IndexOf("-",0);
				line2=strEndDate.IndexOf("-",line1+1);
				string strshortEndDate=strEndDate.Substring(0,4);
				if(int.Parse(strEndDate.Substring(line1+1,line2-(line1+1)))<10)
					strshortEndDate+="0"+int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
				else
					strshortEndDate+=int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
				if(int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1)))<10)
					strshortEndDate+="0"+int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
				else
					strshortEndDate+=int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();

				this.DBBind(strshortBeginDate,strshortEndDate);
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
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind(string strbegin,string strend)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strsql="";
			string strProType=Helper.Query("select cnvcProductType from tbProductClass where cnvcProductClassCode=(select cnvcInvCCode from tbInventory where cnvcInvCode='"+this.txtInvCode.Text.Trim()+"')").Rows[0][0].ToString();
			if(strProType=="FINALPRODUCT")
			{
				strsql="select a.cndDate as 日期,a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,c.cnvcInvName as 存货名称,b.cnvcComunitName as 单位,a.cnnQuantity as 盘点量,";
				strsql+="a.cnnCheckEnter as 盘点入库,a.cnnPoEnter as 采购入库,a.cnnPoReturn as 采购退货,a.cnnProductUse as 生产使用,a.cnnProductLost as 生产损耗,";
				strsql+="a.cnnProductEnter as 生产入库,a.cnnAssgOut as 分货出库,a.cnnAssgEnter as 分货入库,a.cnnMoveEnter as 调拨入库,a.cnnMoveOut as 调拨出库,";
				strsql+="a.cnnTranLost as 其中运输损耗,a.cnnConsCount as 销售,a.cnnConsLost as 销售损耗,a.cnnExpLost as 过期损耗,cnnProDiff as 差异";
				strsql+=" from tbStorageBalanceRelationDetail a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode and b.cnvcComunitCode=c.cnvcSTComunitCode";
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='FINALPRODUCT')";
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"' and a.cndDate between '"+strbegin+"' and '"+strend+"'";
			}
			else
			{
				strsql="select a.cndDate as 日期,a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,c.cnvcInvName as 存货名称,b.cnvcComunitName as 单位,a.cnnQuantity as 盘点量,";
				strsql+="a.cnnCheckEnter as 盘点入库,a.cnnPoEnter as 采购入库,a.cnnPoReturn as 采购退货,a.cnnProductUse as 生产使用,a.cnnProductLost as 生产损耗,";
				strsql+="a.cnnMoveEnter as 调拨入库,a.cnnMoveOut as 调拨出库,a.cnnTranLost as 其中运输损耗,a.cnnConsCount as 销售,a.cnnConsLost as 销售损耗,";
				strsql+="a.cnnExpLost as 过期损耗,cnnMatDiff  as 差异";
				strsql+=" from tbStorageBalanceRelationDetail a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode ";
				strsql+=" and b.cnvcComunitCode=c.cnvcSTComunitCode and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType<>'FINALPRODUCT')";
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"' and a.cndDate between '"+strbegin+"' and '"+strend+"'";
			}
			strsql+=" order by a.cndDate,a.cnvcInvCode,a.cnvcWhCode";

			DataTable dtbalance=Helper.Query(strsql);
			this.TableConvert(dtbalance,"仓库","Warehouse","vcCommCode","vcCommName");
			dtbalance.TableName="库存平衡关系明细报表";
			Session["QUERY"] = dtbalance;
			Session["toExcel"]=dtbalance;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtbalance);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			int line1=strBeginDate.IndexOf("-",0);
			int line2=strBeginDate.IndexOf("-",line1+1);
			string strshortBeginDate=strBeginDate.Substring(0,4);
			if(int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1)))<10)
				strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
			else
				strshortBeginDate+=int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
			if(int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1)))<10)
				strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();
			else
				strshortBeginDate+=int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();

			line1=strEndDate.IndexOf("-",0);
			line2=strEndDate.IndexOf("-",line1+1);
			string strshortEndDate=strEndDate.Substring(0,4);
			if(int.Parse(strEndDate.Substring(line1+1,line2-(line1+1)))<10)
				strshortEndDate+="0"+int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
			else
				strshortEndDate+=int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
			if(int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1)))<10)
				strshortEndDate+="0"+int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
			else
				strshortEndDate+=int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
			this.DBBind(strshortBeginDate,strshortEndDate);
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmStorageBalanceReport.aspx");
		}
	}
}
