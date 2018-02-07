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
	/// wfmDeptInventoryLostReport ��ժҪ˵����
	/// </summary>
	public class wfmDeptInventoryLostReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlWhCode;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlLostType;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected ucPageView UcPageView1;
		protected string strEndDate;
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
				this.FillDropDownList("tbNameCodeToStorage",this.ddlLostType,"vcCommSign='LostType' and vcCommCode<>'0'","ȫ��");
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
			string strsql="select t.��ص���,t.����,t.�ֿ�,t.���,t.��λ,sum(t.�������) as �������,t.���ʱ��,t.����Ա,t.�������,t.��ע";
			strsql+="  from (select '' as ��ص���,a.cnvcDeptID as ����,a.cnvcWhCode as �ֿ�,b.cnvcInvName as ���,a.cnvcComunitCode as ��λ,";
			strsql+="a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount as �������,a.cndLostDate as ���ʱ��,a.cnvcOperID as ����Ա,a.cnvcLostType as �������,a.cnvcComments as ��ע";
			strsql+=" from tbLostSerial a,tbInventory b where a.cnvcInvalidFlag='1' and cnvcLostType='1' and a.cnvcInvCode=b.cnvcInvCode";
			strsql+=" union all select c.cnvcCode as ��ص���,a.cnvcDeptID as ����,a.cnvcWhCode as �ֿ�,b.cnvcInvName as ���,a.cnvcComunitCode as ��λ,";
			strsql+="a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount as �������,a.cndLostDate as ���ʱ��,a.cnvcOperID as ����Ա,a.cnvcLostType as �������,a.cnvcComments as ��ע";
			strsql+=" from tbLostSerial a,tbInventory b,tbRdRecord c where a.cnvcInvalidFlag='1' and cnvcLostType='2' and a.cnvcInvCode=b.cnvcInvCode and a.cnnProduceSerialNo=c.cnnRdID) t";
			strsql+=" where t.���ʱ�� between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
			if(this.ddlLostType.SelectedValue!="ȫ��")
				strsql+=" and t.�������='"+this.ddlLostType.SelectedValue+"'";
			if(this.ddlDept.SelectedValue!="ȫ��")
				strsql+=" and t.����='"+this.ddlDept.SelectedValue+"'";
			if(this.ddlWhCode.SelectedValue!="ȫ��")
				strsql+=" and t.�ֿ�='"+this.ddlWhCode.SelectedValue+"'";
			strsql+=" group by t.��ص���,t.����,t.�ֿ�,t.���,t.��λ,t.���ʱ��,t.����Ա,t.�������,t.��ע order by t.���ʱ��";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ֿ�","Warehouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dtout,"����Ա","tbLogin","vcLoginID","vcOperName");
			this.TableConvert(dtout,"�������","tbNameCodeToStorage","vcCommCode","vcCommName","vcCommSign='LostType'");
			dtout.TableName="�������ͳ��";
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
