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
	public class wfmMakeLog :wfmBase
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
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnMakeLog;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.CheckBox chkSelf;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWarehouse;
		protected System.Web.UI.WebControls.Label Label1;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnClear.Attributes.Add("onclick","if(!confirm('�Ƿ�ȷ�����Ԥ�����ݣ�'))return false;");				
			if(!this.IsPostBack)
			{
				this.BindDept(ddlProduceDept, "cnvcDeptType <>'Corp'");
				if(Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				BindProduceLog(strProduceSerialNo);
				//Session["ProduceSerialNo"] = strProduceSerialNo;
				this.DataGrid1.Visible = false;
				this.BindWarehouse(ddlWarehouse,"cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"' and cnbFreeze=0");
				btnQueryMake_Click(null,null);
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
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnMakeLog.Click += new System.EventHandler(this.btnMakeLog_Click);
			this.btnQueryMake.Click += new System.EventHandler(this.btnQueryMake_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
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
				this.chkSelf.Checked = produceLog.cnbSelf;
				
				this.txtProduceDate.Enabled = false;
				this.txtProduceSerialNo.Enabled = false;
				this.ddlProduceDept.Enabled = false;
				this.chkSelf.Enabled = false;

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
				if(chkSelf.Checked)
				{
					pf.AddMakeLogSelf(pLog,operLog);
				}
				else
				{
					
					pf.AddMakeLog(pLog,operLog);
				}
				Popup("Ԥ���ɹ�");
				BindGrid();
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		
		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmProducePlanQueryMake.aspx");
		}

		private void btnQueryMake_Click(object sender, System.EventArgs e)
		{
			BindGrid();
			this.DataGrid1.Visible = true;
		}

		private void BindGrid()
		{
			string strProduceSerialNo = txtProduceSerialNo.Text;

			string strsql1 = "select * from tbMakeLog where cnnProduceSerialNo="+strProduceSerialNo;
			DataTable dtMakeLog = Helper.Query(strsql1);
			if(dtMakeLog.Rows.Count == 0 )
			{
				//this.Popup("�޷���λ�����ƻ�");
				return;
			}
			Entity.MakeLog ml = new MakeLog(dtMakeLog);

			string strsql2 = "select * from tbMakeDetail where cnnMakeSerialNo="+ml.cnnMakeSerialNo.ToString();
			DataTable dtMakeDetail = Helper.Query(strsql2);

//			string strSql = @"select a.*,b.cnvcOperID,b.cndOperDate from tbMakeDetail a 
//left outer join tbMakeLog b on b.cnnMakeSerialNo=b.cnnMakeSerialNo
//where b.cnnProduceSerialNo=" + strProduceSerialNo;
//			DataTable dtMakeLog = Helper.Query(strSql);
			Helper.DataTableConvert(dtMakeDetail,"cnnMakeSerialNo","cnvcOperID",dtMakeLog,"cnnMakeSerialNo","cnvcOperID","");
			Helper.DataTableConvert(dtMakeDetail,"cnnMakeSerialNo","cndOperDate",dtMakeLog,"cnnMakeSerialNo","cndOperDate","");

			this.DataTableConvert(dtMakeDetail, "cnvcOperID", "tbLogin", "vcLoginID", "vcOperName", "");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcProduceUnitCode","tbInventory","cnvcInvCode","cnvcProduceUnitCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcStComUnitCode","tbInventory","cnvcInvCode","cnvcStComUnitCode","");
			//this.DataTableConvert(dtMakeLog,"cnvcInvCode","cnbSelf","tbInventory","cnvcInvCode","cnbSelf","");
			this.DataTableConvert(dtMakeDetail,"cnvcProduceUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataTableConvert(dtMakeDetail,"cnvcStComUnitCode","cnvcStComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvCCode","tbInventory","cnvcInvCode","cnvcInvCCode","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCCode","cnvcProductClass","tbProductClass","cnvcProductClassCode","cnvcProductClassName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCCode","cnvcProductType","tbProductClass","cnvcProductClassCode","cnvcProductType","");
			this.DataTableConvert(dtMakeDetail,"cnvcProductType","cnvcProductTypeName","tbNameCode","cnvcCode","cnvcName","");
			this.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");

			this.DataTableConvert(dtMakeDetail,"cnvccomunitcode","cnnchangrate","tbComputationUnit","cnvccomunitcode","cnichangrate","");
			//this.DataTableConvert(dtMakeDetail,"cnvcproduceunitcode","cnnchangrate_pd","tbComputationUnit","cnvccomunitcode","cnichangrate","");
			this.DataTableConvert(dtMakeDetail,"cnvcstcomunitcode","cnnchangrate_st","tbComputationUnit","cnvccomunitcode","cnichangrate","");

//			string strSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+this.ddlProduceDept.SelectedValue+"')"
//+" and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) "
//+"group by cnvcInvCode";
			string strSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
='"+this.ddlWarehouse.SelectedValue+"'"
				+" and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) "
				+"group by cnvcInvCode";
			DataTable dtwh = Helper.Query(strSql);

			Helper.DataTableConvert(dtMakeDetail,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");
			foreach(DataRow dr in dtMakeDetail.Rows)
			{
				if(dr["cnnchangrate"].ToString() == "")
				{
					this.Popup("�޴˲�Ʒ���룺"+dr["cnvcInvCode"].ToString()+"�Ĳ�Ʒ��");
					return;
				}
				double dchangerate = Convert.ToDouble(dr["cnnchangrate"].ToString());
				double dchangerate_st = Convert.ToDouble(dr["cnnchangrate_st"].ToString());
				//double dchangerate_pd = Convert.ToDouble(dr["cnnchangrate_pd"].ToString());
				string strwh = dr["cnnwhcount"].ToString();
				decimal dwh = 0;
				if( strwh != "")						
				{														
					//dwh = Convert.ToDecimal(Math.Floor(Convert.ToDouble(strwh)*dchangerate/dchangerate_st));
					dwh = Convert.ToDecimal(Math.Round(Convert.ToDouble(strwh)*dchangerate/dchangerate_st,4));
				}
				dr["cnnwhcount"] = dwh;
			}
			DataView dvMake = new DataView(dtMakeDetail);
			dvMake.Sort = "cnvcProductType asc,cnvcInvCCode asc";
			this.DataGrid1.DataSource = dvMake;
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
				if(this.chkSelf.Checked)
				{
					pf.AdjustMakeDetailSelf(strProduceSerialNo,ol);
				}
				else
				{
					pf.AdjustMakeDetail(strProduceSerialNo,ol);
				}
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
				string strAdjustcount = ((TextBox)e.Item.Cells[12].Controls[0]).Text;

				string strstcount = ((TextBox)e.Item.Cells[11].Controls[0]).Text;
				string strinvcode = e.Item.Cells[5].Text;

				string strmakecount = e.Item.Cells[8].Text;
				string strcount = e.Item.Cells[13].Text;
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
//				if(Convert.ToDecimal(strmakecount)>Convert.ToDecimal(strcount)+Convert.ToDecimal(strAdjustcount)+Convert.ToDecimal(strstcount))
//					throw new Exception("�ƻ������������ܴ��ڿ��������������������������������֮��");
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
			string strsql = "select * from tbmakelog where cnnproduceserialno="+this.txtProduceSerialNo.Text;
			DataTable dt = Helper.Query(strsql);
			if(dt.Rows.Count == 0)
			{
				this.Popup("�޷���λԤ����ˮ");
				return;
			}
			Entity.MakeLog ml = new MakeLog(dt);
			this.Response.Redirect("wfmMakeDetail.aspx?MakeType=0&MakeSerialNo="+ml.cnnMakeSerialNo.ToString()+"&ProduceSerialNo="+this.txtProduceSerialNo.Text);
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			//���Ԥ������
//			ProduceLog pLog = new ProduceLog();
//			pLog.cnnProduceSerialNo = decimal.Parse(txtProduceSerialNo.Text);
//			pLog.cnvcOperID = oper.strLoginID;

			OperLog operLog = new OperLog();
			operLog.cnvcOperID = oper.strLoginID;
			operLog.cnvcDeptID = oper.strDeptID;
			operLog.cnvcOperType = "���Ԥ������";
			ProduceFacade pf = new ProduceFacade();

			try
			{
				pf.ClearMake(txtProduceSerialNo.Text,operLog);
				Popup("���Ԥ�����ݳɹ�");				
				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
			}
			catch(Exception ex)
			{
				this.Popup(ex.Message);
			}
			
		}

	}
}
