using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Monster_Collector.Managers;

namespace Monster_Collector.Pages;

public class IndexModel : PageModel
{
    public IEnumerable<Monster> Monsters = [];

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
