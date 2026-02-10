namespace Montcrest.Web.ViewModels.Hr
{
    public class HrApplicantReviewViewModel
    {
        public int ApplicationId { get; set; }

        public string CandidateName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; } = string.Empty;

        public string ResumePath { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string? HrRemarks { get; set; }

       
    }
}
