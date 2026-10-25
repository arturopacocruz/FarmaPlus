using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmaPlus.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    CodigoBarras = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RequiereReceta = table.Column<bool>(type: "boolean", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Fármacos y tratamientos terapéuticos", "Medicamentos" },
                    { 2, "Higiene y cuidado corporal", "Cuidado Personal" },
                    { 3, "Nutrición y complementos vitamínicos", "Vitaminas y Suplementos" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "CodigoBarras", "Descripcion", "FechaVencimiento", "Nombre", "Precio", "RequiereReceta", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "7750123456789", "Analgésico y antipirético en tabletas", new DateTime(2027, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paracetamol 500mg", 3.50m, false, 120 },
                    { 2, 1, "7750987654321", "Antibiótico de amplio espectro en cápsulas", new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Amoxicilina 500mg", 12.00m, true, 50 },
                    { 3, 2, "7751122334455", "Antiséptico antibacterial para manos", new DateTime(2028, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alcohol en Gel 500ml", 7.50m, false, 80 },
                    { 4, 3, "7755566778899", "Tabletas efervescentes para reforzar defensas", new DateTime(2027, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vitamina C + Zinc 1000mg", 15.00m, false, 65 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
