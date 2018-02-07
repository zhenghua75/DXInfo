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
using System.Text.RegularExpressions;
using CommCenter;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmSaleLoseNew.
	/// </summary>
	public class wfmSaleLoseNew : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlProductName;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label lblLoseDate;
		protected System.Web.UI.WebControls.Button btnLoseAdd;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtWeather;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlLoseType;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label lblUnit;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.Button btnAllOK;

		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept",ddlDept);
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlDept.Enabled=false;
					}
					this.lblLoseDate.Text=DateTime.Now.ToLongDateString();
					this.FillDropDownList("PClass",ddlProductClass,"vcCommSign='FINALPRODUCT'");
					this.ddlLoseType.Items.Add(new ListItem("生产损耗","生产损耗"));
					this.ddlLoseType.Items.Add(new ListItem("销售中损耗","销售中损耗"));
					this.ddlLoseType.Items.Add(new ListItem("销售剩余损耗","销售剩余损耗"));
					this.ddlLoseType.SelectedIndex=0;

					this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE'");
					this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
					string strTableName="";
					if(this.ddlProductType.SelectedValue=="FINALPRODUCT"||this.ddlProductType.SelectedValue=="SEMIPRODUCT")
					{
						strTableName="tbFormula";
					}
					if(this.ddlProductType.SelectedValue=="Pack"||this.ddlProductType.SelectedValue=="Raw")
					{
						strTableName="AllMaterial";
					}
					
					this.FillDropDownList(strTableName,this.ddlProductName,"cnvcProductClass='"+this.ddlProductClass.SelectedValue+"'");
					if(this.ddlProductName.Items.Count>0)
					{
						DataTable dtproduct=(DataTable)Application[strTableName];
						DataView dview=dtproduct.DefaultView;
						dview.RowFilter="vcCommCode='"+this.ddlProductName.SelectedValue+"'";
						this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
					}
					else
					{
						this.lblUnit.Text="无";
					}

					DataTable dtLoseList=new DataTable();
					dtLoseList.Columns.Add("cnvcDeptID");
					dtLoseList.Columns.Add("cnvcDeptName");
					dtLoseList.Columns.Add("cnvcProductCode");
					dtLoseList.Columns.Add("cnvcProductName");
					dtLoseList.Columns.Add("cndLoseDate");
					dtLoseList.Columns.Add("cnvcWeather");
					dtLoseList.Columns.Add("cnnLoseCount");
					dtLoseList.Columns.Add("cnvcUnit");
					dtLoseList.Columns.Add("cnvcLoseComments");
					dtLoseList.Columns.Add("cnvcLoseType");
					dtLoseList.Columns.Add("cnvcOperID");
					dtLoseList.Columns.Add("cndLoseOperDate");
					Session.Remove("NewLoseList");
					Session["NewLoseList"]=dtLoseList;
					this.DataGrid1.PageSize = 20;
					this.DataGrid1.DataSource=dtLoseList;
					this.DataGrid1.DataBind();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.ddlProductClass.SelectedIndexChanged += new System.EventHandler(this.ddlProductClass_SelectedIndexChanged);
			this.ddlProductName.SelectedIndexChanged += new System.EventHandler(this.ddlProductName_SelectedIndexChanged);
			this.btnLoseAdd.Click += new System.EventHandler(this.btnLoseAdd_Click);
			this.btnAllOK.Click += new System.EventHandler(this.btnAllOK_Click);
			this.DataGrid1.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemCreated);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlProductClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlProductName.Items.Clear();
			string strTableName="";
			if(this.ddlProductType.SelectedValue=="FINALPRODUCT"||this.ddlProductType.SelectedValue=="SEMIPRODUCT")
			{
				strTableName="tbFormula";
			}
			if(this.ddlProductType.SelectedValue=="Pack"||this.ddlProductType.SelectedValue=="Raw")
			{
				strTableName="AllMaterial";
			}
					
			this.FillDropDownList(strTableName,this.ddlProductName,"cnvcProductClass='"+this.ddlProductClass.SelectedValue+"'");
			if(this.ddlProductName.Items.Count>0)
			{
				DataTable dtproduct=(DataTable)Application[strTableName];
				DataView dview=dtproduct.DefaultView;
				dview.RowFilter="vcCommCode='"+this.ddlProductName.SelectedValue+"'";
				this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
			}
			else
			{
				this.lblUnit.Text="无";
			}
		}

		private void btnLoseAdd_Click(object sender, System.EventArgs e)
		{
			if(this.ddlProductName.Items.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("产品不可为空！");
				return;
			}

			string strCount=this.txtCount.Text.Trim();
			if(strCount==""||!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("损耗数量必须为数字！");
				return;
			}

			string strUnit=this.lblUnit.Text.Trim();
			if(strUnit==""||strUnit=="无")
			{
				this.SetErrorMsgPageBydirHistory("该产品的单位不存在，请先更新产品单位！");
				return;
			}
			
			bool existflag=false;
			string strDeptID=this.ddlDept.SelectedValue;
			string strProductCode=this.ddlProductName.SelectedValue;
			string strLoseType=this.ddlLoseType.SelectedValue;
			DataTable dtLoseList=(DataTable)Session["NewLoseList"];
			foreach(DataRow drtmp in dtLoseList.Rows)
			{
				if(drtmp["cnvcDeptID"].ToString()==strDeptID&&drtmp["cnvcProductCode"].ToString()==strProductCode&&drtmp["cnvcLoseType"].ToString()==strLoseType)
				{
					drtmp["cnnLoseCount"]=(Math.Round(double.Parse(drtmp["cnnLoseCount"].ToString()),2)+Math.Round(double.Parse(strCount),2)).ToString();
					existflag=true;
					break;
				}
			}

			if(!existflag)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			
				DataRow drnew=dtLoseList.NewRow();
				drnew["cnvcDeptID"]=strDeptID;
				drnew["cnvcDeptName"]=this.ddlDept.SelectedItem.Text.Trim();
				drnew["cnvcProductCode"]=strProductCode;
				drnew["cnvcProductName"]=this.ddlProductName.SelectedItem.Text;
				drnew["cndLoseDate"]=DateTime.Now.ToShortDateString();
				drnew["cnvcWeather"]=this.txtWeather.Text.Trim();
				drnew["cnnLoseCount"]=strCount;
				drnew["cnvcUnit"]=strUnit;
				drnew["cnvcLoseComments"]=this.txtComments.Text.Trim();
				drnew["cnvcLoseType"]=this.ddlLoseType.SelectedValue;
				drnew["cnvcOperID"]=ls1.strOperName;
				drnew["cndLoseOperDate"]=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
				dtLoseList.Rows.Add(drnew);
			}
			Session.Remove("NewLoseList");
			Session["NewLoseList"]=dtLoseList;
			this.DataGrid1.PageSize = 20;
			this.DataGrid1.DataSource=dtLoseList;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_ItemCreated(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem|e.Item.ItemType==ListItemType.EditItem)
			{
				TableCell tcell1=e.Item.Cells[12];
				LinkButton btnDel=(LinkButton)tcell1.Controls[0];
				btnDel.Attributes.Add("onclick","return confirm('你确定的要删除此记录吗？');");
				btnDel.Text="删除";
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtLoseList=(DataTable)Session["NewLoseList"];
			string strDeptID=e.Item.Cells[0].Text.Trim();
			string strProductCode=e.Item.Cells[2].Text.Trim();
			string strLoseType=e.Item.Cells[9].Text.Trim();
			for(int i=0;i<dtLoseList.Rows.Count;i++)
			{
				if(dtLoseList.Rows[i]["cnvcDeptID"].ToString()==strDeptID&&dtLoseList.Rows[i]["cnvcProductCode"].ToString()==strProductCode&&dtLoseList.Rows[i]["cnvcLoseType"].ToString()==strLoseType)
				{
					dtLoseList.Rows.RemoveAt(i);
					break;
				}
			}
			Session.Remove("NewLoseList");
			Session["NewLoseList"]=dtLoseList;
			this.DataGrid1.PageSize = 20;
			this.DataGrid1.DataSource=dtLoseList;
			this.DataGrid1.DataBind();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmSaleLoseQuery.aspx");
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlProductClass.Items.Clear();
			this.ddlProductName.Items.Clear();
			this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
			string strTableName="";
			if(this.ddlProductType.SelectedValue=="FINALPRODUCT"||this.ddlProductType.SelectedValue=="SEMIPRODUCT")
			{
				strTableName="tbFormula";
			}
			if(this.ddlProductType.SelectedValue=="Pack"||this.ddlProductType.SelectedValue=="Raw")
			{
				strTableName="AllMaterial";
			}
					
			this.FillDropDownList(strTableName,this.ddlProductName,"cnvcProductClass='"+this.ddlProductClass.SelectedValue+"'");
			if(this.ddlProductName.Items.Count>0)
			{
				DataTable dtproduct=(DataTable)Application[strTableName];
				DataView dview=dtproduct.DefaultView;
				dview.RowFilter="vcCommCode='"+this.ddlProductName.SelectedValue+"'";
				this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
			}
			else
			{
				this.lblUnit.Text="无";
			}
		}

		private void ddlProductName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strTableName="";
			if(this.ddlProductType.SelectedValue=="FINALPRODUCT"||this.ddlProductType.SelectedValue=="SEMIPRODUCT")
			{
				strTableName="tbFormula";
			}
			if(this.ddlProductType.SelectedValue=="Pack"||this.ddlProductType.SelectedValue=="Raw")
			{
				strTableName="AllMaterial";
			}

			if(this.ddlProductName.Items.Count>0)
			{
				DataTable dtproduct=(DataTable)Application[strTableName];
				DataView dview=dtproduct.DefaultView;
				dview.RowFilter="vcCommCode='"+this.ddlProductName.SelectedValue+"'";
				this.lblUnit.Text=dview[0]["cnvcUnit"].ToString();
			}
			else
			{
				this.lblUnit.Text="无";
			}
		}

		private void btnAllOK_Click(object sender, System.EventArgs e)
		{
			DataTable dtLoseList=(DataTable)Session["NewLoseList"];
			if(dtLoseList==null||dtLoseList.Rows.Count==0||this.DataGrid1.Items.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("没有录入任何损耗信息，请先录入！");
				return;
			}
			else
			{
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					if(StoBusi.NewSaleLoseAdd(dtLoseList))
					{
						this.SetSuccMsgPageBydir("报损成功！","Storage/wfmSaleLoseQuery.aspx");
						return;
					}
					else
					{
						this.SetErrorMsgPageBydir("报损时发生错误，请重试！");
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
		}
	}
}
