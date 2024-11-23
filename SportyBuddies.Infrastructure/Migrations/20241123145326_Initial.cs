using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    occurred_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "jsonb", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "buddies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    opposite_buddy_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    matched_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_buddies", x => x.id);
                    table.ForeignKey(
                        name: "fk_buddies_buddies_opposite_buddy_id",
                        column: x => x.opposite_buddy_id,
                        principalTable: "buddies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_conversations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    opposite_match_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    matched_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    match_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    swipe = table.Column<int>(type: "integer", nullable: true),
                    swipe_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_matches", x => x.id);
                    table.ForeignKey(
                        name: "fk_matches_matches_opposite_match_id",
                        column: x => x.opposite_match_id,
                        principalTable: "matches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_conversations_conversation_id",
                        column: x => x.conversation_id,
                        principalTable: "conversations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    conversation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participants", x => x.id);
                    table.ForeignKey(
                        name: "fk_participants_conversations_conversation_id",
                        column: x => x.conversation_id,
                        principalTable: "conversations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: true),
                    main_photo_id = table.Column<Guid>(type: "uuid", nullable: true),
                    preferences_min_age = table.Column<int>(type: "integer", nullable: true),
                    preferences_max_age = table.Column<int>(type: "integer", nullable: true),
                    preferences_max_distance = table.Column<int>(type: "integer", nullable: true),
                    preferences_gender = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_user_photos_main_photo_id",
                        column: x => x.main_photo_id,
                        principalTable: "user_photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "user_sports",
                columns: table => new
                {
                    sport_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_sports", x => new { x.sport_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_sports_sports_sport_id",
                        column: x => x.sport_id,
                        principalTable: "sports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_sports_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_buddies_conversation_id",
                table: "buddies",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_buddies_matched_user_id",
                table: "buddies",
                column: "matched_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_buddies_opposite_buddy_id",
                table: "buddies",
                column: "opposite_buddy_id");

            migrationBuilder.CreateIndex(
                name: "ix_buddies_user_id",
                table: "buddies",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_conversations_creator_id",
                table: "conversations",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_matched_user_id",
                table: "matches",
                column: "matched_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_opposite_match_id",
                table: "matches",
                column: "opposite_match_id");

            migrationBuilder.CreateIndex(
                name: "ix_matches_user_id",
                table: "matches",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_conversation_id",
                table: "messages",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_sender_id",
                table: "messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_participants_conversation_id",
                table: "participants",
                column: "conversation_id");

            migrationBuilder.CreateIndex(
                name: "ix_participants_user_id",
                table: "participants",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_photos_user_id",
                table: "user_photos",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_sports_user_id",
                table: "user_sports",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_main_photo_id",
                table: "users",
                column: "main_photo_id");

            migrationBuilder.AddForeignKey(
                name: "fk_buddies_conversations_conversation_id",
                table: "buddies",
                column: "conversation_id",
                principalTable: "conversations",
                principalColumn: "id");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_photos_users_user_id",
                table: "user_photos");

            migrationBuilder.DropTable(
                name: "buddies");

            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "participants");

            migrationBuilder.DropTable(
                name: "user_sports");

            migrationBuilder.DropTable(
                name: "conversations");

            migrationBuilder.DropTable(
                name: "sports");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_photos");
        }
    }
}
