using Microsoft.EntityFrameworkCore;

public class MonsterContext : DbContext
{
    public DbSet<Monster> Monsters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=MonsterManager.sqlite");
    }
}