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
	/// wfmDeptStorageEnterDetail 的摘要说明。
	/// </summary>
	public class wfmDeptStorageEnterDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtQueryPoID;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Button btEnterComf;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtgopage;

		protected ucPageView UcPageView1;
		protected int gopage;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
            this.ClientScript.RegisterStartupScript(this.GetType(), "hide", "<script lanaguage=javascript>ShowHide('1','none');</script>");
			string strrdid=Request.QueryString["rdid"];
			string strcode=Request.QueryString["code"];
			string strdept=Request.QueryString["dept"];
			string strwhid=Request.QueryString["whid"];
			if(strrdid==""||strrdid==null||strcode==""||strcode==null||strdept==""||strdept==null||strwhid==""||strwhid==null)
			{
				this.SetErrorMsgPageBydir("关键参数传递错误，请重试！");
				return;
			}
			else
			{
				if (!IsPostBack )
				{
					gopage=-1;
					this.txtgopage.Text="-1";
					Session.Remove("QUERY");
					Session.Remove("page_view");
					this.txtCode.Text=strcode;
					this.txtRdID.Text=strrdid;
					this.FillDropDownList("NewDept",this.ddlDept,"vcCommCode='"+strdept+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"vcCommCode='"+strwhid+"'");
					this.txtCode.Enabled=false;
					this.txtRdID.Enabled=false;
					this.txtRdID.Visible=false;
					this.ddlDept.Enabled=false;
					this.ddlWhouse.Enabled=false;
					this.btEnterComf.Enabled=false;
					string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strrdid).Rows[0]["cnvcState"].ToString();
					if(strState!="0")
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[12].Visible=false;
					}
				}
				else
				{
					gopage=int.Parse(this.txtgopage.Text.Trim())-1;
					this.txtgopage.Text=gopage.ToString();
				}
				string strrdcount=Helper.Query("select count(*) as rdcount from tbRdRecordDetail where cnnRdID="+strrdid).Rows[0]["rdcount"].ToString();
				if(strrdcount!="0")
				{
					this.Button1.Enabled=false;
				}
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
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btEnterComf.Click += new System.EventHandler(this.btEnterComf_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			string strRdid=this.txtRdID.Text.Trim();
			string strsql="select a.cnnAutoID,a.cnnRdID,a.cnvcPOID,a.cnvcInvCode,b.cnvcInvName,a.cnvcGroupCode,a.cnvcComunitCode,convert(char(10),a.cndMdate,120) as cndMdate,";
			strsql+="convert(char(10),a.cndExpDate,120) as cndExpDate,c.cnnAssignCount as cnnPoCount,(d.cnnLostCount+d.cnnAddCount-d.cnnReduceCount) as cnnLostCount,a.cnnQuantity";
			strsql+=" from tbRdRecordDetail a,tbInventory b,tbAssignDetail c,tbLostSerial d where a.cnnRdID="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcPOID=cast(c.cnnAssignSerialNo as varchar(12))";
			strsql+=" and a.cnvcInvCode=c.cnvcInvCode and convert(char(10),a.cndMdate,120)=convert(char(10),c.cndMdate,120) and convert(char(10),a.cndExpDate,120)=convert(char(10),c.cndExpDate,120) and a.cnnRdID=d.cnnProduceSerialNo and a.cnvcInvCode=d.cnvcInvCode";
			strsql+=" and convert(char(10),a.cndMdate,120)=convert(char(10),d.cndMdate,120) and convert(char(10),a.cndExpDate,120)=convert(char(10),d.cndExpDate,120) union all select a.cnnAutoID,a.cnnRdID,a.cnvcPOID,a.cnvcInvCode,b.cnvcInvName,a.cnvcGroupCode,a.cnvcComunitCode,";
			strsql+="convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate,c.cnnAssignCount as cnnPoCount,0 as cnnLostCount,a.cnnQuantity";
			strsql+=" from tbRdRecordDetail a,tbInventory b,tbAssignDetail c where a.cnnRdID="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode and a.cnvcPOID=cast(c.cnnAssignSerialNo as varchar(12))";
			strsql+=" and a.cnvcInvCode=c.cnvcInvCode and convert(char(10),a.cndMdate,120)=convert(char(10),c.cndMdate,120) and convert(char(10),a.cndExpDate,120)=convert(char(10),c.cndExpDate,120) and a.cnvcInvCode+convert(char(10),a.cndMdate,120)+convert(char(10),a.cndExpDate,120)";
			strsql+=" not in(select cnvcInvCode+convert(char(10),cndMdate,120)+convert(char(10),cndExpDate,120) from tbLostSerial where cnnProduceSerialNo="+strRdid+")";
			DataTable dtdetail=Helper.Query(strsql);

			this.TableConvert(dtdetail,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcComunitCode","ComputationUnit","vcCommCode","vcCommName");
			Session["asdetailtomod"]=dtdetail;
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource = dtdetail;
			this.Datagrid2.DataBind();

			string strState=Helper.Query("select cnvcState from tbRdRecord where cnnRdID="+strRdid).Rows[0]["cnvcState"].ToString();
			if(strState=="0"&&dtdetail.Rows.Count>0)
			{
				this.btEnterComf.Enabled=true;
			}
			else if(strState!="0")
			{
				this.Button1.Enabled=false;
				this.btEnterComf.Enabled=false;
				this.Datagrid2.Columns[12].Visible=false;
			}
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void btEnterComf_Click(object sender, System.EventArgs e)
		{
			string strWhcode=this.ddlWhouse.SelectedValue;
			string strRdid=this.txtRdID.Text.Trim();

			if(strWhcode=="")
			{
				this.Popup("仓库不能为空！");
				return;
			}
			DataTable dtdetail=(DataTable)Session["asdetailtomod"];
			if(dtdetail==null||dtdetail.Rows.Count<=0)
			{
				this.Popup("该分货入库单无任何货品记录，请先关联生产分货单！");
				return;
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "分货入库单入库";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.DeptStorageEnterExecEntering(ol,strWhcode,dtdetail,strRdid,this.ddlDept.SelectedValue);
			if(ret > 0 )
			{
				this.Popup("分货入库单入库成功！");
				this.DBBind();
			}
			else
			{
				this.Popup("分货入库单入库失败！");
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strsql="";
			if(this.txtQueryPoID.Text.Trim()=="")
			{
				strsql="select cnnAssignSerialNo,cnvcShipDeptID,cnvcShipOperID,cnvcReceiveDeptID,cnvcReceiveOperID from tbAssignLog where cnvcReceiveDeptID='"+this.ddlDept.SelectedValue+"'";
				strsql+=" and cast(cnnAssignSerialNo as varchar(12)) not in(select distinct cnvcPoID from tbRdRecordDetail where cnnRdID in(select distinct cnnRdID from tbRdRecord where cnvcRdCode='RD007' and cnvcDepID='"+this.ddlDept.SelectedValue+"'))";
			}
			else
			{
				strsql="select cnnAssignSerialNo,cnvcShipDeptID,cnvcShipOperID,cnvcReceiveDeptID,cnvcReceiveOperID from tbAssignLog where cnvcReceiveDeptID='"+this.ddlDept.SelectedValue+"' and cnnAssignSerialNo="+this.txtQueryPoID.Text.Trim();
			}
			DataTable dt = Helper.QueryLongTrans(strsql);
			this.TableConvert(dt,"cnvcShipDeptID","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dt,"cnvcReceiveDeptID","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dt,"cnvcShipOperID","tbLogin","vcLoginID","vcOperName");
			this.TableConvert(dt,"cnvcReceiveOperID","tbLogin","vcLoginID","vcOperName");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			string strAssID=item.Cells[0].Text.Trim();
			OperLog operLog=new OperLog();
			operLog.cnvcOperType = "添加关联分货单";
			operLog.cnvcOperID = this.oper.strLoginID;
			operLog.cnvcDeptID = this.oper.strDeptID;
			StorageFacade sto=new StorageFacade();
			int ret=sto.AddDeptStorageEnterDetail(operLog,this.txtRdID.Text.Trim(),strAssID,this.ddlDept.SelectedValue);
			if(ret > 0 )
			{
				this.Popup("添加关联分货单成功！");
			}
			else
			{
				this.Popup("添加关联分货单失败！");
			}

			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DBBind();
		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			this.Datagrid2.DataSource=(DataTable)Session["asdetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["asdetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			if(((TextBox)e.Item.Cells[11].Controls[0]).Text.Trim()==""||!this.JudgeIsNum(((TextBox)e.Item.Cells[11].Controls[0]).Text.Trim()))
			{
				this.Popup("入库数量不能为空且必须是数字！");
				return;
			}
			if(((TextBox)e.Item.Cells[10].Controls[0]).Text.Trim()==""||!this.JudgeIsNum(((TextBox)e.Item.Cells[10].Controls[0]).Text.Trim()))
			{
				this.Popup("损耗数量不能为空且必须是数字！");
				return;
			}
			Entity.RdRecordDetail rrd=new RdRecordDetail();
			rrd.cnnAutoID=int.Parse(e.Item.Cells[0].Text.Trim());
			rrd.cnnQuantity=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[11].Controls[0]).Text.Trim()),2);
			decimal dlostcount=Math.Round(decimal.Parse(((TextBox)e.Item.Cells[10].Controls[0]).Text.Trim()),2);
			decimal doutcount=Math.Round(decimal.Parse(e.Item.Cells[9].Text.Trim()),2);
			
			if(rrd.cnnAutoID.ToString()==""||rrd.cnnAutoID==0)
			{
				this.Popup("分货入库单子表标识不正确！");
				return;
			}
			if(doutcount!=rrd.cnnQuantity+dlostcount)
			{
				this.Popup("损耗数量+入库数量不等于分货数量");
				return;
			}
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "修改分货入库子表";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdateDeptStorageEnterDetail(ol,rrd,dlostcount);
			if(ret > 0 )
			{
				this.Popup("修改分货入库单子表明细成功！");
				this.DBBind();
			}
			else
			{
				this.Popup("修改分货入库单子表明细失败！");
			}
		}

		protected void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["asdetailtomod"];
			this.Datagrid2.DataBind();
		}
	}
}
