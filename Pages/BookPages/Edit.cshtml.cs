
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
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
            authorsList, "ID", "FullName", selectedAuthorId);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Book = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

        if (Book == null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);
        PopulateDropdowns(Book.AuthorID);

        ViewData["CurrentAuthorName"] = Book.Author != null
            ? $"{Book.Author.FirstName} {Book.Author.LastName}"
            : "";

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.AuthorID,
            b => b.Price,
            b => b.PublishingDate,
            b => b.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        UpdateBookCategories(_context, selectedCategories, bookToUpdate);
        Book = bookToUpdate;
        PopulateAssignedCategoryData(_context, bookToUpdate);
        PopulateDropdowns(bookToUpdate.AuthorID);

        return Page();
    }
}
