using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using ModuleA.ViewModels;
using ModuleA.Views;
using ModuleB.ViewModels;
using ModuleB.Views;
using PluralsightPrismDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PluralsightPrismDemo
{
    public class WindowManager
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public WindowManager(IUnityContainer container)
        {
            this._container = container;
            this._regionManager = container.Resolve<IRegionManager>();
        }


        #region Commnands
        private DelegateCommand<object> _moduleACommand;
        public DelegateCommand<object> ModuleACommand
        {
            get
            {
                if (_moduleACommand == null)
                    _moduleACommand = new DelegateCommand<object>(ExecuteModuleACommand);
                return _moduleACommand;
            }
        }
        public void ExecuteModuleACommand(object param)
        {
            ClearRegion(RegionNames.ContentRegion);
            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            var vm = _container.Resolve<IModuleAViewModel>();
            region.Add(vm.View);

        }

        private DelegateCommand<object> _moduleBCommand;
        public DelegateCommand<object> ModuleBCommand
        {
            get
            {
                if (_moduleBCommand == null)
                    _moduleBCommand = new DelegateCommand<object>(ExecuteModuleBCommand);
                return _moduleBCommand;
            }
        }
        public void ExecuteModuleBCommand(object param)
        {
            _regionManager.Regions[RegionNames.ContentRegion].Add(_container.Resolve<IModuleBViewModel>().View);
        }

        #endregion

        private void ChangeView(UserControl view)
        {
            ClearRegion(RegionNames.ContentRegion);
            _regionManager.Regions[RegionNames.ContentRegion].Add(view);
        }
        private void ClearRegion(string regionName)
        {
            foreach (var v in _regionManager.Regions[regionName].Views)
            {
                _regionManager.Regions[regionName].Remove(v);
            }
        }
    }
}
