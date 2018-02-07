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

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmProviderDetail.
	/// </summary>
	public class wfmProviderDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btcancel;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.TextBox txtProviderCode;
		protected System.Web.UI.WebControls.TextBox txtProviderName;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtLinkName;
		protected System.Web.UI.WebControls.TextBox txtLinkPhone;
		protected System.Web.UI.WebControls.Button btnFind;
		protected System.Web.UI.WebControls.TextBox txtProviderAbbName;
		protected System.Web.UI.WebControls.DropDownList ddlTrade;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.TextBox txtQualification;
		protected System.Web.UI.WebControls.DropDownList ddlActiveFlag;
		protected System.Web.UI.WebControls.TextBox txtFax;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.DropDownList ddlCredit;
		protected System.Web.UI.WebControls.TextBox txtPostCode;

		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			string strPVid=Request.QueryString["PVID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);

			if(!IsPostBack)
			{
				this.btnFind.Visible=false;
				if(strPVid==""||strPVid==null)
				{
					this.btAdd.Enabled=true;
					this.btMod.Enabled=false;
					lbltitle.Text="新供应商档案录入";
					this.ddlCredit.Items.Add(new ListItem("好","0"));
					this.ddlCredit.Items.Add(new ListItem("一般","1"));
					this.ddlCredit.Items.Add(new ListItem("不好","2"));
					this.ddlCredit.SelectedIndex=0;
					this.ddlActiveFlag.Items.Add(new ListItem("有效","1"));
					this.ddlActiveFlag.Items.Add(new ListItem("失效","0"));
					this.ddlActiveFlag.SelectedIndex=0;
					this.txtProviderCode.Text="P-----";
					this.txtProviderCode.Enabled=false;
				}
				else
				{
					this.btnFind.Enabled=false;
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.ddlCredit.Items.Add(new ListItem("好","0"));
					this.ddlCredit.Items.Add(new ListItem("一般","1"));
					this.ddlCredit.Items.Add(new ListItem("不好","2"));
					this.ddlCredit.SelectedIndex=0;
					this.ddlActiveFlag.Items.Add(new ListItem("有效","1"));
					this.ddlActiveFlag.Items.Add(new ListItem("失效","0"));
					this.ddlActiveFlag.SelectedIndex=0;

					CMSMStruct.ProviderStruct ps1=StoBusi.GetProviderDetailOne(strPVid);
					this.txtProviderCode.Text=ps1.strPrvdCode;
					this.txtProviderName.Text=ps1.strPrvdName;
					this.txtProviderAbbName.Text=ps1.strPrvdAbbName;
					this.ddlCredit.SelectedIndex=this.ddlCredit.Items.IndexOf(this.ddlCredit.Items.FindByValue(ps1.strPrvdCredit));
					this.txtQualification.Text=ps1.strPrvdQualification;
					this.ddlActiveFlag.SelectedIndex=this.ddlActiveFlag.Items.IndexOf(this.ddlActiveFlag.Items.FindByValue(ps1.strActiveFlag));
					this.txtLinkName.Text=ps1.strPrvdLinkName;
					this.txtLinkPhone.Text=ps1.strPrvdPhone;
					this.txtFax.Text=ps1.strPrvdFax;
					this.txtEmail.Text=ps1.strPrvdEmail;
					this.txtPostCode.Text=ps1.strPostCode;
					this.txtAddress.Text=ps1.strAddress;
					lbltitle.Text="供应商档案修改";
					Session["psold"]=ps1;
					this.txtProviderCode.Enabled=false;
					this.txtProviderName.Enabled=false;
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
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStruct ps1=new CMSMStruct.ProviderStruct();
			ps1.strPrvdCode=this.txtProviderCode.Text.Trim();
			ps1.strPrvdName=this.txtProviderName.Text.Trim();
			ps1.strPrvdAbbName=this.txtProviderAbbName.Text.Trim();
			ps1.strPrvdCredit=this.ddlCredit.SelectedValue;
			ps1.strPrvdQualification=this.txtQualification.Text.Trim();
			ps1.strActiveFlag=this.ddlActiveFlag.SelectedValue;
			ps1.strPrvdLinkName=this.txtLinkName.Text.Trim();
			ps1.strPrvdPhone=this.txtLinkPhone.Text.Trim();
			ps1.strPrvdFax=this.txtFax.Text.Trim();
			ps1.strPrvdEmail=this.txtEmail.Text.Trim();
			ps1.strPostCode=this.txtPostCode.Text.Trim();
			ps1.strAddress=this.txtAddress.Text.Trim();
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			ps1.strPrvdCreater=ls1.strOperName;
			ps1.strOperName=ls1.strOperName;

			if(ps1.strPrvdName==""||ps1.strPrvdName.Length>20)
			{
				this.Popup("供应商名称不能为空或长度过长！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				string strcount=StoBusi.IsExistProvider(ps1.strPrvdName);
				if(strcount!="0")
				{
					this.Popup("该供应商名称已经存在，请使用不同的名称！");
					return;
				}

				if(StoBusi.NewProviderAdd(ps1))
				{
					this.SetSuccMsgPageBydir("新增供应商档案成功！","Storage/wfmProviderDetail.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("新增供应商档案时发生错误，请重试！");
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

		private void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStruct ps1=new CMSMStruct.ProviderStruct();
			ps1.strPrvdCode=this.txtProviderCode.Text.Trim();
			ps1.strPrvdName=this.txtProviderName.Text.Trim();
			ps1.strPrvdAbbName=this.txtProviderAbbName.Text.Trim();
			ps1.strAddress=this.txtAddress.Text.Trim();
			ps1.strPostCode=this.txtPostCode.Text.Trim();
			ps1.strPrvdPhone=this.txtLinkPhone.Text.Trim();
			ps1.strPrvdFax=this.txtFax.Text.Trim();
			ps1.strPrvdEmail=this.txtEmail.Text.Trim();
			ps1.strPrvdLinkName=this.txtLinkName.Text.Trim();
			ps1.strPrvdCredit=this.ddlCredit.SelectedValue;
			ps1.strPrvdQualification=this.txtQualification.Text.Trim();
			ps1.strActiveFlag=this.ddlActiveFlag.SelectedValue;
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			ps1.strOperName=ls1.strOperName;

			if(ps1.strPrvdCode==""||ps1.strPrvdCode.Length>=10)
			{
				this.SetErrorMsgPageBydirHistory("供应商编码不能为空或长度过长！");
				return;
			}
			if(ps1.strPrvdName==""||ps1.strPrvdName.Length>20)
			{
				this.SetErrorMsgPageBydirHistory("供应商名称不能为空或长度过长！");
				return;
			}

			CMSMStruct.ProviderStruct psold=(CMSMStruct.ProviderStruct)Session["psold"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.ModProviderInfo(ps1,psold))
				{
					this.SetSuccMsgPageBydir("修改供应商资料成功！","Storage/wfmProvider.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("修改供应商资料时发生错误，请重试！");
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

		private void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmProvider.aspx");
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
//			string strProviderCode=this.txtProviderCode.Text.Trim();
//			if(strProviderCode=="")
//			{
//				this.SetErrorMsgPageBydirHistory("请输入供应商编码！");
//				return;
//			}
//			Hashtable htapp=(Hashtable)Application["appconf"];
//			string strcons=(string)htapp["cons"];
//			StoBusi=new BusiComm.StorageBusi(strcons);
//			try
//			{
//				string strProviderName=StoBusi.IsExistProvider(strProviderCode);
//				if(strProviderName=="")
//				{
//					this.txtProviderName.Text="";
//					this.txtProviderName.Enabled=true;
//				}
//				else
//				{
//					this.txtProviderName.Text=strProviderName;
//					this.txtProviderName.Enabled=false;
//				}
//			}
//			catch(Exception er)
//			{
//				this.clog.WriteLine(er);
//				this.SetErrorMsgPageBydir("查询错误，请重试！");
//				return;
//			}
		}
	}
}
