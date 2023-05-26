using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiChamba.Migrations
{
    /// <inheritdoc />
    public partial class TablaRecurso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RECURSO",
                columns: table => new
                {
                    ID_RECURSO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITULO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IMAGEN = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECURSO", x => x.ID_RECURSO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RECURSO");
        }
    }
}
