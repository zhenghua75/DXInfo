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
	/// wfmMakeDetail ��ժҪ˵����
	/// </summary>
	public class wfmMakeDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnExcel;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblProduceSerialNo;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.TextBox txtProduceSerialNo;
		private void Page_Load(object sender, System.EventArgs e)
		{			
			// �ڴ˴������û������Գ�ʼ��ҳ��
			//btnPrint.Attributes.Add("onclick","return PrintDataGrid(document.all('DataGrid1'))");
			if(!this.IsPostBack)
			{
				if(Request["MakeSerialNo"] == null || Request["ProduceSerialNo"] == null)
				{
					Popup("��Ч����");
					return;
				}
				//string strDetailSql="";
				string strMakeSerialNo = Request["MakeSerialNo"].ToString();
				string strProduceSerialNo = Request["ProduceSerialNo"].ToString();
				//string strMakeType = Request["MakeType"].ToString();
				//string strMakeLogSql = "select * from tbMakeLog where cnnMakeSerialNo="+strMakeSerialNo;
				string strSql = "select * from tbMakeDetail where cnnstcount>0  and cnnMakeSerialNo="+strMakeSerialNo;
				DataTable dtDetail = Helper.Query(strSql);
				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcInvName","tbInventory","cnvcInvCode","cnvcInvName","");
				//this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcinvccode","tbInventory","cnvcInvCode","cnvcinvccode","");
				//this.DataTableConvert(dtDetail,"cnvcInvcCode","cnvcproducttype","tbproductclass","cnvcProductClassCode","cnvcproducttype","");
				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcInvStd","tbInventory","cnvcInvCode","cnvcInvStd","");
				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcComUnitCode","tbInventory","cnvcInvCode","cnvcComUnitCode","");
				this.DataTableConvert(dtDetail,"cnvcComUnitCode","cnvcComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcSTComUnitCode","tbInventory","cnvcInvCode","cnvcSTComUnitCode","");
				this.DataTableConvert(dtDetail,"cnvcSTComUnitCode","cnvcSTComUnitName","tbComputationUnit","cnvcComUnitCode","cnvcComUnitName","");
				

//				this.DataTableConvert(dtDetail,"cnvcComUnitCode","cniChangeRate","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
//				this.DataTableConvert(dtDetail,"cnvcSTComUnitCode","cniChangeRate_st","tbComputationUnit","cnvcComUnitCode","cniChangRate","");
//				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");

//				this.DataTableConvert(dtDetail,"cnvcInvCode","cnvcGroupCode","tbInventory","cnvcInvCode","cnvcGroupCode","");
//				this.DataTableConvert(dtDetail,"cnvcGroupCode","cnvcComUnitCode_main","tbComputationUnit","cnvcGroupCode","cnvcComUnitCode","cnbMainUnit=1");
//				this.DataTableConvert(dtDetail,"cnvcComUnitCode_main","cniChangeRate_main","tbComputationUnit","cnvcGroupCode","cniChangRate","");
//				dtDetail.Columns.Add("cnnOutCount");
//				//DataTable dtUnit = Application["tbComputationUnit"] as DataTable;
//				foreach(DataRow dr in dtDetail.Rows)
//				{
//					if(dr["cniChangeRate_st"].ToString() == "")
//						dr["cniChangeRate_st"] = 1;
//					if(dr["cniChangeRate_main"].ToString() == "")
//						dr["cniChangeRate_main"] = 1;
//					string strcomunitcode = dr["cnvccomunitcode"].ToString();
//					string strcomunitcode_main = dr["cnvcComUnitCode_main"].ToString();
//
//					decimal dstcount = Convert.ToDecimal(dr["cnnstcount"].ToString());
//					decimal dchangerate = Convert.ToDecimal(dr["cniChangeRate"].ToString());
//					decimal dchangerate_st = Convert.ToDecimal(dr["cniChangeRate_st"].ToString());
//					decimal dchangerate_main = Convert.ToDecimal(dr["cniChangeRate_main"].ToString());
//
//					if(strcomunitcode == strcomunitcode_main)
//					{
//						dr["cnnOutCount"] = (dchangerate/dchangerate_st) * dstcount;
//					}
//					else
//					{
//						dr["cnnOutCount"] = ((dchangerate_main/dchangerate_st) /(dchangerate_main/dchangerate)) * dstcount;
//					}
//				}
				//DataTable dtMakeLog = Helper.Query(strMakeLogSql);
				//MakeLog mLog = new MakeLog(dtMakeLog);
				//DataView dv = new DataView(dtDetail);
				//dv.RowFilter = "cnvcproducttype<>'FINALPRODUCT' and ";
				this.txtProduceSerialNo.Text = strProduceSerialNo;
				this.lblProduceSerialNo.Text = "������ţ�"+strProduceSerialNo.PadLeft(10,'0');
				this.lblTitle.Text = "���ϵ�"+DateTime.Now.ToString("yyyy��MM��dd��");
				//this.DataGrid1.Caption
				this.DataGrid1.DataSource = dtDetail;
				this.DataGrid1.DataBind();		
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
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Datagrid2.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.Datagrid2_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			//if(Session["ProduceSerialNo"] != null)
			this.Response.Redirect("wfmMakeLog.aspx?ProduceSerialNo="+this.txtProduceSerialNo.Text);//Session["ProduceSerialNo"].ToString());
		}

		private void btnExcel_Click(object sender, System.EventArgs e)
		{
			if(this.DataGrid1.Items.Count > 0)
			{
				this.DataGridToExcel(DataGrid1, this.DataGrid1.Caption);
			}
			else
			{
				this.DataGridToExcel(Datagrid2, this.Datagrid2.Caption);
			}
			
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)
			{
				e.Item.Cells[0].ColumnSpan=4;    
				e.Item.Cells[0].Text = "����������ˣ�";
				for(int  j=1;j<4;j++)      
				{      
					e.Item.Cells[j].Visible=false;      
				}   
				e.Item.Cells[4].ColumnSpan=3;     
				e.Item.Cells[4].Text = "�����ˣ�"+oper.strOperName;
				for(int  j=3;j<5;j++)      
				{      
					e.Item.Cells[j].Visible=false;      
				}
			}
		}

		private void Datagrid2_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Footer)      
			{      
				e.Item.Cells[0].ColumnSpan=2;     
				e.Item.Cells[0].Text = "����������ˣ�";
				e.Item.Cells[1].Visible=false;     
				e.Item.Cells[2].ColumnSpan=2;  
				e.Item.Cells[2].Text = "�����ˣ�"+oper.strOperName;
				e.Item.Cells[3].Visible=false; 				
			}
		}
	}
}
