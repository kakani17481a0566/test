using test.ViewModels.Roles;

namespace test.Services.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRolesAsync();
        Task CreateRoleAsync(RoleViewModel model);
        Task UpdateRoleAsync(RoleViewModel model);
        Task<bool> DeleteRoleAsync(int id);
        Task<RoleViewModel> GetRoleByIdAsync(int id);
    }
}
