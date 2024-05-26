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
                    { "3249c1ae-2b14-4374-8c09-cefdb403fe89", 44, 13, "A big scary Troll", 43, "Troll" },
                    { "3e3cd1f4-6d47-4fee-a6f5-7db18a02d8d5", 26, 18, "A big scary Troll", 68, "Troll" },
                    { "5d5ee1ac-1f91-4261-9e18-bf1d12c1ec22", 33, 6, "A big scary Ogre", 44, "Ogre" },
                    { "62f6e0d5-5c6e-4eee-934d-0b7e33e84dda", 43, 15, "A big scary Dragon", 92, "Dragon" },
                    { "6d1ba26e-e820-4c40-9225-22e947c3ca9a", 45, 7, "A big scary Ogre", 90, "Ogre" },
                    { "6f4442d9-36c8-4c78-ae21-a0c5c54e3f6a", 27, 10, "A big scary Demon", 66, "Demon" },
                    { "8f3c3796-6a1c-48db-8aeb-05aa1340aba4", 17, 15, "A big scary Ogre", 64, "Ogre" },
                    { "b988d987-0f42-47b2-8962-7183c2ebb4e4", 26, 12, "A big scary Demon", 79, "Demon" },
                    { "ddd65601-f0bd-411c-8a3a-8873a2a3a4e8", 44, 12, "A big scary Dragon", 79, "Dragon" },
                    { "f32f7866-07d0-41c5-be7d-b25d9079408f", 35, 18, "A big scary Goblin", 42, "Goblin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Monsters");
        }
    }
}
