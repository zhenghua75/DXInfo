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
	/// wfmAddProductLost ��ժҪ˵����
	/// </summary>
	public class wfmAddProductLost : wfmBase
	{
		#region �ֶ�
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.DropDownList ddlInv;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceUnitCode;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.Button Button2;

		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				//this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
				//this.FillDropDownList("tbDept", ddlDept, "cnvcDeptType<>'Corp'","ȫ��");
				this.BindDept(ddlDept,"cnvcDeptType<>'Corp'");
				this.SetDDL(this.ddlDept,this.oper.strDeptID);
				if(this.oper.strDeptID !="CEN00")
				{				
					this.ddlDept.Enabled = false;		
				}
				//this.BindGoods(ddlGoods);
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
			this.txtInvCode.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
			this.txtInvName.TextChanged += new System.EventHandler(this.txtInvName_TextChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.ddlInv.SelectedIndexChanged += new System.EventHandler(this.ddlInv_SelectedIndexChanged);
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
//			string strSpell = this.TextBox2.Text;
//			DataTable dtGoods = this.GetGoods();
//			DataRow[] drs = dtGoods.Select("vcSpell like '"+strSpell+"%'");
//			if(drs.Length>0)
//			{
//				string strGoodsID = drs[0]["vcGoodsID"].ToString();
//				ListItem li = ddlGoods.Items.FindByValue(strGoodsID);
//				if(li!=null)
//				{
//					ddlGoods.SelectedIndex = ddlGoods.Items.IndexOf(li);
//				}
//			}
			this.Button3_Click(null,null);
		}
		
		private void SetProduct(DataTable dtProduct)
		{
			Session["tbLostSerial"] = dtProduct;
		}
		public DataTable GetProduct()
		{
			DataTable dtProduct = null;			
			if(Session["tbLostSerial"] != null)
			{
				dtProduct = (DataTable) Session["tbLostSerial"];
			}
			else
			{
				Session["tbLostSerial"] =GetNULLGrid();
			}
			return dtProduct;
		}
		private DataTable GetNULLGrid()
		{
			Entity.LostSerial ls = new LostSerial();
			DataTable dt = ls.ToTable();
			dt.Columns.Add("cnvcInvName");
			return dt.Clone();
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
			string strGoodsID = this.ddlInv.SelectedValue;
			string strGoodsName = this.ddlInv.SelectedItem.Text;
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
			DataRow[] drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"'");
			if(drs.Length==0)
			{
				DataRow dr = dtProduct.NewRow();
				dr["cnvcInvCode"] = strGoodsID;
				dr["cnvcInvName"] = strGoodsName;
				dr["cnnLostCount"] = strCount;
				dr["cnvcComments"] = this.txtComments.Text;
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
			string strComments = ((TextBox)e.Item.Cells[3].Controls[0]).Text;
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
			DataRow[] drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"'");
			if(drs.Length>0)
			{
				drs[0]["cnnLostCount"] = strCount;
				drs[0]["cnvcComments"] = strComments;
			}
			SetProduct(dtProduct);
			this.MyDataGrid.EditItemIndex = -1;
			BindGrid();
		}

		private void MyDataGrid_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strGoodsID = e.Item.Cells[0].Text;
			
			DataTable dtProduct = GetProduct();
			DataRow[] drs = dtProduct.Select("cnvcInvCode='"+strGoodsID+"'");
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
			DataTable dtLostSerial = GetProduct();
			ArrayList alLostSerial = new ArrayList();
			if(dtLostSerial.Rows.Count>0)
			{
				foreach(DataRow dr in dtLostSerial.Rows)
				{
					LostSerial ps = new LostSerial(dr);
					ps.cndLostDate = Convert.ToDateTime(TextBox1.Text);
					ps.cnvcOperID = oper.strLoginID;
					ps.cnvcDeptID = ddlDept.SelectedValue;

					alLostSerial.Add(ps);
				}

				OperLog ol = new OperLog();
				ol.cnvcDeptID = oper.strDeptID;
				ol.cnvcOperID = oper.strLoginID;
				ol.cnvcOperType = "������Ʒ����";

				ProductFacade pf = new ProductFacade();
				pf.AddProductLostSerial(alLostSerial,ol);

				this.Popup("������Ʒ����ɹ���");

				//��������

				Session.Remove("tbLostSerial");
				BindGrid();
			}
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			string strsql = "select cnvcInvCode,cnvcInvName from tbInventory where 1=1 ";
			if(this.txtInvCode.Text != "")
			{
				strsql += " and cnvcInvCode like '%"+txtInvCode.Text+"%' ";
			}
			if(this.txtInvName.Text != "")
			{
				strsql += " and cnvcInvName like '%"+txtInvName.Text+"%'";
			}
			DataTable dt = Helper.Query(strsql);
			this.ddlInv.DataTextField = "cnvcInvName";
			this.ddlInv.DataValueField= "cnvcInvCode";
			this.ddlInv.DataSource = dt;
			this.ddlInv.DataBind();

			ddlInv_SelectedIndexChanged(null,null);
		}

		private void ddlInv_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strsql = "select cnvcInvCode,cnvcInvName,cnvcProduceUnitCode from tbInventory where cnvcInvCode='"+this.ddlInv.SelectedValue+"'";			
			DataTable dt = Helper.Query(strsql);
			Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dt);
			strsql = "select * from tbcomputationunit where cnvccomunitcode='"+inv.cnvcProduceUnitCode+"'";
			dt = Helper.Query(strsql);
			if(dt.Rows.Count > 0)
			{
				Entity.ComputationUnit cu = new AMSApp.zhenghua.Entity.ComputationUnit(dt);
				this.txtProduceUnitCode.Text = cu.cnvcComUnitName;
			}
		}

		private void txtInvName_TextChanged(object sender, System.EventArgs e)
		{
			Button3_Click(null,null);
		}
	}
}
