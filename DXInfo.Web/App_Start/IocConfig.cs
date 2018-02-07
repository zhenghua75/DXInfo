using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using DXInfo.Data.Contracts;
using DXInfo.Data;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace DXInfo.Web.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public void Dispose()
        {
            var disposable = kernel as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            kernel = null;
        }

        public object GetService(Type serviceType)
        {
            if (kernel == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (kernel == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return kernel.GetAll(serviceType);
        }
    }
    public class IocConfig
    {
        public static void RegisterIoc()
        {
            try
            {
                var kernel = new StandardKernel();
                //kernel.Bind<IAMSCMUow>().To<AMSCMUow>().InScope(c => System.Web.HttpContext.Current);
                kernel.Bind<IFairiesMemberManageUow>().To<FairiesMemberManageUow>().InScope(c => System.Web.HttpContext.Current);
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