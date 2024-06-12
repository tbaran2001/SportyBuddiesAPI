﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportyBuddiesAPI.DbContexts;

#nullable disable

namespace SportyBuddiesAPI.Migrations
{
    [DbContext(typeof(SportyBuddiesContext))]
    [Migration("20240611163856_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SportyBuddiesAPI.Entities.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sport1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sport2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sport3"
                        });
                });

            modelBuilder.Entity("SportyBuddiesAPI.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "User3"
                        });
                });

            modelBuilder.Entity("SportyBuddiesAPI.Entities.UserSport", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SportId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("UserSports");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            SportId = 1
                        },
                        new
                        {
                            UserId = 1,
                            SportId = 2
                        },
                        new
                        {
                            UserId = 2,
                            SportId = 2
                        },
                        new
                        {
                            UserId = 2,
                            SportId = 3
                        },
                        new
                        {
                            UserId = 3,
                            SportId = 1
                        },
                        new
                        {
                            UserId = 3,
                            SportId = 3
                        });
                });

            modelBuilder.Entity("SportyBuddiesAPI.Entities.UserSport", b =>
                {
                    b.HasOne("SportyBuddiesAPI.Entities.Sport", "Sport")
                        .WithMany("UserSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportyBuddiesAPI.Entities.User", "User")
                        .WithMany("UserSports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportyBuddiesAPI.Entities.Sport", b =>
                {
                    b.Navigation("UserSports");
                });

            modelBuilder.Entity("SportyBuddiesAPI.Entities.User", b =>
                {
                    b.Navigation("UserSports");
                });
#pragma warning restore 612, 618
        }
    }
}
