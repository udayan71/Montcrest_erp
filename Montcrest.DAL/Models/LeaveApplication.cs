using Montcrest.DAL.Enums;

namespace Montcrest.DAL.Models
{
    public class LeaveApplication
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ManagerId { get; set; }
        public Manager Manager { get; set; } = null!;

        public LeaveType LeaveType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ReviewedOn { get; set; }
        public string? ManagerRemarks { get; set; }
    }
}
