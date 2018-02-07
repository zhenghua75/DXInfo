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
	/// wfmSaleDailyCheckReport 的摘要说明。
	/// </summary>
	public class wfmSaleDailyCheckReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlWhCode;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlFlag;
		protected System.Web.UI.WebControls.Button btExcel;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept,"","全部");
					this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","全部");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				this.ddlFlag.Items.Add(new ListItem("全部","全部"));
				this.ddlFlag.Items.Add(new ListItem("未确认","0"));
				this.ddlFlag.Items.Add(new ListItem("已确认","1"));
				this.ddlFlag.SelectedIndex=0;
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
				strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
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
			string strsql="select a.cnvcCheckNo as 盘点序号,a.cnnSerialNo as 盘点流水,a.cnvcDeptID as 部门,a.cnvcWhCode as 仓库,a.cnvcInvCode as 存货编码,b.cnvcInvName as 存货名称,";
			strsql+="a.cnvcUnitCode as 单位,a.cnnSysCount as 系统存量,a.cnnCheckCount as 盘点存量,convert(char(10),a.cndMdate,120) as 生产日期,convert(char(10),a.cndExpDate,120) as 过期日期,";
			strsql+="a.cnvcOperName as 操作员,a.cndOperDate as 操作日期,(case a.cnvcFlag when '0' then '未确认' else '已确认更新' end) as 确认标志";
			strsql+=" from tbStorageCheckLog a,tbInventory b where a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59' and a.cnvcInvCode=b.cnvcInvCode";
			if(this.ddlFlag.SelectedValue!="全部")
				strsql+=" and a.cnvcFlag='"+this.ddlFlag.SelectedValue+"'";
			if(this.ddlDept.SelectedValue!="全部")
				 strsql+=" and a.cnvcDeptID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhCode.SelectedValue!="全部")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhCode.SelectedValue+"'";
			if(this.txtInvCode.Text.Trim()!="")
				strsql+=" and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"'";
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and b.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" order by a.cnvcCheckNo,a.cnnSerialNo";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"单位","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="仓库库存盘点查询";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","全部");
			}
		}
	}
}
