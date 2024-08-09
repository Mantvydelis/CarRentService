using AutomobiliuNuoma.Core.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AutomobiliuNuoma.Core.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMongoDbCacheRepository _mongoCache;

        public CacheService(IMongoDbCacheRepository mongoDbCache)
        {
            _mongoCache = mongoDbCache;
        }
        public async Task DeleteCaches()
        {
            while (true)
            {
                Console.WriteLine("Cache bus istrinta po minutes");
                await Task.Delay(TimeSpan.FromMinutes(1));
                var erase = _mongoCache.IstrintiCache();

                await Task.WhenAll(erase);
                Console.WriteLine("Duombazes istrintos");

            }

        }



    }
}
