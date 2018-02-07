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
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
namespace AMSApp.zhenghua.Warhouse
{
	/// <summary>
	/// wfmAddWareHouse 的摘要说明。
	/// </summary>
	public class wfmAddWareHouse : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.TextBox txtWhCode;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.TextBox txtWhName;
		protected System.Web.UI.WebControls.TextBox txtWhAddress;
		protected System.Web.UI.WebControls.TextBox txtWhPhone;
		protected System.Web.UI.WebControls.TextBox txtFrequency;
		protected System.Web.UI.WebControls.TextBox txtWhMemo;
		protected System.Web.UI.WebControls.DropDownList ddlDepCode;
		protected System.Web.UI.WebControls.DropDownList ddlWhPerson;
		protected System.Web.UI.WebControls.DropDownList ddlWhValueStyle;
		protected System.Web.UI.WebControls.CheckBox chkFreeze;
		protected System.Web.UI.WebControls.DropDownList ddlFrequency;
		protected System.Web.UI.WebControls.DropDownList ddlWHProperty;
		protected System.Web.UI.WebControls.CheckBox chkShop;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			this.Response.Expires = -1;
			this.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(-1);
			this.Response.CacheControl = "no-cache";
			this.Button2.Attributes.Add("onclick","window.returnValue='cccc';window.close()");
			if(!this.IsPostBack)
			{
				Bind();
				string strflag = this.Request.QueryString["flag"].ToString();
				//string strinvccode = this.Request.QueryString["invccode"].ToString();
				if(strflag=="add")
				{
					this.lblTitle.Text = "添加仓库档案";		
					this.txtWhCode.Text = this.ddlDepCode.SelectedValue+this.GetWhCode(this.ddlDepCode.SelectedValue);
					
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "修改仓库档案";
					string strwhcode = this.Request.QueryString["whcode"].ToString();
					string strsql = "select * from tbwarehouse where cnvcwhcode='"+strwhcode+"'";
					DataTable dtWh = Helper.Query(strsql);
					Entity.Warehouse wh = new AMSApp.zhenghua.Entity.Warehouse(dtWh);
					BindWh(wh);
					//this.txtWhCode.Enabled = false;
					this.ddlDepCode.Enabled = false;
				}
				this.txtWhCode.Enabled = false;
				
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.ddlDepCode.SelectedIndexChanged += new System.EventHandler(this.ddlDepCode_SelectedIndexChanged);			
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//确定
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "添加存货档案";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;


			Entity.Warehouse wh = this.GetWh();
			Business.WareHouseFacade whf = new WareHouseFacade();

			if(!this.JudgeIsNum(this.txtFrequency.Text))
			{
				this.Popup("盘点周期请输入数字");
				return;
			}
			if(this.JudgeIsNull(this.txtWhName.Text))
			{
				this.Popup("请输入仓库名称");
				return;
			}
			if(this.lblTitle.Text == "添加仓库档案")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "添加存货档案";
//				ol.cnvcOperID = this.oper.strLoginID;
//				ol.cnvcDeptID = this.oper.strDeptID;
//
//
//				Entity.Warehouse wh = this.GetWh();
//				Business.WareHouseFacade whf = new WareHouseFacade();
				int ret = whf.AddWareHouse(ol,wh);
				if(ret > 0 )
				{
					this.Popup("添加仓库档案成功！");
				}
				else
				{
					this.Popup("添加仓库档案失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "修改仓库档案")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "修改仓库档案";
//				ol.cnvcOperID = this.oper.strLoginID;
//				ol.cnvcDeptID = this.oper.strDeptID;
//
//
//				Entity.Warehouse wh = GetWh();
//				
//				Business.WareHouseFacade whf = new WareHouseFacade();
				int ret = whf.UpdateWareHouse(ol,wh);
				if(ret > 0 )
				{
					this.Popup("修改仓库档案成功！");
				}
				else
				{
					this.Popup("修改仓库档案失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
		}
		private void Bind()
		{
			this.BindDept(this.ddlDepCode,"");
			this.BindOper(this.ddlWhPerson,"");//"vcDeptID='"+this.ddlDepCode.SelectedValue+"'");
			this.BindNameCode(this.ddlWhValueStyle,"cnvcType='ValueType'");
			this.BindNameCode(this.ddlFrequency,"cnvcType='CheckCycle'");
			this.BindNameCode(this.ddlWHProperty,"cnvcType='WhAttr'");
		}

		private void BindWh(Entity.Warehouse wh)
		{
			this.txtWhCode.Text = wh.cnvcWhCode;
			this.txtWhName.Text = wh.cnvcWhName;
			this.SetDDL(this.ddlDepCode,wh.cnvcDepCode);
			this.txtWhAddress.Text = wh.cnvcWhAddress;
			this.txtWhPhone.Text = wh.cnvcWhPhone;
			this.SetDDL(this.ddlWhPerson,wh.cnvcWhPerson);
			this.SetDDL(this.ddlWhValueStyle,wh.cnvcWhValueStyle);
			this.chkFreeze.Checked = wh.cnbFreeze;
			this.txtFrequency.Text = wh.cnnFrequency.ToString();
			this.SetDDL(this.ddlFrequency,wh.cnvcFrequency);
			this.SetDDL(this.ddlWHProperty,wh.cnvcWhProperty.ToString());
			this.chkShop.Checked = wh.cnbShop;
		}

		private Entity.Warehouse GetWh()
		{
			if(this.txtFrequency.Text == "") this.txtFrequency.Text = "1";
			Entity.Warehouse wh = new AMSApp.zhenghua.Entity.Warehouse();
			wh.cnvcWhCode = this.txtWhCode.Text;
			wh.cnvcWhName = this.txtWhName.Text;
			wh.cnvcDepCode = this.ddlDepCode.SelectedValue;
			wh.cnvcWhAddress = this.txtWhAddress.Text;
			wh.cnvcWhPhone = this.txtWhPhone.Text;
			wh.cnvcWhPerson = this.ddlWhPerson.SelectedValue;
			wh.cnvcWhValueStyle = this.ddlWhValueStyle.SelectedValue;
			wh.cnbFreeze = this.chkFreeze.Checked;
			wh.cnnFrequency = Convert.ToInt16(this.txtFrequency.Text);
			wh.cnvcFrequency = this.ddlFrequency.SelectedValue;
			wh.cnvcWhProperty =this.ddlWHProperty.SelectedValue;
			wh.cnbShop = this.chkShop.Checked;
			return wh;
		}
		private void ddlDepCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//部门
			//this.BindOper(this.ddlWhPerson,"vcDeptID='"+this.ddlDepCode.SelectedValue+"'");
			if(this.lblTitle.Text == "添加仓库档案")
			this.txtWhCode.Text = this.ddlDepCode.SelectedValue+this.GetWhCode(this.ddlDepCode.SelectedValue);
		}
	}
}
