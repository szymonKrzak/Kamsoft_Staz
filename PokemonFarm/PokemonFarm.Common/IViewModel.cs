using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonFarm.Common
{
    public interface IViewModel
    {
        IView View { get; set; }
    }
}
