using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Infrastructure.Data.Migrations
{
    public partial class AddSomeCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Comedies')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Dramas')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Science Fiction')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Romance')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Description]) VALUES ('Thriller')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Categories]");
        }
    }
}
