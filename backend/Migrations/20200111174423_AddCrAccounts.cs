using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddCrAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "CrAccount",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    IsVerified = table.Column<bool>(),
                    CreatedAt = table.Column<DateTime>(),
                    VerifiedAt = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrAccount", x => x.Id);
                    table.ForeignKey(
                        "FK_CrAccount_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_CrAccount_UserId",
                "CrAccount",
                "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "CrAccount");
        }
    }
}