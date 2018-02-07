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
	/// wfmProductListReport 的摘要说明。
	/// </summary>
	public class wfmProductListReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.TextBox txtBeginDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.Button Button1;

		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Button1.Attributes.Add("onclick","javascript:window.open('../../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			if(!this.IsPostBack)
			{
//				this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","全部");
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

			string strSql = "";
//			if(ddlDept.SelectedValue == "全部")
//			{
//				strSql = "select cnnSerialNo as '生产序号',cnvcCode as '产品编码',cnvcName as '产品名称',cnnCount+cnnAddCount-cnnReduceCount as '生产数量',convert(char(10),cndCreateDate,121) as '生产日期',cnvcDeptID as '生产部门',cnvcOperID as '操作员',cndOperDate as '操作日期' from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ "  order by cnnSerialNo,cnvcCode";
				strSql = "SELECT cnnproduceserialno as '生产序号',cnvcInvCode as '产品编码','' AS '产品名称',cnnProduceCount as '计划生产数量',cnnCheckCount as '实际生产数量',CONVERT(char(10),cndmdate,121) as '生产日期',CONVERT(char(10),cndExpDate,121) as '过期日期',cnvcProduceDeptID as '生产部门',cnvcOperID as '操作员',CONVERT(char(10),cndOperDate,121)  as '操作日期' FROM tbProduceCheckLog"
					+ " where cnnCheckCount>0 ";
			if(this.txtBeginDate.Text != "")
			{
				strSql += " and convert(char(10),cndMDate,121) >= '"+txtBeginDate.Text+"'";
			}
			if(this.txtEndDate.Text != "")
			{
				strSql += " and convert(char(10),cndMDate,121) <='"+txtEndDate.Text+"'";
			}
			strSql += " and cnvcProduceDeptID like '"+ddlDept.SelectedValue+"' order by cnnproduceSerialNo,cnvcInvCode";
//			}
//			else
//			{
//				strSql = "select cnnSerialNo as '生产序号',cnvcCode as '产品编码',cnvcName as '产品名称',cnnCount+cnnAddCount-cnnReduceCount as '生产数量',convert(char(10),cndCreateDate,121) as '生产日期',cnvcDeptID as '生产部门',cnvcOperID as '操作员',cndOperDate as '操作日期'  from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
//					+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"' order by cnnSerialNo,cnvcCode";
//			}
			
			DataTable dtOut = Helper.Query(strSql);

			//this.DataTableConvert(dtOut, "cnvcDeptID", "tbCommCode", "vcCommCode", "vcCommName", "vcCommSign='MD'");
			//this.DataTableConvert(dtOut, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			
			this.DataTableConvert(dtOut,"生产部门","生产部门","tbDept","cnvcDeptID","cnvcDeptName","");
			this.DataTableConvert(dtOut,"操作员","操作员","tbLogin","vcLoginID","vcOperName","");
			this.DataTableConvert(dtOut,"产品编码","产品名称","tbInventory","cnvcInvCode","cnvcInvName","");
			//this.TableConvert(dtOut,"生产部门","tbCommCode","vcCommCode","vcCommName","vcCommSign='MD'");
			//this.TableConvert(dtOut,"操作员","tbLogin","vcLoginID","vcOperName");
//			this.CodeConvert(dtOut,"生产部门","tbCommCode","vcCommSign='MD'");
//			this.TableConvert(dtOut,"操作员","tbLogin");

			Session["QUERY"] = dtOut;
			Session["toExcel"]=dtOut;

			UcPageView1.MyDataGrid.PageSize = 30;
			DataView dvOut =new DataView(dtOut);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();

		}
	}
}
