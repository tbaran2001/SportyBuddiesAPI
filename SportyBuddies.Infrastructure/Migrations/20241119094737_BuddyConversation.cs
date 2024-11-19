using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BuddyConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "conversation_id",
                table: "buddies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_buddies_conversation_id",
                table: "buddies",
                column: "conversation_id");

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_conversations_conversation_id",
                table: "buddies",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_buddies_conversations_conversation_id",
                table: "buddies");

            migrationBuilder.DropIndex(
                name: "ix_buddies_conversation_id",
                table: "buddies");

            migrationBuilder.DropColumn(
                name: "conversation_id",
                table: "buddies");
        }
    }
}
