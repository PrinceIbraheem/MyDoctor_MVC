using Microsoft.AspNetCore.Mvc;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Customer/Home/Index
        public IActionResult Index()
        {
            //  „ Ã·» Department Ê Clinic „⁄« ·Ì „ ≈ŸÂ«—Â„« ›Ì «·»ÿ«ﬁ…
            IEnumerable<Doctor> doctorList = _unitOfWork.Doctor.GetAll(includeProperties: "Department,Clinic");
            return View(doctorList);
        }

        // GET: Customer/Home/DoctorDetails/5
        public IActionResult DoctorDetails(int id)
        {
            // Ã·» »Ì«‰«  «·ÿ»Ì» „⁄ «·ﬁ”„ Ê«·⁄Ì«œ… · ›«’Ì· «·»—Ê›«Ì·
            var doctor = _unitOfWork.Doctor.Get(d => d.Id == id, includeProperties: "Department,Clinic");
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
    }
}