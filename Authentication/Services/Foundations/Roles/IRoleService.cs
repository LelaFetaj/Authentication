using Authentication.Models.Entities.Roles;

namespace Authentication.Services.Foundations.Roles {
    public interface IRoleService 
    {
        ValueTask<Role> AddRoleAsync(Role role);
        ValueTask<List<Role>> RetrieveAllRolesAsync();
        ValueTask<Role> RetrieveRoleByRoleName(string roleName);
        ValueTask<Role> RemoveRoleById(Guid roleId);
    }
}
