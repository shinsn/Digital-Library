using Microsoft.EntityFrameworkCore;

namespace DigitalLibraryFinal.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }



    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost;database=LibrarySmall;user id=sa; Password=Jajrdth!112;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__70DAFC14684D1BD0");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("AuthorID");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Book__447D36EBCD9D0DA8");

            entity.ToTable("Book");

            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.PublisherNameNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__PublisherN__4D94879B");

            entity.HasMany(d => d.Authors).WithMany(p => p.Isbns)
                .UsingEntity<Dictionary<string, object>>(
                    "WrittenBy",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        //.OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__WrittenBy__Autho__5BE2A6F2"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.Cascade)
                        //.OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__WrittenBy__ISBN__5CD6CB2B"),
                    j =>
                    {
                        j.HasKey("Isbn", "AuthorId").HasName("PK__WrittenB__1370992A17B2EDCC");
                        j.ToTable("WrittenBy");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(20)
                            .IsUnicode(false)
                            .HasColumnName("ISBN");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("AuthorID");
                    });

            entity.HasMany(d => d.GenreNames).WithMany(p => p.Isbns)
                .UsingEntity<Dictionary<string, object>>(
                    "BookGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreName")
                        .OnDelete(DeleteBehavior.Cascade)
                        //.OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookGenre__Genre__571DF1D5"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn")
                        .OnDelete(DeleteBehavior.Cascade)
                        //.OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BookGenres__ISBN__5629CD9C"),
                    j =>
                    {
                        j.HasKey("Isbn", "GenreName").HasName("PK__BookGenr__DFC32AD84E5743BF");
                        j.ToTable("BookGenres");
                        j.IndexerProperty<string>("Isbn")
                            .HasMaxLength(20)
                            .IsUnicode(false)
                            .HasColumnName("ISBN");
                        j.IndexerProperty<string>("GenreName")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreName).HasName("PK__Genre__BBE1C338C3CECAF8");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherName).HasName("PK__Publishe__5F0E22489624C098");

            entity.ToTable("Publisher");

            entity.Property(e => e.PublisherName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });




        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
