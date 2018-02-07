//using System;
//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using Ninject;
//using Ninject.Syntax;
//using System.Web.Http.Dependencies;

//namespace ynhnTransportManage.App_Start
//{
//    //IDependencyScope 表示依赖项范围的接口
//    public class NinjectDependencyScope : IDependencyScope
//    {
//        private IResolutionRoot resolver;

//        internal NinjectDependencyScope(IResolutionRoot resolver)
//        {
//            Contract.Assert(resolver != null);

//            this.resolver = resolver;
//        }

//        public void Dispose()
//        {
//            var disposable = resolver as IDisposable;
//            if (disposable != null)
//                disposable.Dispose();

//            resolver = null;
//        }

//        public object GetService(Type serviceType)
//        {
//            if (resolver == null)
//                throw new ObjectDisposedException("this", "This scope has already been disposed");

//            return resolver.TryGet(serviceType);
//        }

//        public IEnumerable<object> GetServices(Type serviceType)
//        {
//            if (resolver == null)
//                throw new ObjectDisposedException("this", "This scope has already been disposed");

//            return resolver.GetAll(serviceType);
//        }
//    }
//}