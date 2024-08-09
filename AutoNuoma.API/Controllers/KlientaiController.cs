using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AutoNuoma.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class KlientaiController : Controller
    {
        private readonly IAutonuomaService _autonuomaService;
        private readonly IMongoDbCacheRepository _mongoDbCacheRepository;

        public KlientaiController(IAutonuomaService autonuomaService, IMongoDbCacheRepository mongoDbCacheRepository )
        {
            _autonuomaService = autonuomaService;
            _mongoDbCacheRepository = mongoDbCacheRepository;
        }

        [HttpGet("GautiVisusKlientus")]
        public async Task<IActionResult> Index()
        {
            var visiKlientai = await _autonuomaService.GautiVisusKlientus();
            return Ok(visiKlientai);
        }

        [HttpGet("RastiKlientaPagalId")]
        public async Task<IActionResult> GautiKlientaPagalId(int id)
        {
            var klientas = await _autonuomaService.GautiKlientaPagalId(id);
            return Ok(klientas);
 
        }


        [HttpPost("PridetiKlienta")]
        public async Task<IActionResult> GautiKlientaPagalId(Klientas klientas)
        {

            try
            {
                await _autonuomaService.PridetiNaujaKlienta(klientas);
                return Ok();
            }
            catch
            {
                return Problem();
            }

        }

        [HttpPost("KoreguotiKlientoInfo")]
        public async Task<IActionResult> KoreguotiKlientoInfo(int id, string vardas, string pavarde, DateOnly gimimoMetai)
        {
            try
            {
                var klientasId = await _autonuomaService.KoreguotiKlientoInfo(id, vardas, pavarde, gimimoMetai);
                return Ok(klientasId);

            }
            catch
            {
                return Problem();
            }

        }

        [HttpDelete("IstrintiKlienta")]
        public async Task<IActionResult> IstrintiKlienta(int id)
        {
            await _autonuomaService.IstrintiKlienta(id);
            return Ok();
        }













    }
}
