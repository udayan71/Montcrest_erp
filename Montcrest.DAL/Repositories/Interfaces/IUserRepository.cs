using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);

        Task<User?> GetByEmailAsync(string email);

        Task AddAsync(User user); 

        Task UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistsByEmailAsync(string Email);
    }
}
