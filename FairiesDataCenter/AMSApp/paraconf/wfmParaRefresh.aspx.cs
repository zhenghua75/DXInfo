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
using DataAccess;
using CommCenter;
using ynhnTransportManage;
using Ninject;
using DXInfo.Data.Contracts;
using System.Web.Mvc;

namespace AMSApp.paraconf
{
	/// <summary>
	/// Summary description for wfmParaRefresh.
	/// </summary>
	public class wfmParaRefresh : wfmBase
	{
        //[Inject]
        //public IFairiesMemberManageUow Uow { get; set; }
        //[Inject]
        //public IAMSCMUow AmscmUow { get; set; }
		protected System.Web.UI.WebControls.Button btrefresh;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
            if (Session["Login"] == null)
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
			this.btrefresh.Click += new System.EventHandler(this.btrefresh_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected void btrefresh_Click(object sender, System.EventArgs e)
		{
			try
			{
				DataSet dsIn  = new DataSet();

				InitCode inc=new InitCode();
				Hashtable htapp=(Hashtable)Application["appconf"];
				string strcons=(string)htapp["cons"];
				DataSet dsOut = inc.LoadCodeTable(strcons);
				AMSApp.zhenghua.Business.Helper.LoadInitCode(Application);
			
				//错误返回表

				//返回结果存放到Application
				Application.Set("tbCommCode",dsOut.Tables["tbCommCode"]);
                Application.Set("AssAT", dsOut.Tables["AssAT"]);
                Application.Set("AT1", dsOut.Tables["AT1"]);
				Application.Set("AllMD",dsOut.Tables["AllMD"]);
				Application.Set("MAC",dsOut.Tables["MAC"]);
				Application.Set("OperFunc",dsOut.Tables["OperFunc"]);
				Application.Set("IOTime",dsOut.Tables["IOTime"]);
				Application.Set("Goods",dsOut.Tables["Goods"]);
				Application.Set("PClass",dsOut.Tables["PClass"]);
				Application.Set("AllMaterial",dsOut.Tables["AllMaterial"]);
				Application.Set("Provider",dsOut.Tables["Provider"]);
				Application.Set("NewDept",dsOut.Tables["NewDept"]);
				Application.Set("tbNameCodeToStorage",dsOut.Tables["tbNameCodeToStorage"]);
				Application.Set("tbFormula",dsOut.Tables["tbFormula"]);
				Application.Set("DeptMapInfo",dsOut.Tables["DeptMapInfo"]);
				Application.Set("Warehouse",dsOut.Tables["Warehouse"]);
				Application.Set("ComputationGroup",dsOut.Tables["ComputationGroup"]);
				Application.Set("ComputationUnit",dsOut.Tables["ComputationUnit"]);

				Hashtable htOperFunc=new Hashtable();
				DataTable dttmp=dsOut.Tables["OperFunc"];
				if(dttmp.Rows.Count>0)
				{
					string strOperID="";
					ArrayList alFuncList=null;
					for(int i=0;i<dttmp.Rows.Count;i++)
					{
						CMSMStruct.MenuStruct menu1=new CMSMStruct.MenuStruct();
						menu1.strFuncName=dttmp.Rows[i]["vcFuncName"].ToString();
						menu1.strFuncAddress=dttmp.Rows[i]["vcFuncAddress"].ToString();
						if(strOperID==dttmp.Rows[i]["vcOperID"].ToString())
						{
							alFuncList.Add(menu1);
							if(i==dttmp.Rows.Count-1)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}
						}
						else
						{
							if(strOperID!=""&&alFuncList.Count>0)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}

							alFuncList=new ArrayList();
							alFuncList.Add(menu1);
							strOperID=dttmp.Rows[i]["vcOperID"].ToString();
							if(i==dttmp.Rows.Count-1)
							{
								htOperFunc.Add(strOperID,alFuncList);
							}
						}
					}
				}
				Application.Set("OperFunc",htOperFunc);

				Hashtable htIOTime=new Hashtable();
				dttmp=null;
				dttmp=dsOut.Tables["IOTime"];
				if(dttmp.Rows.Count>0)
				{
					string strOfficer="";
					ArrayList altmp=null;
					for(int i=0;i<dttmp.Rows.Count;i++)
					{
						CMSMStruct.SignIOTimeStruct sio1=new CommCenter.CMSMStruct.SignIOTimeStruct();
						sio1.strSIOTID=dttmp.Rows[i]["iotName"].ToString();
						sio1.strOfficer=dttmp.Rows[i]["Officer"].ToString();
						sio1.strClassName=dttmp.Rows[i]["vcClassName"].ToString();
						sio1.strClassId=dttmp.Rows[i]["vcClassId"].ToString();
						sio1.strInTime=dttmp.Rows[i]["InTime"].ToString();
						sio1.strOutTime=dttmp.Rows[i]["OutTime"].ToString();
						if(strOfficer==sio1.strOfficer)
						{
							altmp.Add(sio1);
							if(i==dttmp.Rows.Count-1)
							{
								htIOTime.Add(strOfficer,altmp);
							}
						}
						else
						{
							if(strOfficer!=""&&altmp.Count>0)
							{
								htIOTime.Add(strOfficer,altmp);
							}
							altmp=new ArrayList();
							altmp.Add(sio1);
							strOfficer=sio1.strOfficer;
							if(i==dttmp.Rows.Count-1)
							{
								htIOTime.Add(strOfficer,altmp);
							}
						}
					}
				}
				Application.Set("IOTime",htIOTime);

				Application.UnLock();
                //ynhnTransportManage.Helpers.SyncDept();
                //ynhnTransportManage.Helpers.SyncOper();
                //var uow = System.Web.Mvc.DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                var amsuow = DependencyResolver.Current.GetService<IAMSCMUow>();
                DXInfo.Business.Common businessCommon = new DXInfo.Business.Common(uow);
                Common centerCommon = new Common(uow);
                businessCommon.SyncDept(amsuow);//Uow, AmscmUow);
                centerCommon.SyncOper(amsuow);//Uow, AmscmUow);
                this.SetSuccMsgPageBydir("参数刷新成功！", "paraconf/wfmParaRefresh.aspx");
			}
			catch(Exception er)
			{
				AMSLog clog=new AMSLog();
				clog.WriteLine(er);
			}
		}

	}
}
