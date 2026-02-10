using Montcrest.BLL.DTOs;
using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Interfaces
{
    public interface IJobApplicationService
    {
        Task ApplyAsync(int userId, int technologyId, string resumePath);
        Task<IEnumerable<JobApplication>> GetApplicationsByUserAsync(int userId);
        Task<CandidateReviewDto?> GetReviewAsync(int applicationId, int userId);


    }
}
