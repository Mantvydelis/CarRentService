using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiService
    {
        void NuskaitytiIsFailo();
        void IrasytiIFaila();
        void PridetiAutomobili(Automobilis automobilis);
        List<Automobilis> PaieskaPagalMarke(string marke);
        List<Automobilis> GautiVisusAutomobilius();
        List<Elektromobilis> GautiVisusElektromobilius();
        List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto();

        
    }
}
