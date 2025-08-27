using Microsoft.AspNetCore.Mvc;
using StudentGradeManagement.Data;
using StudentGradeManagement.Models;
using System.Linq;

namespace StudentGradeManagement.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Index(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                ModelState.AddModelError("", "Please enter a student ID.");
                return View();
            }

            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                ModelState.AddModelError("", "Student not found.");
                return View();
            }

            return RedirectToAction("Result", new { studentId = student.StudentId });
        }

        
        public IActionResult Result(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                return NotFound();

            var student = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
                return NotFound();

            
            int term1Total = student.Tamil_T1 + student.English_T1 + student.Maths_T1 + student.Science_T1 + student.Social_T1;
            int term2Total = student.Tamil_T2 + student.English_T2 + student.Maths_T2 + student.Science_T2 + student.Social_T2;
            int term3Total = student.Tamil_T3 + student.English_T3 + student.Maths_T3 + student.Science_T3 + student.Social_T3;

           
            string term1To2 = term2Total > term1Total ? "Improved from Term 1 to Term 2." :
                              term2Total == term1Total ? "Performance same from Term 1 to Term 2." :
                              "Needs improvement from Term 1 to Term 2.";

            string term2To3 = term3Total > term2Total ? "Improved from Term 2 to Term 3." :
                              term3Total == term2Total ? "Performance same from Term 2 to Term 3." :
                              "Needs improvement from Term 2 to Term 3.";

            ViewBag.Notification = term1To2 + " " + term2To3;

            return View(student);
        }
    }
}
