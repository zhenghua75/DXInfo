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
	/// wfmInventoryMoveDetail 的摘要说明。
	/// </summary>
	public class wfmInventoryMoveDetail : wfmBase
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Button btnreturn;
		protected System.Web.UI.WebControls.Button btEnterComf;
		protected System.Web.UI.WebControls.Button btnQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox txtRdID;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtCode;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlProvider;
		protected System.Web.UI.WebControls.TextBox txtQueryInvCode;
		protected System.Web.UI.WebControls.TextBox txtMoveCount;
		protected System.Web.UI.WebControls.TextBox txtQueryInvName;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.DropDownList ddlRdCode;
		protected System.Web.UI.WebControls.TextBox txtgopage;
		protected System.Web.UI.WebControls.Label Label2;
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
					this.FillDropDownList("tbNameCodeToStorage",this.ddlRdCode,"vcCommSign='RdType'");
					this.txtCode.Enabled=false;
					this.txtRdID.Enabled=false;
					this.txtRdID.Visible=false;
					this.ddlWhouse.Enabled=false;
					this.ddlDept.Enabled=false;
					this.btEnterComf.Enabled=false;
					this.ddlRdCode.Enabled=false;
					DataTable dtrd=Helper.Query("select cnvcState,cnvcRdCode from tbRdRecord where cnnRdID="+strrdid);
					this.ddlRdCode.SelectedIndex=this.ddlRdCode.Items.IndexOf(this.ddlRdCode.Items.FindByValue(dtrd.Rows[0]["cnvcRdCode"].ToString()));
					if(dtrd.Rows[0]["cnvcState"].ToString()!="0")
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[9].Visible=false;
						this.Datagrid2.Columns[10].Visible=false;
					}
					if(dtrd.Rows[0]["cnvcRdCode"].ToString()=="RD006")
					{
						this.btEnterComf.Text="确定入库";
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[10].Visible=false;
					}
				}
				else
				{
					gopage=int.Parse(this.txtgopage.Text.Trim())-1;
					this.txtgopage.Text=gopage.ToString();
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
			this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
			this.btEnterComf.Click += new System.EventHandler(this.btEnterComf_Click);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Datagrid2.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_DeleteCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DBBind()
		{
			string strRdid=this.txtRdID.Text.Trim();
			string strsql="";
			if(this.ddlRdCode.SelectedValue=="RD005")
			{
				strsql+="select a.cnnRdID,a.cnvcInvCode,b.cnvcInvName,a.cnnPrice,a.cnvcGroupCode,a.cnvcComunitCode,0 as cnnOutCount,0 as cnnLostCount,sum(a.cnnQuantity) as cnnQuantity,sum(a.cnnCost) as cnnCost";
				strsql+=" from tbRdRecordDetail a,tbInventory b where a.cnnRdID="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode group by a.cnnRdID,a.cnvcInvCode,b.cnvcInvName,a.cnnPrice,a.cnvcGroupCode,a.cnvcComunitCode";
			}
			else
			{
				strsql+="select t.cnnRdID,t.cnvcInvCode,t.cnvcInvName,t.cnnPrice,t.cnvcGroupCode,t.cnvcComunitCode,sum(t.cnnOutCount) as cnnOutCount,sum(t.cnnLostCount) as cnnLostCount,sum(t.cnnQuantity) as cnnQuantity,sum(t.cnnCost) as cnnCost from (";
				strsql+="select a.cnnRdID,a.cnvcInvCode,b.cnvcInvName,a.cnnPrice,a.cnvcGroupCode,a.cnvcComunitCode,c.cnnQuantity as cnnOutCount,0 as cnnLostCount,a.cnnQuantity,a.cnnCost";
				strsql+=" from tbRdRecordDetail a,tbInventory b,tbRdRecordDetail c where c.cnnRdID<>"+strRdid+" and a.cnvcPoID=c.cnvcPoID and a.cnvcInvCode=b.cnvcInvCode and a.cnvcInvCode=c.cnvcInvCode and a.cnnRdID="+strRdid;
				strsql+=" and a.cnvcInvCode+convert(char(8),a.cndMdate,112)+convert(char(8),a.cndExpDate,112) not in (select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112) from tbLostSerial";
				strsql+=" where cnnProduceSerialNo=(select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+strRdid+") and cnvcRdCode='RD005')) ";
				strsql+=" and a.cndMdate=c.cndMdate and a.cndExpDate=c.cndExpDate union all select a.cnnRdID,a.cnvcInvCode,b.cnvcInvName,a.cnnPrice,a.cnvcGroupCode,a.cnvcComunitCode,d.cnnQuantity as cnnOutCount,c.cnnLostCount,a.cnnQuantity,a.cnnCost";
				strsql+=" from tbRdRecordDetail a,tbInventory b,(select cnvcInvCode,cnnLostCount,cndMdate,cndExpDate from tbLostSerial where cnnProduceSerialNo=(select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+strRdid+")";
				strsql+=" and cnvcRdCode='RD005')) c, tbRdRecordDetail d where d.cnnRdID<>"+strRdid+" and a.cnvcPoID=d.cnvcPoID and a.cnvcInvCode=b.cnvcInvCode and a.cnvcInvCode=c.cnvcInvCode and a.cnvcInvCode=d.cnvcInvCode and a.cnnRdID="+strRdid;
				strsql+=" and a.cndMdate=c.cndMdate and a.cndExpDate=c.cndExpDate and a.cndMdate=d.cndMdate and a.cndExpDate=d.cndExpDate) t group by t.cnnRdID,t.cnvcInvCode,t.cnvcInvName,t.cnnPrice,t.cnvcGroupCode,t.cnvcComunitCode";
			}
			DataTable dtdetail=Helper.Query(strsql);
			this.TableConvert(dtdetail,"cnvcGroupCode","ComputationGroup","vcCommCode","vcCommName");
			this.TableConvert(dtdetail,"cnvcComunitCode","ComputationUnit","vcCommCode","vcCommName");
			Session["movdetailtomod"]=dtdetail;
			this.Datagrid2.EditItemIndex=-1;

			string stroutrdState=Helper.Query("select cnvcState from tbRdRecord where cnvcRdCode='RD005' and cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+strRdid+")").Rows[0]["cnvcState"].ToString();
			string strinrdState=Helper.Query("select cnvcState from tbRdRecord where cnvcRdCode='RD006' and cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+strRdid+")").Rows[0]["cnvcState"].ToString();
			if(this.ddlRdCode.SelectedValue=="RD006")
			{
				this.Datagrid2.Columns[5].Visible=true;
				this.Datagrid2.Columns[6].Visible=true;
				this.Datagrid2.Columns[7].HeaderText="入库数量";
				if(stroutrdState=="0")
				{
					this.Button1.Enabled=false;
					this.btEnterComf.Enabled=false;
					this.Datagrid2.Columns[9].Visible=false;
					this.Datagrid2.Columns[10].Visible=false;
				}
				else
				{
					if(strinrdState=="0")
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=true;
						this.Datagrid2.Columns[9].Visible=true;
						this.Datagrid2.Columns[10].Visible=false;
					}
					else
					{
						this.Button1.Enabled=false;
						this.btEnterComf.Enabled=false;
						this.Datagrid2.Columns[9].Visible=false;
						this.Datagrid2.Columns[10].Visible=false;
					}
				}
			}
			else
			{
				this.Datagrid2.Columns[5].Visible=false;
				this.Datagrid2.Columns[6].Visible=false;
				this.Datagrid2.Columns[7].HeaderText="出库数量";
				if(stroutrdState=="0")
				{
					this.Button1.Enabled=true;
					this.btEnterComf.Enabled=true;
					this.Datagrid2.Columns[9].Visible=false;
					this.Datagrid2.Columns[10].Visible=true;
				}
				else
				{
					this.Button1.Enabled=false;
					this.btEnterComf.Enabled=false;
					this.Datagrid2.Columns[9].Visible=false;
					this.Datagrid2.Columns[10].Visible=false;
				}
			}
			this.Datagrid2.DataSource = dtdetail;
			this.Datagrid2.DataBind();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void btEnterComf_Click(object sender, System.EventArgs e)
		{
			string strWhcode=this.ddlWhouse.SelectedValue;
			string strRdid=this.txtRdID.Text.Trim();
			string strRdCode=this.ddlRdCode.SelectedValue;
			if(strRdCode=="")
			{
				this.SetErrorMsgPageBydir("调拨单据信息不全，请重试！");
				return;
			}
			if(strWhcode=="")
			{
				this.Popup("仓库不能为空！");
				return;
			}
			DataTable dtm=(DataTable)Session["movdetailtomod"];
			if(dtm==null||dtm.Rows.Count<=0)
			{
				this.Popup("该调拨单无任何货品记录，请先录入要调拨的货品！");
				return;
			}

			string strcomments="";
			if(strRdCode=="RD005")
			{
				strcomments = "调拨单出库";
				string strsql="select top 1 a.cnvcInvCode from tbCurrentStock a,tbRdRecordDetail b,tbComputationUnit c,tbRdRecord d where b.cnnRdID="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode";
				strsql+=" and b.cnvcComunitCode=c.cnvcComunitCode and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112)";
				strsql+=" and a.cnvcWhCode=d.cnvcWhCode and b.cnnRdID=d.cnnRdID and a.cnvcStopFlag='0' and b.cnnQuantity>a.cnnAvaQuantity*c.cniChangRate";
				DataTable dterrinv=Helper.Query(strsql);
				if(dterrinv.Rows.Count>0)
				{
					this.Popup("存货编码："+dterrinv.Rows[0][0].ToString()+" 的当前可用库存已经不足于调拨，请检查！");
					return;
				}
			}
			else
			{
				strcomments = "调拨单入库";
			}
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
            ol.cnvcOperType = strcomments;
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.RdRecordMoveDetailExecMoving(ol,strWhcode,strRdid,strRdCode);
			if(ret > 0 )
			{
				this.Popup(strcomments+"成功！");
				this.DBBind();
			}
			else
			{
				this.Popup(strcomments+"失败！");
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			string strsql="select a.cnvcInvCode,a.cnvcInvName,a.cniInvCCost,a.cnvcGroupCode,a.cnvcSTComUnitCode,a.cnvcSTComUnitCode as cnvcComUnitName,";
			strsql+="sum(cast(c.cnnAvaQuantity/d.cniChangRate as numeric(12,4))) as cnnQuantity from tbInventory a,tbCurrentStock c,tbComputationUnit d where c.cnvcStopFlag='0' and a.cnvcSTComUnitCode=d.cnvcComUnitCode";
			strsql+=" and c.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode=c.cnvcInvCode and a.cnvcInvCode not in(select cnvcInvCode from tbRdRecordDetail where cnnRdID='"+this.txtRdID.Text.Trim()+"')";
			if(this.txtQueryInvCode.Text.Trim()!="")
			{
				strsql+=" and a.cnvcInvCode='"+this.txtQueryInvCode.Text.Trim()+"'";
			}
			if(this.txtQueryInvName.Text.Trim()!="")
			{
				strsql+=" and a.cnvcInvName like '%"+this.txtQueryInvName.Text.Trim()+"%'";
			}
			strsql+=" group by a.cnvcInvCode,a.cnvcInvName,a.cniInvCCost,a.cnvcGroupCode,a.cnvcSTComUnitCode,a.cnvcSTComUnitCode order by a.cnvcInvCode,a.cnvcInvName";
			DataTable dt = Helper.Query(strsql);
			this.TableConvert(dt,"cnvcComUnitName","ComputationUnit","vcCommCode","vcCommName");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
            this.ClientScript.RegisterStartupScript(this.GetType(), "show", "<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.txtMoveCount.Text.Trim()==""||!this.JudgeIsNum(this.txtMoveCount.Text.Trim()))
			{
				this.Popup("调拨数量不能为空且必须是数字！");
				return;
			}
			DataGridItem item = this.DataGrid1.SelectedItem;
			decimal dmovecount=Math.Round(decimal.Parse(this.txtMoveCount.Text.Trim()),2);
			decimal dmovecountCom=Math.Round(decimal.Parse(this.txtMoveCount.Text.Trim()),2);
			DataTable dtrate=Helper.Query("select cniChangRate from tbComputationUnit where cnvcComunitCode='"+item.Cells[4].Text.Trim()+"'");
			if(dtrate.Rows.Count==0)
			{
				this.Popup("单位换算率不存在，请检查单位组配置！");
				return;
			}
			else
			{
				dmovecountCom=Math.Round(dmovecountCom*decimal.Parse(dtrate.Rows[0]["cniChangRate"].ToString()),2);
			}
			DataTable dtcount=Helper.Query("select sum(cnnAvaQuantity) as cnnQuantity from tbCurrentStock where cnvcStopFlag='0' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcInvCode='"+item.Cells[0].Text.Trim()+"'");
			if(dtcount==null||dtcount.Rows.Count<=0)
			{
				this.Popup("仓库内无此存货！");
				return;
			}
			else if(dmovecountCom>Math.Round(decimal.Parse(dtcount.Rows[0]["cnnQuantity"].ToString()),2))
			{
				this.Popup("仓库内存货数量不足"+dmovecount.ToString()+"！");
				return;
			}

			ArrayList alrdout=new ArrayList();
			ArrayList alrdin=new ArrayList();
			decimal dstepcount=0;
			decimal dalreadycount=0;
			string strRdid=Helper.Query("select cnnRdID from tbRdRecord where cnvcCode='"+this.txtCode.Text.Trim()+"' and cnvcRdCode='RD006'").Rows[0]["cnnRdID"].ToString();
			DataTable dtinv=Helper.Query("select cnvcWhCode,cnvcInvCode,cndMdate,cndExpDate,cnnAvaQuantity from tbCurrentStock where cnvcStopFlag='0' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcInvCode='"+item.Cells[0].Text.Trim()+"' and cnnAvaQuantity>0 order by cndExpDate");
			for(int i=0;i<dtinv.Rows.Count;i++)
			{
				dstepcount+=Math.Round(decimal.Parse(dtinv.Rows[i]["cnnAvaQuantity"].ToString()),2);
				if(dstepcount>dmovecountCom)
				{
					RdRecordDetail rdout=new RdRecordDetail();
					rdout.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
					rdout.cnvcPOID=this.txtCode.Text.Trim();
					rdout.cnvcInvCode=item.Cells[0].Text.Trim();
					rdout.cnnQuantity=Math.Round((dmovecountCom-dalreadycount)/decimal.Parse(dtrate.Rows[0]["cniChangRate"].ToString()),2);
					rdout.cnnPrice=Math.Round(decimal.Parse(item.Cells[2].Text.Trim()),2);
					rdout.cnnCost=rdout.cnnQuantity*rdout.cnnPrice;
					rdout.cnvcGroupCode=item.Cells[3].Text.Trim();
					rdout.cnvcComunitCode=item.Cells[4].Text.Trim();
					rdout.cnvcFlag="0";
					rdout.cndMdate=(DateTime)dtinv.Rows[i]["cndMdate"];
					rdout.cndExpDate=(DateTime)dtinv.Rows[i]["cndExpDate"];
					alrdout.Add(rdout);

					RdRecordDetail rdin=new RdRecordDetail();
					rdin.cnnRdID=int.Parse(strRdid);
					rdin.cnvcPOID=this.txtCode.Text.Trim();
					rdin.cnvcInvCode=item.Cells[0].Text.Trim();
					rdin.cnnQuantity=Math.Round((dmovecountCom-dalreadycount)/decimal.Parse(dtrate.Rows[0]["cniChangRate"].ToString()),2);
					rdin.cnnPrice=Math.Round(decimal.Parse(item.Cells[2].Text.Trim()),2);
					rdin.cnnCost=rdin.cnnQuantity*rdin.cnnPrice;
					rdin.cnvcGroupCode=item.Cells[3].Text.Trim();
					rdin.cnvcComunitCode=item.Cells[4].Text.Trim();
					rdin.cnvcFlag="0";
					rdin.cndMdate=rdout.cndMdate;
					rdin.cndExpDate=rdout.cndExpDate;
					alrdin.Add(rdin);

					dalreadycount+=dmovecountCom-dalreadycount;
					break;
				}
				else
				{
					RdRecordDetail rdout=new RdRecordDetail();
					rdout.cnnRdID=int.Parse(this.txtRdID.Text.Trim());
					rdout.cnvcPOID=this.txtCode.Text.Trim();
					rdout.cnvcInvCode=item.Cells[0].Text.Trim();
					rdout.cnnQuantity=Math.Round(decimal.Parse(dtinv.Rows[i]["cnnAvaQuantity"].ToString())/decimal.Parse(dtrate.Rows[0]["cniChangRate"].ToString()),2);
					rdout.cnnPrice=Math.Round(decimal.Parse(item.Cells[2].Text.Trim()),2);
					rdout.cnnCost=rdout.cnnQuantity*rdout.cnnPrice;
					rdout.cnvcGroupCode=item.Cells[3].Text.Trim();
					rdout.cnvcComunitCode=item.Cells[4].Text.Trim();
					rdout.cnvcFlag="0";
					rdout.cndMdate=(DateTime)dtinv.Rows[i]["cndMdate"];
					rdout.cndExpDate=(DateTime)dtinv.Rows[i]["cndExpDate"];
					alrdout.Add(rdout);

					RdRecordDetail rdin=new RdRecordDetail();
					rdin.cnnRdID=int.Parse(strRdid);
					rdin.cnvcPOID=this.txtCode.Text.Trim();
					rdin.cnvcInvCode=item.Cells[0].Text.Trim();
					rdin.cnnQuantity=Math.Round(decimal.Parse(dtinv.Rows[i]["cnnAvaQuantity"].ToString())/decimal.Parse(dtrate.Rows[0]["cniChangRate"].ToString()),2);
					rdin.cnnPrice=Math.Round(decimal.Parse(item.Cells[2].Text.Trim()),2);
					rdin.cnnCost=rdin.cnnQuantity*rdin.cnnPrice;
					rdin.cnvcGroupCode=item.Cells[3].Text.Trim();
					rdin.cnvcComunitCode=item.Cells[4].Text.Trim();
					rdin.cnvcFlag="0";
					rdin.cndMdate=rdout.cndMdate;
					rdin.cndExpDate=rdout.cndExpDate;
					alrdin.Add(rdin);

					dalreadycount+=Math.Round(decimal.Parse(dtinv.Rows[i]["cnnAvaQuantity"].ToString()),2);
				}
			}
				
			if(dalreadycount!=dmovecountCom)
			{
				this.Popup("系统错误，调拨数量不正确，请重试！");
				return;
			}
			OperLog operLog=new OperLog();
			operLog.cnvcOperType = "添加调拨存货";
			operLog.cnvcOperID = this.oper.strLoginID;
			operLog.cnvcDeptID = this.oper.strDeptID;
			StorageFacade sto=new StorageFacade();
			int ret=sto.AddRdRecordMoveDetail(operLog,alrdout,alrdin);
			if(ret > 0 )
			{
				this.Popup("添加调拨存货成功！");
			}
			else
			{
				this.Popup("添加调拨存货失败！");
			}

			this.DataGrid1.DataSource = null;
			this.DataGrid1.DataBind();
			this.DBBind();
		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			this.Datagrid2.DataSource=(DataTable)Session["movdetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["movdetailtomod"];
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string strRdCode=this.ddlRdCode.SelectedValue;
			if(strRdCode=="")
			{
				this.SetErrorMsgPageBydir("调拨单据信息不全，请重试！");
				return;
			}
			string stroutcount="0";
			string strLostcount="0";
			string strrealcount="0";
			decimal dlostcount=0;
			Entity.RdRecordDetail rrd=new RdRecordDetail();
			if(strRdCode=="RD006")
			{
				stroutcount=e.Item.Cells[5].Text.Trim();
				strLostcount=((TextBox)e.Item.Cells[6].Controls[0]).Text.Trim();
				strrealcount=((TextBox)e.Item.Cells[7].Controls[0]).Text.Trim();
				if(strLostcount==""||!this.JudgeIsNum(strLostcount))
				{
					this.Popup("损耗数量不能为空且必须是数字！");
					return;
				}
				if(strrealcount==""||!this.JudgeIsNum(strrealcount))
				{
					this.Popup("入库数量不能为空且必须是数字！");
					return;
				}
				string strRate=Helper.Query("select cniChangRate from tbComputationUnit where cnvcComunitCode=(select cnvcSTComunitCode from tbInventory where cnvcInvCode='"+e.Item.Cells[1].Text.Trim()+"')").Rows[0]["cniChangRate"].ToString();
				decimal doutcount=Math.Round(decimal.Parse(stroutcount)*decimal.Parse(strRate),2);
				dlostcount=Math.Round(decimal.Parse(strLostcount)*decimal.Parse(strRate),2);
				decimal drealcount=Math.Round(decimal.Parse(strrealcount)*decimal.Parse(strRate),2);
				if(doutcount!=drealcount+dlostcount)
				{
					this.Popup("损耗数量+入库数量不等于出库数量");
					return;
				}
				rrd.cnnRdID=int.Parse(e.Item.Cells[0].Text.Trim());
				rrd.cnvcInvCode=e.Item.Cells[1].Text.Trim();
				rrd.cnnQuantity=Math.Round(decimal.Parse(strrealcount),2);
				if(rrd.cnnRdID.ToString()==""||rrd.cnnRdID==0)
				{
					this.Popup("调拨单标识不正确！");
					return;
				}
			}

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "修改调拨单子表";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.UpdateRdRecordMoveDetail(strRdCode,ol,rrd,strLostcount);
			if(ret > 0 )
			{
				this.Popup("修改调拨单子表明细成功！");
				this.DBBind();
			}
			else
			{
				this.Popup("修改调拨单子表明细失败！");
			}
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;
			this.Datagrid2.DataSource=(DataTable)Session["movdetailtomod"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
		}

		private void Datagrid2_DeleteCommand(object source, DataGridCommandEventArgs e)
		{
			string strRdID=e.Item.Cells[0].Text.Trim();
			string strInvCode=e.Item.Cells[1].Text.Trim();

			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "删除调拨单子表";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			StorageFacade sto = new StorageFacade();				
			int ret = sto.DeleteRdRecordMoveDetail(ol,strRdID,strInvCode);
			if(ret > 0 )
			{
				this.Popup("删除调拨单子表明细成功！");
				this.DBBind();
			}
			else
			{
				this.Popup("删除调拨单子表明细失败！");
			}
		}
	}
}
