using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomobiliuNuoma.FrontEnd.Pages
{
    public class KlientaiModel : PageModel
    {
        private readonly IAutonuomaService _autonuomaService;
        public List<Klientas> Klientai;

        public KlientaiModel(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }
        public void OnGet()
        {
            Klientai = _autonuomaService.GautiVisusKlientus();
        }
    }
}
