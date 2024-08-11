using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
        private readonly IDarbuotojaiRepository _darbuotojaiRepository;
        private readonly IMongoDbCacheRepository _mongoCache;

        private List<Automobilis> VisiAutomobiliai = new List<Automobilis>();

        private List<NuomosUzsakymas> VisiUzsakymai = new List<NuomosUzsakymas>();

        public AutonuomosService(IKlientaiService klientaiService, IAutomobiliaiService automobiliaiService, IUzsakymaiRepository uzsakymaiRepository, IDarbuotojaiRepository darbuotojaiRepository, IMongoDbCacheRepository mongoDbCacheRepository)
        {
            _automobiliaiService = automobiliaiService;
            _klientaiService = klientaiService;
            _uzsakymaiRepository = uzsakymaiRepository;
            _darbuotojaiRepository = darbuotojaiRepository;
            _mongoCache = mongoDbCacheRepository;
        }

        public async Task<List<Automobilis>> GautiVisusAutomobilius()
        {
            if (VisiAutomobiliai.Count == 0)
                VisiAutomobiliai = await _automobiliaiService.GautiVisusAutomobilius();
            return VisiAutomobiliai;
        }

        public async Task PridetiNaujaAutomobili(Automobilis automobilis)
        {
            await _automobiliaiService.PridetiAutomobili(automobilis);
        }


        public async Task<List<Klientas>> GautiVisusKlientus()
        {

            List<Klientas> results;

            if ((results = _mongoCache.GautiVisusKlientus().Result) != null && results.Any())
                return results;

            results = await _klientaiService.GautiVisusKlientus();

            if (results != null && results.Any())
            {
                foreach (var klientas in results)
                {
                    await _mongoCache.PridetiKlienta(klientas);
                }
            }

            return results;
        }
        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {

            List<Elektromobilis> results;

            if ((results = _mongoCache.GautiVisusElektromobilius().Result) != null && results.Any())
                return results;

            results = await _automobiliaiService.GautiVisusElektromobilius();

            if (results != null && results.Any())
            {
                foreach (var elektromobilis in results)
                {
                    await _mongoCache.PridetiElektromobili(elektromobilis);
                }
            }

            return results;


        }
        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAuto()
        {

            List<NaftosKuroAutomobilis> results;

            if ((results = _mongoCache.GautiVisusNaftosKuroAuto().Result) != null && results.Any())
                return results;

            results = await _automobiliaiService.GautiVisusNaftosKuroAuto();

            if (results != null && results.Any())
            {
                foreach (var naftosKuroAuto in results)
                {
                    await _mongoCache.PridetiNaftosKuroAuto(naftosKuroAuto);
                }
            }

            return results;



        }
        public async Task SukurtiNuoma(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas, int darbuotojasId)
        {
            var darbuotojas = await _darbuotojaiRepository.GautiDarbuotojaPagalId(darbuotojasId);
            var nuomosUzsakymas = new NuomosUzsakymas(klientasId, automobilisId, nuomosPradzia, dienuKiekis, autoTipas, darbuotojasId);
           

            await _uzsakymaiRepository.PridetiNaujaUzsakyma(nuomosUzsakymas);
            await _mongoCache.PridetiUzsakyma(nuomosUzsakymas);


        }


        public async Task PridetiNaujaKlienta(Klientas klientas)
        {
            await _klientaiService.PridetiNaujaKlienta(klientas);
            await _mongoCache.PridetiKlienta(klientas);
        }


        public async Task SkaiciuotiBendraNuomosKaina()
        {
            throw new NotImplementedException();
        }

        public async Task<List<NuomosUzsakymas>> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde) /*Cia koreguoti*/
        {
            Klientas klientas = await _klientaiService.PaieskaPagalVardaPavarde(klientoVardas, klientoPavarde);

            if (klientas == null)
            {
                return new List<NuomosUzsakymas>();
            }

            return VisiUzsakymai.Where(u => u.Uzsakovas == klientas).ToList();
        }

        public async Task<List<NuomosUzsakymas>> GautiVisusUzsakymus()
        {



            List<NuomosUzsakymas> results;

            if ((results = _mongoCache.GautiVisusNuomosUzsakymus().Result) != null && results.Any())
                return results;

            results = await _uzsakymaiRepository.GautiVisusNuomosUzsakymus();

            if (results != null && results.Any())
            {
                foreach (var nuomosUzsakymas in results)
                {
                    await _mongoCache.PridetiUzsakyma(nuomosUzsakymas);
                }
            }

            return results;



        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id)
        {
            NaftosKuroAutomobilis result;
            if ((result = _mongoCache.GautiNaftosKuroAutoPagalId(id).Result) != null)
                return result;
            result = await _automobiliaiService.GautiNaftosAutoPagalId(id);
            await _mongoCache.PridetiNaftosKuroAuto(result);
            return result;


        }

        public async Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            var result1 = await _automobiliaiService.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
            var result2 = await _mongoCache.KoreguotiNaftosKuroAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);


            return result1 ?? result2;


        }

        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {
            Elektromobilis result;
            if ((result = _mongoCache.GautiElektromobiliPagalId(id).Result) != null)
                return result;
            result = await _automobiliaiService.GautiElektromobiliPagalId(id);
            await _mongoCache.PridetiElektromobili(result);
            return result;

        }

        public async Task <Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            var result1 = await _automobiliaiService.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
            var result2 = await _mongoCache.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);


            return result1 ?? result2;

        }
       

        public async Task<Klientas> GautiKlientaPagalId(int id)
        {
            Klientas result;
            if ((result = _mongoCache.GautiKlientaPagalId(id).Result) != null)
                return result;
            result = await _klientaiService.GautiKlientaPagalId(id);
            await _mongoCache.PridetiKlienta(result);
            return result;
        }

        public async Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {

            var result1 = await _klientaiService.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);
            var result2 = await _mongoCache.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);

            return result1 ?? result2;
        }

        public async Task<NuomosUzsakymas> GautiUzsakymaPagalId(int id)
        {


            NuomosUzsakymas result;
            if ((result = _mongoCache.GautiUzsakymaPagalId(id).Result) != null)
                return result;
            result = await _uzsakymaiRepository.GautiUzsakymaPagalId(id);
            await _mongoCache.PridetiUzsakyma(result);
            return result;

        }

        public async Task KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId)
        {
            var surastiDarbuotojoId = await _darbuotojaiRepository.GautiDarbuotojaPagalId(darbuotojasId);
            await _uzsakymaiRepository.KoreguotiNuomosInfo(id, klientasId, autoTipas, automobilisId, nuomosPradzia, dienuKiekis, darbuotojasId);
            await _mongoCache.KoreguotiNuomosUzsakymoInfo(id, klientasId, autoTipas, automobilisId, nuomosPradzia, dienuKiekis, darbuotojasId);

        }

        public async Task IstrintiNaftaAuto(int id)
        {
            await _automobiliaiService.IstrintiNaftaAuto(id);
        }

        public async Task IstrintiElektromobili(int id)
        {
            await _automobiliaiService.IstrintiElektromobili(id);
        }

        public async Task IstrintiKlienta(int id)
        {
            await _klientaiService.IstrintiKlienta(id);
            await _mongoCache.IstrintiKlienta(id);
        }

        public async Task IstrintiUzsakyma(int id)
        {

            await _uzsakymaiRepository.IstrintiUzsakyma(id);

        }

        public async Task PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            await _darbuotojaiRepository.PridetiDarbuotoja(darbuotojas);
            await _mongoCache.PridetiDarbuotoja(darbuotojas);
        }

        public async Task<List<Darbuotojas>> GautiVisusDarbuotojus()
        {
            List<Darbuotojas> results;

            if ((results = _mongoCache.GautiVisusDarbuotojus().Result) != null && results.Any())
                return results;

            results = await _darbuotojaiRepository.GautiVisusDarbuotojus();

            if (results != null && results.Any())
            {
                foreach (var darbuotojas in results)
                {
                    await _mongoCache.PridetiDarbuotoja(darbuotojas);
                }
            }

            return results;
        }

        public async Task<Darbuotojas> GautiDarbuotojaPagalId(int id)
        {
            Darbuotojas result;
            if ((result = _mongoCache.GautiDarbuotojaPagalId(id).Result) != null)
                return result;
            result = await _darbuotojaiRepository.GautiDarbuotojaPagalId(id);
            await _mongoCache.PridetiDarbuotoja(result);
            return result;


            
        }

        public async Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {
            var result1 = await _darbuotojaiRepository.KoreguotiDarbuotojoInfo(id, vardas, pavarde, pareigos);
            var result2 = await _mongoCache.KoreguotiDarbuotojoInfo(id, vardas, pavarde, pareigos);

            return result1 ?? result2;

        }

        public async Task IstrintiDarbuotoja(int id)
        {
            await _darbuotojaiRepository.IstrintiDarbuotoja(id);
            await _mongoCache.IstrintiDarbuotoja(id);

        }



    }
}

