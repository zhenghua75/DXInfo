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
using System.Text.RegularExpressions;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmBillOfEnterStorageDetail.
	/// </summary>
	public class wfmBillOfEnterStorageDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtDeliverMan;
		protected System.Web.UI.WebControls.TextBox txtValidateOper;
		protected System.Web.UI.WebControls.TextBox txtSafeOper;
		protected System.Web.UI.WebControls.TextBox txtStorageOper;
		protected System.Web.UI.WebControls.TextBox txtBillOper;
		protected System.Web.UI.WebControls.TextBox txtStandardUnit;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.WebControls.TextBox txtSum;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnEnterNew;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.DropDownList ddlEnterType;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList ddlProduct;
		protected System.Web.UI.WebControls.TextBox txtStandardCount;
	
		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label lblUnit;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtMaterialFilter;
		protected System.Web.UI.WebControls.Button btnQueryFilter;
		protected string strBeginDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			string strBillEnterID=Request.QueryString["ID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			if(!IsPostBack)
			{
				this.txtStandardUnit.ReadOnly=true;
				this.txtStandardCount.ReadOnly=true;
				if(strBillEnterID==""||strBillEnterID==null)
				{
					strBeginDate=DateTime.Now.ToShortDateString();
					this.btnAdd.Enabled=true;
					this.FillDropDownList("Provider",this.ddlProvider);
					this.ddlEnterType.Items.Add(new ListItem("采购入库","CA01"));
					this.ddlEnterType.Items.Add(new ListItem("剩余回入","CA03"));
					this.ddlEnterType.Items.Add(new ListItem("工具类材料归还","CA05"));
					this.ddlEnterType.SelectedIndex=0;
					DataTable dtout=new DataTable();
					dtout.Columns.Add("cnvcProviderCode");
					dtout.Columns.Add("cnvcProviderName");
					dtout.Columns.Add("cnvcProductCode");
					dtout.Columns.Add("cnvcProductName");
					dtout.Columns.Add("cnvcStandardUnit");
					dtout.Columns.Add("cnnStandardCount");
					dtout.Columns.Add("cnvcUnit");
					dtout.Columns.Add("cnnPrice");
					dtout.Columns.Add("cnnCount");
					dtout.Columns.Add("cnnSum");
					Session["EnterDetail"]=dtout;
					this.DataGrid1.DataSource=dtout;
					this.DataGrid1.DataBind();
					this.btnPrint.Enabled=false;

					this.ddlProduct.Items.Clear();
					string strProvider=this.ddlProvider.SelectedValue;
					try
					{
						DataTable dtMaterial=StoBusi.GetMaterialInfoByProvider(strProvider,"");
						if(dtMaterial==null)
						{
							this.SetErrorMsgPageBydir("查询出错，请重试！");
							return;
						}
						else
						{
							this.FillDropDownList(dtMaterial,this.ddlProduct);
							if(this.ddlProduct.Items.Count>0)
							{
								DataTable dtmaterial=(DataTable)Application["AllMaterial"];
								DataView dview=dtmaterial.DefaultView;
								dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
								this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
								this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
								this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
							}
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
						return;
					}
				}
				else
				{
					this.btnAdd.Enabled=false;
					this.FillDropDownList("Provider",this.ddlProvider);
					this.ddlEnterType.Items.Add(new ListItem("采购入库","Stock"));
					this.ddlEnterType.Items.Add(new ListItem("剩余回入","Leave"));
					this.ddlEnterType.Items.Add(new ListItem("工具类材料归还","CA05"));
					this.ddlEnterType.SelectedIndex=0;
					DataSet dsout=StoBusi.GetBillOfEnterStorageOneLog(strBillEnterID);
					dsout.DataSetName="面包工坊原料(材料)进仓单";
					Session.Remove("BillPrint");
					Session["BillPrint"]=dsout.Copy();
					DataTable dtLog=dsout.Tables["EnterLog"];
					DataTable dtDetail=dsout.Tables["EnterDetail"];

					this.ddlProvider.SelectedIndex=this.ddlProvider.Items.IndexOf(this.ddlProvider.Items.FindByValue(dtLog.Rows[0]["cnvcProviderCode"].ToString()));
					this.ddlEnterType.SelectedIndex=this.ddlEnterType.Items.IndexOf(this.ddlEnterType.Items.FindByText(dtLog.Rows[0]["cnvcEnterType"].ToString()));
					strBeginDate=dtLog.Rows[0]["cndEnterDate"].ToString().Substring(0,10);
					this.txtDeliverMan.Text=dtLog.Rows[0]["cnvcDeliverMan"].ToString();
					this.txtValidateOper.Text=dtLog.Rows[0]["cnvcValidateOperID"].ToString();
					this.txtSafeOper.Text=dtLog.Rows[0]["cnvcSafeOperID"].ToString();
					this.txtStorageOper.Text=dtLog.Rows[0]["cnvcStorageOperID"].ToString();
					this.txtBillOper.Text=dtLog.Rows[0]["cnvcBillOperID"].ToString();
					this.ddlProvider.Enabled=false;
					this.ddlEnterType.Enabled=false;
					this.txtDeliverMan.Enabled=false;
					this.txtValidateOper.Enabled=false;
					this.txtSafeOper.Enabled=false;
					this.txtStorageOper.Enabled=false;
					this.txtBillOper.Enabled=false;
					this.btnEnterNew.Enabled=false;
					this.txtMaterialFilter.Enabled=false;

					this.ddlProduct.Enabled=false;
					this.txtStandardUnit.Enabled=false;
					this.txtPrice.Enabled=false;
					this.txtCount.Enabled=false;
					this.txtSum.Enabled=false;
					this.btnAdd.Enabled=false;
					if(this.ddlEnterType.SelectedItem.Text=="采购入库")
					{
						this.btnPrint.Enabled=true;
					}
					else
					{
						this.btnPrint.Enabled=false;
					}

					this.DataGrid1.DataSource=dtDetail;
					this.DataGrid1.DataBind();

					this.DataGrid1.Columns[this.DataGrid1.Columns.Count-1].Visible=false;
				}
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnQueryFilter.Click += new System.EventHandler(this.btnQueryFilter_Click);
			this.ddlProvider.SelectedIndexChanged += new System.EventHandler(this.ddlProvider_SelectedIndexChanged);
			this.btnEnterNew.Click += new System.EventHandler(this.btnEnterNew_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			DataTable dtIn=(DataTable)Session["EnterDetail"];
			string strProviderCode=this.ddlProvider.SelectedValue;
			string strProductCode=this.ddlProduct.SelectedValue;
			bool dupproflag=false;
			for(int i=0;i<dtIn.Rows.Count;i++)
			{
				if(dtIn.Rows[i]["cnvcProviderCode"].ToString()==strProviderCode&&dtIn.Rows[i]["cnvcProductCode"].ToString()==strProductCode)
				{
					dupproflag=true;
					break;
				}
			}
			if(dupproflag)
			{
				this.SetErrorMsgPageBydirHistory("该供应商提供的此种商品已经存在列表中，不能再添加！");
				return;
			}
			else
			{
				DataRow drnew=dtIn.NewRow();
				if(this.ddlProvider.SelectedValue=="")
				{
					this.SetErrorMsgPageBydirHistory("供应商不能为空！");
					return;
				}
				else
				{
					drnew["cnvcProviderCode"]=this.ddlProvider.SelectedValue;
					drnew["cnvcProviderName"]=this.ddlProvider.SelectedItem.Text;
				}

				if(this.ddlProduct.SelectedValue=="")
				{
					this.SetErrorMsgPageBydirHistory("产品不能为空！");
					return;
				}
				else
				{
					drnew["cnvcProductCode"]=this.ddlProduct.SelectedValue;
					drnew["cnvcProductName"]=this.ddlProduct.SelectedItem.Text;
				}

				if(this.txtStandardUnit.Text.Trim()=="")
				{
					this.SetErrorMsgPageBydirHistory("进仓规格不能为空！");
					return;
				}
				else
				{
					drnew["cnvcStandardUnit"]=this.txtStandardUnit.Text.Trim();
				}

				if(this.txtStandardCount.Text.Trim()=="")
				{
					this.SetErrorMsgPageBydirHistory("规格数量不能为空！");
					return;
				}
				else
				{
					drnew["cnnStandardCount"]=this.txtStandardCount.Text.Trim();
				}

				if(this.lblUnit.Text.Trim()=="")
				{
					this.SetErrorMsgPageBydirHistory("单位不能为空！");
					return;
				}
				else
				{
					drnew["cnvcUnit"]=this.lblUnit.Text.Trim();
				}

				if(this.txtPrice.Text.Trim()==""||!Regex.IsMatch(this.txtPrice.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("供货价格必须是数字！");
					return;
				}
				else
				{
					drnew["cnnPrice"]=this.txtPrice.Text.Trim();
				}

				if(this.txtCount.Text.Trim()==""||!Regex.IsMatch(this.txtCount.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("数量必须是数字！");
					return;
				}
				else
				{
					drnew["cnnCount"]=this.txtCount.Text.Trim();
				}

				if(this.txtSum.Text.Trim()==""||!Regex.IsMatch(this.txtSum.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("金额必须是数字！");
					return;
				}
				else
				{
					drnew["cnnSum"]=this.txtSum.Text.Trim();
				}

				dtIn.Rows.Add(drnew);
				this.ddlProvider.Enabled=false;
				Session.Remove("EnterDetail");
				Session["EnterDetail"]=dtIn;
				this.DataGrid1.DataSource=dtIn;
				this.DataGrid1.DataBind();
			}
		}

		private void btnEnterNew_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			if(strBeginDate==null||strBeginDate=="")
			{
				this.SetErrorMsgPageBydirHistory("进仓日期不能为空，请重新选择日期！");
				return;
			}
			DataTable dtDetail=(DataTable)Session["EnterDetail"];
			if(dtDetail.Rows.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("进仓货品为空，请先添加产品！");
				return;
			}
			string strProviderCode=this.ddlProvider.SelectedValue;
			string strDate=strBeginDate;
			string strSysTime=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
			string strEnterType=this.ddlEnterType.SelectedItem.Text;
			string strStorageOperType=this.ddlEnterType.SelectedValue;
			string strDeliverMan=this.txtDeliverMan.Text.Trim();
			string strValidateOperID=this.txtValidateOper.Text.Trim();
			string strSafeOperID=this.txtSafeOper.Text.Trim();
			string strStorageOperID=this.txtStorageOper.Text.Trim();
			string strBillOperID=this.txtBillOper.Text.Trim();

			if(strProviderCode=="")
			{
				this.SetErrorMsgPageBydirHistory("供应商不能为空！");
				return;
			}

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			Hashtable htpara=new Hashtable();
			htpara.Add("strProviderCode",strProviderCode);
			htpara.Add("strDate",strDate);
			htpara.Add("strSysTime",strSysTime);
			htpara.Add("strEnterType",strEnterType);
			htpara.Add("strStorageOperType",strStorageOperType);
			htpara.Add("strDeliverMan",strDeliverMan);
			htpara.Add("strValidateOperID",strValidateOperID);
			htpara.Add("strSafeOperID",strSafeOperID);
			htpara.Add("strStorageOperID",strStorageOperID);
			htpara.Add("strBillOperID",strBillOperID);
			htpara.Add("strOperID",ls1.strOperName);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.NewBillOfEnterStorageAdd(htpara,dtDetail))
				{
					this.SetSuccMsgPageBydir("新进仓单录入成功！","Storage/wfmBillOfEnterStorage.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("新进仓单录入时发生错误，请重试！");
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

		private void ddlProvider_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlProduct.Items.Clear();
			string strProvider=this.ddlProvider.SelectedValue;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetMaterialInfoByProvider(strProvider,this.txtMaterialFilter.Text.Trim());
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.FillDropDownList(dtout,this.ddlProduct);
					if(this.ddlProduct.Items.Count>0)
					{
						DataTable dtmaterial=(DataTable)Application["AllMaterial"];
						DataView dview=dtmaterial.DefaultView;
						dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
						this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
						this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
						this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
					}
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtIn=(DataTable)Session["EnterDetail"];
			int dataindex=e.Item.ItemIndex;
			dtIn.Rows.RemoveAt(dataindex);
			Session.Remove("EnterDetail");
			Session["EnterDetail"]=dtIn;
			this.DataGrid1.DataSource=dtIn;
			this.DataGrid1.DataBind();
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmCommPrint.aspx?type=EnterBill");
		}

		private void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.ddlProduct.Items.Count>0)
			{
				DataTable dtmaterial=(DataTable)Application["AllMaterial"];
				DataView dview=dtmaterial.DefaultView;
				dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
				this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
				this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
				this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmBillOfEnterStorage.aspx");
		}

		private void btnQueryFilter_Click(object sender, System.EventArgs e)
		{
			if(this.ddlProvider.Enabled)
			{
				string strFilter=this.txtMaterialFilter.Text.Trim();
				try
				{
					DataSet dsout=StoBusi.GetMaterialProviderByFilter(strFilter);
					if(dsout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						return;
					}
					else
					{
						this.FillDropDownList(dsout.Tables["provider"],this.ddlProvider);
						this.FillDropDownList(dsout.Tables["material"],this.ddlProduct);
						if(this.ddlProduct.Items.Count>0)
						{
							DataTable dtmaterial=(DataTable)Application["AllMaterial"];
							DataView dview=dtmaterial.DefaultView;
							dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
							this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
							this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
							this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
						}
					}
				}
				catch(Exception er)
				{
					this.clog.WriteLine(er);
					this.SetErrorMsgPageBydir("查询错误，请重试！");
					return;
				}
			}
			else
			{
				this.ddlProduct.Items.Clear();
				string strProvider=this.ddlProvider.SelectedValue;
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					DataTable dtout=StoBusi.GetMaterialInfoByProvider(strProvider,this.txtMaterialFilter.Text.Trim());
					if(dtout==null)
					{
						this.SetErrorMsgPageBydir("查询出错，请重试！");
						return;
					}
					else
					{
						this.FillDropDownList(dtout,this.ddlProduct);
						if(this.ddlProduct.Items.Count>0)
						{
							DataTable dtmaterial=(DataTable)Application["AllMaterial"];
							DataView dview=dtmaterial.DefaultView;
							dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
							this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
							this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
							this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
						}
					}
				}
				catch(Exception er)
				{
					this.clog.WriteLine(er);
					this.SetErrorMsgPageBydir("查询错误，请重试！");
					return;
				}
			}
		}
	}
}
