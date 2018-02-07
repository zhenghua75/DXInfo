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
	/// wfmBalanceQuery 的摘要说明。
	/// </summary>
	public partial class wfmBalanceQuery : wfmBase
	{
		protected string strEndDate;
		protected string strBeginDate;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected string strExcelPath = string.Empty;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0');");
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}

			string beginDate = Convert.ToDateTime(strBeginDate).ToString("yyyy-MM-dd");
			string endDate = Convert.ToDateTime(strEndDate).ToString("yyyy-MM-dd");
			string deptId = ddlDept.SelectedValue;
			if(deptId=="全部") deptId = "";
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
            dtExcel.Columns["vcLocalDeptName"].ColumnName = "发卡门店";
			dtExcel.Columns["vcRemoteDeptName"].ColumnName = "门店";
			dtExcel.Columns["nFillFee_Pay"].ColumnName = "充值金额(支出)";
			dtExcel.Columns["nFillProm_Pay"].ColumnName = "充值赠送金额(支出)";
			dtExcel.Columns["nFee_Pay"].ColumnName = "消费金额(支出)";
			dtExcel.Columns["sumFee_Pay"].ColumnName = "小计(支出)";
			dtExcel.Columns["nFillFee_Income"].ColumnName = "充值金额(收入)";
			dtExcel.Columns["nFillProm_Income"].ColumnName = "充值赠送金额(收入)";
			dtExcel.Columns["nFee_Income"].ColumnName = "消费金额(收入)";
			dtExcel.Columns["sumFee_Income"].ColumnName = "小计(收入)";
			dtExcel.Columns["nFillFee_Dif"].ColumnName = "充值金额(差额)";
			dtExcel.Columns["nFillProm_Dif"].ColumnName = "充值赠送金额(差额)";
			dtExcel.Columns["nFee_Dif"].ColumnName = "消费金额(差额)";
			dtExcel.Columns["sumFee_Dif"].ColumnName = "小计(差额)";

			Session["toExcel"]=dtExcel;
		}

		private void btnProcBalance_Click(object sender, System.EventArgs e)
		{
			string strdays= txtDays.Text;
			
			string strSql = "Proc_Balance_ByDate {0}";
			AMSApp.zhenghua.Business.Helper.ExcuteNoQuery(string.Format(strSql,strdays));
			this.Popup("计算结算数据成功");
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
				
				

				e.Item.Cells[13].Text = @"门店</td>
		<td colspan=4 align='center'>支出</td>
		<td colspan=4 align='center'>收入</td>
		<td colspan=4 align='center'>差额(收入-支出)</td>
	</tr>
	<tr>
		
		<td>充值金额</td>
<td>充值赠送金额</td>
		<td>消费金额</td>
		<td>小计</td>
		<td>充值金额</td>
<td>充值赠送金额</td>
		<td>消费金额</td>
		<td>小计</td>
		<td>充值金额</td>
<td>充值赠送金额</td>
		<td>消费金额</td>
		<td>小计</td>
	</tr>";

			}
		}

		protected void btnExcel_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
