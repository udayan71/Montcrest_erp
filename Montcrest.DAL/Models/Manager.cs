namespace Montcrest.DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }

        // FK to User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<Employee>? Employees { get; set; }
        public ICollection<LeaveApplication>? LeaveApplications { get; set; }
    }
}
