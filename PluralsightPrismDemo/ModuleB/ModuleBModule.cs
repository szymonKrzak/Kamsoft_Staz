using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleB.ViewModels;
using ModuleB.Views;
using PluralsightPrismDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ModuleBModule(IUnityContainer container)
        {
            this._container = container;
            this._regionManager = container.Resolve<IRegionManager>();

            container.RegisterType<IModuleBViewModel, ModuleBViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleBView, ModuleBView>(new ContainerControlledLifetimeManager());
        }

        public void Initialize()
        {
            #region
            //_container.RegisterType<IModuleBViewModel, ModuleBViewModel>();
            //_container.RegisterType<IModuleBView, ModuleBView>();

            //IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            //var vm = _container.Resolve<IModuleBViewModel>();
            //region.Add(vm.View);
            #endregion
        }
    }
}
