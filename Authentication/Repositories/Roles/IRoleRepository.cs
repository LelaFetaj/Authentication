using Authentication.Models.Entities.Roles;

namespace Authentication.Repositories.Roles
{
    public interface IRoleRepository
    {
        ValueTask<Role> InsertRoleAsync(Role role);
        ValueTask<List<Role>> SelectAllRolesAsync();
        ValueTask<Role> SelectRoleByNameAsync(string roleName);
        ValueTask<Role> SelectRoleByIdAsync(Guid roleId);
        ValueTask<Role> DeleteRoleByIdAsync(Role role);
    }
}
