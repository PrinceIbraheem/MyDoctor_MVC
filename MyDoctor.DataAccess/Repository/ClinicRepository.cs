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
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private ApplicationDbContext _context;
        public ClinicRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
        }
    }
}
