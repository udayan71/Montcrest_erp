using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface IJobApplicationRepository
    {

        Task AddAsync (JobApplication application);

        Task UpdateAsync(JobApplication application);

        Task<List<JobApplication>> GetExamPendingApplicationsAsync();
        Task<JobApplication?> GetByIdWithUserAsync(int id);


        Task<bool> ExistsAsync(int userId, int technologyId);


        Task<JobApplication?> GetByIdAsync(int id);
        Task<List<JobApplication>> GetByUserIdAsync(int userId);
        Task<List<JobApplication>> GetByTechnologyIdAsync(int technologyId);

    }
}
