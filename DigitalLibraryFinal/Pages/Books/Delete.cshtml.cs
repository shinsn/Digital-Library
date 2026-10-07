using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryFinal.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly LibraryContext _context;

        public DeleteModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Isbn == id);

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


        public async Task<IActionResult> OnPostAsync()
        {
            if (Book == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.GenreNames)  
                .Include(b => b.Authors)      
                .FirstOrDefaultAsync(b => b.Isbn == Book.Isbn);

            if (book != null)
            {
                book.GenreNames.Clear(); 
                book.Authors.Clear();   // clears the many to many relationship

                _context.Books.Remove(book);


                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }



    }

}
