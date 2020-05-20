using Moq;
using Ninject;
using PhoneStore.Domain.Abstract;
using PhoneStore.Domain.Concrete;
using PhoneStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhoneStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernelParam;
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IPhoneRepository>().To<EFPhoneRepository>();
        }
    }
}