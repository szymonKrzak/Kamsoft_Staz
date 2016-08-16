using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBOne;
using Prism.Regions;
using Prism.Commands;
using Microsoft.Practices.Unity;
using System.Windows.Controls;
using PrismDemo.Views;
using System.Windows;

namespace PrismDemo
{
    public class WindowManager:BindableBase
    {
        #region Prism 6
        /*private readonly IRegionManager _regionManager;
        public DelegateCommand<string> NavigationCommand { get; set; }

        public WindowManager(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigationCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("MainRegion", uri);
        }*/
        #endregion

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
            ClearRegion(Regions.MainRegion);
            _regionManager.Regions[Regions.MainRegion].Add(view);
        }

        private void ClearRegion(string regionName)
        {
            foreach (var v in _regionManager.Regions[regionName].Views)
            {
                _regionManager.Regions[regionName].Remove(v);
            }
        }

        #region Command ViewA
        private DelegateCommand<object> _viewACommand;
        public DelegateCommand<object> ViewACommand
        {
            get
            {
                if(_viewACommand == null)
                {
                    _viewACommand = new DelegateCommand<object>(ExecuteViewA);
                }
                return _viewACommand;
            }
           
        }
        private void ExecuteViewA(object param)
        {
            ChangeView(_container.Resolve<ViewA>());
        }

        #endregion
        #region Command ViewB
        private DelegateCommand<object> _viewBCommand;
        public DelegateCommand<object> ViewBCommand
        {
            get
            {
                if (_viewBCommand == null)
                {
                    _viewBCommand = new DelegateCommand<object>(ExecuteViewB);
                }
                return _viewBCommand;
            }

        }
        private void ExecuteViewB(object param)
        {
            ChangeView(_container.Resolve<ViewB>());
        }
        #endregion
        #region Command ViewModuleBusiness
        private DelegateCommand<object> _viewMBCommand;
        public DelegateCommand<object> ViewMBCommand
        {
            get
            {
                if (_viewMBCommand == null)
                {
                    _viewMBCommand = new DelegateCommand<object>(ExecuteMB);
                }
                return _viewMBCommand;
            }

        }
        private void ExecuteMB(object param)
        {
            ChangeView(_container.Resolve<MBOne.Views.ViewMBOne>());
        }
        #endregion
    }
}
