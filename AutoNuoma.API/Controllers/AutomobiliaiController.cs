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
        private readonly IAutomobiliaiService _automobiliaiService;



        public AutomobiliaiController(IAutonuomaService autonuomaService, IAutomobiliaiService automobiliaiService)
        {
            _autonuomaService = autonuomaService;
            _automobiliaiService = automobiliaiService;
        }



        [HttpGet("GautiVisusElektromobilius")]
        public async Task<IActionResult> Index()
        {
            var visiEv = await _autonuomaService.GautiVisusElektromobilius();
            return Ok(visiEv);
        }

        [HttpGet("GautiVisusNaftosAuto")]
        public async Task<IActionResult> GautiVisusNaftosKuroAuto()
        {
            var visiNaftaAuto = await _autonuomaService.GautiVisusNaftosKuroAuto();
            return Ok(visiNaftaAuto);
        }

        [HttpGet("RastiElektromobiliPagalId")]
        public async Task<IActionResult> GautiElektromobiliPagalId(int id)
        {
            var elId = await _autonuomaService.GautiElektromobiliPagalId(id);
            return Ok(elId);
        }

        [HttpGet("RastiNaftosKuroAutoPagalId")]
        public async Task<IActionResult> GautiNaftosAutoPagalId(int id)
        {
            var naId = await _autonuomaService.GautiNaftosAutoPagalId(id);
            return Ok(naId);
        }


        [HttpPost("PridetiElektromobili")]
        public async Task<IActionResult> GautiElektromobiliPagalId(Elektromobilis ev)
        {
            try
            {
                await _autonuomaService.PridetiNaujaAutomobili(ev);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("PridetiNaftosKuroAuto")]
        public async Task<IActionResult> GautiNaftosAutoPagalId(NaftosKuroAutomobilis benz)
        {
            try
            {
                await _autonuomaService.PridetiNaujaAutomobili(benz);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiElektromobilioInfo")]
        public async Task<IActionResult> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            try
            {
                var elId = await _autonuomaService.KoreguotiElektromobilioInfo(id, marke, modelis, nuomosKaina, baterijosTalpa, krovimoLaikas);
                return Ok(elId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiNaftosAutoInfo")]
        public async Task<IActionResult> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            try
            {
                var naftaId = await _autonuomaService.KoreguotiNaftaAutoInfo(id, marke, modelis, nuomosKaina, degaluSanaudos);
                return Ok(naftaId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpDelete("IstrintiElektromobili")]
        public async Task<IActionResult> IstrintiElektromobili(int id)
        {
            await _autonuomaService.IstrintiElektromobili(id);
            return Ok();
        }

        [HttpDelete("IstrintiNaftosKuroAuto")]
        public async Task<IActionResult> IstrintiNaftaAuto(int id)
        {
            await _autonuomaService.IstrintiNaftaAuto(id);
            return Ok();
        }


        [HttpGet("GautiElektromobiliuSkaiciu")]
        public async Task<IActionResult> GautiElektromobiliuSkaiciu()
        {
            var elId = await _automobiliaiService.GautiElektromobiliuSkaiciu();
            return Ok(elId);
        }

        [HttpGet("GautiNaftosKuroAutomobiliuSkaiciu")]
        public async Task<IActionResult> GautiNaftosKuroAutoSkaiciu()
        {
            var naId = await _automobiliaiService.GautiNaftosKuroAutoSkaiciu();
            return Ok(naId);
        }


    }
}
