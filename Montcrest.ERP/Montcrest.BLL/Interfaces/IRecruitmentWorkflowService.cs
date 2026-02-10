using Montcrest.BLL.DTOs.Hr;

namespace Montcrest.BLL.Interfaces
{
    public interface IRecruitmentWorkflowService
    {
        Task ReviewApplicationAsync(int applicationId, string remarks);

        Task SendExamAsync(int applicationId, string examLink);

        Task SubmitExamResultAsync(int applicationId, int score);

        Task RejectApplicationAsync(int applicationId, string remarks);

        Task SyncExamScoreAsync(int applicationId);

        Task<HrApplicantReviewDto> GetApplicationForReviewAsync(int applicationId);
    }
}
