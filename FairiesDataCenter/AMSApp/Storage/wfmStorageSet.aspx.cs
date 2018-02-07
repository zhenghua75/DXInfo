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
	/// Summary description for wfmStorageSet.
	/// </summary>
	public class wfmStorageSet : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.DropDownList ddlQueryType;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
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
					this.ddlQueryType.Items.Add(new ListItem("全部","全部"));
					this.ddlQueryType.Items.Add(new ListItem("正常库存查询","0"));
					this.ddlQueryType.Items.Add(new ListItem("告警库存查询","1"));
					this.ddlQueryType.SelectedIndex=0;

					this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE'");
					this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");
					Session.Remove("QUERY");
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
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlProductClass.Items.Clear();
			this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");
		}

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			string strDeptID=this.ddlDept.SelectedValue;
			string strQueryType=this.ddlQueryType.SelectedValue;
			string strProductType=this.ddlProductType.SelectedValue;
			string strProductClass=this.ddlProductClass.SelectedValue;

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strQueryType",strQueryType);
			htPara.Add("strProductType",strProductType);
			htPara.Add("strProductClass",strProductClass);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetCurSafeStorage(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"cnvcStorageDeptID","NewDept");
					Session["QUERY"] = dtout;
				}
				
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

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			DataTable dttmp=(DataTable)Session["QUERY"];
			Session.Remove("QUERY");
			double StatdardSafeLowCount=Math.Round(double.Parse(((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim()),2);
			double StatdardSafeUpCount=Math.Round(double.Parse(((TextBox)e.Item.Cells[7].Controls[0]).Text.Trim()),2);
			string strProductCode=e.Item.Cells[2].Text.Trim();
			DataTable dtproduct=(DataTable)Application["AllMaterial"];
			DataView dview=dtproduct.DefaultView;
			dview.RowFilter="vcCommCode='"+strProductCode+"'";
			double StatdardCount=Math.Round(double.Parse(dview[0]["cnnStatdardCount"].ToString()),2);
			double SafeLowCount=Math.Round(StatdardSafeLowCount*StatdardCount,2);
			double SafeUpCount=Math.Round(StatdardSafeUpCount*StatdardCount,2);
			
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			string strDeptID=e.Item.Cells[0].Text.Trim();
			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strProductCode",strProductCode);
			htPara.Add("SafeLowCount",SafeLowCount.ToString());
			htPara.Add("SafeUpCount",SafeUpCount.ToString());
			htPara.Add("strOperName",ls1.strOperName);
			htPara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.UpdateProductSafeCount(htPara))
				{
					dttmp.Rows[e.Item.ItemIndex]["cnnSafeCount"]=StatdardSafeLowCount.ToString();
					dttmp.Rows[e.Item.ItemIndex]["cnnSafeUpCount"]=StatdardSafeUpCount.ToString();
					Session["QUERY"]=dttmp;
					this.DataGrid1.EditItemIndex=-1;
					this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
					this.DataGrid1.DataBind();
				}
				else
				{
					this.SetErrorMsgPageBydir("更新安全库存失败！");
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

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.ddlDept.SelectedValue=="FYZX1")
			{
				this.ddlProductType.Items.Clear();
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE' and vcCommCode in('Pack','Raw')");
				this.ddlProductClass.Items.Clear();
				this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");

			}
			else
			{
				this.ddlProductType.Items.Clear();
				this.FillDropDownList("tbNameCodeToStorage",this.ddlProductType,"vcCommSign='PRODUCTTYPE'");
				this.ddlProductClass.Items.Clear();
				this.FillDropDownList("PClass",this.ddlProductClass,"vcCommSign='"+this.ddlProductType.SelectedValue+"'","全部");
			}
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(e.Item.ItemType==ListItemType.Item||e.Item.ItemType==ListItemType.AlternatingItem)
			{
				TableCell tcellCur=e.Item.Cells[5];
				TableCell tcelllow=e.Item.Cells[6];
				TableCell tcellup=e.Item.Cells[7];
				double curcount=Math.Round(double.Parse(tcellCur.Text.Trim()),2);
				double lowcount=Math.Round(double.Parse(tcelllow.Text.Trim()),2);
				double upcount=Math.Round(double.Parse(tcellup.Text.Trim()),2);
				if(curcount<=lowcount)
				{
					tcellCur.BackColor=Color.Red;
					tcelllow.BackColor=Color.Khaki;
				}
				if(curcount>=upcount)
				{
					tcellCur.BackColor=Color.Red;
					tcellup.BackColor=Color.Khaki;
				}
			}
		}
	}
}
