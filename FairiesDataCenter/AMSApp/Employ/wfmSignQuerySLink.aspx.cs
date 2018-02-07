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
using BusiComm;
using CommCenter;

namespace AMSApp.Employ
{
	/// <summary>
	/// wfmSignQuerySLink 的摘要说明。
	/// </summary>
	public partial class wfmSignQuerySLink : wfmBase
	{
		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					if(Request.QueryString.HasKeys())
					{
						this.txtBegin.Text=Request.QueryString["begin"];
						this.txtEnd.Text=Request.QueryString["end"];
						string strDeptName=Request.QueryString["dept"];
						string stremp=Request.QueryString["emp"];

						Session.Remove("QUERY");
						Session.Remove("toExcel");
						Session.Remove("page_view");
						string strBeginDate = this.txtBegin.Text.Trim();
						string strEndDate = this.txtEnd.Text.Trim();
						if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
						{
							this.SetErrorMsgPageBydirHistory("时间不能为空！");
							return;
						}

						Hashtable htapp=(Hashtable)Application["appconf"];
						string strcons=(string)htapp["cons"];
						BusiComm.EmpBusi empb=new BusiComm.EmpBusi(strcons);

						Hashtable htPara=new Hashtable();
						htPara.Add("strDeptName",strDeptName);
						htPara.Add("strBegin",strBeginDate);
						htPara.Add("strEnd",strEndDate);
						htPara.Add("empname",stremp);

						try
						{
							DataTable dtout=new DataTable();
							dtout=empb.GetSignDetailQuery(htPara);
							if(dtout==null)
							{
								this.SetErrorMsgPageBydir("查询出错，请重试！");
								return;
							}
							else
							{
								dtout.TableName="考勤明细列表";
								DataTable dtexcel=dtout.Copy();
								Session["QUERY"] = dtout;
								for(int i=0;i<dtexcel.Rows.Count;i++)
								{
									dtexcel.Rows[i][1]="'"+dtexcel.Rows[i][1].ToString();
									if(dtexcel.Rows[i][5].ToString()!="")
									{
										dtexcel.Rows[i][5]="'"+dtexcel.Rows[i][5].ToString();
									}
									if(dtexcel.Rows[i][6].ToString()!="")
									{
										dtexcel.Rows[i][6]="'"+dtexcel.Rows[i][6].ToString();
									}
								}
								Session["toExcel"]=dtexcel;
								if(dtout.Rows.Count<=0)
								{
									btnExcel.Enabled=false;
								}
								else
								{
									btnExcel.Enabled=true;	
								}
							}

							UcPageView1.MyDataGrid.PageSize = 30;
							DataView dvOut =new DataView(dtout);
							this.UcPageView1.MyDataSource = dvOut;
							this.UcPageView1.BindGrid();
						}
						catch(Exception er)
						{
							this.clog.WriteLine(er);
							this.SetErrorMsgPageBydir("查询错误，请重试！");
							return;
						}
					}
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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

		}
		#endregion

	}
}
