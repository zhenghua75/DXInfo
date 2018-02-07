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
	/// wfmStorageBalanceReport ��ժҪ˵����
	/// </summary>
	public class wfmStorageBalanceReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProClass;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlProType;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btnRefreshSTBA;
		protected System.Web.UI.WebControls.Button btExcel;
	
		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept,"","ȫ��");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","ȫ��");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProType,"vcCommSign='PRODUCTTYPE'");
				this.FillDropDownList("PClass",this.ddlProClass,"","ȫ��");
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.ddlProType.SelectedIndexChanged += new System.EventHandler(this.ddlProType_SelectedIndexChanged);
			this.btnRefreshSTBA.Click += new System.EventHandler(this.btnRefreshSTBA_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("toExcel");
			if(this.ddlDept.SelectedValue==""||this.ddlWhouse.SelectedValue=="")
			{
				this.Popup("�޲��źͲֿ���Ϣ����Ȩ���ʣ�");
				return;
			}
			string strsql="";
			if(this.ddlProType.SelectedValue=="FINALPRODUCT")
			{
				strsql="select a.cnvcWhCode as �ֿ�,a.cnvcInvCode as �������,c.cnvcInvName as �������,b.cnvcComunitName as ��λ,a.cnnQuantity as ��ǰ�����,";
				strsql+="a.cnnCheckEnter as �̵����,a.cnnPoEnter as �ɹ����,a.cnnPoReturn as �ɹ��˻�,a.cnnProductUse as ����ʹ��,a.cnnProductLost as �������,";
				strsql+="a.cnnProductEnter as �������,a.cnnAssgOut as �ֻ�����,a.cnnAssgEnter as �ֻ����,a.cnnMoveEnter as �������,a.cnnMoveOut as ��������,";
				strsql+="a.cnnTranLost as �����������,a.cnnConsCount as ����,a.cnnConsLost as �������,a.cnnExpLost as �������,cnnProDiff as ����";
				strsql+=" from tbStorageBalanceRelation a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode and b.cnvcComunitCode=c.cnvcSTComunitCode";
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='FINALPRODUCT')";
			}
			else
			{
				strsql="select a.cnvcWhCode as �ֿ�,a.cnvcInvCode as �������,c.cnvcInvName as �������,b.cnvcComunitName as ��λ,a.cnnQuantity as ��ǰ�����,";
				strsql+="a.cnnCheckEnter as �̵����,a.cnnPoEnter as �ɹ����,a.cnnPoReturn as �ɹ��˻�,a.cnnProductUse as ����ʹ��,a.cnnProductLost as �������,";
				strsql+="a.cnnMoveEnter as �������,a.cnnMoveOut as ��������,a.cnnTranLost as �����������,a.cnnConsCount as ����,a.cnnConsLost as �������,";
				strsql+="a.cnnExpLost as �������,cnnMatDiff  as ����";
				strsql+=" from tbStorageBalanceRelation a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode ";
				strsql+=" and b.cnvcComunitCode=c.cnvcSTComunitCode and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType<>'FINALPRODUCT')";
			}
			if(this.ddlWhouse.SelectedValue!="ȫ��")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			else
			{
				if(this.ddlDept.SelectedValue!="ȫ��")
					strsql+=" and a.cnvcWhCode in(select cnvcWhCode from tbWareHouse where cnvcDepCode='"+this.ddlDept.SelectedValue+"')";
			}
			if(this.ddlProClass.SelectedValue!="ȫ��")
				strsql+=" and c.cnvcInvCCode='"+this.ddlProClass.SelectedValue+"'";
			else
			{
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='"+this.ddlProType.SelectedValue+"')";
			}
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and c.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" order by a.cnvcInvCode,a.cnvcWhCode";

			DataTable dtbalance=Helper.Query(strsql);
			dtbalance.Columns.Add("�鿴");
			foreach(DataRow dr in dtbalance.Rows)
			{
				dr["�鿴"]="<a href='wfmStorageBalanceDetailReport.aspx?whcode="+dr["�ֿ�"].ToString()+"&Invcode=" + dr["�������"].ToString() + "&Invname="+dr["�������"].ToString()+"'>��ϸ</a>";
			}
			this.TableConvert(dtbalance,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			dtbalance.TableName="���ƽ���ϵ����";
			Session["QUERY"] = dtbalance;
			Session["toExcel"]=dtbalance;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtbalance);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","ȫ��");
			}
		}

		private void ddlProType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.FillDropDownList("PClass",this.ddlProClass,"vcCommSign='"+this.ddlProType.SelectedValue+"'","ȫ��");
		}

		private void btnRefreshSTBA_Click(object sender, System.EventArgs e)
		{
			StorageFacade sto = new StorageFacade();				
			int ret = sto.CreateStorageBalance();
			if(ret > 0 )
			{
				this.Popup("����ˢ�¿��ƽ���ϵ�ɹ���");
			}
			else
			{
				this.Popup("����ˢ�¿��ƽ���ϵʧ�ܣ�");
			}
		}
	}
}
