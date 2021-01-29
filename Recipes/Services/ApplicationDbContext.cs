using Microsoft.EntityFrameworkCore;

namespace Recipes.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
    }
}