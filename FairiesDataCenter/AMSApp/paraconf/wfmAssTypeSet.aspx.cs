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
using BusiComm;
using CommCenter;

namespace AMSApp.paraconf
{
	/// <summary>
	/// wfmAssTypeSet ��ժҪ˵����
	/// </summary>
	public partial class wfmAssTypeSet : wfmBase
	{
		BusiComm.Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.ddlDisp.Items.Add("��ѡ��");
					this.ddlDisp.Items.Add("��");
					this.ddlDisp.Items.Add("��");
					this.ddlDisp.SelectedIndex=0;
					Session.Remove("QUERY");
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

		private void DGBind()
		{
			Session.Remove("Query");
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			try
			{
				DataTable dtout=m1.GetAssType();
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				
				Session["Query"]=dtout;
				this.DataGrid1.EditItemIndex=-1;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
				((Button)this.DataGrid1.Items[0].Cells[4].Controls[0]).Visible=false;
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			if(this.txtAssTypeName.Text.Trim().Length>=10)
			{
				this.Popup("��Ա�������Ʋ��ܳ���5�������֣�");
				return;
			}
			if(this.txtAssTypeCode.Text.Trim().Length!=5)
			{
				this.Popup("��Ա���ͱ������Ϊ5���ַ���");
				return;
			}
			else if(this.txtAssTypeCode.Text.Trim().Substring(0,2)!="AT")
			{
				this.Popup("��Ա���ͱ�������롰AT����ͷ��");
				return;
			}
			else if(m1.GetAssTypeExist(this.txtAssTypeCode.Text.Trim()))
			{
				this.Popup("��Ա���ͱ����Ѿ����ڣ�");
				return;
			}
			if(this.txtAssTypeRate.Text.Trim()=="")
			{
				this.Popup("�ۿ۲���Ϊ�գ�");
				return;
			}
			else if(!this.JudgeIsNum(this.txtAssTypeRate.Text.Trim(),"�ۿ۱��������֣���"))
				return;
			else if(this.txtAssTypeRate.Text.Trim()=="1"||this.txtAssTypeRate.Text.Trim()=="10")
			{
				this.Popup("�ۿ۱�����0��10���ڵ����֣��Ҳ���Ϊ1��10��");
				return;
			}
			else if(double.Parse(this.txtAssTypeRate.Text.Trim())<0||double.Parse(this.txtAssTypeRate.Text.Trim())>=10)
			{
				this.Popup("�ۿ۱�����0��10���ڵ����֣��Ҳ���Ϊ1��10��");
				return;
			}

			if(this.ddlDisp.SelectedIndex==0)
			{
				this.Popup("��ѡ���Ƿ����Σ�");
				return;
			}

			string strComments="";
			if(this.ddlDisp.SelectedIndex==1)
				strComments="X"+this.txtAssTypeRate.Text.Trim();
			else
				strComments="Y"+this.txtAssTypeRate.Text.Trim();
			try
			{
				if(!m1.ModifyAssType(this.txtAssTypeCode.Text.Trim(),this.txtAssTypeName.Text.Trim(),strComments,"ADD"))
				{
					this.SetErrorMsgPageBydir("��ӻ�Ա���ͳ��������ԣ�");
					return;
				}
				else
				{
					this.Popup("��ӻ�Ա���ͳɹ���");
					this.DGBind();
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("ϵͳ���������ԣ�");
				return;
			}
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DGBind();
		}

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			string strdis=((Label)this.DataGrid1.Items[e.Item.ItemIndex].Cells[3].FindControl("lbldis")).Text.Trim();
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
			DropDownList ddldis=(DropDownList)this.DataGrid1.Items[e.Item.ItemIndex].Cells[3].FindControl("dgddlDisp");
			ddldis.Items.Add("��");
			ddldis.Items.Add("��");
			ddldis.SelectedIndex=ddldis.Items.IndexOf(ddldis.Items.FindByText(strdis));
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string strAssTypeName=((TextBox)e.Item.Cells[0].Controls[0]).Text.Trim();
			string strAssTypeCode=e.Item.Cells[1].Text.Trim();
			string strAssTypeRate=((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
			string strComments=((DropDownList)e.Item.Cells[3].Controls[1]).SelectedItem.Text.Trim();
			if(strAssTypeName.Length>=10)
			{
				this.Popup("��Ա�������Ʋ��ܳ���5�������֣�");
				return;
			}
			if(strAssTypeRate=="")
			{
				this.Popup("�ۿ۲���Ϊ�գ�");
				return;
			}
			else if(!this.JudgeIsNum(strAssTypeRate,"�ۿ۱��������֣���"))
				return;
			else if(strAssTypeRate=="1"||strAssTypeRate=="10")
			{
				this.Popup("�ۿ۱�����0��10���ڵ����֣��Ҳ���Ϊ1��10��");
				return;
			}
			else if(double.Parse(strAssTypeRate)<0||double.Parse(strAssTypeRate)>=10)
			{
				this.Popup("�ۿ۱�����0��10���ڵ����֣��Ҳ���Ϊ1��10��");
				return;
			}

			if(strComments=="��")
				strComments="X"+strAssTypeRate;
			else
				strComments="Y"+strAssTypeRate;

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			try
			{
				if(!m1.ModifyAssType(strAssTypeCode,strAssTypeName,strComments,"MOD"))
				{
					this.SetErrorMsgPageBydir("�޸Ļ�Ա���ͳ��������ԣ�");
					return;
				}
				else
				{
					this.Popup("�޸Ļ�Ա���ͳɹ���");
					this.DGBind();
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("ϵͳ���������ԣ�");
				return;
			}
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}
	}
}
