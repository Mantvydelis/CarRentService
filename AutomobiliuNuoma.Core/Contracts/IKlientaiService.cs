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
        void NuskaitytiIsFailo();
        void IrasytiIFaila();
        List<Klientas> GautiVisusKlientus();
        Klientas PaieskaPagalVardaPavarde(string vardas, string pavarde);

        void PridetiNaujaKlienta(Klientas klientas);

        Klientas GautiKlientaPagalId(int id);

        Klientas KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai);
    }
}
