using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InjectedDataEmpleado_Tipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Prefijo", "Titulo" },
                values: new object[] { "CNF", "DE CONFIANZA" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Prefijo", "Titulo" },
                values: new object[] { "PLA", "DE PLANTA" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { "EMPLEADO EVENTUAL CON CONTRATO TEMPORAL", "EVT", "EVENTUAL" });

            migrationBuilder.InsertData(
                table: "Empleados_Tipo",
                columns: new[] { "ID", "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { 4, "BENEFICIARIO DE FORMACION PROFESIONAL", "BEC", "BECARIO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Prefijo", "Titulo" },
                values: new object[] { "Prefijo 1", "CONFIANZA" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Prefijo", "Titulo" },
                values: new object[] { "Prefijo 2", "OPERATIVO" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { "Descripcion 3", "Prefijo 3", "Tipo 3" });
        }
    }
}
