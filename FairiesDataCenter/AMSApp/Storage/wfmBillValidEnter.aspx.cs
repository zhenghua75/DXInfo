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
using System.Text.RegularExpressions;

namespace AMSApp.Storage
{
	/// <summary>
	/// Summary description for wfmBillValidEnter.
	/// </summary>
	public class wfmBillValidEnter : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlValidDept;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblReceiveDate;
		protected System.Web.UI.WebControls.Button btnValidEnter;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtAssignID;
		protected System.Web.UI.WebControls.TextBox txtReceiveOper;
		protected System.Web.UI.WebControls.Label lblAssignID;
		protected System.Web.UI.WebControls.Label Label4;

		BusiComm.StorageBusi StoBusi;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
				if (!IsPostBack )
				{
					this.FillDropDownList("NewDept",ddlValidDept,"vcCommSign='SalesRoom'");
					if(ls1.strDeptID!="CEN00"&&ls1.strLimit!="CL001")
					{
						this.ddlValidDept.SelectedIndex=this.ddlValidDept.Items.IndexOf(this.ddlValidDept.Items.FindByValue(ls1.strNewDeptID));
						this.ddlValidDept.Enabled=false;
					}
					this.txtReceiveOper.Text=ls1.strOperName;
					this.lblReceiveDate.Text=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

					Session.Remove("QUERY");
					Session.Remove("page_view");
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
			this.btnValidEnter.Click += new System.EventHandler(this.btnValidEnter_Click);
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			string strDeptID=this.ddlValidDept.SelectedValue;
			string strAssignID=this.txtAssignID.Text.Trim();

			if(strAssignID=="")
			{
				this.SetErrorMsgPageBydirHistory("生产序号不能为空！");
				return;
			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strAssignID",strAssignID);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				DataTable dtout=StoBusi.GetAssignToValidEnter(htPara);
				if(dtout==null)
				{
					this.lblAssignID.Text="";
					this.SetErrorMsgPageBydir("查询出错，请重试！");
					return;
				}
				else
				{
					this.TableConvert(dtout,"cnvcShipDeptID","NewDept");
					this.TableConvert(dtout,"cnvcProductType","tbNameCodeToStorage","vcCommSign='PRODUCTTYPE'");
					this.lblAssignID.Text=strAssignID;
					dtout.TableName="验收信息表";
					DataTable dtexcel=dtout.Copy();
					Session["QUERY"] = dtout;
				}
				
				//				this.DataGrid1.PageSize = 30;
				this.DataGrid1.DataSource=dtout;
				this.DataGrid1.DataBind();
			}
			catch(Exception er)
			{
				this.lblAssignID.Text="";
				this.clog.WriteLine(er);
				this.SetErrorMsgPageBydir("查询错误，请重试！");
				return;
			}
		}

		private void btnValidEnter_Click(object sender, System.EventArgs e)
		{
			if(this.lblAssignID.Text.Trim()=="")
			{
				this.SetErrorMsgPageBydir("生产序号错误，请重新查询！");
				return;
			}

			DataTable dtIn=(DataTable)Session["QUERY"];
			if(dtIn==null||dtIn.Rows.Count==0)
			{
				this.SetErrorMsgPageBydirHistory("没有任何需要验收的分货单内容，请检查生产序号是否正确！");
				return;
			}

			string strDeptID=this.ddlValidDept.SelectedValue;
			string strAssignID=this.lblAssignID.Text.Trim();
			string strReceiveOper=this.txtReceiveOper.Text.Trim();
			string strValidDate=DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString();

			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strAssignID",strAssignID);
			htPara.Add("strReceiveOper",strReceiveOper);
			htPara.Add("strValidDate",strValidDate);
			htPara.Add("strOperID",ls1.strOperName);
			
			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			StoBusi=new BusiComm.StorageBusi(strcons);
			try
			{
				if(StoBusi.AssignToValidEnterFinal(htPara,dtIn))
				{
					this.SetSuccMsgPageBydir("订单验收入库成功！","Storage/wfmBillValidEnter.aspx");
					return;
				}
				else
				{
					this.SetErrorMsgPageBydir("订单验收入库时发生错误，请重试！");
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

		private void DataGrid1_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=e.Item.ItemIndex;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}

		private void DataGrid1_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string strTravelCount=((TextBox)e.Item.Cells[11].Controls[0]).Text.Trim();
			string strValidOkCount=((TextBox)e.Item.Cells[12].Controls[0]).Text.Trim();
			string strValidNoCount=((TextBox)e.Item.Cells[13].Controls[0]).Text.Trim();
			if(strTravelCount==""||!Regex.IsMatch(strTravelCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("运输损耗量必须是数字！");
				return;
			}
			if(strValidOkCount==""||!Regex.IsMatch(strValidOkCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("验收合格量必须是数字！");
				return;
			}
			if(strValidNoCount==""||!Regex.IsMatch(strValidNoCount,@"^[+|-]{0,1}(\d*)\.{0,1}\d{0,}$"))
			{
				this.SetErrorMsgPageBydirHistory("验收不合格量必须是数字！");
				return;
			}

			double RealCount=Math.Round(double.Parse(e.Item.Cells[10].Text),2);
			double TravelCount=Math.Round(double.Parse(strTravelCount),2);
			double ValidOkCount=Math.Round(double.Parse(strValidOkCount),2);
			double ValidNoCount=Math.Round(double.Parse(strValidNoCount),2);
			if(TravelCount+ValidOkCount+ValidNoCount!=RealCount)
			{
				this.SetErrorMsgPageBydirHistory("实发量应等于运输损耗量+验收合格量+验收不合格量，请检查！");
				return;
			}

			DataTable dttmp=(DataTable)Session["QUERY"];
			Session.Remove("QUERY");
			dttmp.Rows[e.Item.ItemIndex]["cnnTravelCount"]=TravelCount.ToString();
			dttmp.Rows[e.Item.ItemIndex]["cnnValidOkCount"]=ValidOkCount.ToString();
			dttmp.Rows[e.Item.ItemIndex]["cnnValidNoCount"]=ValidNoCount.ToString();
			Session["QUERY"]=dttmp;
			this.DataGrid1.EditItemIndex=-1;
			this.DataGrid1.DataSource=(DataTable)Session["QUERY"];
			this.DataGrid1.DataBind();
		}
	}
}
