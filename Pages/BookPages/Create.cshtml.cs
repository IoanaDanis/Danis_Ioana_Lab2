
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Danis_Ioana_Lab2Context _context;

    public CreateModel(Danis_Ioana_Lab2Context context)
    {
        _context = context;
    }

    private void PopulateDropdowns()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher, "ID", "PublisherName");

        var authorsList = _context.Authors
            .Select(a => new
            {
                a.ID,
                FullName = a.FirstName + " " + a.LastName
            })
            .ToList();

        ViewData["AuthorID"] = new SelectList(
            authorsList, "ID", "FullName");
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        Book = new Book
        {
            BookCategories = new List<BookCategory>()
        };

        PopulateDropdowns();
        PopulateAssignedCategoryData(_context, Book);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        Book.BookCategories = new List<BookCategory>();

        if (selectedCategories != null)
        {
            foreach (var cat in selectedCategories)
            {
                Book.BookCategories.Add(new BookCategory
                {
                    CategoryID = int.Parse(cat)
                });
            }
        }

        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            PopulateAssignedCategoryData(_context, Book);
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
