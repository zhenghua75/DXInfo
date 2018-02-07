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
	/// wfmDeptManage ��ժҪ˵����
	/// </summary>
	public partial class wfmDeptManage : wfmBase
	{
        //protected System.Web.UI.WebControls.Label Label4;
        //protected System.Web.UI.WebControls.Label Label5;
        //protected System.Web.UI.WebControls.TextBox txtNewName;
        //protected System.Web.UI.WebControls.TextBox txtNewID;
        //protected System.Web.UI.WebControls.TextBox txtSortNum;

		Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					if(ls1.strLoginID!="admin")
					{
						this.txtClientID.Enabled=false;
						this.txtClientName.Enabled=false;
                        //this.txtNewID.Enabled=false;
                        //this.txtNewName.Enabled=false;
                        //this.txtSortNum.Enabled=false;
                        this.txtComments.Enabled = false;
						this.btnAdd.Enabled=false;
						this.DataGrid1.Columns[5].Visible=false;
					}
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
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand+=new DataGridCommandEventHandler(DataGrid1_UpdateCommand);
			this.DataGrid1.CancelCommand+=new DataGridCommandEventHandler(DataGrid1_CancelCommand);

		}
		#endregion

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DGBind();
		}

		private void DGBind()
		{
			Session.Remove("Query");
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			try
			{
				DataTable dtout=m1.GetDeptManageInfo();
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				
				Session["Query"]=dtout;
				this.DataGrid1.EditItemIndex=-1;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
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
			string strClientID=this.txtClientID.Text.Trim();
			string strClientName=this.txtClientName.Text.Trim();
            string strComments = this.txtComments.Text.Trim();
            //string strNewID=this.txtNewID.Text.Trim();
            //string strNewName=this.txtNewName.Text.Trim();
            //string strSortNum=this.txtSortNum.Text.Trim();
			if(strClientID==""&&strClientID.Length>5)
			{
				this.Popup("�ͻ��˲��ű�Ų���Ϊ����Ϊ5λ��");
				return;
			}
			if(strClientName=="")
			{
				this.Popup("�ͻ��˲������Ʋ���Ϊ�գ�");
				return;
			}
            //if(strNewID==""&&strNewID.Length>5)
            //{
            //    this.Popup("��������еĲ��ű�Ų���Ϊ����Ϊ5λ��");
            //    return;
            //}
            //if(strNewName=="")
            //{
            //    this.Popup("��������еĲ������Ʋ���Ϊ�գ�");
            //    return;
            //}
            //if(strSortNum==""||!this.JudgeIsNum(strSortNum))
            //{
            //    this.Popup("������Ų���Ϊ����Ϊ���֣�");
            //    return;
            //}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
            int DeptCount = m1.IsExsitDeptInfo(strClientID);//,strNewID);
			if(DeptCount==1)
			{
				this.Popup("���ͻ��˲��ű�š��Ѿ����ڣ����飡");
				return;
			}
			if(DeptCount==2)
			{
				this.Popup("���ͻ��˲��ű�š��Ѿ����ڣ����飡");
				return;
			}

			Hashtable htpara=new Hashtable();
			htpara.Add("strClientID",strClientID);
			htpara.Add("strClientName",strClientName);
            htpara.Add("strComments", strComments);
			//htpara.Add("strNewID",strNewID);
			//htpara.Add("strNewName",strNewName);
			//htpara.Add("strSortNum",strSortNum);
			try
			{
				if(m1.InsertDeptManageInfo(htpara))
				{
					this.Popup("����²��Ų����ɹ���");
					this.DGBind();
				}
				else
				{
					this.SetErrorMsgPageBydir("����²��Ų������������ԣ�");
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

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string strClientName=((TextBox)e.Item.Cells[0].Controls[0]).Text.Trim();
			string strClientID=e.Item.Cells[1].Text.Trim();
            string strComments = ((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
            //string strNewName=((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
            //string strNewID=e.Item.Cells[3].Text.Trim();
            //string strSortNum=((TextBox)e.Item.Cells[4].Controls[0]).Text.Trim();
			if(strClientName=="")
			{
				this.Popup("�ͻ��˲������Ʋ���Ϊ�գ�");
				return;
			}
            //if(strNewName=="")
            //{
            //    this.Popup("��������еĲ������Ʋ���Ϊ�գ�");
            //    return;
            //}
            //if(strSortNum==""||!this.JudgeIsNum(strSortNum))
            //{
            //    this.Popup("������Ų���Ϊ����Ϊ���֣�");
            //    return;
            //}
			Hashtable htpara=new Hashtable();
			htpara.Add("strClientID",strClientID);
			htpara.Add("strClientName",strClientName);
            //htpara.Add("strNewID",strNewID);
            //htpara.Add("strNewName",strNewName);
            //htpara.Add("strSortNum",strSortNum);
            htpara.Add("strComments", strComments);
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			try
			{
				if(m1.UpdateDeptManageInfo(htpara))
				{
					this.Popup("�޸Ĳ��Ų����ɹ���");
					this.DGBind();
				}
				else
				{
					this.SetErrorMsgPageBydir("�޸Ĳ��Ų������������ԣ�");
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

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

	}
}
