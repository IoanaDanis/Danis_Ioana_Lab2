using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly Danis_Ioana_Lab2Context _context;

    public IndexModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Publisher> Publisher { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Publisher = await _context.Publisher.ToListAsync();
    }
}