﻿// <auto-generated />
using System;
using AutomobiliuNuoma.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutomobiliuNuoma.Core.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240812134810_InitialCreate3")]
    partial class InitialCreate3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AutomobiliuNuoma.Core.Models.Darbuotojas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AtliktuUzsakymuKiekis")
                        .HasColumnType("int");

                    b.Property<double>("BazinisAtlyginimas")
                        .HasColumnType("float");

                    b.Property<int>("Pareigos")
                        .HasColumnType("int");

                    b.Property<string>("Pavarde")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vardas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Darbuotojai");
                });

            modelBuilder.Entity("AutomobiliuNuoma.Core.Models.Elektromobilis", b =>
                {
                    b.Property<int>("AutomobilisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutomobilisId"));

                    b.Property<int>("BaterijosTalpa")
                        .HasColumnType("int");

                    b.Property<int>("KrovimoLaikas")
                        .HasColumnType("int");

                    b.Property<string>("Marke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NuomosKaina")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AutomobilisId");

                    b.ToTable("Elektromobiliai");
                });

            modelBuilder.Entity("AutomobiliuNuoma.Core.Models.Klientas", b =>
                {
                    b.Property<int>("KlientasId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KlientasId"));

                    b.Property<DateOnly>("GimimoMetai")
                        .HasColumnType("date");

                    b.Property<string>("Pavarde")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vardas")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlientasId");

                    b.ToTable("Klientai");
                });

            modelBuilder.Entity("AutomobiliuNuoma.Core.Models.NaftosKuroAutomobilis", b =>
                {
                    b.Property<int>("AutomobilisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AutomobilisId"));

                    b.Property<double>("DegaluSanaudos")
                        .HasColumnType("float");

                    b.Property<string>("Marke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NuomosKaina")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AutomobilisId");

                    b.ToTable("NaftosKuroAuto");
                });
#pragma warning restore 612, 618
        }
    }
}
