using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class PuestoDescriptivosEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Statuses_Id_Status",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Ubicaciones_Id_Ubicacion",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados");

            migrationBuilder.CreateTable(
                name: "PuestosDescriptivo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Puesto = table.Column<int>(type: "int", nullable: false),
                    Resumen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fecha_Actualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuestosDescriptivo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PuestosDescriptivo_Puestos_ID_Puesto",
                        column: x => x.ID_Puesto,
                        principalTable: "Puestos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados",
                column: "Id_Puesto");

            migrationBuilder.CreateIndex(
                name: "IX_PuestosDescriptivo_ID_Puesto",
                table: "PuestosDescriptivo",
                column: "ID_Puesto");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Statuses_Id_Status",
                table: "Empleados",
                column: "Id_Status",
                principalTable: "Statuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Ubicaciones_Id_Ubicacion",
                table: "Empleados",
                column: "Id_Ubicacion",
                principalTable: "Ubicaciones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Statuses_Id_Status",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Ubicaciones_Id_Ubicacion",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "PuestosDescriptivo");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados",
                column: "Id_Puesto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Statuses_Id_Status",
                table: "Empleados",
                column: "Id_Status",
                principalTable: "Statuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Ubicaciones_Id_Ubicacion",
                table: "Empleados",
                column: "Id_Ubicacion",
                principalTable: "Ubicaciones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
