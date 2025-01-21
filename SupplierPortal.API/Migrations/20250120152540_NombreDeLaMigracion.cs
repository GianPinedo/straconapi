using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplierPortal.API.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameTable(
                name: "ProductoSolicitudCompra",
                newName: "ProductosSolicitudesCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoSolicitudCompra_SolicitudCompraId",
                table: "ProductosSolicitudesCompra",
                newName: "IX_ProductosSolicitudesCompra_SolicitudCompraId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductoSolicitudCompra_ProductoId",
                table: "ProductosSolicitudesCompra",
                newName: "IX_ProductosSolicitudesCompra_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductosSolicitudesCompra",
                table: "ProductosSolicitudesCompra",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosSolicitudesCompra_Productos_ProductoId",
                table: "ProductosSolicitudesCompra",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosSolicitudesCompra_SolicitudesCompra_SolicitudCompraId",
                table: "ProductosSolicitudesCompra",
                column: "SolicitudCompraId",
                principalTable: "SolicitudesCompra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSolicitudesCompra_Productos_ProductoId",
                table: "ProductosSolicitudesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductosSolicitudesCompra_SolicitudesCompra_SolicitudCompraId",
                table: "ProductosSolicitudesCompra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductosSolicitudesCompra",
                table: "ProductosSolicitudesCompra");

            migrationBuilder.RenameTable(
                name: "ProductosSolicitudesCompra",
                newName: "ProductoSolicitudCompra");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosSolicitudesCompra_SolicitudCompraId",
                table: "ProductoSolicitudCompra",
                newName: "IX_ProductoSolicitudCompra_SolicitudCompraId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosSolicitudesCompra_ProductoId",
                table: "ProductoSolicitudCompra",
                newName: "IX_ProductoSolicitudCompra_ProductoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductoSolicitudCompra",
                table: "ProductoSolicitudCompra",
                column: "Id");

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
    }
}
