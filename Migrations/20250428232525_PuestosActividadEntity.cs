using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PuestosActividadEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PuestosActividad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PuestoDescriptivo = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fecha_Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestosActividad", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PuestosActividad_PuestosDescriptivo_ID_PuestoDescriptivo",
                        column: x => x.ID_PuestoDescriptivo,
                        principalTable: "PuestosDescriptivo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PuestosActividad_ID_PuestoDescriptivo",
                table: "PuestosActividad",
                column: "ID_PuestoDescriptivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PuestosActividad");
        }
    }
}
