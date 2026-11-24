using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Danis_Ioana_Lab2Context _context;

    public CreateModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    private void PopulateDropdowns()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
            "PublisherName");

        var authorsList = _context.Authors
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToList();

        ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName");
    }

    public IActionResult OnGet()
    {
        PopulateDropdowns();
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}