using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresasContactoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactosEmpresas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Nombre_Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Puesto_Ref = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosEmpresas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactosEmpresas_Empresas_Id_Empresa",
                        column: x => x.Id_Empresa,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "ID", "Clave", "Direccion", "Fecha_Creacion", "Id_Org", "RFC", "Razon_Social" },
                values: new object[] { 1, "EMP1", "", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", "EMPRESA PRUEBA" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Descripcion_Status", "Status_Emp" },
                values: new object[] { 5, "EMPLEADO INACTIVO POR INCAPACIDAD MEDICA", "INCAPACIDAD" });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "ID", "Clave", "Descripcion", "Id_Empresa", "Nombre" },
                values: new object[] { 1, "AR1", "AREA PRUEBA", 1, "AREA PRUEBA" });

            migrationBuilder.CreateIndex(
                name: "IX_ContactosEmpresas_Id_Empresa",
                table: "ContactosEmpresas",
                column: "Id_Empresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactosEmpresas");

            migrationBuilder.DeleteData(
                table: "Areas",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empresas",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
