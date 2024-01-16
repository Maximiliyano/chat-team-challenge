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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MessageFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageFiles_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "CreatedAt", "IsPublic", "Topic", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1507), true, "General", null },
                    { 2, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1907), false, "Artist", null },
                    { 3, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1915), false, "Creator", null },
                    { 4, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1922), false, "Designer", null },
                    { 5, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1955), false, "Dancer", null },
                    { 6, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1959), false, "Videographer", null },
                    { 7, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1962), false, "Photographer", null },
                    { 8, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1967), false, "Painter", null },
                    { 9, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1969), false, "Musician", null },
                    { 10, new DateTime(2024, 1, 3, 9, 22, 1, 845, DateTimeKind.Utc).AddTicks(1972), false, "Writer", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "CreatedAt", "Description", "DiscordLink", "Email", "InstagramLink", "IsRemote", "Password", "Roles", "SpotifyLink", "TelegramLink", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, "New Waldofort", new DateTime(2024, 1, 3, 9, 22, 1, 960, DateTimeKind.Utc).AddTicks(4922), "solid state hacking alliance THX Clothing, Movies & Tools override Money Market Account Wisconsin Virgin Islands, U.S. Borders", "america.biz", "Larue_Gottlieb72@gmail.com", "america.biz", false, "$2a$11$lcNSBUt8ZZmiP/Jt2hkSUu2yJw5UPov9E4ZKHlbt5NEVh2UvuDyGS", 128, "america.biz", "america.biz", null, "Ettie.Schaefer" },
                    { 2, "Taureanton", new DateTime(2024, 1, 3, 9, 22, 2, 75, DateTimeKind.Utc).AddTicks(6051), "Ridge bandwidth Key Secured circuit Incredible Incredible Rubber Computer networks Greens Michigan", "marjory.info", "Leonard.Lang47@gmail.com", "marjory.info", true, "$2a$11$b.nepnCW8B6v/QjS/vBhZOJB9Qi6ym.DWQq5XKqAyVB7SlVRsfSV2", 0, "marjory.info", "marjory.info", null, "Alexanne_Lowe" },
                    { 3, "New Forest", new DateTime(2024, 1, 3, 9, 22, 2, 191, DateTimeKind.Utc).AddTicks(37), "Small Steel Shirt Movies & Clothing Fantastic Plastic Hat Accounts Rubber RSS attitude iterate 1080p Saint Kitts and Nevis", "katheryn.name", "Keenan.Batz@gmail.com", "katheryn.name", true, "$2a$11$CHRIxSMz6VjGskVAZE/rZ.ULDxoOqhv3d34dF/kLrZYO2MxtqH9GG", 128, "katheryn.name", "katheryn.name", null, "Jaylan.Hackett35" },
                    { 4, "South Havenshire", new DateTime(2024, 1, 3, 9, 22, 2, 307, DateTimeKind.Utc).AddTicks(5338), "Generic Beauty Berkshire matrix Vision-oriented overriding indexing Greenland Intelligent Mandatory", "lynn.net", "Hector.Quigley46@yahoo.com", "lynn.net", false, "$2a$11$OrQCVjJQ2cEC1WNued4Se.lZ8ksbt4qY9C419JPe1nDVLm0FJ3BBW", 128, "lynn.net", "lynn.net", null, "Odessa.Hodkiewicz76" },
                    { 5, "Christiansenfurt", new DateTime(2024, 1, 3, 9, 22, 2, 421, DateTimeKind.Utc).AddTicks(150), "Reunion Investment Account Platinum white withdrawal withdrawal Markets azure Handmade Concrete Chips extensible", "daphne.com", "Lorena53@hotmail.com", "daphne.com", false, "$2a$11$FeLQq1nB9CNoZINu6wby5ep6xUvjzb64AfHEaAxmpweyRRIUcWu9C", 4, "daphne.com", "daphne.com", null, "Dulce.Macejkovic64" },
                    { 6, "West Joyce", new DateTime(2024, 1, 3, 9, 22, 2, 533, DateTimeKind.Utc).AddTicks(9999), "users 24/365 Handcrafted Steel Shirt International Investment Account Checking Account backing up Strategist Awesome override", "jonathon.name", "Esteban32@gmail.com", "jonathon.name", false, "$2a$11$Lw236jYGv9VDPg/4jBx/neisODuBJ/l9BWs2wt4.sX8GZIbYYBqCy", 1, "jonathon.name", "jonathon.name", null, "Keely41" },
                    { 7, "West Magnus", new DateTime(2024, 1, 3, 9, 22, 2, 647, DateTimeKind.Utc).AddTicks(620), "invoice protocol invoice bi-directional connect Plains transform Mill Tasty integrated", "arvel.biz", "Jillian.Reynolds@yahoo.com", "arvel.biz", false, "$2a$11$zYqBdic7z5FTZ3OHoT7OGurCB8V23RM9.iKDR0WYR9KZajxbdxkiy", 1, "arvel.biz", "arvel.biz", null, "Vern_Dooley" },
                    { 8, "South Bill", new DateTime(2024, 1, 3, 9, 22, 2, 759, DateTimeKind.Utc).AddTicks(3705), "Cordoba Oro Valleys Roads transition hacking quantifying Analyst Avon SAS Garden, Grocery & Electronics", "baby.com", "Estell_Berge@yahoo.com", "baby.com", false, "$2a$11$ojovfo6I4rsjH619uzYKvOyw7looonH3.nTWumW2WpzyBPYM1IXNm", 2, "baby.com", "baby.com", null, "Wilhelm.Altenwerth92" },
                    { 9, "Lake Calebview", new DateTime(2024, 1, 3, 9, 22, 2, 872, DateTimeKind.Utc).AddTicks(2333), "task-force lime Generic Cameroon Tasty Soft Shirt parsing circuit Bedfordshire bus teal", "arnold.biz", "Nelda.Konopelski@gmail.com", "arnold.biz", true, "$2a$11$p10VjrH1./TBVhUWMR3ele6xLU3/8jwnVF98qFZKjVkQsPlQfqlo.", 256, "arnold.biz", "arnold.biz", null, "Muriel85" },
                    { 10, "Carolville", new DateTime(2024, 1, 3, 9, 22, 2, 986, DateTimeKind.Utc).AddTicks(160), "secondary ADP Wells Aruban Guilder Rest cohesive XML asymmetric Gorgeous Cotton Fish navigate", "vernice.info", "Elinore.Kling@gmail.com", "vernice.info", false, "$2a$11$4/6ARN5d1Zs/uQNJ.hFkeu1RrlhFBt7.CgCe2gi7A1NFvsGNAuvH2", 32, "vernice.info", "vernice.info", null, "Aletha52" }
                });

            migrationBuilder.InsertData(
                table: "ChatMembers",
                columns: new[] { "Id", "ChatId", "CreatedAt", "Role", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8343), 1, null, 8 },
                    { 2, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8384), 1, null, 8 },
                    { 3, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8391), 1, null, 7 },
                    { 4, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8397), 0, null, 10 },
                    { 5, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8403), 1, null, 6 },
                    { 6, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8408), 1, null, 9 },
                    { 7, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8412), 0, null, 9 },
                    { 8, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8418), 1, null, 1 },
                    { 9, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8422), 1, null, 6 },
                    { 10, 1, new DateTime(2024, 1, 3, 9, 22, 2, 988, DateTimeKind.Utc).AddTicks(8426), 1, null, 9 }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Body", "ChatId", "CreatedAt", "IsRead", "ReceiverId", "SenderId", "SenderUserName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Principal Checking Account Borders holistic Pula Intelligent bypass heuristic deposit visualize", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8495), false, 5, 2, "Muriel85", null },
                    { 2, "deposit Avon Frozen methodologies Arkansas Rustic Small Underpass Jewelery deposit", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8623), true, 7, 7, "Ettie.Schaefer", null },
                    { 3, "payment Shoes & Music Ohio Communications Lead US Dollar Buckinghamshire Regional Streamlined Fantastic Metal Chair", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8749), false, 10, 7, "Dulce.Macejkovic64", null },
                    { 4, "Optimized Bedfordshire Plastic calculate haptic haptic 3rd generation Islands Cambridgeshire Investment Account", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8788), true, 9, 7, "Alexanne_Lowe", null },
                    { 5, "Personal Loan Account International transmitting Direct Checking Account encoding Plastic generating users radical", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8829), true, 2, 9, "Vern_Dooley", null },
                    { 6, "SMTP navigating open-source Small Rubber Chips Producer Director application Licensed Soft Soap Developer experiences", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8893), true, 3, 1, "Keely41", null },
                    { 7, "Practical Fresh Sausages Personal Loan Account navigate Indiana AI Robust Auto Loan Account Gorgeous monitoring Directives", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(8964), true, 2, 3, "Ettie.Schaefer", null },
                    { 8, "olive Leone Fresh multi-byte Indiana Associate lavender Brooks eco-centric Practical Plastic Shoes", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(9033), false, 10, 8, "Wilhelm.Altenwerth92", null },
                    { 9, "help-desk circuit Practical Hills Lithuania South Carolina pink Washington strategize olive", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(9075), true, 8, 6, "Dulce.Macejkovic64", null },
                    { 10, "Data transmitting Ville efficient Avon methodical Bedfordshire lime Gold Awesome", 1, new DateTime(2024, 1, 3, 9, 22, 2, 990, DateTimeKind.Utc).AddTicks(9125), false, 4, 8, "Ettie.Schaefer", null }
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
                name: "IX_MessageFiles_MessageId",
                table: "MessageFiles",
                column: "MessageId");

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
                name: "MessageFiles");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
