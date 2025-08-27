using Microsoft.AspNetCore.Mvc;
using StudentGradeManagement.Data;
using StudentGradeManagement.Models;

namespace StudentGradeManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
            public IActionResult Index()
        {
        
        return View();
        }

        public IActionResult List()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

       
        public IActionResult Create() => View();

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View(student);
        }

        
        public IActionResult Edit(string studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null) return NotFound();
            return View(student);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            return View(student);
        }

        
        public IActionResult Delete(string studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null) return NotFound();
            return View(student);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(List));
        }

        
        public IActionResult Details(string studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student == null) return NotFound();
            return View(student);
        }
    }
}
