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
	/// wfmInventoryMove ��ժҪ˵����
	/// </summary>
	public class wfmInventoryMove : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
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
				}
				else
				{
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
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
			string strsql="select cnnRdID as �����ʶ,cnvcCode as ��������,cnvcRdCode as ��������,cnvcDepID as ����,cnvcWhCode as �ֿ�,cnvcMaker as �Ƶ���,cndARVDate as ������������,";
			strsql+="cnvcState as ״̬ from tbRdRecord where cnvcCode like 'MOV%' and cndARVDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.txtCode.Text.Trim()!="")
				strsql+=" and cnvcCode='"+this.txtCode.Text.Trim()+"'";
			else
			{
				strsql+=" and cnvcCode in(select cnvcCode from tbRdRecord where cnvcDepID='"+this.ddlDept.SelectedValue+"')";
			}
			strsql+=" order by cnvcCode,cnnRdID";

			DataTable dtout=Helper.Query(strsql);
			dtout.Columns.Add("��������");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["��������"]="<a href='wfmInventoryMoveAdd.aspx?rdid=" + dtout.Rows[i]["�����ʶ"].ToString() +"'>����</a>";
			}
			dtout.Columns.Add("������ϸ");
			for(int i=0;i<dtout.Rows.Count;i++)
			{
				dtout.Rows[i]["������ϸ"]="<a href='wfmInventoryMoveDetail.aspx?rdid=" + dtout.Rows[i]["�����ʶ"].ToString()+"&code="+dtout.Rows[i]["��������"].ToString()+"&dept="+dtout.Rows[i]["����"].ToString()+"&whid="+dtout.Rows[i]["�ֿ�"].ToString()+"'>��ϸ</a>";
			}
			this.TableConvert(dtout,"��������","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdType'");
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"״̬","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='RdState'");
			dtout.TableName="���ŵ������б�";
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
				Response.Redirect("wfmInventoryMoveAdd.aspx");	
			}
		}
	}
}
