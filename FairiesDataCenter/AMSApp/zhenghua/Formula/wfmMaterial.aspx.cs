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
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Formula
{
	/// <summary>
	/// wfmMaterial 的摘要说明。
	/// </summary>
	public class wfmMaterial : wfmBase
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtMaterialCode;
		protected System.Web.UI.WebControls.TextBox txtMaterialName;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!this.IsPostBack)
			{
				//绑定grid
				//BindMaterial();
			}
		}

		private void BindMaterial()
		{
			string strSql = "select cnvcMaterialCode as cnvcOldMaterialCode,* from tbMaterial where cnvcMaterialCode like '%" + txtMaterialCode.Text +
			                "%' and cnvcMaterialName like '%" + txtMaterialName.Text + "%'";
			DataTable dtMaterial = Helper.Query(strSql);
			this.DataTableConvert(dtMaterial, "cnvcLeastUnit", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType = 'LEASTUNIT'");
			this.DataTableConvert(dtMaterial, "cnvcProductType", "tbNameCode", "cnvcCode", "cnvcName", "cnvcType = 'PRODUCTTYPE'");
			this.DataTableConvert(dtMaterial, "cnvcProductClass", "tbProductClass", "cnvcProductClassCode", "cnvcProductClassName", "");

//			MaterialFacade mf = new MaterialFacade();
//			DataTable dtMaterial = mf.GetMaterials(this.txtMaterialCode.Text,this.txtMaterialName.Text);
			DataGrid1.DataSource = dtMaterial;
			DataGrid1.DataBind();
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//编辑
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			BindMaterial();
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			//分页
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			BindMaterial();
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//取消
			this.DataGrid1.EditItemIndex=-1;
			BindMaterial();
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//更新
			try
			{
				//if(!JudgeIsCode())
				Material mat = new Material();
				mat.cnvcMaterialCode = ((TextBox)e.Item.Cells[0].Controls[1]).Text;
				

				mat.cnvcMaterialName = ((TextBox)e.Item.Cells[1].Controls[0]).Text;
				mat.cnvcLeastUnit = ((DropDownList) e.Item.Cells[2].Controls[1]).SelectedValue;
				mat.cnnPrice = decimal.Parse(((TextBox)e.Item.Cells[3].Controls[0]).Text);
				mat.cnvcProductType = ((DropDownList) e.Item.Cells[4].Controls[1]).SelectedValue;
				mat.cnnConversion = decimal.Parse(((TextBox)e.Item.Cells[6].Controls[0]).Text);
				mat.cnvcUnit = ((TextBox)e.Item.Cells[7].Controls[0]).Text;
				mat.cnvcStandardUnit = ((TextBox)e.Item.Cells[8].Controls[0]).Text;
				mat.cnnStatdardCount = decimal.Parse(((TextBox)e.Item.Cells[9].Controls[0]).Text);
				
				mat.cnvcProductClass = ((DropDownList) e.Item.Cells[5].Controls[1]).SelectedValue;
				
				if(!JudgeIsCode(mat.cnvcProductType,mat.cnvcProductClass,mat.cnvcMaterialCode))
				{
					//Popup("编码错误");
					//return;
					throw new Exception("编码错误");
				}

				string strOldMaterialCode = e.Item.Cells[11].Text;
				
				if(strOldMaterialCode != mat.cnvcMaterialCode)
				{
					string strSql = "select * from tbMaterial where cnvcMaterialCode='"+mat.cnvcMaterialCode+"'";
					DataTable dtMaterial = Helper.Query(strSql);
					if(dtMaterial.Rows.Count > 0)
					{
						//throw new Exception("相同编码原料材料已存在");
						Popup("相同编码原料材料已存在");
						return;
					}
				}
				
				OperLog operLog = new OperLog();
				operLog.cnvcOperID = oper.strLoginID;
				operLog.cnvcDeptID = oper.strDeptID;
				operLog.cnvcOperType = "修改原料材料";

				MaterialFacade mf = new MaterialFacade();
				mf.UpdateMaterial(mat, strOldMaterialCode,operLog);
				this.DataGrid1.EditItemIndex=-1;
				BindMaterial();
				Popup("更新成功！");
			}
			catch(Exception ex)
			{
				Popup(ex.Message);
				return;
			}
			
			
		}

		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			//控制修改TextBox的宽度     
			System.Web.UI.WebControls.TextBox   tb;   
			int   intLength;  
 
//			DataTable dtLeastUnit = Helper.BindCommCode("Least");
//			DataTable dtProductType = Helper.BindCommCode("PType");
			

			if(e.Item.ItemType==ListItemType.EditItem)
			{
				DropDownList ddlLeastUnit=(DropDownList)e.Item.FindControl("ddlLeastUnit");

				BindNameCode(ddlLeastUnit, "cnvctype='LEASTUNIT'");

				ddlLeastUnit.Items.FindByText(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcLeastUnit"))).Selected = true;

				DropDownList ddlProductType=(DropDownList)e.Item.FindControl("ddlProductType");
//				ddlProductType.DataSource = dtProductType;
//				ddlProductType.DataValueField = "vcCommCode";
//				ddlProductType.DataTextField = "vcCommName";
//				ddlProductType.DataBind();

				BindNameCode(ddlProductType, "cnvcType='PRODUCTTYPE' and (cnvcCode='Raw' or cnvcCode='Pack')");

				ddlProductType.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductType"))).Selected = true;

				DropDownList ddlProductClass=(DropDownList)e.Item.FindControl("ddlProductClass");
				BindProductClass(ddlProductClass,
				                 "cnvcProductType='" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductType")) + "'");
				ListItem li =
					ddlProductClass.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductClass")));
				if(li != null)
				{
					li.Selected = true;
				}
				//ddlProductClass.Items.FindByValue(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "cnvcProductClass"))).Selected = true;

				//循环所有单元   
				for(int   i=0;   i<e.Item.Cells.Count-1;i++)   
				{   
					//单元内是否有控件   
					if(e.Item.Cells[i].Controls.Count>0)   
					{   
						//如果是TextBox控件   
						if(e.Item.Cells[i].Controls[0].GetType().ToString()=="System.Web.UI.WebControls.TextBox")   
						{   
							tb   =   (TextBox)e.Item.Cells[i].Controls[0];   
							intLength   =   0;   
							intLength   =   tb.Text.Length;   
							intLength   =   intLength   *   7;   
							if(intLength==0)   intLength=20;   
							tb.Width   =   Unit.Pixel(70);   
							//tb.CssClass="DataGridTextBox";   //你的CSS样式表名称   
						}   
					}   
				}
			}

		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//取消
			this.txtMaterialCode.Text = "";
			this.txtMaterialName.Text = "";
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("./wfmAddMaterial.aspx");
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			//查询
			this.DataGrid1.CurrentPageIndex = 0;
			this.BindMaterial();

		}
		protected void ddlProductType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DropDownList ddl = (DropDownList)sender;
			string strCode = ddl.SelectedValue;
			TableCell cell = (TableCell)ddl.Parent;
			DataGridItem item = (DataGridItem)cell.Parent;
			DropDownList ddlProductClass=(DropDownList)item.FindControl("ddlProductClass");
			BindProductClass(ddlProductClass,
			                 "cnvcProductType='" + strCode + "'");
							
		}
	}
}
