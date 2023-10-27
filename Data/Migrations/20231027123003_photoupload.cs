using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitycrudMVC.Data.Migrations
{
    public partial class photoupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "Photos");
        }
    }
}
