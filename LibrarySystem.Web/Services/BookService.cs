using LibarySystem.Core;
using LibrarySystem.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Web.Services
{
    public class BookService
    {
        private readonly IDbContextFactory<LibraryContext> _dbContextFactory;

        public BookService(IDbContextFactory<LibraryContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Books.FindAsync(id);
        }

        public async Task<Book?> GetBookByISBNAsync(string isbn)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<List<Book>> SearchBooksAsync(string searchTerm)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            if (string.IsNullOrWhiteSpace(searchTerm))
                return await context.Books.ToListAsync();

            searchTerm = searchTerm.ToLower();
            return await context.Books
                .Where(b => b.Title.ToLower().Contains(searchTerm) ||
                            b.Author.ToLower().Contains(searchTerm) ||
                            b.ISBN.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Books.Add(book);
            await context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Books.Update(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var book = await context.Books.FindAsync(id);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }
    }
}
