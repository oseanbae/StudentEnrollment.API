namespace StudentEnrollment.API.Models
{
    public class Enrollment
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public required Student Student { get; set; }
        public required Course Course { get; set; }
    }
}
