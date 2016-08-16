using MenuRegion.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleA.ViewModels;
using ModuleA.Views;
using ModuleB.ViewModels;
using ModuleB.Views;
using PokemonFarm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MenuRegion.ViewModels
{
    public class MenuRegionViewModel : ViewModelBase, IMenuRegionViewModel
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public MenuRegionViewModel(IMenuRegionView view, IUnityContainer container, IRegionManager regionManager) : base(view)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        #region Commands
        private DelegateCommand<object> _moduleACommand;
        public DelegateCommand<object> ModuleACommand
        {
            get
            {
                if(_moduleACommand == null)
                {
                    _moduleACommand = new DelegateCommand<object>(ExecuteModuleACommand);
                }
                return _moduleACommand;
            }
        }
        private void ExecuteModuleACommand(object obj)
        {
            ClearRegion(RegionNames.MainRegion);
            var vm = _container.Resolve<IModuleAViewModel>();
            _regionManager.Regions[RegionNames.MainRegion].Add(vm.View);
        }

        private DelegateCommand<object> _moduleBCommand;
        public DelegateCommand<object> ModuleBCommand
        {
            get
            {
                if (_moduleBCommand == null)
                {
                    _moduleBCommand = new DelegateCommand<object>(ExecuteModuleBCommand);
                }
                return _moduleBCommand;
            }
        }
        private void ExecuteModuleBCommand(object obj)
        {
            ClearRegion(RegionNames.MainRegion);
            var vm = _container.Resolve<IModuleBViewModel>();
            _regionManager.Regions[RegionNames.MainRegion].Add(vm.View);
        }

        private void ClearRegion(string regionName)
        {
            foreach (var v in _regionManager.Regions[regionName].Views)
            {
                _regionManager.Regions[regionName].Remove(v);
            }
        }
        #endregion
    }
}
