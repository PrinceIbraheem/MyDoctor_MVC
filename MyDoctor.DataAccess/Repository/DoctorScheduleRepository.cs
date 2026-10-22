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
    public class DoctorScheduleRepository : Repository<DoctorSchedule>, IDoctorScheduleRepository
    {
        private ApplicationDbContext _context;
        public DoctorScheduleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(DoctorSchedule doctorSchedule)
        {
            _context.DoctorSchedules.Update(doctorSchedule);
        }
    }
}
