using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMiaPizzeriaEFRelazione1n.Migrations
{
    /// <inheritdoc />
    public partial class relazione1nPizzeria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_CategoriaId",
                table: "Pizza",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Categoria_CategoriaId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_CategoriaId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Pizza");
        }
    }
}
