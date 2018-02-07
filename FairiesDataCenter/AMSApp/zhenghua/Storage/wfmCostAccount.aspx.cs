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

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmCostAccount ��ժҪ˵����
	/// </summary>
	public class wfmCostAccount : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlQueryType;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Button btnInvAccount;
		protected System.Web.UI.WebControls.Button btProductAccount;

		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.ddlQueryType.Items.Add(new ListItem("ԭ���ϳɱ�����","ԭ���ϳɱ�����"));
				this.ddlQueryType.Items.Add(new ListItem("��Ʒ�ɱ�����","��Ʒ�ɱ�����"));
				this.ddlQueryType.SelectedIndex=0;
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnInvAccount.Click += new System.EventHandler(this.btnInvAccount_Click);
			this.btProductAccount.Click += new System.EventHandler(this.btProductAccount_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			string strbegin=DateTime.Now.Year.ToString()+"-"+DateTime.Now.Month.ToString()+"-1";
			string strEnd=DateTime.Now.Year.ToString()+"-"+DateTime.Now.AddMonths(1).Month.ToString()+"-1";
			string strsql="select top 10 cnnOperSerialNo as ������ˮ,cnvcOperType as ��������,cnvcOperID as ����Ա���,b.vcOperName as ����Ա����,cnvcDeptID as ����,";
			strsql+="cndOperDate as ����ʱ��,cnvcComments as ���� from tbOperLog a,tbLogin b where cnvcOperType='"+this.ddlQueryType.SelectedValue+"' and a.cnvcOperID=b.vcLoginID";
			strsql+=" and a.cndOperDate between '"+strbegin+"' and '"+strEnd+"' order by cnnOperSerialNo desc";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			dtout.TableName="�ɱ����������־";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 10;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnInvAccount_Click(object sender, System.EventArgs e)
		{
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "ԭ���ϳɱ�����";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.InventoryCostAccount(ol,"mate");
			if(ret > 0 )
			{
				this.Popup("ԭ���ϳɱ�����ɹ���");
			}
			else
			{
				this.Popup("ԭ���ϳɱ�����ʧ�ܣ�");
			}
		}

		private void btProductAccount_Click(object sender, System.EventArgs e)
		{
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "��Ʒ�ɱ�����";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.InventoryCostAccount(ol,"prod");
			if(ret > 0 )
			{
				this.Popup("��Ʒ�ɱ�����ɹ���");
			}
			else
			{
				this.Popup("��Ʒ�ɱ�����ʧ�ܣ�");
			}
		}
	}
}
