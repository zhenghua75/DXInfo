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
	/// wfmStorageHisReport 的摘要说明。
	/// </summary>
	public class wfmStorageHisReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btExcel;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProType;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;

		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("toExcel");
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

			DateTime dtEndNext=DateTime.Parse(strEndDate);
			dtEndNext=dtEndNext.AddDays(1);
			string strshortEndNextDate=dtEndNext.Year.ToString();
			if(dtEndNext.Month<10)
				strshortEndNextDate+="0"+dtEndNext.Month.ToString();
			else
				strshortEndNextDate+=dtEndNext.Month.ToString();
			if(dtEndNext.Day<10)
				strshortEndNextDate+="0"+dtEndNext.Day.ToString();
			else
				strshortEndNextDate+=dtEndNext.Day.ToString();

			string strsql="";
			if(this.ddlWhouse.SelectedValue=="全部")
				strsql="exec sp_QueryHisStorageReport '%%','"+strshortBeginDate+"','"+strshortEndDate+"','"+strshortEndNextDate+"',";
			else
				strsql="exec sp_QueryHisStorageReport '"+this.ddlWhouse.SelectedValue+"%','"+strshortBeginDate+"','"+strshortEndDate+"','"+strshortEndNextDate+"',";
			if(this.ddlProType.SelectedValue=="全部")
				strsql+="'%%',";
			else
				strsql+="'"+this.ddlProType.SelectedValue+"%',";
			if(this.txtInvName.Text.Trim()=="")
				strsql+="'%%'";
			else
				strsql+="'%"+this.txtInvName.Text.Trim()+"%'";

			DataTable dtcur=Helper.Query(strsql);
			this.TableConvert(dtcur,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtcur,"单位","ComputationUnit","vcCommCode","vcCommName");
			dtcur.TableName="历史库存报表";
			Session["QUERY"] = dtcur;
			Session["toExcel"]=dtcur;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtcur);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
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
