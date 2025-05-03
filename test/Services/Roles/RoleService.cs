using test.Data;
using test.Models;
using test.ViewModels.Roles;
using Microsoft.EntityFrameworkCore;

namespace test.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();
        }

        public async Task CreateRoleAsync(RoleViewModel model)
        {
            var role = new RolesModel { Name = model.Name };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RoleViewModel model)
        {
            var role = await _context.Roles.FindAsync(model.Id);
            if (role != null)
            {
                role.Name = model.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RoleViewModel> GetRoleByIdAsync(int id)
        {
            return await _context.Roles
                .Where(r => r.Id == id)
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
