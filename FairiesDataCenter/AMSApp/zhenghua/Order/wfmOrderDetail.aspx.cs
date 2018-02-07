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

namespace AMSApp.zhenghua.Order
{
	/// <summary>
	/// wfmOrderDetail ��ժҪ˵����
	/// </summary>
	public class wfmOrderDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				BindGrid();
				if(Session["ProductList"] == null)
				{
					this.Label1.Text = "����ϸ�ڡ�δѡ���Ʒ����ͨ����Ʒ��ѯѡ���Ʒ��";
				}
				else
				{
					this.Label1.Text = "����ϸ��";
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
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void BindGrid()
		{			
			if(Session["ProductList"] != null)
			{
				DataTable dtOrderBookDetail = (DataTable) (Session["ProductList"]);
				this.DataGrid1.DataSource = dtOrderBookDetail;
				this.DataGrid1.DataBind();
			}
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			this.BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				if(Session["ProductList"] == null)
				{
					Popup("������ѡ���Ʒ");
					return;
				}
				string strProductCode = e.Item.Cells[0].Text;				
				string strPrice = e.Item.Cells[3].Text;
				string strCount = ((TextBox) e.Item.Cells[4].Controls[0]).Text;
				if(this.JudgeIsNull(strCount,"����"))
				{
					return;
				}
				if(!this.JudgeIsNum(strCount,"����"))
				{
					return;
				}
				if(decimal.Parse(strCount) <= 0)
				{
					Popup("�������������");
					return;
				}
				decimal dSum = decimal.Parse(strPrice)*decimal.Parse(strCount);
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcProductCode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					drOrderBookDetail[0]["cnnOrderCount"] = strCount;
					drOrderBookDetail[0]["cnnSum"] = dSum.ToString();
				}
				this.DataGrid1.EditItemIndex = -1;
				this.BindGrid();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{								
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[7].Controls[0]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('ȷ��ɾ����')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			} 
		}

		private void DataGrid1_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				string strProductCode = e.Item.Cells[0].Text;
				DataTable dtOrderBookDetail = (DataTable) Session["ProductList"];
				DataRow[] drOrderBookDetail = dtOrderBookDetail.Select("cnvcProductCode='" + strProductCode + "'");
				if(drOrderBookDetail.Length >0)
				{
					dtOrderBookDetail.Rows.Remove(drOrderBookDetail[0]);
				}
				this.BindGrid();
			}
		}

	}
}
