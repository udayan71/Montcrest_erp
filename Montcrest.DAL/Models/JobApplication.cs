using Montcrest.DAL.Enums;

namespace Montcrest.DAL.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int TechnologyId { get; set; }

        
        public string ResumePath { get; set; } = string.Empty;

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;

        // HR Review
        public DateTime? ReviewedOn { get; set; }
        public string? HrRemarks { get; set; }

        // Exam
        public string? ExamLink { get; set; }
        public DateTime? ExamSentOn { get; set; }

        public int? ExamScore { get; set; }
        public ExamResult ExamResult { get; set; } = ExamResult.Pending;
        public DateTime? ExamCompletedOn { get; set; }

        // Final Decision
        public bool IsSelected { get; set; }
        public DateTime? SelectionDate { get; set; }

        public User? User { get; set; }
        public Technology? Technology { get; set; }
    }
}
