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
    public class IntegrationTests : IDisposable
    {
        private readonly LibraryContext _context;

        public IntegrationTests()
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
        public async Task Test8_CanCreateLoan()
        {
            // Test 8: Integration - skapa lån
            var book = new Book("INT1", "Bok att låna", "Författare", 2024);
            var member = new Member("M001", "Låntagare", "test@test.com");

            _context.Books.Add(book);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            _context.Loans.Add(loan);
            var result = await _context.SaveChangesAsync();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task Test9_CanReturnBook()
        {
            // Test 9: Integration - returnera bok
            var book = new Book("INT2", "Bok att returnera", "Författare", 2024);
            var member = new Member("M002", "Låntagare 2", "test2@test.com");

            _context.Books.Add(book);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7));
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            loan.ReturnBook();
            var result = await _context.SaveChangesAsync();

            Assert.True(loan.IsReturned);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public async Task Test10_CanSearchBooks()
        {
            // Test 10: Integration - sökning
            _context.Books.Add(new Book("INT3", "Sagan om ringen", "J.R.R. Tolkien", 1954));
            _context.Books.Add(new Book("INT4", "Hobbiten", "J.R.R. Tolkien", 1937));
            _context.Books.Add(new Book("INT5", "Harry Potter", "J.K. Rowling", 1997));
            await _context.SaveChangesAsync();

            var searchTerm = "Tolkien";
            var results = await _context.Books
                .Where(b => b.Author.Contains(searchTerm))
                .ToListAsync();

            Assert.Equal(2, results.Count);
        }
    }
}
