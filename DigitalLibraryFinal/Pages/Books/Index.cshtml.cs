using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DigitalLibraryFinal.Models;

namespace DigitalLibraryFinal.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DigitalLibraryFinal.Models.LibraryContext _context;

        public IndexModel(DigitalLibraryFinal.Models.LibraryContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Books
                .Include(b => b.PublisherNameNavigation).ToListAsync();
        }
    }
}
