using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using ModuleA;
using ModuleB;
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
    public class Bootstrapper : UnityBootstrapper
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
            Container.Resolve<Menu>();

            shell.Show();
            rm.Regions[RegionNames.ToolbarRegion].Add(Container.Resolve<Menu>());

            return shell;
        }

        protected override void InitializeShell()
        {
            IModule moduleA = Container.Resolve<ModuleAModule>();
            moduleA.Initialize();
        }
    }
}
