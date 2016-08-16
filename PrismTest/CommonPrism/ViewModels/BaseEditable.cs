using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPrism.Enities;
using Microsoft.Practices.Prism.Events;

namespace CommonPrism.ViewModels
{
    public class BaseEditable
    {
        private Contract entity;
        private IEventAggregator eventAggregator;

        public BaseEditable(Contract entity, IEventAggregator eventAggregator)
        {
            this.entity = entity;
            this.eventAggregator = eventAggregator;
        }
    }
}
