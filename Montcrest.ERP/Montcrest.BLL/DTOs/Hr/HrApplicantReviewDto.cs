namespace Montcrest.BLL.DTOs.Hr
{
    public class HrApplicantReviewDto
    {
        public int ApplicationId { get; set; }

        public string CandidateName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public string ResumePath { get; set; } = string.Empty;

        public string TechnologyName { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public int? ExamScore { get; set; }
        public string? ExamResult { get; set; }
        public string? ExamLink { get; set; }

        public string? HrRemarks { get; set; }
    }
}
