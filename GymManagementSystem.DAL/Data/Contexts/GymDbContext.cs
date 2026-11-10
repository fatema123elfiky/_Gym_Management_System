using GymManagementSystem.DAL.Data.Configurations;
using GymManagementSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            // search for anything implement IEntityConfiguration in same project 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Plan> Plans { get; set; } // table , crud operations
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MemberSession> MemberSession { get; set; }
        public DbSet<MemberShip> MemberShip { get; set; }
    }
}
