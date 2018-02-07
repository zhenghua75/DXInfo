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
	/// wfmPoStockAdd ��ժҪ˵����
	/// </summary>
	public class wfmPoStockAdd : wfmBase
	{
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtAddress;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.TextBox txtPoID;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtPlanCycle;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.Button btreturn;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Button btChState;
	
		BusiComm.StorageBusi StoBusi;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			string strPoID=Request.QueryString["POID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);

			if(!IsPostBack)
			{
				if(strPoID==""||strPoID==null)
				{
					this.btAdd.Enabled=true;
					this.btMod.Enabled=false;
					this.FillDropDownList("Provider",this.ddlProvider);
					this.txtPoID.Enabled=false;
					this.btChState.Visible=false;
					string strCycle=DateTime.Today.Year.ToString();
					if(DateTime.Today.Month<10)
						strCycle+="0"+DateTime.Today.Month.ToString();
					else
						strCycle+=DateTime.Today.Month.ToString();
					this.txtPoID.Text="POS"+strCycle+"----";
					string strNextCycle=DateTime.Today.AddMonths(1).Year.ToString();
					if(DateTime.Today.AddMonths(1).Month<10)
						strNextCycle+="0"+DateTime.Today.AddMonths(1).Month.ToString();
					else
						strNextCycle+=DateTime.Today.AddMonths(1).Month.ToString();
					this.txtPlanCycle.Text=strNextCycle;
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.FillDropDownList("Provider",this.ddlProvider);
					CMSMStruct.PoStockMainStruct psm1=StoBusi.GetPoStockMainOne(strPoID);
					this.txtPoID.Text=psm1.strPoID;
					this.ddlProvider.SelectedIndex=this.ddlProvider.Items.IndexOf(this.ddlProvider.Items.FindByValue(psm1.strPrvdCode));
					this.txtPlanCycle.Text=psm1.strPlanCycle;
					this.txtAddress.Text=psm1.strAddress;
					this.txtComments.Text=psm1.strComments;
					Session["psmold"]=psm1;
					this.txtPoID.Enabled=false;
					this.ddlProvider.Enabled=false;
					switch(psm1.strPoState)
					{
						case "0":
							this.btChState.Text="ִ��";
							break;
						case "1":
							this.txtPlanCycle.Enabled=false;
							this.txtAddress.Enabled=false;
							this.txtComments.Enabled=false;
							this.btMod.Enabled=false;
							this.btChState.Text="���";
							break;
						case "2":
							this.txtPlanCycle.Enabled=false;
							this.txtAddress.Enabled=false;
							this.txtComments.Enabled=false;
							this.btMod.Enabled=false;
							this.btChState.Visible=false;
							break;
					}
				}
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
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btChState.Click += new System.EventHandler(this.btChState_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			CMSMStruct.PoStockMainStruct psm1=new CMSMStruct.PoStockMainStruct();
			psm1.strPoID=this.txtPoID.Text.Trim();
			psm1.strPrvdCode=this.ddlProvider.SelectedValue;
			psm1.strAddress=this.txtAddress.Text.Trim();
			psm1.strComments=this.txtComments.Text.Trim();
			psm1.strPoState="0";
			psm1.strPlanCycle=this.txtPlanCycle.Text.Trim();
			psm1.strCreater=ls1.strOperName;
			psm1.strModer="";
			psm1.strChecker="";
			psm1.strCloser="";

			if(psm1.strPoID==""||psm1.strPoID.Length!=13)
			{
				this.Popup("�ɹ������Ų���ȷ��");
				return;
			}
			if(psm1.strPrvdCode=="")
			{
				this.Popup("��Ӧ�̲���Ϊ�գ�");
				return;
			}
			if(psm1.strPlanCycle=="")
			{
				this.Popup("�ɹ����ڲ���Ϊ�գ�");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.IsExistPoProviderCycle(psm1.strPrvdCode,psm1.strPlanCycle))
				{
					this.Popup("�ù�Ӧ���ڱ��ɹ��������Ѿ��ж��������ֱ�������ж�������Ӳɹ���Ʒ��");
					return;
				}

				if(StoBusi.NewPoSotckMainAdd(psm1))
				{
					this.SetSuccMsgPageBydir("�²ɹ��ƻ�����ɹ��������һ����ӻ�Ʒ��","Storage/wfmPoStock.aspx?poid="+psm1.strPoID,"Storage/wfmPoStockDetail.aspx?POID="+psm1.strPoID+"&Prvd="+psm1.strPrvdCode+"&pos=0","��һ��");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�²ɹ���������ʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			CMSMStruct.PoStockMainStruct psm1=new CMSMStruct.PoStockMainStruct();
			psm1.strPoID=this.txtPoID.Text.Trim();
			psm1.strPrvdCode=this.ddlProvider.SelectedValue;
			psm1.strAddress=this.txtAddress.Text.Trim();
			psm1.strComments=this.txtComments.Text.Trim();
			psm1.strPlanCycle=this.txtPlanCycle.Text.Trim();
			psm1.strModer=ls1.strOperName;

			if(psm1.strPoID==""||psm1.strPoID.Length!=13)
			{
				this.Popup("�ɹ������Ų���ȷ��");
				return;
			}
			if(psm1.strPrvdCode=="")
			{
				this.Popup("��Ӧ�̲���Ϊ�գ�");
				return;
			}
			if(psm1.strPlanCycle=="")
			{
				this.Popup("�ɹ����ڲ���Ϊ�գ�");
				return;
			}

			CMSMStruct.PoStockMainStruct psmold=(CMSMStruct.PoStockMainStruct)Session["psmold"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if((psm1.strPrvdCode!=psmold.strPrvdCode||psm1.strPlanCycle!=psmold.strPlanCycle)&&StoBusi.IsExistPoProviderCycle(psm1.strPrvdCode,psm1.strPlanCycle))
				{
					this.Popup("�ù�Ӧ���ڱ��ɹ��������Ѿ��ж����������޸ģ�");
					return;
				}

				if(StoBusi.ModPoSotckMainInfo(psm1,psmold))
				{
					this.SetSuccMsgPageBydir("�޸Ĳɹ������������ݳɹ���","Storage/wfmPoStock.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�޸Ĳɹ�������������ʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

//		private void Checking()
//		{
//			string strPoID=this.txtPoID.Text.Trim();
//			Hashtable htapp=(Hashtable)Application["appconf"];
//			string strcons=(string)htapp["cons"];
//			StoBusi=new BusiComm.StorageBusi(strcons);
//			try
//			{
//				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(StoBusi.IsExistPoStockDetailStateUncheck(strPoID))
//				{
//					this.Popup("�òɹ������»���δ���ͨ�����Ӷ�������������Ӷ�����");
//					return;
//				}
//
//				if(StoBusi.PoSotckMainChecked(strPoID,ls1.strOperName))
//				{
//					this.SetSuccMsgPageBydir("�ɹ�����������˳ɹ���","Storage/wfmPoStock.aspx");
//					return;
//				}
//				else
//				{
//					this.SetErrorMsgPageBydir("�ɹ������������ʱ�������������ԣ�");
//					return;
//				}
//			}
//			catch(Exception er)
//			{
//				this.clog.WriteLine(er);
//				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
//				return;
//			}
//		}

		private void Execing()
		{
			string strPoID=this.txtPoID.Text.Trim();
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(!StoBusi.IsExistPoStockDetailStock(strPoID))
				{
					this.Popup("�ö�����û���κ���ϸ������Ӷ�����ϸ��");
					return;
				}
				if(StoBusi.PoSotckMainExecing(strPoID))
				{
					this.SetSuccMsgPageBydir("�ɹ�������ʼִ�гɹ���","Storage/wfmPoStock.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�ɹ�������ʼִ��ʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void Closed()
		{
			string strPoID=this.txtPoID.Text.Trim();
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if(StoBusi.PoSotckMainClose(strPoID,ls1.strOperName))
				{
					this.SetSuccMsgPageBydir("�ɹ�������ɳɹ���","Storage/wfmPoStock.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�ɹ��������ʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void btChState_Click(object sender, System.EventArgs e)
		{
			switch(btChState.Text.Trim())
			{
				case "ִ��":
					this.Execing();
					break;
				case "���":
					this.Closed();
					break;
			}
		}
	}
}
