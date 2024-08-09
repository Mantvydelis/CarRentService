using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IUzsakymaiRepository
    {
        Task<List<NuomosUzsakymas>> GautiVisusNuomosUzsakymus();
        Task PridetiNaujaUzsakyma(NuomosUzsakymas nuomosUzsakymas);

        Task <NuomosUzsakymas> GautiUzsakymaPagalId(int id);

        Task KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId);

        Task IstrintiUzsakyma (int id);
    }
}
