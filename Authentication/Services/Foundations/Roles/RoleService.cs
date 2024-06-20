using Authentication.Models.Entities.Roles;
using Authentication.Repositories.Roles;

namespace Authentication.Services.Foundations.Roles
{
    sealed partial class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository) 
        {
            this.roleRepository = roleRepository;
        }

        public ValueTask<Role> AddRoleAsync(Role role) =>
            TryCatch(async () =>
            {
                ValidateRoleOnCreate(role);

                return await this.roleRepository.InsertRoleAsync(role);
            });

        public ValueTask<List<Role>> RetrieveAllRolesAsync() =>
            TryCatch(async () => await this.roleRepository.SelectAllRolesAsync());

        public ValueTask<Role> RetrieveRoleByRoleName(string roleName) =>
            TryCatch(async () =>
            {
                ValidateRoleName(roleName);

                return await this.roleRepository.SelectRoleByNameAsync(roleName);
            });

        //public ValueTask<Role> RetrieveRoleByIdAsync(Guid roleId) =>
        //    TryCatch(async () =>
        //    {
        //        ValidateRoleId(roleId);

        //        return await this.roleRepository.SelectRoleByIdAsync(roleId);
        //    });

        public ValueTask<Role> RemoveRoleByIdAsync(Guid roleId) =>
            TryCatch(async () =>
            {
                ValidateRoleId(roleId);

                Role role = await this.roleRepository.SelectRoleByIdAsync(roleId);

                return await this.roleRepository.DeleteRoleByIdAsync(role);
            });
    }
}
