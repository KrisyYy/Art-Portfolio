using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtPortfolio.Data.Migrations
{
    public partial class ArtworksCommentsandCommissionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Artworks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Artworks");
        }
    }
}
