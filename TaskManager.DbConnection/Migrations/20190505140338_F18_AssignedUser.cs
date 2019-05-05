using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.DbConnection.Migrations
{
    public partial class F18_AssignedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Tasks");
        }
    }
}
