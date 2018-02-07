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
	/// wfmProvGInfo ��ժҪ˵����
	/// </summary>
	public class wfmProvGInfo : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.TextBox txtProviderName;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtProviderID;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtGoodsCode;
		protected System.Web.UI.WebControls.TextBox txtGoodsName;
		protected System.Web.UI.WebControls.Label Label1;

		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsCode;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtQueryGoodsName;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlInvalidFlag;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtStockPrice;
		protected System.Web.UI.WebControls.TextBox txtStockUnit;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtProducer;
		protected ucPageView UcPageView1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.btnSave.Enabled=false;
					this.txtGoodsCode.Enabled=false;
					this.txtGoodsName.Enabled=false;
					this.txtStockUnit.Enabled=false;
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.ddlInvalidFlag.Items.Add(new ListItem("��Ч","1"));
					this.ddlInvalidFlag.Items.Add(new ListItem("��Ч","0"));
					this.ddlInvalidFlag.SelectedIndex=0;
					string strPVid=Request.QueryString["pid"];
					string strPVname=Request.QueryString["pname"];
					string strGoodsCode=Request.QueryString["gid"];
					if(strPVid==null||strPVid==""||strPVname==null||strPVname=="")
					{
						this.txtProviderID.Text="";
						this.txtProviderName.Text="";
						this.btnAdd.Enabled=false;
						this.ddlInvalidFlag.Enabled=false;
						this.Button1.Enabled=false;
					}
					else
					{
						if(strGoodsCode==null||strGoodsCode=="")
						{
							this.txtProviderID.Text=strPVid.Trim();
							this.txtProviderName.Text=strPVname.Trim();
							this.btnAdd.Enabled=true;
							this.ddlInvalidFlag.Enabled=false;
						}
						else
						{
							this.txtProviderID.Text=strPVid.Trim();
							this.txtProviderName.Text=strPVname.Trim();
							this.txtGoodsCode.Text=strGoodsCode.Trim();
							this.btnAdd.Enabled=false;
							this.btnSave.Enabled=true;
							this.txtGoodsCode.Enabled=false;
							this.txtGoodsName.Enabled=false;
							this.ddlInvalidFlag.Enabled=true;

							Hashtable htapp=(Hashtable)Application["appconf"];
							string strcons=(string)htapp["cons"];
							StoBusi=new BusiComm.StorageBusi(strcons);
							CMSMStruct.ProviderStockStruct pss=StoBusi.GetProvStockOne(strPVid.Trim(),strGoodsCode.Trim());
							this.txtStockUnit.Text=pss.strGoodsUnit;
							this.txtStockPrice.Text=pss.dGoodsPrice.ToString();
							this.ddlInvalidFlag.SelectedIndex=this.ddlInvalidFlag.Items.IndexOf(this.ddlInvalidFlag.Items.FindByValue(pss.strInvalidFlag));
							this.txtGoodsName.Text=pss.strGoodsName;
							this.txtProducer.Text=pss.strProducer;
						}
						this.dgbind();
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			string strPrvdName=this.txtProviderName.Text.Trim();
			CMSMStruct.ProviderStockStruct pss1=new CommCenter.CMSMStruct.ProviderStockStruct();
			pss1.strPrvdCode=this.txtProviderID.Text.Trim();
			pss1.strGoodsCode=this.txtGoodsCode.Text.Trim();
			pss1.strGoodsName=this.txtGoodsName.Text.Trim();
			pss1.strProducer=this.txtProducer.Text.Trim();
			if(pss1.strPrvdCode=="")
			{
				this.Popup("��Ӧ�̱���Ϊ�գ��޷���ӣ�");
				return;
			}
			if(this.txtStockPrice.Text.Trim()=="")
			{
				this.Popup("�������۲���Ϊ�գ�");
				return;
			}
			else if(!this.JudgeIsNum(this.txtStockPrice.Text.Trim()))
			{
				this.Popup("�������۱��������֣�");
				return;
			}
			else
			{
				pss1.dGoodsPrice=Math.Round(double.Parse(this.txtStockPrice.Text.Trim()),2);
			}
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			if(pss1.strGoodsCode=="")
			{
				this.Popup("��Ӧ��Ʒ����Ϊ�գ��޷���ӣ�");
				return;
			}
			else if(StoBusi.IsExistProvGoods(pss1.strPrvdCode,pss1.strGoodsCode))
			{
				this.Popup("�ù�Ӧ�̹�Ӧ�û�Ʒ��ϵ�Ѿ����ڣ�");
				return;
			}

			if(pss1.strGoodsName=="")
			{
				this.Popup("��Ӧ��Ʒ����Ϊ�գ��޷���ӣ�");
				return;
			}
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			pss1.strOperName=ls1.strOperName;

			if(StoBusi.NewProviderStockAdd(pss1))
			{
				this.SetSuccMsgPageBydir("��Ӧ�̹�Ӧ��Ʒ��ӳɹ���","Storage/wfmProvGInfo.aspx?pid="+pss1.strPrvdCode+"&pname="+strPrvdName);
				this.dgbind();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("��Ӧ�̹�Ӧ��Ʒ���ʧ�ܣ�");
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStockStruct pss=new CommCenter.CMSMStruct.ProviderStockStruct();
			double dGoodsPrice=0;
			if(this.txtStockPrice.Text.Trim()=="")
			{
				this.Popup("�������۲���Ϊ�գ�");
				return;
			}
			else if(!this.JudgeIsNum(this.txtStockPrice.Text.Trim()))
			{
				this.Popup("�������۱��������֣�");
				return;
			}
			else
			{
				dGoodsPrice=Math.Round(double.Parse(this.txtStockPrice.Text.Trim()),2);
			}
			pss.strPrvdCode=this.txtProviderID.Text.Trim();
			pss.strGoodsCode=this.txtGoodsCode.Text.Trim();
			pss.strInvalidFlag=this.ddlInvalidFlag.SelectedValue;
			pss.dGoodsPrice=dGoodsPrice;
			pss.strProducer=this.txtProducer.Text.Trim();

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.ModProvGoodsInfo(pss))
				{
					this.SetSuccMsgPageBydir("�޸Ĺ�Ӧ��Ʒ�ɹ���","Storage/wfmProvGInfo.aspx?pid="+this.txtProviderID.Text.Trim()+"&pname="+this.txtProviderName.Text.Trim());
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("�޸Ĺ�Ӧ��Ʒʱ�������������ԣ�");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
		}

		private void dgbind()
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			DataTable dtout = StoBusi.GetProviderToGoods(this.txtProviderID.Text.Trim(),this.txtProviderName.Text.Trim());
			if(dtout==null)
			{
				this.SetErrorMsgPageBydir("��ѯ���������ԣ�");
				return;
			}
			else
			{
				this.TableConvert(dtout,"�ɹ�������λ","ComputationUnit");
				dtout.TableName="��Ӧ�̹�Ӧ��Ʒ�嵥";
				DataTable dtexcel=dtout.Copy();
				Session["QUERY"] = dtout;
			}
				
			UcPageView1.MyDataGrid.PageSize = 10;
			DataView dvOut =new DataView(dtout);
			this.UcPageView1.MyDataSource = dvOut;
			this.UcPageView1.BindGrid();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);

			DataTable dt = StoBusi.GetGoodsBySelect(this.txtProviderID.Text.Trim(),this.txtQueryGoodsCode.Text.Trim(),this.txtQueryGoodsName.Text.Trim());
			this.TableConvert(dt,"cnvcStockUnit","ComputationUnit");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			this.txtGoodsCode.Text=item.Cells[0].Text.Trim();
			this.txtGoodsName.Text=item.Cells[1].Text.Trim();
			this.txtStockUnit.Text=item.Cells[2].Text.Trim();
			this.txtStockPrice.Text=item.Cells[3].Text.Trim();
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
		}
	}
}
