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
	/// wfmPoStockReturnDetail ��ժҪ˵����
	/// </summary>
	public class wfmPoStockReturnDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Button btnreturn;
		protected System.Web.UI.WebControls.Button btEnterComf;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsCode;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtQueryEnterCode;
		protected System.Web.UI.WebControls.TextBox txtgopage;

		protected ucPageView UcPageView1;
		protected int gopage;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			string strrdid=Request.QueryString["rdid"];
			string strcode=Request.QueryString["code"];
			string strdept=Request.QueryString["dept"];
			string strwhid=Request.QueryString["whid"];
			if(strrdid==""||strrdid==null||strcode==""||strcode==null||strdept==""||strdept==null||strwhid==""||strwhid==null)
			{
				this.SetErrorMsgPageBydir("�ؼ��������ݴ��������ԣ�");
				return;
			}
			else
			{
				if (!IsPostBack )
				{
					gopage=-1;
					this.txtgopage.Text="-1";
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.txtCode.Text=strcode;
					this.txtRdID.Text=strrdid;
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+strdept+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"vcCommCode='"+strwhid+"'");
					this.txtCode.Enabled=false;
					this.txtRdID.Enabled=false;
					this.txtRdID.Visible=false;
					this.ddlWhouse.Enabled=false;
					this.ddlDept.Enabled=false;
					this.btEnterComf.Enabled=false;
					string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strrdid).Rows[0]["cnvcState"].ToString();
					if(strState!="0")
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[12].Visible=false;
					}
				}
				else
				{
					gopage=int.Parse(this.txtgopage.Text.Trim())-1;
					this.txtgopage.Text=gopage.ToString();
				}
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
			this.btEnterComf.Click += new System.EventHandler(this.btEnterComf_Click);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			string strRdid=this.txtRdID.Text.Trim();
			string strsql="select a.cnnAutoID,a.cnnRdID,a.cnvcProviderID,a.cnvcInvCode,b.cnvcInvName,a.cnnPrice,a.cnvcGroupCode,a.cnvcComunitCode,c.cnnQuantity as cnnEnterQuantity,a.cnnQuantity,";
			strsql+="a.cnvcBatch,a.cnnCost,a.cnvcCommens,a.cndMDate,a.cndExpDate from tbRdRecordDetail a,tbInventory b,(select cnnAutoID,cnvcPoID,cnvcProviderID,cnvcInvCode,cnnQuantity from tbRdRecordDetail) c";
			strsql+=" where a.cnvcInvCode=b.cnvcInvCode and a.cnnRdID="+strRdid+" and a.cnvcPoID=c.cnvcPoID and a.cnnMPoID=c.cnnAutoID and a.cnvcProviderID=c.cnvcProviderID and a.cnvcInvCode=c.cnvcInvCode";
			DataTable dtdetail=Helper.Query(strsql);

			strsql="select cnvcInvCode as ������,cnvcComunitCode as ��λ����,cnvcComunitCode as ��λ,sum(cnnQuantity) as ����,sum(cnnCost) as ���,cndMdate as ��������,cndExpDate as ��������";
			strsql+=" from tbRdRecordDetail where cnnRdID="+strRdid+" group by cnvcInvCode,cnvcComunitCode,cndMdate,cndExpDate";
			DataTable dtsum=Helper.Query(strsql);
			Session["sumenter"]=dtsum;

			this.TableConvert(dtdetail,"cnvcProviderID","Provider","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcComunitCode","ComputationUnit","vcCommCode","vcCommName");
			Session["Redetailtomod"]=dtdetail;
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource = dtdetail;
			this.Datagrid2.DataBind();

			this.TableConvert(dtsum,"��λ","ComputationUnit","vcCommCode","vcCommName");
			UcPageView1.MyDataGrid.Caption="�ɹ��˻��ӵ����ܱ�";
			UcPageView1.MyDataGrid.PageSize = 20;
			this.UcPageView1.MyDataSource = dtsum.DefaultView;
			this.UcPageView1.BindGrid();

			string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strRdid).Rows[0]["cnvcState"].ToString();
			if(strState=="0"&&dtsum.Rows.Count>0)
			{
				this.btEnterComf.Enabled=true;
			}
			else if(strState!="0")
			{
				this.Button1.Enabled=false;
				this.btEnterComf.Enabled=false;
				this.Datagrid2.Columns[12].Visible=false;
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void btEnterComf_Click(object sender, System.EventArgs e)
		{
			string strWhcode=this.ddlWhouse.SelectedValue;
			string strRdid=this.txtRdID.Text.Trim();

			if(strWhcode=="")
			{
				this.Popup("�ֿⲻ��Ϊ�գ�");
				return;
			}
			DataTable dtsum=(DataTable)Session["sumenter"];
			if(dtsum==null||dtsum.Rows.Count<=0)
			{
				this.Popup("�òɹ��˻������κλ�Ʒ��¼������¼��Ҫ�˵Ļ�Ʒ��");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "�ɹ��˻�������";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.PoStockReturnExecReturning(ol,strWhcode,dtsum,strRdid);
			if(ret > 0 )
			{
				this.Popup("�ɹ��˻�������ɹ���");
				this.DBBind();
			}
			else
			{
				this.Popup("�ɹ��˻�������ʧ�ܣ�");
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strsql="select b.cnnAutoID,b.cnvcPoID,a.cnvcCode,a.cndARVDate,b.cnvcProviderID,b.cnvcProviderID as cnvcProviderName,b.cnvcInvCode,c.cnvcInvName";
			strsql+=" from tbRdRecord a,tbRdRecordDetail b,tbInventory c where a.cnnRdID=b.cnnRdID and a.cnvcDepID='"+this.ddlDept.SelectedValue+"' and a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			strsql+=" and b.cnvcInvCode=c.cnvcInvCode and cnvcRdCode='RD001' and cnvcState='1' and b.cnnAutoID not in(select distinct cnnMPoID from tbRdRecordDetail)";

			if(this.txtQueryEnterCode.Text.Trim()!="")
			{
				strsql+=" and a.cnvcCode like '%"+this.txtQueryEnterCode.Text.Trim()+"%'";
			}
			if(this.txtQueryGoodsCode.Text.Trim()!="")
			{
				strsql+=" and b.cnvcInvCode like '%"+this.txtQueryGoodsCode.Text.Trim()+"%'";
			}

			DataTable dt = Helper.Query(strsql);
			this.TableConvert(dt,"cnvcProviderName","Provider","vcCommCode","vcCommName");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			string strselsql="select "+this.txtRdID.Text.Trim()+",'"+item.Cells[1].Text.Trim()+"',"+item.Cells[0].Text.Trim()+",cnvcProviderID,cnvcInvCode,0,cnnPrice,0,0,cnvcGroupCode,cnvcComunitCode,cnvcBatch,'0','',cndMdate,cnnMassDate,cnvcMassUnit,cndExpDate";
			strselsql+=" from tbRdRecordDetail where cnnAutoID="+item.Cells[0].Text.Trim()+" and cnvcPoID='"+item.Cells[1].Text.Trim()+"' and cnvcProviderID='"+item.Cells[4].Text.Trim()+"' and cnvcInvCode='"+item.Cells[6].Text.Trim()+"'";
			OperLog operLog=new OperLog();
			operLog.cnvcOperType = "��Ӳɹ��˻�Ʒ";
			operLog.cnvcOperID = this.oper.strLoginID;
			operLog.cnvcDeptID = this.oper.strDeptID;
			StorageFacade sto=new StorageFacade();
			int ret=sto.AddPoStockReturnDetail(operLog,strselsql);
			if(ret > 0 )
			{
				this.Popup("����˻�Ʒ�ɹ���");
			}
			else
			{
				this.Popup("����˻�Ʒʧ�ܣ�");
			}

			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DBBind();
		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			this.Datagrid2.DataSource=(DataTable)Session["Redetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["Redetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			decimal EnterQuantity=Math.Round(decimal.Parse(e.Item.Cells[8].Text.Trim()),2);
			Entity.RdRecordDetail rrd=new RdRecordDetail();
			rrd.cnnAutoID=int.Parse(e.Item.Cells[0].Text.Trim());
			rrd.cnnQuantity=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[9].Controls[0]).Text.Trim()),2);
			rrd.cnvcCommens=((TextBox)e.Item.Cells[11].Controls[0]).Text.Trim();
			if(rrd.cnnQuantity>EnterQuantity)
			{
				this.Popup("�ɹ��˻��������ܴ���ԭ���������");
				return;
			}
			if(rrd.cnnAutoID.ToString()==""||rrd.cnnAutoID==0)
			{
				this.Popup("�ɹ��˻����ӱ��ʶ����ȷ��");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "�޸Ĳɹ��˻��ӱ�";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdatePoStockReturnDetail(ol,rrd);
			if(ret > 0 )
			{
				this.Popup("�޸Ĳɹ��˻����ӱ���ϸ�ɹ���");
				this.DBBind();
			}
			else
			{
				this.Popup("�޸Ĳɹ��˻����ӱ���ϸʧ�ܣ�");
			}
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["Redetailtomod"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
		}
	}
}
