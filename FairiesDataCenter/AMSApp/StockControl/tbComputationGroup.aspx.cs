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
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;
namespace AMSApp.StockControl
{
	/// <summary>
	/// wfmAddComputationGroup ��ժҪ˵����
	/// </summary>
	public class tbComputationGroup : wfmBase
	{
        //protected System.Web.UI.WebControls.Label Label1;
        //protected System.Web.UI.WebControls.TextBox TextBox1;
        //protected System.Web.UI.WebControls.Label Label2;
        //protected System.Web.UI.WebControls.TextBox TextBox2;
        //protected System.Web.UI.WebControls.Label lblTitle;
        //protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
            //this.Response.Expires = -1;
            //this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
            //this.Response.CacheControl = "no-cache";

            //if(!this.IsPostBack)
            //{
            //    string strflag = this.Request.QueryString["flag"].ToString();
            //    if(strflag=="add")
            //    {
            //        this.lblTitle.Text = "��Ӽ�����λ��";
            //        this.TextBox2.Text = this.GetComputationGroupCode();
            //        //this.TextBox2.Enabled = false;
            //    }
            //    if(strflag=="modify")
            //    {
            //        this.lblTitle.Text = "�޸ļ�����λ��";
            //        string strgroupcode = this.Request.QueryString["groupcode"].ToString();
            //        string strgroupname = this.Request.QueryString["groupname"].ToString();
            //        this.TextBox2.Text = strgroupcode;
            //        this.TextBox1.Text = strgroupname;
            //        //this.TextBox2.Enabled = false;
            //    }
            //    this.TextBox2.Enabled = false;
			//}
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
			//this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        //private void Button1_Click(object sender, System.EventArgs e)
        //{
        //    //��Ӽ�����λ��
        //    if(this.JudgeIsNull(this.TextBox2.Text))
        //    {
        //        this.Popup("������λ������ȡʧ�ܣ���ر�ҳ�����»�ȡ");
        //        return;
        //    }
        //    if(this.JudgeIsNull(this.TextBox1.Text))
        //    {
        //        this.Popup("�����������λ������");
        //        return;
        //    }
        //    if(this.lblTitle.Text == "��Ӽ�����λ��")
        //    {
        //        Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
        //        ol.cnvcOperType = "��Ӽ�����λ��";
        //        ol.cnvcOperID = this.oper.strLoginID;
        //        ol.cnvcDeptID = this.oper.strDeptID;

        //        Entity.ComputationGroup cg = new ComputationGroup();
        //        cg.cnvcGroupCode = this.TextBox2.Text;
        //        cg.cnvcGroupName = this.TextBox1.Text;
				
        //        Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
        //        int ret = bcu.AddComputationGroup(ol,cg);
        //        if(ret > 0 )
        //        {
        //            this.Popup("��Ӽ�����λ��ɹ���");
        //        }
        //        else
        //        {
        //            this.Popup("��Ӽ�����λ��ʧ�ܣ�");
        //        }

        //        //this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
        //    }
        //    else if(this.lblTitle.Text == "�޸ļ�����λ��")
        //    {
        //        Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
        //        ol.cnvcOperType = "�޸ļ�����λ��";
        //        ol.cnvcOperID = this.oper.strLoginID;
        //        ol.cnvcDeptID = this.oper.strDeptID;

				

        //        Entity.ComputationGroup cg = new ComputationGroup();
        //        cg.cnvcGroupCode = this.TextBox2.Text;
        //        cg.cnvcGroupName = this.TextBox1.Text;

        //        Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
        //        int ret = bcu.UpdateComputationGroup(ol,cg);
        //        if(ret > 0 )
        //        {
        //            this.Popup("�޸ļ�����λ��ɹ���");
        //        }
        //        else
        //        {
        //            this.Popup("�޸ļ�����λ��ʧ�ܣ�");
        //        }
				
        //    }
        //    this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
        //}
	
		
	}
}
