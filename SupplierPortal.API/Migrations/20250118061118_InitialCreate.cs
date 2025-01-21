using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupplierPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesCompra_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesCompra_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoSolicitudCompra",
                columns: table => new
                {
                    ProductosId = table.Column<int>(type: "int", nullable: false),
                    SolicitudesCompraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoSolicitudCompra", x => new { x.ProductosId, x.SolicitudesCompraId });
                    table.ForeignKey(
                        name: "FK_ProductoSolicitudCompra_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoSolicitudCompra_SolicitudesCompra_SolicitudesCompraId",
                        column: x => x.SolicitudesCompraId,
                        principalTable: "SolicitudesCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Enlace", "Nombre", "PrecioUnitario" },
                values: new object[,]
                {
                    { 1, "https://example.com/producto1", "Producto 1", 10.5m },
                    { 2, "https://example.com/producto2", "Producto 2", 20m },
                    { 3, "https://example.com/producto3", "Producto 3", 15m },
                    { 4, "https://example.com/producto4", "Producto 4", 50m }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "Id", "Contacto", "Direccion", "Nombre" },
                values: new object[,]
                {
                    { 1, "contacto@proveedora.com", "Calle 123", "Proveedor A" },
                    { 2, "contacto@proveedorb.com", "Avenida 456", "Proveedor B" },
                    { 3, "contacto@proveedorc.com", "Pasaje 789", "Proveedor C" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contraseña", "NombreUsuario", "Rol" },
                values: new object[,]
                {
                    { 1, "colocador123", "colocador1", "Colocador" },
                    { 2, "aprobador123", "aprobador1", "Aprobador" }
                });

            migrationBuilder.InsertData(
                table: "SolicitudesCompra",
                columns: new[] { "Id", "Estado", "Fecha", "ProveedorId", "UsuarioId" },
                values: new object[] { 1, "Pendiente", new DateTime(2025, 1, 18, 1, 11, 17, 128, DateTimeKind.Local).AddTicks(1460), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSolicitudCompra_SolicitudesCompraId",
                table: "ProductoSolicitudCompra",
                column: "SolicitudesCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCompra_ProveedorId",
                table: "SolicitudesCompra",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCompra_UsuarioId",
                table: "SolicitudesCompra",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductoSolicitudCompra");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "SolicitudesCompra");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
