using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
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
        private readonly IUzsakymaiRepository _uzsakymaiRepository;

        private List<Automobilis> VisiAutomobiliai = new List<Automobilis>();

        private List<NuomosUzsakymas> VisiUzsakymai = new List<NuomosUzsakymas>();

        public AutonuomosService(IKlientaiService klientaiService, IAutomobiliaiService automobiliaiService, IUzsakymaiRepository uzsakymaiRepository)
        {
            _automobiliaiService = automobiliaiService;
            _klientaiService = klientaiService;
            _uzsakymaiRepository = uzsakymaiRepository;
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
        public void SukurtiNuoma(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas)
        {
            var nuomosUzsakymas = new NuomosUzsakymas(klientasId, automobilisId, nuomosPradzia, dienuKiekis, autoTipas);
            _uzsakymaiRepository.PridetiNaujaUzsakyma(nuomosUzsakymas);
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
            return _uzsakymaiRepository.GautiVisusNuomosUzsakymus();

        }

        public NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id)
        {
            return _automobiliaiService.GautiNaftosAutoPagalId(id);
        }

        public NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            return _automobiliaiService.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
        }

        public Elektromobilis GautiElektromobiliPagalId(int id)
        {
            return _automobiliaiService.GautiElektromobiliPagalId(id);
        }

        public Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            return _automobiliaiService.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
        }

        public Klientas GautiKlientaPagalId(int id)
        {
            return _klientaiService.GautiKlientaPagalId(id);
        }

        public Klientas KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            return _klientaiService.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);
        }

        public NuomosUzsakymas GautiUzsakymaPagalId(int id)
        {
            return _uzsakymaiRepository.GautiUzsakymaPagalId(id);

        }

        public void KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis)
        {
            _uzsakymaiRepository.KoreguotiNuomosInfo(id, klientasId, autoTipas, automobilisId, nuomosPradzia, dienuKiekis);

        }

        public NaftosKuroAutomobilis IstrintiNaftaAuto(int id)
        {
            return _automobiliaiService.IstrintiNaftaAuto(id);
        }

        public Elektromobilis IstrintiElektromobili(int id)
        {
            return _automobiliaiService.IstrintiElektromobili(id);
        }
    }
}

