using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MyDoctor.Models
{
        public class Department
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain English letters only.")]
            [Display(Name = "Specialization Name")]
            [MaxLength(100)]
            public string Name { get; set; } = string.Empty;

            [Display(Name = "Display Order")]
            [Range(1, 1000, ErrorMessage = "Display Order Must be from 1 to 1000 ")]
            public int DisplayOrder { get; set; }
        }
}

