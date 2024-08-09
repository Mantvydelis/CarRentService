using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiService
    {
        Task NuskaitytiIsFailo();
        Task IrasytiIFaila();
        Task PridetiAutomobili(Automobilis automobilis);

        Task<List<Automobilis>> PaieskaPagalMarke(string marke);
        Task<List<Automobilis>> GautiVisusAutomobilius();
        Task<List<Elektromobilis>> GautiVisusElektromobilius();
        Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAuto();

        Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id);

        Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Task<Elektromobilis> GautiElektromobiliPagalId(int id);

        Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        Task IstrintiNaftaAuto(int id);

        Task IstrintiElektromobili(int id);

        Task<int> GautiElektromobiliuSkaiciu();

        Task<int> GautiNaftosKuroAutoSkaiciu();

    }

}
