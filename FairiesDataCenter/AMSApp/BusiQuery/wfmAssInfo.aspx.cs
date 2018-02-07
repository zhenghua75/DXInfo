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
using cc;
using System.IO;

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// Summary description for wfmAssInfo.
	/// </summary>
	public class wfmAssInfo : wfmBase
	{
		protected System.Web.UI.WebControls.TextBox txtAssName;
        protected System.Web.UI.WebControls.TextBox txtPhone;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtCardID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.DropDownList ddlAssType;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
        protected System.Web.UI.WebControls.Label lblSumFee;
        protected System.Web.UI.WebControls.Label lblSumIG;
		protected System.Web.UI.WebControls.DropDownList ddlAssState;
		protected System.Web.UI.HtmlControls.HtmlInputText txtBegin;

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;
		BusiComm.BusiQuery busiq;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Button btnExcel;
	
		protected string strBeginDate;
		protected string strEndDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
                    this.FillDropDownList("AssAT", ddlAssType, "vcCommSign ='AT'", "全部");
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
					this.FillDropDownList("tbCommCode", ddlAssState, "vcCommSign ='AS'","全部");
                    //this.ddlAssType.Items.Remove(this.ddlAssType.Items[3]);
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
			else
			{
				Response.Redirect("../Exit.aspx");
			}
			//Session["Login"]=null;
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
			busiq=new BusiComm.BusiQuery(strcons);

			Hashtable htPara=new Hashtable();
			string strCardID=txtCardID.Text.Trim();
			htPara.Add("strCardID",strCardID);
			string strAssName=txtAssName.Text.Trim();
			htPara.Add("strAssName",strAssName);
            string strPhone = txtPhone.Text.Trim();
            htPara.Add("strPhone", strPhone);
			string strAssType=ddlAssType.SelectedValue;
			string strAssState=ddlAssState.SelectedValue;
			string strDeptID=ddlDept.SelectedValue;
			if(strAssType=="全部")
			{
				strAssType="";
			}
			htPara.Add("strAssType",strAssType);
			if(strAssState=="全部")
			{
				strAssState="";
			}
			htPara.Add("strAssState",strAssState);
			if(strDeptID=="全部")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			
			try
			{
                double sum_Fee = 0;
                double sum_IG = 0;
				DataTable dtout=busiq.GetAssInfo(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"会员类型","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"会员状态","tbCommCode","vcCommSign='AS'");
					this.TableConvert(dtout,"门店","tbCommCode","vcCommSign='MD'");
					dtout.TableName="会员资料清单";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						if(dtexcel.Rows[i][0].ToString().Substring(0,1)!="V")
						{
							dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
						}
						dtexcel.Rows[i][2]="'"+dtexcel.Rows[i][2].ToString();
						dtexcel.Rows[i][10]="'"+dtexcel.Rows[i][10].ToString();
					}
					Session["toExcel"]=dtexcel;
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
					if(dtout.Rows.Count<=0)
					{
                        btnExcel.Enabled = false;
                        this.lblSumFee.Text = "当前余额汇总：0";
                        this.lblSumIG.Text = "当前积分汇总：0";
					}
					else
					{
                        for (int j = 0; j < dtout.Rows.Count; j++)
                        {
                            sum_Fee += Math.Round(double.Parse(dtout.Rows[j]["当前余额"].ToString()), 2);
                            sum_IG += Math.Round(double.Parse(dtout.Rows[j]["当前积分"].ToString()), 2);
                        }
					}
				}
                sum_Fee = Math.Round(sum_Fee, 2);
                this.lblSumFee.Text = "当前余额汇总：" + sum_Fee.ToString() + "元";
                this.lblSumIG.Text = "当前积分汇总：" + sum_IG.ToString();
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
