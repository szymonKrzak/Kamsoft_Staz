using ModuleB.Views;
using PluralsightPrismDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.ViewModels
{
    public class ModuleBViewModel : ViewModelBase, IModuleBViewModel
    {
        public ModuleBViewModel(IModuleBView view) : base(view)
        {
        }
    }
}
