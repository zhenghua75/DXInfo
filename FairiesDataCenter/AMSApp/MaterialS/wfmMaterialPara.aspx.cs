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
using BusiComm;

namespace AMSApp.MaterialS
{
	/// <summary>
	/// wfmMaterialPara 的摘要说明。
	/// </summary>
	public class wfmMaterialPara : wfmBase
	{
		protected System.Web.UI.WebControls.Label lblPromt;
		protected System.Web.UI.WebControls.Button btcancel;
		protected System.Web.UI.WebControls.Button btDel;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.TextBox txtMaterialName;
		protected System.Web.UI.WebControls.TextBox txtStandardUnit;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.WebControls.TextBox txtProviderName;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtBatchNo;

		MaterialSBusi ms1=null;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]==null)
			{
				Response.Redirect("../Exit.aspx");
				return;
			}

			string strid=Request.QueryString["id"];
			string strBatch=Request.QueryString["batch"];
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			ms1=new MaterialSBusi(strcons);

			if(!IsPostBack)
			{
				this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='MaterialType'");
				Session["mssold"]=null;
				if(strid==""||strid==null)
				{
					this.btAdd.Enabled=true;
					this.btDel.Enabled=false;
					this.btMod.Enabled=false;
					lbltitle.Text="新原材料资料录入";
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btDel.Enabled=true;
					this.btMod.Enabled=true;
					this.txtBatchNo.Enabled=false;
					CMSMStruct.MaterialSStruct mss=ms1.GetMaterialInfo(strBatch,strid);
					this.txtBatchNo.Text=mss.strBatchNo;
					this.txtMaterialName.Text=mss.strMaterialName;
					this.txtStandardUnit.Text=mss.strStandardUnit;
					this.txtUnit.Text=mss.strUnit;
					this.txtPrice.Text=mss.dPrice.ToString();
					this.txtProviderName.Text=mss.strProviderName;
					this.ddlMaterialType.SelectedIndex=this.ddlMaterialType.Items.IndexOf(this.ddlMaterialType.Items.FindByValue(mss.strMaterialType));
					lbltitle.Text="原材料资料修改--正在编辑的原材料编码为："+strid;
					Session["mssold"]=mss;
					this.txtPrice.Enabled=false;
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
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btDel.Click += new System.EventHandler(this.btDel_Click);
			this.btcancel.Click += new System.EventHandler(this.btcancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.MaterialSStruct mss=new CMSMStruct.MaterialSStruct();

			if(txtBatchNo.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("原材料批次不能为空！");
				return;
			}
			else
			{
				mss.strBatchNo=txtBatchNo.Text.Trim();
			}

			if(txtMaterialName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("原材料名称不能为空！");
				return;
			}
			else if(ms1.ChkMaterialNameDup(mss.strBatchNo,txtMaterialName.Text.Trim()))
			{
				mss.strMaterialName=txtMaterialName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("该原材料名称已经存在，请重新输入！");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("单价不能为空！");
				return;
			}
			else
			{
				mss.dPrice=double.Parse(txtPrice.Text.Trim());
			}

			if(txtUnit.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("单位不能为空！");
				return;
			}
			else
			{
				mss.strUnit=txtUnit.Text.Trim();
			}

			if(txtProviderName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("供应商不能为空！");
				return;
			}
			else
			{
				mss.strProviderName=txtProviderName.Text.Trim();
			}

			mss.strStandardUnit=this.txtStandardUnit.Text.Trim();
			mss.strMaterialType=this.ddlMaterialType.SelectedValue;

			if(!ms1.InsertMaterial(mss))
			{
				this.SetErrorMsgPageBydir("添加原材料资料失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("添加原材料资料成功！","");
				return;
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.MaterialSStruct mssold=(CMSMStruct.MaterialSStruct)Session["mssold"];
			if(mssold.strMaterialCode=="")
			{
				this.SetErrorMsgPageBydir("获取原材料编号错误！");
				return;
			}

			CMSMStruct.MaterialSStruct mssnew=new CMSMStruct.MaterialSStruct();
			mssnew.strMaterialCode=mssold.strMaterialCode;

			if(txtMaterialName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("原材料名称不能为空！");
				return;
			}
			else if(ms1.ChkNewMaterialNameDup(txtBatchNo.Text.Trim(),txtMaterialName.Text.Trim(),mssold.strMaterialCode))
			{
				mssnew.strMaterialName=txtMaterialName.Text.Trim();
			}
			else
			{
				this.SetErrorMsgPageBydirHistory("该原材料名称已经存在，请重新输入！");
				return;	
			}

			if(txtPrice.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("单价不能为空！");
				return;
			}
			else
			{
				mssnew.dPrice=double.Parse(txtPrice.Text.Trim());
			}

			if(txtUnit.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("单位不能为空！");
				return;
			}
			else
			{
				mssnew.strUnit=txtUnit.Text.Trim();
			}

			if(txtProviderName.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydirHistory("供应商不能为空！");
				return;
			}
			else
			{
				mssnew.strProviderName=txtProviderName.Text.Trim();
			}

			mssnew.strStandardUnit=this.txtStandardUnit.Text.Trim();
			mssnew.strMaterialType=this.ddlMaterialType.SelectedValue;

			if(!ms1.UpdateMaterial(mssnew,mssold))
			{
				this.SetErrorMsgPageBydir("保存原材料修改信息失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("保存原材料修改信息成功！","");
				return;
			}
		}

		private void btcancel_Click(object sender, System.EventArgs e)
		{
			this.RedirectPage("wfmMaterialManage.aspx");
		}

		private void btDel_Click(object sender, System.EventArgs e)
		{
			CMSMStruct.MaterialSStruct mssold=(CMSMStruct.MaterialSStruct)Session["mssold"];
			if(mssold.strBatchNo==""||mssold.strMaterialCode=="")
			{
				this.SetErrorMsgPageBydir("获取原材料批次和编号错误！");
				return;
			}

			if(!ms1.CancelMaterial(mssold))
			{
				this.SetErrorMsgPageBydir("原材料作废失败，请重试！");
				return;
			}
			else
			{
				this.SetSuccMsgPageBydir("原材料作废成功！","");
				return;
			}
		}
	}
}
