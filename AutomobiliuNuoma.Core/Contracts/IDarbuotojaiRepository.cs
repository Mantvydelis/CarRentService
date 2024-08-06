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
        void PridetiDarbuotoja(Darbuotojas darbuotojas);

        List<Darbuotojas> GautiVisusDarbuotojus();
        Darbuotojas GautiDarbuotojaPagalId(int id);

        Darbuotojas KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos);

        void IstrintiDarbuotoja(int id);



    }
}
