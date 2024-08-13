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
    public class UzsakymaiEFDRepository : IUzsakymaiRepository
    {
        public async Task<NuomosUzsakymas> GautiUzsakymaPagalId(int id)
        {
            using (var context = new MyDbContext())
            {

                var nuomosUzsakymas = await context.NuomosUzsakymas.FindAsync(id);

                if (nuomosUzsakymas != null)
                {
                    Console.WriteLine($"{nuomosUzsakymas.UzsakymasId} {nuomosUzsakymas.NuomosPradzia} {nuomosUzsakymas.DienuKiekis} {nuomosUzsakymas.KlientoVardas} {nuomosUzsakymas.AutomobilisId} {nuomosUzsakymas.ElektromobilisId} {nuomosUzsakymas.BenzAutomobilisId} {nuomosUzsakymas.KlientasId} {nuomosUzsakymas.AutoTipas} {nuomosUzsakymas.DarbuotojasId}");
                }
                else
                {
                    Console.WriteLine("Nuomos uzsakymas nerastas.");
                }

                return nuomosUzsakymas;

            }
        }



        public async Task<List<NuomosUzsakymas>> GautiVisusNuomosUzsakymus()
        {
            using (var context = new MyDbContext())
            {

                List<NuomosUzsakymas> visiUzsakymai = await context.NuomosUzsakymas.ToListAsync();


                foreach (NuomosUzsakymas p in visiUzsakymai)
                {
                    Console.WriteLine($"{p.UzsakymasId} {p.NuomosPradzia} {p.DienuKiekis} {p.KlientoVardas} {p.AutomobilisId} {p.ElektromobilisId} {p.BenzAutomobilisId} {p.KlientasId} {p.AutoTipas} {p.DarbuotojasId}");
                }

                return visiUzsakymai;
            }
        }

        public async Task IstrintiUzsakyma(int id)
        {
            using (var context = new MyDbContext())
            {
                context.NuomosUzsakymas.Remove(context.NuomosUzsakymas.Find(id));
                context.SaveChanges();
            }
        }

        public async Task<NuomosUzsakymas> KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId)
        {
            using (var context = new MyDbContext())
            {

                var uzsakymas = await context.NuomosUzsakymas.FindAsync(id);

                if (uzsakymas == null) return null;

                uzsakymas.UzsakymasId = id;
                uzsakymas.KlientasId = klientasId;
                uzsakymas.AutoTipas = autoTipas;
                uzsakymas.AutomobilisId = automobilisId;
                uzsakymas.NuomosPradzia = nuomosPradzia;
                uzsakymas.DienuKiekis = dienuKiekis;
                uzsakymas.DarbuotojasId = darbuotojasId;


                await context.SaveChangesAsync();

                return uzsakymas;

            }
        }

        public async Task PridetiNaujaUzsakyma(NuomosUzsakymas nuomosUzsakymas)
        {
            using (var context = new MyDbContext())
            {
                await context.NuomosUzsakymas.AddAsync(nuomosUzsakymas);
                await context.SaveChangesAsync();
            }
        }
    }
}
