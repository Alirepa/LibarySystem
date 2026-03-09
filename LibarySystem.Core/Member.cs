using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Member
    {
        [Key]
        public int Id { get; set; }  // Databas-ID

        [Required]
        [StringLength(10)]
        public string MemberId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public DateTime MemberSince { get; set; }

        // Navigation property
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public Member() { } // Parameterlös konstruktor för EF

        public Member(string memberId, string name, string email)
        {
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            MemberSince = DateTime.Now;
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null) throw new ArgumentNullException(nameof(loan));
            Loans.Add(loan);
        }

        public IEnumerable<Loan> GetLoans() => Loans;

        public string GetMemberInfo()
        {
            return $"ID: {MemberId}, Name: {Name}, Email: {Email}, Member since: {MemberSince:yyyy-MM-dd}, Active loans: {GetActiveLoansCount()}";
        }

        public int GetActiveLoansCount()
        {
            int count = 0;
            foreach (var loan in Loans)
            {
                if (!loan.IsReturned) count++;
            }
            return count;
        }
    }
}
