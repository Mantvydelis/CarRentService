using AutomobiliuNuoma.Core.Models;
using AutoNuoma.API.Contracts;

namespace AutoNuoma.API.Service
{
    public class CacheService : ICacheService
    {
        public List<Elektromobilis> Elektromobiliai = new List<Elektromobilis>();

        public void AddElektromobilisToCache(Elektromobilis ev)
        {
            Elektromobiliai.Add(ev);
        }
        public Elektromobilis GetElektromobilisByIdFromCache(int id)
        {
            foreach (Elektromobilis ev in Elektromobiliai)
            {
                if (ev.AutomobilisId == id)
                    return ev;
            }
            return null;
        }

    }
}
