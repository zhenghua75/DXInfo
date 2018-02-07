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

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmOperPurview.
	/// </summary>
	public class wfmOperPurview : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblOperName;
		protected System.Web.UI.WebControls.Label lblOperID;
		protected System.Web.UI.WebControls.Button btnok;
		protected System.Web.UI.WebControls.CheckBoxList cblFunc;
		protected System.Web.UI.WebControls.TextBox txtCS;
		BusiComm.Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				string strLoginID=this.Request.QueryString["id"].ToString();
				string strOperName=this.Request.QueryString["name"].ToString();
				string strFuncType = this.Request.QueryString["FuncType"].ToString();
				this.txtCS.Text = strFuncType;
				if(strLoginID==null||strLoginID==""||strOperName==null||strOperName=="" || strFuncType==null||strFuncType=="")
				{
					this.SetErrorMsgPageBydirHistory("所选操作员登录ID错误，请重试！");
					return;
				}
				else
				{
					this.lblOperID.Text=strLoginID;
					this.lblOperName.Text=strOperName;

					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					m1=new BusiComm.Manager(strcons);
					//if(strFuncType="BS")
					DataSet dsout=m1.GetFuncList(strLoginID,strFuncType);
					DataTable dtfunclist=dsout.Tables["funclist"];
					DataTable dtoperfunc=dsout.Tables["operfunc"];
					if(dtfunclist!=null)
					{
						cblFunc.DataSource = dtfunclist;
						cblFunc.DataTextField = "cnvcFuncName";
						cblFunc.DataValueField = "cnvcFuncAddress";		

						cblFunc.DataBind();

						foreach (ListItem liFunctionList in this.cblFunc.Items)
						{
							if (liFunctionList.Selected)
							{
								liFunctionList.Selected = false;
							}
						}
						{
							foreach (ListItem liFunctionList in this.cblFunc.Items)

						if(dtoperfunc!=null&&dtoperfunc.Rows.Count>0)
							{					
								foreach (DataRow drOperFunc in dtoperfunc.Rows)
								{
									if (drOperFunc["vcFuncName"].ToString().Equals(liFunctionList.Text))
									{
										liFunctionList.Selected = true;
									}
								}
							}
						}
					}
					else
					{
						this.SetErrorMsgPageBydirHistory("获取功能列表错误！");
						return;
					}
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
			this.btnok.Click += new System.EventHandler(this.btnok_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnok_Click(object sender, System.EventArgs e)
		{
			ArrayList alnewoperfunc=new ArrayList();
			foreach (ListItem liFunctionList in this.cblFunc.Items)
			{
				if (liFunctionList.Selected)
				{
					CMSMStruct.MenuStruct mstmp=new CommCenter.CMSMStruct.MenuStruct();
					mstmp.strFuncName=liFunctionList.Text;
					mstmp.strFuncAddress=liFunctionList.Value;
					alnewoperfunc.Add(mstmp);
				}
			}

			string strOperID=this.lblOperID.Text.Trim();
			if(strOperID=="")
			{
				this.SetErrorMsgPageBydir("操作员信息错误，请重试！");
				return;
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			string strFuncType = txtCS.Text;
			if(m1.UpdateOperPurview(strOperID,alnewoperfunc,strFuncType))
			{
				if(strFuncType=="BS")
				this.SetSuccMsgPageBydir("操作员权限修改成功！","paraconf/wfmLoginOper.aspx");
				else
					this.SetSuccMsgPageBydir("操作员权限修改成功！","paraconf/wfmDeptOperManage.aspx");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("操作员权限修改失败，请重试！");
				return;
			}
		}
	}
}
