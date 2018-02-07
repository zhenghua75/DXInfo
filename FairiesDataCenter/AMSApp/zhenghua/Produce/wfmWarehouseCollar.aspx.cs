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
	/// wfmMakeLog ��ժҪ˵����
	/// </summary>
	public class wfmWarehouseCollar :wfmBase
	{
		#region �ֶ�
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
			Button2.Attributes["onClick"]="javascript:return confirm(getwh());";   
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();

				DataTable dt = Helper.Query("select * from tbmakelog where cnnproduceserialno="+strProduceSerialNo);
				if(dt.Rows.Count==0)
				{
					this.Popup("����������ϸ����");
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
				operLog.cnvcOperType = "����Ԥ��";

				ProduceFacade pf = new ProduceFacade();
				pf.AddMakeLog(pLog,operLog);
				Popup("Ԥ���ɹ�");
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
			//this.DataGrid1.Caption = "���ϵ�"+DateTime.Now.ToString("yyyy��MM��dd��");
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
			//Ԥ������
			try
			{
				string strProduceSerialNo = this.txtProduceSerialNo.Text;
				OperLog ol = new OperLog();
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;
				ol.cnvcOperType = "����Ԥ��";
				//AdjustMakeDetail

				ProduceFacade pf = new ProduceFacade();
				pf.AdjustMakeDetail(strProduceSerialNo,ol);
				this.Popup("����Ԥ���ɹ�");
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
			//����
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
					throw new Exception("�޴˴��");
				}
				Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);
				if(!inv.cnbSelf && strAdjustcount != "0")
				{
					throw new Exception("�����ƴ�������ܵ�������");
				}
				OperLog ol = new OperLog();
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;
				ol.cnvcOperType = "��������";

				MakeDetail md = new MakeDetail();
				md.cnnMakeSerialNo = Convert.ToDecimal(strMakeSerialNo);
				md.cnnAdjustCount = Convert.ToDecimal(strAdjustcount);
				md.cnnStCount = Convert.ToDecimal(strstcount);
				md.cnvcInvCode = strinvcode;
				ProduceFacade pf = new ProduceFacade();
				pf.UpdateMakeDetail(strProduceSerialNo,md,ol);
				this.Popup("���µ�������������������ɹ�");
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
			//���ϵ���ӡ
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
					//this.Popup(strinvname+"�Ѿ������������������");
					this.Popup("�Ѿ�������������");
					return;
				}
				
				if(!this.JudgeIsNum(strwhcount))
				{
					this.Popup(strinvname+"�޿�棬��������ԭ���ϣ�����ԭ���Ͽ��");
					return;
				}
				decimal doutcount = Convert.ToDecimal(stroutcount);
				decimal dwhcount = Convert.ToDecimal(strwhcount);
				if(doutcount>dwhcount)
				{
					this.Popup(strinvname+"����������㣬��������ԭ���Ͻ�������������ԭ���Ͽ��");
					return;
				}
				Entity.RdRecordDetail rrd = new RdRecordDetail();
				rrd.cnvcInvCode = strinvcode;
				rrd.cnnQuantity = Convert.ToDecimal(stroutcount);
				DataRow[] drInvs = dtInv.Select("cnvcInvCode='"+strinvcode+"'");
				if(drInvs.Length == 0)
				{
					this.Popup(strinvname+"�������δ�ҵ�");
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
			rr.cnvcComments = "������������";
			rr.cnvcMaker = this.oper.strLoginID;
			rr.cnnProorderID = Convert.ToDecimal(this.txtProduceSerialNo.Text);
			rr.cnvcState = "2";


			OperLog ol = new OperLog();
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;
			ol.cnvcOperType = "������������";
			
			string strWarehouse = this.ddlWarehouse.SelectedValue;
			try
			{
				ProduceFacade pf = new ProduceFacade();
				pf.Collar(this.txtMakeSerialNo.Text,rr,al,ol,strWarehouse);
				this.Popup("�����������óɹ�");
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);				
			}
			BindGrid();
		}

	}
}
