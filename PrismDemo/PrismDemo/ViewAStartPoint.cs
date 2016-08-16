using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using PrismDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo
{
    public class ViewAStartPoint: IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ViewAStartPoint(IUnityContainer container)
        {
            _regionManager = container.Resolve<IRegionManager>();
            _container = container;

            container.RegisterType<ViewA>(new ContainerControlledLifetimeManager());
            container.RegisterType<ViewB>(new ContainerControlledLifetimeManager());
        }

        public void Initialize()
        {
        }
    }
}
