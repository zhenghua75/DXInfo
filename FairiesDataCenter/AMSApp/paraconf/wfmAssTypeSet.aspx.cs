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

namespace AMSApp.paraconf
{
	/// <summary>
	/// wfmAssTypeSet 的摘要说明。
	/// </summary>
	public partial class wfmAssTypeSet : wfmBase
	{
		BusiComm.Manager m1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(Session["Login"]!=null)
			{
				if (!IsPostBack )
				{
					this.ddlDisp.Items.Add("请选择");
					this.ddlDisp.Items.Add("是");
					this.ddlDisp.Items.Add("否");
					this.ddlDisp.SelectedIndex=0;
					Session.Remove("QUERY");
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
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
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);

		}
		#endregion

		private void DGBind()
		{
			Session.Remove("Query");
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new BusiComm.Manager(strcons);
			try
			{
				DataTable dtout=m1.GetAssType();
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				
				Session["Query"]=dtout;
				this.DataGrid1.EditItemIndex=-1;
				this.DataGrid1.DataSource = dtout;
				this.DataGrid1.DataBind();
				((Button)this.DataGrid1.Items[0].Cells[4].Controls[0]).Visible=false;
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		protected void btnAdd_Click(object sender, System.EventArgs e)
		{
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			if(this.txtAssTypeName.Text.Trim().Length>=10)
			{
				this.Popup("会员类型名称不能超过5个中文字！");
				return;
			}
			if(this.txtAssTypeCode.Text.Trim().Length!=5)
			{
				this.Popup("会员类型编码必须为5个字符！");
				return;
			}
			else if(this.txtAssTypeCode.Text.Trim().Substring(0,2)!="AT")
			{
				this.Popup("会员类型编码必须与“AT”开头！");
				return;
			}
			else if(m1.GetAssTypeExist(this.txtAssTypeCode.Text.Trim()))
			{
				this.Popup("会员类型编码已经存在！");
				return;
			}
			if(this.txtAssTypeRate.Text.Trim()=="")
			{
				this.Popup("折扣不能为空！");
				return;
			}
			else if(!this.JudgeIsNum(this.txtAssTypeRate.Text.Trim(),"折扣必须是数字，！"))
				return;
			else if(this.txtAssTypeRate.Text.Trim()=="1"||this.txtAssTypeRate.Text.Trim()=="10")
			{
				this.Popup("折扣必须是0到10以内的数字，且不能为1和10！");
				return;
			}
			else if(double.Parse(this.txtAssTypeRate.Text.Trim())<0||double.Parse(this.txtAssTypeRate.Text.Trim())>=10)
			{
				this.Popup("折扣必须是0到10以内的数字，且不能为1和10！");
				return;
			}

			if(this.ddlDisp.SelectedIndex==0)
			{
				this.Popup("请选择是否屏蔽！");
				return;
			}

			string strComments="";
			if(this.ddlDisp.SelectedIndex==1)
				strComments="X"+this.txtAssTypeRate.Text.Trim();
			else
				strComments="Y"+this.txtAssTypeRate.Text.Trim();
			try
			{
				if(!m1.ModifyAssType(this.txtAssTypeCode.Text.Trim(),this.txtAssTypeName.Text.Trim(),strComments,"ADD"))
				{
					this.SetErrorMsgPageBydir("添加会员类型出错，请重试！");
					return;
				}
				else
				{
					this.Popup("添加会员类型成功！");
					this.DGBind();
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("系统错误，请重试！");
				return;
			}
		}

		protected void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DGBind();
		}

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			string strdis=((Label)this.DataGrid1.Items[e.Item.ItemIndex].Cells[3].FindControl("lbldis")).Text.Trim();
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
			DropDownList ddldis=(DropDownList)this.DataGrid1.Items[e.Item.ItemIndex].Cells[3].FindControl("dgddlDisp");
			ddldis.Items.Add("是");
			ddldis.Items.Add("否");
			ddldis.SelectedIndex=ddldis.Items.IndexOf(ddldis.Items.FindByText(strdis));
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string strAssTypeName=((TextBox)e.Item.Cells[0].Controls[0]).Text.Trim();
			string strAssTypeCode=e.Item.Cells[1].Text.Trim();
			string strAssTypeRate=((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
			string strComments=((DropDownList)e.Item.Cells[3].Controls[1]).SelectedItem.Text.Trim();
			if(strAssTypeName.Length>=10)
			{
				this.Popup("会员类型名称不能超过5个中文字！");
				return;
			}
			if(strAssTypeRate=="")
			{
				this.Popup("折扣不能为空！");
				return;
			}
			else if(!this.JudgeIsNum(strAssTypeRate,"折扣必须是数字，！"))
				return;
			else if(strAssTypeRate=="1"||strAssTypeRate=="10")
			{
				this.Popup("折扣必须是0到10以内的数字，且不能为1和10！");
				return;
			}
			else if(double.Parse(strAssTypeRate)<0||double.Parse(strAssTypeRate)>=10)
			{
				this.Popup("折扣必须是0到10以内的数字，且不能为1和10！");
				return;
			}

			if(strComments=="是")
				strComments="X"+strAssTypeRate;
			else
				strComments="Y"+strAssTypeRate;

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			m1=new Manager(strcons);
			try
			{
				if(!m1.ModifyAssType(strAssTypeCode,strAssTypeName,strComments,"MOD"))
				{
					this.SetErrorMsgPageBydir("修改会员类型出错，请重试！");
					return;
				}
				else
				{
					this.Popup("修改会员类型成功！");
					this.DGBind();
					return;
				}
			}
			catch(Exception er)
			{
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("系统错误，请重试！");
				return;
			}
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}
	}
}
