using Moq;
using Ninject;
using PhoneStore.Domain.Abstract;
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
            Mock<IPhoneRepository> mock = new Mock<IPhoneRepository>();
            mock.Setup(m => m.Phones).Returns(new List<Phone>
            {
                new Phone { Name = "IPhone X", Price = 9999},
                new Phone { Name = "Samsung Galaxy A10", Price = 3199},
                new Phone { Name = "Nokia Lumia", Price = 2549.5M}
            });
            kernel.Bind<IPhoneRepository>().ToConstant(mock.Object);
        }
    }
}