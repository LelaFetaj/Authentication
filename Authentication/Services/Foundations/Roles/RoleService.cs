using Authentication.Models.Entities.Roles;
using Authentication.Repositories.Roles;

namespace Authentication.Services.Foundations.Roles 
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository) 
        {
            this.roleRepository = roleRepository;
        }

        public async ValueTask<Role> AddRoleAsync(Role role) => 
            await this.roleRepository.InsertRoleAsync(role);

        public async ValueTask<List<Role>> RetrieveAllRolesAsync() =>
            await this.roleRepository.SelectAllRolesAsync();

        public async ValueTask<Role> RetrieveRoleByRoleName(string roleName) =>
            await this.roleRepository.SelectRoleByNameAsync(roleName);

        public async ValueTask<Role> RemoveRoleById(Guid roleId)
        {
            Role maybeRole = await this.roleRepository.SelectRoleByIdAsync(roleId);

            return await this.roleRepository.DeleteRoleByIdAsync(maybeRole);
        }
    }
}
