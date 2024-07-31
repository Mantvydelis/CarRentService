using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NuomosUzsakymas
    {
        public Klientas Uzsakovas { get; set; }
        public Automobilis NuomuojamasAuto { get; set; }
        public DateTime NuomosPradzia { get; set; }
        public int DienuKiekis { get; set; }
      
    }
}
