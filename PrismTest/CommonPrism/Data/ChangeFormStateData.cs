using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPrism.Enums;

namespace CommonPrism.Data
{
    public class ChangeFormStateData
    {
        public ChangeFormStateData(object sender, ViewMode previousMode, ViewMode newMode)
        {
            Sender = sender;
            PreviousMode = previousMode;
            NewMode = newMode;
        }

        public object Sender { get; private set; }
        public ViewMode PreviousMode { get; private set; }
        public ViewMode NewMode { get; private set; }
    }
}
