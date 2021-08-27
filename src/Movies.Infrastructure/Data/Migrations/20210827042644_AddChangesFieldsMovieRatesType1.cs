using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Infrastructure.Data.Migrations
{
    public partial class AddChangesFieldsMovieRatesType1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "MovieRates",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "MovieRates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
