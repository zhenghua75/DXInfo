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
	/// Summary description for wfmConsItem.
	/// </summary>
	public partial class wfmConsItemMd : wfmBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputText txtBegin;
		protected System.Web.UI.HtmlControls.HtmlInputText txtEnd;
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
					this.FillDropDownList("tbCommCode", ddlAssType, "vcCommSign ='AT'","全部");
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					this.FillDropDownList("tbCommCode", DropDownList1, "vcCommSign ='MD'","全部");

					this.FillDropDownList("tbCommCode", ddlBillType, "vcCommSign ='PT'","全部");
					this.ddlConsFlag.Items.Add(new ListItem("正常消费","0"));
					this.ddlConsFlag.Items.Add(new ListItem("已撤消","9"));
					this.ddlConsFlag.SelectedIndex=0;
					if(ls1.strLimit!="CL001")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					string strDept=ddlDept.SelectedValue;
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					busiq=new BusiComm.BusiQuery(strcons);
					DataTable dtoper=busiq.GetConsOperList(strDept,strBeginDate,strEndDate);
					this.FillDropDownList(dtoper,ddlOper,"全部");
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

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);

			Hashtable htPara=new Hashtable();
			string strCardID=txtCardID.Text.Trim();
			htPara.Add("strCardID",strCardID);
			string strAssName=txtAssName.Text.Trim();
			htPara.Add("strAssName",strAssName);
			string strSerial=txtSerial.Text.Trim();
            htPara.Add("strSerial",strSerial);
			string strAssType=ddlAssType.SelectedValue;
			string strOperName=ddlOper.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			string strConsFlag=this.ddlConsFlag.SelectedValue;
			string strBillType=this.ddlBillType.SelectedValue;

			string strDeptIdMd = this.DropDownList1.SelectedValue;
			if(strAssType=="全部")
			{
				strAssType="";
			}
			htPara.Add("strAssType",strAssType);
			if(strOperName=="全部")
			{
				strOperName="";
			}
			htPara.Add("strOperName",strOperName);
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			if(strBillType=="全部")
			{
				strBillType="";
			}
			htPara.Add("strBillType",strBillType);
			htPara.Add("strBegin",strBeginDate);
			htPara.Add("strEnd",strEndDate);
			htPara.Add("strConsFlag",strConsFlag);
			
			if(strDeptIdMd == "全部")
			{
				strDeptIdMd = "";
			}
			htPara.Add("strDeptIdMd",strDeptIdMd);
			try
			{
				double dsum=0;
				double dcashsum=0;
				DataTable dtout=busiq.GetConsQueryMd(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"付款类型","tbCommCode","vcCommSign='PT'");
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign='MD'");
					this.TableConvert(dtout,"发卡门店","tbCommCode","vcCommSign='MD'");
					dtout.TableName="消费明细";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						dsum+=Math.Round(double.Parse(dtexcel.Rows[i]["合计"].ToString()),2);
						if(dtexcel.Rows[i]["付款类型"].ToString()=="支付现金")
						{
							dcashsum+=Math.Round(double.Parse(dtexcel.Rows[i]["合计"].ToString()),2);
						}
						if(dtexcel.Rows[i][3].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i][3]="'"+dtexcel.Rows[i][3].ToString();
						}
						dtexcel.Rows[i][4]="'"+dtexcel.Rows[i][4].ToString();
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

				dsum=Math.Round(dsum,2);
				dcashsum=Math.Round(dcashsum,2);
				this.lblSum.Text="金额汇总："+dsum.ToString()+"元\r\r\r\r\r\r\r\r现金汇总："+dcashsum.ToString()+"元";
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

		protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
		{
			string strDept=ddlDept.SelectedValue;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);
			DataTable dtoper=busiq.GetConsOperList(strDept,strBeginDate,strEndDate);
			this.FillDropDownList(dtoper,ddlOper,"全部");
			Session["QUERY"] = null;
			Session["toExcel"] = null;
			this.UcPageView1.MyDataGrid.DataSource = null;
			this.UcPageView1.MyDataGrid.DataBind();
			this.UcPageView1.FootBar.Visible=false;
		}
	}
}
