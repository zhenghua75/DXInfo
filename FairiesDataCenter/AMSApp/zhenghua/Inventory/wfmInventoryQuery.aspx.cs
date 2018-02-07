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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;
namespace AMSApp.zhenghua.Inventory
{
	/// <summary>
	/// wfmInventoryQuery 的摘要说明。
	/// </summary>
	public class wfmInventoryQuery : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidflag;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Response.Expires = -1;
			this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
			this.Response.CacheControl = "no-cache";
			this.Button2.Attributes.Add("onclick","window.close()");
			if(!this.IsPostBack)
			{

				if(this.Request.QueryString["flag"] == null)
				{
					this.Popup("无效链接");
					return;
				}
					string strflag = this.Request.QueryString["flag"].ToString();
					this.hidflag.Value = strflag;					
				
				if(strflag == "part")
				{
					this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' and (cnvcCode='SEMIPRODUCT' or cnvcCode = 'FINALPRODUCT')");
				}
				else
				{
					this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' ");
				}
				ListItem li = new ListItem("所有", "%");
				this.ddlProductType.Items.Insert(0, li);
				BindProductClass(ddlProductClass,
					"cnvcProductType like '" +
					ddlProductType.SelectedValue + "'");

				
				this.ddlProductClass.Items.Insert(0, li);
				LoadProductClass();
				LoadNULLInventory();
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
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			
		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//查询
			this.DataGrid2.CurrentPageIndex = 0;
			//BindDataGrid();
			LoadInventory(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtInvCode.Text,this.txtInvName.Text);
		}
		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			BindProductClass(ddlProductClass,
				"cnvcProductType like '" +
				ddlProductType.SelectedValue + "'");
			ListItem li = new ListItem("所有", "%");
			this.ddlProductClass.Items.Insert(0, li);
		}
		private DataTable GetProductClass()
		{
			string strt = "";
			string strt2 = "";
			if(hidflag.Value == "part")//母件
			{
				strt = "and (cnvcCode='SEMIPRODUCT' or cnvcCode = 'FINALPRODUCT')";
				strt2 = " and a.cnvcProductType='SEMIPRODUCT' or a.cnvcProductType='FINALPRODUCT'";
			}
//			if(hidflag.Value == "child")//子件
//			{
//				strt = "and (cnvcCode='Raw' or cnvcCode = 'Pack')";
//				strt2 = " and a.cnvcProductType='Raw' or a.cnvcProductType='Pack'";
//			}
			return Helper.Query("select a.cnvcProducttype,b.cnvcName as cnvcProductTypeName,a.cnvcProductClassCode,a.cnvcProductClassName from "
				+" tbproductclass a left outer join (select * from tbnamecode where cnvctype = 'PRODUCTTYPE' "+strt+" ) b"
				+" on a.cnvcProductType=b.cnvcCode where 1=1 "+strt2+" order by a.cnvcProducttype,a.cnvcproductclasscode");
		}
		private DataTable GetInventory(string strinvccode)
		{
			DataTable dt= Helper.Query("select * from tbinventory where cnvcinvccode='"+strinvccode+"'");
			this.DataTableConvert(dt,"cnvcinvccode","cnvcinvccode","tbProductClass","cnvcproductclasscode","cnvcproductclassname","");

			this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbComputationGroup","cnvcgroupcode","cnvcgroupname","");

			//this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbProductClass","cnvcgroupcode","cnvcgroupname","");
			this.DataTableConvert(dt,"cnvccomunitcode","cnvccomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcsacomunitcode","cnvcsacomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcpucomunitcode","cnvcpucomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcstcomunitcode","cnvcstcomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcproduceunitcode","cnvcproduceunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcshopunitcode","cnvcshopunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

			this.DataTableConvert(dt,"cnvccreateperson","cnvccreateperson","tblogin","vcloginid","vcopername","");
			this.DataTableConvert(dt,"cnvcmodifyperson","cnvcmodifyperson","tblogin","vcloginid","vcopername","");

			this.DataTableConvert(dt,"cnvcvaluetype","cnvcvaluetype","tbnamecode","cnvccode","cnvcname","cnvctype='VALUETYPE'");
			return dt;
		}
		private DataTable GetInventory(string strproductype,string strproductclass,string strinvccode,string strinvname)
		{
			string strsql = @"select a.* from tbinventory a
left outer join tbproductclass b on a.cnvcinvccode=b.cnvcproductclasscode
where a.cnvcinvcode like '%{0}%'
and a.cnvcinvname like '%{1}%'
and b.cnvcproducttype like '%{2}%'
and a.cnvcinvccode like '%{3}%'";
			if(hidflag.Value=="part")
			{
				strsql += " and b.cnvcProductType in ('SEMIPRODUCT', 'FINALPRODUCT')";
			}
			strsql = string.Format(strsql,strinvccode,strinvname,strproductype,strproductclass);
			DataTable dt= Helper.Query(strsql);
			this.DataTableConvert(dt,"cnvcinvccode","cnvcinvccode","tbProductClass","cnvcproductclasscode","cnvcproductclassname","");

			this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbComputationGroup","cnvcgroupcode","cnvcgroupname","");

			//this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbProductClass","cnvcgroupcode","cnvcgroupname","");
			this.DataTableConvert(dt,"cnvccomunitcode","cnvccomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcsacomunitcode","cnvcsacomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcpucomunitcode","cnvcpucomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcstcomunitcode","cnvcstcomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcproduceunitcode","cnvcproduceunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
			this.DataTableConvert(dt,"cnvcshopunitcode","cnvcshopunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

			this.DataTableConvert(dt,"cnvccreateperson","cnvccreateperson","tblogin","vcloginid","vcopername","");
			this.DataTableConvert(dt,"cnvcmodifyperson","cnvcmodifyperson","tblogin","vcloginid","vcopername","");

			this.DataTableConvert(dt,"cnvcvaluetype","cnvcvaluetype","tbnamecode","cnvccode","cnvcname","cnvctype='VALUETYPE'");

			return dt;
		}

		private void LoadProductClass()
		{
			DataTable dtProductClass = GetProductClass();
			if(dtProductClass.Rows.Count == 0)
			{
				dtProductClass.Rows.Add(dtProductClass.NewRow());
			}
			this.DataGrid1.DataSource = dtProductClass;
			this.DataGrid1.DataBind();
		}
		private void LoadInventory(string strinvccode)
		{
			DataTable dtInventory = GetInventory(strinvccode);
			if(dtInventory.Rows.Count == 0)
			{
				dtInventory.Rows.Add(dtInventory.NewRow());
				dtInventory.Rows[0]["cnbProductBill"] = false;
				dtInventory.Rows[0]["cnbSale"] = false;
				dtInventory.Rows[0]["cnbPurchase"] = false;
				dtInventory.Rows[0]["cnbSelf"] = false;
				dtInventory.Rows[0]["cnbComsume"] = false;
				//dtInventory.Columns.Add("cnvcProduceUnitCodeName");
			}
			this.DataTableConvert(dtInventory,"cnvcProduceUnitCode","cnvcProduceUnitCodeName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataGrid2.DataSource = dtInventory;
			this.DataGrid2.DataBind();
		}
		private void LoadInventory(string strproductype,string strproductclass,string strinvccode,string strinvname)
		{
			DataTable dtInventory = GetInventory(strproductype,strproductclass,strinvccode,strinvname);
			if(dtInventory.Rows.Count == 0)
			{
				dtInventory.Rows.Add(dtInventory.NewRow());
				dtInventory.Rows[0]["cnbProductBill"] = false;
				dtInventory.Rows[0]["cnbSale"] = false;
				dtInventory.Rows[0]["cnbPurchase"] = false;
				dtInventory.Rows[0]["cnbSelf"] = false;
				dtInventory.Rows[0]["cnbComsume"] = false;
				//dtInventory.Columns.Add("cnvcProduceUnitCodeName");
			}
			this.DataTableConvert(dtInventory,"cnvcProduceUnitCode","cnvcProduceUnitCodeName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataGrid2.DataSource = dtInventory;
			this.DataGrid2.DataBind();
		}
		private void LoadNULLInventory()
		{
			DataTable dtInventory = Helper.Query("select * from tbinventory where 1<>1");
			if(dtInventory.Rows.Count == 0)
			{
				dtInventory.Rows.Add(dtInventory.NewRow());
				dtInventory.Rows[0]["cnbProductBill"] = false;
				dtInventory.Rows[0]["cnbSale"] = false;
				dtInventory.Rows[0]["cnbPurchase"] = false;
				dtInventory.Rows[0]["cnbSelf"] = false;
				dtInventory.Rows[0]["cnbComsume"] = false;
				//dtInventory.Columns.Add("cnvcProduceUnitCodeName");
			}
			this.DataTableConvert(dtInventory,"cnvcProduceUnitCode","cnvcProduceUnitCodeName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataGrid2.DataSource = dtInventory;
			this.DataGrid2.DataBind();
		}
		
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//产品类别
			if(e.Item.ItemType == ListItemType.Header)
			{
			
				for(int i=0;i<e.Item.Cells.Count;i++)
				{
					e.Item.Cells[i].Width   =e.Item.Cells[i].Text.Length*18;
				}
			}
		}

		private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//存货档案
			if(e.Item.ItemType == ListItemType.Header)
			{
			
				for(int i=0;i<e.Item.Cells.Count;i++)
				{
					e.Item.Cells[i].Width   =e.Item.Cells[i].Text.Length*18;
				}
			}
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//
			if(this.DataGrid1.SelectedIndex >= 0)
			{
				string strinvccode = this.DataGrid1.SelectedItem.Cells[1].Text;
				this.LoadInventory(strinvccode);
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//选择
			if(this.DataGrid2.SelectedIndex >= 0)
			{
				string strinvcode = this.DataGrid2.SelectedItem.Cells[1].Text;
				if(strinvcode=="nbsp;")return;
				string strinvname = this.DataGrid2.SelectedItem.Cells[2].Text;
				string strinvccode = this.DataGrid2.SelectedItem.Cells[3].Text;
				string strgroupcode = this.DataGrid2.SelectedItem.Cells[20].Text;
				string stProduceUnitCode = this.DataGrid2.SelectedItem.Cells[25].Text;

				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory();
				inv.cnvcInvCode = strinvcode;
				inv.cnvcInvName = strinvname;
				inv.cnvcInvCCode = strinvccode;
				inv.cnvcGroupCode = strgroupcode;
				inv.cnvcProduceUnitCode = stProduceUnitCode;
				
				
				if(hidflag.Value == "part")
				{
					Session["part"] = inv;
					string strsql = "select * from tbinventory where cnvcinvcode in (select cnvcComponentInvCode from tbBillOfMaterials where cnvcPartInvcode='"+strinvcode+"')";
					DataTable dtchild = Helper.Query(strsql);
					Session["child"] = dtchild;

					strsql = "select * from tbBillOfMaterials where cnvcPartInvcode='"+strinvcode+"'";
					DataTable dtBOM = Helper.Query(strsql);
					Entity.BillOfMaterials bom = new BillOfMaterials(dtBOM);
					Session["BaseQtyD"] = bom.cnnBaseQtyD;
					Session["bom"] = dtBOM;

				}
				if(hidflag.Value == "child")
				{
					//子件
					if(Session["child"] == null)
					{
						Session["child"] = inv.ToTable();
					}
					else
					{
						
						if(Session["part"] != null)
						{
							Entity.Inventory invpart = Session["part"] as Entity.Inventory;
							if(invpart.cnvcInvCode == inv.cnvcInvCode)
							{
								this.Popup("母件已选！");
								return;
							}
						}
						DataTable dt = Session["child"] as DataTable;
						DataRow[] drs = dt.Select("cnvcinvcode='"+inv.cnvcInvCode+"'");
						if(drs.Length > 0 )
						{
							this.Popup("子件已选！");
							return;
						}
						int iColloms = dt.Columns.Count;
						object[] oArray = new object[iColloms];
						inv.ToRow().ItemArray.CopyTo(oArray, 0);
						dt.Rows.Add(oArray);

						//dt.Rows.Add(inv.ToRow());
						Entity.BillOfMaterials bom = new BillOfMaterials();
						bom.cnvcComponentInvCode = inv.cnvcInvCode;														   
						if(Session["bom"] == null)
						{							
							Session["bom"] = bom.ToTable();
						}
						DataTable dtbom = Session["bom"] as DataTable;
						
						object[] bomarray = new object[dtbom.Columns.Count];
						bom.ToRow().ItemArray.CopyTo(bomarray,0);
						dtbom.Rows.Add(bomarray);
						Session["bom"] = dtbom;
						Session["child"] = dt;
					}					
				}
			}
			this.Response.Write("<script type=\"text/javascript\">window.close()</script>");
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//关闭
		}
	}
}
