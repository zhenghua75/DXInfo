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
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmPrintReceiveSend.
	/// </summary>
	public class wfmPrintReceiveSend : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSendSerial;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;

		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.btnPrint.Enabled=false;
					Session.Remove("BillPrint");
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			string strSendSerial=this.txtSendSerial.Text.Trim();
			
			if(strSendSerial=="")
			{
				this.SetErrorMsgPageBydirHistory("出货单号不能为空！");
				return;
			}

			DataTable dtSendLog=new DataTable("dtSendLog");
			dtSendLog.Columns.Add("cnnSendSerialNo");
			DataRow dr=dtSendLog.NewRow();
			dr["cnnSendSerialNo"]=strSendSerial;
			dtSendLog.Rows.Add(dr);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetBillOfReceiveSendPrint(strSendSerial);
				if(dtout==null)
				{
					this.btnPrint.Enabled=false;
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					dtout.TableName="dtSendDetail";
					Session.Remove("BillPrint");
					DataSet dsout=new DataSet("领料发货单");
					dsout.Tables.Add(dtout.Copy());
					dsout.Tables.Add(dtSendLog);
					Session["BillPrint"]=dsout;
					this.btnPrint.Enabled=true;
				}

				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmCommPrint.aspx?type=ReceiveSendOutBill");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmBillOfReceive.aspx");
		}
	}
}
