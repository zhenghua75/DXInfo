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
	/// Summary description for wfmEmpInfo.
	/// </summary>
	public partial class wfmEmpInfo : wfmBase
	{

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;
		EmpBusi ebu;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL003"||ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					this.FillDropDownList("tbCommCode", ddlstate, "vcCommSign ='ES'");
					if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
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

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			ebu=new EmpBusi(strcons);

			Hashtable htPara=new Hashtable();
			string strCardID=txtCardID.Text.Trim();
			htPara.Add("strCardID",strCardID);
			string strEmpName=txtEmpName.Text.Trim();
			htPara.Add("strEmpName",strEmpName);
			htPara.Add("strstate",ddlstate.SelectedValue);
			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			
			try
			{
				DataTable dtout=ebu.GetEmployees(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"在职情况","tbCommCode","vcCommSign='ES'");
					this.TableConvert(dtout,"学历","tbCommCode","vcCommSign='DE'");
					this.TableConvert(dtout,"当前所属门店","tbCommCode","vcCommSign='MD'");
					this.TableConvert(dtout,"职务","tbCommCode","vcCommSign='OF'");
					dtout.TableName="员工资料清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
						dtexcel.Rows[i][3]="'"+dtexcel.Rows[i][3].ToString();
						dtexcel.Rows[i][6]="'"+dtexcel.Rows[i][6].ToString();
					}
					dtexcel.Columns.Remove("操作");
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

		private void btExport_ServerClick(object sender, System.EventArgs e)
		{
			if ( Session["toExcel"] != null )
			{
				strExcelPath = "javascript:OpenNewWin('../DataGridToExcel.aspx')";
			}
			else
			{
				this.SetErrorMsgPageBydir("表格没有数据可以导出!");
				return;
			}
		}

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }

	}
}
