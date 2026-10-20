using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClinicController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public ClinicController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        // GET: Department
        public IActionResult Index()
        {
            // إذا كانت GetAll تعيد List مباشرة بدون Task
            var clinic = _unitofwork.Clinic.GetAll()
                .OrderBy(d => d.Id)
                .ToList();

            return View(clinic);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Clinic clinic)
        {
            if (ModelState.IsValid)
            {

                _unitofwork.Clinic.Add(clinic);
                _unitofwork.Save();

                TempData["success"] = "Clinic created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = _unitofwork.Clinic.Get(u => u.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Clinic clinic)
        {
            if (id != clinic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.Clinic.Update(clinic);
                    _unitofwork.Save();

                    TempData["success"] = "Clinic updated successfully!";
                }
                catch (Exception)
                {
                    var clinfromdb = _unitofwork.Clinic.Get(u => u.Id == clinic.Id);
                    if (clinfromdb == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinic = _unitofwork.Clinic.Get(u => u.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var clinic = _unitofwork.Clinic.Get(u => u.Id == id);
            if (clinic != null)
            {
                _unitofwork.Clinic.Remove(clinic);
                _unitofwork.Save();

                TempData["success"] = "Clinic deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}