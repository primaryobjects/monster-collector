using System.Linq;
using System.Data.SQLite;

/// <summary>
/// 1. Open a Terminal in VSCode
/// 2. dotnet add package Microsoft.EntityFrameworkCore.Design
/// 3. dotnet add package Microsoft.EntityFrameworkCore.Sqlite
/// 4. dotnet tool install --global dotnet-ef
/// 5. dotnet ef migrations add InitialCreate
/// 6. dotnet ef database update
///
/// Note, if you want to re-generate the initial seeded database:
/// 1. Delete the folder Migrations.
/// 2. dotnet ef migrations add InitialCreate
/// 3. dotnet ef database update
/// </summary>
public static class MonsterManager
{
    public static List<Monster> Load()
    {
        List<Monster> monsters;

        using (var context = new MonsterContext())
        {
            monsters = context.Monsters.ToList();
        }

        return monsters;
    }

    public static Monster? Find(string id)
    {
        Monster? monster = null;

        using (var context = new MonsterContext())
        {
            monster = context.Monsters.Find(id);
            /*monster = (from m in context.Monsters
                       where m.Id == id
                       select m).FirstOrDefault();*/
        }

        return monster;
    }

    public static Monster? Update(Monster monster)
    {
        Monster? result = null;

        using (var context = new MonsterContext())
        {
            var existingMonster = context.Monsters.Find(monster.Id);
            if (existingMonster != null)
            {
                existingMonster.Name = monster.Name;
                existingMonster.Health = monster.Health;
                existingMonster.Attack = monster.Attack;
                existingMonster.Defense = monster.Defense;

                result = existingMonster;

                context.SaveChanges();
            }
        }

        return result;
    }
}