using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using CommonPrism.Data;
using CommonPrism.Enities;
using CommonPrism.Events;
using CommonPrism.Enums;
using CommonPrism.DataModel;

namespace Module1.ViewModels
{
    public class TextBoxVM: NotImplementedException
    {
        private readonly IEventAggregator _eventAggregator;
        private Contract _contr;
        public Contract Contr
        {
            get { return _contr; }
            set
            {
                _contr = value;
            }
        }

        public TextBoxVM(IUnityContainer container)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<ChangeFormStateEvent>().Subscribe(OnChangeViewModeEventHandler);
            _eventAggregator.GetEvent<SetChangeContextEvent<ContractDataContext>>().Subscribe(OnChangeContextDataHandler);

            Contr = new Contract { Id = 1, Name = "Szymon" };
            SelectedItem = new ContractEditable(Contr, _eventAggregator);
            SelectedItem.StarListeningForChanges();
            SelectedItem.SetupValidations();
            SelectedItem.SetViewMode(ViewMode.ReadOnly, ViewMode.Edit);
        }

        private ContractEditable _selectedItem;
        public ContractEditable SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        protected void OnChangeViewModeEventHandler(ChangeFormStateData data)
        {
            FormState = data.NewMode;
            RefreshView();
        }

        protected void OnChangeContextDataHandler(ChangeContextData<ContractDataContext> data)
        {
            ChangeContext = data.Context;
            RefreshView();
        }
    }
}
