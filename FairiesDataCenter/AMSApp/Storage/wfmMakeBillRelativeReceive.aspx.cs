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
	/// Summary description for wfmMakeBillRelativeReceive.
	/// </summary>
	public class wfmMakeBillRelativeReceive :wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnRelativeMakeToReceive;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnReturn;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!this.IsPostBack)
			{
				this.btnRelativeMakeToReceive.Enabled=false;
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
			this.btnRelativeMakeToReceive.Click += new System.EventHandler(this.btnRelativeMakeToReceive_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.BindData();
		}

		private void BindData()
		{
			Session.Remove("Query");
			Session.Remove("ToReceive");
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetMakeBillNoRelative();
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					Session["ToReceive"]=dtout.Copy();
					this.TableConvert(dtout,"cnvcProduceDeptID","NewDept");
					this.TableConvert(dtout,"cnvcGroup","tbNameCodeToStorage","vcCommSign='GROUP'");
					dtout.TableName="制令领料单";
					if(dtout.Rows.Count>0)
					{
						this.btnRelativeMakeToReceive.Enabled=true;
					}
				}
				
				Session["Query"]=dtout;
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

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.DataGrid1.DataSource = (DataTable)Session["Query"];
			this.DataGrid1.DataBind();
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmBillOfReceive.aspx");
		}

		private void btnRelativeMakeToReceive_Click(object sender, System.EventArgs e)
		{
			if(Session["ToReceive"]!=null)
			{
				DataTable dtIn=(DataTable)Session["ToReceive"];
				if(dtIn.Rows.Count>0)
				{
					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					StoBusi=new BusiComm.StorageBusi(strcons);
					try
					{
						if(!StoBusi.RelativeMakeToReceive(dtIn))
						{
							this.SetErrorMsgPageBydir("关联错误，请重试！");
							return;
						}
						else
						{
							Session.Remove("ToReceive");
							this.SetSuccMsgPageBydir("关联成功！","Storage/wfmMakeBillRelativeReceive.aspx");
							return;
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("关联错误，请重试！");
						return;
					}
				}
			}
		}
	}
}
