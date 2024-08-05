using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using System;
using System.Linq;

namespace AutomobiliuNuomaConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        IAutonuomaService autonuomaService = SetupDependencies();
        while (true)
        {
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1. Rodyti visus automobilius (KOL KAS NEVEIKIA)");
            Console.WriteLine("2. Rodyti visus klientus");
            Console.WriteLine("3. Rodyti visus elektromobilius is duombazes");
            Console.WriteLine("4. Rodyti visus naftos kuro automobilius is duombazes");
            Console.WriteLine("5. Prideti automobili i duombaze");
            Console.WriteLine("6. Rodyti visus klientus is duombazes");
            Console.WriteLine("7. Prideti klienta i duombaze");
            Console.WriteLine("8. Gauti visus uzsakymus is duombazes");
            Console.WriteLine("9. Formuoti nuomos uzsakyma i duombaze");
            Console.WriteLine("10. Pakeisti duomenis automobiliu duombazeje");
            Console.WriteLine("11. Pakeisti duomenis klientu duombazeje");
            Console.WriteLine("12. Pakeisti uzsakymu duomenis duombazeje");

            string pasirinkimas = Console.ReadLine();
            switch (pasirinkimas)
            {
                case "1": /*neveikia*/
                    List<Automobilis> auto = autonuomaService.GautiVisusAutomobilius();
                    foreach (Automobilis a in auto)
                    {
                        Console.WriteLine(a);
                    }
                    break;
                case "2": /*PERMETA I DUOMBAZE, O NE LISTA*/
                    List<Klientas> klientai = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas k in klientai)
                    {
                        Console.WriteLine(k);
                    }
                    break;
                case "3": /*duombaze*/
                    List<Elektromobilis> elektromobiliai = autonuomaService.GautiVisusElektromobilius();
                    foreach (Elektromobilis ev in elektromobiliai)
                    {
                        Console.WriteLine(ev);
                    }
                    break;
                case "4": /*duombaze*/
                    List<NaftosKuroAutomobilis> naftosKuroAutomobiliai = autonuomaService.GautiVisusNaftosKuroAuto();
                    foreach (NaftosKuroAutomobilis v in naftosKuroAutomobiliai)
                    {
                        Console.WriteLine(v);
                    }
                    break;

                case "5": /*duombaze*/
                    Automobilis naujasAuto = new Automobilis();
                    int ikrovimoLaikas = 0;
                    int baterijosTalpa = 0;
                    double kuroSanaudos = 0;
                    Console.WriteLine("Elektromobilis - 1  Naftos Kuro Auto - 2: ");
                    string tipas = Console.ReadLine();
                    switch (tipas)
                    {
                        case "1":
                            Console.WriteLine("Iveskite Ikrovimo laika");
                            ikrovimoLaikas = int.Parse(Console.ReadLine());
                            Console.WriteLine("Iveskite Baterijos talpa");
                            baterijosTalpa = int.Parse(Console.ReadLine());
                            break;
                        case "2":
                            Console.WriteLine("Iveskite kuro sanaudas");
                            kuroSanaudos = double.Parse(Console.ReadLine());
                            break;
                    }
                    Console.WriteLine("Iveskite marke");
                    string marke = Console.ReadLine();
                    Console.WriteLine("Iveskite modeli");
                    string modelis = Console.ReadLine();
                    Console.WriteLine("Iveskite nuomos kaina");
                    decimal nuomosKaina = decimal.Parse(Console.ReadLine());
                    switch (tipas)
                    {
                        case "1":
                            naujasAuto = new Elektromobilis(marke, modelis, nuomosKaina, baterijosTalpa, ikrovimoLaikas);
                            break;
                        case "2":
                            naujasAuto = new NaftosKuroAutomobilis(marke, modelis, nuomosKaina, kuroSanaudos);
                            break;
                    }
                    autonuomaService.PridetiNaujaAutomobili(naujasAuto);

                    break;

                case "6": /*duombaze*/
                    List<Klientas> klientaiDB = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas kl in klientaiDB)
                    {
                        Console.WriteLine(kl);
                    }
                    break;

                case "7":

                    Console.WriteLine("Iveskite kliento varda");
                    string klientoVardas = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento pavarde");
                    string klientoPavarde = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento gimimo data (yyyy-mm-dd)");
                    DateOnly klientoGimimoData = DateOnly.Parse(Console.ReadLine());

                    Klientas naujasKlientas = new Klientas(klientoVardas, klientoPavarde, klientoGimimoData);

                    autonuomaService.PridetiNaujaKlienta(naujasKlientas);

                    break;

                case "8": /*gauti uzsakymus is duombazes*/
                    var uzsakymai = autonuomaService.GautiVisusUzsakymus();
                    if (uzsakymai.Count == 0)
                    {
                        Console.WriteLine("Nera uzsakymu.");
                    }
                    else
                    {
                        var naftosKuroAutomobiliai2 = autonuomaService.GautiVisusNaftosKuroAuto();
                        var elektromobiliai2 = autonuomaService.GautiVisusElektromobilius();
                        var klientai2 = autonuomaService.GautiVisusKlientus();

                        foreach (var uzsakymas in uzsakymai)
                        {
                            var klientas = klientai2.FirstOrDefault(k => k.KlientasId == uzsakymas.KlientasId);

                            Automobilis automobilis = null;

                            if (uzsakymas.AutoTipas == "NaftosKuroAutomobilis")
                            {
                                automobilis = naftosKuroAutomobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }
                            else if (uzsakymas.AutoTipas == "Elektromobilis")
                            {
                                automobilis = elektromobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }

                            if (automobilis != null)
                            {
                                Console.WriteLine($"{uzsakymas.UzsakymasId}. Uzsakovas: {klientas.Vardas} {klientas.Pavarde}, Uzsakytas automobilis: {automobilis.Marke} {automobilis.Modelis}, Nuomos Pradzia: {uzsakymas.NuomosPradzia.ToShortDateString()}, Dienu Kiekis: {uzsakymas.DienuKiekis} Pabaigos Data: {uzsakymas.gautiPabaigosData().ToShortDateString()}");
                            }

                        }
                    }
                    break;

                case "9": /*Prideti prie duombazes uzsakyma*/

                    Console.WriteLine("Pasirinkite kliento id:");
                    var visiKlientai = autonuomaService.GautiVisusKlientus();
                    foreach (var k in visiKlientai) Console.WriteLine($"{k.KlientasId} {k.Vardas} {k.Pavarde}");
                    int klientasId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Iveskite nuomos pradzios data (yyyy-mm-dd):");
                    DateTime nuomosPradzia = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Iveskite dienu kieki:");
                    int nuomosDienuKiekis = int.Parse(Console.ReadLine());

                    Console.WriteLine("Elektromobilis - 1  Naftos Kuro Auto - 2: ");
                    string AutoTipas = Console.ReadLine();

                    switch (AutoTipas)
                    {
                        case "1":
                            Console.WriteLine("Pasirinkite automobilio id:");
                            var visiAuto1 = autonuomaService.GautiVisusElektromobilius();
                            foreach (var a in visiAuto1) Console.WriteLine($"{a.AutomobilisId} {a.Marke} {a.Modelis}");
                            int automobilisId1 = int.Parse(Console.ReadLine());
                            autonuomaService.SukurtiNuoma(klientasId, automobilisId1, nuomosPradzia, nuomosDienuKiekis, AutoTipas);
                            break;
                        case "2":
                            Console.WriteLine("Pasirinkite automobilio id:");
                            var visiAuto2 = autonuomaService.GautiVisusNaftosKuroAuto();
                            foreach (var a in visiAuto2) Console.WriteLine($"{a.AutomobilisId} {a.Marke} {a.Modelis}");
                            int automobilisId2 = int.Parse(Console.ReadLine());
                            autonuomaService.SukurtiNuoma(klientasId, automobilisId2, nuomosPradzia, nuomosDienuKiekis, AutoTipas);
                            break;
                    }
                    break;

                case "10": //Pakeisti duomenis automobiliu duombazeje
                    Console.WriteLine("Pasirinkite automobiliu sarasa: 1 - Naftos kuro automobiliai, 2 - Elektromobiliai:");
                    string keitimoTipas = Console.ReadLine();
                    switch (keitimoTipas)
                    {
                        case "1":
                            var visiAuto2 = autonuomaService.GautiVisusNaftosKuroAuto();
                            foreach (var a in visiAuto2) Console.WriteLine($"{a.AutomobilisId}. {a.Marke} {a.Modelis}, Nuomos kaina: {a.NuomosKaina} eur., Degalu Sanaudos: {a.DegaluSanaudos}");
                            Console.WriteLine("Pasirinkite norimo keisti automobilio id:");
                            int id = int.Parse(Console.ReadLine());
                            var dabartineAuto = autonuomaService.GautiNaftosAutoPagalId(id);

                            Console.WriteLine("Iveskite nauja marke arba spauskite ENTER:");
                            string naujaMarke = Console.ReadLine();
                            if (string.IsNullOrEmpty(naujaMarke))
                            {
                                naujaMarke = dabartineAuto.Marke;
                            }
                            Console.WriteLine("Iveskite nauja modeli arba spauskite ENTER:");
                            string naujasModelis = Console.ReadLine();
                            if (string.IsNullOrEmpty(naujasModelis))
                            {
                                naujasModelis = dabartineAuto.Modelis;
                            }
                            Console.WriteLine("Iveskite nauja nuomos kaina arba spauskite ENTER:");
                            string arNaujaNuomosKaina = Console.ReadLine();
                            decimal naujaNuomosKaina;
                            if (string.IsNullOrEmpty(arNaujaNuomosKaina))
                            {
                                naujaNuomosKaina = dabartineAuto.NuomosKaina;

                            }
                            else
                            {
                                naujaNuomosKaina = decimal.Parse(arNaujaNuomosKaina);
                            }

                            Console.WriteLine("Iveskite naujas degalu sanaudas arba spauskite ENTER:");
                            string arNaujosDegaluSanaudos = Console.ReadLine();
                            double naujosDegaluSanaudos;
                            if (string.IsNullOrEmpty(arNaujosDegaluSanaudos))
                            {
                                naujosDegaluSanaudos = dabartineAuto.DegaluSanaudos;

                            }
                            else
                            {
                                naujosDegaluSanaudos = double.Parse(arNaujosDegaluSanaudos);
                            }

                            var atnaujintasAutomobilis = autonuomaService.KoreguotiNaftaAutoInfo(id, naujaMarke, naujasModelis, naujaNuomosKaina, naujosDegaluSanaudos);

                            break;


                        case "2":
                            var visiAuto1 = autonuomaService.GautiVisusElektromobilius();
                            foreach (var a in visiAuto1) Console.WriteLine($"{a.AutomobilisId}. {a.Marke} {a.Modelis}, Nuomos kaina: {a.NuomosKaina} eur., Baterijos talpa: {a.BaterijosTalpa} KmW, Krovimo Laikas: {a.KrovimoLaikas} val.");

                            Console.WriteLine("Pasirinkite norimo keisti elektromobilio id:");
                            int idElektrinio = int.Parse(Console.ReadLine());
                            var dabartinisElektromobilis = autonuomaService.GautiElektromobiliPagalId(idElektrinio);

                            Console.WriteLine("Iveskite nauja elektromobilio marke arba spauskite ENTER:");
                            string naujaElektromobilioMarke = Console.ReadLine();
                            if (string.IsNullOrEmpty(naujaElektromobilioMarke))
                            {
                                naujaElektromobilioMarke = dabartinisElektromobilis.Marke;
                            }
                            Console.WriteLine("Iveskite nauja modeli arba spauskite ENTER:");
                            string naujasElektromobilioModelis = Console.ReadLine();
                            if (string.IsNullOrEmpty(naujasElektromobilioModelis))
                            {
                                naujasElektromobilioModelis = dabartinisElektromobilis.Modelis;
                            }
                            Console.WriteLine("Iveskite nauja nuomos kaina arba spauskite ENTER:");
                            string arNaujaElektromobilioNuomosKaina = Console.ReadLine();
                            decimal naujaElektromobilioNuomosKaina;
                            if (string.IsNullOrEmpty(arNaujaElektromobilioNuomosKaina))
                            {
                                naujaElektromobilioNuomosKaina = dabartinisElektromobilis.NuomosKaina;

                            }
                            else
                            {
                                naujaElektromobilioNuomosKaina = decimal.Parse(arNaujaElektromobilioNuomosKaina);
                            }

                            Console.WriteLine("Iveskite nauja baterijos talpa arba spauskite ENTER:");
                            string arNaujaBaterijosTalpa = Console.ReadLine();
                            int naujaBaterijosTalpa;
                            if (string.IsNullOrEmpty(arNaujaBaterijosTalpa))
                            {
                                naujaBaterijosTalpa = dabartinisElektromobilis.BaterijosTalpa;

                            }
                            else
                            {
                                naujaBaterijosTalpa = int.Parse(arNaujaBaterijosTalpa);
                            }

                            Console.WriteLine("Iveskite nauja krovimo laika arba spauskite ENTER:");
                            string arNaujasKrovimoLaikas = Console.ReadLine();
                            int naujasKrovimoLaikas;
                            if (string.IsNullOrEmpty(arNaujasKrovimoLaikas))
                            {
                                naujasKrovimoLaikas = dabartinisElektromobilis.KrovimoLaikas;

                            }
                            else
                            {
                                naujasKrovimoLaikas = int.Parse(arNaujasKrovimoLaikas);
                            }

                            var atnaujintasElektromobilis = autonuomaService.KoreguotiElektromobilioInfo(idElektrinio, naujaElektromobilioMarke, naujasElektromobilioModelis, naujaElektromobilioNuomosKaina, naujaBaterijosTalpa, naujasKrovimoLaikas);

                            break;

                    }


                    break;

                case "11": //Pakeisti duomenis klientu duombazeje
                    var visiKlientaiSarase = autonuomaService.GautiVisusKlientus();
                    foreach (var a in visiKlientaiSarase) Console.WriteLine($"{a.KlientasId}, {a.Vardas} {a.Pavarde}, Gimimo metai: {a.GimimoMetai}");

                    Console.WriteLine("Pasirinkite norimo keisti kliento id:");
                    int idKliento = int.Parse(Console.ReadLine());
                    var dabartinisKlientas = autonuomaService.GautiKlientaPagalId(idKliento);

                    Console.WriteLine("Iveskite nauja kliento varda arba spauskite ENTER:");
                    string naujasKlientoVardas = Console.ReadLine();
                    if (string.IsNullOrEmpty(naujasKlientoVardas))
                    {
                        naujasKlientoVardas = dabartinisKlientas.Vardas;
                    }

                    Console.WriteLine("Iveskite nauja kliento pavarde arba spauskite ENTER:");
                    string naujaKlientoPavarde = Console.ReadLine();
                    if (string.IsNullOrEmpty(naujaKlientoPavarde))
                    {
                        naujaKlientoPavarde = dabartinisKlientas.Pavarde;
                    }

                    Console.WriteLine("Iveskite nauja kliento gimimo data (YYYY-MM-DD) arba spauskite ENTER:");
                    string arNaujaGimimoData = Console.ReadLine();
                    DateOnly naujaGimimoData;
                    if (string.IsNullOrEmpty(arNaujaGimimoData))
                    {
                        naujaGimimoData = dabartinisKlientas.GimimoMetai;

                    }
                    else
                    {
                        naujaGimimoData = DateOnly.Parse(arNaujaGimimoData);
                    }

                    var atnaujintasKlientas = autonuomaService.KoreguotiKlientoInfo(idKliento, naujasKlientoVardas, naujaKlientoPavarde, naujaGimimoData);


                    break;

                case "12": //Pakeisti uzsakymu duomenis duombazeje. NEBAIGTA
                    var visiUzsakymi = autonuomaService.GautiVisusUzsakymus();
                    if (visiUzsakymi.Count == 0)
                    {
                        Console.WriteLine("Nera uzsakymu.");
                    }
                    else
                    {
                        var naftosKuroAutomobiliai2 = autonuomaService.GautiVisusNaftosKuroAuto();
                        var elektromobiliai2 = autonuomaService.GautiVisusElektromobilius();
                        var klientai2 = autonuomaService.GautiVisusKlientus();

                        foreach (var uzsakymas in visiUzsakymi)
                        {
                            var klientas = klientai2.FirstOrDefault(k => k.KlientasId == uzsakymas.KlientasId);

                            Automobilis automobilis = null;

                            if (uzsakymas.AutoTipas == "NaftosKuroAutomobilis")
                            {
                                automobilis = naftosKuroAutomobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }
                            else if (uzsakymas.AutoTipas == "Elektromobilis")
                            {
                                automobilis = elektromobiliai2
                                    .FirstOrDefault(a => a.AutomobilisId == uzsakymas.AutomobilisId);
                            }

                            if (automobilis != null)
                            {
                                Console.WriteLine($" {uzsakymas.UzsakymasId}. Uzsakovas: {klientas.Vardas} {klientas.Pavarde}, Uzsakytas automobilis: {automobilis.Marke} {automobilis.Modelis}, Nuomos Pradzia: {uzsakymas.NuomosPradzia.ToShortDateString()}, Dienu Kiekis: {uzsakymas.DienuKiekis} Pabaigos Data: {uzsakymas.gautiPabaigosData().ToShortDateString()}");
                            }
                        }
                    }

                    Console.WriteLine("Pasirinkite uzsakymo Id, kuri norite keisti:");
                    int idUzsakymo = int.Parse(Console.ReadLine());
                    var dabartinisUzsakymas = autonuomaService.GautiUzsakymaPagalId(idUzsakymo);

                    var visiUzsakymoKlientai = autonuomaService.GautiVisusKlientus();
                    foreach (var k in visiUzsakymoKlientai) Console.WriteLine($"{k.KlientasId} {k.Vardas} {k.Pavarde}");
                    Console.WriteLine("Pasirinkite i kuri klienta noretumete pakeisti arba spauskite ENTER");
                    string arNaujasKlientoId = Console.ReadLine();
                    int naujasKlientoId;
                    if (string.IsNullOrEmpty(arNaujasKlientoId))
                    {
                        naujasKlientoId = dabartinisUzsakymas.KlientasId;
                    }
                    else
                    {
                        naujasKlientoId = int.Parse(arNaujasKlientoId);
                    }

                    int naujasAutomobilisId = 0;
                    string autoTipas = dabartinisUzsakymas.AutoTipas;

                    Console.WriteLine("Ar norite keisti uzsakymo automobili? Taip/Ne :");
                    string keiciamasAutoTipas = Console.ReadLine();
                    if (keiciamasAutoTipas.Equals("Taip", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Pasirinkite: 1 - elektromobilis, 2 - naftos kuro automobilis");
                        string kurisAutomobilis = Console.ReadLine();
                        if (kurisAutomobilis == "1")
                        {
                            Console.WriteLine("Pasirinkite i kuri elektromobili norite pakeisti:");
                            var visiAuto1 = autonuomaService.GautiVisusElektromobilius();
                            foreach (var a in visiAuto1) Console.WriteLine($"{a.AutomobilisId}. {a.Marke} {a.Modelis}");
                            if (!int.TryParse(Console.ReadLine(), out naujasAutomobilisId))
                            {
                                Console.WriteLine("Neteisingas Automobilio Id formatas.");
                                break;
                            }
                            autoTipas = "Elektromobilis";
                        }
                        else if (kurisAutomobilis == "2")
                        {
                            Console.WriteLine("Pasirinkite i kuri naftos kuro automobilili norite pakeisti:");
                            var visiAuto2 = autonuomaService.GautiVisusNaftosKuroAuto();
                            foreach (var a in visiAuto2) Console.WriteLine($"{a.AutomobilisId}. {a.Marke} {a.Modelis}");
                            if (!int.TryParse(Console.ReadLine(), out naujasAutomobilisId))
                            {
                                Console.WriteLine("Neteisingas Automobilio Id formatas.");
                                break;
                            }
                            autoTipas = "NaftosKuroAutomobilis";
                        }
                        else
                        {
                            Console.WriteLine("Neteisingas pasirinkimas.");
                            break;
                        }
                    }

                        Console.WriteLine("Iveskite nauja nuomos pradzia (YYYY-MM-DD) arba spauskite ENTER:");
                    string arNaujaNuomosPradzia = Console.ReadLine();
                    DateTime naujaNuomosPradzia;
                    
                    if (string.IsNullOrEmpty(arNaujaNuomosPradzia))
                    {
                        naujaNuomosPradzia = dabartinisUzsakymas.NuomosPradzia;

                    }
                    else
                    {
                        naujaNuomosPradzia = DateTime.Parse(arNaujaNuomosPradzia);
                    }


                    Console.WriteLine("Iveskite nauja dienu kieki arba spauskite ENTER:");
                    string arNaujasDienuKiekis = Console.ReadLine();
                    int naujasDienuKiekis;

                    if (string.IsNullOrEmpty(arNaujasDienuKiekis))
                    {
                        naujasDienuKiekis = dabartinisUzsakymas.DienuKiekis;

                    }
                    else
                    {
                        naujasDienuKiekis = int.Parse(arNaujasDienuKiekis);
                    }

                    autonuomaService.KoreguotiNuomosInfo(dabartinisUzsakymas.UzsakymasId, naujasKlientoId, autoTipas, naujasAutomobilisId, naujaNuomosPradzia, naujasDienuKiekis);
                    break;




                default:
                    Console.WriteLine("Neteisingas pasirinkimas. Bandykite dar karta.");
                    break;

            }
        }

        }
    
    public static IAutonuomaService SetupDependencies()
    {
        //IKlientaiRepository klientaiRepository = new KlientaiFileRepository("Klientai.csv");
        //IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiFileRepository("Auto.csv");
        IKlientaiRepository klientaiRepository = new KlientaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");
        IAutomobiliaiRepository automobiliaiRepository = new AutomobiliaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");
        IUzsakymaiRepository uzsakymaiRepository = new UzsakymaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;");

        IKlientaiService klientaiService = new KlientaiService(klientaiRepository);
        IAutomobiliaiService automobiliaiService = new AutomobiliaiService(automobiliaiRepository);
        return new AutonuomosService(klientaiService, automobiliaiService, uzsakymaiRepository);
    }
}