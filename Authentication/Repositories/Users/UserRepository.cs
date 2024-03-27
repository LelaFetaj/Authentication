using Authentication.Models.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Authentication.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async ValueTask<User> InsertUserAsync(User user, string password)
        {
            var broker = new UserRepository(this.userManager);
            await broker.userManager.CreateAsync(user, password);

            return user;
        }

        public async ValueTask<List<User>> SelectAllUsersAsync()
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.Users.ToListAsync();
        }

        public async ValueTask<User> SelectUserByIdAsync(Guid userId)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.FindByIdAsync(userId.ToString());
        }

        public async ValueTask<User> SelectUserByUsernameAsync(string username)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.FindByNameAsync(username);
        }

        public async ValueTask<User> UpdateUserAsync(User user)
        {
            var broker = new UserRepository(this.userManager);
            await broker.userManager.UpdateAsync(user);

            return user;
        }

        public async ValueTask<User> DeleteUserAsync(User user)
        {
            var broker = new UserRepository(this.userManager);
            await broker.userManager.DeleteAsync(user);

            return user;
        }

        public async ValueTask<IdentityResult> AddToRoleAsync(User user, string  roleName)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.AddToRoleAsync(user, roleName);
        }

        public async ValueTask<IdentityResult> RemoveFromRoleAsync(User user, string role)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.RemoveFromRoleAsync(user, role);
        }

        public async ValueTask<bool> IsInRoleAsync(User user, string role)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.IsInRoleAsync(user, role);
        }

        public async ValueTask<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.GetUsersInRoleAsync(roleName);
        }

        public async ValueTask<IEnumerable<string>> GetUserRoles(User user)
        {
            var broker = new UserRepository(this.userManager);

            return await broker.userManager.GetRolesAsync(user);
        }
    }
}
