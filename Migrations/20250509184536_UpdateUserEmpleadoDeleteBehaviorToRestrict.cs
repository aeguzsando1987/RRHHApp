using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEmpleadoDeleteBehaviorToRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empleados_Id_Empleado",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empleados_Id_Empleado",
                table: "AspNetUsers",
                column: "Id_Empleado",
                principalTable: "Empleados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empleados_Id_Empleado",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empleados_Id_Empleado",
                table: "AspNetUsers",
                column: "Id_Empleado",
                principalTable: "Empleados",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
