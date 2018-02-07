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
	/// Summary description for wfmEmpUnitSign.
	/// </summary>
	public partial class wfmEmpUnitSign : wfmBase
	{

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;

		BusiComm.EmpBusi empb;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					ListItem li1=new ListItem("个人考勤详情","0");
					ListItem li2=new ListItem("个人考勤日志","1");
					ddlType.Items.Add(li1);
					ddlType.Items.Add(li2);
					ddlType.SelectedIndex=0;
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
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

		protected void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strmonth=txtMonth.Text.Trim();
			if(strmonth==""||strmonth.Length!=6)
			{
				this.SetErrorMsgPageBydir("查询月份为6位数，如：200801！");
				return;
			}

			string strCardID=txtCardID.Text.Trim();
			string strEmpName=txtEmpName.Text.Trim();
			if(strCardID==""&&strEmpName=="")
			{
				this.SetErrorMsgPageBydir("员工卡号和员工姓名请至少填写一个！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			empb=new BusiComm.EmpBusi(strcons);

			Hashtable htPara=new Hashtable();
			string strType=ddlType.SelectedValue;
			htPara.Add("strmonth",strmonth);
			htPara.Add("strType",strType);
			htPara.Add("strCardID",strCardID);
			htPara.Add("strEmpName",strEmpName);

			try
			{
				DataTable dtout=new DataTable();
				dtout=empb.GetSignUnitQuery(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dtexcel.Rows[i][1]="'"+dtexcel.Rows[i][1].ToString();
					}
					if(strType=="0")
					{
						dtout.TableName="个人考勤详情";
					}
					else
					{
						dtout.TableName="个人考勤日志";
						this.TableConvert(dtout,"考勤标志","tbCommCode","vcCommSign='SFlag'");
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
