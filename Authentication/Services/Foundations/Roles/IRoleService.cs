using Authentication.Models.Entities.Roles;

namespace Authentication.Services.Foundations.Roles {
    public interface IRoleService 
    {
        ValueTask<List<Role>> SelectAllRolesAsync();
    }
}
