#region 引入
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
#endregion
namespace AMSApp.zhenghua.Produce
{
	/// <summary>
	/// wfmDividModify 的摘要说明。
	/// </summary>
	public class wfmDividModify : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button btnCheck;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.TextBox txtProduceState;
		protected System.Web.UI.WebControls.Button btnCheckQuery;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.Button btnAdjustCheck;
		protected System.Web.UI.WebControls.Button btnReturn;
		#endregion

		#region page_load
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
				Session.Remove("tbProduceDetail");
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();				
				BindProduceLog(strProduceSerialNo);
				//QueryProduceDetail();
				btnQuery_Click(null,null);
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
				
				this.CheckBox1.Checked = produceLog.cnvcProduceState=="4"?true:false;
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
			this.btnAdjustCheck.Click += new System.EventHandler(this.btnAdjustCheck_Click);
			this.btnCheckQuery.Click += new System.EventHandler(this.btnCheckQuery_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProduceCheck.aspx");
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

			string strsql1 = "select * from tbmakedetail where cnnCount>0 and  cnnmakeserialno="+ml.cnnMakeSerialNo.ToString();
			DataTable dtMakeDetail = Helper.Query(strsql1);

			string strsql2 = "select * from tbproducechecklog where cnnproduceserialno="+this.txtProduceSerialNo.Text;
			DataTable dtCheckLog = Helper.Query(strsql2);
			//dtMakeDetail.Columns.Add("cnnCheckCount");

			
			string strpdsql = "select * from tbProduceDetail where cnnProduceSerialNo="+this.txtProduceSerialNo.Text;
			DataTable dtpd = Helper.Query(strpdsql);

			Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcIsNew",dtpd,"cnvcInvCode","cnnOrderCount","");

			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvCCode","tbInventory","cnvcInvCode","cnvcInvCCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCCode","cnvcProductType","tbProductClass","cnvcProductClassCode","cnvcProductType","");


			if(dtCheckLog.Rows.Count == 0)
			{
				Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnCheckCount",dtMakeDetail,"cnvcInvCode","cnnCount","");
				dtMakeDetail.Columns.Add("cnnTeamID");
				dtMakeDetail.Columns.Add("cnnProducerID");
				foreach(DataRow dr in dtMakeDetail.Rows)
				{
					if(dr["cnnCheckCount"].ToString() == "")
					{
						dr["cnnCheckCount"] = 0;
					}
					if(dr["cnvcProductType"].ToString() != "FINALPRODUCT" && dr["cnvcIsNew"].ToString() == "")
					{
						dr["cnnCheckCount"] = 0;
					}
				}
			}
			else
			{
				Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnCheckCount",dtCheckLog,"cnvcInvCode","cnnCheckCount","");
				Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnTeamID",dtCheckLog,"cnvcInvCode","cnnTeamID","");
				Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnProducerID",dtCheckLog,"cnvcInvCode","cnnProducerID","");
			}
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			
			Helper.DataTableConvert(dtMakeDetail,"cnnMakeSerialNo","cnnProduceSerialNo",dtMakeLog,"cnnMakeSerialNo","cnnProduceSerialNo","");

			
//			string strSql = "select * from tbteam";
//			DataTable dtTeam = Helper.Query(strSql);
//			Helper.DataTableConvert(dtMakeDetail,"cnnTeamID","cnvcTeamName",dtTeam,"cnnTeamID","cnvcTeamName","");
//			strSql = "select * from tbproducer";
//			DataTable dtProducer = Helper.Query(strSql);
//			Helper.DataTableConvert(dtMakeDetail,"cnnProducerID","cnvcProducerName",dtProducer,"cnnProducerID","cnvcProducerName","");

			
			dtMakeDetail.Columns["cnnMakeCount"].ColumnName = "cnnOrderCount";
			dtMakeDetail.Columns["cnnCount"].ColumnName = "cnnProduceCount";
			Session["tbProduceDetail"] = dtMakeDetail;
		}
//		private void JudgeIsComponent(DataTable dtBOM,DataTable dtMakeDetail)
//		{
//			foreach(DataRow drMakeDetail in dtMakeDetail.Rows)
//			{
//				Entity.MakeDetail md = new MakeDetail(drMakeDetail);
//			}
//		}
		private void BindCheckLog()
		{
			string strSql = "select * from tbProduceCheckLog where cnnProduceSerialNo=" + txtProduceSerialNo.Text+" order by cnvcInvCode";
			DataTable dtCheck = Helper.Query(strSql);
			this.DataTableConvert(dtCheck,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtCheck,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
			this.DataTableConvert(dtCheck,"cnvcComUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");

//			strSql = "select * from tbteam";
//			DataTable dtTeam = Helper.Query(strSql);
//			Helper.DataTableConvert(dtCheck,"cnnTeamID","cnvcTeamName",dtTeam,"cnnTeamID","cnvcTeamName","");
//			strSql = "select * from tbproducer";
//			DataTable dtProducer = Helper.Query(strSql);
//			Helper.DataTableConvert(dtCheck,"cnnProducerID","cnvcProducerName",dtProducer,"cnnProducerID","cnvcProducerName","");
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
				
			this.DataGrid1.DataSource = dtProduce;
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

				DropDownList   ddlTeam=(DropDownList)e.Item.Cells[7].FindControl("ddlTeam");   				
				DropDownList ddlProducer = (DropDownList)e.Item.Cells[8].FindControl("ddlProducer");
				if(ddlTeam.Items.Count == 0) throw new Exception("请录入生产组");
				if(ddlProducer.Items.Count == 0) throw new Exception("请录入生产员");

				if(Session["tbProduceDetail"] == null)
				{
					ProduceCheckLog check = new ProduceCheckLog();
					check.cnvcOperID = oper.strLoginID;
					check.cnnProduceSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
					check.cnvcInvCode = e.Item.Cells[1].Text;
					check.cnnCheckCount = Convert.ToDecimal(strCheckCount);
					check.cnnTeamID = Convert.ToInt32(ddlTeam.SelectedValue);
					check.cnnProducerID = Convert.ToInt32(ddlProducer.SelectedValue);
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
					drProduct[0]["cnnTeamID"] = ddlTeam.SelectedValue;
					drProduct[0]["cnnProducerID"] = ddlProducer.SelectedValue;
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
			try
			{
				if(Session["tbProduceDetail"] == null)
				{
					//throw new Exception("请首先使用【盘点清单】按钮，查询盘点清单情况");				
					QueryProduceDetail();
				}
				DataTable dtProduce = (DataTable) Session["tbProduceDetail"];

				foreach(DataGridItem dgi in this.DataGrid1.Items)
				{
				
					string strCheckCount = ((TextBox) dgi.Cells[6].Controls[1]).Text;
					if(this.JudgeIsNull(strCheckCount))
						throw new Exception("请输入盘点量");
					if(!this.JudgeIsNum(strCheckCount))
						throw new Exception("请输入数字");
					string strProduceCount = dgi.Cells[5].Text;				
					decimal dProduceCount = Convert.ToDecimal(strProduceCount);
					decimal dCheckCount = Convert.ToDecimal(strCheckCount);
					if(dCheckCount >dProduceCount)
						throw new Exception("盘点量过大，请手工调整");

					DropDownList   ddlTeam=(DropDownList)dgi.Cells[7].FindControl("ddlTeamText");   				
					DropDownList ddlProducer = (DropDownList)dgi.Cells[8].FindControl("ddlProducerText");
					if(ddlTeam.Items.Count == 0) throw new Exception("请录入生产组");
					if(ddlProducer.Items.Count == 0) throw new Exception("请录入生产员");

//					if(Session["tbProduceDetail"] == null)
//					{
//						ProduceCheckLog check = new ProduceCheckLog();
//						check.cnvcOperID = oper.strLoginID;
//						check.cnnProduceSerialNo = Convert.ToDecimal(dgi.Cells[0].Text);
//						check.cnvcInvCode = dgi.Cells[1].Text;
//						check.cnnCheckCount = Convert.ToDecimal(strCheckCount);
//						check.cnnTeamID = Convert.ToInt32(ddlTeam.SelectedValue);
//						check.cnnProducerID = Convert.ToInt32(ddlProducer.SelectedValue);
//						GoodsFacade gf = new GoodsFacade();
//
//						OperLog operLog = new OperLog();
//						operLog.cnvcOperID = oper.strLoginID;
//						operLog.cnvcDeptID = oper.strDeptID;
//						operLog.cnvcOperType = "调整盘点量";
//
//						gf.UpdateProduceCheck(check,operLog);
//					}
//					else 
//					{						
						string strCode = dgi.Cells[1].Text;
//						DataTable dtProduce = (DataTable) Session["tbProduceDetail"];
						DataRow[] drProduct = dtProduce.Select("cnvcInvCode='" + strCode + "'");
						drProduct[0]["cnnCheckCount"] = strCheckCount;
						drProduct[0]["cnnTeamID"] = ddlTeam.SelectedValue;
						drProduct[0]["cnnProducerID"] = ddlProducer.SelectedValue;
						
					//}

					//this.DataGrid1.EditItemIndex = -1;
					
				}


				GoodsFacade gf = new GoodsFacade();
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "生产盘点";
				gf.ProduceCheck(dtProduce,operLog, ddlProduceDept.SelectedValue,txtProduceSerialNo.Text);
				Session["tbProduceDetail"] = dtProduce;
				this.CheckBox1.Checked = true;
				Popup("盘点完成");
				//this.btnCheck.Enabled = false;
				//this.DataGrid1.DataSource = null;
				//this.DataGrid1.DataBind();
				//Session["tbProduceDetail"] = null;
				btnQuery_Click(null,null);
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
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

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//
			string  strsql =  "select * from tbteam";   
			DataTable dtTeam = Helper.Query(strsql);
			strsql = "select * from tbproducer";
			DataTable dtProducer = Helper.Query(strsql);
//			if(e.Item.ItemType==ListItemType.EditItem)   
//			{   
//				DropDownList   ddlTeam=(DropDownList)e.Item.Cells[7].FindControl("ddlTeam");   
//				ddlTeam.DataSource = dtTeam;
//				ddlTeam.DataTextField = "cnvcTeamName";
//				ddlTeam.DataValueField = "cnnTeamID";
//				ddlTeam.DataBind();
//				this.SetDDL(ddlTeam,Convert.ToString(DataBinder.Eval(e.Item.DataItem,"cnnTeamID")));
//				DropDownList ddlProducer = (DropDownList)e.Item.Cells[8].FindControl("ddlProducer");
//				ddlProducer.DataSource = dtProducer;
//				ddlProducer.DataTextField = "cnvcProducerName";
//				ddlProducer.DataValueField = "cnnProducerID";
//				ddlProducer.DataBind();
//				this.SetDDL(ddlProducer,Convert.ToString(DataBinder.Eval(e.Item.DataItem,"cnnProducerID")));
//			}   
//			else 
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DropDownList   ddlTeam=(DropDownList)e.Item.Cells[7].FindControl("ddlTeamText");   
				ddlTeam.DataSource = dtTeam;
				ddlTeam.DataTextField = "cnvcTeamName";
				ddlTeam.DataValueField = "cnnTeamID";
				ddlTeam.DataBind();
				this.SetDDL(ddlTeam,Convert.ToString(DataBinder.Eval(e.Item.DataItem,"cnnTeamID")));
				//ddlTeam.Enabled = false;
				DropDownList ddlProducer = (DropDownList)e.Item.Cells[8].FindControl("ddlProducerText");
				ddlProducer.DataSource = dtProducer;
				ddlProducer.DataTextField = "cnvcProducerName";
				ddlProducer.DataValueField = "cnnProducerID";
				ddlProducer.DataBind();
				this.SetDDL(ddlProducer,Convert.ToString(DataBinder.Eval(e.Item.DataItem,"cnnProducerID")));
				//ddlProducer.Enabled = false;
			}
		}

		private void btnAdjustCheck_Click(object sender, System.EventArgs e)
		{
			//统一调整盘点量
			try
			{
				
				
				this.Popup("调整盘点成功");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			this.BindGrid();
		}
		
	}
}
