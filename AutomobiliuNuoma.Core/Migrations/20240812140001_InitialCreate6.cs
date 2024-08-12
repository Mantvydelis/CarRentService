using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomobiliuNuoma.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elektromobiliai");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NaftosKuroAuto",
                table: "NaftosKuroAuto");

            migrationBuilder.RenameTable(
                name: "NaftosKuroAuto",
                newName: "Automobilis");

            migrationBuilder.AlterColumn<double>(
                name: "DegaluSanaudos",
                table: "Automobilis",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "BaterijosTalpa",
                table: "Automobilis",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Automobilis",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "KrovimoLaikas",
                table: "Automobilis",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Automobilis",
                table: "Automobilis",
                column: "AutomobilisId");

            migrationBuilder.CreateTable(
                name: "NuomosUzsakymas",
                columns: table => new
                {
                    UzsakymasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NuomosPradzia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DienuKiekis = table.Column<int>(type: "int", nullable: false),
                    KlientoVardas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KlientoPavarde = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutomobilisId = table.Column<int>(type: "int", nullable: false),
                    ElektromobilisId = table.Column<int>(type: "int", nullable: false),
                    BenzAutomobilisId = table.Column<int>(type: "int", nullable: false),
                    KlientasId = table.Column<int>(type: "int", nullable: false),
                    AutoTipas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DarbuotojasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NuomosUzsakymas", x => x.UzsakymasId);
                    table.ForeignKey(
                        name: "FK_NuomosUzsakymas_Automobilis_AutomobilisId",
                        column: x => x.AutomobilisId,
                        principalTable: "Automobilis",
                        principalColumn: "AutomobilisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NuomosUzsakymas_Klientai_KlientasId",
                        column: x => x.KlientasId,
                        principalTable: "Klientai",
                        principalColumn: "KlientasId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NuomosUzsakymas_AutomobilisId",
                table: "NuomosUzsakymas",
                column: "AutomobilisId");

            migrationBuilder.CreateIndex(
                name: "IX_NuomosUzsakymas_KlientasId",
                table: "NuomosUzsakymas",
                column: "KlientasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NuomosUzsakymas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Automobilis",
                table: "Automobilis");

            migrationBuilder.DropColumn(
                name: "BaterijosTalpa",
                table: "Automobilis");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Automobilis");

            migrationBuilder.DropColumn(
                name: "KrovimoLaikas",
                table: "Automobilis");

            migrationBuilder.RenameTable(
                name: "Automobilis",
                newName: "NaftosKuroAuto");

            migrationBuilder.AlterColumn<double>(
                name: "DegaluSanaudos",
                table: "NaftosKuroAuto",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NaftosKuroAuto",
                table: "NaftosKuroAuto",
                column: "AutomobilisId");

            migrationBuilder.CreateTable(
                name: "Elektromobiliai",
                columns: table => new
                {
                    AutomobilisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaterijosTalpa = table.Column<int>(type: "int", nullable: false),
                    KrovimoLaikas = table.Column<int>(type: "int", nullable: false),
                    Marke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NuomosKaina = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elektromobiliai", x => x.AutomobilisId);
                });
        }
    }
}
