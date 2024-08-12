using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class DarbuotojaiEFDRepository : IDarbuotojaiRepository
    {
        public async Task<Darbuotojas> GautiDarbuotojaPagalId(int id)
        {
            using (var context = new MyDbContext())
            {

                var darbuotojas = await context.Darbuotojai.FindAsync(id);

                if (darbuotojas != null)
                {
                    Console.WriteLine($"{darbuotojas.Id} {darbuotojas.Vardas} {darbuotojas.Pavarde} {darbuotojas.Pareigos} {darbuotojas.BazinisAtlyginimas} {darbuotojas.AtliktuUzsakymuKiekis}");
                }
                else
                {
                    Console.WriteLine("Darbuotojas nerastas.");
                }

                return darbuotojas;

            }
        }

        public async Task<List<Darbuotojas>> GautiVisusDarbuotojus()
        {
            using (var context = new MyDbContext())
            {

                List<Darbuotojas> visiDarbuotojai = await context.Darbuotojai.ToListAsync();


                foreach (Darbuotojas p in visiDarbuotojai)
                {
                    Console.WriteLine($"{p.Id} {p.Vardas} {p.Pavarde} {p.Pareigos} {p.BazinisAtlyginimas} {p.AtliktuUzsakymuKiekis}");
                }

                return visiDarbuotojai;
            }
        }

        public async Task IstrintiDarbuotoja(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Darbuotojai.Remove(context.Darbuotojai.Find(id));
                context.SaveChanges();
            }
        }

        public async Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos, double bazinisAtlyginimas, int atliktuUzsakymuSkaicius)
        {


            using (var context = new MyDbContext())
            {

                var darbuotojas = await context.Darbuotojai.FindAsync(id);

                if (darbuotojas == null) return null;

                darbuotojas.Id = id;
                darbuotojas.Vardas = vardas;
                darbuotojas.Pavarde = pavarde;
                darbuotojas.Pareigos = pareigos;
                darbuotojas.BazinisAtlyginimas = bazinisAtlyginimas;
                darbuotojas.AtliktuUzsakymuKiekis = atliktuUzsakymuSkaicius;


                await context.SaveChangesAsync();

                return darbuotojas;

            }
        }

        public async Task PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            using (var context = new MyDbContext())
            {
                await context.Darbuotojai.AddAsync(darbuotojas);
                await context.SaveChangesAsync();
            }
        }
    }
}
