
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Danis_Ioana_Lab2.Models;

namespace Danis_Ioana_Lab2.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly Danis_Ioana_Lab2Context _context;

        public DetailsModel(Danis_Ioana_Lab2Context context)
        {
            _context = context;
        }

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
    }
}
