using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IDarbuotojaiRepository
    {
        Task PridetiDarbuotoja(Darbuotojas darbuotojas);

        Task<List<Darbuotojas>> GautiVisusDarbuotojus();
        Task<Darbuotojas> GautiDarbuotojaPagalId(int id);

        Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos, double bazinisAtlyginimas, int atliktuUzsakymuSkaicius);

        Task IstrintiDarbuotoja(int id);



    }
}
