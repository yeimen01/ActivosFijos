using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivosFijos.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnioDepreciacion",
                table: "ActivosFijo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Empleado",
                columns: new[] { "Id", "Apellido", "Cedula", "DepartamentoId", "Estado", "FechaIngreso", "Nombre", "TipoPersona" },
                values: new object[] { 1, "Gomez", "40213108481", 1, 0, new DateTime(2023, 3, 11, 10, 2, 22, 756, DateTimeKind.Local).AddTicks(828), "Carlos", 0 });

            migrationBuilder.InsertData(
                table: "TipoActivo",
                columns: new[] { "Id", "CuentaContableCompra", "CuentaContableDepreciacion", "Descripcion", "Estado" },
                values: new object[] { 1, "65", "66", "Electronico", 0 });

            migrationBuilder.InsertData(
                table: "ActivosFijo",
                columns: new[] { "Id", "AnioDepreciacion", "DepartamentoId", "DepreciacionAcumulada", "Descripcion", "FechaRegistro", "TipoActivoId", "ValorCompra", "ValorDepreciacion" },
                values: new object[] { 1, 2025, 1, 0.0, "Laptop", new DateTime(2023, 3, 11, 10, 2, 22, 756, DateTimeKind.Local).AddTicks(858), 1, 25000.0, 4000.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivosFijo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleado",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoActivo",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "AnioDepreciacion",
                table: "ActivosFijo");
        }
    }
}
