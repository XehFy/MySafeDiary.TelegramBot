using Microsoft.EntityFrameworkCore.Migrations;

namespace MySafeDiary.Data.Migrations
{
    public partial class UserStatesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailing",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNoteing",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPasswording",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailing",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsNoteing",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPasswording",
                table: "Users");
        }
    }
}
