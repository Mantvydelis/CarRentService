using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class KlientaiEFDRepository : IKlientaiRepository
    {
        public async Task<Klientas> GautiKlientaPagalId(int id)
        {
            using (var context = new MyDbContext())
            {

                var klientas = await context.Klientai.FindAsync(id);

                if (klientas != null)
                {
                    Console.WriteLine($"{klientas.KlientasId} {klientas.Vardas} {klientas.Pavarde} {klientas.GimimoMetai}");
                }
                else
                {
                    Console.WriteLine("Klientas nerastas.");
                }

                return klientas;

            }
        }

        public async Task<List<Klientas>> GautiVisusKlientus()
        {
            using (var context = new MyDbContext())
            {

                List<Klientas> visiKlientai = await context.Klientai.ToListAsync();


                foreach (Klientas p in visiKlientai)
                {
                    Console.WriteLine($"{p.KlientasId} {p.Vardas} {p.Pavarde} {p.GimimoMetai}");
                }

                return visiKlientai;
            }
        }

        public async Task IstrintiKlienta(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Klientai.Remove(context.Klientai.Find(id));
                context.SaveChanges();
            }
        }

        public async Task<Klientas> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            using (var context = new MyDbContext())
            {

                var klientas = await context.Klientai.FindAsync(id);

                if (klientas == null) return null;

                klientas.KlientasId = id;
                klientas.Vardas = vardas;
                klientas.Pavarde = pavarde;
                klientas.GimimoMetai = gimimoMetai;
               

                await context.SaveChangesAsync();

                return klientas;

            }
        }

        public async Task PridetiNaujaKlienta(Klientas klientas)
        {
            using (var context = new MyDbContext())
            {
                await context.Klientai.AddAsync(klientas);
                await context.SaveChangesAsync();
            }
        }
    }
}
