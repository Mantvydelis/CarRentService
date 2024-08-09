using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IKlientaiService
    {
        Task NuskaitytiIsFailo();
        Task IrasytiIFaila();
        Task<List<Klientas>> GautiVisusKlientus();
        Task<Klientas> PaieskaPagalVardaPavarde(string vardas, string pavarde);

        Task PridetiNaujaKlienta(Klientas klientas);

        Task<Klientas> GautiKlientaPagalId(int id);

        Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai);

        Task IstrintiKlienta(int id);
    }
}
