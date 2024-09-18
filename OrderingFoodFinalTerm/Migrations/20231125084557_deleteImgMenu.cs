using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderingFoodFinalTerm.Migrations
{
    public partial class deleteImgMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Menus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
