using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cediva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFlujoMuelleYDespacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenesFacturasMuelle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroFactura = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClienteNombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ClienteNit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Muelle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuditadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HoraAuditoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DespachadoPor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HoraDespacho = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Transportadora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlacaVehiculo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Conductor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesFacturasMuelle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineasOrdenesFacturasMuelle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrdenFacturaMuelleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoBarras = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CantidadPedida = table.Column<int>(type: "int", nullable: false),
                    CantidadPistoleada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasOrdenesFacturasMuelle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasOrdenesFacturasMuelle_OrdenesFacturasMuelle_OrdenFacturaMuelleId",
                        column: x => x.OrdenFacturaMuelleId,
                        principalTable: "OrdenesFacturasMuelle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineasOrdenesFacturasMuelle_OrdenFacturaMuelleId",
                table: "LineasOrdenesFacturasMuelle",
                column: "OrdenFacturaMuelleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesFacturasMuelle_NumeroFactura",
                table: "OrdenesFacturasMuelle",
                column: "NumeroFactura",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasOrdenesFacturasMuelle");

            migrationBuilder.DropTable(
                name: "OrdenesFacturasMuelle");
        }
    }
}
