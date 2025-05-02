using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpresasDireccionesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas_Direcciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Numero_Ext = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Numero_Int = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Colonia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo_Postal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Referencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo_Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Modificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas_Direcciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Empresas_Direcciones_Empresas_Id_Empresa",
                        column: x => x.Id_Empresa,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Direcciones_Id_Empresa",
                table: "Empresas_Direcciones",
                column: "Id_Empresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas_Direcciones");
        }
    }
}
