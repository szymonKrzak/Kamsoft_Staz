using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPrism.Data;

namespace CommonPrism.Events
{
    public class SetChangeContextEvent<T>: CompositePresentationEvent<ChangeContextData<T>>
    {
    }
}
