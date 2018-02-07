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

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmPoStockReturnAdd 的摘要说明。
	/// </summary>
	public class wfmPoStockReturnAdd : wfmBase
	{
		protected System.Web.UI.WebControls.Button btreturn;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.TextBox txtArvAddress;
		protected System.Web.UI.WebControls.TextBox txtShipAddress;
		protected System.Web.UI.WebControls.TextBox txtWHPerson;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.DropDownList ddlDeptID;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.TextBox txtEnterCode;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;

		protected string strArriveDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			string strRdId=Request.QueryString["rdid"];
			if(!IsPostBack)
			{
				this.txtRdID.Visible=false;
				if(strRdId==""||strRdId==null)
				{
					this.btAdd.Enabled=true;
					this.btMod.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlDeptID,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
					if(this.oper.strNewDeptID=="CEN00")
					{
						this.btAdd.Enabled=false;
						this.btMod.Enabled=false;
					}
					DataTable dtwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
					if(dtwh!=null&&dtwh.Rows.Count>0)
						this.txtWHPerson.Text=dtwh.Rows[0]["vcOperName"].ToString();
					else
						this.txtWHPerson.Text="";
					strArriveDate=DateTime.Now.ToShortDateString();
					this.txtEnterCode.Enabled=false;
					string strCycle=DateTime.Today.Year.ToString();
					if(DateTime.Today.Month<10)
						strCycle+="0"+DateTime.Today.Month.ToString();
					else
						strCycle+=DateTime.Today.Month.ToString();
					this.txtEnterCode.Text="PRE"+strCycle+"----";
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.FillDropDownList("NewDept",this.ddlDeptID,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
					string strSql = "select * from tbRdRecord where cnnRdID='" + strRdId + "'";
					DataTable dtout = Helper.Query(strSql);
					this.txtRdID.Text=strRdId;
					this.txtEnterCode.Text=dtout.Rows[0]["cnvcCode"].ToString();
					this.ddlWhouse.SelectedIndex=this.ddlWhouse.Items.IndexOf(this.ddlWhouse.Items.FindByValue(dtout.Rows[0]["cnvcWhCode"].ToString()));
					this.ddlDeptID.SelectedIndex=this.ddlDeptID.Items.IndexOf(this.ddlDeptID.Items.FindByValue(dtout.Rows[0]["cnvcDepID"].ToString()));
					this.strArriveDate=((DateTime)dtout.Rows[0]["cndARVDate"]).ToShortDateString();
					this.txtArvAddress.Text=dtout.Rows[0]["cnvcARVAddress"].ToString();
					this.txtShipAddress.Text=dtout.Rows[0]["cnvcShipAddress"].ToString();
					this.txtComments.Text=dtout.Rows[0]["cnvcComments"].ToString();
					this.txtWHPerson.Text=dtout.Rows[0]["cnvcWhpersonName"].ToString();
					this.txtEnterCode.Enabled=false;
					this.ddlWhouse.Enabled=false;
					this.ddlDeptID.Enabled=false;
					if(dtout.Rows[0]["cnvcState"].ToString()!="0")
					{
						this.txtWHPerson.Enabled=false;
						this.btMod.Enabled=false;
					}
				}
			}
			else
			{
				strArriveDate = Request.Form["txtArvDate"].ToString();
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
			this.ddlWhouse.SelectedIndexChanged += new System.EventHandler(this.ddlWhouse_SelectedIndexChanged);
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			strArriveDate = Request.Form["txtArvDate"].ToString();
			if(strArriveDate==""||strArriveDate==null)
			{
				this.Popup("到货时间不能为空，请重新选择时间！");
                this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
				return;
			}
			Entity.RdRecord rd=new RdRecord();
			rd.cnvcCode=this.txtEnterCode.Text.Trim();
			rd.cnvcRdCode="RD002";
			rd.cnvcRdFlag="1";
			rd.cnvcWhCode=this.ddlWhouse.SelectedValue;
			rd.cnvcDepID=this.ddlDeptID.SelectedValue;
			rd.cnvcOperName=this.oper.strOperName;
			rd.cnvcComments=this.txtComments.Text.Trim();
			rd.cnvcMaker=this.oper.strOperName;
			rd.cnvcShipAddress=this.txtShipAddress.Text.Trim();
			rd.cndARVDate=DateTime.Parse(strArriveDate);
			rd.cnvcARVAddress=this.txtArvAddress.Text.Trim();
			rd.cnvcState="0";

			if(rd.cnvcCode==""||rd.cnvcCode.Length!=13)
			{
				this.Popup("采购退货单号不正确！");
				return;
			}
			if(rd.cnvcWhCode=="")
			{
				this.Popup("仓库不能为空！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "添加采购退货主单";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.AddRdRecordCom("RD002",ol,rd);
			if(ret > 0 )
			{
				this.Popup("添加采购退货单内容成功！");
			}
			else
			{
				this.Popup("添加采购退货单内容失败！");
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			strArriveDate = Request.Form["txtArvDate"].ToString();
			if(strArriveDate==""||strArriveDate==null)
			{
				this.Popup("到货时间不能为空，请重新选择时间！");
				return;
			}
			Entity.RdRecord rdrm=new RdRecord();
			rdrm.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
			rdrm.cndARVDate=DateTime.Parse(strArriveDate);
			rdrm.cnvcShipAddress=this.txtShipAddress.Text.Trim();
			rdrm.cnvcARVAddress=this.txtArvAddress.Text.Trim();
			rdrm.cnvcComments=this.txtComments.Text.Trim();
			rdrm.cnvcModer=oper.strOperName;

			if(rdrm.cnnRdID.ToString()==""||rdrm.cnnRdID==0)
			{
				this.Popup("采购退货单主表标识不正确！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "修改采购退货主单";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdateRdRecordCom("RD002",ol,rdrm);
			if(ret > 0 )
			{
				this.Popup("修改采购退货单内容成功！");
			}
			else
			{
				this.Popup("修改采购退货单内容失败！");
			}
		}

		private void ddlWhouse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtwh=Helper.Query("select * from tbWarehouse where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'");
			if(dtwh!=null&&dtwh.Rows.Count>0)
				this.txtWHPerson.Text=dtwh.Rows[0]["cnvcWhPerson"].ToString();
			else
				this.txtWHPerson.Text="";
		}

	}
}
