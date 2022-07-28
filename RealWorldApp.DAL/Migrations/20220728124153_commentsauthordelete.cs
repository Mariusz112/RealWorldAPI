using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorldApp.DAL.Migrations
{
    public partial class commentsauthordelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Article_ArticlesId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ArticlesId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Article_ArticlesId",
                table: "Comment",
                column: "ArticlesId",
                principalTable: "Article",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Article_ArticlesId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ArticlesId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Article_ArticlesId",
                table: "Comment",
                column: "ArticlesId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
