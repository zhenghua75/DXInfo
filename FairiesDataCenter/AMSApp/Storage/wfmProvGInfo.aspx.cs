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
	/// wfmProvGInfo 的摘要说明。
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
			// 在此处放置用户代码以初始化页面
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
					this.ddlInvalidFlag.Items.Add(new ListItem("有效","1"));
					this.ddlInvalidFlag.Items.Add(new ListItem("无效","0"));
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

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
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
				this.Popup("供应商编码为空，无法添加！");
				return;
			}
			if(this.txtStockPrice.Text.Trim()=="")
			{
				this.Popup("供货单价不能为空！");
				return;
			}
			else if(!this.JudgeIsNum(this.txtStockPrice.Text.Trim()))
			{
				this.Popup("供货单价必须是数字！");
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
				this.Popup("供应货品编码为空，无法添加！");
				return;
			}
			else if(StoBusi.IsExistProvGoods(pss1.strPrvdCode,pss1.strGoodsCode))
			{
				this.Popup("该供应商供应该货品关系已经存在！");
				return;
			}

			if(pss1.strGoodsName=="")
			{
				this.Popup("供应货品名称为空，无法添加！");
				return;
			}
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			pss1.strOperName=ls1.strOperName;

			if(StoBusi.NewProviderStockAdd(pss1))
			{
				this.SetSuccMsgPageBydir("供应商供应货品添加成功！","Storage/wfmProvGInfo.aspx?pid="+pss1.strPrvdCode+"&pname="+strPrvdName);
				this.dgbind();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("供应商供应货品添加失败！");
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.ProviderStockStruct pss=new CommCenter.CMSMStruct.ProviderStockStruct();
			double dGoodsPrice=0;
			if(this.txtStockPrice.Text.Trim()=="")
			{
				this.Popup("供货单价不能为空！");
				return;
			}
			else if(!this.JudgeIsNum(this.txtStockPrice.Text.Trim()))
			{
				this.Popup("供货单价必须是数字！");
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
					this.SetSuccMsgPageBydir("修改供应货品成功！","Storage/wfmProvGInfo.aspx?pid="+this.txtProviderID.Text.Trim()+"&pname="+this.txtProviderName.Text.Trim());
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("修改供应货品时发生错误，请重试！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
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
				this.SetErrorMsgPageBydir("查询出错，请重试！");
				return;
			}
			else
			{
				this.TableConvert(dtout,"采购计量单位","ComputationUnit");
				dtout.TableName="供应商供应货品清单";
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
