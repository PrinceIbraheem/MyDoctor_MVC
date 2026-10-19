using MyDoctor.DataAccess.DataConnection;
using MyDoctor.DataAccess.Repository.IRepository;
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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Department = new DepartmentRepository(_context);
            Doctor = new DoctorRepository(_context);
        }
       
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
