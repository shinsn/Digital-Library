using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryFinal.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly DigitalLibraryFinal.Models.LibraryContext _context;

        public IndexModel(DigitalLibraryFinal.Models.LibraryContext context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publishers.OrderBy(x => x.PublisherName).ToListAsync();
        }
    }
}
