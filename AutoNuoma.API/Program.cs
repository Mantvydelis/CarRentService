using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using AutoNuoma.API.Contracts;
using AutoNuoma.API.Service;
using Microsoft.Extensions.DependencyInjection;





var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IAutomobiliaiRepository, AutomobiliaiDbRepository>(_ => new AutomobiliaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IKlientaiRepository, KlientaiDBRepository>(_ => new KlientaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IUzsakymaiRepository, UzsakymaiDBRepository>(_ => new UzsakymaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));

builder.Services.AddTransient<IKlientaiService, KlientaiService>();
builder.Services.AddTransient<IAutomobiliaiService, AutomobiliaiService>();
builder.Services.AddTransient<IAutonuomaService, AutonuomosService>();
builder.Services.AddSingleton<ICacheService, CacheService>();







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



static IAutonuomaService SetupDependencies()
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

