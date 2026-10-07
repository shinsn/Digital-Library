using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryFinal.Pages.Publishers
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _context;

        public DetailsModel(LibraryContext context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; } = default!;
        public List<Book> Books { get; set; } = new List<Book>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.PublisherName == id);
            if (publisher == null)
            {
                return NotFound();
            }
            else
            {
                Publisher = publisher;
                Books = _context.Books.Where(x => x.PublisherName == id).OrderBy(x => x.Title).ToList();
            }
            return Page();
        }
    }
}
