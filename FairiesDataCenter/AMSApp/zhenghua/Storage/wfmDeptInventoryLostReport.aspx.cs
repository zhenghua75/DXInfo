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
	/// wfmDeptInventoryLostReport 的摘要说明。
	/// </summary>
	public class wfmDeptInventoryLostReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhCode;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlLostType;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
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
				this.FillDropDownList("tbNameCodeToStorage",this.ddlLostType,"vcCommSign='LostType' and vcCommCode<>'0'","全部");
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
			string strsql="select t.相关单号,t.部门,t.仓库,t.存货,t.单位,sum(t.损耗数量) as 损耗数量,t.损耗时间,t.操作员,t.损耗类型,t.备注";
			strsql+="  from (select '' as 相关单号,a.cnvcDeptID as 部门,a.cnvcWhCode as 仓库,b.cnvcInvName as 存货,a.cnvcComunitCode as 单位,";
			strsql+="a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount as 损耗数量,a.cndLostDate as 损耗时间,a.cnvcOperID as 操作员,a.cnvcLostType as 损耗类型,a.cnvcComments as 备注";
			strsql+=" from tbLostSerial a,tbInventory b where a.cnvcInvalidFlag='1' and cnvcLostType='1' and a.cnvcInvCode=b.cnvcInvCode";
			strsql+=" union all select c.cnvcCode as 相关单号,a.cnvcDeptID as 部门,a.cnvcWhCode as 仓库,b.cnvcInvName as 存货,a.cnvcComunitCode as 单位,";
			strsql+="a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount as 损耗数量,a.cndLostDate as 损耗时间,a.cnvcOperID as 操作员,a.cnvcLostType as 损耗类型,a.cnvcComments as 备注";
			strsql+=" from tbLostSerial a,tbInventory b,tbRdRecord c where a.cnvcInvalidFlag='1' and cnvcLostType='2' and a.cnvcInvCode=b.cnvcInvCode and a.cnnProduceSerialNo=c.cnnRdID) t";
			strsql+=" where t.损耗时间 between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlLostType.SelectedValue!="全部")
				strsql+=" and t.损耗类型='"+this.ddlLostType.SelectedValue+"'";
			if(this.ddlDept.SelectedValue!="全部")
				strsql+=" and t.部门='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhCode.SelectedValue!="全部")
				strsql+=" and t.仓库='"+this.ddlWhCode.SelectedValue+"'";
			strsql+=" group by t.相关单号,t.部门,t.仓库,t.存货,t.单位,t.损耗时间,t.操作员,t.损耗类型,t.备注 order by t.损耗时间";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"部门","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"仓库","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"单位","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"操作员","tbLogin","vcLoginID","vcOperName");
			this.TableConvert(dtout,"损耗类型","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='LostType'");
			dtout.TableName="销售损耗统计";
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
