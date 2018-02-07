using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DXInfo.Web.App_Start;
using System.Net;
using System.Data;
using System.Collections;
using DXInfo.Web.Models;
using DXInfo.Data.Contracts;

namespace DXInfo.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitData();
            DXInfo.Web.Models.Helper.logonCss();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            IocConfig.RegisterIoc();
            MapperConfig.RegisterMapper();

            
            //if (Helper.IsAMSApp())
            //{
            //    AMSAppProcess();
            //}
        }

        private void InitData()
        {
            if (DXInfo.Web.Models.Helper.JudgeSqlUpdate())
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FairiesMemberManage"].ConnectionString;
                DXInfo.Sync.Sync sync = new DXInfo.Sync.Sync(connectionString);
                //using (sync.ServerConn)
                //{
                bool bAMSApp = DXInfo.Web.Models.Helper.IsAMSApp();
                sync.ServerConn.Open();
                DXInfo.Web.Models.Helper.SyncStruct(sync.ServerConn,bAMSApp);
                sync.ServerConn.Close();
                //}
                if (!bAMSApp)
                {
                    sync.ProvisionServer();
                    sync.CheckProvisionServer();
                }
                DXInfo.Web.Models.Helper.UpdateSqlVersionConfig();
            }
        }

        //#region AMSApp处理
        //private void AMSAppProcess()
        //{
        //    var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    Common centerCommon = new Common(Uow);

        //    #region 刷新AMSApp数据至FairiesMemberManage
        //    if (Helper.IsAMSAppRefresh())
        //    {
        //        centerCommon.SyncDept();
        //        centerCommon.SyncOper();
        //        centerCommon.SyncGT();
        //        centerCommon.SyncGoods();
        //    }
        //    #endregion

        //}
        //#endregion
    }
}