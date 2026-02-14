using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class LoanManager
    {
        private List<Loan> _loans;

        public LoanManager()
        {
            _loans = new List<Loan>();
        }

        public Loan CreateLoan(Book book, Member member, int loanDays = 14)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            if (member == null) throw new ArgumentNullException(nameof(member));
            if (!book.IsAvailable) throw new InvalidOperationException("Book is not available");

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(loanDays));
            _loans.Add(loan);
            return loan;
        }

        public void ReturnLoan(Loan loan)
        {
            if (loan == null) throw new ArgumentNullException(nameof(loan));
            loan.ReturnBook();
        }

        public IEnumerable<Loan> GetAllLoans() => _loans;

        public IEnumerable<Loan> GetOverdueLoans()
        {
            return _loans.Where(loan => loan.IsOverdue);
        }
    }
}
