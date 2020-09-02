using Microsoft.EntityFrameworkCore.Migrations;

namespace FANT2.Migrations
{
    public partial class addedPos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Annonse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "Annonse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Annonse");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Annonse");
        }
    }
}
