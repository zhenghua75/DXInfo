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
namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmCheckInWh 的摘要说明。
	/// </summary>
	public class wfmCheckInWh : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCheck;
		protected System.Web.UI.WebControls.Button btnCheckQuery;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.TextBox txtProduceState;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DropDownList ddlWarehouse;
		protected System.Web.UI.WebControls.TextBox txtMakeSerialNo;
		protected System.Web.UI.WebControls.TextBox txtDays;
		#endregion 

		#region page_load
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			btnCheck.Attributes["onClick"]="javascript:return confirm(getwh());";   
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				//this.BindDept(ddlOrderDept, "cnvcDeptType <>'Corp'");
				//ListItem li = new ListItem("所有", "%");
				//ddlOrderDept.Items.Insert(0, li);
				//this.BindNameCode(ddlGroup, "cnvcType='GROUP'");
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
				//string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();			
	
				DataTable dt = Helper.Query("select * from tbmakelog where cnnproduceserialno="+strProduceSerialNo);
				if(dt.Rows.Count==0)
				{
					this.Popup("搜索领用明细出错！");
					return;
				}
				Entity.MakeLog ml = new MakeLog(dt);
				string strMakeSerialNo = ml.cnnMakeSerialNo.ToString();
				txtMakeSerialNo.Text = strMakeSerialNo;

				BindProduceLog(strProduceSerialNo);
				this.BindWarehouse(ddlWarehouse,"cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"' and cnbFreeze=0");
				QueryProduceDetail();
				btnQuery_Click(null,null);
				
				//this.txtExpDate.Text = DateTime.Now.AddDays(this.ExpDate).ToString("yyyy-MM-dd");
				txtDays.Text = "0";
			}
		}

		#endregion
		private void BindProduceLog(string strProduceSerialNo)
		{
			string strSql = "select * from tbProduceLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtProduceLog = Helper.Query(strSql);
			if(dtProduceLog.Rows.Count > 0)
			{
				ProduceLog produceLog = new ProduceLog(dtProduceLog);
				this.ddlProduceDept.Items.FindByValue(produceLog.cnvcProduceDeptID).Selected = true;
				this.txtProduceSerialNo.Text = produceLog.cnnProduceSerialNo.ToString();
				this.txtProduceDate.Text = produceLog.cndProduceDate.ToString("yyyy-MM-dd");
				
				this.txtProduceDate.Enabled = false;
				this.txtProduceSerialNo.Enabled = false;
				this.ddlProduceDept.Enabled = false;
				txtProduceState.Text = produceLog.cnvcProduceState;
				//				if(produceLog.cnvcProduceState == "3" || produceLog.cnvcProduceState == "4")
				//				{
				//					this.btnCheck.Enabled = false;
				//				}
				//				else
				//				{
				//					this.btnCheck.Enabled = true;
				//				}
				

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
			this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
			this.btnCheckQuery.Click += new System.EventHandler(this.btnCheckQuery_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProduceCheckWh.aspx");
		}
		private void QueryProduceDetail()
		{
			//			string strSql = "select a.cnnProduceSerialNo,a.cnvcInvCode,a.cnnProduceCount as cnnOrderCount,a.cnnProduceCount,isnull(b.cnnCheckCount,0) as cnnCheckCount,isnull(b.cnnAssignCount,0) as cnnAssignCount from tbProduceDetail a left outer join tbProduceCheckLog b"
			//					+" on a.cnnProduceSerialNo=b.cnnProduceSerialNo and a.cnvcInvCode=b.cnvcInvCode where a.cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by a.cnvcInvCode";
			//			DataTable dtProduce = Helper.Query(strSql);	
			string strsql = "select * from tbmakelog where cnnproduceserialno="+this.txtProduceSerialNo.Text;
			DataTable dtMakeLog = Helper.Query(strsql);
			if(dtMakeLog.Rows.Count==0)
			{
				this.Popup("无法找到生产计划");
				return;
			}
			Entity.MakeLog ml = new MakeLog(dtMakeLog);

			string strsql1 = "select * from tbmakedetail where cnncount>0 and cnnmakeserialno="+ml.cnnMakeSerialNo.ToString();
			DataTable dtMakeDetail = Helper.Query(strsql1);

			string strsql2 = "select * from tbproducechecklog where cnnproduceserialno="+this.txtProduceSerialNo.Text;
			DataTable dtCheckLog = Helper.Query(strsql2);

			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");

			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvStd","tbInventory","cnvcInvCode","cnvcInvStd","");

			this.DataTableConvert(dtMakeDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnCheckCount",dtCheckLog,"cnvcInvCode","cnnCheckCount","");
			Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnbInWh",dtCheckLog,"cnvcInvCode","cnbInWh","");
			//Helper.DataTableConvert(dtMakeDetail,"cnnMakeSerialNo","cnnProduceSerialNo",dtMakeLog,"cnnMakeSerialNo","cnnProduceSerialNo","");


			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcSTComUnitCode","tbInventory","cnvcInvCode","cnvcSTComUnitCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcSTComUnitCode","cnvcSTComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");

			this.DataTableConvert(dtMakeDetail,"cnvcProduceUnitCode","cniChangeRate","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
			this.DataTableConvert(dtMakeDetail,"cnvcSTComUnitCode","cniChangeRate_st","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
			//this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");

			//this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");
			//this.DataTableConvert(dtMakeDetail,"cnvcGroupCode","cnvcComUnitCode_main","tbComputationUnit","cnvcGroupCode","cnvcComUnitCode","cnbMainUnit=1");
			//this.DataTableConvert(dtMakeDetail,"cnvcComUnitCode_main","cniChangeRate_main","tbComputationUnit","cnvcGroupCode","cniChangRate","");


			dtMakeDetail.Columns.Add("cnnInCount");
			//DataTable dtUnit = Application["tbComputationUnit"] as DataTable;
			foreach(DataRow dr in dtMakeDetail.Rows)
			{
//				if(dr["cnnCheckCount"].ToString() == "")
//				{
//					dr["cnnCheckCount"] = 0;
//				}

//				if(dr["cniChangeRate_st"].ToString() == "")
//					dr["cniChangeRate_st"] = "1";
//				if(dr["cniChangeRate_main"].ToString() == "")
//					dr["cniChangeRate_main"] = 1;
				//string strcomunitcode = dr["cnvcproduceunitcode"].ToString();
				//string strcomunitcode_main = dr["cnvcComUnitCode_main"].ToString();

				decimal dcheckcount = Convert.ToDecimal(dr["cnncheckcount"].ToString());
				decimal dchangerate = Convert.ToDecimal(dr["cniChangeRate"].ToString());
				decimal dchangerate_st = Convert.ToDecimal(dr["cniChangeRate_st"].ToString());
				//decimal dchangerate_main = Convert.ToDecimal(dr["cniChangeRate_main"].ToString());

//				if(strcomunitcode == strcomunitcode_main)
//				{
//					dr["cnnInCount"] = (dchangerate/dchangerate_st) * dcheckcount;
//				}
//				else
//				{
					dr["cnnInCount"] = Math.Round((dchangerate/dchangerate_st) * dcheckcount,2);
				//}
			}
			dtMakeDetail.Columns["cnnMakeCount"].ColumnName = "cnnOrderCount";
			dtMakeDetail.Columns["cnnCount"].ColumnName = "cnnProduceCount";
			Session["tbProduceDetail"] = dtMakeDetail;
		}
		private void BindCheckLog()
		{
			string strSql = "select * from tbProduceCheckLog where cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by cnvcInvCode";
			DataTable dtCheck = Helper.Query(strSql);
			this.DataTableConvert(dtCheck,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtCheck,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
			this.DataTableConvert(dtCheck,"cnvcComUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");

			DataGrid1.DataSource = dtCheck;
			DataGrid1.DataBind();
		}
		private void BindGrid()
		{
			if(Session["tbProduceDetail"] == null)
			{
				btnCheckQuery_Click(null,null);
			}
			else
			{
				btnQuery_Click(null,null);
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//BindGrid();
			if(Session["tbProduceDetail"] == null)
			{
				QueryProduceDetail();
			}
			DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
			DataView dv = new DataView(dtProduce);
			dv.RowFilter = "cnnInCount>'0'";
			this.DataGrid1.DataSource = dv;//dtProduce;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			this.BindGrid();			
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			this.BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

			try
			{
				string strCheckCount = ((TextBox) e.Item.Cells[6].Controls[0]).Text;
				if(this.JudgeIsNull(strCheckCount))
					throw new Exception("请输入盘点量");
				if(!this.JudgeIsNum(strCheckCount))
					throw new Exception("请输入数字");
				string strProduceCount = e.Item.Cells[5].Text;				
				decimal dProduceCount = Convert.ToDecimal(strProduceCount);
				decimal dCheckCount = Convert.ToDecimal(strCheckCount);
				if(dCheckCount >dProduceCount)
					throw new Exception("盘点量过大，请手工调整");
				if(Session["tbProduceDetail"] == null)
				{
					ProduceCheckLog check = new ProduceCheckLog();
					check.cnvcOperID = oper.strLoginID;
					check.cnnProduceSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
					check.cnvcInvCode = e.Item.Cells[1].Text;
					check.cnnCheckCount = Convert.ToDecimal(strCheckCount);
					GoodsFacade gf = new GoodsFacade();

					OperLog operLog = new OperLog();
					operLog.cnvcOperID = oper.strLoginID;
					operLog.cnvcDeptID = oper.strDeptID;
					operLog.cnvcOperType = "调整盘点量";

					gf.UpdateProduceCheck(check,operLog);
				}
				else
				{
					string strCode = e.Item.Cells[1].Text;
					DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
					DataRow[] drProduct = dtProduce.Select("cnvcInvCode='" + strCode + "'");
					drProduct[0]["cnnCheckCount"] = strCheckCount;
					Session["tbProduceDetail"] = dtProduce;
				}

				this.DataGrid1.EditItemIndex = -1;
				this.BindGrid();
				this.Popup("修改成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		private void btnCheck_Click(object sender, System.EventArgs e)
		{
			if(Session["tbProduceDetail"] == null)
			{
				this.Popup("请首先使用【盘点清单】按钮，查询计划情况");				
				return;
			}
			if(this.JudgeIsNull(txtDays.Text))
			{
				this.Popup("请输入过期天数调整量");
				return;
			}
			DataTable dtpd = (DataTable)Session["tbProduceDetail"];

			ArrayList al = new ArrayList();
			DataTable dtInv = Application["tbInventory"] as DataTable;
			DataTable dtProductClass = Application["tbProductClass"] as DataTable;
//			string strSql = "select * from tbProduceCheckLog where cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by cnvcInvCode";
//			DataTable dtCheck = Helper.Query(strSql);

			//foreach(DataGridItem dgi in this.DataGrid1.Items)
			foreach(DataRow dr in dtpd.Rows)
			{
				//Entity.ProduceCheckLog pc = new ProduceCheckLog(dr);

				string strinvcode = dr["cnvcInvCode"].ToString();//dgi.Cells[0].Text;
				string strinvname = dr["cnvcInvName"].ToString();//dgi.Cells[1].Text;
				string strincount = dr["cnnInCount"].ToString();//dgi.Cells[6].Text;
				//string strwhcount = dgi.Cells[7].Text;
				bool iswh = Convert.ToBoolean(dr["cnbInWh"].ToString());//((CheckBox)dgi.Cells[7].Controls[1]).Checked;
				if(iswh)
				{
					this.Popup("已经完成了盘点入库");
					return;
				}				
				decimal dincount = Convert.ToDecimal(strincount);
				Entity.RdRecordDetail rrd = new RdRecordDetail();
				rrd.cnvcInvCode = strinvcode;
				rrd.cnnQuantity = Convert.ToDecimal(strincount);
				DataRow[] drInvs = dtInv.Select("cnvcInvCode='"+strinvcode+"'");
				if(drInvs.Length == 0)
				{
					this.Popup(strinvname+"存货档案未找到");
					return;
				}
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);

				DataRow[] drProductClasses = dtProductClass.Select("cnvcProductClassCode='"+inv.cnvcInvCCode+"'");
				if(drProductClasses.Length == 0)
				{
					this.Popup(strinvname+"的存货类别未找到");
					return;
				}
				Entity.ProductClass pc = new ProductClass(drProductClasses[0]);

				rrd.cnvcGroupCode = inv.cnvcGroupCode;
				rrd.cnvcComunitCode = inv.cnvcSTComUnitCode;
				rrd.cnvcFlag = "0";
				rrd.cndExpDate = Convert.ToDateTime(this.txtProduceDate.Text).AddDays(pc.cnnDays).AddDays(Convert.ToDouble(txtDays.Text));//Convert.ToDateTime(this.txtExpDate.Text);
				rrd.cndMdate = Convert.ToDateTime(this.txtProduceDate.Text);
				
				//if(rrd.cnnQuantity == 0)continue;
				al.Add(rrd);

			}

			if(al.Count == 0)
			{
				this.Popup("无入库产品，不用生产入库！");
				return;
			}
			Entity.RdRecord rr = new RdRecord();
			rr.cnvcRdCode = "RD009";
			rr.cnvcRdFlag = "0";
			rr.cnvcWhCode = this.ddlWarehouse.SelectedValue;
			rr.cnvcDepID = this.ddlProduceDept.SelectedValue;
			rr.cnvcOperName = this.oper.strOperName;
			rr.cnvcComments = "生产入库";
			rr.cnvcMaker = this.oper.strLoginID;
			rr.cnnProorderID = Convert.ToDecimal(this.txtProduceSerialNo.Text);
			rr.cnvcState = "2";			
			
			OperLog ol = new OperLog();
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;
			ol.cnvcOperType = "生产入库";
			string strWarehouse = this.ddlWarehouse.SelectedValue;
			try
			{
				ProduceFacade pf = new ProduceFacade();
				pf.CheckInWh(this.txtMakeSerialNo.Text,rr,al,ol,strWarehouse);
				this.Popup("生产入库成功");
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);				
			}
			QueryProduceDetail();
			BindGrid();			
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();

		}

		private void btnCheckQuery_Click(object sender, System.EventArgs e)
		{
			//Session.Remove("tbProduceDetail");
			//BindCheckLog();
			this.Response.Redirect("wfmCheckDetail.aspx?produceserialno="+this.txtProduceSerialNo.Text);
		}
	}
}

