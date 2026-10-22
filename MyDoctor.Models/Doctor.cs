using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyDoctor.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; [System.Runtime.CompilerServices.CompilerGenerated] set; }

        [Required]
        [Display(Name = "Doctor Name")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Specialization / Title")]
        public string? Title { get; set; } 

        [Required]
        [Display(Name = "Bio / Description")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ImageUrl { get; set; } 

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        [ValidateNever] 
        public Department? Department { get; set; }

        [Display(Name = "Clinic")]
        public int ClinicId { get; set; }

        [ForeignKey("ClinicId")]
        [ValidateNever]
        public Clinic? Clinic { get; set; }
        // داخل مودل الدكتور Doctor.cs

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "ConsultationFee")]
        [Required(ErrorMessage = "Please specify the medical examination fee.")]
        public decimal ConsultationFee { get; set; }
        public ICollection<DoctorSchedule>? Schedules { get; set; }
    }
}