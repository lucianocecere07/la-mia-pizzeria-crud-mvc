using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMiaPizzeriaEFRelazione1n.Migrations
{
    /// <inheritdoc />
    public partial class relazione1n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Pizza",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "Pizza",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }
    }
}
