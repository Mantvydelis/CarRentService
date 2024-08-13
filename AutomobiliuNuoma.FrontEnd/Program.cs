using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//builder.Services.AddTransient<IAutomobiliaiRepository, AutomobiliaiDbRepository>(_ => new AutomobiliaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
//builder.Services.AddTransient<IKlientaiRepository, KlientaiDBRepository>(_ => new KlientaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
//builder.Services.AddTransient<IUzsakymaiRepository, UzsakymaiDBRepository>(_ => new UzsakymaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
//builder.Services.AddTransient<IDarbuotojaiRepository, DarbuotojaiDbRepository>(_ => new DarbuotojaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IKlientaiService, KlientaiService>();
builder.Services.AddTransient<IAutomobiliaiService, AutomobiliaiService>();
builder.Services.AddTransient<IAutonuomaService, AutonuomosService>();
builder.Services.AddTransient<IAutomobiliaiRepository, AutomobiliaiEFDRepository>(_ => new AutomobiliaiEFDRepository());
builder.Services.AddTransient<IKlientaiRepository, KlientaiEFDRepository>(_ => new KlientaiEFDRepository());
builder.Services.AddTransient<IDarbuotojaiRepository, DarbuotojaiEFDRepository>(_ => new DarbuotojaiEFDRepository());
builder.Services.AddTransient<IUzsakymaiRepository, UzsakymaiEFDRepository>(_ => new UzsakymaiEFDRepository());
builder.Services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient("mongodb+srv://mantvydassemeta:Slaptazodis@cluster0.awg2t.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"));
builder.Services.AddTransient<IMongoDbCacheRepository, MongoDbCacheRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
