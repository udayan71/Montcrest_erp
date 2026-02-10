using Montcrest.BLL.DTOs;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobRepo;
        private readonly ITechnologyRepository _techRepo;

        public JobApplicationService (IJobApplicationRepository jobRepo, ITechnologyRepository techRepo)
        {
            _jobRepo = jobRepo;
            _techRepo = techRepo;
        }

        public async Task ApplyAsync(int userId, int technologyId, string resumePath)
        {
            if (!await _techRepo.ExistsAsync(technologyId))
            {
                throw new Exception("Invalid technology selected");

            }
        
            if( await _jobRepo.ExistsAsync( userId,technologyId))
            {
                throw new Exception("You have already applied for this technology");
            }

            var application = new JobApplication
            {
                UserId = userId,
                TechnologyId = technologyId,
                ResumePath = resumePath,
                AppliedOn = DateTime.UtcNow,
            };

            await _jobRepo.AddAsync(application);
        
        }

        public async Task<IEnumerable<JobApplication>> GetApplicationsByUserAsync(int userId)
        {
            return await _jobRepo.GetByUserIdAsync(userId);
        }

        public async Task<CandidateReviewDto?> GetReviewAsync(int applicationId, int userId)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null || app.UserId != userId)
                return null;

            // Make sure Technology is loaded
            var tech = await _techRepo.GetByIdAsync(app.TechnologyId);

            return new CandidateReviewDto
            {
                ApplicationId = app.Id,
                TechnologyName = tech?.Name ?? "N/A",
                ResumePath = app.ResumePath
            };
        }


    }
}
