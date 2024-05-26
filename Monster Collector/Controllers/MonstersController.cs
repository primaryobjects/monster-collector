using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
}