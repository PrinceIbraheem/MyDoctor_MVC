using MyDoctor.Models;

namespace MyDoctor.DataAccess.Repository.IRepository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        void Update(Doctor obj);
    }
}