using Authentication.Models.Entities.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Repositories.Roles {
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> roleManager;

        public RoleRepository(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async ValueTask<Role> InsertRoleAsync(Role role)
        {
            var broker = new RoleRepository(this.roleManager);
            await broker.roleManager.CreateAsync(role);

            return role;
        }

        public async ValueTask<List<Role>> SelectAllRolesAsync()
        {
            var broker = new RoleRepository(this.roleManager);

            return await broker.roleManager.Roles.ToListAsync();
        }

        public async ValueTask<Role> SelectRoleByNameAsync(string roleName)
        {
            var broker = new RoleRepository(this.roleManager);

            return await broker.roleManager.FindByNameAsync(roleName);
        }
    }
}
