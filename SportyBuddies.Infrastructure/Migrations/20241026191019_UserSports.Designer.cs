﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportyBuddies.Infrastructure.Common.Persistence;

#nullable disable

namespace SportyBuddies.Infrastructure.Migrations
{
    [DbContext(typeof(SportyBuddiesDbContext))]
    [Migration("20241026191019_UserSports")]
    partial class UserSports
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SportyBuddies.Domain.Buddies.Buddy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MatchedUserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchedUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Buddies");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("MatchDateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MatchedUserId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Swipe")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("SwipeDateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MatchedUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Messages.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Sports.Sport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("MainPhotoId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MainPhotoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.UserPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPhotos");
                });

            modelBuilder.Entity("UserSports", b =>
                {
                    b.Property<Guid>("SportId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("SportId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSports");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Buddies.Buddy", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "MatchedUser")
                        .WithMany()
                        .HasForeignKey("MatchedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportyBuddies.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MatchedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Matches.Match", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "MatchedUser")
                        .WithMany()
                        .HasForeignKey("MatchedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportyBuddies.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MatchedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Messages.Message", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportyBuddies.Domain.Users.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Users.UserPhoto", "MainPhoto")
                        .WithMany()
                        .HasForeignKey("MainPhotoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsOne("SportyBuddies.Domain.Users.Preferences", "Preferences", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Gender")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("MaxAge")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("MinAge")
                                .HasColumnType("INTEGER");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
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
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserSports", b =>
                {
                    b.HasOne("SportyBuddies.Domain.Sports.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportyBuddies.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportyBuddies.Domain.Users.User", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
