using DigitalLibraryFinal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalLibraryFinal.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly LibraryContext _context;

    public List<Book> BookList { get; set; }

    //public List<Author>AuthorList { get; set; }


    public IndexModel(ILogger<IndexModel> logger, LibraryContext context)
    {
        _logger = logger;
        _context = context;
        BookList = default!;
        //AuthorList = default!;

    }

//need to add FK's and PK's on new int table... causing build errors bc of relationship problems
    public void OnGet()
    {   
        BookList = _context.Books.OrderBy(x => x.Title).ToList();
        //AuthorList = _context.Authors.ToList()
    }
}
