using Microsoft.Practices.Unity;
using ModuleOne.Views;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleOne
{
    public class ModuleOneStartPoint : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ModuleOneStartPoint(IUnityContainer container)
        {
            _regionManager = container.Resolve<IRegionManager>();
            _container = container;

            container.RegisterType<DataGridView>(new ContainerControlledLifetimeManager());
        }

        public void Initialize()
        {
        }
    }
}
