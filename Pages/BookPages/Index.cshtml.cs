using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Danis_Ioana_Lab2Context _context;

    public IndexModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorId { get; set; }

    public SelectList AuthorsList { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var authors = await _context.Authors.ToListAsync();

        var authorsWithFullName = authors.Select(a => new
        {
            ID = a.ID,
            FullName = a.FirstName + " " + a.LastName
        }).ToList();

        AuthorsList = new SelectList(authorsWithFullName, "ID", "FullName", SelectedAuthorId);

        var booksQuery = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .AsQueryable();

        if (SelectedAuthorId.HasValue)
        {
            booksQuery = booksQuery.Where(b => b.AuthorID == SelectedAuthorId.Value);
        }

        Book = await booksQuery.ToListAsync();
    }
}