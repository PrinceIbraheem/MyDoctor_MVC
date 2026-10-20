using System.ComponentModel.DataAnnotations;

namespace MyDoctor.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // رابط الموقع المباشر من خرائط جوجل
        public string? MapUrl { get; set; }

        // اسم المنطقة أو المدينة للعرض السريع
        public string? CityOrRegion { get; set; }
    }
}