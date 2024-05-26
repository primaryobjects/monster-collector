using System;
using System.Data.SQLite;

public static class MonsterManager
{
    private static string _connectionString = "data source=MonsterManager.sqlite";

    public static bool Exists()
    {
        bool result = false;

        using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
        {
            conn.Open();

            string sql = "SELECT name FROM sqlite_master WHERE type='table' AND name='Monsters';";
            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
            {
                result = command.ExecuteScalar() != null;
            }

            conn.Close();
        }

        return result;
    }

    public static IEnumerable<Monster> CreateDatabase()
    {
        List<Monster> monsters = new List<Monster>();

        if (!Exists())
        {
            using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();

                string sql = "CREATE TABLE Monsters (Id VARCHAR(32) PRIMARY KEY, Name VARCHAR(20), Health INT, Attack INT, Defense INT)";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    command.ExecuteNonQuery();
                }

                sql = "INSERT INTO Monsters (Id, Name, Health, Attack, Defense) VALUES (@Id, @Name, @Health, @Attack, @Defense)";
                using (SQLiteCommand command = new SQLiteCommand(sql, conn))
                {
                    for (int i=0; i<10; i++)
                    {
                        Monster monster = new Monster();

                        command.Parameters.AddWithValue("@Id", monster.Id);
                        command.Parameters.AddWithValue("@Name", monster.Name);
                        command.Parameters.AddWithValue("@Health", monster.Health);
                        command.Parameters.AddWithValue("@Attack", monster.Attack);
                        command.Parameters.AddWithValue("@Defense", monster.Defense);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        monsters.Add(monster);
                    }
                }

                conn.Close();
            }
        }
        else
        {
            monsters = MonsterManager.Load();
        }

        return monsters;
    }

    public static List<Monster> Load()
    {
        List<Monster> monsters = new List<Monster>();

        using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
        {
            conn.Open();

            string sql = "SELECT * FROM Monsters";

            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        monsters.Add(new Monster
                            {
                                Id = reader["Id"] == DBNull.Value ? null : reader["Id"].ToString(),
                                Name = reader["Name"].ToString(),
                                Health = Convert.ToInt32(reader["Health"]),
                                Attack = Convert.ToInt32(reader["Attack"]),
                                Defense = Convert.ToInt32(reader["Defense"])
                            }
                        );
                    }
                }
            }
        }

        return monsters;
    }

    public static Monster? Find(string id)
    {
        Monster? monster = null;

        using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
        {
            conn.Open();

            string sql = "SELECT * FROM Monsters WHERE Id = @Id";

            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        monster = new Monster
                        {
                            Id = reader["Id"] == DBNull.Value ? null : reader["Id"].ToString(),
                            Name = reader["Name"].ToString(),
                            Health = Convert.ToInt32(reader["Health"]),
                            Attack = Convert.ToInt32(reader["Attack"]),
                            Defense = Convert.ToInt32(reader["Defense"])
                        };
                    }
                }
            }
        }

        return monster;
    }

    public static void Update(Monster monster)
    {
        using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
        {
            conn.Open();

            string sql = "UPDATE Monsters SET Name = @Name, Health = @Health, Attack = @Attack, Defense = @Defense WHERE Id = @Id";
            using (SQLiteCommand command = new SQLiteCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@Id", monster.Id);
                command.Parameters.AddWithValue("@Name", monster.Name);
                command.Parameters.AddWithValue("@Health", monster.Health);
                command.Parameters.AddWithValue("@Attack", monster.Attack);
                command.Parameters.AddWithValue("@Defense", monster.Defense);

                command.ExecuteNonQuery();
            }

            conn.Close();
        }
    }
}