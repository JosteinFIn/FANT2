using Microsoft.EntityFrameworkCore.Migrations;

namespace FANT2.Data.Migrations
{
    public partial class ClientToAnnonse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Annonse",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Annonse");
        }
    }
}
