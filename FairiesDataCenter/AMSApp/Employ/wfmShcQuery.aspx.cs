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
using CommCenter;
using BusiComm;

namespace AMSApp.Employ
{
	/// <summary>
	/// Summary description for wfmShcQuery.
	/// </summary>
	public partial class wfmShcQuery : wfmBase
	{

		EmpBusi ebu;
		protected string strSchDate;
		protected string strManager;

		protected ucPageView UcPageView1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
//					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//					if(ls1.strLimit=="CL004")
//					{
//						this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//						return;
//					}
					strSchDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("page_view");
				}
				else
				{
					strSchDate = Request.Form["txtSchDate"].ToString();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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

		}
		#endregion

		protected void btquery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			string[] SchDatelist=strSchDate.Split('-');
			if(SchDatelist.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}
			string strSchID=SchDatelist[0];
			if(int.Parse(SchDatelist[1])<10)
			{
				strSchID+="0" + SchDatelist[1];
			}
			else
			{
				strSchID+=SchDatelist[1];
			}
			if(int.Parse(SchDatelist[2])<10)
			{
				strSchID+="0" + SchDatelist[2];
			}
			else
			{
				strSchID+=SchDatelist[2];
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			ebu=new EmpBusi(strcons);

			if(strSchDate=="")
			{
				this.SetErrorMsgPageBydir("请选择要查询排班的日期！");
				return;
			}

			try
			{
				DataTable dtout=ebu.GetEmpSchLog(strSchID);
				if(dtout==null||dtout.Rows.Count<=0)
				{
					this.SetErrorMsgPageBydir("查询排班表出错！");
					return;
				}
				else
				{
					dtout.TableName="排班情况";
					Session["QUERY"] = dtout;
					UcPageView1.MyDataGrid.PageSize = 30;
					DataView dvOut =new DataView(dtout);
					this.UcPageView1.MyDataSource = dvOut;
					this.UcPageView1.BindGrid();
				}
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
