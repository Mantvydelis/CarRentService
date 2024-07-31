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
        private List<Klientas> VisiKlientai = new List<Klientas>();
        public KlientaiService(IKlientaiRepository klientaiRepository)
        {
            _klientaiRepository = klientaiRepository;
        }

        public List<Klientas> GautiVisusKlientus()
        {
            if (VisiKlientai.Count == 0)
                VisiKlientai = _klientaiRepository.GautiVisusKlientus();
            return VisiKlientai;
        }

        public void IrasytiIFaila()
        {
            throw new NotImplementedException();
        }

        public void NuskaitytiIsFailo()
        {
            throw new NotImplementedException();
        }

        public Klientas PaieskaPagalVardaPavarde(string vardas, string pavarde)
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

        public void PridetiNaujaKlienta(Klientas klientas)
        {
            _klientaiRepository.PridetiNaujaKlienta(klientas);
        }

    }
}
