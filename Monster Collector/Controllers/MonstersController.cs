using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/monster")]
[ApiController]
public class MonsterController : ControllerBase
{
    [HttpPut("{id}")]
    public IActionResult UpdateMonster(string id, Monster monster)
    {
        Monster existingMonster = MonsterManager.Find(id);
        if (existingMonster != null)
        {
            existingMonster.Name = monster.Name;
            existingMonster.Health = monster.Health;
            existingMonster.Attack = monster.Attack;
            existingMonster.Defense = monster.Defense;

            // Save the changes...
            MonsterManager.Update(existingMonster);

            // Return a success result...
            return new JsonResult(existingMonster);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetMonster(string id)
    {
        var monster = MonsterManager.Find(id);

        return monster != null ? new JsonResult(monster) : NotFound();
    }
}