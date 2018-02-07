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
	/// wfmPoStockReport ��ժҪ˵����
	/// </summary>
	public class wfmPoStockReport : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtCycle;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button btExcel;
	
		protected ucPageView UcPageView1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");

			// �ڴ˴������û������Գ�ʼ��ҳ��
			if (!IsPostBack )
			{
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.FillDropDownList("Provider",this.ddlProvider,"","ȫ��");
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
			string strCycle=this.txtCycle.Text.Trim();
			string strsql="select a.cnvcPlanCycle as �ɹ�����,a.cnvcPrvdCode as ��Ӧ��,count(distinct a.cnvcPoID) as �ɹ�������,b.cnvcGoodsCode as �ɹ��������,c.cnvcInvName as �ɹ����,b.cnvcStockUnit as �ɹ���λ,";
			strsql+="b.cnnStockPrice as �ɹ�����,sum(b.cnnStockCountSum) as �ܲɹ�����,sum(b.cnnStockFeeSum) as �ܲɹ����,sum(b.cnnArriveCount) as �ܵ�������,sum(b.cnnArriveFee) as �ܵ������,";
			strsql+="cast(cast((round(sum(b.cnnArriveCount)/sum(b.cnnStockCountSum),4)*100) as numeric(10,2)) as varchar(10))+'%' as ��ɶ� from tbPoStockMain a,tbPoStockSum b,tbInventory c";
			strsql+=" where a.cnvcPoID=b.cnvcPoID and b.cnvcGoodsCode=c.cnvcInvCode";
			if(strCycle!="")
				strsql+=" and a.cnvcPlanCycle='"+strCycle+"'";
			if(this.ddlProvider.SelectedValue!="ȫ��")
				strsql+=" and a.cnvcPrvdCode='"+this.ddlProvider.SelectedValue+"'";
			strsql+=" group by a.cnvcPlanCycle,a.cnvcPrvdCode,b.cnvcGoodsCode,c.cnvcInvName,b.cnvcStockUnit,b.cnnStockPrice order by a.cnvcPlanCycle,a.cnvcPrvdCode,b.cnvcGoodsCode";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"��Ӧ��","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtout,"�ɹ���λ","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="�ɹ�����";
			Session["QUERY"] = dtout;
			Session["toExcel"]=dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
