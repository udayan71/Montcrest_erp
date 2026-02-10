using Montcrest.DAL.Enums;

namespace Montcrest.BLL.DTOs.Manager
{
    public class ManagerLeaveApplicationDto
    {
        public int LeaveId { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;

        public LeaveType LeaveType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; }

        public DateTime AppliedOn { get; set; }

        public string? ManagerRemarks { get; set; }
    }
}
