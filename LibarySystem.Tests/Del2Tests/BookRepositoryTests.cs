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
    public class BookRepositoryTests : IDisposable
    {
        private readonly LibraryContext _context;

        public BookRepositoryTests()
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
        public async Task Test1_Database_CanConnect()
        {
            // Test 1: Kontrollera att databasen går att ansluta till
            var canConnect = await _context.Database.CanConnectAsync();
            Assert.True(canConnect);
        }

        [Fact]
        public async Task Test2_CanAddBookToDatabase()
        {
            // Test 2: Kontrollera att man kan lägga till en bok
            var book = new Book("12345", "Testbok", "Testförfattare", 2024);

            _context.Books.Add(book);
            var result = await _context.SaveChangesAsync();

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test3_CanFindBookByISBN()
        {
            // Test 3: Kontrollera att man kan hitta en bok via ISBN
            var book = new Book("99999", "Sökbar bok", "Författare", 2024);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var foundBook = await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == "99999");

            Assert.NotNull(foundBook);
            Assert.Equal("Sökbar bok", foundBook.Title);
        }

        [Fact]
        public async Task Test4_CanCountBooks()
        {
            // Test 4: Kontrollera att man kan räkna böcker
            _context.Books.Add(new Book("111", "Bok 1", "F1", 2020));
            _context.Books.Add(new Book("222", "Bok 2", "F2", 2021));
            _context.Books.Add(new Book("333", "Bok 3", "F3", 2022));
            await _context.SaveChangesAsync();

            var count = await _context.Books.CountAsync();

            Assert.Equal(3, count);
        }
    }
}
