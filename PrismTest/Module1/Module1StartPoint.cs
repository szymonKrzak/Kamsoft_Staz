using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Module1.Views;


namespace Module1
{
    public class Module1StartPoint : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public Module1StartPoint(IUnityContainer container)
        {
            _regionManager = container.Resolve<IRegionManager>();
            _container = container;


            container.RegisterType<TextBoxView>(new ContainerControlledLifetimeManager());
        }

        public void Initialize()
        {
           
        }
    }
}
