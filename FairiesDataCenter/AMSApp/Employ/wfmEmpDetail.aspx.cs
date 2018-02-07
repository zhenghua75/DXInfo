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
					this.ddlSex.Items.Add("男");
					this.ddlSex.Items.Add("女");
					this.ddlFlag.Items.Add(new ListItem("在职","0"));
					this.ddlFlag.Items.Add(new ListItem("离职","1"));

					if(strid==""||strid==null)
					{
						this.SetErrorMsgPageBydir("参数错误，无法调整员工属性！");
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
						lbltitle.Text="员工信息调整";
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
				this.SetErrorMsgPageBydir("调整员工信息出错，请退出本页面重试！");
				return;
			}
			else if(esold.strCardID!=esnew.strCardID)
			{
				this.SetErrorMsgPageBydir("调整员工信息出错，请退出本页面重试！");
				return;
			}
			if(txtEmpName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("员工姓名不能为空！");
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
						this.SetErrorMsgPageBydir("该员工姓名已经存在，请重新输入！");
						return;	
					}
				}
			}

			if(txtEmpNbr.Text.Trim()!=""&&txtEmpNbr.Text.Trim().Length!=15&&txtEmpNbr.Text.Trim().Length!=18)
			{
				this.SetErrorMsgPageBydir("身份证号不正确！");
				return;
			}
			else
			{
				esnew.strEmpNbr=txtEmpNbr.Text.Trim();
			}

			if(strInDate==""||strInDate==null)
			{
				this.SetErrorMsgPageBydir("入职时间不能为空！");
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
				this.SetErrorMsgPageBydir("调整员工信息失败或者没有做任何调整！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("调整员工信息成功！","EmpLoy/wfmEmpInfo.aspx");
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
//				this.SetErrorMsgPageBydir("员工卡号不能为空！");
//				return;
//			}
//			else if(esins.ChkEmpCardIDDup(txtCardID.Text.Trim()))
//			{
//				es1.strCardID=txtCardID.Text.Trim();
//			}
//			else
//			{
//				this.SetErrorMsgPageBydir("该卡号已经存在，请重新输入！");
//				return;	
//			}
//
//			if(txtEmpName.Text.Trim()=="")
//			{
//				this.SetErrorMsgPageBydir("员工姓名不能为空！");
//				return;
//			}
//			else if(esins.ChkEmpNameDup(txtEmpName.Text.Trim()))
//			{
//				es1.strEmpName=txtEmpName.Text.Trim();
//			}
//			else
//			{
//				this.SetErrorMsgPageBydir("该员工姓名已经存在，请重新输入！");
//				return;	
//			}
//
//			if(txtEmpNbr.Text.Trim()=="")
//			{
//				this.SetErrorMsgPageBydir("身份证号不能为空！");
//				return;
//			}
//			else
//			{
//				es1.strEmpNbr=txtEmpNbr.Text.Trim();
//			}
//
//			if(strInDate==""||strInDate==null)
//			{
//				this.SetErrorMsgPageBydir("入职时间不能为空！");
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
//				this.SetErrorMsgPageBydir("添加员工信息失败，请重试！");
//				return;
//			}
//			else
//			{
//				this.SetSuccMsgPageBydir("添加员工信息成功！");
//				return;
//			}
//		}

	}
}
