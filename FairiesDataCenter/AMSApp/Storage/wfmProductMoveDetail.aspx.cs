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
	/// Summary description for wfmProductMoveDetail.
	/// </summary>
	public class wfmProductMoveDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlProduct;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.DropDownList ddlOutDept;
		protected System.Web.UI.WebControls.DropDownList ddlInDept;
		protected System.Web.UI.WebControls.TextBox txtOutOperID;
		protected System.Web.UI.WebControls.TextBox txtInOperID;
		protected System.Web.UI.WebControls.Button btnMoveNew;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		BusiComm.StorageBusi StoBusi;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.TextBox txtBillState;
		protected System.Web.UI.WebControls.TextBox txtMoveID;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtProductFilter;
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
			this.txtUnit.Enabled=false;
			string strMoveID=Request.QueryString["ID"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			if(!IsPostBack)
			{
				if(strMoveID==""||strMoveID==null)
				{
					strBeginDate=DateTime.Now.ToShortDateString();
					this.btnAdd.Enabled=true;
					this.FillDropDownList("NewDept",this.ddlInDept,"vcCommSign='SalesRoom'");
					this.FillDropDownList("NewDept",this.ddlOutDept,"vcCommSign='SalesRoom'");
					this.FillDropDownList("Goods",this.ddlProduct);

					string strProductCode=this.ddlProduct.SelectedValue;
					try
					{
						string strUnit=StoBusi.GetGoodsUnit(strProductCode);
						if(strUnit==null||strUnit=="")
						{
							this.txtUnit.Text="不存在";
						}
						else
						{
							this.txtUnit.Text=strUnit;
						}
					}
					catch(Exception er)
					{
						this.clog.WriteLine(er);
						this.SetErrorMsgPageBydir("查询错误，请重试！");
						return;
					}

					DataTable dtout=new DataTable();
					dtout.Columns.Add("cnvcProductCode");
					dtout.Columns.Add("cnvcProductName");
					dtout.Columns.Add("cnvcUnit");
					dtout.Columns.Add("cnnMoveCount");
					dtout.Columns.Add("cnnLoseCount");
					dtout.Columns.Add("cnnRealMoveCount");
					dtout.Columns.Add("cnvcComments");
					Session["MoveDetail"]=dtout;
					this.DataGrid1.DataSource=dtout;
					this.DataGrid1.DataBind();
					this.DataGrid1.Columns[5].Visible=false;
					this.DataGrid1.Columns[6].Visible=false;
					this.DataGrid1.Columns[7].Visible=false;
					this.btnPrint.Enabled=false;
					this.txtInOperID.Enabled=false;
				}
				else
				{
					this.txtMoveID.Text=strMoveID;
					this.btnAdd.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlInDept,"vcCommSign='SalesRoom'");
					this.FillDropDownList("NewDept",this.ddlOutDept,"vcCommSign='SalesRoom'");
					DataSet dsout=StoBusi.GetMoveOneLog(strMoveID);
					DataTable dtLog=dsout.Tables["MoveLog"];
					DataTable dtDetail=dsout.Tables["MoveDetail"];
					Session["MoveDetail"]=dtDetail;

					this.ddlInDept.SelectedIndex=this.ddlInDept.Items.IndexOf(this.ddlInDept.Items.FindByValue(dtLog.Rows[0]["cnvcInDeptID"].ToString()));
					this.ddlOutDept.SelectedIndex=this.ddlOutDept.Items.IndexOf(this.ddlOutDept.Items.FindByValue(dtLog.Rows[0]["cnvcOutDeptID"].ToString()));
					
					Session.Remove("BillPrint");
					DataSet dsprint=new DataSet("面包工坊调拨单");
					dtLog.Rows[0]["cnvcInDeptID"]=this.ddlInDept.SelectedItem.Text;
					dtLog.Rows[0]["cnvcOutDeptID"]=this.ddlOutDept.SelectedItem.Text;
					dsprint.Tables.Add(dtLog.Copy());
					dsprint.Tables.Add(dtDetail.Copy());
					Session["BillPrint"]=dsprint;

					strBeginDate=dtLog.Rows[0]["cndMoveDate"].ToString();
					this.txtInOperID.Text=dtLog.Rows[0]["cnvcInOperID"].ToString();
					this.txtOutOperID.Text=dtLog.Rows[0]["cnvcOutOperID"].ToString();
					this.txtBillState.Text=dtLog.Rows[0]["cnvcBillState"].ToString();
					this.ddlInDept.Enabled=false;
					this.ddlOutDept.Enabled=false;
					this.txtOutOperID.Enabled=false;

					this.ddlProduct.Enabled=false;
					this.txtCount.Enabled=false;
					this.txtComments.Enabled=false;
					this.btnPrint.Enabled=true;

					switch(this.txtBillState.Text.Trim())
					{
						case "0":
							this.btnMoveNew.Text="调拨单-收货";
							((BoundColumn)this.DataGrid1.Columns[4]).ReadOnly=true;
							this.DataGrid1.Columns[8].Visible=false;
							this.btnMoveNew.Attributes.Add("onclick","return confirm('请确认你已经正确填写了\"损耗量和实际数量\"，并要进行本调拨单收货确认。\\n\\n确认收货？');");
							break;
						case "1":
							this.btnMoveNew.Text="调拨单";
							this.btnMoveNew.Enabled=false;
							this.txtInOperID.ReadOnly=false;
							this.DataGrid1.Columns[7].Visible=false;
							this.DataGrid1.Columns[8].Visible=false;
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
			this.btnMoveNew.Click += new System.EventHandler(this.btnMoveNew_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnQueryFilter.Click += new System.EventHandler(this.btnQueryFilter_Click);
			this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnMoveNew_Click(object sender, System.EventArgs e)
		{
			DataTable dtDetail=(DataTable)Session["MoveDetail"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			Hashtable htpara=new Hashtable();
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];

			switch(this.txtBillState.Text.Trim())
			{
				case "":
					#region 新调拨单录入出货
					if(dtDetail.Rows.Count==0)
					{
						this.SetErrorMsgPageBydirHistory("调拨产品为空，请先添加产品！");
						return;
					}
					string strOutDeptID=this.ddlOutDept.SelectedValue;
					string strInDeptID=this.ddlInDept.SelectedValue;
					string strMoveDate=strBeginDate;
					string strOutOperID=this.txtOutOperID.Text.Trim();
					string strInOperID=this.txtInOperID.Text.Trim();
					string strOperDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
					string strOperID=ls1.strOperName;

					if(strOutOperID=="")
					{
						this.SetErrorMsgPageBydirHistory("调出人不能为空！");
						return;
					}

					htpara.Add("strOutDeptID",strOutDeptID);
					htpara.Add("strInDeptID",strInDeptID);
					htpara.Add("strMoveDate",strMoveDate);
					htpara.Add("strOutOperID",strOutOperID);
					htpara.Add("strInOperID",strInOperID);
					htpara.Add("strOperDate",strOperDate);
					htpara.Add("strOperID",strOperID);

					try
					{
						if(StoBusi.NewProductMoveAdd(htpara,dtDetail))
						{
							this.SetSuccMsgPageBydir("新调拨单录入成功！","Storage/wfmProductMove.aspx");
						}
						else
						{
							this.SetErrorMsgPageBydir("新调拨单录入时发生错误，请重试！");
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
					#region 调拨单--验收入库
					if(this.txtInOperID.Text.Trim()=="")
					{
						this.SetErrorMsgPageBydirHistory("调入人不能为空！");
						return;
					}
					htpara.Add("MoveID",this.txtMoveID.Text.Trim());
					htpara.Add("strInDeptID",this.ddlInDept.SelectedValue);
					htpara.Add("strMoveInOper",this.txtInOperID.Text.Trim());
					htpara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
					htpara.Add("strOperID",ls1.strOperName);
					try
					{
						if(StoBusi.UpdateProductMoveValidEnter(htpara,dtDetail))
						{
							this.SetSuccMsgPageBydir("调拨单收货成功！","Storage/wfmProductMove.aspx");
						}
						else
						{
							this.SetErrorMsgPageBydir("调拨单收货时发生错误，请重试！");
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
			string strUnit=this.txtUnit.Text.Trim();
			if(strUnit==""||strUnit=="不存在")
			{
				this.SetErrorMsgPageBydirHistory("商品的单位不存在，请检查！");
				return;
			}
			if(this.ddlInDept.SelectedValue==this.ddlOutDept.SelectedValue)
			{
				this.SetErrorMsgPageBydirHistory("不能在同一个店之间进行调拨！");
				return;
			}
			DataTable dtIn=(DataTable)Session["MoveDetail"];
			string strProductCode=this.ddlProduct.SelectedValue;
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

				if(this.txtCount.Text.Trim()==""||!Regex.IsMatch(this.txtCount.Text.Trim(),@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					this.SetErrorMsgPageBydirHistory("数量必须是数字！");
					return;
				}
				else
				{
					drnew["cnnMoveCount"]=this.txtCount.Text.Trim();
				}

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					if(StoBusi.QueryCurrentProductStorage(this.ddlOutDept.SelectedValue,drnew["cnvcProductCode"].ToString())<=double.Parse(drnew["cnnMoveCount"].ToString()))
					{
						this.SetErrorMsgPageBydirHistory(drnew["cnvcProductName"].ToString()+" 库存量不足以发货！");
						return;
					}
				}
				catch(Exception er)
				{
					this.clog.WriteLine(er);
					this.SetErrorMsgPageBydir("查询错误，请重试！");
					return;
				}

				drnew["cnnLoseCount"]="0";
				drnew["cnnRealMoveCount"]="0";

				drnew["cnvcComments"]=this.txtComments.Text.Trim();
				drnew["cnvcUnit"]=this.txtUnit.Text.Trim();

				dtIn.Rows.Add(drnew);
				this.ddlOutDept.Enabled=false;
				this.ddlInDept.Enabled=false;
				Session.Remove("MoveDetail");
				Session["MoveDetail"]=dtIn;
				this.DataGrid1.DataSource=dtIn;
				this.DataGrid1.DataBind();
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtIn=(DataTable)Session["MoveDetail"];
			int dataindex=e.Item.ItemIndex;
			dtIn.Rows.RemoveAt(dataindex);
			Session.Remove("MoveDetail");
			Session["MoveDetail"]=dtIn;
			this.DataGrid1.DataSource=dtIn;
			this.DataGrid1.DataBind();
		}

		private void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strProductCode=this.ddlProduct.SelectedValue;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				string strUnit=StoBusi.GetGoodsUnit(strProductCode);
				if(strUnit==null||strUnit=="")
				{
					this.txtUnit.Text="不存在";
				}
				else
				{
					this.txtUnit.Text=strUnit;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmCommPrint.aspx?type=MoveBill");
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("wfmProductMove.aspx");
		}

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["MoveDetail"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtdetail=new DataTable();
			switch(this.txtBillState.Text.Trim())
			{
				case "0":
					string outcount1=e.Item.Cells[4].Text.Trim();
					string Losecount=((TextBox)e.Item.Cells[5].Controls[0]).Text.Trim();
					string realcount=((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim();
					if(realcount==""||!Regex.IsMatch(realcount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						this.SetErrorMsgPageBydirHistory("实际数量必须是数字！");
						return;
					}
					if(Losecount==""||!Regex.IsMatch(Losecount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
					{
						this.SetErrorMsgPageBydirHistory("损耗量必须是数字！");
						return;
					}

					if(double.Parse(outcount1)-double.Parse(Losecount)!=double.Parse(realcount))
					{
						this.SetErrorMsgPageBydirHistory("发货量应等于损耗量与实际数量之和！");
						return;
					}
					
					dtdetail=(DataTable)Session["MoveDetail"];
					dtdetail.Rows[e.Item.ItemIndex]["cnnLoseCount"]=Losecount;
					dtdetail.Rows[e.Item.ItemIndex]["cnnRealMoveCount"]=realcount;
					Session.Remove("MoveDetail");
					Session["MoveDetail"]=dtdetail;
					this.DataGrid1.EditItemIndex=-1;
					this.DataGrid1.DataSource=dtdetail;
					this.DataGrid1.DataBind();
					break;
			}
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["MoveDetail"];
			this.DataGrid1.DataBind();
		}

		private void btnQueryFilter_Click(object sender, System.EventArgs e)
		{
			this.FillDropDownList("Goods",this.ddlProduct,"vcCommName like '%"+this.txtProductFilter.Text.Trim()+"%'");

			string strProductCode=this.ddlProduct.SelectedValue;
			try
			{
				string strUnit=StoBusi.GetGoodsUnit(strProductCode);
				if(strUnit==null||strUnit=="")
				{
					this.txtUnit.Text="不存在";
				}
				else
				{
					this.txtUnit.Text=strUnit;
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
