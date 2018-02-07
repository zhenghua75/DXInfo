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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// wfmAssMod ��ժҪ˵����
	/// </summary>
	public class wfmAssMod : wfmBase
	{
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.TextBox txtCardID;
		protected System.Web.UI.WebControls.TextBox txtAssID;
		protected System.Web.UI.WebControls.TextBox txtAssName;
		protected System.Web.UI.WebControls.TextBox txtSpell;
		protected System.Web.UI.WebControls.TextBox txtAssNbr;
		protected System.Web.UI.WebControls.TextBox txtAssType;
		protected System.Web.UI.WebControls.TextBox txtLinkPhone;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtLinkAddress;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.DropDownList ddlAssType;

		BusiComm.BusiQuery busi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			string strAssid=Request.QueryString["aid"];
			string strCardid=Request.QueryString["cid"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busi=new BusiComm.BusiQuery(strcons);

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(!IsPostBack)
			{
				Session.Remove("asold");
				Session.Remove("asnew");
				CMSMStruct.AssociatorStruct ass1=busi.GetAssDetailInfo(strAssid,strCardid);
				txtAssID.Text=ass1.strAssID;
				txtCardID.Text=ass1.strCardID;
				txtAssName.Text=ass1.strAssName;
				txtSpell.Text=ass1.strSpell;
				txtAssNbr.Text=ass1.strAssNbr;
                if (ass1.strAssTypeDisp == "X" || ass1.strAssTypeDisp == "x")
                {
                    this.FillDropDownList("AssAT", this.ddlAssType, "vcCommSign='AT'");
                    this.ddlAssType.Enabled = false;
                }
                else
                {
                    this.FillDropDownList("AT1", this.ddlAssType);
                    this.ddlAssType.Enabled = true;
                }
                ddlAssType.SelectedIndex = ddlAssType.Items.IndexOf(ddlAssType.Items.FindByValue(ass1.strAssType));
				txtLinkPhone.Text=ass1.strLinkPhone;
				txtEmail.Text=ass1.strEmail;
				txtLinkAddress.Text=ass1.strLinkAddress;
				txtComments.Text=ass1.strComments;
				Session["asold"]=ass1;
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.AssociatorStruct asold=(CMSMStruct.AssociatorStruct)Session["asold"];
			if(asold.strCardID!=txtCardID.Text.Trim()||txtAssID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("����ʧ�ܣ������ԣ�");
				return;
			}

			CMSMStruct.AssociatorStruct asnew=new CMSMStruct.AssociatorStruct();
			asnew.strCardID=asold.strCardID;
			asnew.strAssID=asold.strAssID;
			if(txtAssName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��Ա��������Ϊ�գ�");
				return;
			}

			asnew.strAssName=txtAssName.Text.Trim();
			asnew.strSpell=txtSpell.Text.Trim();
			asnew.strAssNbr=txtAssNbr.Text.Trim();
			asnew.strLinkPhone=txtLinkPhone.Text.Trim();
			asnew.strLinkAddress=txtLinkAddress.Text.Trim();
			asnew.strEmail=txtEmail.Text.Trim();
			asnew.strComments=txtComments.Text.Trim();
            asnew.strAssType = ddlAssType.SelectedItem.Value;
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];

			if(!busi.UpdateAssDetail(asnew,asold,ls1))
			{
				this.SetErrorMsgPageBydir("�޸Ļ�Ա����ʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�޸Ļ�Ա���ϳɹ���","BusiQuery/wfmAssInfo.aspx");
				return;
			}
		}
	}
}
