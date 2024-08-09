using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Services
{
    public class DarbuotojaiService : IDarbuotojaiService
    {
        private readonly IDarbuotojaiRepository _darbuotojaiRepository;
        private readonly IMongoDbCacheRepository _mongoCache;
        
        public DarbuotojaiService(IDarbuotojaiRepository darbuotojaiRepository, IMongoDbCacheRepository mongoDbCache)
        {
            _darbuotojaiRepository = darbuotojaiRepository;
            _mongoCache = mongoDbCache;
        }

        public async Task<Darbuotojas> GautiDarbuotojaPagalId(int darbuotojoId)
        {

            return await _darbuotojaiRepository.GautiDarbuotojaPagalId(darbuotojoId);

        }

        public async Task<List<Darbuotojas>> GautiDarbuotojus()
        {
            return await _darbuotojaiRepository.GautiVisusDarbuotojus();
        }

        public async Task PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            await _darbuotojaiRepository.PridetiDarbuotoja(darbuotojas);
        }

    }
}
