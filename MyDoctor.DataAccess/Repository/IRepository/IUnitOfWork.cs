using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Department { get; }
        IDoctorRepository Doctor { get; }
        IClinicRepository Clinic { get; }
        IPatientRepository Patient { get; }
        IAppointmentRepository Appointment { get; }
        IDoctorScheduleRepository DoctorSchedule { get; }
        void Save();
    }
}
