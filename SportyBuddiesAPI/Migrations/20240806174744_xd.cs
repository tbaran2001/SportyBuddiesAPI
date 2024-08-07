using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportyBuddiesAPI.Migrations
{
    /// <inheritdoc />
    public partial class xd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d28fff4-8f9d-4c41-837b-da79a5bdc250");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23282a16-cfdc-467d-b493-234736845657");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d28fff4-8f9d-4c41-837b-da79a5bdc250", null, "Admin", "ADMIN" },
                    { "23282a16-cfdc-467d-b493-234736845657", null, "User", "USER" }
                });
        }
    }
}
