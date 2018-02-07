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
	/// wfmPoStockEnter 的摘要说明。
	/// </summary>
	public class wfmPoStockEnter : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtPoEnterID;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label2;

		BusiComm.StorageBusi StoBusi;
		protected ucPageView UcPageView1;
		protected string strEndDate;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtPoID;
		protected string strBeginDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					Session.Remove("QUERY");
					Session.Remove("page_view");
					strBeginDate=DateTime.Now.ToString("yyyy-MM-dd");
					strEndDate=DateTime.Now.ToString("yyyy-MM-dd");
					if(ls1.strNewDeptID=="CEN00")
					{
						this.FillDropDownList("NewDept",this.ddlDept);
						this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
					}
					else
					{
						this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+ls1.strNewDeptID+"'");
						this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+ls1.strNewDeptID+"'");
					}
					this.ddlState.Items.Add(new ListItem("未入库","0"));
					this.ddlState.Items.Add(new ListItem("已入库","1"));
					this.ddlState.SelectedIndex=0;
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string strPoEnterID=this.txtPoEnterID.Text.Trim();
			string strWHouse=this.ddlWhouse.SelectedValue;
			string strDept=this.ddlDept.SelectedValue;
			string strState=this.ddlState.SelectedValue;
			if(strWHouse=="全部")
				strWHouse="";
			if(strDept=="全部")
				strDept="";

			Hashtable htPara=new Hashtable();
			htPara.Add("strPoEnterID",strPoEnterID);
			htPara.Add("strDept",strDept);
			htPara.Add("strWHouse",strWHouse);
			htPara.Add("strState",strState);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			htPara.Add("strPoID",this.txtPoID.Text.Trim());
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetPoStockEnterMain(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"部门","NewDept");
					this.TableConvert(dtout,"仓库","Warehouse");
					dtout.TableName="采购入库单列表";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
				}
				
				UcPageView1.MyDataGrid.PageSize = 20;
				DataView dvOut =new DataView(dtout);
				this.UcPageView1.MyDataSource = dvOut;
				this.UcPageView1.BindGrid();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("../zhenghua/Storage/wfmPoStockEnterAdd.aspx");
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}
	}
}
