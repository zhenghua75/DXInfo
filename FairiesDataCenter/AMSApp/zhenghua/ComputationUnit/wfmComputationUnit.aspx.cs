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
using AMSApp.zhenghua;
using Microsoft;
using Microsoft.Web;
using Microsoft.Web.UI;
using Microsoft.Web.UI.WebControls;

namespace AMSApp.zhenghua.ComputationUnit
{
	/// <summary>
	/// wfmComputationUnit ��ժҪ˵����
	/// </summary>
	public class wfmComputationUnit : wfmBase
	{
		#region �ֶ�
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Button Button4;
		protected System.Web.UI.WebControls.Button Button5;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblComputationGroup;
		protected System.Web.UI.WebControls.Label lblComnUnitCode;
		protected System.Web.UI.WebControls.Label lblComUnitName;
		protected System.Web.UI.WebControls.DropDownList ddlComputationGroup;
		protected System.Web.UI.WebControls.TextBox txtComUnitCode;
		protected System.Web.UI.WebControls.TextBox txtComUnitName;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnRefreshGroup;
		protected System.Web.UI.WebControls.Button btnRefreshUnit;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			Button1.Attributes["OnClick"] = "return confirm('��ȷ��Ҫɾ��������λ����?')";
			Button2.Attributes["OnClick"] = "return confirm('��ȷ��Ҫɾ��������λ��?')";
			if(!this.IsPostBack)
			{				
				LoadGroup();
				LoadNULLUnit();
			}
		}

		private DataTable GetComputationUnit(string strgroupcode)
		{
			return Helper.Query("select * from tbcomputationUnit where cnvcGroupCode='"+strgroupcode+"'");
		}
		private DataTable GetComputationGroup()
		{
			return Helper.Query("select * from tbcomputationgroup");
		}

		
		private void LoadGroup()
		{
			DataTable dtComputationGroup = GetComputationGroup();
			if(dtComputationGroup.Rows.Count == 0)
			{			
				dtComputationGroup.Rows.Add(dtComputationGroup.NewRow());

			}
			DataGrid1.DataSource = dtComputationGroup;
			DataGrid1.DataBind();

			this.ddlComputationGroup.DataSource = dtComputationGroup;
			this.ddlComputationGroup.DataTextField = "cnvcGroupName";
			this.ddlComputationGroup.DataValueField = "cnvcGroupCode";
			this.ddlComputationGroup.DataBind();
			ListItem li = new ListItem("����","%");
			this.ddlComputationGroup.Items.Insert(0,li);
		}

		private void LoadUnit(string strgroupcode)
		{
			DataTable dtComputationUnit = GetComputationUnit(strgroupcode);
			if(dtComputationUnit.Rows.Count == 0)
			{
				dtComputationUnit.Rows.Add(dtComputationUnit.NewRow());
				dtComputationUnit.Rows[0]["cnbMainUnit"] = false;
			}
			DataGrid2.DataSource = dtComputationUnit;
			DataGrid2.DataBind();
		}

