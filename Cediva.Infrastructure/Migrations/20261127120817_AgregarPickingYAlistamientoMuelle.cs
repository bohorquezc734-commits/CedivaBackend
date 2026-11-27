using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cediva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPickingYAlistamientoMuelle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlistadoPor",
                table: "OrdenesFacturasMuelle",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraAlistamiento",
                table: "OrdenesFacturasMuelle",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CantidadAlistada",
                table: "LineasOrdenesFacturasMuelle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlistadoPor",
                table: "OrdenesFacturasMuelle");

            migrationBuilder.DropColumn(
                name: "HoraAlistamiento",
                table: "OrdenesFacturasMuelle");

            migrationBuilder.DropColumn(
                name: "CantidadAlistada",
                table: "LineasOrdenesFacturasMuelle");
        }
    }
}
