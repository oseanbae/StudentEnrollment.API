using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEnrollment.API.Models
{
    public class Enrollment
    {
        public long Id { get; set; }

        [Required, ForeignKey(nameof(Student))]
        public long StudentId { get; set; }
        [Required, ForeignKey(nameof(Course))]
        public long CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public required Student Student { get; set; }
        public required Course Course { get; set; }
    }
}
