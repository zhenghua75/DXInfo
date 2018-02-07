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
	/// wfmDeptInventoryLost ��ժҪ˵����
	/// </summary>
	public class wfmDeptInventoryLost : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label2;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
				strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",this.ddlDept);
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			string strsql="select a.cnnLostSerialNo as �����ˮ,b.cnvcInvName as ��Ĵ��,a.cnvcComUnitCode as ��λ,a.cnnLostCount as �������,a.cnnAddCount as ������,a.cnnReduceCount as ������,convert(char(10),a.cndLostDate,120) as �������,a.cnvcDeptID as ������,";
			strsql+="a.cnvcWhCode as ����ֿ�,a.cndMdate as ��������,a.cndExpDate as ��������,a.cnvcOperID as ����Ա,a.cndOperDate as ����ʱ��,a.cnvcComments as ��ע,(case a.cnvcInvalidFlag when '0' then 'δȷ��' else '��ȷ��' end) as ȷ�ϱ�־";
			strsql+=" from tbLostSerial a,tbInventory b where a.cnvcLostType='1' and a.cnvcInvCode=b.cnvcInvCode and a.cndLostDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlDept.SelectedValue!="")
				strsql+=" and a.cnvcDeptID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhouse.SelectedValue!="")
				strsql+=" and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			strsql+=" order by a.cnnLostSerialNo";

			DataTable dtout=Helper.Query(strsql);
			dtout.Columns.Add("����");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				if(dtout.Rows[i]["ȷ�ϱ�־"].ToString()=="δȷ��")
					dtout.Rows[i]["����"]="<a href='wfmDeptInventoryLostDetail.aspx?seno=" + dtout.Rows[i]["�����ˮ"].ToString() +"'>����</a>";
			}
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"������","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"����ֿ�","Warehouse","vcCommCode","vcCommName");
			dtout.TableName="���۱����б�";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if(this.ddlDept.SelectedValue=="")
			{
				this.Popup("�޲��Ųֿ���Ϣ�����ܲ�����");
				return;
			}
			else
			{
				Response.Redirect("wfmDeptInventoryLostDetail.aspx");	
			}
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}
	}
}
