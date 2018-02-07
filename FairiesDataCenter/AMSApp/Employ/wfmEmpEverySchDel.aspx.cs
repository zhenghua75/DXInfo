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
	/// Summary description for wfmEmpEverySchDel.
	/// </summary>
	///

	public partial class wfmEmpEverySchDel : wfmBase
	{
		EmpBusi ebu;
		protected System.Web.UI.WebControls.Label lblDeptID;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack )
			{
				string strSchID="";
				string strEmpName="";
				string strClass="";
				if(Request.QueryString.HasKeys())
				{
					strSchID=Request.QueryString["date"];
					strEmpName=Request.QueryString["emp"];
					strClass=Request.QueryString["class"];
				}
				else
				{
					this.SetErrorMsgPageBydir("参数无效，删除失败，请重试！");
					return;
				}

				if(strSchID==""||strEmpName==""||strClass=="")
				{
					this.SetErrorMsgPageBydir("参数无效，删除失败，请重试！");
					return;
				}
				else
				{
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					ebu=new EmpBusi(strcons);

					if(ebu.SchedEmpDailyEveryDel(strSchID,strEmpName,strClass))
					{
						this.SetSuccMsgPageBydir("删除员工排班成功！","Employ/wfmWorkDailyEvery.aspx");
						return;
					}
					else
					{
						this.SetErrorMsgPageBydirHistory("删除员工排班失败！");
						return;
					}
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
		}
		#endregion
	}
}
