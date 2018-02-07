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
	/// wfmAddProducer 的摘要说明。
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
			// 在此处放置用户代码以初始化页面
			this.Response.Expires = -1;
			this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
			this.Response.CacheControl = "no-cache";
			this.Button2.Attributes.Add("onclick","window.returnValue='cccc';window.close()");
			if(!this.IsPostBack)
			{
				string strflag = this.Request.QueryString["flag"].ToString();
				if(strflag=="add")
				{
					this.lblTitle.Text = "添加生产员";							
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "修改生产员";
					this.txtProducerID.Text = this.Request.QueryString["producerid"].ToString();
					this.txtProducerName.Text = this.Request.QueryString["producername"].ToString();					
				}				
			}
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
			ol.cnvcOperType = "添加生产员";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			Business.WareHouseFacade whf = new WareHouseFacade();
			
			if(this.JudgeIsNull(this.txtProducerName.Text))
			{
				this.Popup("请输入生产员名称");
				return;
			}
			if(this.lblTitle.Text == "添加生产员")
			{
				ol.cnvcOperType = "添加生产员";
				Entity.Producer producer = new AMSApp.zhenghua.Entity.Producer();
				producer.cnvcProducerName = this.txtProducerName.Text;
				int ret = whf.AddProducer(ol,producer);
				if(ret > 0 )
				{
					this.Popup("添加生产员成功！");
				}
				else
				{
					this.Popup("添加生产员失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "修改生产员")
			{
				ol.cnvcOperType = "修改生产员";
				Entity.Producer producer = new AMSApp.zhenghua.Entity.Producer();
				producer.cnnProducerID = Convert.ToInt32(this.txtProducerID.Text);
				producer.cnvcProducerName = this.txtProducerName.Text;
				int ret = whf.UpdateProducer(ol,producer);
				if(ret > 0 )
				{
					this.Popup("修改生产员成功！");
				}
				else
				{
					this.Popup("修改生产员失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
		}
	}
}
