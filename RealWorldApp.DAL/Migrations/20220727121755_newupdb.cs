using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealWorldApp.DAL.Migrations
{
    public partial class newupdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Article_ArticlesId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_ArticlesId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ArticlesId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "ArticlesTags",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlesTags", x => new { x.ArticlesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ArticlesTags_Article_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlesTags_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlesTags_TagsId",
                table: "ArticlesTags",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlesTags");

            migrationBuilder.AddColumn<int>(
                name: "ArticlesId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ArticlesId",
                table: "Tag",
                column: "ArticlesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Article_ArticlesId",
                table: "Tag",
                column: "ArticlesId",
                principalTable: "Article",
                principalColumn: "Id");
        }
    }
}
