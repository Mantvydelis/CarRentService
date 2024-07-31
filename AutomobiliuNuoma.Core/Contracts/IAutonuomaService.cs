using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutonuomaService
    {
        void PridetiNaujaAutomobili(Automobilis automobilis);
        List<Automobilis> GautiVisusAutomobilius();
        List<Klientas> GautiVisusKlientus();
        void SukurtiNuoma(string klientoVardas, string klientoPavarde, int autoId, DateTime nuomosPradzia, int dienos);
        List<Elektromobilis> GautiVisusElektromobilius();
        List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto();

        void PridetiNaujaKlienta(Klientas klientas);

        List<NuomosUzsakymas> GautiVisusUzsakymus();

        void SkaiciuotiBendraNuomosKaina();

        List<NuomosUzsakymas> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde);


    }
}
