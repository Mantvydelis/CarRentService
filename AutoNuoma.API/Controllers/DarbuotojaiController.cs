using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Enums;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DarbuotojaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;
        public DarbuotojaiController(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }

        [HttpGet("GautiVisusDarbuotojus")]
        public IActionResult Index()
        {
            var visiDarbuotojai = _autonuomaService.GautiVisusDarbuotojus();
            return Ok(visiDarbuotojai);
        }

        [HttpPost("PridetiDarbuotoja")]
        public IActionResult PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            try
            {
                _autonuomaService.PridetiDarbuotoja(darbuotojas);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpGet("RastiDarbuotojaPagalId")]
        public IActionResult GautiKlientaPagalId(int id)
        {
            var darbuootojasId = _autonuomaService.GautiDarbuotojaPagalId(id);
            return Ok(darbuootojasId);
        }

        [HttpPost("KoreguotiDarbuotojoInfo")]
        public IActionResult KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {
            try
            {
                var darbuotojasId = _autonuomaService.KoreguotiDarbuotojoInfo(id, vardas, pavarde, pareigos);
                return Ok(darbuotojasId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpDelete("IstrintiDarbuotoja")]
        public IActionResult IstrintiDarbuotoja(int id)
        {
            _autonuomaService.IstrintiDarbuotoja(id);
            return Ok();
        }





        //KoreguotiDarbuotojoInfo
    }
}
