using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomobiliuNuoma.FrontEnd.Pages
{
    public class NuomosUzsakymaiModel : PageModel
    {
        private readonly IAutonuomaService _autonuomaService;
        public List<NuomosUzsakymas> NuomosUzsakymai;

        public NuomosUzsakymaiModel(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }
        public async Task OnGet()
        {

            NuomosUzsakymai = await _autonuomaService.GautiVisusUzsakymus();

        }
    }
}
