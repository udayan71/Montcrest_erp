namespace Montcrest.Web.ViewModels.Hr
{
    public class HrApplicantViewModel
    {
        public int ApplicationId { get; set; }

        public string CandidateName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public string Resume { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime AppliedOn { get; set; }

        public int? ExamScore { get; set; }
        public string ExamResult { get; set; } = string.Empty;
        public string? ExamLink { get; set; }
    }
}
