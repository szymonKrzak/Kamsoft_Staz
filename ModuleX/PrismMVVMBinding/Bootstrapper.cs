using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Unity;
using Prism.Regions;
using ModuleOne;
using Prism.Modularity;
using Microsoft.Practices.Unity;

namespace PrismMVVMBinding
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

            shell.Show();
            rm.Regions[Regions.MenuRegion].Add(Container.Resolve<Menu>());
            return shell;
        }

        protected override void InitializeModules()
        {
            IModule module = Container.Resolve<ModuleOneStartPoint>();
            module.Initialize();
        }
    }
}
