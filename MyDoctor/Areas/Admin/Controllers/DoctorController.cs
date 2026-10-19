using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;
using MyDoctor.Models.ViewModel;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Doctor/Index
        public IActionResult Index()
        {
            // جلب الأطباء مع تضمين جدول الأقسام (Include Department) لعرض اسم التخصص
            var objDoctorList = _unitOfWork.Doctor.GetAll(includeProperties: "Department").ToList();
            return View(objDoctorList);
        }

        // GET: Admin/Doctor/Upsert (Create / Edit)
        public IActionResult Upsert(int? id)
        {
            DoctorVM doctorVM = new()
            {
                DepartmentList = _unitOfWork.Department.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Doctor = new Doctor()
            };

            if (id == null || id == 0)
            {
                // Create Mode
                return View(doctorVM);
            }
            else
            {
                // Update Mode
                doctorVM.Doctor = _unitOfWork.Doctor.Get(u => u.Id == id);
                if (doctorVM.Doctor == null)
                {
                    return NotFound();
                }
                return View(doctorVM);
            }
        }

        // POST: Admin/Doctor/Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DoctorVM doctorVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string doctorPath = Path.Combine(wwwRootPath, @"images\doctors");

                    // حذف الصورة القديمة إذا كان الطبيب مسجلاً مسبقاً وله صورة
                    if (!string.IsNullOrEmpty(doctorVM.Doctor?.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(wwwRootPath, doctorVM.Doctor.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // حفظ الصورة الجديدة
                    using (var fileStream = new FileStream(Path.Combine(doctorPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    doctorVM.Doctor.ImageUrl = @"\images\doctors\" + fileName;
                }

                if (doctorVM.Doctor?.Id == 0)
                {
                    // إضافة طبيب جديد
                    _unitOfWork.Doctor.Add(doctorVM.Doctor);
                    TempData["success"] = "Doctor created successfully!";
                }
                else
                {
                    // تعديل بيانات طبيب موجود
                    _unitOfWork.Doctor.Update(doctorVM.Doctor);
                    TempData["success"] = "Doctor updated successfully!";
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // إعادة تعبئة القائمة المنسدلة في حال وجود خطأ في البيانات المدخلة
                doctorVM.DepartmentList = _unitOfWork.Department.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(doctorVM);
            }
        }

        // GET: Admin/Doctor/Delete (اختياري إذا أردت صفحة حذف مستقلة أو استخدمنا الـ Modal)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = _unitOfWork.Doctor.Get(u => u.Id == id, includeProperties: "Department");
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Admin/Doctor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _unitOfWork.Doctor.Get(u => u.Id == id);
            if (doctor != null)
            {
                // حذف صورة الطبيب من السيرفر عند الحذف النهائي
                if (!string.IsNullOrEmpty(doctor.ImageUrl))
                {
                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, doctor.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                _unitOfWork.Doctor.Remove(doctor);
                _unitOfWork.Save();

                TempData["success"] = "Doctor deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}