using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Infrastructure.Data.Migrations
{
    public partial class AddSomeChangesRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "MovieRates",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "MovieRates",
                newName: "UserId");
        }
    }
}
