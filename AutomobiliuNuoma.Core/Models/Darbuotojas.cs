﻿using AutomobiliuNuoma.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class Darbuotojas
    {
        public int Id { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public DarbuotojasPareigos Pareigos { get; set; }

        public double BazinisAtlyginimas { get; set; }

        public int AtliktuUzsakymuKiekis { get; set; }



        public Darbuotojas(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {
            Id = id; Vardas = vardas; Pavarde = pavarde; Pareigos = pareigos;

        }

        public Darbuotojas() { }

        public override string ToString()
        {
            return $"{Id} {Vardas} {Pavarde} Pareigos: {Pareigos}";
        }


        public double SkaiciuotiAtlyginima(double bazinisAtlyginimas, int atliktuUzsakymuKiekis)
        {
            switch (Pareigos)
            {
                case DarbuotojasPareigos.Direktorius:
                case DarbuotojasPareigos.Mechanikas:
                    return bazinisAtlyginimas;
                case DarbuotojasPareigos.Vadybininkas:
                    return bazinisAtlyginimas + (atliktuUzsakymuKiekis * 7);
                default:
                    return bazinisAtlyginimas;
            }
        }

    }
}
