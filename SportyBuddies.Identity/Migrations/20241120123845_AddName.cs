﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "AspNetUsers");
        }
    }
}
