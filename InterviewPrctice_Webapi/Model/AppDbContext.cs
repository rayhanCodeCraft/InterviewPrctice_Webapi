using Microsoft.EntityFrameworkCore;

namespace InterviewPrctice_Webapi.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Experiences)
                .WithOne(exp => exp.Employee)
                .HasForeignKey(exp => exp.EmployeeId);
        }
    }
}
