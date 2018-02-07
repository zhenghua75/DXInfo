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
using System.Text.RegularExpressions;
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmBillOfReceiveSend.
	/// </summary>
	public class wfmBillOfReceiveSend : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnSendOK;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.DataGrid DataGrid2;
		protected System.Web.UI.WebControls.DataGrid DataGrid3;
		protected System.Web.UI.WebControls.Button btnPrint;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnSendOK.Attributes.Add("onclick","return confirm('请确认你已经正确填写了各门店的\"发货量\"，并已将这批领料单发货。\\n\\n确认发货？');");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					Session.Remove("QUERY");
					Session.Remove("SendReceiveID");
					Session.Remove("SendReceiveDetail");
					DataTable dtSendReceiveID=new DataTable();
					dtSendReceiveID.Columns.Add("cnnReceiveSerialNo");
					dtSendReceiveID.Columns.Add("cnvcReceiveType");
					DataTable dtSendReceiveDetail=new DataTable();
					Session["SendReceiveID"]=dtSendReceiveID;
					Session["SendReceiveDetail"]=dtSendReceiveDetail;
					this.btnPrint.Enabled=false;
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
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.DataGrid1.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_ItemCommand);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.btnSendOK.Click += new System.EventHandler(this.btnSendOK_Click);
			this.DataGrid2.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid2_DeleteCommand);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Query");
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetBillOfReceiveNoSend();
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"cnvcReceiveDeptID","NewDept");
					this.TableConvert(dtout,"cnvcGroup","tbNameCodeToStorage","vcCommSign='GROUP'");
					this.TableConvert(dtout,"cnvcBillState","tbNameCodeToStorage","vcCommSign='RECEIVE_OS'");
					dtout.TableName="领料单";
				}
				
				Session["Query"]=dtout;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();

				this.DataGrid2.DataSource = (DataTable)Session["SendReceiveID"];
				this.DataGrid2.DataBind();

				this.DataGrid3.DataSource = (DataTable)Session["SendReceiveDetail"];
				this.DataGrid3.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmBillOfReceive.aspx");
		}

		private void btnSendOK_Click(object sender, System.EventArgs e)
		{
			DataTable dtSendReceiveID=(DataTable)Session["SendReceiveID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				string strOperDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
				if(StoBusi.UpdateBatchBillOfReceiveSend(dtSendReceiveID,ls1.strOperName,strOperDate))
				{
					this.SetSuccMsgPageBydir("领料单发货成功！","Storage/wfmBillOfReceive.aspx");
				}
				else
				{
					this.SetErrorMsgPageBydir("领料单发货时发生错误，请重试！");
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
			}
		}

		private void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName=="Select")
			{
				string strReceiveID=e.Item.Cells[0].Text;
				string strReceiveType=e.Item.Cells[8].Text;
				bool dupflag=false;
				DataTable dtSendReceiveID=(DataTable)Session["SendReceiveID"];
				for(int s=0;s<dtSendReceiveID.Rows.Count;s++)
				{
					if(dtSendReceiveID.Rows[s]["cnnReceiveSerialNo"].ToString()==strReceiveID)
					{
						dupflag=true;
						break;
					}
				}
				if(dupflag)
				{
					this.SetErrorMsgPageBydirHistory("此领料单已经添加到发货列表！");
					return;
				}
				else
				{
					DataRow drnew=dtSendReceiveID.NewRow();
					drnew["cnnReceiveSerialNo"]=strReceiveID;
					drnew["cnvcReceiveType"]=strReceiveType;
					dtSendReceiveID.Rows.Add(drnew);
					Session.Remove("SendReceiveID");
					Session["SendReceiveID"]=dtSendReceiveID;

					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					StoBusi=new BusiComm.StorageBusi(strcons);
					try
					{
						DataTable dtout=StoBusi.GetBillOfReceiveTempDetail(dtSendReceiveID);
						if(dtout==null)
						{
							this.SetErrorMsgPageBydir("查询出错，请重试！");
							return;
						}
						else
						{
							dtout.TableName="领料单";
							Session.Remove("SendReceiveDetail");
							Session["SendReceiveDetail"]=dtout;
							if(dtout.Rows.Count>0)
							{
								this.btnPrint.Enabled=true;
							}
							else
							{
								this.btnPrint.Enabled=false;
							}
						}

						this.DataGrid2.DataSource = (DataTable)Session["SendReceiveID"];
						this.DataGrid2.DataBind();

						this.DataGrid3.DataSource = (DataTable)Session["SendReceiveDetail"];
						this.DataGrid3.DataBind();
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
						return;
					}
				}
			}
		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.DataGrid1.DataSource = (DataTable)Session["Query"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid2_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName=="Delete")
			{
				string strReceiveID=e.Item.Cells[0].Text;
				DataTable dtSendReceiveID=(DataTable)Session["SendReceiveID"];
				for(int s=0;s<dtSendReceiveID.Rows.Count;s++)
				{
					if(dtSendReceiveID.Rows[s]["cnnReceiveSerialNo"].ToString()==strReceiveID)
					{
						dtSendReceiveID.Rows[s].Delete();
						break;
					}
				}

				Session.Remove("SendReceiveID");
				Session["SendReceiveID"]=dtSendReceiveID;

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					DataTable dtout=StoBusi.GetBillOfReceiveTempDetail(dtSendReceiveID);
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						return;
					}
					else
					{
						dtout.TableName="领料单";
						Session.Remove("SendReceiveDetail");
						Session["SendReceiveDetail"]=dtout;
						if(dtout.Rows.Count>0)
						{
							this.btnPrint.Enabled=true;
						}
						else
						{
							this.btnPrint.Enabled=false;
						}
					}

					this.DataGrid2.DataSource = (DataTable)Session["SendReceiveID"];
					this.DataGrid2.DataBind();

					this.DataGrid3.DataSource = (DataTable)Session["SendReceiveDetail"];
					this.DataGrid3.DataBind();
				}
				catch(Exception er)
				{
					this.clog.WriteLine(er);
					this.SetErrorMsgPageBydir("查询错误，请重试！");
					return;
				}
			}
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			DataTable dtout=((DataTable)Session["SendReceiveDetail"]).Copy();
			dtout.TableName="dtSendDetail";
			Session.Remove("BillPrint");
			DataSet dsout=new DataSet("领料发货单--预发单");
			dsout.Tables.Add(dtout);

			dtout=new DataTable("dtSendLog");
			dtout.Columns.Add("cnnSendSerialNo");
			DataRow dr=dtout.NewRow();
			dr["cnnSendSerialNo"]="";
			dtout.Rows.Add(dr);

			dsout.Tables.Add(dtout);
			Session["BillPrint"]=dsout;

			Response.Redirect("wfmCommPrint.aspx?type=ReceiveSendOutBill");
		}
	}
}
