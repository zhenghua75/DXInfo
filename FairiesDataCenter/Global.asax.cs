using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Routing;
using System.Diagnostics;
using AutoMapper;
using System.Collections;
using DataAccess;
using System.Data;
using CommCenter;
using ynhnTransportManage.App_Start;
using DXInfo.Data.Contracts;
using Ninject;

//using DXInfo.Data;
using System.Reflection;
using DXInfo.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Timers;
using DXInfo.Business;

namespace ynhnTransportManage
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    //public class NinjectControllerFactory : DefaultControllerFactory
    //{
    //    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
    //    {
    //        IController ic= DependencyResolver.Current.GetService(controllerType) as IController;
    //        //DependencyResolver.Current.GetService<controllerType>();
    //        return ic;
    //    }
    //}
    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer timer = null;
        protected void Application_Start()
        {
            #region 设置
            AreaRegistration.RegisterAllAreas();  
            IocConfig.RegisterIoc();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            #endregion

            var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            DXInfo.Business.Common businessCommon = new DXInfo.Business.Common(Uow);
            Common centerCommon = new Common(Uow);
            if (!centerCommon.IsAMSApp())
            {
                InitData();
            }
            
            #region 处理AMSApp
            if (centerCommon.IsAMSApp())
            {
                AMSAppProcess(businessCommon,centerCommon);
            }
            #endregion

            #region 映射对象
            Mapper.CreateMap<DXInfo.Models.ScrapVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.ScrapVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.TransVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.TransVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.CheckVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.CheckVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.InvLocator>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.InvLocator>();

            Mapper.CreateMap<DXInfo.Models.RdRecord, DXInfo.Models.InvLocator>();
            Mapper.CreateMap<DXInfo.Models.RdRecords, DXInfo.Models.InvLocator>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.RdRecord, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.RdRecord,ynhnTransportManage.Models.StockManage.RdRecord>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.RdRecord, DXInfo.Models.MixVouch>();
            Mapper.CreateMap<DXInfo.Models.MixVouch, ynhnTransportManage.Models.StockManage.RdRecord>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.MixVouch, DXInfo.Models.MixVouch>();
            Mapper.CreateMap<DXInfo.Models.MixVouch, ynhnTransportManage.Models.StockManage.MixVouch>();

            

            Mapper.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.TransVouch>();
            Mapper.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.TransVouchs>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.TransVouch, DXInfo.Models.TransVouch>();
            Mapper.CreateMap<DXInfo.Models.TransVouch, ynhnTransportManage.Models.StockManage.TransVouch>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.AdjustLocatorVouch, DXInfo.Models.AdjustLocatorVouch>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, ynhnTransportManage.Models.StockManage.AdjustLocatorVouch>();

            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.CheckVouch, DXInfo.Models.CheckVouch>();
            Mapper.CreateMap<DXInfo.Models.CheckVouch, ynhnTransportManage.Models.StockManage.CheckVouch>();

            Mapper.CreateMap<DXInfo.Models.CurrentInvLocator, DXInfo.Models.CheckVouchs>();
            Mapper.CreateMap<DXInfo.Models.CurrentStock, DXInfo.Models.CheckVouchs>();
            Mapper.CreateMap<ynhnTransportManage.Models.StockManage.ScrapVouch, DXInfo.Models.ScrapVouch>();
            Mapper.CreateMap<DXInfo.Models.ScrapVouch, ynhnTransportManage.Models.StockManage.ScrapVouch>();
            Mapper.CreateMap<DXInfo.Models.Receipts, DXInfo.Models.ReceiptHis>();
            #endregion

            #region 自动同步零售数据入库存

            bool isSyncSaleStock = businessCommon.IsSyncSaleStock();
            if (isSyncSaleStock)
            {
                timer = new Timer();
                timer.Enabled = true;
                timer.Interval = 3 * 60 * 1000;
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Start();
            }
            #endregion
        }
        object lockObject = new object();
        bool IsRun = false;
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (lockObject)
            {
                if (!IsRun)
                {
                    IsRun = true;
                    try
                    {
                        var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                        StockManageFacade smf = new StockManageFacade(Uow);
                        smf.BatchRetailStock();
                    }
                    catch (Exception ex)
                    {
                        ExceptionPolicy.HandleException(ex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                    }
                    finally
                    {
                        IsRun = false;
                    }
                }
            }
        }
        private void InitData()
        {
            if (Common.JudgeSqlUpdate())
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FairiesMemberManage"].ConnectionString;
                DXInfo.Sync.Sync sync = new DXInfo.Sync.Sync(connectionString);
                //using (sync.ServerConn)
                //{
                    sync.ServerConn.Open();
                    Common.SyncStruct(sync.ServerConn);
                    sync.ServerConn.Close();
                //}
                sync.ProvisionServer();
                sync.CheckProvisionServer();
                Common.UpdateSqlVersionConfig();
            }
        }

        #region 错误处理
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            //if (ex.Message == "文件不存在。")
            //{
            //    throw new Exception(string.Format("{0} {1}", ex.Message, HttpContext.Current.Request.Url.ToString()), ex);
            //}
            EventLog.WriteEntry("ynhnTransportManage",
              "MESSAGE: " + ex.Message +
              "\nSOURCE: " + ex.Source +
              "\nFORM: " + Request.Form.ToString() +
              "\nQUERYSTRING: " + Request.QueryString.ToString() +
              "\nTARGETSITE: " + ex.TargetSite +
              "\nSTACKTRACE: " + ex.StackTrace,
              EventLogEntryType.Error);
            ExceptionPolicy.HandleException(ex, DXInfo.Models.EnumHelper.ExceptionPolicy);
            ExceptionPolicy.HandleException(new Exception(string.Format("{0} {1}", ex.Message, HttpContext.Current.Request.Url.ToString()), ex), DXInfo.Models.EnumHelper.ExceptionPolicy);
        }
        #endregion

        #region AMSApp处理
        private void AMSAppProcess(DXInfo.Business.Common businessCommon, Common centerCommon)
        {
            #region 连接串缓存
            Hashtable htapp = new Hashtable();
            htapp.Add("cons", System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            Application["appconf"] = htapp;
            #endregion

            #region 导入基本数据
            ParaInit(Application);
            AMSApp.zhenghua.Business.Helper.LoadInitCode(Application);
            #endregion

            #region 刷新AMSApp数据至FairiesMemberManage
            if (centerCommon.IsAMSAppRefresh())
            {
                var AmscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();
                businessCommon.SyncDept(AmscmUow);
                centerCommon.SyncOper(AmscmUow);
                centerCommon.SyncGT(AmscmUow);
                businessCommon.SyncGoods(AmscmUow);
            }
            #endregion

        }
        public void ParaInit(System.Web.HttpApplicationState app)
        {
            try
            {
                DataSet dsIn = new DataSet();
                InitCode inc = new InitCode();
                Hashtable htapp = (Hashtable)Application["appconf"];
                string strcons = (string)htapp["cons"];
                DataSet dsOut = inc.LoadCodeTable(strcons);

                //错误返回表

                //返回结果存放到Application
                app["tbCommCode"] = dsOut.Tables["tbCommCode"];
                app["AssAT"] = dsOut.Tables["AssAT"];
                app["AT1"] = dsOut.Tables["AT1"];
                app["AllMD"] = dsOut.Tables["AllMD"];
                app["MAC"] = dsOut.Tables["MAC"];
                app["OperFunc"] = dsOut.Tables["OperFunc"];
                app["IOTime"] = dsOut.Tables["IOTime"];
                app["Goods"] = dsOut.Tables["Goods"];
                app["PClass"] = dsOut.Tables["PClass"];
                app["AllMaterial"] = dsOut.Tables["AllMaterial"];
                app["Provider"] = dsOut.Tables["Provider"];
                app["NewDept"] = dsOut.Tables["NewDept"];
                app["tbNameCodeToStorage"] = dsOut.Tables["tbNameCodeToStorage"];
                app["tbFormula"] = dsOut.Tables["tbFormula"];
                app["DeptMapInfo"] = dsOut.Tables["DeptMapInfo"];
                app["Warehouse"] = dsOut.Tables["Warehouse"];
                app["ComputationGroup"] = dsOut.Tables["ComputationGroup"];
                app["ComputationUnit"] = dsOut.Tables["ComputationUnit"];

                Hashtable htOperFunc = new Hashtable();
                DataTable dttmp = dsOut.Tables["OperFunc"];
                if (dttmp.Rows.Count > 0)
                {
                    string strOperID = "";
                    ArrayList alFuncList = null;
                    for (int i = 0; i < dttmp.Rows.Count; i++)
                    {
                        CMSMStruct.MenuStruct menu1 = new CMSMStruct.MenuStruct();
                        menu1.strFuncName = dttmp.Rows[i]["vcFuncName"].ToString();
                        menu1.strFuncAddress = dttmp.Rows[i]["vcFuncAddress"].ToString();
                        if (strOperID == dttmp.Rows[i]["vcOperID"].ToString())
                        {
                            alFuncList.Add(menu1);
                            if (i == dttmp.Rows.Count - 1)
                            {
                                htOperFunc.Add(strOperID, alFuncList);
                            }
                        }
                        else
                        {
                            if (strOperID != "" && alFuncList.Count > 0)
                            {
                                htOperFunc.Add(strOperID, alFuncList);
                            }

                            alFuncList = new ArrayList();
                            alFuncList.Add(menu1);
                            strOperID = dttmp.Rows[i]["vcOperID"].ToString();
                            if (i == dttmp.Rows.Count - 1)
                            {
                                htOperFunc.Add(strOperID, alFuncList);
                            }
                        }
                    }
                }
                app["OperFunc"] = htOperFunc;

                Hashtable htIOTime = new Hashtable();
                dttmp = null;
                dttmp = dsOut.Tables["IOTime"];
                if (dttmp.Rows.Count > 0)
                {
                    string strOfficer = "";
                    ArrayList altmp = null;
                    for (int i = 0; i < dttmp.Rows.Count; i++)
                    {
                        CMSMStruct.SignIOTimeStruct sio1 = new CommCenter.CMSMStruct.SignIOTimeStruct();
                        sio1.strSIOTID = dttmp.Rows[i]["iotName"].ToString();
                        sio1.strOfficer = dttmp.Rows[i]["Officer"].ToString();
                        sio1.strClassName = dttmp.Rows[i]["vcClassName"].ToString();
                        sio1.strClassId = dttmp.Rows[i]["vcClassId"].ToString();
                        sio1.strInTime = dttmp.Rows[i]["InTime"].ToString();
                        sio1.strOutTime = dttmp.Rows[i]["OutTime"].ToString();
                        if (strOfficer == sio1.strOfficer)
                        {
                            altmp.Add(sio1);
                            if (i == dttmp.Rows.Count - 1)
                            {
                                htIOTime.Add(strOfficer, altmp);
                            }
                        }
                        else
                        {
                            if (strOfficer != "" && altmp.Count > 0)
                            {
                                htIOTime.Add(strOfficer, altmp);
                            }

                            altmp = new ArrayList();
                            altmp.Add(sio1);
                            strOfficer = sio1.strOfficer;
                            if (i == dttmp.Rows.Count - 1)
                            {
                                htIOTime.Add(strOfficer, altmp);
                            }
                        }
                    }
                }
                app["IOTime"] = htIOTime;

                app.UnLock();
            }
            catch (Exception e)
            {
                AMSLog clog = new AMSLog();
                clog.WriteLine(e);
            }

        }
        #endregion

        protected void Application_End(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
    }
}