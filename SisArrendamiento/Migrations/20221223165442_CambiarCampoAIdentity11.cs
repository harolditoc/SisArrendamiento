using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisArrendamiento.Migrations
{
    /// <inheritdoc />
    public partial class CambiarCampoAIdentity11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arrendador",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    cedulaidentidad = table.Column<string>(name: "cedula_identidad", type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("arrendador_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "arrendatario",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    apellidos = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    cedulaidentidad = table.Column<string>(name: "cedula_identidad", type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    fechaingreso = table.Column<DateTime>(name: "fecha_ingreso", type: "date", nullable: false),
                    fechanacimiento = table.Column<DateTime>(name: "fecha_nacimiento", type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("arrendatario_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "cuarto",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    piso = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    zona = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cuarto_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "luz_baño",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anteriorluzlectura = table.Column<decimal>(name: "anterior_luz_lectura", type: "decimal(9,2)", nullable: true),
                    actualluzlectura = table.Column<decimal>(name: "actual_luz_lectura", type: "decimal(9,2)", nullable: true),
                    kwconsumido = table.Column<decimal>(name: "kw_consumido", type: "decimal(9,2)", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(9,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("luz_baño_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "luz_cuarto",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anteriorluzlectura = table.Column<decimal>(name: "anterior_luz_lectura", type: "decimal(9,2)", nullable: true),
                    actualluzlectura = table.Column<decimal>(name: "actual_luz_lectura", type: "decimal(9,2)", nullable: true),
                    kwconsumido = table.Column<decimal>(name: "kw_consumido", type: "decimal(9,2)", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(9,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("luz_cuarto_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "luz_escalera",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    anteriorluzlectura = table.Column<decimal>(name: "anterior_luz_lectura", type: "decimal(9,2)", nullable: true),
                    actualluzlectura = table.Column<decimal>(name: "actual_luz_lectura", type: "decimal(9,2)", nullable: true),
                    kwconsumido = table.Column<decimal>(name: "kw_consumido", type: "decimal(9,2)", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(9,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("luz_escalera_PK", x => x.codigo);
                });

            migrationBuilder.CreateTable(
                name: "conviviente",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombres = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    cedulaidentidad = table.Column<string>(name: "cedula_identidad", type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    arrendatariocodigo = table.Column<int>(name: "arrendatario_codigo", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("conviviente_PK", x => x.codigo);
                    table.ForeignKey(
                        name: "conviviente_arrendatario_FK",
                        column: x => x.arrendatariocodigo,
                        principalTable: "arrendatario",
                        principalColumn: "codigo");
                });

            migrationBuilder.CreateTable(
                name: "alquiler",
                columns: table => new
                {
                    codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    alquilermensual = table.Column<decimal>(name: "alquiler_mensual", type: "decimal(9,2)", nullable: true),
                    agua = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    cable = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    fechaactual = table.Column<DateTime>(name: "fecha_actual", type: "date", nullable: false),
                    fechavencimiento = table.Column<DateTime>(name: "fecha_vencimiento", type: "date", nullable: false),
                    pagototal = table.Column<decimal>(name: "pago_total", type: "decimal(9,2)", nullable: true),
                    arrendadorcodigo = table.Column<int>(name: "arrendador_codigo", type: "int", nullable: false),
                    luzcuartocodigo = table.Column<int>(name: "luz_cuarto_codigo", type: "int", nullable: false),
                    luzbañocodigo = table.Column<int>(name: "luz_baño_codigo", type: "int", nullable: false),
                    luzescaleracodigo = table.Column<int>(name: "luz_escalera_codigo", type: "int", nullable: false),
                    arrendatariocodigo = table.Column<int>(name: "arrendatario_codigo", type: "int", nullable: false),
                    cuartocodigo = table.Column<int>(name: "cuarto_codigo", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("alquiler_PK", x => x.codigo);
                    table.ForeignKey(
                        name: "alquiler_arrendador_FK",
                        column: x => x.arrendadorcodigo,
                        principalTable: "arrendador",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "alquiler_arrendatario_FK",
                        column: x => x.arrendatariocodigo,
                        principalTable: "arrendatario",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "alquiler_cuarto_FK",
                        column: x => x.cuartocodigo,
                        principalTable: "cuarto",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "alquiler_luz_baño_FK",
                        column: x => x.luzbañocodigo,
                        principalTable: "luz_baño",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "alquiler_luz_cuarto_FK",
                        column: x => x.luzcuartocodigo,
                        principalTable: "luz_cuarto",
                        principalColumn: "codigo");
                    table.ForeignKey(
                        name: "alquiler_luz_escalera_FK",
                        column: x => x.luzescaleracodigo,
                        principalTable: "luz_escalera",
                        principalColumn: "codigo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_arrendador_codigo",
                table: "alquiler",
                column: "arrendador_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_arrendatario_codigo",
                table: "alquiler",
                column: "arrendatario_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_cuarto_codigo",
                table: "alquiler",
                column: "cuarto_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_luz_baño_codigo",
                table: "alquiler",
                column: "luz_baño_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_luz_cuarto_codigo",
                table: "alquiler",
                column: "luz_cuarto_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_alquiler_luz_escalera_codigo",
                table: "alquiler",
                column: "luz_escalera_codigo");

            migrationBuilder.CreateIndex(
                name: "IX_conviviente_arrendatario_codigo",
                table: "conviviente",
                column: "arrendatario_codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alquiler");

            migrationBuilder.DropTable(
                name: "conviviente");

            migrationBuilder.DropTable(
                name: "arrendador");

            migrationBuilder.DropTable(
                name: "cuarto");

            migrationBuilder.DropTable(
                name: "luz_baño");

            migrationBuilder.DropTable(
                name: "luz_cuarto");

            migrationBuilder.DropTable(
                name: "luz_escalera");

            migrationBuilder.DropTable(
                name: "arrendatario");
        }
    }
}
