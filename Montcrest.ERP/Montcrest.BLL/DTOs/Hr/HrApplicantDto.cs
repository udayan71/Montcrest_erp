namespace Montcrest.BLL.DTOs.Hr
{
    public class HrApplicantDto
    {
        public int ApplicationId { get; set; }
        public string CandidateName { get; set; }

        public string Email { get; set; }

        public string Resume {  get; set; }

        public string MobileNumber { get; set; }

        public string Status { get; set; }

        public int? ExamScore { get; set; }
        public string? ExamResult { get; set; }
        public string? ExamLink { get; set; }


        public DateTime AppliedOn { get; set; }
    }
}
