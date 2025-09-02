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
        public async Task<IActionResult> Enroll([FromBody] EnrollmentDto dto)
        {
            var student = await _context.Students.FindAsync(dto.StudentId);
            var course = await _context.Courses.FindAsync(dto.CourseId);

            if (student == null || course == null)
                return NotFound("Student or Course not found");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            // return full DTO so frontend can update immediately
            var result = new EnrollmentDto
            {
                EnrollId = enrollment.EnrollId,
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                StudentName = student.Name,
                CourseName = course.Title
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e => new EnrollmentDto
                {
                    EnrollId = e.EnrollId,
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    StudentName = e.Student.Name,
                    CourseName = e.Course.Title
                })
                .ToListAsync();

            return Ok(enrollments);
        }
    }
}
