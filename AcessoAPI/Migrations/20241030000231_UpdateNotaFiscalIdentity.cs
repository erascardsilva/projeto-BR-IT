using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessoAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotaFiscalIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotasFiscais",
                table: "NotasFiscais");

            migrationBuilder.RenameTable(
                name: "NotasFiscais",
                newName: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "NotaFiscal",
                newName: "DataEmissao");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_ClienteID",
                table: "NotaFiscal",
                newName: "IX_NotaFiscal_ClienteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotaFiscal",
                table: "NotaFiscal",
                column: "NotaFiscalID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Clientes_ClienteID",
                table: "NotaFiscal",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Clientes_ClienteID",
                table: "NotaFiscal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotaFiscal",
                table: "NotaFiscal");

            migrationBuilder.RenameTable(
                name: "NotaFiscal",
                newName: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "DataEmissao",
                table: "NotasFiscais",
                newName: "Data");

            migrationBuilder.RenameIndex(
                name: "IX_NotaFiscal_ClienteID",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_ClienteID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotasFiscais",
                table: "NotasFiscais",
                column: "NotaFiscalID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteID",
                table: "NotasFiscais",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
