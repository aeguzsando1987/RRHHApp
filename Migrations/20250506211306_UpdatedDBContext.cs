using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Organizaciones",
                columns: new[] { "Id", "Clave", "Fecha_Creacion", "Nombre" },
                values: new object[] { 1, "ORG1", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CONGLOMERADO" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Descripcion_Status", "Status_Emp" },
                values: new object[,]
                {
                    { 1, "EMPLEADO ACTIVO", "ACTIVO" },
                    { 2, "EMPLEADO SUSPENDIDO", "SUSPENDIDO" },
                    { 3, "EMPLEADO INACTIVO POR BAJA VOLUNTARIA", "BAJA VOLUNTARIA" },
                    { 4, "EMPLEADO INACTIVO POR BAJA INVOLUNTARIA", "BAJA INVOLUNTARIA" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Organizaciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
