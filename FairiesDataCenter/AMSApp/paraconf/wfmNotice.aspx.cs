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


namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmNotice.
	/// </summary>
	public class wfmNotice : wfmBase
	{
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.TextBox txtContent;


		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.Button btnExcel;
		BusiComm.Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			else
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if(ls1.strLimit!="CL001")
				{
					this.btAdd.Enabled=false;
					btnExcel.Enabled=false;
				}
				if (!IsPostBack )
				{
					if(ls1.strLimit!="CL001")
					{
						this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD' and vcCommCode='"+ls1.strDeptID+"'");
					}
					else
					{
						this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btQuery_Click(object sender, System.EventArgs e)
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

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);

			Hashtable htPara=new Hashtable();
			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1.strLimit!="CL001")
			{
				strDeptID=ls1.strDeptID+"','all";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strContent",txtContent.Text.Trim());
			
			try
			{
				DataTable dtout=m1.GetNotice(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"发往门店","tbCommCode","vcCommSign='MD'");
					dtout.TableName="系统通知清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					dtexcel.Columns.Remove("操作");
					Session["toExcel"]=dtexcel;
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						if(ls1.strLimit=="CL001")
						{
							btnExcel.Enabled=true;
						}
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

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmNoticeDetail.aspx");
		}
	}
}
