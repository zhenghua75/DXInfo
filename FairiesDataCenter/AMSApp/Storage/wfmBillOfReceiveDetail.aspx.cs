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
	/// Summary description for wfmBillOfReceiveDetail.
	/// </summary>
	public class wfmBillOfReceiveDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtStandardUnit;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlProduct;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Button btnReceiveNew;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList ddlReceiveDeptID;
		protected System.Web.UI.WebControls.TextBox txtClass;
		protected System.Web.UI.WebControls.TextBox txtMaterialInchargeOperID;
		protected System.Web.UI.WebControls.TextBox txtStorageInchargeOperID;
		protected System.Web.UI.WebControls.TextBox txtSendOperID;
		protected System.Web.UI.WebControls.TextBox txtReceiveCount;
		protected System.Web.UI.WebControls.TextBox txtClassStorage;
		protected System.Web.UI.WebControls.TextBox txtReceiveOperID;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtStandardCount;
		protected System.Web.UI.WebControls.DropDownList ddlGroup;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.DropDownList ddlBillType;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.Label lblUnit;
		protected System.Web.UI.WebControls.TextBox txtBillState;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.TextBox txtReceiveID;

		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label Label3;
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
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			string strBillReceiveID=Request.QueryString["ID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			if(!IsPostBack)
			{
				this.txtStandardUnit.ReadOnly=true;
				this.txtStandardCount.ReadOnly=true;
				if(strBillReceiveID==""||strBillReceiveID==null)
				{
					strBeginDate=DateTime.Now.ToShortDateString();
					this.btnAdd.Enabled=true;
					this.FillDropDownList("NewDept",this.ddlReceiveDeptID);
					this.FillDropDownList("tbNameCodeToStorage",this.ddlGroup,"vcCommSign='GROUP'");
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlReceiveDeptID.SelectedIndex=this.ddlReceiveDeptID.Items.IndexOf(this.ddlReceiveDeptID.Items.FindByValue(ls1.strNewDeptID));
						this.ddlReceiveDeptID.Enabled=false;
					}
					this.ddlBillType.Items.Add(new ListItem("无制令原材料领用单","0"));
					this.ddlBillType.Items.Add(new ListItem("无制令工具特殊领用单","1"));
					this.ddlBillType.SelectedIndex=0;

					if(this.ddlBillType.SelectedValue=="0")
					{
						this.FillDropDownList("PClass",this.ddlMaterialType,"vcCommSign in('Raw','Pack')");
					}
					else
					{
						this.ddlMaterialType.Items.Add(new ListItem("生产使用工用具","22001~22999"));
						this.ddlMaterialType.Items.Add(new ListItem("门店销售使用工用具","23001~23999"));
						this.ddlMaterialType.Items.Add(new ListItem("机电工用具及零部件","25001~25999"));
						this.ddlMaterialType.SelectedIndex=0;
					}
					
					this.FillDropDownList("AllMaterial",this.ddlProduct,"cnvcProductClass='"+this.ddlMaterialType.SelectedValue+"'");
					if(this.ddlProduct.Items.Count>0)
					{
						DataTable dtmaterial=(DataTable)Application["AllMaterial"];
						DataView dview=dtmaterial.DefaultView;
						dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
						this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
						this.txtStandardUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
						this.txtStandardCount.Text=dview[0]["cnnStatdardCount"].ToString();
					}

					DataTable dtout=new DataTable();
					dtout.Columns.Add("cnvcProductCode");
					dtout.Columns.Add("cnvcProductName");
					dtout.Columns.Add("cnvcStandardUnit");
					dtout.Columns.Add("cnnStandardCount");
					dtout.Columns.Add("cnvcUnit");
					dtout.Columns.Add("cnnClassStorage");
					dtout.Columns.Add("cnvcReceiveOperID");
					dtout.Columns.Add("cnnReceiveCount");
					dtout.Columns.Add("cnnOutCount");
					dtout.Columns.Add("cnnLoseCount");
					dtout.Columns.Add("cnnCount");
					Session["ReceiveDetail"]=dtout;
					this.DataGrid1.DataSource=dtout;
					this.DataGrid1.DataBind();
					this.DataGrid1.Columns[8].Visible=false;
					this.DataGrid1.Columns[9].Visible=false;
					this.DataGrid1.Columns[10].Visible=false;
					this.DataGrid1.Columns[11].Visible=false;
					this.btnPrint.Enabled=false;
				}
				else
				{
					this.btnAdd.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlReceiveDeptID);
					this.FillDropDownList("tbNameCodeToStorage",this.ddlGroup,"vcCommSign='GROUP'");
					this.ddlBillType.Items.Add(new ListItem("原材料制令领用单","0"));
					this.ddlBillType.Items.Add(new ListItem("无制令原材料领用单","1"));
					this.ddlBillType.Items.Add(new ListItem("无制令工具特殊领用单","2"));
					this.ddlBillType.SelectedIndex=0;
					DataSet dsout=StoBusi.GetBillOfReceiveOneLog(strBillReceiveID);
					DataTable dtLog=dsout.Tables["ReceiveLog"];
					DataTable dtDetail=dsout.Tables["ReceiveDetail"];
					Session["ReceiveDetail"]=dtDetail;

					this.ddlBillType.SelectedIndex=this.ddlBillType.Items.IndexOf(this.ddlBillType.Items.FindByText(dtLog.Rows[0]["cnvcReceiveType"].ToString()));
					this.ddlReceiveDeptID.SelectedIndex=this.ddlReceiveDeptID.Items.IndexOf(this.ddlReceiveDeptID.Items.FindByValue(dtLog.Rows[0]["cnvcReceiveDeptID"].ToString()));
					this.ddlGroup.SelectedIndex=this.ddlGroup.Items.IndexOf(this.ddlGroup.Items.FindByValue(dtLog.Rows[0]["cnvcGroup"].ToString()));

					DataSet dsprint=new DataSet("无制令工具特殊领用单（"+dtLog.Rows[0]["cnvcClass"].ToString()+"）");
					Session.Remove("BillPrint");
					DataTable dtLogCopy=dtLog.Copy();
					dtLogCopy.Rows[0]["cnvcReceiveDeptID"]=this.ddlReceiveDeptID.SelectedItem.Text;
					dtLogCopy.Rows[0]["cnvcGroup"]=this.ddlGroup.SelectedItem.Text;
					dsprint.Tables.Add(dtLogCopy);
					dsprint.Tables.Add(dtDetail.Copy());
					Session["BillPrint"]=dsprint;

					this.txtReceiveID.Text=strBillReceiveID;
					strBeginDate=dtLog.Rows[0]["cndReceiveDate"].ToString().Substring(0,10);
					this.txtClass.Text=dtLog.Rows[0]["cnvcClass"].ToString();
					this.txtMaterialInchargeOperID.Text=dtLog.Rows[0]["cnvcMaterialInchargeOperID"].ToString();
					this.txtStorageInchargeOperID.Text=dtLog.Rows[0]["cnvcStorageInchargeOperID"].ToString();
					this.txtSendOperID.Text=dtLog.Rows[0]["cnvcSendOperID"].ToString();
					this.txtBillState.Text=dtLog.Rows[0]["cnvcBillState"].ToString();
					this.ddlBillType.Enabled=false;
					this.ddlReceiveDeptID.Enabled=false;
					this.ddlGroup.Enabled=false;
					this.txtClass.Enabled=false;
					this.txtMaterialInchargeOperID.Enabled=false;
					this.txtStorageInchargeOperID.Enabled=false;
					this.txtSendOperID.Enabled=false;

					this.ddlMaterialType.Enabled=false;
					this.ddlProduct.Enabled=false;
					this.txtStandardUnit.Enabled=false;
					this.txtStandardCount.Enabled=false;
					this.txtReceiveCount.Enabled=false;
					this.txtClassStorage.Enabled=false;
					this.txtReceiveOperID.Enabled=false;
					this.btnAdd.Enabled=false;
					if(this.ddlBillType.SelectedItem.Text=="无制令工具特殊领用单")
					{
						this.btnPrint.Enabled=true;
					}
					else
					{
						this.btnPrint.Enabled=false;
					}

					switch(this.txtBillState.Text.Trim())
					{
						case "0":
							this.btnReceiveNew.Text="发货量确认";
							this.DataGrid1.Columns[9].Visible=false;
							this.DataGrid1.Columns[10].Visible=false;
							this.DataGrid1.Columns[12].Visible=false;
//							this.btnReceiveNew.Attributes.Add("onclick","return confirm('请确认你已经正确填写了\"发货量\"，并已将本领料单发货。\\n\\n确认发货？');");
							break;
						case "1":
							this.btnReceiveNew.Text="收货确认";
							((BoundColumn)this.DataGrid1.Columns[8]).ReadOnly=true;
							this.DataGrid1.Columns[12].Visible=false;
							this.btnReceiveNew.Enabled=true;
							this.btnReceiveNew.Attributes.Add("onclick","return confirm('请确认你已经正确填写了\"损耗量和实际领用量\"，并要进行本领料单收货确认。\\n\\n确认收货？');");
							break;
						case "2":
							this.btnReceiveNew.Text="领料单";
							this.btnReceiveNew.Enabled=false;
							this.DataGrid1.Columns[11].Visible=false;
							this.DataGrid1.Columns[12].Visible=false;
							break;
					}

					this.DataGrid1.DataSource=dtDetail;
					this.DataGrid1.DataBind();
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
			this.ddlBillType.SelectedIndexChanged += new System.EventHandler(this.ddlBillType_SelectedIndexChanged);
			this.btnReceiveNew.Click += new System.EventHandler(this.btnReceiveNew_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.ddlMaterialType.SelectedIndexChanged += new System.EventHandler(this.ddlMaterialType_SelectedIndexChanged);
			this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnReceiveNew_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			DataTable dtDetail=(DataTable)Session["ReceiveDetail"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			Hashtable htpara=new Hashtable();
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];

			switch(this.txtBillState.Text.Trim())
			{
				case "":
					#region 新领料单录入
					if(dtDetail.Rows.Count==0)
					{
						this.SetErrorMsgPageBydirHistory("领用货品为空，请先添加产品！");
						return;
					}
					string strBillType=this.ddlBillType.SelectedItem.Text;
					string strReceiveDeptID=this.ddlReceiveDeptID.SelectedValue;
					string strReceiveDate=strBeginDate;
					string strSysTime=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
					string strGroup=this.ddlGroup.SelectedValue;
					string strClass=this.txtClass.Text.Trim();
					string strMaterialInchargeOperID=this.txtMaterialInchargeOperID.Text.Trim();
					string strStorageInchargeOperID=this.txtStorageInchargeOperID.Text.Trim();
					string strSendOperID=this.txtSendOperID.Text.Trim();

					if(strReceiveDeptID=="")
					{
						this.SetErrorMsgPageBydirHistory("领料单位不能为空！");
						return;
					}

					htpara.Add("strBillType",strBillType);
					htpara.Add("strReceiveDeptID",strReceiveDeptID);
					htpara.Add("strReceiveDate",strReceiveDate);
					htpara.Add("strSysTime",strSysTime);
					htpara.Add("strGroup",strGroup);
					htpara.Add("strClass",strClass);
					htpara.Add("strMaterialInchargeOperID",strMaterialInchargeOperID);
					htpara.Add("strStorageInchargeOperID",strStorageInchargeOperID);
					htpara.Add("strSendOperID",strSendOperID);
					htpara.Add("strOperID",ls1.strOperName);

					try
					{
						if(StoBusi.NewBillOfReceiveAdd(htpara,dtDetail))
						{
							this.SetSuccMsgPageBydir("新领料单录入成功！","Storage/wfmBillOfReceive.aspx");
						}
						else
						{
							this.SetErrorMsgPageBydir("新领料单录入时发生错误，请重试！");
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
					}
					#endregion
					break;
				case "0":
					#region 领料单--发货
					htpara.Add("ReceiveID",this.txtReceiveID.Text.Trim());
					htpara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
					htpara.Add("strOperID",ls1.strOperName);
					try
					{
						if(StoBusi.UpdateBillOfReceiveOut(htpara,dtDetail))
						{
							this.SetSuccMsgPageBydir("填写领料单发货量成功！","Storage/wfmBillOfReceiveSend.aspx");
						}
						else
						{
							this.SetErrorMsgPageBydir("填写领料单发货量时发生错误，请重试！");
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
					}
					#endregion
					break;
				case "1":
					#region 领料单--验收入库
					htpara.Add("ReceiveID",this.txtReceiveID.Text.Trim());
					htpara.Add("strReceiveDeptID",this.ddlReceiveDeptID.SelectedValue);
					htpara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
					htpara.Add("strOperID",ls1.strOperName);
					try
					{
						if(StoBusi.UpdateBillOfReceiveValidEnter(htpara,dtDetail))
						{
							this.SetSuccMsgPageBydir("领料单收货成功！","Storage/wfmBillOfReceive.aspx");
						}
						else
						{
							this.SetErrorMsgPageBydir("领料单收货时发生错误，请重试！");
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
					}
					#endregion
					break;
			}		
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{			
			DataTable dtIn=(DataTable)Session["ReceiveDetail"];
			string strProductCode=this.ddlProduct.SelectedValue;

			if(strProductCode=="")
			{
				this.SetErrorMsgPageBydirHistory("领用原材料不能为空！");
				return;
			}

			bool dupproflag=false;
			for(int i=0;i<dtIn.Rows.Count;i++)
			{
				if(dtIn.Rows[i]["cnvcProductCode"].ToString()==strProductCode)
				{
					dupproflag=true;
					break;
				}
			}
			if(dupproflag)
			{
				this.SetErrorMsgPageBydirHistory("此种商品已经存在列表中，不能再添加！");
				return;
			}
			else
			{
				DataRow drnew=dtIn.NewRow();
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
					this.SetErrorMsgPageBydirHistory("规格单位不能为空！");
					return;
				}
				else
				{
					drnew["cnvcStandardUnit"]=this.txtStandardUnit.Text.Trim();
				}

				if(this.txtStandardCount.Text.Trim()==""||!Regex.IsMatch(this.txtStandardCount.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("规格数量必须是数字！");
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

				if(this.txtReceiveCount.Text.Trim()==""||!Regex.IsMatch(this.txtReceiveCount.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("应领量必须是数字！");
					return;
				}
				else
				{
					drnew["cnnReceiveCount"]=this.txtReceiveCount.Text.Trim();
				}

				if(this.txtClassStorage.Text.Trim()==""||!Regex.IsMatch(this.txtClassStorage.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("上班库存必须是数字！");
					return;
				}
				else
				{
					drnew["cnnClassStorage"]=this.txtClassStorage.Text.Trim();
				}

				drnew["cnnCount"]="0";
				drnew["cnnOutCount"]="0";
				drnew["cnnLoseCount"]="0";

				if(this.txtReceiveOperID.Text.Trim()=="")
				{
					this.SetErrorMsgPageBydirHistory("领料人不能为空！");
					return;
				}
				else
				{
					drnew["cnvcReceiveOperID"]=this.txtReceiveOperID.Text.Trim();
				}

				dtIn.Rows.Add(drnew);
				this.ddlReceiveDeptID.Enabled=false;
				Session.Remove("ReceiveDetail");
				Session["ReceiveDetail"]=dtIn;
				this.DataGrid1.DataSource=dtIn;
				this.DataGrid1.DataBind();
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtIn=(DataTable)Session["ReceiveDetail"];
			int dataindex=e.Item.ItemIndex;
			dtIn.Rows.RemoveAt(dataindex);
			Session.Remove("ReceiveDetail");
			Session["ReceiveDetail"]=dtIn;
			this.DataGrid1.DataSource=dtIn;
			this.DataGrid1.DataBind();
		}

		private void ddlBillType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlMaterialType.Items.Clear();
			this.ddlProduct.Items.Clear();
			if(this.ddlBillType.SelectedValue=="0")
			{
				this.FillDropDownList("PClass",this.ddlMaterialType,"vcCommSign in('Raw','Pack')");
			}
			else
			{
				this.ddlMaterialType.Items.Add(new ListItem("生产使用工用具","22001~22999"));
				this.ddlMaterialType.Items.Add(new ListItem("门店销售使用工用具","23001~23999"));
				this.ddlMaterialType.Items.Add(new ListItem("机电工用具及零部件","25001~25999"));
				this.ddlMaterialType.SelectedIndex=0;
			}
					
			this.FillDropDownList("AllMaterial",this.ddlProduct,"cnvcProductClass='"+this.ddlMaterialType.SelectedValue+"' and vcCommName like '%"+this.txtMaterialFilter.Text.Trim()+"%'");
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

		private void ddlMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			this.ddlProduct.Items.Clear();
			this.FillDropDownList("AllMaterial",this.ddlProduct,"cnvcProductClass='"+this.ddlMaterialType.SelectedValue+"' and vcCommName like '%"+this.txtMaterialFilter.Text.Trim()+"%'");
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

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			if(this.ddlBillType.SelectedItem.Text=="无制令工具特殊领用单")
			{
				Response.Redirect("wfmCommPrint.aspx?type=SpecReceiveBill");
			}
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

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtdetail=new DataTable();
			switch(this.txtBillState.Text.Trim())
			{
				case "0":
					string receivecount=e.Item.Cells[7].Text.Trim();
					string outcount=((TextBox)e.Item.Cells[8].Controls[0]).Text.Trim();
					double StandCount=Math.Round(double.Parse(e.Item.Cells[3].Text.Trim()),2);
					if(outcount==""||!Regex.IsMatch(outcount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						this.SetErrorMsgPageBydirHistory("发货量必须是数字！");
						return;
					}

					Hashtable htapp=(Hashtable)Application["appconf"];
					string strcons=(string)htapp["cons"];
					StoBusi=new BusiComm.StorageBusi(strcons);
					try
					{
						double StorageCount=StoBusi.QueryCurrentProductStorage("FYZX1",e.Item.Cells[0].Text.Trim());
						if(StorageCount<double.Parse(outcount)*StandCount)
						{
							this.SetErrorMsgPageBydirHistory(e.Item.Cells[1].Text.Trim()+" 库存量不足以发货！");
							return;
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
						return;
					}
					
					dtdetail=(DataTable)Session["ReceiveDetail"];
					dtdetail.Rows[e.Item.ItemIndex]["cnnOutCount"]=outcount;
					Session.Remove("ReceiveDetail");
					Session["ReceiveDetail"]=dtdetail;
					this.DataGrid1.EditItemIndex=-1;
					this.DataGrid1.DataSource=dtdetail;
					this.DataGrid1.DataBind();
					break;
				case "1":
					string outcount1=e.Item.Cells[8].Text.Trim();
					string Losecount=((TextBox)e.Item.Cells[9].Controls[0]).Text.Trim();
					string realcount=((TextBox)e.Item.Cells[10].Controls[0]).Text.Trim();
					if(realcount==""||!Regex.IsMatch(realcount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						this.SetErrorMsgPageBydirHistory("实际领用量必须是数字！");
						return;
					}
					if(Losecount==""||!Regex.IsMatch(Losecount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						this.SetErrorMsgPageBydirHistory("损耗量必须是数字！");
						return;
					}

					if(double.Parse(outcount1)-double.Parse(Losecount)!=double.Parse(realcount))
					{
						this.SetErrorMsgPageBydirHistory("发货量应等于损耗量与实际领用量之和！");
						return;
					}
					
					dtdetail=(DataTable)Session["ReceiveDetail"];
					dtdetail.Rows[e.Item.ItemIndex]["cnnLoseCount"]=Losecount;
					dtdetail.Rows[e.Item.ItemIndex]["cnnCount"]=realcount;
					Session.Remove("ReceiveDetail");
					Session["ReceiveDetail"]=dtdetail;
					this.DataGrid1.EditItemIndex=-1;
					this.DataGrid1.DataSource=dtdetail;
					this.DataGrid1.DataBind();
					break;
			}
		}

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["ReceiveDetail"];
			this.DataGrid1.DataBind();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			switch(this.txtBillState.Text.Trim())
			{
				case "":
					Response.Redirect("wfmBillOfReceive.aspx");
					break;
				case "0":
					Response.Redirect("wfmBillOfReceiveSend.aspx");
					break;
				case "1":
					Response.Redirect("wfmBillOfReceive.aspx");
					break;
				case "2":
					Response.Redirect("wfmBillOfReceive.aspx");
					break;
			}
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["ReceiveDetail"];
			this.DataGrid1.DataBind();
		}

		private void btnQueryFilter_Click(object sender, System.EventArgs e)
		{
			this.ddlProduct.Items.Clear();
			this.FillDropDownList("AllMaterial",this.ddlProduct,"cnvcProductClass='"+this.ddlMaterialType.SelectedValue+"' and vcCommName like '%"+this.txtMaterialFilter.Text.Trim()+"%'");
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
}
