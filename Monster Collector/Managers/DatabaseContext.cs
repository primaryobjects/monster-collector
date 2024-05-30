using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class DatabaseContext : DbContext
{
    public DbSet<Monster> Monsters { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    public DatabaseContext()
    {
        // Check if the database table already exists.
        if (Database.EnsureCreated())
        {
            // Populate the database.
            InitializeMonsters();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Load environment variables.
        Env.Load();

        // Configure connection string.
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
    }

    private void InitializeMonsters()
    {
        Console.WriteLine("Generating monsters for database.");
        var cohereManager = new CohereManager();
        if (!cohereManager.IsValid())
        {
            Console.WriteLine("Missing CohereApiKey in .env file. Register an API key at https://dashboard.cohere.com/api-keys");
        }

        List<string> existingNames = new List<string>();

        // Generate monsters.
        var monsters = new List<Monster>();
        for (int i=0; i<10; i++)
        {
            var monster = new Monster();
            if (cohereManager.IsValid())
            {
                // Generate a name and description using an LLM service, providing the list of already used names to avoid duplicates.
                monster.GenerateNameDescription(existingNames);
            }
            monsters.Add(monster);

            // Prevent duplicate names.
            existingNames.Add(monster.Name);
        }

        // Add the monsters to the database.
        Monsters.AddRange(monsters);
        SaveChanges();
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
                v => JsonConvert.DeserializeObject<List<string?>>(v) ?? new List<string?>());
            auditLog.Property(al => al.OldValues).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string?>>(v) ?? new List<string?>());
        });
    }

    public override int SaveChanges()
    {
        var auditEntries = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
            {
                var auditEntry = new AuditLog
                {
                    TableName = entry.Metadata.GetTableName(),
                    ActionType = entry.State.ToString(),
                    KeyValues = JsonConvert.SerializeObject(AuditManager.GetKeyValues(entry)),
                    OldValues = entry.State == EntityState.Added || entry.State == EntityState.Modified ? AuditManager.GetOldValues(entry) : new List<string?>(),
                    NewValues = entry.State == EntityState.Added || entry.State == EntityState.Modified ? AuditManager.GetNewValues(entry) : new List<string?>(),
                    ChangedColumns = entry.State == EntityState.Modified ? AuditManager.GetChangedColumns(entry) : new List<string>()
                };

                auditEntries.Add(auditEntry);
            }
        }

        AuditLogs.AddRange(auditEntries);

        return base.SaveChanges();
    }
}