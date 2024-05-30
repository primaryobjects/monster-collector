using DotNetEnv;
using Monster_Collector.Managers;
using Monster_Collector.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

void InitializeDatabase()
{
    using var context = new DatabaseContext();
    if (context.Database.EnsureCreated())
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
            var monster = new MonsterFactory().Create(existingNames);
            monsters.Add(monster);

            // Prevent duplicate names.
            existingNames.Add(monster.Name);
        }

        // Add the monsters to the database.
        context.Monsters.AddRange(monsters);
        context.SaveChanges();
    }
}

// Load environment variables.
Env.Load();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

InitializeDatabase();

app.Run();
