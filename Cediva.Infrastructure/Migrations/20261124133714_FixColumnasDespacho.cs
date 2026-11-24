using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cediva.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnasDespacho : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "DespachoId",
                table: "ItemsDespacho",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ClienteNombre",
                table: "Despachos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDespacho",
                table: "Despachos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClienteNombre",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "FechaDespacho",
                table: "Despachos");

            migrationBuilder.AlterColumn<Guid>(
                name: "DespachoId",
                table: "ItemsDespacho",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
