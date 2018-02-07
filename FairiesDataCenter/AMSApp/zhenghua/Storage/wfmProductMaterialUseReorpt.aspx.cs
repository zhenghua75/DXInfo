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
	/// wfmProductMaterialUseReorpt ��ժҪ˵����
	/// </summary>
	public class wfmProductMaterialUseReorpt : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label1;

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
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			string strsql="";
			if(this.ddlDept.SelectedValue!="ȫ��")
			{
				strsql="select t.cnvcProduceDeptID as ����,t.cnvcInvCode as �������,t.cnvcInvName as �������,t.cnvcProduceUnitCode as ��λ,cnnMakeCount as Ӧ����,";
				strsql+="cnnCollarCount as ʵ��ʹ����,isnull(g.lost,0) as ����� from (select c.cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode,";
				strsql+="sum(cnnMakeCount) as cnnMakeCount,sum(cnnCollarCount) as cnnCollarCount from tbMakeLog a,tbMakeDetail b,tbProduceCheckLog c,tbInventory d";
				strsql+=" where a.cnnMakeSerialNo=b.cnnMakeSerialNo and a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59' and c.cnvcProduceDeptID='"+this.ddlDept.SelectedValue+"'"; 
				strsql+=" and a.cnnProduceSerialNo=c.cnnProduceSerialNo and b.cnvcInvCode=d.cnvcInvCode and b.cnvcInvCode=d.cnvcInvCode";
				strsql+=" group by cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode) t left join (select a.cnvcInvCode,";
				strsql+="sum((a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount)*c.cniChangRate/d.cniChangRate) as lost from tbLostSerial a,tbInventory b,tbComputationUnit c,";
				strsql+="tbComputationUnit d where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
				strsql+=" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcComunitCode=c.cnvcComunitCode and b.cnvcProduceUnitCode=d.cnvcComunitCode group by a.cnvcInvCode) g";
				strsql+=" on t.cnvcInvCode=g.cnvcInvCode order by t.cnvcProduceDeptID,t.cnvcInvCode,t.cnvcInvName";
			}
			else
			{
				strsql="select t.cnvcProduceDeptID as ����,t.cnvcInvCode as �������,t.cnvcInvName as �������,t.cnvcProduceUnitCode as ��λ,cnnMakeCount as Ӧ����,";
				strsql+="cnnCollarCount as ʵ��ʹ����,isnull(g.lost,0) as ����� from (select c.cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode,";
				strsql+="sum(cnnMakeCount) as cnnMakeCount,sum(cnnCollarCount) as cnnCollarCount from tbMakeLog a,tbMakeDetail b,tbProduceCheckLog c,tbInventory d";
				strsql+=" where a.cnnMakeSerialNo=b.cnnMakeSerialNo and a.cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'"; 
				strsql+=" and a.cnnProduceSerialNo=c.cnnProduceSerialNo and b.cnvcInvCode=d.cnvcInvCode and b.cnvcInvCode=d.cnvcInvCode";
				strsql+=" group by cnvcProduceDeptID,b.cnvcInvCode,d.cnvcInvName,d.cnvcProduceUnitCode) t left join (select a.cnvcInvCode,";
				strsql+="sum((a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount)*c.cniChangRate/d.cniChangRate) as lost from tbLostSerial a,tbInventory b,tbComputationUnit c,";
				strsql+="tbComputationUnit d where cndOperDate between '"+strBeginDate+"' and '"+strEndDate+" 23:59:59'";
				strsql+=" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcComunitCode=c.cnvcComunitCode and b.cnvcProduceUnitCode=d.cnvcComunitCode group by a.cnvcInvCode) g";
				strsql+=" on t.cnvcInvCode=g.cnvcInvCode order by t.cnvcProduceDeptID,t.cnvcInvCode,t.cnvcInvName";
			}
				
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"����","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"��λ","ComputationUnit","vcCommCode","vcCommName");
			dtout.TableName="ԭ��������ʹ�ñ�";
			Session["QUERY"] = dtout;
			UcPageView1.MyDataGrid.PageSize = 20;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}
	}
}
