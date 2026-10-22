using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyDoctor.DataAccess.Repository.IRepository;
using MyDoctor.Models;
using System.Security.Claims;

namespace MyDoctor.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookingController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // 1. GET: صفحة عرض شاشة الحجز
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // جلب قائمة الأطباء لملء الـ Dropdown في الواجهة
            var doctorsList = _unitOfWork.Doctor.GetAll();
            ViewBag.Doctors = new SelectList(doctorsList, "Id", "FullName"); // تأكد أن اسم خاصية الاسم في مودل الدكتور تطابق "FullName" أو الاسم لديك

            return View();
        }

        // 2. POST: استقبال بيانات الحجز ورفع الإشعار وحفظه في قاعدة البيانات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment, IFormFile? receiptFile)
        {
            // جلب الـ PatientId للمستخدم الحالي المسجل دخول
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            // (تأكد من طريقة جلب الـ PatientId حسب نظام الـ Identity لديك، أو ربطه مباشرة بجدول المرضى)
            // مثال افتراضي: 
            // appointment.PatientId = GetCurrentPatientId(claim.Value);

            // جلب سعر الكشف الخاص بالطبيب المختار لحفظه مع الحجز
            var doctor = _unitOfWork.Doctor.Get(d => d.Id == appointment.DoctorId);
            if (doctor != null)
            {
                appointment.Amount = doctor.ConsultationFee;
            }

            // تعيين الحالة الأولية للحجز
            appointment.Status = "PaymentUnderReview"; // قيد مراجعة إشعار بنكك
            appointment.CreatedDate = DateTime.Now;
            appointment.PaymentSubmittedDate = DateTime.Now;

            // معالجة رفع صورة إشعار بنكك
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (receiptFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(receiptFile.FileName);
                string receiptsPath = Path.Combine(wwwRootPath, @"uploads\receipts");

                if (!Directory.Exists(receiptsPath))
                {
                    Directory.Exists(receiptsPath);
                }

                using (var fileStream = new FileStream(Path.Combine(receiptsPath, fileName), FileMode.Create))
                {
                    await receiptFile.CopyToAsync(fileStream);
                }

                appointment.PaymentReceiptUrl = @"/uploads/receipts/" + fileName;
            }

            if (ModelState.IsValid)
            {
                // الحفظ باستخدام الـ Unit of Work
                _unitOfWork.Appointment.Add(appointment);
                _unitOfWork.Save();

                TempData["Success"] = "تم تقديم طلب الحجز بنجاح، جاري مراجعة إشعار بنكك.";
                return RedirectToAction(nameof(Index)); // أو توجيهه لصفحة حجوزاتي
            }

            // إذا حدث خطأ، نعيد تعبئة قائمة الأطباء ونرجع لنفس الصفحة
            var doctorsList = _unitOfWork.Doctor.GetAll();
            ViewBag.Doctors = new SelectList(doctorsList, "Id", "FullName", appointment.DoctorId);

            return View(appointment);
        }

        // 3. GET: دالة جلب الأوقات المتاحة عبر AJAX (المتوافقة مع الواجهة)
        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(int doctorId, DateTime date)
        {
            DayOfWeek selectedDay = date.DayOfWeek;

            // جلب دوام الطبيب في هذا اليوم
            var schedule = _unitOfWork.DoctorSchedule.Get(
                s => s.DoctorId == doctorId && s.DayOfWeek == selectedDay && s.IsAvailable
            );

            if (schedule == null)
            {
                return Json(new List<string>());
            }

            // توليد الفترات الزمنية
            List<TimeSpan> allSlots = new List<TimeSpan>();
            TimeSpan currentTime = schedule.StartTime;

            while (currentTime.Add(TimeSpan.FromMinutes(schedule.SlotDurationMinutes)) <= schedule.EndTime)
            {
                allSlots.Add(currentTime);
                currentTime = currentTime.Add(TimeSpan.FromMinutes(schedule.SlotDurationMinutes));
            }

            // جلب المواعيد المحجوزة مسبقاً في نفس اليوم
            var bookedAppointments =_unitOfWork.Appointment.GetAll(
                a => a.DoctorId == doctorId &&
                     a.AppointmentDate.Date == date.Date &&
                     a.Status != "Cancelled"
            );

            var bookedTimes = bookedAppointments.Select(a => a.AppointmentDate.TimeOfDay).ToList();

            // استبعاد الأوقات المحجوزة
            var availableSlots = allSlots
                .Where(slot => !bookedTimes.Contains(slot))
                .Select(slot => slot.ToString(@"hh\:mm"))
                .ToList();

            return Json(availableSlots);
        }
    }
}