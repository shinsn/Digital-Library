using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalLibraryFinal.Models;

namespace DigitalLibraryFinal.Pages.Genres
{
    public class DetailsModel : PageModel
    {
        private readonly DigitalLibraryFinal.Models.LibraryContext _context;

        public DetailsModel(DigitalLibraryFinal.Models.LibraryContext context)
        {
            _context = context;
        }

        public Genre Genre { get; set; } = default!;

        public List<Book> Books { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .Include(b => b.Isbns)
                .FirstOrDefaultAsync(m => m.GenreName == id);
            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                Genre = genre;
                Books = Genre.Isbns.ToList();
            }
            return Page();
        }
    }
}
