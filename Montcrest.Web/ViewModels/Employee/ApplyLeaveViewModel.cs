using System.ComponentModel.DataAnnotations;
using Montcrest.Web.Enums;
namespace Montcrest.Web.ViewModels.Employee
{
    public class ApplyLeaveViewModel
    {
        [Required]
        public LeaveType LeaveType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [Required]
        public string Reason { get; set; } = string.Empty;
    }
}
