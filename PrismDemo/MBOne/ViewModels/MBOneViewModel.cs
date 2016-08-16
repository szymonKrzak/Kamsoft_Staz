using Common;
using Common.Events;
using Common.SQLDataBase;
using MBOne.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBOne.ViewModels
{
    public class MBOneViewModel:BindableBase
    {
        private string _addFirstName;
        public string AddFirstName
        {
            get { return _addFirstName; }
            set { SetProperty(ref _addFirstName, value); }
        }

        private string _addLastName;
        public string AddLastName
        {
            get { return _addLastName; }
            set { SetProperty(ref _addLastName, value); }
        }

        public AddNewEmployee addNewEmployee { get; set; }

        //DataBase dataBase = new DataBase();
        Connection connection = new Connection();
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<Employee> _itemsSource;
        public ObservableCollection<Employee> ItemsSource
        {
            get { return _itemsSource; }
            set { SetProperty(ref _itemsSource, value); }
        }

        public MBOneViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddCommand = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand).ObservesProperty(() => AddFirstName).ObservesProperty(() => AddLastName);
            UpdateCommand = new DelegateCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand).ObservesProperty(() => ItemsSource);
            _eventAggregator.GetEvent<AddEvent>().Subscribe(AddEventHandler);

            LoadData();
        }

        public DelegateCommand AddCommand { get; set; }
        private bool CanExecuteAddCommand()
        {
            if (!String.IsNullOrWhiteSpace(AddFirstName) && !String.IsNullOrWhiteSpace(AddLastName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ExecuteAddCommand()
        {
            AddNewEmployee addNewEmployee = new AddNewEmployee { FirstName = AddFirstName, LastName = AddLastName };
            _eventAggregator.GetEvent<Common.Events.AddEvent>().Publish(addNewEmployee);
        }

        public DelegateCommand UpdateCommand { get; set; }
        private bool CanExecuteUpdateCommand()
        {
            return true;
            
        }
        private void ExecuteUpdateCommand()
        {

            //_eventAggregator.GetEvent<UpdateEvent>().Publish(LastUpdated.ToString());
        }

        private void AddEventHandler(AddNewEmployee o)
        {

            ItemsSource = connection.AddNewWorker(o.FirstName, o.LastName);
        }

        private void LoadData()
        {
            //ItemsSource = dataBase.GetDB;
            ItemsSource = connection.GetData();
        }


    }
}
