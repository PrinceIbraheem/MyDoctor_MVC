using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Models;
namespace MyDoctor.DataAccess.DataConnection
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet <DoctorSchedule> DoctorSchedules { get; set; }

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
                     DepartmentId = 1,
                     ClinicId=1,
                     ConsultationFee=15000
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
                      DepartmentId = 2,
                      ClinicId=2,
                      ConsultationFee=20000
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
                      DepartmentId = 3,
                      ClinicId=3,
                      ConsultationFee=30000
                  }
                );
            modelBuilder.Entity<Clinic>().HasData(
                new Clinic
                {
                    Id = 1,
                    Name = "عيادة الأطفال التخصصية",
                    CityOrRegion = "الرياض - حي النفل",
                    MapUrl = "https://maps.google.com/?q=24.7136,46.6753"
                },
                new Clinic
                {
                    Id = 2,
                    Name = "عيادة الباطنية والقلب",
                    CityOrRegion = "جدة - حي الزهراء",
                    MapUrl = "https://maps.google.com/?q=21.5433,39.1728"
                },
                new Clinic
                {
                    Id = 3,
                    Name = "عيادة الباطنية والقلب",
                    CityOrRegion = "جدة - حي الزهراء",
                    MapUrl = "https://maps.google.com/?q=21.5433,39.1728"
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "Yssir",
                    Gender = "Male",
                    NationalId = "1029384756",
                    PhoneNumber = "009665605802",
                    Email = "MMMOOOO@gmail.com",
                    Address ="jeddah",
                    DateOfBirth = new DateTime(2026, 1, 1),
                    CreatedDate = new DateTime(2026, 1, 1)
                }
            );
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    DoctorId = 1, // يجب أن يكون هناك طبيب بهذه المعرف في جدول الأطباء
                    PatientId = 1, // يجب أن يكون هناك مريض بهذا المعرف في جدول المرضى
                    AppointmentDate = new DateTime(2026, 11, 1, 10, 0, 0),
                    Amount = 15000.00m, // قيمة الكشف بالجنيه
                    Status = "PaymentUnderReview", // حالة الحجز: قيد مراجعة إشعار بنكك
                    BankakTransactionId = "TXN987654321", // رقم عملية بنكك المرفوع
                    PaymentReceiptUrl = "/uploads/receipts/receipt_1.jpg", // مسار صورة الإشعار
                    PaymentSubmittedDate = new DateTime(2026, 10, 21, 14, 30, 0),
                    Notes = "استشارة طبية بخصوص آلام في الظهر",
                    CreatedDate = new DateTime(2026, 10, 21, 14, 0, 0)

                }
                );
            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule 
                {
                Id = 1,
                DoctorId = 1,
                DayOfWeek = DayOfWeek.Sunday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(14, 0, 0),
                SlotDurationMinutes = 30,
                IsAvailable = true

                },
                new DoctorSchedule
                {
                    Id = 2,
                    DoctorId = 1,
                    DayOfWeek = DayOfWeek.Monday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(14, 0, 0),
                    SlotDurationMinutes = 30,
                    IsAvailable = true
                },
                new DoctorSchedule
                {
                    Id = 3,
                    DoctorId = 1,
                    DayOfWeek = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(14, 0, 0),
                    SlotDurationMinutes = 30,
                    IsAvailable = true
                }

                );

        }
    }

}