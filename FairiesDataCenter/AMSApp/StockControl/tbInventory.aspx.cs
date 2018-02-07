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

using System.Web.Script.Serialization;
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
using System.Web.Services;
namespace AMSApp.StockControl
{
	/// <summary>
	/// wfmInventory 的摘要说明。
	/// </summary>
	public class tbInventory : wfmBase
	{
        //#region 字段
        //protected System.Web.UI.WebControls.Label lblTitle;
        //protected System.Web.UI.WebControls.Label Label1;
        //protected System.Web.UI.WebControls.DataGrid DataGrid1;
        //protected System.Web.UI.WebControls.Label Label2;
        //protected System.Web.UI.WebControls.DataGrid DataGrid2;
        //protected System.Web.UI.WebControls.Button Button1;
        //protected System.Web.UI.WebControls.Button Button2;
        //protected System.Web.UI.WebControls.Button Button3;
        //protected System.Web.UI.WebControls.Button Button4;
        //protected System.Web.UI.WebControls.Button Button5;
        //protected System.Web.UI.WebControls.Button Button6;
        //protected System.Web.UI.WebControls.DropDownList ddlProductClass;
        //protected System.Web.UI.WebControls.DropDownList ddlProductType;
        //protected System.Web.UI.WebControls.TextBox txtInvCode;
        //protected System.Web.UI.WebControls.TextBox txtInvName;
        //protected System.Web.UI.WebControls.Button btnQuery;
        //protected System.Web.UI.WebControls.Button btnExcel;
        //protected System.Web.UI.WebControls.Button btnRefreshInv;
        //protected System.Web.UI.WebControls.Button btnRefreshClass;
        //protected System.Web.UI.WebControls.CheckBox chkcn;
        //#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            //Button3.Attributes["OnClick"] = "return confirm('你确定要删除存货分类吗?')";
            //Button6.Attributes["OnClick"] = "return confirm('你确定要删除存货吗?')";
            //this.btnExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

            //this.Button6.Visible = false;
            //if(!this.IsPostBack)
            //{
            //    this.BindNameCode(ddlProductType, "cnvcType = 'PRODUCTTYPE' ");
            //    ListItem li = new ListItem("所有", "%");
            //    this.ddlProductType.Items.Insert(0, li);
            //    BindProductClass(ddlProductClass,
            //        "cnvcProductType like '" +
            //        ddlProductType.SelectedValue + "'");

				
            //    this.ddlProductClass.Items.Insert(0, li);

            //    LoadProductClass();
            //    LoadNULLInventory();

				
			//}
//			if(Session["g1idx"] != null)
//			{
//				string strg1idx = Session["g1idx"].ToString();
//				int ig1idx = Convert.ToInt32(strg1idx);
//				if (ig1idx < this.DataGrid1.Items.Count)
//				{
//					this.DataGrid1.SelectedIndex = ig1idx;
//				}
//			}
//			if(Session["g2idx"] != null)
//			{
//				string strg2idx = Session["g2idx"].ToString();
//				int ig2idx = Convert.ToInt32(strg2idx);
//				if (ig2idx < this.DataGrid2.Items.Count)
//				{
//					this.DataGrid2.SelectedIndex = ig2idx;
//				}
//			}
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
            //this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
            //this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
            //this.DataGrid1.PageIndexChanged +=new DataGridPageChangedEventHandler(DataGrid1_PageIndexChanged);
            //this.Button1.Click += new System.EventHandler(this.Button1_Click);
            //this.Button2.Click += new System.EventHandler(this.Button2_Click);
            //this.Button3.Click += new System.EventHandler(this.Button3_Click);
            //this.DataGrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid2_ItemDataBound);
            //this.DataGrid2.PageIndexChanged +=new DataGridPageChangedEventHandler(DataGrid2_PageIndexChanged);
            //this.Button4.Click += new System.EventHandler(this.Button4_Click);
            //this.Button5.Click += new System.EventHandler(this.Button5_Click);
            //this.Button6.Click += new System.EventHandler(this.Button6_Click);
            //this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            //this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
            //this.btnExcel.Click+=new EventHandler(btnExcel_Click);
            //this.btnRefreshClass.Click+=new EventHandler(btnRefreshClass_Click);
            //this.btnRefreshInv.Click+=new EventHandler(btnRefreshInv_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
        //private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    //
        //    BindProductClass(ddlProductClass,
        //        "cnvcProductType like '" +
        //        ddlProductType.SelectedValue + "'");
        //    ListItem li = new ListItem("所有", "%");
        //    this.ddlProductClass.Items.Insert(0, li);
        //}
        //private void btnQuery_Click(object sender, System.EventArgs e)
        //{
        //    //查询
        //    this.DataGrid2.CurrentPageIndex = 0;
        //    //BindDataGrid();
        //    this.DataGrid1.SelectedIndex = -1;
        //    LoadInventory(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtInvCode.Text,this.txtInvName.Text);
			
