using Microsoft.EntityFrameworkCore.Migrations;

namespace Haik.Migrations
{
    public partial class haikdbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCommercial",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCommercial",
                table: "AspNetUsers");
        }
    }
}
