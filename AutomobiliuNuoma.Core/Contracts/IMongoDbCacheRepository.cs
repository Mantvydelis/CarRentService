using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IMongoDbCacheRepository
    {
        Task PridetiDarbuotoja(Darbuotojas darbuotojas);

        Task PridetiKlienta(Klientas klientas);
        Task<Darbuotojas> GautiDarbuotojaPagalId(int id);

        Task<Klientas> GautiKlientaPagalId(int id);

        Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos);

        Task<Klientas> KoreguotiKlientoInfo(int klientasId, string vardas, string pavarde, DateOnly gimimoMetai);

        Task<List<Darbuotojas>> GautiVisusDarbuotojus();

        Task<List<Klientas>> GautiVisusKlientus();

        Task IstrintiDarbuotoja(int id);

        Task IstrintiKlienta(int klientasId);
    }
}
