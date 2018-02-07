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
	/// wfmInventoryMoveReport ��ժҪ˵����
	/// </summary>
	public class wfmInventoryMoveReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtInvName;
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
				this.FillDropDownList("NewDept",this.ddlDept,"","ȫ��");
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
			string strsql="select t.cnvcCode as ��������,t.cnvcDepID as ��������,t.cnvcWhCode as �����ֿ�,n.cnvcInvName as ���,t.cnvcComunitCode as ��λ,sum(t.cnnQuantity) as ��������,convert(char(10),t.cndARVDate,120) as ��������,";
			strsql+="t.cnvcState as ����״̬,w.cnvcDepID as ���벿��,w.cnvcWhCode as ����ֿ�,sum(w.cnnQuantity) as ��������,convert(char(10),w.cndARVDate,120) as ��������,w.cnvcState as ����״̬";
			strsql+=" from (select a.cnvcCode,a.cnvcDepID,a.cnvcWhCode,b.cnvcInvCode,b.cnvcComunitCode,sum(b.cnnQuantity) as cnnQuantity,a.cndARVDate,a.cnvcState from tbRdRecord a,tbRdRecordDetail b";
			strsql+=" where a.cnvcRdCode='RD005' and a.cnnRdID=b.cnnRdID and a.cnvcState='2' group by a.cnvcCode,a.cnvcDepID,a.cnvcWhCode,b.cnvcInvCode,b.cnvcComunitCode,a.cndARVDate,a.cnvcState ) t,(select a.cnvcCode,a.cnvcDepID,a.cnvcWhCode,b.cnvcInvCode,b.cnvcComunitCode,sum(b.cnnQuantity) as cnnQuantity,a.cndARVDate,a.cnvcState";
			strsql+=" from tbRdRecord a,tbRdRecordDetail b where a.cnvcRdCode='RD006' and a.cnnRdID=b.cnnRdID group by a.cnvcCode,a.cnvcDepID,a.cnvcWhCode,b.cnvcInvCode,b.cnvcComunitCode,a.cndARVDate,a.cnvcState) w,tbInventory n where t.cnvcCode=w.cnvcCode and t.cnvcInvCode=w.cnvcInvCode and t.cnvcInvCode=n.cnvcInvCode";
			strsql+=" and (t.cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59' or w.cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59')";
			if(this.ddlDept.SelectedValue!="ȫ��")
				strsql+=" and (t.cnvcDepID='"+this.ddlDept.SelectedValue+"' or w.cnvcDepID='"+this.ddlDept.SelectedValue+"')";
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and n.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" group by t.cnvcCode,t.cnvcDepID,t.cnvcWhCode,n.cnvcInvName,t.cnvcComunitCode,convert(char(10),t.cndARVDate,120),t.cnvcState,w.cnvcDepID,w.cnvcWhCode,convert(char(10),w.cndARVDate,120),w.cnvcState order by t.cnvcCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"��������","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�����ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"����״̬","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			this.TableConvert(dtout,"���벿��","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"����ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"����״̬","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			dtout.TableName="��������";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
