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

namespace AMSApp.Employ
{
	/// <summary>
	/// Summary description for wfmEmpDetail.
	/// </summary>
	public partial class wfmEmpDetail : wfmBase
	{

		protected string strExcelPath = string.Empty;
		protected string strInDate;
		EmpBusi esins;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				string strid=Request.QueryString["id"];
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				esins=new EmpBusi(strcons);

				if(!this.IsPostBack)
				{
					strInDate=DateTime.Now.ToShortDateString();
					this.FillDropDownList("tbCommCode", ddlDegree, "vcCommSign ='DE'");
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
					this.FillDropDownList("tbCommCode", ddlOfficer, "vcCommSign ='OF'");
					this.ddlSex.Items.Add("��");
					this.ddlSex.Items.Add("Ů");
					this.ddlFlag.Items.Add(new ListItem("��ְ","0"));
					this.ddlFlag.Items.Add(new ListItem("��ְ","1"));

					if(strid==""||strid==null)
					{
						this.SetErrorMsgPageBydir("���������޷�����Ա�����ԣ�");
					}
					else
					{
						Session.Remove("esold");
						CMSMStruct.EmployeeStruct es1=esins.GetEmpInfo(strid);
						txtCardID.Text=es1.strCardID;
						txtEmpName.Text=es1.strEmpName;
						txtEmpNbr.Text=es1.strEmpNbr;
						ddlSex.Items.FindByText(es1.strSex).Selected=true;
						txtLinkPhone.Text=es1.strLinkPhone;
						ddlDegree.Items.FindByValue(es1.strDegree).Selected=true;
						strInDate=es1.strInDate;
						ddlDept.Items.FindByValue(es1.strDeptID).Selected=true;
						ddlFlag.Items.FindByValue(es1.strFlag).Selected=true;
						ddlOfficer.Items.FindByValue(es1.strOfficer).Selected=true;
						txtAddress.Text=es1.strAddress;
						txtComments.Text=es1.strComments;
						this.txtCardID.ReadOnly=true;
						lbltitle.Text="Ա����Ϣ����";
						Session["esold"]=es1;
					}
				}
				else
				{
					strInDate = Request.Form["txtInDate"].ToString();
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

		protected void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.EmployeeStruct esnew=new CommCenter.CMSMStruct.EmployeeStruct();
			esnew.strCardID=txtCardID.Text.Trim();
			CMSMStruct.EmployeeStruct esold=(CMSMStruct.EmployeeStruct)Session["esold"];
			if(esold==null)
			{
				this.SetErrorMsgPageBydir("����Ա����Ϣ�������˳���ҳ�����ԣ�");
				return;
			}
			else if(esold.strCardID!=esnew.strCardID)
			{
				this.SetErrorMsgPageBydir("����Ա����Ϣ�������˳���ҳ�����ԣ�");
				return;
			}
			if(txtEmpName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("Ա����������Ϊ�գ�");
				return;
			}
			else
			{
				esnew.strEmpName=txtEmpName.Text.Trim();
				if(esold.strEmpName==esnew.strEmpName)
				{
					esnew.strEmpName=txtEmpName.Text.Trim();
				}
				else
				{
					if(esins.ChkEmpNameDup(txtEmpName.Text.Trim()))
					{
						esnew.strEmpName=txtEmpName.Text.Trim();
					}
					else
					{
						this.SetErrorMsgPageBydir("��Ա�������Ѿ����ڣ����������룡");
						return;	
					}
				}
			}

			if(txtEmpNbr.Text.Trim()!=""&&txtEmpNbr.Text.Trim().Length!=15&&txtEmpNbr.Text.Trim().Length!=18)
			{
				this.SetErrorMsgPageBydir("���֤�Ų���ȷ��");
				return;
			}
			else
			{
				esnew.strEmpNbr=txtEmpNbr.Text.Trim();
			}

			if(strInDate==""||strInDate==null)
			{
				this.SetErrorMsgPageBydir("��ְʱ�䲻��Ϊ�գ�");
				return;
			}
			else
			{
				esnew.strInDate=strInDate;
			}
			
			esnew.strFlag=ddlFlag.SelectedValue;
			esnew.strOfficer=ddlOfficer.SelectedValue;
			esnew.strDeptID=ddlDept.SelectedValue;
			esnew.strSex=ddlSex.SelectedValue;
			esnew.strDegree=ddlDegree.SelectedValue;
			esnew.strLinkPhone=txtLinkPhone.Text.Trim();
			esnew.strAddress=txtAddress.Text.Trim();
			esnew.strComments=txtComments.Text.Trim();

			if(!esins.ModEmployee(esold,esnew))
			{
				this.SetErrorMsgPageBydir("����Ա����Ϣʧ�ܻ���û�����κε�����");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("����Ա����Ϣ�ɹ���","EmpLoy/wfmEmpInfo.aspx");
				return;
			}
		}

		protected void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmEmpInfo.aspx");
		}


//		private void btAdd_Click(object sender, System.EventArgs e)
//		{
//			CMSMStruct.EmployeeStruct es1=new CommCenter.CMSMStruct.EmployeeStruct();
//			if(txtCardID.Text.Trim()=="")
//			{
//				this.SetErrorMsgPageBydir("Ա�����Ų���Ϊ�գ�");
//				return;
//			}
//			else if(esins.ChkEmpCardIDDup(txtCardID.Text.Trim()))
//			{
//				es1.strCardID=txtCardID.Text.Trim();
//			}
//			else
//			{
//				this.SetErrorMsgPageBydir("�ÿ����Ѿ����ڣ����������룡");
//				return;	
//			}
//
//			if(txtEmpName.Text.Trim()=="")
//			{
//				this.SetErrorMsgPageBydir("Ա����������Ϊ�գ�");
//				return;
//			}
//			else if(esins.ChkEmpNameDup(txtEmpName.Text.Trim()))
//			{
//				es1.strEmpName=txtEmpName.Text.Trim();
//			}
//			else
//			{
//				this.SetErrorMsgPageBydir("��Ա�������Ѿ����ڣ����������룡");
//				return;	
//			}
//
//			if(txtEmpNbr.Text.Trim()=="")
//			{
//				this.SetErrorMsgPageBydir("���֤�Ų���Ϊ�գ�");
//				return;
//			}
//			else
//			{
//				es1.strEmpNbr=txtEmpNbr.Text.Trim();
//			}
//
//			if(strInDate==""||strInDate==null)
//			{
//				this.SetErrorMsgPageBydir("��ְʱ�䲻��Ϊ�գ�");
//				return;
//			}
//			else
//			{
//				es1.strInDate=strInDate;
//			}
//			
//			es1.strDeptID=ddlDept.SelectedValue;
//			es1.strSex=ddlSex.SelectedValue;
//			es1.strDegree=ddlDegree.SelectedValue;
//			es1.strLinkPhone=txtLinkPhone.Text.Trim();
//			es1.strAddress=txtAddress.Text.Trim();
//			es1.strComments=txtComments.Text.Trim();
//
//			if(!esins.InsertEmployee(es1))
//			{
//				this.SetErrorMsgPageBydir("���Ա����Ϣʧ�ܣ������ԣ�");
//				return;
//			}
//			else
//			{
//				this.SetSuccMsgPageBydir("���Ա����Ϣ�ɹ���");
//				return;
//			}
//		}

	}
}
