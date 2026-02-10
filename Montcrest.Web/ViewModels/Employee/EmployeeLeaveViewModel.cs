using Montcrest.Web.Enums;

namespace Montcrest.Web.ViewModels.Employee
{
    public class EmployeeLeaveViewModel
    {
        public int LeaveId { get; set; }

        public LeaveType LeaveType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; }

        public DateTime AppliedOn { get; set; }

        public string? ManagerRemarks { get; set; }
    }
}
