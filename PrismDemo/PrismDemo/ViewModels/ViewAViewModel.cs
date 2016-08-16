using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismDemo.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrismDemo.ViewModels
{
    public class ViewAViewModel: BindableBase
    {
        #region Fields
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private DateTime? _lastUpdated;
        public DateTime? LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }
        #endregion
        private readonly IEventAggregator _eventAggregator;

        public ViewAViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => FirstName).ObservesProperty(() => LastName);

        }

        public DelegateCommand UpdateCommand { get; set; }
        private bool CanExecute()
        {
            if(!String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName))
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
        private void Execute()
        {
            LastUpdated = DateTime.Now;
            _eventAggregator.GetEvent<UpdateEvent>().Publish(LastUpdated.ToString());
        }
    }
}
