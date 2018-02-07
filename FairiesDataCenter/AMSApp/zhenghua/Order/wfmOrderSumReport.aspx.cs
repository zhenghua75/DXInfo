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
	public class wfmOrderSumReport : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		//protected System.Web.UI.WebControls.Label lblOper;
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
				if(Session["OrderSumReport"] == null)
				{
					Popup("��Ч����");
					return;
				}				
				string strReport = Session["OrderSumReport"].ToString();
				DataTable dtReport = Helper.Query(strReport);
				

				this.DataTableConvert(dtReport, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
				
				DataTable dtDept = (DataTable)Application["tbDept"];
			
				DataTable dtpt = new DataTable();
				dtpt.Columns.Add("��Ʒ����");
				dtpt.Columns.Add("��Ʒ����");
				foreach(DataRow drDept in dtDept.Rows)
				{
					if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("CEN00"))
					{
						dtpt.Columns.Add(drDept["cnvcDeptName"].ToString());	
					}
						
				}
				//dtpt.Columns.Add("�ϼ�");
				dtpt.Columns.Add("�ϼ�");
				//Hashtable htProduct = new Hashtable();
				foreach(DataRow drReport in dtReport.Rows)
				{
					//DataRow drpt = null;
					DataRow[] drpts = dtpt.Select("��Ʒ����='"+drReport["cnvcinvcode"].ToString()+"'");
					if(drpts.Length > 0)
					{
						DataRow drpt = drpts[0];
						drpt["��Ʒ����"] = drReport["cnvcinvcode"].ToString();
						drpt["��Ʒ����"] = drReport["cnvcinvname"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["�ϼ�"] = drReport2[0]["cnnCount"];
						//dtpt.Rows.Add(drpt);
					}
					else
					{
						DataRow drpt = dtpt.NewRow();
						drpt["��Ʒ����"] = drReport["cnvcinvcode"].ToString();
						drpt["��Ʒ����"] = drReport["cnvcinvname"].ToString();
						drpt[drReport["cnvcOrderDeptIDComments"].ToString()] = drReport["cnnCount"].ToString();
//						DataRow[] drReport2 = dtAssign2.Select("cnvcProductCode='" + drReport["cnvcProductCode"].ToString() + "'");
//						if(drReport2.Length > 0)
//							drpt["�ϼ�"] = drReport2[0]["cnnCount"];
						dtpt.Rows.Add(drpt);

					}
				
				
				}
				//�ϼ�����
				foreach(DataRow dr in dtpt.Rows)
				{
					foreach(DataRow drDept in dtDept.Rows)
					{
						if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("CEN00"))
						{
							string strCount = dr[drDept["cnvcDeptName"].ToString()].ToString();	
							if(strCount == "")
								strCount = "0";
							string strSum = dr["�ϼ�"].ToString();
							if(strSum == "")
								strSum = "0";
							dr["�ϼ�"] = decimal.Parse(strCount)+decimal.Parse(strSum);
						}
						
					}
				}

				this.DataGrid1.DataSource = dtpt;
				this.DataGrid1.DataBind();

				//this.lblOper.Text = this.oper.strOperName;
				this.lblDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
//				if(Request["OrderType"] != null)
//				{
//					this.lblDate.Text += Request["OrderType"].ToString();
//				}
				this.lblDate.Text += "���ܱ�";
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
			Session["OrderSumReport"] = null;
			this.Response.Redirect("wfmOrderQuery.aspx");
		}
	}
}
