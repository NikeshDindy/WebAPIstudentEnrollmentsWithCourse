using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIstudentEnrollmentsWithCourse.Data;
using WebAPIstudentEnrollmentsWithCourse.Model;

namespace WebAPIstudentEnrollmentsWithCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EnrollmentsController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);

            if (student == null || course == null) return NotFound();

            var enrollment = new Enrollment { StudentId = studentId, CourseId = courseId };
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"{student.Name} enrolled in {course.Title}" });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAll()
           => await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();

    }
}
