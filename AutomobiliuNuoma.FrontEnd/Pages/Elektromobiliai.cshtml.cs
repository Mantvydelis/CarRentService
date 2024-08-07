using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomobiliuNuoma.FrontEnd.Pages
{
    public class ElektromobiliaiModel : PageModel
    {
        private readonly IAutonuomaService _autonuomaService;
        public List<Elektromobilis> Elektromobiliai;

        public ElektromobiliaiModel(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }


        public void OnGet()
        {
            Elektromobiliai = _autonuomaService.GautiVisusElektromobilius();
        }
    }
}
