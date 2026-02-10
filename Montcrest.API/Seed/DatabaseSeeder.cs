using Montcrest.BLL.Interfaces;
using Montcrest.DAL.Context;
using Montcrest.DAL.Enums;
using Montcrest.DAL.Models;
using Montcrest.DAL.Repositories.Interfaces;

namespace Montcrest.API.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            var context = scope.ServiceProvider.GetRequiredService<MontcrestDbContext>();

            // ---------------- HR Seed ----------------
            var hrEmail = "hr@montcrest.com";

            var existingHr = await userRepository.GetByEmailAsync(hrEmail);
            if (existingHr == null)
            {
                var hrUser = new User
                {
                    FullName = "Montcrest HR",
                    Email = hrEmail,
                    MobileNumber = "9999999999",
                    Address = "Montcrest Office",
                    Role = UserRole.HR
                };

                await authService.RegisterAsync(hrUser, "Hr@123");
            }

            // ---------------- MANAGER Seed ----------------
            var managerEmail = "manager@montcrest.com";

            var existingManagerUser = await userRepository.GetByEmailAsync(managerEmail);

            if (existingManagerUser == null)
            {
                var managerUser = new User
                {
                    FullName = "Montcrest Manager",
                    Email = managerEmail,
                    MobileNumber = "8888888888",
                    Address = "Montcrest Office",
                    Role = UserRole.Manager
                };

                await authService.RegisterAsync(managerUser, "Manager@123");

                // After RegisterAsync, fetch again (to get ID)
                existingManagerUser = await userRepository.GetByEmailAsync(managerEmail);
            }

            // Insert into Managers table
            var existingManagerRow = context.Managers
                .FirstOrDefault(m => m.UserId == existingManagerUser!.Id);

            if (existingManagerRow == null)
            {
                var manager = new Manager
                {
                    UserId = existingManagerUser.Id
                };

                context.Managers.Add(manager);
                await context.SaveChangesAsync();
            }
        }
    }
}
