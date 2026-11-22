using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cediva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnasMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoMovimiento",
                table: "Movimientos",
                newName: "Tipo");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaMovimiento",
                table: "Movimientos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaMovimiento",
                table: "Movimientos");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Movimientos",
                newName: "TipoMovimiento");
        }
    }
}
