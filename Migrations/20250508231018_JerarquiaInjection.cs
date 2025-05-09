using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class JerarquiaInjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jerarquias",
                columns: new[] { "ID", "Clave", "Descripcion", "Nivel", "Titulo" },
                values: new object[,]
                {
                    { 1, "DIR", "DIRECCION GENERAL", 0, "DIRECCION GENERAL" },
                    { 2, "DAR", "DIRECCION DE AREA O UNIDAD", 1, "DIRECCION" },
                    { 3, "GER", "GERENCIA DE AREA O DEPARTAMENTO", 2, "GERENCIA" },
                    { 4, "CRD", "COORDINACION/JEFATURA DE DEPTO. U OFICINA", 3, "COORDINACION" },
                    { 6, "SUP", "SUPERVISION DE DEPTO. U OFICINA", 5, "SUPERVISION" },
                    { 7, "AUX", "COLABORACION AUX. ADMVA. U OPERATIVA", 6, "AUXILIAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 7);
        }
    }
}
