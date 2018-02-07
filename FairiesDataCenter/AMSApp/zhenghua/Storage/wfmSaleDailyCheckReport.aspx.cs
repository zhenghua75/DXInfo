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
	/// wfmSaleDailyCheckReport ��ժҪ˵����
	/// </summary>
	public class wfmSaleDailyCheckReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlWhCode;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlFlag;
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
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept,"","ȫ��");
					this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","ȫ��");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				this.ddlFlag.Items.Add(new ListItem("ȫ��","ȫ��"));
				this.ddlFlag.Items.Add(new ListItem("δȷ��","0"));
				this.ddlFlag.Items.Add(new ListItem("��ȷ��","1"));
				this.ddlFlag.SelectedIndex=0;
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
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
			string strsql="select a.cnvcCheckNo as �̵����,a.cnnSerialNo as �̵���ˮ,a.cnvcDeptID as ����,a.cnvcWhCode as �ֿ�,a.cnvcInvCode as �������,b.cnvcInvName as �������,";
			strsql+="a.cnvcUnitCode as ��λ,a.cnnSysCount as ϵͳ����,a.cnnCheckCount as �̵����,convert(char(10),a.cndMdate,120) as ��������,convert(char(10),a.cndExpDate,120) as ��������,";
			strsql+="a.cnvcOperName as ����Ա,a.cndOperDate as ��������,(case a.cnvcFlag when '0' then 'δȷ��' else '��ȷ�ϸ���' end) as ȷ�ϱ�־";
			strsql+=" from tbStorageCheckLog a,tbInventory b where a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59' and a.cnvcInvCode=b.cnvcInvCode";
			if(this.ddlFlag.SelectedValue!="ȫ��")
				strsql+=" and a.cnvcFlag='"+this.ddlFlag.SelectedValue+"'";
			if(this.ddlDept.SelectedValue!="ȫ��")
				 strsql+=" and a.cnvcDeptID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhCode.SelectedValue!="ȫ��")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhCode.SelectedValue+"'";
			if(this.txtInvCode.Text.Trim()!="")
				strsql+=" and a.cnvcInvCode='"+this.txtInvCode.Text.Trim()+"'";
			if(this.txtInvName.Text.Trim()!="")
				strsql+=" and b.cnvcInvName like '%"+this.txtInvName.Text.Trim()+"%'";
			strsql+=" order by a.cnvcCheckNo,a.cnnSerialNo";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="�ֿ����̵��ѯ";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhCode,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'","ȫ��");
			}
		}
	}
}
