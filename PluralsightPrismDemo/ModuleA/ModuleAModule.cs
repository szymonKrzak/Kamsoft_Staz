using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleA.ViewModels;
using ModuleA.Views;
using PluralsightPrismDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModuleA
{
    public class ModuleAModule: IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ModuleAModule(IUnityContainer container)
        {
            this._container = container;
            this._regionManager = container.Resolve<IRegionManager>();

            container.RegisterType<IModuleAViewModel, ModuleAViewModel>(new ContainerControlledLifetimeManager());
            container.RegisterType<IModuleAView, ModuleAView>(new ContainerControlledLifetimeManager());
        }

        public void Initialize()
        {
            #region
            //_container.RegisterType<IModuleAViewModel, ModuleAViewModel>();
            //_container.RegisterType<IModuleAView, ModuleAView>();

            //IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            //var vm = _container.Resolve<IModuleAViewModel>();
            //region.Add(vm.View);
            #endregion
        }
    }
}
