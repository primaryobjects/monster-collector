namespace Monster_Collector.Managers;

public class DatabaseInitializer(LLM llm, MonsterFactory monsterFactory)
{
    private readonly LLM Llm = llm;
    private readonly MonsterFactory monsterFactory = monsterFactory;

    public void InitializeDatabase()
    {
        using var context = new DatabaseContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Generating monsters for database.");
            var cohereManager = Llm;
            if (!cohereManager.IsValid())
            {
                Console.WriteLine("Missing CohereApiKey in .env file. Register an API key at https://dashboard.cohere.com/api-keys");
            }

            List<string> existingNames = [];

            // Generate monsters.
            var monsters = new List<Monster>();
            for (int i=0; i<10; i++)
            {
                var monster = monsterFactory.Create(existingNames);
                monsters.Add(monster);

                // Prevent duplicate names.
                existingNames.Add(monster.Name);
            }

            // Add the monsters to the database.
            context.Monsters.AddRange(monsters);
            context.SaveChanges();
        }
    }
}