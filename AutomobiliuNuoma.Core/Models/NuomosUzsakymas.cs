using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NuomosUzsakymas
    {
        public Klientas Uzsakovas { get; set; }
        public Automobilis NuomuojamasAuto { get; set; }
        public DateTime NuomosPradzia { get; set; }
        public int DienuKiekis { get; set; }

        public string KlientoVardas {  get; set; }
        public string KlientoPavarde {  get; set; }
        public int AutomobilisId { get; set; }

        public int ElektromobilisId { get; set; }

        public int BenzAutomobilisId { get; set; }
        public int KlientasId { get; set; }

        public string AutoTipas { get; set; }

        public int UzsakymasId { get; set; }

        public Darbuotojas DarbuotojasId { get; set; }


        public NuomosUzsakymas(Klientas uzsakovas, Automobilis nuomuojamasAuto, DateTime nuomosPradzia, int dienuKiekis)
        {
            Uzsakovas = uzsakovas;
            NuomuojamasAuto = nuomuojamasAuto;
            NuomosPradzia = nuomosPradzia;
            DienuKiekis = dienuKiekis;
        }

        public NuomosUzsakymas(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas)
        {
            KlientasId = klientasId;
            AutomobilisId = automobilisId;
            NuomosPradzia = nuomosPradzia;
            DienuKiekis = dienuKiekis;
            AutoTipas = autoTipas;
        }

        public NuomosUzsakymas() { }

        public decimal skaiciuotiNuomosKaina() /*REIKS PAKOREGUOTI IR PRIDETI PRIE CASE 8*/
        {
            return DienuKiekis * NuomuojamasAuto.NuomosKaina;
        }

        public DateTime gautiPabaigosData()
        {
            return NuomosPradzia.AddDays(DienuKiekis);
        }


    }


}
