using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Monster
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Monster()
    {
        Random rand = new Random((int)DateTime.Now.Ticks);
        Id = Guid.NewGuid().ToString();
        Name = Constants.MonsterNames[rand.Next(Constants.MonsterNames.Count)];
        Description = "A big scary " + Name;
        Health = rand.Next(20, 101);
        Attack = rand.Next(10, 51);
        Defense = rand.Next(5, 26);
    }

    public string? GenerateNameDescription(Monster monster, List<string> ignoreNames, string? customPrompt = null)
    {
        string? output = null;

        try
        {
            Console.WriteLine($"Generating name and description for type {Name}");

            string prompt = @$"
            Generate a unique creative monster name and description for a scary monster in a dungeon game.
            The description should be no longer than one sentence. The name should be three or less words and be creative and unique.
            Output the result in the format: Name, Description. The Name and Description must be separated by a comma.
            The type of monster should be {Name}.
            {customPrompt ?? ""}
            The name of the monster can not include any of the following names.";

            // Format the prompt to remove spaces and newlines.
            prompt = prompt.Replace("\r\n", "").Trim();
            prompt = Regex.Replace(prompt, @"\s+", " ");

            // Call the LLM.
            output = new CohereManager().GetTextAsync(prompt, ignoreNames != null && ignoreNames.Count > 0 ? string.Join(", ", ignoreNames) : "null").GetAwaiter().GetResult();

            // Parse the output for "name, description".
            if (output is not null)
            {
                var parts = output.Split(",", 2);
                Name = parts[0].Trim();
                Description = parts[1].Trim();

                Console.WriteLine($"Created {Name}, {Description}");    
            }
        }
        catch (Exception excep)
        {
            Console.WriteLine(excep.Message);
        }

        return output;
    }

    public override string ToString()
    {
        return "(" + Id + ") " + Name + " - Health " + Health + ", Attack " + Attack + ", Defense " + Defense + "\n" + Description;
    }
}