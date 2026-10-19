using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyDoctor.Models.ViewModel
{
    public class DoctorVM
    {
        public Doctor? Doctor { get; set; }

        [ValidateNever]
        public required IEnumerable<SelectListItem> DepartmentList { get; set; }
    }
}