using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalLibraryFinal.Models;

namespace DigitalLibraryFinal.Pages.Authors
{
    public class DetailsModel : PageModel
    {
        private readonly DigitalLibraryFinal.Models.LibraryContext _context;

        public DetailsModel(DigitalLibraryFinal.Models.LibraryContext context)
        {
            _context = context;
        }

        public Author Author { get; set; } = default!;

        public List<Book> Books { get; set; } = [];


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.Authors
                .Include(a => a.Isbns)
                .FirstOrDefaultAsync(m => m.AuthorId == id);
            

            if (author == null)
            {
                return NotFound();
            }
            else
            {
                Author = author;
                Books = Author.Isbns.ToList();
            }
            return Page();
        }
    }
}
