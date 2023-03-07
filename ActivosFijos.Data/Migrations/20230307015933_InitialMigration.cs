using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivosFijos.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsientosContables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inventario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaContable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    FechaAsiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MontoAsiento = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsientosContables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoActivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaContableCompra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaContableDepreciacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    TipoPersona = table.Column<int>(type: "int", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivosFijo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    TipoActivoId = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValorCompra = table.Column<double>(type: "float", nullable: false),
                    DepreciacionAcumulada = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivosFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivosFijo_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivosFijo_TipoActivo_TipoActivoId",
                        column: x => x.TipoActivoId,
                        principalTable: "TipoActivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculoDepreciacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AñoProceso = table.Column<int>(type: "int", nullable: false),
                    MesProceso = table.Column<int>(type: "int", nullable: false),
                    ActivoFijoId = table.Column<int>(type: "int", nullable: false),
                    AsientoContableId = table.Column<int>(type: "int", nullable: false),
                    FechaProceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MontoDepreciado = table.Column<double>(type: "float", nullable: false),
                    DepreciacionAcumulada = table.Column<double>(type: "float", nullable: false),
                    CuentaCompra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaDepreciacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculoDepreciacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculoDepreciacion_ActivosFijo_ActivoFijoId",
                        column: x => x.ActivoFijoId,
                        principalTable: "ActivosFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalculoDepreciacion_AsientosContables_AsientoContableId",
                        column: x => x.AsientoContableId,
                        principalTable: "AsientosContables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijo_DepartamentoId",
                table: "ActivosFijo",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivosFijo_TipoActivoId",
                table: "ActivosFijo",
                column: "TipoActivoId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculoDepreciacion_ActivoFijoId",
                table: "CalculoDepreciacion",
                column: "ActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_CalculoDepreciacion_AsientoContableId",
                table: "CalculoDepreciacion",
                column: "AsientoContableId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_DepartamentoId",
                table: "Empleado",
                column: "DepartamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculoDepreciacion");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "ActivosFijo");

            migrationBuilder.DropTable(
                name: "AsientosContables");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "TipoActivo");
        }
    }
}
