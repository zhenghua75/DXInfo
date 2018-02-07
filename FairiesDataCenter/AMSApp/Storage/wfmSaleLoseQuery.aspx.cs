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
	/// wfmSaleLoseQuery ��ժҪ˵����
	/// </summary>
	public class wfmSaleLoseQuery : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlLoseType;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;

		BusiComm.StorageBusi StoBusi;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected string strBeginDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept",ddlDept);
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlDept.Enabled=false;
					}
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					this.ddlLoseType.Items.Add(new ListItem("�������","�������"));
					this.ddlLoseType.Items.Add(new ListItem("���������","���������"));
					this.ddlLoseType.Items.Add(new ListItem("����ʣ�����","����ʣ�����"));
					this.ddlLoseType.SelectedIndex=0;

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
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}

			string strDeptID=this.ddlDept.SelectedValue;
			string strLoseType=this.ddlLoseType.SelectedValue;

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			htPara.Add("strLoseType",strLoseType);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetLoseDaySale(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				else
				{
					dtout.TableName="���۱����";
					Session["QUERY"] = dtout;
				}
				
				this.DataGrid1.PageSize = 20;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
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
			this.Response.Redirect("wfmSaleLoseNew.aspx");
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string strSerial=e.Item.Cells[0].Text.Trim();
			if(strSerial=="")
			{
				this.SetErrorMsgPageBydirHistory("�Ҳ���Ҫɾ�������ݣ�");
				return;
			}
			else
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					if(StoBusi.SaleLoseDelete(strSerial))
					{
						this.SetSuccMsgPageBydir("ɾ����ļ�¼�ɹ���","Storage/wfmSaleLoseQuery.aspx");
						return;
					}
					else
					{
						this.SetErrorMsgPageBydir("ɾ����ļ�¼ʱ�������������ԣ�");
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
		}

		private void DataGrid1_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem|e.Item.ItemType==ListItemType.EditItem)
			{
				TableCell tcell1=e.Item.Cells[10];
				LinkButton btnDel=(LinkButton)tcell1.Controls[0];
				btnDel.Attributes.Add("onclick","return confirm('��ȷ����Ҫɾ���˼�¼��');");
				btnDel.Text="ɾ��";
			}
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem|e.Item.ItemType==ListItemType.EditItem)
			{
				TableCell tcell1=e.Item.Cells[10];
				LinkButton btnDel=(LinkButton)tcell1.Controls[0];
				if(e.Item.Cells[9].Text.Trim()=="��ȷ��")
				{
					btnDel.Visible=false;
				}
			}
		}
	}
}
