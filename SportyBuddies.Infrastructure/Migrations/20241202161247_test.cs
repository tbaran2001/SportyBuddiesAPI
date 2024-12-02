using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_buddies_domain_users_matched_user_id",
                table: "buddies");

            migrationBuilder.DropForeignKey(
                name: "fk_buddies_domain_users_user_id",
                table: "buddies");

            migrationBuilder.DropForeignKey(
                name: "fk_conversations_domain_users_creator_id",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "fk_domain_users_user_photos_main_photo_id",
                table: "domain_users");

            migrationBuilder.DropForeignKey(
                name: "fk_matches_domain_users_matched_user_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "fk_matches_domain_users_user_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_domain_users_sender_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_participants_domain_users_user_id",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "fk_user_photos_domain_users_user_id",
                table: "user_photos");

            migrationBuilder.DropForeignKey(
                name: "fk_user_sports_domain_users_user_id",
                table: "user_sports");

            migrationBuilder.DropPrimaryKey(
                name: "pk_domain_users",
                table: "domain_users");

            migrationBuilder.RenameTable(
                name: "domain_users",
                newName: "users");

            migrationBuilder.RenameIndex(
                name: "ix_domain_users_main_photo_id",
                table: "users",
                newName: "ix_users_main_photo_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_users_matched_user_id",
                table: "buddies",
                column: "matched_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_users_user_id",
                table: "buddies",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversations_users_creator_id",
                table: "conversations",
                column: "creator_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_matches_users_matched_user_id",
                table: "matches",
                column: "matched_user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_matches_users_user_id",
                table: "matches",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages",
                column: "sender_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_participants_users_user_id",
                table: "participants",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_photos_users_user_id",
                table: "user_photos",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_sports_users_user_id",
                table: "user_sports",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_photos_main_photo_id",
                table: "users",
                column: "main_photo_id",
                principalTable: "user_photos",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_buddies_users_matched_user_id",
                table: "buddies");

            migrationBuilder.DropForeignKey(
                name: "fk_buddies_users_user_id",
                table: "buddies");

            migrationBuilder.DropForeignKey(
                name: "fk_conversations_users_creator_id",
                table: "conversations");

            migrationBuilder.DropForeignKey(
                name: "fk_matches_users_matched_user_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "fk_matches_users_user_id",
                table: "matches");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_users_sender_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_participants_users_user_id",
                table: "participants");

            migrationBuilder.DropForeignKey(
                name: "fk_user_photos_users_user_id",
                table: "user_photos");

            migrationBuilder.DropForeignKey(
                name: "fk_user_sports_users_user_id",
                table: "user_sports");

            migrationBuilder.DropForeignKey(
                name: "fk_users_user_photos_main_photo_id",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "domain_users");

            migrationBuilder.RenameIndex(
                name: "ix_users_main_photo_id",
                table: "domain_users",
                newName: "ix_domain_users_main_photo_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_domain_users",
                table: "domain_users",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_domain_users_matched_user_id",
                table: "buddies",
                column: "matched_user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_domain_users_user_id",
                table: "buddies",
                column: "user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_conversations_domain_users_creator_id",
                table: "conversations",
                column: "creator_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_domain_users_user_photos_main_photo_id",
                table: "domain_users",
                column: "main_photo_id",
                principalTable: "user_photos",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_matches_domain_users_matched_user_id",
                table: "matches",
                column: "matched_user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_matches_domain_users_user_id",
                table: "matches",
                column: "user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messages_domain_users_sender_id",
                table: "messages",
                column: "sender_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_participants_domain_users_user_id",
                table: "participants",
                column: "user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_photos_domain_users_user_id",
                table: "user_photos",
                column: "user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_sports_domain_users_user_id",
                table: "user_sports",
                column: "user_id",
                principalTable: "domain_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
