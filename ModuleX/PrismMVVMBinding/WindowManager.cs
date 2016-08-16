using Microsoft.Practices.Unity;
using ModuleOne.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PrismMVVMBinding
{
    public class WindowManager:BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public WindowManager(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = container.Resolve<IEventAggregator>();
            _regionManager = container.Resolve<IRegionManager>();
        }

        private void ChangeView(UserControl view)
        {
            //ClearRegion(Regions.MainRegion);
            _regionManager.Regions[Regions.MainRegion].Add(view);
        }

        private DelegateCommand<object> _moduleBusinessCommand;
        public DelegateCommand<object> ModuleBusinessCommand
        {
            get
            {
                if (_moduleBusinessCommand == null)
                {
                    _moduleBusinessCommand = new DelegateCommand<object>(ExecuteViewModuleBusiness);
                }
                return _moduleBusinessCommand;
            }

        }
        private void ExecuteViewModuleBusiness(object param)
        {
            ChangeView(_container.Resolve<DataGridView>());
        }
    }
}
