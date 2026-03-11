using LibarySystem.Core;
using LibrarySystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Tests.Del2Tests
{
    public class BookCrudTests : IDisposable
    {
        private readonly LibraryContext _context;

        public BookCrudTests()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new LibraryContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task Test5_Create_AddNewBook()
        {
            // Test 5: CREATE - lägg till ny bok
            var book = new Book("CRUD1", "Skapad bok", "Författare", 2024);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var savedBook = await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == "CRUD1");

            Assert.NotNull(savedBook);
        }

        [Fact]
        public async Task Test6_Read_GetBookById()
        {
            // Test 6: READ - läs bok
            var book = new Book("CRUD2", "Läsbar bok", "Författare", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var foundBook = await _context.Books.FindAsync(book.Id);

            Assert.NotNull(foundBook);
            Assert.Equal("Läsbar bok", foundBook.Title);
        }

        [Fact]
        public async Task Test7_Update_ChangeBookTitle()
        {
            // Test 7: UPDATE - uppdatera bok
            var book = new Book("CRUD3", "Gammal titel", "Författare", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            book.Title = "Ny titel";
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            var updatedBook = await _context.Books.FindAsync(book.Id);
            Assert.Equal("Ny titel", updatedBook.Title);
        }
    }
}
