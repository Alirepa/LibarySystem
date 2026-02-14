using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Loan
    {
        public Book Book {  get; set; }
        public Member Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; private set; }
    
        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;
        public bool IsReturned => ReturnDate.HasValue;
        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;

            book.IsAvailable = false;
            member.AddLoan(this);
        }

        public void ReturnBook()
        {
            if (IsReturned) return;

            ReturnDate = DateTime.Now;
            Book.IsAvailable = true;
        }
    }
}
