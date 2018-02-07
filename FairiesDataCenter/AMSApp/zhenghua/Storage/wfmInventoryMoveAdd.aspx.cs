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
	/// wfmInventoryMoveAdd 的摘要说明。
	/// </summary>
	public class wfmInventoryMoveAdd : wfmBase
	{
		protected System.Web.UI.WebControls.Button btreturn;
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.TextBox txtArvAddress;
		protected System.Web.UI.WebControls.TextBox txtShipAddress;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.TextBox txtMoveCode;
		protected System.Web.UI.WebControls.DropDownList ddlRdCode;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList ddlOutDeptID;
		protected System.Web.UI.WebControls.DropDownList ddlOutWhouse;
		protected System.Web.UI.WebControls.DropDownList ddlInDeptID;
		protected System.Web.UI.WebControls.DropDownList ddlInWhouse;
		protected System.Web.UI.WebControls.TextBox txtInWHPerson;
		protected System.Web.UI.WebControls.TextBox txtOutWHPerson;
		protected System.Web.UI.HtmlControls.HtmlTableRow troutwh;
		protected System.Web.UI.HtmlControls.HtmlTableRow troutdate;
		protected System.Web.UI.HtmlControls.HtmlTableRow trinwh;
		protected System.Web.UI.HtmlControls.HtmlTableRow trindate;
	
		protected string strOutArriveDate;
		protected string strInArriveDate;

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
					this.FillDropDownList("tbNameCodeToStorage",this.ddlRdCode,"vcCommSign='RdType'");
					this.ddlRdCode.SelectedIndex=this.ddlRdCode.Items.IndexOf(this.ddlRdCode.Items.FindByValue("RD005"));
					this.ddlRdCode.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlOutDeptID,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlOutWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("NewDept",this.ddlInDeptID);
					this.FillDropDownList("Warehouse",this.ddlInWhouse,"cnvcDepCode='"+this.ddlInDeptID.SelectedValue+"'");
					DataTable dtoutwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlOutWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
					if(dtoutwh!=null&&dtoutwh.Rows.Count>0)
						this.txtOutWHPerson.Text=dtoutwh.Rows[0]["vcOperName"].ToString();
					else
						this.txtOutWHPerson.Text="";
					DataTable dtinwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlInWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
					if(dtinwh!=null&&dtinwh.Rows.Count>0)
						this.txtInWHPerson.Text=dtinwh.Rows[0]["vcOperName"].ToString();
					else
						this.txtInWHPerson.Text="";
					strOutArriveDate=DateTime.Now.ToString("yyyy-MM-dd");
					strInArriveDate=DateTime.Now.ToString("yyyy-MM-dd");
					this.txtMoveCode.Enabled=false;
					string strCycle=DateTime.Today.Year.ToString();
					if(DateTime.Today.Month<10)
						strCycle+="0"+DateTime.Today.Month.ToString();
					else
						strCycle+=DateTime.Today.Month.ToString();
					this.txtMoveCode.Text="MOV"+strCycle+"----";
					if(this.oper.strNewDeptID=="CEN00")
					{
						this.btAdd.Enabled=false;
						this.btMod.Enabled=false;
					}
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.FillDropDownList("tbNameCodeToStorage",this.ddlRdCode,"vcCommSign='RdType'");
					this.FillDropDownList("NewDept",this.ddlOutDeptID);
					this.FillDropDownList("Warehouse",this.ddlOutWhouse);
					this.FillDropDownList("NewDept",this.ddlInDeptID);
					this.FillDropDownList("Warehouse",this.ddlInWhouse);
					string strSql = "select * from tbRdRecord where cnnRdID='" + strRdId + "'";
					DataTable dtout = Helper.Query(strSql);
					this.txtRdID.Text=strRdId;
					this.txtMoveCode.Text=dtout.Rows[0]["cnvcCode"].ToString();
					if(dtout.Rows[0]["cnvcRdCode"].ToString()=="RD005")
					{
						this.trinwh.Visible=false;
						this.trindate.Visible=false;
						this.ddlOutWhouse.SelectedIndex=this.ddlOutWhouse.Items.IndexOf(this.ddlOutWhouse.Items.FindByValue(dtout.Rows[0]["cnvcWhCode"].ToString()));
						this.ddlOutDeptID.SelectedIndex=this.ddlOutDeptID.Items.IndexOf(this.ddlOutDeptID.Items.FindByValue(dtout.Rows[0]["cnvcDepID"].ToString()));
						strOutArriveDate=((DateTime)dtout.Rows[0]["cndARVDate"]).ToShortDateString();
						this.txtOutWHPerson.Text=dtout.Rows[0]["cnvcWhpersonName"].ToString();
						this.ddlOutWhouse.Enabled=false;
						this.ddlOutDeptID.Enabled=false;
						this.txtOutWHPerson.Enabled=false;
					}
					if(dtout.Rows[0]["cnvcRdCode"].ToString()=="RD006")
					{
						this.troutwh.Visible=false;
						this.troutdate.Visible=false;
						this.ddlInDeptID.SelectedIndex=this.ddlInDeptID.Items.IndexOf(this.ddlInDeptID.Items.FindByValue(dtout.Rows[0]["cnvcDepID"].ToString()));
						this.ddlInWhouse.SelectedIndex=this.ddlInWhouse.Items.IndexOf(this.ddlInWhouse.Items.FindByValue(dtout.Rows[0]["cnvcWhCode"].ToString()));
						strInArriveDate=((DateTime)dtout.Rows[0]["cndARVDate"]).ToShortDateString();
						this.txtInWHPerson.Text=dtout.Rows[0]["cnvcWhpersonName"].ToString();
						this.ddlInWhouse.Enabled=false;
						this.ddlInDeptID.Enabled=false;
						this.txtInWHPerson.Enabled=false;
					}
					this.ddlRdCode.SelectedIndex=this.ddlRdCode.Items.IndexOf(this.ddlRdCode.Items.FindByValue(dtout.Rows[0]["cnvcRdCode"].ToString()));
					this.txtArvAddress.Text=dtout.Rows[0]["cnvcARVAddress"].ToString();
					this.txtShipAddress.Text=dtout.Rows[0]["cnvcShipAddress"].ToString();
					this.txtComments.Text=dtout.Rows[0]["cnvcComments"].ToString();
					this.txtMoveCode.Enabled=false;
					this.ddlRdCode.Enabled=false;
					if(dtout.Rows[0]["cnvcState"].ToString()!="0")
					{
						this.txtOutWHPerson.Enabled=false;
						this.txtInWHPerson.Enabled=false;
						this.btMod.Enabled=false;
					}
					if(this.oper.strNewDeptID=="CEN00")
					{
						this.btAdd.Enabled=false;
						this.btMod.Enabled=false;
					}
				}
			}
			else
			{
				if(Request.Form["txtOutArvDate"]!=null)
					strOutArriveDate = Request.Form["txtOutArvDate"].ToString();
				if(Request.Form["txtInArvDate"]!=null)
				strInArriveDate = Request.Form["txtInArvDate"].ToString();
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
			this.ddlOutWhouse.SelectedIndexChanged += new System.EventHandler(this.ddlOutWhouse_SelectedIndexChanged);
			this.ddlInDeptID.SelectedIndexChanged += new System.EventHandler(this.ddlInDeptID_SelectedIndexChanged);
			this.ddlInWhouse.SelectedIndexChanged += new System.EventHandler(this.ddlInWhouse_SelectedIndexChanged);
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			strOutArriveDate = Request.Form["txtOutArvDate"].ToString();
			if(strOutArriveDate==""||strOutArriveDate==null)
			{
				this.Popup("出库时间不能为空，请重新选择时间！");
				return;
			}
			strInArriveDate = Request.Form["txtInArvDate"].ToString();
			if(strInArriveDate==""||strInArriveDate==null)
			{
				this.Popup("入库时间不能为空，请重新选择时间！");
				return;
			}
			Entity.RdRecord rdout=new RdRecord();
			rdout.cnvcCode=this.txtMoveCode.Text.Trim();
			rdout.cnvcRdCode="RD005";
			rdout.cnvcRdFlag="1";
			rdout.cnvcWhCode=this.ddlOutWhouse.SelectedValue;
			rdout.cnvcDepID=this.ddlOutDeptID.SelectedValue;
			rdout.cnvcWhpersonName=this.txtOutWHPerson.Text.Trim();
			rdout.cnvcOperName=this.oper.strOperName;
			rdout.cnvcComments=this.txtComments.Text.Trim();
			rdout.cnvcMaker=this.oper.strOperName;
			rdout.cnvcShipAddress=this.txtShipAddress.Text.Trim();
			rdout.cndARVDate=DateTime.Parse(strOutArriveDate);
			rdout.cnvcARVAddress=this.txtArvAddress.Text.Trim();
			rdout.cnvcState="0";

			Entity.RdRecord rdin=new RdRecord();
			rdin.cnvcCode=this.txtMoveCode.Text.Trim();
			rdin.cnvcRdCode="RD006";
			rdin.cnvcRdFlag="0";
			rdin.cnvcWhCode=this.ddlInWhouse.SelectedValue;
			rdin.cnvcDepID=this.ddlInDeptID.SelectedValue;
			rdin.cnvcWhpersonName=this.txtInWHPerson.Text.Trim();
			rdin.cnvcOperName=this.oper.strOperName;
			rdin.cnvcComments=this.txtComments.Text.Trim();
			rdin.cnvcMaker=this.oper.strOperName;
			rdin.cnvcShipAddress=this.txtShipAddress.Text.Trim();
			rdin.cndARVDate=DateTime.Parse(strInArriveDate);
			rdin.cnvcARVAddress=this.txtArvAddress.Text.Trim();
			rdin.cnvcState="0";

			if(rdout.cnvcCode==""||rdout.cnvcCode.Length!=13||rdin.cnvcCode==""||rdin.cnvcCode.Length!=13)
			{
				this.Popup("调拨单号不正确！");
				return;
			}
			if(rdout.cnvcDepID==""||rdout.cnvcWhCode=="")
			{
				this.Popup("出库部门或仓库不能为空！");
				return;
			}
			if(rdin.cnvcDepID==""||rdin.cnvcWhCode=="")
			{
				this.Popup("出库部门或仓库不能为空！");
				return;
			}
			if(rdout.cnvcWhCode==rdin.cnvcWhCode)
			{
				this.Popup("同一仓库不能调拨！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "添加调拨主单";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.AddRdRecordMove(ol,rdout,rdin);
			if(ret > 0 )
			{
				this.Popup("添加调拨单内容成功！");
			}
			else
			{
				this.Popup("添加调拨单内容失败！");
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			if(this.ddlRdCode.SelectedValue=="RD005")
			{
				strOutArriveDate = Request.Form["txtOutArvDate"].ToString();
				if(strOutArriveDate==""||strOutArriveDate==null)
				{
					this.Popup("出库时间不能为空，请重新选择时间！");
					return;
				}
				Entity.RdRecord rd=new RdRecord();
				rd.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
				rd.cndARVDate=DateTime.Parse(strOutArriveDate);
				rd.cnvcShipAddress=this.txtShipAddress.Text.Trim();
				rd.cnvcARVAddress=this.txtArvAddress.Text.Trim();
				rd.cnvcComments=this.txtComments.Text.Trim();
				rd.cnvcModer=oper.strOperName;

				if(rd.cnnRdID.ToString()==""||rd.cnnRdID==0)
				{
					this.Popup("调拨出库单主表标识不正确！");
					return;
				}

				Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "修改调拨出库单主单";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;

				StorageFacade sto = new StorageFacade();				
				int ret = sto.UpdateRdRecordCom("RD005",ol,rd);
				if(ret > 0 )
				{
					this.Popup("修改调拨出库单内容成功！");
				}
				else
				{
					this.Popup("修改调拨出库单内容失败！");
				}
			}
			if(this.ddlRdCode.SelectedValue=="RD006")
			{
				strInArriveDate = Request.Form["txtInArvDate"].ToString();
				if(strInArriveDate==""||strInArriveDate==null)
				{
					this.Popup("入库时间不能为空，请重新选择时间！");
					return;
				}
				Entity.RdRecord rd=new RdRecord();
				rd.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
				rd.cndARVDate=DateTime.Parse(strInArriveDate);
				rd.cnvcShipAddress=this.txtShipAddress.Text.Trim();
				rd.cnvcARVAddress=this.txtArvAddress.Text.Trim();
				rd.cnvcComments=this.txtComments.Text.Trim();
				rd.cnvcModer=oper.strOperName;

				if(rd.cnnRdID.ToString()==""||rd.cnnRdID==0)
				{
					this.Popup("调拨入库单主表标识不正确！");
					return;
				}

				Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
				ol.cnvcOperType = "修改调拨入库单主单";
				ol.cnvcOperID = this.oper.strLoginID;
				ol.cnvcDeptID = this.oper.strDeptID;

				StorageFacade sto = new StorageFacade();				
				int ret = sto.UpdateRdRecordCom("RD005",ol,rd);
				if(ret > 0 )
				{
					this.Popup("修改调拨入库单内容成功！");
				}
				else
				{
					this.Popup("修改调拨入库单内容失败！");
				}
			}
		}

		private void ddlOutWhouse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlOutWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
			if(dtwh!=null&&dtwh.Rows.Count>0)
				this.txtOutWHPerson.Text=dtwh.Rows[0]["vcOperName"].ToString();
			else
				this.txtOutWHPerson.Text="";
		}

		private void ddlInWhouse_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlInWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
			if(dtwh!=null&&dtwh.Rows.Count>0)
				this.txtInWHPerson.Text=dtwh.Rows[0]["vcOperName"].ToString();
			else
				this.txtInWHPerson.Text="";
		}

		private void ddlInDeptID_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.FillDropDownList("Warehouse",this.ddlInWhouse,"cnvcDepCode='"+this.ddlInDeptID.SelectedValue+"'");
			DataTable dtwh=Helper.Query("select b.vcOperName from tbWarehouse a,tbLogin b where a.cnvcWhCode='"+this.ddlInWhouse.SelectedValue+"' and a.cnvcWhPerson=b.vcLoginID");
			if(dtwh!=null&&dtwh.Rows.Count>0)
				this.txtInWHPerson.Text=dtwh.Rows[0]["vcOperName"].ToString();
			else
				this.txtInWHPerson.Text="";
		}

	}
}
