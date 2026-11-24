
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly Danis_Ioana_Lab2Context _context;

        public EditModel(Danis_Ioana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher = await _context.Publisher
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Publisher == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var publisherToUpdate = await _context.Publisher
                .FirstOrDefaultAsync(m => m.ID == id);

            if (publisherToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                publisherToUpdate,
                "Publisher",
                p => p.PublisherName))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
