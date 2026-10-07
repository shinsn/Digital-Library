using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DigitalLibraryFinal.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _context;

        public DetailsModel(LibraryContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Books
                .Include(b => b.GenreNames)
                .FirstOrDefaultAsync(b => b.Isbn == id);
           
            if (book == null)
            {
                return NotFound();
            }
            else
            {
                Book = book;
               
            }
            return Page();
        }
    }
}
