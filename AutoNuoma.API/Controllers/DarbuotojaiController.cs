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
        private readonly IMongoDbCacheRepository _mongoDbCacheRepository;

        public DarbuotojaiController(IAutonuomaService autonuomaService, IMongoDbCacheRepository mongoDbCacheRepository)
        {
            _autonuomaService = autonuomaService;
            _mongoDbCacheRepository = mongoDbCacheRepository;
        }

        [HttpGet("GautiVisusDarbuotojus")]
        public async Task<IActionResult> Index()
        {
            var visiDarbuotojai = await _autonuomaService.GautiVisusDarbuotojus();
            return Ok(visiDarbuotojai);
        }

        [HttpPost("PridetiDarbuotoja")]
        public async Task<IActionResult> PridetiDarbuotoja(Darbuotojas darbuotojas)
        {
            try
            {
               await _autonuomaService.PridetiDarbuotoja(darbuotojas);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpGet("RastiDarbuotojaPagalId")]
        public async Task<IActionResult> GautiDarbuotojaPagalId(int id)
        {
            var darbuotojasId = await _autonuomaService.GautiDarbuotojaPagalId(id);
            return Ok(darbuotojasId);
        }

        [HttpPost("KoreguotiDarbuotojoInfo")]
        public async Task<IActionResult> KoreguotiDarbuotojoInfo(int id, string vardas, string pavarde, DarbuotojasPareigos pareigos)
        {
            try
            {
                var darbuotojasId = await _autonuomaService.KoreguotiDarbuotojoInfo(id, vardas, pavarde, pareigos);
                return Ok(darbuotojasId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpDelete("IstrintiDarbuotoja")]
        public async Task<IActionResult> IstrintiDarbuotoja(int id)
        {
            await _autonuomaService.IstrintiDarbuotoja(id);
            return Ok();
        }





    
    }
}
