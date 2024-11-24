using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_buddies_buddies_opposite_buddy_id",
                table: "buddies");

            migrationBuilder.DropForeignKey(
                name: "fk_matches_matches_opposite_match_id",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "ix_matches_opposite_match_id",
                table: "matches");

            migrationBuilder.DropIndex(
                name: "ix_buddies_opposite_buddy_id",
                table: "buddies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_matches_opposite_match_id",
                table: "matches",
                column: "opposite_match_id");

            migrationBuilder.CreateIndex(
                name: "ix_buddies_opposite_buddy_id",
                table: "buddies",
                column: "opposite_buddy_id");

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_buddies_opposite_buddy_id",
                table: "buddies",
                column: "opposite_buddy_id",
                principalTable: "buddies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_matches_matches_opposite_match_id",
                table: "matches",
                column: "opposite_match_id",
                principalTable: "matches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
