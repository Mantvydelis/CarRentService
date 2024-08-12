using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomobiliuNuoma.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "NaftosKuroAuto",
                columns: table => new
                {
                    AutomobilisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegaluSanaudos = table.Column<double>(type: "float", nullable: false),
                    Marke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NuomosKaina = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaftosKuroAuto", x => x.AutomobilisId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elektromobiliai");

            migrationBuilder.DropTable(
                name: "NaftosKuroAuto");
        }
    }
}
