using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class BookCatalog
    {
        private List<Book> _books;

        public BookCatalog()
        {
            _books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _books.Add(book);
        }

        public IEnumerable<Book> GetAllBooks() => _books;

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            return _books.Where(book => book.Matches(searchTerm));
        }

        public IEnumerable<Book> SortBooksByTitle()
        {
            return _books.OrderBy(book => book.Title);
        }

        public IEnumerable<Book> SortBooksByPublishedYear()
        {
            return _books.OrderBy(book => book.PublishedYear);
        }

        public int GetTotalBookCount() => _books.Count;

        public int GetBorrowedBookCount()
        {
            return _books.Count(book => !book.IsAvailable);
        }
    }
}

