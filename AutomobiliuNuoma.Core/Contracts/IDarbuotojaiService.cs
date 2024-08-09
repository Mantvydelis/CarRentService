using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IDarbuotojaiService
    {
        Task PridetiDarbuotoja(Darbuotojas darbuotojas);
        Task<List<Darbuotojas>> GautiDarbuotojus();
        Task<Darbuotojas> GautiDarbuotojaPagalId(int darbuotojoId);



    }
}
