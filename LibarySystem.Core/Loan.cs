using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }  // Databas-ID

        [Required]
        public int BookId { get; set; }  // Foreign Key

        [Required]
        public int MemberId { get; set; }  // Foreign Key

        // Navigation properties
        [ForeignKey("BookId")]
        public Book Book { get; set; } = null!;

        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [NotMapped]  // Beräknas, sparas inte i databas
        public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;

        [NotMapped]
        public bool IsReturned => ReturnDate.HasValue;

        public Loan() { } // Parameterlös konstruktor för EF

        public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
        {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));
            LoanDate = loanDate;
            DueDate = dueDate;
            ReturnDate = null;

            book.IsAvailable = false;
            // Vi lägger inte till loan i member.Loans här - det görs via navigation property
        }

        public void ReturnBook()
        {
            if (IsReturned) return;

            ReturnDate = DateTime.Now;
            Book.IsAvailable = true;
        }
    }
}
