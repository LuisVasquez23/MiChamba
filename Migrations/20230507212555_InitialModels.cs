using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiChamba.Migrations
{
    /// <inheritdoc />
    public partial class InitialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DIRECCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFONO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APELLIDO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TELEFONO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "OFERTA",
                columns: table => new
                {
                    ID_OFERTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITULO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REQUISITOS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UBICACION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FECHA_PUBLICACION = table.Column<DateTime>(type: "datetime", nullable: false),
                    ID_EMPRESA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFERTA", x => x.ID_OFERTA);
                    table.ForeignKey(
                        name: "FK_OFERTA_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CALIFICACION",
                columns: table => new
                {
                    ID_CALIFICACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_EMPRESA = table.Column<int>(type: "int", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    CALIFICACION = table.Column<int>(type: "int", nullable: false),
                    COMENTARIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FECHA_CALIFICACION = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CALIFICACION", x => x.ID_CALIFICACION);
                    table.ForeignKey(
                        name: "FK_CALIFICACION_EMPRESA_ID_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CALIFICACION_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSTULACION",
                columns: table => new
                {
                    ID_POSTULACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_OFERTA = table.Column<int>(type: "int", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false),
                    FECHA_POSTULACION = table.Column<DateTime>(type: "datetime", nullable: false),
                    ESTADO_POSTULACION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSTULACION", x => x.ID_POSTULACION);
                    table.ForeignKey(
                        name: "FK_POSTULACION_OFERTA_ID_OFERTA",
                        column: x => x.ID_OFERTA,
                        principalTable: "OFERTA",
                        principalColumn: "ID_OFERTA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSTULACION_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CALIFICACION_ID_EMPRESA",
                table: "CALIFICACION",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_CALIFICACION_ID_USUARIO",
                table: "CALIFICACION",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_OFERTA_ID_EMPRESA",
                table: "OFERTA",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_POSTULACION_ID_OFERTA",
                table: "POSTULACION",
                column: "ID_OFERTA");

            migrationBuilder.CreateIndex(
                name: "IX_POSTULACION_ID_USUARIO",
                table: "POSTULACION",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CALIFICACION");

            migrationBuilder.DropTable(
                name: "POSTULACION");

            migrationBuilder.DropTable(
                name: "OFERTA");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
