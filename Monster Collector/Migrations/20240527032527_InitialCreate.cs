using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Monster_Collector.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TableName = table.Column<string>(type: "TEXT", nullable: true),
                    ActionType = table.Column<string>(type: "TEXT", nullable: true),
                    KeyValues = table.Column<string>(type: "TEXT", nullable: false),
                    OldValues = table.Column<string>(type: "TEXT", nullable: false),
                    NewValues = table.Column<string>(type: "TEXT", nullable: false),
                    ChangedColumns = table.Column<string>(type: "TEXT", nullable: false),
                    DateChanged = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monsters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Health = table.Column<int>(type: "INTEGER", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monsters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Monsters",
                columns: new[] { "Id", "Attack", "Defense", "Description", "Health", "Name" },
                values: new object[,]
                {
                    { "06e5397e-c8f4-468e-8105-154448a0d5c6", 22, 10, "A big scary Goblin", 58, "Goblin" },
                    { "27e7d817-f023-4041-ad44-d090c8c7d6f6", 46, 7, "A big scary Ogre", 80, "Ogre" },
                    { "35e61d33-ef38-437c-8f98-7f14e913da96", 39, 14, "A big scary Dragon", 98, "Dragon" },
                    { "5057a8fc-1677-46fa-abe8-f14fd47fb845", 10, 15, "A big scary Dragon", 63, "Dragon" },
                    { "682fb900-b1e7-4ff1-8a58-ce689121051d", 45, 13, "A big scary Goblin", 52, "Goblin" },
                    { "86b49eb9-ad59-43bb-962b-c000d6890dad", 24, 20, "A big scary Troll", 61, "Troll" },
                    { "93e6cb95-f3c5-4395-b97a-6aad1bf4b209", 45, 16, "A big scary Demon", 91, "Demon" },
                    { "ada29946-39ad-4d2b-afb6-fc9206eaeded", 28, 9, "A big scary Ogre", 75, "Ogre" },
                    { "ed34e5e1-ddce-4e63-a300-015de6bab7f1", 34, 11, "A big scary Troll", 86, "Troll" },
                    { "f4cc6398-b37d-4fe8-85b1-47c56bbe60c1", 23, 22, "A big scary Ogre", 74, "Ogre" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Monsters");
        }
    }
}
