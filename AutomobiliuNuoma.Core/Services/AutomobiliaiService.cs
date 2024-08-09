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
    public class AutomobiliaiService : IAutomobiliaiService
    {
        private readonly IAutomobiliaiRepository _automobiliaiRepository;
        public AutomobiliaiService(IAutomobiliaiRepository automobiliaiRepository)
        {
            _automobiliaiRepository = automobiliaiRepository;
        }
        public async Task<List<Automobilis>> GautiVisusAutomobilius()
        {
            List<Automobilis> visiAutomobiliai = new List<Automobilis>();
            var elektriniai = _automobiliaiRepository.GautiVisusElektromobilius();
            var naftoskuro = _automobiliaiRepository.GautiVisusNaftosKuroAutomobilius();
            await Task.WhenAll(elektriniai, naftoskuro);
            visiAutomobiliai.AddRange(elektriniai.Result);
            visiAutomobiliai.AddRange(naftoskuro.Result);

            return visiAutomobiliai.ToList();
            //..
        }

        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {
            return await _automobiliaiRepository.GautiVisusElektromobilius();
        }
        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAuto()
        {
            return await _automobiliaiRepository.GautiVisusNaftosKuroAutomobilius();
        }

        public async Task IrasytiIFaila()
        {
            throw new NotImplementedException();
        }

        public async Task NuskaitytiIsFailo()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Automobilis>> PaieskaPagalMarke(string marke)
        {
            List<Automobilis> paieskosRezultatai = new List<Automobilis>();
            List<Automobilis> automobiliai = await _automobiliaiRepository.NuskaitytiAutomobilius();
            foreach (Automobilis a in automobiliai)
            {
                if (a.Marke == marke)
                    paieskosRezultatai.Add(a);
            }
            return paieskosRezultatai;
        }

        public async Task PridetiAutomobili(Automobilis automobilis)
        {
            if (automobilis is Elektromobilis)
                await _automobiliaiRepository.IrasytiElektromobili((Elektromobilis)automobilis);
            else
                await _automobiliaiRepository.IrasytiNaftosKuroAutomobili((NaftosKuroAutomobilis)automobilis);
        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id)
        {
            return await _automobiliaiRepository.GautiNaftosAutoPagalId(id);
        }

        public async Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            return await _automobiliaiRepository.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
        }

        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {
            return await _automobiliaiRepository.GautiElektromobiliPagalId(id);
        }

        public async Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            return await _automobiliaiRepository.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
        }

        public async Task IstrintiNaftaAuto(int id)
        {
            await _automobiliaiRepository.IstrintiNaftaAuto(id);
        }

        public async Task IstrintiElektromobili(int id)
        {
            await _automobiliaiRepository.IstrintiElektromobili(id);
        }

    }
}
