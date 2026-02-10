using Montcrest.BLL.DTOs.Hr;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.BLL.Services
{
    public class HrService : IHrService
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public HrService(
            ITechnologyRepository technologyRepository,
            IJobApplicationRepository jobApplicationRepository)
        {
            _technologyRepository = technologyRepository;
            _jobApplicationRepository = jobApplicationRepository;
        }

        // 1️⃣ HR dashboard – technologies with application count
        public async Task<List<HrTechnologySummaryDto>> GetTechnologiesAsync()
        {
            var technologies = await _technologyRepository.GetAllAsync();

            var result = technologies.Select(t => new HrTechnologySummaryDto
            {
                TechnologyId = t.Id,
                TechnologyName = t.Name,
                ApplicationsCount = t.JobApplications?.Count ?? 0
            }).ToList();

            return result;
        }

        
        public async Task<List<HrApplicantDto>> GetApplicantsByTechnologyAsync(int technologyId)
        {
            var applications =
                await _jobApplicationRepository.GetByTechnologyIdAsync(technologyId);

            return applications.Select(a => new HrApplicantDto
            {
                ApplicationId = a.Id,
                CandidateName = a.User.FullName,
                Email = a.User.Email,
                MobileNumber=a.User.MobileNumber,
                Status=a.Status.ToString(),
                Resume = a.ResumePath,
                AppliedOn = a.AppliedOn,

                ExamScore = a.ExamScore,
                ExamResult = a.ExamResult.ToString(),
                ExamLink = a.ExamLink
            }).ToList();
        }
    }
}
