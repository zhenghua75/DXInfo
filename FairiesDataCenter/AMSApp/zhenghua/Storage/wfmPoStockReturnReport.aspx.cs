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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmPoStockReturnReport ��ժҪ˵����
	/// </summary>
	public class wfmPoStockReturnReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.Button btExcel;
		protected string strBeginDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				this.FillDropDownList("Provider",this.ddlProvider,"","ȫ��");
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
				strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
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
			string strsql="select a.cnvcCode as �˻�����,a.cnvcDepID as ����,a.cnvcWhCode as �ֿ�,b.cnvcPOID as ��Ӧ�ɹ�����,b.cnvcProviderID as ��Ӧ��,b.cnvcInvCode as �������,";
			strsql+="c.cnvcInvName as �������,b.cnvcComunitCode as ��λ,b.cnnQuantity as �˻�����,b.cnnPrice as ����,b.cnnCost as ���";
			strsql+=" from tbRdRecord a,tbRdRecordDetail b,tbInventory c where a.cnvcRdCode='RD002' and a.cnnRdID=b.cnnRdID and a.cnvcState='1' and b.cnvcInvCode=c.cnvcInvCode";
			strsql+=" and a.cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlProvider.SelectedValue!="ȫ��")
				strsql+=" and b.cnvcProviderID='"+this.ddlProvider.SelectedValue+"'";
			strsql+=" order by a.cnvcCode,a.cnvcDepID,a.cnvcWhCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��Ӧ��","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="�ɹ��˻�����";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}