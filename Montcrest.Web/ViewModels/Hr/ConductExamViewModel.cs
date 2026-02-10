namespace Montcrest.Web.ViewModels.Hr
{
    public class ConductExamViewModel
    {
        public int ApplicationId { get; set; }

        public string CandidateName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; } = string.Empty;

        public string ExamLink { get; set; } = string.Empty;
    }
}
