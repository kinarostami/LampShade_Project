using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Infrastucture.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ArticleCategoryPictureAltandPictureTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureAlt",
                table: "ArticleCategories",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PictureTitle",
                table: "ArticleCategories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureAlt",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "PictureTitle",
                table: "ArticleCategories");
        }
    }
}
