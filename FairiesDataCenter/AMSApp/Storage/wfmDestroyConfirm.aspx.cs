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

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmDestroyConfirm.
	/// </summary>
	public class wfmDestroyConfirm :wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlLoseDept;
		protected System.Web.UI.WebControls.DropDownList ddlLoseType;
		protected System.Web.UI.WebControls.DropDownList ddlDestroyFlag;
		protected System.Web.UI.WebControls.TextBox txtConfirmOper;
		protected System.Web.UI.WebControls.Label lblConfirmDate;
		protected System.Web.UI.WebControls.Label Label1;
	
		protected string strEndDate;
		protected string strBeginDate;
		protected System.Web.UI.WebControls.Button btnBatchConfirm;
		protected System.Web.UI.WebControls.Button btnAllSelect;
		BusiComm.StorageBusi StoBusi;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept", this.ddlLoseDept,"","全部");
					strBeginDate=DateTime.Now.ToShortDateString();
					strEndDate=DateTime.Now.ToShortDateString();
					this.ddlLoseType.Items.Add(new ListItem("全部","全部"));
					this.ddlLoseType.Items.Add(new ListItem("销售中损耗","销售中损耗"));
					this.ddlLoseType.Items.Add(new ListItem("销售剩余损耗","销售剩余损耗"));
					this.ddlLoseType.Items.Add(new ListItem("验收不合格退货","验收不合格退货"));
					this.ddlLoseType.SelectedIndex=0;

					this.ddlDestroyFlag.Items.Add(new ListItem("未确认","0"));
					this.ddlDestroyFlag.Items.Add(new ListItem("已确认","1"));
					this.ddlDestroyFlag.SelectedIndex=0;

					this.txtConfirmOper.Text=ls1.strOperName;
					this.lblConfirmDate.Text=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

					if(ls1.strDeptID!="FYZX1"&&ls1.strDeptID!="CEN00")
					{
						this.btnAllSelect.Enabled=false;
						this.btnBatchConfirm.Enabled=false;
					}
					else
					{
						this.btnAllSelect.Enabled=true;
						this.btnBatchConfirm.Enabled=true;
					}

					Session.Remove("QUERY");
					Session.Remove("page_view");
				}
				else
				{
					strBeginDate = Request.Form["txtBegin"].ToString();
					strEndDate =  Request.Form["txtEnd"].ToString();
				}
			}
			else
			{
				Response.Redirect("../Exit.aspx");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btnAllSelect.Click += new System.EventHandler(this.btnAllSelect_Click);
			this.btnBatchConfirm.Click += new System.EventHandler(this.btnBatchConfirm_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			strBeginDate = Request.Form["txtBegin"].ToString();
			strEndDate =  Request.Form["txtEnd"].ToString();
			if(strBeginDate==""||strEndDate==""||strBeginDate==null||strEndDate==null)
			{
				this.SetErrorMsgPageBydir("时间不能为空，请重新选择时间！");
				return;
			}
			string strLoseDept=this.ddlLoseDept.SelectedValue;
			string strLoseType=this.ddlLoseType.SelectedValue;
			string strConfirmFlag=this.ddlDestroyFlag.SelectedValue;

			Hashtable htPara=new Hashtable();
			htPara.Add("strLoseDept",strLoseDept);
			htPara.Add("strLoseType",strLoseType);
			htPara.Add("strConfirmFlag",strConfirmFlag);
			htPara.Add("strBeginDate",strBeginDate);
			htPara.Add("strEndDate",strEndDate);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetLoseInfo(htPara);
				if(dtout==null)
				{
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"cnvcDeptID","NewDept");
					if(strConfirmFlag=="1")
					{
						this.DataGrid1.Columns[0].Visible=false;
						this.btnAllSelect.Enabled=false;
						this.btnBatchConfirm.Enabled=false;
					}
					else
					{
						CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
						if(ls1.strDeptID!="FYZX1"&&ls1.strDeptID!="CEN00")
						{
							this.DataGrid1.Columns[0].Visible=false;
							this.btnAllSelect.Enabled=false;
							this.btnBatchConfirm.Enabled=false;
						}
						else
						{
							this.DataGrid1.Columns[0].Visible=true;
							this.btnAllSelect.Enabled=true;
							this.btnBatchConfirm.Enabled=true;
						}
					}
					dtout.TableName="验收信息表";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
				}
				
				this.DataGrid1.CurrentPageIndex=0;
				this.DataGrid1.PageSize = 20;
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
//
//		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=-1;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
//		{
//			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
//			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//			this.DataGrid1.DataBind();
//		}
//
//		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
//		{
//			string strLoseSerial=e.Item.Cells[0].Text.Trim();
//			string strConfirmOper=this.txtConfirmOper.Text.Trim();
//			string strConfirmDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();
//
//			Hashtable htPara=new Hashtable();
//			htPara.Add("strLoseSerial",strLoseSerial);
//			htPara.Add("strConfirmOper",strConfirmOper);
//			htPara.Add("strConfirmDate",strConfirmDate);
//
//			Hashtable htapp=(Hashtable)Application["appconf"];
//			string strcons=(string)htapp["cons"];
//			StoBusi=new BusiComm.StorageBusi(strcons);
//			try
//			{
//				if(StoBusi.UpdateLoseConfirm(htPara))
//				{
//					DataTable dttmp=(DataTable)Session["QUERY"];
//					Session.Remove("QUERY");
//					dttmp.Rows[e.Item.ItemIndex]["cnvcDestroyFlag"]="已确认";
//					dttmp.Rows[e.Item.ItemIndex]["cnvcDestroyOperID"]=strConfirmOper;
//					dttmp.Rows[e.Item.ItemIndex]["cndDestroyDate"]=strConfirmDate;
//					Session["QUERY"]=dttmp;
//					this.DataGrid1.EditItemIndex=-1;
//					this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
//					this.DataGrid1.DataBind();
//				}
//				else
//				{
//					this.SetErrorMsgPageBydir("损耗确认失败，请重试！");
//					return;
//				}
//			}
//			catch(Exception er)
//			{
//				this.clog.WriteLine(er);
//				this.SetErrorMsgPageBydir("查询错误，请重试！");
//				return;
//			}
//		}

		private void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			CheckBox chk1;
			bool isselect=false;
			foreach(DataGridItem oitem in this.DataGrid1.Items)
			{
				chk1=(CheckBox)oitem.FindControl("chkLose");
				if(chk1.Checked)
				{
					isselect=true;
					break;
				}
			}
			if(!isselect&&this.btnAllSelect.Text=="单页全选")
			{
				this.DataGrid1.CurrentPageIndex=e.NewPageIndex;
				this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
				this.DataGrid1.DataBind();
			}
		}

		private void btnAllSelect_Click(object sender, System.EventArgs e)
		{
			CheckBox chk1;
			if(this.btnAllSelect.Text=="单页全选")
			{
				foreach(DataGridItem oitem in this.DataGrid1.Items)
				{
					chk1=(CheckBox)oitem.FindControl("chkLose");
					chk1.Checked=true;
				}
				this.btnAllSelect.Text="单页全取消";
			}
			else
			{
				foreach(DataGridItem oitem in this.DataGrid1.Items)
				{
					chk1=(CheckBox)oitem.FindControl("chkLose");
					chk1.Checked=false;
				}
				this.btnAllSelect.Text="单页全选";
			}
		}

		private void btnBatchConfirm_Click(object sender, System.EventArgs e)
		{
			CheckBox chk1;
			string strserial="";
			int confirmcount=0;
			foreach(DataGridItem oitem in this.DataGrid1.Items)
			{
				chk1=(CheckBox)oitem.FindControl("chkLose");
				if(chk1.Checked)
				{
					strserial+=oitem.Cells[1].Text.Trim()+",";
					confirmcount++;
				}
			}

			if(strserial.Length>0)
			{
				strserial=strserial.Substring(0,strserial.Length-1);
				string strConfirmOper=this.txtConfirmOper.Text.Trim();
				string strConfirmDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

				Hashtable htPara=new Hashtable();
				htPara.Add("strLoseSerial",strserial);
				htPara.Add("strConfirmOper",strConfirmOper);
				htPara.Add("strConfirmDate",strConfirmDate);

				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				StoBusi=new BusiComm.StorageBusi(strcons);
				try
				{
					if(StoBusi.UpdateLoseConfirm(htPara))
					{
						this.SetSuccMsgPageBydir("损耗确认成功，本次共确认："+confirmcount.ToString()+"条损耗记录！","Storage/wfmDestroyConfirm.aspx");
						return;
					}
					else
					{
						this.SetErrorMsgPageBydir("损耗确认失败，请重试！");
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
}
