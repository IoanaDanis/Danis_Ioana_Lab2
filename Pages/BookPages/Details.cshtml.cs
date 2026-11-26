using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly Danis_Ioana_Lab2Context _context;
    public DetailsModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
    .Include(m => m.Author)
    .Include(m => m.Publisher)
    .Include(m => m.BookCategories)
        .ThenInclude(bc => bc.Category)
    .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        else
        {
            Book = book;
        }

        return Page();
    }
}
