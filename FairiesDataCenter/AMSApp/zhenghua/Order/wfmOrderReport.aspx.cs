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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmDividReport ��ժҪ˵����
	/// </summary>
	public class wfmOrderReport : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblOper;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Button btnReturn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				if(Session["OrderReport"] == null)
				{
					Popup("��Ч����");
					return;
				}				
				string strReport = Session["OrderReport"].ToString();
				DataTable dtReport = Helper.Query(strReport);

				this.DataTableConvert(dtReport, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
				this.DataTableConvert(dtReport, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
				//this.DataTableConvert(dtReport, "cnvcOrderState", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERSTATE'");
				this.DataTableConvert(dtReport, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
				this.DataTableConvert(dtReport, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			
				this.DataGrid1.DataSource = dtReport;
				this.DataGrid1.DataBind();

				this.lblOper.Text = this.oper.strOperName;
				this.lblDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
				if(Request["OrderType"] != null)
				{
					this.lblDate.Text += Request["OrderType"].ToString();
				}
				this.lblDate.Text += "�嵥��";
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
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			Session["OrderReport"] = null;
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
