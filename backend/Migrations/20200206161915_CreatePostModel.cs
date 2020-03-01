using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class CreatePostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Posts",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AuthorId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(),
                    UpdatedAt = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        "FK_Posts_AspNetUsers_AuthorId",
                        x => x.AuthorId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Posts_AuthorId",
                "Posts",
                "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Posts");
        }
    }
}