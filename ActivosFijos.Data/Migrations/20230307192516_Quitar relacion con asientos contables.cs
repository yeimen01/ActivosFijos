using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivosFijos.Data.Migrations
{
    /// <inheritdoc />
    public partial class Quitarrelacionconasientoscontables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalculoDepreciacion_AsientosContables_AsientoContableId",
                table: "CalculoDepreciacion");

            migrationBuilder.DropIndex(
                name: "IX_CalculoDepreciacion_AsientoContableId",
                table: "CalculoDepreciacion");

            migrationBuilder.DropColumn(
                name: "AsientoContableId",
                table: "CalculoDepreciacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AsientoContableId",
                table: "CalculoDepreciacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalculoDepreciacion_AsientoContableId",
                table: "CalculoDepreciacion",
                column: "AsientoContableId");

            migrationBuilder.AddForeignKey(
                name: "FK_CalculoDepreciacion_AsientosContables_AsientoContableId",
                table: "CalculoDepreciacion",
                column: "AsientoContableId",
                principalTable: "AsientosContables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
