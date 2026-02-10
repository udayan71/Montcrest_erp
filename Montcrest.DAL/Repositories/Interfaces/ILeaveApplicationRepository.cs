using Montcrest.DAL.Models;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface ILeaveApplicationRepository
    {
        Task AddAsync(LeaveApplication leave);

        Task<List<LeaveApplication>> GetByEmployeeIdAsync(int employeeId);

        Task<List<LeaveApplication>> GetByManagerIdAsync(int managerId);

        Task<LeaveApplication?> GetByIdAsync(int leaveId);

        Task UpdateAsync(LeaveApplication leave);
    }
}
