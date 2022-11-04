using Microsoft.EntityFrameworkCore.Migrations;

namespace MySafeDiary.Data.Migrations
{
    public partial class UserId_DiaryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_Users_UserId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Notes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Diaries",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_Users_UserId",
                table: "Diaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                table: "Notes",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diaries_Users_UserId",
                table: "Diaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                table: "Notes");

            migrationBuilder.AlterColumn<int>(
                name: "DiaryId",
                table: "Notes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Diaries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Diaries_Users_UserId",
                table: "Diaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Diaries_DiaryId",
                table: "Notes",
                column: "DiaryId",
                principalTable: "Diaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
