using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcessoAPI.Migrations
{
    /// <inheritdoc />
    public partial class NOTPRODUAJUSTES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Clientes_ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropIndex(
                name: "IX_NotasFiscais_ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "ClienteID",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "NumeroNota",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "NotasFiscais",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "DataEmissao",
                table: "NotasFiscais",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "NotasFiscais",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "NotasFiscais",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "NotasFiscais");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "NotasFiscais",
                newName: "ValorTotal");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "NotasFiscais",
                newName: "DataEmissao");

            migrationBuilder.AddColumn<int>(
                name: "ClienteID",
                table: "NotasFiscais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "NotasFiscais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "NotasFiscais",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NumeroNota",
                table: "NotasFiscais",
                type: "nvarchar(max)",
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

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

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
    }
}
