using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class DatabaseContext : DbContext
{
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

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
        base.OnModelCreating(builder);

        // Setup the audit log for serialization of Dictionary types.
        InitializeAuditLog(builder);

        // Generate monsters.
        var monsters = new List<Monster>();
        for (int i=0; i<10; i++)
        {
            monsters.Add(new Monster());
        }

        // Add the monsters to the database.
        builder.Entity<Monster>().HasData(monsters);
    }

    private void InitializeAuditLog(ModelBuilder builder)
    {
        // Configure the AuditLog entity.
        builder.Entity<AuditLog>(auditLog =>
        {
            auditLog.ToTable("AuditLogs");
            auditLog.HasKey(al => al.Id);
            auditLog.Property(al => al.NewValues).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, object?>>(v));
            auditLog.Property(al => al.OldValues).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, object?>>(v));
        });
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                var auditEntry = new AuditLog
                {
                    TableName = entry.Metadata.GetTableName(),
                    ActionType = entry.State.ToString(),
                    KeyValues = JsonConvert.SerializeObject(AuditManager.GetKeyValues(entry)),
                    OldValues = entry.State == EntityState.Added || entry.State == EntityState.Modified ? AuditManager.GetOldValues(entry) : new Dictionary<string, object?>(),
                    NewValues = entry.State == EntityState.Added || entry.State == EntityState.Modified ? AuditManager.GetNewValues(entry) : new Dictionary<string, object?>(),
                    ChangedColumns = entry.State == EntityState.Modified ? AuditManager.GetChangedColumns(entry) : new List<string>()
                };

                AuditLogs.Add(auditEntry);
            }
        }

        return base.SaveChanges();
    }
}