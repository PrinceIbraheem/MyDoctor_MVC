using System.ComponentModel.DataAnnotations;

namespace MyDoctor.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string? Email { get; set; }

        [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        public string? Address { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
        public string? Gender { get; set; } // Male / Female

        [Display(Name = "National ID")]
        [MaxLength(20, ErrorMessage = "National ID cannot exceed 20 characters")]
        public string? NationalId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}