using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomobiliuNuoma.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Darbuotojai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pareigos = table.Column<int>(type: "int", nullable: false),
                    BazinisAtlyginimas = table.Column<double>(type: "float", nullable: false),
                    AtliktuUzsakymuKiekis = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Darbuotojai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klientai",
                columns: table => new
                {
                    KlientasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GimimoMetai = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klientai", x => x.KlientasId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Darbuotojai");

            migrationBuilder.DropTable(
                name: "Klientai");
        }
    }
}
