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
        public IActionResult Index()
        {
            var visiUzsakymai = _autonuomaService.GautiVisusUzsakymus();
            return Ok(visiUzsakymai);
        }

        [HttpGet("RastiUzsakymaPagalId")]
        public IActionResult GautiUzsakymaPagalId(int id)
        {
            var klId = _autonuomaService.GautiUzsakymaPagalId(id);
            return Ok(klId);
        }

        [HttpPost("PridetiUzsakyma")]
        public IActionResult GautiUzsakymaPagalId(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas)
        {
            try
            {
                _autonuomaService.SukurtiNuoma(klientasId, automobilisId, nuomosPradzia, dienuKiekis, autoTipas);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiUzsakymoInfo")]
        public IActionResult KoreguotiNuomosInfo(int id, int klientasId, string autoTipas, int automobilisId, DateTime nuomosPradzia, int dienuKiekis)
        {
            try
            {
                _autonuomaService.KoreguotiNuomosInfo(id, klientasId, autoTipas, automobilisId, nuomosPradzia, dienuKiekis);
                return Ok();

            }
            catch
            {
                return Problem();
            }

        }


        [HttpGet("IstrintiUzsakyma")]
        public IActionResult IstrintiUzsakyma(int id)
        {
            _autonuomaService.IstrintiUzsakyma(id);
            return Ok();
        }



    }
}
