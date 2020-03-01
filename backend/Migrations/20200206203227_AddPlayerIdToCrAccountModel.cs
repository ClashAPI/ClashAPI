using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddPlayerIdToCrAccountModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_CrAccount_AspNetUsers_UserId",
                "CrAccount");

            migrationBuilder.DropPrimaryKey(
                "PK_CrAccount",
                "CrAccount");

            migrationBuilder.RenameTable(
                "CrAccount",
                newName: "CrAccounts");

            migrationBuilder.RenameIndex(
                "IX_CrAccount_UserId",
                table: "CrAccounts",
                newName: "IX_CrAccounts_UserId");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "Content",
                "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "ExpiresAt",
                "CrAccounts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                "PlayerId",
                "CrAccounts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                "PK_CrAccounts",
                "CrAccounts",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_CrAccounts_AspNetUsers_UserId",
                "CrAccounts",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_CrAccounts_AspNetUsers_UserId",
                "CrAccounts");

            migrationBuilder.DropPrimaryKey(
                "PK_CrAccounts",
                "CrAccounts");

            migrationBuilder.DropColumn(
                "ExpiresAt",
                "CrAccounts");

            migrationBuilder.DropColumn(
                "PlayerId",
                "CrAccounts");

            migrationBuilder.RenameTable(
                "CrAccounts",
                newName: "CrAccount");

            migrationBuilder.RenameIndex(
                "IX_CrAccounts_UserId",
                table: "CrAccount",
                newName: "IX_CrAccount_UserId");

            migrationBuilder.AlterColumn<string>(
                "Title",
                "Posts",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                "Content",
                "Posts",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                "PK_CrAccount",
                "CrAccount",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_CrAccount_AspNetUsers_UserId",
                "CrAccount",
                "UserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}