using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Services
{
    public class AutonuomosService : IAutonuomaService
    {
        private readonly IKlientaiService _klientaiService;
        private readonly IAutomobiliaiService _automobiliaiService;

        private List<Automobilis> VisiAutomobiliai = new List<Automobilis>();

        private List<NuomosUzsakymas> VisiUzsakymai = new List<NuomosUzsakymas>();

        public AutonuomosService(IKlientaiService klientaiService, IAutomobiliaiService automobiliaiService)
        {
            _automobiliaiService = automobiliaiService;
            _klientaiService = klientaiService;
        }

        public List<Automobilis> GautiVisusAutomobilius()
        {
            if (VisiAutomobiliai.Count == 0)
                VisiAutomobiliai = _automobiliaiService.GautiVisusAutomobilius();
            return VisiAutomobiliai;
        }

        public void PridetiNaujaAutomobili(Automobilis automobilis)
        {
            _automobiliaiService.PridetiAutomobili(automobilis);
        }


        public List<Klientas> GautiVisusKlientus()
        {
            return _klientaiService.GautiVisusKlientus();
        }
        public List<Elektromobilis> GautiVisusElektromobilius()
        {
            return _automobiliaiService.GautiVisusElektromobilius();
        }
        public List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto()
        {
            return _automobiliaiService.GautiVisusNaftosKuroAuto();
        }
        public void SukurtiNuoma(string klientoVardas, string klientoPavarde, int autoId, DateTime nuomosPradzia, int dienos)
        {
            Klientas klientas = _klientaiService.PaieskaPagalVardaPavarde(klientoVardas, klientoPavarde);

            Automobilis automobilis = new Automobilis();

            foreach (Automobilis a in VisiAutomobiliai)
            {
                if (a.Id == autoId)
                    automobilis = a;
            }

            NuomosUzsakymas nuomosUzsakymas = new NuomosUzsakymas
            {
                Uzsakovas = klientas,
                NuomuojamasAuto = automobilis,
                NuomosPradzia = nuomosPradzia,
                DienuKiekis = dienos
            };
            VisiUzsakymai.Add(nuomosUzsakymas);
        }

        public void PridetiNaujaKlienta(Klientas klientas)
        {
            _klientaiService.PridetiNaujaKlienta(klientas);
        }


        public void SkaiciuotiBendraNuomosKaina()
        {
            throw new NotImplementedException();
        }

        public List<NuomosUzsakymas> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde)
        {
            Klientas klientas = _klientaiService.PaieskaPagalVardaPavarde(klientoVardas, klientoPavarde);

            if (klientas == null)
            {
                return new List<NuomosUzsakymas>();
            }

            return VisiUzsakymai.Where(u => u.Uzsakovas == klientas).ToList();
        }

        public List<NuomosUzsakymas> GautiVisusUzsakymus()
        {
            return VisiUzsakymai;
        }
    }
    }

