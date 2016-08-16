using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleB.ViewModels;
using ModuleB.Views;
using PokemonFarm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleBModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            _container.RegisterType<IModuleBViewModel, ModuleBViewModel>();
            _container.RegisterType<IModuleBView, ModuleBView>();
        }
    }
}
