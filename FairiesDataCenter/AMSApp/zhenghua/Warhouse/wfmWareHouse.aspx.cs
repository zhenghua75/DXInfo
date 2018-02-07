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
	/// wfmWareHouse ��ժҪ˵����
	/// </summary>
	public class wfmWareHouse : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				Loadwh();
			}
		}
		private DataTable GetWh()
		{
			DataTable dt = Helper.Query("select * from tbwarehouse");
			this.DataTableConvert(dt,"cnvcwhperson","cnvcwhperson","tbLogin","vcLoginID","vcOperName","");
			this.DataTableConvert(dt,"cnvcwhproperty","cnnwhproperty","tbNameCode","cnvccode","cnvcname","cnvctype='WhAttr'");
			//
			this.DataTableConvert(dt,"cnvcWhValueStyle","cnvcWhValueStyle","tbNameCode","cnvccode","cnvcname","cnvctype='valuetype'");
			//cnvcDepCode
			this.DataTableConvert(dt,"cnvcDepCode","cnvcDepCode","tbdept","cnvcdeptid","cnvcdeptname","");
			return dt;
		}
		private void Loadwh()
		{
			DataTable dt = GetWh();
			if(dt.Rows.Count == 0)
			{
				dt.Rows.Add(dt.NewRow());
				dt.Rows[0]["cnbFreeze"] = false;
				dt.Rows[0]["cnbShop"] = false;
			}
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.DataGrid1.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//this.RegisterStartupScript("addinv","<script type=\"text/javascript\">OpenWarehouseWin('add','','','');</script>");
            this.ClientScript.RegisterStartupScript(this.GetType(), "addinv", "<script type=\"text/javascript\">OpenWarehouseWin('add','','','');</script>");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			if(this.DataGrid1.SelectedIndex >= 0)
			{
				string strwhcode = this.DataGrid1.SelectedItem.Cells[1].Text;
				this.ClientScript.RegisterStartupScript(this.GetType(),"addinv","<script type=\"text/javascript\">OpenWarehouseWin('modify','"+strwhcode+"');</script>");

			}
			else
				this.Popup("��ѡ��ֿ⵵��");
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
			Loadwh();
		}		
	}
}
