using DotNetEnv;
using Monster_Collector.Managers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddSingleton<LLM, CohereManager>()
    .AddSingleton<MonsterFactory>();

var app = builder.Build();

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

new DatabaseInitializer(app.Services.GetRequiredService<LLM>(),
    app.Services.GetRequiredService<MonsterFactory>()).InitializeDatabase();

app.Run();
