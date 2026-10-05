using Microsoft.EntityFrameworkCore;
using MyDoctor.Models;
namespace MyDoctor.DataConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, DisplayOrder = 1, Name = "Pediatrics" },
                new Department { Id = 2, DisplayOrder = 2, Name = "Obstetrics & Gynecology" },
                new Department { Id = 3, DisplayOrder = 3, Name = "Dentistry" },
                new Department { Id = 4, DisplayOrder = 4, Name = "Dermatology & Cosmetics" },
                new Department { Id = 5, DisplayOrder = 5, Name = "Internal Medicine & Cardiology" }
                );
        }
    }
}
