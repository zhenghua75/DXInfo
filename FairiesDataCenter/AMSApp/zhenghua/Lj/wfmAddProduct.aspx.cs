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

namespace AMSApp.zhenghua.Lj
{
	/// <summary>
	/// wfmAddProduct ��ժҪ˵����
	/// </summary>
	public class wfmAddProduct : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList ddlGoods;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
				this.BindGoods(ddlGoods);
				this.TextBox1.Text = DateTime.Now.ToString("yyyy-MM-dd");
				BindGrid();
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
			this.TextBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.MyDataGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_CancelCommand);
			this.MyDataGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_EditCommand);
			this.MyDataGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_UpdateCommand);
			this.MyDataGrid.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_DeleteCommand);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void TextBox2_TextChanged(object sender, System.EventArgs e)
		{
			//this.Popup("test");
			string strSpell = this.TextBox2.Text;
			DataTable dtGoods = this.GetGoods();
			DataRow[] drs = dtGoods.Select("vcSpell like '"+strSpell+"%'");
			if(drs.Length>0)
			{
				string strGoodsID = drs[0]["vcGoodsID"].ToString();
				ListItem li = ddlGoods.Items.FindByValue(strGoodsID);
				if(li!=null)
				{
					ddlGoods.SelectedIndex = ddlGoods.Items.IndexOf(li);
				}
			}
		}
		
		private void SetProduct(DataTable dtProduct)
		{
			Session["tbProductSerial"] = dtProduct;
		}
		public DataTable GetProduct()
		{
			DataTable dtProduct = null;			
			if(Session["tbProductSerial"] != null)
			{
				dtProduct = (DataTable) Session["tbProductSerial"];
			}
			else
			{
				dtProduct = Helper.Query("select * from tbProductSerial where 1<>1");
				Session["tbProductSerial"] = dtProduct;
			}
			return dtProduct;
		}
		private void BindGrid()
		{
			DataTable dtProduct = GetProduct();
			this.MyDataGrid.DataSource = dtProduct;
			this.MyDataGrid.DataBind();
		}
		private void Button1_Click(object sender, System.EventArgs e)
		{
			//��Ӳ�Ʒ
			
			string strGoodsID = this.ddlGoods.SelectedValue;
			string strGoodsName = this.ddlGoods.SelectedItem.Text;
			string strCount = txtCount.Text;

			if(strCount=="")
			{
				this.Popup("�������Ʒ������");
				return;
			}
			if(!this.JudgeIsNum(strCount))
			{
				this.Popup("���������֣�");
				return;
			}
			if(Convert.ToInt32(strCount)<=0)
			{
				this.Popup("��Ʒ������������㣡");
				return;
			}

			DataTable dtProduct = GetProduct();
			DataRow[] drs = dtProduct.Select("cnvcCode='"+strGoodsID+"'");
			if(drs.Length==0)
			{
				DataRow dr = dtProduct.NewRow();
				dr["cnvcCode"] = strGoodsID;
				dr["cnvcName"] = strGoodsName;
				dr["cnnCount"] = strCount;
				dtProduct.Rows.Add(dr);
				SetProduct(dtProduct);
				BindGrid();
			}
		}

		private void MyDataGrid_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.MyDataGrid.EditItemIndex=e.Item.ItemIndex;
			BindGrid();
		}

		private void MyDataGrid_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.MyDataGrid.EditItemIndex = -1;
			BindGrid();
		}

		private void MyDataGrid_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strGoodsID = e.Item.Cells[0].Text;
			string strCount = ((TextBox)e.Item.Cells[2].Controls[0]).Text;
			if(strCount=="")
			{
				this.Popup("�������Ʒ������");
				return;
			}
			if(!this.JudgeIsNum(strCount))
			{
				this.Popup("���������֣�");
				return;
			}
			if(Convert.ToInt32(strCount)<=0)
			{
				this.Popup("��Ʒ������������㣡");
				return;
			}
			DataTable dtProduct = GetProduct();
			DataRow[] drs = dtProduct.Select("cnvcCode='"+strGoodsID+"'");
			if(drs.Length>0)
			{
				drs[0]["cnnCount"] = strCount;
			}
			SetProduct(dtProduct);
			this.MyDataGrid.EditItemIndex = -1;
			BindGrid();
		}

		private void MyDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strGoodsID = e.Item.Cells[0].Text;
			
			DataTable dtProduct = GetProduct();
			DataRow[] drs = dtProduct.Select("cnvcCode='"+strGoodsID+"'");
			if(drs.Length>0)
			{
				dtProduct.Rows.Remove(drs[0]);
				//drs[0]["cnnCount"] = strCount;
			}
			SetProduct(dtProduct);
			BindGrid();
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//������Ʒ���
			DataTable dtProductSerial = GetProduct();
			ArrayList alProductSerial = new ArrayList();
			if(dtProductSerial.Rows.Count>0)
			{
				foreach(DataRow dr in dtProductSerial.Rows)
				{
					ProductSerial ps = new ProductSerial(dr);
					ps.cndCreateDate = Convert.ToDateTime(TextBox1.Text);
					ps.cnvcOperID = oper.strLoginID;
					ps.cnvcDeptID = ddlDept.SelectedValue;

					alProductSerial.Add(ps);
				}

				OperLog ol = new OperLog();
				ol.cnvcDeptID = oper.strDeptID;
				ol.cnvcOperID = oper.strLoginID;
				ol.cnvcOperType = "������Ʒ���";

				ProductFacade pf = new ProductFacade();
				pf.AddProductSerial(alProductSerial,ol);

				this.Popup("������Ʒ���ɹ���");

				//��������

				Session.Remove("tbProductSerial");
				BindGrid();
			}
		}
	}
}
