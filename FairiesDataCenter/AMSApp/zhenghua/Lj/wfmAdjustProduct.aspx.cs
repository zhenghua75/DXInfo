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
namespace AMSApp.zhenghua.Lj
{
	/// <summary>
	/// wfmAdjustProduct ��ժҪ˵����
	/// </summary>
	public class wfmAdjustProduct : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DataGrid MyDataGrid;
		protected System.Web.UI.WebControls.TextBox txtBeginDate;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.Button btnQuery;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if(!this.IsPostBack)
			{
				this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
				this.txtBeginDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				if(Session["tbProductSerial_Query"] != null)
				{
					Session.Remove("tbProductSerial_Query");
				}
				BindGrid();
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
			this.MyDataGrid.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.MyDataGrid_ItemCommand);
			this.MyDataGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.MyDataGrid_PageIndexChanged);
			this.MyDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.MyDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void BindGrid()
		{
			DataTable dtProduct = GetProduct();
			this.MyDataGrid.DataSource = dtProduct;			
			this.MyDataGrid.DataBind();
		}

		public DataTable GetProduct()
		{
			DataTable dtProduct = null;			
			if(Session["tbProductSerial_Query"] != null)
			{
				dtProduct = (DataTable) Session["tbProductSerial_Query"];
			}
			else
			{
				dtProduct = Helper.Query("select cnnSerialNo,cnvcName,cnvcCode,cnnCount+cnnAddCount-cnnReduceCount as cnnSum,cnnCount,cnnAddCount,cnnReduceCount from tbProductSerial where 1<>1");
				Session["tbProductSerial_Query"] = dtProduct;
			}
			return dtProduct;
		}
		private void SetProduct(DataTable dtProduct)
		{
			Session["tbProductSerial_Query"] = dtProduct;
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//��ѯ
			string strSql = "select cnnSerialNo,cnvcName,cnvcCode,cnnCount+cnnAddCount-cnnReduceCount as cnnSum,cnnCount,cnnAddCount,cnnReduceCount,Convert(char(10),cndCreateDate,121) as cndCreateDate from tbProductSerial where convert(char(10),cndCreateDate,121) >= '"+txtBeginDate.Text+"' and  "
				+ " convert(char(10),cndCreateDate,121) <='"+txtEndDate.Text+"'"
				+ " and cnvcDeptID = '"+ddlDept.SelectedValue+"' order by cnnSerialNo,cnvcCode";

			DataTable dtOut = Helper.Query(strSql);

			SetProduct(dtOut);
			this.MyDataGrid.CurrentPageIndex = 0;
			BindGrid();
		}



		private void MyDataGrid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.MyDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void MyDataGrid_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strSerialNo;// = e.Item.Cells[0].Text;
			string strGoodsID;// = e.Item.Cells[1].Text;
			string strCount;// = e.Item.Cells[4].Text;
			ProductSerial ps;
			OperLog ol;
			ProductFacade pf;
			DataRow[] drs;
			DataTable dtProduct = GetProduct();
			
			switch(e.CommandName)
			{
				case "AdjustAdd":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					strCount = e.Item.Cells[4].Text;
					string strAddCount = ((TextBox)e.Item.Cells[5].Controls[1]).Text;
					
					if(strAddCount=="")
					{
						this.Popup("�������������");
						return;
					}
					if(!this.JudgeIsNum(strAddCount))
					{
						this.Popup("���������֣�");
						return;
					}
					if(Convert.ToInt32(strAddCount)<=0)
					{
						this.Popup("��������������㣡");
						return;
					}

					drs = dtProduct.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						drs[0]["cnnAddCount"] = strAddCount;
						drs[0]["cnnSum"] = Convert.ToInt32(strCount)+Convert.ToInt32(strAddCount);
					}
					ps = new ProductSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;
					ps.cnnAddCount = Convert.ToInt32(strAddCount);

					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "������Ʒ����";

					pf = new ProductFacade();
					pf.AdjustProductSerial_Add(ps,ol);


					SetProduct(dtProduct);
					this.Popup("�����ɹ���");
					BindGrid();
					break;
				case "AdjustReduce":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					strCount = e.Item.Cells[4].Text;
					string strReduceCount = ((TextBox)e.Item.Cells[6].Controls[1]).Text;

					if(strReduceCount=="")
					{
						this.Popup("�������������");
						return;
					}
					if(!this.JudgeIsNum(strReduceCount))
					{
						this.Popup("���������֣�");
						return;
					}
					if(Convert.ToInt32(strReduceCount)<=0)
					{
						this.Popup("��������������㣡");
						return;
					}

					if(Convert.ToInt32(strCount)-Convert.ToInt32(strReduceCount)<=0)
					{
						this.Popup("����������С����������");
						return;
					}
					drs = dtProduct.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						drs[0]["cnnReduceCount"] = strReduceCount;
						drs[0]["cnnSum"] = Convert.ToInt32(strCount)-Convert.ToInt32(strReduceCount);
					}

					ps = new ProductSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;
					ps.cnnReduceCount = Convert.ToInt32(strReduceCount);


					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "������Ʒ����";

					pf = new ProductFacade();
					pf.AdjustProductSerial_Reduce(ps,ol);

					SetProduct(dtProduct);
					this.Popup("�����ɹ���");
					BindGrid();
					break;
				case "Delete":
					strSerialNo = e.Item.Cells[0].Text;
					strGoodsID = e.Item.Cells[1].Text;
					//strCount = e.Item.Cells[4].Text;
					drs = dtProduct.Select("cnvcCode='"+strGoodsID+"' and cnnSerialNo="+strSerialNo);
					if(drs.Length>0)
					{
						dtProduct.Rows.Remove(drs[0]);
						//drs[0]["cnnCount"] = strCount;
					}

					ps = new ProductSerial();
					ps.cnnSerialNo = Convert.ToInt32(strSerialNo);
					ps.cnvcCode = strGoodsID;

					ol = new OperLog();
					ol.cnvcDeptID = oper.strDeptID;
					ol.cnvcOperID = oper.strLoginID;
					ol.cnvcOperType = "������Ʒɾ��";

					pf = new ProductFacade();
					pf.AdjustProductSerial_Delete(ps,ol);

					SetProduct(dtProduct);
					this.Popup("������Ʒɾ���ɹ���");

					if((this.MyDataGrid.CurrentPageIndex==MyDataGrid.PageCount-1)&&MyDataGrid.Items.Count==1)
					{
						if(MyDataGrid.CurrentPageIndex-1>1)
						{
							MyDataGrid.CurrentPageIndex = MyDataGrid.CurrentPageIndex-1;
						}
						else
						{
							MyDataGrid.CurrentPageIndex = 0;
						}
            
					} 

					BindGrid();
					break;
			}
			
		}

		private void MyDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				//ɾ��ȷ��
				LinkButton delBttn = (LinkButton) e.Item.Cells[10].Controls[1];
				delBttn.Attributes.Add("onclick",@"javascript:return confirm('ȷ��ɾ���������Ϊ����" + e.Item.Cells[0].Text+"����Ʒ����Ϊ����"+e.Item.Cells[2].Text + "���Ĳ�Ʒ��?');"); 
			}
		}
	}
}
