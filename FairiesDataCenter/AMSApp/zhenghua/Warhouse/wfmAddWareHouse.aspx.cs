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
	/// wfmAddWareHouse ��ժҪ˵����
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
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
					this.lblTitle.Text = "��Ӳֿ⵵��";		
					this.txtWhCode.Text = this.ddlDepCode.SelectedValue+this.GetWhCode(this.ddlDepCode.SelectedValue);
					
				}
				if(strflag=="modify")
				{
					this.lblTitle.Text = "�޸Ĳֿ⵵��";
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.ddlDepCode.SelectedIndexChanged += new System.EventHandler(this.ddlDepCode_SelectedIndexChanged);			
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//ȷ��
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "��Ӵ������";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;


			Entity.Warehouse wh = this.GetWh();
			Business.WareHouseFacade whf = new WareHouseFacade();

			if(!this.JudgeIsNum(this.txtFrequency.Text))
			{
				this.Popup("�̵���������������");
				return;
			}
			if(this.JudgeIsNull(this.txtWhName.Text))
			{
				this.Popup("������ֿ�����");
				return;
			}
			if(this.lblTitle.Text == "��Ӳֿ⵵��")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "��Ӵ������";
//				ol.cnvcOperID = this.oper.strLoginID;
//				ol.cnvcDeptID = this.oper.strDeptID;
//
//
//				Entity.Warehouse wh = this.GetWh();
//				Business.WareHouseFacade whf = new WareHouseFacade();
				int ret = whf.AddWareHouse(ol,wh);
				if(ret > 0 )
				{
					this.Popup("��Ӳֿ⵵���ɹ���");
				}
				else
				{
					this.Popup("��Ӳֿ⵵��ʧ�ܣ�");
				}

				this.Response.Write("<script type=\"text/javascript\">window.returnValue=true;window.close()</script>");
			}
			if(this.lblTitle.Text == "�޸Ĳֿ⵵��")
			{
				//Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "�޸Ĳֿ⵵��";
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
					this.Popup("�޸Ĳֿ⵵���ɹ���");
				}
				else
				{
					this.Popup("�޸Ĳֿ⵵��ʧ�ܣ�");
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
			//����
			//this.BindOper(this.ddlWhPerson,"vcDeptID='"+this.ddlDepCode.SelectedValue+"'");
			if(this.lblTitle.Text == "��Ӳֿ⵵��")
			this.txtWhCode.Text = this.ddlDepCode.SelectedValue+this.GetWhCode(this.ddlDepCode.SelectedValue);
		}
	}
}
