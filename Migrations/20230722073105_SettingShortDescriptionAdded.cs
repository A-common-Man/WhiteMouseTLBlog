using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhiteMouseTLBlog.Migrations
{
    public partial class SettingShortDescriptionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Settings");
        }
    }
}