        //}

//        private DataView GetInventory(string strproductype,string strproductclass,string strinvccode,string strinvname)
//        {
//            string strsql = @"select a.* from tbinventory a
//left outer join tbproductclass b on a.cnvcinvccode=b.cnvcproductclasscode
//where a.cnvcinvcode like '%{0}%'
//and a.cnvcinvname like '%{1}%'
//and b.cnvcproducttype like '%{2}%'
//and a.cnvcinvccode like '%{3}%'";
//            strsql = string.Format(strsql,strinvccode,strinvname,strproductype,strproductclass);
//            DataTable dt= Helper.Query(strsql);
//            this.DataTableConvert(dt,"cnvcinvccode","cnvcproducttype","tbProductClass","cnvcproductclasscode","cnvcproducttype","");
//            this.DataTableConvert(dt,"cnvcinvccode","cnvcinvccode","tbProductClass","cnvcproductclasscode","cnvcproductclassname","");
			
//            this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbComputationGroup","cnvcgroupcode","cnvcgroupname","");

//            //this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbProductClass","cnvcgroupcode","cnvcgroupname","");
//            this.DataTableConvert(dt,"cnvccomunitcode","cnvccomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcsacomunitcode","cnvcsacomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcpucomunitcode","cnvcpucomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcstcomunitcode","cnvcstcomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcproduceunitcode","cnvcproduceunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcshopunitcode","cnvcshopunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

//            this.DataTableConvert(dt,"cnvccreateperson","cnvccreateperson","tblogin","vcloginid","vcopername","");
//            this.DataTableConvert(dt,"cnvcmodifyperson","cnvcmodifyperson","tblogin","vcloginid","vcopername","");

//            this.DataTableConvert(dt,"cnvcvaluetype","cnvcvaluetype","tbnamecode","cnvccode","cnvcname","cnvctype='VALUETYPE'");
//            DataView dv = new DataView(dt);
//            dv.Sort = "cnvcproducttype,cnvcinvccode,cnvcinvcode";
//            return dv;
//        }

//        private void LoadInventory(string strproductype,string strproductclass,string strinvccode,string strinvname)
//        {
//            DataView dv = GetInventory(strproductype,strproductclass,strinvccode,strinvname);
//            DataTable dtInventory = dv.Table;
//            if(dtInventory.Rows.Count == 0)
//            {
//                dtInventory.Rows.Add(dtInventory.NewRow());
//                dtInventory.Rows[0]["cnbProductBill"] = false;
//                dtInventory.Rows[0]["cnbSale"] = false;
//                dtInventory.Rows[0]["cnbPurchase"] = false;
//                dtInventory.Rows[0]["cnbSelf"] = false;
//                dtInventory.Rows[0]["cnbComsume"] = false;
//                //dtInventory.Columns.Add("cnvcProduceUnitCodeName");
//            }
//            this.DataTableConvert(dtInventory,"cnvcProduceUnitCode","cnvcproduceunitname","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
//            this.DataGrid2.DataSource = dtInventory;
//            this.DataGrid2.DataBind();
//        }

//        private DataTable GetInventory(string strinvccode)
//        {
//            DataTable dt = Helper.Query("select * from tbinventory where cnvcinvccode='"+strinvccode+"'");
//            this.DataTableConvert(dt,"cnvcinvccode","cnvcinvccode","tbProductClass","cnvcproductclasscode","cnvcproductclassname","");

//            this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbComputationGroup","cnvcgroupcode","cnvcgroupname","");

//            //this.DataTableConvert(dt,"cnvcgroupcode","cnvcgroupcode","tbProductClass","cnvcgroupcode","cnvcgroupname","");
//            this.DataTableConvert(dt,"cnvccomunitcode","cnvccomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcsacomunitcode","cnvcsacomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcpucomunitcode","cnvcpucomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcstcomunitcode","cnvcstcomunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcproduceunitcode","cnvcproduceunitname","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");
//            this.DataTableConvert(dt,"cnvcshopunitcode","cnvcshopunitcode","tbComputationUnit","cnvccomunitcode","cnvccomunitname","");

//            this.DataTableConvert(dt,"cnvccreateperson","cnvccreateperson","tblogin","vcloginid","vcopername","");
//            this.DataTableConvert(dt,"cnvcmodifyperson","cnvcmodifyperson","tblogin","vcloginid","vcopername","");

//            this.DataTableConvert(dt,"cnvcvaluetype","cnvcvaluetype","tbnamecode","cnvccode","cnvcname","cnvctype='VALUETYPE'");
//            return dt;
//        }

//        private void LoadInventory(string strinvccode)
//        {
//            DataTable dtInventory = GetInventory(strinvccode);
//            if(dtInventory.Rows.Count == 0)
//            {
//                //dtInventory.Columns.Add("cnvcproduceunitname");
//                dtInventory.Rows.Add(dtInventory.NewRow());
//                dtInventory.Rows[0]["cnbProductBill"] = false;
//                dtInventory.Rows[0]["cnbSale"] = false;
//                dtInventory.Rows[0]["cnbPurchase"] = false;
//                dtInventory.Rows[0]["cnbSelf"] = false;
//                dtInventory.Rows[0]["cnbComsume"] = false;
//            }
//            this.DataGrid2.DataSource = dtInventory;
//            this.DataGrid2.DataBind();
//        }
//        private void LoadNULLInventory()
//        {
//            DataTable dtInventory = Helper.Query("select * from tbinventory where 1<>1");
//            if(dtInventory.Rows.Count == 0)
//            {
//                dtInventory.Rows.Add(dtInventory.NewRow());
//                dtInventory.Rows[0]["cnbProductBill"] = false;
//                dtInventory.Rows[0]["cnbSale"] = false;
//                dtInventory.Rows[0]["cnbPurchase"] = false;
//                dtInventory.Rows[0]["cnbSelf"] = false;
//                dtInventory.Rows[0]["cnbComsume"] = false;
//            }
//            dtInventory.Columns.Add("cnvcproduceunitname");
//            this.DataGrid2.DataSource = dtInventory;
//            this.DataGrid2.DataBind();
//        }

//        private void Button4_Click(object sender, System.EventArgs e)
//        {
//            //添加存货
//            if(this.DataGrid1.SelectedIndex >= 0)
//            {
//                string strinvccode = this.DataGrid1.SelectedItem.Cells[1].Text;
//                if( this.JudgeIsNull(strinvccode))
//                {
//                    this.Popup("请选择存货分类");
//                    return;
//                }
//                this.ClientScript.RegisterStartupScript(this.GetType(), "addinv", "<script type=\"text/javascript\">OpenInventoryWin('add','','" + strinvccode + "');</script>");
////				Session["g1idx"] = this.DataGrid1.SelectedIndex;
////				Session["g2idx"] = this.DataGrid2.SelectedIndex;
//            }
//            else
//                this.Popup("请选择存货分类");

//        }

//        private void Button5_Click(object sender, System.EventArgs e)
//        {
//            //修改存货
//            if(this.DataGrid2.SelectedIndex >= 0)
//            {
//                string strinvcode = this.DataGrid2.SelectedItem.Cells[1].Text;
//                string strinvccode = this.DataGrid2.SelectedItem.Cells[3].Text;
//                if(this.JudgeIsNull(strinvcode) || this.JudgeIsNull(strinvccode))
//                {					
//                    this.Popup("请选择存货");
//                    return;
//                }
//                if(strinvcode == "&nbsp;" || strinvccode == "&nbsp;")
//                {
//                    this.Popup("请选择存货");
//                    return;
//                }
//                this.ClientScript.RegisterStartupScript(this.GetType(), "modifyinv", "<script type=\"text/javascript\">OpenInventoryWin('modify','" + strinvcode + "','" + strinvccode + "');</script>");
////				Session["g1idx"] = this.DataGrid1.SelectedIndex;
////				Session["g2idx"] = this.DataGrid2.SelectedIndex;
//            }
//            else
//                this.Popup("请选择存货");
//        }

