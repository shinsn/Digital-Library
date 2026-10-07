using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryFinal.Pages.Books
{
    public class CreateModel : PageModel
    {
        public class BookInputModel
        {
            public string Isbn { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public DateOnly PublishDate { get; set; }

            public string PublisherName { get; set; } = string.Empty;

            public List<string> SelectedGenreNames { get; set; } = new();
            public List<SelectListItem> AllGenres { get; set; } = new();

            public List<int> SelectedAuthorIds { get; set; } = new();
            public List<SelectListItem> AllAuthors { get; set; } = new();

        }


        private readonly LibraryContext _context;

        public CreateModel(LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookInputModel Input { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {

            Input.AllGenres = await _context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.GenreName,
                    Text = g.GenreName
                }).ToListAsync();


            Input.AllAuthors = await _context.Authors
                .Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.AuthorName
                }).ToListAsync();


            ViewData["PublisherName"] = new SelectList(_context.Publishers, "PublisherName", "PublisherName");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                Input.AllGenres = await _context.Genres
                    .Select(g => new SelectListItem
                    {
                        Value = g.GenreName,
                        Text = g.GenreName
                    }).ToListAsync();

                Input.AllAuthors = await _context.Authors
                    .Select(a => new SelectListItem
                    {
                        Value = a.AuthorId.ToString(),
                        Text = a.AuthorName
                    }).ToListAsync();

                ViewData["PublisherName"] = new SelectList(_context.Publishers, "PublisherName", "PublisherName");

                return Page();
            }


            var selectedGenres = await _context.Genres
                .Where(g => Input.SelectedGenreNames.Contains(g.GenreName))
                .ToListAsync();

            var selectedAuthors = await _context.Authors
                .Where(a => Input.SelectedAuthorIds.Contains(a.AuthorId))
                .ToListAsync();


            var newBook = new Book
            {
                Isbn = Input.Isbn,
                Title = Input.Title,
                Description = Input.Description,
                PublishDate = Input.PublishDate,
                PublisherName = Input.PublisherName,
                GenreNames = selectedGenres,
                Authors = selectedAuthors
            };


            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
