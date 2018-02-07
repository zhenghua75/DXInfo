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
	/// Summary description for wfmBillOfEnterStorage.
	/// </summary>
	public class wfmBillOfEnterStorage : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.Button btnAdd;

		protected string strEndDate;
		protected ucPageView UcPageView1;
		protected string strBeginDate;
		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("Provider", this.ddlProvider,"","ȫ��");
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}

			string strProviderID=this.ddlProvider.SelectedValue;
			if(strProviderID=="")
			{
				this.SetErrorMsgPageBydir("��Ӧ�̲���Ϊ�գ�");
				return;
			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strProviderID",strProviderID);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetBillOfEnterStorage(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				else
				{
					dtout.TableName="���ֵ�";
					Session["QUERY"] = dtout;
				}
				
				UcPageView1.MyDataGrid.PageSize = 20;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1.strDeptID=="FYZX1"||ls1.strDeptID=="CEN00")
			{
				Response.Redirect("wfmBillOfEnterStorageDetail.aspx");
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("ԭ���Ͻ��ֹ���ֻ���������Ĳ���Ա����ʹ�ã�");
				return;
			}
		}
	}
}
