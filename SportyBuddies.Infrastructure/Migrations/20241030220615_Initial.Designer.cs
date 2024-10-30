﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportyBuddies.Infrastructure;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    [DbContext(typeof(SportyBuddiesDbContext))]
    [Migration("20241030220615_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SportyBuddies.Domain.Buddies.Buddy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("match_date_time");

                    b.Property<Guid>("MatchedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("matched_user_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_buddies");

                    b.HasIndex("MatchedUserId")
                        .HasDatabaseName("ix_buddies_matched_user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_buddies_user_id");

                    b.ToTable("buddies", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("match_date_time");

                    b.Property<Guid>("MatchedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("matched_user_id");

                    b.Property<int?>("Swipe")
                        .HasColumnType("integer")
                        .HasColumnName("swipe");

                    b.Property<DateTime?>("SwipeDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("swipe_date_time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_matches");

                    b.HasIndex("MatchedUserId")
                        .HasDatabaseName("ix_matches_matched_user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_matches_user_id");

                    b.ToTable("matches", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Messages.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipient_id");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid")
                        .HasColumnName("sender_id");

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time_sent");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Sports.Sport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_sports");

                    b.ToTable("sports", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on_utc");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<Guid?>("MainPhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("main_photo_id");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("MainPhotoId")
                        .HasDatabaseName("ix_users_main_photo_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.UserPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_photos");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_photos_user_id");

                    b.ToTable("user_photos", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasColumnType("text")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("occurred_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_outbox_messages");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("UserSports", b =>
                {
                    b.Property<Guid>("SportId")
                        .HasColumnType("uuid")
                        .HasColumnName("sport_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("SportId", "UserId")
                        .HasName("pk_user_sports");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_sports_user_id");

                    b.ToTable("user_sports", (string)null);
                });

            modelBuilder.Entity("SportyBuddies.Domain.Buddies.Buddy", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "MatchedUser")
                        .WithMany()
                        .HasForeignKey("MatchedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_buddies_users_matched_user_id");

                    b.HasOne("SportyBuddies.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_buddies_users_user_id");

                    b.Navigation("MatchedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Matches.Match", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "MatchedUser")
                        .WithMany()
                        .HasForeignKey("MatchedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_matches_users_matched_user_id");

                    b.HasOne("SportyBuddies.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_matches_users_user_id");

                    b.Navigation("MatchedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.UserPhoto", "MainPhoto")
                        .WithMany()
                        .HasForeignKey("MainPhotoId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_users_user_photos_main_photo_id");

                    b.OwnsOne("SportyBuddies.Domain.Users.Preferences", "Preferences", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<int>("Gender")
                                .HasColumnType("integer")
                                .HasColumnName("preferences_gender");

                            b1.Property<int>("MaxAge")
                                .HasColumnType("integer")
                                .HasColumnName("preferences_max_age");

                            b1.Property<int>("MinAge")
                                .HasColumnType("integer")
                                .HasColumnName("preferences_min_age");

                            b1.HasKey("UserId");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserId")
                                .HasConstraintName("fk_users_users_id");
                        });

                    b.Navigation("MainPhoto");

                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.UserPhoto", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_photos_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserSports", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Sports.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_sports_sports_sport_id");

                    b.HasOne("SportyBuddies.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_sports_users_user_id");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
