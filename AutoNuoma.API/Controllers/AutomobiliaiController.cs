using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutomobiliaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;


        public AutomobiliaiController(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
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

        [HttpGet("RastiElektromobiliPagalId")]
        public IActionResult GautiElektromobiliPagalId(int id)
        {
            var elId = _autonuomaService.GautiElektromobiliPagalId(id);
            return Ok(elId);
        }

        [HttpGet("RastiNaftosKuroAutoPagalId")]
        public IActionResult GautiNaftosAutoPagalId(int id)
        {
            var naId = _autonuomaService.GautiNaftosAutoPagalId(id);
            return Ok(naId);
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

        [HttpPost("PridetiNaftosKuroAuto")]
        public IActionResult GautiNaftosAutoPagalId(NaftosKuroAutomobilis benz)
        {
            try
            {
                _autonuomaService.PridetiNaujaAutomobili(benz);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiElektromobilioInfo")]
        public IActionResult KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            try
            {
                var elId = _autonuomaService.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
                return Ok(elId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiNaftosAutoInfo")]
        public IActionResult KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            try
            {
                var naftaId = _autonuomaService.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
                return Ok(naftaId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpGet("IstrintiElektromobili")]
        public IActionResult IstrintiElektromobili(int id)
        {
            _autonuomaService.IstrintiElektromobili(id);
            return Ok();
        }

        [HttpGet("IstrintiNaftosKuroAuto")]
        public IActionResult IstrintiNaftaAuto(int id)
        {
            _autonuomaService.IstrintiNaftaAuto(id);
            return Ok();
        }

    }
}
