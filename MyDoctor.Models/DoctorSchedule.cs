using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDoctor.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int Id { get; set; }

        // ربط الجدول بمودل الطبيب
        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        // يوم العمل: (Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday)
        [Required]
        [Display(Name = "Day")]
        public DayOfWeek DayOfWeek { get; set; }

        // وقت بداية الدوام (مثلاً 09:00:00)
        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }

        // وقت نهاية الدوام (مثلاً 17:00:00)
        [Required]
        [Display(Name = "End Time")]
        public TimeSpan EndTime { get; set; }

        // مدة الكشف الواحد بالدقائق (مثلاً 20 أو 30 دقيقة)
        [Required]
        [Range(10, 120, ErrorMessage = "The duration of the examination must be between 10 and 120 minutes.")]
        [Display(Name = "(Examination duration (in minutes))")]
        public int SlotDurationMinutes { get; set; } = 30;

        // حالة اليوم (هل العيادة متاحة في هذا اليوم أم مغلقة)
        public bool IsAvailable { get; set; } = true;
    }
}