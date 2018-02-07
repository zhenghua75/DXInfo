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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
namespace AMSApp.zhenghua.Warhouse
{
	/// <summary>
	/// wfmAddTeam ��ժҪ˵����
	/// </summary>
	public class wfmAddTeam : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtTeamID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtTeamName;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.Response.Expires = -1;
			this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
			this.Response.CacheControl = "no-cache";
			this.Button2.Attributes.Add("onclick","window.returnValue='cccc';window.close()");
			if(!this.IsPostBack)
			{
				string strflag = this.Request.QueryString["flag"].ToString();
				if(strflag=="add")
				{
					this.lblTitle.Text = "���������";							
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "�޸�������";
					this.txtTeamID.Text = this.Request.QueryString["teamid"].ToString();
					this.txtTeamName.Text = this.Request.QueryString["teamname"].ToString();					
				}				
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//����޸�������
			//ȷ��
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "���������";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			Business.WareHouseFacade whf = new WareHouseFacade();
			
			if(this.JudgeIsNull(this.txtTeamName.Text))
			{
				this.Popup("����������������");
				return;
			}
			if(this.lblTitle.Text == "���������")
			{
				ol.cnvcOperType = "���������";
				Entity.Team team = new AMSApp.zhenghua.Entity.Team();
				team.cnvcTeamName = this.txtTeamName.Text;
				int ret = whf.AddTeam(ol,team);
				if(ret > 0 )
				{
					this.Popup("���������ɹ���");
				}
				else
				{
					this.Popup("���������ʧ�ܣ�");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "�޸�������")
			{
				ol.cnvcOperType = "�޸�������";
				Entity.Team team = new AMSApp.zhenghua.Entity.Team();
				team.cnnTeamID = Convert.ToInt32(this.txtTeamID.Text);
				team.cnvcTeamName = this.txtTeamName.Text;
				int ret = whf.UpdateTeam(ol,team);
				if(ret > 0 )
				{
					this.Popup("�޸�������ɹ���");
				}
				else
				{
					this.Popup("�޸�������ʧ�ܣ�");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
		}
	}
}
