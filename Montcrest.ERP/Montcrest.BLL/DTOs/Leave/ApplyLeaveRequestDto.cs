using Montcrest.DAL.Enums;

namespace Montcrest.BLL.DTOs.Leave
{
    public class ApplyLeaveRequestDto
    {
        public LeaveType LeaveType { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
