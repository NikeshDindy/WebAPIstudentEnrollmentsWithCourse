namespace WebAPIstudentEnrollmentsWithCourse.Model
{
    public class EnrollmentDto
    {
        public int EnrollId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string StudentName { get; set; } = "";
        public string CourseName { get; set; } = "";
    }
}
