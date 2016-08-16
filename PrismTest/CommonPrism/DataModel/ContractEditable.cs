using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPrism.ViewModels;
using CommonPrism.Enities;
using Microsoft.Practices.Prism.Events;

namespace CommonPrism.DataModel
{
    public class ContractEditable: BaseEditable
    {
        public ContractEditable(Contract entity, IEventAggregator eventAggregator): base(entity, eventAggregator)
        {

        }
    }
}
