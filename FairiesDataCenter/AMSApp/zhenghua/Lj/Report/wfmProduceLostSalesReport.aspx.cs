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
	/// wfmProduceLostSalesReport ��ժҪ˵����
	/// </summary>
	public class wfmProduceLostSalesReport : wfmBase
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
				//this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","ȫ��");
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			Session.Remove("QUERY");
			Session.Remove("toExcel");

//			string strSql1 = "";
//			string strSql2 = "";
//			string strSql3 = "";
//			string strSql4 = "";
//			if(ddlDept.SelectedValue == "ȫ��")
//			{
//				strSql1 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������' from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql2 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������' from tbProductLostSerial where convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql3 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������' from tbSalesSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql4 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '�̵���' from tbCheckSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//			}
//			else
//			{
//				strSql1 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������'   from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql2 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������'   from tbProductLostSerial where convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql3 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '������'   from tbSalesSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql4 = "select cnvcDeptID as '����',cnvcCode as '��Ʒ����',cnvcName as '��Ʒ����',sum(cnnCount+cnnAddCount-cnnReduceCount) as '�̵���'   from tbCheckSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//			}
//
//			DataTable dtOut1 = Helper.Query(strSql1);			
//			DataTable dtOut2 = Helper.Query(strSql2);
//			DataTable dtOut3 = Helper.Query(strSql3);
//			DataTable dtOut4 = Helper.Query(strSql4);
//
//
//			dtOut1.Columns.Add("������");
//			dtOut1.Columns.Add("������");
//			dtOut1.Columns.Add("�̵���");
//			foreach(DataRow dr1 in dtOut1.Rows)
//			{
//				foreach(DataRow dr2 in dtOut2.Rows)
//				{
//					if(dr1["����"].ToString() == dr2["����"].ToString() && dr1["��Ʒ����"].ToString() == dr2["��Ʒ����"].ToString())
//					{
//						dr1["������"] = dr2["������"].ToString();
//					}
//					
//				}
//				foreach(DataRow dr3 in dtOut3.Rows)
//				{
//					if(dr1["����"].ToString() == dr3["����"].ToString() && dr1["��Ʒ����"].ToString() == dr3["��Ʒ����"].ToString())
//					{
//						dr1["������"] = dr3["������"].ToString();
//					}
//					
//				}
//				foreach(DataRow dr4 in dtOut4.Rows)
//				{
//					if(dr1["����"].ToString() == dr4["����"].ToString() && dr1["��Ʒ����"].ToString() == dr4["��Ʒ����"].ToString())
//					{
//						dr1["�̵���"] = dr4["�̵���"].ToString();
//					}
//					
//				}
//			}
			//[ProduceSalesReport]
			DataTable dtOut1 = Helper.QueryLongTrans("ProduceSalesReport '"+ddlDept.SelectedValue+"','"+txtBeginDate.Text+"','"+txtEndDate.Text+"'");			
			//this.TableConvert(dtOut1,"����","tbCommCode","vcCommCode","vcCommName","vcCommSign='MD'");
			//this.DataTableConvert(dtOut1,"����","����","tbDept","cnvcDeptID","cnvcDeptName","");
			//this.TableConvert(dtOut,"����Ա","tbLogin","vcLoginID","vcOperName");
			this.DataTableConvert(dtOut1,"����","����","tbDept","cnvcDeptID","cnvcDeptName","");
			this.DataTableConvert(dtOut1,"��Ʒ����","��Ʒ����","tbInventory","cnvcInvCode","cnvcInvName","");
			dtOut1.Columns.Add("������2");
			foreach(DataRow dr in dtOut1.Rows)
			{
				decimal d1 = Convert.ToDecimal(dr["������"].ToString());
				if(d1>0)
				{
					dr["������2"] = "<font color=\"#ff0000\">"+d1.ToString()+"</font>";
				}
				else
				{
					dr["������2"] = d1;
				}
			}
			dtOut1.Columns.Remove("������");
			dtOut1.Columns["������2"].ColumnName = "������";
			Session["QUERY"] = dtOut1;
			Session["toExcel"]=dtOut1;

			UcPageView1.MyDataGrid.PageSize = 30;
			DataView dvOut =new DataView(dtOut1);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
