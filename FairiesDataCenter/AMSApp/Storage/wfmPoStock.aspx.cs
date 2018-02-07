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
	/// wfmPoStock ��ժҪ˵����
	/// </summary>
	public class wfmPoStock : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtPoID;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlPoState;

		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtCycle;
		protected ucPageView UcPageView1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				string strpoid=Request.QueryString["poid"];
				if (!IsPostBack )
				{
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.FillDropDownList("tbNameCodeToStorage",this.ddlPoState,"vcCommSign='PoState'","ȫ��");
					this.FillDropDownList("Provider",this.ddlProvider,"","ȫ��");
					if(strpoid!=null||strpoid!="")
						this.txtPoID.Text=strpoid;
					this.DBBind("0");
					Session.Remove("poid");
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind(string strstate)
		{
			string strPoID=this.txtPoID.Text.Trim();
			string strProvider=this.ddlProvider.SelectedValue;
			string strCycle=this.txtCycle.Text.Trim();
			string strPoState=this.ddlPoState.SelectedValue;
			if(strstate=="")
			{	
				if(strPoState=="ȫ��")
					strPoState="";
			}
			else
			{
				strPoState=strstate;
			}

			if(strProvider=="ȫ��")
				strProvider="";

			Hashtable htPara=new Hashtable();
			htPara.Add("strPoID",strPoID);
			htPara.Add("strProvider",strProvider);
			htPara.Add("strCycle",strCycle);
			htPara.Add("strPoState",strPoState);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetPoStockMain(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				else
				{
					this.TableConvert(dtout,"��Ӧ�̱���","Provider");
					this.TableConvert(dtout,"����״̬","tbNameCodeToStorage","vcCommSign='PoState'");
					dtout.TableName="�����б�";
					DataTable dtexcel=dtout.Copy();
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

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind("");
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmPoStockAdd.aspx");
		}
	}
}
