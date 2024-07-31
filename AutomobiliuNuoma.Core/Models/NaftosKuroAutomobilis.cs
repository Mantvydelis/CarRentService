using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NaftosKuroAutomobilis : Automobilis
    {
        public double DegaluSanaudos { get; set; }
        public NaftosKuroAutomobilis(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
           : base(id, marke, modelis, nuomosKaina)
        {
            DegaluSanaudos = degaluSanaudos;
        }
        public NaftosKuroAutomobilis()
        {


        }

        public NaftosKuroAutomobilis(string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
    : base(marke, modelis, nuomosKaina)
        {
            DegaluSanaudos = degaluSanaudos;
        }

        public override string ToString()
        {
            return $"{Id} {Marke} {Modelis} {NuomosKaina} {DegaluSanaudos.ToString("F1")}L/100km";
        }
    }
}
