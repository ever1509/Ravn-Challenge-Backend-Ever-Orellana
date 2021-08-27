using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Infrastructure.Data.Migrations
{
    public partial class AddChangesFieldsMovieRatesChangeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieRates",
                table: "MovieRates");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "MovieRates",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "MovieRateId",
                table: "MovieRates",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieRates",
                table: "MovieRates",
                column: "MovieRateId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRates_MovieId",
                table: "MovieRates",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieRates",
                table: "MovieRates");

            migrationBuilder.DropIndex(
                name: "IX_MovieRates_MovieId",
                table: "MovieRates");

            migrationBuilder.DropColumn(
                name: "MovieRateId",
                table: "MovieRates");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "MovieRates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieRates",
                table: "MovieRates",
                columns: new[] { "MovieId", "UserID" });
        }
    }
}
