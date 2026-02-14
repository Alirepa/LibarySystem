using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Library
    {
        private BookCatalog _bookCatalog;
        private MemberRegistry _memberRegistry;
        private LoanManager _loanManager;

        public Library()
        {
            _bookCatalog = new BookCatalog();
            _memberRegistry = new MemberRegistry();
            _loanManager = new LoanManager();
        }

        // Bokrelaterade metoder
        public void AddBook(Book book) => _bookCatalog.AddBook(book);
        public IEnumerable<Book> SearchBooks(string searchTerm) => _bookCatalog.SearchBooks(searchTerm);
        public IEnumerable<Book> SortBooksByTitle() => _bookCatalog.SortBooksByTitle();
        public IEnumerable<Book> SortBooksByPublishedYear() => _bookCatalog.SortBooksByPublishedYear();

        // Medlemsrelaterade metoder
        public void RegisterMember(Member member) => _memberRegistry.RegisterMember(member);
        public Member? GetMember(string memberId) => _memberRegistry.GetMember(memberId);

        public IEnumerable<Member> GetAllMembers()
        {
            return _memberRegistry.GetAllMembers();
        }

        // Lånerelaterade metoder
        public Loan? BorrowBook(string isbn, string memberId)
        {
            var book = _bookCatalog.GetAllBooks().FirstOrDefault(b => b.ISBN == isbn);
            var member = _memberRegistry.GetMember(memberId);

            if (book == null || member == null || !book.IsAvailable)
                return null;

            return _loanManager.CreateLoan(book, member);
        }

        public bool ReturnBook(string isbn)
        {
            var loan = _loanManager.GetAllLoans()
                .FirstOrDefault(l => !l.IsReturned && l.Book.ISBN == isbn);

            if (loan == null) return false;

            _loanManager.ReturnLoan(loan);
            return true;
        }

        // Statistikmetoder
        public int GetTotalBooks() => _bookCatalog.GetTotalBookCount();
        public int GetBorrowedBooksCount() => _bookCatalog.GetBorrowedBookCount();
        public Member? GetMostActiveBorrower() => _memberRegistry.GetMostActiveBorrower();
        public int GetOverdueLoansCount() => _loanManager.GetOverdueLoans().Count();

        public string GetLibraryStatistics()
        {
            return $"Total books: {GetTotalBooks()}\n" +
                   $"Borrowed books: {GetBorrowedBooksCount()}\n" +
                   $"Overdue loans: {GetOverdueLoansCount()}\n" +
                   $"Registered members: {_memberRegistry.GetAllMembers().Count()}";
        }
    }
}
