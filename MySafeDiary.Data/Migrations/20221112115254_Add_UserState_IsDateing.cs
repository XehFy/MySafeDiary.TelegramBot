using Microsoft.EntityFrameworkCore.Migrations;

namespace MySafeDiary.Data.Migrations
{
    public partial class Add_UserState_IsDateing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDateing",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDateing",
                table: "Users");
        }
    }
}
