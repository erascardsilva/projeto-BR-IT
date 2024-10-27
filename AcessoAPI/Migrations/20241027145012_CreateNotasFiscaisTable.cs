using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotasFiscaisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Fornecedores");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Fornecedores");

            migrationBuilder.RenameColumn(
                name: "Estoque",
                table: "Produtos",
                newName: "FornecedorID");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Fornecedores",
                newName: "Contato");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "NotasFiscais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVenda",
                table: "NotasFiscais",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NotasFiscais",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoNota",
                table: "NotasFiscais",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_ClienteID",
                table: "NotasFiscais",
                column: "ClienteID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteID",
                table: "NotasFiscais",
                column: "ClienteID",
                principalTable: "Clientes",
                principalColumn: "ClienteID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "DataVenda",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "TipoNota",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FornecedorID",
                table: "Produtos",
                newName: "Estoque");

            migrationBuilder.RenameColumn(
                name: "Contato",
                table: "Fornecedores",
                newName: "CNPJ");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Fornecedores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Fornecedores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
