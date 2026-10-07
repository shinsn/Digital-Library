namespace DigitalLibraryFinal.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual Publisher PublisherNameNavigation { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> GenreNames { get; set; } = new List<Genre>();

}
