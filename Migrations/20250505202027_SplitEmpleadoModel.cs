using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SplitEmpleadoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido_Materno",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Apellido_Paterno",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Clave",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Email_corporativo",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Fecha_Inicio",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Fecha_Nacimiento",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Fecha_Termino",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Fotografia",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "NSS",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "RFC",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "Empleados");

            migrationBuilder.CreateTable(
                name: "Empleados_Perfil",
                columns: table => new
                {
                    Id_Empleado = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido_Paterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido_Materno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Edo_Civil = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Fecha_Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Termino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NSS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Curp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Tipo_Empleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados_Perfil", x => x.Id_Empleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Perfil_Empleados_Id_Empleado",
                        column: x => x.Id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados_Perfil");

            migrationBuilder.AddColumn<string>(
                name: "Apellido_Materno",
                table: "Empleados",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellido_Paterno",
                table: "Empleados",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Clave",
                table: "Empleados",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email_corporativo",
                table: "Empleados",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Inicio",
                table: "Empleados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Nacimiento",
                table: "Empleados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Termino",
                table: "Empleados",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Fotografia",
                table: "Empleados",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NSS",
                table: "Empleados",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Empleados",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RFC",
                table: "Empleados",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "Empleados",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
