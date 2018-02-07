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
using BusiComm;
using CommCenter;

namespace AMSApp.MaterialS
{
	/// <summary>
	/// wfmMaterialOut 的摘要说明。
	/// </summary>
	public class wfmMaterialOut : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.TextBox txtQueryMaterialName;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtQueryMaterialCode;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Button btCancel;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtCurCount;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.TextBox txtProviderName;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtStandardUnit;
		protected System.Web.UI.WebControls.TextBox txtMaterialCode;
		protected System.Web.UI.WebControls.TextBox txtMaterialName;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtOutCount;
		protected System.Web.UI.WebControls.TextBox txtBatchNo;

		MaterialSBusi msb1=null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}
			if(!IsPostBack)
			{
				this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='MaterialType'");
                this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
				this.txtMaterialCode.Enabled=false;
				this.txtMaterialName.Enabled=false;
				this.txtStandardUnit.Enabled=false;
				this.txtUnit.Enabled=false;
				this.txtPrice.Enabled=false;
				this.txtProviderName.Enabled=false;
				this.ddlMaterialType.Enabled=false;
				this.txtCurCount.Enabled=false;
				this.txtBatchNo.Enabled=false;
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if(ls1.strDeptID=="CEN00"||ls1.strNewDeptID=="")
				{
					this.Popup("没有操作权限，请使用部门帐号！");
					this.btnQuery.Enabled=false;
					this.btAdd.Enabled=false;
					return;
				}
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
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1.strDeptID=="CEN00"||ls1.strNewDeptID=="")
			{
				this.Popup("没有操作权限，请使用部门帐号！");
				this.btnQuery.Enabled=false;
				this.btAdd.Enabled=false;
				return;
			}
			if(this.txtMaterialCode.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("请选择要入库有原材料！");
				return;
			}
			string strOutCount=this.txtOutCount.Text.Trim();
			if(strOutCount=="")
			{
				this.SetErrorMsgPageBydirHistory("请输入本次出库数量！");
				return;
			}
			if(!this.JudgeIsNum(strOutCount,"本次出库数量"))
			{
				return;
			}
		
			CMSMStruct.MaterialOutStruct mes1=new CommCenter.CMSMStruct.MaterialOutStruct();
			mes1.dOutCount=Math.Round(double.Parse(strOutCount),2);
			mes1.strBatchNo=this.txtBatchNo.Text.Trim();
			mes1.strMaterialCode=this.txtMaterialCode.Text.Trim();
			mes1.strMaterialName=this.txtMaterialName.Text.Trim();
			mes1.strStandardUnit=this.txtStandardUnit.Text.Trim();
			mes1.strUnit=this.txtUnit.Text.Trim();
			mes1.dPrice=Math.Round(double.Parse(this.txtPrice.Text.Trim()),2);
			mes1.strProviderName=this.txtProviderName.Text.Trim();
			mes1.strMaterialType=this.ddlMaterialType.SelectedValue;
			mes1.dLastCount=Math.Round(double.Parse(this.txtCurCount.Text.Trim()),2);
			mes1.dCount=mes1.dLastCount-mes1.dOutCount;
			mes1.strOperType="0";
			mes1.strOutDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
//			mes1.strDeptID=ls1.strNewDeptID;
			mes1.strDeptID=ls1.strDeptID;
			mes1.strOperDate=mes1.strOutDate;
			mes1.strOperName=ls1.strOperName;

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);

			CMSMStruct.MaterialSStruct msscur= msb1.GetMaterialInfo(mes1.strBatchNo,mes1.strMaterialCode);
			if(msscur.dCurCount<mes1.dOutCount)
			{
				this.Popup("当前数量不足！");
				return;
			}
			if(msscur.dCurCount!=mes1.dLastCount)
			{
				this.SetErrorMsgPageBydir("当前数量可能有变动，请重新录入！");
				return;
			}

			if(msb1.InsertMaterialOut(mes1))
			{
				this.SetSuccMsgPageBydir("录入原材料出库成功！","");
				return;
			}
			else
			{
				this.SetErrorMsgPageBydir("录入原材料出库失败，请重试！");
				return;
			}
		}

		private void btCancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("../wfmWelcome.aspx");
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);

			DataTable dt = msb1.GetMaterialsBySelect(this.txtQueryMaterialCode.Text.Trim(),this.txtQueryMaterialName.Text.Trim());
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);
			CMSMStruct.MaterialSStruct mss= msb1.GetMaterialInfo(item.Cells[0].Text.Trim(),item.Cells[1].Text.Trim());
			this.txtBatchNo.Text=mss.strBatchNo;
			this.txtMaterialCode.Text=mss.strMaterialCode;
			this.txtMaterialName.Text=mss.strMaterialName;
			this.txtStandardUnit.Text=mss.strStandardUnit;
			this.txtUnit.Text=mss.strUnit;
			this.txtPrice.Text=mss.dPrice.ToString();
			this.txtProviderName.Text=mss.strProviderName;
			this.txtCurCount.Text=mss.dCurCount.ToString();
			this.ddlMaterialType.SelectedIndex=this.ddlMaterialType.Items.IndexOf(this.ddlMaterialType.Items.FindByValue(mss.strMaterialType));
		}
	}
}
