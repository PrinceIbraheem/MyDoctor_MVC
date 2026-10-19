using Microsoft.EntityFrameworkCore;
using MyDoctor.Models;
namespace MyDoctor.DataAccess.DataConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, DisplayOrder = 1, Name = "Pediatrics" },
                new Department { Id = 2, DisplayOrder = 2, Name = "Obstetrics & Gynecology" },
                new Department { Id = 3, DisplayOrder = 3, Name = "Dentistry" },
                new Department { Id = 4, DisplayOrder = 4, Name = "Dermatology & Cosmetics" },
                new Department { Id = 5, DisplayOrder = 5, Name = "Internal Medicine & Cardiology" }
                );
            //Seed Doctor Table
            modelBuilder.Entity<Doctor>().HasData(
                 new Doctor
                 {
                     Id = 1,
                     Name = "Dr. Rawia Abdelrahman",
                     Title = "Specialist in Internal Medicine",
                     Description = "Holder of the British Fellowship. Faculty of Medicine, University of Khartoum.",
                     PhoneNumber = "+966500000000",
                     Email = "rawia@example.com",
                     ImageUrl = "/images/doctors/rawia.jpg",
                     DepartmentId = 1
                 },
                  new Doctor
                  {
                      Id = 2,
                      Name = "Dr. Ahmed Mohamed",
                      Title = "Consultant in Pediatrics",
                      Description = "Experienced pediatric consultant providing comprehensive healthcare for children.",
                      PhoneNumber = "+966511111111",
                      Email = "ahmed@example.com",
                      ImageUrl = "/images/doctors/ahmed.jpg",
                      DepartmentId = 2
                  },
                  new Doctor
                  {
                      Id = 3,
                      Name = "Dr. Sara Ali",
                      Title = "Specialist in Dermatology",
                      Description = "Specialist in diagnosing and treating skin conditions.",
                      PhoneNumber = "+966522222222",
                      Email = "sara@example.com",
                      ImageUrl = "/images/doctors/sara.jpg",
                      DepartmentId = 3
                  }
                );
        }
    }
}
