using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmployeeUbicacionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Ubicaciones");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Statuses");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Ubicaciones",
                newName: "Ubicacion_Referencial");

            migrationBuilder.AddColumn<int>(
                name: "Id_Empresa",
                table: "Ubicaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion_Status",
                table: "Statuses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status_Emp",
                table: "Statuses",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id_Ubicacion",
                table: "Empleados",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicaciones_Id_Empresa",
                table: "Ubicaciones",
                column: "Id_Empresa");

            migrationBuilder.AddForeignKey(
                name: "FK_Ubicaciones_Empresas_Id_Empresa",
                table: "Ubicaciones",
                column: "Id_Empresa",
                principalTable: "Empresas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ubicaciones_Empresas_Id_Empresa",
                table: "Ubicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Ubicaciones_Id_Empresa",
                table: "Ubicaciones");

            migrationBuilder.DropColumn(
                name: "Id_Empresa",
                table: "Ubicaciones");

            migrationBuilder.DropColumn(
                name: "Descripcion_Status",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Status_Emp",
                table: "Statuses");

            migrationBuilder.RenameColumn(
                name: "Ubicacion_Referencial",
                table: "Ubicaciones",
                newName: "Descripcion");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Ubicaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Statuses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id_Ubicacion",
                table: "Empleados",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
