using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using Ninject;
using Moq;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBinds();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        
        private void AddBinds()
        {
            // Mock implementation of the IProductRepositry Interface
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> { 
                new Product {Name = "Football", Price = 25},
                new Product {Name = "Surf board", Price = 179},
                new Product {Name = "Sunning shoues", Price = 95}
            }.AsQueryable());
            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}