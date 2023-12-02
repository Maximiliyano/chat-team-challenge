using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChatTeamChallenge.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Roles = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRemote = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscordLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelegramLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpotifyLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    SenderUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMembers_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "CreatedAt", "IsPublic", "Topic", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1484), false, "invoice Tunnel", null },
                    { 2, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1588), true, "indexing bypassing Incredible Rubber Bacon", null },
                    { 3, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1660), true, "Rustic Frozen Chips Enterprise-wide Yuan Renminbi", null },
                    { 4, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1687), true, "Chad upward-trending", null },
                    { 5, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1710), true, "Unbranded Soft Car Movies", null },
                    { 6, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1718), true, "Grocery", null },
                    { 7, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1744), true, "calculating", null },
                    { 8, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1787), false, "Squares multi-byte", null },
                    { 9, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1826), false, "Assistant Bedfordshire cyan", null },
                    { 10, new DateTime(2023, 11, 26, 21, 32, 50, 564, DateTimeKind.Utc).AddTicks(1879), true, "connect Ports transition", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "CreatedAt", "Description", "DiscordLink", "Email", "InstagramLink", "IsRemote", "Password", "Roles", "SpotifyLink", "TelegramLink", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, "Collinsville", new DateTime(2023, 11, 26, 21, 32, 50, 678, DateTimeKind.Utc).AddTicks(352), "functionalities real-time Street Toys & Shoes generate User-centric Connecticut Licensed Concrete Bike West Virginia Producer", "nicolette.net", "Elisha.Mertz@yahoo.com", "nicolette.net", true, "$2a$11$ZwqDnxrFXRKJABaXmtU4d.dmKB0F1cjZvzTZ/DTFdz79BySdZrrNy", 1, "nicolette.net", "nicolette.net", null, "Hiram.Grant90" },
                    { 2, "D'angeloview", new DateTime(2023, 11, 26, 21, 32, 50, 791, DateTimeKind.Utc).AddTicks(4904), "Automotive & Movies leading-edge schemas index Vermont virtual Park content-based Gorgeous Concrete Bike e-business", "tyshawn.biz", "Jamar_Ward@yahoo.com", "tyshawn.biz", true, "$2a$11$NUjwOIllNkbiZgKj67A5/uPNxxYrjBNgzihi240fj6PXqkY2c7NjG", 1, "tyshawn.biz", "tyshawn.biz", null, "Jermaine89" },
                    { 3, "North Ezequiel", new DateTime(2023, 11, 26, 21, 32, 50, 903, DateTimeKind.Utc).AddTicks(9535), "Bedfordshire Antigua and Barbuda deposit Directives Plastic Knolls Internal Incredible Plastic Soap Pines SDD", "berenice.org", "Robert.Wehner21@gmail.com", "berenice.org", false, "$2a$11$/2thddPDfjYj437/nmTcwuisoN6P.VT/YShilSUKj6ZtqRCNZaauG", 512, "berenice.org", "berenice.org", null, "Dedrick93" },
                    { 4, "Lake Lizeth", new DateTime(2023, 11, 26, 21, 32, 51, 23, DateTimeKind.Utc).AddTicks(5466), "Handmade Plastic Pants Granite Implementation payment navigate Bedfordshire Berkshire capacitor sticky Shoal", "lisette.biz", "Kathlyn68@yahoo.com", "lisette.biz", true, "$2a$11$ScSXhc7NGYN5cN1JfvvrcO3r1bCiLHOXAcDEHaLtlmoaRxShZnzmW", 128, "lisette.biz", "lisette.biz", null, "Amelie.Lynch" },
                    { 5, "New Scarlett", new DateTime(2023, 11, 26, 21, 32, 51, 140, DateTimeKind.Utc).AddTicks(3369), "schemas United Kingdom Cook Islands multi-byte Village Garden Crescent application Licensed Concrete Computer parsing", "hailey.com", "Zoey.Casper@hotmail.com", "hailey.com", true, "$2a$11$lG750/eYkVUbUXSpFGBeF.pey.kx2rZwriRojWyzeTkS8QO51jIgK", 16, "hailey.com", "hailey.com", null, "Ike75" },
                    { 6, "West Kenyon", new DateTime(2023, 11, 26, 21, 32, 51, 256, DateTimeKind.Utc).AddTicks(8340), "interface Fords deploy withdrawal contextually-based Implementation firmware indigo dedicated Morocco", "jedidiah.org", "Anderson_Balistreri@hotmail.com", "jedidiah.org", true, "$2a$11$iUTIZRJcGGS0WHLlGsZ8tem8VJn7ieY32/hSjJY5THFtGMW8UFx7y", 8, "jedidiah.org", "jedidiah.org", null, "Kole99" },
                    { 7, "MacGyverburgh", new DateTime(2023, 11, 26, 21, 32, 51, 381, DateTimeKind.Utc).AddTicks(9653), "Data Tasty Granite Chair UAE Dirham cyan capacitor Concrete teal SQL plum Program", "kamron.net", "Leonel.Heathcote@gmail.com", "kamron.net", true, "$2a$11$sTRfHqE7Ru0JvwXac8gzveuCrROLUgLVETyZ7jigCs4k.i89GoqS.", 8, "kamron.net", "kamron.net", null, "Bessie.Little" },
                    { 8, "New Mariellemouth", new DateTime(2023, 11, 26, 21, 32, 51, 509, DateTimeKind.Utc).AddTicks(7687), "repurpose Awesome South Dakota payment Product gold Communications Tanzania Credit Card Account integrated", "adelle.com", "Blanche_Abernathy43@gmail.com", "adelle.com", false, "$2a$11$sWm9YihJxlqMqMQIbR7UrOK.aN2i.DfLy0rrH5.xPaztpt/fQme8.", 128, "adelle.com", "adelle.com", null, "Trey_Welch" },
                    { 9, "Efrenbury", new DateTime(2023, 11, 26, 21, 32, 51, 648, DateTimeKind.Utc).AddTicks(6174), "Handmade Sleek Frozen Bacon Taka Berkshire compress bandwidth Unbranded Borders reboot benchmark", "lawrence.com", "Billie.Gerlach20@yahoo.com", "lawrence.com", false, "$2a$11$b/mv.cWQujrL48QxfKkCnugdzP1LhO66oniPXSu.mTrCvkZlt9Cwe", 512, "lawrence.com", "lawrence.com", null, "Mckenna60" },
                    { 10, "Lourdesfort", new DateTime(2023, 11, 26, 21, 32, 51, 766, DateTimeKind.Utc).AddTicks(2312), "deposit reboot Health envisioneer Investor Customer Executive zero defect Books B2C", "patrick.name", "Oma.Schneider72@hotmail.com", "patrick.name", false, "$2a$11$E.Xfs1GdIB6rIQm5z2Eh4eElqcYKFBQDOoX6XHcxXzKxoapN2ArEO", 4, "patrick.name", "patrick.name", null, "Harrison.Kozey20" }
                });

            migrationBuilder.InsertData(
                table: "ChatMembers",
                columns: new[] { "Id", "ChatId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3278), null, 4 },
                    { 2, 7, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3323), null, 9 },
                    { 3, 4, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3329), null, 10 },
                    { 4, 6, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3363), null, 2 },
                    { 5, 8, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3366), null, 4 },
                    { 6, 6, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3370), null, 5 },
                    { 7, 6, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3373), null, 2 },
                    { 8, 6, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3376), null, 2 },
                    { 9, 4, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3379), null, 4 },
                    { 10, 5, new DateTime(2023, 11, 26, 21, 32, 51, 769, DateTimeKind.Utc).AddTicks(3383), null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "ChatId", "CreatedAt", "IsRead", "ReceiverId", "SenderId", "SenderUserName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "hard drive revolutionize circuit Beauty & Computers Cove Representative distributed modular Belarus Saint Pierre and Miquelon", 2, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4495), false, 9, 1, "Amelie.Lynch", null },
                    { 2, "Inlet Baby intangible Movies Avon Mobility programming Awesome Cotton Chair invoice Borders", 1, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4619), false, 3, 8, "Ike75", null },
                    { 3, "Books generate Village Shore scale Intuitive matrix withdrawal blue Somalia", 9, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4690), false, 5, 2, "Jermaine89", null },
                    { 4, "Movies Associate USB demand-driven Checking Account Station program Unbranded Fresh Shoes real-time Money Market Account", 5, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4746), false, 2, 8, "Ike75", null },
                    { 5, "deposit withdrawal Refined Cotton Car Montana Triple-buffered Savings Account solid state extend Bedfordshire Proactive", 7, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4797), true, 10, 1, "Hiram.Grant90", null },
                    { 6, "South Dakota deploy Lead Practical Soft Keyboard internet solution driver Rustic impactful lavender schemas", 10, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4878), false, 9, 6, "Dedrick93", null },
                    { 7, "digital Mountains Wooden portals 24/365 Engineer Unbranded Rubber Sausages Public-key Rustic Plastic", 4, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4920), false, 7, 8, "Jermaine89", null },
                    { 8, "collaboration Configuration Wooden Intelligent Rubber Tuna back-end Product invoice AGP monitor Brand", 2, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(4956), true, 5, 3, "Dedrick93", null },
                    { 9, "Incredible Granite Chips impactful Manat Optimized back-end yellow JBOD dynamic array Berkshire", 2, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(5034), true, 5, 3, "Bessie.Little", null },
                    { 10, "Small Granite Car cross-platform RSS Nevada withdrawal Indiana Shores New Zealand Dollar markets PNG", 3, new DateTime(2023, 11, 26, 21, 32, 51, 771, DateTimeKind.Utc).AddTicks(5113), true, 2, 5, "Mckenna60", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMembers_ChatId",
                table: "ChatMembers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMembers_UserId",
                table: "ChatMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMembers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
