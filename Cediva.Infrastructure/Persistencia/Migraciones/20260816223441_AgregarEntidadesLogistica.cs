using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cediva.Infrastructure.Persistencia.Migraciones
{
    /// <inheritdoc />
    public partial class AgregarEntidadesLogistica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UnidadMedida",
                table: "Productos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoBarras",
                table: "Productos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Cantidad_Unidad",
                table: "Inventarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Cantidad_Valor",
                table: "Inventarios",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "Inventarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Calle",
                table: "Despachos",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Ciudad",
                table: "Despachos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_CodigoPostal",
                table: "Despachos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Complemento",
                table: "Despachos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Departamento",
                table: "Despachos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Numero",
                table: "Despachos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion_Pais",
                table: "Despachos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Despachos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ItemDespachos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DespachoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cantidad_Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad_Unidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDespachos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDespachos_Despachos_DespachoId",
                        column: x => x.DespachoId,
                        principalTable: "Despachos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cantidad_Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Cantidad_Unidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InventarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Inventarios_InventarioId",
                        column: x => x.InventarioId,
                        principalTable: "Inventarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos",
                column: "CodigoBarras",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemDespachos_DespachoId",
                table: "ItemDespachos",
                column: "DespachoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_InventarioId",
                table: "Movimientos",
                column: "InventarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDespachos");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Cantidad_Unidad",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "Cantidad_Valor",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "Inventarios");

            migrationBuilder.DropColumn(
                name: "Direccion_Calle",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_Ciudad",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_CodigoPostal",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_Complemento",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_Departamento",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_Numero",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Direccion_Pais",
                table: "Despachos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Despachos");

            migrationBuilder.AlterColumn<int>(
                name: "UnidadMedida",
                table: "Productos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ImagenUrl",
                table: "Productos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodigoBarras",
                table: "Productos",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
