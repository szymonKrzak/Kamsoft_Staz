using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPrism.Data
{
    public class ChangeContextData<T>
    {
        public ChangeContextData(object sender, T newContext)
        {
            Sender = sender;
            Context = newContext;
        }

        public object Sender { get; private set; }
        public T Context { get; private set; }
    }
}
