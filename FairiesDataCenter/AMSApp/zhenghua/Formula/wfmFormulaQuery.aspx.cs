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

namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmFormulaQuery 的摘要说明。
	/// </summary>
	public class wfmFormulaQuery : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtinvcode;
		protected System.Web.UI.WebControls.TextBox txtinvname;
		protected System.Web.UI.WebControls.Button btnAdd;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{

				this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' and (cnvcCode='SEMIPRODUCT' or cnvcCode = 'FINALPRODUCT')");

				ListItem li = new ListItem("所有", "%");
				this.ddlProductType.Items.Insert(0, li);
				BindProductClass(ddlProductClass,
					"cnvcProductType in('SEMIPRODUCT','FINALPRODUCT') and cnvcProductType like '" +
					ddlProductType.SelectedValue + "'");

				
				this.ddlProductClass.Items.Insert(0, li);

				btnQuery_Click(null,null);
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
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			//添加
			this.Response.Redirect("./wfmFormula.aspx?flag=add");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//取消
			this.txtProductCode.Text = "";
			this.txtProductName.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}

		private void BindDataGrid()
		{
			//string strSql = "select cnvcProductCode,cnvcProductName,cnvcProductType,cnvcProductClass,cnnCostSum,cnvcUnit,cnvcFeel,cnvcOrganise,cnvcColor,cnvcTaste,b.nPrice as cnnPrice from tbFormula a left outer join tbGoods b on a.cnvcProductCode=b.vcGoodsID where 1=1 ";
//			string strSql = "select a.cnvcinvcode as cnvcProductCode,a.cnvcinvname as cnvcProductName,b.cnvcProductType, "
//+" a.cnvcinvccode as cnvcProductClass,a.cniinvncost as cnnCostSum,c.cnvccomunitname as cnvcUnit,cnvcFeel,cnvcOrganise,cnvcColor,cnvcTaste, "
//+" a.cnfretailprice as cnnPrice  "
//+" from tbinventory a left outer join tbproductclass b "
//+" on a.cnvcinvccode=b.cnvcproductclasscode "
//+" left outer join tbcomputationunit c on a.cnvcshopunitcode=c.cnvccomunitcode where 1=1 ";
			string strSql = @"
select a.cnvcinvcode as cnvcProductCode,a.cnvcinvname as cnvcProductName,
b.cnvcProductType, 
 a.cnvcinvccode as cnvcProductClass,
a.cniinvncost as cnnCostSum,c.cnvccomunitname as cnvcUnit,
cnvcFeel,cnvcOrganise,cnvcColor,cnvcTaste, 
 a.cnfretailprice as cnnPrice ,case when f.bomcount>0 then '有' else '无' end as havebom
 from tbinventory a 
left outer join tbproductclass b on a.cnvcinvccode=b.cnvcproductclasscode 
left outer join tbcomputationunit c on a.cnvcproduceunitcode=c.cnvccomunitcode
left outer join (select d.cnvcinvcode,count(*) as bomcount from tbinventory d
join tbbillofmaterials e on d.cnvcinvcode=e.cnvcpartinvcode
group by d.cnvcinvcode) f on a.cnvcinvcode=f.cnvcinvcode where 1=1";
			if(ddlProductType.SelectedValue == "%")
			{
				strSql += " and (b.cnvcProductType='SEMIPRODUCT' or b.cnvcProductType='FINALPRODUCT')";
			}
			else
			{
				strSql += " and b.cnvcProductType='" + ddlProductType.SelectedValue+"'";
			}
			//strSql += "a.cnvcProductType like '" + ddlProductType.SelectedValue;
			strSql += " and isnull(a.cnvcinvccode,'') like '" + ddlProductClass.SelectedValue + "' and a.cnvcinvcode like '%" +
			txtProductCode.Text + "%' and cnvcinvname like '%" + txtProductName.Text + "%'";
			if (txtinvcode.Text != "" || txtinvname.Text != "")
			strSql += " and a.cnvcinvcode in (select g.cnvcpartinvcode from tbbillofmaterials g "
+" left outer join tbinventory h on g.cnvccomponentinvcode=h.cnvcinvcode "
+" where g.cnvccomponentinvcode like '%"+txtinvcode.Text+"%' "
+" and h.cnvcinvname like '%"+txtinvname.Text+"%') " ;
			DataTable dtFormula = Helper.Query(strSql);
			this.DataTableConvert(dtFormula, "cnvcProductType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='PRODUCTTYPE'");
			this.DataTableConvert(dtFormula, "cnvcProductClass", "tbProductClass", "cnvcProductClassCode", "cnvcProductClassName", "");
			DataGrid1.DataSource = dtFormula;
			
			DataGrid1.DataBind();
		}
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//查询
			this.DataGrid1.CurrentPageIndex = 0;
			BindDataGrid();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindDataGrid();			
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			BindProductClass(ddlProductClass,
			                 "cnvcProductType in('SEMIPRODUCT','FINALPRODUCT') and cnvcProductType like '" +
			                 ddlProductType.SelectedValue + "'");
			ListItem li = new ListItem("所有", "%");
			this.ddlProductClass.Items.Insert(0, li);
		}

		private void btnCost_Click(object sender, System.EventArgs e)
		{
			//成本刷新
			try
			{
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "成本刷新";

				MaterialFacade mf = new MaterialFacade();
				mf.UpdateCost(operLog);
				Popup("成本刷新成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}
	}
}
