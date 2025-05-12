using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserWithCorrectHashAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion", "Nombre" },
                values: new object[] { "AREA_DEFAULT", "Descripción del Área de Administración", "Área de Administración" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "ea4bb1b0-1229-4b18-af48-9ef544af709d", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.UpdateData(
                table: "Departamentos",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion", "Nombre" },
                values: new object[] { "DEP_DEFAULT", "Descripción del Departamento de TI", "Departamento de TI" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Descripcion", "Prefijo" },
                values: new object[] { "Personal admvo., gerencial estrategico de confianza", "CONF" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { "Personal operativo con opcion de hrs extras", "OPR", "OPERATIVO" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { "SOCIOS EN NOMINA DE EMPRESA", "SOC", "SOCIO" });

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Direccion", "Fecha_Creacion", "RFC", "Razon_Social" },
                values: new object[] { "EMP_DEFAULT", "Calle Falsa 123, Colonia Centro", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "XAXX010101000", "Empresa Principal" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion" },
                values: new object[] { "DIRGRAL", "MAXIMA AUTORIDAD" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Clave", "Descripcion", "Titulo" },
                values: new object[] { "DIRAREA", "DIR. AREA O DIVISION", "DIRECCION DE AREA" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 3,
                column: "Descripcion",
                value: "GERENCIA DE DEPTO. O UNIDAD");

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Clave", "Descripcion", "Titulo" },
                values: new object[] { "JEFDPTO", "JEFATURA DE DPTO.", "JEFATURA DEPTO." });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 6,
                column: "Descripcion",
                value: "SUPERVISION DE DPTO.");

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 7,
                column: "Descripcion",
                value: "COLABORACION AUX. ADMVA. OP.");

            migrationBuilder.InsertData(
                table: "Jerarquias",
                columns: new[] { "ID", "Clave", "Descripcion", "Nivel", "Titulo" },
                values: new object[] { 5, "COORD", "COORD.PROYECTOS O EQUIPOS", 4, "COORDINACION" });

            migrationBuilder.UpdateData(
                table: "Organizaciones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Clave", "Fecha_Creacion", "Nombre" },
                values: new object[] { "ORG_DEFAULT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organización Principal" });

            migrationBuilder.InsertData(
                table: "Ubicaciones",
                columns: new[] { "ID", "Clave", "Id_Empresa", "Ubicacion_Referencial" },
                values: new object[] { 1, "UBI_PRINCIPAL", 1, "Oficinas Centrales" });

            migrationBuilder.InsertData(
                table: "Puestos",
                columns: new[] { "ID", "Clave", "Descripcion", "Id_Departamento", "Id_Jerarquia", "Titulo" },
                values: new object[] { 1, "PST_SUPERADMIN", "Puesto de Super Administrador del sistema", 1, 5, "Super Administrador" });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "ID", "Id_Jefe", "Id_Puesto", "Id_Status", "Id_Ubicacion" },
                values: new object[] { 1, null, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "ConcurrencyStamp", "Email", "EmailConfirmed", "Id_Empleado", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, true, "c2b1a09f-8d6e-4c5a-7b4f-3e2d1c0b9a8e", "superadmin@example.com", true, 1, false, null, "SUPERADMIN@EXAMPLE.COM", "SUPERADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMRtuZ08na0uYj9mgXHFn+N831HTSjdk1fOUkCi6AawFZO2yK1CxdgjKe1clHKXrzA==", null, false, "d3a4be3b-5e7b-4b7f-8c6a-1a9f8e7d6c5b", false, "superadmin@example.com" });

            migrationBuilder.InsertData(
                table: "Empleados_Perfil",
                columns: new[] { "Id_Empleado", "Apellido_Materno", "Apellido_Paterno", "Clave", "Curp", "Edo_Civil", "Email", "Fecha_Inicio", "Fecha_Nacimiento", "Fecha_Termino", "Fotografia", "Id_Tipo_Empleado", "NSS", "Nombres", "RFC", "Sexo", "Tel" },
                values: new object[] { 1, "User", "Admin", "SUPERADMIN", "XXXX010101HXXXTX01", "Soltero/a", "superadmin@example.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "00000000000", "Super", "XAXX010101000", "M", "555-0100" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Empleados_Perfil",
                keyColumn: "Id_Empleado",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empleados",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Puestos",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ubicaciones",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Areas",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion", "Nombre" },
                values: new object[] { "AR1", "AREA PRUEBA", "AREA PRUEBA" });

            migrationBuilder.UpdateData(
                table: "Departamentos",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion", "Nombre" },
                values: new object[] { "DEP1", "DEPARTAMENTO PRUEBA", "DEPARTAMENTO PRUEBA" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Descripcion", "Prefijo" },
                values: new object[] { "COLABORADOR DE CONFIANZA", "CNF" });

            migrationBuilder.UpdateData(
                table: "Empleados_Tipo",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Descripcion", "Prefijo", "Titulo" },
                values: new object[] { "TECNICO OPERATIVO CON HRS. EXTRA", "PLA", "DE PLANTA" });

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

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Direccion", "Fecha_Creacion", "RFC", "Razon_Social" },
                values: new object[] { "EMP1", "", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "EMPRESA PRUEBA" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Clave", "Descripcion" },
                values: new object[] { "DIR", "DIRECCION GENERAL" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Clave", "Descripcion", "Titulo" },
                values: new object[] { "DAR", "DIRECCION DE AREA O UNIDAD", "DIRECCION" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 3,
                column: "Descripcion",
                value: "GERENCIA DE AREA O DEPARTAMENTO");

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "Clave", "Descripcion", "Titulo" },
                values: new object[] { "CRD", "COORDINACION/JEFATURA DE DEPTO. U OFICINA", "COORDINACION" });

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 6,
                column: "Descripcion",
                value: "SUPERVISION DE DEPTO. U OFICINA");

            migrationBuilder.UpdateData(
                table: "Jerarquias",
                keyColumn: "ID",
                keyValue: 7,
                column: "Descripcion",
                value: "COLABORACION AUX. ADMVA. U OPERATIVA");

            migrationBuilder.UpdateData(
                table: "Organizaciones",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Clave", "Fecha_Creacion", "Nombre" },
                values: new object[] { "ORG1", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CONGLOMERADO" });
        }
    }
}
