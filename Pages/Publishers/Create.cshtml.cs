using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly Danis_Ioana_Lab2Context _context;

        public CreateModel(Danis_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Publisher == null || Publisher == null)
            {
                return Page();
            }

            _context.Publisher.Add(Publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
