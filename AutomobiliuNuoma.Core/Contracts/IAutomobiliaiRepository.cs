using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiRepository
    {
        Task<List<Automobilis>> NuskaitytiAutomobilius();
        Task IrasytiAutomobilius();
        Task IrasytiElektromobili(Elektromobilis elektromobilis);
        Task IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis automobilis);
        Task<List<Elektromobilis>> GautiVisusElektromobilius();
        Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAutomobilius();

        Task<Elektromobilis> GautiElektromobiliPagalId(int id);

        Task <NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id);

        Task <NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Task <Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        Task IstrintiNaftaAuto(int id);

        Task IstrintiElektromobili(int id);

       


    }
}
