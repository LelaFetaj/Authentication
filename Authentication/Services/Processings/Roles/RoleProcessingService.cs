using Authentication.Models.Entities.Roles;
using Authentication.Services.Foundations.Roles;

namespace Authentication.Services.Processings.Roles
{
    public class RoleProcessingService : IRoleProcessingService
    {
        private readonly IRoleService roleService;

        public RoleProcessingService(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public async ValueTask<Role> AddRoleAsync(string roleName)
        {
            var role = await this.roleService.RetrieveRoleByRoleName(roleName);

            //if (role is not null)
            //{
            //    throw "A role with this name already exists! Please choose another name!";
            //}

            Role newRole = new() { Name = roleName };

            return await this.roleService.AddRoleAsync(newRole);
        }

        public async ValueTask<List<Role>> RetrieveAllRolesAsync() =>
            await this.roleService.RetrieveAllRolesAsync();

        public async ValueTask<Role> RetrieveRoleByNameAsync(string roleName) =>
            await this.roleService.RetrieveRoleByRoleName(roleName);

        public async ValueTask<Role> RemoveRoleByIdAsync(Guid roleId) =>
            await this.roleService.RemoveRoleById(roleId);
    }
}