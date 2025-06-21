using Microsoft.EntityFrameworkCore;

namespace StatusManagerAPI.Models
{
    public class StatusManagerContext : DbContext
    {
        public StatusManagerContext(DbContextOptions<StatusManagerContext> contextOptions) : base(contextOptions)
        {
        }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 2, Name = "Inactive" },
                new Status { Id = 3, Name = "Pending" }
            );
        }
    }
}
