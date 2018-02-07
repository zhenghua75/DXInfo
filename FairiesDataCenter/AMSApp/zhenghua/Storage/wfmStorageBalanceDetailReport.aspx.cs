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
	/// wfmStorageBalanceDetailReport ��ժҪ˵����
	/// </summary>
	public class wfmStorageBalanceDetailReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Button btExcel;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// �ڴ˴������û������Գ�ʼ��ҳ��
			string strWhCode=Request.QueryString["whcode"];
			string strInvCode=Request.QueryString["Invcode"];
			string strInvName=Request.QueryString["Invname"];
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				this.FillDropDownList("Warehouse",this.ddlWhouse,"vcCommCode='"+strWhCode+"'");
				this.txtInvCode.Text=strInvCode;
				this.txtInvName.Text=strInvName;
				this.ddlWhouse.Enabled=false;
				this.txtInvCode.Enabled=false;
				this.txtInvName.Enabled=false;
				int line1=strBeginDate.IndexOf("-",0);
				int line2=strBeginDate.IndexOf("-",line1+1);
				string strshortBeginDate=strBeginDate.Substring(0,4);
				if(int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1)))<10)
					strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
				else
					strshortBeginDate+=int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
				if(int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1)))<10)
					strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();
				else
					strshortBeginDate+=int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();

				line1=strEndDate.IndexOf("-",0);
				line2=strEndDate.IndexOf("-",line1+1);
				string strshortEndDate=strEndDate.Substring(0,4);
				if(int.Parse(strEndDate.Substring(line1+1,line2-(line1+1)))<10)
					strshortEndDate+="0"+int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
				else
					strshortEndDate+=int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
				if(int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1)))<10)
					strshortEndDate+="0"+int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
				else
					strshortEndDate+=int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();

				this.DBBind(strshortBeginDate,strshortEndDate);
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
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind(string strbegin,string strend)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strsql="";
			string strProType=Helper.Query("select cnvcProductType from tbProductClass where cnvcProductClassCode=(select cnvcInvCCode from tbInventory where cnvcInvCode='"+this.txtInvCode.Text.Trim()+"')").Rows[0][0].ToString();
			if(strProType=="FINALPRODUCT")
			{
				strsql="select a.cndDate as ����,a.cnvcWhCode as �ֿ�,a.cnvcInvCode as �������,c.cnvcInvName as �������,b.cnvcComunitName as ��λ,a.cnnQuantity as �̵���,";
				strsql+="a.cnnCheckEnter as �̵����,a.cnnPoEnter as �ɹ����,a.cnnPoReturn as �ɹ��˻�,a.cnnProductUse as ����ʹ��,a.cnnProductLost as �������,";
				strsql+="a.cnnProductEnter as �������,a.cnnAssgOut as �ֻ�����,a.cnnAssgEnter as �ֻ����,a.cnnMoveEnter as �������,a.cnnMoveOut as ��������,";
				strsql+="a.cnnTranLost as �����������,a.cnnConsCount as ����,a.cnnConsLost as �������,a.cnnExpLost as �������,cnnProDiff as ����";
				strsql+=" from tbStorageBalanceRelationDetail a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode and b.cnvcComunitCode=c.cnvcSTComunitCode";
				strsql+=" and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='FINALPRODUCT')";
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"' and a.cndDate between '"+strbegin+"' and '"+strend+"'";
			}
			else
			{
				strsql="select a.cndDate as ����,a.cnvcWhCode as �ֿ�,a.cnvcInvCode as �������,c.cnvcInvName as �������,b.cnvcComunitName as ��λ,a.cnnQuantity as �̵���,";
				strsql+="a.cnnCheckEnter as �̵����,a.cnnPoEnter as �ɹ����,a.cnnPoReturn as �ɹ��˻�,a.cnnProductUse as ����ʹ��,a.cnnProductLost as �������,";
				strsql+="a.cnnMoveEnter as �������,a.cnnMoveOut as ��������,a.cnnTranLost as �����������,a.cnnConsCount as ����,a.cnnConsLost as �������,";
				strsql+="a.cnnExpLost as �������,cnnMatDiff  as ����";
				strsql+=" from tbStorageBalanceRelationDetail a,tbComputationUnit b,tbInventory c where a.cnvcInvCode=c.cnvcInvCode ";
				strsql+=" and b.cnvcComunitCode=c.cnvcSTComunitCode and c.cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType<>'FINALPRODUCT')";
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"' and a.cndDate between '"+strbegin+"' and '"+strend+"'";
			}
			strsql+=" order by a.cndDate,a.cnvcInvCode,a.cnvcWhCode";

			DataTable dtbalance=Helper.Query(strsql);
			this.TableConvert(dtbalance,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			dtbalance.TableName="���ƽ���ϵ��ϸ����";
			Session["QUERY"] = dtbalance;
			Session["toExcel"]=dtbalance;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtbalance);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			int line1=strBeginDate.IndexOf("-",0);
			int line2=strBeginDate.IndexOf("-",line1+1);
			string strshortBeginDate=strBeginDate.Substring(0,4);
			if(int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1)))<10)
				strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
			else
				strshortBeginDate+=int.Parse(strBeginDate.Substring(line1+1,line2-(line1+1))).ToString();
			if(int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1)))<10)
				strshortBeginDate+="0"+int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();
			else
				strshortBeginDate+=int.Parse(strBeginDate.Substring(line2+1,strBeginDate.Length-(line2+1))).ToString();

			line1=strEndDate.IndexOf("-",0);
			line2=strEndDate.IndexOf("-",line1+1);
			string strshortEndDate=strEndDate.Substring(0,4);
			if(int.Parse(strEndDate.Substring(line1+1,line2-(line1+1)))<10)
				strshortEndDate+="0"+int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
			else
				strshortEndDate+=int.Parse(strEndDate.Substring(line1+1,line2-(line1+1))).ToString();
			if(int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1)))<10)
				strshortEndDate+="0"+int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
			else
				strshortEndDate+=int.Parse(strEndDate.Substring(line2+1,strEndDate.Length-(line2+1))).ToString();
			this.DBBind(strshortBeginDate,strshortEndDate);
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmStorageBalanceReport.aspx");
		}
	}
}
