using LibarySystem.Core;
using LibrarySystem.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Web.Services
{
    public class LoanService
    {
        private readonly IDbContextFactory<LibraryContext> _dbContextFactory;

        public LoanService(IDbContextFactory<LibraryContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }

        public async Task<List<Loan>> GetActiveLoansAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null)
                .ToListAsync();
        }

        public async Task<Loan?> CreateLoanAsync(string isbn, string memberId, int loanDays = 14)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            var book = await context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
            var member = await context.Members.FirstOrDefaultAsync(m => m.MemberId == memberId);

            if (book == null || member == null || !book.IsAvailable)
                return null;

            var loan = new Loan
            {
                BookId = book.Id,
                MemberId = member.Id,
                LoanDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(loanDays),
                Book = book,
                Member = member
            };

            book.IsAvailable = false;
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            return loan;
        }

        public async Task<bool> ReturnBookAsync(string isbn)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            var loan = await context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Book.ISBN == isbn && l.ReturnDate == null);

            if (loan == null)
                return false;

            loan.ReturnDate = DateTime.Now;
            loan.Book.IsAvailable = true;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetOverdueLoansCountAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Loans
                .Where(l => l.ReturnDate == null && l.DueDate < DateTime.Now)
                .CountAsync();
        }
    }
}
