using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomobiliuNuoma.FrontEnd.Pages
{
    public class DarbuotojaiModel : PageModel
    {
        private readonly IAutonuomaService _autonuomaService;
        public List<Darbuotojas> Darbuotojai;

        public DarbuotojaiModel(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }
        public void OnGet()
        {
            Darbuotojai = _autonuomaService.GautiVisusDarbuotojus();
        }
    }
}
