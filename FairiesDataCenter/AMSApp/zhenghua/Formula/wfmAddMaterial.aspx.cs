using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AMSApp.zhenghua;
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;
using System.Text;
namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmAddMaterial 的摘要说明。
	/// </summary>
	public class wfmAddMaterial : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList ddlLeastUnit;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.TextBox txtPrice;
		protected System.Web.UI.WebControls.TextBox txtConversion;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtStandardUnit;
		protected System.Web.UI.WebControls.TextBox txtStandardCount;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.DropDownList ddlProductType;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
//				DataTable dtLeastUnit = Helper.BindCommCode("Least");
//				this.ddlLeastUnit.DataSource = dtLeastUnit;
//				this.ddlLeastUnit.DataValueField = "vcCommCode";
//				this.ddlLeastUnit.DataTextField = "vcCommName";
//				this.ddlLeastUnit.DataBind();

				BindNameCode(ddlLeastUnit, "cnvcType='LEASTUNIT'");
				

//				DataTable dtProductType = Helper.BindCommCode("PType");
//				this.ddlProductType.DataSource = dtProductType;
//				this.ddlProductType.DataValueField = "vcCommCode";
//				this.ddlProductType.DataTextField = "vcCommName";
//				this.ddlProductType.DataBind();

				BindNameCode(ddlProductType, "cnvcType='PRODUCTTYPE' and (cnvcCode='Raw' or cnvcCode='Pack')");
				BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "'");

				BindCode();
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
			this.ddlProductType.SelectedIndexChanged += new System.EventHandler(this.ddlProductType_SelectedIndexChanged);
			this.ddlProductClass.SelectedIndexChanged += new System.EventHandler(this.ddlProductClass_SelectedIndexChanged);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			//添加
			//字段判断
			// 不为空判断
			if(JudgeIsNull(this.txtProductCode.Text, "原料编码"))
				return;
			if(JudgeIsNull(this.txtProductName.Text, "原料名称"))
				return;
			if(JudgeIsNull(this.txtPrice.Text, "计量价格"))
				return;
			if(JudgeIsNull(this.txtConversion.Text, "换算关系"))
				return;
			if(JudgeIsNull(this.txtUnit.Text, "出仓单位"))
				return;
			if(JudgeIsNull(this.txtStandardUnit.Text, "规格单位"))
				return;
			if(JudgeIsNull(this.txtStandardCount.Text, "规格数量"))
				return;
			if(!JudgeIsNum(this.txtPrice.Text, "计量价格"))
				return;
			if(!JudgeIsNum(this.txtConversion.Text, "换算关系"))
				return;
			if(!JudgeIsNum(this.txtStandardCount.Text, "规格数量"))
				return;
			if(!JudgeIsCode(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtProductCode.Text))
			{
				Popup("编码错误");
				return;
			}
				
			Material mat = new Material();
			mat.cnnConversion = decimal.Parse(txtConversion.Text);
			mat.cnnPrice = decimal.Parse(txtPrice.Text);
			mat.cnnStatdardCount = decimal.Parse(txtStandardCount.Text);
			mat.cnvcLeastUnit = ddlLeastUnit.SelectedValue;
			mat.cnvcMaterialCode = txtProductCode.Text;
			mat.cnvcMaterialName = txtProductName.Text;
			mat.cnvcProductType = ddlProductType.SelectedValue;
			mat.cnvcStandardUnit = txtStandardUnit.Text;
			mat.cnvcUnit = txtUnit.Text;
			mat.cnvcProductClass = ddlProductClass.SelectedValue;

			//tbBusiLog
			string strSql = "select * from tbMaterial where cnvcMaterialCode='"+mat.cnvcMaterialCode+"'";
			DataTable dtMaterial = Helper.Query(strSql);
			if(dtMaterial.Rows.Count > 0)
			{
				Popup("相同编码原料材料已存在");
				return;
			}

			OperLog operLog = new OperLog();
			operLog.cnvcOperID = oper.strLoginID;
			operLog.cnvcDeptID = oper.strDeptID;
			operLog.cnvcOperType = "添加原料材料";

			MaterialFacade mf = new MaterialFacade();
			mf.AddMaterial(mat,operLog);
			this.Popup("原料材料添加成功！");
			btnCancel_Click(null, null);
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			txtConversion.Text = "";
			txtPrice.Text = "";
			txtStandardCount.Text = "";
			txtProductCode.Text = "";
			txtProductName.Text = "";
			txtStandardUnit.Text = "";
			txtUnit.Text = "";
		}

		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			//
			this.Response.Redirect("./wfmMaterial.aspx");
		}

		private void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.BindProductClass(ddlProductClass, "cnvcProductType='" + ddlProductType.SelectedValue + "'");
		}

		private void BindCode()
		{
			string strClass = ddlProductClass.SelectedValue;
			//string strType = ddlProductType.SelectedValue;
			
			
			string[] strClasses = strClass.Split('~');
			string strCodeBegin = strClasses[0];
			string strCodeEnd = strClasses[1];

			string strFSql = " select top 1 cnvcMaterialCode from tbMaterial "
				+" where cnvcMaterialCode>="+strCodeBegin+" and cnvcMaterialCode<"+strCodeEnd
				//+" and cnvcProductType in('Raw','Pack') "
				+" order by cnvcMaterialCode desc ";
			DataTable dt = Helper.Query(strFSql);
			if(dt.Rows.Count > 0)
			{
				string strCode = dt.Rows[0][0].ToString();
				if(this.JudgeIsNum(strCode))
				{
					int iCode = int.Parse(strCode);
					if(this.JudgeIsNum(strCodeEnd))
					{
						int iEnd = int.Parse(strCodeEnd);
						if(iCode+1<=iEnd)
						{
							this.txtProductCode.Text = Convert.ToString(iCode+1);
						}
						else
						{
							Popup("无合适的"+ddlProductClass.SelectedItem.Text+"编码");
							this.txtProductCode.Text = "";
						}

					}
				}
			}
			else
			{
				Popup(this.ddlProductClass.SelectedItem.Text+"类别的原材料未入库，从头开始编码");
				this.txtProductCode.Text = strCodeBegin;
			}
						
		}

		private void ddlProductClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindCode();
		}
	}
}
