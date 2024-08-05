﻿using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Contracts
{
    public interface IAutomobiliaiRepository
    {
        List<Automobilis> NuskaitytiAutomobilius();
        void IrasytiAutomobilius();
        void IrasytiElektromobili(Elektromobilis elektromobilis);
        void IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis automobilis);
        List<Elektromobilis> GautiVisusElektromobilius();
        List<NaftosKuroAutomobilis> GautiVisusNaftosKuroAutomobilius();

        Elektromobilis GautiElektromobiliPagalId(int id);

        NaftosKuroAutomobilis GautiNaftosAutoPagalId(int id);

        NaftosKuroAutomobilis KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos);

        Elektromobilis KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas);

        void IstrintiNaftaAuto(int id);

        void IstrintiElektromobili(int id);
    }
}
