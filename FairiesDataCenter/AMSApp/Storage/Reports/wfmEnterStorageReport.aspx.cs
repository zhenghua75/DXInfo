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

namespace AMSApp.Storage.Report
{
	/// <summary>
	/// Summary description for wfmEnterSorageReport.
	/// </summary>
	public class wfmEnterStorageReport : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlQueryType;
		protected System.Web.UI.WebControls.TextBox txtProviderID;
		protected System.Web.UI.WebControls.TextBox txtProviderName;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.DropDownList ddlMaterilName;
		protected System.Web.UI.WebControls.DropDownList ddlMonth;
	
		protected ucPageView UcPageView1;
		protected System.Web.UI.WebControls.Button btnExcel;
		BusiComm.StorageBusi StoBusi;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.btnExcel.Attributes.Add("onclick","javascript:window.open('../../DataGridToExcel.aspx', 'Sample', 'toolbar=no,location=no,directories=no,status=yes,menubar=yes,scrollbars=no,resizable=yes,copyhistory=yes,width=790,height=520,left=0,top=0')");
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.ddlQueryType.Items.Add(new ListItem("原材料分供应商进仓报表","MoreProviderEnter"));
					this.ddlQueryType.Items.Add(new ListItem("单供应商分原材料进仓报表","OneProviderEnter"));

					string strvalue="";
					string strYear=DateTime.Now.Year.ToString();
					for(int i=0;i<12;i++)
					{
						if(i!=0&&DateTime.Now.AddMonths(-i).Month==12)
						{
							strYear=DateTime.Now.AddYears(-1).Year.ToString();
						}
						if(DateTime.Now.AddMonths(-i).Month<10)
						{
							strvalue=strYear+"0"+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						else
						{
							strvalue=strYear+(DateTime.Now.AddMonths(-i).Month).ToString();
						}
						this.ddlMonth.Items.Add(new ListItem(strvalue,strvalue));
					}

					this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='PRODUCTTYPE' and vcCommCode in('Pack','Raw')","全部");

					string strType=this.ddlMaterialType.SelectedValue;
					this.FillDropDownList("AllMaterial",this.ddlMaterilName,"cnvcProductType='"+strType+"'","全部");

					Session.Remove("QUERY");
					Session.Remove("toExcel");
					Session.Remove("page_view");
				}

				if(this.UcPageView1.MyDataGrid.DataSource!=null)
				{
					if(((DataView)this.UcPageView1.MyDataGrid.DataSource).Count>0)
					{
						UcPageView1.FootBar.Visible = true;
						btnExcel.Enabled=true;
					}
					else
					{
						btnExcel.Enabled=false;
					}
				}
				else
				{
					btnExcel.Enabled=false;
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("QUERY");
			Session.Remove("toExcel");
			string strQueryType=this.ddlQueryType.SelectedValue;
			string strMonth=this.ddlMonth.SelectedValue;
			string strProviderID=this.txtProviderID.Text.Trim();
			string strProviderName=this.txtProviderName.Text.Trim();
			string strMaterialType=this.ddlMaterialType.SelectedValue;
			string strMaterialID=this.ddlMaterilName.SelectedValue;
			string strMaterialName=this.ddlMaterilName.SelectedItem.Text;

			Hashtable htPara=new Hashtable();
			htPara.Add("strQueryType",strQueryType);
			htPara.Add("strMonth",strMonth);
			htPara.Add("strProviderID",strProviderID);
			htPara.Add("strProviderName",strProviderName);
			htPara.Add("strMaterialType",strMaterialType);
			htPara.Add("strMaterialID",strMaterialID);
			htPara.Add("strMaterialName",strMaterialName);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.QueryBillEnterReport(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBy2dir("查询出错，请重试！");
					return;
				}
				else
				{
					dtout.TableName="进仓报表";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
					Session["toExcel"]=dtexcel;
					if(dtout.Rows.Count<=0)
					{
						btnExcel.Enabled=false;
					}
					else
					{
						btnExcel.Enabled=true;	
					}
				}
				
				UcPageView1.MyDataGrid.PageSize = 20;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBy2dir("查询错误，请重试！");
				return;
			}
		}

		private void ddlMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ddlMaterilName.Items.Clear();
			string strType=this.ddlMaterialType.SelectedValue;
			this.FillDropDownList("AllMaterial",this.ddlMaterilName,"cnvcProductType='"+strType+"'","全部");
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.ddlMaterialType.SelectedIndexChanged += new System.EventHandler(this.ddlMaterialType_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
