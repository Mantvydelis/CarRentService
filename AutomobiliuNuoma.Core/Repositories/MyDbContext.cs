using AutomobiliuNuoma.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<Elektromobilis> Elektromobiliai { get; set; }
        public DbSet<NaftosKuroAutomobilis> NaftosKuroAuto { get; set; }
        public DbSet<Klientas> Klientai { get; set; }

        public DbSet<Darbuotojas> Darbuotojai { get; set; }

        public DbSet<NuomosUzsakymas> NuomosUzsakymas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=AutoNuoma;Trusted_Connection=True;TrustServerCertificate=true;");
        }

    }
}
