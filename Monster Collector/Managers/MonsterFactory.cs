﻿using System.Text.RegularExpressions;

namespace Monster_Collector.Managers;

public partial class MonsterFactory(LLM llm)
{
    private readonly LLM Llm = llm;

    [GeneratedRegex(@"\s+")]
    private static partial Regex MyRegex();

    public Monster Create(List<string> ignoreNames, string? customPrompt = null)
    {
        var monster = new Monster();
        if (Llm.IsValid())
        {
            GenerateNameDescription(monster, ignoreNames, customPrompt);
        }

        return monster;
    }

    private string? GenerateNameDescription(Monster monster, List<string> ignoreNames, string? customPrompt = null)
    {
        string? output = null;

        try
        {
            Console.WriteLine($"Generating name and description for type {monster.Name}");

            string prompt = @$"
            Generate a unique creative monster name and description for a scary monster in a dungeon game.
            The description should be no longer than one sentence. The name should be three or less words and be creative and unique.
            Output the result in the format: Name, Description. The Name and Description must be separated by a comma.
            The type of monster should be {monster.Name}.
            {customPrompt ?? ""}
            The name of the monster can not include any of the following names.";

            // Format the prompt to remove spaces and newlines.
            prompt = prompt.Replace("\r\n", "").Trim();
            prompt = MyRegex().Replace(prompt, " ");

            // Call the LLM.
            output = Llm.GetTextAsync(prompt, ignoreNames != null && ignoreNames.Count > 0 ? string.Join(", ", ignoreNames) : "null").GetAwaiter().GetResult();

            // Parse the output for "name, description".
            if (output is not null)
            {
                var parts = output.Split(",", 2);
                monster.Name = parts[0].Trim();
                monster.Description = parts[1].Trim();

                Console.WriteLine($"Created {monster.Name}, {monster.Description}");
            }
        }
        catch (Exception excep)
        {
            Console.WriteLine(excep.Message);
        }

        return output;
    }

    public string? GenerateImage(Monster monster)
    {
        string? url = "";

        try
        {
            Console.WriteLine($"Generating image for {monster.Name} - {monster.Description}");
            var imageResult = llm.GetImage($"{monster.Name} - {monster.Description}").GetAwaiter().GetResult();

            Console.WriteLine($"Created {monster.Description}");
        }
        catch (Exception excep)
        {
            Console.WriteLine(excep.Message);
        }

        return url;
    }
}