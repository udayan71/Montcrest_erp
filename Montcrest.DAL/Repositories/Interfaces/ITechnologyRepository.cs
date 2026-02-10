using Montcrest.DAL.Models;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface ITechnologyRepository
    {
        Task<List<Technology>> GetAllAsync();
        Task<Technology?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
