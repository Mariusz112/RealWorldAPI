using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorldApp.DAL.Migrations
{
    public partial class articleAuthorAndSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Title");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Title",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Title",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Title",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Title",
                type: "nvarchar(5000)",
                nullable: true,
                computedColumnSql: "[Title] + '-' + cast([Id] as nvarchar(2000))");

            migrationBuilder.CreateIndex(
                name: "IX_Title_AuthorId",
                table: "Title",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Title_AspNetUsers_AuthorId",
                table: "Title",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Title_AspNetUsers_AuthorId",
                table: "Title");

            migrationBuilder.DropIndex(
                name: "IX_Title_AuthorId",
                table: "Title");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Title");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Title");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Title");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Title",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
