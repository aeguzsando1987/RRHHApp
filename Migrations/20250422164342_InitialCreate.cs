using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RRHH.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jerarquias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jerarquias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Organizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Org = table.Column<int>(type: "int", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Razon_Social = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Empresas_Organizaciones_Id_Org",
                        column: x => x.Id_Org,
                        principalTable: "Organizaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id_Empresa = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Areas_Empresas_Id_Empresa",
                        column: x => x.Id_Empresa,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id_Area = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Departamentos_Areas_Id_Area",
                        column: x => x.Id_Area,
                        principalTable: "Areas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Puestos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Id_Departamento = table.Column<int>(type: "int", nullable: false),
                    Id_Jerarquia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puestos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Puestos_Departamentos_Id_Departamento",
                        column: x => x.Id_Departamento,
                        principalTable: "Departamentos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Puestos_Jerarquias_Id_Jerarquia",
                        column: x => x.Id_Jerarquia,
                        principalTable: "Jerarquias",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido_Paterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido_Materno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Termino = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email_corporativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NSS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id_Status = table.Column<int>(type: "int", nullable: false),
                    Id_Puesto = table.Column<int>(type: "int", nullable: false),
                    Id_Jefe = table.Column<int>(type: "int", nullable: true),
                    Id_Ubicacion = table.Column<int>(type: "int", nullable: false),
                    Fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Empleados_Empleados_Id_Jefe",
                        column: x => x.Id_Jefe,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Puestos_Id_Puesto",
                        column: x => x.Id_Puesto,
                        principalTable: "Puestos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_Statuses_Id_Status",
                        column: x => x.Id_Status,
                        principalTable: "Statuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_Ubicaciones_Id_Ubicacion",
                        column: x => x.Id_Ubicacion,
                        principalTable: "Ubicaciones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Empleado = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Empleados_Id_Empleado",
                        column: x => x.Id_Empleado,
                        principalTable: "Empleados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Id_Empresa",
                table: "Areas",
                column: "Id_Empresa");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_Id_Area",
                table: "Departamentos",
                column: "Id_Area");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Jefe",
                table: "Empleados",
                column: "Id_Jefe");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Puesto",
                table: "Empleados",
                column: "Id_Puesto");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Status",
                table: "Empleados",
                column: "Id_Status");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Id_Ubicacion",
                table: "Empleados",
                column: "Id_Ubicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_Id_Org",
                table: "Empresas",
                column: "Id_Org");

            migrationBuilder.CreateIndex(
                name: "IX_Puestos_Id_Departamento",
                table: "Puestos",
                column: "Id_Departamento");

            migrationBuilder.CreateIndex(
                name: "IX_Puestos_Id_Jerarquia",
                table: "Puestos",
                column: "Id_Jerarquia");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Empleado",
                table: "Users",
                column: "Id_Empleado",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Puestos");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Jerarquias");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Organizaciones");
        }
    }
}
