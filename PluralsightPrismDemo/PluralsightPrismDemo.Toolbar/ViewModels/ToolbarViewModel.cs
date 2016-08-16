using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Common;
using PluralsightPrismDemo.Toolbar.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PluralsightPrismDemo.Toolbar.ViewModels
{
    public class ToolbarViewModel : ViewModelBase, IToolbarViewModel
    {
        //private readonly IRegionManager _regionManager;
        //private readonly IUnityContainer _container;

       

        public ToolbarViewModel(IToolbarView view/*, IUnityContainer container*/) : base(view)
        {
            //_container = container;
            //_regionManager = container.Resolve<IRegionManager>();

            ModuleACommand = new DelegateCommand(Execute, CanExecute);
        }

        public DelegateCommand ModuleACommand { get; set; }
        private bool CanExecute()
        {
            return true;
        }
        private void Execute()
        {
            MessageBox.Show("fewf");
        }














        //private DelegateCommand<object> _moduleACommand;
        //public DelegateCommand<object> ModuleACommand
        //{
        //    get
        //    {
        //        if (_moduleACommand == null)
        //            _moduleACommand = new DelegateCommand<object>(ExecuteModuleACommand);
        //        return _moduleACommand;
        //    }
        //}
        //public void ExecuteModuleACommand(object param)
        //{
        //    //ChangeView(_container.Resolve<IModuleAViewModel>());
        //    MessageBox.Show("dfa");
        //}

        /*private string _message = "Ready";
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }*/

    }
}
