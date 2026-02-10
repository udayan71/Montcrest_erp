using Montcrest.BLL.DTOs.Hr;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Enums;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.BLL.Services
{
    public class RecruitmentWorkflowService : IRecruitmentWorkflowService
    {
        private readonly IJobApplicationRepository _jobRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmailService _emailService;
        private readonly IGoogleSheetService _googleSheetService;

        private const int PassMarks = 35;

        public RecruitmentWorkflowService(
            IJobApplicationRepository jobRepo,
            IUserRepository userRepo,
            IEmailService emailService,
            IGoogleSheetService googleSheetService)
        {
            _jobRepo = jobRepo;
            _userRepo = userRepo;
            _emailService = emailService;
            _googleSheetService = googleSheetService;
        }

        public async Task<HrApplicantReviewDto> GetApplicationForReviewAsync(int applicationId)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            return new HrApplicantReviewDto
            {
                ApplicationId = app.Id,
                CandidateName = app.User!.FullName,
                Email = app.User.Email,
                MobileNumber = app.User.MobileNumber,
                ResumePath = app.ResumePath,
                TechnologyName = app.Technology!.Name,
                Status = app.Status.ToString(),
                HrRemarks = app.HrRemarks,
                ExamLink = app.ExamLink,
                ExamScore = app.ExamScore,
                ExamResult = app.ExamResult.ToString()
            };
        }

        public async Task ReviewApplicationAsync(int applicationId, string remarks)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status == ApplicationStatus.Selected || app.Status == ApplicationStatus.Rejected)
                throw new Exception("Finalized application cannot be reviewed again.");

            if (app.Status != ApplicationStatus.Applied)
                throw new Exception("Only Applied applications can be reviewed");

            app.Status = ApplicationStatus.Reviewed;
            app.HrRemarks = remarks;
            app.ReviewedOn = DateTime.UtcNow;

            await _jobRepo.UpdateAsync(app);
        }

        public async Task SendExamAsync(int applicationId, string examLink)
        {
            var app = await _jobRepo.GetByIdWithUserAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status != ApplicationStatus.Reviewed)
                throw new Exception("Only Reviewed applications can receive exam");

            app.ExamLink = examLink;
            app.ExamSentOn = DateTime.UtcNow;
            app.Status = ApplicationStatus.ExamSent;

            await _jobRepo.UpdateAsync(app);

            // Send email
            await _emailService.SendExamLinkAsync(app.User!.Email, app.User.FullName, examLink);
        }

        public async Task SyncExamScoreAsync(int applicationId)
        {
            var app = await _jobRepo.GetByIdWithUserAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status != ApplicationStatus.ExamSent)
                throw new Exception("Exam is not sent yet");

            var score = await _googleSheetService.GetCandidateScoreAsync(app.User!.Email);

            if (score == null)
                throw new Exception("Score not found in Google Sheet yet");

            await SubmitExamResultAsync(applicationId, score.Value);
        }



        public async Task FetchExamScoreAndUpdateAsync(int applicationId)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status != ApplicationStatus.ExamSent)
                throw new Exception("Exam must be sent before fetching score");

            var score = await _googleSheetService.GetCandidateScoreAsync(app.User!.Email);

            if (score == null)
                throw new Exception("Score not found yet in Google Sheet");

            await SubmitExamResultAsync(applicationId, score.Value);
        }

        public async Task SubmitExamResultAsync(int applicationId, int score)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status != ApplicationStatus.ExamSent)
                throw new Exception("Exam result can only be submitted after exam is sent");

            app.ExamScore = score;
            app.ExamCompletedOn = DateTime.UtcNow;
            app.Status = ApplicationStatus.ExamCompleted;

            if (score >= PassMarks)
            {
                app.ExamResult = ExamResult.Passed;
                app.Status = ApplicationStatus.Selected;
                app.IsSelected = true;
                app.SelectionDate = DateTime.UtcNow;

                var user = app.User!;
                user.Role = UserRole.Employee;
                user.IsEmployee = true;
                user.JoinedOn = DateTime.UtcNow;

                await _userRepo.UpdateAsync(user);
            }
            else
            {
                app.ExamResult = ExamResult.Failed;
                app.Status = ApplicationStatus.Rejected;
                app.IsSelected = false;
            }

            await _jobRepo.UpdateAsync(app);
        }

        public async Task RejectApplicationAsync(int applicationId, string remarks)
        {
            var app = await _jobRepo.GetByIdAsync(applicationId);

            if (app == null)
                throw new Exception("Application not found");

            if (app.Status == ApplicationStatus.Selected)
                throw new Exception("Selected candidate cannot be rejected");

            app.Status = ApplicationStatus.Rejected;
            app.HrRemarks = remarks;
            app.ReviewedOn = DateTime.UtcNow;

            await _jobRepo.UpdateAsync(app);
        }
    }
}
