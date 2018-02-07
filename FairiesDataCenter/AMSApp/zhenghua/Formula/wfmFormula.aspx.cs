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
using AMSApp.zhenghua.Business;
using AMSApp.zhenghua.Entity;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmFormula 的摘要说明。
	/// </summary>
	public class wfmFormula : wfmBase
	{
		#region 字段

		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Button btnReturn;
		protected System.Web.UI.WebControls.Label lblFormula;
		protected System.Web.UI.WebControls.DropDownList ddlProductClass;
		protected System.Web.UI.WebControls.TextBox txtProductCode;
		protected System.Web.UI.WebControls.TextBox txtProductName;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DataGrid dgBOM;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtBaseQtyD;
		protected System.Web.UI.WebControls.DropDownList ddlGroupCode;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList ddlUnit;
		#endregion
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{								
				this.BindProductClass(this.ddlProductClass,"");
				this.BindComputationGroup(this.ddlGroupCode,"");
				this.BindComputationUnit(this.ddlUnit,"");
				if(this.Request.QueryString["flag"] != null)
				{
					string strflag = this.Request.QueryString["flag"].ToString();
					if(strflag == "add")
					{
						Session["part"] = null;
						Session["child"] = null;
						Session["bom"] = null;
						Session["BaseQtyD"] = null;
					}
					if(strflag == "setpart")
					{
						string strinvcode = this.Request.QueryString["invcode"].ToString();
						GetInv(strinvcode);
					}
					if(strflag == "Edit" || strflag == "Detail")
					{
						string strinvcode = this.Request.QueryString["invcode"].ToString();
						GetInv(strinvcode);
					}
				}
				if(Session["BaseQtyD"] != null)
				{
					this.txtBaseQtyD.Text = Session["BaseQtyD"].ToString();
				}
				BindPart();
				BindChild();
			}
			
		}

		private void BindPart()
		{
			if(Session["part"] != null)
			{
				Entity.Inventory inv = Session["part"] as Entity.Inventory;
				this.SetDDL(this.ddlProductClass,inv.cnvcInvCCode);
				this.SetDDL(this.ddlUnit,inv.cnvcProduceUnitCode);
				this.SetDDL(this.ddlGroupCode,inv.cnvcGroupCode);
				this.txtProductCode.Text = inv.cnvcInvCode;
				this.txtProductName.Text = inv.cnvcInvName;
			}
		}
		private void BindChild()
		{			
			if(Session["child"] != null)
			{
				DataTable dtchild = Session["child"] as DataTable;
				if(!dtchild.Columns.Contains("cnnBaseQtyN"))
					dtchild.Columns.Add("cnnBaseQtyN");
				if(Session["bom"] != null)
				{
					DataTable dtbom = Session["bom"] as DataTable;
					foreach(DataRow dr in dtchild.Rows)
					{
						Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dr);
						DataRow[] drs = dtbom.Select("cnvcComponentInvCode='"+inv.cnvcInvCode+"'");
						if(drs.Length>0)
						{
							Entity.BillOfMaterials bom = new BillOfMaterials(drs[0]);
							dr["cnnBaseQtyN"] = bom.cnnBaseQtyN;
						}
					}
				}
				this.DataTableConvert(dtchild, "cnvcProduceUnitCode","cnvcProduceUnitCodeName", "tbComputationUnit", "cnvcComUnitCode", "cnvcComUnitName","");
				this.dgBOM.DataSource = dtchild;
				this.dgBOM.DataBind();
			}
			else
			{
				Entity.Inventory inv = new Entity.Inventory();
				DataTable dtchild = inv.ToTable().Clone();
				dtchild.Columns.Add("cnnBaseQtyN");
				this.dgBOM.DataSource = dtchild;
				this.dgBOM.DataBind();
			}			
		}

		private void GetInv(string strinvcode)
		{
			string strsql = "select * from tbinventory where cnvcinvcode='"+strinvcode+"'";
			DataTable dtInv = Helper.Query(strsql);
			Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(dtInv);
			Session["part"] = inv;

			strsql = "select * from tbinventory where cnvcinvcode in (select cnvcComponentInvCode from tbBillOfMaterials where cnvcPartInvcode='"+strinvcode+"')";
			DataTable dtchild = Helper.Query(strsql);
			Session["child"] = dtchild;

			strsql = "select * from tbBillOfMaterials where cnvcPartInvcode='"+strinvcode+"'";
			DataTable dtBOM = Helper.Query(strsql);
			Entity.BillOfMaterials bom = new BillOfMaterials(dtBOM);
			Session["BaseQtyD"] = bom.cnnBaseQtyD;
			Session["bom"] = dtBOM;
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
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.dgBOM.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBOM_CancelCommand);
			this.dgBOM.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBOM_EditCommand);
			this.dgBOM.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBOM_UpdateCommand);
			this.dgBOM.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgBOM_DeleteCommand);
			this.dgBOM.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgBOM_ItemDataBound);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion		

		#region button
		private void btnReturn_Click(object sender, System.EventArgs e)
		{
			//返回
			this.Response.Redirect("./wfmFormulaQuery.aspx");
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			//添加
			try
			{
				string strCount = this.txtBaseQtyD.Text;
				if(strCount == "")
				{
					Popup("请输入基础用量");
					return;
				}
				if(!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
				{
					Popup("基础用量请输入数字");
					return;
				}

				string strProductCode = txtProductCode.Text;
				if(strProductCode == "")
				{
					Popup("请选择存货");
					return;
				}
				
				if(Session["part"] == null || Session["bom"] == null)
				{
					this.Popup("数据异常！");
					return;
				}
				Entity.Inventory inv = Session["part"] as Entity.Inventory;
				DataTable dtbom = Session["bom"] as DataTable;
				if(dtbom.Rows.Count == 0)
				{
					this.Popup("请选择子件！");
					return;
				}
				foreach(DataRow drbom in dtbom.Rows)
				{
					Entity.BillOfMaterials bom = new BillOfMaterials(drbom);
					if(bom.cnnBaseQtyN <=0) 
					{
						this.Popup("请输入用量");
						return;
					}
					drbom["cnvcpartinvcode"] = inv.cnvcInvCode;
					drbom["cnnbaseqtyd"] = strCount;
				}
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "编辑配方";

				Business.InventoryFacade inf = new InventoryFacade();

				int ret = inf.UpdateBOM(operLog,inv.cnvcInvCode,dtbom);
				if(ret > 0 )
				{
					this.Popup("编辑配方成功！");
				}
				else
				{
					this.Popup("编辑配方失败！");
				}
						

			}
			catch(Exception ex)
			{
				Popup(ex.Message);
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			//查询母件
			Session["BaseQtyD"] = this.txtBaseQtyD.Text;
            this.ClientScript.RegisterStartupScript(this.GetType(), "querypart", "<script type=\"text/javascript\">OpenInvWin('part');</script>");

		}

		private void Button2_Click(object sender, System.EventArgs e)
		{
			Session["BaseQtyD"] = this.txtBaseQtyD.Text;
            this.ClientScript.RegisterStartupScript(this.GetType(), "querybom", "<script type=\"text/javascript\">OpenInvWin('child');</script>");
		}

		#endregion
		#region BOM
		private void dgBOM_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//编辑
			this.dgBOM.EditItemIndex = e.Item.ItemIndex;
			this.BindChild();
		}
		private void dgBOM_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string strCode = e.Item.Cells[5].Text;
			DataTable dtchild = (DataTable) Session["child"];
			DataRow[] drs = dtchild.Select("cnvcinvcode='" + strCode + "'");
			dtchild.Rows.Remove(drs[0]);
			Session["child"] = dtchild;

			if(Session["bom"] != null)
			{
				DataTable dtbom = Session["bom"] as DataTable;
				DataRow[] drboms = dtbom.Select("cnvcComponentInvCode='"+strCode+"'");
				dtbom.Rows.Remove(drboms[0]);
				Session["bom"] = dtbom;
			}
			BindChild();
		}

		private void dgBOM_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//取消
			this.dgBOM.EditItemIndex = -1;
			this.BindChild();
		}

		private void dgBOM_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//更新	
			string strCount = ((TextBox) e.Item.Cells[2].Controls[1]).Text;
			if(strCount == "")
			{
				Popup("请输入用量");
				return;
			}
			if(!Regex.IsMatch(strCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				Popup("请输入数字");
				return;
			}
			string strCode = e.Item.Cells[5].Text;

			DataTable dtbom = null;
			if(Session["bom"] != null)
			{
				dtbom = (DataTable) Session["bom"];
			}
			else
			{
				Entity.BillOfMaterials bom = new BillOfMaterials();
				dtbom = bom.ToTable().Clone();
			}
				
			DataRow[] drboms = dtbom.Select("cnvcComponentInvCode='"+strCode+"'");
			if(drboms.Length>0)
			{
				drboms[0]["cnnBaseQtyN"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			}
			else
			{
				DataRow drbom = dtbom.NewRow();
				drbom["cnvcComponentInvCode"] = strCode;
				drbom["cnnBaseQtyN"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
				dtbom.Rows.Add(drbom);
			}
				

			Session["bom"] = dtbom;

			DataTable dtchild = Session["child"] as DataTable;
			DataRow[] drchildren = dtchild.Select("cnvcInvCode='"+strCode+"'");
			if(drchildren.Length>0)
			{
				drchildren[0]["cnnbaseQtyN"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			}
			Session["child"] = dtchild;
			this.dgBOM.EditItemIndex = -1;
			BindChild();
			
		}

		private void dgBOM_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{				
			if(e.Item.ItemType == ListItemType.Item ||e.Item.ItemType == ListItemType.AlternatingItem)
			{
				LinkButton btnDelete = (LinkButton)(e.Item.Cells[4].Controls[1]);
				btnDelete.Attributes.Add("onClick","JavaScript:return confirm('确定删除？')");
				e.Item.Attributes.Add("onMouseOver","this.style.backgroundColor='#FFCC66'");
				e.Item.Attributes.Add("onMouseOut","this.style.backgroundColor='#ffffff'");
			}
		}

		#endregion
	}
}
