using Microsoft.EntityFrameworkCore;
using TravelPlanner.Objects.Entities;

namespace TravelPlanner.Objects.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
