using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class KlientaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;

        public KlientaiController(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }

        [HttpGet("GautiVisusKlientus")]
        public IActionResult Index()
        {
            var visiKlientai = _autonuomaService.GautiVisusKlientus();
            return Ok(visiKlientai);
        }

        [HttpGet("RastiKlientaPagalId")]
        public IActionResult GautiKlientaPagalId(int id)
        {
            var klId = _autonuomaService.GautiKlientaPagalId(id);
            return Ok(klId);
        }


        [HttpPost("PridetiKlienta")]
        public IActionResult GautiKlientaPagalId(Klientas klientas)
        {
            try
            {
                _autonuomaService.PridetiNaujaKlienta(klientas);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiKlientoInfo")]
        public IActionResult KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            try
            {
                var klientasId = _autonuomaService.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);
                return Ok(klientasId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpDelete("IstrintiKlienta")]
        public IActionResult IstrintiKlienta(int id)
        {
            _autonuomaService.IstrintiKlienta(id);
            return Ok();
        }













    }
}
