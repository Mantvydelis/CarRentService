using AutomobiliuNuoma.Core.Models;

namespace AutoNuoma.API.Contracts
{
    public interface ICacheService
    {
        void AddElektromobilisToCache(Elektromobilis ev);
        Elektromobilis GetElektromobilisByIdFromCache(int id);


    }
}
