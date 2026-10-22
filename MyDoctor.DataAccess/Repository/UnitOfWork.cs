using MyDoctor.DataAccess.DataConnection;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IDepartmentRepository Department { get; private set; }

        public IDoctorRepository Doctor { get; private set; }
        public IClinicRepository Clinic { get; private set; }
        public IPatientRepository Patient { get; private set; }
        public IAppointmentRepository Appointment { get; private set; }
        public IDoctorScheduleRepository DoctorSchedule { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Department = new DepartmentRepository(_context);
            Doctor = new DoctorRepository(_context);
            Clinic=new ClinicRepository(_context);
            Patient=new PatientRepository(_context);
            Appointment=new AppointmentRepository(_context);
            DoctorSchedule=new DoctorScheduleRepository(_context);
            
        }
       
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
