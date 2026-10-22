using MyDoctor.DataAccess.DataConnection;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.DataAccess.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
        }
    }
}
