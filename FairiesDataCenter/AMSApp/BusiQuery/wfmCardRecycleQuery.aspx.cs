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

namespace AMSApp.BusiQuery
{
	/// <summary>
	/// wfmCardRecycleQuery ��ժҪ˵����
	/// </summary>
	public partial class wfmCardRecycleQuery : wfmBase
	{

		protected string strExcelPath = string.Empty;
		protected ucPageView UcPageView1;
		BusiComm.BusiQuery busiq;
		protected string strBeginDate;
		protected string strEndDate;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'","ȫ��");
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					Session.Remove("QUERY");
					Session.Remove("toExcel");
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

		}
		#endregion

		protected void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			busiq=new BusiComm.BusiQuery(strcons);

			Hashtable htPara=new Hashtable();
			string strCardID=txtCardID.Text.Trim();
			htPara.Add("strCardID",strCardID);
			string strAssName=txtAssName.Text.Trim();
			htPara.Add("strAssName",strAssName);
			string strLinkPhone=txtLinkPhone.Text.Trim();
			htPara.Add("strLinkPhone",strLinkPhone);
			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID=="ȫ��")
			{
				strDeptID="";
			}
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			
			try
			{
				DataTable dtout=busiq.GetCardRecycle(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					btnExcel.Enabled=false;
					return;
				}
				else
				{
					this.TableConvert(dtout,"��Ա����","tbCommCode","vcCommSign='AT'");
					this.TableConvert(dtout,"��Ա״̬","tbCommCode","vcCommSign='AS'");
					this.TableConvert(dtout,"�ŵ�","tbCommCode","vcCommSign='MD'");
					dtout.TableName="��Ա�������嵥";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					for(int i=0;i<dtexcel.Rows.Count;i++)
					{
						if(this.JudgeIsNum(dtexcel.Rows[i][0].ToString().Substring(0,1)))
						{
							dtexcel.Rows[i][0]="'"+dtexcel.Rows[i][0].ToString();
						}
						dtexcel.Rows[i][2]="'"+dtexcel.Rows[i][2].ToString();
						dtexcel.Rows[i][10]="'"+dtexcel.Rows[i][10].ToString();
					}
					Session["toExcel"]=dtexcel;
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						if(ls1.strLimit=="CL001")
						{
							btnExcel.Enabled=true;
						}
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
	}
}
