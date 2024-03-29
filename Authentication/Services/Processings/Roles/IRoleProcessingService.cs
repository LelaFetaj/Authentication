using Authentication.Models.Entities.Roles;

namespace Authentication.Services.Processings.Roles
{
    public interface IRoleProcessingService
    {
        ValueTask<Role> AddRoleAsync(string roleName);
        ValueTask<List<Role>> RetrieveAllRolesAsync();
        ValueTask<Role> RetrieveRoleByNameAsync(string roleName);
        ValueTask<Role> RemoveRoleByIdAsync(Guid roleid);
    }
}