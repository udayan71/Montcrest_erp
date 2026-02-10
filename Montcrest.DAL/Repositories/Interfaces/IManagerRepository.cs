using Montcrest.DAL.Models;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        Task<Manager?> GetByUserIdAsync(int userId);
        Task AddAsync(Manager manager);
    }
}
