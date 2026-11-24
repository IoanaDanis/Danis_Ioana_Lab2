using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class EditModel : PageModel
{
    private readonly Danis_Ioana_Lab2Context _context;

    public EditModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    private void PopulateDropdowns(int? selectedAuthorId = null)
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

        ViewData["AuthorID"] = new SelectList(authorsList, "ID", "FullName", selectedAuthorId);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(m => m.Author)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        PopulateDropdowns(Book.AuthorID);
        ViewData["CurrentAuthorName"] = Book.Author != null
            ? $"{Book.Author.FirstName} {Book.Author.LastName}"
            : "";
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns(Book.AuthorID);
            return Page();
        }

        _context.Attach(Book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(Book.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}