using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SupplierPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductoSolicitudCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoSolicitudCompra_Productos_ProductosId",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoSolicitudCompra_SolicitudesCompra_SolicitudesCompraId",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoSolicitudCompra",
                table: "ProductoSolicitudCompra");

            migrationBuilder.RenameColumn(
                name: "SolicitudesCompraId",
                table: "ProductoSolicitudCompra",
                newName: "SolicitudCompraId");

            migrationBuilder.RenameColumn(
                name: "ProductosId",
                table: "ProductoSolicitudCompra",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoSolicitudCompra_SolicitudesCompraId",
                table: "ProductoSolicitudCompra",
                newName: "IX_ProductoSolicitudCompra_SolicitudCompraId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductoSolicitudCompra",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ProductoSolicitudCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "ProductoSolicitudCompra",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoSolicitudCompra",
                table: "ProductoSolicitudCompra",
                column: "Id");

            migrationBuilder.InsertData(
                table: "ProductoSolicitudCompra",
                columns: new[] { "Id", "Cantidad", "PrecioUnitario", "ProductoId", "SolicitudCompraId" },
                values: new object[,]
                {
                    { 1, 2, 10.5m, 1, 1 },
                    { 2, 1, 20m, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductoSolicitudCompra_ProductoId",
                table: "ProductoSolicitudCompra",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoSolicitudCompra_Productos_ProductoId",
                table: "ProductoSolicitudCompra",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoSolicitudCompra_SolicitudesCompra_SolicitudCompraId",
                table: "ProductoSolicitudCompra",
                column: "SolicitudCompraId",
                principalTable: "SolicitudesCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductoSolicitudCompra_Productos_ProductoId",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoSolicitudCompra_SolicitudesCompra_SolicitudCompraId",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductoSolicitudCompra",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropIndex(
                name: "IX_ProductoSolicitudCompra_ProductoId",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DeleteData(
                table: "ProductoSolicitudCompra",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductoSolicitudCompra",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ProductoSolicitudCompra");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "ProductoSolicitudCompra");

            migrationBuilder.RenameColumn(
                name: "SolicitudCompraId",
                table: "ProductoSolicitudCompra",
                newName: "SolicitudesCompraId");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "ProductoSolicitudCompra",
                newName: "ProductosId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoSolicitudCompra_SolicitudCompraId",
                table: "ProductoSolicitudCompra",
                newName: "IX_ProductoSolicitudCompra_SolicitudesCompraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoSolicitudCompra",
                table: "ProductoSolicitudCompra",
                columns: new[] { "ProductosId", "SolicitudesCompraId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoSolicitudCompra_Productos_ProductosId",
                table: "ProductoSolicitudCompra",
                column: "ProductosId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoSolicitudCompra_SolicitudesCompra_SolicitudesCompraId",
                table: "ProductoSolicitudCompra",
                column: "SolicitudesCompraId",
                principalTable: "SolicitudesCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
