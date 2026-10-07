using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalLibraryFinal.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false),
                    AuthorName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Author__70DAFC14684D1BD0", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genre__BBE1C338C3CECAF8", x => x.GenreName);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishe__5F0E22489624C098", x => x.PublisherName);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PublishDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PublisherName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Book__447D36EBCD9D0DA8", x => x.ISBN);
                    table.ForeignKey(
                        name: "FK__Book__PublisherN__4D94879B",
                        column: x => x.PublisherName,
                        principalTable: "Publisher",
                        principalColumn: "PublisherName");
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    GenreName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookGenr__DFC32AD84E5743BF", x => new { x.ISBN, x.GenreName });
                    table.ForeignKey(
                        name: "FK__BookGenre__Genre__571DF1D5",
                        column: x => x.GenreName,
                        principalTable: "Genre",
                        principalColumn: "GenreName");
                    table.ForeignKey(
                        name: "FK__BookGenres__ISBN__5629CD9C",
                        column: x => x.ISBN,
                        principalTable: "Book",
                        principalColumn: "ISBN");
                });

            migrationBuilder.CreateTable(
                name: "WrittenBy",
                columns: table => new
                {
                    ISBN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WrittenB__1370992A17B2EDCC", x => new { x.ISBN, x.AuthorID });
                    table.ForeignKey(
                        name: "FK__WrittenBy__Autho__5BE2A6F2",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "AuthorID");
                    table.ForeignKey(
                        name: "FK__WrittenBy__ISBN__5CD6CB2B",
                        column: x => x.ISBN,
                        principalTable: "Book",
                        principalColumn: "ISBN");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherName",
                table: "Book",
                column: "PublisherName");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_GenreName",
                table: "BookGenres",
                column: "GenreName");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenBy_AuthorID",
                table: "WrittenBy",
                column: "AuthorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "WrittenBy");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
