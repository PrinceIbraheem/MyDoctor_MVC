using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        // GET: Department
        public IActionResult Index()
        {
            // إذا كانت GetAll تعيد List مباشرة بدون Task
            var departments = _unitofwork.Department.GetAll()
                .OrderBy(d => d.DisplayOrder)
                .ToList();

            return View(departments);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                // بدون await لأنها عملية سريعة أو Synchronous في الـ Repository
                _unitofwork.Department.Add(department);
                _unitofwork.Save();

                TempData["success"] = "Specialty created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitofwork.Department.Get(u => u.Id == id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.Department.Update(department);
                    _unitofwork.Save();

                    TempData["success"] = "Specialty updated successfully!";
                }
                catch (Exception)
                {
                    var deptFromDb = _unitofwork.Department.Get(u => u.Id == department.Id);
                    if (deptFromDb == null)
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
            return View(department);
        }

        // GET: Department/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitofwork.Department.Get(u => u.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _unitofwork.Department.Get(u => u.Id == id);
            if (department != null)
            {
                _unitofwork.Department.Remove(department);
                _unitofwork.Save();

                TempData["success"] = "Specialty deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}