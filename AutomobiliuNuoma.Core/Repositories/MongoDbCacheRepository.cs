using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class MongoDbCacheRepository : IMongoDbCacheRepository
    {
        private IMongoCollection<Darbuotojas> _darbuotojaiCache;
        private IMongoCollection<Klientas> _klientaiCache;
        private IMongoCollection<Elektromobilis> _elektromobiliaiCache;
        private IMongoCollection<NaftosKuroAutomobilis> _naftosKuroAutoCache;
        private IMongoCollection<NuomosUzsakymas> _nuomosUzsakymasCache;

        public MongoDbCacheRepository(IMongoClient mongoClient)
        {
            _darbuotojaiCache = mongoClient.GetDatabase("darbuotojai").GetCollection<Darbuotojas>("darbuotojai_cache");
            _klientaiCache = mongoClient.GetDatabase("klientai").GetCollection<Klientas>("klientai_cache");
            _elektromobiliaiCache = mongoClient.GetDatabase("elektromobiliai").GetCollection<Elektromobilis>("elektromobiliai_cache");
            _naftosKuroAutoCache = mongoClient.GetDatabase("naftosKuroAuto").GetCollection<NaftosKuroAutomobilis>("naftosKuroAuto_cache");
            _nuomosUzsakymasCache = mongoClient.GetDatabase("nuomosUzsakymai").GetCollection<NuomosUzsakymas>("nuomosUzsakymai_cache");
        }


        public async Task PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            await _darbuotojaiCache.InsertOneAsync(darbuotojas);
        }

        public async Task PridetiKlienta(Klientas klientas)
        {
            await _klientaiCache.InsertOneAsync(klientas);
        }



        public async Task<Darbuotojas> GautiDarbuotojaPagalId(int id)
        {
            try
            {
                return (await _darbuotojaiCache.FindAsync<Darbuotojas>(x => x.Id == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Klientas> GautiKlientaPagalId(int id)
        {
            try
            {
                return (await _klientaiCache.FindAsync<Klientas>(x => x.KlientasId == id)).First();
            }
            catch
            {
                return null;

            }

        }

        public async Task<Darbuotojas> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {

            var filter = Builders<Darbuotojas>.Filter.Eq(d => d.Id, id);

            var update = Builders<Darbuotojas>.Update
                 .Set(d => d.Vardas, vardas)
                 .Set(d => d.Pavarde, pavarde)
                 .Set(d => d.Pareigos, pareigos);

            var result = await _darbuotojaiCache.UpdateOneAsync(filter, update);

            if (result.MatchedCount > 0)
            {
                return await _darbuotojaiCache.Find(filter).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }

        }


        public async Task<Klientas> KoreguotiKlientoInfo(int klientasId, string vardas, string pavarde, DateOnly gimimoMetai)
        {

            var filter = Builders<Klientas>.Filter.Eq(d => d.KlientasId, klientasId);

            var update = Builders<Klientas>.Update
                 .Set(d => d.Vardas, vardas)
                 .Set(d => d.Pavarde, pavarde)
                 .Set(d => d.GimimoMetai, gimimoMetai);

            var result = await _klientaiCache.UpdateOneAsync(filter, update);

            if (result.MatchedCount > 0)
            {
                return await _klientaiCache.Find(filter).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }

        }

        public async Task<List<Darbuotojas>> GautiVisusDarbuotojus()
        {
            try
            {

                var allDarbuotojai = await _darbuotojaiCache.FindAsync<Darbuotojas>(_ => true);
                return await allDarbuotojai.ToListAsync();
            }
            catch
            {
                return null;
            }
        }


        public async Task<List<Klientas>> GautiVisusKlientus()
        {
            try
            {

                var allKlientai = await _klientaiCache.FindAsync<Klientas>(_ => true);
                return await allKlientai.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task IstrintiDarbuotoja(int id)
        {

            await _darbuotojaiCache.DeleteOneAsync<Darbuotojas>(x => x.Id == id);


        }

        public async Task IstrintiKlienta(int klientasId)
        {
            await _klientaiCache.DeleteOneAsync<Klientas>(x => x.KlientasId == klientasId);


        }

        public async Task IstrintiCache()
        {
            var istrintiKlientus = _klientaiCache.Database.DropCollectionAsync("klientai_cache");
            var istrintiDarbuotojus = _darbuotojaiCache.Database.DropCollectionAsync("darbuotojai_cache");
            var istrintiElektromobilius = _elektromobiliaiCache.Database.DropCollectionAsync("elektromobiliai_cache");
            var istrintiNaftosKuroAuto = _naftosKuroAutoCache.Database.DropCollectionAsync("naftosKuroAuto_cache");
            var istrintiUzsakymus = _nuomosUzsakymasCache.Database.DropCollectionAsync("nuomosUzsakymai_cache");

            await Task.WhenAll(istrintiDarbuotojus, istrintiKlientus, istrintiElektromobilius, istrintiNaftosKuroAuto, istrintiUzsakymus);
        }


        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {
            try
            {
                return (await _elektromobiliaiCache.FindAsync<Elektromobilis>(x => x.AutomobilisId == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosKuroAutoPagalId(int id)
        {
            try
            {
                return (await _naftosKuroAutoCache.FindAsync<NaftosKuroAutomobilis>(x => x.AutomobilisId == id)).First();
            }
            catch
            {
                return null;
            }
        }

        public async Task PridetiElektromobili(Elektromobilis elektromobilis)
        {
            await _elektromobiliaiCache.InsertOneAsync(elektromobilis);
        }

        public async Task PridetiNaftosKuroAuto(NaftosKuroAutomobilis naftosKuroAutomobilis)
        {
            await _naftosKuroAutoCache.InsertOneAsync(naftosKuroAutomobilis);
        }



        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {
            try
            {

                var allElektromobiliai = await _elektromobiliaiCache.FindAsync<Elektromobilis>(_ => true);
                return await allElektromobiliai.ToListAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAuto()
        {
            try
            {

                var allNaftosKuroAuto = await _naftosKuroAutoCache.FindAsync<NaftosKuroAutomobilis>(_ => true);
                return await allNaftosKuroAuto.ToListAsync();
            }
            catch
            {
                return null;
            }
        }


        public async Task<Elektromobilis> KoreguotiElektromobilioInfo(int automobilisId, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {

            var filter = Builders<Elektromobilis>.Filter.Eq(d => d.AutomobilisId, automobilisId);

            var update = Builders<Elektromobilis>.Update
                 .Set(d => d.Marke, marke)
                 .Set(d => d.Modelis, modelis)
                 .Set(d => d.NuomosKaina, nuomosKaina)
                 .Set(d => d.BaterijosTalpa, baterijosTalpa)
                 .Set(d => d.KrovimoLaikas, krovimoLaikas);

            var result = await _elektromobiliaiCache.UpdateOneAsync(filter, update);

            if (result.MatchedCount > 0)
            {
                return await _elektromobiliaiCache.Find(filter).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }

        }


        public async Task<NaftosKuroAutomobilis> KoreguotiNaftosKuroAutoInfo(int automobilisId, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {

            var filter = Builders<NaftosKuroAutomobilis>.Filter.Eq(d => d.AutomobilisId, automobilisId);

            var update = Builders<NaftosKuroAutomobilis>.Update
                 .Set(d => d.Marke, marke)
                 .Set(d => d.Modelis, modelis)
                 .Set(d => d.NuomosKaina, nuomosKaina)
                 .Set(d => d.DegaluSanaudos, degaluSanaudos);

            var result = await _naftosKuroAutoCache.UpdateOneAsync(filter, update);

            if (result.MatchedCount > 0)
            {
                return await _naftosKuroAutoCache.Find(filter).FirstOrDefaultAsync();
            }
            else
            {
                return null;
            }

        }






        public async Task PridetiUzsakyma(NuomosUzsakymas uzsakymas)
        {
            await _nuomosUzsakymasCache.InsertOneAsync(uzsakymas);
        }


        public async Task<List<NuomosUzsakymas>> GautiVisusNuomosUzsakymus()
        {
            try
            {

                var visiUzsakymai = await _nuomosUzsakymasCache.FindAsync<NuomosUzsakymas>(_ => true);
                return await visiUzsakymai.ToListAsync();
            }
            catch
            {
                return null;
            }
        }


        public async Task<long> GautiElektromobiliuSkaiciu()
        {
            return await _elektromobiliaiCache.CountDocumentsAsync(x => true);

        }






    }
}
