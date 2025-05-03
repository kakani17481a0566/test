using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Data
{
    public class ApplicationDbContext : DbContext // 👈 Add inheritance
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
    }
}
