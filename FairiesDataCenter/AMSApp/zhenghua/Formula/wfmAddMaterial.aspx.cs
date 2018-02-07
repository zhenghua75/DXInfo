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
	/// wfmAddMaterial ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
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


		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
			//���
			//�ֶ��ж�
			// ��Ϊ���ж�
			if(JudgeIsNull(this.txtProductCode.Text, "ԭ�ϱ���"))
				return;
			if(JudgeIsNull(this.txtProductName.Text, "ԭ������"))
				return;
			if(JudgeIsNull(this.txtPrice.Text, "�����۸�"))
				return;
			if(JudgeIsNull(this.txtConversion.Text, "�����ϵ"))
				return;
			if(JudgeIsNull(this.txtUnit.Text, "���ֵ�λ"))
				return;
			if(JudgeIsNull(this.txtStandardUnit.Text, "���λ"))
				return;
			if(JudgeIsNull(this.txtStandardCount.Text, "�������"))
				return;
			if(!JudgeIsNum(this.txtPrice.Text, "�����۸�"))
				return;
			if(!JudgeIsNum(this.txtConversion.Text, "�����ϵ"))
				return;
			if(!JudgeIsNum(this.txtStandardCount.Text, "�������"))
				return;
			if(!JudgeIsCode(this.ddlProductType.SelectedValue,this.ddlProductClass.SelectedValue,this.txtProductCode.Text))
			{
				Popup("�������");
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
				Popup("��ͬ����ԭ�ϲ����Ѵ���");
				return;
			}

			OperLog operLog = new OperLog();
			operLog.cnvcOperID = oper.strLoginID;
			operLog.cnvcDeptID = oper.strDeptID;
			operLog.cnvcOperType = "���ԭ�ϲ���";

			MaterialFacade mf = new MaterialFacade();
			mf.AddMaterial(mat,operLog);
			this.Popup("ԭ�ϲ�����ӳɹ���");
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
							Popup("�޺��ʵ�"+ddlProductClass.SelectedItem.Text+"����");
							this.txtProductCode.Text = "";
						}

					}
				}
			}
			else
			{
				Popup(this.ddlProductClass.SelectedItem.Text+"����ԭ����δ��⣬��ͷ��ʼ����");
				this.txtProductCode.Text = strCodeBegin;
			}
						
		}

		private void ddlProductClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindCode();
		}
	}
}
