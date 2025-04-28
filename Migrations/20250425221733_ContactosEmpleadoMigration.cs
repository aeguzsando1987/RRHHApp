using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ContactosEmpleadoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados");

            migrationBuilder.CreateTable(
                name: "ContactosEmpleados",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Empleado = table.Column<int>(type: "int", nullable: false),
                    Nombre_Contacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Relacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosEmpleados", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactosEmpleados_Empleados_Id_Empleado",
                        column: x => x.Id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados",
                column: "Id_Puesto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactosEmpleados_Id_Empleado",
                table: "ContactosEmpleados",
                column: "Id_Empleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactosEmpleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados",
                column: "Id_Puesto");
        }
    }
}
