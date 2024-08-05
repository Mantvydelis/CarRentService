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
        List<NuomosUzsakymas> GautiVisusNuomosUzsakymus();
        void PridetiNaujaUzsakyma(NuomosUzsakymas nuomosUzsakymas);

        public NuomosUzsakymas GautiUzsakymaPagalId(int id);

        void KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis);

        void IstrintiUzsakyma (int id);
    }
}
