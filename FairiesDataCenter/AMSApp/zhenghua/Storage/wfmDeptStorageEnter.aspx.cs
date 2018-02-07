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
	/// wfmDeptStorageEnter ��ժҪ˵����
	/// </summary>
	public class wfmDeptStorageEnter : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtEnterID;
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
				this.ddlState.Items.Add(new ListItem("δ���","0"));
				this.ddlState.Items.Add(new ListItem("�����","1"));
				this.ddlState.SelectedIndex=0;
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
			string strsql="select cnnRdID as �����ʶ,cnvcCode as �ֻ���ⵥ��,cnvcDepID as ����,cnvcWhCode as �ֿ�,cnvcMaker as �Ƶ���,cndARVDate as �������,";
			strsql+="cnvcState as ״̬ from tbRdRecord where cnvcRdCode='RD007'";
			strsql+=" and cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.txtEnterID.Text.Trim()!="")
				strsql+=" and cnvcCode='"+this.txtEnterID.Text.Trim()+"'";
			if(this.ddlDept.SelectedValue!="ȫ��")
				strsql+=" and cnvcDepID='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhouse.SelectedValue!="ȫ��")
				strsql+=" and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			if(this.ddlState.SelectedValue!="")
				strsql+=" and cnvcState='"+this.ddlState.SelectedValue+"'";
			strsql+=" order by cnvcCode";

			DataTable dtout=Helper.Query(strsql);
			dtout.Columns.Add("��������");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["��������"]="<a href='wfmDeptStorageEnterAdd.aspx?rdid=" + dtout.Rows[i]["�����ʶ"].ToString() +"'>����</a>";
			}
			dtout.Columns.Add("������ϸ");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["������ϸ"]="<a href='wfmDeptStorageEnterDetail.aspx?rdid=" + dtout.Rows[i]["�����ʶ"].ToString()+"&code="+dtout.Rows[i]["�ֻ���ⵥ��"].ToString()+"&dept="+dtout.Rows[i]["����"].ToString()+"&whid="+dtout.Rows[i]["�ֿ�"].ToString()+"'>��ϸ</a>";
			}
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"״̬","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			dtout.TableName="�ֻ���ⵥ�б�";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmDeptStorageEnterAdd.aspx");
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
