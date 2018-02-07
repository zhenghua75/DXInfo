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
	/// wfmDeptOperManage ��ժҪ˵����
	/// </summary>
	public class wfmDeptOperManage : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.TextBox txtOperName;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnExcel;

		protected ucPageView UcPageView1;
		protected string strExcelPath = string.Empty;
		BusiComm.Manager m1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.btnExcel.Enabled=false;
					this.FillDropDownList("AllMD", ddlDept,"vcCommSign='MD' and vcCommCode not in('CEN00','FYZX1')","ȫ��");
					if(ls1.strDeptID!="CEN00"&&ls1.strDeptID!="FYZX1")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
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

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);

			Hashtable htPara=new Hashtable();
			string strOperName=this.txtOperName.Text.Trim();
			htPara.Add("strOperName",strOperName);
			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID=="ȫ��")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			htPara.Add("strLoginID",ls1.strLoginID);
			
			try
			{
				DataTable dtout=m1.GetClientOper(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"�ŵ�","AllMD");
					this.TableConvert(dtout,"Ȩ��","tbCommCode","vcCommSign='LM'");
					dtout.TableName="�ͻ��˲���Ա�嵥";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					dtexcel.Columns.Remove("����");
					Session["toExcel"]=dtexcel;

					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						btnExcel.Enabled=true;	
					}
				}

				UcPageView1.MyDataGrid.PageSize = 30;
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
			this.RedirectPage("wfmClientOperDetail.aspx");
		}
	}
}
