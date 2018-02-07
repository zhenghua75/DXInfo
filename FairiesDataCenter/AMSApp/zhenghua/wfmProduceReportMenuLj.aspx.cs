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
using CommCenter;
namespace AMSApp.zhenghua
{
	/// <summary>
	/// wfmProduceMenu ��ժҪ˵����
	/// </summary>
	public class wfmProduceReportMenuLj : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblProduceMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;

		protected System.Web.UI.HtmlControls.HtmlTableRow trProductListReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProductSumReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProductLostListReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProductLostSumReport;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trSalesCheckListReport;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trSalesCheckSumReport;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trSalesChart;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProduceLogReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProduceLostSalesReport;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProductMaterialUseReorpt;

		protected System.Web.UI.HtmlControls.HtmlTableRow trCostReport;
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			// Put user code to initialize the page here
			CMSMStruct.LoginStruct ls1=new CommCenter.CMSMStruct.LoginStruct();
			if(Session["Login"]==null)
			{
				Response.Redirect("Exit.aspx");
			}
			else
			{
				ls1=(CMSMStruct.LoginStruct)Session["Login"];
			}
			trnoprom.Visible = true;
			trProductListReport.Visible = false;
			trProductSumReport.Visible = false;
			trProductLostListReport.Visible = false;
			trProductLostSumReport.Visible = false;
//			trSalesCheckListReport.Visible = false;
//			trSalesCheckSumReport.Visible = false;
//			trSalesChart.Visible = false;
			trProduceLogReport.Visible = false;
			trProduceLostSalesReport.Visible = false;
			trProductMaterialUseReorpt.Visible=false;
			trCostReport.Visible = false;

			#region ���Ƶ�ǰ��ʾ�˵�
			Hashtable htOperFunc=(Hashtable)Application["OperFunc"];
			ArrayList almenu=(ArrayList)htOperFunc[ls1.strLoginID];
			if(almenu!=null)
			{
				for(int i=0;i<almenu.Count;i++)
				{
					CMSMStruct.MenuStruct ms1=(CMSMStruct.MenuStruct)almenu[i];
					HtmlTableRow trCurrent = tblProduceMenu.FindControl("tr" + ms1.strFuncAddress.Replace("wfm", String.Empty)) as HtmlTableRow;
					
					if(trCurrent!=null)
					{
						trCurrent.Visible = true;
						trnoprom.Visible=false;
					}
					
					
				}
			}
			#endregion
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
