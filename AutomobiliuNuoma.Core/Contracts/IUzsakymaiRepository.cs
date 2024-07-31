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
    }
}
