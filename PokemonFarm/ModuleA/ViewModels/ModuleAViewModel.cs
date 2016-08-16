using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonFarm.Common;
using ModuleA.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using PokemonFarm.Common.Events;

namespace ModuleA.ViewModels
{
    public class ModuleAViewModel : ViewModelBase, IModuleAViewModel
    {
        IEventAggregator _eventAggregator;

        public ModuleAViewModel(IModuleAView view, IEventAggregator eventAggregator) : base(view)
        {
            this._eventAggregator = eventAggregator;

            Person = new Person();
            Person.CreatePerson();
        }

        private Person _person;
        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged("Person");
            }
        }

        private DelegateCommand<object> _updateCommand;
        public DelegateCommand<object> UpdateCommand
        {
            get
            {
                if(_updateCommand == null)
                {
                    _updateCommand = new DelegateCommand<object>(ExecuteUpdateCommand);
                }
                return _updateCommand;
            }
        }

        private void ExecuteUpdateCommand(object obj)
        {
            _eventAggregator.GetEvent<UpdateEvent>().Publish(Person.FirstName);
        }
    }
}
