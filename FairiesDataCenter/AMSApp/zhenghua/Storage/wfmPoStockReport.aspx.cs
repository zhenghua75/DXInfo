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
	/// wfmPoStockReport 的摘要说明。
	/// </summary>
	public class wfmPoStockReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtCycle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btExcel;
	
		protected ucPageView UcPageView1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// 在此处放置用户代码以初始化页面
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.FillDropDownList("Provider",this.ddlProvider,"","全部");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strCycle=this.txtCycle.Text.Trim();
			string strsql="select a.cnvcPlanCycle as 采购周期,a.cnvcPrvdCode as 供应商,count(distinct a.cnvcPoID) as 采购订单数,b.cnvcGoodsCode as 采购存货编码,c.cnvcInvName as 采购存货,b.cnvcStockUnit as 采购单位,";
			strsql+="b.cnnStockPrice as 采购单价,sum(b.cnnStockCountSum) as 总采购数量,sum(b.cnnStockFeeSum) as 总采购金额,sum(b.cnnArriveCount) as 总到货数量,sum(b.cnnArriveFee) as 总到货金额,";
			strsql+="cast(cast((round(sum(b.cnnArriveCount)/sum(b.cnnStockCountSum),4)*100) as numeric(10,2)) as varchar(10))+'%' as 完成度 from tbPoStockMain a,tbPoStockSum b,tbInventory c";
			strsql+=" where a.cnvcPoID=b.cnvcPoID and b.cnvcGoodsCode=c.cnvcInvCode";
			if(strCycle!="")
				strsql+=" and a.cnvcPlanCycle='"+strCycle+"'";
			if(this.ddlProvider.SelectedValue!="全部")
				strsql+=" and a.cnvcPrvdCode='"+this.ddlProvider.SelectedValue+"'";
			strsql+=" group by a.cnvcPlanCycle,a.cnvcPrvdCode,b.cnvcGoodsCode,c.cnvcInvName,b.cnvcStockUnit,b.cnnStockPrice order by a.cnvcPlanCycle,a.cnvcPrvdCode,b.cnvcGoodsCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"供应商","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtout,"采购单位","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="采购报表";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
