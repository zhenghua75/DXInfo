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
namespace AMSApp.zhenghua.Inventory
{
	/// <summary>
	/// wfmAddInventory 的摘要说明。
	/// </summary>
	public class wfmAddInventory : wfmBase
	{
		#region 字段
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList ddlInvCCode;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.CheckBox chkProductBill;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.CheckBox chkSale;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.CheckBox chkPurchase;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.CheckBox chkSelf;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.CheckBox chkComsume;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.TextBox txtInvCCost;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.TextBox txtInvNCost;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.TextBox txtSafeNum;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.TextBox txtLowSum;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.TextBox txtSDate;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.TextBox txtEDate;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.DropDownList ddlValueType;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.TextBox txtInvStd;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.DropDownList ddlGroupCode;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.DropDownList ddlComUnitCode;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.DropDownList ddlSAComUnitCode;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.DropDownList ddlPUComUnitCode;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.DropDownList ddlSTComUnitCode;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.DropDownList ddlProduceUnitCode;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.TextBox txtRetailPrice;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.DropDownList ddlShopUnitCode;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.TextBox txtFeel;
		protected System.Web.UI.WebControls.TextBox txtOrganise;
		protected System.Web.UI.WebControls.TextBox txtColor;
		protected System.Web.UI.WebControls.TextBox txtTaste;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.TextBox txtExpire;
		protected System.Web.UI.WebControls.TextBox txtDue;
		protected System.Web.UI.WebControls.Button Button2;
		#endregion
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
				string strinvccode = this.Request.QueryString["invccode"].ToString();
				if(strflag=="add")
				{
					this.lblTitle.Text = "添加存货档案";
					ListItem liInvCCode = this.ddlInvCCode.Items.FindByValue(strinvccode);
					if(liInvCCode != null)
					{
						liInvCCode.Selected = true;
					}
					BindUnit();
					string strnewinvcode = this.GetInvCode(strinvccode);
					this.txtInvCode.Text = strnewinvcode;
					//this.txtInvCode.Enabled = false;
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "修改存货档案";
					string strinvcode = this.Request.QueryString["invcode"].ToString();
					string strsql = "select * from tbInventory where cnvcInvCode='"+strinvcode+"'";
					DataTable dtInv = Helper.Query(strsql);
					Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dtInv);
					BindInv(inv);
					//this.txtInvCode.Enabled = false;
				}
				this.txtInvCode.Enabled = false;
				this.ddlInvCCode.Enabled = false;
				this.txtInvNCost.Enabled = false;
			}
		}

		private void BindInv(Entity.Inventory inv)
		{
			this.txtInvCode.Text = inv.cnvcInvCode;
			this.txtInvName.Text = inv.cnvcInvName;
			this.SetDDL(this.ddlInvCCode,inv.cnvcInvCCode);
			this.chkProductBill.Checked = inv.cnbProductBill;
			this.chkSale.Checked = inv.cnbSale;
			this.chkPurchase.Checked = inv.cnbPurchase;
			this.chkSelf.Checked = inv.cnbSelf;
			this.chkComsume.Checked = inv.cnbComsume;
			this.txtInvCCost.Text = inv.cniInvCCost.ToString();
			this.txtInvNCost.Text = inv.cniInvNCost.ToString();
			this.txtSafeNum.Text = inv.cniSafeNum.ToString();
			this.txtLowSum.Text = inv.cniLowSum.ToString();
			if(inv.cndSDate > DateTime.MinValue)
			this.txtSDate.Text = inv.cndSDate.ToString("yyyy-MM-dd");
			if(inv.cndEDate > DateTime.MinValue)
			this.txtEDate.Text = inv.cndEDate.ToString("yyyy-MM-dd");
			this.SetDDL(this.ddlValueType,inv.cnvcValueType);
			this.txtInvStd.Text = inv.cnvcInvStd;
			this.SetDDL(this.ddlGroupCode,inv.cnvcGroupCode);
			BindUnit();
			this.SetDDL(this.ddlComUnitCode,inv.cnvcComUnitCode);
			this.SetDDL(this.ddlPUComUnitCode,inv.cnvcPUComUnitCode);
			this.SetDDL(this.ddlSAComUnitCode,inv.cnvcSAComUnitCode);
			this.SetDDL(this.ddlSTComUnitCode,inv.cnvcSTComUnitCode);
			this.SetDDL(this.ddlShopUnitCode,inv.cnvcShopUnitCode);
			this.SetDDL(this.ddlProduceUnitCode,inv.cnvcProduceUnitCode);
			this.txtRetailPrice.Text = inv.cnfRetailPrice.ToString();
			this.txtFeel.Text = inv.cnvcFeel;
			this.txtOrganise.Text = inv.cnvcOrganise;
			this.txtColor.Text = inv.cnvcColor;
			this.txtTaste.Text = inv.cnvcTaste;
			//添加字段 zhh
			this.txtExpire.Text = inv.cnnExpire.ToString();
			this.txtDue.Text = inv.cnnDue.ToString();
		}
		private Entity.Inventory GetInv()
		{
			if(this.txtInvCCost.Text == "") this.txtInvCCost.Text = "0";
			if(this.txtInvNCost.Text == "") this.txtInvNCost.Text = "0";
			if(this.txtSafeNum.Text == "") this.txtSafeNum.Text = "0";
			if(this.txtLowSum.Text == "") this.txtLowSum.Text = "0";
			if(this.txtRetailPrice.Text == "") this.txtRetailPrice.Text = "0";
			if(this.txtExpire.Text == "") this.txtExpire.Text = "0";
			if(this.txtDue.Text == "") this.txtDue.Text = "5";
			Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory();
			inv.cnvcInvCode = this.txtInvCode.Text;
			inv.cnvcInvName = this.txtInvName.Text;
			inv.cnvcInvCCode = this.ddlInvCCode.SelectedValue;
			inv.cnbProductBill = this.chkProductBill.Checked;
			inv.cnbSale = this.chkSale.Checked;
			inv.cnbPurchase = this.chkPurchase.Checked;
			inv.cnbSelf = this.chkSelf.Checked;
			inv.cnbComsume = this.chkComsume.Checked;
			inv.cniInvCCost = Convert.ToDecimal(this.txtInvCCost.Text);
			inv.cniInvNCost = Convert.ToDecimal(this.txtInvNCost.Text);
			inv.cniSafeNum = Convert.ToDecimal(this.txtSafeNum.Text);
			inv.cniLowSum = Convert.ToDecimal(this.txtLowSum.Text);
			if(this.txtSDate.Text == "")
				inv.cndSDate = DateTime.MinValue;
			else
				inv.cndSDate = Convert.ToDateTime(this.txtSDate.Text);
			if(this.txtEDate.Text == "")
				inv.cndEDate = DateTime.MinValue;
			else
				inv.cndEDate = Convert.ToDateTime(this.txtEDate.Text);
			inv.cnvcValueType = this.ddlValueType.SelectedValue;
			inv.cnvcInvStd = this.txtInvStd.Text;
			inv.cnvcGroupCode = this.ddlGroupCode.SelectedValue;
			inv.cnvcComUnitCode = this.ddlComUnitCode.SelectedValue;
			inv.cnvcPUComUnitCode = this.ddlPUComUnitCode.SelectedValue;
			inv.cnvcSAComUnitCode = this.ddlSAComUnitCode.SelectedValue;
			inv.cnvcSTComUnitCode = this.ddlSTComUnitCode.SelectedValue;
			inv.cnvcShopUnitCode = this.ddlShopUnitCode.SelectedValue;
			inv.cnvcProduceUnitCode = this.ddlProduceUnitCode.SelectedValue;
			inv.cnfRetailPrice = Convert.ToDecimal(this.txtRetailPrice.Text);
			inv.cnvcFeel = this.txtFeel.Text;
			inv.cnvcOrganise = this.txtOrganise.Text;
			inv.cnvcColor = this.txtColor.Text;
			inv.cnvcTaste = this.txtTaste.Text;

			//添加字段
			inv.cnnExpire = Convert.ToInt32(this.txtExpire.Text);
			inv.cnnDue = Convert.ToInt32(this.txtDue.Text);
			return inv;
		}
		private void Bind()
		{
			this.BindProductClass(this.ddlInvCCode,"");
			this.BindNameCode(this.ddlValueType,"cnvctype='ValueType'");

			this.BindComputationGroup(this.ddlGroupCode,"");
			
		}
		private void BindUnit()
		{
			this.BindComputationUnit(this.ddlComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"' and cnbMainUnit=1");

			this.BindComputationUnit(this.ddlSAComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
			this.BindComputationUnit(this.ddlPUComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
			this.BindComputationUnit(this.ddlSTComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
			this.BindComputationUnit(this.ddlProduceUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
			this.BindComputationUnit(this.ddlShopUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
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
			this.ddlInvCCode.SelectedIndexChanged += new System.EventHandler(this.ddlInvCCode_SelectedIndexChanged);
			this.ddlGroupCode.SelectedIndexChanged += new System.EventHandler(this.ddlGroupCode_SelectedIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ddlInvCCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string strinvccode = this.ddlInvCCode.SelectedValue;
			string strinvcode = this.GetInvCode(strinvccode);
			this.txtInvCode.Text = strinvcode;
		}

		private void ddlGroupCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			BindUnit();
//			this.BindComputationUnit(this.ddlComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
//
//			this.BindComputationUnit(this.ddlSAComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
//			this.BindComputationUnit(this.ddlPUComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
//			this.BindComputationUnit(this.ddlSTComUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
//			this.BindComputationUnit(this.ddlProduceUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
//			this.BindComputationUnit(this.ddlShopUnitCode,"cnvcGroupCode='"+this.ddlGroupCode.SelectedValue+"'");
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//确定
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			//ol.cnvcOperType = "添加存货档案";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;


			Entity.Inventory inv = GetInv();
				
			Business.InventoryFacade binv = new InventoryFacade();

			if(this.JudgeIsNull(this.txtInvCode.Text))
			{
				this.Popup("存货编码获取错误，请重新打开此页面");
				return;
			}
			if(this.JudgeIsNull(this.txtInvName.Text))
			{
				this.Popup("请输入存货名称");
				return;
			}
//			if(this.JudgeIsNull(this.txtInvCCost.Text))
//			{
//				this.Popup("请输入参考成本");
//				return;
//			}
			if(!this.JudgeIsNum(this.txtInvCCost.Text))
			{
				this.Popup("参考成本请输入数字");
				return;
			}
			if(!this.JudgeIsNum(this.txtSafeNum.Text))
			{
				this.Popup("安全库存量请输入数字");
				return;
			}
			if(!this.JudgeIsNum(this.txtLowSum.Text))
			{
				this.Popup("最低库存量请输入数字");
				return;
			}
			if(!this.JudgeIsNum(this.txtExpire.Text))
			{
				this.Popup("过期限制请输入数字");
				return;
			}
			if(!this.JudgeIsNum(this.txtRetailPrice.Text))
			{
				this.Popup("零售价请输入数字");
				return;
			}
			if(this.lblTitle.Text == "添加存货档案")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "添加存货档案";
//				ol.cnvcOperID = this.oper.strLoginID;
//				ol.cnvcDeptID = this.oper.strDeptID;
//
//
//				Entity.Inventory inv = GetInv();
//				
//				Business.InventoryFacade binv = new InventoryFacade();
				int ret = binv.AddInventory(ol,inv);
				if(ret > 0 )
				{
					this.Popup("添加存货档案成功！");
				}
				else
				{
					this.Popup("添加存货档案失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "修改存货档案")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "修改存货档案";
//				ol.cnvcOperID = this.oper.strLoginID;
//				ol.cnvcDeptID = this.oper.strDeptID;
//
//
//				Entity.Inventory inv = GetInv();
//				
//				Business.InventoryFacade binv = new InventoryFacade();
				int ret = binv.UpdateInventory(ol,inv);
				if(ret > 0 )
				{
					this.Popup("修改存货档案成功！");
				}
				else
				{
					this.Popup("修改存货档案失败！");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
		}
	}
}
