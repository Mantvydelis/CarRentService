using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class Elektromobilis : Automobilis
    {
        public int BaterijosTalpa { get; set; }
        public int KrovimoLaikas { get; set; }
        public Elektromobilis(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
            : base(id, marke, modelis, nuomosKaina)
        {
            BaterijosTalpa = baterijosTalpa;
            KrovimoLaikas = krovimoLaikas;
        }
        public Elektromobilis(string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
            : base(marke, modelis, nuomosKaina)
        {
            BaterijosTalpa = baterijosTalpa;
            KrovimoLaikas = krovimoLaikas;
        }

        public Elektromobilis(){}
        public override string ToString()
        {
            return $"{Id} {Marke} {Modelis} {NuomosKaina} {BaterijosTalpa}kwh {KrovimoLaikas} val.";
        }
    }
}
