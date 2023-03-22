using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppMvcBasica.Data.Migrations
{
    public partial class ApplicationDbContextModelSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Enderecos");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Enderecos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Enderecos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Enderecos",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Enderecos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Enderecos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                unique: true);
        }
    }
}
