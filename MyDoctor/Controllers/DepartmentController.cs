using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.DataConnection;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .OrderBy(d => d.DisplayOrder)
                .ToListAsync();
            return View(departments);
        }
    }
}