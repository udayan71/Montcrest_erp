using Microsoft.EntityFrameworkCore;
using Montcrest.DAL.Context;
using Montcrest.DAL.Enums;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.DAL.Repositories.Implementations
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly MontcrestDbContext _context;

        public JobApplicationRepository(MontcrestDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(JobApplication application)
        {
            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobApplication application)
        {
            _context.JobApplications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task<JobApplication?> GetByIdWithUserAsync(int id)
        {
            return await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.Technology)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<JobApplication>> GetExamPendingApplicationsAsync()
        {
            return await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.Technology)
                .Where(a => a.Status == ApplicationStatus.ExamSent &&
                            a.ExamScore == null)
                .ToListAsync();
        }


        public async Task<bool> ExistsAsync(int userId, int technologyId)
        {
            return await _context.JobApplications
                .AnyAsync(a => a.UserId == userId && a.TechnologyId == technologyId);
        }

        public async Task<JobApplication?> GetByIdAsync(int id)
        {
            return await _context.JobApplications
                .Include(a => a.User)
                .Include(a => a.Technology)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<JobApplication>> GetByUserIdAsync(int userId)
        {
            return await _context.JobApplications
                .Include(a => a.Technology)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<JobApplication>> GetByTechnologyIdAsync(int technologyId)
        {
            return await _context.JobApplications
                .Include(a => a.User)
                .Where(a => a.TechnologyId == technologyId)
                .ToListAsync();
        }

    }
}
