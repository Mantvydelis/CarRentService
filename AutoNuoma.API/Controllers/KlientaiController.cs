using AutomobiliuNuoma.Core.Contracts;
using AutoNuoma.API.Contracts;
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


        //Rodyti visus klientus is duombazes
        //Prideti klienta i duombaze
        //Pakeisti duomenis klientu duombazeje
        //Istrinti klienta is duombazes






    }
}
