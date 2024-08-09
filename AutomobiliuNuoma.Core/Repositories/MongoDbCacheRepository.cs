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

        public MongoDbCacheRepository(IMongoClient mongoClient)
        {
            _darbuotojaiCache = mongoClient.GetDatabase("darbuotojai").GetCollection<Darbuotojas>("darbuotojai_cache");
            _klientaiCache = mongoClient.GetDatabase("klientai").GetCollection<Klientas>("klientai_cache");
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





    }
}
