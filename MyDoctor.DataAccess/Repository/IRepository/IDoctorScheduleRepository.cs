using MyDoctor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.DataAccess.Repository.IRepository
{
    public interface IDoctorScheduleRepository : IRepository<DoctorSchedule>
    {
        void Update(DoctorSchedule doctorSchedule);
    }
}
