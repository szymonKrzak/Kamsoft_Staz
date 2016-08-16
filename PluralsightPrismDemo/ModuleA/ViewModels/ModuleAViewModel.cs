using Microsoft.Practices.Prism.Commands;
using ModuleA.Views;
using PluralsightPrismDemo.Business;
using PluralsightPrismDemo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA.ViewModels
{
    public class ModuleAViewModel : ViewModelBase, IModuleAViewModel
    {
        public ModuleAViewModel(IModuleAView view) : base(view)
        {
            CreatePerson();
            SaveCommand = new DelegateCommand(ExecuteSave, CanExecuteSave);
        }

        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                _person.PropertyChanged += _person_PropertyChanged;
                OnPropertyChanged("Person");
            }
        }

        private void _person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        public void CreatePerson()
        {
            Person = new Person()
            {
                FirstName = "A",
                LastName = "a",
                Age = 0
            };
        }


        public DelegateCommand SaveCommand { get; set; }
        private bool CanExecuteSave()
        {
            if (!string.IsNullOrWhiteSpace(Person.FirstName) && !string.IsNullOrWhiteSpace(Person.LastName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ExecuteSave()
        {
            Person.LastUpdated = DateTime.Now;
        }
    }
}
