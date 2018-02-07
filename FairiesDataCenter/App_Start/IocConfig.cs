using DXInfo.Data;
using DXInfo.Data.Contracts;
using Ninject;
using System.Web.Mvc;
using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Entity;
using System.Collections.Generic;

namespace ynhnTransportManage.App_Start
{
    public class IocConfig
    {
        public static void RegisterIoc()
        {
            try
            {
                var kernel = new StandardKernel(); // Ninject IoC

                // These registrations are "per instance request".
                // See http://blog.bobcravens.com/2010/03/ninject-life-cycle-management-or-scoping/
                //.InScope(c => System.Web.HttpContext.Current);
                //kernel.Bind<RepositoryFactories>().ToSelf().InScope(c => System.Web.HttpContext.Current);

                //kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();//.InScope(c => System.Web.HttpContext.Current);
                //if (Common.IsAMSApp())
                //{
                //IDictionary<Type, Func<DbContext, object>> factories = new Dictionary<Type,Func<DbContext,object>>{
                //    {typeof(IAMSCMUow),dbContext=>new AMSCMDbContext()}
                //};
                //kernel.Bind<IAMSCMUow>().To();// new AMSCMUow(new RepositoryFactories(factories));
                //RepositoryProvider rp = new RepositoryProvider();
                //rp.DbContext = new AMSCMDbContext();
                kernel.Bind<IAMSCMUow>().To<AMSCMUow>().InScope(c => System.Web.HttpContext.Current);//.WithConstructorArgument("repositoryProvider", rp);
                //}
                //RepositoryProvider rp2 = new RepositoryProvider();
                //rp2.DbContext = new FairiesMemberManageDbContext();
                kernel.Bind<IFairiesMemberManageUow>().To<FairiesMemberManageUow>().InScope(c => System.Web.HttpContext.Current);//.WithConstructorArgument("repositoryProvider", rp2);
                
                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel)
                DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, DXInfo.Models.EnumHelper.ExceptionPolicy);
                throw ex;
            }
        }
    }
}