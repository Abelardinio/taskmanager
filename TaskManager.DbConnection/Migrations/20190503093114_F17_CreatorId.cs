using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.DbConnection.Migrations
{
    public partial class F17_CreatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Projects",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Projects");
        }
    }
}
