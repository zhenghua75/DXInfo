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
	/// wfmDividGoods ��ժҪ˵����
	/// </summary>
	public class wfmDividGoods : wfmBase
	{
		#region �ֶ�
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.TextBox txtProduceState;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button btnQueryGoods;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlOrderDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnDivideGoods;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.DropDownList ddlAssignSerialNo;
		protected System.Web.UI.WebControls.Label lblWarehouse;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.Button btnClearDivideGoods;		
		protected System.Web.UI.WebControls.DropDownList ddlWarehouse;
		#endregion
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��btnDivideGoods
			btnDivideGoods.Attributes["onClick"]="javascript:return confirm(getwh());"; 
			Button1.Attributes["onClick"]="javascript:return confirm(getwh());"; 
			btnClearDivideGoods.Attributes["onClick"] = "javascript:return confirm('ȷ������ֻ�������');";
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				this.BindDept(ddlOrderDept, "cnvcDeptType <>'Corp'");
				ListItem li = new ListItem("����", "%");
				ddlOrderDept.Items.Insert(0, li);
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				BindProduceLog(strProduceSerialNo);
				this.BindWarehouse(ddlWarehouse,"cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"'");
				this.btnExcel.Enabled = false;
				if(ddlAssignSerialNo.Items.Count > 0)
					btnQuery_Click(null,null);
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
			this.btnDivideGoods.Click += new System.EventHandler(this.btnDivideGoods_Click);
			this.btnClearDivideGoods.Click += new System.EventHandler(this.btnClearDivideGoods_Click);
			this.btnQueryGoods.Click += new System.EventHandler(this.btnQueryGoods_Click);
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
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
				txtProduceState.Text = produceLog.cnvcProduceState;
				this.CheckBox1.Checked = produceLog.cnvcProduceState=="7"?true:false;

			}
			BindAssignLog(strProduceSerialNo);
			

		}
		private void BindAssignLog(string strProduceSerialNo)
		{
			string strAssignSql = "select distinct cnnAssignSerialNo from tbAssignLog where cnnProduceSerialNo=" + strProduceSerialNo;
			DataTable dtAssignLog = Helper.Query(strAssignSql);
			this.ddlAssignSerialNo.Items.Clear();
			this.ddlAssignSerialNo.DataSource = dtAssignLog;
			this.ddlAssignSerialNo.DataTextField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataValueField = "cnnAssignSerialNo";
			this.ddlAssignSerialNo.DataBind();
		}
		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQueryGoods.aspx");
		}

		private void btnDivideGoods_Click(object sender, System.EventArgs e)
		{
			//���ɷֻ�����
			try
			{
				string strwhhouse = this.ddlWarehouse.SelectedValue;
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�ֻ�";

				GoodsFacade goods = new GoodsFacade();
				goods.AddAssignLog(pLog,operLog,strwhhouse);
				BindAssignLog(txtProduceSerialNo.Text);
				Popup("�ֻ��ɹ�");
				this.btnQuery_Click(null,null);
				//this.btnDivideGoods.Enabled = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		private void btnQueryGoods_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ddlAssignSerialNo.Items.Count <1)
					throw new Exception("���������ɷֻ�����");
				//���ɷֻ�ƾ��
				string strSql = "select a.cnvcReceiveDeptID,b.cnvcInvCode,sum(b.cnnAssignCount) as cnnAssignCount from tbAssignLog a "
					+ " left outer join tbAssignDetail b "
					+ " on a.cnnAssignSerialNo=b.cnnAssignSerialNo "
					+ " and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
					+ " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue//txtProduceSerialNo.Text
					+ " group by a.cnvcReceiveDeptID,b.cnvcInvCode  order by b.cnvcInvCode";
				DataTable dtAssign = Helper.Query(strSql);
				this.DataTableConvert(dtAssign, "cnvcReceiveDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");//cnvcshipDeptID
				this.DataTableConvert(dtAssign,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
				DataTable dtDept = (DataTable)Application["tbDept"];
			
				string strSql2 = "select b.cnvcInvCode,sum(b.cnnAssignCount) as cnnAssignCount from tbAssignLog a "
					+ " left outer join tbAssignDetail b "
					+ " on a.cnnAssignSerialNo=b.cnnAssignSerialNo "
					+ " and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
					+ " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue//txtProduceSerialNo.Text
					+ " group by b.cnvcInvCode  order by b.cnvcInvCode";
				DataTable dtAssign2 = Helper.Query(strSql2);
				this.DataTableConvert(dtAssign2,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
				this.DataGrid1.Caption = "�ֻ�ƾ��";
				DataTable dtpt = new DataTable();
				dtpt.Columns.Add("��Ʒ����");
				dtpt.Columns.Add("��Ʒ����");
				foreach(DataRow drDept in dtDept.Rows)
				{
					if(!drDept["cnvcDeptType"].ToString().Equals("Corp"))//&&!drDept["cnvcDeptType"].ToString().Equals("CEN00"))
					{
						dtpt.Columns.Add(drDept["cnvcDeptName"].ToString());	
					}
						
				}
				dtpt.Columns.Add("�ϼ�");
				Hashtable htProduct = new Hashtable();
				foreach(DataRow drAssign in dtAssign.Rows)
				{
					//DataRow drpt = null;
					DataRow[] drpts = dtpt.Select("��Ʒ����='"+drAssign["cnvcInvCode"].ToString()+"'");
					if(drpts.Length > 0)
					{
						DataRow drpt = drpts[0];
						drpt["��Ʒ����"] = drAssign["cnvcInvCode"].ToString();
						drpt["��Ʒ����"] = drAssign["cnvcInvName"].ToString();
						drpt[drAssign["cnvcReceiveDeptIDComments"].ToString()] = drAssign["cnnAssignCount"].ToString();
						DataRow[] drAssign2 = dtAssign2.Select("cnvcInvCode='" + drAssign["cnvcInvCode"].ToString() + "'");
						if(drAssign2.Length > 0)
							drpt["�ϼ�"] = drAssign2[0]["cnnAssignCount"];
						//dtpt.Rows.Add(drpt);
					}
					else
					{
						DataRow drpt = dtpt.NewRow();
						drpt["��Ʒ����"] = drAssign["cnvcInvCode"].ToString();
						drpt["��Ʒ����"] = drAssign["cnvcInvName"].ToString();
						drpt[drAssign["cnvcReceiveDeptIDComments"].ToString()] = drAssign["cnnAssignCount"].ToString();
						DataRow[] drAssign2 = dtAssign2.Select("cnvcInvCode='" + drAssign["cnvcInvCode"].ToString() + "'");
						if(drAssign2.Length > 0)
							drpt["�ϼ�"] = drAssign2[0]["cnnAssignCount"];
						dtpt.Rows.Add(drpt);

					}
				
				
				}
				DataView dv = new DataView(dtpt);
				dv.Sort = "��Ʒ����";
				this.DataGrid1.DataSource = dv;
				this.DataGrid1.DataBind();

				this.DataGrid2.DataSource = null;
				this.DataGrid2.DataBind();
				this.btnExcel.Enabled=true;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void BindAssignDetail()
		{
			string strSql = "select a.*,b.cnvcShipDeptID,b.cnvcReceiveDeptID,b.cndShipDate,c.cnvcOrderType,d.cnvcInvName,d.cnvcComUnitCode,d.cnfRetailPrice  "
			                + " from tbAssignDetail a "
			                + " left outer join tbAssignLog  b on a.cnnAssignSerialNo=b.cnnAssignSerialNo and a.cnnOrderSerialNo=b.cnnOrderSerialNo "
			                + " left outer join tbOrderBook c on a.cnnOrderSerialNo=c.cnnOrderSerialNo "
				+" left outer join tbInventory d on a.cnvcInvCode = d.cnvcInvCode "
			                + " where a.cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue + " and cnvcReceiveDeptID like '" +
			                ddlOrderDept.SelectedValue + "' and a.cnnOrderSerialNo not in (select cnnOrderSerialNo from tbAssignLog where cnnPrintFlag>0 and cnnAssignSerialNo=" + ddlAssignSerialNo.SelectedValue + ") ";
			if(txtProductCode.Text != "")
			{
				strSql += " and a.cnvcInvCode like '%" + txtProductCode.Text + "%'";
			}
			if(txtProductName.Text != "")
			{
				strSql += " and d.cnvcInvName like '%" + txtProductName.Text + "%'";
			}
			strSql += " order by a.cnvcInvCode";
			DataTable dtAssignDetail = Helper.Query(strSql);
			this.DataTableConvert(dtAssignDetail,"cnvcComUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataTableConvert(dtAssignDetail, "cnvcShipDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtAssignDetail, "cnvcReceiveDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtAssignDetail, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			dtAssignDetail.Columns.Add("cnnSum");
			foreach(DataRow dr in dtAssignDetail.Rows)
			{
				dr["cnnSum"] = Convert.ToDouble(dr["cnnAssignCount"].ToString())*Convert.ToDouble(dr["cnfRetailPrice"].ToString());
			}
			this.DataGrid2.DataSource = dtAssignDetail;
			this.DataGrid2.DataBind();
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}
		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(ddlAssignSerialNo.Items.Count < 1)
				{
					this.DataGrid2.DataSource = null;
					this.DataGrid2.DataBind();
					this.DataGrid1.DataSource = null;
					this.DataGrid1.DataBind();
					throw new Exception("���������ɷֻ�����");
				}
				this.btnExcel.Enabled=false;
				BindAssignDetail();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			BindAssignDetail();
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			BindAssignDetail();

		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			BindAssignDetail();
		}

		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
//				if(txtProduceState.Text == "4")
//				{
//					throw new Exception("�˶��������䣬�����޸ķֻ���");
//				}
				string strCount = ((TextBox) e.Item.Cells[11].Controls[0]).Text;
				if(this.JudgeIsNull(strCount))
					throw new Exception("�������̵���");
				if(!this.JudgeIsNum(strCount))
					throw new Exception("����������");
				AssignDetail assign = new AssignDetail();
				assign.cnnAssignSerialNo = Convert.ToDecimal(e.Item.Cells[0].Text);
				assign.cnnProduceSerialNo = Convert.ToDecimal(txtProduceSerialNo.Text);
				assign.cnnOrderSerialNo = Convert.ToDecimal(e.Item.Cells[3].Text);
				assign.cnvcInvCode = e.Item.Cells[6].Text;
				assign.cnnAssignCount = Convert.ToDecimal(((TextBox) e.Item.Cells[11].Controls[0]).Text);				
				string strOrderType = e.Item.Cells[14].Text;
				decimal dOrderCount = Convert.ToDecimal(e.Item.Cells[10].Text);
				if(strOrderType=="WDO")
				{
					if(dOrderCount > assign.cnnAssignCount)
					{
						throw new Exception("�ⶩ�����ֻ����������ڶ�����");
					}
				}
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�����ֻ���";

				GoodsFacade gf = new GoodsFacade();
				gf.UpdateAssignLog(assign,operLog);
				this.Popup("�޸ĳɹ�");
				this.DataGrid2.EditItemIndex = -1;
				BindAssignDetail();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);					
			}

		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			this.DataGridToExcel(this.DataGrid1, this.ddlProduceDept.SelectedItem.Text+"�ֻ�ƾ��");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//�ֻ�����
			string strWarehouse = this.ddlWarehouse.SelectedValue;
			string strProduceSerialNo = this.txtProduceSerialNo.Text;
			try
			{
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�ֻ�����";

				GoodsFacade gf = new GoodsFacade();
				gf.AssignOut(strProduceSerialNo,operLog,strWarehouse);
				this.CheckBox1.Checked = true;
				this.Popup("�ֻ�����ɹ�");				
			}
			catch(Exception ex)
			{
				Popup(ex.Message);					
			}
		}

		private void btnClearDivideGoods_Click(object sender, System.EventArgs e)
		{
			//����ֻ�����
			try
			{
				//string strwhhouse = this.ddlWarehouse.SelectedValue;
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "����ֻ�����";

				GoodsFacade goods = new GoodsFacade();
				goods.DeleteAssignLog(pLog,operLog);
				BindAssignLog(txtProduceSerialNo.Text);
				Popup("����ֻ����ݳɹ�");
				this.btnQuery_Click(null,null);
				//this.btnDivideGoods.Enabled = false;
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}
	}
}
