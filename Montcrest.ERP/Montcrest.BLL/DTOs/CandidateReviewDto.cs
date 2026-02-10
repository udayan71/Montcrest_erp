namespace Montcrest.BLL.DTOs
{
    public class CandidateReviewDto
    {
        public int ApplicationId { get; set; }
        public string TechnologyName { get; set; } = string.Empty;
        public string ResumePath { get; set; } = string.Empty;
    }
}
