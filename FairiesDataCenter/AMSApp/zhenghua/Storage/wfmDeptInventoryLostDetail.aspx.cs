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
	/// wfmDeptInventoryLostDetail 的摘要说明。
	/// </summary>
	public class wfmDeptInventoryLostDetail : wfmBase
	{
		protected System.Web.UI.WebControls.Button btMod;
		protected System.Web.UI.WebControls.Button btAdd;
		protected System.Web.UI.WebControls.TextBox txtComments;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.DropDownList ddlDeptID;
		protected System.Web.UI.WebControls.Label lbltitle;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox txtSerialNo;
		protected System.Web.UI.WebControls.TextBox txtInvCode;
		protected System.Web.UI.WebControls.TextBox txtInvName;
		protected System.Web.UI.WebControls.TextBox txtLostCount;
		protected System.Web.UI.WebControls.TextBox txtAddCount;
		protected System.Web.UI.WebControls.TextBox txtReduceCount;
		protected System.Web.UI.WebControls.TextBox txtQueryInvName;
		protected System.Web.UI.WebControls.TextBox txtQueryInvCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtUnit;
		protected System.Web.UI.WebControls.TextBox txtUnitCode;
		protected System.Web.UI.WebControls.Button btConfirm;
		protected System.Web.UI.WebControls.TextBox txtMdate;
		protected System.Web.UI.WebControls.TextBox txtExpDate;
		protected System.Web.UI.WebControls.Button btReturn;

		protected string strLostDate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			string strSeNo=Request.QueryString["seno"];
			if(!IsPostBack)
			{
				this.txtSerialNo.Visible=false;
				this.txtUnitCode.Visible=false;
				if(strSeNo==""||strSeNo==null)
				{
					this.btAdd.Enabled=true;
					this.btMod.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlDeptID,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
					strLostDate=DateTime.Now.ToString("yyyy-MM-dd");
					this.ddlDeptID.Enabled=false;
					this.txtAddCount.Enabled=false;
					this.txtReduceCount.Enabled=false;
					this.txtInvCode.Enabled=false;
					this.txtInvName.Enabled=false;
					this.txtUnit.Enabled=false;
					this.txtMdate.Enabled=false;
					this.txtExpDate.Enabled=false;
					this.btConfirm.Visible=false;
					if(this.oper.strNewDeptID=="CEN00")
					{
						this.btAdd.Enabled=false;
						this.btMod.Enabled=false;
						this.btConfirm.Enabled=false;
					}
				}
				else
				{
					this.btAdd.Enabled=false;
					this.btMod.Enabled=true;
					this.Button1.Enabled=false;
					this.FillDropDownList("NewDept",this.ddlDeptID,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
					string strSql = "select a.*,b.cnvcInvName,b.cnvcComunitCode,c.cnvcComUnitName from tbLostSerial a,tbInventory b,tbComputationUnit c where a.cnnLostSerialNo=" + strSeNo+" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcComunitCode=c.cnvcComunitCode";
					DataTable dtout = Helper.Query(strSql);
					this.txtSerialNo.Text=strSeNo;
					this.ddlWhouse.SelectedIndex=this.ddlWhouse.Items.IndexOf(this.ddlWhouse.Items.FindByValue(dtout.Rows[0]["cnvcWhCode"].ToString()));
					this.ddlDeptID.SelectedIndex=this.ddlDeptID.Items.IndexOf(this.ddlDeptID.Items.FindByValue(dtout.Rows[0]["cnvcDeptID"].ToString()));
					this.txtInvCode.Text=dtout.Rows[0]["cnvcInvCode"].ToString();
					this.txtInvName.Text=dtout.Rows[0]["cnvcInvName"].ToString();
					this.txtUnit.Text=dtout.Rows[0]["cnvcComUnitName"].ToString();
					this.txtUnitCode.Text=dtout.Rows[0]["cnvcComunitCode"].ToString();
					this.txtLostCount.Text=dtout.Rows[0]["cnnLostCount"].ToString();
					this.txtAddCount.Text=dtout.Rows[0]["cnnAddCount"].ToString();
					this.txtReduceCount.Text=dtout.Rows[0]["cnnReduceCount"].ToString();
					this.strLostDate=((DateTime)dtout.Rows[0]["cndLostDate"]).ToString("yyyy-MM-dd");
					this.txtMdate.Text=((DateTime)dtout.Rows[0]["cndMdate"]).ToString("yyyy-MM-dd");
					this.txtExpDate.Text=((DateTime)dtout.Rows[0]["cndExpDate"]).ToString("yyyy-MM-dd");
					this.txtComments.Text=dtout.Rows[0]["cnvcComments"].ToString();
					this.ddlWhouse.Enabled=false;
					this.ddlDeptID.Enabled=false;
					this.txtInvCode.Enabled=false;
					this.txtInvName.Enabled=false;
					this.txtUnit.Enabled=false;
					this.txtLostCount.Enabled=false;
					this.txtMdate.Enabled=false;
					this.txtExpDate.Enabled=false;
					if(dtout.Rows[0]["cnvcInvalidFlag"].ToString()=="0")
					{
						this.btMod.Enabled=true;
						this.btConfirm.Visible=true;
					}
					else
					{
						this.txtAddCount.Enabled=false;
						this.txtReduceCount.Enabled=false;
						this.txtComments.Enabled=false;
						this.Button1.Enabled=false;
						this.btMod.Enabled=false;
						this.btConfirm.Visible=false;
					}
					if(this.oper.strNewDeptID=="CEN00")
					{
						this.btAdd.Enabled=false;
						this.btMod.Enabled=false;
						this.btConfirm.Enabled=false;
					}
				}
			}
			else
			{
				strLostDate = Request.Form["txtLostDate"].ToString();
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
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			this.btMod.Click += new System.EventHandler(this.btMod_Click);
			this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
			this.btReturn.Click += new System.EventHandler(this.btReturn_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btAdd_Click(object sender, System.EventArgs e)
		{
			strLostDate = Request.Form["txtLostDate"].ToString();
			if(strLostDate==""||strLostDate==null)
			{
				this.Popup("损耗时间不能为空，请重新选择时间！");
				return;
			}
			if(this.txtLostCount.Text.Trim()=="")
			{
				this.Popup("损耗数量不能为空！");
				return;
			}
			if(this.txtMdate.Text.Trim()==""||this.txtExpDate.Text.Trim()=="")
			{
				this.Popup("生产日期和过期日期不能为空！");
				return;
			}
			Entity.LostSerial ls=new LostSerial();
			ls.cnvcInvCode=this.txtInvCode.Text.Trim();
			ls.cnnLostCount=Math.Round(decimal.Parse(this.txtLostCount.Text.Trim()),2);
			ls.cnnAddCount=0;
			ls.cnnReduceCount=0;
			ls.cndLostDate=DateTime.Parse(strLostDate);
			ls.cnvcDeptID=this.ddlDeptID.SelectedValue;
			ls.cnvcWhCode=this.ddlWhouse.SelectedValue;
			ls.cnvcOperID=this.oper.strLoginID;
			ls.cnvcLostType="1";
			ls.cnvcComments=this.txtComments.Text.Trim();
			ls.cnvcInvalidFlag="0";
			ls.cnvcComunitCode=this.txtUnitCode.Text.Trim();
			ls.cndMdate=DateTime.Parse(this.txtMdate.Text.Trim());
			ls.cndExpDate=DateTime.Parse(this.txtExpDate.Text.Trim());

			if(ls.cnvcDeptID==""||ls.cnvcWhCode=="")
			{
				this.Popup("部门和仓库不能为空！");
				return;
			}
			if(ls.cnvcInvCode=="")
			{
				this.Popup("报损存货不能为空！");
				return;
			}
			if(ls.cndExpDate.CompareTo(DateTime.Now)>0)
			{
				this.Popup("此存货尚未过期，不能报废！");
				return;
			}
			string strsql="select cast(cnnQuantity/(select cniChangRate from tbComputationUnit where cnvcComunitCode='"+ls.cnvcComunitCode+"') as numeric(10,4)) as cnnQuantity from tbCurrentStock";
			strsql+=" where cnvcWhCode='"+ls.cnvcWhCode+"' and cnvcInvCode='"+ls.cnvcInvCode+"' and convert(char(10),cndMdate,121)='"+ls.cndMdate.ToString("yyyy-MM-dd")+"' and convert(char(10),cndExpDate,121)='"+ls.cndExpDate.ToString("yyyy-MM-dd")+"'";
			string strQuantity=Helper.Query(strsql).Rows[0]["cnnQuantity"].ToString();
			if(ls.cnnLostCount>Math.Round(decimal.Parse(strQuantity),2))
			{
				this.Popup("损耗数量不能大于库存数量！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "添加过期损耗";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.AddLostSerial(ol,ls);
			if(ret > 0 )
			{
				this.Popup("添加过期损耗成功！");
			}
			else
			{
				this.Popup("添加过期损耗失败！");
			}
		}

		private void btMod_Click(object sender, System.EventArgs e)
		{
			strLostDate = Request.Form["txtLostDate"].ToString();
			if(strLostDate==""||strLostDate==null)
			{
				this.Popup("损耗时间不能为空，请重新选择时间！");
				return;
			}
			if(this.txtAddCount.Text.Trim()==""||this.txtReduceCount.Text.Trim()=="")
			{
				this.Popup("调增量和调减量不能为空！");
				return;
			}
			Entity.LostSerial ls=new LostSerial();
			ls.cnnLostSerialNo=int.Parse(this.txtSerialNo.Text.Trim());
			ls.cnnAddCount=Math.Round(decimal.Parse(this.txtAddCount.Text.Trim()),2);
			ls.cnnReduceCount=Math.Round(decimal.Parse(this.txtReduceCount.Text.Trim()),2);
			ls.cndLostDate=DateTime.Parse(strLostDate);
			ls.cnvcComments=this.txtComments.Text.Trim();
			ls.cnvcOperID=this.oper.strLoginID;
			if(ls.cnnLostSerialNo.ToString()=="")
			{
				this.Popup("损耗流水不正确！");
				return;
			}
			string strsql="select count(*) from tbLostSerial a,tbCurrentStock b,tbComputationUnit c where a.cnnLostSerialNo="+ls.cnnLostSerialNo.ToString()+" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcWhCode=b.cnvcWhCode";
			strsql+=" and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112)";
			strsql+=" and a.cnvcComunitCode=c.cnvcComunitCode and a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount>(b.cnnQuantity*c.cniChangRate)";
			DataTable dterrcount=Helper.Query(strsql);
			if(dterrcount.Rows.Count>0)
			{
				this.Popup("损耗数量不能大于库存数量！");
				return;
			}
			
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "修改过期损耗";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdateLostSerial(ol,ls);
			if(ret > 0 )
			{
				this.Popup("修改过期损耗成功！");
			}
			else
			{
				this.Popup("修改过期损耗失败！");
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strsql="select top 10 a.cnvcInvCode,a.cnvcInvName,a.cnvcGroupCode,a.cnvcSTComUnitCode,b.cnvcComUnitName,convert(char(10),c.cndMdate,120) as cndMdate,convert(char(10),c.cndExpDate,120) as cndExpDate";
			strsql+=",cast(c.cnnQuantity/b.cniChangRate as numeric(12,4)) as cnnQuantity,cast(c.cnnStopQuantity/b.cniChangRate as numeric(12,4)) as cnnStopQuantity,cast(c.cnnAvaQuantity/b.cniChangRate as numeric(12,4)) as cnnAvaQuantity";
			strsql+=" from tbInventory a,tbComputationUnit b,tbCurrentStock c where a.cnvcSTComUnitCode=b.cnvcComUnitCode and c.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode=c.cnvcInvCode";
			strsql+=" and c.cnnQuantity>0 and convert(char(8),c.cndExpDate,112)<=convert(char(8),getdate(),112)";
			if(this.txtQueryInvCode.Text.Trim()!="")
			{
				strsql+=" and a.cnvcInvCode='"+this.txtQueryInvCode.Text.Trim()+"'";
			}
			if(this.txtQueryInvName.Text.Trim()!="")
			{
				strsql+=" and a.cnvcInvName like '%"+this.txtQueryInvName.Text.Trim()+"%'";
			}
			DataTable dt = Helper.Query(strsql);
			this.TableConvert(dt,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommname");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			this.txtInvCode.Text=item.Cells[0].Text.Trim();
			this.txtInvName.Text=item.Cells[1].Text.Trim();
			this.txtUnit.Text=item.Cells[4].Text.Trim();
			this.txtUnitCode.Text=item.Cells[3].Text.Trim();
			this.txtMdate.Text=item.Cells[5].Text.Trim();
			this.txtExpDate.Text=item.Cells[6].Text.Trim();
			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.ddlWhouse.Enabled=false;
		}

		private void btConfirm_Click(object sender, System.EventArgs e)
		{
			if(this.txtSerialNo.Text.Trim()==""||this.txtInvCode.Text.Trim()=="")
			{
				this.Popup("损耗流水不正确！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "确认过期损耗";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			string strsql="select a.cnnLostSerialNo from tbLostSerial a,tbCurrentStock b,tbComputationUnit c where a.cnnLostSerialNo="+this.txtSerialNo.Text.Trim()+" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcWhCode=b.cnvcWhCode";
			strsql+=" and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112)";
			strsql+=" and a.cnvcComunitCode=c.cnvcComunitCode and a.cnnLostCount+a.cnnAddCount-a.cnnReduceCount>(b.cnnQuantity*c.cniChangRate)";
			DataTable dterrcount=Helper.Query(strsql);
			if(dterrcount.Rows.Count>0)
			{
				this.Popup("损耗数量大于当前库存量，可能是库存已经有变动，请检查修改损耗数量！");
				return;
			}

			StorageFacade sto = new StorageFacade();				
			int ret = sto.ConfirmLostSerial(ol,this.txtSerialNo.Text.Trim());
			if(ret > 0 )
			{
				this.Popup("确认过期损耗成功！");
				this.txtAddCount.Enabled=false;
				this.txtReduceCount.Enabled=false;
				this.txtComments.Enabled=false;
				this.Button1.Enabled=false;
				this.btMod.Enabled=false;
				this.btConfirm.Visible=false;
			}
			else
			{
				this.Popup("确认过期损耗失败！");
			}
		}

		private void btReturn_Click(object sender, System.EventArgs e)
		{
			this.Response.Redirect("wfmDeptInventoryLost.aspx");
		}
	}
}
