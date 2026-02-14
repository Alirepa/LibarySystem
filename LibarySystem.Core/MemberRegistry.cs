using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibarySystem.Core
{
    public class MemberRegistry
    {
        private List<Member> _members;
        private Dictionary<string, List<Loan>> _loansByMember;

        public MemberRegistry()
        {
            _members = new List<Member>();
            _loansByMember = new Dictionary<string, List<Loan>>();
        }

        public void RegisterMember(Member member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));
            _members.Add(member);
            _loansByMember[member.MemberId] = new List<Loan>();
        }

        public Member? GetMember(string memberId)
        {
            return _members.FirstOrDefault(m => m.MemberId == memberId);
        }

        public IEnumerable<Member> GetAllMembers() => _members;

        public void AddLoanToMember(string memberId, Loan loan)
        {
            if (_loansByMember.ContainsKey(memberId))
            {
                _loansByMember[memberId].Add(loan);
            }
        }

        public Member? GetMostActiveBorrower()
        {
            if (_members.Count == 0) return null;

            return _members
                .OrderByDescending(m => m.GetLoans().Count())
                .FirstOrDefault();
        }
    }
}
