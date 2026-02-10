using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Context;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.DAL.Repositories.Implementations
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly MontcrestDbContext _context;

        public ManagerRepository(MontcrestDbContext context)
        {
            _context = context;
        }

        public async Task<Manager?> GetByUserIdAsync(int userId)
        {
            return await _context.Managers
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.UserId == userId);
        }

        public async Task AddAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
        }
    }
}
