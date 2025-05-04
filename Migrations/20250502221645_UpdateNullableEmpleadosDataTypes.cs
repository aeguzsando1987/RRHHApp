using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNullableEmpleadosDataTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Direccion_Empleados_Id_Empleado",
                table: "Empleados_Direccion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados_Direccion",
                table: "Empleados_Direccion");

            migrationBuilder.RenameTable(
                name: "Empleados_Direccion",
                newName: "Empleados_Direcciones");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_Direccion_Id_Empleado",
                table: "Empleados_Direcciones",
                newName: "IX_Empleados_Direcciones_Id_Empleado");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Termino",
                table: "Empleados",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados_Direcciones",
                table: "Empleados_Direcciones",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Direcciones_Empleados_Id_Empleado",
                table: "Empleados_Direcciones",
                column: "Id_Empleado",
                principalTable: "Empleados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Direcciones_Empleados_Id_Empleado",
                table: "Empleados_Direcciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados_Direcciones",
                table: "Empleados_Direcciones");

            migrationBuilder.RenameTable(
                name: "Empleados_Direcciones",
                newName: "Empleados_Direccion");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_Direcciones_Id_Empleado",
                table: "Empleados_Direccion",
                newName: "IX_Empleados_Direccion_Id_Empleado");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Fecha_Termino",
                table: "Empleados",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados_Direccion",
                table: "Empleados_Direccion",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Direccion_Empleados_Id_Empleado",
                table: "Empleados_Direccion",
                column: "Id_Empleado",
                principalTable: "Empleados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
