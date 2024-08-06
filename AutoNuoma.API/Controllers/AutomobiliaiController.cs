using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutoNuoma.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutomobiliaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;
        private readonly ICacheService _cache;

        public AutomobiliaiController(IAutonuomaService autonuomaService, ICacheService cacheService)
        {
            _autonuomaService = autonuomaService;
            _cache = cacheService;
        }

        [HttpGet("GautiVisusElektromobilius")]
        public IActionResult Index()
        {
            var visiEv = _autonuomaService.GautiVisusElektromobilius();
            return Ok(visiEv);
        }

        [HttpGet("GautiVisusNaftosAuto")]
        public IActionResult GautiVisusNaftosKuroAuto()
        {
            var visiNaftaAuto = _autonuomaService.GautiVisusNaftosKuroAuto();
            return Ok(visiNaftaAuto);
        }



        //Rodyti visus automobilius
        //Prideti automobili i duombaze
        //Pakeisti duomenis automobiliu duombazeje
        //Istrinti automobili is duombazes








        [HttpGet("GautiElektromobiliPagalId")]
        public IActionResult GautiElektromobiliPagalId(int id)
        {
            var ev = _cache.GetElektromobilisByIdFromCache(id);
            if (ev != null)
                return Ok(ev);

            var visiEv = _autonuomaService.GautiVisusElektromobilius();
            foreach (Elektromobilis e in visiEv)
            {
                _cache.AddElektromobilisToCache(e);
            }
            foreach (Elektromobilis a in visiEv)
            {
                if (a.AutomobilisId == id)
                    return Ok(a);
            }

            return NotFound();
        }
        [HttpGet("GautiElektromobiliPagalIdBeStatuso")]
        public Elektromobilis GautiElektromobiliPagalIdBeStatuso(int id)
        {
            var ev = _cache.GetElektromobilisByIdFromCache(id);
            if (ev != null)
                return ev;

            var visiEv = _autonuomaService.GautiVisusElektromobilius();
            foreach (Elektromobilis e in visiEv)
            {
                _cache.AddElektromobilisToCache(e);
            }
            foreach (Elektromobilis a in visiEv)
            {
                if (a.AutomobilisId == id)
                    return a;
            }

            return null;
        }
        [HttpPost("PridetiElektromobili")]
        public IActionResult GautiElektromobiliPagalId(Elektromobilis ev)
        {
            try
            {
                _autonuomaService.PridetiNaujaAutomobili(ev);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }
    }
}
