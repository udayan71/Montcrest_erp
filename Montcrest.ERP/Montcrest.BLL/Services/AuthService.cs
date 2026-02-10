using Montcrest.BLL.Helpers;
using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;
using Montcrest.DAL.Enums;


namespace Montcrest.BLL.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(User user, string password)
        {
            if (await _userRepository.ExistsByEmailAsync(user.Email))
            {
                throw new Exception("Email already exists");
            }

            user.PasswordHash = PasswordHasher.Hash(password);
            user.Role = UserRole.Candidate;
           
            await _userRepository.AddAsync(user);
        }

        public async Task<User?> ValidateUserAsync(string email,string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            if (!PasswordHasher.Verify(password, user.PasswordHash))
                return null;

            return user;

        }
    }
}
