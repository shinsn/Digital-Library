using System;
using System.Collections.Generic;

namespace DigitalLibraryFinal.Models;

public partial class Genre
{
    public string GenreName { get; set; } = null!;

    public virtual ICollection<Book> Isbns { get; set; } = new List<Book>();


}
