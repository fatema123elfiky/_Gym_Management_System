using GymManagementSystem.DAL.Data.Configurations;
using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.DAL.Data.Contexts
{
    public class GymDbContext : DbContext

    {

        // instead of onconfiguring !!
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlanConfigurations());
        }

        public DbSet<Plan> Plans { get; set; } // table , crud operations
    }
}
