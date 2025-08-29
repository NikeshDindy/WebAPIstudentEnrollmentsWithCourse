using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIstudentEnrollmentsWithCourse.Data;
using WebAPIstudentEnrollmentsWithCourse.Model;

namespace WebAPIstudentEnrollmentsWithCourse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CoursesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
            => await _context.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student).ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Course>> Create(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = course.Id }, course);
        }
    }
}
