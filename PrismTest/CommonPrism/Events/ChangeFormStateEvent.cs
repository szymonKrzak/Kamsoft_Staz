using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using CommonPrism.Data;

namespace CommonPrism.Events
{
    public class ChangeFormStateEvent : CompositePresentationEvent<ChangeFormStateData>
    {
    }
}
