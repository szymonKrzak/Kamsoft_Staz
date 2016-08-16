using MenuRegion.ViewModels;
using MenuRegion.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PokemonFarm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuRegion
{
    public class MenuRegionModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public MenuRegionModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IMenuRegionViewModel, MenuRegionViewModel>();
            _container.RegisterType<IMenuRegionView, MenuRegionView>();

            var vm = _container.Resolve<IMenuRegionViewModel>();
            IRegion region = _regionManager.Regions[RegionNames.MenuRegion];
            
            region.Add(vm.View);
        }
    }
}