		private void LoadNULLUnit()
		{
			DataTable dtComputationUnit = Helper.Query("select * from tbcomputationUnit where 1<>1");
			if(dtComputationUnit.Rows.Count == 0)
			{
				dtComputationUnit.Rows.Add(dtComputationUnit.NewRow());
				dtComputationUnit.Rows[0]["cnbMainUnit"] = false;
			}
			DataGrid2.DataSource = dtComputationUnit;
			DataGrid2.DataBind();
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
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button4.Click += new System.EventHandler(this.Button4_Click);
			this.Button5.Click += new System.EventHandler(this.Button5_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.DataGrid1.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
			this.DataGrid2.PageIndexChanged+=new DataGridPageChangedEventHandler(DataGrid2_PageIndexChanged);
			this.btnQuery.Click+=new EventHandler(btnQuery_Click);
			this.btnRefreshGroup.Click+=new EventHandler(btnRefreshGroup_Click);
			this.btnRefreshUnit.Click+=new EventHandler(btnRefreshUnit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ListBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//��ʾ������λ
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			//�޸ļ�����λ��
			if(this.DataGrid1.SelectedIndex >=0)
			{
				string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
				string strgroupname = this.DataGrid1.SelectedItem.Cells[1].Text;
                this.ClientScript.RegisterStartupScript(this.GetType(), "modify", "<script type=\"text/javascript\">OpenGroupWin('modify','" + strgroupcode + "','" + strgroupname + "');</script>");
			}
			else
				this.Popup("��ѡ�������λ��");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//ɾ��������λ��
			if(this.DataGrid1.SelectedIndex<0)
			{
				this.Popup("��ѡ�������λ��");
				return;
			}
			string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
			string strgroupname = this.DataGrid1.SelectedItem.Cells[1].Text;

			string strsql = "select * from tbcomputationunit where cnvcgroupcode='"+strgroupcode+"'";
			DataTable dtcomputationunit = Helper.Query(strsql);
			if(dtcomputationunit.Rows.Count>0)
			{
				this.Popup("������λ���ѱ�ʹ���޷�ɾ����");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "ɾ��������λ��";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			Entity.ComputationGroup cg = new Entity.ComputationGroup();
			cg.cnvcGroupCode = strgroupcode;
			cg.cnvcGroupName = strgroupname;

			Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
			int ret = bcu.DeleteComputationGroup(ol,cg);
			LoadGroup();
			if(ret > 0 )
			{
				this.Popup("ɾ��������λ��ɹ���");
				LoadGroup();
			}
			else
			{
				this.Popup("ɾ��������λ��ʧ�ܣ�");
			}

		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
			this.DataGrid2.CurrentPageIndex = 0;
			this.LoadUnit(strgroupcode);
		}

		private void Button4_Click(object sender, System.EventArgs e)
		{
			//��Ӽ�����λ
			if(this.DataGrid1.SelectedIndex >= 0)
			{
				string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
				string strgroupname = this.DataGrid1.SelectedItem.Cells[1].Text;
                this.ClientScript.RegisterStartupScript(this.GetType(), "addunit", "<script type=\"text/javascript\">OpenUnitWin('add','" + strgroupcode + "','" + strgroupname + "','','','','');</script>");

			}
			else
				this.Popup("��ѡ�������λ��");
		}

		private void Button5_Click(object sender, System.EventArgs e)
		{
			//�޸ļ�����λ
			if(this.DataGrid1.SelectedIndex >= 0 && this.DataGrid2.SelectedIndex >= 0)
			{
				string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
				string strgroupname = this.DataGrid1.SelectedItem.Cells[1].Text;
				string strcomunitcode = this.DataGrid2.SelectedItem.Cells[0].Text;
				string strcomunitname = this.DataGrid2.SelectedItem.Cells[1].Text;
				string strmainunit = Convert.ToString(((CheckBox)this.DataGrid2.SelectedItem.Cells[2].Controls[1]).Checked);
				string strchangerate = this.DataGrid2.SelectedItem.Cells[3].Text;
                this.ClientScript.RegisterStartupScript(this.GetType(), "modifyunit", "<script type=\"text/javascript\">OpenUnitWin('modify','" + strgroupcode + "','" + strgroupname + "','" + strcomunitcode + "','" + strcomunitname + "','" + strmainunit + "','" + strchangerate + "');</script>");

			}
			else
				this.Popup("��ѡ�������λ");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//ɾ��������λ
			if(this.DataGrid2.SelectedIndex >= 0)
			{
				Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "ɾ��������λ";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;

				Entity.ComputationUnit cu = new AMSApp.zhenghua.Entity.ComputationUnit();
				cu.cnvcComunitCode = this.DataGrid2.SelectedItem.Cells[0].Text;

				//string strsql = "select * from tbcomputationunit where cnvccomunitcode='"+cu.cnvcComunitCode+"' and cnbmainunit=1";
				DataTable dt = Application["tbInventory"] as DataTable;
				DataView dv = new DataView(dt);
				dv.RowFilter = "cnvcproduceunitcode='"+cu.cnvcComunitCode+"' or cnvcstcomunitcode='"+cu.cnvcComunitCode+"'";
				if(dv.Count > 0)
				{
					this.Popup("����ɾ���Ѿ�ʹ���еļ�����λ");
					return;
				}
				Business.ComputationUnit bcu = new AMSApp.zhenghua.Business.ComputationUnit();
				int ret = bcu.DeleteComputationUnit(ol,cu);

				DataGrid1_SelectedIndexChanged(null,null);
				if(ret > 0 )
				{
					this.Popup("ɾ��������λ�ɹ���");
					LoadGroup();
				}
				else
				{
					this.Popup("ɾ��������λʧ�ܣ�");
				}
			}
			else
				this.Popup("��ѡ�������λ");
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.LoadGroup();
		}

		private void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			if(this.DataGrid1.SelectedIndex > -1)
			{
				this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
				//DataGrid1_SelectedIndexChanged(null,null);
				string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
				//this.DataGrid2.CurrentPageIndex = 0;
				this.LoadUnit(strgroupcode);
			}
			else
			{
				this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
				BindDataGrid2();
			}
		}
		private void BindDataGrid2()
		{
			string strsql = "select * from tbcomputationUnit where cnvcGroupCode like '"+this.ddlComputationGroup.SelectedValue+"' ";
			if(this.txtComUnitCode.Text != "")
				strsql += " and cnvcComUnitcode like '"+this.txtComUnitCode.Text+"%'";
			if(this.txtComUnitName.Text != "")
				strsql += " and cnvcComUnitName like '%"+this.txtComUnitName.Text+"%'";
			DataTable dt = Helper.Query(strsql);
			//
			this.DataGrid2.DataSource = dt;
			this.DataGrid2.DataBind();
		}
		private void btnQuery_Click(object sender, EventArgs e)
		{
			//
			this.DataGrid2.CurrentPageIndex = 0;
			this.DataGrid1.SelectedIndex = -1;
			BindDataGrid2();
		}

		private void btnRefreshGroup_Click(object sender, EventArgs e)
		{
			//ˢ�¼�����λ��
			LoadGroup();
		}

		private void btnRefreshUnit_Click(object sender, EventArgs e)
		{
			//ˢ�¼�����λ
			if(this.DataGrid1.SelectedIndex > -1)
			{
				//this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
				//DataGrid1_SelectedIndexChanged(null,null);
				string strgroupcode = this.DataGrid1.SelectedItem.Cells[0].Text;
				//this.DataGrid2.CurrentPageIndex = 0;
				this.LoadUnit(strgroupcode);
			}
			else
			{
				//this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
				BindDataGrid2();
			}
		}
	}
}
