using AutomobiliuNuoma.Core.Contracts;
using AutomobiliuNuoma.Core.Repositories;
using AutomobiliuNuoma.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddTransient<IAutomobiliaiRepository, AutomobiliaiDbRepository>(_ => new AutomobiliaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IKlientaiRepository, KlientaiDBRepository>(_ => new KlientaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IUzsakymaiRepository, UzsakymaiDBRepository>(_ => new UzsakymaiDBRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IDarbuotojaiRepository, DarbuotojaiDbRepository>(_ => new DarbuotojaiDbRepository("Server=localhost;Database=Automobiliai;Trusted_Connection=True;"));
builder.Services.AddTransient<IKlientaiService, KlientaiService>();
builder.Services.AddTransient<IAutomobiliaiService, AutomobiliaiService>();
builder.Services.AddTransient<IAutonuomaService, AutonuomosService>();

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
