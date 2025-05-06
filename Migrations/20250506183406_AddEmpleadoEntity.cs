using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpleadoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo_Empleado",
                table: "Empleados_Perfil",
                newName: "Id_Tipo_Empleado");

            migrationBuilder.CreateTable(
                name: "Empleados_Tipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Prefijo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados_Tipo", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Empleados_Tipo",
                columns: new[] { "ID", "Descripcion", "Prefijo", "Titulo" },
                values: new object[,]
                {
                    { 1, "COLABORADOR DE CONFIANZA", "Prefijo 1", "CONFIANZA" },
                    { 2, "TECNICO OPERATIVO CON HRS. EXTRA", "Prefijo 2", "OPERATIVO" },
                    { 3, "Descripcion 3", "Prefijo 3", "Tipo 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Perfil_Id_Tipo_Empleado",
                table: "Empleados_Perfil",
                column: "Id_Tipo_Empleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Perfil_Empleados_Tipo_Id_Tipo_Empleado",
                table: "Empleados_Perfil",
                column: "Id_Tipo_Empleado",
                principalTable: "Empleados_Tipo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Perfil_Empleados_Tipo_Id_Tipo_Empleado",
                table: "Empleados_Perfil");

            migrationBuilder.DropTable(
                name: "Empleados_Tipo");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Perfil_Id_Tipo_Empleado",
                table: "Empleados_Perfil");

            migrationBuilder.RenameColumn(
                name: "Id_Tipo_Empleado",
                table: "Empleados_Perfil",
                newName: "Tipo_Empleado");
        }
    }
}
