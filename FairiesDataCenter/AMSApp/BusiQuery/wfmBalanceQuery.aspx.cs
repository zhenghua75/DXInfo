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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// wfmBalanceQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmBalanceQuery : wfmBase
	{
		protected string strEndDate;
		protected string strBeginDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected string strExcelPath = string.Empty;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0');");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","ȫ��");
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}

				if(this.DataGrid1.DataSource!=null)
				{
					if(((DataTable)this.DataGrid1.DataSource).Rows.Count>0)
					{						
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
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
			this.btnProcBalance.Click+=new EventHandler(btnProcBalance_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);

		}
		#endregion

		protected void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}

			string beginDate = Convert.ToDateTime(strBeginDate).ToString("yyyy-MM-dd");
			string endDate = Convert.ToDateTime(strEndDate).ToString("yyyy-MM-dd");
			string deptId = ddlDept.SelectedValue;
			if(deptId=="ȫ��") deptId = "";
			string strSql = "proc_Balance_Report '{0}','{1}','{2}'";
			DataTable dt = AMSApp.zhenghua.Business.Helper.Query(string.Format(strSql,beginDate,endDate,deptId));
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
			if(dt.Rows.Count<=0)
			{
				btnExcel.Enabled=false;
			}
			else
			{
				btnExcel.Enabled=true;	
			}
			Session["QUERY"] = dt;
			DataTable dtExcel = dt.Copy();
            dtExcel.Columns["vcLocalDeptName"].ColumnName = "�����ŵ�";
			dtExcel.Columns["vcRemoteDeptName"].ColumnName = "�ŵ�";
			dtExcel.Columns["nFillFee_Pay"].ColumnName = "��ֵ���(֧��)";
			dtExcel.Columns["nFillProm_Pay"].ColumnName = "��ֵ���ͽ��(֧��)";
			dtExcel.Columns["nFee_Pay"].ColumnName = "���ѽ��(֧��)";
			dtExcel.Columns["sumFee_Pay"].ColumnName = "С��(֧��)";
			dtExcel.Columns["nFillFee_Income"].ColumnName = "��ֵ���(����)";
			dtExcel.Columns["nFillProm_Income"].ColumnName = "��ֵ���ͽ��(����)";
			dtExcel.Columns["nFee_Income"].ColumnName = "���ѽ��(����)";
			dtExcel.Columns["sumFee_Income"].ColumnName = "С��(����)";
			dtExcel.Columns["nFillFee_Dif"].ColumnName = "��ֵ���(���)";
			dtExcel.Columns["nFillProm_Dif"].ColumnName = "��ֵ���ͽ��(���)";
			dtExcel.Columns["nFee_Dif"].ColumnName = "���ѽ��(���)";
			dtExcel.Columns["sumFee_Dif"].ColumnName = "С��(���)";

			Session["toExcel"]=dtExcel;
		}

		private void btnProcBalance_Click(object sender, System.EventArgs e)
		{
			string strdays= txtDays.Text;
			
			string strSql = "Proc_Balance_ByDate {0}";
			AMSApp.zhenghua.Business.Helper.ExcuteNoQuery(string.Format(strSql,strdays));
			this.Popup("����������ݳɹ�");
		}

		private void DataGrid1_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//string txt = "";
			if(e.Item.ItemType == ListItemType.Header )
			{
				e.Item.Cells[0].ColumnSpan=2;
				e.Item.Cells[0].RowSpan =2;
				e.Item.Cells[0].Visible=false;
				e.Item.Cells[1].Visible = false;	
							
				e.Item.Cells[2].Visible= false;
				e.Item.Cells[3].Visible = false;
				e.Item.Cells[4].Visible = false;
				e.Item.Cells[5].Visible=false;
				e.Item.Cells[5].ColumnSpan = 4;

				
				e.Item.Cells[6].Visible = false;
				e.Item.Cells[7].Visible = false;
				e.Item.Cells[8].Visible=false;
				e.Item.Cells[9].Visible = false;
				e.Item.Cells[9].ColumnSpan = 4;

				
				e.Item.Cells[10].Visible = false;
				e.Item.Cells[11].Visible = false;
				e.Item.Cells[12].Visible = false;
				//e.Item.Cells[13].Visible = false;
				e.Item.Cells[13].ColumnSpan = 2;
				e.Item.Cells[13].RowSpan=2;
				
				

				e.Item.Cells[13].Text = @"�ŵ�</td>
		<td colspan=4 align='center'>֧��</td>
		<td colspan=4 align='center'>����</td>
		<td colspan=4 align='center'>���(����-֧��)</td>
	</tr>
	<tr>
		
		<td>��ֵ���</td>
<td>��ֵ���ͽ��</td>
		<td>���ѽ��</td>
		<td>С��</td>
		<td>��ֵ���</td>
<td>��ֵ���ͽ��</td>
		<td>���ѽ��</td>
		<td>С��</td>
		<td>��ֵ���</td>
<td>��ֵ���ͽ��</td>
		<td>���ѽ��</td>
		<td>С��</td>
	</tr>";

			}
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
