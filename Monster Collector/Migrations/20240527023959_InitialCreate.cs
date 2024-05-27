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
                    { "06706ea4-5fa8-40cb-91b7-f316ad4011e5", 27, 19, "A big scary Ogre", 76, "Ogre" },
                    { "6bfa80d7-c5d6-427e-8dd6-4370bea8ee94", 34, 6, "A big scary Ogre", 69, "Ogre" },
                    { "8f2341ef-aa83-4a93-81fd-31962adda1f4", 35, 21, "A big scary Goblin", 80, "Goblin" },
                    { "9546bf54-f4c5-48fd-aaf9-df59edca3af1", 45, 16, "A big scary Troll", 82, "Troll" },
                    { "a390051d-b53e-4a12-9c5c-d257024f37ab", 40, 8, "A big scary Troll", 81, "Troll" },
                    { "b69d832e-b04a-49e1-a0d7-a6885eb798d2", 10, 12, "A big scary Troll", 59, "Troll" },
                    { "cc7c19d6-f08b-419e-80e8-1cfb05aa8d22", 21, 19, "A big scary Dragon", 59, "Dragon" },
                    { "df2bf1af-e150-443b-b312-af5a31524939", 32, 5, "A big scary Ogre", 77, "Ogre" },
                    { "e7e03c0d-270a-4c5a-9bfc-69a534efee85", 35, 6, "A big scary Demon", 51, "Demon" },
                    { "fb0faf97-9751-4ac2-ab74-85a51081d186", 37, 13, "A big scary Demon", 78, "Demon" }
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
