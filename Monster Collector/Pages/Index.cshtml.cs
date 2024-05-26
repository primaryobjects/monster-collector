using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Monster_Collector.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<Monster> Monsters = new List<Monster>();

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Monsters = MonsterManager.Load();
    }
}
