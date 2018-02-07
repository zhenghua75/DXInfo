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
using System.Collections.Specialized;

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// Summary description for wfmBusiIncome.
	/// </summary>
	public partial class wfmBusiIncome : wfmBase
	{

		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		BusiComm.BusiQuery busiq;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit!="CL001")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					if(ls1.strLimit!="CL001")
					{
						ListItem li = ddlDept.Items.FindByValue(ls1.strDeptID);//
                        if (li != null)
                        {
                            li.Selected = true;
                        }
						ddlDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToString("yyyy-M-d");//.ToShortDateString();
                    strEndDate = DateTime.Now.ToString("yyyy-M-d");//.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
				if(this.UcPageView1.MyDataGrid.DataSource!=null)
				{
					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
					{
						UcPageView1.FootBar.Visible = true;
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
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
			if(strBeginDate==""||strBeginDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}

			string[] beginlist=strBeginDate.Split('-');
			string[] endlist=strEndDate.Split('-');
			if(beginlist.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}

			string strBegin=strBeginDate;
			string strEnd=strEndDate;
			strBegin=beginlist[0];
			if(int.Parse(beginlist[1])<10)
			{
				strBegin+="0" + int.Parse(beginlist[1].ToString()).ToString();
			}
			else
			{
				strBegin+=beginlist[1];
			}
			if(int.Parse(beginlist[2])<10)
			{
				strBegin+="0" + int.Parse(beginlist[2].ToString()).ToString();
			}
			else
			{
				strBegin+=beginlist[2];
			}

			DateTime dtyestoday=new DateTime(int.Parse(beginlist[0]),int.Parse(beginlist[1]),int.Parse(beginlist[2]));
			dtyestoday=dtyestoday.AddDays(-1);
			string strYestoday=dtyestoday.Year.ToString();
			if(dtyestoday.Month<10)
			{
				strYestoday+="0" + int.Parse(dtyestoday.Month.ToString()).ToString();
			}
			else
			{
				strYestoday+=dtyestoday.Month.ToString();
			}
			if(dtyestoday.Day<10)
			{
				strYestoday+="0" + int.Parse(dtyestoday.Day.ToString()).ToString();
			}
			else
			{
				strYestoday+=dtyestoday.Day.ToString();
			}

			strEnd=endlist[0];
			if(int.Parse(endlist[1])<10)
			{
				strEnd+="0" + int.Parse(endlist[1].ToString()).ToString();
			}
			else
			{
				strEnd+=endlist[1];
			}
			if(int.Parse(endlist[2])<10)
			{
				strEnd+="0" + int.Parse(endlist[2].ToString()).ToString();
			}
			else
			{
				strEnd+=endlist[2];
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);
			string strDeptID=ddlDept.SelectedValue;
			string strDeptName=ddlDept.SelectedItem.Text.Trim();
			Hashtable htPara=new Hashtable();
			if(strDeptID=="全部")
			{
				strDeptID="%%";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBegin",strBegin);
			htPara.Add("strEnd",strEnd);
			htPara.Add("strYestoday",strYestoday);
			
			try
			{
				DataTable dtout=busiq.BusiIncomeReport(htPara,strDeptName,(DataTable)Application["tbCommCode"]);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					if(strDeptName=="全部")
					{
						dtout.TableName="业务量报表-所有门店";
					}
					else
					{
						dtout.TableName="业务量报表-"+strDeptName;
					}
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] =dtout;
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

				UcPageView1.MyDataGrid.PageSize = 1000;
				DataView dvOut1 =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut1;
				this.UcPageView1.BindGrid();
				this.UcPageView1.FootBar.Visible=false;
				if(strDeptName!="全部")
				{
					DataTable dtat=(DataTable)Application["tbCommCode"];
					DataView dvat=dtat.DefaultView;
					dvat.RowFilter="vcCommSign='AT'";
					int rowind=15+dvat.Count-1;
					this.UcPageView1.MyDataGrid.Items[rowind].BackColor=Color.SkyBlue;
					this.UcPageView1.MyDataGrid.Items[rowind+4+dvat.Count].BackColor=Color.SkyBlue;
					this.UcPageView1.MyDataGrid.Items[rowind+8+dvat.Count*2].BackColor=Color.SkyBlue;
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
