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
namespace AMSApp.zhenghua.Lj.Report
{
	/// <summary>
	/// wfmProductLostListReport ��ժҪ˵����
	/// </summary>
	public class wfmProductLostListReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtBeginDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
	
		protected ucPageView UcPageView1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.Button1.Attributes.Add("onclick","javascript:window.open('../../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			if(!this.IsPostBack)
			{
				//this.FillDropDownList("tbDept", ddlDept, "cnvcDeptType<>'Corp'","ȫ��");
//				this.BindDept(ddlDept,"cnvcDeptType<>'Corp'");
//				ListItem li = new ListItem("����","%");
//				this.ddlDept.Items.Insert(0,li);
				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("����", "%");
				this.ddlDept.Items.Add(li);
				this.SetDDL(this.ddlDept,this.oper.strDeptID);
				if(this.oper.strDeptID !="CEN00")
				{				
					this.ddlDept.Enabled = false;		
				}
				//this.ddlDept.Items.Add("ȫ��");
				this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
			this.Load += new System.EventHandler(this.Page_Load);
this.Button2.Click += new System.EventHandler(this.Button2_Click);
		}
		#endregion

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			string strSql = "";
//			if(ddlDept.SelectedValue == "ȫ��")
//			{
//				strSql = "select cnnLostSerialNo as '�������',cnvcInvCode as '��Ʒ����',cnvcInvName as '��Ʒ����',cnnLostCount+cnnAddCount-cnnReduceCount as '��������',convert(char(10),cndLostDate,121) as '��������',cnvcDeptID as '������',cnvcOperID as '����Ա',cndOperDate as '��������' from tbLostSerial where convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"'"
//					+ "  order by cnnLostSerialNo,cnvcInvCode";
			//}
//			else
//			{
				strSql = "select cnnLostSerialNo as '�������',cnvcInvCode as '��Ʒ����','' as '��Ʒ����',cnnLostCount+cnnAddCount-cnnReduceCount as '��������',cnvccomments as '����ԭ��',convert(char(10),cndLostDate,121) as '��������',cnvcDeptID as '������',cnvcOperID as '����Ա',cndOperDate as '��������'  from tbLostSerial where cnvcLostType='0' ";
				if(this.txtBeginDate.Text != "") strSql += " and convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"'";
				if(this.txtEndDate.Text != "") strSql 	+= " and  convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"'";
					strSql += " and cnvcDeptID like '"+ddlDept.SelectedValue+"' order by cnnLostSerialNo,cnvcInvCode";
//			}

			DataTable dtOut = Helper.Query(strSql);

			//this.DataTableConvert(dtOut, "cnvcDeptID", "tbCommCode", "vcCommCode", "vcCommName", "vcCommSign='MD'");
			//this.DataTableConvert(dtOut, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");

			//this.TableConvert(dtOut,"������","tbDept","cnvcDeptID","cnvcDeptName","");
			//this.TableConvert(dtOut,"����Ա","tbLogin","vcLoginID","vcOperName");
			//this.TableConvert(dtOut,"��Ʒ����","tbInventory",
			this.DataTableConvert(dtOut,"��Ʒ����","��Ʒ����","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtOut,"������","������","tbDept","cnvcDeptID","cnvcDeptName","");
			this.DataTableConvert(dtOut,"����Ա","����Ա","tbLogin","vcLoginID","vcOperName","");
			//			this.CodeConvert(dtOut,"��������","tbCommCode","vcCommSign='MD'");
			//			this.TableConvert(dtOut,"����Ա","tbLogin");

			Session["QUERY"] = dtOut;
			Session["toExcel"]=dtOut;

			UcPageView1.MyDataGrid.PageSize = 30;
			DataView dvOut =new DataView(dtOut);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();

		}
	}
}
