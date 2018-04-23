using System;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Ninject;
using DXInfo.Data;
using DXInfo.Data.Contracts;
using Ninject.Extensions.Wcf;
using Ninject.Web.Common;

using Ninject.Web.Common.WebHost;

namespace DXInfo.WcfRestService
{

    public class Global : NinjectHttpApplication
    {
        //void Application_Start(object sender, EventArgs e)
        //{
        //    RegisterRoutes();
        //}

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            RegisterRoutes();
        }
        private void RegisterRoutes()
        {
            // Edit the base address of Service1 by replacing the "Service1" string below
            //RouteTable.Routes.Add(new ServiceRoute("Service1", new WebServiceHostFactory(), typeof(Service1)));
            RouteTable.Routes.Add(new ServiceRoute("Service1", new NinjectServiceHostFactory(), typeof(Service1)));
        }

        protected override IKernel CreateKernel()
        {
            var kernal = new StandardKernel();
            //kernal.Bind<RepositoryFactories>().To<RepositoryFactories>().InRequestScope();
            //kernal.Bind<IRepositoryProvider>().To<RepositoryProvider>();
            kernal.Bind<IFairiesMemberManageUow>().To<FairiesMemberManageUow>();
            return kernal;
            //return new StandardKernel(new WCFNinjectModule());
        }
    }
}
