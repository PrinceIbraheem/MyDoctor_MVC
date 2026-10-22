using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDoctor.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        // ربط الحجز بالطبيب والمعالج
        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        // ربط الحجز بالمريض
        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        // تاريخ ووقت الحجز
        [Required(ErrorMessage = "يرجى تحديد تاريخ ووقت الموعد")]
        [Display(Name = "تاريخ الموعد")]
        public DateTime AppointmentDate { get; set; }

        // قيمة رسوم الحجز / الكشف
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        // حالة الحجز: (PendingPayment, PaymentUnderReview, Confirmed, Cancelled, Completed)
        [StringLength(50)]
        public string Status { get; set; } = "PendingPayment";

        // رقم عملية التحويل في بنكك (يدخله المريض)
        [Display(Name = "رقم عملية بنكك")]
        [StringLength(100)]
        public string? BankakTransactionId { get; set; }

        // مسار صورة إشعار التحويل من بنكك
        public string? PaymentReceiptUrl { get; set; }

        // تاريخ رفع الإشعار
        public DateTime? PaymentSubmittedDate { get; set; }

        // ملاحظات المريض / الأعراض
        [Display(Name = "ملاحظات طبية / الأعراض")]
        public string? Notes { get; set; }

        // تاريخ إنشاء الحجز في النظام
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}