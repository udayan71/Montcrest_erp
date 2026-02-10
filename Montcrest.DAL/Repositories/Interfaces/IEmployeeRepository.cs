using Montcrest.DAL.Models;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByUserIdAsync(int userId);
        Task<Employee?> GetByIdAsync(int employeeId);
    }
}
