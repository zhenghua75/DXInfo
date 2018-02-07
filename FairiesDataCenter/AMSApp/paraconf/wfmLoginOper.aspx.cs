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
	/// Summary description for wfmLoginOper.
	/// </summary>
	public class wfmLoginOper : wfmBase
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtLoginName;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected ucPageView UcPageView1;


		protected string strExcelPath = string.Empty;
		protected System.Web.UI.WebControls.Button btnExcel;
		BusiComm.Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
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
					this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD'","全部");
					if(ls1.strLimit!="CL001")
					{
						ListItem li = ddlDept.Items.FindByValue(ls1.strDeptID);
                        if (li != null)
                        {
                            li.Selected = true;
                        }
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			Hashtable htPara=new Hashtable();
			string strLoginName=txtLoginName.Text.Trim();
			htPara.Add("strLoginName",strLoginName);
			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			
			try
			{
				DataTable dtout=m1.GetLoginOper(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"门店","AllMD");
					this.TableConvert(dtout,"查看权限","tbCommCode","vcCommSign='CLT'");
					dtout.TableName="网站操作员清单";
					DataTable dtexcel=dtout.Copy();
                    dtout.Columns.Remove("功能权限");
					Session["QUERY"] = dtout;
					dtexcel.Columns.Remove("功能权限");
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

		private void Button2_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmOperDetail.aspx");
		}

	}
}
