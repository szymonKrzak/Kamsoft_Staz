using Microsoft.Practices.Prism.Events;
using ModuleB.Views;
using PokemonFarm.Common;
using PokemonFarm.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleB.ViewModels
{
    public class ModuleBViewModel : ViewModelBase, IModuleBViewModel
    {
        IEventAggregator _eventAggregator;

        public ModuleBViewModel(IModuleBView view, IEventAggregator eventAggregator) : base(view)
        {
            Person = new Person();
            Person.CreatePerson();

            this._eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateEvent>().Subscribe(PersonUpdate);
        }

        private void PersonUpdate(string firstName)
        {
            Person.FirstName = firstName;
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

    }
}
