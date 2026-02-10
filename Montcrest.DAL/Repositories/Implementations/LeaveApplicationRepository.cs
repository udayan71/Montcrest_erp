using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Context;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.DAL.Repositories.Implementations
{
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        private readonly MontcrestDbContext _context;

        public LeaveApplicationRepository(MontcrestDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeaveApplication leave)
        {
            _context.LeaveApplications.Add(leave);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LeaveApplication>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.LeaveApplications
                .Include(l => l.Manager)
                    .ThenInclude(m => m.User)
                .Where(l => l.EmployeeId == employeeId)
                .OrderByDescending(l => l.AppliedOn)
                .ToListAsync();
        }


        public async Task<List<LeaveApplication>> GetByManagerIdAsync(int managerId)
        {
            return await _context.LeaveApplications
                .Include(l => l.Employee)
                    .ThenInclude(e => e.User)
                .Where(l => l.ManagerId == managerId)
                .OrderByDescending(l => l.AppliedOn)
                .ToListAsync();
        }


        public async Task<LeaveApplication?> GetByIdAsync(int leaveId)
        {
            return await _context.LeaveApplications
                .Include(l => l.Employee)
                    .ThenInclude(e => e.User)
                .Include(l => l.Manager)
                    .ThenInclude(m => m.User)
                .FirstOrDefaultAsync(l => l.Id == leaveId);
        }


        public async Task UpdateAsync(LeaveApplication leave)
        {
            _context.LeaveApplications.Update(leave);
            await _context.SaveChangesAsync();
        }
    }
}
