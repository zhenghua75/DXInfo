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
using BusiComm;

namespace AMSApp.Employ
{
	/// <summary>
	/// Summary description for wfmWorkDaily.
	/// </summary>
	public partial class wfmWorkDaily : wfmBase
	{
	
		EmpBusi ebu;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSchDate;
		protected string strSchDate;
		protected string strManager;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(Session["Login"]!=null)
			{
				CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
//				if(ls1.strLimit=="CL003"||ls1.strLimit=="CL004")
//				{
//					this.SetErrorMsgPageBydir("对不起，你没有权限使用此功能！");
//					return;
//				}
				if (!IsPostBack )
				{
					this.FillDropDownList("tbCommCode", ddlDept, "vcCommSign ='MD'");
					this.FillDropDownList("tbCommCode",ddlOfficer,"vcCommSign='OF'");
					if(ls1.strLimit=="CL002")
					{
						ddlDept.Items.FindByValue(ls1.strDeptID).Selected=true;
						ddlDept.Enabled=false;
					}
					else
					{
						if(Request.QueryString.HasKeys())
						{
							string strDeptLast=Request.QueryString["dept"];
							ddlDept.Items.FindByValue(strDeptLast).Selected=true;
						}
					}
					strSchDate=DateTime.Now.ToShortDateString();
					this.lblManager.Text="--";

					ddlClass.Items.Clear();
					string strOfficer=ddlOfficer.SelectedValue;
					Hashtable htTime=(Hashtable)Application["IOTime"];
					if(!htTime.ContainsKey(strOfficer))
					{
						this.SetErrorMsgPageBydir("暂无该职务的班次信息，请通知管理员设置参数！");
						return;
					}
					else
					{
						ArrayList alclass=(ArrayList)htTime[strOfficer];
						CMSMStruct.SignIOTimeStruct siot=new CommCenter.CMSMStruct.SignIOTimeStruct();
						for(int i=0;i<alclass.Count;i++)
						{
							siot=(CMSMStruct.SignIOTimeStruct)alclass[i];
							ListItem litmp=new ListItem(siot.strClassName,siot.strClassId);
							ddlClass.Items.Add(litmp);
						}
					}
				}
				else
				{
					strSchDate = Request.Form["txtSchDate"].ToString();
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

		}
		#endregion

		protected void btOk_Click(object sender, System.EventArgs e)
		{
			string[] SchDatelist=strSchDate.Split('-');
			if(SchDatelist.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}
			string strSchID=SchDatelist[0];
			if(int.Parse(SchDatelist[1])<10)
			{
				strSchID+="0" + SchDatelist[1];
			}
			else
			{
				strSchID+=SchDatelist[1];
			}
			if(int.Parse(SchDatelist[2])<10)
			{
				strSchID+="0" + SchDatelist[2];
			}
			else
			{
				strSchID+=SchDatelist[2];
			}
			string strDeptID=ddlDept.SelectedValue;
			string strClass=ddlClass.SelectedItem.Text;
			string strClassID=ddlClass.SelectedValue;
			string strDeptName=ddlDept.SelectedItem.Text;
			string strManager=this.lblManager.Text;
			string strOfficer=ddlOfficer.SelectedItem.Text;
			string strOfficerID=ddlOfficer.SelectedValue;
			Hashtable htpara=(Hashtable)Session["Schcond"];
			if(htpara==null)
			{
				this.SetErrorMsgPageBydir("请先查询，再进行排班！");
				return;
			}
			if(strDeptID!=htpara["strDeptID"].ToString()||strClass!=htpara["strClass"].ToString()||strManager!=htpara["strManager"].ToString()||strOfficer!=htpara["strOfficer"].ToString()||strSchID!=htpara["strSchID"].ToString())
			{
				this.SetErrorMsgPageBydir("排班计划参数与之前选定的不一致，请重新查询刷新再排班！");
				return;
			}

			ArrayList alSched=new ArrayList();
			for(int i=0;i<ltbSchedEmp.Items.Count;i++)
			{
				alSched.Add(ltbSchedEmp.Items[i].Text);
			}
			Hashtable htIOT=(Hashtable)Application["IOTime"];
			ArrayList altime=(ArrayList)htIOT[strOfficerID];
			string strCheckInDate="";
			string strCheckOutDate="";
			for(int j=0;j<altime.Count;j++)
			{
				CMSMStruct.SignIOTimeStruct siottmp=(CMSMStruct.SignIOTimeStruct)altime[j];
				if((strOfficerID+strClassID)==siottmp.strSIOTID)
				{
					strCheckInDate=strSchDate+" "+siottmp.strInTime+":00";
					strCheckOutDate=strSchDate+" "+siottmp.strOutTime+":00";
				}
			}
			if(strCheckInDate==""||strCheckOutDate=="")
			{
				this.SetErrorMsgPageBydir("获取上下班时间参数错误，请检查参数！");
				return;
			}
			htpara.Add("strCheckInDate",strCheckInDate);
			htpara.Add("strCheckOutDate",strCheckOutDate);

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			ebu=new EmpBusi(strcons);
			if(ebu.SchedEmpDaily(htpara,alSched))
			{
				Session.Remove("Schcond");
				this.ltbAllEmp.Items.Clear();
				this.ltbSchedEmp.Items.Clear();
				this.btquery.Enabled=true;
				this.ddlClass.Enabled=true;
				this.ddlOfficer.Enabled=true;
				this.ddlDept.Enabled=true;
				this.lblManager.Text="--";
				this.SetSuccMsgPageBydir("排班计划保存成功！","Employ/wfmWorkDaily.aspx?dept="+strDeptID);
			}
			else
			{
				this.SetErrorMsgPageBydir("排班计划保存失败！");
			}
		}

		protected void btquery_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Schcond");
			this.ltbAllEmp.Items.Clear();
			this.ltbSchedEmp.Items.Clear();
			string[] SchDatelist=strSchDate.Split('-');
			if(SchDatelist.Length!=3)
			{
				this.SetErrorMsgPageBydir("时间格式不正确！");
				return;
			}
			string strSchID=SchDatelist[0];
			if(int.Parse(SchDatelist[1])<10)
			{
				strSchID+="0" + SchDatelist[1];
			}
			else
			{
				strSchID+=SchDatelist[1];
			}
			if(int.Parse(SchDatelist[2])<10)
			{
				strSchID+="0" + SchDatelist[2];
			}
			else
			{
				strSchID+=SchDatelist[2];
			}
			DateTime datetimesch=new DateTime(int.Parse(SchDatelist[0]),int.Parse(SchDatelist[1]),int.Parse(SchDatelist[2]));
			if(datetimesch.CompareTo(DateTime.Today)<0)
			{
				this.SetErrorMsgPageBydir("不能对以前的排班计划进行修改！");
				return;
			}

			Hashtable htapp=(Hashtable)Application["appconf"];
			string strcons=(string)htapp["cons"];
			ebu=new EmpBusi(strcons);

			string strDeptID=ddlDept.SelectedValue;
			if(strDeptID!="")
			{
				DataTable dtMana=ebu.GetEmpManager(strDeptID);
				strManager="";
				for(int i=0;i<dtMana.Rows.Count;i++)
				{
					strManager+=dtMana.Rows[i]["vcEmpName"].ToString()+",";
				}
				if(strManager!="")
				{
					strManager=strManager.Substring(0,strManager.Length-1);
				}
				this.lblManager.Text=strManager;
			}
			else
			{
				this.SetErrorMsgPageBydir("门店信息错误，请重新刷新页面再操作！");
				return;
			}
			if(strManager=="")
			{
				this.SetErrorMsgPageBydir("没有找到店长信息！");
				return;
			}
			
			string strOfficer=ddlOfficer.SelectedItem.Text;
			string strOfficerID=ddlOfficer.SelectedValue;
			string strClass=ddlClass.SelectedItem.Text;
			string strDeptName=ddlDept.SelectedItem.Text;
			if(strDeptID==""||strDeptName==""||strClass==""||strSchDate=="")
			{
				this.SetErrorMsgPageBydir("请选择要排班的参数信息！");
				return;
			}

			Hashtable htPara=new Hashtable();
			htPara.Add("strDeptID",strDeptID);
			htPara.Add("strDeptName",strDeptName);
			htPara.Add("strOfficer",strOfficer);
			htPara.Add("strOfficerID",strOfficerID);
			htPara.Add("strClass",strClass);
			htPara.Add("strSchID",strSchID);
			htPara.Add("strManager",strManager);
			Session["Schcond"]=htPara;
			DataSet dsout=ebu.GetEmpSchList(htPara);
			if(dsout==null||dsout.Tables["tbEmployee"]==null||dsout.Tables["tbEmpSchLog"]==null)
			{
				this.SetErrorMsgPageBydir("查询当前排班情况出错！");
				return;
			}
			else
			{
				this.lblSchedTitle.Text=strSchDate+"的"+strClass+"有如下员工上班";
				for(int i=0;i<dsout.Tables["tbEmpSchLog"].Rows.Count;i++)
				{
					this.ltbSchedEmp.Items.Add(dsout.Tables["tbEmpSchLog"].Rows[i]["vcEmpName"].ToString());
				}
				
				for(int k=0;k<dsout.Tables["tbEmployee"].Rows.Count;k++)
				{
					if(dsout.Tables["tbEmpSchLog"].Rows.Count>0)
					{
						bool flag=false;
						for(int j=0;j<dsout.Tables["tbEmpSchLog"].Rows.Count;j++)
						{
							if(dsout.Tables["tbEmployee"].Rows[k]["vcEmpName"].ToString()==dsout.Tables["tbEmpSchLog"].Rows[j]["vcEmpName"].ToString())
							{
								flag=true;
								break;
							}
						}
						if(!flag)
						{
							this.ltbAllEmp.Items.Add(dsout.Tables["tbEmployee"].Rows[k]["vcEmpName"].ToString());
						}
					}
					else
					{
						this.ltbAllEmp.Items.Add(dsout.Tables["tbEmployee"].Rows[k]["vcEmpName"].ToString());
					}
				}
			}

			this.btquery.Enabled=false;
			this.ddlClass.Enabled=false;
			this.ddlOfficer.Enabled=false;
			this.ddlDept.Enabled=false;
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			int selectcount=0;
			ArrayList alselect=new ArrayList();
			for(int i=0;i<ltbAllEmp.Items.Count;i++)
			{
				if(ltbAllEmp.Items[i].Selected)
				{
					selectcount++;
					alselect.Add(i);
				}
			}
			for(int j=0;j<selectcount;j++)
			{
				ltbAllEmp.Items[(int)alselect[j]-j].Selected=false;
				this.ltbSchedEmp.Items.Add(ltbAllEmp.Items[(int)alselect[j]-j]);
				this.ltbAllEmp.Items.Remove(ltbAllEmp.Items[(int)alselect[j]-j]);
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			int selectcount=0;
			ArrayList alselect=new ArrayList();
			for(int i=0;i<ltbSchedEmp.Items.Count;i++)
			{
				if(ltbSchedEmp.Items[i].Selected)
				{
					selectcount++;
					alselect.Add(i);
				}
			}
			for(int j=0;j<selectcount;j++)
			{
				ltbSchedEmp.Items[(int)alselect[j]-j].Selected=false;
				this.ltbAllEmp.Items.Add(ltbSchedEmp.Items[(int)alselect[j]-j]);
				this.ltbSchedEmp.Items.Remove(ltbSchedEmp.Items[(int)alselect[j]-j]);
			}
		}

		protected void btrefresh_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Schcond");
			this.ltbAllEmp.Items.Clear();
			this.ltbSchedEmp.Items.Clear();
			this.btquery.Enabled=true;
			this.ddlClass.Enabled=true;
			this.ddlOfficer.Enabled=true;
			CMSMStruct.LoginStruct ls1=(CMSMStruct.LoginStruct)Session["Login"];
			if(ls1.strLimit=="CL001")
			{
				ddlDept.Enabled=true;
			}
			this.lblManager.Text="--";
		}

		protected void ddlOfficer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ddlClass.Items.Clear();
			string strOfficer=ddlOfficer.SelectedValue;
			Hashtable htTime=(Hashtable)Application["IOTime"];
			if(!htTime.ContainsKey(strOfficer))
			{
				this.SetErrorMsgPageBydir("暂无该职务的班次信息，请通知管理员设置参数！");
				return;
			}
			else
			{
				ArrayList alclass=(ArrayList)htTime[strOfficer];
				CMSMStruct.SignIOTimeStruct siot=new CommCenter.CMSMStruct.SignIOTimeStruct();
				for(int i=0;i<alclass.Count;i++)
				{
					siot=(CMSMStruct.SignIOTimeStruct)alclass[i];
					ListItem litmp=new ListItem(siot.strClassName,siot.strClassId);
					ddlClass.Items.Add(litmp);
				}
			}
		}			
	}
}
