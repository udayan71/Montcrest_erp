namespace Montcrest.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        // FK to User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // FK to Manager
        public int ManagerId { get; set; }
        public Manager Manager { get; set; } = null!;

        public DateTime JoinedOn { get; set; } = DateTime.UtcNow;

        public ICollection<LeaveApplication>? LeaveApplications { get; set; }
    }
}
