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
	/// Summary description for wfmStockPlanDept.
	/// </summary>
	public class wfmStockPlanDept : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.TextBox txtCount;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.DropDownList ddlProduct;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Button btnNewPlan;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtUnit;

		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					string strvalue="";
//					for(int i=0;i<6;i++)
//					{
//						if(DateTime.Now.AddMonths(-i).Month<10)
//						{
//							strvalue=DateTime.Now.Year+"0"+(DateTime.Now.AddMonths(-i).Month).ToString();
//						}
//						else
//						{
//							strvalue=DateTime.Now.Year+(DateTime.Now.AddMonths(-i).Month).ToString();
//						}
//						this.ddlMonth.Items.Add(new ListItem(strvalue,strvalue));
//					}
					if(DateTime.Now.Month<10)
					{
						strvalue=DateTime.Now.Year+"0"+(DateTime.Now.Month).ToString();
					}
					else
					{
						strvalue=DateTime.Now.Year+(DateTime.Now.Month).ToString();
					}
					this.ddlMonth.Items.Add(new ListItem(strvalue,strvalue));

					this.FillDropDownList("NewDept",this.ddlDept);
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlDept.Enabled=false;
					}
					this.FillDropDownList("AllMaterial",this.ddlProduct);
					DataTable dtmaterial=(DataTable)Application["AllMaterial"];
					DataView dview=dtmaterial.DefaultView;
					dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
					this.txtUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
					DataTable dtout=new DataTable();
					dtout.Columns.Add("cnvcProductCode");
					dtout.Columns.Add("cnvcProductName");
					dtout.Columns.Add("cnnPlanCount");
					dtout.Columns.Add("cnvcStandardUnit");
					dtout.Columns.Add("cnvcMonth");
					dtout.Columns.Add("cnvcPlanDeptID");
					dtout.Columns.Add("cnvcPlanDeptName");
					dtout.Columns.Add("DelFlag");
					Session["PalnDetail"]=dtout;
					this.DataGrid1.DataSource=dtout;
					this.DataGrid1.DataBind();
					this.btnAdd.Enabled=false;
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
			this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnNewPlan.Click += new System.EventHandler(this.btnNewPlan_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			DataTable dtIn=(DataTable)Session["PalnDetail"];
			string strProductCode=this.ddlProduct.SelectedValue;
			string strPlanDeptID=this.ddlDept.SelectedValue;
			bool dupproflag=false;
			for(int i=0;i<dtIn.Rows.Count;i++)
			{
				if(dtIn.Rows[i]["cnvcProductCode"].ToString()==strProductCode&&dtIn.Rows[i]["cnvcPlanDeptID"].ToString()==strPlanDeptID)
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
					this.SetErrorMsgPageBydirHistory("预估总用量必须是数字！");
					return;
				}
				else
				{
					drnew["cnnPlanCount"]=this.txtCount.Text.Trim();
				}

				if(this.txtUnit.Text.Trim()=="")
				{
					this.SetErrorMsgPageBydirHistory("单位不能为空！");
					return;
				}
				else
				{
					drnew["cnvcStandardUnit"]=this.txtUnit.Text.Trim();
				}

				drnew["cnvcMonth"]=this.ddlMonth.SelectedItem.Text;
				drnew["cnvcPlanDeptID"]=this.ddlDept.SelectedValue;
				drnew["cnvcPlanDeptName"]=this.ddlDept.SelectedItem.Text;
				drnew["DelFlag"]="0";

				dtIn.Rows.Add(drnew);
				Session.Remove("PalnDetail");
				Session["PalnDetail"]=dtIn;
				this.DataGrid1.DataSource=dtIn;
				this.DataGrid1.DataBind();

				for(int i=0;i<dtIn.Rows.Count;i++)
				{
					if(dtIn.Rows[i]["DelFlag"].ToString()=="1")
					{
						((LinkButton)this.DataGrid1.Items[i].Cells[7].Controls[0]).Visible=false;
					}
				}
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dtIn=(DataTable)Session["PalnDetail"];
			int dataindex=e.Item.ItemIndex;
			dtIn.Rows.RemoveAt(dataindex);
			Session.Remove("PalnDetail");
			Session["PalnDetail"]=dtIn;
			this.DataGrid1.DataSource=dtIn;
			this.DataGrid1.DataBind();
			for(int i=0;i<dtIn.Rows.Count;i++)
			{
				if(dtIn.Rows[i]["DelFlag"].ToString()=="1")
				{
					((LinkButton)this.DataGrid1.Items[i].Cells[7].Controls[0]).Visible=false;
				}
			}
		}

		private void btnNewPlan_Click(object sender, System.EventArgs e)
		{
			DataTable dtDetail=(DataTable)Session["PalnDetail"];
			if(dtDetail.Rows.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("采购计划产品为空，请先添加产品！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.NewStockPlanAdd(dtDetail))
				{
					this.SetSuccMsgPageBydir("新采购计划录入成功！","Storage/wfmStockPlan.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("新采购计划录入时发生错误，请重试！");
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

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			string strDeptID=this.ddlDept.SelectedValue;
			string strMonth=this.ddlMonth.SelectedItem.Text;

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strMonth",strMonth);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetPlanDeptDetail(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				this.TableConvert(dtout,"cnvcPlanDeptName","NewDept","vcCommSign='SalesRoom'");
				Session.Remove("PalnDetail");
				Session["PalnDetail"]=dtout;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
				for(int i=0;i<dtout.Rows.Count;i++)
				{
					if(dtout.Rows[i]["DelFlag"].ToString()=="1")
					{
						((LinkButton)this.DataGrid1.Items[i].Cells[7].Controls[0]).Visible=false;
					}
				}

				this.btnQuery.Enabled=false;
				this.btnAdd.Enabled=true;
				this.ddlDept.Enabled=false;
				this.ddlMonth.Enabled=false;
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			this.ddlDept.Enabled=true;
			this.ddlMonth.Enabled=true;
			DataTable dtout=new DataTable();
			dtout.Columns.Add("cnvcProductCode");
			dtout.Columns.Add("cnvcProductName");
			dtout.Columns.Add("cnnPlanCount");
			dtout.Columns.Add("cnvcStandardUnit");
			dtout.Columns.Add("cnvcMonth");
			dtout.Columns.Add("cnvcPlanDeptID");
			dtout.Columns.Add("cnvcPlanDeptName");
			dtout.Columns.Add("DelFlag");
			Session.Remove("PalnDetail");
			Session["PalnDetail"]=dtout;
			this.DataGrid1.DataSource=dtout;
			this.DataGrid1.DataBind();
			this.btnQuery.Enabled=true;
			this.ddlDept.Enabled=true;
			this.ddlMonth.Enabled=true;
		}

		private void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtmaterial=(DataTable)Application["AllMaterial"];
			DataView dview=dtmaterial.DefaultView;
			dview.RowFilter="vcCommCode='"+this.ddlProduct.SelectedValue+"'";
			this.txtUnit.Text=dview[0]["cnvcStandardUnit"].ToString();
		}
	}
}
