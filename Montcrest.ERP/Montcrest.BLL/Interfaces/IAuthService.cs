using Montcrest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montcrest.BLL.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(User user, string password);
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
