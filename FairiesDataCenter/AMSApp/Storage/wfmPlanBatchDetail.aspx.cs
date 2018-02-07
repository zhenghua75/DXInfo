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
using System.Text.RegularExpressions;
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmPlanBatchDetail.
	/// </summary>
	public class wfmPlanBatchDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btcancel;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.DropDownList ddlBatch;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.TextBox txtSumFee;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		protected string strBeginDate;
		protected System.Web.UI.WebControls.TextBox txtMonth;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					string strProductCode=Request.QueryString["PID"];
					string strProductName=Request.QueryString["PName"];
					string strUnit=Request.QueryString["PUnit"];
					string strMonth=Request.QueryString["month"];
					if(strProductCode==null||strProductCode==""||strProductName==null||strProductName==""||strUnit==null||strUnit=="")
					{
						this.SetErrorMsgPageBydir("产品信息有误，请重试！");
						return;
					}
					else
					{
						this.txtProductCode.Text=strProductCode;
						this.txtProductName.Text=strProductName;
						this.txtUnit.Text=strUnit;
						this.txtMonth.Text=strMonth;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					this.txtProductCode.ReadOnly=true;
					this.txtProductName.ReadOnly=true;
					this.txtUnit.ReadOnly=true;
					this.txtMonth.ReadOnly=true;
					this.ddlBatch.Items.Add(new ListItem("第一批","1"));
					this.ddlBatch.Items.Add(new ListItem("第二批","2"));
					this.ddlBatch.Items.Add(new ListItem("第三批","3"));
					this.ddlBatch.Items.Add(new ListItem("第四批","4"));
					this.ddlBatch.SelectedIndex=0;
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
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
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btcancel_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmStockPlan.aspx");
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			string strProductCode=this.txtProductCode.Text.Trim();
			string strProductName=this.txtProductName.Text.Trim();
			string strUnit=this.txtUnit.Text.Trim();
			string strBatch=this.ddlBatch.SelectedValue;
			string strStartDate=strBeginDate;
			string strCount=this.txtCount.Text.Trim();
			string strSumFee=this.txtSumFee.Text.Trim();
			string strMonth=this.txtMonth.Text.Trim();
			if(strCount==""||!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("数量必须是数字！");
				return;
			}
			if(strSumFee==""||!Regex.IsMatch(strSumFee,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("费用必须是数字！");
				return;
			}

			Hashtable htpara=new Hashtable();
			htpara.Add("strProductCode",strProductCode);
			htpara.Add("strProductName",strProductName);
			htpara.Add("strUnit",strUnit);
			htpara.Add("strBatch",strBatch);
			htpara.Add("strStartDate",strStartDate);
			htpara.Add("strCount",strCount);
			htpara.Add("strSumFee",strSumFee);
			htpara.Add("strMonth",strMonth);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.UpdateSotckPlanBatch(htpara))
				{
					this.SetSuccMsgPageBydir("修改成功！","Storage/wfmStockPlan.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("修改采购计划时发生错误，请重试！");
					return;
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
