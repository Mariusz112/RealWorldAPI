using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorldApp.DAL.Migrations
{
    public partial class article_description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Title",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Title");
        }
    }
}
