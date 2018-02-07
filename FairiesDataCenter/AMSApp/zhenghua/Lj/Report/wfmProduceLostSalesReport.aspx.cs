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
	/// wfmProduceLostSalesReport 的摘要说明。
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
			// 在此处放置用户代码以初始化页面
			this.Button1.Attributes.Add("onclick","javascript:window.open('../../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			if(!this.IsPostBack)
			{
				//this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
//				this.BindDept(ddlDept,"cnvcDeptType<>'Corp'");
//				ListItem li = new ListItem("所有","%");
//				this.ddlDept.Items.Insert(0,li);
				this.BindDept(ddlDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("所有", "%");
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//查询
			Session.Remove("QUERY");
			Session.Remove("toExcel");

//			string strSql1 = "";
//			string strSql2 = "";
//			string strSql3 = "";
//			string strSql4 = "";
//			if(ddlDept.SelectedValue == "全部")
//			{
//				strSql1 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '生产量' from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql2 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '报损量' from tbProductLostSerial where convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql3 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '销售量' from tbSalesSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//				strSql4 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '盘点量' from tbCheckSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"' group by cnvcDeptID,cnvcCode,cnvcName "
//					+ "  order by cnvcDeptID,cnvcCode";
//			}
//			else
//			{
//				strSql1 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '生产量'   from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql2 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '报损量'   from tbProductLostSerial where convert(char(10),cndLostDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndLostDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql3 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '销售量'   from tbSalesSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"'  group by cnvcDeptID,cnvcCode,cnvcName  order by cnvcDeptID,cnvcCode";
//				strSql4 = "select cnvcDeptID as '部门',cnvcCode as '产品编码',cnvcName as '产品名称',sum(cnnCount+cnnAddCount-cnnReduceCount) as '盘点量'   from tbCheckSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
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
//			dtOut1.Columns.Add("报损量");
//			dtOut1.Columns.Add("销售量");
//			dtOut1.Columns.Add("盘点量");
//			foreach(DataRow dr1 in dtOut1.Rows)
//			{
//				foreach(DataRow dr2 in dtOut2.Rows)
//				{
//					if(dr1["部门"].ToString() == dr2["部门"].ToString() && dr1["产品编码"].ToString() == dr2["产品编码"].ToString())
//					{
//						dr1["报损量"] = dr2["报损量"].ToString();
//					}
//					
//				}
//				foreach(DataRow dr3 in dtOut3.Rows)
//				{
//					if(dr1["部门"].ToString() == dr3["部门"].ToString() && dr1["产品编码"].ToString() == dr3["产品编码"].ToString())
//					{
//						dr1["销售量"] = dr3["销售量"].ToString();
//					}
//					
//				}
//				foreach(DataRow dr4 in dtOut4.Rows)
//				{
//					if(dr1["部门"].ToString() == dr4["部门"].ToString() && dr1["产品编码"].ToString() == dr4["产品编码"].ToString())
//					{
//						dr1["盘点量"] = dr4["盘点量"].ToString();
//					}
//					
//				}
//			}
			//[ProduceSalesReport]
			DataTable dtOut1 = Helper.QueryLongTrans("ProduceSalesReport '"+ddlDept.SelectedValue+"','"+txtBeginDate.Text+"','"+txtEndDate.Text+"'");			
			//this.TableConvert(dtOut1,"部门","tbCommCode","vcCommCode","vcCommName","vcCommSign='MD'");
			//this.DataTableConvert(dtOut1,"部门","部门","tbDept","cnvcDeptID","cnvcDeptName","");
			//this.TableConvert(dtOut,"操作员","tbLogin","vcLoginID","vcOperName");
			this.DataTableConvert(dtOut1,"部门","部门","tbDept","cnvcDeptID","cnvcDeptName","");
			this.DataTableConvert(dtOut1,"产品编码","产品名称","tbInventory","cnvcInvCode","cnvcInvName","");
			dtOut1.Columns.Add("差异量2");
			foreach(DataRow dr in dtOut1.Rows)
			{
				decimal d1 = Convert.ToDecimal(dr["差异量"].ToString());
				if(d1>0)
				{
					dr["差异量2"] = "<font color=\"#ff0000\">"+d1.ToString()+"</font>";
				}
				else
				{
					dr["差异量2"] = d1;
				}
			}
			dtOut1.Columns.Remove("差异量");
			dtOut1.Columns["差异量2"].ColumnName = "差异量";
			Session["QUERY"] = dtOut1;
			Session["toExcel"]=dtOut1;

			UcPageView1.MyDataGrid.PageSize = 30;
			DataView dvOut =new DataView(dtOut1);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
