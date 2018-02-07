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
	/// wfmProducer ��ժҪ˵����
	/// </summary>
	public class wfmProducer : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				LoadProducer();
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
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private DataTable GetProudcer()
		{
			DataTable dt = Helper.Query("select * from tbproducer");
			return dt;
		}
		private void LoadProducer()
		{
			DataTable dt = GetProudcer();
			if(dt.Rows.Count == 0)
			{
				dt.Rows.Add(dt.NewRow());				
			}
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
		}
		private void Button1_Click(object sender, System.EventArgs e)
		{
            this.ClientScript.RegisterStartupScript(this.GetType(), "addproducer", "<script type=\"text/javascript\">OpenProducerWin('add','','');</script>");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			if(this.DataGrid1.SelectedIndex >= 0)
			{
				string strproducerid = this.DataGrid1.SelectedItem.Cells[1].Text;
				string strproducername = this.DataGrid1.SelectedItem.Cells[2].Text;
				if(strproducerid == "" || strproducerid == "&nbsp;") 
				{
					this.Popup("��ѡ������Ա");
					return;
				}
                this.ClientScript.RegisterStartupScript(this.GetType(), "modifyproducer", "<script type=\"text/javascript\">OpenProducerWin('modify','" + strproducerid + "','" + strproducername + "');</script>");

			}
			else
				this.Popup("��ѡ������Ա");
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Header)
			{
			
				for(int i=0;i<e.Item.Cells.Count;i++)
				{
					e.Item.Cells[i].Width   =Unit.Pixel(e.Item.Cells[i].Text.Length*18);
				}
			}
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			LoadProducer();
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("../Produce/wfmProduceCheck.aspx");
		}		
	}
}
