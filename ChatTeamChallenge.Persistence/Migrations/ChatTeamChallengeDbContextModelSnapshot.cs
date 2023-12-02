﻿// <auto-generated />
using System;
using ChatTeamChallenge.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatTeamChallenge.Persistence.Migrations
{
    [DbContext(typeof(ChatTeamChallengeDbContext))]
    partial class ChatTeamChallengeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Chats", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1484),
                            IsPublic = false,
                            Topic = "invoice Tunnel"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1588),
                            IsPublic = true,
                            Topic = "indexing bypassing Incredible Rubber Bacon"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1660),
                            IsPublic = true,
                            Topic = "Rustic Frozen Chips Enterprise-wide Yuan Renminbi"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1687),
                            IsPublic = true,
                            Topic = "Chad upward-trending"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1710),
                            IsPublic = true,
                            Topic = "Unbranded Soft Car Movies"
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1718),
                            IsPublic = true,
                            Topic = "Grocery"
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1744),
                            IsPublic = true,
                            Topic = "calculating"
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1787),
                            IsPublic = false,
                            Topic = "Squares multi-byte"
                        },
                        new
                        {
                            Id = 9,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1826),
                            IsPublic = false,
                            Topic = "Assistant Bedfordshire cyan"
                        },
                        new
                        {
                            Id = 10,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1879),
                            IsPublic = true,
                            Topic = "connect Ports transition"
                        });
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.ChatMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMembers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatId = 8,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3278),
                            UserId = 4
                        },
                        new
                        {
                            Id = 2,
                            ChatId = 7,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3323),
                            UserId = 9
                        },
                        new
                        {
                            Id = 3,
                            ChatId = 4,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3329),
                            UserId = 10
                        },
                        new
                        {
                            Id = 4,
                            ChatId = 6,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3363),
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            ChatId = 8,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3366),
                            UserId = 4
                        },
                        new
                        {
                            Id = 6,
                            ChatId = 6,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3370),
                            UserId = 5
                        },
                        new
                        {
                            Id = 7,
                            ChatId = 6,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3373),
                            UserId = 2
                        },
                        new
                        {
                            Id = 8,
                            ChatId = 6,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3376),
                            UserId = 2
                        },
                        new
                        {
                            Id = 9,
                            ChatId = 4,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3379),
                            UserId = 4
                        },
                        new
                        {
                            Id = 10,
                            ChatId = 5,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3383),
                            UserId = 4
                        });
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int?>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("SenderUserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "hard drive revolutionize circuit Beauty & Computers Cove Representative distributed modular Belarus Saint Pierre and Miquelon",
                            ChatId = 2,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4495),
                            IsRead = false,
                            ReceiverId = 9,
                            SenderId = 1,
                            SenderUserName = "Amelie.Lynch"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Inlet Baby intangible Movies Avon Mobility programming Awesome Cotton Chair invoice Borders",
                            ChatId = 1,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4619),
                            IsRead = false,
                            ReceiverId = 3,
                            SenderId = 8,
                            SenderUserName = "Ike75"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Books generate Village Shore scale Intuitive matrix withdrawal blue Somalia",
                            ChatId = 9,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4690),
                            IsRead = false,
                            ReceiverId = 5,
                            SenderId = 2,
                            SenderUserName = "Jermaine89"
                        },
                        new
                        {
                            Id = 4,
                            Body = "Movies Associate USB demand-driven Checking Account Station program Unbranded Fresh Shoes real-time Money Market Account",
                            ChatId = 5,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4746),
                            IsRead = false,
                            ReceiverId = 2,
                            SenderId = 8,
                            SenderUserName = "Ike75"
                        },
                        new
                        {
                            Id = 5,
                            Body = "deposit withdrawal Refined Cotton Car Montana Triple-buffered Savings Account solid state extend Bedfordshire Proactive",
                            ChatId = 7,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4797),
                            IsRead = true,
                            ReceiverId = 10,
                            SenderId = 1,
                            SenderUserName = "Hiram.Grant90"
                        },
                        new
                        {
                            Id = 6,
                            Body = "South Dakota deploy Lead Practical Soft Keyboard internet solution driver Rustic impactful lavender schemas",
                            ChatId = 10,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4878),
                            IsRead = false,
                            ReceiverId = 9,
                            SenderId = 6,
                            SenderUserName = "Dedrick93"
                        },
                        new
                        {
                            Id = 7,
                            Body = "digital Mountains Wooden portals 24/365 Engineer Unbranded Rubber Sausages Public-key Rustic Plastic",
                            ChatId = 4,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4920),
                            IsRead = false,
                            ReceiverId = 7,
                            SenderId = 8,
                            SenderUserName = "Jermaine89"
                        },
                        new
                        {
                            Id = 8,
                            Body = "collaboration Configuration Wooden Intelligent Rubber Tuna back-end Product invoice AGP monitor Brand",
                            ChatId = 2,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4956),
                            IsRead = true,
                            ReceiverId = 5,
                            SenderId = 3,
                            SenderUserName = "Dedrick93"
                        },
                        new
                        {
                            Id = 9,
                            Body = "Incredible Granite Chips impactful Manat Optimized back-end yellow JBOD dynamic array Berkshire",
                            ChatId = 2,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(5034),
                            IsRead = true,
                            ReceiverId = 5,
                            SenderId = 3,
                            SenderUserName = "Bessie.Little"
                        },
                        new
                        {
                            Id = 10,
                            Body = "Small Granite Car cross-platform RSS Nevada withdrawal Indiana Shores New Zealand Dollar markets PNG",
                            ChatId = 3,
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(5113),
                            IsRead = true,
                            ReceiverId = 2,
                            SenderId = 5,
                            SenderUserName = "Mckenna60"
                        });
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens", (string)null);
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstagramLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRemote")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("SpotifyLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Collinsville",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 678, DateTimeKind.Utc).AddTicks(352),
                            Description = "functionalities real-time Street Toys & Shoes generate User-centric Connecticut Licensed Concrete Bike West Virginia Producer",
                            DiscordLink = "nicolette.net",
                            Email = "Elisha.Mertz@yahoo.com",
                            InstagramLink = "nicolette.net",
                            IsRemote = true,
                            Password = "$2a$11$ZwqDnxrFXRKJABaXmtU4d.dmKB0F1cjZvzTZ/DTFdz79BySdZrrNy",
                            Roles = 1,
                            SpotifyLink = "nicolette.net",
                            TelegramLink = "nicolette.net",
                            Username = "Hiram.Grant90"
                        },
                        new
                        {
                            Id = 2,
                            City = "D'angeloview",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 791, DateTimeKind.Utc).AddTicks(4904),
                            Description = "Automotive & Movies leading-edge schemas index Vermont virtual Park content-based Gorgeous Concrete Bike e-business",
                            DiscordLink = "tyshawn.biz",
                            Email = "Jamar_Ward@yahoo.com",
                            InstagramLink = "tyshawn.biz",
                            IsRemote = true,
                            Password = "$2a$11$NUjwOIllNkbiZgKj67A5/uPNxxYrjBNgzihi240fj6PXqkY2c7NjG",
                            Roles = 1,
                            SpotifyLink = "tyshawn.biz",
                            TelegramLink = "tyshawn.biz",
                            Username = "Jermaine89"
                        },
                        new
                        {
                            Id = 3,
                            City = "North Ezequiel",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 50, 903, DateTimeKind.Utc).AddTicks(9535),
                            Description = "Bedfordshire Antigua and Barbuda deposit Directives Plastic Knolls Internal Incredible Plastic Soap Pines SDD",
                            DiscordLink = "berenice.org",
                            Email = "Robert.Wehner21@gmail.com",
                            InstagramLink = "berenice.org",
                            IsRemote = false,
                            Password = "$2a$11$/2thddPDfjYj437/nmTcwuisoN6P.VT/YShilSUKj6ZtqRCNZaauG",
                            Roles = 512,
                            SpotifyLink = "berenice.org",
                            TelegramLink = "berenice.org",
                            Username = "Dedrick93"
                        },
                        new
                        {
                            Id = 4,
                            City = "Lake Lizeth",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 23, DateTimeKind.Utc).AddTicks(5466),
                            Description = "Handmade Plastic Pants Granite Implementation payment navigate Bedfordshire Berkshire capacitor sticky Shoal",
                            DiscordLink = "lisette.biz",
                            Email = "Kathlyn68@yahoo.com",
                            InstagramLink = "lisette.biz",
                            IsRemote = true,
                            Password = "$2a$11$ScSXhc7NGYN5cN1JfvvrcO3r1bCiLHOXAcDEHaLtlmoaRxShZnzmW",
                            Roles = 128,
                            SpotifyLink = "lisette.biz",
                            TelegramLink = "lisette.biz",
                            Username = "Amelie.Lynch"
                        },
                        new
                        {
                            Id = 5,
                            City = "New Scarlett",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 140, DateTimeKind.Utc).AddTicks(3369),
                            Description = "schemas United Kingdom Cook Islands multi-byte Village Garden Crescent application Licensed Concrete Computer parsing",
                            DiscordLink = "hailey.com",
                            Email = "Zoey.Casper@hotmail.com",
                            InstagramLink = "hailey.com",
                            IsRemote = true,
                            Password = "$2a$11$lG750/eYkVUbUXSpFGBeF.pey.kx2rZwriRojWyzeTkS8QO51jIgK",
                            Roles = 16,
                            SpotifyLink = "hailey.com",
                            TelegramLink = "hailey.com",
                            Username = "Ike75"
                        },
                        new
                        {
                            Id = 6,
                            City = "West Kenyon",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 256, DateTimeKind.Utc).AddTicks(8340),
                            Description = "interface Fords deploy withdrawal contextually-based Implementation firmware indigo dedicated Morocco",
                            DiscordLink = "jedidiah.org",
                            Email = "Anderson_Balistreri@hotmail.com",
                            InstagramLink = "jedidiah.org",
                            IsRemote = true,
                            Password = "$2a$11$iUTIZRJcGGS0WHLlGsZ8tem8VJn7ieY32/hSjJY5THFtGMW8UFx7y",
                            Roles = 8,
                            SpotifyLink = "jedidiah.org",
                            TelegramLink = "jedidiah.org",
                            Username = "Kole99"
                        },
                        new
                        {
                            Id = 7,
                            City = "MacGyverburgh",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 381, DateTimeKind.Utc).AddTicks(9653),
                            Description = "Data Tasty Granite Chair UAE Dirham cyan capacitor Concrete teal SQL plum Program",
                            DiscordLink = "kamron.net",
                            Email = "Leonel.Heathcote@gmail.com",
                            InstagramLink = "kamron.net",
                            IsRemote = true,
                            Password = "$2a$11$sTRfHqE7Ru0JvwXac8gzveuCrROLUgLVETyZ7jigCs4k.i89GoqS.",
                            Roles = 8,
                            SpotifyLink = "kamron.net",
                            TelegramLink = "kamron.net",
                            Username = "Bessie.Little"
                        },
                        new
                        {
                            Id = 8,
                            City = "New Mariellemouth",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 509, DateTimeKind.Utc).AddTicks(7687),
                            Description = "repurpose Awesome South Dakota payment Product gold Communications Tanzania Credit Card Account integrated",
                            DiscordLink = "adelle.com",
                            Email = "Blanche_Abernathy43@gmail.com",
                            InstagramLink = "adelle.com",
                            IsRemote = false,
                            Password = "$2a$11$sWm9YihJxlqMqMQIbR7UrOK.aN2i.DfLy0rrH5.xPaztpt/fQme8.",
                            Roles = 128,
                            SpotifyLink = "adelle.com",
                            TelegramLink = "adelle.com",
                            Username = "Trey_Welch"
                        },
                        new
                        {
                            Id = 9,
                            City = "Efrenbury",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 648, DateTimeKind.Utc).AddTicks(6174),
                            Description = "Handmade Sleek Frozen Bacon Taka Berkshire compress bandwidth Unbranded Borders reboot benchmark",
                            DiscordLink = "lawrence.com",
                            Email = "Billie.Gerlach20@yahoo.com",
                            InstagramLink = "lawrence.com",
                            IsRemote = false,
                            Password = "$2a$11$b/mv.cWQujrL48QxfKkCnugdzP1LhO66oniPXSu.mTrCvkZlt9Cwe",
                            Roles = 512,
                            SpotifyLink = "lawrence.com",
                            TelegramLink = "lawrence.com",
                            Username = "Mckenna60"
                        },
                        new
                        {
                            Id = 10,
                            City = "Lourdesfort",
                            CreatedAt = new DateTime(2023, 11, 26, 21, 32, 51, 766, DateTimeKind.Utc).AddTicks(2312),
                            Description = "deposit reboot Health envisioneer Investor Customer Executive zero defect Books B2C",
                            DiscordLink = "patrick.name",
                            Email = "Oma.Schneider72@hotmail.com",
                            InstagramLink = "patrick.name",
                            IsRemote = false,
                            Password = "$2a$11$E.Xfs1GdIB6rIQm5z2Eh4eElqcYKFBQDOoX6XHcxXzKxoapN2ArEO",
                            Roles = 4,
                            SpotifyLink = "patrick.name",
                            TelegramLink = "patrick.name",
                            Username = "Harrison.Kozey20"
                        });
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.ChatMember", b =>
                {
                    b.HasOne("ChatTeamChallenge.Domain.Apartments.Chat", "Chat")
                        .WithMany("Members")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChatTeamChallenge.Domain.Apartments.User", "User")
                        .WithMany("Members")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.Message", b =>
                {
                    b.HasOne("ChatTeamChallenge.Domain.Apartments.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.RefreshToken", b =>
                {
                    b.HasOne("ChatTeamChallenge.Domain.Apartments.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.Chat", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ChatTeamChallenge.Domain.Apartments.User", b =>
                {
                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}