using LibarySystem.Core;
using LibrarySystem.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Web.Services
{
    public class MemberService
    {
        private readonly IDbContextFactory<LibraryContext> _dbContextFactory;

        public MemberService(IDbContextFactory<LibraryContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Members
                .Include(m => m.Loans)
                .ThenInclude(l => l.Book)
                .ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Members
                .Include(m => m.Loans)
                .ThenInclude(l => l.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Member?> GetMemberByMemberIdAsync(string memberId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Members
                .FirstOrDefaultAsync(m => m.MemberId == memberId);
        }

        public async Task AddMemberAsync(Member member)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Members.Add(member);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMemberAsync(Member member)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            context.Members.Update(member);
            await context.SaveChangesAsync();
        }
    }
}
