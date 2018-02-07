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
	/// wfmProduceOrder ��ժҪ˵����
	/// </summary>
	public class wfmProduceOrder : wfmBase
	{
		#region �ֶ�
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProduceDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProduceDate;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtShipBeginDate;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtShipEndDate;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button btnModify;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		protected System.Web.UI.WebControls.Button btnQueryOrder;
		protected System.Web.UI.WebControls.Button btnLinkOrder;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.Button btnQueryProduct;
		protected System.Web.UI.WebControls.CheckBox chkSelf;
		protected System.Web.UI.WebControls.Label lblTitle;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				if(Request["OperType"] == null || Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strOperType = Request["OperType"].ToString();
				//ViewState["OperType"] = strOperType;
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();

				BindDisp(strOperType);
				BindProduceLog(strProduceSerialNo);
				this.btnQueryProduct_Click(null,null);
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
			this.btnLinkOrder.Click += new System.EventHandler(this.btnLinkOrder_Click);
			this.btnQueryOrder.Click += new System.EventHandler(this.btnQueryOrder_Click);
			this.btnQueryProduct.Click += new System.EventHandler(this.btnQueryProduct_Click);
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid2_PageIndexChanged);
			this.DataGrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_CancelCommand);
			this.DataGrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_EditCommand);
			this.DataGrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_UpdateCommand);
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
				this.txtShipBeginDate.Text = produceLog.cndShipBeginDate.ToString("yyyy-MM-dd");
				this.txtShipEndDate.Text = produceLog.cndShipEndDate.ToString("yyyy-MM-dd");
				this.chkSelf.Checked = produceLog.cnbSelf;
				if(produceLog.cnvcProduceState!= "0")
				{
					this.btnModify.Visible = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;
				}				

			}
		}
		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQuery.aspx");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
