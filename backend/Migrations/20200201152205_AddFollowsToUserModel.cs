using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddFollowsToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ClanFollow",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClanTag = table.Column<string>(nullable: true),
                    ClanName = table.Column<string>(nullable: true),
                    FollowerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanFollow", x => x.Id);
                    table.ForeignKey(
                        "FK_ClanFollow_AspNetUsers_FollowerId",
                        x => x.FollowerId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "PlayerFollow",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerTag = table.Column<string>(nullable: true),
                    PlayerName = table.Column<string>(nullable: true),
                    FollowerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFollow", x => x.Id);
                    table.ForeignKey(
                        "FK_PlayerFollow_AspNetUsers_FollowerId",
                        x => x.FollowerId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_ClanFollow_FollowerId",
                "ClanFollow",
                "FollowerId");

            migrationBuilder.CreateIndex(
                "IX_PlayerFollow_FollowerId",
                "PlayerFollow",
                "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ClanFollow");

            migrationBuilder.DropTable(
                "PlayerFollow");
        }
    }
}