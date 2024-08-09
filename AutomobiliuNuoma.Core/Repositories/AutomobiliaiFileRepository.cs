using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class AutomobiliaiFileRepository : IAutomobiliaiRepository
    {
        private readonly string _filePath;
        public AutomobiliaiFileRepository(string autoFilePath)
        {
            _filePath = autoFilePath;
        }

        public Task<NaftosKuroAutomobilis> GautiNaftosAuto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Elektromobilis>> GautiVisusElektromobilius()
        {
            List<Elektromobilis> visiAuto = new List<Elektromobilis>();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    string eilute = sr.ReadLine();
                    string[] vertes = eilute.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (vertes.Length == 6)
                        visiAuto.Add(new Elektromobilis(int.Parse(vertes[0]), vertes[1], vertes[2], decimal.Parse(vertes[3]),
                            int.Parse(vertes[4]), int.Parse(vertes[5])));
                }
            }
            return visiAuto;
        }

        public async Task<List<NaftosKuroAutomobilis>> GautiVisusNaftosKuroAutomobilius()
        {
            throw new NotImplementedException();
        }

        public async Task IrasytiAutomobilius()
        {
            throw new NotImplementedException();
        }

        public async Task IrasytiElektromobili(Elektromobilis elektromobilis)
        {
            throw new NotImplementedException();
        }

        public async Task IrasytiNaftosKuroAutomobili(NaftosKuroAutomobilis automobilis)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Automobilis>> NuskaitytiAutomobilius()
        {
            List<Automobilis> visiAuto = new List<Automobilis>();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    string eilute = sr.ReadLine();
                    string[] vertes = eilute.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (vertes.Length == 6)
                        visiAuto.Add(new Elektromobilis(int.Parse(vertes[0]), vertes[1], vertes[2], decimal.Parse(vertes[3]),
                            int.Parse(vertes[4]), int.Parse(vertes[5])));
                    else
                        visiAuto.Add(new NaftosKuroAutomobilis(int.Parse(vertes[0]), vertes[1], vertes[2], decimal.Parse(vertes[3]),
                            double.Parse(vertes[4])));
                }
            }
            return visiAuto;
        }

        public async Task<Elektromobilis> GautiElektromobiliPagalId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<NaftosKuroAutomobilis> GautiNaftosAutoPagalId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<NaftosKuroAutomobilis> KoreguotiNaftaAutoInfo(int id, string marke, string modelis, decimal nuomosKaina, double degaluSanaudos)
        {
            throw new NotImplementedException();
        }

        public async Task<Elektromobilis> KoreguotiElektromobilioInfo(int id, string marke, string modelis, decimal nuomosKaina, int baterijosTalpa, int krovimoLaikas)
        {
            throw new NotImplementedException();
        }

        public async Task IstrintiNaftaAuto(int id)
        {
            throw new NotImplementedException();
        }

        public async Task IstrintiElektromobili(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Automobilis> PaieskaPagalMarke(string marke)
        {
            throw new NotImplementedException();
        }

        Task<List<Elektromobilis>> IAutomobiliaiRepository.GautiVisusElektromobilius()
        {
            throw new NotImplementedException();
        }

        Task<List<NaftosKuroAutomobilis>> IAutomobiliaiRepository.GautiVisusNaftosKuroAutomobilius()
        {
            throw new NotImplementedException();
        }

        public Task<int> GautiElektromobiliuSkaiciu()
        {
            throw new NotImplementedException();
        }

        public Task<int> GautiNaftosKuroAutoSkaiciu()
        {
            throw new NotImplementedException();
        }
    }
}
