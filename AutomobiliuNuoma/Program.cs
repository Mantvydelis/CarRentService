using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Models;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using System;

namespace AutomobiliuNuomaConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        IAutonuomaService autonuomaService = SetupDependencies();
        while(true)
        {
            Console.WriteLine("1. Rodyti visus automobilius (KOL KAS NEVEIKIA)");
            Console.WriteLine("2. Rodyti visus klientus");
            Console.WriteLine("3. Formuoti nuomos uzsakyma");
            Console.WriteLine("4. Gauti visus uzsakymus"); 
            Console.WriteLine("5. Rodyti visus elektromobilius is duombazes");
            Console.WriteLine("6. Rodyti visus naftos kuro automobilius is duombazes");
            Console.WriteLine("7. Prideti automobili i duombaze");
            Console.WriteLine("8. Rodyti visus klientus is duombazes");
            Console.WriteLine("9. Prideti klienta i duombaze");

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
                case "2": /*KOL KAS PERMETA I DUOMBAZE, O NE LISTA*/
                    List<Klientas> klientai = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas k in klientai)
                    {
                        Console.WriteLine(k);
                    }
                    break;

                case "3": /*neduombaze*/
                    Console.WriteLine("Nuomos uzsakymas: ");
                    foreach (Klientas k in autonuomaService.GautiVisusKlientus())
                    {
                        Console.WriteLine(k);
                    }

                    Console.WriteLine("Iveskite norimo kliento varda");
                    string vardas = Console.ReadLine();
                    Console.WriteLine("Iveskite norimo kliento pavarde");
                    string pavarde = Console.ReadLine();

                    foreach (Automobilis a in autonuomaService.GautiVisusAutomobilius())
                    {
                        Console.WriteLine(a);
                    }

                    Console.WriteLine("Pasirinkite automobili pagal Id sarase: ");
                    int autoId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Iveskite kiek dienu autommobilis yra isnuomojamas: ");
                    int dienos = int.Parse(Console.ReadLine());

                    autonuomaService.SukurtiNuoma(vardas, pavarde, autoId, DateTime.Now, dienos);

                    break;

                case "4":
                    var uzsakymai = autonuomaService.GautiVisusUzsakymus();
                    if (uzsakymai.Count == 0)
                    {
                        Console.WriteLine("Nera uzsakymu.");
                    }
                    else
                    {
                        foreach (var uzsakymas in uzsakymai)
                        {
                            Console.WriteLine($"Uzsakovas: {uzsakymas.Uzsakovas.Vardas} {uzsakymas.Uzsakovas.Pavarde}, Nuomuojamas Auto: {uzsakymas.NuomuojamasAuto.Marke} {uzsakymas.NuomuojamasAuto.Modelis}, Nuomos Pradzia: {uzsakymas.NuomosPradzia.ToShortDateString()}, Dienu Kiekis: {uzsakymas.DienuKiekis}, Pabaigos Data: {uzsakymas.gautiPabaigosData().ToShortDateString()}, Nuomos Kaina: {uzsakymas.skaiciuotiNuomosKaina()}");
                        }
                    }
                    break;

                case "5": /*duombaze*/
                    List<Elektromobilis> elektromobiliai = autonuomaService.GautiVisusElektromobilius();
                    foreach (Elektromobilis ev in elektromobiliai)
                    {
                        Console.WriteLine(ev);
                    }
                    break;
                case "6": /*duombaze*/
                    List<NaftosKuroAutomobilis> naftosKuroAutomobiliai = autonuomaService.GautiVisusNaftosKuroAuto();
                    foreach (NaftosKuroAutomobilis v in naftosKuroAutomobiliai)
                    {
                        Console.WriteLine(v);
                    }
                    break;

                case "7": /*duombaze*/
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

                case "8": /*duombaze*/
                    List<Klientas> klientaiDB = autonuomaService.GautiVisusKlientus();
                    foreach (Klientas kl in klientaiDB)
                    {
                        Console.WriteLine(kl);
                    }
                    break;

                case "9":
                    
                    Console.WriteLine("Iveskite kliento varda");
                    string klientoVardas = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento pavarde");
                    string klientoPavarde = Console.ReadLine();
                    Console.WriteLine("Iveskite kliento gimimo data (yyyy-mm-dd)");
                    DateOnly klientoGimimoData = DateOnly.Parse(Console.ReadLine());

                    Klientas naujasKlientas = new Klientas(klientoVardas, klientoPavarde, klientoGimimoData);

                    autonuomaService.PridetiNaujaKlienta(naujasKlientas);

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
        IKlientaiService klientaiService = new KlientaiService(klientaiRepository);
        IAutomobiliaiService automobiliaiService = new AutomobiliaiService(automobiliaiRepository);
        return new AutonuomosService(klientaiService, automobiliaiService);
    }
}