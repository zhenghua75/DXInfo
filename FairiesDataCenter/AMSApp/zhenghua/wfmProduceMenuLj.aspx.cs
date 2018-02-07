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
	public class wfmProduceMenuLj : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblProduceMenu;
		protected System.Web.UI.HtmlControls.HtmlTableRow trnoprom;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trAddProduct;
//		protected System.Web.UI.HtmlControls.HtmlTableRow trAdjustProduct;

		protected System.Web.UI.HtmlControls.HtmlTableRow trAddProductLost;
		protected System.Web.UI.HtmlControls.HtmlTableRow trAdjustProductLost;

		//protected System.Web.UI.HtmlControls.HtmlTableRow trAddSales;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trCheck;

		//protected System.Web.UI.HtmlControls.HtmlTableRow trAdjustSales;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trAdjustCheck;


		//protected System.Web.UI.HtmlControls.HtmlTableRow trSalesRoomProduce;


		//protected System.Web.UI.HtmlControls.HtmlTableRow trMaterial;
		protected System.Web.UI.HtmlControls.HtmlTableRow trFormulaQuery;

		//protected System.Web.UI.HtmlControls.HtmlTableRow trProductQuery;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trOrderDetail;
		//protected System.Web.UI.HtmlControls.HtmlTableRow trOrder;
		protected System.Web.UI.HtmlControls.HtmlTableRow trOrderQuery;

		protected System.Web.UI.HtmlControls.HtmlTableRow trProducePlanQuery;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProducePlanQueryMake;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProducePlanQueryGoods;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProduceCheck;
		protected System.Web.UI.HtmlControls.HtmlTableRow trWarehouseOut;
		protected System.Web.UI.HtmlControls.HtmlTableRow trProduceCheckWh;
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
//			trAddProduct.Visible = false;
			trAddProductLost.Visible = false;
//			trAdjustProduct.Visible = false;
			trAdjustProductLost.Visible = false;
			//trAddSales.Visible = false;
			//trCheck.Visible = false;

			//trAdjustSales.Visible = false;
			//trAdjustCheck.Visible = false;

//			trMaterial.Visible = false;
//			trFormulaQuery.Visible = false;
//			trProductQuery.Visible = false;
//			trOrderDetail.Visible = false;
			//trOrder.Visible = false;
			trOrderQuery.Visible = false;
			trProducePlanQuery.Visible = false;
			trProducePlanQueryMake.Visible = false;
			trProducePlanQueryGoods.Visible = false;
			//trSalesRoomProduce.Visible = false;
			trProduceCheck.Visible = false;
			trWarehouseOut.Visible = false;
			trProduceCheckWh.Visible = false;

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
