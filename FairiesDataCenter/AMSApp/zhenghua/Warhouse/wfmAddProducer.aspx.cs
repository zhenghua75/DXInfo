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
	/// wfmAddProducer ��ժҪ˵����
	/// </summary>
	public class wfmAddProducer : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtProducerID;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProducerName;
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
					this.lblTitle.Text = "�������Ա";							
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "�޸�����Ա";
					this.txtProducerID.Text = this.Request.QueryString["producerid"].ToString();
					this.txtProducerName.Text = this.Request.QueryString["producername"].ToString();					
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
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "�������Ա";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			Business.WareHouseFacade whf = new WareHouseFacade();
			
			if(this.JudgeIsNull(this.txtProducerName.Text))
			{
				this.Popup("����������Ա����");
				return;
			}
			if(this.lblTitle.Text == "�������Ա")
			{
				ol.cnvcOperType = "�������Ա";
				Entity.Producer producer = new AMSApp.zhenghua.Entity.Producer();
				producer.cnvcProducerName = this.txtProducerName.Text;
				int ret = whf.AddProducer(ol,producer);
				if(ret > 0 )
				{
					this.Popup("�������Ա�ɹ���");
				}
				else
				{
					this.Popup("�������Աʧ�ܣ�");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "�޸�����Ա")
			{
				ol.cnvcOperType = "�޸�����Ա";
				Entity.Producer producer = new AMSApp.zhenghua.Entity.Producer();
				producer.cnnProducerID = Convert.ToInt32(this.txtProducerID.Text);
				producer.cnvcProducerName = this.txtProducerName.Text;
				int ret = whf.UpdateProducer(ol,producer);
				if(ret > 0 )
				{
					this.Popup("�޸�����Ա�ɹ���");
				}
				else
				{
					this.Popup("�޸�����Աʧ�ܣ�");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
		}
	}
}
