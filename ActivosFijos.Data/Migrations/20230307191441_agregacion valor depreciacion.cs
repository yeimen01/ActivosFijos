using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ActivosFijos.Data.Migrations
{
    /// <inheritdoc />
    public partial class agregacionvalordepreciacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ValorDepreciacion",
                table: "ActivosFijo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorDepreciacion",
                table: "ActivosFijo");
        }
    }
}
