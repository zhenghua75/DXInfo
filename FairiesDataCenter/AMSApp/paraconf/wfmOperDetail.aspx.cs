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

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmOperDetail.
	/// </summary>
	public class wfmOperDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btDel;
		protected System.Web.UI.WebControls.Button btcancel;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.TextBox txtLoginID;
		protected System.Web.UI.WebControls.TextBox txtOperName;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList ddlLimit;

		Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			this.btDel.Attributes.Add("onclick","javascript:return window.confirm('��ȷ��Ҫɾ����')");   

			string strid=Request.QueryString["id"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//			if(ls1.strLimit=="CL003")
//			{
//				this.SetErrorMsgPageBydir("�Բ�����û��Ȩ��ʹ�ô˹��ܣ�");
//				return;
//			}

			if(!IsPostBack)
			{
				Session.Remove("lsold");
				Session.Remove("lsnew");
				this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD'");
				if(ls1.strLimit=="CL002")
				{
					ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
					ddlDept.Enabled=false;
				}

				this.FillDropDownList("tbCommCode", ddlLimit,"vcCommSign='CLT'");
				if(strid==""||strid==null)
				{
					this.btAdd.Enabled=true;
					this.btDel.Enabled=false;
					this.btMod.Enabled=false;
					lbltitle.Text="����վ����Ա¼��";
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btDel.Enabled=false;
					this.btMod.Enabled=true;
					CMSMStruct.LoginStruct lsold=m1.GetLoginInfo(strid);
					txtLoginID.Text=lsold.strLoginID;
					txtOperName.Text=lsold.strOperName;
					ddlLimit.Items.FindByValue(lsold.strLimit).Selected=true;
					ddlDept.Items.FindByValue(lsold.strDeptID).Selected=true;
					if(ls1.strLimit!="CL001")
					{
						ddlLimit.Enabled=false;
						ddlDept.Enabled=false;
					}
                    if (ls1.strLoginID == "admin")
                    {
                        btDel.Enabled = true;
                    }
					lbltitle.Text="��վ����Ա�޸�ɾ��";
					txtLoginID.Enabled=false;
					Session["lsold"]=lsold;
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
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsnew=new CMSMStruct.LoginStruct();
			if(txtLoginID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("��¼ID����Ϊ�գ�");
				return;
			}
			else if(m1.ChkLoginIDDup(txtLoginID.Text.Trim()))
			{
				lsnew.strLoginID=txtLoginID.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("�õ�¼ID�Ѿ����ڣ����������룡");
				return;	
			}

			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("����Ա���Ʋ���Ϊ�գ�");
				return;
			}
			else if(m1.ChkOperNameDup(lsnew.strLoginID,txtOperName.Text.Trim()))
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("�ò���Ա�����Ѿ����ڣ����������룡");
				return;	
			}

			lsnew.strLimit=ddlLimit.SelectedValue;
			lsnew.strDeptID=ddlDept.SelectedValue;

			if(!m1.InsertLogin(lsnew))
			{
				this.SetErrorMsgPageBydir("�����վ����Աʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�����վ����Ա�ɹ���","");
				return;
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsold=(CMSMStruct.LoginStruct)Session["lsold"];
			if(lsold.strLoginID!=txtLoginID.Text.Trim())
			{
				this.SetErrorMsgPageBydir("����ʧ�ܣ������ԣ�");
				return;
			}

			CMSMStruct.LoginStruct lsnew=new CMSMStruct.LoginStruct();
			lsnew.strLoginID=lsold.strLoginID;
			if(txtOperName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("����Ա���Ʋ���Ϊ�գ�");
				return;
			}
			else if(txtOperName.Text.Trim()==lsold.strOperName)
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else if(m1.ChkOperNameDup(lsold.strLoginID,txtOperName.Text.Trim()))
			{
				lsnew.strOperName=txtOperName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydir("�ò���Ա�����Ѿ����ڣ����������룡");
				return;	
			}

			lsnew.strLimit=ddlLimit.SelectedValue;
			lsnew.strDeptID=ddlDept.SelectedValue;

			if(!m1.UpdateLogin(lsnew,lsold))
			{
				this.SetErrorMsgPageBydir("�����վ����Աʧ�ܣ������ԣ�");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("�����վ����Ա�ɹ���","");
				return;
			}
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct lsold=(CMSMStruct.LoginStruct)Session["lsold"];
			if(lsold.strLoginID==null||lsold.strLoginID=="")
			{
				this.SetErrorMsgPageBydir("��ȡ����Ա����");
				return;
			}
			if(m1.DeleteLogin(lsold.strLoginID))
			{
                this.SetSuccMsgPageBydir("ɾ������Ա�ɹ���", "./paraconf/wfmLoginOper.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("ɾ������Աʧ�ܣ�");
				return;
			}			
		}

		private void btcancel_Click(object sender, System.EventArgs e)
		{
            this.RedirectPage("wfmLoginOper.aspx");
		}
	}
}
