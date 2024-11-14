using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MaxDistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "preferences_max_distance",
                table: "users",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "preferences_max_distance",
                table: "users");
        }
    }
}
