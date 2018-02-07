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
	/// wfmMakeLog 的摘要说明。
	/// </summary>
	public class wfmWarehouseCollar :wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.Button btnQueryMake;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox txtMakeSerialNo;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWarehouse;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			Button2.Attributes["onClick"]="javascript:return confirm(getwh());";   
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("无效链接");
					return;
				}
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
				BindGrid();
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
			this.btnQueryMake.Click += new System.EventHandler(this.btnQueryMake_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

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

			}
		}
		private void btnMakeLog_Click(object sender, System.EventArgs e)
		{
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "生产预估";

				ProduceFacade pf = new ProduceFacade();
				pf.AddMakeLog(pLog,operLog);
				Popup("预估成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		
		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmWarehouseOut.aspx");
		}

		private void btnQueryMake_Click(object sender, System.EventArgs e)
		{
			BindGrid();
			//this.DataGrid1.Visible = true;
		}

		private void BindGrid()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;
			string strMakeSerialNo = txtMakeSerialNo.Text;
			//string strMakeType = Request["MakeType"].ToString();
			//string strMakeLogSql = "select * from tbMakeLog where cnnMakeSerialNo="+strMakeSerialNo;
			string strSql = "select * from tbMakeDetail where cnnstcount>0 and cnnMakeSerialNo="+strMakeSerialNo;
			DataTable dtDetail = Helper.Query(strSql);
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcInvStd","tbInventory","cnvcInvCode","cnvcInvStd","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcSTComUnitCode","tbInventory","cnvcInvCode","cnvcSTComUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcSTComUnitCode","cnvcSTComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
				
//
			this.DataTableConvert(dtDetail,"cnvcComUnitCode","cniChangeRate","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
			this.DataTableConvert(dtDetail,"cnvcSTComUnitCode","cniChangeRate_st","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
//			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");
//
//			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");
//			this.DataTableConvert(dtDetail,"cnvcGroupCode","cnvcComUnitCode_main","tbComputationUnit","cnvcGroupCode","cnvcComUnitCode","cnbMainUnit=1");
//			this.DataTableConvert(dtDetail,"cnvcComUnitCode_main","cniChangeRate_main","tbComputationUnit","cnvcGroupCode","cniChangRate","");
//			dtDetail.Columns.Add("cnnOutCount");
			//DataTable dtUnit = Application["tbComputationUnit"] as DataTable;
//			foreach(DataRow dr in dtDetail.Rows)
//			{
//				if(dr["cniChangeRate_st"].ToString() == "")
//					dr["cniChangeRate_st"] = "1";
//				if(dr["cniChangeRate_main"].ToString() == "")
//					dr["cniChangeRate_main"] = 1;
//				string strcomunitcode = dr["cnvccomunitcode"].ToString();
//				string strcomunitcode_main = dr["cnvcComUnitCode_main"].ToString();
//
//				decimal dstcount = Convert.ToDecimal(dr["cnnstcount"].ToString());
//				decimal dchangerate = Convert.ToDecimal(dr["cniChangeRate"].ToString());
//				decimal dchangerate_st = Convert.ToDecimal(dr["cniChangeRate_st"].ToString());
//				decimal dchangerate_main = Convert.ToDecimal(dr["cniChangeRate_main"].ToString());
//
//				if(strcomunitcode == strcomunitcode_main)
//				{
//					dr["cnnOutCount"] = (dchangerate/dchangerate_st) * dstcount;
//				}
//				else
//				{
//					dr["cnnOutCount"] = ((dchangerate_main/dchangerate_st) /(dchangerate_main/dchangerate)) * dstcount;
//				}
//			}


			string strSql1 = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"') and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) "
				+"group by cnvcInvCode";
			DataTable dtwh = Helper.Query(strSql1);

			Helper.DataTableConvert(dtDetail,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");
			foreach(DataRow dr in dtDetail.Rows)
			{
				if(dr["cnnwhcount"].ToString() != "")
				{
					double dchangerate = Convert.ToDouble(dr["cniChangeRate"].ToString());
					double dchangerate_st = Convert.ToDouble(dr["cniChangeRate_st"].ToString());
					double dwhcount = Convert.ToDouble(dr["cnnwhcount"].ToString());
					//dr["cnnwhcount"] = Convert.ToDecimal(Math.Floor(dwhcount/dchangerate_st));
					dr["cnnwhcount"] = Convert.ToDecimal(Math.Round(dwhcount/dchangerate_st,4));
				}
			}

			//DataTable dtMakeLog = Helper.Query(strMakeLogSql);
			//MakeLog mLog = new MakeLog(dtMakeLog);
			//this.DataGrid1.Caption = "领料单"+DateTime.Now.ToString("yyyy年MM月dd日");
			this.DataGrid1.DataSource = dtDetail;
			this.DataGrid1.DataBind();		
		}


		

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}


		private void Button1_Click(object sender, System.EventArgs e)
		{
			//预估调整
			try
			{
				string strProduceSerialNo = this.txtProduceSerialNo.Text;
				OperLog ol = new OperLog();
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;
				ol.cnvcOperType = "调整预估";
				//AdjustMakeDetail

				ProduceFacade pf = new ProduceFacade();
				pf.AdjustMakeDetail(strProduceSerialNo,ol);
				this.Popup("调整预估成功");
				BindGrid();
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = -1;
			BindGrid();
		}

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex = e.Item.ItemIndex;
			BindGrid();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//更新
			try
			{
				string strProduceSerialNo = this.txtProduceSerialNo.Text;
				string strMakeSerialNo = e.Item.Cells[0].Text;
				string strAdjustcount = ((TextBox)e.Item.Cells[11].Controls[0]).Text;

				string strstcount = ((TextBox)e.Item.Cells[10].Controls[0]).Text;
				string strinvcode = e.Item.Cells[5].Text;

				DataTable dtInv = Application["tbInventory"] as DataTable;
				DataRow[] drInvs = dtInv.Select("cnvcinvcode='"+strinvcode+"'");
				if(drInvs.Length == 0)
				{
					throw new Exception("无此存货");
				}
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);
				if(!inv.cnbSelf && strAdjustcount != "0")
				{
					throw new Exception("非自制存货，不能调整数量");
				}
				OperLog ol = new OperLog();
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;
				ol.cnvcOperType = "调整数量";

				MakeDetail md = new MakeDetail();
				md.cnnMakeSerialNo = Convert.ToDecimal(strMakeSerialNo);
				md.cnnAdjustCount = Convert.ToDecimal(strAdjustcount);
				md.cnnStCount = Convert.ToDecimal(strstcount);
				md.cnvcInvCode = strinvcode;
				ProduceFacade pf = new ProduceFacade();
				pf.UpdateMakeDetail(strProduceSerialNo,md,ol);
				this.Popup("更新调整数量及库存领用量成功");
				this.DataGrid1.EditItemIndex = -1;
				BindGrid();
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			//领料单打印
			ArrayList al = new ArrayList();
			DataTable dtInv = Application["tbInventory"] as DataTable;
			foreach(DataGridItem dgi in this.DataGrid1.Items)
			{
				string strinvcode = dgi.Cells[0].Text;
				string strinvname = dgi.Cells[1].Text;
				string stroutcount = dgi.Cells[6].Text;
				string strwhcount = dgi.Cells[7].Text;
				bool iscollar = ((CheckBox)dgi.Cells[8].Controls[1]).Checked;
				if(iscollar)
				{
					//this.Popup(strinvname+"已经完成了生产材料领用");
					this.Popup("已经领用生产材料");
					return;
				}
				
				if(!this.JudgeIsNum(strwhcount))
				{
					this.Popup(strinvname+"无库存，不能领用原材料，请检查原材料库存");
					return;
				}
				decimal doutcount = Convert.ToDecimal(stroutcount);
				decimal dwhcount = Convert.ToDecimal(strwhcount);
				if(doutcount>dwhcount)
				{
					this.Popup(strinvname+"库存数量不足，不能领用原材料进行生产，请检查原材料库存");
					return;
				}
				Entity.RdRecordDetail rrd = new RdRecordDetail();
				rrd.cnvcInvCode = strinvcode;
				rrd.cnnQuantity = Convert.ToDecimal(stroutcount);
				DataRow[] drInvs = dtInv.Select("cnvcInvCode='"+strinvcode+"'");
				if(drInvs.Length == 0)
				{
					this.Popup(strinvname+"存货档案未找到");
					return;
				}
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);
				rrd.cnvcGroupCode = inv.cnvcGroupCode;
				rrd.cnvcComunitCode = inv.cnvcSTComUnitCode;
				rrd.cnvcFlag = "0";

				al.Add(rrd);

			}

			Entity.RdRecord rr = new RdRecord();
			rr.cnvcRdCode = "RD008";
			rr.cnvcRdFlag = "0";
			rr.cnvcWhCode = this.ddlWarehouse.SelectedValue;
			rr.cnvcDepID = this.ddlProduceDept.SelectedValue;
			rr.cnvcOperName = this.oper.strOperName;
			rr.cnvcComments = "生产材料领用";
			rr.cnvcMaker = this.oper.strLoginID;
			rr.cnnProorderID = Convert.ToDecimal(this.txtProduceSerialNo.Text);
			rr.cnvcState = "2";


			OperLog ol = new OperLog();
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;
			ol.cnvcOperType = "生产材料领用";
			
			string strWarehouse = this.ddlWarehouse.SelectedValue;
			try
			{
				ProduceFacade pf = new ProduceFacade();
				pf.Collar(this.txtMakeSerialNo.Text,rr,al,ol,strWarehouse);
				this.Popup("生产材料领用成功");
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);				
			}
			BindGrid();
		}

	}
}
