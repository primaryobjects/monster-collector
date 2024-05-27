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
                    { "0191aef6-f7e8-45e1-9212-564e6b4d2eb5", 45, 17, "A big scary Goblin", 89, "Goblin" },
                    { "5a6adbc0-3d93-48a1-8170-6c955bf10a53", 50, 24, "A big scary Troll", 90, "Troll" },
                    { "6bc823a2-793f-4445-ae45-9a839fe6089a", 32, 23, "A big scary Demon", 45, "Demon" },
                    { "70509de8-0ce9-43fb-92b9-5cd9027933ff", 25, 9, "A big scary Dragon", 80, "Dragon" },
                    { "7e26a7e3-d516-43f2-ad46-79360aec5fb0", 26, 7, "A big scary Dragon", 68, "Dragon" },
                    { "bf9f5481-2715-40cb-9300-d1d8cf8af261", 47, 20, "A big scary Goblin", 94, "Goblin" },
                    { "c14e59db-1478-49c7-80a1-9e5850771d8d", 43, 25, "A big scary Ogre", 91, "Ogre" },
                    { "cc73bc38-e059-4b73-bbad-d89a42dd8bba", 26, 21, "A big scary Dragon", 33, "Dragon" },
                    { "d0012337-b68e-42fa-bca7-7e41d07a815d", 49, 6, "A big scary Troll", 22, "Troll" },
                    { "e27a46b2-cb7a-49bb-bde2-74659bbc3f43", 15, 18, "A big scary Goblin", 55, "Goblin" }
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
