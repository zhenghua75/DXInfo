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

namespace AMSApp.Storage.Report
{
	/// <summary>
	/// Summary description for wfmStorageCheckLog.
	/// </summary>
	public class wfmStorageCheckLog : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.Button btnExcel;

		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		protected string strExcelPath = string.Empty;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept",this.ddlDept);
					this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE'","全部");
					this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");

					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlDept.Enabled=false;
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
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

			string strDeptID=this.ddlDept.SelectedValue;
			string strProductType=this.ddlProductType.SelectedValue;
			string strProductClass=this.ddlProductClass.SelectedValue;

			Hashtable htPara=new Hashtable();
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strProductType",strProductType);
			htPara.Add("strProductClass",strProductClass);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetStorageCheckLog(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBy2dir("查询出错，请重试！");
					return;
				}
				else
				{
					dtout.TableName="库存盘点日志";
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
				
				UcPageView1.MyDataGrid.PageSize = 20;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBy2dir("查询错误，请重试！");
				return;
			}
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlProductClass.Items.Clear();
			this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");
		}
	}
}
