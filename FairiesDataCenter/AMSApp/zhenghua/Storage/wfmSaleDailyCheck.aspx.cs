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
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.Business;

namespace AMSApp.zhenghua.Storage
{
	/// <summary>
	/// wfmSaleDailyCheck ��ժҪ˵����
	/// </summary>
	public class wfmSaleDailyCheck : wfmBase
	{
		protected System.Web.UI.WebControls.Button btnCheckOk;
		protected System.Web.UI.WebControls.Button btQuery;
		protected System.Web.UI.WebControls.DropDownList ddlWhouse;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList ddlDept;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.TextBox txtQueryInvCode;
		protected System.Web.UI.WebControls.TextBox txtQueryInvName;
		protected System.Web.UI.WebControls.Button btReSelect;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtCheckCount;
		protected System.Web.UI.WebControls.DataGrid Datagrid2;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.DropDownList ddlDayCheckNo;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.TextBox txtCheckNo;
		protected string strCheckDate;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			this.ClientScript.RegisterStartupScript(this.GetType(),"hide","<script lanaguage=javascript>ShowHide('1','none');</script>");
			if (!IsPostBack )
			{
				if(this.oper.strNewDeptID=="CEN00")
				{
					this.FillDropDownList("NewDept",ddlDept);
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
				}
				else
				{
					this.FillDropDownList("NewDept",ddlDept,"vcCommCode='"+this.oper.strDeptID+"'");
					this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.oper.strDeptID+"'");
				}
				strCheckDate=DateTime.Now.ToString("yyyy-MM-dd");
				Session.Remove("QUERY");
				Session.Remove("page_view");
				this.btnCheckOk.Enabled=false;
				this.txtCheckNo.Visible=false;
				this.ddlState.Items.Add(new ListItem("ȫ��","ȫ��"));
				this.ddlState.Items.Add(new ListItem("δȷ��","0"));
				this.ddlState.Items.Add(new ListItem("��ȷ�ϸ���","1"));
				this.ddlState.SelectedIndex=0;
				this.Button1.Enabled=false;
				DataTable dtcheckno=Helper.Query("select distinct cnvcCheckNo as cnvcCode,cnvcCheckNo as cnvcName from tbStorageCheckLog where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo like convert(char(8),getdate(),112)+'%'");
				this.FillDropDownList(dtcheckno,this.ddlDayCheckNo);
				this.ddlDayCheckNo.Items.Insert(0,new ListItem("ȫ��","ȫ��"));
			}
			else
			{
				strCheckDate=Request.Form["txtCheckDate"].ToString();
			}
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.ddlDept.SelectedIndexChanged += new System.EventHandler(this.ddlDept_SelectedIndexChanged);
			this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
			this.btReSelect.Click += new System.EventHandler(this.btReSelect_Click);
			this.ddlState.SelectedIndexChanged += new System.EventHandler(this.ddlState_SelectedIndexChanged);
			this.btnCheckOk.Click += new System.EventHandler(this.btnCheckOk_Click);
			this.Datagrid2.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.Datagrid2_PageIndexChanged);
			this.Datagrid2.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_CancelCommand);
			this.Datagrid2.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_EditCommand);
			this.Datagrid2.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.Datagrid2_UpdateCommand);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.DataGrid1.SelectedIndexChanged += new System.EventHandler(this.DataGrid1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btQuery_Click(object sender, System.EventArgs e)
		{
			this.DBBind();
		}

		private void btnCheckOk_Click(object sender, System.EventArgs e)
		{
			DataTable dt=(DataTable)Session["checktomod"];
			if(this.ddlDayCheckNo.SelectedValue=="")
			{
				this.Popup("�̵���Ų���ȷ�������̵��¼�Ѿ����ǵ��ղ�����¼��");
				return;
			}
			if(dt.Rows.Count<=0)
			{
				this.Popup("���κ��̵�����¼��");
				return;
			}
			Entity.OperLog ol = new AMSApp.zhenghua.Entity.OperLog();
			ol.cnvcOperType = "�ֿ����̵�";
			ol.cnvcOperID = this.oper.strLoginID;
			ol.cnvcDeptID = this.oper.strDeptID;

			DataTable dtre=Helper.Query("select count(*) from tbStorageCheckLog where cnvcCheckNo='"+this.ddlDayCheckNo.SelectedValue+"' and (cndMdate is null or cndExpDate is null)");
			if(dtre.Rows[0][0].ToString()!="0")
			{
				this.Popup("�̵����д����������ڻ��������Ϊ�յļ�¼�������޸ģ�");
				return;
			}
			StorageFacade sto = new StorageFacade();				
			int ret = sto.StorageCheckLogConfirm(ol,this.ddlDayCheckNo.SelectedValue,this.ddlWhouse.SelectedValue,this.ddlDept.SelectedValue);
			if(ret > 0 )
			{
				this.Popup("�ֿ����̵�ȷ�ϸ��¿��ɹ���");
				this.DBBind();
			}
			else
			{
				this.Popup("�ֿ����̵�ȷ�ϸ��¿��ʧ�ܣ�");
			}
		}

		private void DBBind()
		{
			Session.Remove("QUERY");
			Session.Remove("page_view");
			strCheckDate = Request.Form["txtCheckDate"].ToString();
			if(strCheckDate==null||strCheckDate=="")
			{
				this.Popup("ʱ�䲻��Ϊ�գ�������ѡ��ʱ�䣡");
				return;
			}
			DateTime dtcheckdate=DateTime.Parse(strCheckDate);
			string strquerydate=strCheckDate.Substring(0,4);
			int liindex1=strCheckDate.IndexOf("-");
			int liindex2=strCheckDate.IndexOf("-",liindex1+1);
			if(liindex2-liindex1==2)
				strquerydate+="0"+strCheckDate.Substring(liindex1+1,1);
			else
				strquerydate+=strCheckDate.Substring(liindex1+1,2);
			if(strCheckDate.Length-liindex2==2)
				strquerydate+="0"+strCheckDate.Substring(liindex2+1,1);
			else
				strquerydate+=strCheckDate.Substring(liindex2+1,2);
			string strWHouse=this.ddlWhouse.SelectedValue;
			if(strquerydate!=this.txtCheckNo.Text.Trim())
			{
				this.ddlState.SelectedIndex=this.ddlState.Items.IndexOf(this.ddlState.Items.FindByValue("ȫ��"));
			}
			this.txtCheckNo.Text=strquerydate;
			this.ddlDept.Enabled=false;
			this.ddlWhouse.Enabled=false;
			this.btnCheckOk.Enabled=false;
			if(dtcheckdate.CompareTo(DateTime.Now.Date)!=0)
			{
				this.Button1.Enabled=false;
			}
			if(this.ddlState.SelectedValue=="ȫ��")
			{
				this.ddlDayCheckNo.SelectedIndex=this.ddlDayCheckNo.Items.IndexOf(this.ddlDayCheckNo.Items.FindByValue("ȫ��"));
				this.ddlDayCheckNo.Enabled=false;
			}
			DataTable dtcheckno=new DataTable();
			string strsql="select a.cnvcCheckNo,a.cnnSerialNo,a.cnvcDeptID,a.cnvcWhCode,a.cnvcInvCode,b.cnvcInvName,convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate,a.cnvcUnitCode,a.cnnSysCount,a.cnnCheckCount,a.cnvcOperName,a.cndOperDate,";
			strsql+="(case a.cnvcFlag when '0' then 'δȷ��' else '��ȷ�ϸ���' end) as cnvcFlag from tbStorageCheckLog a,tbInventory b where a.cnvcInvCode=b.cnvcInvCode and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo like '"+strquerydate+"%'";
			if(this.ddlState.SelectedValue!="ȫ��")
			{
				strsql+=" and a.cnvcFlag='"+this.ddlState.SelectedValue+"'";
			}
			if(this.ddlDayCheckNo.SelectedValue!="ȫ��")
			{
				strsql+=" and a.cnvcCheckNo='"+this.ddlDayCheckNo.SelectedValue+"'";
			}
			strsql+=" order by a.cnvcCheckNo,a.cnnSerialNo";
			DataTable dtout=Helper.Query(strsql);
			this.TableConvert(dtout,"cnvcDeptID","NewDept","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcWhCode","WareHouse","vcCommCode","vcCommName");
			this.TableConvert(dtout,"cnvcUnitCode","ComputationUnit","vcCommCode","vcCommName");
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=dtout;
			Session["checktomod"]=dtout;
			if(this.ddlState.SelectedValue=="0"&&this.ddlDayCheckNo.SelectedValue!="ȫ��")
			{
				if(dtcheckdate.CompareTo(DateTime.Now.Date)==0)
				{
					this.Datagrid2.Columns[9].Visible=true;
					this.Datagrid2.Columns[10].Visible=false;
					this.Datagrid2.Columns[11].Visible=true;
					this.Datagrid2.Columns[12].Visible=false;
					this.Datagrid2.Columns[16].Visible=true;
					this.btnCheckOk.Enabled=true;
					this.Button1.Enabled=true;
					this.ddlDept.Enabled=false;
					this.ddlWhouse.Enabled=false;
					this.ddlState.Enabled=false;
					this.ddlDayCheckNo.Enabled=false;
				}
				else
				{
					this.Datagrid2.Columns[9].Visible=true;
					this.Datagrid2.Columns[10].Visible=false;
					this.Datagrid2.Columns[11].Visible=true;
					this.Datagrid2.Columns[12].Visible=false;
					this.Datagrid2.Columns[16].Visible=false;
					this.btnCheckOk.Enabled=false;
					this.Button1.Enabled=false;
				}
			}
			else
			{
				if(dtcheckdate.CompareTo(DateTime.Now.Date)==0)
				{
					this.Datagrid2.Columns[9].Visible=true;
					this.Datagrid2.Columns[10].Visible=false;
					this.Datagrid2.Columns[11].Visible=true;
					this.Datagrid2.Columns[12].Visible=false;
					this.Datagrid2.Columns[16].Visible=false;
					this.btnCheckOk.Enabled=false;
					this.Button1.Enabled=true;
				}
				else
				{
					this.Datagrid2.Columns[9].Visible=true;
					this.Datagrid2.Columns[10].Visible=false;
					this.Datagrid2.Columns[11].Visible=true;
					this.Datagrid2.Columns[12].Visible=false;
					this.Datagrid2.Columns[16].Visible=false;
					this.btnCheckOk.Enabled=false;
					this.Button1.Enabled=false;
				}
			}
			this.Datagrid2.CurrentPageIndex=0;
			this.Datagrid2.DataBind();
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.Button1.Enabled=false;
				this.btnCheckOk.Enabled=false;
			}
		}

		private void btReSelect_Click(object sender, System.EventArgs e)
		{
			this.ddlState.SelectedIndex=this.ddlState.Items.IndexOf(this.ddlState.Items.FindByValue("ȫ��"));
			this.ddlDayCheckNo.Items.Clear();
			this.ddlDayCheckNo.Items.Add(new ListItem("ȫ��","ȫ��"));
			this.ddlDept.Enabled=true;
			this.ddlWhouse.Enabled=true;
			this.ddlState.Enabled=true;
			this.ddlDayCheckNo.Enabled=true;
			this.btnCheckOk.Enabled=false;
			DataTable dt=new DataTable();
			this.Datagrid2.DataSource=dt;
			this.Datagrid2.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(this.txtQueryInvCode.Text.Trim()==""&&this.txtQueryInvName.Text.Trim()=="")
			{
				this.Popup("��ѯ�̵��������������������һ����");
				return;
			}
			string strsql="select cnvcInvCode,cnvcInvName,cnvcSTComunitCode,cnvcSTComunitCode as cnvcSTComunitName,cnvcInvCCode,cnnSysCount,isnull(cndMdate,'') as cndMdate,isnull(cndExpDate,'') as cndExpDate from (";
			strsql+="select a.cnvcInvCode,a.cnvcInvName,a.cnvcSTComunitCode,a.cnvcSTComunitCode as cnvcSTComunitName,a.cnvcInvCCode,cast(b.cnnQuantity/c.cniChangRate as numeric(18,2)) as cnnSysCount,convert(char(10),b.cndMdate,120) as cndMdate,";
			strsql+="convert(char(10),b.cndExpDate,120) as cndExpDate from tbInventory a,tbCurrentStock b,tbComputationUnit c where b.cnvcStopFlag='0' and b.cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and a.cnvcInvCode=b.cnvcInvCode and a.cnvcSTComunitCode=c.cnvcComunitCode";
			strsql+=" and a.cnvcInvCode+isnull(convert(char(8),b.cndMdate,112),'')+isnull(convert(char(8),b.cndExpDate,112),'') not in(select cnvcInvCode+isnull(convert(char(8),cndMdate,112),'')+isnull(convert(char(8),cndExpDate,112),'')";
			strsql+=" from tbStorageCheckLog where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo in(select distinct cnvcCheckNo from tbStorageCheckLog where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"'";
			strsql+=" and cnvcCheckNo like convert(char(8),getdate(),112)+'%' and cnvcFlag='0')) and a.cnvcInvCode+convert(char(8),b.cndMdate,112)+convert(char(8),b.cndExpDate,112) not in(select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112)";
			strsql+=" from tbCurrentStock where convert(char(8),cndExpDate,112)<convert(char(8),getdate(),112) and cnnQuantity=0 and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"') union all select a.cnvcInvCode,a.cnvcInvName,a.cnvcSTComunitCode,a.cnvcSTComunitCode as cnvcSTComunitName,a.cnvcInvCCode,";
			strsql+="0 as cnnSysCount,null as cndMdate,null as cndExpDate from tbInventory a where a.cnvcInvCode not in(select cnvcInvCode from tbCurrentStock where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"')";
			strsql+="  and a.cnvcInvCode not in(select distinct cnvcInvCode from tbStorageCheckLog where cnvcDeptID='"+this.ddlDept.SelectedValue+"' and cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo like convert(char(8),getdate(),112)+'%' and cnvcFlag='0')) c";

			string strcon="";
			if(this.txtQueryInvCode.Text.Trim()!="")
			{
				strcon+=" where c.cnvcInvCode='"+this.txtQueryInvCode.Text.Trim()+"'";
			}
			if(this.txtQueryInvName.Text.Trim()!="")
			{
				if(strcon=="")
					strcon+=" where c.cnvcInvName like '%"+this.txtQueryInvName.Text.Trim()+"%'";
				else
					strcon+=" and c.cnvcInvName like '%"+this.txtQueryInvName.Text.Trim()+"%'";
			}
			strsql+=strcon;
			DataTable dt = Helper.Query(strsql);
			this.TableConvert(dt,"cnvcSTComunitName","ComputationUnit","vcCommCode","vcCommName");
			this.TableConvert(dt,"cnvcInvCCode","PClass","vcCommCode","vcCommName");
			this.DataGrid1.DataSource = dt;
			this.DataGrid1.DataBind();
			this.ClientScript.RegisterStartupScript(this.GetType(),"show","<script lanaguage=javascript>ShowHide('1','block');</script>");
		}

		private void DataGrid1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataGridItem item = this.DataGrid1.SelectedItem;
			if(this.txtCheckCount.Text.Trim()==""||!this.JudgeIsNum(this.txtCheckCount.Text.Trim()))
			{
				this.Popup("�̵��������Ϊ���ұ��������֣�");
				return;
			}
				
			StorageCheckLog scl=new StorageCheckLog();
			scl.cnvcDeptID=this.ddlDept.SelectedValue;
			scl.cnvcWhCode=this.ddlWhouse.SelectedValue;
			scl.cnvcInvCode=item.Cells[0].Text.Trim();
			scl.cnnSysCount=Math.Round(decimal.Parse(item.Cells[5].Text.Trim()),2);
			scl.cnnCheckCount=Math.Round(decimal.Parse(this.txtCheckCount.Text.Trim()),2);
			scl.cnvcUnitCode=item.Cells[2].Text.Trim();
			scl.cnvcOperName=this.oper.strOperName;
			scl.cnvcFlag="0";
			string strMdate="";
			string strExpdate="";
			if(item.Cells[6].Text.Trim()!="")
			{
				scl.cndMdate=DateTime.Parse(item.Cells[6].Text.Trim());
				strMdate=scl.cndMdate.Year.ToString();
				if(scl.cndMdate.Month<10)
					strMdate+="0"+scl.cndMdate.Month.ToString();
				else
					strMdate+=scl.cndMdate.Month.ToString();
				if(scl.cndMdate.Day<10)
					strMdate+="0"+scl.cndMdate.Day.ToString();
				else
					strMdate+=scl.cndMdate.Day.ToString();
			}
			if(item.Cells[7].Text.Trim()!="")
			{
				scl.cndExpDate=DateTime.Parse(item.Cells[7].Text.Trim());
				strExpdate=scl.cndExpDate.Year.ToString();
				if(scl.cndExpDate.Month<10)
					strExpdate+="0"+scl.cndExpDate.Month.ToString();
				else
					strExpdate+=scl.cndExpDate.Month.ToString();
				if(scl.cndExpDate.Day<10)
					strExpdate+="0"+scl.cndExpDate.Day.ToString();
				else
					strExpdate+=scl.cndExpDate.Day.ToString();
			}
			string strcheckno="";

			string strisexist="";
			if(strMdate==""&&strExpdate=="")
			{
				strisexist=Helper.Query("select count(*) from tbStorageCheckLog where cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcInvCode='"+scl.cnvcInvCode+"' and cnvcFlag='0' and cnvcCheckNo like convert(char(8),getdate(),112)+'%'").Rows[0][0].ToString();
			}
			else
			{
				strisexist=Helper.Query("select count(*) from tbStorageCheckLog where cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcInvCode='"+scl.cnvcInvCode+"' and cnvcFlag='0' and cnvcCheckNo like convert(char(8),getdate(),112)+'%' and convert(char(8),cndMdate,112)='"+strMdate+"' and convert(char(8),cndExpDate,112)='"+strExpdate+"'").Rows[0][0].ToString();
			}
			if(strisexist!="0")
			{
				this.Popup("���ֿ��"+item.Cells[1].Text.Trim()+"�ڽ����ڻ���δȷ�ϵ��̵��¼��������ӣ�");
				return;
			}
			else
			{
				StorageFacade sto=new StorageFacade();
				int ret=sto.AddStorageCheckLog(scl,this.txtCheckNo.Text.Trim(),out strcheckno);
				if(ret > 0 )
				{
					this.Popup("����̵����ɹ���");
					this.ddlState.SelectedIndex=this.ddlState.Items.IndexOf(this.ddlState.Items.FindByValue("0"));
					DataTable dtcheckno=Helper.Query("select distinct cnvcCheckNo as cnvcCode,cnvcCheckNo as cnvcName from tbStorageCheckLog where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcCheckNo like '"+this.txtCheckNo.Text.Trim()+"%'");
					this.FillDropDownList(dtcheckno,this.ddlDayCheckNo);
					this.ddlDayCheckNo.SelectedIndex=this.ddlDayCheckNo.Items.IndexOf(this.ddlDayCheckNo.Items.FindByValue(strcheckno));
					this.ddlDept.Enabled=false;
					this.ddlWhouse.Enabled=false;
					this.ddlDayCheckNo.Enabled=false;
					this.ddlState.Enabled=false;
					this.btnCheckOk.Enabled=true;
				}
				else
				{
					if(ret<0)
					{
						this.Popup("�òֿ����̵���š�"+strcheckno+"�����Ѿ��д˴�����̵��¼�������ԣ�");
					}
					else
					{
						this.Popup("����̵���ʧ�ܣ�");
					}
				}

				this.DataGrid1.DataSource = null;
				this.DataGrid1.DataBind();
				this.DBBind();
			}
		}

		private void Datagrid2_EditCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=e.Item.ItemIndex;
			this.Datagrid2.DataSource=(DataTable)Session["checktomod"];
			this.Datagrid2.DataBind();
			this.Datagrid2.Columns[9].Visible=false;
			this.Datagrid2.Columns[10].Visible=true;
			this.Datagrid2.Columns[11].Visible=false;
			this.Datagrid2.Columns[12].Visible=true;
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				if(dgi.ItemIndex==e.Item.ItemIndex)
				{
					if(dgi.Cells[9].Text.Trim()!=""&&dgi.Cells[9].Text.Trim()!="&nbsp;")
					{
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[10].Controls[1]).Value=dgi.Cells[9].Text.Trim();
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[12].Controls[1]).Value=dgi.Cells[11].Text.Trim();
					}
					else
					{
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[10].Controls[1]).Value="";
						((System.Web.UI.HtmlControls.HtmlInputText)dgi.Cells[12].Controls[1]).Value="";
					}
				}
				else
				{
					dgi.Cells[10].Text=dgi.Cells[9].Text.Trim();
					dgi.Cells[12].Text=dgi.Cells[11].Text.Trim();
				}
			}
			this.btnCheckOk.Enabled=false;
		}

		private void Datagrid2_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataSource=(DataTable)Session["checktomod"];
			this.Datagrid2.DataBind();
			this.Datagrid2.Columns[9].Visible=true;
			this.Datagrid2.Columns[10].Visible=false;
			this.Datagrid2.Columns[11].Visible=true;
			this.Datagrid2.Columns[12].Visible=false;
			this.btnCheckOk.Enabled=true;
		}

		private void hidecontrol()
		{
			foreach(DataGridItem dgi in this.Datagrid2.Items)
			{
				dgi.Cells[10].Text=dgi.Cells[9].Text.Trim();
				dgi.Cells[12].Text=dgi.Cells[11].Text.Trim();
			}
		}

		private void Datagrid2_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			string checkcount=((TextBox)e.Item.Cells[8].Controls[0]).Text.Trim();
			string strmdate="";
			string strexpdate="";
			string strflag="";
			if(e.Item.Cells[9].Text.Trim()==""||e.Item.Cells[9].Text.Trim()=="&nbsp;")
			{
				strmdate=((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[10].Controls[1]).Value;
				strexpdate=((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[12].Controls[1]).Value;
			}
			else
			{
				strmdate=e.Item.Cells[9].Text.Trim();
				strexpdate=e.Item.Cells[11].Text.Trim();
			}

			string checkno=e.Item.Cells[0].Text.Trim();
			if(checkcount==""||!this.JudgeIsNum(checkcount))
			{
				this.Popup("�̵��������Ϊ���ұ��������֣�");
				this.hidecontrol();
				return;
			}
			if(strmdate=="")
			{
				this.Popup("�������ڲ���Ϊ�գ�");
				this.hidecontrol();
				return;
			}
			if(strexpdate=="")
			{
				this.Popup("�������ڲ���Ϊ�գ�");
				this.hidecontrol();
				return;
			}
			StorageCheckLog scl=new StorageCheckLog();
			scl.cnnSerialNo=decimal.Parse(e.Item.Cells[1].Text.Trim());
			scl.cnnCheckCount=Math.Round(decimal.Parse(checkcount),2);
			scl.cnvcOperName=this.oper.strOperName;
			if(e.Item.Cells[9].Text.Trim()==""||e.Item.Cells[9].Text.Trim()=="&nbsp;")
			{
				scl.cndMdate=DateTime.Parse(strmdate);
				scl.cndExpDate=DateTime.Parse(strexpdate);
				strflag="up_date";
				if(scl.cndMdate.CompareTo(scl.cndExpDate)>0)
				{
					this.Popup("�������ڲ��ܴ��ڹ������ڣ�");
					this.hidecontrol();
					return;
				}
			}
			StorageFacade sto=new StorageFacade();
			int ret=sto.UpdateStorageCheckLog(scl,strflag);
			if(ret > 0 )
			{
				this.Popup("�޸��̵���������ɹ���");
			}
			else
			{
				this.Popup("�޸��̵��������ʧ�ܣ�");
			}

			this.DBBind();
			this.ddlDept.Enabled=false;
			this.ddlWhouse.Enabled=false;
			this.ddlDayCheckNo.Enabled=false;
			this.btnCheckOk.Enabled=true;
		}

		private void Datagrid2_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.Datagrid2.CurrentPageIndex = e.NewPageIndex;			
			this.Datagrid2.DataSource=(DataTable)Session["checktomod"];
			this.Datagrid2.EditItemIndex=-1;
			this.Datagrid2.DataBind();
		}

		private void ddlState_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataTable dtcheckno=new DataTable();
			if(this.ddlState.SelectedValue=="ȫ��")
			{
				this.ddlDayCheckNo.SelectedIndex=this.ddlDayCheckNo.Items.IndexOf(this.ddlDayCheckNo.Items.FindByValue("ȫ��"));
				this.ddlDayCheckNo.Enabled=false;
			}
			else
			{
				dtcheckno=Helper.Query("select distinct cnvcCheckNo as cnvcCode,cnvcCheckNo as cnvcName from tbStorageCheckLog where cnvcWhCode='"+this.ddlWhouse.SelectedValue+"' and cnvcFlag='"+this.ddlState.SelectedValue+"' and cnvcCheckNo like '"+this.txtCheckNo.Text.Trim()+"%'");
				this.FillDropDownList(dtcheckno,this.ddlDayCheckNo);
				this.ddlDayCheckNo.Items.Insert(0,new ListItem("ȫ��","ȫ��"));
				this.ddlDayCheckNo.Enabled=true;
			}
		}

		private void ddlDept_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.oper.strNewDeptID=="CEN00")
			{
				this.FillDropDownList("Warehouse",this.ddlWhouse,"cnvcDepCode='"+this.ddlDept.SelectedValue+"'");
			}
		}
	}
}