        //private void Button6_Click(object sender, System.EventArgs e)
        //{
        //    //删除存货
        //    if(this.DataGrid2.SelectedIndex >= 0)
        //    {
        //        Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
        //        ol.cnvcOperType = "删除存货";
        //        ol.cnvcOperID = this.oper.strLoginID;
        //        ol.cnvcDeptID = this.oper.strDeptID;

        //        Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory();
        //        inv.cnvcInvCCode = this.DataGrid2.SelectedItem.Cells[1].Text;

        //        Business.InventoryFacade invf = new AMSApp.zhenghua.Business.InventoryFacade();
        //        int ret = invf.DeleteInventory(ol,inv);
        //        if(ret > 0 )
        //        {
        //            this.Popup("删除存货成功！");
        //            this.LoadProductClass();
        //        }
        //        else
        //        {
        //            this.Popup("删除存货失败！");
        //        }
        //    }			
        //    else
        //        this.Popup("请选择存货");
        //}

//        private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
//        {
//            //产品类别
//            if(e.Item.ItemType == ListItemType.Header)
//            {
			
//                for(int i=0;i<e.Item.Cells.Count;i++)
//                {
//                    e.Item.Cells[i].Width   =Unit.Pixel(e.Item.Cells[i].Text.Length*18);
//                }
//            }
//        }

//        private void DataGrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
//        {
//            //存货档案
//            if(e.Item.ItemType == ListItemType.Header)
//            {
			
//                for(int i=0;i<e.Item.Cells.Count;i++)
//                {
					
//                    e.Item.Cells[i].Width   =Unit.Pixel(e.Item.Cells[i].Text.Length*18);
//                }
//            }
//        }

		
//        private void DataGrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
//        {
//            //
//            if(this.DataGrid1.SelectedIndex >= 0)
//            {
//                this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
//                string strinvccode = this.DataGrid1.SelectedItem.Cells[1].Text;
//                this.LoadInventory(strinvccode);
//            }
//            else
//            {
//                this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
//                //this.btnQuery_Click(null,null);
//                LoadInventory(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtInvCode.Text,this.txtInvName.Text);
//            }
//        }

//        private void btnExcel_Click(object sender, EventArgs e)
//        {
//            //导出所有存货
//            Session.Remove("QUERY");
//            Session.Remove("toExcel");
//            string strsql = "";
//            if(this.chkcn.Checked)
//            {
//                strsql= @"
//SELECT [cnbProductBill] AS '允许生产订单',[cnvcInvCode] as '存货编码',[cnvcInvName] as '存货名称',[cnvcInvStd] as '规格型号',[cnvcInvCCode] as '产品类别'
//      ,[cnbSale] as '是否销售',[cnbPurchase] as '是否外购',[cnbSelf] as '是否自制',[cnbComsume] as '是否生产耗用'
//      ,[cniInvCCost] as '参考成本',[cniInvNCost] as '最新成本',[cniSafeNum] as '安全库存量',[cniLowSum] as '最低库存量'
//      ,[cndSDate] as '启用日期',[cndEDate] as '停用日期',[cnvcCreatePerson] as '建档人',[cnvcModifyPerson] as '修改人'
//      ,[cndModifyDate] as '修改时间',[cnvcValueType] as '计价方式'
//      ,[cnvcGroupCode] AS '计量单位组'
//      ,[cnvcComUnitCode] as '主计量单位'
//      ,[cnvcSAComUnitCode] AS '销售计量单位'
//      ,[cnvcPUComUnitCode] as '采购计量单位'
//      ,[cnvcSTComUnitCode] AS '库存计量单位'
//      ,[cnvcProduceUnitCode] as '生产计量单位'
//      ,[cnfRetailPrice] as '零售价'
//      ,[cnvcShopUnitCode] as '零售计量单位'
//      ,[cnvcFeel] as '口感'
//      ,[cnvcOrganise] as '组织'
//      ,[cnvcColor] AS '颜色'
//      ,[cnvcTaste]  as '口味'
//	  ,[cnnExpire] as '过期限制'
//	  ,[cnnDue] as '到期提示'
//  FROM [tbInventory]";
//            }
//            else
//            {
//                strsql = "select * from tbinventory";
//            }
			
//            DataTable dtOut = Helper.Query(strsql);
//            Session["QUERY"] = dtOut;
//            Session["toExcel"]=dtOut;
//        }


//        private void btnRefreshInv_Click(object sender, EventArgs e)
//        {
//            //
//            if(this.DataGrid1.SelectedIndex >= 0)
//            {
//                //this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
//                string strinvccode = this.DataGrid1.SelectedItem.Cells[1].Text;
//                this.LoadInventory(strinvccode);
//            }
//            else
//            {
//                //this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
//                //this.btnQuery_Click(null,null);
//                LoadInventory(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtInvCode.Text,this.txtInvName.Text);
//            }
//        }
	}
}
