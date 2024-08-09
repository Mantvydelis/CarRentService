using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Services
{
    public class KlientaiService : IKlientaiService
    {
        private readonly IKlientaiRepository _klientaiRepository;
        private readonly IMongoDbCacheRepository _mongoCache;



        private List<Klientas> VisiKlientai = new List<Klientas>();
        public KlientaiService(IKlientaiRepository klientaiRepository, IMongoDbCacheRepository mongoDbCache)
        {
            _klientaiRepository = klientaiRepository;
            _mongoCache = mongoDbCache;
        }

        public async Task<List<Klientas>> GautiVisusKlientus()
        {

            if (VisiKlientai.Count == 0)
                VisiKlientai = await _klientaiRepository.GautiVisusKlientus();
            return VisiKlientai;

        }

        public async Task IrasytiIFaila()
        {
            throw new NotImplementedException();
        }

        public async Task NuskaitytiIsFailo()
        {
            throw new NotImplementedException();
        }

        public async Task<Klientas> PaieskaPagalVardaPavarde(string vardas, string pavarde)
        {
            Klientas klientas = new Klientas();
            foreach (Klientas k in VisiKlientai)
            {
                if (k.Vardas == vardas && k.Pavarde == pavarde)
                {
                    klientas = k;
                    break;
                }
            }
            return klientas;
        }

        public async Task PridetiNaujaKlienta(Klientas klientas)
        {
            await _klientaiRepository.PridetiNaujaKlienta(klientas);
            await _mongoCache.PridetiKlienta(klientas);

        }

        public async Task<Klientas> GautiKlientaPagalId(int id)
        {
            return await _klientaiRepository.GautiKlientaPagalId(id);

        }

        public async Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            return await _klientaiRepository.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);
        }

        public async Task IstrintiKlienta(int id)
        {
            await _klientaiRepository.IstrintiKlienta(id);

        }


    }
}
