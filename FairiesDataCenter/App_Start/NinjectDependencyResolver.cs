using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace ynhnTransportManage.App_Start
{
    //IDependencyResolver 表示依赖关系注入容器
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
}
