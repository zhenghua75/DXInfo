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
	/// Summary description for wfmSignQuery.
	/// </summary>
	public partial class wfmSignQuery : wfmBase
	{
		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		BusiComm.EmpBusi empb;
		protected string strExcelPath = string.Empty;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001"&&ls1.strLimit!="CL002")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
					if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					ListItem li1=new ListItem("总体情况","0");
					ListItem li2=new ListItem("明细情况","1");
					ddlType.Items.Add(li1);
					ddlType.Items.Add(li2);
					ddlType.SelectedIndex=0;
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
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string[] SchDatelistBegin=strBeginDate.Split('-');
			if(SchDatelistBegin.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}
			string strSchIDBegin=SchDatelistBegin[0];
			if(int.Parse(SchDatelistBegin[1])<10)
			{
				strSchIDBegin+="0" + SchDatelistBegin[1];
			}
			else
			{
				strSchIDBegin+=SchDatelistBegin[1];
			}
			if(int.Parse(SchDatelistBegin[2])<10)
			{
				strSchIDBegin+="0" + SchDatelistBegin[2];
			}
			else
			{
				strSchIDBegin+=SchDatelistBegin[2];
			}

			string[] SchDatelistEnd=strEndDate.Split('-');
			if(SchDatelistEnd.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}
			string strSchIDEnd=SchDatelistEnd[0];
			if(int.Parse(SchDatelistEnd[1])<10)
			{
				strSchIDEnd+="0" + SchDatelistEnd[1];
			}
			else
			{
				strSchIDEnd+=SchDatelistEnd[1];
			}
			if(int.Parse(SchDatelistEnd[2])<10)
			{
				strSchIDEnd+="0" + SchDatelistEnd[2];
			}
			else
			{
				strSchIDEnd+=SchDatelistEnd[2];
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			empb=new BusiComm.EmpBusi(strcons);

			Hashtable htPara=new Hashtable();
			string strDeptName=ddlDept.SelectedItem.Text;
			if(strDeptName=="全部")
			{
				strDeptName="";
			}
			htPara.Add("strDeptName",strDeptName);
			string strType=ddlType.SelectedValue;
			htPara.Add("strType",strType);
			htPara.Add("strBegin",strSchIDBegin);
			htPara.Add("strEnd",strSchIDEnd);

			try
			{
				DataTable dtout=new DataTable();
				switch(strType)
				{
					case "0":
						if(int.Parse(strSchIDBegin)>int.Parse(strSchIDEnd))
						{
							this.SetErrorMsgPageBydir("开始时间不能大于结束时间！");
							return;
						}

						dtout=empb.GetSignSumQuery(htPara);
						if(dtout==null)
						{
							this.SetErrorMsgPageBydir("查询出错，请重试！");
							return;
						}
						else
						{
							dtout.TableName="总体考勤情况";
							DataTable dtexcel=dtout.Copy();
							Session["QUERY"] = dtout;

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
						break;
					case "1":
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
						break;
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
