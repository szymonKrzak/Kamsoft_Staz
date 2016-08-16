using Pluralsight_MVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pluralsight_MVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    { 
        private Person _modelPerson;
        public Person ModelPerson
        {
            get { return _modelPerson; }
            set
            {
                _modelPerson = value;
                OnPropertyChanged("ModelPerson");

            }
        }

        private ICommand _savePersonCommand;
        public ICommand SavePersonCommand
        {
            get { return _savePersonCommand; }
            set
            {
                _savePersonCommand = value;
                OnPropertyChanged("SavePersonCommand");
            }
        }

        public MainViewModel()
        {
            InitializeCommand();
            LoadPerson();
        }

        private void InitializeCommand()
        {
            SavePersonCommand = new SavePersonCommand(UpdatePerson);
        }

        private void UpdatePerson()
        {
            ModelPerson.UpdatedDate = DateTime.Now;
        }

        private void LoadPerson()
        {
            ModelPerson = new Person()
            {
                FirstName = "X",
                LastName = "Y"
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class SavePersonCommand : ICommand
    {
        Action _executeMethod;

        public SavePersonCommand(Action updatePerson)
        {
            _executeMethod = updatePerson;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }
    }
}
