using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class CreateModel
{
    public string? Prompt { get; set; }
}

[Route("api/monster")]
[ApiController]
public class MonsterController : ControllerBase
{
    [HttpPut("{id}")]
    public IActionResult UpdateMonster(string id, Monster monster)
    {
        monster.Id = id;

        // Save the changes.
        var result = MonsterManager.Update(monster);

        return result != null ? new JsonResult(result) : NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult GetMonster(string id)
    {
        var monster = MonsterManager.Find(id);

        return monster != null ? new JsonResult(monster) : NotFound();
    }

    [HttpPost()]
    public IActionResult CreateMonster(CreateModel model)
    {
        // Load the exiting names of monsters.
        var names = MonsterManager.Names();

        // Generate a new monster.
        var monster = new Monster();
        monster.GenerateNameDescription(names, model.Prompt);

        // Save the monster.
        MonsterManager.Update(monster);

        return new JsonResult(monster);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMonster(string id)
    {
        Monster? monster = MonsterManager.Delete(id);

        return monster != null ? new JsonResult(monster) : NotFound();
    }
}