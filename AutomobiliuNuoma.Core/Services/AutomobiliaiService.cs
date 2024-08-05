﻿using AutomobiliuNuoma.Core.Contracts;
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
        public List<Automobilis> GautiVisusAutomobilius()
        {
            return _automobiliaiRepository.NuskaitytiAutomobilius();
        }
        public List<Elektromobilis> GautiVisusElektromobilius()
        {
            return _automobiliaiRepository.GautiVisusElektromobilius();
        }
        public List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto()
        {
            return _automobiliaiRepository.GautiVisusNaftosKuroAutomobilius();
        }

        public void IrasytiIFaila()
        {
            throw new NotImplementedException();
        }

        public void NuskaitytiIsFailo()
        {
            throw new NotImplementedException();
        }

        public List<Automobilis> PaieskaPagalMarke(string marke)
        {
            List<Automobilis> paieskosRezultatai = new List<Automobilis>();
            List<Automobilis> automobiliai = _automobiliaiRepository.NuskaitytiAutomobilius();
            foreach (Automobilis a in automobiliai)
            {
                if (a.Marke == marke)
                    paieskosRezultatai.Add(a);
            }
            return paieskosRezultatai;
        }

        public void PridetiAutomobili(Automobilis automobilis)
        {
            if (automobilis is Elektromobilis)
                _automobiliaiRepository.IrasytiElektromobili((Elektromobilis)automobilis);
            else
                _automobiliaiRepository.IrasytiNaftosKuroAutomobili((NaftosKuroAutomobilis)automobilis);
        }

        public NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id)
        {
            return _automobiliaiRepository.GautiNaftosAutoPagalId(id);
        }

        public NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            return _automobiliaiRepository.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
        }

        public Elektromobilis GautiElektromobiliPagalId(int id)
        {
            return _automobiliaiRepository.GautiElektromobiliPagalId(id);
        }

        public Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            return _automobiliaiRepository.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
        }

        public void IstrintiNaftaAuto(int id)
        {
            _automobiliaiRepository.IstrintiNaftaAuto(id);
        }

        public void IstrintiElektromobili(int id)
        {
            _automobiliaiRepository.IstrintiElektromobili(id);
        }

    }
}
