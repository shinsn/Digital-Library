using System;
using System.Collections.Generic;

namespace DigitalLibraryFinal.Models;

public partial class Publisher
{
    public string PublisherName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
