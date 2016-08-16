using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Common;
using PluralsightPrismDemo.Toolbar.ViewModels;
using PluralsightPrismDemo.Toolbar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPrismDemo.Toolbar
{
    public class ToolbarModule:IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ToolbarModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IToolbarViewModel, ToolbarViewModel>();
            _container.RegisterType<IToolbarView, ToolbarView>();
            
            var vm = _container.Resolve<IToolbarViewModel>();
            IRegion region = _regionManager.Regions[RegionNames.ToolbarRegion];
            region.Add(vm.View);
        }
    }
}
