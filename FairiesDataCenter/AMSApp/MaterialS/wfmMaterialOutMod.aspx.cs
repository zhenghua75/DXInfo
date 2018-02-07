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
	/// wfmMaterialOutMod 的摘要说明。
	/// </summary>
	public class wfmMaterialOutMod : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList ddlMaterialType;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtMaterialName;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtMaterialCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOutSerial;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
	
		protected string strEndDate;
		protected string strBeginDate;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label9;
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
			if(!this.IsPostBack)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				Session.Remove("QUERY");
				strBeginDate=DateTime.Now.ToShortDateString();
				strEndDate=DateTime.Now.ToShortDateString();
				this.FillDropDownList("tbNameCodeToStorage",this.ddlMaterialType,"vcCommSign='MaterialType'","全部");
				this.FillDropDownList("NewDept",this.ddlDept,"vcCommSign='SalesRoom'");
				if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
				{
					this.ddlDept.SelectedIndex=this.ddlDept.Items.IndexOf(this.ddlDept.Items.FindByValue(ls1.strNewDeptID));
					this.ddlDept.Enabled=false;
				}
			}
			else
			{
				strBeginDate = Request.Form["txtBegin"].ToString();
				strEndDate =  Request.Form["txtEnd"].ToString();
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.DataGrid1.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_DeleteCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBbind()
		{
			Session.Remove("oldOutCount");
			Session.Remove("OutForMod");
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}

			Session.Remove("QUERY");

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			msb1=new MaterialSBusi(strcons);
			string strMaterialCode=txtMaterialCode.Text.Trim();
			string strMaterialName=txtMaterialName.Text.Trim();
			string strMaterialType=ddlMaterialType.SelectedValue;
			string strOutSerial=txtOutSerial.Text.Trim();
			Hashtable htpara=new Hashtable();
			htpara.Add("strMaterialCode",strMaterialCode);
			htpara.Add("strMaterialName",strMaterialName);
			htpara.Add("strMaterialType",strMaterialType);
			htpara.Add("strOutSerial",strOutSerial);
			htpara.Add("strBeginDate",strBeginDate);
			htpara.Add("strEndDate",strEndDate);
			htpara.Add("strBatchNo",this.txtBatchNo.Text.Trim());
			htpara.Add("strDeptID",this.ddlDept.SelectedValue);

			try
			{
				DataTable dtout=msb1.GetMaterialOutForMod(htpara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					dtout.TableName="原材料出库查询";
					Session["OutForMod"] = dtout;
					this.DataGrid1.EditItemIndex=-1;
					this.DataGrid1.Columns[16].Visible=true;
					CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
					if(ls1.strDeptID=="CEN00"||ls1.strNewDeptID=="")
					{
						this.DataGrid1.Columns[15].Visible=false;
						this.DataGrid1.Columns[16].Visible=false;
					}
					this.DataGrid1.DataSource=dtout;
					this.DataGrid1.DataBind();
				}
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			this.DBbind();
		}

		
		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			Session.Remove("oldOutCount");
			string oldOutCount=e.Item.Cells[10].Text.Trim();
			Session["oldOutCount"]=oldOutCount;
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.Columns[16].Visible=false;
			this.DataGrid1.DataSource=(DataTable)Session["OutForMod"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.Columns[16].Visible=true;
			this.DataGrid1.DataSource=(DataTable)Session["OutForMod"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				string newOutCount=((TextBox)e.Item.Cells[10].Controls[0]).Text.Trim();
				string OutSerial=e.Item.Cells[0].Text.Trim();
				if(newOutCount=="")
				{
					this.Popup("请输入出库量");
					return;
				}
				if(!this.JudgeIsNum(newOutCount,"出库量"))
				{
					return;
				}
				
				double dnewOutCount=Math.Round(double.Parse(newOutCount),2);
				double doldOutCount=Math.Round(double.Parse(Session["oldOutCount"].ToString()),2);
				double dLastCount=Math.Round(double.Parse(e.Item.Cells[9].Text.Trim()),2);
				string strChangeCount=(dnewOutCount-doldOutCount).ToString();
				string strNewCurCount=(dLastCount-dnewOutCount).ToString();

				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				Hashtable htpara=new Hashtable();
				htpara.Add("strNewOutCount",newOutCount);
				htpara.Add("strNewCurCount",strNewCurCount);
				htpara.Add("strSerialOld",OutSerial);
				htpara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
				htpara.Add("strOperName",ls1.strOperName);
				htpara.Add("strDeptID",ls1.strNewDeptID);
				htpara.Add("strChangeCount",strChangeCount);
				htpara.Add("strMaterialCode",e.Item.Cells[2].Text.Trim());
				htpara.Add("strBatchNo",e.Item.Cells[1].Text.Trim());

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				msb1=new MaterialSBusi(strcons);

				CMSMStruct.MaterialSStruct msscur= msb1.GetMaterialInfo(e.Item.Cells[1].Text.Trim(),e.Item.Cells[2].Text.Trim());
				if((dnewOutCount-doldOutCount)>0&&msscur.dCurCount<(dnewOutCount-doldOutCount))
				{
					this.Popup("当前数量不足！");
					return;
				}

				if(msb1.MaterialOutMod(htpara))
				{
					this.Popup("原材料出库修正成功！");
					this.DBbind();
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("原材料出库修正失败！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			try
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				Hashtable htpara=new Hashtable();
				htpara.Add("strOutCount",e.Item.Cells[10].Text.Trim());
				htpara.Add("strSerialOld",e.Item.Cells[0].Text.Trim());
				htpara.Add("strOperDate",DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString());
				htpara.Add("strOperName",ls1.strOperName);
				htpara.Add("strDeptID",ls1.strNewDeptID);
				htpara.Add("strMaterialCode",e.Item.Cells[2].Text.Trim());
				htpara.Add("strBatchNo",e.Item.Cells[1].Text.Trim());

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				msb1=new MaterialSBusi(strcons);

				if(msb1.MaterialOutModDetele(htpara))
				{
					this.Popup("原材料出库修正删除成功！");
					this.DBbind();
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("原材料出库修正删除失败！");
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}
	}
}
