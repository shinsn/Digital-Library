namespace DigitalLibraryFinal.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public virtual ICollection<Book> Isbns { get; set; } = new List<Book>();
}
