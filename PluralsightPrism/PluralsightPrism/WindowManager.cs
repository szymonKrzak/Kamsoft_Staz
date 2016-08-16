using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluralsightPrism
{
    public class WindowManager: BindableBase
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
    }
}
