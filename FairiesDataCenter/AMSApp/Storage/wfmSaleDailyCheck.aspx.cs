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
	/// Summary description for wfmSaleDailyCheck.
	/// </summary>
	public class wfmSaleDailyCheck : wfmBase
	{
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblCheckDate;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox Textbox1;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox Textbox2;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnCheckOk;
		protected System.Web.UI.WebControls.Button btnreset;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
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
					if(ls1.strDeptID!="CEN00")
					{
						this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlDept.Enabled=false;
					}
					this.FillDropDownList("tbNameCodeToStorage",ddlProductType, "vcCommSign ='PRODUCTTYPE'");
					this.lblCheckDate.Text=DateTime.Now.ToLongDateString();
					this.FillDropDownList("PClass",ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.btnCheckOk.Enabled=false;
					this.btnreset.Enabled=false;
					this.btnEdit.Enabled=false;
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.btnreset.Click += new System.EventHandler(this.btnreset_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnCheckOk.Click += new System.EventHandler(this.btnCheckOk_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("page_view");
			string strDeptID=this.ddlDept.SelectedValue;
			string strDate=DateTime.Now.ToShortDateString();
			string strPType=this.ddlProductType.SelectedValue;
			string strPClass=this.ddlProductClass.SelectedValue;

			string strOldDept="";
			if(strPType=="FINALPRODUCT")
			{
				DataTable deptmap=(DataTable)Application["DeptMapInfo"];
				for(int i=0;i<deptmap.Rows.Count;i++)
				{
					if(deptmap.Rows[i]["cnvcNewDeptID"].ToString()==strDeptID)
					{
						strOldDept+=deptmap.Rows[i]["cnvcOldDeptID"].ToString()+"','";
					}
				}
				if(strOldDept.Substring(strOldDept.Length-3,3)=="','")
				{
					strOldDept=strOldDept.Substring(0,strOldDept.Length-3);
				}
			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strOldDept",strOldDept);
			htPara.Add("strDate",strDate);
//			htPara.Add("strYestoday",strYestoday);
			htPara.Add("strPType",strPType);
			htPara.Add("strPClass",strPClass);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetCheckData(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					dtout.TableName="销售日盘点";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					if(dtout.Rows.Count>0)
					{
						this.btnEdit.Enabled=true;
					}
					else
					{
						this.btnEdit.Enabled=false;
					}
				}

				this.btnCheckOk.Enabled=false;
				this.btQuery.Enabled=false;
				this.btnreset.Enabled=true;
				this.ddlDept.Enabled=false;
				this.ddlProductType.Enabled=false;
				this.ddlProductClass.Enabled=false;

				if(strDeptID=="FYZX1")
				{
					this.DataGrid1.Columns[7].Visible=false;
					this.DataGrid1.Columns[8].Visible=false;
					this.DataGrid1.Columns[12].Visible=false;
					this.DataGrid1.Columns[6].Visible=true;
					this.DataGrid1.Columns[11].Visible=true;
				}
				else
				{
					this.DataGrid1.Columns[7].Visible=true;
					this.DataGrid1.Columns[8].Visible=true;
					this.DataGrid1.Columns[12].Visible=true;
					this.DataGrid1.Columns[6].Visible=false;
					this.DataGrid1.Columns[11].Visible=false;
				
				}
				this.DataGrid1.Columns[14].Visible=true;
				this.DataGrid1.Columns[15].Visible=false;
				
//				this.DataGrid1.PageSize = 30;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.btnCheckOk.Enabled=false;
			this.ddlProductClass.Items.Clear();
			this.FillDropDownList("PClass",ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
		}

//		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=-1;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
//		{
//			DataTable dttmp=(DataTable)Session["QUERY"];
//			Session.Remove("QUERY");
//			double SystemCount=Math.Round(double.Parse(e.Item.Cells[13].Text),2);
//			double RealCount=Math.Round(double.Parse(((TextBox)e.Item.Cells[14].Controls[0]).Text.Trim()),2);
//
//			dttmp.Rows[e.Item.ItemIndex]["cnnRealCount"]=RealCount.ToString();
//			dttmp.Rows[e.Item.ItemIndex]["cnnDifferentCount"]=(SystemCount-RealCount).ToString();
//			Session["QUERY"]=dttmp;
//			this.DataGrid1.EditItemIndex=-1;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}

		private void btnCheckOk_Click(object sender, System.EventArgs e)
		{
			if(this.Textbox2.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("盘点人不能为空！");
				return;
			}

			string strDeptID=this.ddlDept.SelectedValue;
			string strDate=DateTime.Now.ToShortDateString();
			string strWeather=this.Textbox1.Text.Trim();
			string strCheckOper=this.Textbox2.Text.Trim();
			string strManager=this.Textbox3.Text.Trim();
			string strProductType=this.ddlProductType.SelectedValue;

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			string strOperDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strDate",strDate);
			htPara.Add("strWeather",strWeather);
			htPara.Add("strCheckOper",strCheckOper);
			htPara.Add("strManager",strManager);
			htPara.Add("strOperName",ls1.strOperName);
			htPara.Add("strOperDate",strOperDate);
			htPara.Add("strProductType",strProductType);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.DayCheckFinal(htPara,(DataTable)Session["QUERY"]))
				{
					this.DataGrid1.Columns[14].Visible=true;
					this.DataGrid1.Columns[15].Visible=false;
					this.btnCheckOk.Enabled=false;
					this.btnEdit.Enabled=false;
					this.btnEdit.Text="编辑";
					this.SetSuccMsgPageBydir("盘点成功！","Storage/wfmSaleDailyCheck.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("盘点时发生错误，请重试！");
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

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.btnCheckOk.Enabled=false;
			if(this.ddlDept.SelectedValue=="FYZX1")
			{
				this.ddlProductType.Items.Remove(this.ddlProductType.Items.FindByValue("FINALPRODUCT"));
				this.ddlProductType.Items.Remove(this.ddlProductType.Items.FindByValue("SEMIPRODUCT"));
				this.ddlProductClass.Items.Clear();
				this.FillDropDownList("PClass",ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
			}
			else
			{
				this.ddlProductType.Items.Clear();
				this.FillDropDownList("tbNameCodeToStorage",ddlProductType, "vcCommSign ='PRODUCTTYPE'");
				this.ddlProductClass.Items.Clear();
				this.FillDropDownList("PClass",ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'");
			}
		}

		private void btnreset_Click(object sender, System.EventArgs e)
		{
			this.btQuery.Enabled=true;
			this.btnreset.Enabled=false;
			this.btnCheckOk.Enabled=false;
			this.btnEdit.Enabled=false;
			this.ddlDept.Enabled=true;
			this.ddlProductType.Enabled=true;
			this.ddlProductClass.Enabled=true;

			DataTable dtout=new DataTable();
			dtout.Columns.Add("cnvcProductCode");
			dtout.Columns.Add("cnvcProductName");
			dtout.Columns.Add("cnnProductPrice");
			dtout.Columns.Add("cnvcUnit");
			dtout.Columns.Add("cnnOriginalStorage");
			dtout.Columns.Add("cnnOrderCount");
			dtout.Columns.Add("cnnMoveOutCount");
			dtout.Columns.Add("cnnMoveInCount");
			dtout.Columns.Add("cnnLoseCount");
			dtout.Columns.Add("cnnFreeCount");
			dtout.Columns.Add("cnnUseCount");
			dtout.Columns.Add("cnnSellCount");
			dtout.Columns.Add("cnnSystemCount");
			dtout.Columns.Add("cnnRealCount");
			dtout.Columns.Add("cnnDifferentCount");
			dtout.Columns.Add("cnnDifferentSum");
			this.DataGrid1.DataSource=dtout;
			this.DataGrid1.DataBind();
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if(btnEdit.Text=="编辑")
			{
				this.DataGrid1.Columns[14].Visible=false;
				this.DataGrid1.Columns[15].Visible=true;
				for(int i=0;i<this.DataGrid1.Items.Count;i++)
				{
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[15].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt1=con1 as TextBox;
							txt1.Text=this.DataGrid1.Items[i].Cells[14].Text.Trim();
						}
					}
				}
				btnEdit.Text="锁定编辑";
				this.btnCheckOk.Enabled=false;
			}
			else if(btnEdit.Text=="锁定编辑")
			{
				DataTable dtCheckList=(DataTable)Session["QUERY"];
				for(int i=0;i<this.DataGrid1.Items.Count;i++)
				{
					foreach(System.Web.UI.Control con1 in this.DataGrid1.Items[i].Cells[15].Controls)
					{
						if(con1 is System.Web.UI.WebControls.TextBox)
						{
							TextBox txt1=con1 as TextBox;
							dtCheckList.Rows[i]["cnnRealCount"]=txt1.Text.Trim();
						}
					}
				}
				btnEdit.Text="编辑";
				this.btnCheckOk.Enabled=true;

				Session.Remove("QUERY");
				Session["QUERY"]=dtCheckList;
				this.DataGrid1.DataSource=dtCheckList;
				this.DataGrid1.DataBind();
				this.DataGrid1.Columns[14].Visible=true;
				this.DataGrid1.Columns[15].Visible=false;
			}
		}
	}
}
