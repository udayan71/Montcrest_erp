using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Context;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.DAL.Repositories.Implementations
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly MontcrestDbContext _context;

        public TechnologyRepository(MontcrestDbContext context)
        {
            _context = context;
        }

        public async Task<List<Technology>> GetAllAsync()
        {
            return await _context.Technologies
                .Include(t => t.JobApplications)
                .ToListAsync();
        }


        public async Task<Technology?> GetByIdAsync(int id)
        {
            return await _context.Technologies
                .FirstOrDefaultAsync(t => t.Id == id);
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Technologies.AnyAsync(x => x.Id == id);
        }
    }
}
