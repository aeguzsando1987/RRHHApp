using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPuestoDescriptivoFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PuestosActividad_PuestosDescriptivo_ID_PuestoDescriptivo",
                table: "PuestosActividad");

            migrationBuilder.DropIndex(
                name: "IX_PuestosDescriptivo_ID_Puesto",
                table: "PuestosDescriptivo");

            migrationBuilder.RenameColumn(
                name: "ID_PuestoDescriptivo",
                table: "PuestosActividad",
                newName: "ID_PuestosDescriptivo");

            migrationBuilder.RenameIndex(
                name: "IX_PuestosActividad_ID_PuestoDescriptivo",
                table: "PuestosActividad",
                newName: "IX_PuestosActividad_ID_PuestosDescriptivo");

            migrationBuilder.CreateIndex(
                name: "IX_PuestosDescriptivo_ID_Puesto",
                table: "PuestosDescriptivo",
                column: "ID_Puesto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PuestosActividad_PuestosDescriptivo_ID_PuestosDescriptivo",
                table: "PuestosActividad",
                column: "ID_PuestosDescriptivo",
                principalTable: "PuestosDescriptivo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PuestosActividad_PuestosDescriptivo_ID_PuestosDescriptivo",
                table: "PuestosActividad");

            migrationBuilder.DropIndex(
                name: "IX_PuestosDescriptivo_ID_Puesto",
                table: "PuestosDescriptivo");

            migrationBuilder.RenameColumn(
                name: "ID_PuestosDescriptivo",
                table: "PuestosActividad",
                newName: "ID_PuestoDescriptivo");

            migrationBuilder.RenameIndex(
                name: "IX_PuestosActividad_ID_PuestosDescriptivo",
                table: "PuestosActividad",
                newName: "IX_PuestosActividad_ID_PuestoDescriptivo");

            migrationBuilder.CreateIndex(
                name: "IX_PuestosDescriptivo_ID_Puesto",
                table: "PuestosDescriptivo",
                column: "ID_Puesto");

            migrationBuilder.AddForeignKey(
                name: "FK_PuestosActividad_PuestosDescriptivo_ID_PuestoDescriptivo",
                table: "PuestosActividad",
                column: "ID_PuestoDescriptivo",
                principalTable: "PuestosDescriptivo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