//			string strOperType = ViewState["OperType"].ToString();
//			if(strOperType == "Edit")
//			{
				BindProduceLog(this.txtProduceSerialNo.Text);
			//}
		}
		private void BindDisp(string strOperType)
		{
			switch(strOperType)
			{
				case "Edit":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = true;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = true;
					this.txtShipEndDate.Enabled = true;

					this.btnModify.Visible = true;
					this.chkSelf.Enabled = true;
					this.btnLinkOrder.Visible = false;
					this.btnQueryOrder.Visible = false;
					this.btnQueryProduct.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;

					this.lblTitle.Text = "�����ƻ��޸�";
					break;
				case "Order":
					this.ddlProduceDept.Enabled = false;
					this.txtProduceDate.Enabled = false;
					this.txtProduceSerialNo.Enabled = false;
					this.txtShipBeginDate.Enabled = false;
					this.txtShipEndDate.Enabled = false;

					this.btnModify.Visible = false;
					this.btnLinkOrder.Visible = true;
					this.btnQueryOrder.Visible = true;
					this.btnQueryProduct.Visible = true;
					this.btnCancel.Visible = false;

					this.DataGrid1.Visible = false;
					this.DataGrid2.Visible = false;
					this.chkSelf.Enabled = false;

					this.lblTitle.Text = "�����ƻ���������";
					this.btnLinkOrder.Text = "��������";
					this.btnQueryOrder.Text = "�����嵥";
					break;				
			}
		}

		private void btnModify_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(JudgeIsNull(txtProduceDate.Text,"��������"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipBeginDate.Text,"��ʼ����"))
				{
					//Popup();
					return;
				}
				if(JudgeIsNull(txtShipEndDate.Text,"��������"))
				{
					//Popup();
					return;
				}
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				pLog.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				pLog.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				pLog.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				pLog.cnvcOperID = oper.strLoginID;
				pLog.cnbSelf = this.chkSelf.Checked;
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "�޸������ƻ�";

				ProduceFacade pFacade = new ProduceFacade();
				pFacade.UpdateProduceLog(pLog,operLog);
				Popup("�޸ĳɹ�");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void btnLinkOrder_Click(object sender, System.EventArgs e)
		{
			//��������
			try
			{
				ProduceLog pLog = new ProduceLog();
				pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
				pLog.cndProduceDate = DateTime.Parse(txtProduceDate.Text);
				pLog.cndShipBeginDate = DateTime.Parse(txtShipBeginDate.Text);
				pLog.cndShipEndDate = DateTime.Parse(txtShipEndDate.Text);
				pLog.cnvcProduceDeptID = ddlProduceDept.SelectedValue;
				pLog.cnvcOperID = oper.strLoginID;

				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "��������";
				


				ProduceFacade pFacade = new ProduceFacade();
				pFacade.LindOrder(pLog,operLog);
				Popup("���������ɹ�");		
				btnQueryProduct_Click(null,null);
			}		
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
			
		}

		private void btnQueryOrder_Click(object sender, System.EventArgs e)
		{
			//string strOperType = ViewState["OperType"].ToString();
			string strSql = "";
			DataTable dtProduceOrderLog = null;
			strSql = "select a.cnnProduceSerialNo,b.cnnOrderSerialNo,b.cnvcOrderDeptID,b.cnvcProduceDeptID,b.cnvcOrderType,b.cndShipDate,b.cnvcOperID,b.cndOperDate from tbProduceOrderLog a "
				+ " left join tbOrderBook b on a.cnnOrderSerialNo=b.cnnOrderSerialNo "
				+ " where a.cnvcType='0' and a.cnnProduceSerialNo=" + txtProduceSerialNo.Text;
			dtProduceOrderLog = Helper.Query(strSql);
			this.DataTableConvert(dtProduceOrderLog, "cnvcOrderType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType='ORDERTYPE'");
			//this.DataGrid1.Columns[6].HeaderText = "��������";

			//dtProduceOrderLog = Helper.Query(strSql);
			this.DataTableConvert(dtProduceOrderLog, "cnvcOrderDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");
			this.DataTableConvert(dtProduceOrderLog, "cnvcProduceDeptID", "tbDept", "cnvcDeptID", "cnvcDeptName", "");			
			this.DataTableConvert(dtProduceOrderLog, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");

			this.DataGrid1.DataSource = dtProduceOrderLog;
			this.DataGrid1.DataBind();
			this.DataGrid1.Visible = true;
			this.DataGrid2.Visible = false;
			
		}

		private void btnQueryProduct_Click(object sender, System.EventArgs e)
		{
			string strSql = "";
			DataTable dtDetail = null;
			strSql = "select * from tbProduceDetail where cnnProduceSerialNo=" + txtProduceSerialNo.Text;


			dtDetail = Helper.Query(strSql);
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcStComUnitCode","tbInventory","cnvcInvCode","cnvcStComUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcStComUnitCode","cnvcStComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");

			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
			this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");

			this.DataTableConvert(dtDetail,"cnvcComUnitCode","cniChangRate","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
			this.DataTableConvert(dtDetail,"cnvcStComUnitCode","cniChangRate_st","tbComputationUnit","cnvcComUnitCode","cniChangRate","");


			strSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"') "
				+" and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) "
				+"group by cnvcInvCode";
			DataTable dtwh = Helper.Query(strSql);
			

			Helper.DataTableConvert(dtDetail,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");

			foreach(DataRow dr in dtDetail.Rows)
			{
				if(dr["cnnwhcount"].ToString() == "") dr["cnnwhcount"] = 0;
				double dwhcount = Convert.ToDouble(dr["cnnwhcount"].ToString());
				double dchangRate = Convert.ToDouble(dr["cniChangRate"].ToString());
				double dchangRate_st = Convert.ToDouble(dr["cniChangRate_st"].ToString());
				//dr["cnnwhcount"] = Math.Floor(dwhcount*dchangRate/dchangRate_st);
				dr["cnnwhcount"] = Math.Round(dwhcount*dchangRate/dchangRate_st,4);

			}

			this.DataGrid2.DataSource = dtDetail;
			this.DataGrid2.DataBind();
			this.DataGrid2.Visible = true;
			this.DataGrid1.Visible = false;
		}
		#region ҳ���¼�
		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.btnQueryOrder_Click(null, null);
		}

		private void DataGrid2_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DataGrid2.CurrentPageIndex = e.NewPageIndex;
			this.btnQueryProduct_Click(null, null);
		}

		private void DataGrid2_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = e.Item.ItemIndex;
			this.btnQueryProduct_Click(null, null);
		}

		private void DataGrid2_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.DataGrid2.EditItemIndex = -1;
			this.btnQueryProduct_Click(null, null);
		}
		#endregion
		private void DataGrid2_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//���¼ƻ���������
			try
			{
				OperLog ol = new OperLog();
				ol.cnvcOperType = "���¼ƻ���������";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;
				
				ProduceDetail pd = new ProduceDetail();
				pd.cnnProduceSerialNo = Convert.ToDecimal(this.txtProduceSerialNo.Text);
				pd.cnvcInvCode = e.Item.Cells[0].Text;
				pd.cnnProduceCount = Convert.ToDecimal(((TextBox)e.Item.Cells[6].Controls[0]).Text);
				ProduceFacade pf = new ProduceFacade();
				pf.UpdateProduceDetail(pd,ol);
				this.DataGrid2.EditItemIndex = -1;
				btnQueryProduct_Click(null,null);
				this.Popup("�ɹ����¼ƻ���������");
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
		}
	}
}
