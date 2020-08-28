using Microsoft.EntityFrameworkCore.Migrations;

namespace FANT2.Migrations
{
    public partial class addNavigationproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonse_Category_KategoriId",
                table: "Annonse");

            migrationBuilder.DropIndex(
                name: "IX_Annonse_KategoriId",
                table: "Annonse");

            migrationBuilder.DropColumn(
                name: "KategoriId",
                table: "Annonse");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Annonse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Annonse_CategoryId",
                table: "Annonse",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonse_Category_CategoryId",
                table: "Annonse",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Annonse_Category_CategoryId",
                table: "Annonse");

            migrationBuilder.DropIndex(
                name: "IX_Annonse_CategoryId",
                table: "Annonse");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Annonse");

            migrationBuilder.AddColumn<int>(
                name: "KategoriId",
                table: "Annonse",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Annonse_KategoriId",
                table: "Annonse",
                column: "KategoriId");

            migrationBuilder.AddForeignKey(
                name: "FK_Annonse_Category_KategoriId",
                table: "Annonse",
                column: "KategoriId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
