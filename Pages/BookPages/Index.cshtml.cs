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
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorId { get; set; }

    public SelectList AuthorsList { get; set; } = default!;

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        var authors = await _context.Authors.ToListAsync();

        var authorsWithFullName = authors.Select(a => new
        {
            ID = a.ID,
            FullName = a.FirstName + " " + a.LastName
        }).ToList();

        AuthorsList = new SelectList(authorsWithFullName, "ID", "FullName", SelectedAuthorId);

        BookD = new BookData();

        var booksQuery = _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Author)
            .Include(b => b.BookCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Title);

        if (SelectedAuthorId.HasValue)
        {
            booksQuery = booksQuery.Where(b => b.AuthorID == SelectedAuthorId.Value)
                .OrderBy(b => b.Title);
        }

        BookD.Books = await booksQuery.ToListAsync();

        Book = BookD.Books.ToList();

        if (id != null)
        {
            BookID = id.Value;

            Book book = BookD.Books
                .Where(i => i.ID == id.Value).Single();

            BookD.Categories = book.BookCategories.Select(s => s.Category);
        }
    }
}