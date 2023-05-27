using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiChamba.Migrations
{
    /// <inheritdoc />
    public partial class ActualizandoCampo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ESTADO",
                table: "USUARIO",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ESTADO",
                table: "USUARIO");
        }
    }
}
