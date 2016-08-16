using Prism.Unity;
using PrismDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using MBOne.Views;
using Prism.Regions;
using MBOne;
using Prism.Modularity;

namespace PrismDemo
{
    public class Bootstrapper: UnityBootstrapper
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<WindowManager>(new ContainerControlledLifetimeManager());
        }

        protected override DependencyObject CreateShell()
        {
            Shell shell = Container.Resolve<Shell>();

            var rm = Container.Resolve<IRegionManager>();
            RegionManager.SetRegionManager(shell, rm);

            shell.Show();
            rm.Regions[Regions.MenuRegion].Add(Container.Resolve<Menu>());
            return shell;
        }

        protected override void InitializeModules()
        {
            IModule module = Container.Resolve<ViewAStartPoint>();
            //IModule mod = Container.Resolve<MBOneStartPoint>();
            //mod.Initialize();
            module.Initialize();
        }

    }
}
