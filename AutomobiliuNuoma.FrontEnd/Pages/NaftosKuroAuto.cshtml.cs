using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomobiliuNuoma.FrontEnd.Pages
{
    public class NaftosKuroAutoModel : PageModel
    {
        private readonly IAutonuomaService _autonuomaService;
        public List<NaftosKuroAutomobilis> NaftosKuroAutomobiliai;

        public NaftosKuroAutoModel(IAutonuomaService autonuomaService)
        {
            _autonuomaService = autonuomaService;
        }
        public async Task OnGet()
        {

            NaftosKuroAutomobiliai = await _autonuomaService.GautiVisusNaftosKuroAuto();

        }
    }
}
