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
        void NuskaitytiIsFailo();
        void IrasytiIFaila();
        void PridetiAutomobili(Automobilis automobilis);
        List<Automobilis> PaieskaPagalMarke(string marke);
        List<Automobilis> GautiVisusAutomobilius();
        List<Elektromobilis> GautiVisusElektromobilius();
        List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAuto();

        NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id);

        NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Elektromobilis GautiElektromobiliPagalId(int id);

        Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        public NaftosKuroAutomobilis IstrintiNaftaAuto(int id);

        public Elektromobilis IstrintiElektromobili(int id);

    }

}
