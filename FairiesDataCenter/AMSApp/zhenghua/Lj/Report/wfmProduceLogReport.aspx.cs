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
	/// wfmProduceLogReport 的摘要说明。
	/// </summary>
	public class wfmProduceLogReport : wfmBase
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
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			string strSql = "";
//			if(ddlDept.SelectedValue == "全部")
//			{
//				strSql = "select cnnOperSerialNo as '操作序号',cnvcOperType as '操作类型',cnvcDeptID as '部门',cnvcOperID as '操作员',cndOperDate as '操作日期' from tbOperLog where convert(char(10),cndOperDate,121) >= '"+txtBeginDate.Text+"' and  "
//					+ " convert(char(10),cndOperDate,121) <='"+txtEndDate.Text+"' and cnvcOperType like '生产产品%' "
//					+ "  order by cnnOperSerialNo";
//			}
//			else
//			{
				strSql = "select cnnOperSerialNo as '操作序号',cnvcOperType as '操作类型',cnvcComments as '描述',cnvcDeptID as '部门',cnvcOperID as '操作员',cndOperDate as '操作日期'  from tbOperLog where convert(char(10),cndOperDate,121) >= '"+txtBeginDate.Text+"' and  "
					+ " convert(char(10),cndOperDate,121) <='"+txtEndDate.Text+"' and cnvcOperType in (select cnvcname from tbnamecode where cnvctype='OPERTYPE')  "
					+ " and cnvcDeptID like '"+ddlDept.SelectedValue+"' order by cnnOperSerialNo";
//			}

			DataTable dtOut = Helper.Query(strSql);

			//this.DataTableConvert(dtOut, "cnvcDeptID", "tbCommCode", "vcCommCode", "vcCommName", "vcCommSign='MD'");
			//this.DataTableConvert(dtOut, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");

			this.TableConvert(dtOut,"部门","tbCommCode","vcCommCode","vcCommName","vcCommSign='MD'");
			this.TableConvert(dtOut,"操作员","tbLogin","vcLoginID","vcOperName");
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
