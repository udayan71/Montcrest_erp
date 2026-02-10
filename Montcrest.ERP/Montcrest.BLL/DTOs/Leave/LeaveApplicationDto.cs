using Montcrest.DAL.Enums;

namespace Montcrest.BLL.DTOs.Leave
{
    public class LeaveApplicationDto
    {
        public int LeaveId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;

        public LeaveType LeaveType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; }

        public DateTime AppliedOn { get; set; }

        public string? ManagerRemarks { get; set; }
    }
}
