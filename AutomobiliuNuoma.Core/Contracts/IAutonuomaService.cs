﻿using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutonuomaService
    {
        void PridetiNaujaAutomobili(Automobilis automobilis);
        List<Automobilis> GautiVisusAutomobilius();
        List<Klientas> GautiVisusKlientus();
        void SukurtiNuoma(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas, int darbuotojasId);

        List<Elektromobilis> GautiVisusElektromobilius();
        List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto();

        void PridetiNaujaKlienta(Klientas klientas);

        List<NuomosUzsakymas> GautiVisusUzsakymus();

        void SkaiciuotiBendraNuomosKaina();

        List<NuomosUzsakymas> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde);

        NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id);

        NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Elektromobilis GautiElektromobiliPagalId(int id);

        Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        Klientas GautiKlientaPagalId(int id);

        Klientas KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai);

        public NuomosUzsakymas GautiUzsakymaPagalId(int id);

        void KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId);

        void IstrintiNaftaAuto(int id);

        void IstrintiElektromobili(int id);

        void IstrintiKlienta(int id);

        void IstrintiUzsakyma(int id);


        void PridetiDarbuotoja(Darbuotojas darbuotojas);

        List<Darbuotojas> GautiVisusDarbuotojus();
        Darbuotojas GautiDarbuotojaPagalId(int id);

        Darbuotojas KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos);

        void IstrintiDarbuotoja(int id);
    }

}
