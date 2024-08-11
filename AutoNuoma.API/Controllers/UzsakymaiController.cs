using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UzsakymaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;

        public UzsakymaiController(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }

        [HttpGet("GautiVisusUzsakymus")]
        public async Task<IActionResult> Index()
        {
            var visiUzsakymai = await _autonuomaService.GautiVisusUzsakymus();
            return Ok(visiUzsakymai);
        }

        [HttpGet("RastiUzsakymaPagalId")]
        public async Task<IActionResult> GautiUzsakymaPagalId(int id)
        {
            var klId = await _autonuomaService.GautiUzsakymaPagalId(id);
            return Ok(klId);
        }

        [HttpGet("RastiUzsakymaPagalVardaPavarde")]
        public async Task<IActionResult> gautiUzsakymusPagalKlienta(string klientoVardas, string klientoPavarde)
        {
            var klId = await _autonuomaService.gautiUzsakymusPagalKlienta(klientoVardas, klientoPavarde);
            return Ok(klId);
        }


        [HttpPost("PridetiUzsakyma")]
        public async Task<IActionResult> GautiUzsakymaPagalId(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas, int darbuotojasId)
        {
            try
            {
                await _autonuomaService.SukurtiNuoma(klientasId, automobilisId, nuomosPradzia, dienuKiekis, autoTipas, darbuotojasId);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiUzsakymoInfo")]
        public async Task<IActionResult> KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, int darbuotojasId)
        {
            try
            {
                await _autonuomaService.KoreguotiNuomosInfo(id, klientasId, autoTipas, automobilisId, nuomosPradzia, dienuKiekis, darbuotojasId);
                return Ok();

            }
            catch
            {
                return Problem();
            }

        }


        [HttpDelete("IstrintiUzsakyma")]
        public async Task<IActionResult> IstrintiUzsakyma(int id)
        {
            await _autonuomaService.IstrintiUzsakyma(id);
            return Ok();
        }



    }
}
