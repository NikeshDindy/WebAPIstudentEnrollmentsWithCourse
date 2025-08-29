using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPIstudentEnrollmentsWithCourse.Model
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }



        [JsonIgnore]
        public Student Student { get; set; } = null!;

        [JsonIgnore]
        public Course Course { get; set; } = null!;
    }
}
