using AutomobiliuNuoma.Core.Enums;
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
        Task PridetiNaujaAutomobili(Automobilis automobilis);
        Task<List<Automobilis>> GautiVisusAutomobilius();
        Task<List<Klientas>> GautiVisusKlientus();
        Task SukurtiNuoma(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas, int darbuotojasId);

        Task<List<Elektromobilis>> GautiVisusElektromobilius();
        Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAuto();

        Task PridetiNaujaKlienta(Klientas klientas);

        Task<List<NuomosUzsakymas>> GautiVisusUzsakymus();

        Task SkaiciuotiBendraNuomosKaina();

        Task<List<NuomosUzsakymas>> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde);

        Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id);

        Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Task<Elektromobilis> GautiElektromobiliPagalId(int id);

        Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        Task<Klientas> GautiKlientaPagalId(int id);

        Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai);

        Task<NuomosUzsakymas> GautiUzsakymaPagalId(int id);

        Task KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId);

        Task IstrintiNaftaAuto(int id);

        Task IstrintiElektromobili(int id);

        Task IstrintiKlienta(int id);

        Task IstrintiUzsakyma(int id);


        Task PridetiDarbuotoja(Darbuotojas darbuotojas);

        Task<List<Darbuotojas>> GautiVisusDarbuotojus();
        Task<Darbuotojas> GautiDarbuotojaPagalId(int id);

        Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos, double bazinisAtlyginimas, int atliktuUzsakymuSkaicius);

        Task IstrintiDarbuotoja(int id);

   
    }

}
