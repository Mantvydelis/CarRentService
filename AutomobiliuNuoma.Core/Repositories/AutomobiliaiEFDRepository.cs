using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class AutomobiliaiEFDRepository : IAutomobiliaiRepository
    {
        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {
            using (var context = new MyDbContext())
            {

                var elektromobilis = await context.Elektromobiliai.FindAsync(id);

                if (elektromobilis != null)
                {
                    Console.WriteLine($"{elektromobilis.AutomobilisId} {elektromobilis.Marke} {elektromobilis.Modelis} {elektromobilis.NuomosKaina} {elektromobilis.BaterijosTalpa} {elektromobilis.KrovimoLaikas}");
                }
                else
                {
                    Console.WriteLine("Elektromobilis nerastas.");
                }

                return elektromobilis;

            }
        }

        public Task<int> GautiElektromobiliuSkaiciu()
        {
            using (var context = new MyDbContext())
            {
                return Task.FromResult(context.Elektromobiliai.Count());
            }
        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id)
        {
            using (var context = new MyDbContext())
            {

                var naftosKuroAuto = await context.NaftosKuroAuto.FindAsync(id);

                if (naftosKuroAuto != null)
                {
                    Console.WriteLine($"{naftosKuroAuto.AutomobilisId} {naftosKuroAuto.Marke} {naftosKuroAuto.Modelis} {naftosKuroAuto.NuomosKaina} {naftosKuroAuto.DegaluSanaudos}");
                }
                else
                {
                    Console.WriteLine("Automobilis nerastas.");
                }

                return naftosKuroAuto;

            }
        }

        public Task<int> GautiNaftosKuroAutoSkaiciu()
        {
            using (var context = new MyDbContext())
            {
                return Task.FromResult(context.NaftosKuroAuto.Count());
            }
        }

        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {

            using (var context = new MyDbContext())
            {
              
                List<Elektromobilis> visiElektromobiliai = await context.Elektromobiliai.ToListAsync();

               
                foreach (Elektromobilis p in visiElektromobiliai)
                {
                    Console.WriteLine($"{p.AutomobilisId} {p.Marke} {p.Modelis} {p.NuomosKaina} {p.BaterijosTalpa} {p.KrovimoLaikas}");
                }

                return visiElektromobiliai; 
            }

        }

        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAutomobilius()
        {
            using (var context = new MyDbContext())
            {

                List<NaftosKuroAutomobilis> visiNaftosAuto = await context.NaftosKuroAuto.ToListAsync();


                foreach (NaftosKuroAutomobilis p in visiNaftosAuto)
                {
                    Console.WriteLine($"{p.AutomobilisId} {p.Marke} {p.Modelis} {p.NuomosKaina} {p.DegaluSanaudos}");
                }

                return visiNaftosAuto;
            }
        }

        public Task IrasytiAutomobilius()
        {
            throw new NotImplementedException();
        }

        public async Task IrasytiElektromobili(Elektromobilis elektromobilis)
        {

            using (var context = new MyDbContext())
            {
                await context.Elektromobiliai.AddAsync(elektromobilis);
                await context.SaveChangesAsync();
            }


        }

        public async Task IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis automobilis)
        {
            using (var context = new MyDbContext())
            {
                await context.NaftosKuroAuto.AddAsync(automobilis);
                await context.SaveChangesAsync();
            }
        }

        public async Task IstrintiElektromobili(int id)
        {
            using (var context = new MyDbContext())
            {
                context.Elektromobiliai.Remove(context.Elektromobiliai.Find(id));
                context.SaveChanges();
            }
        }

        public async Task IstrintiNaftaAuto(int id)
        {
            using (var context = new MyDbContext())
            {
                context.NaftosKuroAuto.Remove(context.NaftosKuroAuto.Find(id));
                context.SaveChanges();
            }
        }

        public async Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {

            using (var context = new MyDbContext())
            {

                    var elektromobilis = await context.Elektromobiliai.FindAsync(id);

                    if (elektromobilis == null) return null;

                    elektromobilis.Marke = marke;
                    elektromobilis.Modelis = modelis;
                    elektromobilis.NuomosKaina = nuomosKaina;
                    elektromobilis.BaterijosTalpa = baterijosTalpa;
                    elektromobilis.KrovimoLaikas = krovimoLaikas;

                    await context.SaveChangesAsync();

                    return elektromobilis;

            }
            

        }
        public async Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            using (var context = new MyDbContext())
            {

                var naftosKuroAuto = await context.NaftosKuroAuto.FindAsync(id);

                if (naftosKuroAuto == null) return null;

                naftosKuroAuto.Marke = marke;
                naftosKuroAuto.Modelis = modelis;
                naftosKuroAuto.NuomosKaina = nuomosKaina;
                naftosKuroAuto.DegaluSanaudos = degaluSanaudos;

                await context.SaveChangesAsync();

                return naftosKuroAuto;

            }
        }

        public Task<List<Automobilis>> NuskaitytiAutomobilius()
        {
            throw new NotImplementedException();
        }
    }
}
