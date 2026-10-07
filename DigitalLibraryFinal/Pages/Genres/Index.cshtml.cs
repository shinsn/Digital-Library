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
    public class IndexModel : PageModel
    {
        private readonly DigitalLibraryFinal.Models.LibraryContext _context;

        public IndexModel(DigitalLibraryFinal.Models.LibraryContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Genre = await _context.Genres.ToListAsync();
        }
    }
}
