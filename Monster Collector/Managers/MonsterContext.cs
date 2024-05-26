using Microsoft.EntityFrameworkCore;

public class MonsterContext : DbContext
{
    public DbSet<Monster> Monsters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=MonsterManager.sqlite");
    }

    /// <summary>
    /// To re-generate the initial seeded database and execute this method:
    /// 1. Delete the folder Migrations.
    /// 2. dotnet ef migrations add InitialCreate
    /// 3. dotnet ef database update
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var monsters = new List<Monster>();
        for (int i=0; i<10; i++)
        {
            monsters.Add(new Monster());
        }

        // Add the monsters to the database.
        builder.Entity<Monster>().HasData(monsters);
    }
}