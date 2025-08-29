using System.ComponentModel.DataAnnotations;

namespace WebAPIstudentEnrollmentsWithCourse.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        // Navigation
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
