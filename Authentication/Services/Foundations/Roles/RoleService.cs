using Authentication.Models.Entities.Roles;
using Authentication.Repositories.Roles;

namespace Authentication.Services.Foundations.Roles {
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository) 
        {
            this.roleRepository = roleRepository;
        }

        public async ValueTask<List<Role>> SelectAllRolesAsync() =>
            await this.roleRepository.SelectAllRolesAsync();
    }
}
