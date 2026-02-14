using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class Member
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime MemberSince { get; set; }
        private List<Loan> _loans;

        public Member (string memberId, string name, string email)
        {
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException( nameof(email));
            MemberSince = DateTime.Now;
            _loans = new List<Loan>();
        }

        public void AddLoan(Loan loan)
        {
            if (loan == null) throw new ArgumentNullException(nameof(loan));
            _loans.Add(loan);
        }

        public IEnumerable<Loan> GetLoans() => _loans;

        public string GetMemberInfo()
        {
            return $"ID: {MemberId}, Name: {Name}, Email: {Email}, Member since: {MemberSince:yyyy-MM-dd}, Active loans: {_loans.Count}";
        }

        public int GetActiveLoansCount()
        {
            int count = 0;
            foreach (var loan in _loans)
            {
                if(!loan.IsReturned) count++;
            }
            return count;
        }
    }
}
