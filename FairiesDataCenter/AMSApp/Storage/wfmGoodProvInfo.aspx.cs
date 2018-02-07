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

namespace AMSApp.Storage
{
	/// <summary>
	/// wfmGoodProvInfo ��ժҪ˵����
	/// </summary>
	public class wfmGoodProvInfo : wfmBase
	{
		BusiComm.StorageBusi StoBusi;
		protected ucPageView UcPageView1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					Session.Remove("QUERY");
					Session.Remove("page_view");
					string strGoodsName=Request.QueryString["gname"];
					if(strGoodsName!=null&&strGoodsName!="")
					{
						this.dgbind(strGoodsName.Trim());
					}
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dgbind(string strGoodsName)
		{
			if(strGoodsName!="��Ʒ")
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				DataTable dtout = StoBusi.GetGoodsToProvider(strGoodsName);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
					return;
				}
				else
				{
					dtout.TableName="��Ʒ��Ӧ��Ӧ���嵥";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
				}
				
				UcPageView1.MyDataGrid.PageSize = 10;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
		}
	}
}
